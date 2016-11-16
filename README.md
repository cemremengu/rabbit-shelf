## rabbit-shelf [![Build status](https://ci.appveyor.com/api/projects/status/wg46aoqycv3h7qvi?svg=true)](https://ci.appveyor.com/project/cemremengu/rabbit-shelf) [![NuGet version](https://img.shields.io/nuget/v/RabbitShelf.svg?style=flat)](https://www.nuget.org/packages/RabbitShelf)
A tiny service framework with [EasyNetQ](http://easynetq.com/) & [Topshelf](http://topshelf-project.com/)

## Usage
- Create a console project
- Define your service by extending `AdvancedShelf` (`IAdvancedBus`) or `Shelf` (`IBus`)
- Override `Start` and `Stop` methods in your service.
- Define your logger by implementing `IEasyNetQLogger`
- Type:
```cs
public class Program
{
	public static void Main(string[] args)
	{
		 new RabbitShelf<MyService, MyLogger>()
		 {
			Description = "There is a rabbit on top shelf!", 
			DisplayName = "Rabbit on top-shelf demo", 
			ServiceName = "Demo"
		 }.Run();
	}
}
```
- Have fun!

See `RabbitShelf.Demo` for a more concrete example.

## Can I help to improve it and/or fix bugs? ##

Absolutely! Please feel free to raise issues, fork the source code, send pull requests, etc.

## Credits

This package is partly based on [this](https://github.com/EasyNetQ/EasyNetQ/wiki/Wiring-up-EasyNetQ-with-TopShelf-and-Windsor) example.

