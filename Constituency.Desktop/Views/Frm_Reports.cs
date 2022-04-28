using Constituency.Desktop.Components;
using Constituency.Desktop.Entities;
using Constituency.Desktop.Helpers;
using Constituency.Desktop.Models;
using FontAwesome.Sharp;
using Microsoft.AppCenter.Crashes;
using System.Data;

namespace Constituency.Desktop.Views
{
    public partial class Frm_Reports : Form
    {
        //Fields
        private string token = string.Empty;
        WaitFormFunc waitForm = new WaitFormFunc();

        //objects
        User user { get; set; }

        private List<ConstituencyC> ConstituenciesList;
        private List<PollingDivision> PollingDivisionsList;
        private List<CanvasType> CanvasTypeList;
        private List<Canvas> CanvasList;
        public Frm_Reports()
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            InitializeComponent();
            token = Main.GetInstance().tokenResponse.Token;
            user = Main.GetInstance().tokenResponse.User;
        }

        private async void Frm_Reports_Load(object sender, EventArgs e)
        {
            await LoadConstituencies();
            await LoadCanvasTypes();
            DGVFormats();
        }
        private async Task<bool> ValidateAccess(string requiredAccess)
        {
            try
            {
                var roles = await LoadUsersRoles(user);
                if (roles != null && roles.Contains(requiredAccess))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                UtilRecurrent.UnlockForm(waitForm, this);
                return false;
            }
        }
        private async Task<List<string>> LoadUsersRoles(User user)
        {
            UtilRecurrent.LockForm(waitForm, this);
            Response response = await ApiServices.GetUserRoles<IList<string>>("Users/Roles", user.UserName,token);
            UtilRecurrent.UnlockForm(waitForm, this);
            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return null;
            }
            return (List<string>)response.Result;
        }

        #region Tab1
        private void DGVFormats()
        {//tag= 1 para campos obligados

            UtilRecurrent.FindAllControlsIterative(tabControl1, "DataGridView").Cast<DataGridView>().ToList().ForEach(x => x.DefaultCellStyle.BackColor = Color.Beige);
            UtilRecurrent.FindAllControlsIterative(tabControl1, "DataGridView").Cast<DataGridView>().ToList().ForEach(x => x.AlternatingRowsDefaultCellStyle.BackColor = Color.Bisque);
            UtilRecurrent.FindAllControlsIterative(tabControl1, "DataGridView").Cast<DataGridView>().ToList().ForEach(x => x.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells);
            UtilRecurrent.FindAllControlsIterative(tabControl1, "DataGridView").Cast<DataGridView>().ToList().ForEach(x => x.RowHeadersVisible = false);
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

                cmb2Contituency.DataSource = null;
                cmb2Contituency.DataSource = ConstituenciesList;
                cmb2Contituency.ValueMember = "Id";
                cmb2Contituency.DisplayMember = "Name";
                cmb2Contituency.SelectedItem = null;
            }
        }
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
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
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
        private async Task LoadCanvasTypes()
        {
            Response response = await ApiServices.GetListAsync<CanvasType>("CanvasTypes", token);
            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return;
            }
            CanvasTypeList = new((List<CanvasType>)response.Result);
            if (CanvasTypeList.Any())
            {
                cmbCanvasTypes.DataSource = null;
                cmbCanvasTypes.DataSource = CanvasTypeList.Where(x => x.Active).ToList();
                cmbCanvasTypes.ValueMember = "Id";
                cmbCanvasTypes.DisplayMember = "Type";
                cmbCanvasTypes.SelectedItem = null;               
               
            }
        }
        private void cmbCanvasType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbCanvasTypes.SelectedIndex > -1)
                {
                    FillUpdComboboxCanvas();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void FillUpdComboboxCanvas()
        {
            try
            {
                if (CanvasTypeList.Where(p => p.Id == (int)cmbCanvasTypes.SelectedValue).Select(p => p.Canvas).Any())
                {
                    cmbCanvas.DataSource = null;
                    CanvasList = new List<Canvas>();
                    CanvasList = CanvasTypeList.Where(p => p.Id == (int)cmbCanvasTypes.SelectedValue).SelectMany(p => p.Canvas).ToList();
                    cmbCanvas.DataSource = CanvasList;
                    cmbCanvas.ValueMember = "Id";
                    cmbCanvas.DisplayMember = "Name";
                    cmbCanvas.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async void radioButton1_Click(object sender, EventArgs e)
        {
            if ( cmbDivision.SelectedItem == null)
            {
                UtilRecurrent.ErrorMessage("Division is required.");
                return;
            }
            if (rjCanvasActive.Checked&& cmbCanvas.SelectedItem==null)
            {
                UtilRecurrent.ErrorMessage("Canvas is required when you are filtering by Canvas.");
                return;
            }
            int divisionId = (cmbDivision.SelectedItem as PollingDivision).Id;
            if (rbtnInterviewed.Checked)
            {
                //voy a filtrar por canvasD
                if (rjCanvasActive.Checked)
                {
                    int canvasId = (cmbCanvas.SelectedItem as Canvas).Id;
                    FillUpDGV("Voters/ByDivisionsAndCanvas", divisionId, canvasId,true);                   
                }
                else
                {
                    FillUpDGV("Voters/ByDivisionsAndAllOpenCanvas", divisionId, 0, true);
                }
            }
            if (rbtnNotInterviewed.Checked)
            {
                //voy a filtrar por canvasD
                if (rjCanvasActive.Checked)
                {
                    int canvasId = (cmbCanvas.SelectedItem as Canvas).Id;
                    FillUpDGV("Voters/ByDivisionsAndCanvas", divisionId, canvasId, false);
                }
                else
                {
                    FillUpDGV("Voters/ByDivisionsAndAllOpenCanvas", divisionId, 0, false);
                }

            }
        }

        #endregion

        private  async void FillUpDGV(string route, int divisionId, int canvasId, bool interviewd)
        {
            dgv1Interviews.DataSource = null;
            var result = await  LoadCanvasReport(route, divisionId, canvasId, interviewd);
            dgv1Interviews.DataSource = result.Select(v => new
            {               
                Div = v.PollingDivision.Name,
                v.Reg,
                v.FullName,
                DateOfBirth = v.DOB,
                v.Sex,
                v.Occupation,
                v.Address,
                v.Mobile1,
                v.Mobile2,
                v.HomePhone,
                v.WorkPhone,
                v.Email
            }).ToList();
            lblTotal1.Text = result.Count.ToString();

        }
        private async Task<List<Voter>> LoadCanvasReport(string controller, int divisionId, int canvasId, bool interviewed)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.GetListAsyncReportsVotersByCanvas<Voter>(controller, divisionId, canvasId, interviewed, token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return null;
                }
                return new((List<Voter>)response.Result);

            }
            catch (Exception ex)
            {
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return null;
            }
           
        }

        private void rjCanvasActive_CheckedChanged(object sender, EventArgs e)
        {
            tPanelCanvas.Enabled = rjCanvasActive.Checked;
            cmbCanvasTypes.SelectedIndex = -1;
            cmbCanvas.SelectedIndex = -1;
        }
        
        private async void ibtnRefresh_Click(object sender, EventArgs e)
        {
            if (!await ValidateAccess(((IconButton)sender).Tag.ToString()))
            {
                UtilRecurrent.InformationMessage("You have not access to this feature in this Application.\r\n Please Contact your System Administrator to request the access", "User Access");
                return;
            }
            dgv1Interviews.MultiSelect = true;
            UtilRecurrent.ExportToExcel(dgv1Interviews);
            dgv1Interviews.MultiSelect = false;
            dgv1Interviews.CurrentCell = null;
        }
   
       
    }
}
