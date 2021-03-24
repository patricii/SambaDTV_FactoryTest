namespace ModFactoryTest.VisualInspection
{
    partial class CameraGUI
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
            this.picWebCam = new System.Windows.Forms.PictureBox();
            this.panelControls = new System.Windows.Forms.Panel();
            this.buttonProcessLedDetection = new System.Windows.Forms.Button();
            this.buttonCaptureFrameLedOn = new System.Windows.Forms.Button();
            this.buttonCaptureFrameLedOff = new System.Windows.Forms.Button();
            this.picResult = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picWebCam)).BeginInit();
            this.panelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picResult)).BeginInit();
            this.SuspendLayout();
            // 
            // picWebCam
            // 
            this.picWebCam.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picWebCam.BackColor = System.Drawing.Color.Black;
            this.picWebCam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picWebCam.Location = new System.Drawing.Point(12, 14);
            this.picWebCam.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picWebCam.Name = "picWebCam";
            this.picWebCam.Size = new System.Drawing.Size(647, 428);
            this.picWebCam.TabIndex = 0;
            this.picWebCam.TabStop = false;
            // 
            // panelControls
            // 
            this.panelControls.Controls.Add(this.buttonProcessLedDetection);
            this.panelControls.Controls.Add(this.buttonCaptureFrameLedOn);
            this.panelControls.Controls.Add(this.buttonCaptureFrameLedOff);
            this.panelControls.Location = new System.Drawing.Point(415, 14);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(244, 38);
            this.panelControls.TabIndex = 1;
            this.panelControls.Visible = false;
            // 
            // buttonProcessLedDetection
            // 
            this.buttonProcessLedDetection.Location = new System.Drawing.Point(165, 3);
            this.buttonProcessLedDetection.Name = "buttonProcessLedDetection";
            this.buttonProcessLedDetection.Size = new System.Drawing.Size(75, 33);
            this.buttonProcessLedDetection.TabIndex = 2;
            this.buttonProcessLedDetection.Text = "Process";
            this.buttonProcessLedDetection.UseVisualStyleBackColor = true;
            this.buttonProcessLedDetection.Visible = false;
            this.buttonProcessLedDetection.Click += new System.EventHandler(this.buttonProcessLedDetection_Click);
            // 
            // buttonCaptureFrameLedOn
            // 
            this.buttonCaptureFrameLedOn.Location = new System.Drawing.Point(84, 3);
            this.buttonCaptureFrameLedOn.Name = "buttonCaptureFrameLedOn";
            this.buttonCaptureFrameLedOn.Size = new System.Drawing.Size(75, 33);
            this.buttonCaptureFrameLedOn.TabIndex = 1;
            this.buttonCaptureFrameLedOn.Text = "LED ON";
            this.buttonCaptureFrameLedOn.UseVisualStyleBackColor = true;
            this.buttonCaptureFrameLedOn.Visible = false;
            this.buttonCaptureFrameLedOn.Click += new System.EventHandler(this.buttonCaptureFrameLedOn_Click);
            // 
            // buttonCaptureFrameLedOff
            // 
            this.buttonCaptureFrameLedOff.Location = new System.Drawing.Point(3, 3);
            this.buttonCaptureFrameLedOff.Name = "buttonCaptureFrameLedOff";
            this.buttonCaptureFrameLedOff.Size = new System.Drawing.Size(75, 33);
            this.buttonCaptureFrameLedOff.TabIndex = 0;
            this.buttonCaptureFrameLedOff.Text = "LED OFF";
            this.buttonCaptureFrameLedOff.UseVisualStyleBackColor = true;
            this.buttonCaptureFrameLedOff.Visible = false;
            this.buttonCaptureFrameLedOff.Click += new System.EventHandler(this.buttonCaptureFrameLedOff_Click);
            // 
            // picResult
            // 
            this.picResult.Location = new System.Drawing.Point(12, 14);
            this.picResult.Name = "picResult";
            this.picResult.Size = new System.Drawing.Size(648, 428);
            this.picResult.TabIndex = 2;
            this.picResult.TabStop = false;
            // 
            // CameraGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.ClientSize = new System.Drawing.Size(672, 456);
            this.Controls.Add(this.panelControls);
            this.Controls.Add(this.picWebCam);
            this.Controls.Add(this.picResult);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CameraGUI";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DTV Mod Factory Test - LED Visual Inspection";
            this.Load += new System.EventHandler(this.CameraGUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picWebCam)).EndInit();
            this.panelControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.PictureBox picWebCam;
        private System.Windows.Forms.PictureBox picResult;

        public System.Windows.Forms.Button buttonProcessLedDetection;
        public System.Windows.Forms.Button buttonCaptureFrameLedOn;
        public System.Windows.Forms.Button buttonCaptureFrameLedOff;
    }
}

