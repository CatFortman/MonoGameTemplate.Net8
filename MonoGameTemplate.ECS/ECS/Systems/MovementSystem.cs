using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.ECS;
using MonoGameLibrary.ECS.Systems;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.ECS.Components;

namespace MonoGameTemplate.ECS.Systems;

public class MovementSystem : IGameSystem
{
    public void Update(GameContext context, GameTime gameTime, IEcsScene scene)
    {
        foreach (var entity in scene.Entities.Query<PositionComponent, VelocityComponent>(scene.ActiveEntities))
        {
            ref var position = ref scene.Entities.GetRef<PositionComponent>(entity.Id);
            ref var velocity = ref scene.Entities.GetRef<VelocityComponent>(entity.Id);

            position.Value += velocity.Value;
        }
    }

    public void Draw(GameContext context, GameTime gameTime, IEcsScene scene) { }
}