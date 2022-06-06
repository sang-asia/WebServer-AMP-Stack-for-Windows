using System;
using System.Collections.Generic;
using System.IO;

namespace WebServer.Libraries
{
    delegate void LogEventHandler(Logger s, LogEventArgs e);

    enum LogType
    {
        Info,
        Success,
        Warning,
        Error
    }

    class LogEventArgs : EventArgs
    {
        public readonly string AppName = "";
        public readonly string Content;
        public readonly DateTime DateTime;
        public readonly LogType Type;
        private readonly string rtf_colors = @"{\colortbl;"
            + @"\red50\green50\blue50;"    // cf1 - Info
            + @"\red44\green144\blue145;"  // cf2 - App name
            + @"\red179\green39\blue49;"   // cf3 - Error
            + @"\red179\green153\blue39;"  // cf4 - Warning
            + @"\red200\green200\blue200;" // cf5 - Separator
            + @"\red150\green150\blue150;" // cf6 - Time
            + @"\red39\green179\blue51;"   // cf7 - Success
        + "}";

        public LogEventArgs(DateTime date_time, string content, LogType type = LogType.Info, string app_name = "")
        {
            this.AppName = app_name;
            this.Content = content;
            this.DateTime = date_time;
            this.Type = type;
        }

        public string ToRtf()
        {
            string content = @"{\rtf1\pc " + this.rtf_colors;

            content += @"\cf2 [ \cf6 " + this.DateTime.ToString("HH:mm:ss") + @" \cf5 / ";

            if (this.AppName != "")
            {
                content += @"\cf2 \b " + this.AppName + @"\b0  ]\cf5  ";
            }

            if (this.Type == LogType.Error)
            {
                content += @"\cf3 ";
            }
            else if (this.Type == LogType.Warning)
            {
                content += @"\cf4 ";
            }
            else if (this.Type == LogType.Success)
            {
                content += @"\cf7 ";
            }
            else
            {
                content += @"\cf1 ";
            }

            return content + this.Content.Replace(@"\", @"\\") + "}";
        }
    }

    class Logger
    {
        private string _app_name = "";
        private string _file_name = "";

        /// <summary>
        /// Constructor
        /// </summary>
        public Logger(string file_name, string app_name = "")
        {
            this._app_name = app_name;
            this._file_name = file_name;
        }

        /// <summary>
        /// Get file path
        /// </summary>
        private string GetPath()
        {
            return Path.Combine(Program.DIR_LOGS, this._file_name + ".txt");
        }

        /// <summary>
        /// Get buffer
        /// </summary>
        private List<string> GetBuffer()
        {
            string path = this.GetPath();

            if (!Buffers.ContainsKey(path))
            {
                Buffers.Add(path, new List<string> { });
            }

            return Buffers[path];
        }

        /// <summary>
        /// Get file stream
        /// </summary>
        private StreamWriter GetStream()
        {
            string path = this.GetPath();

            if (!Streams.ContainsKey(path))
            {
                if (!File.Exists(path))
                {
                    try
                    {
                        File.Create(path).Close();
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }

                try
                {
                    StreamWriter stream = new StreamWriter(path, append: true);
                    Streams.Add(path, stream);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return Streams[path];
        }

        /// <summary>
        /// Try to flush log buffer to file
        /// </summary>
        private void TryFlushBuffer()
        {
            List<string> buffer = this.GetBuffer();
            StreamWriter stream = this.GetStream();

            if (stream == null)
            {
                Dialog.Error("Can not write log to file. File:\n\n" + this.GetPath() + "\n\nis used by other application.");
                return;
            }

            while (buffer.Count > 0)
            {
                stream.WriteLine(buffer[0]);
                buffer.RemoveAt(0);
            }

            stream.Flush();
        }

        /// <summary>
        /// Write log
        /// </summary>
        public void Log(string content, LogType type = LogType.Info)
        {
            DateTime dt = DateTime.Now;
            string log_line = "";

            content = content.Replace(Path.Combine(Program.DIR_ROOT) + "\\", "");
            
            OnLog?.Invoke(this, new LogEventArgs(dt, content, type, this._app_name));

            if (this._app_name != "")
            {
                log_line = dt.ToString("[HH:mm:ss]") + "[" + this._app_name + "]";
            }
            else
            {
                log_line = dt.ToString("[HH:mm:ss]");
            }

            log_line += "[" + type.ToString() + "] " + content;

            this.GetBuffer().Add(log_line);
            this.TryFlushBuffer();
        }

        /// <summary>
        /// Log Error
        /// </summary>
        /// <param name="content"></param>
        public void Error(string content)
        {
            this.Log(content, LogType.Error);
        }

        /// <summary>
        /// Log Warning
        /// </summary>
        /// <param name="content"></param>
        public void Warning(string content)
        {
            this.Log(content, LogType.Warning);
        }

        /// <summary>
        /// Log Success
        /// </summary>
        /// <param name="content"></param>
        public void Success(string content)
        {
            this.Log(content, LogType.Success);
        }

        /// <summary>
        /// Log event
        /// </summary>
        public static event LogEventHandler OnLog;

        /// <summary>
        /// Log streams
        /// </summary>
        public static Dictionary<string, StreamWriter> Streams = new Dictionary<string, StreamWriter> { };

        /// <summary>
        /// Log buffer (waiting content that can not be written to files)
        /// </summary>
        public static Dictionary<string, List<string>> Buffers = new Dictionary<string, List<string>> { };

        /// <summary>
        /// Create log for setup
        /// </summary>
        public static Logger Setup(string name)
        {
            return new Logger("Setup-" + name + "-" + DateTime.Now.ToString("yyyyMMdd-HHmmss"), name);
        }

        /// <summary>
        /// Create log for app
        /// </summary>
        public static Logger App(string name)
        {
            return new Logger("Log-" + DateTime.Now.ToString("yyyyMMdd"), name);
        }
    }
}
