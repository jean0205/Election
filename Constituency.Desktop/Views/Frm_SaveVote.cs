using Constituency.Desktop.Components;
using Constituency.Desktop.Entities;
using Constituency.Desktop.Helpers;
using Constituency.Desktop.Models;
using Microsoft.AppCenter.Crashes;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Constituency.Desktop.Views
{
    public partial class Frm_SaveVote : Form
    {
        //Fields
        private string token = string.Empty;
        User user { get; set; }
        Voter Voter { get; set; }

        private ObservableCollection<NationalElection> ElectionList;
        
        WaitFormFunc waitForm = new WaitFormFunc();
        private List<Voter> VoterList;
        ElectionVote ElectionVote { get; set; }
        int userVotes = 0;
        public Frm_SaveVote()
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            InitializeComponent();
            token = Main.GetInstance().tokenResponse.Token;
            user = Main.GetInstance().tokenResponse.User;
        }

        private async void Frm_SaveVote_Load(object sender, EventArgs e)
        {           
            UtilRecurrent.LockForm(waitForm, this);
            await LoadElections();
            await LoadVoters();
            await LoadUserVotes();
            UtilRecurrent.UnlockForm(waitForm, this);
            lblUser.Text = user.FullName+ $" / Saved Votes: [{userVotes}]";
        }
        private async Task LoadVoters()
        {
            try
            {
             
                Response response = await ApiServices.GetListAsync<Voter>("Voters/NotTrack", token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return;
                }
                VoterList = new((List<Voter>)response.Result);
                if (!VoterList.Any())
                {
                    UtilRecurrent.ErrorMessage("No voters were loaded. Contact your IT Personnel");
                }
            }
            catch (Exception ex)
            {
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
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
        private async Task LoadUserVotes()
        {
            try
            {
                Response response = await ApiServices.FindAsync<int>("ElectionVotes/ByUser",user.Id.ToString(), token);

                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return;
                }
                userVotes =(int)response.Result;                
            }
            catch (Exception ex)
            {
                UtilRecurrent.UnlockForm(waitForm, this);
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private async Task LoadVotersInDivision(int divisionId)
        {
            try
            {
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.GetListAsync<Voter>("Voters/InDivisions", divisionId.ToString(), token);
                UtilRecurrent.UnlockForm(waitForm, this);
                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return;
                }
                
                VoterList = new List<Voter>((List<Voter>)response.Result);                
                if (VoterList.Any())
                {
                   
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        }
        private async void txtReg_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13 && ((TextBox)sender).TextLength > 0)
                {
                    var vot = VoterList.FirstOrDefault(x => x.Reg == txtReg.Text);

                    if (vot != null)
                    {                       
                        Voter = vot;
                        ShowVoterInformation();
                    }
                    else
                    {
                        UtilRecurrent.InformationMessage("Voter not found.", "Not Found");
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
        private void ShowVoterInformation()
        {
            try
            {
                if (Voter.Id > 0)
                {
                    List<PropertyInfo> properties = Voter.GetType().GetProperties().ToList();
                    List<TextBox> VoterTextBox = UtilRecurrent.FindAllTextBoxIterative(tableLayoutPanel2);
                    foreach (TextBox txt in VoterTextBox)
                    {
                        if (properties.Where(p => p.GetValue(Voter) != null && p.Name == txt.Name.Replace("txt", string.Empty)).Any())
                        {
                            txt.Text = properties.Where(p => p.GetValue(Voter) != null && p.Name == txt.Name.Replace("txt", string.Empty)).First().GetValue(Voter).ToString();
                        }
                    }
                    txtDivision.Text = Voter.PollingDivision.Name;
                    txtConstituency.Text = Voter.PollingDivision.Constituency.Name;
                    ibtnSave.Visible = true;
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
            try
            {
                var vot = VoterList.FirstOrDefault(x => x.Reg == txtReg.Text);

                if (vot != null)
                {
                    Voter = vot;
                    ShowVoterInformation();
                }
                else
                {
                    Voter = new();
                    UtilRecurrent.InformationMessage("Voter not found.","Not Found");
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }

        private async void ibtnSave_Click(object sender, EventArgs e)
        {
            if (Voter.Id>0)
            {
                await SaveVote();
            }
            else
            {
                UtilRecurrent.InformationMessage("A valid voter must be loaded before save the vote.", "Not Voter");
            }

        }
        private async Task<bool> SaveVote()
        {
            try
            {
                var electionVote = await BuildElectionVote();
                UtilRecurrent.LockForm(waitForm, this);
                Response response = await ApiServices.PostAsync("ElectionVotes", ElectionVote, token);
                UtilRecurrent.UnlockForm(waitForm, this);

                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return response.IsSuccess;
                }
                userVotes++;
                cleanScreen();
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
        private async Task<ElectionVote> BuildElectionVote()
        {
            try
            {
                ElectionVote = new();
                ElectionVote.Election = ElectionList.Last();
                ElectionVote.VoteTime = DateTime.Now;
                ElectionVote.Voter = Voter;                
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
        private void cleanScreen()
        {
            try
            {
                UtilRecurrent.FindAllControlsIterative(this.tableLayoutPanel2, "TextBox").Cast<TextBox>().ToList().ForEach(x => x.Clear());
                txtReg.Clear();
                Voter = new Voter();
                ibtnSave.Visible = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                UtilRecurrent.ErrorMessage(ex.Message);
            }
        }
    }
}
