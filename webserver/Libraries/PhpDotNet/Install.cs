using System;

namespace WebServer.Libraries.PhpDotNet
{
    class Install
    {
        private string _path;
        private Version _version;

        public string Path { get => _path; }
        public Version Version { get => _version; }

        public Install(string version, string path)
        {
            Version.TryParse(version, out _version);
            _path = path;
        }
    }
}
