using System.Drawing;
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
            imagePanel1.MouseWheel += pb1_MouseWheel;
        }

        private void pb1_MouseWheel(object sender, MouseEventArgs e)
        {
            var pb1 = (ImagePanel) (sender);
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