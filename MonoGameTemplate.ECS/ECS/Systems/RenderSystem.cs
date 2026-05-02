using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.ECS.Systems;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.ECS.Components;

namespace MonoGameTemplate.ECS.Systems;

public class RenderSystem : IGameSystem
{
    public void Update(GameContext context, GameTime gameTime, IEcsScene scene)
    {
        // rendering systems do nothing in update
    }

    public void Draw(GameContext context, GameTime gameTime, IEcsScene scene)
    {
        var entities = scene.Entities;

        foreach (var entity in entities.Query<PositionComponent, SpriteComponent>(scene.ActiveEntities))
        {
            var position = entities.Get<PositionComponent>(entity.Id);
            var sprite = entities.Get<SpriteComponent>(entity.Id);

            sprite.Sprite.Draw(context.SpriteBatch, position.Value);
        }

        context.SpriteBatch.DrawString(
                          scene.Font,
                          "Use WASD or Arrow Keys to Move. Hold Space to Speed Up.",
                          new Vector2(25, 25),
                          Color.MonoGameOrange
                      );
    }
}