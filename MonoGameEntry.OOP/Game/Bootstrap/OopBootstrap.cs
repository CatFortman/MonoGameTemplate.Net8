using MonoGameLibrary;
using MonoGameLibrary.Bootstrap.Interfaces;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.OOP.Game.Scenes;

namespace MonoGameTemplate.OOP.Game.Bootstrap;

public class OopBootstrap : IGameBootstrap
{
    public IScene CreateInitialScene(GameContext context)
    {
        var factory = new SceneFactory();
        var root = new SceneCompositionRoot(factory);

        return root.Create(SceneType.Game, context);
    }
}