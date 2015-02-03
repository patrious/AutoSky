namespace AutoSky
{
    partial class AutoSky
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoSky));
            this.GoogleSkyWebBrowser = new System.Windows.Forms.WebBrowser();
            this.btnMapToTelescope = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTelescopeToMap = new System.Windows.Forms.Button();
            this.txtBoxCoordinateDEC = new System.Windows.Forms.TextBox();
            this.btnMoveMap = new System.Windows.Forms.Button();
            this.comboBoxPOI = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelectPOI = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.googleSkyLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.googleskystatuslabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.telescopeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.telescopestatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.taskprogress = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutControlPanel = new System.Windows.Forms.TableLayoutPanel();
            this.SeachForObject = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtBoxSavePOI = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSavePOI = new System.Windows.Forms.Button();
            this.ManualCoOrdinates = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtBoxCoordinateRA = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.imgCheckRA = new System.Windows.Forms.PictureBox();
            this.imgErrorRA = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.imgCheckDec = new System.Windows.Forms.PictureBox();
            this.imgErrorDec = new System.Windows.Forms.PictureBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTeleUp = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnTeleLeft = new System.Windows.Forms.Button();
            this.btnTeleDown = new System.Windows.Forms.Button();
            this.btnTeleRight = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBoxEventMessages = new System.Windows.Forms.ListBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.statusStrip.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.tableLayoutControlPanel.SuspendLayout();
            this.SeachForObject.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.ManualCoOrdinates.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCheckRA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgErrorRA)).BeginInit();
            this.flowLayoutPanel5.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCheckDec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgErrorDec)).BeginInit();
            this.panel8.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GoogleSkyWebBrowser
            // 
            this.GoogleSkyWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GoogleSkyWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.GoogleSkyWebBrowser.Margin = new System.Windows.Forms.Padding(0);
            this.GoogleSkyWebBrowser.MinimumSize = new System.Drawing.Size(100, 100);
            this.GoogleSkyWebBrowser.Name = "GoogleSkyWebBrowser";
            this.GoogleSkyWebBrowser.ScriptErrorsSuppressed = true;
            this.GoogleSkyWebBrowser.ScrollBarsEnabled = false;
            this.GoogleSkyWebBrowser.Size = new System.Drawing.Size(932, 466);
            this.GoogleSkyWebBrowser.TabIndex = 0;
            this.GoogleSkyWebBrowser.Url = new System.Uri("", System.UriKind.Relative);
            this.GoogleSkyWebBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.GoogleSkyWebBrowser_Navigating);
            // 
            // btnMapToTelescope
            // 
            this.btnMapToTelescope.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnMapToTelescope.BackColor = System.Drawing.Color.Black;
            this.btnMapToTelescope.ForeColor = System.Drawing.Color.White;
            this.btnMapToTelescope.Location = new System.Drawing.Point(151, 20);
            this.btnMapToTelescope.Name = "btnMapToTelescope";
            this.btnMapToTelescope.Size = new System.Drawing.Size(124, 44);
            this.btnMapToTelescope.TabIndex = 1;
            this.btnMapToTelescope.Text = "Map → Telescope";
            this.btnMapToTelescope.UseVisualStyleBackColor = false;
            this.btnMapToTelescope.Click += new System.EventHandler(this.btnMapToTelescope_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Right Ascension Coodinate (HH:MM:SS.SS)";
            // 
            // btnTelescopeToMap
            // 
            this.btnTelescopeToMap.BackColor = System.Drawing.Color.Black;
            this.btnTelescopeToMap.ForeColor = System.Drawing.Color.White;
            this.btnTelescopeToMap.Location = new System.Drawing.Point(22, 20);
            this.btnTelescopeToMap.Name = "btnTelescopeToMap";
            this.btnTelescopeToMap.Size = new System.Drawing.Size(124, 44);
            this.btnTelescopeToMap.TabIndex = 3;
            this.btnTelescopeToMap.Text = "Telescope → Map";
            this.btnTelescopeToMap.UseVisualStyleBackColor = false;
            this.btnTelescopeToMap.Click += new System.EventHandler(this.btnTelescopeToMap_Click);
            // 
            // txtBoxCoordinateDEC
            // 
            this.txtBoxCoordinateDEC.BackColor = System.Drawing.Color.Black;
            this.txtBoxCoordinateDEC.ForeColor = System.Drawing.Color.White;
            this.txtBoxCoordinateDEC.Location = new System.Drawing.Point(3, 29);
            this.txtBoxCoordinateDEC.Name = "txtBoxCoordinateDEC";
            this.txtBoxCoordinateDEC.Size = new System.Drawing.Size(100, 20);
            this.txtBoxCoordinateDEC.TabIndex = 5;
            this.txtBoxCoordinateDEC.TextChanged += new System.EventHandler(this.txtBoxCoordinateDEC_TextChanged);
            // 
            // btnMoveMap
            // 
            this.btnMoveMap.BackColor = System.Drawing.Color.Black;
            this.btnMoveMap.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMoveMap.ForeColor = System.Drawing.Color.White;
            this.btnMoveMap.Location = new System.Drawing.Point(331, 13);
            this.btnMoveMap.Name = "btnMoveMap";
            this.btnMoveMap.Size = new System.Drawing.Size(100, 58);
            this.btnMoveMap.TabIndex = 6;
            this.btnMoveMap.Text = "Go to coordinate";
            this.btnMoveMap.UseVisualStyleBackColor = false;
            this.btnMoveMap.Click += new System.EventHandler(this.btnMoveMap_Click);
            // 
            // comboBoxPOI
            // 
            this.comboBoxPOI.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxPOI.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxPOI.BackColor = System.Drawing.Color.Black;
            this.comboBoxPOI.ForeColor = System.Drawing.Color.White;
            this.comboBoxPOI.FormattingEnabled = true;
            this.comboBoxPOI.Location = new System.Drawing.Point(6, 14);
            this.comboBoxPOI.Name = "comboBoxPOI";
            this.comboBoxPOI.Size = new System.Drawing.Size(121, 21);
            this.comboBoxPOI.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 26);
            this.label2.TabIndex = 8;
            this.label2.Text = "Declination Coordinate (DD:MM:SS.SS)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, -2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Choose an object of interest";
            // 
            // btnSelectPOI
            // 
            this.btnSelectPOI.BackColor = System.Drawing.Color.Black;
            this.btnSelectPOI.ForeColor = System.Drawing.Color.White;
            this.btnSelectPOI.Location = new System.Drawing.Point(15, 41);
            this.btnSelectPOI.Name = "btnSelectPOI";
            this.btnSelectPOI.Size = new System.Drawing.Size(92, 29);
            this.btnSelectPOI.TabIndex = 10;
            this.btnSelectPOI.Text = "Go to object";
            this.btnSelectPOI.UseVisualStyleBackColor = false;
            this.btnSelectPOI.Click += new System.EventHandler(this.btnSelectPOI_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.googleSkyLabel,
            this.googleskystatuslabel,
            this.telescopeLabel,
            this.telescopestatusLabel,
            this.taskprogress});
            this.statusStrip.Location = new System.Drawing.Point(0, 638);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1126, 22);
            this.statusStrip.TabIndex = 18;
            this.statusStrip.Text = "No Connection";
            // 
            // googleSkyLabel
            // 
            this.googleSkyLabel.Name = "googleSkyLabel";
            this.googleSkyLabel.Size = new System.Drawing.Size(69, 17);
            this.googleSkyLabel.Text = "Google Sky:";
            // 
            // googleskystatuslabel
            // 
            this.googleskystatuslabel.ForeColor = System.Drawing.Color.Red;
            this.googleskystatuslabel.Name = "googleskystatuslabel";
            this.googleskystatuslabel.Size = new System.Drawing.Size(88, 17);
            this.googleskystatuslabel.Text = "Not Connected";
            // 
            // telescopeLabel
            // 
            this.telescopeLabel.Name = "telescopeLabel";
            this.telescopeLabel.Size = new System.Drawing.Size(63, 17);
            this.telescopeLabel.Text = "Telescope:";
            // 
            // telescopestatusLabel
            // 
            this.telescopestatusLabel.ForeColor = System.Drawing.Color.Red;
            this.telescopestatusLabel.Name = "telescopestatusLabel";
            this.telescopestatusLabel.Size = new System.Drawing.Size(88, 17);
            this.telescopestatusLabel.Text = "Not Connected";
            // 
            // taskprogress
            // 
            this.taskprogress.BackColor = System.Drawing.Color.Black;
            this.taskprogress.Name = "taskprogress";
            this.taskprogress.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.taskprogress.Size = new System.Drawing.Size(803, 17);
            this.taskprogress.Spring = true;
            this.taskprogress.Text = "Waiting...";
            this.taskprogress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.AutoSize = true;
            this.MainPanel.ColumnCount = 1;
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainPanel.Controls.Add(this.tableLayoutControlPanel, 0, 1);
            this.MainPanel.Controls.Add(this.GoogleSkyWebBrowser, 0, 0);
            this.MainPanel.Location = new System.Drawing.Point(3, 3);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.RowCount = 2;
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainPanel.Size = new System.Drawing.Size(932, 632);
            this.MainPanel.TabIndex = 19;
            // 
            // tableLayoutControlPanel
            // 
            this.tableLayoutControlPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutControlPanel.ColumnCount = 3;
            this.tableLayoutControlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 450F));
            this.tableLayoutControlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutControlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutControlPanel.Controls.Add(this.SeachForObject, 1, 0);
            this.tableLayoutControlPanel.Controls.Add(this.ManualCoOrdinates, 0, 0);
            this.tableLayoutControlPanel.Controls.Add(this.panel4, 2, 0);
            this.tableLayoutControlPanel.Location = new System.Drawing.Point(3, 469);
            this.tableLayoutControlPanel.Name = "tableLayoutControlPanel";
            this.tableLayoutControlPanel.RowCount = 1;
            this.tableLayoutControlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutControlPanel.Size = new System.Drawing.Size(926, 160);
            this.tableLayoutControlPanel.TabIndex = 20;
            // 
            // SeachForObject
            // 
            this.SeachForObject.AutoSize = true;
            this.SeachForObject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SeachForObject.Controls.Add(this.panel2);
            this.SeachForObject.Controls.Add(this.panel3);
            this.SeachForObject.Location = new System.Drawing.Point(453, 3);
            this.SeachForObject.Name = "SeachForObject";
            this.SeachForObject.Padding = new System.Windows.Forms.Padding(15, 10, 0, 0);
            this.SeachForObject.Size = new System.Drawing.Size(302, 91);
            this.SeachForObject.TabIndex = 20;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnSelectPOI);
            this.panel2.Controls.Add(this.comboBoxPOI);
            this.panel2.Location = new System.Drawing.Point(18, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(145, 73);
            this.panel2.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.txtBoxSavePOI);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.btnSavePOI);
            this.panel3.Location = new System.Drawing.Point(169, 13);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(128, 73);
            this.panel3.TabIndex = 22;
            // 
            // txtBoxSavePOI
            // 
            this.txtBoxSavePOI.BackColor = System.Drawing.Color.Black;
            this.txtBoxSavePOI.ForeColor = System.Drawing.Color.White;
            this.txtBoxSavePOI.Location = new System.Drawing.Point(7, 15);
            this.txtBoxSavePOI.Name = "txtBoxSavePOI";
            this.txtBoxSavePOI.Size = new System.Drawing.Size(112, 20);
            this.txtBoxSavePOI.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, -2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Name of point of interest";
            // 
            // btnSavePOI
            // 
            this.btnSavePOI.BackColor = System.Drawing.Color.Black;
            this.btnSavePOI.ForeColor = System.Drawing.Color.White;
            this.btnSavePOI.Location = new System.Drawing.Point(15, 41);
            this.btnSavePOI.Name = "btnSavePOI";
            this.btnSavePOI.Size = new System.Drawing.Size(92, 29);
            this.btnSavePOI.TabIndex = 10;
            this.btnSavePOI.Text = "Save location";
            this.btnSavePOI.UseVisualStyleBackColor = false;
            this.btnSavePOI.Click += new System.EventHandler(this.btnSavePOI_Click);
            // 
            // ManualCoOrdinates
            // 
            this.ManualCoOrdinates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ManualCoOrdinates.Controls.Add(this.flowLayoutPanel4);
            this.ManualCoOrdinates.Controls.Add(this.flowLayoutPanel5);
            this.ManualCoOrdinates.Controls.Add(this.btnMoveMap);
            this.ManualCoOrdinates.Controls.Add(this.panel8);
            this.ManualCoOrdinates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ManualCoOrdinates.Location = new System.Drawing.Point(3, 3);
            this.ManualCoOrdinates.Name = "ManualCoOrdinates";
            this.ManualCoOrdinates.Padding = new System.Windows.Forms.Padding(15, 10, 0, 0);
            this.ManualCoOrdinates.Size = new System.Drawing.Size(444, 154);
            this.ManualCoOrdinates.TabIndex = 20;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.label1);
            this.flowLayoutPanel4.Controls.Add(this.txtBoxCoordinateRA);
            this.flowLayoutPanel4.Controls.Add(this.panel5);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(18, 13);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(166, 58);
            this.flowLayoutPanel4.TabIndex = 21;
            // 
            // txtBoxCoordinateRA
            // 
            this.txtBoxCoordinateRA.BackColor = System.Drawing.Color.Black;
            this.txtBoxCoordinateRA.ForeColor = System.Drawing.Color.White;
            this.txtBoxCoordinateRA.Location = new System.Drawing.Point(3, 29);
            this.txtBoxCoordinateRA.Name = "txtBoxCoordinateRA";
            this.txtBoxCoordinateRA.Size = new System.Drawing.Size(100, 20);
            this.txtBoxCoordinateRA.TabIndex = 6;
            this.txtBoxCoordinateRA.TextChanged += new System.EventHandler(this.txtBoxCoordinateRA_TextChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.imgCheckRA);
            this.panel5.Controls.Add(this.imgErrorRA);
            this.panel5.Location = new System.Drawing.Point(109, 29);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(21, 20);
            this.panel5.TabIndex = 7;
            // 
            // imgCheckRA
            // 
            this.imgCheckRA.Image = ((System.Drawing.Image)(resources.GetObject("imgCheckRA.Image")));
            this.imgCheckRA.Location = new System.Drawing.Point(2, 0);
            this.imgCheckRA.Name = "imgCheckRA";
            this.imgCheckRA.Size = new System.Drawing.Size(19, 20);
            this.imgCheckRA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgCheckRA.TabIndex = 9;
            this.imgCheckRA.TabStop = false;
            this.imgCheckRA.Visible = false;
            // 
            // imgErrorRA
            // 
            this.imgErrorRA.Image = ((System.Drawing.Image)(resources.GetObject("imgErrorRA.Image")));
            this.imgErrorRA.Location = new System.Drawing.Point(1, 0);
            this.imgErrorRA.Name = "imgErrorRA";
            this.imgErrorRA.Size = new System.Drawing.Size(19, 20);
            this.imgErrorRA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgErrorRA.TabIndex = 8;
            this.imgErrorRA.TabStop = false;
            this.imgErrorRA.Visible = false;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Controls.Add(this.label2);
            this.flowLayoutPanel5.Controls.Add(this.txtBoxCoordinateDEC);
            this.flowLayoutPanel5.Controls.Add(this.panel7);
            this.flowLayoutPanel5.Location = new System.Drawing.Point(190, 13);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(135, 58);
            this.flowLayoutPanel5.TabIndex = 21;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.imgCheckDec);
            this.panel7.Controls.Add(this.imgErrorDec);
            this.panel7.Location = new System.Drawing.Point(109, 29);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(22, 18);
            this.panel7.TabIndex = 9;
            // 
            // imgCheckDec
            // 
            this.imgCheckDec.Image = ((System.Drawing.Image)(resources.GetObject("imgCheckDec.Image")));
            this.imgCheckDec.Location = new System.Drawing.Point(2, 0);
            this.imgCheckDec.Name = "imgCheckDec";
            this.imgCheckDec.Size = new System.Drawing.Size(19, 20);
            this.imgCheckDec.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgCheckDec.TabIndex = 10;
            this.imgCheckDec.TabStop = false;
            this.imgCheckDec.Visible = false;
            // 
            // imgErrorDec
            // 
            this.imgErrorDec.Image = ((System.Drawing.Image)(resources.GetObject("imgErrorDec.Image")));
            this.imgErrorDec.Location = new System.Drawing.Point(2, -1);
            this.imgErrorDec.Name = "imgErrorDec";
            this.imgErrorDec.Size = new System.Drawing.Size(19, 20);
            this.imgErrorDec.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgErrorDec.TabIndex = 9;
            this.imgErrorDec.TabStop = false;
            this.imgErrorDec.Visible = false;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.label6);
            this.panel8.Controls.Add(this.btnMapToTelescope);
            this.panel8.Controls.Add(this.btnTelescopeToMap);
            this.panel8.Location = new System.Drawing.Point(18, 77);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(292, 67);
            this.panel8.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Syncing Map and Telescope";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Location = new System.Drawing.Point(761, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(165, 154);
            this.panel4.TabIndex = 21;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label5);
            this.panel6.Controls.Add(this.btnTeleUp);
            this.panel6.Controls.Add(this.button1);
            this.panel6.Controls.Add(this.btnTeleLeft);
            this.panel6.Controls.Add(this.btnTeleDown);
            this.panel6.Controls.Add(this.btnTeleRight);
            this.panel6.Location = new System.Drawing.Point(3, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(162, 154);
            this.panel6.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Telescope Manual Controls";
            // 
            // btnTeleUp
            // 
            this.btnTeleUp.Image = ((System.Drawing.Image)(resources.GetObject("btnTeleUp.Image")));
            this.btnTeleUp.Location = new System.Drawing.Point(57, 22);
            this.btnTeleUp.Name = "btnTeleUp";
            this.btnTeleUp.Size = new System.Drawing.Size(40, 40);
            this.btnTeleUp.TabIndex = 0;
            this.btnTeleUp.UseVisualStyleBackColor = true;
            this.btnTeleUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnTeleUp_MouseDown);
            this.btnTeleUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnTele_MouseUp);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Maroon;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(57, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "Stop";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnTeleLeft
            // 
            this.btnTeleLeft.Image = ((System.Drawing.Image)(resources.GetObject("btnTeleLeft.Image")));
            this.btnTeleLeft.Location = new System.Drawing.Point(11, 63);
            this.btnTeleLeft.Name = "btnTeleLeft";
            this.btnTeleLeft.Size = new System.Drawing.Size(40, 40);
            this.btnTeleLeft.TabIndex = 1;
            this.btnTeleLeft.UseVisualStyleBackColor = true;
            this.btnTeleLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnTeleLeft_MouseDown);
            this.btnTeleLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnTele_MouseUp);
            // 
            // btnTeleDown
            // 
            this.btnTeleDown.Image = ((System.Drawing.Image)(resources.GetObject("btnTeleDown.Image")));
            this.btnTeleDown.Location = new System.Drawing.Point(57, 110);
            this.btnTeleDown.Name = "btnTeleDown";
            this.btnTeleDown.Size = new System.Drawing.Size(40, 40);
            this.btnTeleDown.TabIndex = 3;
            this.btnTeleDown.UseVisualStyleBackColor = true;
            this.btnTeleDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnTeleDown_MouseDown);
            this.btnTeleDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnTele_MouseUp);
            // 
            // btnTeleRight
            // 
            this.btnTeleRight.Image = ((System.Drawing.Image)(resources.GetObject("btnTeleRight.Image")));
            this.btnTeleRight.Location = new System.Drawing.Point(103, 62);
            this.btnTeleRight.Name = "btnTeleRight";
            this.btnTeleRight.Size = new System.Drawing.Size(40, 40);
            this.btnTeleRight.TabIndex = 2;
            this.btnTeleRight.UseVisualStyleBackColor = true;
            this.btnTeleRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnTeleRight_MouseDown);
            this.btnTeleRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnTele_MouseUp);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.33334F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66666F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.MainPanel, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 638F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1126, 638);
            this.tableLayoutPanel2.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.listBoxEventMessages);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(941, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(182, 632);
            this.panel1.TabIndex = 21;
            // 
            // listBoxEventMessages
            // 
            this.listBoxEventMessages.BackColor = System.Drawing.Color.Black;
            this.listBoxEventMessages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxEventMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxEventMessages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.listBoxEventMessages.FormattingEnabled = true;
            this.listBoxEventMessages.HorizontalScrollbar = true;
            this.listBoxEventMessages.Location = new System.Drawing.Point(0, 0);
            this.listBoxEventMessages.Name = "listBoxEventMessages";
            this.listBoxEventMessages.ScrollAlwaysVisible = true;
            this.listBoxEventMessages.Size = new System.Drawing.Size(182, 632);
            this.listBoxEventMessages.TabIndex = 16;
            // 
            // AutoSky
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1126, 660);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.statusStrip);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "AutoSky";
            this.Text = "AutoSky";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AutoSky_FormClosing);
            this.Load += new System.EventHandler(this.AutoSky_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AutoSky_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AutoSky_KeyPress);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.tableLayoutControlPanel.ResumeLayout(false);
            this.tableLayoutControlPanel.PerformLayout();
            this.SeachForObject.ResumeLayout(false);
            this.SeachForObject.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ManualCoOrdinates.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgCheckRA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgErrorRA)).EndInit();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgCheckDec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgErrorDec)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser GoogleSkyWebBrowser;
        private System.Windows.Forms.Button btnMapToTelescope;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTelescopeToMap;
        private System.Windows.Forms.TextBox txtBoxCoordinateDEC;
        private System.Windows.Forms.Button btnMoveMap;
        private System.Windows.Forms.ComboBox comboBoxPOI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelectPOI;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel googleSkyLabel;
        private System.Windows.Forms.ToolStripStatusLabel googleskystatuslabel;
        private System.Windows.Forms.ToolStripStatusLabel telescopeLabel;
        private System.Windows.Forms.ToolStripStatusLabel telescopestatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel taskprogress;
        private System.Windows.Forms.TableLayoutPanel MainPanel;
        private System.Windows.Forms.FlowLayoutPanel ManualCoOrdinates;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.FlowLayoutPanel SeachForObject;
        private System.Windows.Forms.TableLayoutPanel tableLayoutControlPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtBoxCoordinateRA;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnSavePOI;
        private System.Windows.Forms.TextBox txtBoxSavePOI;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBoxEventMessages;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnTeleUp;
        private System.Windows.Forms.Button btnTeleLeft;
        private System.Windows.Forms.Button btnTeleDown;
        private System.Windows.Forms.Button btnTeleRight;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox imgCheckRA;
        private System.Windows.Forms.PictureBox imgErrorRA;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.PictureBox imgCheckDec;
        private System.Windows.Forms.PictureBox imgErrorDec;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label6;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button1;

    }
}

