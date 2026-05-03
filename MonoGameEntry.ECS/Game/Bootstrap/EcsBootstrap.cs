using MonoGameLibrary;
using MonoGameLibrary.Bootstrap.Interfaces;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.ECS.Game.Scenes;

namespace MonoGameTemplate.ECS.Game.Bootstrap;

public class EcsBootstrap : IGameBootstrap
{
    public IScene CreateInitialScene(GameContext context)
    {
        var factory = new SceneFactory();
        var root = new SceneCompositionRoot(factory);

        return root.Create(SceneType.World, context);
    }
}