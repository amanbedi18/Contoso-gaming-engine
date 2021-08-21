# Contoso-gaming-engine
Gaming engine for Contoso Games

[![CI Build](https://github.com/amanbedi18/Contoso-gaming-engine/actions/workflows/ContosoGamingEngineAPI-build.yml/badge.svg)](https://github.com/amanbedi18/Contoso-gaming-engine/actions/workflows/ContosoGamingEngineAPI-build.yml)

[![CD-dev](https://github.com/amanbedi18/Contoso-gaming-engine/actions/workflows/ContosoGamingEngineAPI-deploy.yml/badge.svg)](https://github.com/amanbedi18/Contoso-gaming-engine/actions/workflows/ContosoGamingEngineAPI-deploy.yml)


## Problem Statement

The goal is to build an API to locate the actors in the game. All actors are tagged to the Landmarks and the purpose of the API is to provide routes between landmarks to suggest routes to the actors.  In order to achieve this, the game engine should be able to calculate:

- The distance along certain routes via landmarks
- The number of different routes between landmarks


## Non-Functional Requirements

- Use C#, Java or Python
- Deploy & host on AWS
- Integrate the API with Swagger UI for testing
- Document assumptions made
- Unit test
- Approach for SCM & DevOps (CI-CD)
- Easy to extend design
- In memory data-store but should be extendable to Db later


## Solution Implementation

The following tech stack is used to develop the API:

| Component                          | Technology                                                   |
| ---------------------------------- | ------------------------------------------------------------ |
| API                                | Microsoft ASP.NET Core 5.0                                   |
| Code Analysis                      | Microsoft NetAnalyzers, StyleCop Analyzers                   |
| Swagger                            | Swashbuckle AspNetCore                                       |
| Middleware for error handling      | Hellang Middleware for ProblemDetails                        |
| Exception Handling                 | AspNet Mvc Filter                                            |
| Dependency Injection               | Built in dependency injection in ASP.NET Core, Microsoft Dependency Injection extension in Unit tests |
| Unit testing                       | Xunit, Microsoft .NET Test SDK, Moq, Coverlet code coverage collector |
| Logging                            | Azure App Insights                                           |
| SCM (source control management)    | GitHub                                                       |
| DevOps (CI-CD)                     | GitHub Actions                                               |
| Hosting - AWS (primary)            | AWS (EC2 & Elastic Beanstalk)                                |
| Hosting - Microsoft Azure (backup) | Azure App Service                                            |


## Deployed URL's
Below are the URL's for deployed API:
- [AWS](http://contosogaminapi-dev.ap-south-1.elasticbeanstalk.com/swagger/index.html)
- [Azure](https://contosogamingengineapi.azurewebsites.net/swagger/index.html)



## How to use
The Contoso Gaming Engine API has the following 3 endpoints:

![API Endpoints](./assets/apiendpoints.png)

1. GET ​
```
/api​/v{version}​/locateplayers​/{source}​/{destination}
```

- Gets all possible routes with their weights from source to destination

2. GET 
```
​/api​/v{version}​/locateplayers​/{source}​/{destination}​/{hops}
```

- Gets all possible routes with their weights from source to destination for given number of maximum hops

3. POST
```
/api​/v{version}​/locateplayers​/findroutes
{
   "source": "A", #source should not be same as destination
   "destination": "E"
   "landmarks": {  #optional parameter
        "B",
        "C",
        "D"
    },
    "requiredHops": 3 #optional parameter, defaults to zero if not provided
}
```

- Gets all possible routes with their weights from source to destination for given landmarks

## Sample Runs


## Design
Below section details the high level & low level design of the solution

### High Level Design

The high level design consists of the following components:

![High Level Design](./assets/highleveldesign.png)

- The user makes Http GET or POST request to the REST API
- The API performs validations, calls internal services & serves the response to the user's request
- In memory graph is leveraged as the data store
- Response is sent back to the user in JSON format


### Low Level Design

The low level design consists of the following components:

![High Level Design](./assets/lowleveldesign.png)

1. The user makes Http GET or POST request to the REST API which goes to RouteController
2. The controller runs basic validations on the input parameters like:

- source, destination & hops are required in get requests
- source & destination cannot be the same & landmarks cannot be empty or contain the source or destination

3. The controller calls PlayersLocatorService with appropriate parameters
4. PlayersLocatorService calls InMemoryGraph, providing values for edges & vertices alongwith weights
5. InMemoryGraph validates the incoming values for edges & vertices of the graph
6. InMemoryGraph checks whether the graph already exists in the memory, if not, it creates a new in memory graph with provided values of edges, vertices & weights.
7. InMemoryGraph returns the graph object to PlayersLocatorService
8. PlayersLocatorService runs business validations like:

- Hops should not be negative
- Source, destination & landmarks should exist in the vertices of the in memory graph

9. Validation results are returned to PlayersLocatorService
10. PlayersLocatorService call GraphService to find the requested routes & weights based on input parameters
11. GraphService returns routes with weights as result
12. Resulting list of routes with weights is mapped to API response model
13. The response model is returned to the RouteController
14. The RouteController validates the response model
15. If the response model is valid, it is returned as JSON wrapped in API Action Result, otherwise ProblemDetails JSON is returned with appropriate details for any error, exception, invalid inputs or no route found for given valid inputs





## Unit Testing

## Exception Handling

## Extensibility

## SCM

## DevOps

## Assumptions

- There will always