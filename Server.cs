using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KeyExchangeSample
{
    static class Server
    {
        const string baseLink = "https://eznote.herokuapp.com/api";

        public static async Task<string> SendRequestAsync(HttpMethod method, string category, string command, NameValueCollection collection)
        {
            NameValueCollection httpValueCollection = HttpUtility.ParseQueryString(string.Empty);
            httpValueCollection.Add(collection);
            var query = httpValueCollection.ToString();

            var request = new HttpRequestMessage(method, new Uri($"{baseLink}/{category}/{command}?{query}"));

            Console.Error.WriteLine(request.RequestUri);

            using var client = new HttpClient();
            using var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
