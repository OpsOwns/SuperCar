## sample implemention on CQRS/ES and DDD
 ### implemented event surcing with event store using cosmos db for events write side and ms sql server to read side
 ## The available services are : Cars, CarsView and EventProcessor
 * Cars service is resposible for saving/update cars
 * CarsView retrive data about cars
 * EventProcessor is resposible for push the events to the queue

# Tools
* CosmosDB
* Sql Server
* Entity framework
* Mediatr
* Rabbit MQ

![sourcing](https://user-images.githubusercontent.com/25839864/115249802-41868c80-a129-11eb-8b2e-f56e21075910.JPG)
