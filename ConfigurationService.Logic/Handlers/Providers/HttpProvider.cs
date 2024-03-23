using ConfigurationService.Logic.Handlers.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConfigurationService.Logic.Handlers.Providers
{
    public class HttpProvider : IHttpProvider
    {
        private HttpClient _httpClient;
        public HttpProvider()
        {
            _httpClient = new HttpClient();
        }

        public async Task<T> Get<T>(string request)
        {
            var resp = await _httpClient.GetStringAsync(request);
            return JsonSerializer.Deserialize<T>(resp);
        }
    }
}
