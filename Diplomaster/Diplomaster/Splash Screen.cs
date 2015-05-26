using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Reflection;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Diplomaster
{
    public partial class Splash_Screen : Form
    {
        Bitmap image;
        int frameCount;
        int frameCurrent = 0;

        private const int EM_SETCUEBANNER = 0x1501;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)]string lParam);

        public Splash_Screen()
        {
            InitializeComponent();

            SendMessage(textBox1.Handle, EM_SETCUEBANNER, 0, "Username");
            SendMessage(textBox2.Handle, EM_SETCUEBANNER, 0, "Password");

            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);

            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream myStream = myAssembly.GetManifestResourceStream("Diplomaster.Resources.LoadScreen.gif");
            image = new Bitmap(myStream);

            FrameDimension dimension = new FrameDimension(image.FrameDimensionsList[0]);
            frameCount = image.GetFrameCount(dimension);
        }

        private void CheckUserPassword()
        {
            new FormStart(this).Show();
            Hide();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                CheckUserPassword();
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                CheckUserPassword();
            if (e.KeyCode == Keys.Escape)
                Close();
        }


        

        //Create a Bitmpap Object.
        bool currentlyAnimating = false;

        //This method begins the animation.
        public void AnimateImage()
        {
            if (!currentlyAnimating)
            {
                //Begin the animation only once.
                ImageAnimator.Animate(image, new EventHandler(this.OnFrameChanged));
                currentlyAnimating = true;
            }

            if (frameCurrent > frameCount)
                ImageAnimator.StopAnimate(image, new EventHandler(this.OnFrameChanged));
            else
                frameCurrent++;
        }

        private void OnFrameChanged(object o, EventArgs e)
        {
            //Force a call to the Paint event handler.
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            //Begin the animation.
            AnimateImage();
            //Get the next frame ready for rendering.
            ImageAnimator.UpdateFrames();

            //Draw the next frame in the animation.
            e.Graphics.DrawImage(this.image, new Point(0, 0));
        }

    }
}
