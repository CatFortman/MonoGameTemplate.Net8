using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Scenes;
using MonoGameLibrary.Systems.Interfaces;

namespace MonoGameTemplate.ECS.Scenes;

public class GameplayScene : IScene
{
    private EntityManager _entities;
    private SystemManager _systems;

    public GameplayScene(EntityManager entities, SystemManager systems)
    {
        _entities = entities;
        _systems = systems;
    }

    public void Update(GameContext context, GameTime gameTime)
    {
        _systems.Update(context, gameTime);
    }

    public void Draw(GameContext context, GameTime gameTime)
    {
        _systems.Draw(context, gameTime);
    }

    public void Unload()
    {
        _entities.Clear(); // if you add this later
    }

    public void Load(GameContext context)
    {
        throw new System.NotImplementedException();
    }

}