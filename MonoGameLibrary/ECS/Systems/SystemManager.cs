using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Scenes;

namespace MonoGameLibrary.ECS.Systems;

public class SystemManager
{
    private readonly List<IGameSystem> _systems = new();

    public void Add(IGameSystem system)
    {
        _systems.Add(system);
    }

    public void Update(GameContext context, GameTime gameTime, IEcsScene scene)
    {
        foreach (var system in _systems)
            system.Update(context, gameTime, scene);
    }

    public void Draw(GameContext context, GameTime gameTime, IEcsScene scene)
    {
        foreach (var system in _systems)
            system.Draw(context, gameTime, scene);
    }

    public void Clear()
    {
        _systems.Clear();
    }
}