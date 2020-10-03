namespace Oc34n_OneClickTool
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label5 = new System.Windows.Forms.Label();
            this.ActivationWorker = new System.ComponentModel.BackgroundWorker();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.progressBar1 = new MetroFramework.Controls.MetroProgressBar();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager();
            this.RFSCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.SubstrateBox = new MetroFramework.Controls.MetroCheckBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.MODEL_TEXT = new MetroFramework.Controls.MetroLabel();
            this.UDID_TEXT = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.IMEI_TEXT = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.IOS_TEXT = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.SN_TEXT = new MetroFramework.Controls.MetroLabel();
            this.metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            this.waitingText = new MetroFramework.Controls.MetroLabel();
            this.metroCheckBox2 = new MetroFramework.Controls.MetroCheckBox();
            this.RebootBox = new MetroFramework.Controls.MetroCheckBox();
            this.WildCardBox = new MetroFramework.Controls.MetroCheckBox();
            this.DisableBBBox = new MetroFramework.Controls.MetroCheckBox();
            this.NoOTABox = new MetroFramework.Controls.MetroCheckBox();
            this.SkipSetupBox = new MetroFramework.Controls.MetroCheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Enabled = false;
            this.label5.Location = new System.Drawing.Point(117, 95);
            this.label5.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 6;
            this.label5.Click += new System.EventHandler(this.Label5_Click);
            // 
            // ActivationWorker
            // 
            this.ActivationWorker.WorkerReportsProgress = true;
            this.ActivationWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ActivationWorker_DoWork);
            // 
            // ToolTip
            // 
            this.ToolTip.Tag = "";
            // 
            // progressBar1
            // 
            this.progressBar1.FontSize = MetroFramework.MetroProgressBarSize.Medium;
            this.progressBar1.FontWeight = MetroFramework.MetroProgressBarWeight.Light;
            this.progressBar1.HideProgressText = true;
            this.progressBar1.Location = new System.Drawing.Point(28, 308);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.ProgressBarStyle = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.Size = new System.Drawing.Size(518, 23);
            this.progressBar1.Style = MetroFramework.MetroColorStyle.Teal;
            this.progressBar1.StyleManager = this.metroStyleManager1;
            this.progressBar1.TabIndex = 20;
            this.progressBar1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.progressBar1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.progressBar1.Click += new System.EventHandler(this.ProgressBar1_Click);
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.OwnerForm = this;
            this.metroStyleManager1.Style = MetroFramework.MetroColorStyle.Teal;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // RFSCheckBox
            // 
            this.RFSCheckBox.AutoSize = true;
            this.RFSCheckBox.CustomBackground = false;
            this.RFSCheckBox.FontSize = MetroFramework.MetroLinkSize.Small;
            this.RFSCheckBox.FontWeight = MetroFramework.MetroLinkWeight.Bold;
            this.RFSCheckBox.Location = new System.Drawing.Point(171, 287);
            this.RFSCheckBox.Name = "RFSCheckBox";
            this.RFSCheckBox.Size = new System.Drawing.Size(110, 15);
            this.RFSCheckBox.Style = MetroFramework.MetroColorStyle.Teal;
            this.RFSCheckBox.StyleManager = this.metroStyleManager1;
            this.RFSCheckBox.TabIndex = 18;
            this.RFSCheckBox.Text = "Restore RootFS";
            this.RFSCheckBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.RFSCheckBox.UseStyleColors = true;
            this.RFSCheckBox.UseVisualStyleBackColor = true;
            this.RFSCheckBox.CheckedChanged += new System.EventHandler(this.CheckBox2_CheckedChanged);
            // 
            // SubstrateBox
            // 
            this.SubstrateBox.AutoSize = true;
            this.SubstrateBox.CustomBackground = false;
            this.SubstrateBox.FontSize = MetroFramework.MetroLinkSize.Small;
            this.SubstrateBox.FontWeight = MetroFramework.MetroLinkWeight.Bold;
            this.SubstrateBox.Location = new System.Drawing.Point(171, 266);
            this.SubstrateBox.Name = "SubstrateBox";
            this.SubstrateBox.Size = new System.Drawing.Size(140, 15);
            this.SubstrateBox.Style = MetroFramework.MetroColorStyle.Teal;
            this.SubstrateBox.StyleManager = this.metroStyleManager1;
            this.SubstrateBox.TabIndex = 17;
            this.SubstrateBox.Text = "Don\'t push Substrate";
            this.SubstrateBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.SubstrateBox.UseStyleColors = true;
            this.SubstrateBox.UseVisualStyleBackColor = true;
            this.SubstrateBox.CheckedChanged += new System.EventHandler(this.SubstrateBox_CheckedChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.CustomBackground = false;
            this.metroLabel1.Enabled = false;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel1.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabel1.Location = new System.Drawing.Point(22, 60);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(71, 25);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Teal;
            this.metroLabel1.StyleManager = this.metroStyleManager1;
            this.metroLabel1.TabIndex = 1;
            this.metroLabel1.Text = "Model:";
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel1.UseStyleColors = false;
            this.metroLabel1.Visible = false;
            // 
            // MODEL_TEXT
            // 
            this.MODEL_TEXT.AutoSize = true;
            this.MODEL_TEXT.CustomBackground = false;
            this.MODEL_TEXT.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.MODEL_TEXT.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.MODEL_TEXT.LabelMode = MetroFramework.Controls.MetroLabelMode.Selectable;
            this.MODEL_TEXT.Location = new System.Drawing.Point(105, 60);
            this.MODEL_TEXT.Name = "MODEL_TEXT";
            this.MODEL_TEXT.Size = new System.Drawing.Size(44, 25);
            this.MODEL_TEXT.Style = MetroFramework.MetroColorStyle.Teal;
            this.MODEL_TEXT.StyleManager = this.metroStyleManager1;
            this.MODEL_TEXT.TabIndex = 2;
            this.MODEL_TEXT.Text = "N/A";
            this.MODEL_TEXT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.MODEL_TEXT.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.MODEL_TEXT.UseStyleColors = false;
            this.MODEL_TEXT.Visible = false;
            // 
            // UDID_TEXT
            // 
            this.UDID_TEXT.AutoSize = true;
            this.UDID_TEXT.CustomBackground = false;
            this.UDID_TEXT.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.UDID_TEXT.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.UDID_TEXT.LabelMode = MetroFramework.Controls.MetroLabelMode.Selectable;
            this.UDID_TEXT.Location = new System.Drawing.Point(105, 150);
            this.UDID_TEXT.Name = "UDID_TEXT";
            this.UDID_TEXT.Size = new System.Drawing.Size(44, 25);
            this.UDID_TEXT.Style = MetroFramework.MetroColorStyle.Teal;
            this.UDID_TEXT.StyleManager = this.metroStyleManager1;
            this.UDID_TEXT.TabIndex = 8;
            this.UDID_TEXT.Text = "N/A";
            this.UDID_TEXT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UDID_TEXT.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.UDID_TEXT.UseStyleColors = false;
            this.UDID_TEXT.Visible = false;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.CustomBackground = false;
            this.metroLabel2.Enabled = false;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel2.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabel2.Location = new System.Drawing.Point(22, 90);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(46, 25);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Teal;
            this.metroLabel2.StyleManager = this.metroStyleManager1;
            this.metroLabel2.TabIndex = 3;
            this.metroLabel2.Text = "iOS:";
            this.metroLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel2.UseStyleColors = false;
            this.metroLabel2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(117, 117);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 33;
            // 
            // IMEI_TEXT
            // 
            this.IMEI_TEXT.AutoSize = true;
            this.IMEI_TEXT.CustomBackground = false;
            this.IMEI_TEXT.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.IMEI_TEXT.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.IMEI_TEXT.LabelMode = MetroFramework.Controls.MetroLabelMode.Selectable;
            this.IMEI_TEXT.Location = new System.Drawing.Point(105, 180);
            this.IMEI_TEXT.Name = "IMEI_TEXT";
            this.IMEI_TEXT.Size = new System.Drawing.Size(44, 25);
            this.IMEI_TEXT.Style = MetroFramework.MetroColorStyle.Teal;
            this.IMEI_TEXT.StyleManager = this.metroStyleManager1;
            this.IMEI_TEXT.TabIndex = 10;
            this.IMEI_TEXT.Text = "N/A";
            this.IMEI_TEXT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IMEI_TEXT.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.IMEI_TEXT.UseStyleColors = false;
            this.IMEI_TEXT.Visible = false;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.CustomBackground = false;
            this.metroLabel3.Enabled = false;
            this.metroLabel3.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel3.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabel3.Location = new System.Drawing.Point(22, 120);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(64, 25);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Teal;
            this.metroLabel3.StyleManager = this.metroStyleManager1;
            this.metroLabel3.TabIndex = 5;
            this.metroLabel3.Text = "Serial:";
            this.metroLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel3.UseStyleColors = false;
            this.metroLabel3.Visible = false;
            // 
            // IOS_TEXT
            // 
            this.IOS_TEXT.AutoSize = true;
            this.IOS_TEXT.CustomBackground = false;
            this.IOS_TEXT.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.IOS_TEXT.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.IOS_TEXT.LabelMode = MetroFramework.Controls.MetroLabelMode.Selectable;
            this.IOS_TEXT.Location = new System.Drawing.Point(105, 90);
            this.IOS_TEXT.Name = "IOS_TEXT";
            this.IOS_TEXT.Size = new System.Drawing.Size(44, 25);
            this.IOS_TEXT.Style = MetroFramework.MetroColorStyle.Teal;
            this.IOS_TEXT.StyleManager = this.metroStyleManager1;
            this.IOS_TEXT.TabIndex = 4;
            this.IOS_TEXT.Text = "N/A";
            this.IOS_TEXT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IOS_TEXT.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.IOS_TEXT.UseStyleColors = false;
            this.IOS_TEXT.Visible = false;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.CustomBackground = false;
            this.metroLabel4.Enabled = false;
            this.metroLabel4.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel4.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabel4.Location = new System.Drawing.Point(22, 150);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(62, 25);
            this.metroLabel4.Style = MetroFramework.MetroColorStyle.Teal;
            this.metroLabel4.StyleManager = this.metroStyleManager1;
            this.metroLabel4.TabIndex = 7;
            this.metroLabel4.Text = "UDID:";
            this.metroLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.metroLabel4.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel4.UseStyleColors = false;
            this.metroLabel4.Visible = false;
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.CustomBackground = false;
            this.metroLabel5.Enabled = false;
            this.metroLabel5.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel5.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabel5.Location = new System.Drawing.Point(22, 180);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(56, 25);
            this.metroLabel5.Style = MetroFramework.MetroColorStyle.Teal;
            this.metroLabel5.StyleManager = this.metroStyleManager1;
            this.metroLabel5.TabIndex = 9;
            this.metroLabel5.Text = "IMEI:";
            this.metroLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.metroLabel5.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel5.UseStyleColors = false;
            this.metroLabel5.Visible = false;
            // 
            // SN_TEXT
            // 
            this.SN_TEXT.AutoSize = true;
            this.SN_TEXT.CustomBackground = false;
            this.SN_TEXT.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.SN_TEXT.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.SN_TEXT.LabelMode = MetroFramework.Controls.MetroLabelMode.Selectable;
            this.SN_TEXT.Location = new System.Drawing.Point(105, 120);
            this.SN_TEXT.Name = "SN_TEXT";
            this.SN_TEXT.Size = new System.Drawing.Size(44, 25);
            this.SN_TEXT.Style = MetroFramework.MetroColorStyle.Teal;
            this.SN_TEXT.StyleManager = this.metroStyleManager1;
            this.SN_TEXT.TabIndex = 6;
            this.SN_TEXT.Text = "N/A";
            this.SN_TEXT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SN_TEXT.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.SN_TEXT.UseStyleColors = false;
            this.SN_TEXT.Visible = false;
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.metroProgressSpinner1.Cursor = System.Windows.Forms.Cursors.Default;
            this.metroProgressSpinner1.CustomBackground = false;
            this.metroProgressSpinner1.Location = new System.Drawing.Point(210, 59);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(146, 146);
            this.metroProgressSpinner1.Speed = 2F;
            this.metroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Teal;
            this.metroProgressSpinner1.StyleManager = this.metroStyleManager1;
            this.metroProgressSpinner1.TabIndex = 21;
            this.metroProgressSpinner1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroProgressSpinner1.Value = 50;
            this.metroProgressSpinner1.Visible = false;
            this.metroProgressSpinner1.Click += new System.EventHandler(this.metroProgressSpinner1_Click);
            // 
            // waitingText
            // 
            this.waitingText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.waitingText.AutoSize = true;
            this.waitingText.CustomBackground = false;
            this.waitingText.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.waitingText.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.waitingText.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.waitingText.Location = new System.Drawing.Point(203, 120);
            this.waitingText.Name = "waitingText";
            this.waitingText.Size = new System.Drawing.Size(160, 25);
            this.waitingText.Style = MetroFramework.MetroColorStyle.Teal;
            this.waitingText.StyleManager = this.metroStyleManager1;
            this.waitingText.TabIndex = 22;
            this.waitingText.Text = "Waiting for device...";
            this.waitingText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.waitingText.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.waitingText.UseStyleColors = false;
            this.waitingText.Click += new System.EventHandler(this.metroLabel6_Click);
            // 
            // metroCheckBox2
            // 
            this.metroCheckBox2.AutoSize = true;
            this.metroCheckBox2.Checked = true;
            this.metroCheckBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.metroCheckBox2.CustomBackground = false;
            this.metroCheckBox2.FontSize = MetroFramework.MetroLinkSize.Small;
            this.metroCheckBox2.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.metroCheckBox2.Location = new System.Drawing.Point(28, 225);
            this.metroCheckBox2.Name = "metroCheckBox2";
            this.metroCheckBox2.Size = new System.Drawing.Size(81, 15);
            this.metroCheckBox2.Style = MetroFramework.MetroColorStyle.Teal;
            this.metroCheckBox2.StyleManager = this.metroStyleManager1;
            this.metroCheckBox2.TabIndex = 15;
            this.metroCheckBox2.Text = "Dark mode";
            this.metroCheckBox2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroCheckBox2.UseStyleColors = false;
            this.metroCheckBox2.UseVisualStyleBackColor = true;
            this.metroCheckBox2.CheckedChanged += new System.EventHandler(this.metroCheckBox2_CheckedChanged);
            // 
            // RebootBox
            // 
            this.RebootBox.AutoSize = true;
            this.RebootBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.RebootBox.CustomBackground = false;
            this.RebootBox.FontSize = MetroFramework.MetroLinkSize.Small;
            this.RebootBox.FontWeight = MetroFramework.MetroLinkWeight.Bold;
            this.RebootBox.Location = new System.Drawing.Point(171, 245);
            this.RebootBox.Name = "RebootBox";
            this.RebootBox.Size = new System.Drawing.Size(153, 15);
            this.RebootBox.Style = MetroFramework.MetroColorStyle.Teal;
            this.RebootBox.StyleManager = this.metroStyleManager1;
            this.RebootBox.TabIndex = 16;
            this.RebootBox.Text = "Reboot after activation";
            this.RebootBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.RebootBox.UseStyleColors = true;
            this.RebootBox.UseVisualStyleBackColor = false;
            // 
            // WildCardBox
            // 
            this.WildCardBox.AutoSize = true;
            this.WildCardBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.WildCardBox.CustomBackground = false;
            this.WildCardBox.Enabled = false;
            this.WildCardBox.FontSize = MetroFramework.MetroLinkSize.Small;
            this.WildCardBox.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.WildCardBox.Location = new System.Drawing.Point(171, 225);
            this.WildCardBox.Name = "WildCardBox";
            this.WildCardBox.Size = new System.Drawing.Size(90, 15);
            this.WildCardBox.Style = MetroFramework.MetroColorStyle.Teal;
            this.WildCardBox.StyleManager = this.metroStyleManager1;
            this.WildCardBox.TabIndex = 11;
            this.WildCardBox.Text = "Do wild stuff";
            this.WildCardBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.WildCardBox.UseStyleColors = false;
            this.WildCardBox.UseVisualStyleBackColor = false;
            this.WildCardBox.CheckedChanged += new System.EventHandler(this.metroCheckBox1_CheckedChanged);
            // 
            // DisableBBBox
            // 
            this.DisableBBBox.AutoSize = true;
            this.DisableBBBox.Checked = global::Oc34n_OneClickTool.Properties.Settings.Default.doesDisableBaseband;
            this.DisableBBBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DisableBBBox.CustomBackground = false;
            this.DisableBBBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Oc34n_OneClickTool.Properties.Settings.Default, "doesDisableBaseband", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DisableBBBox.FontSize = MetroFramework.MetroLinkSize.Small;
            this.DisableBBBox.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.DisableBBBox.Location = new System.Drawing.Point(28, 266);
            this.DisableBBBox.Name = "DisableBBBox";
            this.DisableBBBox.Size = new System.Drawing.Size(115, 15);
            this.DisableBBBox.Style = MetroFramework.MetroColorStyle.Teal;
            this.DisableBBBox.StyleManager = this.metroStyleManager1;
            this.DisableBBBox.TabIndex = 13;
            this.DisableBBBox.Text = "Disable baseband";
            this.DisableBBBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.DisableBBBox.UseStyleColors = false;
            this.DisableBBBox.UseVisualStyleBackColor = true;
            this.DisableBBBox.CheckedChanged += new System.EventHandler(this.DisableBBBox_CheckedChanged);
            // 
            // NoOTABox
            // 
            this.NoOTABox.AutoSize = true;
            this.NoOTABox.Checked = global::Oc34n_OneClickTool.Properties.Settings.Default.doesDisableOTA;
            this.NoOTABox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.NoOTABox.CustomBackground = false;
            this.NoOTABox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Oc34n_OneClickTool.Properties.Settings.Default, "doesDisableOTA", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.NoOTABox.FontSize = MetroFramework.MetroLinkSize.Small;
            this.NoOTABox.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.NoOTABox.Location = new System.Drawing.Point(28, 287);
            this.NoOTABox.Name = "NoOTABox";
            this.NoOTABox.Size = new System.Drawing.Size(127, 15);
            this.NoOTABox.Style = MetroFramework.MetroColorStyle.Teal;
            this.NoOTABox.StyleManager = this.metroStyleManager1;
            this.NoOTABox.TabIndex = 14;
            this.NoOTABox.Text = "Disable iOS updates";
            this.NoOTABox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.NoOTABox.UseStyleColors = false;
            this.NoOTABox.UseVisualStyleBackColor = true;
            this.NoOTABox.CheckedChanged += new System.EventHandler(this.NoOTABox_CheckedChanged);
            // 
            // SkipSetupBox
            // 
            this.SkipSetupBox.AutoSize = true;
            this.SkipSetupBox.Checked = global::Oc34n_OneClickTool.Properties.Settings.Default.doesSkipSetup;
            this.SkipSetupBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SkipSetupBox.CustomBackground = false;
            this.SkipSetupBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Oc34n_OneClickTool.Properties.Settings.Default, "doesSkipSetup", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.SkipSetupBox.FontSize = MetroFramework.MetroLinkSize.Small;
            this.SkipSetupBox.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.SkipSetupBox.Location = new System.Drawing.Point(28, 245);
            this.SkipSetupBox.Name = "SkipSetupBox";
            this.SkipSetupBox.Size = new System.Drawing.Size(77, 15);
            this.SkipSetupBox.Style = MetroFramework.MetroColorStyle.Teal;
            this.SkipSetupBox.StyleManager = this.metroStyleManager1;
            this.SkipSetupBox.TabIndex = 12;
            this.SkipSetupBox.Text = "Skip setup";
            this.SkipSetupBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.SkipSetupBox.UseStyleColors = false;
            this.SkipSetupBox.UseVisualStyleBackColor = true;
            this.SkipSetupBox.CheckedChanged += new System.EventHandler(this.SkipSetupBox_CheckedChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(173)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semilight", 26F);
            this.button1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Location = new System.Drawing.Point(330, 225);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(216, 78);
            this.button1.TabIndex = 19;
            this.button1.Text = "Activate";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(574, 340);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.metroCheckBox2);
            this.Controls.Add(this.waitingText);
            this.Controls.Add(this.metroProgressSpinner1);
            this.Controls.Add(this.SN_TEXT);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.IOS_TEXT);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.IMEI_TEXT);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.UDID_TEXT);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MODEL_TEXT);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.DisableBBBox);
            this.Controls.Add(this.SubstrateBox);
            this.Controls.Add(this.RebootBox);
            this.Controls.Add(this.NoOTABox);
            this.Controls.Add(this.RFSCheckBox);
            this.Controls.Add(this.SkipSetupBox);
            this.Controls.Add(this.WildCardBox);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label5);
            this.ForeColor = System.Drawing.Color.DarkGray;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Resizable = false;
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Style = MetroFramework.MetroColorStyle.Teal;
            this.StyleManager = this.metroStyleManager1;
            this.Text = "oc34n r2";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label5;
        public System.ComponentModel.BackgroundWorker ActivationWorker;
        private System.Windows.Forms.ToolTip ToolTip;
        public MetroFramework.Controls.MetroProgressBar progressBar1;
        public MetroFramework.Controls.MetroCheckBox SkipSetupBox;
        public MetroFramework.Controls.MetroCheckBox NoOTABox;
        public MetroFramework.Controls.MetroCheckBox RFSCheckBox;
        public MetroFramework.Controls.MetroCheckBox DisableBBBox;
        public MetroFramework.Controls.MetroCheckBox SubstrateBox;
        public MetroFramework.Controls.MetroCheckBox RebootBox;
        public MetroFramework.Controls.MetroLabel UDID_TEXT;
        private System.Windows.Forms.Label label1;
        public MetroFramework.Controls.MetroLabel MODEL_TEXT;
        public MetroFramework.Controls.MetroLabel SN_TEXT;
        public MetroFramework.Controls.MetroLabel IOS_TEXT;
        public MetroFramework.Controls.MetroLabel IMEI_TEXT;
        public MetroFramework.Controls.MetroLabel metroLabel2;
        public MetroFramework.Controls.MetroLabel metroLabel1;
        public MetroFramework.Controls.MetroLabel metroLabel5;
        public MetroFramework.Controls.MetroLabel metroLabel4;
        public MetroFramework.Controls.MetroLabel metroLabel3;
        public MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
        private MetroFramework.Controls.MetroLabel waitingText;
        private MetroFramework.Controls.MetroCheckBox metroCheckBox2;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroCheckBox WildCardBox;
        public System.Windows.Forms.Button button1;
    }
}

