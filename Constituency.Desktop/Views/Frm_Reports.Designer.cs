namespace Constituency.Desktop.Views
{
    partial class Frm_Reports
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rbtnNotInterviewed = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.rbtnInterviewed = new System.Windows.Forms.RadioButton();
            this.cmbConstituency = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.rjCanvasActive = new Constituency.Desktop.Controls.RJToggleButton();
            this.tPanelCanvas = new System.Windows.Forms.TableLayoutPanel();
            this.cmbCanvas = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cmbCanvasTypes = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotal1 = new System.Windows.Forms.Label();
            this.ibtnRefresh = new FontAwesome.Sharp.IconButton();
            this.dgv1Interviews = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tPanelCanvas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1Interviews)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1411, 811);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(70)))), ((int)(((byte)(53)))));
            this.tabPage1.Controls.Add(this.tableLayoutPanel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1403, 777);
            this.tabPage1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dgv1Interviews, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 127F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1395, 769);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1389, 121);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Interviews By Divisions:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.48875F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.71382F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.82637F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.97106F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 722F));
            this.tableLayoutPanel1.Controls.Add(this.rbtnNotInterviewed, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rbtnInterviewed, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbConstituency, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label15, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbDivision, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTotal1, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.ibtnRefresh, 4, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1383, 93);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // rbtnNotInterviewed
            // 
            this.rbtnNotInterviewed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbtnNotInterviewed.AutoSize = true;
            this.rbtnNotInterviewed.Location = new System.Drawing.Point(126, 60);
            this.rbtnNotInterviewed.Name = "rbtnNotInterviewed";
            this.rbtnNotInterviewed.Size = new System.Drawing.Size(139, 25);
            this.rbtnNotInterviewed.TabIndex = 48;
            this.rbtnNotInterviewed.TabStop = true;
            this.rbtnNotInterviewed.Text = "Not Interviewed";
            this.rbtnNotInterviewed.UseVisualStyleBackColor = true;
            this.rbtnNotInterviewed.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label14.Location = new System.Drawing.Point(16, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(103, 21);
            this.label14.TabIndex = 35;
            this.label14.Text = "Constituency:";
            // 
            // rbtnInterviewed
            // 
            this.rbtnInterviewed.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.rbtnInterviewed.AutoSize = true;
            this.rbtnInterviewed.Location = new System.Drawing.Point(10, 60);
            this.rbtnInterviewed.Name = "rbtnInterviewed";
            this.rbtnInterviewed.Size = new System.Drawing.Size(109, 25);
            this.rbtnInterviewed.TabIndex = 1;
            this.rbtnInterviewed.TabStop = true;
            this.rbtnInterviewed.Text = "Interviewed";
            this.rbtnInterviewed.UseVisualStyleBackColor = true;
            this.rbtnInterviewed.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // cmbConstituency
            // 
            this.cmbConstituency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbConstituency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConstituency.FormattingEnabled = true;
            this.cmbConstituency.Location = new System.Drawing.Point(126, 15);
            this.cmbConstituency.Name = "cmbConstituency";
            this.cmbConstituency.Size = new System.Drawing.Size(313, 29);
            this.cmbConstituency.TabIndex = 37;
            this.cmbConstituency.Tag = "1";
            this.cmbConstituency.SelectionChangeCommitted += new System.EventHandler(this.cmbConstituency_SelectionChangeCommitted);
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label15.Location = new System.Drawing.Point(461, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(69, 21);
            this.label15.TabIndex = 36;
            this.label15.Text = "Division:";
            // 
            // cmbDivision
            // 
            this.cmbDivision.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(537, 15);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(118, 29);
            this.cmbDivision.TabIndex = 38;
            this.cmbDivision.Tag = "1";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.41085F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.58915F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 558F));
            this.tableLayoutPanel4.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.rjCanvasActive, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tPanelCanvas, 2, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(662, 4);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(717, 45);
            this.tableLayoutPanel4.TabIndex = 50;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(72, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 42);
            this.label1.TabIndex = 51;
            this.label1.Text = "Filter By Canvas";
            // 
            // rjCanvasActive
            // 
            this.rjCanvasActive.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rjCanvasActive.AutoSize = true;
            this.rjCanvasActive.Location = new System.Drawing.Point(4, 10);
            this.rjCanvasActive.MinimumSize = new System.Drawing.Size(50, 25);
            this.rjCanvasActive.Name = "rjCanvasActive";
            this.rjCanvasActive.OffBackColor = System.Drawing.Color.Gray;
            this.rjCanvasActive.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.rjCanvasActive.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.rjCanvasActive.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.rjCanvasActive.Size = new System.Drawing.Size(50, 25);
            this.rjCanvasActive.TabIndex = 50;
            this.rjCanvasActive.UseVisualStyleBackColor = true;
            this.rjCanvasActive.CheckedChanged += new System.EventHandler(this.rjCanvasActive_CheckedChanged);
            // 
            // tPanelCanvas
            // 
            this.tPanelCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tPanelCanvas.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tPanelCanvas.ColumnCount = 4;
            this.tPanelCanvas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.88065F));
            this.tPanelCanvas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.68897F));
            this.tPanelCanvas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.92405F));
            this.tPanelCanvas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.50633F));
            this.tPanelCanvas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tPanelCanvas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tPanelCanvas.Controls.Add(this.cmbCanvas, 3, 0);
            this.tPanelCanvas.Controls.Add(this.label20, 2, 0);
            this.tPanelCanvas.Controls.Add(this.cmbCanvasTypes, 1, 0);
            this.tPanelCanvas.Controls.Add(this.label18, 0, 0);
            this.tPanelCanvas.Enabled = false;
            this.tPanelCanvas.Location = new System.Drawing.Point(160, 4);
            this.tPanelCanvas.Name = "tPanelCanvas";
            this.tPanelCanvas.RowCount = 1;
            this.tPanelCanvas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tPanelCanvas.Size = new System.Drawing.Size(553, 37);
            this.tPanelCanvas.TabIndex = 49;
            // 
            // cmbCanvas
            // 
            this.cmbCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCanvas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCanvas.FormattingEnabled = true;
            this.cmbCanvas.Location = new System.Drawing.Point(331, 7);
            this.cmbCanvas.Name = "cmbCanvas";
            this.cmbCanvas.Size = new System.Drawing.Size(218, 29);
            this.cmbCanvas.TabIndex = 40;
            this.cmbCanvas.Tag = "1";
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label20.Location = new System.Drawing.Point(261, 8);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(63, 21);
            this.label20.TabIndex = 39;
            this.label20.Text = "Canvas:";
            // 
            // cmbCanvasTypes
            // 
            this.cmbCanvasTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCanvasTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCanvasTypes.FormattingEnabled = true;
            this.cmbCanvasTypes.Location = new System.Drawing.Point(124, 7);
            this.cmbCanvasTypes.Name = "cmbCanvasTypes";
            this.cmbCanvasTypes.Size = new System.Drawing.Size(123, 29);
            this.cmbCanvasTypes.TabIndex = 47;
            this.cmbCanvasTypes.Tag = "1";
            this.cmbCanvasTypes.SelectionChangeCommitted += new System.EventHandler(this.cmbCanvasType_SelectionChangeCommitted);
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label18.Location = new System.Drawing.Point(11, 8);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(106, 21);
            this.label18.TabIndex = 46;
            this.label18.Text = "Canvas Types:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(485, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 21);
            this.label2.TabIndex = 51;
            this.label2.Text = "Total:";
            // 
            // lblTotal1
            // 
            this.lblTotal1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTotal1.AutoSize = true;
            this.lblTotal1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTotal1.Location = new System.Drawing.Point(537, 62);
            this.lblTotal1.Name = "lblTotal1";
            this.lblTotal1.Size = new System.Drawing.Size(31, 21);
            this.lblTotal1.TabIndex = 52;
            this.lblTotal1.Text = "___";
            // 
            // ibtnRefresh
            // 
            this.ibtnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ibtnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ibtnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(214)))), ((int)(((byte)(98)))));
            this.ibtnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ibtnRefresh.FlatAppearance.BorderSize = 2;
            this.ibtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ibtnRefresh.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ibtnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(33)))), ((int)(((byte)(53)))));
            this.ibtnRefresh.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            this.ibtnRefresh.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(33)))), ((int)(((byte)(53)))));
            this.ibtnRefresh.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ibtnRefresh.IconSize = 28;
            this.ibtnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ibtnRefresh.Location = new System.Drawing.Point(1248, 56);
            this.ibtnRefresh.Name = "ibtnRefresh";
            this.ibtnRefresh.Size = new System.Drawing.Size(131, 33);
            this.ibtnRefresh.TabIndex = 53;
            this.ibtnRefresh.Tag = "Export_Reports";
            this.ibtnRefresh.Text = "Export";
            this.ibtnRefresh.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ibtnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ibtnRefresh.UseVisualStyleBackColor = false;
            this.ibtnRefresh.Click += new System.EventHandler(this.ibtnRefresh_Click);
            // 
            // dgv1Interviews
            // 
            this.dgv1Interviews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1Interviews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv1Interviews.Location = new System.Drawing.Point(3, 130);
            this.dgv1Interviews.Name = "dgv1Interviews";
            this.dgv1Interviews.RowTemplate.Height = 25;
            this.dgv1Interviews.Size = new System.Drawing.Size(1389, 636);
            this.dgv1Interviews.TabIndex = 1;
            // 
            // Frm_Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(70)))), ((int)(((byte)(53)))));
            this.ClientSize = new System.Drawing.Size(1411, 811);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Frm_Reports";
            this.Text = "Frm_Reports";
            this.Load += new System.EventHandler(this.Frm_Reports_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tPanelCanvas.ResumeLayout(false);
            this.tPanelCanvas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1Interviews)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TableLayoutPanel tableLayoutPanel2;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label14;
        private ComboBox cmbConstituency;
        private Label label15;
        private ComboBox cmbDivision;
        private Label label20;
        private ComboBox cmbCanvas;
        private ComboBox cmbCanvasTypes;
        private Label label18;
        private RadioButton rbtnNotInterviewed;
        private RadioButton rbtnInterviewed;
        private DataGridView dgv1Interviews;
        private TableLayoutPanel tPanelCanvas;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label1;
        private Controls.RJToggleButton rjCanvasActive;
        private Label label2;
        private Label lblTotal1;
        private FontAwesome.Sharp.IconButton ibtnRefresh;
    }
}