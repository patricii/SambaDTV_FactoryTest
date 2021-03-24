namespace ModFactoryTestUI
{
    partial class FormPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.tsTop = new System.Windows.Forms.ToolStrip();
            this.tsBtnDay = new System.Windows.Forms.ToolStripButton();
            this.tsbtnMonth = new System.Windows.Forms.ToolStripButton();
            this.tsBtnFilter = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsLabelTrackId = new System.Windows.Forms.ToolStripLabel();
            this.tsTxtBoxTrackId = new System.Windows.Forms.ToolStripTextBox();
            this.tsBtnAbout = new System.Windows.Forms.ToolStripButton();
            this.tsBtnAbortTest = new System.Windows.Forms.ToolStripButton();
            this.tsBtnClear = new System.Windows.Forms.ToolStripButton();
            this.tsBtnJigState = new System.Windows.Forms.ToolStripButton();
            this.tsBtnExecution = new System.Windows.Forms.ToolStripButton();
            this.tsBtnSetup = new System.Windows.Forms.ToolStripButton();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dgvTrackId = new System.Windows.Forms.DataGridView();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rTxtBox = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvTestResult = new System.Windows.Forms.DataGridView();
            this.tsBotton = new System.Windows.Forms.ToolStrip();
            this.bkgWorkingCheckJigClosed = new System.ComponentModel.BackgroundWorker();
            this.bkgWorkingCheckJigOpen = new System.ComponentModel.BackgroundWorker();
            this.tsTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrackId)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestResult)).BeginInit();
            this.SuspendLayout();
            // 
            // tsTop
            // 
            this.tsTop.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.tsTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnDay,
            this.tsbtnMonth,
            this.tsBtnFilter,
            this.toolStripSeparator1,
            this.tsLabelTrackId,
            this.tsTxtBoxTrackId,
            this.tsBtnAbout,
            this.tsBtnSetup,
            this.tsBtnAbortTest,
            this.tsBtnClear,
            this.tsBtnJigState,
            this.tsBtnExecution});
            this.tsTop.Location = new System.Drawing.Point(0, 0);
            this.tsTop.Name = "tsTop";
            this.tsTop.Padding = new System.Windows.Forms.Padding(0, 5, 2, 5);
            this.tsTop.Size = new System.Drawing.Size(1092, 62);
            this.tsTop.TabIndex = 0;
            // 
            // tsBtnDay
            // 
            this.tsBtnDay.AutoSize = false;
            this.tsBtnDay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tsBtnDay.Image = global::ModFactoryTestUI.Properties.Resources.time_left;
            this.tsBtnDay.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsBtnDay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnDay.Name = "tsBtnDay";
            this.tsBtnDay.Size = new System.Drawing.Size(65, 49);
            this.tsBtnDay.Text = "Day";
            this.tsBtnDay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsBtnDay.ToolTipText = "Filter by current day";
            this.tsBtnDay.Click += new System.EventHandler(this.tsBtnDay_Click);
            // 
            // tsbtnMonth
            // 
            this.tsbtnMonth.AutoSize = false;
            this.tsbtnMonth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tsbtnMonth.Image = global::ModFactoryTestUI.Properties.Resources.calendar;
            this.tsbtnMonth.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnMonth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnMonth.Name = "tsbtnMonth";
            this.tsbtnMonth.Size = new System.Drawing.Size(65, 49);
            this.tsbtnMonth.Text = "Month";
            this.tsbtnMonth.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnMonth.ToolTipText = "Filter by current month";
            this.tsbtnMonth.Click += new System.EventHandler(this.tsbtnMonth_Click);
            // 
            // tsBtnFilter
            // 
            this.tsBtnFilter.AutoSize = false;
            this.tsBtnFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tsBtnFilter.Image = global::ModFactoryTestUI.Properties.Resources.filter;
            this.tsBtnFilter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsBtnFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnFilter.Name = "tsBtnFilter";
            this.tsBtnFilter.Size = new System.Drawing.Size(65, 49);
            this.tsBtnFilter.Text = "Filter";
            this.tsBtnFilter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsBtnFilter.Click += new System.EventHandler(this.tsBtnFilter_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 52);
            // 
            // tsLabelTrackId
            // 
            this.tsLabelTrackId.Name = "tsLabelTrackId";
            this.tsLabelTrackId.Size = new System.Drawing.Size(72, 49);
            this.tsLabelTrackId.Text = "TrackID:";
            // 
            // tsTxtBoxTrackId
            // 
            this.tsTxtBoxTrackId.AcceptsReturn = true;
            this.tsTxtBoxTrackId.AcceptsTab = true;
            this.tsTxtBoxTrackId.AutoSize = false;
            this.tsTxtBoxTrackId.AutoToolTip = true;
            this.tsTxtBoxTrackId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tsTxtBoxTrackId.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsTxtBoxTrackId.MaxLength = 10;
            this.tsTxtBoxTrackId.Name = "tsTxtBoxTrackId";
            this.tsTxtBoxTrackId.Size = new System.Drawing.Size(105, 29);
            this.tsTxtBoxTrackId.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tsTxtBoxTrackId.ToolTipText = "TrackId";
            this.tsTxtBoxTrackId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tsTxtBoxTrackId_KeyPress);
            // 
            // tsBtnAbout
            // 
            this.tsBtnAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsBtnAbout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tsBtnAbout.Image = global::ModFactoryTestUI.Properties.Resources.info;
            this.tsBtnAbout.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsBtnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnAbout.Name = "tsBtnAbout";
            this.tsBtnAbout.Size = new System.Drawing.Size(61, 49);
            this.tsBtnAbout.Text = "About";
            this.tsBtnAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsBtnAbout.Click += new System.EventHandler(this.tsBtnAbout_Click);
            // 
            // tsBtnAbortTest
            // 
            this.tsBtnAbortTest.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsBtnAbortTest.AutoSize = false;
            this.tsBtnAbortTest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tsBtnAbortTest.Enabled = false;
            this.tsBtnAbortTest.Image = global::ModFactoryTestUI.Properties.Resources.cancel;
            this.tsBtnAbortTest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsBtnAbortTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnAbortTest.Name = "tsBtnAbortTest";
            this.tsBtnAbortTest.Size = new System.Drawing.Size(65, 49);
            this.tsBtnAbortTest.Text = "Abort";
            this.tsBtnAbortTest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsBtnAbortTest.ToolTipText = "Abort executing test";
            this.tsBtnAbortTest.Click += new System.EventHandler(this.tsBtnAbortTest_Click);
            // 
            // tsBtnClear
            // 
            this.tsBtnClear.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsBtnClear.AutoSize = false;
            this.tsBtnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tsBtnClear.Image = global::ModFactoryTestUI.Properties.Resources.eraser;
            this.tsBtnClear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsBtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnClear.Name = "tsBtnClear";
            this.tsBtnClear.Size = new System.Drawing.Size(65, 49);
            this.tsBtnClear.Text = "Clear";
            this.tsBtnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsBtnClear.Click += new System.EventHandler(this.tsBtnClear_Click);
            // 
            // tsBtnJigState
            // 
            this.tsBtnJigState.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsBtnJigState.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tsBtnJigState.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.tsBtnJigState.Image = global::ModFactoryTestUI.Properties.Resources.cancel;
            this.tsBtnJigState.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsBtnJigState.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnJigState.Name = "tsBtnJigState";
            this.tsBtnJigState.Size = new System.Drawing.Size(81, 49);
            this.tsBtnJigState.Text = "Jig Open";
            this.tsBtnJigState.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsBtnExecution
            // 
            this.tsBtnExecution.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tsBtnExecution.Image = global::ModFactoryTestUI.Properties.Resources.greenBar;
            this.tsBtnExecution.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsBtnExecution.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsBtnExecution.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnExecution.Margin = new System.Windows.Forms.Padding(10, 0, 0, 2);
            this.tsBtnExecution.Name = "tsBtnExecution";
            this.tsBtnExecution.Size = new System.Drawing.Size(132, 50);
            this.tsBtnExecution.Text = "Executing...";
            this.tsBtnExecution.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsBtnExecution.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsBtnSetup
            // 
            this.tsBtnSetup.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsBtnSetup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tsBtnSetup.Image = global::ModFactoryTestUI.Properties.Resources.settings;
            this.tsBtnSetup.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsBtnSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSetup.Name = "tsBtnSetup";
            this.tsBtnSetup.Size = new System.Drawing.Size(76, 49);
            this.tsBtnSetup.Text = "Settings";
            this.tsBtnSetup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsBtnSetup.Click += new System.EventHandler(this.tsBtnSetup_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 62);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(5);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dgvTrackId);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
            this.splitContainer.Panel1MinSize = 205;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tabControl);
            this.splitContainer.Size = new System.Drawing.Size(1092, 505);
            this.splitContainer.SplitterDistance = 205;
            this.splitContainer.SplitterWidth = 7;
            this.splitContainer.TabIndex = 2;
            // 
            // dgvTrackId
            // 
            this.dgvTrackId.AllowUserToAddRows = false;
            this.dgvTrackId.AllowUserToDeleteRows = false;
            this.dgvTrackId.AllowUserToOrderColumns = true;
            this.dgvTrackId.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTrackId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTrackId.Location = new System.Drawing.Point(10, 5);
            this.dgvTrackId.Margin = new System.Windows.Forms.Padding(5);
            this.dgvTrackId.MultiSelect = false;
            this.dgvTrackId.Name = "dgvTrackId";
            this.dgvTrackId.ReadOnly = true;
            this.dgvTrackId.Size = new System.Drawing.Size(190, 495);
            this.dgvTrackId.TabIndex = 0;
            this.dgvTrackId.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTrackId_CellClick);
            this.dgvTrackId.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvTrackId_CellFormatting);
            this.dgvTrackId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvTrackId_KeyDown);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(5);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(880, 505);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rTxtBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(5);
            this.tabPage1.Size = new System.Drawing.Size(872, 471);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Test Summary";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rTxtBox
            // 
            this.rTxtBox.BackColor = System.Drawing.Color.DarkBlue;
            this.rTxtBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rTxtBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rTxtBox.ForeColor = System.Drawing.Color.White;
            this.rTxtBox.Location = new System.Drawing.Point(5, 5);
            this.rTxtBox.Margin = new System.Windows.Forms.Padding(5);
            this.rTxtBox.Name = "rTxtBox";
            this.rTxtBox.ReadOnly = true;
            this.rTxtBox.Size = new System.Drawing.Size(862, 461);
            this.rTxtBox.TabIndex = 0;
            this.rTxtBox.Text = "Test summary";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvTestResult);
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(5);
            this.tabPage2.Size = new System.Drawing.Size(872, 479);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Test Result";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvTestResult
            // 
            this.dgvTestResult.AllowUserToAddRows = false;
            this.dgvTestResult.AllowUserToDeleteRows = false;
            this.dgvTestResult.AllowUserToOrderColumns = true;
            this.dgvTestResult.AllowUserToResizeRows = false;
            this.dgvTestResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvTestResult.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvTestResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTestResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTestResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTestResult.Location = new System.Drawing.Point(5, 5);
            this.dgvTestResult.Margin = new System.Windows.Forms.Padding(5);
            this.dgvTestResult.MultiSelect = false;
            this.dgvTestResult.Name = "dgvTestResult";
            this.dgvTestResult.ReadOnly = true;
            this.dgvTestResult.Size = new System.Drawing.Size(862, 469);
            this.dgvTestResult.TabIndex = 0;
            this.dgvTestResult.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvTestResult_CellFormatting);
            // 
            // tsBotton
            // 
            this.tsBotton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsBotton.Location = new System.Drawing.Point(0, 567);
            this.tsBotton.Name = "tsBotton";
            this.tsBotton.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tsBotton.Size = new System.Drawing.Size(1092, 25);
            this.tsBotton.TabIndex = 1;
            this.tsBotton.Text = "toolStrip1";
            // 
            // bkgWorkingCheckJigClosed
            // 
            this.bkgWorkingCheckJigClosed.WorkerSupportsCancellation = true;
            this.bkgWorkingCheckJigClosed.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkgWorkingCheckJigClosed_DoWork);
            this.bkgWorkingCheckJigClosed.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bkgWorkingCheckJigClosed_RunWorkerCompleted);
            // 
            // bkgWorkingCheckJigOpen
            // 
            this.bkgWorkingCheckJigOpen.WorkerSupportsCancellation = true;
            this.bkgWorkingCheckJigOpen.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkgWorkingCheckJigOpen_DoWork);
            this.bkgWorkingCheckJigOpen.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bkgWorkingCheckJigOpen_RunWorkerCompleted);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 592);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.tsBotton);
            this.Controls.Add(this.tsTop);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormPrincipal_FormClosed);
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.tsTop.ResumeLayout(false);
            this.tsTop.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrackId)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsTop;
        private System.Windows.Forms.ToolStripLabel tsLabelTrackId;
        private System.Windows.Forms.ToolStripTextBox tsTxtBoxTrackId;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView dgvTrackId;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox rTxtBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvTestResult;
        private System.Windows.Forms.ToolStripButton tsBtnAbout;
        private System.Windows.Forms.ToolStripButton tsBtnDay;
        private System.Windows.Forms.ToolStripButton tsbtnMonth;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsBtnFilter;
        private System.Windows.Forms.ToolStripButton tsBtnClear;
        private System.Windows.Forms.ToolStripButton tsBtnAbortTest;
        private System.Windows.Forms.ToolStrip tsBotton;
        private System.Windows.Forms.ToolStripButton tsBtnExecution;
        private System.Windows.Forms.ToolStripButton tsBtnJigState;
        private System.ComponentModel.BackgroundWorker bkgWorkingCheckJigClosed;
        private System.ComponentModel.BackgroundWorker bkgWorkingCheckJigOpen;
        private System.Windows.Forms.ToolStripButton tsBtnSetup;
    }
}

