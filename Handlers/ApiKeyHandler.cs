using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Web;

namespace MySqlWebApi.Handlers
{
    public class ApiKeyHandler : DelegatingHandler
    {
        private const string ApiKeyHeaderName = "ApiKey";
        private const string ApiKey = "9989892165135";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains(ApiKeyHeaderName))
            {
                return request.CreateResponse(HttpStatusCode.Unauthorized, "Missing API Key");
            }

            var apiKeyHeaderValue = request.Headers.GetValues(ApiKeyHeaderName).FirstOrDefault();
            if (apiKeyHeaderValue != ApiKey)
            {
                return request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid API Key");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}