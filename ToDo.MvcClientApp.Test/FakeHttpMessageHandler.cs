using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ToDo.MvcClientApp.Test
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        public virtual HttpResponseMessage Send(HttpRequestMessage request)
        {
           var response = new HttpResponseMessage();
            response.Content = new StringContent("{}");
            return response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Send(request));
        }
    }
}
