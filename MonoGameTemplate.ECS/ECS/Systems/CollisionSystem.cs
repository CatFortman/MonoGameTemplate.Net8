using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.ECS;
using MonoGameLibrary.ECS.Systems;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.ECS.Components;

namespace MonoGameTemplate.ECS.Systems;

public class CollisionSystem : IGameSystem
{
    public void Update(GameContext context, GameTime gameTime, IEcsScene scene)
    {
        var entities = scene.Entities;

        var query = scene.Entities.Query<PositionComponent, SpriteComponent>(scene.ActiveEntities);

        var list = new List<Entity>(query);

        for (int i = 0; i < list.Count; i++)
        {
            var a = list[i];

            var aPos = entities.Get<PositionComponent>(a.Id);
            var aSprite = entities.Get<SpriteComponent>(a.Id);

            var aRect = new Rectangle(
                (int)aPos.Value.X,
                (int)aPos.Value.Y,
                (int)aSprite.Sprite.Width,
                (int)aSprite.Sprite.Height
            );

            for (int j = i + 1; j < list.Count; j++)
            {
                var b = list[j];

                var bPos = entities.Get<PositionComponent>(b.Id);
                var bSprite = entities.Get<SpriteComponent>(b.Id);

                var bRect = new Rectangle(
                    (int)bPos.Value.X,
                    (int)bPos.Value.Y,
                    (int)bSprite.Sprite.Width,
                    (int)bSprite.Sprite.Height
                );

                if (aRect.Intersects(bRect))
                {
                    HandleCollision(a, b, scene);
                }
            }
        }
    }

    private void HandleCollision(Entity a, Entity b, IEcsScene scene)
    {
        var entities = scene.Entities;

        bool aHasPlayer = a.Has<PlayerTag>() || b.Has<PlayerTag>();
        bool aHasBat = a.Has<BatTag>() || b.Has<BatTag>();

        // Example behavior: reset bat position on player collision
        if (aHasPlayer && aHasBat)
        {
            var bat = a.Has<BatTag>() ? a : b;

            ref var pos = ref entities.GetRef<PositionComponent>(bat.Id);

            pos.Value = new Vector2(
                Random.Shared.Next(100, 1000),
                Random.Shared.Next(100, 600)
            );

            ref var vel = ref entities.GetRef<VelocityComponent>(bat.Id);
            vel.Value = RandomDirection() * 3f;
        }
    }

    private Vector2 RandomDirection()
    {
        float angle = (float)(Random.Shared.NextDouble() * Math.PI * 2);

        return new Vector2(
            (float)Math.Cos(angle),
            (float)Math.Sin(angle)
        );
    }

    public void Draw(GameContext context, GameTime gameTime, IEcsScene scene) { }
}