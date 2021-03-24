namespace ModFactoryTestUI
{
    partial class FormSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetup));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageCamera = new System.Windows.Forms.TabPage();
            this.lblCam2 = new System.Windows.Forms.Label();
            this.lblCam1 = new System.Windows.Forms.Label();
            this.rtxtBoxCam2 = new System.Windows.Forms.RichTextBox();
            this.rtxtBoxCam1 = new System.Windows.Forms.RichTextBox();
            this.rtxtBoxCam = new System.Windows.Forms.RichTextBox();
            this.groupBoxConfirm = new System.Windows.Forms.GroupBox();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnYes = new System.Windows.Forms.Button();
            this.groupBoxStation = new System.Windows.Forms.GroupBox();
            this.rbLeftCam = new System.Windows.Forms.RadioButton();
            this.btnConfirmStation = new System.Windows.Forms.Button();
            this.rbRightCam = new System.Windows.Forms.RadioButton();
            this.lblCameraDetails = new System.Windows.Forms.Label();
            this.lblCameraTitle = new System.Windows.Forms.Label();
            this.picWebCamLeft = new System.Windows.Forms.PictureBox();
            this.tabPageCelular = new System.Windows.Forms.TabPage();
            this.pbInsert2Phones = new System.Windows.Forms.PictureBox();
            this.pbRemoveRightPhone = new System.Windows.Forms.PictureBox();
            this.pbInsertRightPhone = new System.Windows.Forms.PictureBox();
            this.pbRemoveLeftPhone = new System.Windows.Forms.PictureBox();
            this.pbInsertLeftPhone = new System.Windows.Forms.PictureBox();
            this.pbRemove2Phones = new System.Windows.Forms.PictureBox();
            this.txtBoxRightCellNumber = new System.Windows.Forms.TextBox();
            this.txtBoxLeftCellNumber = new System.Windows.Forms.TextBox();
            this.btnInsert2Phones = new System.Windows.Forms.Button();
            this.btnRemoveRightPhone = new System.Windows.Forms.Button();
            this.btnInsertRightPhone = new System.Windows.Forms.Button();
            this.btnRemoveLeftPhone = new System.Windows.Forms.Button();
            this.btnInsertLeftPhone = new System.Windows.Forms.Button();
            this.btnRemove2Phones = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblInsert2Phones = new System.Windows.Forms.Label();
            this.lblRemoveRightPhone = new System.Windows.Forms.Label();
            this.lblRightCellNumber = new System.Windows.Forms.Label();
            this.lblInsertRightPhone = new System.Windows.Forms.Label();
            this.lblRemoveLeftPhone = new System.Windows.Forms.Label();
            this.lblLeftCellNumber = new System.Windows.Forms.Label();
            this.lblInsertLeftPhone = new System.Windows.Forms.Label();
            this.lblRemove2Phones = new System.Windows.Forms.Label();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.bckGrdWorkerConfigCell = new System.ComponentModel.BackgroundWorker();
            this.btnReconfigure = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPageCamera.SuspendLayout();
            this.groupBoxConfirm.SuspendLayout();
            this.groupBoxStation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWebCamLeft)).BeginInit();
            this.tabPageCelular.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInsert2Phones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRemoveRightPhone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInsertRightPhone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRemoveLeftPhone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInsertLeftPhone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRemove2Phones)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageCamera);
            this.tabControl.Controls.Add(this.tabPageCelular);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(716, 616);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageCamera
            // 
            this.tabPageCamera.Controls.Add(this.lblCam2);
            this.tabPageCamera.Controls.Add(this.lblCam1);
            this.tabPageCamera.Controls.Add(this.rtxtBoxCam2);
            this.tabPageCamera.Controls.Add(this.rtxtBoxCam1);
            this.tabPageCamera.Controls.Add(this.rtxtBoxCam);
            this.tabPageCamera.Controls.Add(this.groupBoxConfirm);
            this.tabPageCamera.Controls.Add(this.groupBoxStation);
            this.tabPageCamera.Controls.Add(this.lblCameraDetails);
            this.tabPageCamera.Controls.Add(this.lblCameraTitle);
            this.tabPageCamera.Controls.Add(this.picWebCamLeft);
            this.tabPageCamera.Location = new System.Drawing.Point(4, 30);
            this.tabPageCamera.Name = "tabPageCamera";
            this.tabPageCamera.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCamera.Size = new System.Drawing.Size(708, 582);
            this.tabPageCamera.TabIndex = 0;
            this.tabPageCamera.Text = "Configure Cameras";
            this.tabPageCamera.UseVisualStyleBackColor = true;
            // 
            // lblCam2
            // 
            this.lblCam2.AutoSize = true;
            this.lblCam2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCam2.Location = new System.Drawing.Point(30, 698);
            this.lblCam2.Name = "lblCam2";
            this.lblCam2.Size = new System.Drawing.Size(67, 13);
            this.lblCam2.TabIndex = 23;
            this.lblCam2.Text = "Camera 2 :";
            // 
            // lblCam1
            // 
            this.lblCam1.AutoSize = true;
            this.lblCam1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCam1.Location = new System.Drawing.Point(30, 644);
            this.lblCam1.Name = "lblCam1";
            this.lblCam1.Size = new System.Drawing.Size(67, 13);
            this.lblCam1.TabIndex = 22;
            this.lblCam1.Text = "Camera 1 :";
            // 
            // rtxtBoxCam2
            // 
            this.rtxtBoxCam2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtBoxCam2.Location = new System.Drawing.Point(33, 714);
            this.rtxtBoxCam2.Name = "rtxtBoxCam2";
            this.rtxtBoxCam2.Size = new System.Drawing.Size(647, 36);
            this.rtxtBoxCam2.TabIndex = 21;
            this.rtxtBoxCam2.Text = "";
            // 
            // rtxtBoxCam1
            // 
            this.rtxtBoxCam1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtBoxCam1.Location = new System.Drawing.Point(33, 660);
            this.rtxtBoxCam1.Name = "rtxtBoxCam1";
            this.rtxtBoxCam1.Size = new System.Drawing.Size(647, 36);
            this.rtxtBoxCam1.TabIndex = 20;
            this.rtxtBoxCam1.Text = "";
            // 
            // rtxtBoxCam
            // 
            this.rtxtBoxCam.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtBoxCam.Location = new System.Drawing.Point(33, 590);
            this.rtxtBoxCam.Name = "rtxtBoxCam";
            this.rtxtBoxCam.Size = new System.Drawing.Size(647, 40);
            this.rtxtBoxCam.TabIndex = 19;
            this.rtxtBoxCam.Text = "";
            // 
            // groupBoxConfirm
            // 
            this.groupBoxConfirm.Controls.Add(this.btnNo);
            this.groupBoxConfirm.Controls.Add(this.btnYes);
            this.groupBoxConfirm.Location = new System.Drawing.Point(430, 500);
            this.groupBoxConfirm.Name = "groupBoxConfirm";
            this.groupBoxConfirm.Size = new System.Drawing.Size(140, 73);
            this.groupBoxConfirm.TabIndex = 18;
            this.groupBoxConfirm.TabStop = false;
            this.groupBoxConfirm.Visible = false;
            // 
            // btnNo
            // 
            this.btnNo.Location = new System.Drawing.Point(7, 28);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(60, 30);
            this.btnNo.TabIndex = 7;
            this.btnNo.Text = "No";
            this.btnNo.UseVisualStyleBackColor = true;
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(73, 28);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(60, 30);
            this.btnYes.TabIndex = 6;
            this.btnYes.Text = "Yes";
            this.btnYes.UseVisualStyleBackColor = true;
            // 
            // groupBoxStation
            // 
            this.groupBoxStation.Controls.Add(this.rbLeftCam);
            this.groupBoxStation.Controls.Add(this.btnConfirmStation);
            this.groupBoxStation.Controls.Add(this.rbRightCam);
            this.groupBoxStation.Location = new System.Drawing.Point(33, 500);
            this.groupBoxStation.Name = "groupBoxStation";
            this.groupBoxStation.Size = new System.Drawing.Size(364, 73);
            this.groupBoxStation.TabIndex = 17;
            this.groupBoxStation.TabStop = false;
            // 
            // rbLeftCam
            // 
            this.rbLeftCam.AutoSize = true;
            this.rbLeftCam.Location = new System.Drawing.Point(9, 28);
            this.rbLeftCam.Name = "rbLeftCam";
            this.rbLeftCam.Size = new System.Drawing.Size(116, 25);
            this.rbLeftCam.TabIndex = 4;
            this.rbLeftCam.TabStop = true;
            this.rbLeftCam.Text = "Left Station";
            this.rbLeftCam.UseVisualStyleBackColor = true;
            // 
            // btnConfirmStation
            // 
            this.btnConfirmStation.Location = new System.Drawing.Point(265, 25);
            this.btnConfirmStation.Name = "btnConfirmStation";
            this.btnConfirmStation.Size = new System.Drawing.Size(90, 31);
            this.btnConfirmStation.TabIndex = 6;
            this.btnConfirmStation.Text = "Confirm";
            this.btnConfirmStation.UseVisualStyleBackColor = true;
            // 
            // rbRightCam
            // 
            this.rbRightCam.AutoSize = true;
            this.rbRightCam.Location = new System.Drawing.Point(131, 28);
            this.rbRightCam.Name = "rbRightCam";
            this.rbRightCam.Size = new System.Drawing.Size(128, 25);
            this.rbRightCam.TabIndex = 5;
            this.rbRightCam.TabStop = true;
            this.rbRightCam.Text = "Right Station";
            this.rbRightCam.UseVisualStyleBackColor = true;
            // 
            // lblCameraDetails
            // 
            this.lblCameraDetails.AutoSize = true;
            this.lblCameraDetails.Location = new System.Drawing.Point(29, 472);
            this.lblCameraDetails.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblCameraDetails.Name = "lblCameraDetails";
            this.lblCameraDetails.Size = new System.Drawing.Size(221, 21);
            this.lblCameraDetails.TabIndex = 16;
            this.lblCameraDetails.Text = "uiCameraCalibrationDetails";
            // 
            // lblCameraTitle
            // 
            this.lblCameraTitle.AutoSize = true;
            this.lblCameraTitle.Location = new System.Drawing.Point(29, 4);
            this.lblCameraTitle.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblCameraTitle.Name = "lblCameraTitle";
            this.lblCameraTitle.Size = new System.Drawing.Size(202, 21);
            this.lblCameraTitle.TabIndex = 15;
            this.lblCameraTitle.Text = "uiCameraCalibrationTitle";
            // 
            // picWebCamLeft
            // 
            this.picWebCamLeft.BackColor = System.Drawing.Color.Black;
            this.picWebCamLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picWebCamLeft.Location = new System.Drawing.Point(33, 29);
            this.picWebCamLeft.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.picWebCamLeft.Name = "picWebCamLeft";
            this.picWebCamLeft.Size = new System.Drawing.Size(647, 428);
            this.picWebCamLeft.TabIndex = 14;
            this.picWebCamLeft.TabStop = false;
            // 
            // tabPageCelular
            // 
            this.tabPageCelular.Controls.Add(this.btnReconfigure);
            this.tabPageCelular.Controls.Add(this.pbInsert2Phones);
            this.tabPageCelular.Controls.Add(this.pbRemoveRightPhone);
            this.tabPageCelular.Controls.Add(this.pbInsertRightPhone);
            this.tabPageCelular.Controls.Add(this.pbRemoveLeftPhone);
            this.tabPageCelular.Controls.Add(this.pbInsertLeftPhone);
            this.tabPageCelular.Controls.Add(this.pbRemove2Phones);
            this.tabPageCelular.Controls.Add(this.txtBoxRightCellNumber);
            this.tabPageCelular.Controls.Add(this.txtBoxLeftCellNumber);
            this.tabPageCelular.Controls.Add(this.btnInsert2Phones);
            this.tabPageCelular.Controls.Add(this.btnRemoveRightPhone);
            this.tabPageCelular.Controls.Add(this.btnInsertRightPhone);
            this.tabPageCelular.Controls.Add(this.btnRemoveLeftPhone);
            this.tabPageCelular.Controls.Add(this.btnInsertLeftPhone);
            this.tabPageCelular.Controls.Add(this.btnRemove2Phones);
            this.tabPageCelular.Controls.Add(this.lblResult);
            this.tabPageCelular.Controls.Add(this.lblInsert2Phones);
            this.tabPageCelular.Controls.Add(this.lblRemoveRightPhone);
            this.tabPageCelular.Controls.Add(this.lblRightCellNumber);
            this.tabPageCelular.Controls.Add(this.lblInsertRightPhone);
            this.tabPageCelular.Controls.Add(this.lblRemoveLeftPhone);
            this.tabPageCelular.Controls.Add(this.lblLeftCellNumber);
            this.tabPageCelular.Controls.Add(this.lblInsertLeftPhone);
            this.tabPageCelular.Controls.Add(this.lblRemove2Phones);
            this.tabPageCelular.Controls.Add(this.lblInstructions);
            this.tabPageCelular.Location = new System.Drawing.Point(4, 30);
            this.tabPageCelular.Name = "tabPageCelular";
            this.tabPageCelular.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCelular.Size = new System.Drawing.Size(708, 582);
            this.tabPageCelular.TabIndex = 1;
            this.tabPageCelular.Text = "Configure Phones";
            this.tabPageCelular.UseVisualStyleBackColor = true;
            // 
            // pbInsert2Phones
            // 
            this.pbInsert2Phones.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbInsert2Phones.BackgroundImage")));
            this.pbInsert2Phones.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbInsert2Phones.Location = new System.Drawing.Point(664, 463);
            this.pbInsert2Phones.Name = "pbInsert2Phones";
            this.pbInsert2Phones.Size = new System.Drawing.Size(27, 27);
            this.pbInsert2Phones.TabIndex = 23;
            this.pbInsert2Phones.TabStop = false;
            // 
            // pbRemoveRightPhone
            // 
            this.pbRemoveRightPhone.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbRemoveRightPhone.BackgroundImage")));
            this.pbRemoveRightPhone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbRemoveRightPhone.Location = new System.Drawing.Point(664, 410);
            this.pbRemoveRightPhone.Name = "pbRemoveRightPhone";
            this.pbRemoveRightPhone.Size = new System.Drawing.Size(27, 27);
            this.pbRemoveRightPhone.TabIndex = 22;
            this.pbRemoveRightPhone.TabStop = false;
            // 
            // pbInsertRightPhone
            // 
            this.pbInsertRightPhone.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbInsertRightPhone.BackgroundImage")));
            this.pbInsertRightPhone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbInsertRightPhone.Location = new System.Drawing.Point(664, 304);
            this.pbInsertRightPhone.Name = "pbInsertRightPhone";
            this.pbInsertRightPhone.Size = new System.Drawing.Size(27, 27);
            this.pbInsertRightPhone.TabIndex = 21;
            this.pbInsertRightPhone.TabStop = false;
            // 
            // pbRemoveLeftPhone
            // 
            this.pbRemoveLeftPhone.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbRemoveLeftPhone.BackgroundImage")));
            this.pbRemoveLeftPhone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbRemoveLeftPhone.Location = new System.Drawing.Point(664, 251);
            this.pbRemoveLeftPhone.Name = "pbRemoveLeftPhone";
            this.pbRemoveLeftPhone.Size = new System.Drawing.Size(27, 27);
            this.pbRemoveLeftPhone.TabIndex = 20;
            this.pbRemoveLeftPhone.TabStop = false;
            // 
            // pbInsertLeftPhone
            // 
            this.pbInsertLeftPhone.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbInsertLeftPhone.BackgroundImage")));
            this.pbInsertLeftPhone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbInsertLeftPhone.Location = new System.Drawing.Point(664, 140);
            this.pbInsertLeftPhone.Name = "pbInsertLeftPhone";
            this.pbInsertLeftPhone.Size = new System.Drawing.Size(27, 27);
            this.pbInsertLeftPhone.TabIndex = 19;
            this.pbInsertLeftPhone.TabStop = false;
            // 
            // pbRemove2Phones
            // 
            this.pbRemove2Phones.BackgroundImage = global::ModFactoryTestUI.Properties.Resources.warning;
            this.pbRemove2Phones.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbRemove2Phones.Location = new System.Drawing.Point(664, 92);
            this.pbRemove2Phones.Name = "pbRemove2Phones";
            this.pbRemove2Phones.Size = new System.Drawing.Size(27, 27);
            this.pbRemove2Phones.TabIndex = 18;
            this.pbRemove2Phones.TabStop = false;
            // 
            // txtBoxRightCellNumber
            // 
            this.txtBoxRightCellNumber.Enabled = false;
            this.txtBoxRightCellNumber.Location = new System.Drawing.Point(203, 356);
            this.txtBoxRightCellNumber.Name = "txtBoxRightCellNumber";
            this.txtBoxRightCellNumber.Size = new System.Drawing.Size(488, 29);
            this.txtBoxRightCellNumber.TabIndex = 17;
            // 
            // txtBoxLeftCellNumber
            // 
            this.txtBoxLeftCellNumber.Enabled = false;
            this.txtBoxLeftCellNumber.Location = new System.Drawing.Point(203, 197);
            this.txtBoxLeftCellNumber.Name = "txtBoxLeftCellNumber";
            this.txtBoxLeftCellNumber.Size = new System.Drawing.Size(488, 29);
            this.txtBoxLeftCellNumber.TabIndex = 16;
            // 
            // btnInsert2Phones
            // 
            this.btnInsert2Phones.Enabled = false;
            this.btnInsert2Phones.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsert2Phones.Location = new System.Drawing.Point(552, 463);
            this.btnInsert2Phones.Name = "btnInsert2Phones";
            this.btnInsert2Phones.Size = new System.Drawing.Size(93, 27);
            this.btnInsert2Phones.TabIndex = 15;
            this.btnInsert2Phones.Text = "OK";
            this.btnInsert2Phones.UseVisualStyleBackColor = true;
            this.btnInsert2Phones.Visible = false;
            this.btnInsert2Phones.Click += new System.EventHandler(this.btnInsert2Phones_Click);
            // 
            // btnRemoveRightPhone
            // 
            this.btnRemoveRightPhone.Enabled = false;
            this.btnRemoveRightPhone.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveRightPhone.Location = new System.Drawing.Point(552, 410);
            this.btnRemoveRightPhone.Name = "btnRemoveRightPhone";
            this.btnRemoveRightPhone.Size = new System.Drawing.Size(93, 27);
            this.btnRemoveRightPhone.TabIndex = 14;
            this.btnRemoveRightPhone.Text = "OK";
            this.btnRemoveRightPhone.UseVisualStyleBackColor = true;
            this.btnRemoveRightPhone.Visible = false;
            this.btnRemoveRightPhone.Click += new System.EventHandler(this.btnRemoveRightPhone_Click);
            // 
            // btnInsertRightPhone
            // 
            this.btnInsertRightPhone.Enabled = false;
            this.btnInsertRightPhone.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsertRightPhone.Location = new System.Drawing.Point(552, 304);
            this.btnInsertRightPhone.Name = "btnInsertRightPhone";
            this.btnInsertRightPhone.Size = new System.Drawing.Size(93, 27);
            this.btnInsertRightPhone.TabIndex = 13;
            this.btnInsertRightPhone.Text = "OK";
            this.btnInsertRightPhone.UseVisualStyleBackColor = true;
            this.btnInsertRightPhone.Visible = false;
            this.btnInsertRightPhone.Click += new System.EventHandler(this.btnInsertRightPhone_Click);
            // 
            // btnRemoveLeftPhone
            // 
            this.btnRemoveLeftPhone.Enabled = false;
            this.btnRemoveLeftPhone.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveLeftPhone.Location = new System.Drawing.Point(552, 251);
            this.btnRemoveLeftPhone.Name = "btnRemoveLeftPhone";
            this.btnRemoveLeftPhone.Size = new System.Drawing.Size(93, 27);
            this.btnRemoveLeftPhone.TabIndex = 12;
            this.btnRemoveLeftPhone.Text = "OK";
            this.btnRemoveLeftPhone.UseVisualStyleBackColor = true;
            this.btnRemoveLeftPhone.Visible = false;
            this.btnRemoveLeftPhone.Click += new System.EventHandler(this.btnRemoveLeftPhone_Click);
            // 
            // btnInsertLeftPhone
            // 
            this.btnInsertLeftPhone.Enabled = false;
            this.btnInsertLeftPhone.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsertLeftPhone.Location = new System.Drawing.Point(552, 140);
            this.btnInsertLeftPhone.Name = "btnInsertLeftPhone";
            this.btnInsertLeftPhone.Size = new System.Drawing.Size(93, 27);
            this.btnInsertLeftPhone.TabIndex = 11;
            this.btnInsertLeftPhone.Text = "OK";
            this.btnInsertLeftPhone.UseVisualStyleBackColor = true;
            this.btnInsertLeftPhone.Visible = false;
            this.btnInsertLeftPhone.Click += new System.EventHandler(this.btnInsertLeftPhone_Click);
            // 
            // btnRemove2Phones
            // 
            this.btnRemove2Phones.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove2Phones.Location = new System.Drawing.Point(552, 92);
            this.btnRemove2Phones.Name = "btnRemove2Phones";
            this.btnRemove2Phones.Size = new System.Drawing.Size(93, 27);
            this.btnRemove2Phones.TabIndex = 10;
            this.btnRemove2Phones.Text = "OK";
            this.btnRemove2Phones.UseVisualStyleBackColor = true;
            this.btnRemove2Phones.Visible = false;
            this.btnRemove2Phones.Click += new System.EventHandler(this.btnRemove2Phones_Click);
            // 
            // lblResult
            // 
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblResult.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.Location = new System.Drawing.Point(8, 507);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(683, 32);
            this.lblResult.TabIndex = 9;
            this.lblResult.Text = "lblResult";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInsert2Phones
            // 
            this.lblInsert2Phones.AutoSize = true;
            this.lblInsert2Phones.Enabled = false;
            this.lblInsert2Phones.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsert2Phones.Location = new System.Drawing.Point(8, 468);
            this.lblInsert2Phones.Name = "lblInsert2Phones";
            this.lblInsert2Phones.Size = new System.Drawing.Size(111, 17);
            this.lblInsert2Phones.TabIndex = 8;
            this.lblInsert2Phones.Text = "lblInsert2Phones";
            // 
            // lblRemoveRightPhone
            // 
            this.lblRemoveRightPhone.AutoSize = true;
            this.lblRemoveRightPhone.Enabled = false;
            this.lblRemoveRightPhone.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemoveRightPhone.Location = new System.Drawing.Point(8, 415);
            this.lblRemoveRightPhone.Name = "lblRemoveRightPhone";
            this.lblRemoveRightPhone.Size = new System.Drawing.Size(145, 17);
            this.lblRemoveRightPhone.TabIndex = 7;
            this.lblRemoveRightPhone.Text = "lblRemoveRightPhone";
            // 
            // lblRightCellNumber
            // 
            this.lblRightCellNumber.AutoSize = true;
            this.lblRightCellNumber.Enabled = false;
            this.lblRightCellNumber.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRightCellNumber.Location = new System.Drawing.Point(8, 362);
            this.lblRightCellNumber.Name = "lblRightCellNumber";
            this.lblRightCellNumber.Size = new System.Drawing.Size(130, 17);
            this.lblRightCellNumber.TabIndex = 6;
            this.lblRightCellNumber.Text = "lblRightCellNumber";
            // 
            // lblInsertRightPhone
            // 
            this.lblInsertRightPhone.AutoSize = true;
            this.lblInsertRightPhone.Enabled = false;
            this.lblInsertRightPhone.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsertRightPhone.Location = new System.Drawing.Point(8, 309);
            this.lblInsertRightPhone.Name = "lblInsertRightPhone";
            this.lblInsertRightPhone.Size = new System.Drawing.Size(131, 17);
            this.lblInsertRightPhone.TabIndex = 5;
            this.lblInsertRightPhone.Text = "lblInsertRightPhone";
            // 
            // lblRemoveLeftPhone
            // 
            this.lblRemoveLeftPhone.AutoSize = true;
            this.lblRemoveLeftPhone.Enabled = false;
            this.lblRemoveLeftPhone.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemoveLeftPhone.Location = new System.Drawing.Point(8, 256);
            this.lblRemoveLeftPhone.Name = "lblRemoveLeftPhone";
            this.lblRemoveLeftPhone.Size = new System.Drawing.Size(136, 17);
            this.lblRemoveLeftPhone.TabIndex = 4;
            this.lblRemoveLeftPhone.Text = "lblRemoveLeftPhone";
            // 
            // lblLeftCellNumber
            // 
            this.lblLeftCellNumber.AutoSize = true;
            this.lblLeftCellNumber.Enabled = false;
            this.lblLeftCellNumber.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeftCellNumber.Location = new System.Drawing.Point(8, 203);
            this.lblLeftCellNumber.Name = "lblLeftCellNumber";
            this.lblLeftCellNumber.Size = new System.Drawing.Size(121, 17);
            this.lblLeftCellNumber.TabIndex = 3;
            this.lblLeftCellNumber.Text = "lblLeftCellNumber";
            // 
            // lblInsertLeftPhone
            // 
            this.lblInsertLeftPhone.AutoSize = true;
            this.lblInsertLeftPhone.Enabled = false;
            this.lblInsertLeftPhone.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsertLeftPhone.Location = new System.Drawing.Point(8, 150);
            this.lblInsertLeftPhone.Name = "lblInsertLeftPhone";
            this.lblInsertLeftPhone.Size = new System.Drawing.Size(122, 17);
            this.lblInsertLeftPhone.TabIndex = 2;
            this.lblInsertLeftPhone.Text = "lblInsertLeftPhone";
            // 
            // lblRemove2Phones
            // 
            this.lblRemove2Phones.AutoSize = true;
            this.lblRemove2Phones.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemove2Phones.Location = new System.Drawing.Point(8, 97);
            this.lblRemove2Phones.Name = "lblRemove2Phones";
            this.lblRemove2Phones.Size = new System.Drawing.Size(125, 17);
            this.lblRemove2Phones.TabIndex = 1;
            this.lblRemove2Phones.Text = "lblRemove2Phones";
            // 
            // lblInstructions
            // 
            this.lblInstructions.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstructions.Location = new System.Drawing.Point(8, 24);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(692, 25);
            this.lblInstructions.TabIndex = 0;
            this.lblInstructions.Text = "lblInstructions";
            // 
            // bckGrdWorkerConfigCell
            // 
            this.bckGrdWorkerConfigCell.WorkerReportsProgress = true;
            this.bckGrdWorkerConfigCell.WorkerSupportsCancellation = true;
            this.bckGrdWorkerConfigCell.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bckGrdWorkerConfigCell_DoWork);
            this.bckGrdWorkerConfigCell.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bckGrdWorkerConfigCell_ProgressChanged);
            // 
            // btnReconfigure
            // 
            this.btnReconfigure.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReconfigure.Location = new System.Drawing.Point(552, 542);
            this.btnReconfigure.Name = "btnReconfigure";
            this.btnReconfigure.Size = new System.Drawing.Size(93, 27);
            this.btnReconfigure.TabIndex = 24;
            this.btnReconfigure.Text = "Reconfigure";
            this.btnReconfigure.UseVisualStyleBackColor = true;
            this.btnReconfigure.Visible = false;
            this.btnReconfigure.Click += new System.EventHandler(this.btnReconfigure_Click);
            // 
            // FormSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 616);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configurations";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSetup_FormClosing);
            this.Load += new System.EventHandler(this.FormSetup_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageCamera.ResumeLayout(false);
            this.tabPageCamera.PerformLayout();
            this.groupBoxConfirm.ResumeLayout(false);
            this.groupBoxStation.ResumeLayout(false);
            this.groupBoxStation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWebCamLeft)).EndInit();
            this.tabPageCelular.ResumeLayout(false);
            this.tabPageCelular.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInsert2Phones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRemoveRightPhone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInsertRightPhone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRemoveLeftPhone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInsertLeftPhone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRemove2Phones)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageCamera;
        private System.Windows.Forms.Label lblCam2;
        private System.Windows.Forms.Label lblCam1;
        private System.Windows.Forms.RichTextBox rtxtBoxCam2;
        private System.Windows.Forms.RichTextBox rtxtBoxCam1;
        private System.Windows.Forms.RichTextBox rtxtBoxCam;
        private System.Windows.Forms.GroupBox groupBoxConfirm;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.GroupBox groupBoxStation;
        private System.Windows.Forms.RadioButton rbLeftCam;
        private System.Windows.Forms.Button btnConfirmStation;
        private System.Windows.Forms.RadioButton rbRightCam;
        private System.Windows.Forms.Label lblCameraDetails;
        private System.Windows.Forms.Label lblCameraTitle;
        private System.Windows.Forms.PictureBox picWebCamLeft;
        private System.Windows.Forms.TabPage tabPageCelular;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblInsert2Phones;
        private System.Windows.Forms.Label lblRemoveRightPhone;
        private System.Windows.Forms.Label lblRightCellNumber;
        private System.Windows.Forms.Label lblInsertRightPhone;
        private System.Windows.Forms.Label lblRemoveLeftPhone;
        private System.Windows.Forms.Label lblLeftCellNumber;
        private System.Windows.Forms.Label lblInsertLeftPhone;
        private System.Windows.Forms.Label lblRemove2Phones;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Button btnRemove2Phones;
        private System.Windows.Forms.Button btnInsert2Phones;
        private System.Windows.Forms.Button btnRemoveRightPhone;
        private System.Windows.Forms.Button btnInsertRightPhone;
        private System.Windows.Forms.Button btnRemoveLeftPhone;
        private System.Windows.Forms.Button btnInsertLeftPhone;
        private System.Windows.Forms.TextBox txtBoxRightCellNumber;
        private System.Windows.Forms.TextBox txtBoxLeftCellNumber;
        private System.ComponentModel.BackgroundWorker bckGrdWorkerConfigCell;
        private System.Windows.Forms.PictureBox pbInsert2Phones;
        private System.Windows.Forms.PictureBox pbRemoveRightPhone;
        private System.Windows.Forms.PictureBox pbInsertRightPhone;
        private System.Windows.Forms.PictureBox pbRemoveLeftPhone;
        private System.Windows.Forms.PictureBox pbInsertLeftPhone;
        private System.Windows.Forms.PictureBox pbRemove2Phones;
        private System.Windows.Forms.Button btnReconfigure;

    }
}