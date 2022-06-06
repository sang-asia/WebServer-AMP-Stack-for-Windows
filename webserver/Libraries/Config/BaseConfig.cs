using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace WebServer.Libraries.Config
{
    public delegate void ActionRef<T1, T2>(ref T1 p1, ref T2 p2);

    abstract public class BaseConfig
    {
        protected List<string> lines = new List<string> { };
        protected List<KeyValuePair<string, string>> config = new List<KeyValuePair<string, string>> { };

        abstract protected bool CanTrim();

        public List<KeyValuePair<string, string>> Config
        {
            get => this.config;
        }

        /// <summary>
        /// Read config to memory
        /// </summary>
        public void Open(string file)
        {
            string line;
            this.lines.Clear();
            StreamReader stream = new StreamReader(file);

            while ((line = stream.ReadLine()) != null)
            {
                if (this.CanTrim())
                {
                    this.lines.Add(line.Trim());
                }
                else
                {
                    this.lines.Add(line);
                }
            }

            stream.Close();
            this.Parse();
        }

        /// <summary>
        /// Parse memory config to list
        /// </summary>
        abstract public void Parse();

        /// <summary>
        /// Append config
        /// </summary>
        abstract public void Append(string key, string value);

        /// <summary>
        /// Append multiple lines
        /// </summary>
        public void Append(string[] lines)
        {
            foreach (string line in lines)
            {
                this.lines.Add(line);
            }

            this.Parse();
        }

        /// <summary>
        /// Save config
        /// </summary>
        public void Write(string file)
        {
            StreamWriter stream = new StreamWriter(file, false);

            foreach (string line in this.lines)
            {
                stream.WriteLine(line);
            }

            stream.Flush();
            stream.Close();
        }

        /// <summary>
        /// Search and replace config lines
        /// </summary>
        public int Replace(string pattern, string replace, bool stop_on_first = true, bool case_sensitivity = false)
        {
            return this.Search(pattern, stop_on_first, case_sensitivity, (ref List<string> l, ref int i) =>
            {
                l[i] = Regex.Replace(l[i], pattern, replace, case_sensitivity ? RegexOptions.None : RegexOptions.IgnoreCase);
            });
        }

        /// <summary>
        /// Search config lines
        /// </summary>
        public int Search(string pattern,
                          bool stop_on_first = true,
                          bool case_sensitivity = false,
                          ActionRef<List<string>, int> action_for_match = null,
                          Action<BaseConfig> action_not_found = null)
        {
            int counter = 0;
            RegexOptions opt = case_sensitivity ? RegexOptions.None : RegexOptions.IgnoreCase;

            for (int i = 0; i < this.lines.Count; i++)
            {
                if (Regex.IsMatch(this.lines[i], pattern, opt))
                {
                    counter++;
                    action_for_match?.Invoke(ref this.lines, ref i);

                    if (stop_on_first)
                    {
                        break;
                    }
                }
            }

            if (counter > 0)
            {
                this.Parse();
            }
            else
            {
                action_not_found?.Invoke(this);
            }

            return counter;
        }
        
        /// <summary>
        /// Check config key exists
        /// </summary>
        public bool Exists(string key)
        {
            foreach (KeyValuePair<string, string> item in this.config)
            {
                if (item.Key == key)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get config
        /// </summary>
        public List<KeyValuePair<string, string>> Get(string key)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>> { };

            foreach (KeyValuePair<string, string> item in this.config)
            {
                if (item.Key == key)
                {
                    list.Add(item);
                }
            }

            return list;
        }

        /// <summary>
        /// Set config
        /// </summary>
        public void Add(string key, string value)
        {
            this.Config.Add(new KeyValuePair<string, string>(key, value));
            this.Append(key, value);
        }
    }
}
