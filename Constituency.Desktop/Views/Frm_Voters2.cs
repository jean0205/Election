using Constituency.Desktop.Components;
using Constituency.Desktop.Controls;
using Constituency.Desktop.Entities;
using Constituency.Desktop.Helpers;
using Constituency.Desktop.Models;
using Microsoft.AppCenter.Crashes;
using System.Data;
using System.Data.OleDb;
using System.Reflection;

//todo if women and icn different if man
//introduce deceased field
//if deacessed different icon too
//poner la ventana de upload bonita
//agregar validaciones para si algun polling division no existe o algun campo requerido esta en blanco
//hacer table para salvar los duplicados


namespace Constituency.Desktop.Views
{
    public partial class Frm_Voters2 : Form
    {
        //Fields
        private string token = string.Empty;

        //objects
        User user { get; set; }
        Voter Voter { get; set; }

        WaitFormFunc waitForm = new WaitFormFunc();

        //list to load
        private List<Voter> VoterList;
        private List<ConstituencyC> ConstituenciesList;
        private List<PollingDivision> PollingDivisionsList;
        private List<PollingDivision> PollingDivisionsListBatch;
        public Frm_Voters2()
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            InitializeComponent();
            token = Main.GetInstance().tokenResponse.Token;
            user = Main.GetInstance().tokenResponse.User;
        }

        private async void Frm_Voters_Load(object sender, EventArgs e)
        {
            FildsValidations();
            MandatoriesFilds();
            DGVFormats();
            await LoadConstituencies();
            //await LoadVoters();
            await LoadDivisionsBatch();
            VoterList = new();

        }

        #region Tab1
        private void DGVFormats()
        {//tag= 1 para campos obligados

            UtilRecurrent.FindAllControlsIterative(tabControl1, "DataGridView").Cast<DataGridView>().ToList().ForEach(x => x.DefaultCellStyle.BackColor = Color.Beige);
            UtilRecurrent.FindAllControlsIterative(tabControl1, "DataGridView").Cast<DataGridView>().ToList().ForEach(x => x.AlternatingRowsDefaultCellStyle.BackColor = Color.Bisque);
            UtilRecurrent.FindAllControlsIterative(tabControl1, "DataGridView").Cast<DataGridView>().ToList().ForEach(x => x.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells);
            UtilRecurrent.FindAllControlsIterative(tabControl1, "DataGridView").Cast<DataGridView>().ToList().ForEach(x => x.RowHeadersVisible = false);
        }
        private void FildsValidations()
        {
            try
            {
                //tag=2 para only numbers
                UtilRecurrent.FindAllControlsIterative(this, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("2")).ToList().ForEach(x => x.KeyPress += UtilRecurrent.txtOnlyIntegersNumber_KeyPress);

                //tag=3 para only letters
                UtilRecurrent.FindAllControlsIterative(this, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("3")).ToList().ForEach(x => x.KeyPress += UtilRecurrent.txtOnlyLetters_KeyPress);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void MandatoriesFilds()
        {//tag= 1 para campos obligados

            try
            {
                UtilRecurrent.FindAllControlsIterative(this, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1")).ToList().ForEach(x => toolTip1.SetToolTip(x, "Mandatory Field"));
                UtilRecurrent.FindAllControlsIterative(this, "ComboBox").Cast<ComboBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1")).ToList().ForEach(x => toolTip1.SetToolTip(x, "Mandatory Field"));
                //UtilRecurrent.FindAllControlsIterative(this, "RadioButton").Cast<RadioButton>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1")).ToList().ForEach(x => toolTip1.SetToolTip(x, "Mandatory Field"));
                toolTip1.SetToolTip(dtpDOB, "Mandatory Field");
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }

        }
        private void VoterFieldsWhite()
        {
            try
            {
                //todos en blanco antes de comprobar
                UtilRecurrent.FindAllControlsIterative(tpanelVoter, "TextBox").Cast<TextBox>().ToList().ForEach(t => t.BackColor = Color.FromKnownColor(KnownColor.Window));

                UtilRecurrent.FindAllControlsIterative(tpanelVoter, "ComboBox").Cast<ComboBox>().ToList().ForEach(t => t.BackColor = Color.FromKnownColor(KnownColor.Window));

                //UtilRecurrent.FindAllControlsIterative(panel5, "RadioButton").Cast<RadioButton>().ToList().ForEach(t => t.ForeColor = Color.Gainsboro);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }

        private bool PaintRequiredVoter()
        {
            bool missing = false;
            try
            {
                VoterFieldsWhite();
                //validando textbox requeridos no vacios
                if (UtilRecurrent.FindAllControlsIterative(tpanelVoter, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1") && (x.TextLength == 0 || string.IsNullOrWhiteSpace(x.Text))).ToList().Any())
                {
                    UtilRecurrent.FindAllControlsIterative(tpanelVoter, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1") && (x.TextLength == 0 || string.IsNullOrWhiteSpace(x.Text))).ToList().ForEach(t => t.BackColor = Color.LightSalmon);
                    missing = true;
                }
                if (UtilRecurrent.FindAllControlsIterative(tpanelVoter, "ComboBox").Cast<ComboBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1") && x.Text == string.Empty).ToList().Any())
                {
                    UtilRecurrent.FindAllControlsIterative(tpanelVoter, "ComboBox").Cast<ComboBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1") && x.Text == string.Empty).ToList().ForEach(t => t.BackColor = Color.LightSalmon);
                    missing = true;
                }
                //if (UtilRecurrent.FindAllControlsIterative(panel5, "RadioButton").Cast<RadioButton>().Where(x => !x.Checked).ToList().Count == 3)
                //{
                //    UtilRecurrent.FindAllControlsIterative(panel5, "RadioButton").Cast<RadioButton>().ToList().ForEach(t => t.ForeColor = Color.Red); ;
                //    missing = true;
                //}
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
            return missing;
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

                RefreshTreeViewByDivision(new List<Voter>());
            }
        }
        private async Task LoadDivisionsBatch()
        {
            Response response = await ApiServices.GetListAsync<PollingDivision>("PollingDivisions", token);
            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return;
            }
            PollingDivisionsListBatch = new((List<PollingDivision>)response.Result);

        }
        private async Task LoadVoters()
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.GetListAsync<Voter>("Voters", token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return;
                }
                VoterList = new((List<Voter>)response.Result);
                if (VoterList.Any())
                {
                    RefreshTreeView(VoterList.ToList());
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

        private TreeNode FindNode(TreeView tvw, string voterId)
        {
            try
            {
                foreach (TreeNode node in tvw.Nodes)
                {
                    foreach (TreeNode childNode in node.Nodes)
                    {
                        if (childNode.Nodes.Cast<TreeNode>().Where(x => x.Tag.ToString() == voterId).Any())
                        {
                            return childNode.Nodes.Cast<TreeNode>().FirstOrDefault(x => x.Tag.ToString() == voterId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
            return null;
        }
        private void RefreshTreeView(List<Voter> Voters)
        {
            try
            {
                tView1.BeginUpdate();
                tView1.Nodes.Clear();
                //TreeNode node;
                List<TreeNode> treeNodes = new List<TreeNode>();
                List<TreeNode> childNodes = new List<TreeNode>();
                List<TreeNode> childNodes2 = new List<TreeNode>();

                foreach (ConstituencyC contituency in ConstituenciesList)
                {
                    int cant2 = VoterList.Where(v => v.PollingDivision.Constituency.Id == contituency.Id).Count();

                    foreach (PollingDivision division in contituency.PollingDivisions.Distinct())
                    {
                        int cant = VoterList.Where(v => v.PollingDivision.Id == division.Id).Count();
                        if (VoterList.Where(v => v.PollingDivision.Id == division.Id).Any())
                        {
                            foreach (Voter voter in VoterList.OrderBy(v => v.FullName).Where(v => v.PollingDivision.Id == division.Id))
                            {
                                TreeNode node2 = new TreeNode(voter.FullName, 2, 3);
                                node2.Tag = voter.Id;
                                childNodes2.Add(node2);
                            }
                            if (childNodes2.Any())
                            {
                                childNodes.Add(new TreeNode(division.Name + " [" + cant + "]", 0, 1, childNodes2.ToArray()));
                                childNodes[childNodes.Count - 1].Tag = division.Id;
                                childNodes2 = new List<TreeNode>();
                            }
                        }
                    }
                    treeNodes.Add(new TreeNode(contituency.Name + " [" + cant2 + "]",8, 1, childNodes.ToArray()));
                    treeNodes[treeNodes.Count - 1].Tag = contituency.Id;
                    childNodes = new List<TreeNode>();
                }
                tView1.Nodes.AddRange(treeNodes.ToArray());
                //tView1.ExpandAll();
                tView1.CollapseAll();
                rjCollapseAll.Checked = false;
                lblExpand.Text = "EXPAND All";
                tView1.Font = new Font("Segoe UI", 12, FontStyle.Regular);
                groupBox1.Text = "Voters List ( " + Voters.Count.ToString("N0") + " )";
                tView1.EndUpdate();

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void RefreshTreeViewByDivision(List<Voter> Voters)
        {
            try
            {
                tView1.BeginUpdate();
                tView1.Nodes.Clear();
                //TreeNode node;
                List<TreeNode> treeNodes = new List<TreeNode>();
                List<TreeNode> childNodes = new List<TreeNode>();
                List<TreeNode> childNodes2 = new List<TreeNode>();

                foreach (ConstituencyC contituency in ConstituenciesList)
                {
                    int cant2 = Voters.Any() ? Voters.Where(v => v.PollingDivision.Constituency.Id == contituency.Id).Count() : 0;

                    foreach (PollingDivision division in contituency.PollingDivisions.Distinct())
                    {
                        int cant = Voters.Where(v => v.PollingDivision.Id == division.Id).Count();
                        if (Voters.Where(v => v.PollingDivision.Id == division.Id).Any())
                        {
                            foreach (Voter voter in VoterList.Where(v => v.PollingDivision.Id == division.Id).OrderBy(v => v.SurName))
                            {
                                TreeNode node2 = new TreeNode(voter.FullName, 2, 3);
                                node2.Tag = voter.Id;
                                childNodes2.Add(node2);
                            }
                        }
                        childNodes.Add(new TreeNode(division.Name + " [" + cant + "]", 0, 1, childNodes2.ToArray()));
                        childNodes[childNodes.Count - 1].Tag = division.Id;
                        childNodes2 = new List<TreeNode>();
                    }
                    treeNodes.Add(new TreeNode(contituency.Name + " [" + cant2 + "]", 8, 1, childNodes.ToArray()));
                    treeNodes[treeNodes.Count - 1].Tag = contituency.Id;
                    childNodes = new List<TreeNode>();
                }
                tView1.Nodes.AddRange(treeNodes.ToArray());
                //tView1.ExpandAll();
                // tView1.CollapseAll();
                rjCollapseAll.Checked = false;
                lblExpand.Text = "EXPAND All";
                tView1.Font = new Font("Segoe UI", 12, FontStyle.Regular);
                groupBox1.Text = "Voters List ( " + Voters.Count.ToString("N0") + " )";
                tView1.EndUpdate();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async void tView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node != null)
                {

                    if (NodeLevel(e.Node) == 2)
                    {
                        cleanScreen();
                        AfterSelectNodeVoter((int)e.Node.Tag);
                    }
                    if (NodeLevel(e.Node) == 0)
                    {
                        cleanScreen();
                        cmbConstituency.SelectedValue = ConstituenciesList.FirstOrDefault(x => x.Id == (int)e.Node.Tag).Id;
                        FillUpdComboboxDivision();
                        ibtnSaveVoter.Visible = true;
                        ibtnUpdate.Visible = false;
                        e.Node.ExpandAll();
                    }
                    if (NodeLevel(e.Node) == 1)
                    {
                        var node = e.Node;
                        cleanScreen();
                        cmbConstituency.SelectedValue = ConstituenciesList.Where(x => x.PollingDivisions.Any(y => y.Id == (int)e.Node.Tag)).FirstOrDefault().Id;
                        FillUpdComboboxDivision();
                        cmbDivision.SelectedValue = PollingDivisionsList.FirstOrDefault(x => x.Id == (int)e.Node.Tag).Id;
                        ibtnSaveVoter.Visible = true;
                        ibtnUpdate.Visible = false;
                        //leer losvoter theladivision y agregarlos a la lista de voters
                        if (e.Node.Nodes.Count == 0)
                        {
                            await LoadVotersInDivision((int)e.Node.Tag);
                            tView1.SelectedNode = CollectAllNodes(tView1.Nodes).FirstOrDefault(x => x.Tag.ToString() == e.Node.Tag.ToString());
                            tView1.SelectedNode.Expand();
                        }
                        e.Node.ExpandAll();


                    }
                }

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }

        }
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
        private async Task LoadVotersInDivision(int divisionId)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.GetListAsync<Voter>("Voters/InDivisions", divisionId.ToString(), token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return;
                }
                List<Voter> voters = (List<Voter>)response.Result;
                if (voters != null && voters.Any())
                {
                    VoterList.AddRange(voters.Where(x => !VoterList.Any(y => y.Id == x.Id)));
                }
                if (VoterList.Any())
                {
                    RefreshTreeViewByDivision(VoterList);
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
        private async void AfterSelectNodeVoter(int nodeTag)
        {
            VoterFieldsWhite();
            dgvInterviews.DataSource = null;
            try
            {
                if (nodeTag > 0)
                {
                    Voter = new Voter();
                    Voter = await LoadVoterAsyncById(nodeTag);
                    ShowVoterInformation();
                    ibtnSaveVoter.Visible = false;
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
        private async Task<Voter> LoadVoterAsyncById(int id)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.FindAsync<Voter>("Voters", id.ToString(), token);
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
                    rjDeceased.Checked = Voter.Dead;


                    if (Voter.Interviews != null && Voter.Interviews.Any())
                    {
                        dgvInterviews.DataSource = Voter.Interviews.Select(u => new { u.Canvas.Type.Type, u.Canvas.Name, u.Date, Party = u.SupportedParty.Name, u.Interviewer.FullName }).ToList();
                    }
                    if (Voter.ElectionVotes != null && Voter.ElectionVotes.Any())
                    {
                        dgvElections.DataSource = Voter.ElectionVotes.Select(u => new { u.Election.ElectionDate, u.VoteTime }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void cleanScreen()
        {
            try
            {
                UtilRecurrent.FindAllControlsIterative(this.tpanelVoter, "TextBox").Cast<TextBox>().ToList().ForEach(x => x.Clear());
                UtilRecurrent.FindAllControlsIterative(this.tpanelVoter, "ComboBox").Cast<ComboBox>().ToList().ForEach(x => x.SelectedIndex = -1);
                UtilRecurrent.FindAllControlsIterative(this.tpanelVoter, "DateTimePicker").Cast<DateTimePicker>().ToList().ForEach(x => x.Value = DateTime.Now.Date);
                UtilRecurrent.FindAllControlsIterative(this.tpanelVoter, "DataGridView").Cast<DataGridView>().ToList().ForEach(x => x.DataSource = null);
                rjDeceased.Checked = false;
                Voter = new Voter();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async void iconButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (PaintRequiredVoter())
                {
                    UtilRecurrent.ErrorMessage("Requireds fields missing. Find them highlighted in red.");
                    return;
                }
                if (await LoadVoterByRegAsync("Voters/FindRegistration", txtReg.Text) != null)
                {
                    UtilRecurrent.ErrorMessage("The Registration Number already exist ");
                    return;
                }
                //if (dtpDOB.Value.AddYears(18) > DateTime.Now)
                //{
                //    UtilRecurrent.ErrorMessage("Voter younger than 18.");
                //    return;
                //}
                if (txtEmail.TextLength > 0 && !UtilRecurrent.IsValidEmail(txtEmail.Text.Trim()))
                {
                    UtilRecurrent.ErrorMessage("You must provide a valid Email address.");
                    return;
                }
                await SaveVoter();

                if (Voter != null && Voter.Id > 0)
                {
                    VoterList.Add(Voter);
                    RefreshTreeViewByDivision(VoterList);
                    tView1.SelectedNode = FindNode(tView1, Voter.Id.ToString());
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
        private async Task<bool> SaveVoter()
        {
            try
            {
                var voter = BuildVoter();
                voter.Active = true;
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PostAsync("Voters", voter, token);
                UtilRecurrent.UnlockForm(waitForm, this);

                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response.IsSuccess;
                }

                Voter = (Voter)response.Result;
                Voter.PollingDivision.Constituency = ConstituenciesList.FirstOrDefault(x => x.PollingDivisions.Any(y => y.Id == Voter.PollingDivision.Id));
                return true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }
        }
        private Voter BuildVoter()
        {
            try
            {
                Voter = new Voter();
                List<PropertyInfo> properties = Voter.GetType().GetProperties().ToList();
                List<TextBox> voterTextBox = UtilRecurrent.FindAllTextBoxIterative(tpanelVoter);
                foreach (PropertyInfo prop in properties)
                {
                    if (voterTextBox.Where(p => p.Name.Replace("txt", string.Empty) == prop.Name).Any())
                    {
                        prop.SetValue(Voter, voterTextBox.Where(p => p.Name.Replace("txt", string.Empty) == prop.Name).FirstOrDefault().Text.TrimEnd().ToUpper());
                    }
                }
                Voter.Sex = cmbSex.SelectedItem.ToString();
                Voter.DOB = dtpDOB.Value.Date == DateTime.Today ? null : dtpDOB.Value;
                Voter.PollingDivision = PollingDivisionsList.FirstOrDefault(p => p.Id == (int)cmbDivision.SelectedValue);
                Voter.Dead = rjDeceased.Checked;
                return Voter;
            }

            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return null;
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

        private async void ibtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (PaintRequiredVoter())
                {
                    UtilRecurrent.ErrorMessage("Requireds fields missing. Find them highlighted in red.");
                    return;
                }
                var vot = await LoadVoterByRegAsync("Voters/FindRegistration", txtReg.Text);
                if (vot != null && vot.Id != Voter.Id)
                {
                    UtilRecurrent.ErrorMessage("The Registration Number already exist for other voter.");
                    return;
                }
                if (txtEmail.TextLength > 0 && !UtilRecurrent.IsValidEmail(txtEmail.Text.Trim()))
                {
                    UtilRecurrent.ErrorMessage("You must provide a valid Email address.");
                    return;
                }
                await UpdateVoter();

                if (Voter != null && Voter.Id > 0)
                {
                    var oldVoter = VoterList.FirstOrDefault(v => v.Id == Voter.Id);
                    VoterList.Remove(oldVoter);
                    VoterList.Add(Voter);
                    RefreshTreeViewByDivision(VoterList);
                    tView1.SelectedNode = FindNode(tView1, Voter.Id.ToString());
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async Task<bool> UpdateVoter()
        {
            try
            {
                var voter = BuildUpdateVoter();
                voter.Interviews = null;
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PutAsync("Voters", voter, voter.Id, token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return false;
                }
                Voter = (Voter)response.Result;
                Voter.PollingDivision.Constituency = ConstituenciesList.FirstOrDefault(x => x.PollingDivisions.Any(y => y.Id == Voter.PollingDivision.Id));
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
        private Voter BuildUpdateVoter()
        {
            try
            {
                List<PropertyInfo> properties = Voter.GetType().GetProperties().ToList();
                List<TextBox> voterTextBox = UtilRecurrent.FindAllTextBoxIterative(tpanelVoter);
                foreach (PropertyInfo prop in properties)
                {
                    if (voterTextBox.Where(p => p.Name.Replace("txt", string.Empty) == prop.Name).Any())
                    {
                        prop.SetValue(Voter, voterTextBox.Where(p => p.Name.Replace("txt", string.Empty) == prop.Name).FirstOrDefault().Text.TrimEnd().ToUpper());
                    }
                }
                Voter.Sex = cmbSex.SelectedItem.ToString();
                Voter.DOB = dtpDOB.Value.Date == DateTime.Today ? null : dtpDOB.Value;
                Voter.PollingDivision = PollingDivisionsList.FirstOrDefault(p => p.Id == (int)cmbDivision.SelectedValue);
                Voter.Dead = rjDeceased.Checked;

                return Voter;
            }

            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return null;
            }
        }
        private async void ibtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                await LoadVoters();
                tView1.SelectedNode = tView1.Nodes[0];
                lblFiltering.Visible = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }

        }
        #endregion

        #region Events
        private void cmbConstituency_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbConstituency.SelectedIndex > -1)
                {
                    FillUpdComboboxDivision();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }

        }
        #endregion
        #region Others
        public int NodeLevel(TreeNode node)
        {
            int level = 0;
            while ((node = node.Parent) != null) level++;
            return level;
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




        #endregion

        #region Import Batch

        List<Voter> votersBatch = new();
        async void checkVoterList()
        {
            try
            {
                votersBatch = new();
                votersBatch = BuildVoterList();

                label16.Text = votersBatch.Count.ToString();
                //count the repeted voters
                label17.Text = votersBatch.GroupBy(v => v.Reg).Where(g => g.Count() > 1).Count().ToString();
                var duplicatedVoters = votersBatch.GroupBy(v => v.Reg).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
                var correctOnes = votersBatch.Where(v => !String.IsNullOrEmpty(v.Reg)).ToList();

                var batches = BuildChunksWithLinq(correctOnes, 5000);

                foreach (var batch in batches)
                {
                    await UploadMasterFile((List<Voter>)batch);

                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private IEnumerable<IEnumerable<T>> BuildChunksWithLinq<T>(List<T> fullList, int batchSize)
        {
            int total = 0;
            var chunkedList = new List<List<T>>();
            while (total < fullList.Count)
            {
                var chunk = fullList.Skip(total).Take(batchSize);
                chunkedList.Add(chunk.ToList());
                total += batchSize;
            }

            return chunkedList;
        }
        private IEnumerable<IEnumerable<T>> BuildChunksWithLinqAndYield<T>(List<T> fullList, int batchSize)
        {
            int total = 0;
            while (total < fullList.Count)
            {
                yield return fullList.Skip(total).Take(batchSize);
                total += batchSize;
            }
        }
        private async Task<bool> UploadMasterFile(List<Voter> list)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PostMasterFileAsync("Voters/Range", list, token);

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
                UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }

        }
        private List<Voter> BuildVoterList()
        {
            try
            {
                List<Voter> voters = new List<Voter>();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    Voter voter = new Voter();
                    voter.Active = true;
                    voter.SurName = dataGridView1.Rows[i].Cells[1].Value.ToString().ToUpper().Trim();
                    voter.GivenNames = dataGridView1.Rows[i].Cells[2].Value.ToString().ToUpper().Trim();
                    voter.Sex = dataGridView1.Rows[i].Cells[3].Value.ToString().ToUpper().Trim();
                    voter.Address = dataGridView1.Rows[i].Cells[4].Value.ToString().ToUpper().Trim();
                    voter.Occupation = dataGridView1.Rows[i].Cells[5].Value.ToString().ToUpper().Trim();
                    voter.Reg = dataGridView1.Rows[i].Cells[6].Value.ToString().ToUpper().Trim();
                    voter.PollingDivision = new PollingDivision();
                    string pd = dataGridView1.Rows[i].Cells[7].Value.ToString().ToUpper().Trim();
                    voter.PollingDivision = PollingDivisionsListBatch.FirstOrDefault(p => p.Name == pd);
                    voter.DOB = null;
                    voter.Mobile1 = String.Empty;
                    voter.Mobile2 = String.Empty;
                    voter.Email = String.Empty;
                    voter.HomePhone = String.Empty;
                    voter.WorkPhone = String.Empty;
                    voter.Dead = false;
                    voters.Add(voter);

                }
                return voters;

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return null;
            }
        }
        private void Frm_Voters_FormClosing(object sender, FormClosingEventArgs e)
        {

            UtilRecurrent.LockForm(waitForm, this);
            tView1.BeginUpdate();
            tView1.Nodes.Clear();
            tView1.EndUpdate();
        }

        private void Frm_Voters_FormClosed(object sender, FormClosedEventArgs e)
        {
            UtilRecurrent.UnlockForm(waitForm, this);
        }



        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "xlsx";
            ofd.FileName = "Upload File";
            ofd.Filter = "Excel  files|*.xls;*.xlsx";
            ofd.Title = "Select file";
            //  Allow the user to select multiple images.

            if (ofd.ShowDialog() != DialogResult.Cancel)
            {
                try
                {
                    UtilRecurrent.LockForm(waitForm, this);
                    String name = "Sheet1";
                    String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                    ofd.FileName.ToString() +
                                    ";Extended Properties='Excel 12.0 XML;HDR=NO;';";

                    OleDbConnection con = new OleDbConnection(constr);
                    OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
                    con.Open();

                    OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                    DataTable data = new DataTable();
                    sda.Fill(data);
                    dataGridView1.DataSource = data;
                    dataGridView1.DefaultCellStyle.BackColor = Color.Beige;
                    dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Bisque;
                    ibtnImport.Visible = true;
                    UtilRecurrent.UnlockForm(waitForm, this);
                    ibtnUpload.Visible = true;
                }
                catch (Exception ex)
                {
                    UtilRecurrent.UnlockForm(waitForm, this);
                    Crashes.TrackError(ex);
                    UtilRecurrent.ErrorMessage(ex.Message);
                }


            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            checkVoterList();
        }

        #endregion


    }
}
