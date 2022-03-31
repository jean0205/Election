
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
            MandatoriesFildsSectorsTypes();
            await LoadConstituencies();


        }
        #region Mandatories and clear screen
        private void MandatoriesFildsSectorsTypes()
        {//tag= 1 para campos obligados

            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel2, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1")).ToList().ForEach(x => toolTip1.SetToolTip(x, "Mandatory Field"));
        }
        private bool PaintRequiredFilds()
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
        private void cleanScreen()
        {
            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel2, "TextBox").Cast<TextBox>().ToList().ForEach(x => x.Clear());

            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel2, "RJToggleButton").Cast<RJToggleButton>().ToList().ForEach(x => x.Checked = true);

        }
        #endregion

        #region Constituencies
        private ObservableCollection<ConstituencyC> ConstituenciesList;
        ConstituencyC constituency;


        private async Task LoadConstituencies()
        {
            UtilRecurrent.LockForm(waitForm, this);
            Response response = await ApiServices.GetListAsync<ConstituencyC>("Constituencies", token);
            UtilRecurrent.UnlockForm(waitForm, this);

            waitForm.Close();
            Cursor.Show();
            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return;
            }
            ConstituenciesList = new ObservableCollection<ConstituencyC>((List<ConstituencyC>)response.Result);
            if (ConstituenciesList.Any())
            {
                RefreshTreeView(ConstituenciesList.ToList());
                //tView1.SelectedNode = tView1.Nodes[0];
            }
            else
            {
                cleanScreen();
                tView1.Nodes.Clear();
            }
        }
        private void RefreshTreeView(List<ConstituencyC> constituencies)
        {
            try
            {
                tView1.Nodes.Clear();
                List<TreeNode> treeNodes = new List<TreeNode>();
                List<TreeNode> childNodes = new List<TreeNode>();


                foreach (ConstituencyC consttuency in constituencies)
                {
                    childNodes.Add(new TreeNode(consttuency.Name, 0, 1));
                    childNodes[childNodes.Count - 1].Tag = consttuency.Id;
                    addContextMenu(childNodes[childNodes.Count - 1], "Delete Constituency");
                }
                treeNodes.Add(new TreeNode("Sectors", 0, 1, childNodes.ToArray()));
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
                    showSectorInfo(constituency);
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
        private void showSectorInfo(ConstituencyC consty)
        {
            try
            {
                txtSGSE.Text = consty.SGSE;
                txtName.Text = consty.Name;              
                rjActive.Checked = consty.Active;                
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async void ibtnSave_Click(object sender, EventArgs e)
        {
            if (PaintRequiredFilds())
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
                if (constituency.Id > 0)
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

        #endregion

        
    }
}
