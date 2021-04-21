## :boom::boom::boom: sample implemention in CQRS/ES and DDD :star::star::star:

 ### Implementation of event sourcing with event store using cosmos db for events write side and ms sql server for read. Working with Domain Driven Desgin, based on onion architecture.
 ## The available services are : Cars, CarsView and EventProcessor
 * Cars service is responsible for saving/update cars
 * CarsView retrive data about cars
 * EventProcessor is responsible for push the events to the queue

# Tools
* .NET 5
* CosmosDB
* Sql Server
* Entity framework
* Mediatr
* Rabbit MQ
* Automapper

![sourcing](https://user-images.githubusercontent.com/25839864/115249802-41868c80-a129-11eb-8b2e-f56e21075910.JPG)
