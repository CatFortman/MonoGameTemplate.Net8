using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Input;

namespace MonoGameLibrary.Scenes;

public interface ISceneContext
{
    GameContext Game { get; }

    GraphicsDevice GraphicsDevice { get; }
    ContentManager Content { get; }

    InputManager Input { get; }
}