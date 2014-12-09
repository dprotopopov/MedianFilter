using System;
using System.Drawing;
using MyCudafy;

namespace img
{
    internal class CudafyMedianFilter : IFilter
    {
        private readonly int _step;
        private readonly int _videoMemorySize;
        protected Int32 N;
        public Bitmap Newbmp;
        protected Int32 Nh;
        public Bitmap Oldbmp;
        private int _height;
        private int _width;

        public CudafyMedianFilter(Bitmap btm, int videoMemorySize, int gridSize, int blockSize, int step = 3)
        {
            _step = step;
            _videoMemorySize = videoMemorySize;
            if (Newbmp != null)
                Newbmp.Dispose();
            if (Oldbmp != null)
                Oldbmp.Dispose();
            Oldbmp = new Bitmap(btm);
            Newbmp = new Bitmap(btm);
            _width = btm.Width;
            _height = btm.Height;
            GridSize = gridSize;
            BlockSize = blockSize;
        }

        public int GridSize { get; set; }
        public int BlockSize { get; set; }

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
            try
            {
                lock (CudafyFilter.Semaphore)
                {
                    CudafyFilter.SetBitmap(Oldbmp, _step, _videoMemorySize);
                    CudafyFilter.MedianFilter();
                    Newbmp = CudafyFilter.GetBitmap();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}