using Microsoft.Xna.Framework;
using MonoGameLibrary.Scenes;

namespace MonoGameLibrary.ECS.Systems;

public interface IGameSystem
{
    void Update(GameContext context, GameTime gameTime, IEcsScene scene);
    void Draw(GameContext context, GameTime gameTime, IEcsScene scene);
}