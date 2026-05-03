using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.ECS.Systems;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.ECS.Components;

namespace MonoGameTemplate.ECS.Systems;

public class InputSystem : IGameSystem
{
    private const float MOVEMENT_SPEED = 2f;

    public void Update(GameContext context, GameTime gameTime, IEcsScene scene)
    {
        var entities = scene.Entities;
        var k = context.Input.Keyboard;

        foreach (var entity in entities.Query<VelocityComponent>())
        {
            if (!entity.Has<PlayerTag>())
                continue;

            ref var velocity = ref entities.GetRef<VelocityComponent>(entity.Id);

            Vector2 direction = Vector2.Zero;

            if (k.IsKeyDown(Keys.W) || k.IsKeyDown(Keys.Up))
                direction.Y -= 1;

            if (k.IsKeyDown(Keys.S) || k.IsKeyDown(Keys.Down))
                direction.Y += 1;

            if (k.IsKeyDown(Keys.A) || k.IsKeyDown(Keys.Left))
                direction.X -= 1;

            if (k.IsKeyDown(Keys.D) || k.IsKeyDown(Keys.Right))
                direction.X += 1;

            if (direction != Vector2.Zero)
                direction.Normalize();

            float speed = MOVEMENT_SPEED;

            if (k.IsKeyDown(Keys.Space))
                speed *= 1.5f;

            velocity.Value = direction * speed;
        }
    }

    public void Draw(GameContext context, GameTime gameTime, IEcsScene scene) { }
}