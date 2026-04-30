using MonoGameLibrary;
using MonoGameLibrary.Bootstrap.Interfaces;
using MonoGameLibrary.Scenes;
using MonoGameLibrary.Systems.Interfaces;
using MonoGameTemplate.ECS.Scenes;

namespace MonoGameTemplate.ECS.Bootstrap;

public class EcsBootstrap : IGameBootstrap
{
    public IScene CreateInitialScene(GameContext context)
    {
        return new GameplayScene(new EntityManager(), new SystemManager());
    }
}