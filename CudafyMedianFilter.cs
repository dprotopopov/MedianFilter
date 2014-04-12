using System.Drawing;
using MyCudafy;

namespace img
{
    internal class CudafyMedianFilter : MedianFilter, IFilter
    {
        private readonly int _step;
        private readonly int _videoMemorySize;

        public CudafyMedianFilter(Bitmap btm, int videoMemorySize, int step = 3)
            : base(btm, step)
        {
            _step = step;
            _videoMemorySize = videoMemorySize;
            if (newbmp != null)
                newbmp.Dispose();
            if (oldbmp != null)
                oldbmp.Dispose();
            oldbmp = new Bitmap(btm);
            N = step%2 == 0 ? step += 1 : step;
            Nh = N/2;
        }

        public new bool Filter()
        {
            try
            {
                lock (CudafyFilter.Semaphore)
                {
                    CudafyFilter.SetBitmap(oldbmp, _step, _videoMemorySize);
                    CudafyFilter.MedianFilter();
                    newbmp = CudafyFilter.GetBitmap();
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