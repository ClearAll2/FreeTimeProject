using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;

namespace PColor
{
    public partial class Form1 : Form
    {
        
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        public Form1()
        {
            InitializeComponent();
        }

        
        private void MouseMoveTimer_Tick(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            Clipboard.Clear();
            Point cursor = new Point();
            GetCursorPos(ref cursor);

            var c = GetColorAt(cursor);
            panel1.BackColor = c;
            textBox1.Text = c.ToString();


            Bitmap croppedImage;
            Bitmap screenShot = null;
            Graphics screen;
            screenShot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                     Screen.PrimaryScreen.Bounds.Height,
                                     PixelFormat.Format32bppArgb);
            screen = Graphics.FromImage(screenShot);
            screen.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                    Screen.PrimaryScreen.Bounds.Y,
                                    0,
                                    0,
                                    Screen.PrimaryScreen.Bounds.Size,
                                    CopyPixelOperation.SourceCopy);

            
            Rectangle rect = new Rectangle(cursor.X-16, cursor.Y-16, 32, 32);
            try
            {
                croppedImage = screenShot.Clone(rect, PixelFormat.Format32bppArgb);

                pictureBox1.Image = croppedImage;
            }
            catch
            {
                //
            }
            screen.Dispose();
            screenShot.Dispose();
            
        }

        Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
        public Color GetColorAt(Point location)
        {
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }

            return screenPixel.GetPixel(0, 0);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
