# MonoGameTemplate.Net8

A reusable starter template for building 2D desktop games with MonoGame and .NET 8.

This project provides a clean, modern foundation for creating MonoGame projects using the DesktopGL framework, with support for content management, reusable shared libraries, and preconfigured assets.

## Features

- Built on **.NET 8**
- Uses **MonoGame 3.8 DesktopGL**
- Includes **MonoGame Content Pipeline** support
- Separate reusable **class library** for shared game logic
- Preconfigured application icon and manifest
- Supports audio asset organization
- Ready-to-run starter architecture for 2D games

## Solution Structure

```text
MonoGameTemplate.Net8/
│
├── MonoGameLibrary/
│   ├── MonoGameLibrary.csproj
│   └── Shared game logic, utilities, components
│
├── MonoGameTemplate/
│   ├── MonoGameTemplate.csproj
│   ├── Content/
│   │   ├── Audio/
│   │   └── Content.mgcb
│   ├── app.manifest
│   ├── Icon.ico
│   ├── Icon.bmp
│   └── Main game entry point
│
└── MonoGameTemplate.sln
```

## Technologies Used
- .NET 8
- MonoGame Framework DesktopGL
- MonoGame Content Builder
- OpenGL

## Purpose

This template was created to speed up development of future MonoGame-based projects by providing:

- a reusable architecture
- preconfigured tooling
- content pipeline support

It can be used as the starting point for:

- 2D platformers
- RPGs
- simulation games
- top-down exploration games
- pixel-art adventure games

## Getting Started

### Prerequisites

Install the following:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- MonoGame templates/tools
- OpenGL-compatible graphics drivers

### Clone the Repository

```
git clone https://github.com/CatFortman/MonoGameTemplate.Net8.git
```

### Restore Dependencies

```
dotnet restore
```

### Restore .NET Tools

```
dotnet tool restore
```

### Build the Project

```
dotnet build
```

### Run the Project

```
dotnet run --project MonoGameTemplate
```

## Content Pipeline

This project includes support for the MonoGame Content Pipeline.

Example asset folders:

```text
Content/
├── Audio/
├── Sprites/
├── Fonts/
└── Maps/
```

Assets can be managed via:

```
mgcb-editor
```

## Architecture

The solution separates concerns into two projects:

### MonoGameTemplate

Contains:

- application startup
- rendering loop
- asset loading
- input handling
- game initialization

### MonoGameLibrary

Contains reusable systems such as:

- entity management
- scene/state management
- helper utilities
- shared game components

## Future Improvements

Potential additions:

- Scene manager
- Entity Component System (ECS)
- Tilemap support
- Animation system
- Audio manager
- Shader/effects support
- Save/load system


## Acknowledgements

This project was initially inspired by and partially derived from the official MonoGame 2D tutorial series, then extended into a reusable starter template.
