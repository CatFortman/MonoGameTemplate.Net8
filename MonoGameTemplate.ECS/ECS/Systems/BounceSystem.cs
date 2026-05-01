using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.ECS.Systems;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.ECS.Components;

namespace MonoGameTemplate.ECS.Systems;

public class BounceSystem : IGameSystem
{
    public void Update(GameContext context, GameTime gameTime, IEcsScene scene)
    {
        var bounds = context.GraphicsDevice.Viewport.Bounds;
        var entities = scene.Entities;

        foreach (var entity in entities.Query<PositionComponent, VelocityComponent, SpriteComponent>(scene.ActiveEntities))
        {
            ref var position = ref entities.GetRef<PositionComponent>(entity.Id);
            ref var velocity = ref entities.GetRef<VelocityComponent>(entity.Id);
            var sprite = entities.Get<SpriteComponent>(entity.Id);

            bool bounced = false;

            // Horizontal bounce
            if (position.Value.X <= 0)
            {
                position.Value.X = 0;
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
            if (position.Value.Y <= 0)
            {
                position.Value.Y = 0;
                velocity.Value.Y *= -1;
                bounced = true;
            }
            else if (position.Value.Y + sprite.Sprite.Height >= bounds.Height)
            {
                position.Value.Y = bounds.Height - sprite.Sprite.Height;
                velocity.Value.Y *= -1;
                bounced = true;
            }

            // Play sound ONLY if bounce occurred
            if (bounced && entity.Has<BounceSoundComponent>())
            {
                var sound = entities.Get<BounceSoundComponent>(entity.Id);
                sound.Sound.Play();
            }
        }
    }

    public void Draw(GameContext context, GameTime gameTime, IEcsScene scene) { }
}