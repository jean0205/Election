using Constituency.Desktop.Entities;
using Constituency.Desktop.Helpers;
using Constituency.Desktop.Models;
using Microsoft.AppCenter.Crashes;
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
    public partial class Frm_Reports : Form
    {
        //Fields
        private string token = string.Empty;

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

        }

        #region Tab1
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
            CanvasTypeList = new ((List<CanvasType>)response.Result);
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
        private void radioButton1_Click(object sender, EventArgs e)
        {

        }
        #endregion


    }
}
