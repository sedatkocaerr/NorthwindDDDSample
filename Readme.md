## Technologies implemented:

- ASP.NET Core 3.1 (with .NET Core 3.1)
- ASP.NET Web API 
- Entity Framework Core 3.1
- AutoMapper
- FluentValidator
- MediatR
- EventStore
- ElasticSearch


## Architecture:

- Event Sourcing
- Unit of Work
- Repository
- Worker Service
- Domain Driven Design (Layers and Domain Model Pattern)
- Domain Events
- Domain Notification
- Domain Validations
- CQRS 

Event Store Example Photos

![Alt Text](https://i.hizliresim.com/XBRJXA.png)




![Alt Text](https://i.hizliresim.com/hXK4T8.png)

## Using Docker Compose
You can run the required infrastructure components by issuing a simple command:
```
$ docker-compose up
```
form your terminal command line, whilst being inside the repository rot directory.

It might be a good idea to run the services in detached mode, so you don't accidentally stop them. To do this, execute:
```
$ docker-compose up -d
```
To stop all services, from the repository root execute this command:
```
$ docker-compose stop
```
Hence that this command does not remove containers so you can run them again using docker-compose up or docker-compose start and your data will be retained from the previous section.

If you want to clean up all data, use
```
$ docker-compose down
```
This command stops all services and removes containers. The images will still be present locally so when you do docker-compose up - containers will be created almost instantly and everything will start with clean volumes.