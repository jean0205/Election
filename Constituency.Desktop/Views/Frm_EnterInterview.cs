using Constituency.Desktop.Components;
using Constituency.Desktop.Controls;
using Constituency.Desktop.Entities;
using Constituency.Desktop.Helpers;
using Constituency.Desktop.Models;
using Microsoft.AppCenter.Crashes;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace Constituency.Desktop.Views
{
    public partial class Frm_EnterInterview : Form
    {
        //Fields
        private string token = string.Empty;

        //objects
        User user { get; set; }
        Voter Voter { get; set; }
        Interview Interview { get; set; }

        WaitFormFunc waitForm = new WaitFormFunc();

        bool newVoter = false;

        //list to load
        private ObservableCollection<Canvas> CanvasList;
        private List<ConstituencyC> ConstituenciesList;
        private List<PollingDivision> PollingDivisionsList;
        private List<Party> PartiesList;
        private List<Interviewer> InterviewersList;
        private List<Comment> CommentsList;
        public Frm_EnterInterview()
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            InitializeComponent();
            token = Main.GetInstance().tokenResponse.Token;
            user = Main.GetInstance().tokenResponse.User;
        }

        private async void Frm_EnterInterview_Load(object sender, EventArgs e)
        {
            FildsValidations();
            MandatoriesFilds();
            UtilRecurrent.LockForm(waitForm, this);
            await LoadCanvas();
            await LoadConstituencies();
            //await LoadComments();
            await LoadInterviewers();
            await LoadParties();
            UtilRecurrent.UnlockForm(waitForm, this);
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

        #region load Information
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
        private async Task LoadCanvas()
        {
            try
            {
                Response response;
                var roles = await LoadUsersRoles(user);
                if (roles != null && roles.Contains(UserAccess.All_Interviews.ToString()))
                {
                     response = await ApiServices.GetListAsync<Canvas>("Canvas/OpenAll", token);
                }
                else
                {
                     response = await ApiServices.GetListAsync<Canvas>("Canvas/OpenByUser", token);
                }              
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return;
                }
                // Applicant = new Applicant();
                CanvasList = new ObservableCollection<Canvas>((List<Canvas>)response.Result);
                if (CanvasList.Any())
                {
                    RefreshTreeView(CanvasList.ToList());

                    cmbCanvas.DataSource = null;
                    cmbCanvas.DataSource = CanvasList.ToList();
                    cmbCanvas.ValueMember = "Id";
                    cmbCanvas.DisplayMember = "Name";
                    cmbCanvas.SelectedItem = null;
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
        private async Task LoadInterviewers()
        {
            Response response = await ApiServices.GetListAsync<Interviewer>("Interviewers/Actives", token);
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
        private async Task LoadParties()
        {
            Response response = await ApiServices.GetListAsync<Party>("Parties", token);
            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return;
            }
            PartiesList = new((List<Party>)response.Result);
            if (PartiesList.Any())
            {
                cmbISupportedParty.DataSource = null;
                cmbISupportedParty.DataSource = PartiesList;
                cmbISupportedParty.ValueMember = "Id";
                cmbISupportedParty.DisplayMember = "Name";
                cmbISupportedParty.SelectedItem = null;
            }
        }
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
        private void RefreshTreeView(List<Canvas> Canvas)
        {
            try
            {
                tView1.Nodes.Clear();
                //TreeNode node;
                List<TreeNode> treeNodes = new List<TreeNode>();
                List<TreeNode> childNodes = new List<TreeNode>();

                foreach (Canvas canva in Canvas)
                {
                    if (canva.Interviews != null)
                    {
                        int cant2 = canva.Interviews.Count();

                        foreach (Interview interview in canva.Interviews)
                        {
                            TreeNode node = new TreeNode(interview.Voter.FullName, 2, 3);
                            node.Tag = interview.Id;
                            childNodes.Add(node);
                            addContextMenu(childNodes[childNodes.Count - 1], "Delete Interview");
                        }
                        treeNodes.Add(new TreeNode(canva.Name + " [" + cant2 + "]", 0, 1, childNodes.ToArray()));
                        treeNodes[treeNodes.Count - 1].Tag = canva.Id;
                        childNodes = new List<TreeNode>();                       
                    }
                }
                tView1.Nodes.AddRange(treeNodes.ToArray());
                tView1.ExpandAll();
                rjCollapseAll.Checked = true;
                lblExpand.Text = "Collapse All";
                tView1.Font = new Font("Segoe UI", 12, FontStyle.Regular);
                groupBox1.Text = "Interviews List: ( " + Canvas.SelectMany(c => c.Interviews).Count().ToString("N0") + " )";
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
                UtilRecurrent.FindAllControlsIterative(this.tpanelVoter, "ComboBox").Cast<ComboBox>().Where(c => c.Name != "cmbInterviewers").ToList().ForEach(x => x.SelectedIndex = -1);
                UtilRecurrent.FindAllControlsIterative(this.tpanelVoter, "DateTimePicker").Cast<DateTimePicker>().ToList().ForEach(x => x.Value = DateTime.Now.Date);
                Voter = new Voter();
                newVoter = false;

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }

        private async Task<Interview> LoadInterviewAsyncById(int id)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.FindAsync<Interview>("Interviews", id.ToString(), token);
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
                return (Interview)response.Result;
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
                        AfterSelectNodeCanvas((int)e.Node.Tag);
                    }
                    if (NodeLevel(e.Node) == 1)
                    {
                        AfterSelectNodeInterview((int)e.Node.Tag);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);

            }
        }
        private void AfterSelectNodeCanvas(int nodeTag)
        {
            FieldsWhite();
            try
            {
                if (nodeTag > 0)
                {
                    Interview = new();
                    Voter = new();
                    cmbCanvas.SelectedValue = nodeTag;
                    ibtnSaveVoter.Visible = true;
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
        private async void AfterSelectNodeInterview(int nodeTag)
        {
            FieldsWhite();
            try
            {
                if (nodeTag > 0)
                {
                    Interview = new();
                    Voter = new();
                    Interview = await LoadInterviewAsyncById(nodeTag);
                    ShowInterviewInformation();
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
        public int NodeLevel(TreeNode node)
        {
            int level = 0;
            while ((node = node.Parent) != null) level++;
            return level;
        }
        private void ShowInterviewInformation()
        {
            try
            {
                Voter = Interview.Voter;
                ShowVoterInformation();
                cmbCanvas.SelectedValue = Interview.Canvas.Id;
                cmbISupportedParty.SelectedValue = Interview.SupportedParty.Id;
                cmbInterviewers.SelectedValue = Interview.Interviewer.Id;
                // cmbIComment.SelectedValue = Interview.Comment.Id;
                txtIOtherComment.Text = Interview.OtherComment;
                dtpIDate.Value = Interview.Date;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
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
        private async Task UpdateVoter()
        {
            try
            {
                var voter = BuildVoter();
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
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        #endregion

        #region Save/update Interview
        private async void ibtnSaveVoter_Click(object sender, EventArgs e)
        {
            try
            {
                if (PaintRequiredVoter())
                {
                    UtilRecurrent.ErrorMessage("Requireds fields missing. Find them highlighted in red.");
                    return;
                }
                if (cmbISupportedParty.SelectedValue == null && cmbIComment.SelectedValue == null)
                {
                    UtilRecurrent.ErrorMessage("No supported party or comment selected.");
                    return;
                }

                if (txtEmail.TextLength > 0 && !UtilRecurrent.IsValidEmail(txtEmail.Text.Trim()))
                {
                    UtilRecurrent.ErrorMessage("You must provide a valid Email address.");
                    return;
                }
                if (await SaveInterview())
                {
                    await LoadCanvas();
                }
                else { return; }

                if (Interview != null && Interview.Id > 0)
                {

                    tView1.SelectedNode = CollectAllNodes(tView1.Nodes).FirstOrDefault(x => int.Parse(x.Tag.ToString()) == Interview.Id);
                    //await AfterSelectNodeTVw1((int)tView1.SelectedNode.Tag);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }

        }
        private async Task<bool> SaveInterview()
        {
            try
            {
                var voter = BuildVoter();
                if (VoterInterviewedAlready(voter))
                {
                    UtilRecurrent.ErrorMessage("Voter already interviewed in the selected Canvas.");
                    return false;
                }
                voter.Active = true;
                var interview = await BuildInterview(null);
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PostAsync("Interviews", interview, token);
                UtilRecurrent.UnlockForm(waitForm, this);

                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response.IsSuccess;
                }

                Interview = (Interview)response.Result;               
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
        private async void ibtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //TODO valorar si debo crear varios gets en los controladores
                // unos q carguen todas las relaciones y otros
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
                if (txtEmail.TextLength > 0 && !UtilRecurrent.IsValidEmail(txtEmail.Text.Trim()))
                {
                    UtilRecurrent.ErrorMessage("You must provide a valid Email address.");
                    return;
                }
                if (await UpdateInterview((int)tView1.SelectedNode.Tag))
                {
                    await LoadCanvas();

                }
                if (Interview != null && Interview.Id > 0)
                {
                    tView1.SelectedNode = CollectAllNodes(tView1.Nodes).Where(n => NodeLevel(n) == 1).FirstOrDefault(x => int.Parse(x.Tag.ToString()) == Interview.Id);
                    //await AfterSelectNodeTVw1((int)tView1.SelectedNode.Tag);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async Task<bool> UpdateInterview(int? id)
        {
            try
            {
                if (newVoter && VoterInterviewedAlready(BuildVoter()))
                {
                    UtilRecurrent.ErrorMessage("Voter already interviewed in the selected Canvas.");
                    return false;
                }

                var interview = await BuildInterview(id);
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PutAsync("Interviews", interview, interview.Id, token);
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
        private Voter BuildVoter()
        {
            try
            {
                var voter = new Voter();
                voter.Id = Voter.Id;
                List<PropertyInfo> properties = Voter.GetType().GetProperties().ToList();
                List<TextBox> voterTextBox = UtilRecurrent.FindAllTextBoxIterative(tpanelVoter);
                foreach (PropertyInfo prop in properties)
                {
                    if (voterTextBox.Where(p => p.Name.Replace("txt", string.Empty) == prop.Name).Any())
                    {
                        prop.SetValue(voter, voterTextBox.Where(p => p.Name.Replace("txt", string.Empty) == prop.Name).FirstOrDefault().Text.TrimEnd().ToUpper());
                    }
                }
                voter.Sex = cmbSex.SelectedItem.ToString();
                Voter.DOB = dtpDOB.Value.Date == DateTime.Today ? null : dtpDOB.Value;
                voter.PollingDivision = PollingDivisionsList.FirstOrDefault(p => p.Id == (int)cmbDivision.SelectedValue);
                return voter;
            }

            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return null;
            }
        }
        private async Task<Interview> BuildInterview(int? id)
        {
            try
            {
                Interview = new Interview();
                Interview.Canvas = (Canvas)cmbCanvas.SelectedItem;
                Interview.Canvas.Type.Canvas = null;
                Interview.Canvas.Interviews = null;
                Interview.Interviewer = (Interviewer)cmbInterviewers.SelectedItem;
                Interview.Interviewer.Interviews = null;

                Interview.SupportedParty = (Party)cmbISupportedParty.SelectedItem;
                if (cmbIComment.SelectedItem != null)
                {
                    Interview.Comment = new Comment();
                    Interview.Comment = (Comment)cmbIComment.SelectedItem;
                }
                Interview.Date = dtpIDate.Value;
                Interview.OtherComment = txtIOtherComment.Text.ToUpper();
                var voter2 = BuildVoter();
                if (MySerializer.VoterEquealtoVpter2(Voter, voter2))
                {
                    Interview.Voter = Voter;
                    Voter.Interviews = null;
                    Voter.ElectionVotes = null;
                    Interview.Voter = Voter;
                }
                else
                {
                    await UpdateVoter();
                    Voter = await LoadVoterAsyncById(Voter.Id);
                    Voter.Interviews = null;
                    Voter.ElectionVotes = null;
                    Interview.Voter = Voter;
                }
                if (id != null)
                {
                    Interview.Id = (int)id;
                }
                else
                {
                    //creating the interview for first time, I don't want to update the user, just the one who created
                    Interview.RecorderBy = user;
                }               
                return Interview;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return null;
            }
        }
        #endregion

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
        
        private bool VoterInterviewedAlready(Voter voter)
        {
            var canvas = CanvasList.FirstOrDefault(p => p.Id == (int)cmbCanvas.SelectedValue);
            if (canvas.Interviews!=null && canvas.Interviews.Any()&& canvas.Interviews.Where(p => p.Voter.Id == voter.Id).Any())
            {
                return true;
            }
            return false;
        }

        private async void txtReg_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13 && ((TextBox)sender).TextLength > 0)
                {
                    var vot = await LoadVoterByRegAsync("Voters/FindRegistration", ((TextBox)sender).Text);

                    if (vot != null)
                    {
                        newVoter = true;
                        Voter = vot;
                        ShowVoterInformation();
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); 
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }

        private async void ibtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                await LoadCanvas();
                tView1.SelectedNode = tView1.Nodes[0];
                lblFiltering.Visible = false;
                UtilRecurrent.UnlockForm(waitForm, this);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                UtilRecurrent.UnlockForm(waitForm, this);
            }
        }
        public void addContextMenu(TreeNode tvw, string item)
        {
            try
            {
                ContextMenuStrip _contextmenu = new ContextMenuStrip();
                _contextmenu.Items.Add(item);
                _contextmenu.ItemClicked += contextmenu_click;
                tvw.ContextMenuStrip = _contextmenu;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async void contextmenu_click(object sender, ToolStripItemClickedEventArgs e)
        {

            switch (e.ClickedItem.Text)
            {
                case "Delete Interview":
                    {
                        if (UtilRecurrent.yesOrNot("Are you sure you want to delete this interview?", "Delete Interview"))
                        {
                            await DeleteAsyncGeneric("Interviews", Interview.Id);
                            await LoadCanvas();
                            if (tView1.Nodes.Count > 0)
                            {
                                tView1.SelectedNode = tView1.Nodes[0];
                            }
                            UtilRecurrent.UnlockForm(waitForm, this);
                        }
                        break;
                    }
            }
        }
        private async Task DeleteAsyncGeneric(string controller, int id)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.DeleteAsync(controller, id, token);
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
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }

        private void tView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //// Make sure this is the right button.
                //if (e.Button != MouseButtons.Left) return;

                // Select this node.
                TreeNode node_here = ((TreeView)sender).GetNodeAt(e.X, e.Y);
                ((TreeView)sender).SelectedNode = node_here;

                // See if we got a node.
                if (node_here == null)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
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
    }
}
