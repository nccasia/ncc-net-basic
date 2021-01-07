# .NET Core

Source: https://docs.microsoft.com/en-us/dotnet/core/introduction

.NET Core is a free, open-source development platform for building many kinds of apps, such as:
- Web apps, web APIs, and microservices
- Serverless functions in the cloud
- Cloud native apps
- Mobile apps
- Desktop apps
  - Windows WPF
  - Windows Forms
  - Universal Windows Platform (UWP)
- Games
- Internet of Things (IoT)
- Machine learning
- Console apps
- Windows services

## Key Features

### Cross platform
You can create .NET apps for many operating systems, including:
- Windows
- macOS
- Linux
- Android
- iOS
- tvOS
- watchOS

Supported processor architectures include:
- x64
- x86
- ARM32
- ARM64

### Open Source
.NET is open source, using MIT and Apache 2 licenses. .NET is a project of the .NET Foundation.

## Tools

### Languages
C#, Visual Basic, F#

### IDEs
[Visual Studio](https://visualstudio.microsoft.com/vs/), [Visual Studio Code](https://code.visualstudio.com/). [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/)

### SDK and runtimes
[.NET SDK](https://docs.microsoft.com/en-us/dotnet/core/sdk)

### Project system and MSBuild
A .NET app is built from source code by using MSBuild. A project file (.csproj, .fsproj, or .vbproj) specifies targets and associated tasks that are responsible for compiling, packing, and publishing code.

```
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net5.0</TargetFramework>
  </PropertyGroup>
</Project>
```

## Advantages
- Cross-Platform: Create and run .NET Core apps on many operating systems.
- Open Source: Highly optimized framework because of bugs fixes, features and contribution from the large community.
- Mature framework and widely used programming languages: .NET Core is the successor of .NET Framework, along with C#, which has been around for around 20 years.
- Supports a wide range of application types: Desktop, Web, Cloud, Mobile, Gaming, IoT, AI ...
- Increased security: Offers a wide range of easy to use mechanisms for authentication, authorization, data protection, and attack prevention.
- Increased performance significantly: ASP.NET Core with Kestrel web server take advantage of asynchronous programming models, be much more lightweight, and fast.
- Provides modularity, lightweight, and flexibility.
- Is cost effective: support many choices of IDEs and tools, open-source databases and multiple hosting options.
- Has a large community