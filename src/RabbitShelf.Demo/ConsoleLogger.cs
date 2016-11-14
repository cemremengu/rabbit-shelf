namespace RabbitShelf.Demo
{
    using System;
    using EasyNetQ;

    public class ConsoleLogger : IEasyNetQLogger
    {
        public void DebugWrite(string format, params object[] args)
        {
            Console.WriteLine("Debug");
        }

        public void ErrorWrite(Exception exception)
        {
            Console.WriteLine("Exception");
        }

        public void ErrorWrite(string format, params object[] args)
        {
            Console.WriteLine("Error");
        }

        public void InfoWrite(string format, params object[] args)
        {
            Console.WriteLine("Info");
        }
    }
}