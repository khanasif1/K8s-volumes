//https://www.mrjamiebowman.com/software-development/dotnet/kubernetes-configmaps-with-net-core/
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace K8s.Volumes
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigReaderController : ControllerBase
    {
        private readonly ILogger<ConfigReaderController> _logger;
        private readonly IOptionsMonitor<APIConfiguration> _valuesMonitoredConfiguration;
        private readonly APIConfiguration _valuesConfiguration;
        private readonly APISecret _secrets;
        public ConfigReaderController(ILogger<ConfigReaderController> logger, IOptionsMonitor<APIConfiguration> apiMonitoredConfiguration, APIConfiguration apiConfiguration, APISecret secrets)
        {
            _logger = logger;
            _valuesMonitoredConfiguration = apiMonitoredConfiguration;
            _valuesConfiguration = apiConfiguration;
            _secrets = secrets;
        }

        /// <summary>
        /// Standard method for getting configuration values. This does not update if you edit the appsettings.json.
        /// </summary>
        /// <returns></returns>        
        [HttpGet("getconfigmap")]
        public string Get()
        {
            if (_valuesConfiguration?.Message == null)
                return "Configuration is NULL";

            return _valuesConfiguration?.Message;
        }
        /// <summary>
        /// Get secrets from Kubernetes secrets
        /// </summary>
        /// <returns></returns>
        [HttpGet("getsecrets")]
        public string GetSecrets()
        {
            //string secretValue = Environment.GetEnvironmentVariable("App-Secret");
            if (!string.IsNullOrEmpty(_secrets.Secret))
            {
                return "Secret Value: " + _secrets.Secret;
            }
            else
            {
                return "Problem in reading secrets";
            }
        }
    }
}
