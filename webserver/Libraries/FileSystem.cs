using System;
using System.Collections.Generic;
using System.IO;

namespace WebServer.Libraries
{
    static class FileSystem
    {
        /// <summary>
        /// Check a path is a folder and path is not a file
        /// </summary>
        public static bool IsFolderNorFile(string path, bool auto_create = true)
        {
            if (File.Exists(path))
            {
                return false;
            }

            if (auto_create && !Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return true;
        }

        /// <summary>
        /// Delete File/Directory
        /// </summary>
        public static bool Delete(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            else
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check path existence
        /// </summary>
        public static bool Exists(string path)
        {
            return File.Exists(path) || Directory.Exists(path);
        }

        /// <summary>
        /// Check multiple paths existence
        /// </summary>
        public static bool Exists(string[] paths)
        {
            bool result = true;

            for (int i = 0; i < paths.Length; i++)
            {
                result = result && Exists(paths[i]);
            }

            return result;
        }

        /// <summary>
        /// Check existence of relative paths (string type) inside a root
        /// </summary>
        public static bool Exists(string root, string[] relative_paths)
        {
            List<string> paths = new List<string> { };

            for (int i = 0; i < relative_paths.Length; i++)
            {
                paths.Add(Path.Combine(root, relative_paths[i]));
            }

            return Exists(paths.ToArray());
        }

        /// <summary>
        /// Check existence of relative paths (array type) inside a root
        /// </summary>
        public static bool Exists(string root, string[][] relative_paths)
        {
            List<string> paths = new List<string> { };

            for (int i = 0; i < relative_paths.Length; i++)
            {
                paths.Add(Path.Combine(root, Path.Combine(relative_paths[i])));
            }

            return Exists(paths.ToArray());
        }

        /// <summary>
        /// Get current user directory
        /// </summary>
        public static string GetUserDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }

        /// <summary>
        /// Get current user's documents directory
        /// </summary>
        public static string GetUserDocumentsDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
        }

        /// <summary>
        /// Check whether file is ready for write or not
        /// </summary>
        public static bool IsFileReady(string filename)
        {
            // If the file can be opened for exclusive access it means that the file
            // is no longer locked by another process.
            try
            {
                using (FileStream inputStream = File.Open(filename, FileMode.Append, FileAccess.ReadWrite, FileShare.None))
                {
                    return inputStream.Length > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
