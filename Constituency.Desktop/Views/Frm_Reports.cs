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
        private List<Party> PartiesList;
        public Frm_Reports()
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            InitializeComponent();
            token = Main.GetInstance().tokenResponse.Token;
            user = Main.GetInstance().tokenResponse.User;
        }

        private async void Frm_Reports_Load(object sender, EventArgs e)
        {
            // UtilRecurrent.LockForm(waitForm, this);
            if (!await ValidateAccess("General_Reports"))
            {
                tabControl1.TabPages.RemoveAt(1);
            }
            await LoadConstituencies();
            await LoadCanvasTypes();
            await LoadParties();
            UtilRecurrent.UnlockForm(waitForm, this);
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
            Response response = await ApiServices.GetUserRoles<IList<string>>("Users/Roles", user.UserName, token);
            UtilRecurrent.UnlockForm(waitForm, this);
            if (!response.IsSuccess)
            {
                UtilRecurrent.ErrorMessage(response.Message);
                return null;
            }
            return (List<string>)response.Result;
        }

        #region General
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
                cmbConstituency.BindingContext = new BindingContext();
                cmbConstituency.DataSource = null;
                cmbConstituency.DataSource = ConstituenciesList;
                cmbConstituency.ValueMember = "Id";
                cmbConstituency.DisplayMember = "Name";
                cmbConstituency.SelectedItem = null;

                cmb2Constituency.BindingContext = new BindingContext();
                cmb2Constituency.DataSource = null;
                cmb2Constituency.DataSource = ConstituenciesList;
                cmb2Constituency.ValueMember = "Id";
                cmb2Constituency.DisplayMember = "Name";
                cmb2Constituency.SelectedItem = null;
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
                cmb2Party.BindingContext = new BindingContext();
                cmb2Party.DataSource = null;
                cmb2Party.DataSource = PartiesList;
                cmb2Party.ValueMember = "Id";
                cmb2Party.DisplayMember = "Name";
                cmb2Party.SelectedItem = null;
            }
        }

        #endregion

        #region Tab1

        private void cmbConstituency1_SelectionChangeCommitted(object sender, EventArgs e)
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
                    cmb2Division.BindingContext = new BindingContext();
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
                cmbCanvasTypes.BindingContext = new BindingContext();
                cmbCanvasTypes.DataSource = null;
                cmbCanvasTypes.DataSource = CanvasTypeList.Where(x => x.Active).ToList();
                cmbCanvasTypes.ValueMember = "Id";
                cmbCanvasTypes.DisplayMember = "Type";
                cmbCanvasTypes.SelectedItem = null;

                cmb2CanvasTypes.BindingContext = new BindingContext();
                cmb2CanvasTypes.DataSource = null;
                cmb2CanvasTypes.DataSource = CanvasTypeList.Where(x => x.Active).ToList();
                cmb2CanvasTypes.ValueMember = "Id";
                cmb2CanvasTypes.DisplayMember = "Type";
                cmb2CanvasTypes.SelectedItem = null;
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
                    cmbCanvas.BindingContext = new BindingContext();
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
            if (cmbDivision.SelectedItem == null)
            {
                UtilRecurrent.ErrorMessage("Division is required.");
                return;
            }
            if (rjCanvasActive.Checked && cmbCanvas.SelectedItem == null)
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
                    FillUpDGV1("Voters/ByDivisionsAndCanvas", divisionId, canvasId, true);
                }
                else
                {
                    FillUpDGV1("Voters/ByDivisionsAndAllOpenCanvas", divisionId, 0, true);
                }
            }
            if (rbtnNotInterviewed.Checked)
            {
                //voy a filtrar por canvasD
                if (rjCanvasActive.Checked)
                {
                    int canvasId = (cmbCanvas.SelectedItem as Canvas).Id;
                    FillUpDGV1("Voters/ByDivisionsAndCanvas", divisionId, canvasId, false);
                }
                else
                {
                    FillUpDGV1("Voters/ByDivisionsAndAllOpenCanvas", divisionId, 0, false);
                }

            }
        }

        private async void FillUpDGV1(string route, int divisionId, int canvasId, bool interviewd)
        {
            dgv1Interviews.DataSource = null;
            var result = await LoadCanvasReport1(route, divisionId, canvasId, interviewd);
            dgv1Interviews.DataSource = result.Select(v => new
            {
                Div = v.PollingDivision.Name,
                v.Reg,
                v.SurName,
                v.GivenNames,
                v.Sex,
                v.Occupation,
                v.Address,
                v.Mobile1,
                v.Mobile2,
                v.HomePhone,
                v.WorkPhone,
                v.Email
            }).OrderBy(v => v.SurName).ThenBy(v => v.GivenNames).ToList();
            lblTotal1.Text = result.Count.ToString();

        }
        private async Task<List<Voter>> LoadCanvasReport1(string controller, int divisionId, int canvasId, bool interviewed)
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

            tPanelCanvas2.Enabled = rjFCanvas.Checked;
            cmb2CanvasTypes.SelectedIndex = !rjFCanvas.Checked ? -1 : cmb2CanvasTypes.SelectedIndex;
            cmb2Canvas.SelectedIndex = !rjFCanvas.Checked ? -1 : cmb2Canvas.SelectedIndex;

            tPanelConstituency2.Enabled = rjFConstituency.Checked;
            cmb2Constituency.SelectedIndex = !rjFConstituency.Checked ? -1 : cmb2Constituency.SelectedIndex;
            cmb2Division.SelectedIndex = !rjFConstituency.Checked ? -1 : cmb2Division.SelectedIndex;
        }

        private async void ibtnExport1_Click(object sender, EventArgs e)
        {
            if (!await ValidateAccess(((IconButton)sender).Tag.ToString()))
            {
                UtilRecurrent.InformationMessage("You have not access to this feature in this Application.\r\n Please Contact your System Administrator to request the access", "User Access");
                return;
            }

            gbReport.Visible = true;
            gbReport.BringToFront();
            chkColumnsExport.Items.AddRange(dgv1Interviews.Columns.Cast<DataGridViewColumn>().Select(p => p.Name).ToArray());
            //chkColumnsExport.Items.Add("--------------------------------------");            
            chkColumnsExport.Items.AddRange(new List<string> { "RE-Reg (Y/N)", "Party Support", "Contact Number", "Comments" }.ToArray());
            for (int i = 0; i < chkColumnsExport.Items.Count; i++)
            {
                chkColumnsExport.SetItemChecked(i, true);
            }
        }
        private void ImportDataGridViewDataToExcelSheet(DataGridView dgv, List<string> columns)
        {

            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            int cont = 1;
            foreach (string name in columns)
            {
                xlWorkSheet.Cells[1, cont] = name.ToUpper();
                xlWorkSheet.Cells[1, cont].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbLightGreen;
                xlWorkSheet.Cells[1, cont].Font.Bold = true;
                xlWorkSheet.Cells[1, cont].Font.Size = 14;
                xlWorkSheet.Cells[1, cont].Font.Color = System.Drawing.Color.DarkGreen;
                cont++;
            }
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    if (columns.Contains(dgv.Columns[j].Name))
                    {
                        xlWorkSheet.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value.ToString();
                    }
                }
            }
            xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[dgv.RowCount, columns.Count]].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[dgv.RowCount, columns.Count]].Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;

            xlWorkSheet.Rows.RowHeight = 50;

            xlWorkSheet.Columns.AutoFit();
            //xlWorkSheet.Rows.AutoFit();
            xlApp.Visible = true;

            //usar esto si quiero salvarlo en alguna ruta
            //var saveFileDialoge = new SaveFileDialog();
            //saveFileDialoge.FileName = "ReportView";
            //saveFileDialoge.DefaultExt = ".xlsx";
            //if (saveFileDialoge.ShowDialog() == DialogResult.OK)
            //{
            //    xlWorkBook.SaveAs(saveFileDialoge.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //}
            //xlWorkBook.Close(true, misValue, misValue);
            //xlApp.Quit();
            //releaseObject(xlWorkSheet);
            //releaseObject(xlWorkBook);
            //releaseObject(xlApp);
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        #endregion

        #region Tab2

        private void cmbConstituency2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmb2Constituency.SelectedIndex > -1)
                {
                    FillUpdComboboxDivision2();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void FillUpdComboboxDivision2()
        {
            try
            {
                if (ConstituenciesList.Where(p => p.Id == (int)cmb2Constituency.SelectedValue).Select(p => p.PollingDivisions).Any())
                {
                    cmb2Division.BindingContext = new BindingContext();
                    cmb2Division.DataSource = null;
                    PollingDivisionsList = new List<PollingDivision>();
                    PollingDivisionsList = ConstituenciesList.Where(p => p.Id == (int)cmb2Constituency.SelectedValue).SelectMany(p => p.PollingDivisions).ToList();
                    cmb2Division.DataSource = PollingDivisionsList;
                    cmb2Division.ValueMember = "Id";
                    cmb2Division.DisplayMember = "Name";
                    cmb2Division.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void cmbCanvasType2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmb2CanvasTypes.SelectedIndex > -1)
                {
                    FillUpdComboboxCanvas2();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void FillUpdComboboxCanvas2()
        {
            try
            {
                if (CanvasTypeList.Where(p => p.Id == (int)cmb2CanvasTypes.SelectedValue).Select(p => p.Canvas).Any())
                {
                    cmb2Canvas.BindingContext = new BindingContext();
                    cmb2Canvas.DataSource = null;
                    CanvasList = new List<Canvas>();
                    CanvasList = CanvasTypeList.Where(p => p.Id == (int)cmb2CanvasTypes.SelectedValue).SelectMany(p => p.Canvas).ToList();
                    cmb2Canvas.DataSource = CanvasList;
                    cmb2Canvas.ValueMember = "Id";
                    cmb2Canvas.DisplayMember = "Name";
                    cmb2Canvas.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }

        private void ibtnUpdate_Click(object sender, EventArgs e)
        {
            var constituency = cmb2Constituency.SelectedItem as ConstituencyC;
            var division = cmb2Division.SelectedItem as PollingDivision;
            var canvas = cmb2Canvas.SelectedItem as Canvas;
            var party = cmb2Party.SelectedItem as Party;
            //todas las entrevistas en open canvas
            if (constituency == null && division == null && canvas == null && party == null)
            {
                FillUpDGV2("Voters/ByConstituencyDivisionsCanvasAndParty", 0, 0, 0, 0);
            }
            if (constituency == null && division == null && canvas == null && party != null)
            {
                FillUpDGV2("Voters/ByConstituencyDivisionsCanvasAndParty", 0, 0, 0, party.Id);
            }
            if (constituency != null && division == null && canvas == null && party != null)
            {
                FillUpDGV2("Voters/ByConstituencyDivisionsCanvasAndParty", constituency.Id, 0, 0, party.Id);
            }
            if (constituency != null && division != null && canvas == null && party != null)
            {
                FillUpDGV2("Voters/ByConstituencyDivisionsCanvasAndParty", constituency.Id, division.Id, 0, party.Id);
            }
            if (constituency != null && division != null && canvas != null && party != null)
            {
                FillUpDGV2("Voters/ByConstituencyDivisionsCanvasAndParty", constituency.Id, division.Id, canvas.Id, party.Id);
            }
            if (constituency != null && division == null && canvas != null && party == null)
            {
                FillUpDGV2("Voters/ByConstituencyDivisionsCanvasAndParty", constituency.Id, 0, canvas.Id, 0);
            }
            if (constituency != null && division == null && canvas != null && party != null)
            {
                FillUpDGV2("Voters/ByConstituencyDivisionsCanvasAndParty", constituency.Id, 0, canvas.Id, party.Id);
            }
            if (constituency == null && division == null && canvas != null && party != null)
            {
                FillUpDGV2("Voters/ByConstituencyDivisionsCanvasAndParty", 0, 0, canvas.Id, party.Id);
            }
            if (constituency != null && division == null && canvas == null && party == null)
            {
                FillUpDGV2("Voters/ByConstituencyDivisionsCanvasAndParty", constituency.Id, 0, 0, 0);
            }
            if (constituency != null && division != null && canvas == null && party == null)
            {
                FillUpDGV2("Voters/ByConstituencyDivisionsCanvasAndParty", constituency.Id, division.Id, 0, 0);
            }
            if (constituency == null && division == null && canvas != null && party == null)
            {
                FillUpDGV2("Voters/ByConstituencyDivisionsCanvasAndParty", 0, 0, canvas.Id, 0);
            }

        }
        private async Task<List<Voter>> LoadCanvasReport2(string controller, int constituencyId, int divisionId, int canvasId, int partyId)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.GetListAsyncReportsVotersByCanvasParties<Voter>(controller, constituencyId, divisionId, canvasId, partyId, token);
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
        private async void FillUpDGV2(string route, int constituencyId, int divisionId, int canvasId, int partyId)
        {
            dgv2.DataSource = null;
            var result = await LoadCanvasReport2(route, constituencyId, divisionId, canvasId, partyId);
            dgv2.DataSource = result.Select(v => new
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
            lblTotal2.Text = result.Count.ToString();
        }




        #endregion

        private void rjToggleButton1_CheckedChanged(object sender, EventArgs e)
        {
            cmb2Party.Enabled = rjToggleButton1.Checked;
            cmb2Party.SelectedItem = rjToggleButton1.Checked ? cmb2Party.SelectedItem : null;
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            if (!await ValidateAccess(((IconButton)sender).Tag.ToString()))
            {
                UtilRecurrent.InformationMessage("You have not access to this feature in this Application.\r\n Please Contact your System Administrator to request the access", "User Access");
                return;
            }
            dgv2.MultiSelect = true;
            UtilRecurrent.ExportToExcel(dgv2);
            dgv2.MultiSelect = false;
            dgv2.CurrentCell = null;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            List<string> columns = chkColumnsExport.CheckedItems.OfType<object>().Select(li => li.ToString()).ToList();
            if (columns.Count == 0)
            {
                UtilRecurrent.InformationMessage("Please select at least one column to export", "Export");
                return;
            }
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                ImportDataGridViewDataToExcelSheet(dgv1Interviews, columns);
                UtilRecurrent.UnlockForm(waitForm, this);
            }
            catch (Exception ex)
            {
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);               
            }
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            gbReport.Visible = false;
        }
    }
}
