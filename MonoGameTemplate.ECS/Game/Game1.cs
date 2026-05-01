using Microsoft.Xna.Framework;
using MonoGameLibrary.Bootstrap.Interfaces;
using MonoGameTemplate.ECS.Bootstrap;

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

        var context = Core.Context;

        Core.SceneManager.ChangeScene(
            bootstrap.CreateInitialScene(context)
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