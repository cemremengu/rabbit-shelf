namespace RabbitShelf.Demo
{
    using System;
    using EasyNetQ;

    public class MyService : Shelf
    {
        private readonly IAdvancedBus _bus;

        public override void Start()
        {
            Console.WriteLine("Hello");
        }

        public override void Stop()
        {
            Console.WriteLine("Bye");
        }

        public MyService(IAdvancedBus bus) : base(bus)
        {
            _bus = bus;
        }
    }
}