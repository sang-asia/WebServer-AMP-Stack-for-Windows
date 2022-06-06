using System.Diagnostics;

namespace WebServer.Libraries
{
    static class Processes
    {
        /// <summary>
        /// Run exe file
        /// </summary>
        public static bool Exec(string exe, string args = null, bool background = true)
        {
            using (Process p = new Process())
            {
                p.StartInfo.FileName = exe;
                p.StartInfo.Arguments = args;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;

                if (background)
                {
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                }

                p.Start();
                p.WaitForExit();
                
                return p.ExitCode == 0;
            }
        }
    }
}
