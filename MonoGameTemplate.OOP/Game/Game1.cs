using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Bootstrap.Interfaces;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.OOP.Bootstrap;

namespace MonoGameTemplate.OOP.Game1;

public class Game1 : Core
{
    private SceneManager _sceneManager;

    public Game1()
        : base("Game", 1280, 720, false)
    {
    }

    protected override void Initialize()
    {
        base.Initialize();

        var context = Core.Context;

        _sceneManager = new SceneManager(context);

        IGameBootstrap bootstrap = new OopBootstrap();

        _sceneManager.ChangeScene(
            bootstrap.CreateInitialScene(context)
        );
    }

    protected override void Update(GameTime gameTime)
    {
        _sceneManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _sceneManager.Draw(gameTime);

        base.Draw(gameTime);
    }
}