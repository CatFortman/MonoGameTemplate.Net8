using System;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.ECS.Systems;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.ECS.Components;
using MonoGameTemplate.ECS.Interfaces;

namespace MonoGameTemplate.ECS.Systems;

public class WorldBoundsSystem : IGameSystem
{
    public WorldBoundsSystem()
    {
    }

    public void Update(GameContext context, GameTime gameTime, IEcsScene scene)
    {
        if (scene is IWorldBoundsProvider provider)
        {
            Rectangle? worldBounds = provider?.WorldBounds;
            
            if (worldBounds == null)
                throw new InvalidOperationException("WorldBoundsSystem requires IWorldBoundsProvider");

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

                ClampPosition(ref pos, bounds, worldBounds.Value);
            }
        }
    }

    private void ClampPosition(ref PositionComponent pos, BoundsComponent bounds, Rectangle worldBounds)
    {
        float minX = worldBounds.Left - bounds.Offset.X;
        float minY = worldBounds.Top - bounds.Offset.Y;
        float maxX = worldBounds.Right - bounds.Width - bounds.Offset.X;
        float maxY = worldBounds.Bottom - bounds.Height - bounds.Offset.Y;

        pos.Value.X = MathHelper.Clamp(pos.Value.X, minX, maxX);
        pos.Value.Y = MathHelper.Clamp(pos.Value.Y, minY, maxY);
    }

    public void Draw(GameContext context, GameTime gameTime, IEcsScene scene) { }
}