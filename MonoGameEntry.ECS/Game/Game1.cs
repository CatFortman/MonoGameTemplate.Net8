using Microsoft.Xna.Framework;
using MonoGameLibrary.Bootstrap.Interfaces;
using MonoGameTemplate.ECS.Game.Bootstrap;

namespace MonoGameTemplate.ECS.Game1;

public class Game1 : Core
{
    public Game1()
        : base("MonoGameTemplate.ECS", 1280, 720, false)
    {
    }

    protected override void Initialize()
    {
        base.Initialize();

        IGameBootstrap bootstrap = new EcsBootstrap();

        Core.SceneManager.ChangeScene(
            bootstrap.CreateInitialScene(Core.Context)
        );
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
    }
}