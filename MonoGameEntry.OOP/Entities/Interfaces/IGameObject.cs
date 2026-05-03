using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTemplate.OOP.Entities.Interfaces;

interface IGameObject
{
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}