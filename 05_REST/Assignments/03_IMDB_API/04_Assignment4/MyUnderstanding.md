```
For that very first , 
I belive the flow is

program.cs starts the application, its responsible for building the hostbuilder ,
that provides services, and it also ensures the application life cycle,

then we move to startup.cs , here  the 2 major things are, Configuration of services,
 that is registering services into the IServiceConfiguration DI Container,
so that the services are available wherever they are injected via constructor Dependency injection,

Note that startup.cs does not create DI Container, it just takes in registrations

When we have console app, we dont have startup class

but when we convert console to startup class, we have a startup class, 

services.AddControllers(); -> allow controller fnctionality




Program.cs
    ↓
Create Host
    ↓
Create Service Container
    ↓
Startup.ConfigureServices()
    ↓
services.AddControllers()
    ↓
Startup.Configure()
    ↓
app.UseRouting()
    ↓
app.UseEndpoints(...)
       ↓
   MapControllers()
    ↓
Controller endpoints are mapped
    ↓
Application starts listening




```

Tight Coupling example

1. Remove registtrations
