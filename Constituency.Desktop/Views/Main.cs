using Constituency.Desktop.Components;
using Constituency.Desktop.Entities;
using Constituency.Desktop.Helpers;
using Constituency.Desktop.Models;
using FontAwesome.Sharp;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;


namespace Constituency.Desktop.Views
{
    public partial class Main : Form
    {
       
        private static Main _intance;
        WaitFormFunc waitForm = new WaitFormFunc();
        public  Main(TokenResponse token)
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            InitializeComponent();
            _intance = this;
            this.tokenResponse = token;
            
        }
        public TokenResponse tokenResponse { get; set; }
      
        public static Main GetInstance()
        {
            return _intance;
        }    

        private async void Main_Load(object sender, EventArgs e)
        {
            ReadUserInfo();            
              
        }
       

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            if (!await ValidateAccess(((IconButton)sender).Tag.ToString()))
            {
                Analytics.TrackEvent("Frm_EnterInterview NO ACCESS " + tokenResponse.User.FullName);
                UtilRecurrent.InformationMessage("You have not access to this feature in this Application.\r\n Please Contact your System Administrator to request the access", "User Access");
                return;
            }

            try
            {
                Analytics.TrackEvent("Frm_EnterInterview  " + tokenResponse.User.FullName);
                Frm_EnterInterview frm = new();
                frm.Show(this);
            }
            catch (Exception ex)
            {
                UtilRecurrent.ErrorMessage(ex.Message);
                return;
            }
        }

        private async void iconButton3_ClickAsync(object sender, EventArgs e)
        {
            if (!await ValidateAccess(((IconButton)sender).Tag.ToString()))
            {
                Analytics.TrackEvent("Frm_Voters NO ACCESS " + tokenResponse.User.FullName);
                UtilRecurrent.InformationMessage("You have not access to this feature in this Application.\r\n Please Contact your System Administrator to request the access", "User Access");
                return;
            }

            try
            {
                Analytics.TrackEvent("Frm_Voters " + tokenResponse.User.FullName);
                Frm_Voters frm = new Frm_Voters();
                frm.Show(this);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return;
            }
        }

        private async void iconButton2_Click(object sender, EventArgs e)
        {
            //if (!await ValidateAccess(((IconButton)sender).Tag.ToString()))
            //{
            //    Analytics.TrackEvent("Frm_ReviewApplications NO ACCESS " + tokenResponse.User.FullName);
            //    UtilRecurrent.InformationMessage("You have not access to this feature in this Application.\r\n Please Contact your System Administrator to request the access", "User Access");
            //    return;
            //}
           
            try
            {
                Analytics.TrackEvent("Frm_ReviewApplications " + tokenResponse.User.FullName);
                Frm_Houses frm = new Frm_Houses();
                frm.Show(this);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return;
            }
        }
        private async void iconButton1_Click_1(object sender, EventArgs e)
        {
            if (!await ValidateAccess(((IconButton)sender).Tag.ToString()))
            {
                Analytics.TrackEvent("Frm_ElectionVotes NO ACCESS " + tokenResponse.User.FullName);
                UtilRecurrent.InformationMessage("You have not access to this feature in this Application.\r\n Please Contact your System Administrator to request the access", "User Access");
                return;
            }

            try
            {
                Analytics.TrackEvent("Frm_ElectionVotes " + tokenResponse.User.FullName);
                Frm_ElectionVotes frm = new Frm_ElectionVotes();
                frm.Show(this);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return;
            }
        }
        private  void timer1_Tick(object sender, EventArgs e)
        {
            
            lblTime.Text = DateTime.Now.ToLongTimeString();
            lblDate.Text = DateTime.Now.ToLongDateString();
        }

        private async void iconButton2_Click_1(object sender, EventArgs e)
        {
            if (!await ValidateAccess(((IconButton)sender).Tag.ToString()))
            {
                Analytics.TrackEvent("Frm_Reports NO ACCESS " + tokenResponse.User.FullName);
                UtilRecurrent.InformationMessage("You have not access to this feature in this Application.\r\n Please Contact your System Administrator to request the access", "User Access");
                return;
            }
           
            try
            {
                Analytics.TrackEvent("Frm_Reports " + tokenResponse.User.FullName);
                //Frm_Reports frm = new Frm_Reports();
                //frm.Show(this);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return;
            }
        }

        private async void iconButton4_Click(object sender, EventArgs e)
        {
            if (!await ValidateAccess(((IconButton)sender).Tag.ToString()))
            {
                Analytics.TrackEvent("Frm_Configuration NO ACCESS " + tokenResponse.User.FullName);
                UtilRecurrent.InformationMessage("You have not access to this feature in this Application.\r\n Please Contact your System Administrator to request the access", "User Access");
                return;
            }

            try
            {
                Analytics.TrackEvent("Frm_Configuration " + tokenResponse.User.FullName);
                Frm_Configuration frm = new Frm_Configuration();
                frm.Show(this);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return;
            }
        }

        private async void iconButton3_Click(object sender, EventArgs e)
        {
            if (!await ValidateAccess(((IconButton)sender).Tag.ToString()))
            {
                Analytics.TrackEvent("Frm_App_Maintenance NO ACCESS " + tokenResponse.User.FullName);
                UtilRecurrent.InformationMessage("You have not access to this feature in this Application.\r\n Please Contact your System Administrator to request the access", "User Access");
                return;
            }

            try
            {
                Analytics.TrackEvent("Frm_App_Maintenance " + tokenResponse.User.FullName);
                Frm_App_Maintenance frm = new Frm_App_Maintenance();
                frm.Show(this);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return;
            }
        }
        private async void iconButton5_Click_1(object sender, EventArgs e)
        {
            if (!await ValidateAccess(((IconButton)sender).Tag.ToString()))
            {
                Analytics.TrackEvent("Frm_AllApplications NO ACCESS " + tokenResponse.User.FullName);
                UtilRecurrent.InformationMessage("You have not access to this feature in this Application.\r\n Please Contact your System Administrator to request the access", "User Access");
                return;
            }

           
            try
            {
                Analytics.TrackEvent("Frm_AllApplications  " + tokenResponse.User.FullName);
                //Frm_AllApplications frm = new Frm_AllApplications();
                //frm.Show(this);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
               
                return;
            }
        }
        private async void iconButton7_Click(object sender, EventArgs e)
        {
            if (!await ValidateAccess(((IconButton)sender).Tag.ToString()))
            {
                Analytics.TrackEvent("Frm_MasterFile NO ACCESS " + tokenResponse.User.FullName);
                UtilRecurrent.InformationMessage("You have not access to this feature in this Application.\r\n Please Contact your System Administrator to request the access", "User Access");
                return;
            }           
            try
            {
                Analytics.TrackEvent("Frm_MasterFile " + tokenResponse.User.FullName);
                waitForm.Show(this);
                //Frm_MasterFile frm = new Frm_MasterFile();
                //frm.Show(this);
             UtilRecurrent.UnlockForm(waitForm, this);
               
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return;
            }
        }
        private async void ibtnChangePassword_Click(object sender, EventArgs e)
        {
            if (ValidatePasswordChange())
            {
                ChangePasswordViewModel model = new ChangePasswordViewModel()
                {
                    OldPassword = mtxtOldPassw.Text,
                    NewPassword= mtxtNewPassw.Text, 
                    Confirm=mtxtConfirmPassw.Text,
                };
               if( await changePasswordAsync(model))
                {
                    UtilRecurrent.InformationMessage("Your password was sucessfully changed.", "Change Password");
                    mtxtOldPassw.Clear();
                    mtxtNewPassw.Clear();
                    mtxtConfirmPassw.Clear();
                    panelPasswd.Visible = false;
                }

            }

        }
        private async void iconButton5_Click(object sender, EventArgs e)
        {
            if (UtilRecurrent.yesOrNot("Do you want to resend the confirmation email?", "Confirmation Email"))
            {
                await ResendEmail();
            }
        }
        private bool ValidatePasswordChange()
        {
            try
            {

                if (mtxtOldPassw.TextLength<6)
                {
                    UtilRecurrent.InformationMessage("The old password must have at least 6 characters.", "Change Password");
                    return false;
                }
                if (mtxtNewPassw.TextLength < 6|| mtxtConfirmPassw.TextLength < 6)
                {
                    UtilRecurrent.InformationMessage("The new password must have at least 6 characters.", "Change Password");
                    return false;
                }
                if (mtxtNewPassw.Text!=mtxtConfirmPassw.Text)
                {
                    UtilRecurrent.InformationMessage("The new password and the confirm password must be the same.", "Change Password");
                    return false;
                }
                if (mtxtOldPassw.Text== mtxtNewPassw.Text)
                {
                    UtilRecurrent.InformationMessage("The old and the new password can not be the same.", "Change Password");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }
        }
        private void ReadUserInfo()
        {
            lblFirstName.Text = tokenResponse.User.FirstName;
            lblLastName.Text = tokenResponse.User.LastName;
            lblUserName.Text = tokenResponse.User.UserName;
            lblEmail.Text = tokenResponse.User.Email;
            lblPhone.Text = tokenResponse.User.PhoneNumber;
            rjEmailConfirmed.Checked = tokenResponse.User.EmailConfirmed;
            lblConfirmed.Visible = !rjEmailConfirmed.Checked;
            ibtnResenEmail.Visible = !rjEmailConfirmed.Checked;
            pictureBox1.ImageLocation = tokenResponse.User.ImageFullPath;
            lblLogin.Text = tokenResponse.User.LogInTime.ToLocalTime().ToString("dd-MMM-yyyy hh:mm:ss");
            lblLogOut.Text = tokenResponse.User.LogOutTime.ToLocalTime().ToString("dd-MMM-yyyy hh:mm:ss");

        }
        private async Task<bool> ValidateAccess(string requiredAccess)
        {
            try
            {
               var roles= await LoadUsersRoles(tokenResponse.User);
                if (roles!=null && roles.Contains(requiredAccess))
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }
        }
        private async Task<List<string>> LoadUsersRoles(User user)
        {
            UtilRecurrent.LockForm(waitForm, this);
          
            Response response = await ApiServices.GetUserRoles<IList<string>>("Users/Roles", user.UserName, tokenResponse.Token);

         UtilRecurrent.UnlockForm(waitForm, this);
            
            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return null;
            }
            return (List<string>)response.Result;
        }
        private async Task<bool> changePasswordAsync(ChangePasswordViewModel model)
        {
            waitForm.Show(this);
          
            Response response = await ApiServices.PostAsync<ChangePasswordViewModel>("Account/ChangePassword", model, tokenResponse.Token);

         UtilRecurrent.UnlockForm(waitForm, this);
            
            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return false;
            }
            return true;
        }
        private async Task<bool> ResendEmail()
        {
            try
            {
                waitForm.Show(this);
              
                Response response = await ApiServices.ResendEmail("Account/SendEmailConfirmation", tokenResponse.User.UserName, tokenResponse.Token);

             UtilRecurrent.UnlockForm(waitForm, this);
                
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return false;
                }
                UtilRecurrent.InformationMessage(response.Message,"Confirmation Email");
                return true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }
        }
        private void mtxtOldPassw_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void ibtnClosePanel_Click(object sender, EventArgs e)
        {
            panelPasswd.Visible = false;    
        }

        private void ibtnShowPanelPassword_Click(object sender, EventArgs e)
        {
            panelPasswd.Visible = true;
            panelPasswd.BringToFront();
        }

        private void ibtnSeeOld_MouseDown(object sender, MouseEventArgs e)
        {
            if (((IconButton)sender).Tag.ToString() == "o")
            {
                mtxtOldPassw.PasswordChar = '\0';
            }
            if (((IconButton)sender).Tag.ToString() == "n")
            {
                mtxtNewPassw.PasswordChar = '\0';
            }
            if (((IconButton)sender).Tag.ToString() == "c")
            {
                mtxtConfirmPassw.PasswordChar = '\0';
            }
        }

        private void ibtnSeeOld_MouseUp(object sender, MouseEventArgs e)
        {
            if (((IconButton)sender).Tag.ToString() == "o")
            {
                mtxtOldPassw.PasswordChar = '*';
            }
            if (((IconButton)sender).Tag.ToString() == "n")
            {
                mtxtNewPassw.PasswordChar = '*';
            }
            if (((IconButton)sender).Tag.ToString() == "c")
            {
                mtxtConfirmPassw.PasswordChar = '*';
            }
        }

        private async void ibtnPicture_Click(object sender, EventArgs e)
        {
            try
            {
                
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.DefaultExt = "jpg";
                ofd.FileName = "Upload Profile Picture";
                ofd.Filter = "JPG files|*.jpg";
                ofd.Title = "Select Picture";
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    FileInfo fdirectory = new FileInfo(ofd.FileName);
                    FileInfo fPath = new FileInfo(ofd.FileName);
                    string folder = fdirectory.Directory.ToString();
                    string filename = fPath.Name;
                    var savedUser = new Response();
                    savedUser = await SaveUpdateUser("Users/UploadPicture", UserPicture(fPath.ToString()));
                    if (savedUser.IsSuccess)
                    {
                        pictureBox1.ImageLocation = ((User)savedUser.Result).ImageFullPath;
                    }
                }              
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }


        private AddUserViewModel UserPicture(string path)
        {
            try
            {
                return new AddUserViewModel()
                {
                    Active = true,
                    Email = lblEmail.Text,
                    FirstName = lblFirstName.Text.ToUpper(),
                    LastName = lblLastName.Text.ToUpper(),
                    PhoneNumber = lblPhone.Text,
                    Username = tokenResponse.User.UserName,
                    ImageFile = File.ReadAllBytes(path)
                
            };
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return null;
            }

        }
        private async Task<Response> SaveUpdateUser(string route, AddUserViewModel user)
        {
            try
            {
                waitForm.Show(this);
              
                Response response = await ApiServices.RegisterUser(route, user, tokenResponse.Token);
             UtilRecurrent.UnlockForm(waitForm, this);
                
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response;
                }
                return response;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
               
                return new Response
                {
                    IsSuccess = false
                };
            }
        }

        private async void iconButton6_Click(object sender, EventArgs e)
        {
            //Frm_MasterFileImport frm1 = new Frm_MasterFileImport();
            //frm1.Show(this);
            //return;           

            if (UtilRecurrent.yesOrNot("Do you want to reset the profile picture?","Reset Picture"))
            {
                var savedUser = new Response();
                savedUser = await ResetUserUser("Users/ResetPicture", ResetPicture());
                if (savedUser.IsSuccess)
                {
                    pictureBox1.ImageLocation = ((User)savedUser.Result).ImageFullPath;
                }

            }
        }
        private AddUserViewModel ResetPicture()
        {
            try
            {
                return new AddUserViewModel()
                {
                    Active = true,
                    Email = lblEmail.Text,
                    FirstName = lblFirstName.Text.ToUpper(),
                    LastName = lblLastName.Text.ToUpper(),
                    PhoneNumber = lblPhone.Text,
                    Username = tokenResponse.User.UserName,
                };
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return null;
            }

        }
        private async Task<Response> ResetUserUser(string route, AddUserViewModel user)
        {
            try
            {
                waitForm.Show(this);
              
                Response response = await ApiServices.RegisterUser(route, user, tokenResponse.Token);
             UtilRecurrent.UnlockForm(waitForm, this);
                
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response;
                }
                return response;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
               
                return new Response
                {
                    IsSuccess = false
                };
            }
        }

        private async void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // waitForm.Show(this);
                //Cursor.Hide();
                Response response = await ApiServices.LogutAsync("Account/Logout", tokenResponse.Token);
                Analytics.TrackEvent("Logout  " + tokenResponse.User.FullName);
                // waitForm.Close();
                // 

                FormCollection forms = Application.OpenForms;
                for (int i = forms.Count - 1; i >= 0; i--)
                {
                    //if (forms[i].Name != "Frm_Login")
                    //{
                    forms[i].Close();
                    //}
                }
            }
            catch (Exception ex)
            {

                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }

        //private void InstallUpdateSyncWithInfo()
        //{
        //    UpdateCheckInfo info = null;

        //    if (ApplicationDeployment.IsNetworkDeployed)
        //    {
        //        ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
        //        try
        //        {
        //            info = ad.CheckForDetailedUpdate();

        //        }
        //        catch (DeploymentDownloadException dde)
        //        {
        //            MessageBox.Show("The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message);
        //            return;
        //        }
        //        catch (InvalidDeploymentException ide)
        //        {
        //            MessageBox.Show("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message);
        //            return;
        //        }
        //        catch (InvalidOperationException ioe)
        //        {
        //            MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
        //            return;
        //        }
        //        if (info.UpdateAvailable)
        //        {
        //            Boolean doUpdate = true;

        //            if (!info.IsUpdateRequired)
        //            {
        //                DialogResult dr = MessageBox.Show("An update is available. Would you like to update the application now?", "Update Available", MessageBoxButtons.OKCancel);
        //                if (!(DialogResult.OK == dr))
        //                {
        //                    doUpdate = false;
        //                }
        //            }
        //            else
        //            {
        //                // Display a message that the app MUST reboot. Display the minimum required version.
        //                MessageBox.Show("This application has detected a mandatory update from your current " +
        //                    "version to version " + info.MinimumRequiredVersion.ToString() +
        //                    ". The application will now install the update and restart.",
        //                    "Update Available", MessageBoxButtons.OK,
        //                    MessageBoxIcon.Information);
        //            }
        //            if (doUpdate)
        //            {
        //                try
        //                {
        //                    ad.Update();
        //                    MessageBox.Show("The application has been upgraded, and will now restart.");
        //                    Application.Restart();
        //                }
        //                catch (DeploymentDownloadException dde)
        //                {
        //                    MessageBox.Show("Cannot install the latest version of the application. \n\nPlease check your network connection, or try again later. Error: " + dde);
        //                    return;
        //                }
        //            }
        //        }
        //    }
        //}

        private void iconButton8_Click(object sender, EventArgs e)
        {
            //Frm_MasterFileImport frm = new Frm_MasterFileImport();
            //frm.ShowDialog();   
            //InstallUpdateSyncWithInfo();

        }
    }
}
