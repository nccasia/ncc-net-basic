# Simple HelloWorld App

.NET CLI documents: https://docs.microsoft.com/en-us/dotnet/core/tools/

### Creates a new Hello World console application in the current folder:

```
mkdir HelloWorld
cd HelloWorld
dotnet new console
```

The `dotnet new console` command creates two files: Program.cs and HelloWorld.csproj:

```
using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
```

```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net5.0</TargetFramework>
  </PropertyGroup>

</Project>
```

### Before building or running your applications, you need to restore your packages.

```
dotnet restore
```

### Build the application:

```
dotnet build
```

### Run the application:

```
dotnet run
```