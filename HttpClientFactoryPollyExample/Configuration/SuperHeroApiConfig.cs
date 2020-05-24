using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpClientFactoryPollyExample.Configuration
{
    public interface ISuperHeroApiConfig
    {
        string AccessToken { get; set; }
        string BaseUrl { get; set; }
    }

    public class SuperHeroApiConfig : ISuperHeroApiConfig
    {
        public string AccessToken { get; set; }
        public string BaseUrl { get; set; }
    }
}
