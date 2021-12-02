using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace ngschaiderBot
{
    class Utils
    {

        public static string GetRequest(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        public static string RGBtoHex(int r, int g, int b) {
            return "#" + r.ToString("X") + g.ToString("X") + b.ToString("X");
        }

    }
}
