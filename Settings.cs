using System;
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
            Median = median;
            SCI = sci;
            Noise = noise;
        }

        public int Median
        {
            get { return (int) numericUpDownMedian.Value; }
            set { numericUpDownMedian.Value = value; }
        }

        public int SCI
        {
            get { return (int) numericUpDownSCI.Value; }
            set { numericUpDownSCI.Value = value; }
        }

        public int Noise
        {
            get { return (int) numericUpDownNoise.Value; }
            set { numericUpDownNoise.Value = value; }
        }

        public int NumberOfProcess
        {
            get { return (int) numericUpDownNumberOfProcess.Value; }
            set { numericUpDownNumberOfProcess.Value = value; }
        }

        public int VideoMemorySize
        {
            get { return Convert.ToInt32(numericUpDownVideoMemorySize.Value); }
            set { numericUpDownVideoMemorySize.Value = value; }
        }

        public int GridSize
        {
            get { return Convert.ToInt32(numericUpDownGridSize.Value); }
            set { numericUpDownGridSize.Value = value; }
        }

        public int BlockSize
        {
            get { return Convert.ToInt32(numericUpDownBlockSize.Value); }
            set { numericUpDownBlockSize.Value = value; }
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