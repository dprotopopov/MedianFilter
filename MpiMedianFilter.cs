using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace img
{
    internal class MpiMedianFilter : IFilter
    {
        public Bitmap Newbmp;
        public Bitmap Oldbmp;

        public MpiMedianFilter(Bitmap btm, int numberOfProcess = 5, int step = 3)
        {
            NumberOfProcess = numberOfProcess;
            Step = step;
            Oldbmp = new Bitmap(btm);
        }

        public int NumberOfProcess { get; private set; }
        public int Step { get; private set; }

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
                string command =
                    string.Format("/C mpiexec.exe -n {0} mpimedianfilter {1} {2} {3} {4} >> mpi.log",
                        NumberOfProcess,
                        "median", Step,
                        inputFileName,
                        outputFileName);

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