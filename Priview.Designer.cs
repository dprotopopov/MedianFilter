namespace img
{
    partial class Priview
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
            this.imagePanel1 = new YLScsImage.ImagePanel();
            this.SuspendLayout();
            // 
            // imagePanel1
            // 
            this.imagePanel1.CanvasSize = new System.Drawing.Size(60, 40);
            this.imagePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePanel1.Image = null;
            this.imagePanel1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            this.imagePanel1.Location = new System.Drawing.Point(0, 0);
            this.imagePanel1.Name = "imagePanel1";
            this.imagePanel1.Size = new System.Drawing.Size(541, 371);
            this.imagePanel1.TabIndex = 0;
            this.imagePanel1.Zoom = 1F;
            // 
            // Priview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 371);
            this.Controls.Add(this.imagePanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "Priview";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Просмотр изображения";
            this.ResumeLayout(false);

        }

        #endregion

        private YLScsImage.ImagePanel imagePanel1;

    }
}