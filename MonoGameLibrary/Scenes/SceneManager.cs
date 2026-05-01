using Microsoft.Xna.Framework;

namespace MonoGameLibrary.Scenes;

public class SceneManager
{
    private IScene _current;
    private readonly GameContext _context;

    public IScene CurrentScene => _current;

    public SceneManager(GameContext context)
    {
        _context = context;        
    }

    public void ChangeScene(IScene scene)
    {
        _current?.OnExit();
        _current?.Unload();

        _current = scene;

        _current.Load(_context);
        _current.OnEnter();
    }

    public void Update(GameTime gameTime)
    {
        _current?.Update(_context, gameTime);
    }

    public void Draw(GameTime gameTime)
    {
        _current?.Draw(_context, gameTime);
    }
}