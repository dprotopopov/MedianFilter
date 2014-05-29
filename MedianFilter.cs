using System;
using System.Drawing;

namespace img
{
    internal class MedianFilter : IFilter
    {
        #region Переменные

        private Byte[,] B0;
        private Byte[,] G0;
        protected Int32 N;
        public Bitmap Newbmp;
        protected Int32 Nh;
        public Bitmap Oldbmp;
        private Byte[,] R0;

        #endregion

        public MedianFilter()
        {
        }

        public MedianFilter(Bitmap btm, Int32 step = 3)
            : this()
        {
            if (Newbmp != null)
                Newbmp.Dispose();
            Newbmp = new Bitmap(btm);
            Oldbmp = new Bitmap(btm);
            N = step%2 == 0 ? step += 1 : step;
            Nh = N/2;
        }

        public bool Filter()
        {
            try
            {
                R0 = new Byte[Newbmp.Height, Newbmp.Width];
                G0 = new Byte[Newbmp.Height, Newbmp.Width];
                B0 = new Byte[Newbmp.Height, Newbmp.Width];

                for (int y = 0; y < Oldbmp.Height; y++)
                    for (int x = 0; x < Oldbmp.Width; x++)
                    {
                        Color c = Newbmp.GetPixel(x, y);
                        R0[y, x] = c.R;
                        G0[y, x] = c.G;
                        B0[y, x] = c.B;
                    }

                Int32 x1, x2, x3, y1, y2, y3, i, n = N*N/2;
                Byte[] r = new Byte[N*N], g = new Byte[N*N], b = new Byte[N*N];
                for (y1 = Nh; y1 < Newbmp.Height - Nh; y1++)
                {
                    for (x1 = Nh; x1 < Newbmp.Width - Nh; x1++)
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
                                b[i] = B0[y3, x3];
                                i++;
                            }
                        }
                        Array.Sort(r);
                        Array.Sort(g);
                        Array.Sort(b);
                        Newbmp.SetPixel(x1, y1, Color.FromArgb(r[n], g[n], b[n]));
                    }
                    Newbmp.SetPixel(x1, y1, Color.FromArgb(r[n], g[n], b[n]));
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Bitmap GetNewBmp
        {
            get { return Newbmp; }
        }

        public Bitmap GetOldBmp
        {
            get { return Oldbmp; }
        }

        public Bitmap SetBmp
        {
            set
            {
                if (Newbmp != null)
                    Newbmp.Dispose();
                Newbmp = new Bitmap(value);
                Oldbmp = new Bitmap(value);
            }
        }

        public Image AddNoise(Image img, int amount = 15)
        {
            var bmp = new Bitmap(img);
            var r = new Random();
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
    }
}