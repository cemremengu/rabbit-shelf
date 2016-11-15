# rabbit-shelf 
A tiny service framework with RabbitMQ and Topshelf

[![Build status](https://ci.appveyor.com/api/projects/status/wg46aoqycv3h7qvi?svg=true)](https://ci.appveyor.com/project/cemremengu/rabbit-shelf)
[![NuGet version](https://img.shields.io/nuget/v/RabbitShelf.svg?style=flat)](https://www.nuget.org/packages/RabbitShelf)


# Usage
- Create a console project
- Define your service by extending `Shelf`
- Define your logger by implementing `IEasyNetQLogger`
- Type:
```cs
 new RabbitShelf<MyService, MyLogger>()
 {
    Description = "There is a rabbit on top shelf!", 
    DisplayName = "Rabbit on top-shelf demo", 
    ServiceName = "Demo"
 }.Run();
```
- Go!

## Can I help to improve it and/or fix bugs? ##

Absolutely! Please feel free to raise issues, fork the source code, send pull requests, etc.

# Credits

A big thank you goes to the maintainers of:

- EasyNetQ (https://github.com/EasyNetQ/EasyNetQ)
- Topshelf (https://github.com/Topshelf/Topshelf)

This code is partly based on the example at:

https://github.com/EasyNetQ/EasyNetQ/wiki/Wiring-up-EasyNetQ-with-TopShelf-and-Windsor