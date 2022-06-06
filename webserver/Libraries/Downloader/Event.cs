using System;

namespace WebServer.Libraries.Downloader
{
    delegate void DownloaderEventHandler(object s, DownloaderEventArgs e);

    /// <summary>
    /// EventArgs class for Downloader's events
    /// </summary>
    class DownloaderEventArgs : EventArgs
    {
        private readonly Job job;

        /// <summary>
        /// Constructor
        /// </summary>
        public DownloaderEventArgs(Job job)
        {
            this.job = job;
        }

        /// <summary>
        /// Get job of this event
        /// </summary>
        public Job GetJob()
        {
            return this.job;
        }
    }
}
