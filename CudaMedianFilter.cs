using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace img
{
    internal class CudaMedianFilter : IFilter
    {
        public Bitmap Newbmp;
        public Bitmap Oldbmp;

        public CudaMedianFilter(Bitmap btm, int gridSize, int blockSize, int step = 3)
        {
            Step = step;
            Oldbmp = new Bitmap(btm);
            Newbmp = new Bitmap(btm);
            GridSize = gridSize;
            BlockSize = blockSize;
        }

        public int Step { get; set; }
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
                string command = string.Format("/C cudamedianfilter {0} {1} {2} {3} {4} {5} >> cuda.log",
                    "median",
                    Step,
                    inputFileName, outputFileName,
                    GridSize, BlockSize);

                Oldbmp.Save(inputFileName, ImageFormat.Bmp);

                Debug.WriteLine(command);
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