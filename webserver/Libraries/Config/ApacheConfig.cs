using System.Collections.Generic;

namespace WebServer.Libraries.Config
{
    class ApacheConfig : BaseConfig
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
            int postition = line.IndexOf(" ");

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
                if (line.StartsWith("</"))
                {
                    open_count--;
                }
                else if (line.StartsWith("<"))
                {
                    open_count++;
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
            this.lines.Add(key + " " + value);
            this.Parse();
        }

        /// <summary>
        /// Check a directive is activated in config
        /// </summary>
        public bool HasDirective(string directive, string value)
        {
            List<KeyValuePair<string, string>> directives = this.Get(directive);

            foreach (KeyValuePair<string, string> d in directives)
            {
                if (d.Value == value)
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// Check a module is loaded in config
        /// </summary>
        public bool HasMimeType(string type)
        {
            return this.HasDirective("AddType", type);
        }


        /// <summary>
        /// Check a module is loaded in config
        /// </summary>
        public bool IsLoaded(string module)
        {
            return this.HasDirective("LoadModule", module);
        }

        /// <summary>
        /// Check a path is included in config
        /// </summary>
        public bool IsIncluded(string path)
        {
            return this.HasDirective("Include", path);
        }

        /// <summary>
        /// Check a path is included in config
        /// </summary>
        public bool IsIncludedOptional(string path)
        {
            return this.HasDirective("IncludeOptional", path);
        }

        /// <summary>
        /// Get Root path
        /// </summary>
        /// <returns></returns>
        public string GetRoot()
        {
            List<KeyValuePair<string, string>> defines = this.Get("Define");

            foreach (KeyValuePair<string, string> d in defines)
            {
                if (d.Value.StartsWith("SRVROOT "))
                {
                    return d.Value.Replace("SRVROOT ", "").Replace("\"", "");
                }
            }

            return null;
        }

        /// <summary>
        /// Change config root
        /// </summary>
        public void ChangeRoot(string path)
        {
            this.Replace(@"^(\s*)Define(\s*)SRVROOT(\s*)""([^""]*)""(\s*)$", "Define SRVROOT \"" + path + "\"");
            this.Parse();
        }

        /// <summary>
        /// Change port
        /// </summary>
        public void ChangePort(int port)
        {
            this.Replace(@"^(\s*)Listen(\s*)([\d]+)(\s*)$", "Listen " + port.ToString());
            this.Parse();
        }

        /// <summary>
        /// Get Listen port
        /// </summary>
        public int GetPort()
        {
            if (this.Exists("Listen"))
            {
                return int.Parse(this.Get("Listen")[0].Value);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Set directive
        /// </summary>
        public bool SetDirective(string directive, string value)
        {
            bool is_changed = false;

            this.Search(@"^(\s*)" + directive + @"(\s*)(.+)(\s*)(.+)(\s*)$",
                   action_for_match: (ref List<string> lines, ref int index) =>
                   {
                       string proper_line = directive + " " + value;

                       if (lines[index] != proper_line)
                       {
                           lines[index] = proper_line;
                           is_changed = true;
                       }
                   },
                   action_not_found: (BaseConfig c) =>
                   {
                       c.Append(directive, value);
                   }
               );

            return is_changed;
        }
    }
}
