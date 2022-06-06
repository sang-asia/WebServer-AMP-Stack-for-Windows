using System;
using System.Collections.Generic;
using System.Linq;

namespace WebServer.Libraries.Config
{
    class MongoConfig : BaseConfig
    {
        /// <summary>
        /// Can trim config line
        /// </summary>
        protected override bool CanTrim()
        {
            return false;
        }

        /// <summary>
        /// Append config
        /// </summary>
        public override void Append(string key, string value)
        {
            string[] parts = key.Split('.');
            string indent = "";

            for (int i = 0; i < parts.Length; i++)
            {
                if (i + 1 == parts.Length)
                {
                    this.lines.Add(indent + parts[i] + ": " + value);
                }
                else
                {
                    this.lines.Add(indent + parts[i] + ":");
                }

                indent += "   ";
            }

            this.Parse();
        }

        /// <summary>
        /// Parse config
        /// </summary>
        public override void Parse()
        {
            this.config = new List<KeyValuePair<string, string>> { };
            List<string> prefix = new List<string> { };
            List<int> prefix_spaces = new List<int> { };
            int previous_spaces = -1;

            foreach (string line in this.lines)
            {
                if (line.Trim().StartsWith("#"))
                {
                    continue;
                }

                int c = line.TakeWhile(Char.IsWhiteSpace).Count();
                int p = line.IndexOf(':');
                string k = line.Substring(0, p).Trim();
                string v = line.Substring(p + 1).Trim();

                if (v.Length > 0)
                {
                    k = string.Join(".", prefix.ToArray().Union(new List<string> { k }));
                    base.config.Add(new KeyValuePair<string, string>(k, v));
                }
                else
                {
                    if (c <= previous_spaces && prefix.Count > 0)
                    {
                        while (prefix.Count > 0 && prefix_spaces[prefix_spaces.Count - 1] >= c)
                        {
                            prefix.RemoveAt(prefix.Count - 1);
                            prefix_spaces.RemoveAt(prefix_spaces.Count - 1);
                        }
                    }

                    prefix.Add(k);
                    prefix_spaces.Add(c);
                }

                previous_spaces = c;
            }
        }

        /// <summary>
        /// Get port
        /// </summary>
        public int GetPort()
        {
            if (!this.Exists("net.port"))
            {
                return 0;
            }

            List<KeyValuePair<string, string>> ports = this.Get("net.port");

            if (ports.Count == 0)
            {
                return 0;
            }

            return int.Parse(ports[ports.Count - 1].Value);
        }

        /// <summary>
        /// Change port
        /// </summary>
        public void ChangePort(int port)
        {
            this.Search(@"(\s+)port(\s*):(\s*)(\d+)", action_for_match: (ref List<string> lines, ref int index) =>
            {
                this.lines[index] = @"   port: " + port.ToString();
            });
        }
    }
}
