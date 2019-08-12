## Users REST API Sample

This project shows examples of the following concepts:

* Entity Framework Core 2.2 InMemory database for an ASP .NET Core Web API
* Value Objects with Entity Framework
* Query objects to encapsulate predicate and selection expressions when performing a query to a Repository
* Use of Specification pattern inconjunction with Queries and validation of Entities
* Validation of Value Objects and collection of validation errors by the Visitor pattern
* Maybe Functor on creation and retrieval
* Result objects with fluent extensions

The project incorporates ideas from:
* [Entity types with constructors](https://docs.microsoft.com/en-us/ef/core/modeling/constructors)
* [Implement value objects](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/implement-value-objects)
* [Specification Pattern](https://deviq.com/specification-pattern/)
* [Visit your domain objects to keep ’em legit!](https://blog.pragmatists.com/visit-your-domain-objects-to-keep-em-legit-6b5d43e98ef0)
* [Design validations in the domain model layer](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/domain-model-layer-validations)
* [The Maybe functor](https://blog.ploeh.dk/2018/03/26/the-maybe-functor/)
* [Functional C#: Handling failures, input errors](https://enterprisecraftsmanship.com/2015/03/20/functional-c-handling-failures-input-errors/)