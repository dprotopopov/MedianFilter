using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using sci2;

namespace img
{
    internal class Convertor
    {
        private double alfa;
        private Bitmap bmp;
        private Image img;
        private SCI mySCI;

        public Convertor(double alfa = 0)
        {
            this.alfa = alfa; // погрешность
        }

        public Convertor(Bitmap bmp, double alfa = 0)
            : this()
        {
            this.bmp = bmp;
            this.alfa = alfa;
        }

        public SCI getSCI
        {
            get { return mySCI; }
        }

        public Image SetBMP
        {
            set
            {
                bmp = new Bitmap(value);
                img = value;
            }
        }

        public double SetAlfa
        {
            set { alfa = value; }
        }

        public Image ConvertToSCI()
        {
            byte[] sciimg = SerializeImageToSCI(bmp);
            img = DeserializeFromSCI(sciimg);

            return img;
        }

        public Image OpenSCI(string f)
        {
            Stream ss = File.OpenRead(f);
            var b = new BinaryFormatter();
            mySCI = (SCI) b.Deserialize(ss);
            ss.Close();

            return decode(mySCI);
        }

        public void SaveOnDisk(string f)
        {
            Stream ss = File.OpenWrite(f);
            var b = new BinaryFormatter();
            b.Serialize(ss, SerializeSCI(img));
            ss.Close();
        }

        private byte[] SerializeImageToSCI(Image img)
        {
            var ss = new MemoryStream();
            var b = new BinaryFormatter();
            b.Serialize(ss, SerializeSCI(img));
            byte[] rez = ss.ToArray();
            ss.Close();
            return rez;
        }

        private SCI SerializeSCI(Image img)
        {
            var A = (Bitmap) img;
            var mySCI = new SCI();
            Bitmap[] all;
            int bs = 32;

            int w = A.Width;
            int h = A.Height;


            int sw = w/bs;
            int sh = h/bs;

            var B = new Bitmap((w != sw*bs ? bs : 0) + sw*bs, (h != sh*bs ? bs : 0) + sh*bs);

            mySCI.width = B.Width;
            mySCI.height = B.Height;
            mySCI.rwidth = A.Width;
            mySCI.rheight = A.Height;
            mySCI.blockSize = bs;

            for (int i = 0; i < B.Width; i++)
                for (int j = 0; j < B.Height; j++)
                    B.SetPixel(i, j, Color.White);
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    B.SetPixel(i, j, A.GetPixel(i, j));

            sw = B.Width/bs;
            sh = B.Height/bs;

            int bcnt = sw*sh;

            all = new Bitmap[bcnt];

            int pi = 0;
            int pj = 0;
            //заполнение массива all
            for (int k = 0; k < bcnt; k++)
            {
                all[k] = new Bitmap(bs, bs);
                for (int i = 0; i < bs; i++)
                    for (int j = 0; j < bs; j++)
                        all[k].SetPixel(i, j, B.GetPixel(i + pi, j + pj));
                pi += bs;
                if (pi == sw*bs)
                {
                    pi = 0;
                    pj += bs;
                }
            }

            //сжатие в формат SCI
            DateTime nac = DateTime.Now;
            bcnt = all.Length; //Всего блоков
            mySCI.zam = new int[bcnt]; //Замены
            mySCI.pov = new int[bcnt]; //Повороты
            mySCI.BaseBlocks = new List<Bitmap>();
            var tb = new int[bcnt]; //Взятые блоки
            int povorot = 0;
            for (int i = 0; i < bcnt; i++)
                if (tb[i] == 0)
                {
                    tb[i] = 1;
                    mySCI.BaseBlocks.Add(all[i]);
                    mySCI.zam[i] = mySCI.BaseBlocks.Count - 1;
                    mySCI.pov[i] = 0;
                    for (int j = i; j < bcnt; j++)
                        if (tb[j] == 0)
                            if (cmpBlocs(all[i], all[j], ref povorot)) //Нашли одинаковые блоки
                            {
                                tb[j] = 1;
                                mySCI.zam[j] = mySCI.BaseBlocks.Count - 1;
                                mySCI.pov[j] = povorot;
                            }
                }
            DateTime kon = DateTime.Now;
            TimeSpan sp = kon - nac;
            return mySCI;
        }

        private bool cmpBlocs(Bitmap b1, Bitmap b2, ref int pov)
        {
            bool rez = true;
            pov = 0;
            //Без поворота
            for (int i = 0; i < b1.Width; i++)
            {
                for (int j = 0; j < b1.Height; j++)
                    if (!cmpColors(b1.GetPixel(i, j), b2.GetPixel(i, j)))
                    {
                        rez = false;
                        break;
                    }
                if (!rez) break;
            }
            if (rez) return true; //блоки совпадают без поворота

            return false; //если оптимизация по времени то выходим иначе пытаемся найти поворот
        }

        private bool cmpColors(Color c1, Color c2)
        {
            double r = Math.Sqrt(Math.Pow(c1.R - c2.R, 2) + Math.Pow(c1.G - c2.G, 2) + Math.Pow(c1.B - c2.B, 2));
            if (r <= 220*alfa/100) return true;
            return false;
        }

        private Image DeserializeFromSCI(byte[] sciimg)
        {
            var ms = new MemoryStream(sciimg);
            var b = new BinaryFormatter();
            mySCI = (SCI) b.Deserialize(ms);
            ms.Close();
            return decode(mySCI);
        }

        public Bitmap decode(SCI img)
        {
            var rez = new Bitmap(img.width, img.height);
            int cols = img.width/img.blockSize;
            int rows = img.height/img.blockSize;
            int bcnt = img.zam.Length;
            int pi = 0;
            int pj = 0;
            for (int k = 0; k < bcnt; k++)
            {
                if (img.pov[k] == 0)
                {
                    for (int i = 0; i < img.blockSize; i++)
                        for (int j = 0; j < img.blockSize; j++)
                            rez.SetPixel(i + pi, j + pj, img.BaseBlocks[img.zam[k]].GetPixel(i, j));
                }
                else if (img.pov[k] == 1) //90
                {
                    for (int i = 0; i < img.blockSize; i++)
                        for (int j = 0; j < img.blockSize; j++)
                            rez.SetPixel(i + pi, j + pj, img.BaseBlocks[img.zam[k]].GetPixel(img.blockSize - 1 - j, i));
                }
                else if (img.pov[k] == 2) //180
                {
                    for (int i = 0; i < img.blockSize; i++)
                        for (int j = 0; j < img.blockSize; j++)
                            rez.SetPixel(i + pi, j + pj,
                                img.BaseBlocks[img.zam[k]].GetPixel(img.blockSize - 1 - i, img.blockSize - 1 - j));
                }
                else if (img.pov[k] == 3) //270
                {
                    for (int i = 0; i < img.blockSize; i++)
                        for (int j = 0; j < img.blockSize; j++)
                            rez.SetPixel(i + pi, j + pj,
                                img.BaseBlocks[img.zam[k]].GetPixel(j, img.blockSize - 1 - i));
                }
                else if (img.pov[k] == 4) //зеркально по горизонтали
                {
                    for (int i = 0; i < img.blockSize; i++)
                        for (int j = 0; j < img.blockSize; j++)
                            rez.SetPixel(i + pi, j + pj,
                                img.BaseBlocks[img.zam[k]].GetPixel(i, img.blockSize - 1 - j));
                }
                else if (img.pov[k] == 5) //зеркально по горизонтали
                {
                    for (int i = 0; i < img.blockSize; i++)
                        for (int j = 0; j < img.blockSize; j++)
                            rez.SetPixel(i + pi, j + pj,
                                img.BaseBlocks[img.zam[k]].GetPixel(img.blockSize - 1 - i, j));
                }

                pi += img.blockSize;
                if (pi >= img.width)
                {
                    pi = 0;
                    pj += img.blockSize;
                }
            }
            var r = new Bitmap(img.rwidth, img.rheight);
            for (int i = 0; i < img.rwidth; i++)
                for (int j = 0; j < img.rheight; j++) r.SetPixel(i, j, rez.GetPixel(i, j));
            return r;
        }
    }
}