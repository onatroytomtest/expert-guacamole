using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace Faircode.Platform.Remoting.Helpers
{
    public static class Extensions
    {

        /// <summary>
        /// Use this method to get the content of httprespone message .Throws operation cancelled exception incase of failure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        public static T GetResponseContent<T>(this HttpResponseMessage httpResponse, JsonSerializerSettings serializerSettings = null)
        {
            if (httpResponse == null)
            {
                throw new ArgumentNullException(nameof(httpResponse));
            }

            var content = httpResponse.Content?.ReadAsStringAsync().GetAwaiter().GetResult();
            if (httpResponse.IsSuccessStatusCode)
            {
                if (serializerSettings != null)
                {
                    return JsonConvert.DeserializeObject<T>(content, serializerSettings);
                }
                return JsonConvert.DeserializeObject<T>(content);
            }

            if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ArgumentException(content);
            }

            if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException(content);
            }

            if (httpResponse.StatusCode == HttpStatusCode.NotFound)
            {
                throw new InvalidDataException(content);
            }

            if (httpResponse.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new InvalidOperationException(content);
            }

            throw new OperationCanceledException($"There was error while performig the request " +
                $" status code {httpResponse.StatusCode}, content {content}");
        }
    }
}
