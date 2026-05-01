using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.ECS.Systems;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.ECS.Components;

public class SpriteUpdateSystem : IGameSystem
{
    public void Update(GameContext context, GameTime gameTime, IEcsScene scene)
    {
        foreach (var entity in scene.Entities.Query<SpriteComponent>(scene.ActiveEntities))
        {
            scene.Entities.GetRef<SpriteComponent>(entity.Id)
                          .Sprite.Update(gameTime);
        }
    }

    public void Draw(GameContext context, GameTime gameTime, IEcsScene scene) { }
}