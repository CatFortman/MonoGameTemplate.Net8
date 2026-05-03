using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.ECS.Systems;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.ECS.Components;

namespace MonoGameTemplate.ECS.Systems;

public class MovementSystem : IGameSystem
{
    private const float FRAME_COMPENSATION = 60f;

    public void Update(GameContext context, GameTime gameTime, IEcsScene scene)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        var entities = scene.Entities;

        foreach (var entity in entities.Query<PositionComponent, VelocityComponent>())
        {
            ref var position = ref entities.GetRef<PositionComponent>(entity.Id);
            ref var velocity = ref entities.GetRef<VelocityComponent>(entity.Id);

            // OOP-like feel preserved via compensation factor
            position.Value += velocity.Value * dt * FRAME_COMPENSATION;
        }
    }

    public void Draw(GameContext context, GameTime gameTime, IEcsScene scene) { }
}