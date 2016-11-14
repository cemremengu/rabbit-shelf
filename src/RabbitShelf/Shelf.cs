namespace RabbitShelf
{
    using System;
    using EasyNetQ;

    public abstract class Shelf : IRabbit
    {
        private Shelf()
        {
        }

        protected Shelf(IAdvancedBus bus)
        {
            Bus = bus;
        }

        public IAdvancedBus Bus { get; }

        public virtual void Start()
        {
            throw new NotImplementedException();
        }

        public virtual void Stop()
        {
            throw new NotImplementedException();
        }
    }
}