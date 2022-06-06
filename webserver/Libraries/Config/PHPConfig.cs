using System.Collections.Generic;

namespace WebServer.Libraries.Config
{
    public class PHPConfig : BaseConfig
    {
        /// <summary>
        /// Can trim config line
        /// </summary>
        protected override bool CanTrim()
        {
            return true;
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
        /// Parse a line
        /// </summary>
        private KeyValuePair<string, string> ParseLine(string line)
        {
            int postition = line.IndexOf("=");

            if (postition == -1)
            {
                return new KeyValuePair<string, string>(null, null);
            }

            return new KeyValuePair<string, string>(line.Substring(0, postition).Trim(), line.Substring(postition + 1).Trim());
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
                if (line.StartsWith(";") || line.StartsWith("#"))
                {
                    continue;
                }

                config = this.ParseLine(line);

                if (config.Key != null)
                {
                    this.config.Add(config);
                }
            }
        }

        /// <summary>
        /// Set default value for missing essential configs
        /// </summary>
        public void SetDefaultDirectives()
        {
            if (!this.Exists("upload_max_filesize"))
            {
                this.Add("upload_max_filesize", "50M");
            }

            if (!this.Exists("post_max_size"))
            {
                this.Add("post_max_size", "50M");
            }

            if (!this.Exists("max_execution_time"))
            {
                this.Add("max_execution_time", "300");
            }

            if (!this.Exists("max_input_vars"))
            {
                this.Add("max_input_vars", "10000");
            }
        }

        /// <summary>
        /// Set default extensions
        /// </summary>
        public void SetDefaultExtensions()
        {
            this.ClearExtensions();
            this.Add("extension", "bz2");
            this.Add("extension", "curl");
            this.Add("extension", "exif");
            this.Add("extension", "fileinfo");
            this.Add("extension", "gd");
            this.Add("extension", "gettext");
            this.Add("extension", "intl");
            this.Add("extension", "mbstring");
            this.Add("extension", "mysqli");
            this.Add("zend_extension", "opcache");
            this.Add("extension", "openssl");
            this.Add("extension", "pdo_mysql");
        }

        /// <summary>
        /// Clear extension directives
        /// </summary>
        public void ClearExtensions()
        {
            this.Search(
                pattern: @"(\s*)((zend_)*)extension(\s*)=(\s*)(.*)",
                stop_on_first: false,
                action_for_match: (ref List<string> lines, ref int index) =>
                {
                    lines.RemoveAt(index);
                    index--;
                }
            );
        }

        /// <summary>
        /// Set directive value, auto append if missing
        /// </summary>
        public void SetDirective(string directive, string value)
        {
            this.Search(@"(\s*)" + directive + @"(\s*)=(\s*)(.*)",
                   action_for_match: (ref List<string> lines, ref int index) =>
                   {
                       lines[index] = directive + "=" + value;
                   },
                   action_not_found: (BaseConfig c) =>
                   {
                       c.Append(directive, value);
                   }
               );
        }
    }
}
