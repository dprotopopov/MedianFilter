namespace img
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.numericUpDownNoise = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMedian = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSCI = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownCudaBlockSize = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCudaGridSize = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.radioButtonCudaEngine = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownCudafyBlockSize = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.numericUpDownCudafyGridSize = new System.Windows.Forms.NumericUpDown();
            this.label61 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownVideoMemorySize = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownNumberOfProcess = new System.Windows.Forms.NumericUpDown();
            this.radioButtonMpiEngine = new System.Windows.Forms.RadioButton();
            this.radioButtonCudafyEngine = new System.Windows.Forms.RadioButton();
            this.radioButtonNativeEngine = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDownGauss = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.numericUpDownErosionDilation = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMedian)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSCI)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCudaBlockSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCudaGridSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCudafyBlockSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCudafyGridSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVideoMemorySize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberOfProcess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGauss)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownErosionDilation)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(584, 570);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // numericUpDownNoise
            // 
            this.numericUpDownNoise.Location = new System.Drawing.Point(538, 79);
            this.numericUpDownNoise.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownNoise.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownNoise.Name = "numericUpDownNoise";
            this.numericUpDownNoise.Size = new System.Drawing.Size(180, 26);
            this.numericUpDownNoise.TabIndex = 2;
            // 
            // numericUpDownMedian
            // 
            this.numericUpDownMedian.Location = new System.Drawing.Point(538, 151);
            this.numericUpDownMedian.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownMedian.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDownMedian.Name = "numericUpDownMedian";
            this.numericUpDownMedian.Size = new System.Drawing.Size(180, 26);
            this.numericUpDownMedian.TabIndex = 2;
            // 
            // numericUpDownSCI
            // 
            this.numericUpDownSCI.Location = new System.Drawing.Point(537, 115);
            this.numericUpDownSCI.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownSCI.Name = "numericUpDownSCI";
            this.numericUpDownSCI.Size = new System.Drawing.Size(180, 26);
            this.numericUpDownSCI.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(403, 81);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Уровень шума:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 153);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(297, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ранг матрицы в медианном фильтре:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(380, 117);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Погрешность SCI:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.numericUpDownCudaBlockSize);
            this.groupBox1.Controls.Add(this.numericUpDownCudaGridSize);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.radioButtonCudaEngine);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.numericUpDownCudafyBlockSize);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label71);
            this.groupBox1.Controls.Add(this.numericUpDownCudafyGridSize);
            this.groupBox1.Controls.Add(this.label61);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numericUpDownVideoMemorySize);
            this.groupBox1.Controls.Add(this.numericUpDownNumberOfProcess);
            this.groupBox1.Controls.Add(this.radioButtonMpiEngine);
            this.groupBox1.Controls.Add(this.radioButtonCudafyEngine);
            this.groupBox1.Controls.Add(this.radioButtonNativeEngine);
            this.groupBox1.Location = new System.Drawing.Point(82, 285);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(699, 254);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Вычислительная система";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(551, 150);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(84, 29);
            this.button4.TabIndex = 18;
            this.button4.Text = "Опции";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(431, 152);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "x";
            // 
            // numericUpDownCudaBlockSize
            // 
            this.numericUpDownCudaBlockSize.Location = new System.Drawing.Point(453, 150);
            this.numericUpDownCudaBlockSize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownCudaBlockSize.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDownCudaBlockSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCudaBlockSize.Name = "numericUpDownCudaBlockSize";
            this.numericUpDownCudaBlockSize.Size = new System.Drawing.Size(63, 26);
            this.numericUpDownCudaBlockSize.TabIndex = 16;
            this.numericUpDownCudaBlockSize.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // numericUpDownCudaGridSize
            // 
            this.numericUpDownCudaGridSize.Location = new System.Drawing.Point(370, 150);
            this.numericUpDownCudaGridSize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownCudaGridSize.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownCudaGridSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCudaGridSize.Name = "numericUpDownCudaGridSize";
            this.numericUpDownCudaGridSize.Size = new System.Drawing.Size(55, 26);
            this.numericUpDownCudaGridSize.TabIndex = 15;
            this.numericUpDownCudaGridSize.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(326, 154);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 20);
            this.label9.TabIndex = 14;
            this.label9.Text = "Grid";
            // 
            // radioButtonCudaEngine
            // 
            this.radioButtonCudaEngine.AutoSize = true;
            this.radioButtonCudaEngine.Location = new System.Drawing.Point(32, 150);
            this.radioButtonCudaEngine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButtonCudaEngine.Name = "radioButtonCudaEngine";
            this.radioButtonCudaEngine.Size = new System.Drawing.Size(80, 24);
            this.radioButtonCudaEngine.TabIndex = 13;
            this.radioButtonCudaEngine.Text = "CUDA";
            this.radioButtonCudaEngine.UseVisualStyleBackColor = true;
            this.radioButtonCudaEngine.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(551, 197);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(84, 29);
            this.button3.TabIndex = 12;
            this.button3.Text = "Опции";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(551, 102);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 29);
            this.button2.TabIndex = 11;
            this.button2.Text = "Опции";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonCudaChoose_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(431, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "x";
            // 
            // numericUpDownCudafyBlockSize
            // 
            this.numericUpDownCudafyBlockSize.Location = new System.Drawing.Point(453, 104);
            this.numericUpDownCudafyBlockSize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownCudafyBlockSize.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDownCudafyBlockSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCudafyBlockSize.Name = "numericUpDownCudafyBlockSize";
            this.numericUpDownCudafyBlockSize.Size = new System.Drawing.Size(63, 26);
            this.numericUpDownCudafyBlockSize.TabIndex = 9;
            this.numericUpDownCudafyBlockSize.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(326, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "Grid";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(431, 106);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(16, 20);
            this.label71.TabIndex = 10;
            this.label71.Text = "x";
            // 
            // numericUpDownCudafyGridSize
            // 
            this.numericUpDownCudafyGridSize.Location = new System.Drawing.Point(370, 104);
            this.numericUpDownCudafyGridSize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownCudafyGridSize.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownCudafyGridSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCudafyGridSize.Name = "numericUpDownCudafyGridSize";
            this.numericUpDownCudafyGridSize.Size = new System.Drawing.Size(55, 26);
            this.numericUpDownCudafyGridSize.TabIndex = 8;
            this.numericUpDownCudafyGridSize.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(326, 108);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(39, 20);
            this.label61.TabIndex = 7;
            this.label61.Text = "Grid";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(168, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Процессов";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(168, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Память Kb";
            // 
            // numericUpDownVideoMemorySize
            // 
            this.numericUpDownVideoMemorySize.Location = new System.Drawing.Point(259, 104);
            this.numericUpDownVideoMemorySize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownVideoMemorySize.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDownVideoMemorySize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownVideoMemorySize.Name = "numericUpDownVideoMemorySize";
            this.numericUpDownVideoMemorySize.Size = new System.Drawing.Size(57, 26);
            this.numericUpDownVideoMemorySize.TabIndex = 4;
            this.numericUpDownVideoMemorySize.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            // 
            // numericUpDownNumberOfProcess
            // 
            this.numericUpDownNumberOfProcess.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownNumberOfProcess.Location = new System.Drawing.Point(368, 198);
            this.numericUpDownNumberOfProcess.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownNumberOfProcess.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNumberOfProcess.Name = "numericUpDownNumberOfProcess";
            this.numericUpDownNumberOfProcess.Size = new System.Drawing.Size(148, 26);
            this.numericUpDownNumberOfProcess.TabIndex = 3;
            this.numericUpDownNumberOfProcess.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // radioButtonMpiEngine
            // 
            this.radioButtonMpiEngine.AutoSize = true;
            this.radioButtonMpiEngine.Location = new System.Drawing.Point(30, 199);
            this.radioButtonMpiEngine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButtonMpiEngine.Name = "radioButtonMpiEngine";
            this.radioButtonMpiEngine.Size = new System.Drawing.Size(94, 24);
            this.radioButtonMpiEngine.TabIndex = 2;
            this.radioButtonMpiEngine.Text = "MPICH2";
            this.radioButtonMpiEngine.UseVisualStyleBackColor = true;
            // 
            // radioButtonCudafyEngine
            // 
            this.radioButtonCudafyEngine.AutoSize = true;
            this.radioButtonCudafyEngine.Location = new System.Drawing.Point(32, 104);
            this.radioButtonCudafyEngine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButtonCudafyEngine.Name = "radioButtonCudafyEngine";
            this.radioButtonCudafyEngine.Size = new System.Drawing.Size(121, 24);
            this.radioButtonCudafyEngine.TabIndex = 1;
            this.radioButtonCudafyEngine.Text = "CUDAfy.Net";
            this.radioButtonCudafyEngine.UseVisualStyleBackColor = true;
            // 
            // radioButtonNativeEngine
            // 
            this.radioButtonNativeEngine.AutoSize = true;
            this.radioButtonNativeEngine.Checked = true;
            this.radioButtonNativeEngine.Location = new System.Drawing.Point(32, 51);
            this.radioButtonNativeEngine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButtonNativeEngine.Name = "radioButtonNativeEngine";
            this.radioButtonNativeEngine.Size = new System.Drawing.Size(131, 24);
            this.radioButtonNativeEngine.TabIndex = 0;
            this.radioButtonNativeEngine.TabStop = true;
            this.radioButtonNativeEngine.Text = "Windows .Net";
            this.radioButtonNativeEngine.UseVisualStyleBackColor = true;
            this.radioButtonNativeEngine.CheckedChanged += new System.EventHandler(this.radioButtonNativeEngine_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(326, 190);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(201, 20);
            this.label10.TabIndex = 8;
            this.label10.Text = "Сигма в фильтре Гаусса:";
            // 
            // numericUpDownGauss
            // 
            this.numericUpDownGauss.DecimalPlaces = 6;
            this.numericUpDownGauss.Location = new System.Drawing.Point(538, 188);
            this.numericUpDownGauss.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownGauss.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDownGauss.Name = "numericUpDownGauss";
            this.numericUpDownGauss.Size = new System.Drawing.Size(180, 26);
            this.numericUpDownGauss.TabIndex = 7;
            this.numericUpDownGauss.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(123, 225);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(402, 20);
            this.label11.TabIndex = 10;
            this.label11.Text = "Квадрат радиуса в фильтрах эррозии/расшарения:";
            // 
            // numericUpDownErosionDilation
            // 
            this.numericUpDownErosionDilation.DecimalPlaces = 6;
            this.numericUpDownErosionDilation.Location = new System.Drawing.Point(538, 224);
            this.numericUpDownErosionDilation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownErosionDilation.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDownErosionDilation.Name = "numericUpDownErosionDilation";
            this.numericUpDownErosionDilation.Size = new System.Drawing.Size(180, 26);
            this.numericUpDownErosionDilation.TabIndex = 9;
            this.numericUpDownErosionDilation.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownErosionDilation.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // Settings
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(901, 664);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.numericUpDownErosionDilation);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.numericUpDownGauss);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownSCI);
            this.Controls.Add(this.numericUpDownMedian);
            this.Controls.Add(this.numericUpDownNoise);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMedian)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSCI)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCudaBlockSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCudaGridSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCudafyBlockSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCudafyGridSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVideoMemorySize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberOfProcess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGauss)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownErosionDilation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numericUpDownNoise;
        private System.Windows.Forms.NumericUpDown numericUpDownMedian;
        private System.Windows.Forms.NumericUpDown numericUpDownSCI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonMpiEngine;
        private System.Windows.Forms.RadioButton radioButtonCudafyEngine;
        private System.Windows.Forms.RadioButton radioButtonNativeEngine;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownVideoMemorySize;
        private System.Windows.Forms.NumericUpDown numericUpDownNumberOfProcess;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownCudafyBlockSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.NumericUpDown numericUpDownCudafyGridSize;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownCudaBlockSize;
        private System.Windows.Forms.NumericUpDown numericUpDownCudaGridSize;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton radioButtonCudaEngine;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDownGauss;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numericUpDownErosionDilation;
    }
}