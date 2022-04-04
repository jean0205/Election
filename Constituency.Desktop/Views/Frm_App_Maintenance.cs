
using Constituency.Desktop.Components;
using Constituency.Desktop.Controls;
using Constituency.Desktop.Entities;
using Constituency.Desktop.Helpers;
using Constituency.Desktop.Models;
using Microsoft.AppCenter.Crashes;
using System.Collections.ObjectModel;
using System.Data;

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
            MandatoriesFilds();
            await LoadConstituencies();
            await LoadParties();
            await LoadCanvasTypes();    


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

        private void cleanScreen()
        {
            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel2, "TextBox").Cast<TextBox>().ToList().ForEach(x => x.Clear());

            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel2, "RJToggleButton").Cast<RJToggleButton>().ToList().ForEach(x => x.Checked = true);
            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel4, "TextBox").Cast<TextBox>().ToList().ForEach(x => x.Clear());

            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel4, "RJToggleButton").Cast<RJToggleButton>().ToList().ForEach(x => x.Checked = true);
            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel6, "TextBox").Cast<TextBox>().ToList().ForEach(x => x.Clear());

            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel6, "RJToggleButton").Cast<RJToggleButton>().ToList().ForEach(x => x.Checked = true);

            //CanvasTypes
            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel8, "TextBox").Cast<TextBox>().ToList().ForEach(x => x.Clear());
            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel8, "RJToggleButton").Cast<RJToggleButton>().ToList().ForEach(x => x.Checked = true);
            cmbConstituency.SelectedItem = null;

        }
        #endregion

        #region Constituencies
        private ObservableCollection<ConstituencyC> ConstituenciesList;
        ConstituencyC constituency;
        PollingDivision pollingDivision;


        private async Task LoadConstituencies()
        {
            UtilRecurrent.LockForm(waitForm, this);
            Response response = await ApiServices.GetListAsync<ConstituencyC>("Constituencies", token);
            UtilRecurrent.UnlockForm(waitForm, this);

           
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
                cmbConstituency.DataSource = ConstituenciesList;
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
                pd.ForEach(p => txtPollings.Text +=  p.ToString()+ "\r\n");


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
                        await LoadConstituencies();
                    }
                }
                else
                {
                    await UpdateConstituency((int)tView1.SelectedNode.Tag);
                    await LoadConstituencies();
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
        private async Task DeleteConstituency(int id)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.DeleteAsync("Constituencies", id, token);
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
        #endregion
        
        #region Polling Divisions

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
                        await LoadConstituencies();
                    }
                }
                else
                {
                    await UpdatePolling((int)tViewPolling.SelectedNode.Tag);
                    await LoadConstituencies();
                }
                if (pollingDivision!=null && pollingDivision.Id > 0)
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

        #region Parties
        private ObservableCollection<Party> PartiesList;
        Party party;

        private async Task LoadParties()
        {
            UtilRecurrent.LockForm(waitForm, this);
            Response response = await ApiServices.GetListAsync<Party>("Parties", token);
            UtilRecurrent.UnlockForm(waitForm, this);           
            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return;
            }
            PartiesList = new ObservableCollection<Party>((List<Party>)response.Result);
            if (PartiesList.Any())
            {
                RefreshTreeViewParties(PartiesList.ToList());                
                //tView1.SelectedNode = tView1.Nodes[0];
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
                        await LoadParties();
                    }
                }
                else
                {
                    await UpdateParty((int)tvParties.SelectedNode.Tag);
                    await LoadParties();
                }
                if (party!= null && party.Id > 0)
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
                Code = int.Parse(txtPCode.Text.ToUpper())
            };
        }

        #endregion

        #region Canvas Types
        private ObservableCollection<CanvasType> CanvasTypesList;
        CanvasType CanvasType;

        private async Task LoadCanvasTypes()
        {
            UtilRecurrent.LockForm(waitForm, this);
            Response response = await ApiServices.GetListAsync<CanvasType>("CanvasTypes", token);
            UtilRecurrent.UnlockForm(waitForm, this);

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
                if (nodeTag > 0)
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
                if (canvasT.Canvas!= null && canvasT.Canvas.Any())
                {
                    foreach (var item in canvasT.Canvas)
                    {
                        txtCTCanvas.Text += item.ToString() + "\r\n";
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
                        await LoadCanvasTypes();
                    }
                }
                else
                {
                    await UpdateCanvasType((int)tView1.SelectedNode.Tag);
                    await LoadCanvasTypes();
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
                            await DeleteConstituency(constituency.Id);
                            await LoadConstituencies();
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

        
    }

    #endregion


}
