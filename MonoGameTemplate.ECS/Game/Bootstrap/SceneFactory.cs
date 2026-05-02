using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.ECS;
using MonoGameLibrary.ECS.Systems;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.ECS.Components;
using MonoGameTemplate.ECS.Game.Scenes;
using MonoGameTemplate.ECS.Systems;

namespace MonoGameTemplate.ECS.Game.Bootstrap;

public class SceneFactory : ISceneFactory
{
    public IScene CreateGameScene(GameContext context)
    {
        var entities = new EntityManager();
        var systems = new SystemManager();

        var tilemap = Tilemap.FromFile(context.Content, "tilemap-definition.xml");
        tilemap.Scale = new Vector2(4f, 4f);

        var screenBounds = context.GraphicsDevice.PresentationParameters.Bounds;

        var worldBounds = new Rectangle(
            (int)tilemap.TileWidth,
            (int)tilemap.TileHeight,
            screenBounds.Width - (int)tilemap.TileWidth * 2,
            screenBounds.Height - (int)tilemap.TileHeight * 2
        );

        var font = context.Content.Load<SpriteFont>("Fonts/default");

        RegisterSystems(systems);

        CreatePlayer(entities, context, tilemap);
        CreateEnemy(entities, context, worldBounds);

        return new EcsGameScene(
            entities,
            systems,
            worldBounds,
            tilemap,
            font
        );
    }

    private void RegisterSystems(SystemManager systems)
    {
        systems.Add(new InputSystem());
        systems.Add(new MovementSystem());
        systems.Add(new WorldBoundsSystem());
        systems.Add(new BounceSystem());
        systems.Add(new CollisionSystem());
        systems.Add(new GameSystem());
        systems.Add(new SpriteUpdateSystem());
        systems.Add(new RenderSystem());
    }

    private void CreatePlayer(EntityManager entities, GameContext context, Tilemap tilemap)
    {
        var player = entities.CreateEntity();

        var sprite = context.Content
            .Load<TextureAtlas>("atlas-definition.xml")
            .CreateAnimatedSprite("slime-animation");

        sprite.Scale = new Vector2(4f, 4f);

        player.Add(new PositionComponent
        {
            Value = new Vector2(
                tilemap.Columns / 2 * tilemap.TileWidth,
                tilemap.Rows / 2 * tilemap.TileHeight
            )
        });

        player.Add(new VelocityComponent { Value = Vector2.Zero });
        player.Add(new SpriteComponent { Sprite = sprite });
        player.Add(new BoundsComponent { Width = sprite.Width, Height = sprite.Height });

        player.Add(new PlayerTag());
    }

    private void CreateEnemy(EntityManager entities, GameContext context, Rectangle worldBounds)
    {
        var bat = entities.CreateEntity();

        var sprite = context.Content
            .Load<TextureAtlas>("atlas-definition.xml")
            .CreateAnimatedSprite("bat-animation");

        sprite.Scale = new Vector2(4f, 4f);

        bat.Add(new PositionComponent
        {
            Value = new Vector2(worldBounds.Left, worldBounds.Top)
        });

        bat.Add(new VelocityComponent
        {
            Value = RandomDirection() * 3f
        });

        bat.Add(new SpriteComponent { Sprite = sprite });
        bat.Add(new BoundsComponent { Width = sprite.Width, Height = sprite.Height });

        bat.Add(new BounceComponent());
        bat.Add(new EnemyTag());
    }

    private Vector2 RandomDirection()
    {
        float angle = (float)(Random.Shared.NextDouble() * Math.PI * 2);

        return new Vector2(
            MathF.Cos(angle),
            MathF.Sin(angle)
        );
    }
}
