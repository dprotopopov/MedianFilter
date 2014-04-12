using System.Drawing;

namespace img
{
    internal interface IFilter
    {
        bool Filter();
        Bitmap getNewBMP { get; }
    }
}