

using Constituency.Desktop.Components;
using Constituency.Desktop.Helpers;
using Constituency.Desktop.Models;
using Microsoft.AppCenter.Crashes;

namespace Constituency.Desktop.Views
{
    public partial class Frm_Login : Form
    {
        WaitFormFunc waitForm = new WaitFormFunc();
        public Frm_Login()
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            // Make the GUI ignore the DPI setting
           
            InitializeComponent();
            //Font = new Font(Font.Name, 8.25f * 96f / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);
        }
        //public string CurrentVersion
        //{
        //    get
        //    {
        //        return ApplicationDeployment.IsNetworkDeployed
        //               ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
        //               : "Error";
        //    }
        //}

        public object ApplicationDeployment { get; private set; }

        private void iconButton1_ClickAsync(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_Login_Load(object sender, EventArgs e)
        {           

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            LoggingAsync();
        }
        private bool MissingInfo()
        {
            if (txtUserName.TextLength == 0)
            {
                UtilRecurrent.InformationMessage("Enter the user", "Missing Information");
                return true;
            }
            if (mtxtPassword.TextLength == 0)
            {
                UtilRecurrent.InformationMessage("Enter the password", "Missing Information");
                return true;
            }
            if (mtxtPassword.TextLength < 6)
            {
                UtilRecurrent.InformationMessage("The password must have at least 6 characters", "Missing Information");
                return true;
            }
            return false;
        }

        private async void LoggingAsync()
        {
            try
            {
                if (MissingInfo())
                {
                    return;
                }
                waitForm.Show(this);

                Response response = await ApiServices.LoginAsync(new LoginRequest
                {
                    UserName = txtUserName.Text,
                    Password = mtxtPassword.Text
                });
                waitForm.Close();
                ;
                if (!response.IsSuccess)
                {
                    MessageBox.Show(response.Message,
                                    "Error",
                                         MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation,
                                                MessageBoxDefaultButton.Button1);
                    return;
                }
               
                TokenResponse tokenResponse = (TokenResponse)response.Result;
                //TODO LLAMAR AL FORMULARIO PRINCIPAL DESDE AQUI
                //Main main = new Main(tokenResponse);
                //Analytics.TrackEvent("Login  " + tokenResponse.User.FullName);
                //this.Hide();
                //main.ShowDialog();
                //this.Show();
                //txtUserName.Clear();
                //mtxtPassword.Clear();
            }
            catch (Exception ex)
            {
                 Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return;
            }
        }

        private void Frm_Login_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                LoggingAsync();
            }
        }

        private async void label3_Click(object sender, EventArgs e)
        {
            if (txtUserName.TextLength == 0)
            {
                UtilRecurrent.ErrorMessage("Enter the username");
                return;
            }
            if (!UtilRecurrent.yesOrNot("Do you want to reset your password?", "Reset Password"))
            {
                return;
            }
           
            waitForm.Show(this);
            Response response = await ApiServices.PostAsync<RecoverPasswordViewModel>("Account/RecoverPassword", new RecoverPasswordViewModel
            {
                userName = txtUserName.Text
            });
            waitForm.Close();
            ;
            if (!response.IsSuccess)
            {
              UtilRecurrent.ErrorMessage(response.Message);
                return;
            }
            UtilRecurrent.InformationMessage(response.Message, "Recover Password");

        }
    }
}
