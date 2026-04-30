using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MonoGameLibrary.Systems.Interfaces;

public class SystemManager
{
    private readonly List<IGameSystem> _systems = new();

    public void Add(IGameSystem system) => _systems.Add(system);

    public void Update(GameContext context, GameTime gameTime)
    {
        foreach (var system in _systems)
            system.Update(context, gameTime);
    }

    public void Draw(GameContext context, GameTime gameTime)
    {
        foreach (var system in _systems)
            system.Draw(context, gameTime);
    }
}