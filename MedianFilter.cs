using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace img
{
    class MedianFilter : IFilter
    {
        #region Переменные

        public Bitmap oldbmp;
        public Bitmap newbmp;
        protected Int32 N;
        protected Int32 Nh;
        private Byte[,] R0, G0, B0;
        #endregion

        public MedianFilter()
        {
        }

        public MedianFilter(Bitmap btm, Int32 step = 3)
            : this()
        {
            if (this.newbmp != null) 
                newbmp.Dispose();
            this.newbmp = new Bitmap(btm);
            this.oldbmp = new Bitmap(btm);
            this.N = step % 2 == 0 ? step+=1 : step;
            this.Nh = this.N / 2;
        }
        
        public bool Filter()
        {
            try
            {
                R0 = new Byte[newbmp.Height, newbmp.Width];
                G0 = new Byte[newbmp.Height, newbmp.Width];
                B0 = new Byte[newbmp.Height, newbmp.Width];

                for (int y = 0; y < oldbmp.Height; y++)
                    for (int x = 0; x < oldbmp.Width; x++)
                    {
                        Color c = newbmp.GetPixel(x, y);
                        R0[y, x] = (Byte)c.R;
                        G0[y, x] = (Byte)c.G;
                        B0[y, x] = (Byte)c.B;
                    }

                Int32 x1, x2, x3, y1, y2, y3, i, n = N * N / 2;
                Byte[] r = new Byte[N * N], g = new Byte[N * N], b = new Byte[N * N];
                for (y1 = Nh; y1 < newbmp.Height - Nh; y1++)
                {
                    for (x1 = Nh; x1 < newbmp.Width - Nh; x1++)
                    {
                        i = 0;
                        for (y2 = -Nh; y2 <= Nh; y2++)
                        {
                            y3 = y1 + y2;
                            for (x2 = -Nh; x2 <= Nh; x2++)
                            {
                                x3 = x1 + x2;
                                r[i] = R0[y3, x3];
                                g[i] = G0[y3, x3];
                                b[i] = B0[y3, x3]; i++;
                            }
                        }
                        Array.Sort(r); Array.Sort(g); Array.Sort(b);
                        newbmp.SetPixel(x1, y1, Color.FromArgb(r[n], g[n], b[n])); 
                    }
                    newbmp.SetPixel(x1, y1, Color.FromArgb(r[n], g[n], b[n]));
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Image AddNoise(Image img, int amount = 15)
        {
            Bitmap bmp = new Bitmap(img);
            Random r = new Random();
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    int num = r.Next(0, 256);
                    bmp.SetPixel(x, y, Color.FromArgb(255, num, num, num));
                    y += r.Next(0, amount);
                }
                x += r.Next(0, amount);
            }

            return bmp;
        }

        public Bitmap getNewBMP
        {
            get
            {
                return this.newbmp;
            }
        }

        public Bitmap getOldBMP
        {
            get
            {
                return this.oldbmp;
            }
        }

        public Bitmap setBMP
        {
            set
            {
                if (this.newbmp != null)
                    newbmp.Dispose();
                this.newbmp = new Bitmap(value);
                this.oldbmp = new Bitmap(value);
            }
        }

    }
}