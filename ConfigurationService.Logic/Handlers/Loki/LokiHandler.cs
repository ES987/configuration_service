using ConfigsLoaders.Interfaces;
using ConfigurationService.Entities.Dto;
using ConfigurationService.Logic.Handlers.Loki.Entites;
using ConfigurationService.Logic.Handlers.Loki.Interfaces;
using ConfigurationService.Logic.Handlers.Providers.Interfaces;
using System.Collections.Generic;

namespace ConfigurationService.Logic.Handlers.Loki
{
    /// <summary>
    /// для получения  логов из loki
    /// </summary>
    public class LokiHandler : ILokiHandler
    {

        private string _lokiUrl = "";
        private IHttpProvider _httpProvider;
        public LokiHandler(IHttpProvider httpProvider, IConfigsLoader configsLoader)
        {
            _httpProvider = httpProvider;
            _lokiUrl = configsLoader.GetLokiUrl();
        }

        
        public async Task<List<ProviderLogDto>> GetProviderLogs( Guid providerId,long start)
        {
            List<ProviderLogDto> result = new List<ProviderLogDto>();

            string request = _lokiUrl+"/loki/api/v1/query_range?query={data_provider = \"logs\"} |="+$" `{providerId}` | logfmt --strict";
            if (start != 0) {
                request += $"&start={start }";
            }
            var response = await _httpProvider.Get<GetLogsResponse>(request);
            if (response.status == "success")
            {
                foreach (var t in response.data.result)
                {
                    foreach (var val in t.values)
                    {
                        ProviderLogDto log = new ProviderLogDto()
                        {
                            LogLevel = t.stream.LogLevel,
                            Message = t.stream.Message,
                            NanoSeconds = long.Parse(val[0]),
                            DateTime = t.stream.date
                        };
                        result.Add(log);
                    }
                }
            }
            return result;


        }
    }
}
