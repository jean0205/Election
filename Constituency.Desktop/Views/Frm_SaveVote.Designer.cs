namespace Constituency.Desktop.Views
{
    partial class Frm_SaveVote
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label30 = new System.Windows.Forms.Label();
            this.txtReg = new System.Windows.Forms.TextBox();
            this.ibtnUpdate = new FontAwesome.Sharp.IconButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSurName = new System.Windows.Forms.TextBox();
            this.txtGivenNames = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtOccupation = new System.Windows.Forms.TextBox();
            this.txtConstituency = new System.Windows.Forms.TextBox();
            this.txtDivision = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label19 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.ibtnSave = new FontAwesome.Sharp.IconButton();
            this.lblElection = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.66666F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 176F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(893, 485);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel6.Controls.Add(this.label30, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.txtReg, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.ibtnUpdate, 2, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(887, 53);
            this.tableLayoutPanel6.TabIndex = 5;
            // 
            // label30
            // 
            this.label30.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label30.Location = new System.Drawing.Point(133, 16);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(159, 21);
            this.label30.TabIndex = 16;
            this.label30.Text = "Registration Number:";
            // 
            // txtReg
            // 
            this.txtReg.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtReg.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtReg.Location = new System.Drawing.Point(299, 12);
            this.txtReg.Name = "txtReg";
            this.txtReg.Size = new System.Drawing.Size(268, 29);
            this.txtReg.TabIndex = 17;
            this.txtReg.Tag = "1,2";
            this.txtReg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReg_KeyPress);
            // 
            // ibtnUpdate
            // 
            this.ibtnUpdate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ibtnUpdate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ibtnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ibtnUpdate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(244)))), ((int)(((byte)(197)))));
            this.ibtnUpdate.FlatAppearance.BorderSize = 2;
            this.ibtnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ibtnUpdate.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ibtnUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(33)))), ((int)(((byte)(53)))));
            this.ibtnUpdate.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.ibtnUpdate.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(33)))), ((int)(((byte)(53)))));
            this.ibtnUpdate.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ibtnUpdate.IconSize = 33;
            this.ibtnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ibtnUpdate.Location = new System.Drawing.Point(756, 6);
            this.ibtnUpdate.Name = "ibtnUpdate";
            this.ibtnUpdate.Size = new System.Drawing.Size(127, 41);
            this.ibtnUpdate.TabIndex = 18;
            this.ibtnUpdate.Text = "Find";
            this.ibtnUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ibtnUpdate.UseVisualStyleBackColor = false;
            this.ibtnUpdate.Click += new System.EventHandler(this.ibtnUpdate_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.86456F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.13544F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label14, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label15, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.txtSurName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtGivenNames, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtAddress, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtOccupation, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtConstituency, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.txtDivision, 1, 5);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 62);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(887, 243);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(94, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 21);
            this.label1.TabIndex = 19;
            this.label1.Text = "SurName:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(67, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 21);
            this.label2.TabIndex = 20;
            this.label2.Text = "Given Names:";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(81, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 21);
            this.label7.TabIndex = 21;
            this.label7.Text = "Occupation:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(104, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 21);
            this.label6.TabIndex = 22;
            this.label6.Text = "Address:";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label14.Location = new System.Drawing.Point(70, 170);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(103, 21);
            this.label14.TabIndex = 23;
            this.label14.Text = "Constituency:";
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label15.Location = new System.Drawing.Point(104, 211);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(69, 21);
            this.label15.TabIndex = 24;
            this.label15.Text = "Division:";
            // 
            // txtSurName
            // 
            this.txtSurName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSurName.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtSurName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSurName.Location = new System.Drawing.Point(180, 6);
            this.txtSurName.Name = "txtSurName";
            this.txtSurName.ReadOnly = true;
            this.txtSurName.Size = new System.Drawing.Size(421, 29);
            this.txtSurName.TabIndex = 25;
            this.txtSurName.Tag = "1,3";
            // 
            // txtGivenNames
            // 
            this.txtGivenNames.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtGivenNames.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtGivenNames.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtGivenNames.Location = new System.Drawing.Point(180, 46);
            this.txtGivenNames.Name = "txtGivenNames";
            this.txtGivenNames.ReadOnly = true;
            this.txtGivenNames.Size = new System.Drawing.Size(421, 29);
            this.txtGivenNames.TabIndex = 26;
            this.txtGivenNames.Tag = "1,3";
            // 
            // txtAddress
            // 
            this.txtAddress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtAddress.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtAddress.Location = new System.Drawing.Point(180, 126);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ReadOnly = true;
            this.txtAddress.Size = new System.Drawing.Size(703, 29);
            this.txtAddress.TabIndex = 27;
            this.txtAddress.Tag = "";
            // 
            // txtOccupation
            // 
            this.txtOccupation.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtOccupation.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtOccupation.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtOccupation.Location = new System.Drawing.Point(180, 86);
            this.txtOccupation.Name = "txtOccupation";
            this.txtOccupation.ReadOnly = true;
            this.txtOccupation.Size = new System.Drawing.Size(703, 29);
            this.txtOccupation.TabIndex = 28;
            this.txtOccupation.Tag = "";
            // 
            // txtConstituency
            // 
            this.txtConstituency.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtConstituency.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtConstituency.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtConstituency.Location = new System.Drawing.Point(180, 166);
            this.txtConstituency.Name = "txtConstituency";
            this.txtConstituency.ReadOnly = true;
            this.txtConstituency.Size = new System.Drawing.Size(421, 29);
            this.txtConstituency.TabIndex = 29;
            this.txtConstituency.Tag = "1,3";
            // 
            // txtDivision
            // 
            this.txtDivision.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtDivision.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtDivision.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtDivision.Location = new System.Drawing.Point(180, 207);
            this.txtDivision.Name = "txtDivision";
            this.txtDivision.ReadOnly = true;
            this.txtDivision.Size = new System.Drawing.Size(97, 29);
            this.txtDivision.TabIndex = 30;
            this.txtDivision.Tag = "1,3";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.72943F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.27058F));
            this.tableLayoutPanel3.Controls.Add(this.label19, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label18, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.lblUser, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.ibtnSave, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.lblElection, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblDateTime, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 311);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(887, 171);
            this.tableLayoutPanel3.TabIndex = 7;
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label19.Location = new System.Drawing.Point(105, 10);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(67, 21);
            this.label19.TabIndex = 37;
            this.label19.Text = "Election:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(24, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 21);
            this.label5.TabIndex = 38;
            this.label5.Text = "Vote Date and Time:";
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label18.Location = new System.Drawing.Point(127, 94);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(45, 21);
            this.label18.TabIndex = 41;
            this.label18.Text = "User:";
            // 
            // lblUser
            // 
            this.lblUser.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblUser.Location = new System.Drawing.Point(178, 94);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(73, 21);
            this.lblUser.TabIndex = 42;
            this.lblUser.Text = "_________";
            // 
            // ibtnSave
            // 
            this.ibtnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ibtnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ibtnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(159)))), ((int)(((byte)(148)))));
            this.ibtnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(244)))), ((int)(((byte)(197)))));
            this.ibtnSave.FlatAppearance.BorderSize = 2;
            this.ibtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ibtnSave.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ibtnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(33)))), ((int)(((byte)(53)))));
            this.ibtnSave.IconChar = FontAwesome.Sharp.IconChar.CloudUploadAlt;
            this.ibtnSave.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(33)))), ((int)(((byte)(53)))));
            this.ibtnSave.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ibtnSave.IconSize = 33;
            this.ibtnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ibtnSave.Location = new System.Drawing.Point(729, 129);
            this.ibtnSave.Name = "ibtnSave";
            this.ibtnSave.Size = new System.Drawing.Size(155, 39);
            this.ibtnSave.TabIndex = 43;
            this.ibtnSave.Text = "Voted";
            this.ibtnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ibtnSave.UseVisualStyleBackColor = false;
            this.ibtnSave.Click += new System.EventHandler(this.ibtnSave_Click);
            // 
            // lblElection
            // 
            this.lblElection.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblElection.AutoSize = true;
            this.lblElection.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblElection.Location = new System.Drawing.Point(178, 10);
            this.lblElection.Name = "lblElection";
            this.lblElection.Size = new System.Drawing.Size(73, 21);
            this.lblElection.TabIndex = 44;
            this.lblElection.Text = "_________";
            // 
            // lblDateTime
            // 
            this.lblDateTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDateTime.Location = new System.Drawing.Point(178, 52);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(73, 21);
            this.lblDateTime.TabIndex = 45;
            this.lblDateTime.Text = "_________";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Frm_SaveVote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(70)))), ((int)(((byte)(53)))));
            this.ClientSize = new System.Drawing.Size(893, 485);
            this.Controls.Add(this.tableLayoutPanel1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Frm_SaveVote";
            this.Text = "Save Vote";
            this.Load += new System.EventHandler(this.Frm_SaveVote_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel6;
        private Label label30;
        private TextBox txtReg;
        private FontAwesome.Sharp.IconButton ibtnUpdate;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label1;
        private Label label2;
        private Label label7;
        private Label label6;
        private Label label14;
        private Label label15;
        private TextBox txtSurName;
        private TextBox txtGivenNames;
        private TextBox txtAddress;
        private TextBox txtOccupation;
        private TextBox txtConstituency;
        private TextBox txtDivision;
        private Label label19;
        private Label label5;
        private Label label18;
        private Label lblUser;
        private FontAwesome.Sharp.IconButton ibtnSave;
        private Label lblElection;
        private Label lblDateTime;
        private System.Windows.Forms.Timer timer1;
    }
}