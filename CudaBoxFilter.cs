using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace img
{
    internal class CudaBoxFilter : IFilter
    {
        private static readonly int[][] Matrix =
        {
            new[] {0, 1, 0},
            new[] {1, 0, 1},
            new[] {0, 1, 0}
        };

        public Bitmap Newbmp;
        public Bitmap Oldbmp;

        public CudaBoxFilter(Bitmap btm, int gridSize, int blockSize)
        {
            Oldbmp = new Bitmap(btm);
            Newbmp = new Bitmap(btm);
            GridSize = gridSize;
            BlockSize = blockSize;
        }


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
                if (Newbmp != null)
                    Newbmp.Dispose();
                Newbmp = new Bitmap(value);
                Oldbmp = new Bitmap(value);
            }
        }

        public bool Filter()
        {
            try
            {
                string inputFileName = Path.GetTempPath() + Guid.NewGuid() + ".bin";
                string outputFileName = Path.GetTempPath() + Guid.NewGuid() + ".bin";
                string command = string.Format("/C cudaboxfilter {0} {1} {2} {3} {4} {5} >> cuda.log",
                    "box", string.Join(" ",
                        Matrix.Select(row => string.Join(" ", row.Select(item => string.Format("{0}", item))))),
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