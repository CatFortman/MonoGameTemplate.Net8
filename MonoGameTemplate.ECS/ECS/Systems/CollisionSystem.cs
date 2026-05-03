using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.ECS;
using MonoGameLibrary.ECS.Systems;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.ECS.Components;
using MonoGameTemplate.ECS.Scenes;

namespace MonoGameTemplate.ECS.Systems;

public class CollisionSystem : IGameSystem
{
    private readonly HashSet<(int, int)> _previousCollisions = new();

    public void Update(GameContext context, GameTime gameTime, IEcsScene scene)
    {
        if (scene is not ICollisionEventScene eventScene)
            throw new InvalidOperationException("CollisionSystem requires ICollisionEventScene");

        var entities = eventScene.Entities;

        var query = entities.Query<PositionComponent, BoundsComponent>();
        var list = new List<Entity>(query);

        var currentCollisions = new HashSet<(int, int)>();

        for (int i = 0; i < list.Count; i++)
        {
            var a = list[i];
            var aRect = GetBoundsRect(entities, a);

            for (int j = i + 1; j < list.Count; j++)
            {
                var b = list[j];
                var bRect = GetBoundsRect(entities, b);

                if (aRect.Intersects(bRect))
                {
                    var pair = NormalizePair(a.Id, b.Id);
                    currentCollisions.Add(pair);

                    if (!_previousCollisions.Contains(pair))
                    {
                        eventScene.CollisionEvents.Add((a, b));
                    }
                }
            }
        }

        _previousCollisions.Clear();
        foreach (var pair in currentCollisions)
            _previousCollisions.Add(pair);
    }

    private (int, int) NormalizePair(int a, int b)
    {
        return a < b ? (a, b) : (b, a);
    }

    private Rectangle GetBoundsRect(EntityManager entities, Entity e)
    {
        var pos = entities.Get<PositionComponent>(e.Id);
        var bounds = entities.Get<BoundsComponent>(e.Id);

        return new Rectangle(
            (int)(pos.Value.X + bounds.Offset.X),
            (int)(pos.Value.Y + bounds.Offset.Y),
            (int)bounds.Width,
            (int)bounds.Height
        );
    }

    public void Draw(GameContext context, GameTime gameTime, IEcsScene scene) { }
}