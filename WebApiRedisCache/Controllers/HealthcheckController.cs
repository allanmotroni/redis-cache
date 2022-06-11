using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;

namespace WebApiRedisCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthcheckController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;

        public HealthcheckController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public string GetStatus()
        {
            var key = "healthCheckStatus";
            var lastStatus = _distributedCache.GetString(key);
            
            var actualStatus = $"{DateTime.UtcNow:o}";

            _distributedCache.SetString(key, actualStatus);

            var healthCheckResult = $"Actual Status: {actualStatus} | Last Status: {lastStatus}";
            
            return healthCheckResult;
        }
    }
}
