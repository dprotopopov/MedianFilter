using System;
using System.Collections.Generic;
using System.Drawing;

namespace sci2
{
    [Serializable]
    internal class SCI
    {
        public List<Bitmap> BaseBlocks;
        public int blockSize;
        public int height;
        public int[] pov;
        public int rheight;
        public int rwidth;
        public int width;
        public int[] zam;
    }
}