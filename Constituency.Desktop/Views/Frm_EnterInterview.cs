using Constituency.Desktop.Components;
using Constituency.Desktop.Entities;
using Constituency.Desktop.Helpers;
using Constituency.Desktop.Models;
using Microsoft.AppCenter.Crashes;
using System.Collections.ObjectModel;
using System.Data;

namespace Constituency.Desktop.Views
{
    public partial class Frm_EnterInterview : Form
    {
        //Fields
        private string token = string.Empty;

        //objects
        User user { get; set; }
        Voter Voter { get; set; }

        WaitFormFunc waitForm = new WaitFormFunc();


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
        private async Task LoadCanvas()
        {
            try
            {
                //UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.GetListAsync<Canvas>("Canvas", token);
               // UtilRecurrent.UnlockForm(waitForm, this);
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
                    if (canva.Interviews!=null)
                    {
                        int cant2 = canva.Interviews.Count();

                        foreach (Interview interview in canva.Interviews)
                        {
                            TreeNode node = new TreeNode(interview.Voter.FullName, 2, 3);
                            node.Tag = interview.Id;
                            childNodes.Add(node);
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
                groupBox1.Text = "Interviews List: ( " + Canvas.SelectMany(c => c.Interviews).Count().ToString("N") + " )";
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
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
        #endregion

        #region Save Interview
        private async void ibtnSaveVoter_Click(object sender, EventArgs e)
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
                    UtilRecurrent.ErrorMessage("Voter younger than 18.");
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

       
    }
}
