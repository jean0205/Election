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
            await LoadVoters();
            await LoadConstituencies();
        }

        #region Tab1
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
        private TreeNode FindNode(TreeView tvw, string applicantId)
        {
            try
            {
                foreach (TreeNode node in tvw.Nodes)
                {
                    if (node.Nodes.Cast<TreeNode>().Where(x => x.Tag.ToString() == applicantId).Any())
                    {
                        return node.Nodes.Cast<TreeNode>().FirstOrDefault(x => x.Tag.ToString() == applicantId);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
            return (TreeNode)null;
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

                foreach (ConstituencyC contituency in Voters.Select(v => v.PollingDivision.Constituency).Distinct())
                {
                    int cant2 = VoterList.Where(v => v.PollingDivision.Constituency.Id == contituency.Id).Count();
                    foreach (PollingDivision division in contituency.PollingDivisions.Distinct())
                    {
                        int cant = VoterList.Where(v => v.PollingDivision.Id == division.Id).Count();
                        foreach (Voter voter in VoterList.Where(v => v.PollingDivision.Id == division.Id))
                        {
                            TreeNode node2 = new TreeNode(voter.FullName, 2, 3);
                            node2.Tag = voter.Id;
                            childNodes2.Add(node2);
                        }
                        childNodes.Add(new TreeNode(division.Name + " [" + cant + "]", 0, 1, childNodes2.ToArray()));
                        childNodes2 = new List<TreeNode>();
                    }
                    TreeNode node = new TreeNode(contituency.Name + " [" + cant2 + "]", 0, 1);
                    node.Tag = contituency.Id;
                    treeNodes.Add(node);
                }
                tView1.Nodes.AddRange(treeNodes.ToArray());
                tView1.ExpandAll();
                rjCollapseAll.Checked = true;
                lblExpand.Text = "Collapse All";
                tView1.Font = new Font("Segoe UI", 12, FontStyle.Regular);
                groupBox1.Text = "Voters List ( " + Voters.Count + " )";

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void cleanScreen()
        {
            //try
            //{
            //    UtilRecurrent.FindAllControlsIterative(this.tabControl1, "TextBox").Cast<TextBox>().ToList().ForEach(x => x.Clear());
            //    UtilRecurrent.FindAllControlsIterative(this.tabControl1, "ComboBox").Cast<ComboBox>().ToList().ForEach(x => x.SelectedIndex = -1);
            //    UtilRecurrent.FindAllControlsIterative(this.tabControl1, "RadioButton").Cast<RadioButton>().ToList().ForEach(x => x.Checked = false);
            //    UtilRecurrent.FindAllControlsIterative(this.tabControl1, "DataGridView").Cast<DataGridView>().ToList().ForEach(x => x.DataSource = null);
            //    UtilRecurrent.FindAllControlsIterative(this.tabControl1, "DateTimePicker").Cast<DateTimePicker>().ToList().ForEach(x => x.Value = DateTime.Now.Date);
            //    Applicant = new Applicant();
            //    ibtnSaveApp.Visible = true;
            //    ibtnUpdate.Visible = false;
            //    tabControl2.Visible = false;
            //    tableLayoutPanel50.Visible = false;
            //    rjExtendSearch.Enabled = false;
            //    rjMoreInfo.Enabled = false;
            //}
            //catch (Exception ex)
            //{
            //    Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            //}
        }
        private async void iconButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpDOB.Value.AddYears(18) > DateTime.Now)
                {
                    UtilRecurrent.ErrorMessage("Applicant younger than 18.");
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
        #endregion

        #region Events
        private void cmbConstituency_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbConstituency.SelectedIndex > -1)
                {
                    if (ConstituenciesList.Where(p => p.Id == (int)cmbConstituency.SelectedValue).Select(p => p.PollingDivisions).Any())
                    {
                        cmbDivision.DataSource = null;
                        PollingDivisionsList = new List<PollingDivision>();
                        PollingDivisionsList= ConstituenciesList.Where(p => p.Id == (int)cmbConstituency.SelectedValue).SelectMany(p => p.PollingDivisions).ToList();
                        cmbDivision.DataSource = PollingDivisionsList;
                        cmbDivision.ValueMember = "Id";
                        cmbDivision.DisplayMember = "Name";                      
                        cmbDivision.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }

        }
        #endregion




    }
}
