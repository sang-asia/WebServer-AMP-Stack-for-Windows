using System;
using System.IO;
using System.Collections.Generic;

namespace WebServer.Libraries.Config
{
    class HostsConfig : BaseConfig
    {
        public static string FILE_HOSTS = Path.Combine(Environment.SystemDirectory, "drivers", "etc", "hosts");

        /// <summary>
        /// Can trim config line
        /// </summary>
        protected override bool CanTrim()
        {
            return true;
        }

        /// <summary>
        /// Get config instance
        /// </summary>
        public static HostsConfig Get()
        {
            HostsConfig config = new HostsConfig();
            config.Open(FILE_HOSTS);
            config.Parse();
            return config;
        }

        /// <summary>
        /// Append config
        /// </summary>
        public override void Append(string key, string value)
        {
            this.lines.Add(value + " " + key);
            this.Parse();
        }

        /// <summary>
        /// Parse a line
        /// </summary>
        private KeyValuePair<string, string> ParseLine(string line)
        {
            int postition = line.IndexOf(" ");

            if (postition == -1)
            {
                return new KeyValuePair<string, string>(null, null);
            }

            return new KeyValuePair<string, string>(line.Substring(postition + 1).Trim(), line.Substring(0, postition).Trim());
        }

        /// <summary>
        /// Parse config
        /// </summary>
        public override void Parse()
        {
            this.config = new List<KeyValuePair<string, string>> { };
            KeyValuePair<string, string> config;

            foreach (string line in this.lines)
            {
                if (line.StartsWith("#"))
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
        /// Remove domain
        /// </summary>
        public void Remove(string domain)
        {
            this.Search(@"(.+)(\s+)" + domain + @"(\s*)", action_for_match: (ref List<string> lines, ref int index) =>
            {
                this.lines.RemoveAt(index);
                index--;
            });
        }
    }
}
