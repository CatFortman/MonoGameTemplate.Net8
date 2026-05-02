namespace MonoGameLibrary.Scenes
{
    public interface ISceneFactory
    {
        IScene CreateGameScene(GameContext context);
    }
}