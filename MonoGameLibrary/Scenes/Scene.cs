using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary.ECS;
using MonoGameLibrary.ECS.Systems;

namespace MonoGameLibrary.Scenes;

public class Scene : IEcsScene
{
    public EntityManager Entities { get; private set; }
    public HashSet<int> ActiveEntities { get; } = new();

    private readonly List<IGameSystem> _systems = new();

    protected GameContext Context;

    public virtual void Load(GameContext context)
    {
        Context = context;
        Entities = new EntityManager();
    }

    public virtual void Update(GameContext context, GameTime gameTime)
    {
        foreach (var system in _systems)
        {
            system.Update(context, gameTime, this);
        }
    }

    public virtual void Draw(GameContext context, GameTime gameTime)
    {
        foreach (var system in _systems)
        {
            system.Draw(context, gameTime, this);
        }
    }

    public virtual void Unload()
    {
        _systems.Clear();
        ActiveEntities.Clear();
    }

    public virtual void OnEnter() { }
    public virtual void OnExit() { }

    protected void AddSystem(IGameSystem system)
    {
        _systems.Add(system);
    }
}