using App.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;

namespace TechnicalTest.InterfaceAdapter.Adapters
{
    public class HttpAdapter : IHttp
    {
        private string _url;
        private bool _withBaseUrl;
        private HttpClient _httpClient;

        public HttpAdapter(string url = "", TimeSpan? timeout = null)
        {
            _withBaseUrl = !string.IsNullOrWhiteSpace(url);

            if (_withBaseUrl)
            {
                _url = url.TrimEnd('/');
            }

            _httpClient = new HttpClient
            {
                Timeout = timeout ?? TimeSpan.FromSeconds(30)
            };
        }

        public async Task<string> GetAsync(string endPoint, HttpContentType httpMethod = HttpContentType.Json, string jwt = "")
        {
            var requestUrl = BuildUrl(endPoint);

            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUrl))
            {
                request.Headers.Accept.Clear();
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(TypeContent(httpMethod)));

                if (!string.IsNullOrWhiteSpace(jwt))
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadAsStringAsync();
            }
        }

        private string TypeContent(HttpContentType httpMethod)
        {
            switch (httpMethod)
            {
                case HttpContentType.Json:
                    return "application/json";
                case HttpContentType.Xml:
                    return "application/xml";
                default:
                    return "application/json";
            }
        }

        private string BuildUrl(string endPoint)
        {
            if (!_withBaseUrl)
            {
                return endPoint;
            }

            return _url + "/" + endPoint.TrimStart('/');
        }
    }
}