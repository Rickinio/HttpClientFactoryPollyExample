using HttpClientFactoryPollyExample.Models;
using System.Threading.Tasks;

namespace HttpClientFactoryPollyExample.Services
{
    public interface ISuperHeroService
    {
        Task<PowerStats> GetPowerStats(int id);
        Task<PowerStats> GetPowerStatsWithException(int id);
    }
}