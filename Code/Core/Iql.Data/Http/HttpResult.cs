using System;
using System.IO;
using System.Threading.Tasks;

namespace Iql.Data.Http
{
    public class HttpResult : IHttpResult
    {
        public Func<Task<string>> GetResponseTextAsync { get; set; }
        public Func<Task<byte[]>> GetResponseBytesAsync { get; set; }
        public Func<Task<Stream>> GetResponseStreamAsync { get; set; }
        public bool IsOffline { get; set; } = false;
        public string ContentType { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; }

        public HttpResult(
            Func<Task<string>> responseData, 
            Func<Task<byte[]>> responseBytes,
            Func<Task<Stream>> responseStream,
            HttpStatusCode statusCode,
            string contentType,
            bool success)
        {
            GetResponseTextAsync = responseData;
            GetResponseBytesAsync = responseBytes;
            GetResponseStreamAsync = responseStream;
            ContentType = contentType;
            StatusCode = statusCode;
            Success = success;
        }

        public static HttpResult FromNonAsync(
            Func<string> content, 
            Func<byte[]> byteArray, 
            Func<Stream> stream,
            int statusCode = 200,
            bool success = true, 
            string contentType = null)
        {
            return new HttpResult(
                () => Task.FromResult(content()),
                () => Task.FromResult(byteArray()),
                () => Task.FromResult(stream()),
                success ? HttpStatusCode.Ok : 0,
                contentType,
                success);
        }

        public static HttpResult FromString(
            string content, bool success = true, string contentType = null)
        {
            return new HttpResult(
                () => Task.FromResult(content),
                () => Task.FromResult<byte[]>(null),
                () => Task.FromResult<Stream>(null),
                success ? HttpStatusCode.Ok : 0,
                contentType,
                success);
        }

        public static HttpResult EmptySuccess(int statusCode = 200)
        {
            var result = FromString("");
            result.StatusCode = (HttpStatusCode)statusCode;
            return result;
        }

        public static HttpResult Offline()
        {
            var result = FromString("");
            result.IsOffline = true;
            return result;
        }

        public static HttpResult Fail(int statusCode)
        {
            var result = FromString(null);
            result.StatusCode = (HttpStatusCode) statusCode;
            return result;
        }
    }
}