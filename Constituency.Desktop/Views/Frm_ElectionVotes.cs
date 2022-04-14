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
    public partial class Frm_ElectionVotes : Form
    {
        //Fields
        private string token = string.Empty;

        //objects
        User user { get; set; }
        Voter Voter { get; set; }
        ElectionVote ElectionVote { get; set; }

        WaitFormFunc waitForm = new WaitFormFunc();

        private ObservableCollection<NationalElection> ElectionList;
        private ObservableCollection<ElectionVote> ElectionVotesList;
        private List<ConstituencyC> ConstituenciesList;
        private List<PollingDivision> PollingDivisionsList;
        private List<Party> PartiesList;
        private List<Interviewer> InterviewersList;
        private List<Comment> CommentsList;



        public Frm_ElectionVotes()
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            InitializeComponent();
            token = Main.GetInstance().tokenResponse.Token;
            user = Main.GetInstance().tokenResponse.User;
        }

        private async void Frm_ElectionVotes_Load(object sender, EventArgs e)
        {
            FildsValidations();
            MandatoriesFilds();
            UtilRecurrent.LockForm(waitForm, this);
            await LoadConstituencies();
            //await LoadComments();
            await LoadInterviewers();
           // await LoadParties();
            await LoadElections();
            UtilRecurrent.UnlockForm(waitForm, this);
            ibtnUpdate.Visible = false;

        }
        #region Mandatories and Validations

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
                toolTip1.SetToolTip(dtpIDate, "Mandatory Field");

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
        private bool PaintRequiredVoter()
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
        private async Task LoadElections()
        {
            try
            {
                //UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.GetListAsync<NationalElection>("NationalElections/Votes", token);
                // UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return;
                }
                // Applicant = new Applicant();
                ElectionList = new ObservableCollection<NationalElection>((List<NationalElection>)response.Result);
                if (ElectionList != null && ElectionList.Any())
                {
                    RefreshTreeView(ElectionList.ToList());
                    cmbElection.DataSource = null;
                    cmbElection.DataSource = ElectionList.Where(e => e.Open).ToList();



                    cmbElection.ValueMember = "Id";
                    cmbElection.DisplayMember = "ElectionDate";
                    cmbElection.SelectedItem = null;
                    
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
        //private async Task LoadElectionsVotes()
        //{
        //    try
        //    {
        //        //UtilRecurrent.LockForm(waitForm, this);
        //        Response response = await ApiServices.GetListAsync<ElectionVote>("ElectionVotes", token);
        //        // UtilRecurrent.UnlockForm(waitForm, this);
        //        if (!response.IsSuccess)
        //        {
        //            UtilRecurrent.ErrorMessage(response.Message);
        //            return;
        //        }
        //        // Applicant = new Applicant();
        //        ElectionVotesList = new ObservableCollection<ElectionVote>((List<ElectionVote>)response.Result);
        //        if (ElectionVotesList!=null && ElectionVotesList.Any())
        //        {
        //            RefreshTreeView(ElectionVotesList.ToList());                   
        //        }
        //        else
        //        {
        //            cleanScreen();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        UtilRecurrent.UnlockForm(waitForm, this);
        //        Crashes.TrackError(ex);
        //        UtilRecurrent.ErrorMessage(ex.Message);
        //    }
        //}
        private async Task LoadInterviewers()
        {
            Response response = await ApiServices.GetListAsync<Interviewer>("Interviewers", token);
            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return;
            }
            InterviewersList = new((List<Interviewer>)response.Result);
            if (InterviewersList.Any())
            {
                cmbInterviewers.DataSource = null;
                cmbInterviewers.DataSource = InterviewersList;
                cmbInterviewers.ValueMember = "Id";
                cmbInterviewers.DisplayMember = "FullName";
                cmbInterviewers.SelectedItem = null;
            }
        }
        //private async Task LoadParties()
        //{
        //    Response response = await ApiServices.GetListAsync<Party>("Parties", token);
        //    if (!response.IsSuccess)
        //    {
        //        UtilRecurrent.ErrorMessage(response.Message);
        //        return;
        //    }
        //    PartiesList = new((List<Party>)response.Result);
        //    if (PartiesList.Any())
        //    {
        //        cmbISupportedParty.DataSource = null;
        //        cmbISupportedParty.DataSource = PartiesList;
        //        cmbISupportedParty.ValueMember = "Id";
        //        cmbISupportedParty.DisplayMember = "Name";
        //        cmbISupportedParty.SelectedItem = null;
        //    }
        //}
        private async Task LoadComments()
        {
            Response response = await ApiServices.GetListAsync<Comment>("Comments", token);
            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return;
            }
            CommentsList = new((List<Comment>)response.Result);
            if (CommentsList.Any())
            {
                cmbIComment.DataSource = null;
                cmbIComment.DataSource = PartiesList;
                cmbIComment.ValueMember = "Id";
                cmbIComment.DisplayMember = "Text";
                cmbIComment.SelectedItem = null;
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
        private void RefreshTreeView(List<NationalElection> ElectionVotes)
        {
            try
            {
                tView1.Nodes.Clear();
                //TreeNode node;
                List<TreeNode> treeNodes = new List<TreeNode>();
                List<TreeNode> childNodes = new List<TreeNode>();

                foreach (NationalElection election in ElectionVotes)
                {
                    int cant2 = 0;
                    if (election.ElectionVotes != null && election.ElectionVotes.Any())
                    {
                        cant2 = election.ElectionVotes.Count;

                        foreach (ElectionVote vote in election.ElectionVotes)
                        {
                            TreeNode node = new TreeNode(vote.Voter.FullName, 2, 3);
                            node.Tag = vote.Id;
                            childNodes.Add(node);
                        }
                    }
                    treeNodes.Add(new TreeNode(election.ElectionDate.ToString("MMMM-yyyy") + " [" + cant2 + "]", 0, 1, childNodes.ToArray()));
                    treeNodes[treeNodes.Count - 1].Tag = election.Id;
                    childNodes = new List<TreeNode>();
                }
                tView1.Nodes.AddRange(treeNodes.ToArray());
                tView1.ExpandAll();
                rjCollapseAll.Checked = true;
                lblExpand.Text = "Collapse All";
                tView1.Font = new Font("Segoe UI", 12, FontStyle.Regular);
                groupBox1.Text = "Interviews List: ( " + ElectionList.Count.ToString("N0") + " )";
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
        private async Task<ElectionVote> LoadElectionVoteAsyncById(int id)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.FindAsync<ElectionVote>("ElectionVotes", id.ToString(), token);
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
                return (ElectionVote)response.Result;
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
                    if (NodeLevel(e.Node) == 0)
                    {
                        AfterSelectNodeElection((int)e.Node.Tag);
                    }
                    if (NodeLevel(e.Node) == 1)
                    {
                        AfterSelectNodeVote((int)e.Node.Tag);
                    }
                }

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void AfterSelectNodeElection(int nodeTag)
        {
            FieldsWhite();
            try
            {
                if (nodeTag > 0)
                {
                    cmbElection.SelectedValue = nodeTag;
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
        private async void AfterSelectNodeVote(int nodeTag)
        {
            FieldsWhite();
            try
            {
                if (nodeTag > 0)
                {
                    ElectionVote = new();
                    ElectionVote = await LoadElectionVoteAsyncById(nodeTag);
                    ShowElectionVoteInformation();
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
        private void ShowElectionVoteInformation()
        {
            try
            {
                Voter = ElectionVote.Voter;
                ShowVoterInformation();               
                cmbInterviewers.SelectedValue = ElectionVote.Interviewer.Id;
                cmbElection.SelectedValue = ElectionVote.Election.Id;
                cmbISupportedParty.SelectedValue = ElectionVote.SupportedParty.Id;               
                // cmbIComment.SelectedValue = Interview.Comment.Id;
                txtIOtherComment.Text = ElectionVote.OtherComment;
                dtpIDate.Value = ElectionVote.VoteTime;
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
                    dtpDOB.Value = Voter.DOB.Date == new DateTime(1, 1, 0001) ? DateTime.Today : Voter.DOB;
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
                if (PaintRequiredVoter())
                {
                    UtilRecurrent.ErrorMessage("Requireds fields missing. Find them highlighted in red.");
                    return;
                }
                if (txtEmail.TextLength > 0 && !UtilRecurrent.IsValidEmail(txtEmail.Text.Trim()))
                {
                    UtilRecurrent.ErrorMessage("You must provide a valid Email address.");
                    return;
                }
                if (await SaveVote())
                {
                    await LoadElections();
                }

                if (ElectionVote != null && ElectionVote.Id > 0)
                {

                    tView1.SelectedNode = CollectAllNodes(tView1.Nodes).FirstOrDefault(x => int.Parse(x.Tag.ToString()) == ElectionVote.Id);
                    //await AfterSelectNodeTVw1((int)tView1.SelectedNode.Tag);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async Task<bool> SaveVote()
        {
            try
            {
                var electionVote = await BuildElectionVote(null);
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PostAsync("ElectionVotes", ElectionVote, token);
                UtilRecurrent.UnlockForm(waitForm, this);

                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response.IsSuccess;
                }

                ElectionVote = (ElectionVote)response.Result;
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
        private async Task<ElectionVote> BuildElectionVote(int? id)
        {
            try
            {
                ElectionVote = new();
                ElectionVote.Election = cmbElection.SelectedItem as NationalElection;
                ElectionVote.VoteTime = dtpIDate.Value;
                ElectionVote.Voter = Voter;
                ElectionVote.SupportedParty = cmbISupportedParty.SelectedItem as Party;
                ElectionVote.Comment = cmbIComment.SelectedItem as Comment;
                ElectionVote.OtherComment = txtIOtherComment.Text.ToUpper();
                ElectionVote.Interviewer = cmbInterviewers.SelectedItem as Interviewer;
                //making null what is not necesary
                ElectionVote.Voter.Interviews = null;
                ElectionVote.Interviewer.Interviews = null;
                ElectionVote.Election.ElectionVotes = null;

                if (id != null)
                {
                    ElectionVote.Id = (int)id;
                }
                return ElectionVote;
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
                //TODO valorar si debo crear varios gets en los controladores unos q carguen todas las relaciones y otros
                //que solo cargen la entidad principal como aqui en interview que estoy leyendo el voter con las interviews y despues tengo que hacerlas null
                if (tView1.SelectedNode == null || (int)tView1.SelectedNode.Tag == 0)
                {
                    return;
                }
                if (PaintRequiredVoter())
                {
                    UtilRecurrent.ErrorMessage("Requireds fields missing. Find them highlighted in red.");
                    return;
                }
                if (dtpDOB.Value.AddYears(18) > DateTime.Now)
                {
                    UtilRecurrent.ErrorMessage("Voter younger than 18.");
                    return;
                }
                if (txtEmail.TextLength > 0 && !UtilRecurrent.IsValidEmail(txtEmail.Text.Trim()))
                {
                    UtilRecurrent.ErrorMessage("You must provide a valid Email address.");
                    return;
                }
                if (await UpdateElectionVote((int)tView1.SelectedNode.Tag))
                {
                    await LoadElections();
                }
                if (ElectionVote != null && ElectionVote.Id > 0)
                {
                    tView1.SelectedNode = CollectAllNodes(tView1.Nodes).FirstOrDefault(x => int.Parse(x.Tag.ToString()) == ElectionVote.Id);
                    //await AfterSelectNodeTVw1((int)tView1.SelectedNode.Tag);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async Task<bool> UpdateElectionVote(int? id)
        {
            try
            {

                var electionVote = await BuildElectionVote(id);
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PutAsync("ElectionVotes", electionVote, electionVote.Id, token);
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
            {
                if (e.KeyChar == (char)13 && ((TextBox)sender).TextLength > 0)
                {
                    var vot = await LoadVoterByRegAsync("Voters/FindRegistration", ((TextBox)sender).Text);

                    if (vot != null)
                    {
                        Voter = vot;
                        ShowVoterInformation();
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

        private void cmbElection_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void cmbElection_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {

                cmbISupportedParty.DataSource = null;
                if (cmbElection.SelectedItem != null && (cmbElection.SelectedItem as NationalElection).Parties != null && (cmbElection.SelectedItem as NationalElection).Parties.Any())
                {
                    var parties = (cmbElection.SelectedItem as NationalElection).Parties;
                    cmbISupportedParty.DataSource = null;
                    cmbISupportedParty.DataSource = parties;
                    cmbISupportedParty.ValueMember = "Id";
                    cmbISupportedParty.DisplayMember = "Name";
                    cmbISupportedParty.SelectedItem = null;
                }



            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
    }
}
