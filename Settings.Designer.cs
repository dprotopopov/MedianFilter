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
            this.button2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownBlockSize = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownGridSize = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownVideoMemorySize = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownNumberOfProcess = new System.Windows.Forms.NumericUpDown();
            this.radioButtonMpiEngine = new System.Windows.Forms.RadioButton();
            this.radioButtonCudafyEngine = new System.Windows.Forms.RadioButton();
            this.radioButtonNativeEngine = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMedian)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSCI)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBlockSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGridSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVideoMemorySize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberOfProcess)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(510, 394);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // numericUpDownNoise
            // 
            this.numericUpDownNoise.Location = new System.Drawing.Point(382, 47);
            this.numericUpDownNoise.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownNoise.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownNoise.Name = "numericUpDownNoise";
            this.numericUpDownNoise.Size = new System.Drawing.Size(160, 22);
            this.numericUpDownNoise.TabIndex = 2;
            // 
            // numericUpDownMedian
            // 
            this.numericUpDownMedian.Location = new System.Drawing.Point(382, 79);
            this.numericUpDownMedian.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownMedian.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDownMedian.Name = "numericUpDownMedian";
            this.numericUpDownMedian.Size = new System.Drawing.Size(160, 22);
            this.numericUpDownMedian.TabIndex = 2;
            // 
            // numericUpDownSCI
            // 
            this.numericUpDownSCI.Location = new System.Drawing.Point(382, 111);
            this.numericUpDownSCI.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownSCI.Name = "numericUpDownSCI";
            this.numericUpDownSCI.Size = new System.Drawing.Size(160, 22);
            this.numericUpDownSCI.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(262, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Уровень шума:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(110, 81);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(254, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ранг матрицы в медианном фильтре:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(243, 113);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Погрешность SCI:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.numericUpDownBlockSize);
            this.groupBox1.Controls.Add(this.numericUpDownGridSize);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numericUpDownVideoMemorySize);
            this.groupBox1.Controls.Add(this.numericUpDownNumberOfProcess);
            this.groupBox1.Controls.Add(this.radioButtonMpiEngine);
            this.groupBox1.Controls.Add(this.radioButtonCudafyEngine);
            this.groupBox1.Controls.Add(this.radioButtonNativeEngine);
            this.groupBox1.Location = new System.Drawing.Point(83, 171);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(621, 183);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Вычислительная система";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(490, 82);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Опции";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonCudaChoose_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(383, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "x";
            // 
            // numericUpDownBlockSize
            // 
            this.numericUpDownBlockSize.Location = new System.Drawing.Point(403, 83);
            this.numericUpDownBlockSize.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDownBlockSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownBlockSize.Name = "numericUpDownBlockSize";
            this.numericUpDownBlockSize.Size = new System.Drawing.Size(56, 22);
            this.numericUpDownBlockSize.TabIndex = 9;
            this.numericUpDownBlockSize.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // numericUpDownGridSize
            // 
            this.numericUpDownGridSize.Location = new System.Drawing.Point(329, 83);
            this.numericUpDownGridSize.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownGridSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownGridSize.Name = "numericUpDownGridSize";
            this.numericUpDownGridSize.Size = new System.Drawing.Size(49, 22);
            this.numericUpDownGridSize.TabIndex = 8;
            this.numericUpDownGridSize.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(290, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "Grid";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(149, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Процессов";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(149, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Память Kb";
            // 
            // numericUpDownVideoMemorySize
            // 
            this.numericUpDownVideoMemorySize.Location = new System.Drawing.Point(230, 83);
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
            this.numericUpDownVideoMemorySize.Size = new System.Drawing.Size(51, 22);
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
            this.numericUpDownNumberOfProcess.Location = new System.Drawing.Point(327, 121);
            this.numericUpDownNumberOfProcess.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNumberOfProcess.Name = "numericUpDownNumberOfProcess";
            this.numericUpDownNumberOfProcess.Size = new System.Drawing.Size(132, 22);
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
            this.radioButtonMpiEngine.Location = new System.Drawing.Point(27, 122);
            this.radioButtonMpiEngine.Name = "radioButtonMpiEngine";
            this.radioButtonMpiEngine.Size = new System.Drawing.Size(79, 21);
            this.radioButtonMpiEngine.TabIndex = 2;
            this.radioButtonMpiEngine.Text = "MPICH2";
            this.radioButtonMpiEngine.UseVisualStyleBackColor = true;
            // 
            // radioButtonCudafyEngine
            // 
            this.radioButtonCudafyEngine.AutoSize = true;
            this.radioButtonCudafyEngine.Location = new System.Drawing.Point(28, 83);
            this.radioButtonCudafyEngine.Name = "radioButtonCudafyEngine";
            this.radioButtonCudafyEngine.Size = new System.Drawing.Size(104, 21);
            this.radioButtonCudafyEngine.TabIndex = 1;
            this.radioButtonCudafyEngine.Text = "CUDAfy.Net";
            this.radioButtonCudafyEngine.UseVisualStyleBackColor = true;
            // 
            // radioButtonNativeEngine
            // 
            this.radioButtonNativeEngine.AutoSize = true;
            this.radioButtonNativeEngine.Checked = true;
            this.radioButtonNativeEngine.Location = new System.Drawing.Point(28, 41);
            this.radioButtonNativeEngine.Name = "radioButtonNativeEngine";
            this.radioButtonNativeEngine.Size = new System.Drawing.Size(115, 21);
            this.radioButtonNativeEngine.TabIndex = 0;
            this.radioButtonNativeEngine.TabStop = true;
            this.radioButtonNativeEngine.Text = "Windows .Net";
            this.radioButtonNativeEngine.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(490, 120);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Опции";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Settings
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(801, 482);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownSCI);
            this.Controls.Add(this.numericUpDownMedian);
            this.Controls.Add(this.numericUpDownNoise);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBlockSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGridSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVideoMemorySize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberOfProcess)).EndInit();
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
        private System.Windows.Forms.NumericUpDown numericUpDownBlockSize;
        private System.Windows.Forms.NumericUpDown numericUpDownGridSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}