using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace img
{
    internal class MpiMedianFilter : MedianFilter, IFilter
    {
        private readonly byte[][][] _channel;
        private readonly int _height;
        private readonly int _numberOfProcess;
        private readonly int _step;
        private readonly int _width;

        public MpiMedianFilter(Bitmap btm, int numberOfProcess = 5, int step = 3) : base(btm, step)
        {
            _numberOfProcess = numberOfProcess;
            _step = step;
            if (Newbmp != null)
                Newbmp.Dispose();
            if (Oldbmp != null)
                Oldbmp.Dispose();
            Oldbmp = new Bitmap(btm);
            Newbmp = new Bitmap(btm.Width, btm.Height);
            N = step%2 == 0 ? step += 1 : step;
            Nh = N/2;
            _width = btm.Width;
            _height = btm.Height;
            _channel = new[]
            {
                new[] {new byte[_width*_height], new byte[_width*_height], new byte[_width*_height]},
                new[] {new byte[_width*_height], new byte[_width*_height], new byte[_width*_height]}
            };
        }

        public new bool Filter()
        {
            try
            {
                string inputFileName = Path.GetTempPath() + Guid.NewGuid() + ".bin";
                string outputFileName = Path.GetTempPath() + Guid.NewGuid() + ".bin";
                var buffer = new byte[3*1024];
                string command = string.Format("/C mpiexec.exe -n {0} mpifilter {1} {2} {3} {4} {5} {6} >> mpi.log",
                    _numberOfProcess, "median", _step, _width, _height, inputFileName, outputFileName);

                for (int y = 0; y < _height; y++)
                    for (int x = 0; x < _width; x++)
                    {
                        Color c = Oldbmp.GetPixel(x, y);
                        _channel[0][0][y*_width + x] = c.R;
                        _channel[0][1][y*_width + x] = c.G;
                        _channel[0][2][y*_width + x] = c.B;
                    }
                using (var writer = new BinaryWriter(File.Open(inputFileName, FileMode.Create)))
                {
                    for (int i = 0; i < _width * _height; i += buffer.Length/3)
                    {
                        int count = Math.Min(buffer.Length / 3, _width * _height - i);
                        for (int k = 0; k < count; k++)
                            for (int j = 0; j < 3; j++)
                                buffer[3*k + j] = _channel[0][j][i + k];
                        writer.Write(buffer, 0, 3 * count);
                    }
                    writer.Close();
                }

                Debug.WriteLine(command);
                Process process = Process.Start("cmd", command);

                if (process != null)
                {
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        using (var reader = new BinaryReader(File.Open(outputFileName, FileMode.Open)))
                        {
                            for (int i = 0; i < _width * _height; i += buffer.Length / 3)
                            {
                                int count = Math.Min(buffer.Length / 3, _width * _height - i);
                                reader.Read(buffer, 0, 3*count);
                                for (int k = 0; k < count; k++)
                                    for (int j = 0; j < 3; j++)
                                        _channel[1][j][i + k] = buffer[3*k + j];
                            }
                            reader.Close();
                        }
                        for (int y = 0; y < _height; y++)
                            for (int x = 0; x < _width; x++)
                            {
                                Newbmp.SetPixel(x, y,
                                    Color.FromArgb(_channel[1][0][y*_width + x], _channel[1][1][y*_width + x],
                                        _channel[1][2][y*_width + x]));
                            }
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

        public new Bitmap GetNewBmp
        {
            get { return Newbmp; }
        }
    }
}