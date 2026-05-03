using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGameLibrary.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;
using MonoGameLibrary;

namespace MonoGameTemplate.OOP.Game.Scenes;

public class GameSceneContext : ISceneContext
{
    // Game Data
    public GameContext Game { get; init; }
    public GraphicsDevice GraphicsDevice => Game.GraphicsDevice;
    public ContentManager Content => Game.Content;
    public InputManager Input => Game.Input;

    public Tilemap Tilemap { get; init; }
    public Rectangle WorldBounds { get; init; }
    public SpriteFont Font { get; init; }
    public Song Theme { get; init; }

}