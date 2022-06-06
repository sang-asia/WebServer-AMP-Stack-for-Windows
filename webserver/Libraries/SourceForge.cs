using System.Net;

namespace WebServer.Libraries
{
    static class SourceForge
    {
        public static string GetDownloadUrl(string url)
        {
            string final_url = url;

            for (int i = 0; i < 10; i++)
            {
                HttpWebResponse response;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "SlabWAMPSuite";
                request.AllowAutoRedirect = false;

                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (System.Exception)
                {
                    continue;
                }

                url = response.Headers["Location"];

                if (url == null)
                {
                    return final_url;
                }

                final_url = url;
                response.Close();
            }

            return null;
        }
    }
}
