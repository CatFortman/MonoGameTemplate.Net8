using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.ECS;
using MonoGameLibrary.ECS.Systems;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.ECS.Components;

namespace MonoGameTemplate.ECS.Systems;

public class WorldBoundsSystem : IGameSystem
{
    private readonly Rectangle _bounds;

    public WorldBoundsSystem(Rectangle bounds)
    {
        _bounds = bounds;
    }

    public void Update(GameContext context, GameTime gameTime, IEcsScene scene)
    {
        var entities = scene.Entities;

        foreach (var entity in entities.Query<PositionComponent, BoundsComponent>(scene.ActiveEntities))
        {
            ref var pos = ref entities.GetRef<PositionComponent>(entity.Id);
            var bounds = entities.Get<BoundsComponent>(entity.Id);

            // Build rect
            var rect = new Rectangle(
                (int)(pos.Value.X + bounds.Offset.X),
                (int)(pos.Value.Y + bounds.Offset.Y),
                (int)bounds.Width,
                (int)bounds.Height
            );

            // Clamp or bounce depending on components
            if (entity.Has<BounceComponent>() && entity.Has<VelocityComponent>())
            {
                HandleBounce(entity, rect, scene);
            }
            else
            {
                ClampPosition(ref pos, bounds);
            }
        }
    }

    private void ClampPosition(ref PositionComponent pos, BoundsComponent bounds)
    {
        float minX = _bounds.Left - bounds.Offset.X;
        float minY = _bounds.Top - bounds.Offset.Y;
        float maxX = _bounds.Right - bounds.Width - bounds.Offset.X;
        float maxY = _bounds.Bottom - bounds.Height - bounds.Offset.Y;

        pos.Value.X = MathHelper.Clamp(pos.Value.X, minX, maxX);
        pos.Value.Y = MathHelper.Clamp(pos.Value.Y, minY, maxY);
    }

    private void HandleBounce(Entity entity, Rectangle rect, IEcsScene scene)
    {
        var entities = scene.Entities;

        ref var pos = ref entities.GetRef<PositionComponent>(entity.Id);
        ref var vel = ref entities.GetRef<VelocityComponent>(entity.Id);
        var bounds = entities.Get<BoundsComponent>(entity.Id);

        Vector2 normal = Vector2.Zero;

        if (rect.Left <= _bounds.Left)
        {
            pos.Value.X = _bounds.Left - bounds.Offset.X;
            normal.X = 1;
        }
        else if (rect.Right >= _bounds.Right)
        {
            pos.Value.X = _bounds.Right - bounds.Width - bounds.Offset.X;
            normal.X = -1;
        }

        if (rect.Top <= _bounds.Top)
        {
            pos.Value.Y = _bounds.Top - bounds.Offset.Y;
            normal.Y = 1;
        }
        else if (rect.Bottom >= _bounds.Bottom)
        {
            pos.Value.Y = _bounds.Bottom - bounds.Height - bounds.Offset.Y;
            normal.Y = -1;
        }

        if (normal != Vector2.Zero)
        {
            vel.Value = Vector2.Reflect(vel.Value, normal);
        }
    }

    public void Draw(GameContext context, GameTime gameTime, IEcsScene scene) { }
}