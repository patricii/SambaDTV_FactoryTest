using ModFactoryTestCore;
using System;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using System.Linq;

using ModFactoryTestCore.Domain.Tool;
using System.Collections.Generic;
using ModFactoryTestCore.Domain.Test;
using System.ComponentModel;

using log4net;
using log4net.Config;
using log4net.Appender;
using System.IO;
using ModFactoryTestCore.Domain;
using IniParser;
using IniParser.Model;
using ModFactoryTest.VisualInspection.Tool;
using System.Threading;

namespace ModFactoryTestUI
{
    public partial class FormPrincipal : Form 
    {
        private TestCoreController tc;
        private Assembly a;
        private TestCoreMessages.StationType stationType;
        BindingList<MyTestSummary> bindingListTS;
        BindingList<MyTestResult> bindingListTR;
        BindingList<TestResult> bindingListTestResult;
        private bool isOkToRunTests = false;
        public bool flagTestFail = false;
        private string filesToShow = "*_TS_*.log";
        private FileIniDataParser parser;
        public IniData data;
        private static readonly string CONFIG_APP_FILE = "C:/prod/Config/TestConfig.ini";
        private int selectedRowIndex = 0;

        public string TrackId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public ResourceManager rm { get; set; }
        public string myCamString  { get; set; }
        public string myPhoneSerialNumber { get; set; }
        public string leftPhoneSerialNumber { get; set; }
        public string rightPhoneSerialNumber { get; set; }

                     
        delegate int SetTextCallback(TestCoreMessages.TypeMessage type, string text);



        public FormPrincipal()
        {
            InitializeComponent(); 

            tsBtnExecution.Visible = false;

            //Access to Resource Manager
            a = Assembly.Load("ModFactoryTestCore");
            rm = new ResourceManager("ModFactoryTestCore.en-US", a);

            //Access to IniParser
            parser = new FileIniDataParser();
            data = parser.ReadFile(CONFIG_APP_FILE);
            
            RemoveDraftFiles();
                       
            dgvTrackId_configure();
            dgvTestResult_configure();

            initializeBindingList();

            myCamString = data["SETTINGS"]["CAM_STRING"];

            if (data["SETTINGS"]["SIDE"].Equals("LEFT"))
                myPhoneSerialNumber = data["SETTINGS"]["LEFT_CELL_SN"];
            else
                myPhoneSerialNumber = data["SETTINGS"]["RIGHT_CELL_SN"];
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            this.Text = "Motorola - MOD Factory Test - " + 
                String.Format("Version {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());

            this.dgvTestResult.DefaultCellStyle.Font = new Font("Consolas", 12);
            dgvTrackId.DataSource = null;
            dgvTrackId.DataSource = bindingListTS;
            bindingListTS.Clear();
            stationType = (TestCoreMessages.StationType)Enum.Parse(typeof(TestCoreMessages.StationType), data["SETTINGS"]["STATION"]);

            //Find all today trackids and put on datagridview.....
            tsBtnDay_Click(null, null);

            //Configure station screen
            ConfigureScreen(data["SETTINGS"] ["SIDE"]);

            tsTxtBoxTrackId.AcceptsReturn = true;
            tsTxtBoxTrackId.AcceptsTab = true;

            tsTxtBoxTrackId.Focus();

            //Instanciate TestCoreController and TestSuite
            try
            {
                tc = new TestCoreController(WriteTestSummary);
            }
            catch (Exception ex)
            {
                WriteTestSummary(TestCoreMessages.TypeMessage.ERROR, ex.Message);
                WriteTestSummary(TestCoreMessages.TypeMessage.ERROR, "TestCoreController = null");
                isOkToRunTests = false;
                return;
            }
        }

        private void FormPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            RemoveDraftFiles();
            //AdbManager.cleanup();
        }

        private void RemoveDraftFiles()
        {
            //Removing draft files
            var dir = new DirectoryInfo(data["SETTINGS"] ["PATH_LOG_TEST"].ToLower());

            foreach (var file in dir.EnumerateFiles("*XXXX.log"))
            {
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }

        private void initializeBindingList()
        {
            // Create the new BindingList of Part type.
            bindingListTS = new BindingList<MyTestSummary>();
            bindingListTR = new BindingList<MyTestResult>();
            bindingListTestResult = new BindingList<TestResult>();

            // Allow new parts to be added    
            bindingListTS.AllowNew = true;
            bindingListTR.AllowNew = true;
            bindingListTestResult.AllowNew = true;

            // Raise ListChanged events when new parts are added.
            bindingListTS.RaiseListChangedEvents = true;
            bindingListTR.RaiseListChangedEvents = true;
            bindingListTestResult.RaiseListChangedEvents = true;

            // Do not allow parts to be edited.
            bindingListTS.AllowEdit = false;
            bindingListTR.AllowEdit = false;
            bindingListTestResult.AllowEdit = false; 
        }
                  
        private void ConfigureScreen(string position)
        {
            this.StartPosition = FormStartPosition.WindowsDefaultBounds;
            this.WindowState = FormWindowState.Maximized;        
            int currentWidth = this.Width;
            int currentHeight = this.Height;
            this.WindowState = FormWindowState.Normal;
            this.Size = new Size(currentWidth / 2, currentHeight);
            this.StartPosition = FormStartPosition.Manual;

            if (position.Equals("RIGHT"))
                this.Location = new Point(currentWidth / 2, 0);
            else
                this.Location = new Point(0, 0);
        }
        
        public int WriteTestSummary(TestCoreMessages.TypeMessage type, string message)
        {
            if (this.rTxtBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(WriteTestSummary);
                this.Invoke(d, new object[] { type, message });
            }
            else
            {
                switch (type)
                {
                    case TestCoreMessages.TypeMessage.ERROR:                    
                        rTxtBox.BackColor = Color.DarkRed;
                        flagTestFail = true;
                        break;
                    case TestCoreMessages.TypeMessage.WARNING:
                        if(!flagTestFail)
                            rTxtBox.BackColor = Color.DarkBlue;
                        break;
                    case TestCoreMessages.TypeMessage.SUCCESS:
                        if (!flagTestFail)
                            rTxtBox.BackColor = Color.DarkGreen;
                        break;
                    case TestCoreMessages.TypeMessage.CANCELED_BY_USER:
                    case TestCoreMessages.TypeMessage.UPDATE_DGV_TRACKID:
                        dgvTrackId_update(message);
                        break;
                    case TestCoreMessages.TypeMessage.REQUEST_USER_ACTION:
                        ShowMessageBox(rm.GetString("uiUserActionRequired"), message);
                        break;
                    case TestCoreMessages.TypeMessage.START_CAMERA:
                        startCamera();
                        break;
                    case TestCoreMessages.TypeMessage.STOP_CAMERA:
                        stopCamera();
                        break;
                    case TestCoreMessages.TypeMessage.GET_FRAME_LED_ON:
                        AutomatedInspection.captureFrameWithLedTurnedOn();
                        break;
                    case TestCoreMessages.TypeMessage.GET_FRAME_LED_OFF:
                        AutomatedInspection.captureFrameWithLedTurnedOff();
                        break;
                    case TestCoreMessages.TypeMessage.PROCESS_DETECTION:
                        AutomatedInspection.processLedDetection();
                        break;
                    case TestCoreMessages.TypeMessage.UPDATE_TEST_PERCENTUAL:
                        if(tc.runTest)
                            tsBtnExecution.Text = "Executing... " + message + "%";
                        break;

                    default:
                        break;
                }

                if (type != TestCoreMessages.TypeMessage.UPDATE_TEST_PERCENTUAL)
                {
                    rTxtBox.AppendText("\n" + "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] - " + message);
                    rTxtBox.ScrollToCaret();
                }
            }            

            return 0;
        }
        
        public void ShowMessageBox(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tsTxtBoxTrackId_KeyPress(object sender, KeyPressEventArgs e)
        {
            flagTestFail = false;
            rTxtBox.Clear();
            rTxtBox.BackColor = Color.DarkBlue;           
            
            if ((e.KeyChar == (char)9) || (e.KeyChar == (char)13))//Enter key pressed.
            {
                isOkToRunTests = false;
                tsBtnAbortTest.Enabled = true;
                //CheckIfJigIsClosed();

                try
                {
                    tsTxtBoxTrackId.Focus();
                    tsTxtBoxTrackId.Enabled = false;

                    if (tsTxtBoxTrackId.TextLength != 10)
                    {
                        WriteTestSummary(TestCoreMessages.TypeMessage.ERROR, rm.GetString("uiInvalidTrackId") + " - " + tsTxtBoxTrackId);
                        isOkToRunTests = false;
                        tsTxtBoxTrackId.Enabled = true;
                        return;
                    }

                    tc.StartLog4Net(tsTxtBoxTrackId.Text);

                    dgvTrackId.ClearSelection();
                    dgvTrackId.CurrentCell = null;
                    dgvTrackId.Enabled = false;

                    CheckIfJigIsClosed();                   
                   
                }
                catch (Exception ex)
                {
                    int retCode = WriteTestSummary(TestCoreMessages.TypeMessage.ERROR, ex.GetType().ToString() + "\n\t" + ex.Message);

                    tsTxtBoxTrackId.Enabled = true;
                    tsTxtBoxTrackId.Focus();
                    dgvTrackId.Enabled = true;
                    dgvTrackId.ClearSelection();
                    dgvTrackId.CurrentCell = null;
                    tsBtnAbortTest.Enabled = false;
                    tsBtnExecution.Visible = false;
                }
            }
        }
        
        private void tsBtnAbout_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void tsBtnClear_Click(object sender, EventArgs e)
        {
            tsBtnDay_Click(null, null);
            tsTxtBoxTrackId.Focus();
            rTxtBox.Clear();
            rTxtBox.BackColor = Color.DarkBlue;
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvTrackId.CurrentCell != null)
                selectedRowIndex = dgvTrackId.CurrentCell.RowIndex;

            if (tabControl.SelectedIndex == 0)
            {
                bindingListTR.Clear();
                dgvTrackId.DataSource = null;
                filesToShow = "*_TS_*.log";
                findFilesOnDir();
                //tsBtnDay_Click(null, null);
                if (dgvTrackId.Rows.Count > 0 && selectedRowIndex <= dgvTrackId.Rows.Count)
                {
                    dgvTrackId.Rows[selectedRowIndex].Selected = true;
                    dgvTrackId_CellClick(this, new DataGridViewCellEventArgs(0, selectedRowIndex)); 
                }
            }
            else
            {
                bindingListTS.Clear();
                dgvTrackId.DataSource = null;
                dgvTestResult.DataSource = null;
                filesToShow = "*_TR_*.log";
                findFilesOnDir();
                //tsBtnDay_Click(null, null);
                if (dgvTrackId.Rows.Count > 0 && selectedRowIndex <= dgvTrackId.Rows.Count)
                {
                    dgvTrackId.Rows[selectedRowIndex].Selected = true;
                    dgvTrackId_CellClick(this, new DataGridViewCellEventArgs(0, selectedRowIndex));
                }                
            }
        }

        private void tsBtnAbortTest_Click(object sender, EventArgs e)
        {
            if (tc != null)
            {
                tc.runTest = false;
                tsBtnAbortTest.Enabled = false;
                tsBtnExecution.Text = "Aborting...";
                //tsBtnExecution.Image = tsBtnExecution.Image = ModFactoryTestUI.Properties.Resources.redBar;
                tsBtnExecution.Image = ModFactoryTestUI.Properties.Resources.redBar;

                if (bkgWorkingCheckJigClosed.IsBusy)
                    bkgWorkingCheckJigClosed.CancelAsync();
            }
        }

        
        #region DataGridView
        private void dgvTrackId_configure()
        {
            dgvTrackId.Columns.Clear();
            dgvTrackId.AutoGenerateColumns = false;
            dgvTrackId.MultiSelect = false;
            dgvTrackId.RowHeadersVisible = false;
            dgvTrackId.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTrackId.AllowUserToResizeRows = false;

            var colTrackid = new DataGridViewTextBoxColumn();
            colTrackid.HeaderText = "TRACK ID";
            colTrackid.DataPropertyName = "TrackIdNumber";
            colTrackid.SortMode = DataGridViewColumnSortMode.NotSortable;
            colTrackid.ReadOnly = true;

            dgvTrackId.Columns.Add(colTrackid);
        }
        
        private void dgvTestResult_configure()
        {
            dgvTestResult.AutoGenerateColumns = false;
            dgvTestResult.MultiSelect = false;
            dgvTestResult.RowHeadersVisible = false;

            var colCode = new DataGridViewTextBoxColumn();
            colCode.HeaderText = "TEST CODE";
            colCode.DataPropertyName = "Code";
            colCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            colCode.ReadOnly = true;
            dgvTestResult.Columns.Add(colCode);

            var colDescription = new DataGridViewTextBoxColumn();
            colDescription.HeaderText = "DESCRIPTION";
            colDescription.DataPropertyName = "Description";
            colDescription.SortMode = DataGridViewColumnSortMode.NotSortable;
            colDescription.ReadOnly = true;
            dgvTestResult.Columns.Add(colDescription);

            var colValue = new DataGridViewTextBoxColumn();
            colValue.HeaderText = "VALUE";
            colValue.DataPropertyName = "Value";
            colValue.SortMode = DataGridViewColumnSortMode.NotSortable;
            colValue.ReadOnly = true;
            dgvTestResult.Columns.Add(colValue);

            var colHightLimit = new DataGridViewTextBoxColumn();
            colHightLimit.HeaderText = "HIGHT LIMIT";
            colHightLimit.DataPropertyName = "HightLimit";
            colHightLimit.SortMode = DataGridViewColumnSortMode.NotSortable;
            colHightLimit.ReadOnly = true;
            dgvTestResult.Columns.Add(colHightLimit);

            var colLowLimit = new DataGridViewTextBoxColumn();
            colLowLimit.HeaderText = "LOW LIMIT";
            colLowLimit.DataPropertyName = "LowLimit";
            colLowLimit.SortMode = DataGridViewColumnSortMode.NotSortable;
            colLowLimit.ReadOnly = true;
            dgvTestResult.Columns.Add(colLowLimit);

            var colY_hightLimit = new DataGridViewTextBoxColumn();
            colY_hightLimit.HeaderText = "Y HIGHT LIMIT";
            colY_hightLimit.DataPropertyName = "Y_HightLimit";
            colY_hightLimit.SortMode = DataGridViewColumnSortMode.NotSortable;
            colY_hightLimit.ReadOnly = true;
            dgvTestResult.Columns.Add(colY_hightLimit);

            var colY_lowLimit = new DataGridViewTextBoxColumn();
            colY_lowLimit.HeaderText = "Y LOW LIMIT";
            colY_lowLimit.DataPropertyName = "Y_LowLimit";
            colY_lowLimit.SortMode = DataGridViewColumnSortMode.NotSortable;
            colY_lowLimit.ReadOnly = true;
            dgvTestResult.Columns.Add(colY_lowLimit);

            var colresult = new DataGridViewTextBoxColumn();
            colresult.HeaderText = "RESULT";
            colresult.DataPropertyName = "Result";
            colresult.SortMode = DataGridViewColumnSortMode.NotSortable;
            colresult.ReadOnly = true;
            dgvTestResult.Columns.Add(colresult);

            var colUnits = new DataGridViewTextBoxColumn();
            colUnits.HeaderText = "UNITS";
            colUnits.DataPropertyName = "Units";
            colUnits.SortMode = DataGridViewColumnSortMode.NotSortable;
            colUnits.ReadOnly = true;
            dgvTestResult.Columns.Add(colUnits);

            var colErrorMessage = new DataGridViewTextBoxColumn();
            colErrorMessage.HeaderText = "ERROR MESSAGE";
            colErrorMessage.DataPropertyName = "Error_message";
            colErrorMessage.SortMode = DataGridViewColumnSortMode.NotSortable;
            colErrorMessage.ReadOnly = true;
            dgvTestResult.Columns.Add(colErrorMessage);
        }
        
        private void dgvTrackId_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                try
                {
                    MyTestSummary tid = (MyTestSummary)dgvTrackId.Rows[e.RowIndex].DataBoundItem;

                    if (tid != null)
                    {
                        rTxtBox.BackColor = tid.FilePath.Contains("PASS") ? rTxtBox.BackColor = Color.DarkGreen : rTxtBox.BackColor = Color.DarkRed;
                        rTxtBox.Text = File.ReadAllText(tid.FilePath);
                    }
                }
                catch (Exception)
                {
                    rTxtBox.Text = rm.GetString("uiCantReadTrackidFile");
                }
            }
            else
            {
                try
                {
                    MyTestResult tr = (MyTestResult)dgvTrackId.Rows[e.RowIndex].DataBoundItem;

                    if(tr != null)
                    {
                        string json = File.ReadAllText(tr.FilePath);
                        tr.MyTestResultList = TestCaseBase.ToObject(json);

                        bindingListTestResult.Clear();

                        foreach (var item in tr.MyTestResultList)
                        {
                            bindingListTestResult.Add(item);
                        }

                        dgvTestResult.DataSource = null;
                        dgvTestResult.DataSource = bindingListTestResult;
                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void dgvTrackId_update(string newLogFile)
        {
            if (newLogFile != null)
            {
                bindingListTS.Insert(0, new MyTestSummary(newLogFile));
            }

            //tsTxtBoxTrackId.Clear();
            //tsTxtBoxTrackId.Enabled = true;
            //tsTxtBoxTrackId.Focus();
            tsBtnAbortTest.Enabled = false;
            tsBtnExecution.Visible = false;

            dgvTrackId.Enabled = true;
            dgvTrackId.ClearSelection();
            dgvTrackId.CurrentCell = null;

            rTxtBox.BackColor = newLogFile.Contains("PASS") ? Color.DarkGreen : Color.DarkRed;
            this.Update();  
            
            CheckIfJigIsOpen();                    
        }

        private void dgvTrackId_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData & Keys.KeyCode)
            {
                case Keys.Up:
                case Keys.Right:
                case Keys.Down:
                case Keys.Left:
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        private void dgvTrackId_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow myRow in dgvTrackId.Rows)
            {
                if (tabControl.SelectedIndex == 0)
                {
                    MyTestSummary myTrackId = (MyTestSummary)myRow.DataBoundItem;

                    if (myTrackId.FilePath.Contains("PASS"))
                        myRow.DefaultCellStyle.ForeColor = Color.Green;
                    else
                        myRow.DefaultCellStyle.ForeColor = Color.Red;
                }
                else
                {
                    MyTestResult myTestResult = (MyTestResult)myRow.DataBoundItem;

                    if (myTestResult.FilePath.Contains("PASS"))
                        myRow.DefaultCellStyle.ForeColor = Color.Green;
                    else
                        myRow.DefaultCellStyle.ForeColor = Color.Red;
                }
            }
        }

		private void dgvTestResult_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			foreach (DataGridViewRow myRow in dgvTestResult.Rows)
			{
                TestResult myTestResult =(TestResult)myRow.DataBoundItem;

                if (myTestResult.Result.Equals("PASS"))
                {
                    myRow.Cells[2].Style.BackColor = Color.LightGreen;
                    myRow.Cells[3].Style.BackColor = Color.LightGreen;
                    myRow.Cells[4].Style.BackColor = Color.LightGreen;
                    myRow.Cells[5].Style.BackColor = Color.LightGreen;
                    myRow.Cells[6].Style.BackColor = Color.LightGreen;
                    myRow.Cells[7].Style.BackColor = Color.LightGreen;                    
                }
                else if (myTestResult.Result.Equals("FAIL"))
                {
                    myRow.Cells[2].Style.BackColor = Color.MistyRose;
                    myRow.Cells[3].Style.BackColor = Color.MistyRose;
                    myRow.Cells[4].Style.BackColor = Color.MistyRose;
                    myRow.Cells[5].Style.BackColor = Color.MistyRose;
                    myRow.Cells[6].Style.BackColor = Color.MistyRose;
                    myRow.Cells[7].Style.BackColor = Color.MistyRose;
                }
                else
                {
                    myRow.Cells[2].Style.BackColor = Color.White;
                    myRow.Cells[3].Style.BackColor = Color.White;
                    myRow.Cells[4].Style.BackColor = Color.White;
                    myRow.Cells[5].Style.BackColor = Color.White;
                    myRow.Cells[6].Style.BackColor = Color.White;
                    myRow.Cells[7].Style.BackColor = Color.White;
                }
			}
		}
		#endregion
                

        #region Filters

        //private void findFilesOnDir()
        //{
        //    string[] arrTrackIds = Directory.GetFiles(data["SETTINGS"]["PATH_LOG_TEST"], filesToShow);
        //    bindingListTS.Clear();
        //    bindingListTR.Clear();

        //    foreach (string f in arrTrackIds.OrderByDescending(d => d).ToArray())
        //    {
        //        Console.WriteLine(f);
        //        try
        //        {
        //            if (tabControl.SelectedIndex == 0)
        //                bindingListTS.Add(new MyTestSummary(f));
        //            else
        //                bindingListTR.Add(new MyTestResult(f));
        //        }
        //        catch (Exception ex)
        //        {
        //            if (ex.GetType() == typeof(FormatException))
        //                WriteTestSummary(TestCoreMessages.TypeMessage.WARNING, ex.Message + " - " + f);
        //            else
        //                WriteTestSummary(TestCoreMessages.TypeMessage.WARNING, ex.Message);
        //        }
        //    }

        //    dgvTrackId.ClearSelection();
        //    dgvTrackId.CurrentCell = null;
        //}


        private void findFilesOnDir()
        {
            string[] arrTrackIds = Directory.GetFiles(data["SETTINGS"]["PATH_LOG_TEST"], filesToShow);
            bindingListTS.Clear();
            bindingListTR.Clear();

            foreach (string f in arrTrackIds)
            {
                FileInfo fileinfo = new FileInfo(f);

                if ((fileinfo.LastWriteTime.Date >= startDate) && (fileinfo.LastWriteTime.Date <= endDate))
                {
                    try
                    {
                        if (tabControl.SelectedIndex == 0)
                            bindingListTS.Add(new MyTestSummary(f));
                        else
                            bindingListTR.Add(new MyTestResult(f));
                    }
                    catch (Exception ex)
                    {
                        if (ex.GetType() == typeof(FormatException))
                            WriteTestSummary(TestCoreMessages.TypeMessage.WARNING, ex.Message + " - " + f);
                        else
                            WriteTestSummary(TestCoreMessages.TypeMessage.WARNING, ex.Message);
                    }
                }
                
            }

            dgvTrackId.ClearSelection();
            dgvTrackId.CurrentCell = null;






            if (tabControl.SelectedIndex == 0)
            {
                bindingListTS = new BindingList<MyTestSummary>(bindingListTS
                    //.Where(i => i.Date >= startDate)
                .OrderByDescending(i => i.Date)
                .ToList<MyTestSummary>());

                dgvTrackId.DataSource = null;
                dgvTrackId.DataSource = bindingListTS;
                dgvTrackId.ClearSelection();
                dgvTrackId.CurrentCell = null;
                rTxtBox.Clear();
                rTxtBox.BackColor = Color.DarkBlue;
            }
            else
            {
                bindingListTR = new BindingList<MyTestResult>(bindingListTR
                    //.Where(i => i.Date >= startDate)
                .OrderByDescending(i => i.Date)
                .ToList<MyTestResult>());

                dgvTrackId.DataSource = null;
                dgvTrackId.DataSource = bindingListTR;
                dgvTrackId.ClearSelection();
                dgvTrackId.CurrentCell = null;
            }





        }

        private void tsBtnDay_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            startDate = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
            endDate = today;

            findFilesOnDir();            

            //if(tabControl.SelectedIndex == 0)
            //{
            //    bindingListTS = new BindingList<MyTestSummary>(bindingListTS
            //    //.Where(i => i.Date >= startDate)
            //    .OrderByDescending(i => i.Date)
            //    .ToList<MyTestSummary>());

            //    dgvTrackId.DataSource = null;
            //    dgvTrackId.DataSource = bindingListTS;
            //    dgvTrackId.ClearSelection();
            //    dgvTrackId.CurrentCell = null;
            //    rTxtBox.Clear();
            //    rTxtBox.BackColor = Color.DarkBlue;
            //}
            //else 
            //{
            //    bindingListTR = new BindingList<MyTestResult>(bindingListTR
            //    //.Where(i => i.Date >= startDate)
            //    .OrderByDescending(i => i.Date)
            //    .ToList<MyTestResult>());

            //    dgvTrackId.DataSource = null;
            //    dgvTrackId.DataSource = bindingListTR;
            //    dgvTrackId.ClearSelection();
            //    dgvTrackId.CurrentCell = null;                           
            //}
        }

        private void tsbtnMonth_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            startDate = new DateTime(today.Year, today.Month, 1, 0, 0, 0);
            endDate = today;

			findFilesOnDir();
			
            //if (tabControl.SelectedIndex == 0)
            //{
            //    bindingListTS = new BindingList<MyTestSummary>(bindingListTS
            //        //.Where(i => i.Date >= startDate)
            //        .OrderByDescending(i => i.Date)
            //        .ToList<MyTestSummary>());

            //    dgvTrackId.DataSource = null;
            //    dgvTrackId.DataSource = bindingListTS;
            //    dgvTrackId.ClearSelection();
            //    dgvTrackId.CurrentCell = null;
            //    rTxtBox.Clear();
            //    rTxtBox.BackColor = Color.DarkBlue; 
            //}
            //else
            //{
            //    bindingListTR = new BindingList<MyTestResult>(bindingListTR
            //    //.Where(i => i.Date >= startDate)
            //    .OrderByDescending(i => i.Date)
            //    .ToList<MyTestResult>());

            //    dgvTrackId.DataSource = null;
            //    dgvTrackId.DataSource = bindingListTR;
            //    dgvTrackId.ClearSelection();
            //    dgvTrackId.CurrentCell = null;
            //}
        }

        private void tsBtnFilter_Click(object sender, EventArgs e)
        {
            FormSelectInterval f = new FormSelectInterval(this, rm);
            f.ShowDialog();

            findFilesOnDir();

            //if (tabControl.SelectedIndex == 0)
            //{
            //    bindingListTS = new BindingList<MyTestSummary>(bindingListTS
            //            //.Where(i => i.Date >= this.startDate && i.Date <= this.endDate)
            //            .OrderByDescending(i => i.Date)
            //            .ToList<MyTestSummary>());

            //    dgvTrackId.DataSource = null;
            //    dgvTrackId.DataSource = bindingListTS;
            //    dgvTrackId.ClearSelection();
            //    dgvTrackId.CurrentCell = null;
            //    rTxtBox.Clear();
            //    rTxtBox.BackColor = Color.DarkBlue; 
            //}
            //else
            //{
            //    bindingListTR = new BindingList<MyTestResult>(bindingListTR
            //        //.Where(i => i.Date >= this.startDate && i.Date <= this.endDate)
            //        .OrderByDescending(i => i.Date)
            //        .ToList<MyTestResult>());

            //    dgvTrackId.DataSource = null;
            //    dgvTrackId.DataSource = bindingListTR;
            //    dgvTrackId.ClearSelection();
            //    dgvTrackId.CurrentCell = null;
            //}
        }
        #endregion

        
        #region JIG
        private void CheckIfJigIsClosed()
        {
            if (!bkgWorkingCheckJigClosed.IsBusy)
            {
                tsBtnJigState.Image = ModFactoryTestUI.Properties.Resources.alertCancel;

                bkgWorkingCheckJigClosed.RunWorkerAsync(tc);
            }
        }

        private void bkgWorkingCheckJigClosed_DoWork(object sender, DoWorkEventArgs e)
        {
            tc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("uiCheckIfJigIsClosed"));

            if (this.bkgWorkingCheckJigClosed.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            try
            {
                tc.Jig.CloseJig();
            }
            catch (Exception ex)
            {
                isOkToRunTests = false;
                tc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, ex.Message);
                return; 
            }

            isOkToRunTests = true;
        
            //Thread.Sleep(1000);

        }

        private void bkgWorkingCheckJigClosed_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                WriteTestSummary(TestCoreMessages.TypeMessage.ERROR, e.Error.Message);
            }

            else if(e.Cancelled)
            {
                WriteTestSummary(TestCoreMessages.TypeMessage.ERROR, "Waiting for close jig canceled by user.");
            }

            else
            {
                try
                {
                    if (!isOkToRunTests)
                    {
                        tsBtnJigState.Image = ModFactoryTestUI.Properties.Resources.cancel;
                        tsBtnJigState.Text = "Jig Open";
                        throw new Exception(rm.GetString("uiFailToStartTests"));
                    }

                    tc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("uiStartTest") + tsTxtBoxTrackId);

                    tc.runTest = true;
                    tc.TrackId = tsTxtBoxTrackId.Text;
                    tc.StartTests(tsTxtBoxTrackId.Text, stationType);
                    tsBtnAbortTest.Enabled = true;
                    tsBtnExecution.Visible = true;
                    tsBtnExecution.Text = "Executing...";
                    tsBtnExecution.Image = ModFactoryTestUI.Properties.Resources.greenBar;
                    tsBtnJigState.Image = ModFactoryTestUI.Properties.Resources.accept;
                    tsBtnJigState.Text = "Jig Closed";
                    return;
                }
                catch (Exception ex)
                {
                    int retCode = WriteTestSummary(TestCoreMessages.TypeMessage.ERROR, ex.GetType().ToString() + "\n\t" + ex.Message);

                    tsTxtBoxTrackId.Enabled = true;
                    tsTxtBoxTrackId.Focus();
                    dgvTrackId.Enabled = true;
                    dgvTrackId.ClearSelection();
                    dgvTrackId.CurrentCell = null;
                    tsBtnAbortTest.Enabled = false;
                    tsBtnExecution.Visible = false;
                    return;
                }
            }

            tsTxtBoxTrackId.Enabled = true;
            tsTxtBoxTrackId.Focus();
            dgvTrackId.Enabled = true;
            dgvTrackId.ClearSelection();
            dgvTrackId.CurrentCell = null;
            tsBtnAbortTest.Enabled = false;
            tsBtnExecution.Visible = false;
        }

        private void CheckIfJigIsOpen()
        {
            if (!bkgWorkingCheckJigOpen.IsBusy)
            {
                tsBtnJigState.Image = ModFactoryTestUI.Properties.Resources.alertAccept;
                tsBtnJigState.Text = "Jig Closed";
                bkgWorkingCheckJigOpen.RunWorkerAsync(tc);
            }
        }

        private void bkgWorkingCheckJigOpen_DoWork(object sender, DoWorkEventArgs e)
        {
            tc.NotifyUI(TestCoreMessages.TypeMessage.INFORMATION, rm.GetString("uiCheckIfJigIsOpen"));

            if (this.bkgWorkingCheckJigOpen.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            try
            {
                tc.Jig.OpenJig();
            }
            catch (Exception ex)
            {
                isOkToRunTests = false;
                tc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, ex.Message);
                return;
            }
        }

        private void bkgWorkingCheckJigOpen_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                WriteTestSummary(TestCoreMessages.TypeMessage.ERROR, e.Error.Message);
                tsTxtBoxTrackId.Clear();
                tsTxtBoxTrackId.Enabled = true;
                tsTxtBoxTrackId.Focus();
            }

            else if (e.Cancelled)
            {
                WriteTestSummary(TestCoreMessages.TypeMessage.ERROR, "Waiting for open jig canceled by user.");
                tsTxtBoxTrackId.Clear();
                tsTxtBoxTrackId.Enabled = true;
                tsTxtBoxTrackId.Focus();
            }

            else
            {
                tsTxtBoxTrackId.Clear();
                tsTxtBoxTrackId.Enabled = true;
                tsTxtBoxTrackId.Focus();
                rTxtBox.Clear();
                rTxtBox.BackColor = Color.DarkBlue;
                tsBtnJigState.Image = ModFactoryTestUI.Properties.Resources.cancel;
                tsBtnJigState.Text = "Jig Open";
            }
        }

        #endregion


        #region Camera and Cell

        public void startCamera()
        {
            int cornerX = this.Location.X;
            int cornerY = this.Location.Y;
            int sizeHeightFormCamera = 428;
            int sizeWidthFormCamera = 648;
            try
            {
                AutomatedInspection.startCamera(
                    (cornerX + this.Width / 2 - sizeWidthFormCamera / 2), 
                    (cornerY + this.Height / 2 - sizeHeightFormCamera / 2),
                    myCamString);
            }
            catch (Exception ex)
            {
                tc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, ex.Message);
                stopCamera();
            }
        }

        private void stopCamera()
        {
            try
            {
                AutomatedInspection.stopCamera();
            }
            catch (Exception ex)
            {
                tc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, ex.Message);
            }
        }

        private void tsBtnSetup_Click(object sender, EventArgs e)
        {
            FormSetup formSetup = new FormSetup(this);
            formSetup.ShowDialog();

            data["SETTINGS"]["CAM_STRING"] = myCamString;
            parser.WriteFile(CONFIG_APP_FILE, data);

            data["SETTINGS"]["LEFT_CELL_SN"] = leftPhoneSerialNumber;
            parser.WriteFile(CONFIG_APP_FILE, data);

            data["SETTINGS"]["RIGHT_CELL_SN"] = rightPhoneSerialNumber;
            parser.WriteFile(CONFIG_APP_FILE, data);

            if (data["SETTINGS"]["SIDE"].Equals("LEFT"))
                myPhoneSerialNumber = data["SETTINGS"]["LEFT_CELL_SN"];
            else
                myPhoneSerialNumber = data["SETTINGS"]["RIGHT_CELL_SN"];

        }

        #endregion
       


    }

    # region Support Classes
    public class MyTestResult
    {
        public string TrackIdNumber { get; set; }
        public string FilePath { get; set; }
		public DateTime Date { get; set; }

		public List<TestResult> MyTestResultList { get; set; }

		public MyTestResult(string filePath) //c:\\prod\\testLog\\2017-09-21_11-19-52_8888888888_TR_FAIL.log
        {
            int ctrl = 0;

            if (filePath.Contains("CANCELED"))
            {
                ctrl = 4;
            }

            this.FilePath = filePath;
			this.MyTestResultList = new List<TestResult>();

			this.TrackIdNumber = filePath.Substring(filePath.Length - (22 + ctrl), 10);

			int year = Int32.Parse(filePath.Substring(filePath.Length - (42 + ctrl), 4));
			int month = Int32.Parse(filePath.Substring(filePath.Length - (37 + ctrl), 2));
			int day = Int32.Parse(filePath.Substring(filePath.Length - (34 + ctrl), 2));
			int hour = Int32.Parse(filePath.Substring(filePath.Length - (31 + ctrl), 2));
			int min = Int32.Parse(filePath.Substring(filePath.Length - (28 + ctrl), 2));
			int sec = Int32.Parse(filePath.Substring(filePath.Length - (25 + ctrl), 2));

			this.Date = new DateTime(year, month, day, hour, min, sec);			
        }
    }
    
    public class MyTestSummary
    {
        public string FilePath { get; set; }
        public string TrackIdNumber { get; set; }
        public DateTime Date { get; set; }

        public MyTestSummary(string filePath)
        {
            int ctrl = 0;

            if(filePath.Contains("CANCELED"))
            {
                ctrl = 4;
            }

            this.FilePath = filePath;
            this.TrackIdNumber = filePath.Substring(filePath.Length-(22+ctrl), 10);

            int year = Int32.Parse(filePath.Substring(filePath.Length - (42+ctrl), 4));
            int month = Int32.Parse(filePath.Substring(filePath.Length - (37+ctrl), 2));
            int day = Int32.Parse(filePath.Substring(filePath.Length - (34 + ctrl), 2));
            int hour = Int32.Parse(filePath.Substring(filePath.Length - (31 + ctrl), 2));
            int min = Int32.Parse(filePath.Substring(filePath.Length - (28 + ctrl), 2));
            int sec = Int32.Parse(filePath.Substring(filePath.Length - (25 + ctrl), 2));

            this.Date = new DateTime(year, month, day, hour, min, sec);
        }
    }
    #endregion


}
