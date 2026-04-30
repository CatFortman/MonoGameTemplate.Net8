using MonoGameLibrary;
using MonoGameLibrary.Bootstrap.Interfaces;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.OOP.Scenes;

namespace MonoGameTemplate.OOP.Bootstrap;
public class OopBootstrap : IGameBootstrap
{
    public IScene CreateInitialScene(GameContext context)
    {
        return new GameScene();
    }
}