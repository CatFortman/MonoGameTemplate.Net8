using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.ECS;
using MonoGameLibrary.ECS.Systems;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.ECS.Components;
using MonoGameTemplate.ECS.Systems;

namespace MonoGameTemplate.ECS.Scenes;

public class GameScene : IEcsScene, ICollisionEventScene
{
    private EntityManager _entities;
    private SystemManager _systems;
    public SpriteFont Font { get; private set; }
    public List<(Entity A, Entity B)> CollisionEvents { get; } = new();

    public void Load(GameContext context)
    {
        _entities = new EntityManager();
        _systems = new SystemManager();

        Font = context.Content.Load<SpriteFont>("Fonts/default");

        var screenBounds = context.GraphicsDevice.PresentationParameters.Bounds;

        // Register systems (order matters)
        _systems.Add(new InputSystem());
        _systems.Add(new MovementSystem());
        _systems.Add(new WorldBoundsSystem(screenBounds));
        _systems.Add(new BounceSystem());
        _systems.Add(new CollisionSystem());
        _systems.Add(new GameSystem()); 
        _systems.Add(new SpriteUpdateSystem());
        _systems.Add(new RenderSystem());


        var atlas = TextureAtlas.FromFile(context.Content, "atlas-definition.xml");

        // --- Player ---
        var player = _entities.CreateEntity();
        player.Add(new PositionComponent { Value = new Vector2(200, 200) });
        player.Add(new VelocityComponent { Value = Vector2.Zero });

        var playerSprite = atlas.CreateAnimatedSprite("slime-animation");
        player.Add(new SpriteComponent { Sprite = playerSprite });

        player.Add(new BoundsComponent
        {
            Width = playerSprite.Width,
            Height = playerSprite.Height
        });

        var collectSound = context.Content.Load<SoundEffect>("Audio/collect");
        player.Add(new CollectSoundComponent
        {
            Sound = collectSound
        });

        player.Add(new PlayerTag());

        // --- Bat ---
        var bat = _entities.CreateEntity();
        bat.Add(new PositionComponent { Value = new Vector2(300, 100) });
        bat.Add(new VelocityComponent { Value = RandomDirection() * 3f });

        var batSprite = atlas.CreateAnimatedSprite("bat-animation");
        bat.Add(new SpriteComponent { Sprite = batSprite });

        bat.Add(new BoundsComponent
        {
            Width = batSprite.Width,
            Height = batSprite.Height
        });

        bat.Add(new BounceComponent());

        var bounceSound = context.Content.Load<SoundEffect>("Audio/bounce");
        bat.Add(new BounceSoundComponent
        {
            Sound = bounceSound
        });

        bat.Add(new EnemyTag());
    }

    public void Update(GameContext context, GameTime gameTime)
    {
        _systems.Update(context, gameTime, this);
    }

    public void Draw(GameContext context, GameTime gameTime)
    {
        _systems.Draw(context, gameTime, this);
    }

    public void Unload()
    {
        _systems.Clear();
        _entities.Clear();
    }

    public void OnEnter() { }
    public void OnExit() { }

    private Vector2 RandomDirection()
    {
        float angle = (float)(Random.Shared.NextDouble() * Math.PI * 2);

        return new Vector2(
            (float)Math.Cos(angle),
            (float)Math.Sin(angle)
        );
    }

    // Expose ECS access for systems
    public EntityManager Entities => _entities;
    public HashSet<int> ActiveEntities { get; } = new();
}

