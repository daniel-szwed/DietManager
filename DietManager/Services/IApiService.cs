using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DietManager.Services
{
    public interface IApiService : IDisposable
    {
        IApiService SetBaseAddress(string baseAddress);
        IApiService AddHeader(string header, string value);
        IApiService AddHeader(HttpRequestHeader header, string value);
        IApiService SetMethod(HttpMethod method);
        IApiService SetStringContent(string content, Encoding encoding, string mediaType);
        IApiService SetFormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> content);
        IApiService SetTimeout(int miliseconds);
        Task<HttpResponseMessage> SendRequestAsync(string endpoint);
        IEnumerable<Cookie> GetCookies();
        IApiService SetCookie(Cookie cookie);
    }
}
