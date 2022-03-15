using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Faircode.Platform.Remoting.Interfaces;
using Newtonsoft.Json;

namespace Faircode.Platform.Remoting
{
    public sealed class ServiceClient : IServiceClient
    {
        private readonly ClientSettings _clientSettings;
        private string Name => GetType().Name;
        public ServiceClient(ClientSettings clientSettings)
        {
            _clientSettings = clientSettings ?? throw new ArgumentNullException(nameof(clientSettings));
            LogSettings();

        }
        public Task<HttpResponseMessage> MakeGet(Uri baseUri, ServiceContext serviceContext)
        {

            return MakeRestCall(baseUri, serviceContext, HttpMethod.Get);
        }

        public Task<HttpResponseMessage> MakePost(Uri baseUri, ServiceContext serviceContext)
        {
            return MakeRestCall(baseUri, serviceContext, HttpMethod.Post);
        }

        public Task<HttpResponseMessage> MakePut(Uri baseUri, ServiceContext serviceContext)
        {
            return MakeRestCall(baseUri, serviceContext, HttpMethod.Put);
        }

        public Task<HttpResponseMessage> MakePatch(Uri baseUri, ServiceContext serviceContext)
        {
            return MakeRestCall(baseUri, serviceContext, HttpMethod.Patch);
        }

        public Task<HttpResponseMessage> MakeDelete(Uri baseUri, ServiceContext serviceContext)
        {
            return MakeRestCall(baseUri, serviceContext, HttpMethod.Delete);
        }

        private void LogSettings()
        {
        }

        private async Task<HttpResponseMessage> MakeRestCall(Uri baseUrl, ServiceContext context, HttpMethod method)
        {
            try
            {
                using (HttpClient client = new HttpClient { BaseAddress = baseUrl })
                {
                    client.DefaultRequestHeaders.Clear();
                    HttpRequestMessage httpRequest = new HttpRequestMessage();
                    foreach (var header in context.Header)
                    {
                        httpRequest.Headers.Add(header.Key, header.Value);
                    }

                    if (context.Body != null)
                    {
                        var json = JsonConvert.SerializeObject(context.Body);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        httpRequest.Content = content;
                    }
                    httpRequest.Method = method;
                    httpRequest.RequestUri = new Uri(context.Route, UriKind.Relative);
                    return await client.SendAsync(httpRequest)
                        .ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
