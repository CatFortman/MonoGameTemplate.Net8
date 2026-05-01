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

        foreach (var entity in scene.Entities.Query<PositionComponent, VelocityComponent, SpriteComponent>(scene.ActiveEntities))
        {
            ref var position = ref scene.Entities.GetRef<PositionComponent>(entity.Id);
            ref var velocity = ref scene.Entities.GetRef<VelocityComponent>(entity.Id);
            var sprite = scene.Entities.Get<SpriteComponent>(entity.Id);

            if (position.Value.X <= 0 || position.Value.X + sprite.Sprite.Width >= bounds.Width)
                velocity.Value.X *= -1;

            if (position.Value.Y <= 0 || position.Value.Y + sprite.Sprite.Height >= bounds.Height)
                velocity.Value.Y *= -1;
        }
    }

    public void Draw(GameContext context, GameTime gameTime, IEcsScene scene) { }
}