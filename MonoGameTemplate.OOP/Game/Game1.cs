using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Bootstrap.Interfaces;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.OOP.Bootstrap;

namespace MonoGameTemplate.OOP.Game1;

public class Game1 : Core
{
    private SceneManager _sceneManager;

    public Game1() : base("MonoGameTemplate.OOP", 1280, 720, false)
    {        
    }
    
    protected override void Initialize()
    {
        base.Initialize();

        var context = Core.Context;

        if (context == null)
            throw new InvalidOperationException("Core.Context is not initialized.");

        _sceneManager = new SceneManager(Core.Context);

        IGameBootstrap bootstrap = new OopBootstrap();

        _sceneManager.ChangeScene(
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