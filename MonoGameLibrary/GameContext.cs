using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.ECS;
using MonoGameLibrary.Input;
using MonoGameLibrary.Scenes;

namespace MonoGameLibrary;
public class GameContext
{
    public GraphicsDevice GraphicsDevice { get; set; }
    public SpriteBatch SpriteBatch { get; set; }
    public ContentManager Content { get; set; }
    public InputManager Input { get; set; }

    public IScene CurrentScene { get; set; }
    public EntityManager Entities { get; set; }
}