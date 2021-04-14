using Microsoft.Extensions.Configuration;
using System;

namespace SuperCar.Shared.EventStore.Database
{
    public class CosmosConfiguration
    {
        public string AccountEndpoint { get; set; }
        public string AccountKey { get; set; }
        public string DatabaseId { get; set; }
        public string ContainerId { get; set; }

        internal CosmosConfiguration(IConfiguration configuration, string section)
        {
            if (configuration is null)
                throw new ArgumentException($"{nameof(configuration)} can't be null");
            if (string.IsNullOrEmpty(section))
                throw new ArgumentException($"{nameof(section)} can't be null or empty");
            configuration.GetSection(section).Bind(this);
        }
    }
}
