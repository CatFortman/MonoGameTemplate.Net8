using MonoGameLibrary.Scenes;

namespace MonoGameLibrary.Bootstrap.Interfaces;
public interface IGameBootstrap
{
    IScene CreateInitialScene(GameContext context);
}