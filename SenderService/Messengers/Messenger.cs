using SenderService.Administration;
using SenderService.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace SenderService.Messengers
{
    public class Messenger : ISender
    {
        public string ReturningMessage { get; set; }

        public virtual string UrlTemplate { get; set; }
        public virtual Dictionary<string, string> UrlTemplateData { get; set; }

        public virtual void SendMessage()
        {
            var request = WebRequest.Create(UrlTemplate);
            var responseStream = request.GetResponse().GetResponseStream();
            var reader = new StreamReader(responseStream);
            var line = string.Empty;

            var resultResponse = string.Empty;
            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                    resultResponse += line;
            }

            OutputService.Write(resultResponse, true, false, null);
        }
    }
}
