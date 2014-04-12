using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace sci2
{
    [Serializable]
    class SCI
    {
        public int width;
        public int height;
        public int rwidth;
        public int rheight;
        public int blockSize;
        public List<Bitmap> BaseBlocks;
        public int[] zam;
        public int[] pov;
    }
}
