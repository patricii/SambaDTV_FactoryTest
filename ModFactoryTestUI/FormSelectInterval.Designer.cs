namespace ModFactoryTestUI
{
    partial class FormSelectInterval
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSelectInterval));
            this.monthCalendarStartDate = new System.Windows.Forms.MonthCalendar();
            this.monthCalendarEndDate = new System.Windows.Forms.MonthCalendar();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblInitialDate = new System.Windows.Forms.Label();
            this.lblFinalDate = new System.Windows.Forms.Label();
            this.txtBoxMessages = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.mskTxtBoxInitialDate = new System.Windows.Forms.MaskedTextBox();
            this.mskTxtBoxFinalDate = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // monthCalendarStartDate
            // 
            this.monthCalendarStartDate.Location = new System.Drawing.Point(24, 89);
            this.monthCalendarStartDate.Margin = new System.Windows.Forms.Padding(15);
            this.monthCalendarStartDate.Name = "monthCalendarStartDate";
            this.monthCalendarStartDate.TabIndex = 0;
            this.monthCalendarStartDate.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendarStartDate_DateSelected);
            // 
            // monthCalendarEndDate
            // 
            this.monthCalendarEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.monthCalendarEndDate.Location = new System.Drawing.Point(272, 89);
            this.monthCalendarEndDate.Margin = new System.Windows.Forms.Padding(15);
            this.monthCalendarEndDate.Name = "monthCalendarEndDate";
            this.monthCalendarEndDate.TabIndex = 1;
            this.monthCalendarEndDate.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendarEndDate_DateChanged);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(20, 13);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(411, 21);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "Select a valid date interval to filter trackid\'s logs file.";
            // 
            // lblInitialDate
            // 
            this.lblInitialDate.AutoSize = true;
            this.lblInitialDate.Location = new System.Drawing.Point(20, 51);
            this.lblInitialDate.Name = "lblInitialDate";
            this.lblInitialDate.Size = new System.Drawing.Size(101, 21);
            this.lblInitialDate.TabIndex = 3;
            this.lblInitialDate.Text = "Initial date: ";
            // 
            // lblFinalDate
            // 
            this.lblFinalDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFinalDate.AutoSize = true;
            this.lblFinalDate.Location = new System.Drawing.Point(268, 51);
            this.lblFinalDate.Name = "lblFinalDate";
            this.lblFinalDate.Size = new System.Drawing.Size(89, 21);
            this.lblFinalDate.TabIndex = 4;
            this.lblFinalDate.Text = "Final date:";
            // 
            // txtBoxMessages
            // 
            this.txtBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxMessages.Enabled = false;
            this.txtBoxMessages.Location = new System.Drawing.Point(24, 314);
            this.txtBoxMessages.Name = "txtBoxMessages";
            this.txtBoxMessages.Size = new System.Drawing.Size(313, 29);
            this.txtBoxMessages.TabIndex = 7;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(424, 314);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 29);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(343, 314);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 29);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // mskTxtBoxInitialDate
            // 
            this.mskTxtBoxInitialDate.Enabled = false;
            this.mskTxtBoxInitialDate.Location = new System.Drawing.Point(24, 268);
            this.mskTxtBoxInitialDate.Mask = "00/00/0000";
            this.mskTxtBoxInitialDate.Name = "mskTxtBoxInitialDate";
            this.mskTxtBoxInitialDate.Size = new System.Drawing.Size(227, 29);
            this.mskTxtBoxInitialDate.TabIndex = 10;
            this.mskTxtBoxInitialDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mskTxtBoxFinalDate
            // 
            this.mskTxtBoxFinalDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mskTxtBoxFinalDate.Enabled = false;
            this.mskTxtBoxFinalDate.Location = new System.Drawing.Point(272, 268);
            this.mskTxtBoxFinalDate.Mask = "00/00/0000";
            this.mskTxtBoxFinalDate.Name = "mskTxtBoxFinalDate";
            this.mskTxtBoxFinalDate.Size = new System.Drawing.Size(227, 29);
            this.mskTxtBoxFinalDate.TabIndex = 11;
            this.mskTxtBoxFinalDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskTxtBoxFinalDate.ValidatingType = typeof(System.DateTime);
            // 
            // FormSelectInterval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 357);
            this.Controls.Add(this.mskTxtBoxFinalDate);
            this.Controls.Add(this.mskTxtBoxInitialDate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtBoxMessages);
            this.Controls.Add(this.lblFinalDate);
            this.Controls.Add(this.lblInitialDate);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.monthCalendarEndDate);
            this.Controls.Add(this.monthCalendarStartDate);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormSelectInterval";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose Interval";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendarStartDate;
        private System.Windows.Forms.MonthCalendar monthCalendarEndDate;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblInitialDate;
        private System.Windows.Forms.Label lblFinalDate;
        private System.Windows.Forms.TextBox txtBoxMessages;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.MaskedTextBox mskTxtBoxInitialDate;
        private System.Windows.Forms.MaskedTextBox mskTxtBoxFinalDate;
    }
}