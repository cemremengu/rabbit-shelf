namespace RabbitShelf
{
    using System;
    using EasyNetQ;

    public abstract class Shelf : IRabbit
    {
        private Shelf()
        {
        }

        protected Shelf(IBus bus)
        {
            Bus = bus;
        }

        public IBus Bus { get; }

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