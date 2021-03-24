using ModFactoryTestCore.Domain.Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModFactoryTestUI
{

    public partial class FormSetup : Form
    {      
        private FormPrincipal fp;
        
        //Cameras
        int leftRightFlag = 0;
        private DirectX.Capture.Filter Camera;
        private DirectX.Capture.Filters CameraContainer;
        private DirectX.Capture.Capture CameraCaptureInfo1;
        private DirectX.Capture.Capture CameraCaptureInfo2;
        private int totalOfAvailableCameras;
        private string station = "LEFT";
        private string otherStation = "RIGHT";
        bool cam1Active = false;
        bool cam2Active = false;
        string cam1String = string.Empty;
        string cam2String = string.Empty;


        public FormSetup(FormPrincipal formPrincipal)
        {
            this.fp = formPrincipal;

            InitializeComponent();

            lblCameraTitle.Text = fp.rm.GetString("uiCameraCalibrationTitle");
            lblCameraDetails.Text = fp.rm.GetString("uiCameraCalibrationDetails");
            
            groupBoxConfirm.Visible = false;

            lblInstructions.Text =  fp.rm.GetString("uiCellCalibrationTitle");
            lblRemove2Phones.Text =  fp.rm.GetString("uiCellCalibrationRemoveCell");
            lblInsertLeftPhone.Text =  fp.rm.GetString("uiCellCalibrationInsertLeftCell");
            lblLeftCellNumber.Text =  fp.rm.GetString("uiCellCalibrationLeftCellNumber");
            lblRemoveLeftPhone.Text =  fp.rm.GetString("uiCellCalibrationRemoveLeftCell");
            lblInsertRightPhone.Text =  fp.rm.GetString("uiCellCalibrationInsertRightCell");
            lblRightCellNumber.Text =  fp.rm.GetString("uiCellCalibrationRightCellNumber");
            lblRemoveRightPhone.Text =  fp.rm.GetString("uiCellCalibrationRemoveRightCell");
            lblInsert2Phones.Text =  fp.rm.GetString("uiCellCalibrationInsertBothCell");
            lblResult.Text = fp.rm.GetString("uiCellCalibrationResultSuccess");
        }

        private void FormSetup_Load(object sender, EventArgs e)
        {
            try
            {
                CameraContainer = new DirectX.Capture.Filters();
                totalOfAvailableCameras = CameraContainer.VideoInputDevices.Count;

                for (int i = 0; i < totalOfAvailableCameras; i++)
                {
                    try
                    {
                        Camera = CameraContainer.VideoInputDevices[i];
                        if (Camera.Name.Contains("PC CAMERA"))
                        {
                            if (leftRightFlag == 0)
                            {
                                CameraCaptureInfo1 = new DirectX.Capture.Capture(Camera, null);
                                cam1String = Camera.MonikerString;
                                rtxtBoxCam1.Text = Camera.MonikerString;
                            }
                            else
                            {
                                CameraCaptureInfo2 = new DirectX.Capture.Capture(Camera, null);
                                cam2String = Camera.MonikerString;
                                rtxtBoxCam2.Text = Camera.MonikerString;
                            }
                            if (leftRightFlag >= 1) break;
                            leftRightFlag++;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                StartCam1();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
                
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(! fp.myPhoneSerialNumber.Equals(string.Empty))
            {
                leftPhoneNumber  = fp.data["SETTINGS"]["LEFT_CELL_SN"];
                rightPhoneNumber = fp.data["SETTINGS"]["RIGHT_CELL_SN"];

                txtBoxLeftCellNumber.Text = leftPhoneNumber;
                txtBoxRightCellNumber.Text = rightPhoneNumber;
                state = State.PHONES_CONFIGURED;
                btnReconfigure.Visible = true;

				pbRemove2Phones.BackgroundImage = accept;
				pbInsertLeftPhone.BackgroundImage = accept;
				pbRemoveLeftPhone.BackgroundImage = accept;
				pbInsertRightPhone.BackgroundImage = accept;
				pbRemoveRightPhone.BackgroundImage = accept;
				pbInsert2Phones.BackgroundImage = accept;
			}
			
            if(tabControl.SelectedIndex == 1)
            {
                if(! bckGrdWorkerConfigCell.IsBusy)
                    bckGrdWorkerConfigCell.RunWorkerAsync();
            }
            else
            {
                bckGrdWorkerConfigCell.CancelAsync();
            }
        }

        private void btnReconfigure_Click(object sender, EventArgs e)
        {
            if (!bckGrdWorkerConfigCell.IsBusy)
            {                
                txtBoxLeftCellNumber.Text = string.Empty;
                txtBoxRightCellNumber.Text = string.Empty;

                pbInsert2Phones.BackgroundImage = alert;
                pbInsertLeftPhone.BackgroundImage = alert;
                pbInsertRightPhone.BackgroundImage = alert;
                pbRemove2Phones.BackgroundImage = alert;
                pbRemoveLeftPhone.BackgroundImage = alert;
                pbRemoveRightPhone.BackgroundImage = alert;

                lblResult.ForeColor = Color.Black;
                lblResult.Text = string.Empty;

				btnReconfigure.Visible = false;

				runningCellConfiguration = true;
				state = State.WAIT_REMOVE_BOTH_PHONES;
				bckGrdWorkerConfigCell.RunWorkerAsync();
            }
        }

        private void FormSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bckGrdWorkerConfigCell.IsBusy)
                bckGrdWorkerConfigCell.CancelAsync();

            if (CameraCaptureInfo1 != null)
            {
                CameraCaptureInfo1.Stop();
                CameraCaptureInfo1.DisposeCapture();
                CameraCaptureInfo1.PreviewWindow = null;
                CameraCaptureInfo1.Dispose(); 
            }

            if (CameraCaptureInfo2 != null)
            {
                CameraCaptureInfo2.Stop();
                CameraCaptureInfo2.DisposeCapture();
                CameraCaptureInfo2.PreviewWindow = null;
                CameraCaptureInfo2.Dispose(); 
            }

        }
        

        #region CAMERAS
        private void StartCam2()
        {
            if (cam2Active)
                return;
           
            CameraCaptureInfo1.Stop();
            CameraCaptureInfo1.DisposeCapture();
            CameraCaptureInfo1.PreviewWindow = null;
            
            CameraCaptureInfo2.PreviewWindow = this.picWebCamLeft;
            CameraCaptureInfo2.FrameCaptureComplete += UpdateImageLeft;
           
            CameraCaptureInfo2.CaptureFrame();
            cam2Active = true;
            cam1Active = false;

            rtxtBoxCam.Text = "Camera 2: ";
            rtxtBoxCam.AppendText(cam1Active ? cam1String : cam2String); 

        }

        private void StartCam1()
        {
            if (cam1Active)
                return;

            CameraCaptureInfo2.Stop();
            CameraCaptureInfo2.DisposeCapture();
            CameraCaptureInfo2.PreviewWindow = null;
           
            CameraCaptureInfo1.PreviewWindow = this.picWebCamLeft;
            CameraCaptureInfo1.FrameCaptureComplete += UpdateImageLeft;
            
            CameraCaptureInfo1.CaptureFrame();
            cam1Active = true;
            cam2Active = false;

            rtxtBoxCam.Text = "Camera 1: ";
            rtxtBoxCam.AppendText(cam1Active ? cam1String : cam2String); 
        }
             
        private void btnNo_Click(object sender, EventArgs e)
        {
            groupBoxConfirm.Visible = false;
            groupBoxStation.Visible = true;
            lblCameraDetails.Text = fp.rm.GetString("uiCameraCalibrationDetails");

            if(cam1Active)
            {
                StartCam2();
            }
            if(cam2Active)
            {
                StartCam1();
            }

        }

        private void rbLeftCam_CheckedChanged(object sender, EventArgs e)
        {
            if(rbLeftCam.Checked)
            {
                station = "LEFT";
                otherStation = "RIGHT";               
            }

            if(rbRightCam.Checked)
            {
                station = "RIGHT";
                otherStation = "LEFT";
            }
        }
        
        private void btnConfirmStation_Click(object sender, EventArgs e)
        {
            groupBoxStation.Visible = false;

            groupBoxConfirm.Left = 34;
            groupBoxConfirm.Top = 502;
            groupBoxConfirm.Visible = true;

            lblCameraDetails.Text = fp.rm.GetString("uiCameraCalibrationDetailsConfirm") + otherStation + " station";


            if (station.Equals("LEFT"))
            {
                if (cam2Active)
                    StartCam1();
                else
                    StartCam2();
            }
            else
            {
                if (cam1Active)
                    StartCam2();
                else
                    StartCam1();
            }
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            fp.myCamString = fp.data["SETTINGS"]["SIDE"].Equals(otherStation) ? cam2String : cam1String ;
            this.Close();
        }

        public void UpdateImageLeft(PictureBox frame)
        {
        }
        
        public void UpdateImageRight(PictureBox frame)
        {
        }
        #endregion
        

        #region Cellphones
        enum State
        {
            WAIT_REMOVE_BOTH_PHONES = 0,
            WAIT_INSERT_LEFT_PHONE,
            WAIT_REMOVE_LEFT_PHONE,
            WAIT_INSERT_RIGHT_PHONE,
            WAIT_REMOVE_RIGHT_PHONE,
            WAIT_INSERT_BOTH_PHONES,
            PHONES_CONFIGURED
        }

        private volatile bool runningCellConfiguration = true;
        private State state = State.WAIT_REMOVE_BOTH_PHONES;
        private string [] serialNumbers = null;//new string[2];
        private string leftPhoneNumber = string.Empty;
        private string rightPhoneNumber = string.Empty;
        private Image alert = ModFactoryTestUI.Properties.Resources.warning;
        private Image accept = ModFactoryTestUI.Properties.Resources.accept;
        private Image error = ModFactoryTestUI.Properties.Resources.cancel;

        private void bckGrdWorkerConfigCell_DoWork(object sender, DoWorkEventArgs e)
        {
            while (runningCellConfiguration)
            {
                if (bckGrdWorkerConfigCell.CancellationPending)
                    runningCellConfiguration = false;

                bckGrdWorkerConfigCell.ReportProgress(1, (Object)state);

                Thread.Sleep(1000);
            }
        }

        private void bckGrdWorkerConfigCell_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            if (bckGrdWorkerConfigCell.CancellationPending)
                return;

            State sts = (State)e.UserState;

            switch (state)
            {
                case State.WAIT_REMOVE_BOTH_PHONES:
                    removeBothPhones();
                    break;
                case State.WAIT_INSERT_LEFT_PHONE:
                    insertLeftPhone();
                    break;
                case State.WAIT_REMOVE_LEFT_PHONE:
                    removeLeftPhone();
                    break;
                case State.WAIT_INSERT_RIGHT_PHONE:
                    insertRightPhone();
                    break;
                case State.WAIT_REMOVE_RIGHT_PHONE:
                    removeRightPhone();
                    break;
                case State.WAIT_INSERT_BOTH_PHONES:
                    insertBothPhones();
                    break;
                case State.PHONES_CONFIGURED:
                    recordPhonesId();
                    break;

                default:
                    break;
            }    
           
        }

        private void recordPhonesId()
        {
            lblResult.ForeColor = Color.DarkGreen;
            fp.leftPhoneSerialNumber = leftPhoneNumber;
            fp.rightPhoneSerialNumber = rightPhoneNumber;
            btnReconfigure.Visible = true;
            runningCellConfiguration = false;
        }

        private void insertBothPhones()
        {
            lblRemoveRightPhone.Enabled = false; btnRemoveRightPhone.Enabled = false;
            lblInsert2Phones.Enabled = true; btnInsert2Phones.Enabled = true;
            btnInsert2Phones_Click(null, null);
            Thread.Sleep(500);
        }

        private void removeRightPhone()
        {
            lblInsertRightPhone.Enabled = false; btnInsertRightPhone.Enabled = false;
            lblRemoveRightPhone.Enabled = true; btnRemoveRightPhone.Enabled = true;
            btnRemoveRightPhone_Click(null, null);
            Thread.Sleep(500);
        }

        private void insertRightPhone()
        {
            lblRemoveLeftPhone.Enabled = false; btnRemoveLeftPhone.Enabled = false;
            lblInsertRightPhone.Enabled = true; btnInsertRightPhone.Enabled = true;
            btnInsertRightPhone_Click(null, null);
            Thread.Sleep(500);
        }

        private void removeLeftPhone()
        {
            lblInsertLeftPhone.Enabled = false; btnInsertLeftPhone.Enabled = false;
            lblRemoveLeftPhone.Enabled = true; btnRemoveLeftPhone.Enabled = true;
            btnRemoveLeftPhone_Click(null, null);
            Thread.Sleep(500);
        }

        private void insertLeftPhone()
        {
            lblRemove2Phones.Enabled = false; btnRemove2Phones.Enabled = false;
            lblInsertLeftPhone.Enabled = true; btnInsertLeftPhone.Enabled = true;
            btnInsertLeftPhone_Click(null, null);
            Thread.Sleep(500);
        }

        private void removeBothPhones()
        {
            lblRemove2Phones.Enabled = true; btnRemove2Phones.Enabled = true; 
            lblInsertLeftPhone.Enabled = false; btnInsertLeftPhone.Enabled = false;
            lblLeftCellNumber.Enabled  = false; txtBoxLeftCellNumber.Enabled = false;
            lblRemoveLeftPhone.Enabled = false; btnRemoveLeftPhone.Enabled = false;
            lblInsertRightPhone.Enabled = false; btnInsertRightPhone.Enabled = false;
            lblRightCellNumber.Enabled = false; txtBoxRightCellNumber.Enabled = false;
            lblRemoveRightPhone.Enabled = false; btnRemoveRightPhone.Enabled = false;
            lblInsert2Phones.Enabled = false; btnInsert2Phones.Enabled = false;

            lblResult.Text = "Waiting to remove both phones...";

			btnRemove2Phones_Click(null, null);
            Thread.Sleep(500);
        }

        private void btnRemove2Phones_Click(object sender, EventArgs e)
        {
            AdbManager.GetSerialNumbersOfConnectedPhones(out serialNumbers);

            if (serialNumbers.Length == 0)
            {
                pbRemove2Phones.BackgroundImage = accept;
                state = State.WAIT_INSERT_LEFT_PHONE;
                lblResult.Text = "Waiting to insert left phone...";  
                return;
            }

            lblResult.Text = "Waiting to remove all phones...";                 
        }

        private void btnInsertLeftPhone_Click(object sender, EventArgs e)
        {
            AdbManager.GetSerialNumbersOfConnectedPhones(out serialNumbers);

            if (serialNumbers.Length == 1)
            {
                pbInsertLeftPhone.BackgroundImage = accept;
                state = State.WAIT_REMOVE_LEFT_PHONE;
                lblResult.Text = "Waiting to remove left phone...";

                leftPhoneNumber = serialNumbers[0];
                txtBoxLeftCellNumber.Text = leftPhoneNumber;

                return;
            }

            lblResult.Text = "Waiting to insert left phone...";  
        }

        private void btnRemoveLeftPhone_Click(object sender, EventArgs e)
        {
            AdbManager.GetSerialNumbersOfConnectedPhones(out serialNumbers);

            if (serialNumbers.Length == 0)
            {
                pbRemoveLeftPhone.BackgroundImage = accept;
                state = State.WAIT_INSERT_RIGHT_PHONE;
                lblResult.Text = "Waiting to insert right phone...";
                return;
            }

            lblResult.Text = "Waiting to remove left phone..."; 
        }

        private void btnInsertRightPhone_Click(object sender, EventArgs e)
        {
            AdbManager.GetSerialNumbersOfConnectedPhones(out serialNumbers);

            if (serialNumbers.Length == 1)
            {
                if (leftPhoneNumber.Equals(serialNumbers[0]))
                {
                    lblResult.ForeColor = Color.DarkRed;
                    lblResult.Text = "Left and right phones has the same serial number...";
                    return;
                }

                lblResult.ForeColor = Color.Black;
                pbInsertRightPhone.BackgroundImage = accept;
                state = State.WAIT_REMOVE_RIGHT_PHONE;
                lblResult.Text = "Waiting to remove right phone...";
                
                rightPhoneNumber = serialNumbers[0];
                txtBoxRightCellNumber.Text = rightPhoneNumber;

                return;
            }

            lblResult.Text = "Waiting to insert right phone...";
        }

        private void btnRemoveRightPhone_Click(object sender, EventArgs e)
        {
            AdbManager.GetSerialNumbersOfConnectedPhones(out serialNumbers);

            if (serialNumbers.Length == 0)
            {
                pbRemoveRightPhone.BackgroundImage = accept;
                state = State.WAIT_INSERT_BOTH_PHONES;
                lblResult.Text = "Waiting to insert both phones...";
                return;
            }

            lblResult.Text = "Waiting to remove right phone...";
        }

        private void btnInsert2Phones_Click(object sender, EventArgs e)
        {
            AdbManager.GetSerialNumbersOfConnectedPhones(out serialNumbers);

            if (serialNumbers.Length == 2)
            {
                if( ! (serialNumbers.Contains(leftPhoneNumber) && serialNumbers.Contains(rightPhoneNumber))  )
                {
                    lblResult.ForeColor = Color.DarkRed;
                    lblResult.Text = "The inserted phones does not have match with the previous phones calibrated.";
                    return;
                }
                
                lblResult.ForeColor = Color.Black;
                pbInsert2Phones.BackgroundImage = accept;
                state = State.PHONES_CONFIGURED;
                lblResult.Text = fp.rm.GetString("uiCellCalibrationResultSuccess");
                return;
            }

            lblResult.Text = "Waiting to insert both phones...";
        }
        #endregion

    }
}
