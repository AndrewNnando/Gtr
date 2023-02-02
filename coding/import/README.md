# The **import** CLI application

We propose to develop the console application using the following tools and libraries:

* Visual Studio 2022
* dotnet CLI
* The System.CommandLine library
* Microsoft's built-in dependency injection libraries
* Microsoft's built-in logging services.

In the time-frame that we have, we will not address cross-cutting concerns such as authentication and security.

## System.CommandLine

The **System.CommandLine** library. It is used by the dotnet CLI and is part of the [dotnet sdk](https://github.com/dotnet/sdk). 

>The System.CommandLine library provides functionality that is commonly needed by command-line apps, such as parsing the command-line input and displaying help text.

It will allow us to concentrate on writing and testing the application code, rather than developing infrastructure code.

### Installing the **System.CommandLine** package
The library has been in beta despite being available for a few years. Use the dotnet CLI to install it into the project.

>dotnet add package System.CommandLine -f net6.0 --prerelease

## Dependency Injection

We will use dependency injection to loosely couple the application from  repository and logging services. 

The dependency injection library can be installed within Visual Studio, using the NuGet package manager, or by using the dotnet CLI:

>dotnet add package Microsoft.Extensions.DependencyInjection -f net6.0

## Logging

Logs will be created using the .NET logging API. 

>dotnet add package Microsoft.Extensions.Logging.Abstractions -f net6.0