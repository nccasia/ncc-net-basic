# Entity Framework Core
Entity Framework (EF) Core is a lightweight, extensible, open source and cross-platform version of the popular Entity Framework data access technology.

EF Core can serve as an object-relational mapper (O/RM), which:
* Enables .NET developers to work with a database using .NET objects.
* Eliminates the need for most of the data-access code that typically needs to be written.

## Installing Entity Framework Core

### Prerequisites
* EF Core requires a .NET implementation that supports .NET Standard 2.0 to run. EF Core can also be referenced by other .NET Standard 2.0 libraries.
* Use EF Core to develop apps that target .NET Core. Building .NET Core apps requires the .NET Core SDK.
* Different database providers may require specific database engine versions, .NET implementations, or operating systems. Make sure an EF Core database provider is available that supports the right environment for your application.

To add EF Core to an application, install the NuGet package for the database provider you want to use.

If you're building an ASP.NET Core application, you don't need to install the in-memory and SQL Server providers. Those providers are included in current versions of ASP.NET Core, alongside the EF Core runtime.

To install or update NuGet packages, you can use the .NET Core command-line interface (CLI), the Visual Studio Package Manager Dialog, or the Visual Studio Package Manager Console.

### .NET Core CLI
* Use the following .NET Core CLI command from the operating system's command line to install or update the EF Core SQL Server provider:
  ```
  dotnet add package Microsoft.EntityFrameworkCore.SqlServer
  ```

### Visual Studio NuGet Package Manager Dialog
* From the Visual Studio menu, select Tools > NuGet Package Manager > Package Manager Console
* To install the SQL Server provider, run the following command in the Package Manager Console:
  ```
  Install-Package Microsoft.EntityFrameworkCore.SqlServer
  ```

### Get the .NET Core CLI tools
.NET Core CLI tools require the .NET Core SDK
* `dotnet ef` must be installed as a global or local tool. Most developers prefer installing `dotnet ef` as a global tool using the following command:
  ```
  dotnet tool install --global dotnet-ef
  ```
* To update the tools, use the `dotnet tool update` command.
* Install the latest `Microsoft.EntityFrameworkCore.Design` package.
  ```
  dotnet add package Microsoft.EntityFrameworkCore.Design
  ```

### Get the Package Manager Console tools
To get the Package Manager Console tools for EF Core, install the `Microsoft.EntityFrameworkCore.Tools` package. For example, from Visual Studio:
  ```
  Install-Package Microsoft.EntityFrameworkCore.Tools
  ```

## Contents

* [Getting started](./getting-started)
* [DbContext configuration and initialization](./dbcontext)
* [Create and configure model](./configure-model)
* [Manage database schemas](./manage-database-schemas)
* [Query data](./query-data)
* [Save data](./save-data)
* [Change tracking](./change-tracking)
* [Samples](./samples)


