using System;
using System.Drawing;

namespace img
{
    internal class NoiseFilter : IFilter
    {
        #region Переменные

        private readonly Int32 _amount;
        public Bitmap Newbmp;
        public Bitmap Oldbmp;
        private int _height;
        private int _width;

        #endregion

        public NoiseFilter()
        {
        }

        public NoiseFilter(Bitmap btm, int amount = 15)
            : this()
        {
            _amount = amount;
            if (Newbmp != null)
                Newbmp.Dispose();
            Newbmp = new Bitmap(btm);
            Oldbmp = new Bitmap(btm);
            _width = btm.Width;
            _height = btm.Height;
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
                _width = value.Width;
                _height = value.Height;
            }
        }

        public bool Filter()
        {
            Bitmap bmp = GetOldBmp;
            var r = new Random();
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    int num = r.Next(0, 256);
                    bmp.SetPixel(x, y, Color.FromArgb(255, num, num, num));
                    y += r.Next(0, _amount);
                }
                x += r.Next(0, _amount);
            }
            Newbmp = new Bitmap(bmp);
            return true;
        }
    }
}