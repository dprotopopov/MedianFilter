using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace img
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        public Settings(int median, int noise, int sci)
            : this()
        {
            this.Median = median;
            this.SCI = sci;
            this.Noise = noise;
        }

        public int Median
        {
            get
            {
                return (int)numericUpDown2.Value;
            }
            set
            {
                numericUpDown2.Value = (decimal)value;
            }
        }

        public int SCI
        {
            get
            {
                return (int)numericUpDown3.Value;
            }
            set
            {
                numericUpDown3.Value = (decimal)value;
            }
        }

        public int Noise
        {
            get
            {
                return (int)numericUpDown1.Value;
            }
            set
            {
                numericUpDown1.Value = (decimal)value;
            }
        }
        public int NumberOfProcess
        {
            get
            {
                return (int)numericUpDownNumberOfProcess.Value;
            }
            set
            {
                numericUpDownNumberOfProcess.Value = (decimal)value;
            }
        }
        public int VideoMemorySize
        {
            get
            {
                return (int)numericUpDownVideoMemorySize.Value;
            }
            set
            {
                numericUpDownVideoMemorySize.Value = (decimal)value;
            }
        }

        public bool IsNativeEngine 
        {
            get { return radioButtonNativeEngine.Checked; }
        }

        public bool IsCudafyEngine
        {
            get { return radioButtonCudafyEngine.Checked; }
        }

        public bool IsMpiEngine
        {
            get { return radioButtonMpiEngine.Checked; }
        }
    }
}