﻿using System.IO;
using System.Net;

namespace FunkyBDD.SxS.Framework
{
    public class Api
    {
        private readonly WebRequest request;
        private readonly HttpWebResponse response;
        private readonly string  responseContent;
        private readonly string status;

        public Api(string url)
        {
            request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            responseContent = reader.ReadToEnd();
            status = response.StatusDescription;
            reader.Close();
            dataStream.Close();
            response.Close();
        }

        public string Status => status;
    }
}
