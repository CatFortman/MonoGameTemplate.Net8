using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Bootstrap.Interfaces;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.ECS.Bootstrap;

namespace MonoGameTemplate.ECS.Game1;

public class Game1 : Core
{
    private SceneManager _sceneManager;

    public Game1()
        : base("MonoGameTemplate.ECS", 1280, 720, false)
    {
    }

    protected override void Initialize()
    {
        base.Initialize();

        _sceneManager = new SceneManager(Core.Context);

        IGameBootstrap bootstrap = new EcsBootstrap();

        _sceneManager.ChangeScene(
            bootstrap.CreateInitialScene(Core.Context)
        );
    }

    protected override void Update(GameTime gameTime)
    {
        _sceneManager.Update(Core.Context, gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _sceneManager.Draw(Core.Context, gameTime);

        base.Draw(gameTime);
    }
}