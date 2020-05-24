using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpClientFactoryPollyExample.Models;
using HttpClientFactoryPollyExample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HttpClientFactoryPollyExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuperHeroController : ControllerBase
    {
        private readonly ILogger<SuperHeroController> _logger;
        private readonly ISuperHeroService _superHeroService;

        public SuperHeroController(ILogger<SuperHeroController> logger, ISuperHeroService superHeroService)
        {
            _logger = logger;
            _superHeroService = superHeroService;
        }

        [HttpGet("GetPowerStats/{id}")]
        public async Task<ActionResult> GetPowerStats(int id)
        {
            var result = await _superHeroService.GetPowerStats(id);

            return Ok(result);
        }

        [HttpGet("GetPowerStatsWithException/{id}")]
        public async Task<ActionResult> GetPowerStatsWithException(int id)
        {
            var result = await _superHeroService.GetPowerStatsWithException(id);

            return Ok(result);
        }

        [HttpGet("GetException")]
        public async Task<ActionResult> GetException(int id)
        {
            throw new Exception("Exception Message");

            return Ok();
        }
    }
}
