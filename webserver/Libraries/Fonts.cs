using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;

namespace WebServer.Libraries
{
    static class Fonts
    {
        private static readonly PrivateFontCollection fonts = new PrivateFontCollection();

        /// <summary>
        /// Check file extension supported or not
        /// </summary>
        public static bool IsSupported(string path)
        {
            return path.EndsWith(".ttf");
        }

        /// <summary>
        /// Add font file to collection
        /// </summary>
        public static void AddTTF(string path)
        {
            if (File.Exists(path))
            {
                if (IsSupported(path))
                {
                    fonts.AddFontFile(path);
                }
            }
            else if (Directory.Exists(path))
            {
                IEnumerable<string> files = Directory.EnumerateFiles(path);

                foreach(string f in files)
                {
                    AddTTF(f);
                }
            }
        }

        /// <summary>
        /// Get added font by name
        /// </summary>
        public static FontFamily Get(string name)
        {
            return new FontFamily(name, fonts);
        }
    }
}
