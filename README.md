[![Build using Cake.Sdk and File based Cake](https://github.com/cake-build/cakesdk-example/actions/workflows/Cake_Sdk-File.yml/badge.svg?branch=main)](https://github.com/cake-build/cakesdk-example/actions/workflows/Cake_Sdk-File.yml)
&nbsp;
[![Build using Cake.Sdk and Multi-File based Cake](https://github.com/cake-build/cakesdk-example/actions/workflows/Cake_Sdk-MultiFile.yml/badge.svg?branch=main)](https://github.com/cake-build/cakesdk-example/actions/workflows/Cake_Sdk-MultiFile.yml)
&nbsp;
[![Build using Cake.Sdk and Multi-File Advanced Cake](https://github.com/cake-build/cakesdk-example/actions/workflows/Cake_Sdk-MultiFile-Advanced.yml/badge.svg?branch=main)](https://github.com/cake-build/cakesdk-example/actions/workflows/Cake_Sdk-MultiFile-Advanced.yml)
&nbsp;
[![Build using Cake.Sdk and Project based Cake](https://github.com/cake-build/cakesdk-example/actions/workflows/Cake_Sdk-Proj.yml/badge.svg)](https://github.com/cake-build/cakesdk-example/actions/workflows/Cake_Sdk-Proj.yml)


# Cake.Sdk Example Repository

This repository demonstrates minimal, modern usage of [Cake.Sdk](https://www.nuget.org/packages/Cake.Sdk/) for .NET build automation. It showcases file-based, multi-file-based, and project-based approaches for defining Cake build scripts, and includes a minimal .NET class library.

## Features

- **File-based build script**: Standalone `build.cs` using Cake Sdk directives.
- **Multi-file-based build script**: `multifile-build/build.cs` with additional files in build folder.
- **Advanced multi-file-based build script**: `multifile-build-advanced/build.cs` with organized structure, dependency injection, and service patterns.
- **Project-based build script**: `build/build.csproj` referencing Cake.Sdk.
- **Pinned versions**: .NET SDK and Cake.Sdk versions are pinned via `global.json`.
- **CI examples**: Example GitHub Actions workflows for all approaches.

## Repository Structure

```
/
├── build.cs                  # File-based Cake build script
|
├── build/
│   └── build.csproj          # Project-based Cake build script using Cake.Sdk
|
├── multifile-build/
│   ├── build.cs              # Multi-file based Cake build script
│   └── build/
│       └── BuildData.cs      # BuildData model for Multi-file build script
|
├── multifile-build-advanced/
│   ├── build.cs              # Advanced multi-file based Cake build script
│   └── build/
│       ├── Models/
│       │   └── BuildData.cs  # BuildData model with Rebuild property
│       ├── Services/
│       │   ├── IMyService.cs # Service interface
│       │   └── MyService.cs  # Service implementation with GitVersion logic
│       ├── IoC.cs            # Dependency injection configuration
│       └── Task.cs           # Additional task definitions
|
├── src/
│   └── Example/
│       └── Example.csproj    # Minimal .NET class library (net10.0)
|
├── global.json               # Pins .NET SDK and Cake.Sdk versions
├── .github/
│   └── workflows/
│       ├── CakeFile.yml      # GitHub Actions: file-based build
│       ├── CakeProj.yml      # GitHub Actions: project-based build
│       ├── Cake_Sdk-MultiFile.yml # GitHub Actions: multi-file-based build
│       └── Cake_Sdk-MultiFile-Advanced.yml # GitHub Actions: advanced multi-file-based build
|
└── README.md                 # This file
```

## Usage

- **File-based build**:  
  Run with:  
  ```sh
  dotnet cake build.cs
  ```
- **Project-based build**:
  Run with:  
  ```sh
  dotnet run --project build/build.csproj
  ```
- **Multi-file-based build**:
  Run with:
  ```sh
  dotnet cake multifile-build/build.cs
  ```
- **Advanced multi-file-based build**:
  Run with:
  ```sh
  dotnet cake multifile-build-advanced/build.cs
  ```

## Continuous Integration

- **.github/workflows/CakeFile.yml**: Runs the file-based build script on push/PR.
- **.github/workflows/CakeProj.yml**: Runs the project-based build script on push/PR.
- **.github/workflows/Cake_Sdk-MultiFile.yml**: Runs the multi-file-based build script on push/PR.
- **.github/workflows/Cake_Sdk-MultiFile-Advanced.yml**: Runs the advanced multi-file-based build script on push/PR.

All workflows use the pinned .NET SDK and Cake.Sdk versions from `global.json`.

## About Cake.Sdk

[Cake.Sdk](https://www.nuget.org/packages/Cake.Sdk/) is a custom SDK for Cake build automation, providing:
- Minimal project configuration
- Optimized build settings
- Built-in source generation (via Cake.Generator)
- Automatic inclusion of Cake.Core, Cake.Common, and Cake.Cli

**Requirements:** .NET 8.0 or later
