using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace img
{
    public partial class Exp : Form
    {
        Bitmap bmp;
        Bitmap sci;
        Bitmap original;
        bool exp1 = false;

        public Exp(int median = 3, int noise = 15, int sci = 0)
        {
            InitializeComponent();
            label1.Text = "Выберите изображение для " + Environment.NewLine + "его фильтрации и преобразования в CSI";
            label3.Text = "Добавьте шум на изображение (значение < шума больше), " + Environment.NewLine +
                "установите погрешность при кодировании в SCI, " + Environment.NewLine + " а также ранг матрицы в медианной фильтрации";
            label4.Text = "Среднее отклонение всех точек" + Environment.NewLine + " (сравнивание BMP с SCI)";
            label5.Text = "Степень различия изображений BMP и SCI, " + Environment.NewLine + "попиксельное сравнение с оригиналом";

            this.Median = median;
            this.SCI = sci;
            this.Noise = noise;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "bitmap|*.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                bmp = new Bitmap(open.FileName);
                textBox1.Text = open.FileName;
                pictureBox4.Image = bmp;
                original = new Bitmap(open.FileName);
                richTextBox1.Text = "Загрузка изображения BMP\n";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        public int Median
        {
            get
            {
                return (int)numericUpDown5.Value;
            }
            set
            {
                numericUpDown5.Value = (decimal)value;
            }
        }

        public int SCI
        {
            get
            {
                return (int)numericUpDown4.Value;
            }
            set
            {
                numericUpDown4.Value = (decimal)value;
            }
        }

        public int Noise
        {
            get
            {
                return (int)numericUpDown6.Value;
            }
            set
            {
                numericUpDown6.Value = (decimal)value;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Image im = ((PictureBox)sender).Image;
            Priview pr;
            if (im != null)
            {
                pr = new Priview(new Bitmap(im));
                pr.Show();
            }
        }

        private void exp1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                return;
            richTextBox1.Text += "Установка начальных параметров\n";
            button9.Enabled = false;
            exp1 = true;
            bmp = new Bitmap(original);
            Convertor convertor = new Convertor(bmp, SCI);
            label6.Text = "Конвертирование в SCI";
            richTextBox1.Text += "Конвертирование в SCI\n";
            DateTime dold = DateTime.Now;
            sci = new Bitmap(convertor.ConvertToSCI());
            TimeSpan sp = DateTime.Now - dold;
            richTextBox1.Text += string.Format("Время кодирования в BMP в SCI: {0}\n", sp);
            progressBar1.Value = 30;
            MedianFilter medianBMP = new MedianFilter(bmp, Median);
            label6.Text = "Добавление шума на BMP";
            richTextBox1.Text += "Добавление шума на BMP\n";
            bmp = new Bitmap(medianBMP.AddNoise(bmp, Noise));
            pictureBox5.Image = bmp;
            progressBar1.Value += 10;
            label6.Text = "Фильтрация BMP";
            richTextBox1.Text += "Фильтрация BMP\n";
            medianBMP.setBMP = bmp;
            medianBMP.Filter();
            progressBar1.Value += 20;
            bmp = new Bitmap(medianBMP.getNewBMP);

            MedianFilter medianSCI = new MedianFilter(new Bitmap(sci), Median);
            label6.Text = "Добавление шума на SCI";
            richTextBox1.Text += "Добавление шума на SCI\n";
            sci = new Bitmap(medianSCI.AddNoise(sci, Noise));
            pictureBox6.Image = sci;
            progressBar1.Value += 10;
            label6.Text = "Фильтрация SCI";
            richTextBox1.Text += "Фильтрация SCI\n";
            medianSCI.setBMP = sci;
            medianSCI.Filter();
            progressBar1.Value += 20;
            sci = new Bitmap(medianSCI.getNewBMP);

            pictureBox2.Image = bmp;
            pictureBox3.Image = sci;

            label6.Text = "Сравнение BMP-SCI";
            richTextBox1.Text += "Сравнение BMP-SCI с оригиналом\n";
            // сравнение 1 bmp-original
            double otkl = 0.0;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    otkl += Math.Sqrt(Math.Pow((bmp.GetPixel(i, j).R - original.GetPixel(i, j).R), 2) +
                        Math.Pow((bmp.GetPixel(i, j).G - original.GetPixel(i, j).G), 2) + 
                        Math.Pow((bmp.GetPixel(i, j).B - original.GetPixel(i, j).B), 2));
                }
            }
            // сравнение 2 bmp-original
            double otkl2 = 0.0;
            for (int i = 0; i < sci.Width; i++)
            {
                for (int j = 0; j < sci.Height; j++)
                {
                    otkl2 += Math.Sqrt(Math.Pow((sci.GetPixel(i, j).R - original.GetPixel(i, j).R), 2) + Math.Pow((sci.GetPixel(i, j).G - original.GetPixel(i, j).G), 2) + Math.Pow((sci.GetPixel(i, j).B - original.GetPixel(i, j).B), 2));
                }
            }
            progressBar1.Value = 100;
            string message = string.Format("Среднее отклонение BMP от оригинала равно {0}%\nСреднее отклонение SCI от оригинала равно {1}%", Math.Round(100 * (otkl / (bmp.Width * bmp.Height)) / Math.Sqrt(3 * Math.Pow(255, 2)), 5), Math.Round(100 * (otkl2 / (bmp.Width * bmp.Height)) / Math.Sqrt(3 * Math.Pow(255, 2)), 5));
            MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            richTextBox1.Text += message + "\n";
            button9.Enabled = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 4;
        }

        private void exp2_Click(object sender, EventArgs e)
        {
            if (!exp1)
                return;
            button12.Enabled = false;
            progressBar2.Value = 0;
            int count = 0;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    if (bmp.GetPixel(i, j).R == original.GetPixel(i, j).R && bmp.GetPixel(i, j).G == original.GetPixel(i, j).G && bmp.GetPixel(i, j).B == original.GetPixel(i, j).B)
                    {
                        count++;
                    }
                }
            }
            int count2 = 0;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    if (original.GetPixel(i, j).R == sci.GetPixel(i, j).R && original.GetPixel(i, j).G == sci.GetPixel(i, j).G && original.GetPixel(i, j).B == sci.GetPixel(i, j).B)
                    {
                        count2++;
                    }
                }
            }
            progressBar2.Value = 100;
            string message = string.Format("Коэффициен BMP равен {0}%\nКоэффициен SCI равен {1}%", Math.Round((((double)count) / (bmp.Width * bmp.Height)) * 100, 5), Math.Round((((double)count2) / (bmp.Width * bmp.Height)) * 100, 5));
            MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            richTextBox1.Text += message + "\n";
            button12.Enabled = true;
        }
    }
}
