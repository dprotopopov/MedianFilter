using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace img
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            label1.Text = "Программа точечной фильтрации BMP и SCI изображений, " + Environment.NewLine + "оценка искажений после фильтрации.";
            label2.Text = "";
        }
    }
}
