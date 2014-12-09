using System;
using System.Windows.Forms;
using MiniMax.Forms;

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

        public BuildChooseDialog CudaBuildChooseDialog { get; set; }

        public int Median
        {
            get { return (int) numericUpDownMedian.Value; }
            set { numericUpDownMedian.Value = value; }
        }

        public double Gauss
        {
            get { return (double) numericUpDownGauss.Value; }
            set { numericUpDownGauss.Value = (decimal) value; }
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

        public int CudafyGridSize
        {
            get { return Convert.ToInt32(numericUpDownCudafyGridSize.Value); }
            set { numericUpDownCudafyGridSize.Value = value; }
        }

        public int CudafyBlockSize
        {
            get { return Convert.ToInt32(numericUpDownCudafyBlockSize.Value); }
            set { numericUpDownCudafyBlockSize.Value = value; }
        }

        public int CudaGridSize
        {
            get { return Convert.ToInt32(numericUpDownCudaGridSize.Value); }
            set { numericUpDownCudaGridSize.Value = value; }
        }

        public int CudaBlockSize
        {
            get { return Convert.ToInt32(numericUpDownCudaBlockSize.Value); }
            set { numericUpDownCudaBlockSize.Value = value; }
        }

        public bool IsNativeEngine
        {
            get { return radioButtonNativeEngine.Checked; }
        }

        public bool IsCudafyEngine
        {
            get { return radioButtonCudafyEngine.Checked; }
        }

        public bool IsCudaEngine
        {
            get { return radioButtonCudaEngine.Checked; }
        }

        public bool IsMpiEngine
        {
            get { return radioButtonMpiEngine.Checked; }
        }

        public BuildChooseDialog MpiBuildChooseDialog { get; set; }

        private void buttonCudaChoose_Click(object sender, EventArgs e)
        {
            if (CudaBuildChooseDialog.ShowDialog() != DialogResult.OK) return;
            MyLibrary.Collections.Properties values = CudaBuildChooseDialog.Values;
            if (values == null) return;
            CudafyGridSize = 1;
            CudafyBlockSize = Convert.ToInt32(values["N"]);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MpiBuildChooseDialog.ShowDialog() != DialogResult.OK) return;
            MyLibrary.Collections.Properties values = MpiBuildChooseDialog.Values;
            if (values == null) return;
            NumberOfProcess = Convert.ToInt32(values["P"]);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButtonNativeEngine_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void label10_Click(object sender, EventArgs e)
        {
        }
    }
}