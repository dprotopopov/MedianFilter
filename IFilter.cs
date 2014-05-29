using System.Drawing;

namespace img
{
    /// <summary>
    ///     ��������� ������� �����������
    /// </summary>
    internal interface IFilter
    {
        Bitmap GetNewBmp { get; }
        Bitmap GetOldBmp { get; }
        Bitmap SetBmp { set; }

        /// <summary>
        ///     ���������� ��������� ����������
        /// </summary>
        /// <returns></returns>
        bool Filter();
    }
}