using Constituency.Desktop.Components;
using Constituency.Desktop.Entities;
using Constituency.Desktop.Helpers;
using Constituency.Desktop.Models;
using Microsoft.AppCenter.Crashes;
using System.Collections.ObjectModel;
using System.Data;


namespace Constituency.Desktop.Views
{
    public partial class Frm_Votes : Form
    {
        //Fields
        private string token = string.Empty;
        private ObservableCollection<NationalElection> ElectionList;

        WaitFormFunc waitForm = new WaitFormFunc();

        private List<Voter> VoterList;

        int userVotes = 0;

        //objects
        User user { get; set; }
        ElectionVote ElectionVote { get; set; }
        Voter Voter { get; set; }

        private List<ConstituencyC> ConstituenciesList;

        private List<PollingDivision> PollingDivisionsList;

        public Frm_Votes()
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            InitializeComponent();
            token = Main.GetInstance().tokenResponse.Token;
            user = Main.GetInstance().tokenResponse.User;
        }

        private async void Frm_Reports_Load(object sender, EventArgs e)
        {
            UtilRecurrent.LockForm(waitForm, this);
            await LoadElections();
            await LoadUserVotes();
            await LoadConstituencies();
            UtilRecurrent.UnlockForm(waitForm, this);
            lblUser.Text = user.FullName + $" / Saved Votes: [{userVotes}]";
            DGVFormats();
        }
        private async Task LoadElections()
        {
            try
            {
                Response response = await ApiServices.GetListAsync<NationalElection>("NationalElections/Open", token);

                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return;
                }
                ElectionList = new ObservableCollection<NationalElection>((List<NationalElection>)response.Result);
                if (ElectionList != null && ElectionList.Any())
                {
                    lblElection.Text = ElectionList.Last().ElectionDate.ToString("dd-MMMM-yyyy");
                }
                else
                {
                    UtilRecurrent.ErrorMessage("No elections were loaded. Contact your IT Personnel");
                }
            }
            catch (Exception ex)
            {
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
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
            }
        }
        private async Task LoadUserVotes()
        {
            try
            {
                Response response = await ApiServices.FindAsync<int>("ElectionVotes/ByUser", user.Id.ToString(), token);

                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return;
                }
                userVotes = (int)response.Result;
            }
            catch (Exception ex)
            {
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }

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
        private async void cmbDivision_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int divisionId = (cmbDivision.SelectedItem as PollingDivision).Id;
            var result = await LoadVotersInDivision(divisionId);
            FillUpDGV1(result);

        }

        private async void radioButton1_Click(object sender, EventArgs e)
        {
            //if (cmbDivision.SelectedItem == null)
            //{
            //    UtilRecurrent.ErrorMessage("Division is required.");
            //    return;
            //}
            //int divisionId = (cmbDivision.SelectedItem as PollingDivision).Id;
            //if (rbtnInterviewed.Checked)
            //{
            //    FillUpDGV1("Voters/ByDivisionsAndAllOpenCanvas", divisionId, 0, true);
            //}
            //if (rbtnNotInterviewed.Checked)
            //{
            //    FillUpDGV1("Voters/ByDivisionsAndAllOpenCanvas", divisionId, 0, false);
            //}
        }
        private void FillUpDGV1(List<Voter> result)
        {
            dgv1Interviews.DataSource = null;
            dgv1Interviews.DataSource = result.Select(v => new
            {
                Voted = v.ElectionVotes.Count == 1,
                v.Reg,
                v.SurName,
                v.GivenNames,
                v.Sex,
                v.Address,
                v.Occupation,
                PD = v.PollingDivision.Name,
            }).OrderBy(v => v.SurName).ThenBy(v => v.GivenNames).ToList();
            lblTotal1.Text = result.Count.ToString();
            dgv1Interviews.CurrentCell = null;
        }

        private async Task<List<Voter>> LoadVotersInDivision(int divisionId)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.GetListAsync<Voter>("Voters/InDivisions", divisionId.ToString(), ElectionList.Last().Id.ToString(), token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return null;
                }
                VoterList = new List<Voter>((List<Voter>)response.Result);
                if (VoterList.Any())
                {
                    return VoterList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return null;
            }
        }

        private async Task<bool> SaveVote(string reg)
        {
            try
            {
                var electionVote = BuildElectionVote(reg);
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PostAsync("ElectionVotes", ElectionVote, token);
                UtilRecurrent.UnlockForm(waitForm, this);

                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response.IsSuccess;
                }
                userVotes++;
                VoterList.FirstOrDefault(v => v.Reg == reg).ElectionVotes.Add((ElectionVote)response.Result);
                lblUser.Text = user.FullName + $" Saved Votes: [{userVotes}]";
                return true;
            }
            catch (Exception ex)
            {
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }
        }
        private ElectionVote BuildElectionVote(string reg)
        {
            try
            {
                ElectionVote = new();
                ElectionVote.Election = ElectionList.Last();
                ElectionVote.VoteTime = DateTime.Now;
                ElectionVote.Voter = VoterList.FirstOrDefault(x => x.Reg == reg);
                ElectionVote.RecorderBy = user;
                return ElectionVote;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return null;
            }
        }


        private void timer1_Tick_1(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        }        
        private async void dgv1Interviews_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 | e.ColumnIndex < 0)
                {
                    return;
                }
                var senderGrid = (DataGridView)sender;
                
                string reg = senderGrid.Rows[e.RowIndex].Cells["Reg"].Value.ToString();

                int cell = dgv1Interviews.FirstDisplayedScrollingRowIndex;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                {
                    if ((bool)senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == false)
                    {
                        if (await SaveVote(reg))
                        {
                            if (!filter)
                            {
                                FillUpDGV1(VoterList);
                                senderGrid.FirstDisplayedScrollingRowIndex = cell;
                                txtReg.Clear();
                            }
                            else
                            {
                                FillUpDGV1(VoterList.Where(v => v.Reg.Contains(txtReg.Text)).ToList());
                            }
                        }
                    }
                    else
                    {
                        var vote = VoterList.FirstOrDefault(v => v.Reg == reg).ElectionVotes.LastOrDefault();

                        if (!await ValidateAccess("Review_Modify_Votes") && vote.RecorderBy.Id != user.Id)
                        {
                            UtilRecurrent.InformationMessage("You have not access to modify others users entered votes.\r\n Please Contact your System Administrator to request the access", "User Access");
                            return;
                        }                       
                        if (UtilRecurrent.yesOrNot("Are you sure you want to delete this vote?", "Delete Vote"))
                        {
                          
                            if (await DeleteVote(vote.Id, reg))
                            {
                                if (!filter)
                                {
                                    FillUpDGV1(VoterList);
                                    senderGrid.FirstDisplayedScrollingRowIndex = cell;
                                    txtReg.Clear();
                                }
                                else
                                {
                                    FillUpDGV1(VoterList.Where(v => v.Reg.Contains(txtReg.Text)).ToList());
                                }
                            }
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
        private async Task<bool> DeleteVote(int id, string reg)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.DeleteAsync("ElectionVotes", id, token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response.IsSuccess;
                }
                userVotes--;
                VoterList.FirstOrDefault(v => v.Reg == reg).ElectionVotes.Remove(VoterList.FirstOrDefault(v => v.Reg == reg).ElectionVotes.LastOrDefault());
                lblUser.Text = user.FullName + $" Saved Votes: [{userVotes}]";
                return true;
            }
            catch (Exception ex)
            {
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
                return false;
            }
        }

        private void ibtnUpdate_Click(object sender, EventArgs e)
        {
            Frm_SaveVote frm = new Frm_SaveVote();
            frm.ShowDialog();
        }
        bool filter = false;
        private void txtReg_KeyUp(object sender, KeyEventArgs e)
        {
            var txt = (TextBox)sender;
            if (txt.TextLength >= 3 && VoterList != null && VoterList.Any())
            {
                FillUpDGV1(VoterList.Where(v => v.Reg.Contains(txt.Text)).ToList());
                filter = true;
            }
            else if (filter)
            {
                FillUpDGV1(VoterList);
                filter = false;
                txt.Clear();
            }
        }
    }
}
