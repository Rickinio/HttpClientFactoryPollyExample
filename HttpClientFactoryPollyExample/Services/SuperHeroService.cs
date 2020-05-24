using HttpClientFactoryPollyExample.Configuration;
using HttpClientFactoryPollyExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HttpClientFactoryPollyExample.Services
{
    public class SuperHeroService : ISuperHeroService
    {
        //https://www.stevejgordon.co.uk/sending-and-receiving-json-using-httpclient-with-system-net-http-json

        private readonly HttpClient _httpClient;
        private readonly ISuperHeroApiConfig _superHeroApiConfig;

        public SuperHeroService(HttpClient httpClient, ISuperHeroApiConfig superHeroApiConfig)
        {
            _httpClient = httpClient;
            _superHeroApiConfig = superHeroApiConfig;
        }

        public async Task<PowerStats> GetPowerStats(int id)
        {
            return await _httpClient.GetFromJsonAsync<PowerStats>(
                $"{_superHeroApiConfig.AccessToken}/{id}/powerstats"
                );
        }

        public async Task<PowerStats> GetPowerStatsWithException(int id)
        {
            return await _httpClient.GetFromJsonAsync<PowerStats>(
                $"https://localhost:44390/api/SuperHero/GetException");
        }        
    }
}
