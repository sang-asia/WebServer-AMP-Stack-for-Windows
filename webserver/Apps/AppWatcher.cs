using System.Threading;
using WebServer.Libraries;

namespace WebServer.Apps
{
    static class AppWatcher
    {
        private static readonly Tasks TASKS = new Tasks();

        /// <summary>
        /// Start watch an app
        /// </summary>
        public static void Watch(AppBase app)
        {
            TASKS.CreateTask(() =>
            {
                while (app != null)
                {
                    app.CheckStatus();
                    Thread.Sleep(1000);
                }
            });
        }
    }
}
