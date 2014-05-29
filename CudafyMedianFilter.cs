using System.Drawing;
using MyCudafy;

namespace img
{
    internal class CudafyMedianFilter : MedianFilter, IFilter
    {
        private readonly int _step;
        private readonly int _videoMemorySize;

        public CudafyMedianFilter(Bitmap btm, int videoMemorySize, int gridSize = 0, int blockSize = 0, int step = 3)
            : base(btm, step)
        {
            _step = step;
            _videoMemorySize = videoMemorySize;
            if (Newbmp != null)
                Newbmp.Dispose();
            if (Oldbmp != null)
                Oldbmp.Dispose();
            Oldbmp = new Bitmap(btm);
            N = step%2 == 0 ? step += 1 : step;
            Nh = N/2;
            GridSize = gridSize;
            BlockSize = blockSize;
        }

        public int GridSize { get; set; }
        public int BlockSize { get; set; }

        public new bool Filter()
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