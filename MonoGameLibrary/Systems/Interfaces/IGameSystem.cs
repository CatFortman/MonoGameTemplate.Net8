using Microsoft.Xna.Framework;

public interface IGameSystem
{
    void Update(GameContext context, GameTime gameTime);
    void Draw(GameContext context, GameTime gameTime);
}