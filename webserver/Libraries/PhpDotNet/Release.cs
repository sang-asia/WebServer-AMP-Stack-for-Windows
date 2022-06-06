using System;

namespace WebServer.Libraries.PhpDotNet
{
    class Release
    {
        private string _url;
        private Version _version;

        public string Url { get => _url; }
        public Version Version { get => _version; }

        public Release(string version, string url)
        {
            Version.TryParse(version, out _version);
            _url = url;
        }
    }
}
