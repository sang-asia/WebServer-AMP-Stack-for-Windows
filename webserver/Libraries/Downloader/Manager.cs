using System;
using System.Collections.Generic;

namespace WebServer.Libraries.Downloader
{
    /// <summary>
    /// Manage Download Jobs
    /// </summary>
    static class Manager
    {
        // EVENTS
        public static event DownloaderEventHandler EventJobAdded;

        // VARIABLES
        private static readonly List<Job> jobs = new List<Job>();

        /// <summary>
        /// Add new Download Job
        /// </summary>
        /// <param name="url">URL to download</param>
        /// <param name="file">File path to save</param>
        /// <returns>New Job instance</returns>
        public static Job Add(string url, string file)
        {
            Job job = new Job(url, file);
            jobs.Add(job);
            EventJobAdded?.Invoke(null, new DownloaderEventArgs(job));
            return job;
        }
    }
}
