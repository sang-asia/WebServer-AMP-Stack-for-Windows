using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using WebServer.Libraries;

namespace WebServer
{
    static class Program
    {
        public static readonly string UUID = "4aad1c2c-0ebb-4909-a02f-29e2c3ae6fcb";
        public static readonly bool IS_X64 = Environment.Is64BitOperatingSystem;
        public static readonly string FILE_EXE = Assembly.GetEntryAssembly().Location;
        public static readonly string DIR_ROOT = Path.GetDirectoryName(FILE_EXE);
        public static readonly string DIR_MARIADB_DATA = Path.Combine(DIR_ROOT, "Data", "MariaDB");
        public static readonly string DIR_MYSQL_DATA = Path.Combine(DIR_ROOT, "Data", "MySQL");
        public static readonly string DIR_MONGODB_DATA = Path.Combine(DIR_ROOT, "Data", "MongoDB");
        public static readonly string DIR_TEMP = Path.Combine(DIR_ROOT, "Temp");
        public static readonly string DIR_BIN = Path.Combine(DIR_ROOT, "Binary", IS_X64 ? "x64" : "x86");
        public static readonly string DIR_CONFIG = Path.Combine(DIR_ROOT, "Config");
        // public static readonly string DIR_WEB_CONFIG = Path.Combine(Program.DIR_CONFIG, "websites");
        public static readonly string DIR_FONTS = Path.Combine(DIR_ROOT, "Fonts");
        public static readonly string DIR_LOGS = Path.Combine(DIR_ROOT, "Logs");
        public static readonly string DIR_PHP = Path.Combine(DIR_BIN, "PHP");
        public static readonly string DIR_WEBSITES = Path.Combine(DIR_ROOT, "Websites");
        public static readonly string NAME = "WebServer / AMP Stack (" + (IS_X64 ? "x64" : "x86") + ")";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var mutex = new System.Threading.Mutex(true, UUID, out bool is_single);

            if (!is_single)
            {
                Dialog.Error("Another instance is already running.");
                return;
            }

            if (!IsMeetRequirements())
            {
                Application.Exit();
            }
            else
            {
                Fonts.AddTTF(DIR_FONTS);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Forms.frmMain());
                GC.KeepAlive(mutex);
            }
        }

        /// <summary>
        /// Check some requirements
        /// </summary>
        static bool IsMeetRequirements()
        {
            string[] folders = { DIR_TEMP, DIR_BIN, DIR_CONFIG, DIR_FONTS, DIR_MARIADB_DATA, DIR_MYSQL_DATA, DIR_MONGODB_DATA, DIR_LOGS, DIR_PHP, DIR_WEBSITES };

            foreach (string f in folders)
            {
                if (!FileSystem.IsFolderNorFile(f))
                {
                    MessageBox.Show("[" + f + "] is a file. Please delete this file first!", "Directory Error");
                    return false;
                }
            }

            return true;
        }
    }
}
