# MonoGameTemplate.Net8

A reusable starter template for building 2D desktop games with MonoGame and .NET 8. Provides reusable engine components and services while remaining architecture-agnostic.

This project provides a clean, modern foundation for creating MonoGame projects using the DesktopGL framework, with support for content management, reusable shared libraries, and preconfigured assets.

---

## Purpose

Provides reusable engine components and services. Architecture-agnostic.

---

## Core Features (High Level)

- Built on **.NET 8**
- Uses **MonoGame 3.8 DesktopGL**
- Clean separation between engine library and game projects
- Shared reusable game framework (`MonoGameLibrary`)
- Content Pipeline integration (`.mgcb`)
- Input abstraction layer (keyboard, mouse, gamepad)
- 2D rendering utilities (sprites, animation, tilemaps)
- Lightweight scene system (OOP + ECS support)
- Audio support (SFX + music via MonoGame MediaPlayer)
- Ready-to-run game architecture

---

## Project Breakdown

### 🧱 MonoGameLibrary (Engine Core)

This is the reusable “engine layer” used by both OOP and ECS templates.

#### Features

- Game lifecycle abstraction (`GameContext`)
- Graphics abstraction (`SpriteBatch`, rendering helpers)
- Input system abstraction (keyboard, mouse, gamepad)
- Scene management foundation (`Scene`, `IEcsScene`)
- Sprite system:
  - Static sprites
  - Animated sprites
- Tilemap rendering system
- Basic collision primitives (Circle, Rectangle helpers)
- ECS foundation (EntityManager, SystemManager, components)

#### Used by

- OOP Template ✔
- ECS Template ✔

---

### 🎮 MonoGameTemplate.OOP (Object-Oriented Template)

A traditional game architecture using scene-based OOP design.

#### Features

- Scene-driven architecture (`IScene`)
- Direct entity/state management
- Manual game loop logic per scene
- Built-in support for:
  - Sprite animation
  - Tilemaps
  - Input handling (WASD / Arrow keys)
  - Sound effects
  - Background music
- Simple collision handling (rectangle & circle-based)
- Immediate feedback loop design (ideal for prototyping)

#### Best suited for

- Game jams
- Small to medium 2D games
- Rapid prototyping
- Learning MonoGame fundamentals

---

### ⚙️ MonoGameTemplate.ECS (Entity Component System Template)

A data-driven architecture using ECS principles.

#### Features

- Entity Component System (ECS) architecture
- Component-based design:
  - Position / Velocity
  - Sprite / Bounds
  - Tags (Player, Enemy)
  - Audio components (SoundEffect-based)
- System-driven logic:
  - InputSystem
  - MovementSystem
  - BounceSystem
  - CollisionSystem (event-based optional mode)
  - RenderSystem
- World bounds constraint system
- Collision event pipeline (`ICollisionEventScene`)
- Decoupled gameplay logic via systems
- Scalable architecture for complex simulations

#### Key ECS advantages

- Separation of data and behaviour
- Highly reusable systems
- Easier scaling for large entity counts
- Clear gameplay pipeline (Input → Movement → Physics → Collision → Render)

#### Best suited for

- Simulation-heavy games
- Large-scale entity systems
- Long-term scalable projects
- Experimentation with engine architecture

---

## Mapping: Who Uses What

| Component     | OOP Template | ECS Template |
| ------------- | ------------ | ------------ |
| Core          | Yes          | Yes          |
| GameContext   | Optional     | Yes          |
| Graphics      | Yes          | Yes          |
| Input         | Yes          | Yes          |
| Models        | Yes          | Yes          |
| Scene System  | Yes          | Yes (ECS version) |
| IGameSystem   | No           | Yes          |
| SystemManager | No           | Yes          |
| EntityManager | Optional     | Yes          |

---

## Solution Structure

```text
MonoGameTemplate.Net8/
│
├── MonoGameLibrary/
│   ├── Bootstrap/
│   ├── ECS/
│   │   ├── Interfaces/
│   │   ├── Systems/
│   │   ├── ComponentStore.cs/
│   │   ├── Entity.cs/
│   │   └── EntityManager.cs/
│   ├── Graphics/
│   ├── Input/
│   ├── Models/
│   ├── Scenes/
│   ├── Core.cs
│   ├── GameContext.cs
│   └── MonoGameLibrary.csproj
│
├── MonoGameTemplate.ECS/
│   ├── Content/
│   │   ├── Audio/
│   │   ├── Fonts/
│   │   ├── Maps/
│   │   └── Content.mgcb
│   ├── ECS/
│   │   ├── Components/
│   │   └── Systems/
│   ├── Game/
│   │   ├── Bootstrap/
│   │   ├── Scenes/
│   │   └── Game1.cs
│   └── MonoGameTemplate.ECS.csproj
│
├── MonoGameTemplate.OOP/
│   ├── Content/
│   │   ├── Audio/
│   │   ├── Fonts/
│   │   ├── Maps/
│   │   └── Content.mgcb
│   ├── Entities/
│   ├── Game/
│   │   ├── Bootstrap/
│   │   ├── Scenes/
│   │   └── Game1.cs
│   ├── Services/
│   └── MonoGameTemplate.OOP.csproj
│
└── MonoGameTemplate.sln
```

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

### Select Entry Project

```
cd MonoGameTemplate.OOP     
```

or

```
cd MonoGameTemplate.ECS     
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
dotnet run
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
dotnet mgcb-editor ./Content/Content.mgcb
```

## Future Improvements

Potential additions:

- Animation system
- Audio manager
- Shader/effects support
- Save/load system
- Additional Scenes

## Acknowledgements

This project is built using MonoGame and is inspired by the [tutorial](https://docs.monogame.net/articles/tutorials/building_2d_games/) series created by [Aristurtle](https://github.com/AristurtleDev).

Portions of the architecture and learning structure were derived from public educational content.

Credit is also due to [u/JustARandomDude112](https://www.reddit.com/user/JustARandomDude112/) for architectural ideas and design discussions that helped shape parts of this framework.
