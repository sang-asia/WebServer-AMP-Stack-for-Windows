using System.IO;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using System;

namespace WebServer.Libraries.Config
{
    public class WebsiteConfig
    {
        public static string FILE_CONFIG = "httpd.conf";
        public static string FILE_CONFIG_SSL = "httpd_ssl.conf";
        public static string FILE_LOG = "error.log";
        public static string FILE_PHP_INI = "php.ini";
        public static string FILE_EXTERNAL = ".external";
        public static string FOLDER_WWW = "public_html";

        // HTTP
        public string BaseDirectory = "";
        public string DocumentRoot = "";
        public string ErrorLog = "";
        public bool External = false;
        public List<KeyValuePair<string, string>> FcgidInitialEnv = new List<KeyValuePair<string, string>> { };
        public List<KeyValuePair<string, string>> FcgidWrapper = new List<KeyValuePair<string, string>> { };
        public string Name = "";
        public PHPConfig PHPConfig = new PHPConfig();
        public string PHPVersion = "";
        public int Port = 80;
        public string PublicPath = "";
        public List<string> ServerAliases = new List<string> { };
        public string ServerName = "";

        // HTTPS
        public string SslConfigFile;
        public string SslBaseDirectory = "";
        public string SslCert = "";
        public string SslCertKey = "";
        public string SslDocumentRoot = "";
        public string SslErrorLog = "";
        public List<KeyValuePair<string, string>> SslFcgidInitialEnv = new List<KeyValuePair<string, string>> { };
        public List<KeyValuePair<string, string>> SslFcgidWrapper = new List<KeyValuePair<string, string>> { };
        public string SslPHPVersion = "";
        public int SslPort = 0;
        public string SslPublicPath = "";
        public string SslServerName = "";

        /// <summary>
        /// Constructor
        /// </summary>
        public WebsiteConfig()
        {
            this.PHPConfig.SetDefaultDirectives();
            this.PHPConfig.SetDefaultExtensions();
        }

        /// <summary>
        /// Add SSL support
        /// </summary>
        public void AddSslSupport(int ssl_port)
        {
            this.SslBaseDirectory = this.BaseDirectory;
            this.SslDocumentRoot = this.DocumentRoot;
            this.SslErrorLog = this.ErrorLog;
            this.SslFcgidInitialEnv = this.FcgidInitialEnv;
            this.SslFcgidWrapper = this.FcgidWrapper;
            this.SslPort = ssl_port;
            this.SslPublicPath = this.PublicPath;
            this.SslServerName = this.ServerName;
        }

        /// <summary>
        /// Remove SSL support
        /// </summary>
        public void RemoveSslSupport()
        {
            this.SslBaseDirectory = "";
            this.SslDocumentRoot = "";
            this.SslErrorLog = "";
            this.SslFcgidInitialEnv = new List<KeyValuePair<string, string>> { };
            this.SslFcgidWrapper = new List<KeyValuePair<string, string>> { };
            this.SslPort = 0;
            this.SslPublicPath = "";
            this.SslServerName = "";
        }

        /// <summary>
        /// Check whether this website has SSL or not
        /// </summary>
        public bool HasSsl()
        {
            return this.SslServerName != "" && this.SslPort != 0;
        }

        /// <summary>
        /// Get Document Root path
        /// </summary>
        public string GetDocumentRootPath(string folder)
        {
            return Path.GetFullPath(Path.Combine(folder, FOLDER_WWW, this.PublicPath));
        }

        /// <summary>
        /// Get log file path
        /// </summary>
        public string GetLogFilePath(string folder)
        {
            return Path.Combine(folder, FILE_LOG);
        }

        /// <summary>
        /// Format Windows path to Apache Config path
        /// </summary>
        private string FormatWindowsToConfigPath(string path)
        {
            return path.Replace(@"\", "/");
        }

        /// <summary>
        /// Format Apache Config path to Windows path
        /// </summary>
        private string FormatConfigToWindowsPath(string path)
        {
            return path.Replace("/", @"\");
        }

        /// <summary>
        /// Create config file
        /// </summary>
        public void Save(string folder)
        {
            string file_external = Path.Combine(folder, FILE_EXTERNAL);

            if (!this.External)
            {
                this.DocumentRoot = this.GetDocumentRootPath(folder);

                // Prevent public path outsite website folder
                if (!this.DocumentRoot.StartsWith(folder))
                {
                    this.PublicPath = "";
                    this.DocumentRoot = this.GetDocumentRootPath(folder);
                }

                this.BaseDirectory = folder;

                if (File.Exists(file_external))
                {
                    try
                    {
                        File.Delete(file_external);
                    }
                    catch (System.Exception) { }
                }
            }
            else
            {
                if (!File.Exists(file_external))
                {
                    File.Create(file_external).Close();
                }
            }

            this.ErrorLog = this.GetLogFilePath(folder);
            this.SslCert = Path.Combine(Program.DIR_CONFIG, "server.crt");
            this.SslCertKey = Path.Combine(Program.DIR_CONFIG, "server.key");

            // Create document root
            if (!FileSystem.Exists(this.DocumentRoot))
            {
                Directory.CreateDirectory(this.DocumentRoot);
                DirectorySecurity security = Directory.GetAccessControl(FileSystem.GetUserDocumentsDirectory());
                Directory.SetAccessControl(this.DocumentRoot, security);
            }

            // Create error log
            if (!FileSystem.Exists(this.ErrorLog))
            {
                File.WriteAllText(this.ErrorLog, "");
            }

            // Create HTTP config
            string httpd_config = @"<VirtualHost *:" + this.Port.ToString() + @">
	ServerName " + this.ServerName + @"
	DocumentRoot """ + this.FormatWindowsToConfigPath(this.DocumentRoot) + @"""
	DirectoryIndex index.html index.php
	ErrorLog """ + this.FormatWindowsToConfigPath(this.ErrorLog) + @"""";

            foreach (KeyValuePair<string, string> item in this.FcgidInitialEnv)
            {
                if (FileSystem.Exists(item.Value))
                {
                    httpd_config += @"
    FcgidInitialEnv " + item.Key + @" """ + this.FormatWindowsToConfigPath(item.Value) + @"""";
                }
                else
                {
                    httpd_config += @"
    FcgidInitialEnv " + item.Key + @" """ + item.Value + @"""";
                }
            }

            httpd_config += @"
    FcgidMaxRequestLen 1073741824
    <Directory """ + this.FormatWindowsToConfigPath(this.BaseDirectory) + @""">
		AllowOverride All
		Options +ExecCGI
		Require all granted
		AddHandler fcgid-script .php";

            foreach (KeyValuePair<string, string> item in this.FcgidWrapper)
            {
                httpd_config += @"
        FcgidWrapper """ + this.FormatWindowsToConfigPath(item.Key).Replace(" ", @"\ ") + @""" " + item.Value;
            }

                httpd_config += @"
    </Directory>
</VirtualHost>
";

            File.WriteAllText(Path.Combine(folder, FILE_CONFIG), httpd_config);

            // Create SSL config
            string file_ssl_config = Path.Combine(folder, FILE_CONFIG_SSL);

            if (this.HasSsl())
            {
                this.SslDocumentRoot = this.DocumentRoot;
                this.SslErrorLog = this.ErrorLog;

                string https_config = @"<VirtualHost *:" + this.SslPort.ToString() + @">
	ServerName " + this.SslServerName + @"
	DocumentRoot """ + this.FormatWindowsToConfigPath(this.SslDocumentRoot) + @"""
	DirectoryIndex index.html index.php
	ErrorLog """ + this.FormatWindowsToConfigPath(this.SslErrorLog) + @"""
    SSLEngine on
    SSLCertificateFile """ + this.FormatWindowsToConfigPath(this.SslCert) + @"""
    SSLCertificateKeyFile """ + this.FormatWindowsToConfigPath(this.SslCertKey) + @"""
    BrowserMatch ""MSIE [2-5]"" nokeepalive ssl-unclean-shutdown downgrade-1.0 force-response-1.0
    <FilesMatch ""\.(cgi|shtml|phtml|php)$"">
        SSLOptions +StdEnvVars
    </FilesMatch>";

                foreach (KeyValuePair<string, string> item in this.SslFcgidInitialEnv)
                {
                    if (FileSystem.Exists(item.Value))
                    {
                        https_config += @"
    FcgidInitialEnv " + item.Key + @" """ + this.FormatWindowsToConfigPath(item.Value) + @"""";
                    }
                    else
                    {
                        https_config += @"
    FcgidInitialEnv " + item.Key + @" """ + item.Value + @"""";
                    }
                }

                https_config += @"
    FcgidMaxRequestLen 1073741824
    <Directory """ + this.FormatWindowsToConfigPath(this.BaseDirectory) + @""">
		AllowOverride All
		Options +ExecCGI
		Require all granted
		AddHandler fcgid-script .php";

                foreach (KeyValuePair<string, string> item in this.SslFcgidWrapper)
                {
                    https_config += @"
        FcgidWrapper """ + this.FormatWindowsToConfigPath(item.Key).Replace(" ", @"\ ") + @""" " + item.Value;
                }

                https_config += @"
    </Directory>
</VirtualHost>
";

                File.WriteAllText(file_ssl_config, https_config);
            }
            else if (File.Exists(file_ssl_config))
            {
                File.Delete(file_ssl_config);
            }

            // Configure PHP
            string file_ini = Path.Combine(folder, FILE_PHP_INI);

            if (this.PHPVersion == "")
            {
                File.Delete(file_ini);
            }
            else
            {
                this.PHPConfig.Write(file_ini);
            }
        }

        /// <summary>
        /// Parse a website config file
        /// </summary>
        private static WebsiteConfig ParseWebsiteConfig(string dir_website, string file_config, int default_port)
        {
            WebsiteConfig config = new WebsiteConfig();
            StreamReader sr = new StreamReader(file_config);
            string line, line_lc;
            bool started = false;

            config.Name = new DirectoryInfo(dir_website).Name;
            config.External = File.Exists(Path.Combine(dir_website, FILE_EXTERNAL))
                || !Directory.Exists(Path.Combine(dir_website, FOLDER_WWW));

            while ((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                line_lc = line.ToLower();

                if (started)
                {
                    if (line_lc.StartsWith("fcgidinitialenv"))
                    {
                        string[] parts = String.ParamParts(line);

                        if (parts.Length > 2)
                        {
                            config.FcgidInitialEnv.Add(new KeyValuePair<string, string>(parts[1], parts[2]));
                        }
                    }
                    else if (line_lc.StartsWith("fcgidwrapper"))
                    {
                        string[] parts = String.ParamParts(line);

                        if (parts.Length > 2)
                        {
                            if (parts[1].Split(' ').Length == parts[1].Split(new string[] { @"\ " }, StringSplitOptions.None).Length)
                            {
                                Match m = Regex.Match(parts[1], @"(.*)(\\|/)((\d+).(\d+).(\d+))(\\|/)php-cgi.exe(.*)");

                                if (m.Groups.Count > 2)
                                {
                                    config.PHPVersion = m.Groups[3].Value;
                                }

                                config.FcgidWrapper.Add(
                                    new KeyValuePair<string, string>(
                                        config.FormatConfigToWindowsPath(parts[1]).Replace(@"\ ", ""), parts[2]));
                            }
                        }
                    }
                    else if (line_lc.StartsWith("documentroot"))
                    {
                        string[] parts = String.ParamParts(line);

                        if (parts.Length > 1)
                        {
                            config.DocumentRoot = config.FormatConfigToWindowsPath(parts[1]);

                            if (config.DocumentRoot.EndsWith("\\"))
                            {
                                config.DocumentRoot = config.DocumentRoot.Substring(0, config.DocumentRoot.Length - 1);
                            }
                        }
                    }
                    else if (line_lc.StartsWith("errorlog"))
                    {
                        string[] parts = String.ParamParts(line);

                        if (parts.Length > 1)
                        {
                            config.ErrorLog = config.FormatConfigToWindowsPath(parts[1]);
                        }
                    }
                    else if (line_lc.StartsWith("serveralias"))
                    {
                        string[] aliases = String.ParamParts(line.Substring(11));

                        foreach (string a in aliases)
                        {
                            if (a.Trim() != "")
                            {
                                config.ServerAliases.Add(a.Trim());
                            }
                        }
                    }
                    else if (line_lc.StartsWith("servername"))
                    {
                        string[] parts = String.ParamParts(line);

                        if (parts.Length > 1)
                        {
                            config.ServerName = parts[1];
                        }
                    }
                    else if (line_lc.StartsWith("<directory"))
                    {
                        line = line.Replace("<", "").Replace(">", "");
                        string[] parts = String.ParamParts(line);

                        if (parts.Length > 1)
                        {
                            config.BaseDirectory = config.FormatConfigToWindowsPath(parts[1]);
                        }
                    }
                    else if (line_lc.StartsWith("sslcertificatefile"))
                    {
                        string[] parts = String.ParamParts(line);

                        if (parts.Length > 1)
                        {
                            config.SslCert = config.FormatConfigToWindowsPath(parts[1]);
                        }
                    }
                    else if (line_lc.StartsWith("sslcertificatekeyfile"))
                    {
                        string[] parts = String.ParamParts(line);

                        if (parts.Length > 1)
                        {
                            config.SslCertKey = config.FormatConfigToWindowsPath(parts[1]);
                        }
                    }
                }

                if (line_lc.StartsWith("<virtualhost"))
                {
                    started = true;
                    Match m = Regex.Match(line, @"<VirtualHost(.*):(\d+)>");

                    if (m.Groups.Count > 2)
                    {
                        config.Port = int.Parse(m.Groups[2].Value);
                    }
                    else
                    {
                        config.Port = default_port;
                    }
                }
                else if (line_lc.StartsWith("</virtualhost>"))
                {
                    if (config.DocumentRoot.StartsWith(config.BaseDirectory))
                    {
                        if (config.External)
                        {
                            Console.WriteLine(config.PublicPath);
                            Console.WriteLine(config.BaseDirectory);

                            config.PublicPath = config.DocumentRoot.Replace(config.BaseDirectory, "");
                        }
                        else
                        {
                            config.PublicPath = config.DocumentRoot.Replace(Path.Combine(config.BaseDirectory, FOLDER_WWW, ""), "");
                        }

                        config.PublicPath = config.PublicPath.Trim(new char[] { '\\', '/' });
                    }
                    else
                    {
                        config.PublicPath = "";
                    }

                    break;
                }
            }

            sr.Close();
            return config;
        }

        /// <summary>
        /// Check wrappers
        /// </summary>
        private static bool IsValidPhpWrapper(ref List<KeyValuePair<string, string>> wrappers, string valid_wrapper)
        {
            bool is_valid = true;

            for (int i = wrappers.Count - 1; i >= 0; i--)
            {
                if (wrappers[i].Value != ".php")
                {
                    continue;
                }

                if (wrappers[i].Key != valid_wrapper || !File.Exists(wrappers[i].Key))
                {
                    is_valid = false;
                    wrappers.RemoveAt(i);

                    if (File.Exists(valid_wrapper))
                    {
                        wrappers.Add(new KeyValuePair<string, string>(valid_wrapper, ".php"));
                    }
                }
            }

            return is_valid;
        }

        /// <summary>
        /// Check PHPRC path
        /// </summary>
        private static bool IsValidPhpRc(ref List<KeyValuePair<string, string>> envs, string valid_path)
        {
            bool is_valid = true;

            for (int i = 0; i < envs.Count; i++)
            {
                if (envs[i].Key != "PHPRC")
                {
                    continue;
                }

                if (envs[i].Value != valid_path)
                {
                    envs.RemoveAt(i);
                    envs.Add(new KeyValuePair<string, string>("PHPRC", valid_path));
                    is_valid = false;
                    i--;
                }
            }

            return is_valid;
        }

        /// <summary>
        /// Get all websites config in a folder
        /// </summary>
        public static Dictionary<string, List<WebsiteConfig>> GetWebsites(string path, int port_http, int port_https)
        {
            Dictionary<string, List<WebsiteConfig>> list = new Dictionary<string, List<WebsiteConfig>> { };

            string[] directories = Directory.GetDirectories(path);

            foreach (string dir_website in directories)
            {
                string file_http_config = Path.Combine(dir_website, FILE_CONFIG);
                string file_https_config = Path.Combine(dir_website, FILE_CONFIG_SSL);

                if (!File.Exists(file_http_config))
                {
                    continue;
                }

                WebsiteConfig website_config = ParseWebsiteConfig(dir_website, file_http_config, port_http);

                if (File.Exists(file_https_config))
                {
                    WebsiteConfig https_config = ParseWebsiteConfig(dir_website, file_https_config, port_https);
                    website_config.SslConfigFile = file_https_config;
                    website_config.SslBaseDirectory = https_config.BaseDirectory;
                    website_config.SslDocumentRoot = https_config.DocumentRoot;
                    website_config.SslErrorLog = https_config.ErrorLog;
                    website_config.SslFcgidInitialEnv = https_config.FcgidInitialEnv;
                    website_config.SslFcgidWrapper = https_config.FcgidWrapper;
                    website_config.SslPort = https_config.Port;
                    website_config.SslPublicPath = https_config.PublicPath;
                    website_config.SslServerName = https_config.ServerName;
                    website_config.SslCert = https_config.SslCert;
                    website_config.SslCertKey = https_config.SslCertKey;
                }

                string file_php_ini = Path.Combine(dir_website, FILE_PHP_INI);

                if (File.Exists(file_php_ini))
                {
                    website_config.PHPConfig.Open(file_php_ini);
                }

                if (!list.ContainsKey(file_http_config))
                {
                    list.Add(file_http_config, new List<WebsiteConfig> { });
                }

                list[file_http_config].Add(website_config);
            }

            return list;
        }

        /// <summary>
        /// Validate webistes config
        /// </summary>
        public static void ValidateConfig(WebsiteConfig website, string dir, int port_http, int port_https)
        {
            if (port_http == 0)
            {
                port_http = 80;
            }

            if (port_https == 0)
            {
                port_https = 443;
            }

            string dir_website = Path.Combine(dir, website.Name);
            bool need_save = false;

            if (website.Port != port_http)
            {
                website.Port = port_http;
                need_save = true;
            }

            if (!website.External && !website.BaseDirectory.StartsWith(dir_website))
            {
                website.BaseDirectory = dir_website;
                need_save = true;
            }

            if (!website.External && !website.DocumentRoot.StartsWith(dir_website))
            {
                website.DocumentRoot = website.GetDocumentRootPath(dir_website);
                need_save = true;
            }

            if (!website.ErrorLog.StartsWith(dir_website))
            {
                website.ErrorLog = website.GetLogFilePath(dir_website);
                need_save = true;
            }

            if (website.PHPVersion != "")
            {
                // Check php-cgi.exe path
                string file_php_wrapper = Path.Combine(Program.DIR_PHP, website.PHPVersion, "php-cgi.exe");

                // Check HTTP PHP wrapper
                if (!IsValidPhpWrapper(ref website.FcgidWrapper, file_php_wrapper))
                {
                    need_save = true;
                }

                if (!IsValidPhpRc(ref website.FcgidInitialEnv, dir_website))
                {
                    need_save = true;
                }

                // Check HTTPS
                if (website.HasSsl())
                {
                    // PHP wrapper
                    if (!IsValidPhpWrapper(ref website.SslFcgidWrapper, file_php_wrapper))
                    {
                        need_save = true;
                    }

                    if (!IsValidPhpRc(ref website.SslFcgidInitialEnv, dir_website))
                    {
                        need_save = true;
                    }

                    if (website.SslPort != port_https)
                    {
                        website.SslPort = port_https;
                        need_save = true;
                    }

                    if (!website.External && !website.SslBaseDirectory.StartsWith(dir_website))
                    {
                        website.SslBaseDirectory = dir_website;
                        need_save = true;
                    }

                    if (!website.External && !website.SslDocumentRoot.StartsWith(dir_website))
                    {
                        website.SslDocumentRoot = website.GetDocumentRootPath(dir_website);
                        need_save = true;
                    }

                    if (!website.SslErrorLog.StartsWith(dir_website))
                    {
                        website.SslErrorLog = website.GetLogFilePath(dir_website);
                        need_save = true;
                    }

                    if (!website.SslCert.StartsWith(Program.DIR_CONFIG) || !website.SslCertKey.StartsWith(Program.DIR_CONFIG))
                    {
                        need_save = true;
                    }
                }

                // Check php.ini
                string dir_ext = Path.Combine(Program.DIR_PHP, website.PHPVersion, "ext");

                if (!website.PHPConfig.Exists("extension_dir") || !website.PHPConfig.Get("extension_dir")[0].Value.StartsWith(dir_ext))
                {
                    website.PHPConfig.SetDirective("extension_dir", dir_ext);
                    need_save = true;
                }
            }

            // Remove PHP version if could not find any PHP wrapper after check above
            if (!website.FcgidWrapper.Exists((KeyValuePair<string, string> item) => item.Value == ".php"))
            {
                website.PHPVersion = "";
            }

            if (need_save)
            {
                website.Save(dir_website);
            }
        }

        /// <summary>
        /// Validate webistes config
        /// </summary>
        public static void ValidateConfig(Dictionary<string, List<WebsiteConfig>> websites, string dir, int port_http, int port_https)
        {
            

            foreach (KeyValuePair<string, List<WebsiteConfig>> file in websites)
            {
                foreach (WebsiteConfig website in file.Value)
                {
                    ValidateConfig(website, dir, port_http, port_https);
                }
            }
        }
    }
}
