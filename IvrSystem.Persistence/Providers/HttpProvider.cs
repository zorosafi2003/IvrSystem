using IvrSystem.Persistence.IProviders;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvrSystem.Persistence.Providers
{
    public class HttpProvider : IHttpProvider
    {
        public HttpProvider()
        {

        }

        public async Task<RestResponse<T>> Post<T>(string url , object model)
        {
            var options = new RestClientOptions(url);
            var client = new RestClient(options);

            var request = new RestRequest("",Method.Post).AddJsonBody(model);

            return await client.ExecuteAsync<T>(request);
        }
    }
}
