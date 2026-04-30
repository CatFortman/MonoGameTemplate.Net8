using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Input;

namespace MonoGameLibrary;
public class GameContext
{
    public GraphicsDevice GraphicsDevice { get; init; }
    public SpriteBatch SpriteBatch { get; init; }
    public ContentManager Content { get; init; }
    public InputManager Input { get; init; }
}