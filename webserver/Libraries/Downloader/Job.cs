using System;
using System.Net;
using System.Net.Cache;
using System.Threading;
using System.Threading.Tasks;

namespace WebServer.Libraries.Downloader
{
    class Job
    {
        // EVENTS
        public event DownloaderEventHandler OnStart;
        public event DownloaderEventHandler OnCompleted;
        public event DownloaderEventHandler OnCancelled;
        public event DownloaderEventHandler OnPercentageChanged;

        // ENUMS
        public enum EState { New, Downloading, Completed, Cancelled };

        // VARIABLES
        private readonly string id = Guid.NewGuid().ToString();
        private readonly string url;
        private readonly string file;
        private int percent = 0;
        private int percent_old;
        private int percent_seconds;
        private EState state = EState.New;
        private WebClient client;
        private CancellationTokenSource token_source = new CancellationTokenSource();

        /// <summary>
        /// Construct Class
        /// </summary>
        /// <param name="url">URL to download</param>
        /// <param name="file">File path to save</param>
        public Job(string url, string file)
        {
            this.url = url;
            this.file = file;
        }

        /// <summary>
        /// Update download percentage
        /// </summary>
        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.percent = e.ProgressPercentage;
            this.OnPercentageChanged?.Invoke(null, new DownloaderEventArgs(this));
        }

        /// <summary>
        /// Trigger download completed event
        /// </summary>
        private void DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            this.token_source.Cancel();

            if (e.Cancelled)
            {
                this.state = EState.Cancelled;
                this.OnCancelled?.Invoke(null, new DownloaderEventArgs(this));
            }
            else
            {
                this.state = EState.Completed;
                this.OnCompleted?.Invoke(null, new DownloaderEventArgs(this));
            }
        }

        /// <summary>
        /// Create new WebClient
        /// </summary>
        private void CreateClient()
        {
            this.client = new WebClient();
            this.client.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
            this.client.Headers.Add(HttpRequestHeader.UserAgent, "SlabWAMPSuite");
            this.client.DownloadProgressChanged += DownloadProgressChanged;
            this.client.DownloadFileCompleted += DownloadFileCompleted;
            this.percent = 0;
        }

        /// <summary>
        /// Start download
        /// </summary>
        public void Start(bool async = true)
        {
            if (this.state == EState.New || this.state == EState.Cancelled)
            {
                this.CreateClient();

                if (async)
                {
                    this.client.DownloadFileAsync(new Uri(this.url), this.file);
                    this.state = EState.Downloading;
                    OnStart?.Invoke(null, new DownloaderEventArgs(this));

                    Task.Factory.StartNew(() =>
                    {
                        while (true)
                        {
                            if (this.percent == this.percent_old)
                            {
                                this.percent_seconds++;

                                if (this.percent_seconds > 60)
                                {
                                    this.Cancel();
                                    break;
                                }
                            }
                            else
                            {
                                this.percent_seconds = 0;
                                this.percent_old = this.percent;
                            }

                            Thread.Sleep(1000);
                        }
                    }, this.token_source.Token);
                }
                else
                {
                    this.client.DownloadFile(this.url, this.file);
                }
            }
        }

        /// <summary>
        /// Cancel downloading job
        /// </summary>
        public void Cancel()
        {
            if (this.state == EState.Downloading)
            {
                this.client.CancelAsync();
                this.state = EState.Cancelled;
            }
        }

        /// <summary>
        /// Get Job ID
        /// </summary>
        public string GetId()
        {
            return this.id;
        }

        /// <summary>
        /// Get Job state
        /// </summary>
        public EState GetState()
        {
            return this.state;
        }

        /// <summary>
        /// Get Job URL
        /// </summary>
        public string GetUrl()
        {
            return this.url;
        }

        /// <summary>
        /// Get Job file
        /// </summary>
        public string GetFile()
        {
            return this.file;
        }

        /// <summary>
        /// Get Job download percentage
        /// </summary>
        public int GetPercent()
        {
            return this.percent;
        }
    }
}
