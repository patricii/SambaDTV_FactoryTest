using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace ModFactoryTest.VisualInspection
{
    public partial class CameraGUI : Form
    {
        #region Members

        private DirectX.Capture.Filter Camera;
        private DirectX.Capture.Filters CameraContainer;
        private DirectX.Capture.Capture CameraCaptureInfo;

        private Image resultImage;
        private Image targetImage;
        private Image currentImage;
        private Image backgroundImage;
        private Capture captureFlag;

        private int objects = 0;
        private int counter = 0;
        private bool processingLedDetection = false;

        private int threshold = 255 / 4 * 3;
        public int Threshold
        {
            get { return this.threshold; }
            set { this.threshold = value; }
        }

        private int detectionArea = 0;
        public int DetectionArea
        {
            get { return this.detectionArea; }
        }

        #endregion

        #region Helpers Classes

        private enum Capture
        {
            BACKGROUND, TARGET, NOTHING
        }

        #endregion

        #region GUI Methods
        public string CameraId { get; set; }

        public CameraGUI()
        {
            InitializeComponent();
        }

        private void CameraGUI_Load(object sender, EventArgs e)
        {
            CameraContainer = new DirectX.Capture.Filters();
            try
            {
                int totalOfAvailableCameras = CameraContainer.VideoInputDevices.Count;

                for (int i = 0; i < totalOfAvailableCameras; i++)
                {
                    try
                    {
                        Camera = CameraContainer.VideoInputDevices[i];
                        if (Camera.Name.Contains("PC CAMERA") && Camera.MonikerString.Equals(CameraId))
                        {
                            CameraCaptureInfo = new DirectX.Capture.Capture(Camera, null);
                            CameraCaptureInfo.PreviewWindow = this.picWebCam;
                            CameraCaptureInfo.FrameCaptureComplete += UpdateImage;
                            CameraCaptureInfo.CaptureFrame();
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        public void UpdateImage(PictureBox frame)
        {
            try
            {
                currentImage = frame.Image;

                if (captureFlag == Capture.BACKGROUND)
                {
                    backgroundImage = currentImage;
                    captureFlag = Capture.NOTHING;
                }
                if (captureFlag == Capture.TARGET)
                { 
                    targetImage = currentImage;
                    captureFlag = Capture.NOTHING;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void buttonCaptureFrameLedOff_Click(object sender, EventArgs e)
        {
            captureFrameLedOff();
        }

        private void buttonCaptureFrameLedOn_Click(object sender, EventArgs e)
        {
            captureFrameLedOn();
        }

        private void buttonProcessLedDetection_Click(object sender, EventArgs e)
        {
            if (backgroundImage == null)
            {
                MessageBox.Show("Background Image was not captured!");
                return;
            }

            if (targetImage == null)
            {
                MessageBox.Show("Target Image was not captured!");
                return;
            }

            picWebCam.Hide();

            Thread imageProcessingWorker = new Thread(new ThreadStart(processLedDetection));
            imageProcessingWorker.Start();

            processingLedDetection = true;
        }

        #endregion

        #region ProcessDetection

        public void captureFrameLedOff()
        {
            if (CameraCaptureInfo == null)
                throw new Exception("Camera is not connected.");

            captureFlag = Capture.BACKGROUND;
            CameraCaptureInfo.CaptureFrame();
        }

        public void captureFrameLedOn()
        {
            if (CameraCaptureInfo == null)
                throw new Exception("Camera is not connected.");

            captureFlag = Capture.TARGET;
            CameraCaptureInfo.CaptureFrame();
        }

        Bitmap backgroundBitmap;
        Bitmap resultBitmap;
        Bitmap targetBitmap;

        public void processLedDetection()
        {
            //Console.WriteLine("Processing Image ({0}x{1})...", backgroundImage.Width, backgroundImage.Height);

            if (backgroundImage == null)
                throw new Exception("Background Image has not been captured.");
            if (targetImage == null)
                throw new Exception("Target Image has not been captured.");

            if (backgroundBitmap == null) { 
                backgroundBitmap = new Bitmap(backgroundImage);
                resultBitmap = new Bitmap(backgroundImage);
            }
            Thread.Sleep(100);
            targetBitmap = new Bitmap(targetImage);

            int processedPixels = 0;
            int totalOfPixels = backgroundImage.Width * backgroundImage.Height;

            for (int y = 0; y < backgroundImage.Height; y++)
            {
                for (int x = 0; x < backgroundImage.Width; x++)
                {
                    Color p1 = targetBitmap.GetPixel(x, y);
                    Color p2 = backgroundBitmap.GetPixel(x, y);

                    if (isPixelsWithTheSameColor(p1, p2))
                        resultBitmap.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                    else
                        resultBitmap.SetPixel(x, y, Color.FromArgb(255, 255, 255));

                    processedPixels++;
                }

                //Console.Write("Processing... ({0}%)\r", (float) processedPixels / totalOfPixels * 100);
                //Console.Out.Flush();
            }

            int totalOfObjects = 0;
            for (int y = 0; y < backgroundImage.Height; y++)
            {
                for (int x = 0; x < backgroundImage.Width; x++)
                {
                    Color p = resultBitmap.GetPixel(x, y);
                    if (p.R == 255 && p.G == 255 && p.B == 255)
                    {
                        totalOfObjects++;
                        if (totalOfObjects <= 255) 
                            FloodFill(resultBitmap, x, y, Color.FromArgb(totalOfObjects, totalOfObjects, totalOfObjects));
                    }
                }
            }
            //Console.WriteLine("TOTAL of Objects = {0}", totalOfObjects);

            int[] objectArea = new int[totalOfObjects];
            for (int y = 0; y < backgroundImage.Height; y++)
            {
                for (int x = 0; x < backgroundImage.Width; x++)
                {
                    Color p = resultBitmap.GetPixel(x, y);
                    if (p.R == 0)
                        continue;
                    objectArea[p.R - 1]++;
                }
            }

            int majorObjectIndex = -1, majorObjectArea = 0;
            for (int i = 0; i < totalOfObjects; i++)
            {
                //Console.WriteLine("Object #{0} -> Area: {1}", i + 1, objectArea[i]);
                if (majorObjectArea < objectArea[i])
                {
                    majorObjectArea = objectArea[i];
                    majorObjectIndex = i;
                }
            }
            detectionArea = 0;
            if (totalOfObjects > 0)
            {
                detectionArea = objectArea[majorObjectIndex];
                //Console.WriteLine("MAJOR Object #{0} -> Area: {1}", majorObjectIndex + 1, objectArea[majorObjectIndex]);
            }

            for (int y = 0; y < backgroundImage.Height; y++)
            {
                for (int x = 0; x < backgroundImage.Width; x++)
                {
                    Color p = resultBitmap.GetPixel(x, y);
                    if (p.R == (majorObjectIndex + 1))
                        resultBitmap.SetPixel(x, y, Color.FromArgb(0, 255, 0));
                    else
                        resultBitmap.SetPixel(x, y, targetBitmap.GetPixel(x, y));

                }
            }
    
            resultImage = (Image)resultBitmap;
            picResult.Image = resultImage;

            //backgroundBitmap = null;
            //backgroundImage = null;
            //resultBitmap = null;
            resultImage = null;
            targetBitmap = null;
            targetImage = null;

            processingLedDetection = false;

            //Console.WriteLine("DONE!");
        }

        public bool IsProcessing()
        {
            return processingLedDetection;
        }

        #endregion

        #region Filters

        private static Bitmap Pixelate(Bitmap image, Int32 pixelateSize)
        {
            return Pixelate(image, new Rectangle(0, 0, image.Width, image.Height), pixelateSize);
        }

        private static Bitmap Pixelate(Bitmap image, Rectangle rectangle, Int32 pixelateSize)
        {
            Bitmap pixelated = new System.Drawing.Bitmap(image.Width, image.Height);

            using (Graphics graphics = System.Drawing.Graphics.FromImage(pixelated))
                graphics.DrawImage(image, new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            for (Int32 xx = rectangle.X; xx < rectangle.X + rectangle.Width && xx < image.Width; xx += pixelateSize)
            {
                for (Int32 yy = rectangle.Y; yy < rectangle.Y + rectangle.Height && yy < image.Height; yy += pixelateSize)
                {
                    Int32 offsetX = pixelateSize / 2;
                    Int32 offsetY = pixelateSize / 2;

                    while (xx + offsetX >= image.Width) offsetX--;
                    while (yy + offsetY >= image.Height) offsetY--;

                    Color pixel = pixelated.GetPixel(xx + offsetX, yy + offsetY);

                    for (Int32 x = xx; x < xx + pixelateSize && x < image.Width; x++)
                        for (Int32 y = yy; y < yy + pixelateSize && y < image.Height; y++)
                            pixelated.SetPixel(x, y, pixel);
                }
            }

            return pixelated;
        }

        private void CheckPoint(Bitmap image, Stack<Point> points, int x, int y,
            byte oldR, byte oldG, byte oldB, byte oldA,
            byte newR, byte newG, byte newB, byte newA)
        {
            Color pixel = image.GetPixel(x, y);
            byte r = pixel.R; byte g = pixel.G;
            byte b = pixel.B; byte a = pixel.A;
            if ((r == oldR) && (g == oldG) &&
                (b == oldB) && (a == oldA))
            {
                points.Push(new Point(x, y));
                pixel = Color.FromArgb(newA, newR, newG, newB);
                image.SetPixel(x, y, pixel );
                counter++;
            } else {
                counter = 0;
            }
        }

        public void FloodFill(Bitmap image, int x, int y, Color newColor)
        {
            Color pixel = image.GetPixel(x, y);
            byte oldR = pixel.R;
            byte oldG = pixel.G;
            byte oldB = pixel.B;
            byte oldA = pixel.A;
            byte newR = newColor.R;
            byte newG = newColor.G;
            byte newB = newColor.B;
            byte newA = newColor.A;

            if ((oldR == newR) && (oldG == newG) &&
                (oldB == newB) && (oldA == newA)) return;

            Stack<Point> points = new Stack<Point>();
            points.Push(new Point(x, y));
            pixel = Color.FromArgb(newA, newR, newG, newB);
            image.SetPixel(x, y, pixel);

            while (points.Count > 0)
            {
                Point pt = points.Pop();
                if (pt.X > 0) CheckPoint(image, points, pt.X - 1, pt.Y,
                    oldR, oldG, oldB, oldA, newR, newG, newB, newA);
                if (pt.Y > 0) CheckPoint(image, points, pt.X, pt.Y - 1,
                    oldR, oldG, oldB, oldA, newR, newG, newB, newA);
                if (pt.X < image.Width - 1) CheckPoint(image, points, pt.X + 1, pt.Y,
                    oldR, oldG, oldB, oldA, newR, newG, newB, newA);
                if (pt.Y < image.Height - 1) CheckPoint(image, points, pt.X, pt.Y + 1,
                    oldR, oldG, oldB, oldA, newR, newG, newB, newA);
            }
        }

        private bool isPixelsWithTheSameColor(Color pixel1, Color pixel2)
        {
            bool result = Math.Abs(pixel1.R - pixel2.R) < threshold
                       || Math.Abs(pixel1.G - pixel2.G) < threshold
                       || Math.Abs(pixel1.B - pixel2.B) < threshold;

            return result;
        }

        #endregion
    }
}
