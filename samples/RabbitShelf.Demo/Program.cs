namespace RabbitShelf.Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new RabbitShelf<MyService, ConsoleLogger>() {Description = "There is a rabbit on top shelf!", DisplayName = "Rabbit on top-shelf demo", ServiceName = "Demo"}.Run();
        }
    }
}