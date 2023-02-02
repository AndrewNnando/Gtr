# The **import**  application


## Table Of Contents
- [The __import__ application](#the-import-application)
  * [Table Of Contents](#table-of-contents)
- [Getting Started](#getting-started)
  * [Installation](#installation)
  * [Publishing __import__](#publishing-import)
  * [External Dependencies](#external-dependencies)
- [Project Set-up](#project-set-up)
    * [Models](#models)
    * [Interfaces](#interfaces)
    * [Infrastructure](#infrastructure)
    * [Loaders](#loaders)
    * [Commands](#commands)
- [Test Project](#test-project)
- [Reflections](#reflections)

# Getting Started

The following technologies were used to develop the console application:

* Visual Studio 2022
* dotnet CLI
* The System.CommandLine library
* Microsoft's built-in dependency injection libraries
* Microsoft's built-in logging services.

In the time-frame that we have, we will not address cross-cutting concerns such as authentication and security.

## Installation

Download the source code from [GitHub](https://github.com/AndrewNnando/Gartner) and compile.


## Publishing __import__

From within Visual Studio:

* Right-click on the **import** project and select Publish.
* Select the path to where you wish to publish.
* Follow the instructions

From the command prompt, navigate to the folder containing the .csproj, then run the following commands:

>dotnet publish -o _foldername_

>cd _foldername_

>import capterra _file_


Note, _file_ should include the full or relative folder path if it is not in the current folder.

## External Dependencies 

### System.CommandLine

The **System.CommandLine** library is used by the dotnet CLI and is part of the [dotnet sdk](https://github.com/dotnet/sdk). 

>The System.CommandLine library provides functionality that is commonly needed by command-line apps, such as parsing the command-line input and displaying help text.

It will allow us to concentrate on writing and testing the application code, rather than developing infrastructure code.

### Installing the **System.CommandLine** package
The library has been in beta despite being available for a few years. Use the dotnet CLI to install it into the project.

>dotnet add package System.CommandLine -f net6.0 --prerelease

### Dependency Injection

We will use dependency injection to loosely couple the application from  repository and logging services. 

The dependency injection library can be installed within Visual Studio, using the NuGet package manager, or by using the dotnet CLI:

>dotnet add package Microsoft.Extensions.DependencyInjection -f net6.0

### Logging

Logs will be created using the .NET logging API. 

>dotnet add package Microsoft.Extensions.Logging.Abstractions -f net6.0
>dotnet add package Microsoft.Extensions.Logging -f net6.0

### JSON
Microsoft's System.Text.Json namespace has been used in preference to the NewtonSoft libraries. No particular reason, it is simple and does what is needed.

### YAML
The YamlDotNet library has been used to deserialize the capterra file. This is a supported library downloaded from NuGet. However, this NuGet package is an older version. GitHub has the most recent changes.

# Project Set-up

The application has currently been set-up as a single application. At this stage of the project, it was decided not to separate the loading logic into a separate project. In hindsight, this might have better illustrated the concept of clean code and the separation of responsibilities.

The entry point to the application is the Program class. This class defines the services that will be used by the application, adding them to the built-in dependency injection container. The root command is then called to run the application. 

Common functionality is grouped by folders.

## Models

This folder contains both the internal representations of the data (Inventory and InventoryItem classes), and the data transfer object classes used to import external data into the application (ProductList and Product classes). Had the loading logic been separated out into separate projects, then the Inventory and InventoryItem classes would have been in the separate library.

## Interfaces

This folder contains the definitions of the internal data representations.

## Infrastructure

Repository proxies used to connect to external repositories are defined here. A dummy repository has been created for this exercise.

## Loaders

The loading logic for capterra and software advice are defined here. The steps involved in loading the two files are the same except for the type of file. These steps are defined in the base class BaseLoader, and the concrete classes implement the actual loading code that is appropriate to their file type.

## Commands

The System.CommandLine functionality is accessed via the classes here. Both capterra and software advice require a single argument specifying the file  location. As such their command definitions are the same except in the code handling their invocations. Both command classes inherit from the FileInfoArgumentCommand class.



# Test Project

The test project has been organised by the project that is being tested, rather than on based on the type of test (unit, integration, functional etc). The test project is not complete. A TDD approach was taken in the development of the loader classes, and it is of these classes that enough tests were written to implement the solution.

* xunit has been the standard framework extensively used and recommended by many Microsoft-related articles.

* Moq is the mocking framework that is well understood and supported in the community.
