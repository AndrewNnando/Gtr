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
>dotnet add package Microsoft.Extensions.Logging -f net6.0

## JSON
Microsoft's System.Text.Json namespace has been used in preference to the NewtonSoft libraries. No particular reason, it is simple and does what is needed.

## YAML
The YamlDotNet library has been used to deserialize the capterra file. This is a supported library downloaded from NuGet. However, this NuGet package is an older version. GitHub has the most recent changes.

# Installation
Download the source code from [GitHub](https://github.com/AndrewNnando/Gartner) and compile.

## Publish

From within Visual Studio:

* Right-click on the **import** project and select Publish.
* Select the path to where you wish to publish.
* Follow the instructions

From the command prompt:

>dotnet publish -o _foldername_

>cd _foldername_

>import capterra _file_


Note, _file_ should include the full or relative folder path if it is not in the current folder.
