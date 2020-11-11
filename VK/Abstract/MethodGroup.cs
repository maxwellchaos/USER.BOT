using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MethodButton.VK.Abstract
{
    public abstract class MethodGroup
    {
        public abstract string Token { get; set; }

        protected HttpClient httpClient = new HttpClient();

        protected async Task<HttpResponseMessage> SendRequest(string link)
        {
            return await httpClient.GetAsync(link);
        }
    }
}
