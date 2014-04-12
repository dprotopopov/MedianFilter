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
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownVideoMemorySize = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownNumberOfProcess = new System.Windows.Forms.NumericUpDown();
            this.radioButtonMpiEngine = new System.Windows.Forms.RadioButton();
            this.radioButtonCudafyEngine = new System.Windows.Forms.RadioButton();
            this.radioButtonNativeEngine = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVideoMemorySize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberOfProcess)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(343, 372);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(283, 46);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(160, 22);
            this.numericUpDown1.TabIndex = 2;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(283, 78);
            this.numericUpDown2.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(160, 22);
            this.numericUpDown2.TabIndex = 2;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(283, 110);
            this.numericUpDown3.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(160, 22);
            this.numericUpDown3.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Уровень шума:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(254, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ранг матрицы в медианном фильтре:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(144, 112);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Погрешность SCI:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numericUpDownVideoMemorySize);
            this.groupBox1.Controls.Add(this.numericUpDownNumberOfProcess);
            this.groupBox1.Controls.Add(this.radioButtonMpiEngine);
            this.groupBox1.Controls.Add(this.radioButtonCudafyEngine);
            this.groupBox1.Controls.Add(this.radioButtonNativeEngine);
            this.groupBox1.Location = new System.Drawing.Point(14, 169);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(451, 185);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Вычислительная система";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(171, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Процессов";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(171, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Видео память Kb";
            // 
            // numericUpDownVideoMemorySize
            // 
            this.numericUpDownVideoMemorySize.Location = new System.Drawing.Point(309, 86);
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
            this.numericUpDownVideoMemorySize.Size = new System.Drawing.Size(120, 22);
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
            this.numericUpDownNumberOfProcess.Location = new System.Drawing.Point(309, 121);
            this.numericUpDownNumberOfProcess.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNumberOfProcess.Name = "numericUpDownNumberOfProcess";
            this.numericUpDownNumberOfProcess.Size = new System.Drawing.Size(120, 22);
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
            this.radioButtonMpiEngine.Size = new System.Drawing.Size(113, 21);
            this.radioButtonMpiEngine.TabIndex = 2;
            this.radioButtonMpiEngine.Text = "Microsoft MPI";
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
            // Settings
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(509, 440);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVideoMemorySize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberOfProcess)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
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
    }
}