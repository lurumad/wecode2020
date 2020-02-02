# ASP.NET Core 3.0 Workshop

This repo contains the materials for my workshop in the [WeCodeFest 2020](https://wecodefest.com)

## How to build

The solution is built against the latest NET Core 3.0

- Install the required .NET Core SDK using in Linux [install-sdk.sh](install-sdk.sh) or Windows [install-sdk.ps1](install-sdk.ps1).
- Install docker
- Run docker-compose -f .\build\docker-compose-infrastructure.yml up -d (Or you can use SQL Server Express LocalDB)
- Run [build.ps1](build.ps1) in the root of the repo.

## How to code

You can use [Visual Studio 2019](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/). For Visual Studio Code you'll need to install this extension [C# for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp).

## Your job

You have to complete [NotesController](/src/WeCode.Api/Controllers/NotesController.cs) in order to pass al FunctionalTests.
