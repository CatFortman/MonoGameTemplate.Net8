using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameTemplate.OOP.Entities;

public class InputBuffer
{
    public PlayerInput Current { get; private set; }

    public void Capture(GameContext context)
    {
        var k = context.Input.Keyboard;

        Current = new PlayerInput
        {
            Sprint = k.IsKeyDown(Keys.Space),
            Movement = new Vector2(
                (k.IsKeyDown(Keys.D) || k.IsKeyDown(Keys.Right) ? 1 : 0) -
                (k.IsKeyDown(Keys.A) || k.IsKeyDown(Keys.Left) ? 1 : 0),

                (k.IsKeyDown(Keys.S) || k.IsKeyDown(Keys.Down) ? 1 : 0) -
                (k.IsKeyDown(Keys.W) || k.IsKeyDown(Keys.Up) ? 1 : 0)
            )
        };
    }
}