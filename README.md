# rabbit-on-topshelf [![Build status](https://ci.appveyor.com/api/projects/status/usfcl6eegl7qj50l?svg=true)](https://ci.appveyor.com/project/cemremengu/rabbit-on-topshelf)
A tiny service framework with RabbitMQ and Topshelf

# Usage
- Create a console project
- Define your service by extending `TopshelfServiceBase`
- Define your logger by implementing `IEasyNetQLogger`
- Type:
```cs
 new RabbitOnTopshelf<MyService, MyLogger>()
 {
    Description = "There is a rabbit on top shelf!", 
    DisplayName = "Rabbit on top-shelf demo", 
    ServiceName = "Demo"
 }.Run();
```
- Go!

# Disclaimer

A big thank you goes to the maintainers of:

- EasyNetQ (https://github.com/EasyNetQ/EasyNetQ)
- Topshelf (https://github.com/Topshelf/Topshelf)

This code is partly based on the example at:

https://github.com/EasyNetQ/EasyNetQ/wiki/Wiring-up-EasyNetQ-with-TopShelf-and-Windsor