using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace img
{
    internal class CudaDilationFilter : IFilter
    {
        public Bitmap Newbmp;
        public Bitmap Oldbmp;

        public CudaDilationFilter(Bitmap btm, double radius2, int gridSize, int blockSize)
        {
            Radius2 = radius2;
            Oldbmp = new Bitmap(btm);
            Newbmp = new Bitmap(btm);
            GridSize = gridSize;
            BlockSize = blockSize;
        }

        public double Radius2 { get; set; }
        public int GridSize { get; set; }
        public int BlockSize { get; set; }

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
                string inputFileName = Path.GetTempPath() + Guid.NewGuid() + ".bmp";
                string outputFileName = Path.GetTempPath() + Guid.NewGuid() + ".bmp";
                string command = string.Format("/C cudaerosiondilationfilter {0} {1} {2} {3} {4} {5} >> cuda.log",
                    "dilation", Radius2,
                    inputFileName, outputFileName,
                    GridSize, BlockSize);
                Oldbmp.Save(inputFileName, ImageFormat.Bmp);
                Process process = Process.Start("cmd", command);

                if (process != null)
                {
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        Newbmp = new Bitmap(Image.FromFile(outputFileName));
                    }
                    else throw new Exception();
                }
                else throw new Exception();
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
    }
}