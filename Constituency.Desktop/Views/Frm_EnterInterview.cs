using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Constituency.Desktop.Views
{
    public partial class Frm_EnterInterview : Form
    {
        public Frm_EnterInterview()
        {
            InitializeComponent();
        }

        private void txtReg_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        //private async Task<Applicant> LoadApplicantByIDAsync(string route, string Id)
        //{
        //    try
        //    {
        //        waitForm.Show(this);
        //        Cursor.Hide();

        //        Response response = await ApiServices.FindAsync<Applicant>(route, Id, token);
        //        waitForm.Close();
        //        Cursor.Show();
        //        if (!response.IsSuccess)
        //        {
        //            if (response.Message == "Not Found")
        //            {
        //                UtilRecurrent.ErrorMessage(response.Message);
        //                return null;
        //            }
        //            UtilRecurrent.ErrorMessage(response.Message);
        //            return null;
        //        }
        //        return (Applicant)response.Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        Crashes.TrackError(ex); UtilRecurrent.ErrorMessage(ex.Message);
        //        return null;
        //    }
        //}
    }
}
