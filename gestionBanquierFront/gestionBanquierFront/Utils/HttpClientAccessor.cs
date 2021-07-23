using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace gestionBanquierFront.Utils
{
    public static class HttpClientAccessor
    {
        public static Func<HttpClient> ValueFactory = () => {
            var client = new HttpClient();

            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8082/api/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        };

        private static Lazy<HttpClient> client = new Lazy<HttpClient>(ValueFactory);

        public static HttpClient HttpClient
        {
            get { return client.Value; }
        }
    }
}