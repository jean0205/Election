namespace Constituency.Desktop
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.ibtnUpdate = new FontAwesome.Sharp.IconButton();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.label23 = new System.Windows.Forms.Label();
            this.rjFConstituency = new Constituency.Desktop.Controls.RJToggleButton();
            this.tableLayoutPanel16 = new System.Windows.Forms.TableLayoutPanel();
            this.label16 = new System.Windows.Forms.Label();
            this.cmb2Division = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cmb2Contituency = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.label19 = new System.Windows.Forms.Label();
            this.rjFCanvas = new Constituency.Desktop.Controls.RJToggleButton();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.cmb2Canvas = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.Cmb2CanvasType = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cmb2Party = new System.Windows.Forms.ComboBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel11.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            this.tableLayoutPanel16.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 1;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Controls.Add(this.groupBox4, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.dataGridView2, 0, 1);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel11.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 2;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 167F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(1368, 810);
            this.tableLayoutPanel11.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel12);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1362, 161);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Supported Parties In Open Canvas:";
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 2;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanel12.Controls.Add(this.ibtnUpdate, 1, 1);
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel15, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel13, 0, 1);
            this.tableLayoutPanel12.Controls.Add(this.groupBox5, 1, 0);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 2;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(1356, 139);
            this.tableLayoutPanel12.TabIndex = 0;
            // 
            // ibtnUpdate
            // 
            this.ibtnUpdate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ibtnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ibtnUpdate.Dock = System.Windows.Forms.DockStyle.Right;
            this.ibtnUpdate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(244)))), ((int)(((byte)(197)))));
            this.ibtnUpdate.FlatAppearance.BorderSize = 2;
            this.ibtnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ibtnUpdate.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ibtnUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(33)))), ((int)(((byte)(53)))));
            this.ibtnUpdate.IconChar = FontAwesome.Sharp.IconChar.CloudUploadAlt;
            this.ibtnUpdate.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(33)))), ((int)(((byte)(53)))));
            this.ibtnUpdate.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ibtnUpdate.IconSize = 33;
            this.ibtnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ibtnUpdate.Location = new System.Drawing.Point(1198, 74);
            this.ibtnUpdate.Name = "ibtnUpdate";
            this.ibtnUpdate.Size = new System.Drawing.Size(155, 62);
            this.ibtnUpdate.TabIndex = 56;
            this.ibtnUpdate.Text = "Update";
            this.ibtnUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ibtnUpdate.UseVisualStyleBackColor = false;
            this.ibtnUpdate.Visible = false;
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel15.ColumnCount = 3;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.64286F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.35714F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 959F));
            this.tableLayoutPanel15.Controls.Add(this.label23, 1, 0);
            this.tableLayoutPanel15.Controls.Add(this.rjFConstituency, 0, 0);
            this.tableLayoutPanel15.Controls.Add(this.tableLayoutPanel16, 2, 0);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 1;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(1165, 65);
            this.tableLayoutPanel15.TabIndex = 54;
            // 
            // label23
            // 
            this.label23.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label23.Location = new System.Drawing.Point(54, 11);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(100, 42);
            this.label23.TabIndex = 51;
            this.label23.Text = "Filter By Constituency";
            // 
            // rjFConstituency
            // 
            this.rjFConstituency.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rjFConstituency.AutoSize = true;
            this.rjFConstituency.Location = new System.Drawing.Point(4, 20);
            this.rjFConstituency.MinimumSize = new System.Drawing.Size(50, 25);
            this.rjFConstituency.Name = "rjFConstituency";
            this.rjFConstituency.OffBackColor = System.Drawing.Color.Gray;
            this.rjFConstituency.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.rjFConstituency.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.rjFConstituency.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.rjFConstituency.Size = new System.Drawing.Size(50, 25);
            this.rjFConstituency.TabIndex = 50;
            this.rjFConstituency.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel16
            // 
            this.tableLayoutPanel16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel16.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel16.ColumnCount = 4;
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.69675F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.26233F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.1469F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.6362F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel16.Controls.Add(this.label16, 0, 0);
            this.tableLayoutPanel16.Controls.Add(this.cmb2Division, 3, 0);
            this.tableLayoutPanel16.Controls.Add(this.label17, 2, 0);
            this.tableLayoutPanel16.Controls.Add(this.cmb2Contituency, 1, 0);
            this.tableLayoutPanel16.Enabled = false;
            this.tableLayoutPanel16.Location = new System.Drawing.Point(207, 4);
            this.tableLayoutPanel16.Name = "tableLayoutPanel16";
            this.tableLayoutPanel16.RowCount = 1;
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel16.Size = new System.Drawing.Size(954, 57);
            this.tableLayoutPanel16.TabIndex = 49;
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label16.Location = new System.Drawing.Point(15, 18);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(103, 21);
            this.label16.TabIndex = 35;
            this.label16.Text = "Constituency:";
            // 
            // cmb2Division
            // 
            this.cmb2Division.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb2Division.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb2Division.FormattingEnabled = true;
            this.cmb2Division.Location = new System.Drawing.Point(796, 17);
            this.cmb2Division.Name = "cmb2Division";
            this.cmb2Division.Size = new System.Drawing.Size(154, 23);
            this.cmb2Division.TabIndex = 38;
            this.cmb2Division.Tag = "1";
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label17.Location = new System.Drawing.Point(720, 18);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(69, 21);
            this.label17.TabIndex = 36;
            this.label17.Text = "Division:";
            // 
            // cmb2Contituency
            // 
            this.cmb2Contituency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb2Contituency.FormattingEnabled = true;
            this.cmb2Contituency.Location = new System.Drawing.Point(125, 17);
            this.cmb2Contituency.Name = "cmb2Contituency";
            this.cmb2Contituency.Size = new System.Drawing.Size(472, 23);
            this.cmb2Contituency.TabIndex = 39;
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel13.ColumnCount = 3;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel13.Controls.Add(this.label19, 1, 0);
            this.tableLayoutPanel13.Controls.Add(this.rjFCanvas, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel14, 2, 0);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(3, 74);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 1;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(1165, 62);
            this.tableLayoutPanel13.TabIndex = 50;
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label19.Location = new System.Drawing.Point(61, 20);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(120, 21);
            this.label19.TabIndex = 51;
            this.label19.Text = "Filter By Canvas";
            // 
            // rjFCanvas
            // 
            this.rjFCanvas.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rjFCanvas.AutoSize = true;
            this.rjFCanvas.Location = new System.Drawing.Point(4, 18);
            this.rjFCanvas.MinimumSize = new System.Drawing.Size(50, 25);
            this.rjFCanvas.Name = "rjFCanvas";
            this.rjFCanvas.OffBackColor = System.Drawing.Color.Gray;
            this.rjFCanvas.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.rjFCanvas.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.rjFCanvas.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.rjFCanvas.Size = new System.Drawing.Size(50, 25);
            this.rjFCanvas.TabIndex = 50;
            this.rjFCanvas.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel14.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel14.ColumnCount = 4;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel14.Controls.Add(this.cmb2Canvas, 3, 0);
            this.tableLayoutPanel14.Controls.Add(this.label21, 2, 0);
            this.tableLayoutPanel14.Controls.Add(this.Cmb2CanvasType, 1, 0);
            this.tableLayoutPanel14.Controls.Add(this.label22, 0, 0);
            this.tableLayoutPanel14.Enabled = false;
            this.tableLayoutPanel14.Location = new System.Drawing.Point(188, 4);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 1;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(1008, 54);
            this.tableLayoutPanel14.TabIndex = 49;
            // 
            // cmb2Canvas
            // 
            this.cmb2Canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb2Canvas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb2Canvas.FormattingEnabled = true;
            this.cmb2Canvas.Location = new System.Drawing.Point(757, 15);
            this.cmb2Canvas.Name = "cmb2Canvas";
            this.cmb2Canvas.Size = new System.Drawing.Size(247, 23);
            this.cmb2Canvas.TabIndex = 40;
            this.cmb2Canvas.Tag = "1";
            // 
            // label21
            // 
            this.label21.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label21.Location = new System.Drawing.Point(687, 16);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(63, 21);
            this.label21.TabIndex = 39;
            this.label21.Text = "Canvas:";
            // 
            // Cmb2CanvasType
            // 
            this.Cmb2CanvasType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Cmb2CanvasType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb2CanvasType.FormattingEnabled = true;
            this.Cmb2CanvasType.Location = new System.Drawing.Point(255, 15);
            this.Cmb2CanvasType.Name = "Cmb2CanvasType";
            this.Cmb2CanvasType.Size = new System.Drawing.Size(244, 23);
            this.Cmb2CanvasType.TabIndex = 47;
            this.Cmb2CanvasType.Tag = "1";
            // 
            // label22
            // 
            this.label22.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label22.Location = new System.Drawing.Point(142, 16);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(106, 21);
            this.label22.TabIndex = 46;
            this.label22.Text = "Canvas Types:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cmb2Party);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.ForeColor = System.Drawing.Color.White;
            this.groupBox5.Location = new System.Drawing.Point(1174, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(179, 65);
            this.groupBox5.TabIndex = 55;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Party:";
            // 
            // cmb2Party
            // 
            this.cmb2Party.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmb2Party.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb2Party.FormattingEnabled = true;
            this.cmb2Party.Location = new System.Drawing.Point(3, 19);
            this.cmb2Party.Name = "cmb2Party";
            this.cmb2Party.Size = new System.Drawing.Size(173, 23);
            this.cmb2Party.TabIndex = 39;
            this.cmb2Party.Tag = "1";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(3, 170);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 25;
            this.dataGridView2.Size = new System.Drawing.Size(1362, 637);
            this.dataGridView2.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1368, 810);
            this.Controls.Add(this.tableLayoutPanel11);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel11.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tableLayoutPanel15.PerformLayout();
            this.tableLayoutPanel16.ResumeLayout(false);
            this.tableLayoutPanel16.PerformLayout();
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel14.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel11;
        private GroupBox groupBox4;
        private TableLayoutPanel tableLayoutPanel12;
        private FontAwesome.Sharp.IconButton ibtnUpdate;
        private TableLayoutPanel tableLayoutPanel15;
        private Label label23;
        private Controls.RJToggleButton rjFConstituency;
        private TableLayoutPanel tableLayoutPanel16;
        private Label label16;
        private ComboBox cmb2Division;
        private Label label17;
        private ComboBox cmb2Contituency;
        private TableLayoutPanel tableLayoutPanel13;
        private Label label19;
        private Controls.RJToggleButton rjFCanvas;
        private TableLayoutPanel tableLayoutPanel14;
        private ComboBox cmb2Canvas;
        private Label label21;
        private ComboBox Cmb2CanvasType;
        private Label label22;
        private GroupBox groupBox5;
        private ComboBox cmb2Party;
        private DataGridView dataGridView2;
    }
}