using System.Drawing;

namespace img
{
    /// <summary>
    ///     Интерфейс фильтра изображения
    /// </summary>
    internal interface IFilter
    {
        Bitmap GetNewBmp { get; }
        Bitmap GetOldBmp { get; }
        Bitmap SetBmp { set; }

        /// <summary>
        ///     Применение алгоритма фильтрации
        /// </summary>
        /// <returns></returns>
        bool Filter();
    }
}