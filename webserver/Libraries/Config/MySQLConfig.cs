using System.Collections.Generic;

namespace WebServer.Libraries.Config
{
    class MySQLConfig : BaseConfig
    {
        /// <summary>
        /// Can trim config line
        /// </summary>
        protected override bool CanTrim()
        {
            return true;
        }

        /// <summary>
        /// Parse a line
        /// </summary>
        private KeyValuePair<string, string> ParseLine(string line)
        {
            int postition = line.IndexOf("=");

            if (postition == -1)
            {
                return new KeyValuePair<string, string>(null, null);
            }

            return new KeyValuePair<string, string>(line.Substring(0, postition), line.Substring(postition + 1));
        }

        /// <summary>
        /// Read config
        /// </summary>
        public override void Parse()
        {
            this.config = new List<KeyValuePair<string, string>> { };
            KeyValuePair<string, string> config;
            int open_count = 0;

            foreach (string line in this.lines)
            {
                if (line.StartsWith("<"))
                {
                    open_count++;
                }
                else if(line.StartsWith("</"))
                {
                    open_count--;
                }
                else if (line.StartsWith("#"))
                {
                    continue;
                }

                if (open_count > 0)
                {
                    continue;
                }

                config = this.ParseLine(line);

                if (config.Key != null)
                {
                    base.config.Add(config);
                }
            }
        }

        /// <summary>
        /// Append config
        /// </summary>
        public override void Append(string key, string value)
        {
            this.lines.Add(key + "=" + value);
            this.Parse();
        }

        /// <summary>
        /// Change config root
        /// </summary>
        public void ChangeDataDir(string path)
        {
            this.Replace(@"^(\s*)datadir(\s*)=(.*)$", "datadir=" + path.Replace("\\", "/"));
        }

        /// <summary>
        /// Change plugin root
        /// </summary>
        public void ChangePluginDir(string path)
        {
            this.Replace(@"^(\s*)plugin-dir(\s*)=(.*)$", "plugin-dir=" + path.Replace("\\", "/"));
        }

        /// <summary>
        /// Change port
        /// </summary>
        public void ChangePort(int port)
        {
            this.Replace(@"^(\s*)port(\s*)=(\d+)$", "port=" + port.ToString(), false);
        }

        /// <summary>
        /// Get Listen port
        /// </summary>
        public int GetPort()
        {
            if (this.Exists("port"))
            {
                return int.Parse(this.Get("port")[0].Value);
            }
            else
            {
                return 0;
            }
        }
    }
}
