using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Bootstrap.Interfaces;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.OOP.Bootstrap;

namespace MonoGameTemplate.OOP.Game1;

public class Game1 : Core
{
    public Game1() : base("MonoGameTemplate.OOP", 1280, 720, false)
    {        
    }
    
    protected override void Initialize()
    {
        base.Initialize();

        IGameBootstrap bootstrap = new OopBootstrap();

        SceneManager.ChangeScene(
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