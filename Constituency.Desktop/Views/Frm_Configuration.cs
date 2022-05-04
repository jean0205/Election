using Constituency.Desktop.Components;
using Constituency.Desktop.Controls;
using Constituency.Desktop.Entities;
using Constituency.Desktop.Helpers;
using Constituency.Desktop.Models;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Constituency.Desktop.Views
{
    public partial class Frm_Configuration : Form
    {

        WaitFormFunc waitForm = new WaitFormFunc();

        //Fields
        private string token = string.Empty;

        //objects
        User loggedUser { get; set; }
        User user { get; set; }

        private ObservableCollection<User> UserList;
        private List<string> Roles;
        public Frm_Configuration()
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            InitializeComponent();
            token = Main.GetInstance().tokenResponse.Token;
            loggedUser = Main.GetInstance().tokenResponse.User;
        }
        private async void Frm_Configuration_Load(object sender, EventArgs e)
        {
            UtilRecurrent.FindAllControlsIterative(this.gbAccess, "RJToggleButton").Cast<RJToggleButton>().ToList().ForEach(x => x.Click += rjToggle_ClickAsync);
            MandatoriesFilds();
            FildsValidations();
            await LoadUsers();
        }
        private void MandatoriesFilds()
        {//tag= 1 para campos obligados

            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel2, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("1")).ToList().ForEach(x => toolTip1.SetToolTip(x, "Mandatory Field"));
        }

        private void FildsValidations()
        {
            //tag=2 para only letters
            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel2, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("2")).ToList().ForEach(x => x.KeyPress += UtilRecurrent.txtOnlyLetters_KeyPress);

            //tag=3 para only numbers
            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel2, "TextBox").Cast<TextBox>().Where(x => x.Tag != null && x.Tag.ToString().Split(',').ToList().Contains("3")).ToList().ForEach(x => x.KeyPress += UtilRecurrent.txtOnlyIntegersNumber_KeyPress);
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
        private async Task LoadUsers()
        {
            waitForm.Show(this);
            Cursor.Hide();
            Response response = await ApiServices.GetListAsync<User>("Users", token);

            waitForm.Close();
            Cursor.Show();
            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return;
            }
            // Applicant = new Applicant();
            UserList = new ObservableCollection<User>((List<User>)response.Result);
            if (UserList.Any())
            {
                RefreshTreeView(UserList.ToList());
                //tView1.SelectedNode = tView1.Nodes[0];
            }
            else
            {
                cleanScreen();
            }
        }
        private void cleanScreen()
        {
            UtilRecurrent.FindAllControlsIterative(this.tabControl1, "TextBox").Cast<TextBox>().ToList().ForEach(x => x.Clear());
            UtilRecurrent.FindAllControlsIterative(this.tabControl1, "RJToggleButton").Cast<RJToggleButton>().ToList().ForEach(x => x.Checked = false);
            gbAccess.Visible = false;
            gbActions.Visible = false;
            txtUserName.ReadOnly = false;
            gbStatistics.Visible = false;
        }
        private void RefreshTreeView(List<User> users)
        {
            try
            {
                tView1.Nodes.Clear();
                //TreeNode node;
                List<TreeNode> treeNodes = new List<TreeNode>();
                List<TreeNode> childNodes = new List<TreeNode>();

                foreach (string letter in users.OrderBy(x => x.LastName).Select(d => d.LastName[0].ToString().ToUpper()).Distinct().ToList())
                {
                    foreach (User user in users.Where(d => d.LastName[0].ToString().ToUpper() == letter).ToList())
                    {
                        childNodes.Add(new TreeNode(user.FullName, 2, 3));
                        childNodes[childNodes.Count - 1].Tag = user.Id;
                        addContextMenu(childNodes[childNodes.Count - 1], "Delete User");
                        childNodes[childNodes.Count - 1].BackColor= user.Online? Color.Yellow : Color.White;
                    }
                    treeNodes.Add(new TreeNode(letter, 0, 1, childNodes.ToArray()));
                    treeNodes[treeNodes.Count - 1].Tag = Guid.Empty;
                    childNodes = new List<TreeNode>();
                }
                tView1.Nodes.AddRange(treeNodes.ToArray());
                tView1.ExpandAll();
            }
            catch (Exception ex)
            {
                 Crashes.TrackError(ex);  UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void tView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AfterSelectNodeTV1((Guid)e.Node.Tag);
        }
        private async void AfterSelectNodeTV1(Guid nodeTag)
        {
            try
            {
                if (nodeTag != Guid.Empty)
                {
                    user = UserList.Where(u => u.Id == nodeTag).FirstOrDefault();
                    showUserInfo(user);
                    Roles = await LoadUsersRoles(user);
                    UtilRecurrent.FindAllControlsIterative(gbAccess, "RJToggleButton").Cast<RJToggleButton>().ToList().ForEach(x => x.Checked = Roles.Contains(x.Name.Replace("rj", string.Empty)));
                                      
                    txtUserName.ReadOnly = true;
                    lblLogin.Text = user.LogInTime.ToLocalTime().ToString("dd-MMM-yyyy hh:mm:ss");
                    lblLogOut.Text = user.LogOutTime.ToLocalTime().ToString("dd-MMM-yyyy hh:mm:ss");
                    gbStatistics.Visible = true;
                    gbActions.Visible = true;
                    gbAccess.Visible = true;
                }
                else
                {
                    cleanScreen();
                }
            }
            catch (Exception ex)
            {
                 Crashes.TrackError(ex);  UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async Task<List<string>> LoadUsersRoles(User user)
        {
            waitForm.Show(this);
            Cursor.Hide();
            Response response = await ApiServices.GetUserRoles<IList<string>>("Users/Roles", user.UserName, token);

            waitForm.Close();
            Cursor.Show();
            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return null;
            }
            return (List<string>)response.Result;
        }
        private void showUserInfo(User user)
        {
            try
            {
                txtEmail.Text = user.Email;
                txtFirstName.Text = user.FirstName;
                txtLastName.Text = user.LastName;
                txtPhone.Text = user.PhoneNumber;
                txtUserName.Text = user.UserName;
                rjActive.Checked = user.Active;
                rjEmailConfirmed.Checked = user.EmailConfirmed;
                pictureBox1.ImageLocation = user.ImageFullPath;
            }
            catch (Exception ex)
            {
                 Crashes.TrackError(ex);  UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async void ibtnSave_Click(object sender, EventArgs e)
        {
            if (!UtilRecurrent.IsValidEmail(txtEmail.Text))
            {
                UtilRecurrent.ErrorMessage("You must provide a valid Email address.");
                return;
            }
            if (PaintRequiredFilds())
            {
                UtilRecurrent.ErrorMessage("Requireds fields missing. Find them highlighted in red.");
                return;
            }
            try
            {
                var savedUser = new Response();
                if (tView1.SelectedNode == null || (tView1.SelectedNode != null && (Guid)tView1.SelectedNode.Tag == Guid.Empty))
                {
                    savedUser = await SaveUpdateUser("Account/RegisterUser", NewUser());
                }
                else
                {
                    savedUser = await SaveUpdateUser("Users/EditUser", NewUser());
                }
                if (savedUser.IsSuccess)
                {
                    await LoadUsers();
                    tView1.SelectedNode = CollectAllNodes(tView1.Nodes).FirstOrDefault(x => Guid.Parse(x.Tag.ToString()) == ((User)savedUser.Result).Id);
                }
            }
            catch (Exception ex)
            {
                 Crashes.TrackError(ex);  UtilRecurrent.ErrorMessage(ex.Message);
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
        private AddUserViewModel NewUser()
        {
            try
            {
                return new AddUserViewModel()
                {
                    Active = true,
                    Email = txtEmail.Text,
                    FirstName = txtFirstName.Text.ToUpper(),
                    LastName = txtLastName.Text.ToUpper(),
                    PhoneNumber = txtPhone.Text,
                    Username = txtUserName.Text.ToLower(),
                };
            }
            catch (Exception ex)
            {
                 Crashes.TrackError(ex);  UtilRecurrent.ErrorMessage(ex.Message);
                return null;
            }

        }
        private async Task<Response> SaveUpdateUser(string route, AddUserViewModel user)
        {
            try
            {
                waitForm.Show(this);
                Cursor.Hide();
                Response response = await ApiServices.RegisterUser(route, user, token);
                waitForm.Close();
                Cursor.Show();
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response;
                }
                return response;
            }
            catch (Exception ex)
            {
                 Crashes.TrackError(ex);  UtilRecurrent.ErrorMessage(ex.Message);
                return new Response
                {
                    IsSuccess = false
                };
            }
        }
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
                case "Delete User":
                    {
                        if (loggedUser.UserName == user.UserName)
                        {
                            UtilRecurrent.InformationMessage("Your user can not be deleted by yourself. ", "Delete User");
                            return;
                        }
                        await DeleteUser(user.UserName);
                        await LoadUsers();
                        tView1.SelectedNode = tView1.Nodes[0];
                        waitForm.Close();
                        Cursor.Show();
                        break;
                    }
            }
        }
        private async Task DeleteUser(string id)
        {
            try
            {
                waitForm.Show(this);
                Cursor.Hide();
                Response response = await ApiServices.DeleteAsync("Users", id, token);
                waitForm.Close();
                Cursor.Show();
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return;
                }
            }
            catch (Exception ex)
            {
                 Crashes.TrackError(ex);  UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async void rjToggle_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                string access = ((RJToggleButton)sender).Name.Replace("rj", string.Empty);
                if (((RJToggleButton)sender).Checked)
                {
                    if (access == UserAccess.App_Configuration.ToString() || access == UserAccess.Manage_Users.ToString())
                    {
                        if (!UtilRecurrent.yesOrNot("Do you want to grant access to the App configuration for the selected user?", "Critical Access"))
                        {
                            ((RJToggleButton)sender).Checked = false;
                            return;
                        }
                    }
                    var userMod = await AddUserAccess("Users/AddAccess", user, access);
                    ((RJToggleButton)sender).Checked = userMod.Access.FirstOrDefault(a => a == access).Any();
                }
                else
                {
                    if (loggedUser.UserName == user.UserName && (access == UserAccess.App_Configuration.ToString() || access == UserAccess.Manage_Users.ToString()))
                    {
                        UtilRecurrent.InformationMessage("You cannot revoke access to the application settings by yourself. ", "Revoke Access");
                        ((RJToggleButton)sender).Checked = true;
                        return;
                    }
                    if (UtilRecurrent.yesOrNot("Do you want to remove this access to the selected User?", "Remove Access"))
                    {
                        var userMod = await AddUserAccess("Users/RemoveAccess", user, access);
                        ((RJToggleButton)sender).Checked = userMod.Access.Where(a => a == access).Any();
                    }
                    else
                    {
                        ((RJToggleButton)sender).Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                 Crashes.TrackError(ex);  UtilRecurrent.ErrorMessage(ex.Message);
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
            if (node_here == null)
            {
                return;
            }
        }
        private async Task<User> AddUserAccess(string route, User user, string access)
        {
            try
            {
                waitForm.Show(this);
                Cursor.Hide();
                Response response = await ApiServices.AddUserAccess<User>(route, user, access, token);

                waitForm.Close();
                Cursor.Show();
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return null;
                }
                return (User)response.Result;
            }
            catch (Exception ex)
            {
                 Crashes.TrackError(ex);  UtilRecurrent.ErrorMessage(ex.Message);
                return null;
            }
        }
        private async void rjToggleButton1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var userToAD = new Response();
                if (((RJToggleButton)sender).Checked)
                {
                    if (!UtilRecurrent.yesOrNot("Do you want to Re-Activate the selected user?", "Activate User"))
                    {
                        ((RJToggleButton)sender).Checked = false;
                        return;
                    }
                    userToAD = await SaveUpdateUser("Users/ActivateDeactivate", NewUser());
                    await LoadUsers();
                    tView1.SelectedNode = CollectAllNodes(tView1.Nodes).FirstOrDefault(x => Guid.Parse(x.Tag.ToString()) == ((User)userToAD.Result).Id);
                }
                else
                {
                    if (loggedUser.UserName == user.UserName)
                    {
                        UtilRecurrent.InformationMessage("You can not deactivate  your user by yourself. ", "Deactivate User");
                        ((RJToggleButton)sender).Checked = true;
                        return;
                    }
                    if (UtilRecurrent.yesOrNot("Do you want to De-Activate the selected user?", "De-Activate User"))
                    {
                        userToAD = await SaveUpdateUser("Users/ActivateDeactivate", NewUser());
                        await LoadUsers();
                        tView1.SelectedNode = CollectAllNodes(tView1.Nodes).FirstOrDefault(x => Guid.Parse(x.Tag.ToString()) == ((User)userToAD.Result).Id);
                    }
                    else
                    {
                        ((RJToggleButton)sender).Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                 Crashes.TrackError(ex);  UtilRecurrent.ErrorMessage(ex.Message);
            }
        }

        
    }
}
