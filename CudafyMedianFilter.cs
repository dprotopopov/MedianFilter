using System.Drawing;
using MyCudafy;

namespace img
{
    internal class CudafyMedianFilter : IFilter
    {
        public Bitmap Newbmp;
        public Bitmap Oldbmp;

        public CudafyMedianFilter(Bitmap btm, int videoMemorySize, int gridSize, int blockSize, int step = 3)
        {
            Step = step;
            VideoMemorySize = videoMemorySize;
            Oldbmp = new Bitmap(btm);
            Newbmp = new Bitmap(btm);
            GridSize = gridSize;
            BlockSize = blockSize;
        }

        public int Step { get; set; }
        public int VideoMemorySize { get; set; }

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
                Newbmp = new Bitmap(value);
                Oldbmp = new Bitmap(value);
            }
        }

        public bool Filter()
        {
            try
            {
                lock (CudafyFilter.Semaphore)
                {
                    CudafyFilter.SetBitmap(Oldbmp, Step, VideoMemorySize);
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