AkvelonDemoAPI

Application diveded into layers. Each layer has its own services and implementation.

AkvelonDemoAPI - Representation of app. Contains All the controllers and Extension files. 
        Startup class - Class that implements all the applications pipeline and middleware components.
        ProjectController - Has all endpoints for Project entity and CRUD operations
        TaskController - Has all endpoints for Task entity and CRUD operations
        

BusinessLogicLayer - Contains Services of our Application

CommonServices - has common services of our app like LoggerService, MapperProfile etc. P.S Mapping is not included in this app. Yeah i should do that

Contracts - Has every Interface of our app

DataAccessLayer - Contaions database related stuff like Models, DTOs, Contexts, Migrations etc.

Repository - Implementation of Repository Pattern
