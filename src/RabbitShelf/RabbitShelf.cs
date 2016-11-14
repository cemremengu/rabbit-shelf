namespace RabbitShelf
{
    using Autofac;
    using EasyNetQ;
    using Topshelf;
    using IContainer = Autofac.IContainer;

    public class RabbitShelf<TService, TLogger> where TService : Shelf where TLogger : IEasyNetQLogger, new()
    {
        private IContainer _container;
        private readonly ContainerBuilder _builder;

        public RabbitShelf(string host = "localhost", ushort port = 5672)
        {
            _builder = new ContainerBuilder();

            _builder.RegisterType<TService>().As<IRabbit>();
            _builder.RegisterInstance(RabbitBusBuilder.CreateMessageBus<TLogger>(host, port)).As<IAdvancedBus>();
        }

        public string Description { get; set; }

        public string DisplayName { get; set; }

        public string ServiceName { get; set; }

        public void Run()
        {
            _container = _builder.Build();

            HostFactory.Run(x =>
            {
                x.Service<IRabbit>(s =>
                {
                    s.ConstructUsing(name => _container.Resolve<IRabbit>());

                    s.WhenStarted(tc => tc.Start());

                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsLocalSystem();

                x.SetDescription(Description);
                x.SetDisplayName(DisplayName);
                x.SetServiceName(ServiceName);

                x.StartAutomatically(); // Start the service automatically

                x.EnableServiceRecovery(r =>
                {
                    //should this be true for crashed or non-zero exits
                    r.OnCrashOnly();

                    r.RestartService(1);
                });
            });
        }
    }
}