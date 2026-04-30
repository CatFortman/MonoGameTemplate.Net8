using Microsoft.Xna.Framework;

namespace MonoGameLibrary.Scenes;

public class SceneManager
{
    private IScene _currentScene;

    public void ChangeScene(IScene scene, GameContext context)
    {
        _currentScene?.Unload();
        _currentScene = scene;
        _currentScene.Load(context);
    }

    public void Update(GameContext context, GameTime gameTime)
        => _currentScene?.Update(context, gameTime);

    public void Draw(GameContext context, GameTime gameTime)
        => _currentScene?.Draw(context, gameTime);
}
