using Constituency.Desktop.Components;
using Constituency.Desktop.Entities;
using Constituency.Desktop.Helpers;
using Constituency.Desktop.Models;
using Microsoft.AppCenter.Crashes;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace Constituency.Desktop.Views
{
    public partial class Frm_Voters : Form
    {
        //Fields
        private string token = string.Empty;

        //objects
        User user { get; set; }
        Voter Voter { get; set; }

        WaitFormFunc waitForm = new WaitFormFunc();

        //list to load
        private ObservableCollection<Voter> VoterList;
        private List<ConstituencyC> ConstituenciesList;
        private List<PollingDivision> PollingDivisionsList;
        public Frm_Voters()
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            InitializeComponent();
            token = Main.GetInstance().tokenResponse.Token;
            user = Main.GetInstance().tokenResponse.User;
        }

        private async void Frm_Voters_Load(object sender, EventArgs e)
        {
            MandatoriesFilds();
            await LoadConstituencies();
            await LoadVoters();

        }

        #region Tab1

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
            }
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
                // Applicant = new Applicant();
                VoterList = new ObservableCollection<Voter>((List<Voter>)response.Result);
                if (VoterList.Any())
                {
                    RefreshTreeView(VoterList.ToList());
                    //tView1.SelectedNode = tView1.Nodes[0];
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
                            foreach (Voter voter in VoterList.Where(v => v.PollingDivision.Id == division.Id))
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
                    treeNodes.Add(new TreeNode(contituency.Name + " [" + cant2 + "]", 0, 1, childNodes.ToArray()));
                    treeNodes[treeNodes.Count - 1].Tag = contituency.Id;
                    childNodes = new List<TreeNode>();
                }
                tView1.Nodes.AddRange(treeNodes.ToArray());
                tView1.ExpandAll();
                rjCollapseAll.Checked = true;
                lblExpand.Text = "Collapse All";
                tView1.Font = new Font("Segoe UI", 12, FontStyle.Regular);
                groupBox1.Text = "Voters List ( " + Voters.Count.ToString("N") + " )";

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void tView1_AfterSelect(object sender, TreeViewEventArgs e)
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
                    }
                    if (NodeLevel(e.Node) == 1)
                    {
                        cleanScreen();
                        cmbConstituency.SelectedValue = ConstituenciesList.Where(x => x.PollingDivisions.Any(y => y.Id == (int)e.Node.Tag)).FirstOrDefault().Id;
                        FillUpdComboboxDivision();
                        cmbDivision.SelectedValue = PollingDivisionsList.FirstOrDefault(x => x.Id == (int)e.Node.Tag).Id;
                        ibtnSaveVoter.Visible = true;
                        ibtnUpdate.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }

        }
        private void AfterSelectNodeVoter(int nodeTag)
        {
            VoterFieldsWhite();
            try
            {
                if (nodeTag > 0 )
                {
                    Voter = new Voter();
                    Voter = VoterList.FirstOrDefault(v => v.Id == nodeTag);
                    ShowApplicantInformation();
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
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void ShowApplicantInformation()
        {
            try
            {
                if (Voter.Id > 0)
                {
                    List<PropertyInfo> properties = Voter.GetType().GetProperties().ToList();
                    List<TextBox> VoterTextBox = UtilRecurrent.FindAllTextBoxIterative(tpanelVoter);
                    foreach (TextBox txt in VoterTextBox)
                    {
                        if (properties.Where(p => p.Name == txt.Name.Replace("txt", string.Empty)).Any())
                        {

                            txt.Text = properties.Where(p => p.Name == txt.Name.Replace("txt", string.Empty)).First().GetValue(Voter).ToString();
                        }
                    }
                    cmbSex.SelectedItem = Voter.Sex;
                    dtpDOB.Value = Voter.DOB;
                    cmbConstituency.SelectedValue = Voter.PollingDivision.Constituency.Id;
                    FillUpdComboboxDivision();
                    cmbDivision.SelectedValue = Voter.PollingDivision.Id;
                    //rjExtendSearch.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void cleanScreen()
        {
            try
            {
                UtilRecurrent.FindAllControlsIterative(this.tpanelVoter, "TextBox").Cast<TextBox>().ToList().ForEach(x => x.Clear());
                UtilRecurrent.FindAllControlsIterative(this.tpanelVoter, "ComboBox").Cast<ComboBox>().ToList().ForEach(x => x.SelectedIndex = -1);
                UtilRecurrent.FindAllControlsIterative(this.tpanelVoter, "DateTimePicker").Cast<DateTimePicker>().ToList().ForEach(x => x.Value = DateTime.Now.Date);
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
                if (dtpDOB.Value.AddYears(18) > DateTime.Now)
                {
                    UtilRecurrent.ErrorMessage("Applicant younger than 18.");
                    return;
                }
                if (txtEmail.TextLength > 0 && !UtilRecurrent.IsValidEmail(txtEmail.Text.Trim()))
                {
                    UtilRecurrent.ErrorMessage("You must provide a valid Email address.");
                    return;
                }
                if (await SaveVoter())
                {
                    await LoadVoters();
                }

                if (Voter != null && Voter.Id > 0)
                {
                    tView1.SelectedNode = FindNode(tView1, Voter.Id.ToString());
                    //await AfterSelectNodeTVw1((int)tView1.SelectedNode.Tag);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
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
                //UtilRecurrent.InformationMessage("Application sucessfully saved", "Application Saved");
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
                Voter.DOB = dtpDOB.Value;
                Voter.PollingDivision = PollingDivisionsList.FirstOrDefault(p => p.Id == (int)cmbDivision.SelectedValue);
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
                if (dtpDOB.Value.AddYears(18) > DateTime.Now)
                {
                    UtilRecurrent.ErrorMessage("Applicant younger than 18.");
                    return;
                }
                await UpdateVoter();
                await LoadVoters();
                Voter = VoterList.FirstOrDefault(v => v.Id == Voter.Id);
                if (Voter.Id > 0)
                {
                    tView1.SelectedNode = FindNode(tView1, Voter.Id.ToString());
                    // await AfterSelectNodeTVw1((int)tView1.SelectedNode.Tag);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async Task UpdateVoter()
        {
            try
            {
                var voter = BuildUpdateVoter();
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PutAsync("Voters", voter, voter.Id, token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
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
                Voter.DOB = dtpDOB.Value;
                Voter.PollingDivision = PollingDivisionsList.FirstOrDefault(p => p.Id == (int)cmbDivision.SelectedValue);
                return Voter;
            }

            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return null;
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


        #endregion

       
    }
}
