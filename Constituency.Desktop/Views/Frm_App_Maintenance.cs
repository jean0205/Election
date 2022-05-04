
using Constituency.Desktop.Components;
using Constituency.Desktop.Controls;
using Constituency.Desktop.Entities;
using Constituency.Desktop.Helpers;
using Constituency.Desktop.Models;
using Microsoft.AppCenter.Crashes;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Reflection;

namespace Constituency.Desktop.Views
{
    public partial class Frm_App_Maintenance : Form
    {
        WaitFormFunc waitForm = new WaitFormFunc();

        private string token = string.Empty;

        public Frm_App_Maintenance()
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            InitializeComponent();
            token = Main.GetInstance().tokenResponse.Token;
        }
        private async void Frm_App_Maintenance_Load(object sender, EventArgs e)
        {
            cleanScreen();
            MandatoriesFilds();
            DGVFormats();
            FildsValidations();
            await LoadInfo();
           // tabControl1.TabPages.RemoveAt(8);

        }
        public async Task LoadInfo()
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                await LoadConstituencies();
                await LoadParties();
                await LoadCanvasTypes();
                await LoadCanvas();
                await LoadInterviewers();
                await LoadNationalElections();
                await LoadComments();
                UtilRecurrent.UnlockForm(waitForm, this);
            }
            catch (Exception ex)
            {
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }

       

        #region General events
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion
        #region Mandatories and clear screen
        private void MandatoriesFilds()
        {//tag= 1 para campos obligados

            UtilRecurrent.FindAllControlsIterative(tabControl1, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1")).ToList().ForEach(x => toolTip1.SetToolTip(x, "Mandatory Field"));
            UtilRecurrent.FindAllControlsIterative(tabControl1, "ComboBox").Cast<ComboBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1")).ToList().ForEach(x => toolTip1.SetToolTip(x, "Mandatory Field"));
        }
        private void DGVFormats()
        {//tag= 1 para campos obligados

            UtilRecurrent.FindAllControlsIterative(tabControl1, "DataGridView").Cast<DataGridView>().ToList().ForEach(x => x.DefaultCellStyle.BackColor = Color.Beige);
            UtilRecurrent.FindAllControlsIterative(tabControl1, "DataGridView").Cast<DataGridView>().ToList().ForEach(x => x.AlternatingRowsDefaultCellStyle.BackColor = Color.Bisque);
            UtilRecurrent.FindAllControlsIterative(tabControl1, "DataGridView").Cast<DataGridView>().ToList().ForEach(x => x.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells);
            UtilRecurrent.FindAllControlsIterative(tabControl1, "DataGridView").Cast<DataGridView>().ToList().ForEach(x => x.RowHeadersVisible = false);

        }


        private bool PaintRequiredFildsConstituency()
        {
            bool missing = false;
            //todos en blanco antes de comprobar
            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel2, "TextBox").Cast<TextBox>().ToList().ForEach(t => t.BackColor = Color.FromKnownColor(KnownColor.Window));

            //validando textbox requeridos no vacios
            if (UtilRecurrent.FindAllControlsIterative(tableLayoutPanel2, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1") && (x.TextLength == 0 || string.IsNullOrWhiteSpace(x.Text))).ToList().Any())
            {
                UtilRecurrent.FindAllControlsIterative(tableLayoutPanel2, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1") && (x.TextLength == 0 || string.IsNullOrWhiteSpace(x.Text))).ToList().ForEach(t => t.BackColor = Color.LightSalmon);
                missing = true;
            }
            return missing;
        }
        private bool PaintRequiredFildsCanvasType()
        {
            bool missing = false;
            //todos en blanco antes de comprobar
            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel8, "TextBox").Cast<TextBox>().ToList().ForEach(t => t.BackColor = Color.FromKnownColor(KnownColor.Window));

            //validando textbox requeridos no vacios
            if (UtilRecurrent.FindAllControlsIterative(tableLayoutPanel8, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1") && (x.TextLength == 0 || string.IsNullOrWhiteSpace(x.Text))).ToList().Any())
            {
                UtilRecurrent.FindAllControlsIterative(tableLayoutPanel8, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1") && (x.TextLength == 0 || string.IsNullOrWhiteSpace(x.Text))).ToList().ForEach(t => t.BackColor = Color.LightSalmon);
                missing = true;
            }
            return missing;
        }
        private bool PaintRequiredFildsCanvas()
        {
            bool missing = false;
            //todos en blanco antes de comprobar
            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel10, "TextBox").Cast<TextBox>().ToList().ForEach(t => t.BackColor = Color.FromKnownColor(KnownColor.Window));
            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel10, "ComboBox").Cast<ComboBox>().ToList().ForEach(t => t.BackColor = Color.FromKnownColor(KnownColor.Window));

            //validando textbox requeridos no vacios
            if (UtilRecurrent.FindAllControlsIterative(tableLayoutPanel10, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1") && (x.TextLength == 0 || string.IsNullOrWhiteSpace(x.Text))).ToList().Any())
            {
                UtilRecurrent.FindAllControlsIterative(tableLayoutPanel10, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1") && (x.TextLength == 0 || string.IsNullOrWhiteSpace(x.Text))).ToList().ForEach(t => t.BackColor = Color.LightSalmon);
                missing = true;
            }
            if (UtilRecurrent.FindAllControlsIterative(tableLayoutPanel10, "ComboBox").Cast<ComboBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1") && x.Text == string.Empty).ToList().Any())
            {
                UtilRecurrent.FindAllControlsIterative(tableLayoutPanel10, "ComboBox").Cast<ComboBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1") && x.Text == string.Empty).ToList().ForEach(t => t.BackColor = Color.LightSalmon);
                missing = true;
            }
            return missing;
        }
        private bool PaintRequiredFildsInterviewer()
        {
            bool missing = false;
            //todos en blanco antes de comprobar
            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel12, "TextBox").Cast<TextBox>().ToList().ForEach(t => t.BackColor = Color.FromKnownColor(KnownColor.Window));
            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel12, "ComboBox").Cast<ComboBox>().ToList().ForEach(t => t.BackColor = Color.FromKnownColor(KnownColor.Window));

            //validando textbox requeridos no vacios
            if (UtilRecurrent.FindAllControlsIterative(tableLayoutPanel12, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1") && (x.TextLength == 0 || string.IsNullOrWhiteSpace(x.Text))).ToList().Any())
            {
                UtilRecurrent.FindAllControlsIterative(tableLayoutPanel12, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1") && (x.TextLength == 0 || string.IsNullOrWhiteSpace(x.Text))).ToList().ForEach(t => t.BackColor = Color.LightSalmon);
                missing = true;
            }
            if (UtilRecurrent.FindAllControlsIterative(tableLayoutPanel12, "ComboBox").Cast<ComboBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1") && x.Text == string.Empty).ToList().Any())
            {
                UtilRecurrent.FindAllControlsIterative(tableLayoutPanel12, "ComboBox").Cast<ComboBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1") && x.Text == string.Empty).ToList().ForEach(t => t.BackColor = Color.LightSalmon);
                missing = true;
            }
            return missing;
        }
        private void cleanScreen()
        {
            UtilRecurrent.FindAllControlsIterative(tabControl1, "TextBox").Cast<TextBox>().ToList().ForEach(x => x.Clear());

            UtilRecurrent.FindAllControlsIterative(tabControl1, "RJToggleButton").Cast<RJToggleButton>().ToList().ForEach(x => x.Checked = true);
            UtilRecurrent.FindAllControlsIterative(tabControl1, "ComboBox").Cast<ComboBox>().ToList().ForEach(x => x.SelectedItem = null);
            UtilRecurrent.FindAllControlsIterative(tabControl1, "DateTimePicker").Cast<DateTimePicker>().ToList().ForEach(x => x.Value = DateTime.Today);

            //sino resulta hay q hacerlo separado
            //UtilRecurrent.FindAllControlsIterative(tableLayoutPanel2, "TextBox").Cast<TextBox>().ToList().ForEach(x => x.Clear());

            //UtilRecurrent.FindAllControlsIterative(tableLayoutPanel2, "RJToggleButton").Cast<RJToggleButton>().ToList().ForEach(x => x.Checked = true);
            //cmbConstituency.SelectedItem = null;
            //UtilRecurrent.FindAllControlsIterative(tableLayoutPanel4, "TextBox").Cast<TextBox>().ToList().ForEach(x => x.Clear());

            //UtilRecurrent.FindAllControlsIterative(tableLayoutPanel4, "RJToggleButton").Cast<RJToggleButton>().ToList().ForEach(x => x.Checked = true);
            //UtilRecurrent.FindAllControlsIterative(tableLayoutPanel6, "TextBox").Cast<TextBox>().ToList().ForEach(x => x.Clear());

            //UtilRecurrent.FindAllControlsIterative(tableLayoutPanel6, "RJToggleButton").Cast<RJToggleButton>().ToList().ForEach(x => x.Checked = true);

            ////CanvasTypes
            //UtilRecurrent.FindAllControlsIterative(tableLayoutPanel8, "TextBox").Cast<TextBox>().ToList().ForEach(x => x.Clear());
            //UtilRecurrent.FindAllControlsIterative(tableLayoutPanel8, "RJToggleButton").Cast<RJToggleButton>().ToList().ForEach(x => x.Checked = true);

            ////Canvas
            //UtilRecurrent.FindAllControlsIterative(tableLayoutPanel10, "TextBox").Cast<TextBox>().ToList().ForEach(x => x.Clear());
            //UtilRecurrent.FindAllControlsIterative(tableLayoutPanel10, "RJToggleButton").Cast<RJToggleButton>().ToList().ForEach(x => x.Checked = true);
            //cmbCanvasTypes.SelectedItem = null;
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
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        #endregion

        #region Constituencies
        private ObservableCollection<ConstituencyC> ConstituenciesList;
        ConstituencyC constituency;
        PollingDivision pollingDivision;


        private async Task LoadConstituencies()
        {
            //UtilRecurrent.LockForm(waitForm, this);
            Response response = await ApiServices.GetListAsync<ConstituencyC>("Constituencies", token);
            // UtilRecurrent.UnlockForm(waitForm, this);
            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return;
            }
            ConstituenciesList = new ObservableCollection<ConstituencyC>((List<ConstituencyC>)response.Result);
            if (ConstituenciesList.Any())
            {
                RefreshTreeViewConstituency(ConstituenciesList.ToList());
                RefreshTreeViewPolling(ConstituenciesList.ToList());
                cmbConstituency.DataSource = null;
                cmbConstituency.DataSource = ConstituenciesList.Where(c => c.Active).ToList();
                cmbConstituency.ValueMember = "Id";
                cmbConstituency.DisplayMember = "Name";
                cmbConstituency.SelectedItem = null;
                //tView1.SelectedNode = tView1.Nodes[0];
            }
            else
            {
                cleanScreen();
                tView1.Nodes.Clear();
            }
        }
        private void RefreshTreeViewConstituency(List<ConstituencyC> constituencies)
        {
            try
            {
                tView1.Nodes.Clear();
                List<TreeNode> treeNodes = new List<TreeNode>();
                List<TreeNode> childNodes = new List<TreeNode>();


                foreach (ConstituencyC consttuency in constituencies)
                {
                    childNodes.Add(new TreeNode(consttuency.Name, 2, 1));
                    childNodes[childNodes.Count - 1].Tag = consttuency.Id;
                    addContextMenu(childNodes[childNodes.Count - 1], "Delete Constituency");
                }
                treeNodes.Add(new TreeNode("Constituencies", 0, 0, childNodes.ToArray()));
                treeNodes[treeNodes.Count - 1].Tag = 0;
                childNodes = new List<TreeNode>();

                tView1.Nodes.AddRange(treeNodes.ToArray());
                tView1.ExpandAll();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }

        private void tView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AfterSelectNodeTV1((int)e.Node.Tag);
        }
        private void AfterSelectNodeTV1(int nodeTag)
        {
            try
            {
                if (nodeTag > 0)
                {
                    constituency = ConstituenciesList.Where(u => u.Id == nodeTag).FirstOrDefault();
                    showConstituencyInfo(constituency);
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
        private void showConstituencyInfo(ConstituencyC consty)
        {
            try
            {
                txtSGSE.Text = consty.SGSE;
                txtName.Text = consty.Name;
                rjActive.Checked = consty.Active;
                txtPollings.Clear();
                var pd = consty.PollingDivisions.Select(p => p.Name).ToList();
                pd.ForEach(p => txtPollings.Text += p.ToString() + "\r\n");


            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async void ibtnSave_Click(object sender, EventArgs e)
        {
            if (PaintRequiredFildsConstituency())
            {
                UtilRecurrent.ErrorMessage("Requireds fields missing. Find them highlighted in red.");
                return;
            }
            try
            {
                if (tView1.SelectedNode == null || (tView1.SelectedNode != null && (int)tView1.SelectedNode.Tag == 0))
                {
                    if (await SaveConstituency())
                    {
                        //await LoadConstituencies();
                        await LoadInfo();
                    }
                }
                else
                {
                    await UpdateConstituency((int)tView1.SelectedNode.Tag);
                    //await LoadConstituencies();
                    await LoadInfo();
                }
                if (constituency != null && constituency.Id > 0)
                {
                    tView1.SelectedNode = CollectAllNodes(tView1.Nodes).FirstOrDefault(x => int.Parse(x.Tag.ToString()) == constituency.Id);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async Task<bool> SaveConstituency()
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PostAsync("Constituencies", BuildConstituency(), token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response.IsSuccess;
                }

                constituency = (ConstituencyC)response.Result;
                UtilRecurrent.InformationMessage("Constituency sucessfully saved", "Constituency Saved");
                return true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }

        }
        private async Task UpdateConstituency(int id)
        {
            try
            {
                var consty = BuildConstituency();
                consty.Id = id;
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PutAsync("Constituencies", consty, consty.Id, token);
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
        private ConstituencyC BuildConstituency()
        {
            return new ConstituencyC()
            {
                Active = rjActive.Checked,
                SGSE = txtSGSE.Text.ToUpper(),
                Name = txtName.Text.ToUpper()
            };
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
        #endregion

        #region Polling Divisions
        //TODO guardar el id del la constituency en el nodo tambien
        //diferenciar nosd de nivel 0 y de nivel 1
        //cuando eleccione un nodo de nivel 0 poner el valor en el combobos de las constituencies
        private void RefreshTreeViewPolling(List<ConstituencyC> constituencies)
        {
            try
            {
                tViewPolling.Nodes.Clear();
                List<TreeNode> treeNodes = new List<TreeNode>();
                List<TreeNode> childNodes = new List<TreeNode>();

                foreach (ConstituencyC consty in constituencies)
                {
                    foreach (PollingDivision polling in consty.PollingDivisions)
                    {
                        childNodes.Add(new TreeNode(polling.Name, 2, 1));
                        childNodes[childNodes.Count - 1].Tag = polling.Id;
                        addContextMenu(childNodes[childNodes.Count - 1], "Delete Polling");
                    }
                    treeNodes.Add(new TreeNode(consty.Name, 0, 0, childNodes.ToArray()));
                    treeNodes[treeNodes.Count - 1].Tag = 0;
                    childNodes = new List<TreeNode>();
                }

                tViewPolling.Nodes.AddRange(treeNodes.ToArray());
                tViewPolling.ExpandAll();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }

        private void tViewPolling_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AfterSelectNodeTVPolling(e.Node);
        }
        private void AfterSelectNodeTVPolling(TreeNode node)
        {
            try
            {
                int nodeTag = (int)node.Tag;
                if (nodeTag > 0)
                {
                    pollingDivision = ConstituenciesList.SelectMany(c => c.PollingDivisions).Where(p => p.Id == nodeTag).FirstOrDefault();


                    pollingDivision.Constituency = ConstituenciesList.Where(c => c.PollingDivisions.Contains(pollingDivision)).FirstOrDefault();
                    //cmbConstituency.SelectedValue = pollingDivision.Constituency.Id;
                    showPollingInfo(pollingDivision);
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
        private void showPollingInfo(PollingDivision polling)
        {
            try
            {
                txtPName.Text = polling.Name;
                rjPActive.Checked = polling.Active;
                cmbConstituency.SelectedValue = polling.Constituency.Id;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }

        private async void ibtnSaveType_Click(object sender, EventArgs e)
        {
            if (txtPName.TextLength == 0)
            {
                UtilRecurrent.ErrorMessage("Polling name is a required field.");
                return;
            }
            try
            {
                if (tViewPolling.SelectedNode == null || (tViewPolling.SelectedNode != null && (int)tViewPolling.SelectedNode.Tag == 0))
                {
                    if (await SavePolling())
                    {
                        //await LoadConstituencies();
                        await LoadInfo();
                    }
                }
                else
                {
                    await UpdatePolling((int)tViewPolling.SelectedNode.Tag);
                    //await LoadConstituencies();
                    await LoadInfo();
                }
                if (pollingDivision != null && pollingDivision.Id > 0)
                {
                    tViewPolling.SelectedNode = CollectAllNodes(tViewPolling.Nodes).FirstOrDefault(x => int.Parse(x.Tag.ToString()) == pollingDivision.Id);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }

        }
        private async Task<bool> SavePolling()
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PostAsync("PollingDivisions", BuildPolling(), token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response.IsSuccess;
                }

                pollingDivision = (PollingDivision)response.Result;
                UtilRecurrent.InformationMessage("Polling Division sucessfully saved", "Polling Division Saved");
                return true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }

        }
        private async Task UpdatePolling(int id)
        {
            try
            {
                var polling = BuildPolling();
                polling.Id = id;
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PutAsync("PollingDivisions", polling, polling.Id, token);
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
        private PollingDivision BuildPolling()
        {
            return new PollingDivision()
            {
                Active = rjPActive.Checked,
                Name = txtPName.Text.ToUpper(),
                Constituency = ConstituenciesList.FirstOrDefault(c => c.Id == (int)cmbConstituency.SelectedValue)
            };
        }
        #endregion

        #region Comments

        private ObservableCollection<Comment> CommentsList;
        Comment comment;

        private async Task LoadComments()
        {
            //UtilRecurrent.LockForm(waitForm, this);
            Response response = await ApiServices.GetListAsync<Comment>("Comments", token);
            //UtilRecurrent.UnlockForm(waitForm, this);
            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return;
            }
            CommentsList = new ObservableCollection<Comment>((List<Comment>)response.Result);
            if (CommentsList != null && CommentsList.Any())
            {
                RefreshTreeViewComments(CommentsList.ToList());                
                chkElectionParties.Items.Clear();
                chkElectionParties.Items.AddRange(PartiesList.Select(p => p.Name).ToArray());
            }
            else
            {
                cleanScreen();
                tViewComments.Nodes.Clear();
            }
        }
        private void RefreshTreeViewComments(List<Comment> comments)
        {
            try
            {
                tViewComments.Nodes.Clear();
                List<TreeNode> treeNodes = new List<TreeNode>();
                List<TreeNode> childNodes = new List<TreeNode>();
                foreach (Comment comment in comments)
                {
                    childNodes.Add(new TreeNode(comment.Text, 2, 1));
                    childNodes[childNodes.Count - 1].Tag = comment.Id;
                    addContextMenu(childNodes[childNodes.Count - 1], "Delete Comment");
                }
                treeNodes.Add(new TreeNode("Comments", 0, 0, childNodes.ToArray()));
                treeNodes[treeNodes.Count - 1].Tag = 0;
                childNodes = new List<TreeNode>();
                tViewComments.Nodes.AddRange(treeNodes.ToArray());
                tViewComments.ExpandAll();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void tvComments_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AfterSelectNodeTVComments(e.Node);
        }
        private void AfterSelectNodeTVComments(TreeNode node)
        {
            try
            {
                int nodeTag = (int)node.Tag;
                if (nodeTag > 0)
                {
                    comment = CommentsList.Where(p => p.Id == nodeTag).FirstOrDefault();
                    showCommentInfo(comment);
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
        private void showCommentInfo(Comment comment)
        {
            try
            {
                rjCommentActive.Checked = comment.Active;
                txtCommentText.Text = comment.Text;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }

        private async void ibtnSaveComment_Click(object sender, EventArgs e)
        {
            if (txtCommentText.TextLength == 0 )
            {
                UtilRecurrent.ErrorMessage("Comment text is a required field.");
                return;
            }
            try
            {
                if (tViewComments.SelectedNode == null || (tViewComments.SelectedNode != null && (int)tViewComments.SelectedNode.Tag == 0))
                {
                    if (await SaveComment())
                    {
                        //await LoadParties();
                        await LoadInfo();
                    }
                }
                else
                {
                    await UpdateComment((int)tViewComments.SelectedNode.Tag);
                    // await LoadParties();
                    await LoadInfo();
                }
                if (comment != null && comment.Id > 0)
                {
                    tViewComments.SelectedNode = CollectAllNodes(tViewComments.Nodes).FirstOrDefault(x => int.Parse(x.Tag.ToString()) == comment.Id);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async Task<bool> SaveComment()
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PostAsync("Comments", BuildComment(), token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response.IsSuccess;
                }

                comment = (Comment)response.Result;
                UtilRecurrent.InformationMessage("Comment sucessfully saved", "Comment Saved");
                return true;
            }
            catch (Exception ex)
            {
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }
        }
        private async Task UpdateComment(int id)
        {
            try
            {
                var comment = BuildComment();
                comment.Id = id;
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PutAsync("Comments", comment, comment.Id, token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); 
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private Comment BuildComment()
        {
            return new Comment()
            {
                Active = rjCommentActive.Checked,
                Text = txtCommentText.Text,
            };
        }
        #endregion

        #region Parties
        private ObservableCollection<Party> PartiesList;
        Party party;

        private async Task LoadParties()
        {
            //UtilRecurrent.LockForm(waitForm, this);
            Response response = await ApiServices.GetListAsync<Party>("Parties", token);
            //UtilRecurrent.UnlockForm(waitForm, this);
            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return;
            }
            PartiesList = new ObservableCollection<Party>((List<Party>)response.Result);
            if (PartiesList != null && PartiesList.Any())
            {
                RefreshTreeViewParties(PartiesList.ToList());
                //tView1.SelectedNode = tView1.Nodes[0]; 
                chkElectionParties.Items.Clear();
                chkElectionParties.Items.AddRange(PartiesList.Select(p => p.Name).ToArray());
            }
            else
            {
                cleanScreen();
                tvParties.Nodes.Clear();
            }
        }
        private void RefreshTreeViewParties(List<Party> parties)
        {
            try
            {
                tvParties.Nodes.Clear();
                List<TreeNode> treeNodes = new List<TreeNode>();
                List<TreeNode> childNodes = new List<TreeNode>();
                foreach (Party party in parties)
                {
                    childNodes.Add(new TreeNode(party.Name, 2, 1));
                    childNodes[childNodes.Count - 1].Tag = party.Id;
                    addContextMenu(childNodes[childNodes.Count - 1], "Delete Party");
                }
                treeNodes.Add(new TreeNode("Parties", 0, 0, childNodes.ToArray()));
                treeNodes[treeNodes.Count - 1].Tag = 0;
                childNodes = new List<TreeNode>();
                tvParties.Nodes.AddRange(treeNodes.ToArray());
                tvParties.ExpandAll();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void tvParties_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AfterSelectNodeTVParties(e.Node);
        }
        private void AfterSelectNodeTVParties(TreeNode node)
        {
            try
            {
                int nodeTag = (int)node.Tag;
                if (nodeTag > 0)
                {
                    party = PartiesList.Where(p => p.Id == nodeTag).FirstOrDefault();
                    showPartyInfo(party);
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
        private void showPartyInfo(Party party)
        {
            try
            {
                txtPpname.Text = party.Name;
                rjParty.Checked = party.Active;
                txtPCode.Text = party.Code.ToString();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }


        private async void ibtnSaveParty_Click(object sender, EventArgs e)
        {
            if (txtPpname.TextLength == 0 || txtPCode.TextLength == 0)
            {
                UtilRecurrent.ErrorMessage("Code and Name are required fields.");
                return;
            }
            try
            {
                if (tvParties.SelectedNode == null || (tvParties.SelectedNode != null && (int)tvParties.SelectedNode.Tag == 0))
                {
                    if (await SaveParty())
                    {
                        //await LoadParties();
                        await LoadInfo();
                    }
                }
                else
                {
                    await UpdateParty((int)tvParties.SelectedNode.Tag);
                    // await LoadParties();
                    await LoadInfo();
                }
                if (party != null && party.Id > 0)
                {
                    tvParties.SelectedNode = CollectAllNodes(tvParties.Nodes).FirstOrDefault(x => int.Parse(x.Tag.ToString()) == party.Id);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async Task<bool> SaveParty()
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PostAsync("Parties", BuildParty(), token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response.IsSuccess;
                }

                party = (Party)response.Result;
                UtilRecurrent.InformationMessage("Party sucessfully saved", "Party Saved");
                return true;
            }
            catch (Exception ex)
            {
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }

        }
        private async Task UpdateParty(int id)
        {
            try
            {
                var polling = BuildParty();
                polling.Id = id;
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PutAsync("Parties", polling, polling.Id, token);
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
        private Party BuildParty()
        {
            return new Party()
            {
                Active = rjParty.Checked,
                Name = txtPpname.Text.ToUpper(),
                Code = txtPCode.Text.ToUpper()
            };
        }

        #endregion

        #region Canvas Types
        private ObservableCollection<CanvasType> CanvasTypesList;
        CanvasType CanvasType;

        private async Task LoadCanvasTypes()
        {
            //UtilRecurrent.LockForm(waitForm, this);
            Response response = await ApiServices.GetListAsync<CanvasType>("CanvasTypes", token);
            // UtilRecurrent.UnlockForm(waitForm, this);

            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return;
            }
            CanvasTypesList = new ObservableCollection<CanvasType>((List<CanvasType>)response.Result);
            if (CanvasTypesList.Any())
            {
                RefreshTreeViewCanvasTypes(CanvasTypesList.ToList());
                //tView1.SelectedNode = tView1.Nodes[0];
                cmbCanvasTypes.DataSource = null;
                cmbCanvasTypes.DataSource = CanvasTypesList.Where(x => x.Active).ToList();
                cmbCanvasTypes.ValueMember = "Id";
                cmbCanvasTypes.DisplayMember = "Type";
                cmbCanvasTypes.SelectedItem = null;
            }
            else
            {
                cleanScreen();
                TVCanvasType.Nodes.Clear();
            }
        }
        private void RefreshTreeViewCanvasTypes(List<CanvasType> canvasTypes)
        {
            try
            {
                TVCanvasType.Nodes.Clear();
                List<TreeNode> treeNodes = new List<TreeNode>();
                List<TreeNode> childNodes = new List<TreeNode>();


                foreach (CanvasType canvasType in canvasTypes)
                {
                   
                    childNodes.Add(new TreeNode(canvasType.Type, 2, 1));
                    childNodes[childNodes.Count - 1].Tag = canvasType.Id;
                    addContextMenu(childNodes[childNodes.Count - 1], "Delete Canvas Type");
                }
                treeNodes.Add(new TreeNode("Canvas Types", 0, 0, childNodes.ToArray()));
                treeNodes[treeNodes.Count - 1].Tag = 0;
                childNodes = new List<TreeNode>();

                TVCanvasType.Nodes.AddRange(treeNodes.ToArray());
                TVCanvasType.ExpandAll();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void TVCanvasType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AfterSelectNodeTVCanvasType((int)e.Node.Tag);
        }
        private void AfterSelectNodeTVCanvasType(int nodeTag)
        {
            try
            {
                if (nodeTag > 0 && CanvasTypesList != null && CanvasTypesList.Where(u => u.Id == nodeTag).Any())
                {
                    CanvasType = CanvasTypesList.Where(u => u.Id == nodeTag).FirstOrDefault();
                    showCanvasTypeInfo(CanvasType);
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
        private void showCanvasTypeInfo(CanvasType canvasT)
        {
            try
            {
                txtCTName.Text = canvasT.Type;
                txtCTDescription.Text = canvasT.Description;
                rjCTActive.Checked = canvasT.Active;
                txtCTCanvas.Clear();
                if (canvasT.Canvas != null && canvasT.Canvas.Any())
                {
                    foreach (var item in canvasT.Canvas)
                    {
                        var dateFrom = new DateTime();
                        var dateTo = new DateTime();
                        if (item.Interviews != null && item.Interviews.Any())
                        {
                            //select Interviews minimal date and maximal date
                            dateFrom = item.Interviews.Min(x => x.Date);
                            dateTo = item.Interviews.Max(x => x.Date);
                            txtCTCanvas.Text += item.Name.ToString() + " (" + dateFrom.ToString("dd-MMM-yyyy") + " - " + dateTo.ToString("dd-MMM-yyyy") + ")" + "\r\n";
                        }
                        else
                        {
                            txtCTCanvas.Text += item.Name.ToString() + "\r\n";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }

        private async void ibtnSaveCanvasType_Click(object sender, EventArgs e)
        {
            if (PaintRequiredFildsCanvasType())
            {
                UtilRecurrent.ErrorMessage("Requireds fields missing. Find them highlighted in red.");
                return;
            }
            try
            {
                if (TVCanvasType.SelectedNode == null || (TVCanvasType.SelectedNode != null && (int)TVCanvasType.SelectedNode.Tag == 0))
                {
                    if (await SaveCanvasType())
                    {
                        //await LoadCanvasTypes();
                        await LoadInfo();
                    }
                }
                else
                {
                    await UpdateCanvasType((int)TVCanvasType.SelectedNode.Tag);
                    //await LoadCanvasTypes();
                    await LoadInfo();
                }
                if (CanvasType != null && CanvasType.Id > 0)
                {
                    TVCanvasType.SelectedNode = CollectAllNodes(TVCanvasType.Nodes).FirstOrDefault(x => int.Parse(x.Tag.ToString()) == CanvasType.Id);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async Task<bool> SaveCanvasType()
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PostAsync("CanvasTypes", BuildCanvasType(), token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response.IsSuccess;
                }
                CanvasType = (CanvasType)response.Result;
                UtilRecurrent.InformationMessage("Canvas Type sucessfully saved", "Canvas Type Saved");
                return true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }

        }
        private async Task UpdateCanvasType(int id)
        {
            try
            {
                var canvasT = BuildCanvasType();
                canvasT.Id = id;
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PutAsync("CanvasTypes", canvasT, canvasT.Id, token);
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
        private CanvasType BuildCanvasType()
        {
            return new CanvasType()
            {
                Active = rjCTActive.Checked,
                Type = txtCTName.Text.ToUpper(),
                Description = txtCTDescription.Text.ToUpper()
            };
        }
        private async Task DeleteCanvasType(int id)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.DeleteAsync("CanvasTypes", id, token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        #endregion

        #region Interviewer

        private ObservableCollection<Interviewer> InterviewerList;
        Interviewer Interviewer;

        private async Task LoadInterviewers()
        {
            // UtilRecurrent.LockForm(waitForm, this);
            Response response = await ApiServices.GetListAsync<Interviewer>("Interviewers", token);
            //UtilRecurrent.UnlockForm(waitForm, this);

            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return;
            }
            InterviewerList = new ObservableCollection<Interviewer>((List<Interviewer>)response.Result);
            if (InterviewerList.Any())
            {
                RefreshTreeViewInterviewers(InterviewerList.ToList());
                //tView1.SelectedNode = tView1.Nodes[0];
            }
            else
            {
                cleanScreen();
                TVInterviewers.Nodes.Clear();
            }
        }
        private void RefreshTreeViewInterviewers(List<Interviewer> interviewers)
        {
            try
            {
                TVInterviewers.Nodes.Clear();
                List<TreeNode> treeNodes = new List<TreeNode>();
                List<TreeNode> childNodes = new List<TreeNode>();


                foreach (Interviewer interviewer in interviewers)
                {
                    childNodes.Add(new TreeNode(interviewer.FullName, 2, 1));
                    childNodes[childNodes.Count - 1].Tag = interviewer.Id;
                    addContextMenu(childNodes[childNodes.Count - 1], "Delete Interviewer");
                }
                treeNodes.Add(new TreeNode("Interviewers", 0, 0, childNodes.ToArray()));
                treeNodes[treeNodes.Count - 1].Tag = 0;
                childNodes = new List<TreeNode>();

                TVInterviewers.Nodes.AddRange(treeNodes.ToArray());
                TVInterviewers.ExpandAll();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void TVInterviewers_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AfterSelectNodeTVInterviewers((int)e.Node.Tag);
        }
        private void AfterSelectNodeTVInterviewers(int nodeTag)
        {
            try
            {
                if (nodeTag > 0 && InterviewerList is not null && InterviewerList.Where(u => u.Id == nodeTag).Any())
                {
                    Interviewer = InterviewerList.Where(u => u.Id == nodeTag).FirstOrDefault();
                    showInterviewerInfo(Interviewer);
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
        private void showInterviewerInfo(Interviewer interviewer)
        {
            try
            {
                if (interviewer.Id > 0)
                {
                    List<PropertyInfo> properties = interviewer.GetType().GetProperties().ToList();
                    List<TextBox> interviewerTextBox = UtilRecurrent.FindAllTextBoxIterative(tableLayoutPanel12);
                    foreach (TextBox txt in interviewerTextBox)
                    {
                        if (properties.Where(p => p.Name == txt.Name.Replace("txt", string.Empty)).Any())
                        {

                            txt.Text = properties.Where(p => p.Name == txt.Name.Replace("txt", string.Empty)).First().GetValue(Interviewer).ToString();
                        }
                    }
                    rjInterviewer.Checked = interviewer.Active;
                    cmbSex.SelectedItem = interviewer.Sex;
                    dtpDOB.Value = (DateTime)interviewer.DOB;

                    dgvInterviewerInterviews.DataSource = null;
                    if (interviewer.Interviews != null && interviewer.Interviews.Any())
                    {
                        dgvInterviewerInterviews.DataSource = interviewer.Interviews.Select(u => new { Registration = u.Voter.Reg, u.Voter.FullName, PD = u.Voter.PollingDivision.Name, Party = u.SupportedParty != null ? u.SupportedParty.Name : "", u.Date }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async void iconButton1_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (PaintRequiredFildsInterviewer())
                {
                    UtilRecurrent.ErrorMessage("Requireds fields missing. Find them highlighted in red.");
                    return;
                }
                if (dtpDOB.Value.AddYears(16) > DateTime.Now)
                {
                    UtilRecurrent.ErrorMessage("Applicant younger than 16.");
                    return;
                }
                if (txtEmail.TextLength > 0 && !UtilRecurrent.IsValidEmail(txtEmail.Text.Trim()))
                {
                    UtilRecurrent.ErrorMessage("You must provide a valid Email address.");
                    return;
                }
                if (TVInterviewers.SelectedNode == null || (TVInterviewers.SelectedNode != null && (int)TVInterviewers.SelectedNode.Tag == 0))
                {
                    if (await SaveInterviewers())
                    {
                        //await LoadInterviewers();
                        await LoadInfo();
                    }
                }
                else
                {
                    await UpdateInterviewers((int)TVInterviewers.SelectedNode.Tag);
                    // await LoadInterviewers();
                    await LoadInfo();
                }
                if (Interviewer != null && Interviewer.Id > 0)
                {
                    TVInterviewers.SelectedNode = CollectAllNodes(TVInterviewers.Nodes).FirstOrDefault(x => int.Parse(x.Tag.ToString()) == Interviewer.Id);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }

        private async Task<bool> SaveInterviewers()
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PostAsync("Interviewers", BuildInterviewers(), token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response.IsSuccess;
                }
                Interviewer = (Interviewer)response.Result;
                UtilRecurrent.InformationMessage("Interviewer sucessfully saved", "Interviewer Saved");
                return true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }

        }
        private async Task UpdateInterviewers(int id)
        {
            try
            {
                var interv = BuildInterviewers();
                interv.Id = id;
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PutAsync("Interviewers", interv, interv.Id, token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private Interviewer BuildInterviewers()
        {
            try
            {
                Interviewer = new Interviewer();
                List<PropertyInfo> properties = Interviewer.GetType().GetProperties().ToList();
                List<TextBox> intervTextBox = UtilRecurrent.FindAllTextBoxIterative(tableLayoutPanel12);
                foreach (PropertyInfo prop in properties)
                {
                    if (intervTextBox.Where(p => p.Name.Replace("txt", string.Empty) == prop.Name).Any())
                    {
                        prop.SetValue(Interviewer, intervTextBox.Where(p => p.Name.Replace("txt", string.Empty) == prop.Name).FirstOrDefault().Text.TrimEnd().ToUpper());
                    }
                }
                Interviewer.Active = rjInterviewer.Checked;
                Interviewer.Sex = cmbSex.SelectedItem.ToString();
                Interviewer.DOB = dtpDOB.Value;
                return Interviewer;
            }

            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return null;
            }
        }

        #endregion


        #region Canvas 
        private ObservableCollection<Canvas> CanvasList;
        Canvas Canvas;

        private async Task LoadCanvas()
        {
            //UtilRecurrent.LockForm(waitForm, this);
            Response response = await ApiServices.GetListAsync<Canvas>("Canvas", token);
            //UtilRecurrent.UnlockForm(waitForm, this);

            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return;
            }
            CanvasList = new ObservableCollection<Canvas>((List<Canvas>)response.Result);
            if (CanvasList.Any())
            {
                RefreshTreeViewCanvas(CanvasList.ToList());
                //tView1.SelectedNode = tView1.Nodes[0];
            }
            else
            {
                cleanScreen();
                TVCanvas.Nodes.Clear();
            }
        }
        private void RefreshTreeViewCanvas(List<Canvas> canvas)
        {
            try
            {
                TVCanvas.Nodes.Clear();
                List<TreeNode> treeNodes = new List<TreeNode>();
                List<TreeNode> childNodes = new List<TreeNode>();


                foreach (Canvas canva in canvas)
                {
                    childNodes.Add(new TreeNode(canva.Name, 2, 1));
                    childNodes[childNodes.Count - 1].Tag = canva.Id;
                    addContextMenu(childNodes[childNodes.Count - 1], "Delete Canvas");
                }
                treeNodes.Add(new TreeNode("Canvas", 0, 0, childNodes.ToArray()));
                treeNodes[treeNodes.Count - 1].Tag = 0;
                childNodes = new List<TreeNode>();

                TVCanvas.Nodes.AddRange(treeNodes.ToArray());
                TVCanvas.ExpandAll();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void TVCanvas_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AfterSelectNodeTVCanvas((int)e.Node.Tag);
        }
        private void AfterSelectNodeTVCanvas(int nodeTag)
        {
            try
            {
                if (nodeTag > 0)
                {
                    Canvas = CanvasList.Where(u => u.Id == nodeTag).FirstOrDefault();
                    showCanvasInfo(Canvas);
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
        private void showCanvasInfo(Canvas canvas)
        {
            try
            {

                txtCanvasName.Text = canvas.Name;
                txtCanvasDescription.Text = canvas.Description;
                rjCanvasActive.Checked = canvas.Active;
                rjCanvasOpen.Checked = canvas.Open;
                cmbCanvasTypes.SelectedValue = canvas.Type.Id;
                dgvCanvasInterviews.DataSource = null;
                if (canvas.Interviews != null && canvas.Interviews.Any())
                {
                    dgvCanvasInterviews.DataSource = canvas.Interviews.Select(u => new { Registration = u.Voter.Reg, u.Voter.FullName, PD = u.Voter.PollingDivision.Name, Party = u.SupportedParty!=null? u.SupportedParty.Name:"", u.Date, Interviewer = u.Interviewer.FullName }).ToList();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async void ibtnCanvasSave_Click(object sender, EventArgs e)
        {
            if (PaintRequiredFildsCanvas())
            {
                UtilRecurrent.ErrorMessage("Requireds fields missing. Find them highlighted in red.");
                return;
            }
            try
            {
                if (TVCanvas.SelectedNode == null || (TVCanvas.SelectedNode != null && (int)TVCanvas.SelectedNode.Tag == 0))
                {
                    if (await SaveCanvas())
                    {
                        // await LoadCanvas();
                        await LoadInfo();
                    }
                }
                else
                {
                    await UpdateCanvas((int)TVCanvas.SelectedNode.Tag);
                    // await LoadCanvas();
                    await LoadInfo();
                }
                if (Canvas != null && Canvas.Id > 0)
                {
                    TVCanvas.SelectedNode = CollectAllNodes(TVCanvas.Nodes).FirstOrDefault(x => int.Parse(x.Tag.ToString()) == Canvas.Id);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async Task<bool> SaveCanvas()
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PostAsync("Canvas", BuildCanvas(), token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response.IsSuccess;
                }
                Canvas = (Canvas)response.Result;
                UtilRecurrent.InformationMessage("Canvas sucessfully saved", "Canvas Saved");
                return true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }

        }
        private async Task UpdateCanvas(int id)
        {
            try
            {
                var canvas = BuildCanvas();
                canvas.Id = id;
                canvas.Type.Canvas = null;
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PutAsync("Canvas", canvas, canvas.Id, token);
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
        private Canvas BuildCanvas()
        {
            var canva= new Canvas()
            {
                Active = rjCanvasActive.Checked,
                Open = rjCanvasOpen.Checked,
                Name = txtCanvasName.Text,
                Type = CanvasTypesList.Where(u => u.Id == (int)cmbCanvasTypes.SelectedValue).FirstOrDefault(),                Description = txtCanvasDescription.Text.ToUpper()
            };
            canva.Type.Canvas = null;
            return canva;
        }
        private async Task DeleteCanvas(int id)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.DeleteAsync("Canvas", id, token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        #endregion

        #region National Elections

        private ObservableCollection<NationalElection> NationalElectionsList;
        NationalElection NationalElection;

        private async Task LoadNationalElections()
        {
            //UtilRecurrent.LockForm(waitForm, this);
            Response response = await ApiServices.GetListAsync<NationalElection>("NationalElections", token);
            //UtilRecurrent.UnlockForm(waitForm, this);

            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return;
            }
            NationalElectionsList = new ObservableCollection<NationalElection>((List<NationalElection>)response.Result);
            if (NationalElectionsList.Any())
            {
                RefreshTreeViewNationalElection(NationalElectionsList.ToList());
                //tView1.SelectedNode = tView1.Nodes[0];
            }
            else
            {
                cleanScreen();
                TVElections.Nodes.Clear();
            }
        }
        private void RefreshTreeViewNationalElection(List<NationalElection> elections)
        {
            try
            {
                TVElections.Nodes.Clear();
                List<TreeNode> treeNodes = new List<TreeNode>();
                List<TreeNode> childNodes = new List<TreeNode>();


                foreach (NationalElection election in elections)
                {
                    childNodes.Add(new TreeNode(election.ElectionDate.ToString("MMMM-yyyy"), 2, 1));
                    childNodes[childNodes.Count - 1].Tag = election.Id;
                    addContextMenu(childNodes[childNodes.Count - 1], "Delete Election");
                }
                treeNodes.Add(new TreeNode("National Elections", 0, 0, childNodes.ToArray()));
                treeNodes[treeNodes.Count - 1].Tag = 0;
                childNodes = new List<TreeNode>();

                TVElections.Nodes.AddRange(treeNodes.ToArray());
                TVElections.ExpandAll();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void TVElections_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AfterSelectNodeTVElections((int)e.Node.Tag);
        }
        private void AfterSelectNodeTVElections(int nodeTag)
        {
            try
            {
                if (nodeTag > 0)
                {
                    NationalElection = NationalElectionsList.Where(u => u.Id == nodeTag).FirstOrDefault();
                    showNationalElectionsInfo(NationalElection);
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
        private void showNationalElectionsInfo(NationalElection election)
        {
            try
            {
                txtElectionDescription.Text = election.Description;
                dtpElectionDate.Value = election.ElectionDate;
                rjElectionActive.Checked = election.Open;
                foreach (int i in chkElectionParties.CheckedIndices)
                {
                    chkElectionParties.SetItemCheckState(i, CheckState.Unchecked);
                }
                if (election.Parties != null && election.Parties.Count > 0)
                {
                    foreach (Party party in election.Parties)
                    {
                        chkElectionParties.SetItemChecked(chkElectionParties.Items.IndexOf(party.Name), true);
                    }
                }

                if (election.ElectionVotes != null && election.ElectionVotes.Any())
                {
                    DgvElectionVotes.DataSource = election.ElectionVotes;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async void ibtnNationalElectionsSave_Click(object sender, EventArgs e)
        {
            if (chkElectionParties.SelectedItems == null || chkElectionParties.CheckedItems.Count < 1)
            {
                UtilRecurrent.ErrorMessage("Please select at least one party");
                return;
            }
            try
            {
                if (TVElections.SelectedNode == null || (TVElections.SelectedNode != null && (int)TVElections.SelectedNode.Tag == 0))
                {
                    if (await SaveNationalElections())
                    {
                        // await LoadNationalElections();
                        await LoadInfo();
                    }
                }
                else
                {
                    await UpdateNationalElections((int)TVElections.SelectedNode.Tag);
                    //await LoadNationalElections();
                    await LoadInfo();
                }
                if (NationalElection != null && NationalElection.Id > 0)
                {
                    TVElections.SelectedNode = CollectAllNodes(TVElections.Nodes).FirstOrDefault(x => int.Parse(x.Tag.ToString()) == NationalElection.Id);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async Task<bool> SaveNationalElections()
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PostAsync("NationalElections", BuildNationalElections(), token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response.IsSuccess;
                }
                NationalElection = (NationalElection)response.Result;
                UtilRecurrent.InformationMessage("National Election sucessfully saved", "National Election Saved");
                return true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }

        }
        private async Task UpdateNationalElections(int id)
        {
            try
            {
                var nationalElection = BuildNationalElections();
                nationalElection.Id = id;

                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PutAsync("NationalElections", nationalElection, nationalElection.Id, token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private NationalElection BuildNationalElections()
        {
            return new NationalElection()
            {
                Open = rjElectionActive.Checked,
                ElectionDate = dtpElectionDate.Value,
                Description = txtElectionDescription.Text,
                Parties = PartiesList.Where(x => chkElectionParties.CheckedItems.Contains(x.Name)).ToList()
            };
        }
        private async Task DeleteNationalElections(int id)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.DeleteAsync("NationalElections", id, token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        #endregion

        #region Others
        public void addContextMenu(TreeNode tvw, string item)
        {
            ContextMenuStrip _contextmenu = new ContextMenuStrip();
            _contextmenu.Items.Add(item);
            _contextmenu.ItemClicked += contextmenu_click;
            tvw.ContextMenuStrip = _contextmenu;
        }
        private async void contextmenu_click(object sender, ToolStripItemClickedEventArgs e)
        {

            switch (e.ClickedItem.Text)
            {
                case "Delete Constituency":
                    {
                        if (constituency.PollingDivisions.Count == 0)
                        {
                            await DeleteAsyncGeneric("Constituencies", constituency.Id);
                            // await LoadConstituencies();
                            await LoadInfo();
                            if (tView1.Nodes.Count > 0)
                            {
                                tView1.SelectedNode = tView1.Nodes[0];
                            }
                            UtilRecurrent.UnlockForm(waitForm, this);
                        }
                        else
                        {
                            UtilRecurrent.ErrorMessage("The selected constituency can not be deleted.Polling divisions associated to the constituency in the database.");
                        }

                        break;
                    }

                case "Delete Polling":
                    {
                        if (await LoadVoterAsyncByDivision(pollingDivision.Id) == null)
                        {
                            await DeleteAsyncGeneric("PollingDivisions", pollingDivision.Id);
                            // await LoadConstituencies();
                            await LoadInfo();
                            if (tViewPolling.Nodes.Count > 0)
                            {
                                tViewPolling.SelectedNode = tViewPolling.Nodes[0];
                            }
                            UtilRecurrent.UnlockForm(waitForm, this);
                        }
                        else
                        {
                            UtilRecurrent.ErrorMessage("The selected Polling Division can not be deleted. Voters associated to the Polling Division in the database.");
                        }
                        break;
                    }
                case "Delete Party":
                    {
                        if (await LoadAsyncByParty("NationalElections/FindByParty", party.Id) == null && await LoadAsyncByParty("Interviews/FindByParty", party.Id) == null && await LoadAsyncByParty("ElectionVotes/FindByParty", party.Id) == null)
                        {
                            await DeleteAsyncGeneric("Parties", party.Id);
                            //await LoadParties();
                            await LoadInfo();
                            if (tvParties.Nodes.Count > 0)
                            {
                                tvParties.SelectedNode = tvParties.Nodes[0];
                            }
                            UtilRecurrent.UnlockForm(waitForm, this);
                        }
                        else
                        {
                            UtilRecurrent.ErrorMessage("The selected Party can not be deleted. Interviews, Elections, or Votes associated to the Party in the database.");
                        }
                        break;
                    }
                case "Delete Canvas Type":
                    {
                        if (CanvasType.Canvas.Count == 0)
                        {
                            await DeleteAsyncGeneric("CanvasTypes", CanvasType.Id);
                            // await LoadCanvasTypes();
                            await LoadInfo();
                            if (TVCanvasType.Nodes.Count > 0)
                            {
                                TVCanvasType.SelectedNode = TVCanvasType.Nodes[0];
                            }
                            UtilRecurrent.UnlockForm(waitForm, this);
                        }
                        else
                        {
                            UtilRecurrent.ErrorMessage("The selected Canvas Type can not be deleted. Canvas associated to the Canvas Type in the database.");
                        }
                        break;
                    }
                case "Delete Canvas":
                    {
                        if (Canvas.Interviews.Count == 0)
                        {
                            await DeleteAsyncGeneric("Canvas", Canvas.Id);
                            // await LoadCanvas();
                            await LoadInfo();
                            if (TVCanvas.Nodes.Count > 0)
                            {
                                TVCanvas.SelectedNode = TVCanvas.Nodes[0];
                            }
                            UtilRecurrent.UnlockForm(waitForm, this);
                        }
                        else
                        {
                            UtilRecurrent.ErrorMessage("The selected Canvas can not be deleted. Voters associated to the Canvas in the database.");
                        }
                        break;
                    }
                case "Delete Interviewer":
                    {
                        if (Interviewer.Interviews.Count == 0)
                        {
                            await DeleteAsyncGeneric("Interviewers", Interviewer.Id);
                            // await LoadCanvas();
                            await LoadInfo();
                            if (TVInterviewers.Nodes.Count > 0)
                            {
                                TVInterviewers.SelectedNode = TVInterviewers.Nodes[0];
                            }
                            UtilRecurrent.UnlockForm(waitForm, this);
                        }
                        else
                        {
                            UtilRecurrent.ErrorMessage("The selected Interviewer can not be deleted. Interviews associated to the Interviewer in the database.");
                        }
                        break;
                    }
                case "Delete Election":
                    {
                        if (await LoadElectionAsyncWithVotes("NationalElections/IncludingVotes", NationalElection.Id) == null)
                        {
                            await DeleteAsyncGeneric("NationalElections", NationalElection.Id);
                            // await LoadConstituencies();
                            await LoadInfo();
                            if (TVElections.Nodes.Count > 0)
                            {
                                TVElections.SelectedNode = TVElections.Nodes[0];
                            }
                            UtilRecurrent.UnlockForm(waitForm, this);
                        }
                        else
                        {
                            UtilRecurrent.ErrorMessage("The selected Election can not be deleted. Votes associated to the Election in the database.");
                        }
                        break;
                    }
                case "Delete Comment":
                    {
                        if (await LoadInterviewAsyncByComment("Interviews/FindByComment", comment.Id) == null)
                        {
                            await DeleteAsyncGeneric("Comments", comment.Id);
                            // await LoadConstituencies();
                            await LoadInfo();
                            if (tViewComments.Nodes.Count > 0)
                            {
                                tViewComments.SelectedNode = tViewComments.Nodes[0];
                            }
                            UtilRecurrent.UnlockForm(waitForm, this);
                        }
                        else
                        {
                            UtilRecurrent.ErrorMessage("The selected Comment can not be deleted. Interviews associated to the Comment in the database.");
                        }
                        break;
                    }
            }
        }
        #region FindRelations before delete
        private async Task<Voter> LoadVoterAsyncByDivision(int id)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.FindAsync<Voter>("Voters/FindByDivision", id.ToString(), token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
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
        private async Task<Party> LoadAsyncByParty(string controller, int id)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.FindAsync<Party>(controller, id.ToString(), token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    return null;
                }
                return (Party)response.Result;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return null;
            }
        }
        private async Task<Interview> LoadInterviewAsyncByComment(string controller, int id)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.FindAsync<Interview>(controller, id.ToString(), token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    return null;
                }
                return (Interview)response.Result;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return null;
            }
        }
        private async Task<NationalElection> LoadElectionAsyncWithVotes(string controller, int id)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.FindAsync<NationalElection>(controller, id.ToString(), token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    return null;
                }
                return (NationalElection)response.Result;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return null;
            }
        }

        #endregion

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
        private void tView1_MouseClick(object sender, MouseEventArgs e)
        {
            //// Make sure this is the right button.
            //if (e.Button != MouseButtons.Left) return;

            // Select this node.
            TreeNode node_here = ((TreeView)sender).GetNodeAt(e.X, e.Y);
            ((TreeView)sender).SelectedNode = node_here;

            // See if we got a node.
            if (node_here == null) return;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "xlsx";
            ofd.FileName = "Upload File";
            ofd.Filter = "Excel  files|*.xls;*.xlsx";
            ofd.Title = "Select file";
            //  Allow the user to select multiple images.

            if (ofd.ShowDialog() != DialogResult.Cancel)
            {
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
            }
        }

        private async void tableLayoutPanel30_Paint(object sender, PaintEventArgs e)
        {

        }
        private async void iconButton1_Click(object sender, EventArgs e)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                List<ConstituencyC> constituencies = new List<ConstituencyC>();
                var listConst = new List<string>();
                var listPD = new List<string>();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    if (row.Cells[0].Value != null)
                    {
                        var constituency = row.Cells[0].Value.ToString().ToUpper();
                        if (!listConst.Contains(constituency))
                        {
                            listConst.Add(constituency);
                        }

                    }
                }
                foreach (var consty in listConst)
                {
                    if (!ConstituenciesList.Select(c => c.Name.ToUpper()).Contains(consty.ToUpper()))
                    {
                        await SaveConstituencyBatch(consty, consty);
                        constituencies.Add(constituency);
                    }
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    if (row.Cells[1].Value != null)
                    {
                        var pd = row.Cells[1].Value.ToString().ToUpper();
                        if (!listPD.Contains(pd))
                        {
                            listPD.Add(pd);
                            await SavePollingBatch(constituencies.FirstOrDefault(c => c.Name.ToUpper() == row.Cells[0].Value.ToString().ToUpper()), pd);

                        }

                    }
                }
                UtilRecurrent.UnlockForm(waitForm, this);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async Task<bool> SaveConstituencyBatch(string sgse, string name)
        {
            try
            {
                // UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PostAsync("Constituencies", BuildConstituencyB(sgse, name), token);
                // UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response.IsSuccess;
                }

                constituency = (ConstituencyC)response.Result;
                // UtilRecurrent.InformationMessage("Constituency sucessfully saved", "Constituency Saved");
                return true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }

        }
        private ConstituencyC BuildConstituencyB(string sgse, string name)
        {
            return new ConstituencyC()
            {
                Active = true,
                SGSE = sgse.ToUpper(),
                Name = name.ToUpper()
            };
        }

        private async Task<bool> SavePollingBatch(ConstituencyC constituency, string name)
        {
            try
            {
                // UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PostAsync("PollingDivisions", BuildPollingB(constituency, name), token);
                // UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response.IsSuccess;
                }

                pollingDivision = (PollingDivision)response.Result;
                // UtilRecurrent.InformationMessage("Polling Division sucessfully saved", "Polling Division Saved");
                return true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }

        }
        private PollingDivision BuildPollingB(ConstituencyC constituency, string name)
        {
            return new PollingDivision()
            {
                Active = true,
                Name = name,
                Constituency = constituency
            };
        }


    }

    #endregion


}
