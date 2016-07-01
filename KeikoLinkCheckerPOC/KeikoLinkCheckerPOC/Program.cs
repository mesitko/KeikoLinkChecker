using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace KeikoLinkCheckerPOC
{
    class Program
    {
        static void Main(string[] args)
        {
            if( args == null || args.Length < 2)
            {
                Console.WriteLine("Incorect number of args..");
                return;
            }

            try
            {

                String str = "";

                using (StreamReader sr = new StreamReader(args[0]))
                {
                    str = sr.ReadToEnd();

                }
                var lines = str.Replace("\r", "").Split('\n');
                StringBuilder sb = new StringBuilder();
                var i = 0;
                foreach (var line in lines)
                {
                    if (line.Length > 0)
                    {
                        var res = GetStatus(line);
                        
                        sb.AppendLine(string.Format("\"{0}\",\"{1}\",\"{2}\"", res.RequestUrl, res.Status, res.ResponseUrl));

                        Console.WriteLine("Processing: {0} of {1}, Progress: {2}%", i+1, lines.Length,   (int)Math.Floor( (i * 100.0 )/lines.Length));

                        i++;
                    }
                }
                var result = sb.ToString();

                using (StreamWriter sw = new StreamWriter(args[1]))
                {
                    sw.Write(result);
                    sw.Close();
                }
                Console.WriteLine("=========== Done ===========");

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        public static RequestResult GetStatus(string url)
        {
            HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create(url);
            try
            {
                webReq.CookieContainer = new CookieContainer();
                webReq.Method = "GET";
                using (WebResponse response = webReq.GetResponse())
                {
                    /*
                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream);
                        var res = reader.ReadToEnd();
                        
                    }
                     */
                    if (response.ResponseUri.ToString() != url)
                    {
                        return new RequestResult(url, "301", response.ResponseUri.ToString());
                    }
                    return new RequestResult(url, "200", response.ResponseUri.ToString());
                }
            }
            catch (Exception ex)
            {
                return new RequestResult(url, "404", "Page not found");
            }
        }
    }

    public class RequestResult
    {
        private string _requestUrl, _status, _responseUrl;

        public string RequestUrl { get { return _requestUrl; } }
        public string Status {get { return _status; }}
        public string ResponseUrl { get { return _responseUrl; } }

        public RequestResult( string requestUrl, string status, string responseUrl)
        {
            _requestUrl = requestUrl;
            _status = status;
            _responseUrl = responseUrl;
        }
    }
}
