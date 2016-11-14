namespace RabbitShelf
{
    using System.Collections.Generic;
    using EasyNetQ;

    internal class RabbitBusBuilder
    {
        public static IAdvancedBus CreateMessageBus<TLogger>(string host, ushort port) where TLogger : IEasyNetQLogger, new()
        {
            var hostConfiguration = new HostConfiguration {Host = host, Port = port};

            var connection = new ConnectionConfiguration
            {
                Hosts = new List<HostConfiguration> {hostConfiguration}
            };

            connection.Validate();

            return RabbitHutch.CreateBus($"host={host}:{port}",
                service => service.Register<IEasyNetQLogger>(_ => new TLogger())).Advanced;
        }
    }
}