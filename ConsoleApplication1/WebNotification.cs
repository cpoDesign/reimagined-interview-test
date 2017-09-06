using System;
using System.IO;
using System.Net;
using System.Text;

namespace ConsoleApplication1
{
    public class WebNotification : IWebNotification
    {
        private ILogger _logger;
        private ISettings _settings;

        public WebNotification(ILogger logger, ISettings settings)
        {
            if(logger == null) throw new ArgumentNullException(nameof(logger));
            if(settings == null) throw new ArgumentNullException(nameof(logger));
            _logger = logger;
        }

        public bool Notify(string message)
        {
            var url = _settings.GetKey("NotifyUrl") + message;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            // Set some reasonable limits on resources used by this request
            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;
            // Set credentials to use for this request.
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Console.WriteLine("Content length is {0}", response.ContentLength);
            Console.WriteLine("Content type is {0}", response.ContentType);

            // Get the stream associated with the response.
            Stream receiveStream = response.GetResponseStream();

            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

            Console.WriteLine("Response stream received.");
            Console.WriteLine(readStream.ReadToEnd());
            System.IO.File.WriteAllText(@"D:\path.txt", readStream.ReadToEnd());
            response.Close();
            readStream.Close();

            return response.StatusCode == HttpStatusCode.Accepted;
        }
    }
}