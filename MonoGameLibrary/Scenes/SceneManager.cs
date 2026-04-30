using Microsoft.Xna.Framework;

namespace MonoGameLibrary.Scenes;

public class SceneManager
{
    private IScene _currentScene;

    private readonly GameContext _context;

    public SceneManager(GameContext context)
    {
        _context = context;
    }

    public void ChangeScene(IScene newScene)
    {
        _currentScene?.Unload();

        _currentScene = newScene;

        _currentScene.Load(_context);
    }

    public void Update(GameTime gameTime)
    {
        _currentScene?.Update(_context, gameTime);
    }

    public void Draw(GameTime gameTime)
    {
        _currentScene?.Draw(_context, gameTime);
    }
}