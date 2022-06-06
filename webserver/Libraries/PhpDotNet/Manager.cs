using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WebServer.Libraries.Downloader;

namespace WebServer.Libraries.PhpDotNet
{
    enum EVersionType { Archive, Release }

    class Manager
    {
        private static readonly string DOMAIN = "https://windows.php.net";
        private static readonly string FILE_TEMP = Path.Combine(Program.DIR_TEMP, Guid.NewGuid().ToString());
        private static readonly string PATTERN = Program.IS_X64
            ? @"/downloads/releases/(archives/)*php-(\d){1}.(\d)+.(\d)+-Win32-V([CS]{1})(\d)+-x64.zip"
            : @"/downloads/releases/(archives/)*php-(\d){1}.(\d)+.(\d)+-Win32-V([CS]{1})(\d)+-x86.zip";
        private static readonly string URL_ARCHIVE = DOMAIN + "/downloads/releases/archives/";
        private static readonly string URL_RELEASE = DOMAIN + "/downloads/releases/";

        private Dictionary<Version, Install> _installs;
        private Dictionary<Version, Release> _releases;
        private EVersionType _type = EVersionType.Release;
        private string _url = URL_RELEASE;

        public Dictionary<Version, Install> Installs
        {
            get => _installs;
        }

        public Dictionary<Version, Release> Releases
        {
            get => _releases;
        }

        public EVersionType Type
        {
            get => _type;
            set {
                _type = value;

                if (_type == EVersionType.Archive)
                {
                    _url = URL_ARCHIVE;
                }
                else if (_type == EVersionType.Release)
                {
                    _url = URL_RELEASE;
                }
            }
        }

        public string Url
        {
            get => _url;
        }

        /// <summary>
        /// Install a PHP version
        /// </summary>
        public bool Install(Release release)
        {
            string dir_bin = Path.Combine(Program.DIR_PHP, release.Version.ToString(3));
            string dir_temp = Path.Combine(Program.DIR_TEMP, Guid.NewGuid().ToString());

            if (Directory.Exists(dir_bin))
            {
                return false;
            }

            Job job = Downloader.Manager.Add(release.Url, FILE_TEMP);

            try
            {
                job.Start(false);
            }
            catch (Exception)
            {
                return false;
            }

            ZipFile.ExtractToDirectory(FILE_TEMP, dir_temp);
            File.Delete(FILE_TEMP);
            Directory.Move(dir_temp, dir_bin);

            return true;
        }

        /// <summary>
        /// Refresh installed versions
        /// </summary>
        /// <returns></returns>
        public bool RefreshInstalls()
        {
            string[] dirs;
            _installs = new Dictionary<Version, Install> { };

            try
            {
                dirs = Directory.GetDirectories(Program.DIR_PHP);
            }
            catch (Exception)
            {
                return false;
            }

            foreach (string dir in dirs)
            {
                Install i = new Install(new DirectoryInfo(dir).Name, dir);
                _installs.Add(i.Version, i);
            }

            return true;
        }

        /// <summary>
        /// Refresh releases list
        /// </summary>
        public bool RefreshReleases()
        {
            _releases = new Dictionary<Version, Release> { };

            Job job = Downloader.Manager.Add(_url, FILE_TEMP);

            try
            {
                job.Start(false);
            }
            catch (Exception)
            {
                return false;
            }
            
            string html = File.ReadAllText(FILE_TEMP);
            File.Delete(FILE_TEMP);
            MatchCollection matches;

            try
            {
                matches = Regex.Matches(html, PATTERN, RegexOptions.IgnoreCase);
            }
            catch (Exception)
            {
                return false;
            }

            foreach (Match m in matches)
            {
                string v = m.Groups[2].Value + "." + m.Groups[3].Value + "." + m.Groups[4].Value;
                Release r = new Release(v, DOMAIN + m.Value);

                if (!_releases.ContainsKey(r.Version))
                {
                    _releases.Add(r.Version, r);
                }
            }

            return true;
        }

        /// <summary>
        /// Remove an installed version
        /// </summary>
        public bool Remove(Install install)
        {
            if (!Directory.Exists(install.Path))
            {
                return false;
            }

            try
            {
                Directory.Delete(install.Path, true);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
