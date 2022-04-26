using CefSharp;
using CefSharp.WinForms;
using Constituency.Desktop.Components;
using Constituency.Desktop.Controls;
using Constituency.Desktop.Entities;
using Constituency.Desktop.Helpers;
using Constituency.Desktop.Models;
using Constituency.Desktop.Properties;
using Microsoft.AppCenter.Crashes;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace Constituency.Desktop.Views
{
    public partial class Frm_Houses : Form
    {
        //Fields
        private string token = string.Empty;

        //objects
        User user { get; set; }
        Voter Voter { get; set; }
        House House { get; set; }

        WaitFormFunc waitForm = new WaitFormFunc();

        private ObservableCollection<House> HousesList;
        private List<ConstituencyC> ConstituenciesList;
        private List<PollingDivision> PollingDivisionsList;
        ChromiumWebBrowser browser;

        public Frm_Houses()
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            InitializeComponent();
            InitializeChromium();
            token = Main.GetInstance().tokenResponse.Token;
            user = Main.GetInstance().tokenResponse.User;
        }
        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            if (!Cef.IsInitialized)
            {
                Cef.Initialize(settings);
            }

            browser = new ChromiumWebBrowser("about:blank");
            this.panel2.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
        }

        private async void Frm_ElectionVotes_Load(object sender, EventArgs e)
        {
            FildsValidations();
            MandatoriesFilds();
            DGVFormats();
            UtilRecurrent.LockForm(waitForm, this);
            await LoadConstituencies();
            await LoadHouses();
            UtilRecurrent.UnlockForm(waitForm, this);
            ibtnUpdate.Visible = false;

        }
        #region Mandatories and Validations
        private void DGVFormats()
        {//tag= 1 para campos obligados

            UtilRecurrent.FindAllControlsIterative(tabControl1, "DataGridView").Cast<DataGridView>().ToList().ForEach(x => x.DefaultCellStyle.BackColor = Color.Beige);
            UtilRecurrent.FindAllControlsIterative(tabControl1, "DataGridView").Cast<DataGridView>().ToList().ForEach(x => x.AlternatingRowsDefaultCellStyle.BackColor = Color.Bisque);
            UtilRecurrent.FindAllControlsIterative(tabControl1, "DataGridView").Cast<DataGridView>().ToList().ForEach(x => x.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells);
            UtilRecurrent.FindAllControlsIterative(tabControl1, "DataGridView").Cast<DataGridView>().ToList().ForEach(x => x.RowHeadersVisible = false);
            UtilRecurrent.addBottomColumns(dgvIVoters, "DeleteCol", "Delete");
            dgvIVoters.Columns[0].Frozen = true;

        }
        private async void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 | e.ColumnIndex < 0)
                {
                    return;
                }

                var senderGrid = (DataGridView)sender;
                int rowId = System.Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["Id"].Value);
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    if (senderGrid.Columns[e.ColumnIndex].Name == "DeleteCol")
                    {
                        if (UtilRecurrent.yesOrNot("Are you sure to remove the selected voter from this house?", "Remove Voter"))
                        {
                            if (await DeleteVoterFromHouse(rowId))
                            {
                                await LoadHouses();
                                if (House != null && House.Id > 0)
                                {
                                    tView1.SelectedNode = CollectAllNodes(tView1.Nodes).FirstOrDefault(x => x.Tag.ToString() == "H-" + House.Id);

                                }
                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async Task<bool> DeleteVoterFromHouse(int id)
        {
            try
            {

                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PutAsync("Houses/RemoveVoter", House, id, token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response.IsSuccess;
                }
                return true;
            }
            catch (Exception ex)
            {
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }
        }
        private void dgv1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                DataGridView senderGrid = (DataGridView)sender;
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex >= 0)
                {
                    if (senderGrid.Columns[e.ColumnIndex].Name == "DeleteCol")
                    {
                        e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                        var w = Resources.sto.Width;
                        var h = Resources.sto.Height;
                        int x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                        int y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;
                        e.Graphics.DrawImage(Resources.sto, new Rectangle(x, y, w, h));
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void FildsValidations()
        {
            try
            {
                //tag=2 para only numbers
                UtilRecurrent.FindAllControlsIterative(tabPage1, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("2")).ToList().ForEach(x => x.KeyPress += UtilRecurrent.txtOnlyIntegersNumber_KeyPress);

                //tag=3 para only letters
                UtilRecurrent.FindAllControlsIterative(tabPage1, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("3")).ToList().ForEach(x => x.KeyPress += UtilRecurrent.txtOnlyLetters_KeyPress);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void MandatoriesFilds()
        {//tag= 1 para campos obligados

            try
            {
                UtilRecurrent.FindAllControlsIterative(tabPage1, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1")).ToList().ForEach(x => toolTip1.SetToolTip(x, "Mandatory Field"));
                UtilRecurrent.FindAllControlsIterative(tabPage1, "ComboBox").Cast<ComboBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1")).ToList().ForEach(x => toolTip1.SetToolTip(x, "Mandatory Field"));
                toolTip1.SetToolTip(dtpDOB, "Mandatory Field");


            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void FieldsWhite()
        {
            try
            {
                //todos en blanco antes de comprobar
                UtilRecurrent.FindAllControlsIterative(tabPage1, "TextBox").Cast<TextBox>().ToList().ForEach(t => t.BackColor = Color.FromKnownColor(KnownColor.Window));

                UtilRecurrent.FindAllControlsIterative(tabPage1, "ComboBox").Cast<ComboBox>().ToList().ForEach(t => t.BackColor = Color.FromKnownColor(KnownColor.Window));
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private bool PaintRequired()
        {
            bool missing = false;
            try
            {
                FieldsWhite();
                //validando textbox requeridos no vacios
                if (UtilRecurrent.FindAllControlsIterative(tabPage1, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1") && (x.TextLength == 0 || string.IsNullOrWhiteSpace(x.Text))).ToList().Any())
                {
                    UtilRecurrent.FindAllControlsIterative(tabPage1, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1") && (x.TextLength == 0 || string.IsNullOrWhiteSpace(x.Text))).ToList().ForEach(t => t.BackColor = Color.LightSalmon);
                    missing = true;
                }
                if (UtilRecurrent.FindAllControlsIterative(tabPage1, "ComboBox").Cast<ComboBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1") && x.Text == string.Empty).ToList().Any())
                {
                    UtilRecurrent.FindAllControlsIterative(tabPage1, "ComboBox").Cast<ComboBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1") && x.Text == string.Empty).ToList().ForEach(t => t.BackColor = Color.LightSalmon);
                    missing = true;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
            return missing;
        }
        #endregion

        #region Load Info
        private async Task<List<string>> LoadUsersRoles(User user)
        {
            Response response = await ApiServices.GetUserRoles<IList<string>>("Users/Roles", user.UserName, token);
            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return null;
            }
            return (List<string>)response.Result;
        }
        private async Task LoadHouses()
        {
            try
            {
                Response response = await ApiServices.GetListAsync<House>("Houses", token);

                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return;
                }
                HousesList = new ObservableCollection<House>((List<House>)response.Result);
                if (HousesList != null && HousesList.Any())
                {
                    RefreshTreeView(HousesList.ToList());

                }
                else
                {
                    cleanScreen();
                }
            }
            catch (Exception ex)
            {
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }

        private async Task LoadConstituencies()
        {
            Response response = await ApiServices.GetListAsync<ConstituencyC>("Constituencies", token);
            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return;
            }
            ConstituenciesList = new((List<ConstituencyC>)response.Result);
            if (ConstituenciesList.Any())
            {
                cmbConstituency.DataSource = null;
                cmbConstituency.DataSource = ConstituenciesList;
                cmbConstituency.ValueMember = "Id";
                cmbConstituency.DisplayMember = "Name";
                cmbConstituency.SelectedItem = null;
            }
        }
        private void RefreshTreeView(List<House> houses)
        {
            try
            {
                //todo me esta mostrando todas las constituencies incluso sino hay casa en ellas arreglar esto
                tView1.Nodes.Clear();
                //TreeNode node;
                List<TreeNode> treeNodes = new List<TreeNode>();
                List<TreeNode> childNodes = new List<TreeNode>();
                List<TreeNode> childNodes2 = new List<TreeNode>();

                foreach (ConstituencyC consty in ConstituenciesList)
                {
                    int cant2 = houses.Select(h => h.Voters.Select(v => v.PollingDivision.Constituency).Where(c => c.Id == consty.Id)).Count();
                    foreach (PollingDivision division in consty.PollingDivisions)
                    {
                        foreach (House house in houses.Where(h => h.Voters.Where(v => v.PollingDivision.Id == division.Id).Any()).ToList())
                        {
                            TreeNode node2 = new TreeNode(house.Number, 2, 3);
                            node2.Tag = "H-" + house.Id;
                            childNodes2.Add(node2);
                        }
                        if (childNodes2.Any())
                        {
                            childNodes.Add(new TreeNode(division.Name + " [" + childNodes2.Count + "]", 4, 1, childNodes2.ToArray()));
                            childNodes[childNodes.Count - 1].Tag = "D-" + division.Id;
                            childNodes2 = new List<TreeNode>();
                        }
                    }
                    if (childNodes.Any())
                    {
                        treeNodes.Add(new TreeNode(consty.Name + " [" + cant2 + "]", 0, 1, childNodes.ToArray()));
                        treeNodes[treeNodes.Count - 1].Tag = "C-" + consty.Id;
                        childNodes = new List<TreeNode>();
                    }
                }
                List<TreeNode> treeNodesNoVoter = new List<TreeNode>();
                List<TreeNode> childNodesNoVoter = new List<TreeNode>();
                foreach (House house in houses.Where(h => h.Voters == null || !h.Voters.Any()))
                {
                    TreeNode node2 = new TreeNode(house.Number, 2, 3);
                    node2.Tag = "H-" + house.Id;
                    childNodesNoVoter.Add(node2);
                }
                treeNodesNoVoter.Add(new TreeNode("No Voters Hosues", 0, 1, childNodesNoVoter.ToArray()));
                treeNodesNoVoter[treeNodesNoVoter.Count - 1].Tag = "X-" + int.MaxValue;
                tView1.Nodes.AddRange(treeNodes.ToArray());
                if (treeNodesNoVoter.Any())
                {
                    tView1.Nodes.AddRange(treeNodesNoVoter.ToArray());
                }
                tView1.ExpandAll();
                rjCollapseAll.Checked = true;
                lblExpand.Text = "Collapse All";
                tView1.Font = new Font("Segoe UI", 12, FontStyle.Regular);
                groupBox1.Text = "Election Votes List: ( " + HousesList.Count.ToString("N0") + " )";
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        //private void RefreshTreeView(List<House> houses)
        //{
        //    try
        //    {
        //        tView1.Nodes.Clear();
        //        //TreeNode node;
        //        List<TreeNode> treeNodes = new List<TreeNode>();
        //        List<TreeNode> childNodes = new List<TreeNode>();
        //        List<TreeNode> childNodes2 = new List<TreeNode>();

        //        foreach (ConstituencyC consty in houses.SelectMany(h => h.Voters.Select(v => v.PollingDivision.Constituency).Distinct().ToList()))
        //        {
        //            //find the amount of houses that belongs to consty

        //            int cant2 = houses.Select(h => h.Voters.Select(v => v.PollingDivision.Constituency).Where(c => c.Id == consty.Id)).Count();
        //            foreach (PollingDivision division in consty.PollingDivisions.Distinct())
        //            {
        //                int cant = houses.Select(h => h.Voters.Select(v => v.PollingDivision.Id == division.Id)).Count();
        //                //find the houses with voters in the same division 
        //                List<House> houses2 = houses.Where(h => h.Voters.Where(v => v.PollingDivision.Id == division.Id).Any()).ToList();
        //                //find the houses with voters in the same division that division


        //                foreach (House house in houses.Where(h => h.Voters.Where(v => v.PollingDivision.Id == division.Id).Any()).ToList())
        //                {
        //                    TreeNode node2 = new TreeNode(house.Number, 2, 3);
        //                    node2.Tag = house.Id;
        //                    childNodes2.Add(node2);
        //                }

        //                if (childNodes2.Any())
        //                {
        //                    childNodes.Add(new TreeNode(division.Name + " [" + cant + "]", 0, 1, childNodes2.ToArray()));
        //                    childNodes[childNodes.Count - 1].Tag = division.Id;
        //                    childNodes2 = new List<TreeNode>();
        //                }
        //            }
        //            treeNodes.Add(new TreeNode(consty.Name + " [" + cant2 + "]", 0, 1, childNodes.ToArray()));
        //            treeNodes[treeNodes.Count - 1].Tag = consty.Id;
        //            childNodes = new List<TreeNode>();
        //        }
        //        List<TreeNode> treeNodesNoVoter = new List<TreeNode>();
        //        List<TreeNode> childNodesNoVoter = new List<TreeNode>();
        //        foreach (House house in houses.Where(h => h.Voters == null || !h.Voters.Any()))
        //        {
        //            TreeNode node2 = new TreeNode(house.Number, 2, 3);
        //            node2.Tag = house.Id;
        //            childNodesNoVoter.Add(node2);

        //        }
        //        treeNodesNoVoter.Add(new TreeNode("No Voters Hosues", 0, 1, childNodesNoVoter.ToArray()));
        //        treeNodesNoVoter[treeNodesNoVoter.Count - 1].Tag = 0;


        //        tView1.Nodes.AddRange(treeNodes.ToArray());

        //        if (treeNodesNoVoter.Any())
        //        {
        //            tView1.Nodes.AddRange(treeNodesNoVoter.ToArray());
        //        }
        //        tView1.ExpandAll();
        //        rjCollapseAll.Checked = true;
        //        lblExpand.Text = "Collapse All";
        //        tView1.Font = new Font("Segoe UI", 12, FontStyle.Regular);
        //        groupBox1.Text = "Election Votes List: ( " + HousesList.Count.ToString("N0") + " )";
        //    }
        //    catch (Exception ex)
        //    {
        //        Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
        //    }
        //}
        private IEnumerable<TreeNode> CollectAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                yield return node;
                foreach (var child in CollectAllNodes(node.Nodes))
                {
                    yield return child;
                }
            }
        }

        private void cleanScreen()
        {
            try
            {
                UtilRecurrent.FindAllControlsIterative(this.tpanelVoter, "TextBox").Cast<TextBox>().ToList().ForEach(x => x.Clear());
                //UtilRecurrent.FindAllControlsIterative(this.tpanelVoter, "ComboBox").Cast<ComboBox>().ToList().ForEach(x => x.SelectedIndex = -1);
                UtilRecurrent.FindAllControlsIterative(this.tpanelVoter, "DateTimePicker").Cast<DateTimePicker>().ToList().ForEach(x => x.Value = DateTime.Now.Date);

                cmbConstituency.SelectedIndex = -1;
                cmbDivision.SelectedIndex = -1;
                cmbSex.SelectedIndex = -1;
                Voter = new Voter();
                tableLayoutPanel6.Enabled = false;
                dgvIVoters.DataSource = null;
                browser.Load("about:blank");
                iconButton1.Enabled = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async Task<House> LoadHouseAsyncById(int id)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.FindAsync<House>("Houses", id.ToString(), token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    if (response.Message == "Not Found")
                    {
                        UtilRecurrent.ErrorMessage(response.Message);
                        return null;
                    }
                    UtilRecurrent.ErrorMessage(response.Message);
                    return null;
                }
                return (House)response.Result;
            }
            catch (Exception ex)
            {
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return null;
            }
        }
        private void tView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node != null)
                {

                    cleanScreen();
                    if (NodeLevel(e.Node) == 2 || (NodeLevel(e.Node) == 1 && int.Parse(e.Node.Parent.Tag.ToString().Split('-')[1]) == int.MaxValue))
                    {
                        AfterSelectNodeHouse(int.Parse(e.Node.Tag.ToString().Split('-')[1]));
                    }
                    if (NodeLevel(e.Node) == 0 && e.Node.Tag != null)
                    {
                        AfterSelectNodeConstituency(int.Parse(e.Node.Tag.ToString().Split('-')[1]));
                        e.Node.ExpandAll();
                    }
                    if (NodeLevel(e.Node) == 1 && int.Parse(e.Node.Tag.ToString().Split('-')[1]) < int.MaxValue)
                    {
                        AfterSelectNodeConstituency(int.Parse(e.Node.Tag.ToString().Split('-')[1]));
                        e.Node.ExpandAll();

                    }

                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void AfterSelectNodeConstituency(int nodeTag)
        {
            FieldsWhite();
            try
            {
                if (nodeTag >= 0)
                {
                    ibtnSave.Visible = true;
                    ibtnUpdate.Visible = false;
                }
                else
                {
                    cleanScreen();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async void AfterSelectNodeHouse(int nodeTag)
        {
            FieldsWhite();
            try
            {
                if (nodeTag > 0)
                {
                    House = new();
                    House = await LoadHouseAsyncById(nodeTag);
                    ShowHouseInformation();
                    ibtnSave.Visible = false;
                    ibtnUpdate.Visible = true;
                }
                else
                {
                    cleanScreen();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void ShowHouseInformation()
        {
            try
            {
                txtNumber.Text = House.Number;
                txtNumberOfPersons.Text = House.NumberOfPersons.ToString();
                txtLongitue.Text = House.Longitude.ToString();
                txtLatitude.Text = House.Latitude.ToString();
                tableLayoutPanel6.Enabled = true;
                ShowHouseInGoogleMap();
                if (House.Voters == null)
                {
                    return;
                }
                if (!House.Voters.Any())
                {
                    return;
                }

                dgvIVoters.DataSource = House.Voters.Select(x => new
                {
                    x.Id,
                    x.Reg,
                    x.FullName,
                    x.Sex,
                    x.Address,
                    Contact = x.Mobile1 + (x.Mobile1 == "" ? "" : "<>") + x.Mobile2 + (x.Mobile2 == "" ? "" : "<>") + x.HomePhone + (x.HomePhone == "" ? "" : "<>") + x.WorkPhone,
                    x.Email,
                    Interviews = x.Interviews.Count
                }).ToList();
                dgvIVoters.Columns[1].Visible = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void ShowHouseInGoogleMap()
        {
            try
            {
                if (House != null)
                {
                    string url = "https://www.google.com/maps/search/?api=1&query=" + House.Latitude + "," + House.Longitude;
                    browser.Load(url);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);

            }
        }
        private async Task<Voter> LoadVoterByRegAsync(string route, string Id)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.FindAsync<Voter>(route, Id, token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    if (response.Message == "Not Found")
                    {
                        UtilRecurrent.ErrorMessage(response.Message);
                        return null;
                    }
                    UtilRecurrent.ErrorMessage(response.Message);
                    return null;
                }
                return (Voter)response.Result;
            }
            catch (Exception ex)
            {
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return null;
            }
        }
        private void ShowVoterInformation()
        {
            try
            {
                if (Voter.Id > 0)
                {
                    List<PropertyInfo> properties = Voter.GetType().GetProperties().ToList();
                    List<TextBox> VoterTextBox = UtilRecurrent.FindAllTextBoxIterative(tpanelVoter);
                    foreach (TextBox txt in VoterTextBox)
                    {
                        if (properties.Where(p => p.GetValue(Voter) != null && p.Name == txt.Name.Replace("txt", string.Empty)).Any())
                        {
                            txt.Text = properties.Where(p => p.GetValue(Voter) != null && p.Name == txt.Name.Replace("txt", string.Empty)).First().GetValue(Voter).ToString();
                        }
                    }
                    cmbSex.SelectedItem = Voter.Sex;
                    dtpDOB.Value = Voter.DOB.HasValue ? (DateTime)Voter.DOB : DateTime.Today;
                    cmbConstituency.SelectedValue = Voter.PollingDivision.Constituency.Id;
                    FillUpdComboboxDivision();
                    cmbDivision.SelectedValue = Voter.PollingDivision.Id;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void FillUpdComboboxDivision()
        {
            try
            {
                if (ConstituenciesList.Where(p => p.Id == (int)cmbConstituency.SelectedValue).Select(p => p.PollingDivisions).Any())
                {
                    cmbDivision.DataSource = null;
                    PollingDivisionsList = new List<PollingDivision>();
                    PollingDivisionsList = ConstituenciesList.Where(p => p.Id == (int)cmbConstituency.SelectedValue).SelectMany(p => p.PollingDivisions).ToList();
                    cmbDivision.DataSource = PollingDivisionsList;
                    cmbDivision.ValueMember = "Id";
                    cmbDivision.DisplayMember = "Name";
                    cmbDivision.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }

        #endregion

        #region Save update
        private async void iconButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (PaintRequired())
                {
                    UtilRecurrent.ErrorMessage("Requireds fields missing. Find them highlighted in red.");
                    return;
                }
                if (await SaveHouse())
                {
                    await LoadHouses();
                }
                if (House != null && House.Id > 0)
                {
                    tView1.SelectedNode = CollectAllNodes(tView1.Nodes).FirstOrDefault(x => x.Tag.ToString() == "H-" + House.Id);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async Task<bool> SaveHouse()
        {
            try
            {
                var house = await BuildHouse(null);
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PostAsync("Houses", House, token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response.IsSuccess;
                }
                House = (House)response.Result;
                //UtilRecurrent.InformationMessage("Application sucessfully saved", "Application Saved");
                return true;
            }
            catch (Exception ex)
            {
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }
        }
        private async Task<House> BuildHouse(int? id)
        {
            try
            {
                House = new();
                House.Active = true;
                House.Number = txtNumber.Text;
                House.NumberOfPersons = int.Parse(txtNumberOfPersons.Text);
                House.Latitude = double.Parse(txtLatitude.Text);
                House.Longitude = double.Parse(txtLongitue.Text);
                if (id != null)
                {
                    House.Id = (int)id;
                }
                return House;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return null;
            }
        }

        private async void ibtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                if (tView1.SelectedNode == null || tView1.SelectedNode.Tag == null || tView1.SelectedNode.Tag.ToString() == "H-0")
                {
                    return;
                }
                if (PaintRequired())
                {
                    UtilRecurrent.ErrorMessage("Requireds fields missing. Find them highlighted in red.");
                    return;
                }

                if (await UpdateHouse(int.Parse(tView1.SelectedNode.Tag.ToString().Replace("H-", string.Empty))))
                {
                    await LoadHouses();
                }

                if (House != null && House.Id > 0)
                {
                    tView1.SelectedNode = CollectAllNodes(tView1.Nodes).FirstOrDefault(x => x.Tag.ToString() == "H-" + House.Id);

                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async Task<bool> UpdateHouse(int? id)
        {
            try
            {

                var house = await BuildHouse(id);
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PutAsync("Houses", house, house.Id, token);
                UtilRecurrent.UnlockForm(waitForm, this);

                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }
        }
        private async Task<bool> AddHouseVoter(int? id)
        {
            try
            {
                House.Voters = new List<Voter>
                {
                    Voter
                };

                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PutAsync("Houses", House, House.Id, token);
                UtilRecurrent.UnlockForm(waitForm, this);

                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }
        }
        #endregion


        private async void txtReg_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {//TODO SOLO CUANDO TENGA UN VOTANTE ACTIVAR ELBOTON DE ADICCIONAR A LA CASA
                if (e.KeyChar == (char)13 && ((TextBox)sender).TextLength > 0)
                {
                    var vot = await LoadVoterByRegAsync("Voters/FindRegistration", ((TextBox)sender).Text);
                    if (vot != null)
                    {
                        //vot.ElectionVotes = null;
                        //vot.Interviews = null;
                        Voter = vot;
                        ShowVoterInformation();
                        iconButton1.Enabled = true;
                    }
                    else
                    {
                        cleanScreen();
                    }

                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        public int NodeLevel(TreeNode node)
        {
            int level = 0;
            while ((node = node.Parent) != null) level++;
            return level;
        }


        private void cmbElection_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {

                //cmbISupportedParty.DataSource = null;
                //if (cmbElection.SelectedItem != null && (cmbElection.SelectedItem as NationalElection).Parties != null && (cmbElection.SelectedItem as NationalElection).Parties.Any())
                //{
                //    var parties = (cmbElection.SelectedItem as NationalElection).Parties;
                //    cmbISupportedParty.DataSource = null;
                //    cmbISupportedParty.DataSource = parties;
                //    cmbISupportedParty.ValueMember = "Id";
                //    cmbISupportedParty.DisplayMember = "Name";
                //    cmbISupportedParty.SelectedItem = null;
                //}
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void rjCollapseAll_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (((RJToggleButton)sender).Checked)
                {
                    tView1.Font = new Font("Segoe UI", 12, FontStyle.Regular);
                    tView1.ExpandAll();
                    lblExpand.Text = "Collapse All";
                }
                else
                {
                    tView1.CollapseAll();
                    tView1.Font = new Font("Courier New", 12, FontStyle.Regular);
                    tView1.SelectedNode = tView1.Nodes[0];
                    lblExpand.Text = "Expand All";
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }

        private async void ibtnRefresh_Click(object sender, EventArgs e)
        {
            await LoadHouses();
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {

            try
            {
                if (VoterInHouseAlready())
                {
                    UtilRecurrent.ErrorMessage("The Voter is in the House Already.");
                    return;
                }
                if (VoterInOtherHouseAlready())
                {
                    if(UtilRecurrent.yesOrNot("The Voter is in another House. Do you want to move him to this House?","Move Voter")) 
                    {
                        if (await RemoveVoterHouse()) Voter.House = null;
                        
                    }
                    else
                    {
                        return;
                    }                   
                }
                if (!VoterSameHouseDivision())
                {
                    if (UtilRecurrent.yesOrNot("The Voter belongs to a diferent Polling Division than the other(s) Voter(s) in the house, if you continue, the Voter Polling Division will be updated to match the house division. Do you want to continue?", "Update Voter Polling Division"))
                    {
                        var division = House.Voters.First().PollingDivision;
                        Voter.PollingDivision = division;
                        await UpdateVoter();
                    }
                    else
                    {
                        return;
                    }
                }
                if (await AddHouseVoter(int.Parse(tView1.SelectedNode.Tag.ToString().Replace("H-", string.Empty))))
                {
                    await LoadHouses();
                }
                if (House != null && House.Id > 0)
                {
                    tView1.SelectedNode = CollectAllNodes(tView1.Nodes).FirstOrDefault(x => x.Tag.ToString() == "H-" + House.Id);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private bool VoterSameHouseDivision()
        {
            if (House.Voters != null && House.Voters.Any())
            {
                var division = House.Voters.First().PollingDivision;
                if (Voter.PollingDivision != null && Voter.PollingDivision.Id == division.Id)
                {
                    return true;
                }
                return false;
            }
            return true;
        }
        private bool VoterInHouseAlready()
        {
            if (House.Voters != null && House.Voters.Any())
            {
                if (House.Voters.Any(x => x.Id == Voter.Id))
                {
                    return true;
                }
                return false;              
            }
            return false;
        }
        private bool VoterInOtherHouseAlready()
        {
            if (Voter.House != null && House.Voters.Any())
            {
                if (Voter.House.Id != House.Id)
                {
                    return true;
                }               
                return false;
            }
            return false;
        }
        private async Task UpdateVoter()
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PutAsync("Voters", Voter, Voter.Id, token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return;
                }
            }
            catch (Exception ex)
            {
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async Task<bool> RemoveVoterHouse()
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.DeleteAsync("Voters/RemoveHouse", Voter.Id, token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }
        }
    }


}
