using System;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.ECS;
using MonoGameLibrary.ECS.Systems;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.ECS.Components;
using MonoGameTemplate.ECS.Scenes;

namespace MonoGameTemplate.ECS.Systems;

public class GameSystem : IGameSystem
{
    public void Update(GameContext context, GameTime gameTime, IEcsScene scene)
    {
        if (scene is ICollisionEventScene eventScene)
        {
            foreach (var (a, b) in eventScene.CollisionEvents)
            {
                if (a.Has<PlayerTag>() && b.Has<EnemyTag>())
                {
                    HandlePlayerEnemyCollision(a, b, scene);
                }
                else if (b.Has<PlayerTag>() && a.Has<EnemyTag>())
                {
                    HandlePlayerEnemyCollision(b, a, scene);
                }
            }

            eventScene.CollisionEvents.Clear();
        }
    }

    public void Draw(GameContext context, GameTime gameTime, IEcsScene scene)
    {
        // No drawing for this system
    }

    private void HandlePlayerEnemyCollision(Entity player, Entity enemy, IEcsScene scene)
    {
        var entities = scene.Entities;

        // --- SOUND ---
        if (player.Has<CollectSoundComponent>())
        {
            var sound = entities.Get<CollectSoundComponent>(player.Id);
            sound.Sound.Play();
        }

        // --- GAMEPLAY ---
        ref var pos = ref entities.GetRef<PositionComponent>(enemy.Id);
        ref var vel = ref entities.GetRef<VelocityComponent>(enemy.Id);

        pos.Value = new Vector2(
            Random.Shared.Next(100, 1000),
            Random.Shared.Next(100, 600)
        );

        vel.Value = RandomDirection() * 3f;
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