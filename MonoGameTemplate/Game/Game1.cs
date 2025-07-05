using FalseSpirits.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FalseSpirits.Components;
using FalseSpirits.Systems;
using System;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
//using MonoGame.Extended.Tiled.Renderers;
//using MonoGame.Extended.Tiled;

namespace FalseSpirits.Game1;

public class Game1 : Core
{
    // ECS-related
    private EntityManager _entityManager;
    private RestorationSystem _restorationSystem;

    // texture region that defines the slime sprite in the atlas.
    private TextureRegion _slime;

    // texture region that defines the bat sprite in the atlas.
    private TextureRegion _bat;


    // Tiledmap-related
    //private TiledMap _tiledMap;
    //private TiledMapRenderer _mapRenderer;
    public Game1() : base("MonoGameTemplate", 1280, 720, false)
    {        
    }

    protected override void Initialize()
    {
        // Initialize ECS
        _entityManager = new EntityManager();
        _restorationSystem = new RestorationSystem();

        // Create a tree entity
        var tree = _entityManager.CreateEntity();
        tree.AddComponent(new PositionComponent(10, 20));
        tree.AddComponent(new RestorableComponent(RestorationState.Corrupted));
        Console.WriteLine($"Created tree entity with ID: {tree.Id}"); 

        base.Initialize();
    }

    protected override void LoadContent()
    {
         // Create the texture atlas from the XML configuration file
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "atlas-definition.xml");

        // retrieve the slime region from the atlas.
        _slime = atlas.GetRegion("slime");

        // retrieve the bat region from the atlas.
        _bat = atlas.GetRegion("bat");

        //_tiledMap = Content.Load<TiledMap>("forest-example");
        //_mapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Run ECS systems
        //_mapRenderer.Update(gameTime);
        //_restorationSystem.Update(_entityManager);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.MonoGameOrange);

        //SpriteBatch.Begin();
        ////_mapRenderer.Draw();
        //SpriteBatch.End();

        // Begin the sprite batch to prepare for rendering.
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        // Draw the slime texture region at a scale of 4.0
        _slime.Draw(SpriteBatch, Vector2.Zero, Color.White, 0.0f, Vector2.One, 4.0f, SpriteEffects.None, 0.0f);

        // Draw the bat texture region 10px to the right of the slime at a scale of 4.0
        _bat.Draw(SpriteBatch, new Vector2(_slime.Width * 4.0f + 10, 0), Color.White, 0.0f, Vector2.One, 4.0f, SpriteEffects.None, 1.0f);

        // Always end the sprite batch when finished.
        SpriteBatch.End();



        base.Draw(gameTime);
    }
}
