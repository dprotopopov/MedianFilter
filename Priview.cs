using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YLScsImage;

namespace img
{
    public partial class Priview : Form
    {
        public Priview(Bitmap bmp = null)
        {
            InitializeComponent();
            imagePanel1.Image = bmp;
            imagePanel1.MouseWheel += new MouseEventHandler(pb1_MouseWheel);
        }

        void pb1_MouseWheel(object sender, MouseEventArgs e)
        {
            ImagePanel pb1 = (ImagePanel)(sender);
            if (e.Delta > 0)
            {
                if (pb1.Image != null)
                {
                    pb1.Zoom += 0.05f;
                }
            }
            else
            {
                if (pb1.Image != null)
                {
                    pb1.Zoom -= 0.05f;
                }
            }
        }
    }
}
