using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DietManager.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _client;
        private HttpRequestMessage _requestMessage;
        private CookieContainer _cookies;
        private HttpClientHandler _handler;

        public ApiService()
        {
            _handler = new HttpClientHandler();
            _client = new HttpClient(_handler);
            _cookies = new CookieContainer();
            _handler.CookieContainer = _cookies;
            _requestMessage = new HttpRequestMessage();
        }

        public IApiService SetBaseAddress(string baseAddress)
        {
            _client.BaseAddress = new Uri(baseAddress);
            return this;
        }

        public IApiService AddHeader(HttpRequestHeader header, string value)
        {
            _requestMessage.Headers.Add(header.ToString(), value);
            return this;
        }

        public IApiService AddHeader(string header, string value)
        {
            _requestMessage.Headers.Add(header, value);
            return this;
        }

        public IApiService SetMethod(HttpMethod method)
        {
            _requestMessage.Method = method;
            return this;
        }

        public IApiService SetStringContent(string content, Encoding encoding, string mediaType)
        {
            _requestMessage.Content = new StringContent(content, encoding, mediaType);
            return this;
        }

        public IApiService SetFormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> content)
        {
            _requestMessage.Content = new FormUrlEncodedContent(content);
            return this;
        }

        public IApiService SetTimeout(int miliseconds)
        {
            _client.Timeout = TimeSpan.FromMilliseconds(miliseconds);
            return this;
        }

        public Task<HttpResponseMessage> SendRequestAsync(string endpoint)
        {
            _requestMessage.RequestUri = new Uri(_client.BaseAddress + endpoint);
            return _client.SendAsync(_requestMessage);
        }

        public IApiService SetCookie(Cookie cookie)
        {
            _cookies.Add(cookie);
            return this;
        }

        public IEnumerable<Cookie> GetCookies()
        {
            return _cookies.GetCookies(_client.BaseAddress).Cast<Cookie>();
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
