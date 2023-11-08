# FamilyRegistration
Calculates Family Score for Buying Own Home 

https://app.diagrams.net/#G12mZhdZ8vdN39F70kvkzcXdgqlzrv0XNU


# Overview of design and patterns

### Patterns

* Chain of responsability
* Middleware Pipeline

### SOLID Features

* Single Responsability Principle (Middlewares)
* Dependency Injection (Dependency Inversion principle)


### Functional Programming

* DTO Adapters
* Minimal API (ASP.NET Core Web Api)

### OOP Features

* Class and Interface inheritance
* Constructor parameters
* Encapsulation (Properties)
* Methods
* Data Transfer Objects


## What problem it solves ?

We have a list of records that needs to be processed in order to calculate a score based in a series of criterias that may change through time.
Instead of implementing the calculations in a big transactional script, which is hard to maintain, we could break it down into smaller components that performs individually into a sequencial steps.

## how it works ?

I choose to use a pattern based on Chain of Responsability.

We create a specific calculator for each criteria