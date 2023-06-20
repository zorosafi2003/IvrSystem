using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvrSystem.Persistence.IProviders
{
    public interface IHttpProvider
    {
        Task<RestResponse<T>> Post<T>(string url, object model);
    }
}
