using System.Drawing;
using System.IO;

namespace BananaSplit
{
    public static class Utilities
    {
        public static Bitmap BytesToImage(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return null;
            }

            using MemoryStream ms = new MemoryStream(bytes);
            Bitmap bmp = new Bitmap(ms);

            return bmp;

        }
    }
}
