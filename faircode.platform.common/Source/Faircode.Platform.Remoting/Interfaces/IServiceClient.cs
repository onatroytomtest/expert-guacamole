using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Faircode.Platform.Remoting.Interfaces
{
    public interface IServiceClient
    {
        public Task<HttpResponseMessage> MakePost(Uri baseUri, ServiceContext serviceContext);
        public Task<HttpResponseMessage> MakePut(Uri baseUri, ServiceContext serviceContext);
        public Task<HttpResponseMessage> MakeGet(Uri baseUri, ServiceContext serviceContext);
        public Task<HttpResponseMessage> MakePatch(Uri baseUri, ServiceContext serviceContext);
        public Task<HttpResponseMessage> MakeDelete(Uri baseUri, ServiceContext serviceContext);
    }
}
