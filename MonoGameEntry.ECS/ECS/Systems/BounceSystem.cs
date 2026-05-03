using System;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.ECS.Systems;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.ECS.Components;
using MonoGameTemplate.ECS.Interfaces;

namespace MonoGameTemplate.ECS.Systems;

public class BounceSystem : IGameSystem
{
    public void Update(GameContext context, GameTime gameTime, IEcsScene scene)
    {
        if (scene is IWorldBoundsProvider provider)
        {
            Rectangle? worldBounds = provider?.WorldBounds;

            if (worldBounds == null)
                throw new InvalidOperationException("WorldBoundsSystem requires IWorldBoundsProvider");

            var bounds = worldBounds.Value;
            var entities = scene.Entities;

            foreach (var entity in entities.Query<BounceComponent, PositionComponent, VelocityComponent, SpriteComponent>())
            {
                ref var position = ref entities.GetRef<PositionComponent>(entity.Id);
                ref var velocity = ref entities.GetRef<VelocityComponent>(entity.Id);
                var sprite = entities.Get<SpriteComponent>(entity.Id);

                bool bounced = false;

                // Horizontal bounce
                if (position.Value.X <= bounds.Left)
                {
                    position.Value.X = bounds.Left;
                    velocity.Value.X *= -1;
                    bounced = true;
                }
                else if (position.Value.X + sprite.Sprite.Width >= bounds.Width)
                {
                    position.Value.X = bounds.Width - sprite.Sprite.Width;
                    velocity.Value.X *= -1;
                    bounced = true;
                }

                // Vertical bounce
                if (position.Value.Y <= bounds.Top)
                {
                    position.Value.Y = bounds.Top;
                    velocity.Value.Y *= -1;
                    bounced = true;
                }
                else if (position.Value.Y + sprite.Sprite.Height >= bounds.Bottom)
                {
                    position.Value.Y = bounds.Bottom - sprite.Sprite.Height;
                    velocity.Value.Y *= -1;
                    bounced = true;
                }

                // Play sound ONLY if bounce occurred
                if (bounced)
                {
                    var sound = entities.Get<BounceSoundComponent>(entity.Id);
                    sound.Sound.Play();
                }
            }
        }
    }

    public void Draw(GameContext context, GameTime gameTime, IEcsScene scene) { }
}