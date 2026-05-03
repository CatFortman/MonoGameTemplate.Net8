using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using MonoGameTemplate.OOP.Entities;
using MonoGameTemplate.OOP.Entities.Interfaces;

public class Player : IGameObject, ICollidable
{
    public Vector2 Position { get; private set; }

    private readonly float _speed = 2f;
    private readonly float _sprintMultiplier = 1.5f;

    private readonly AnimatedSprite _sprite;

    public Player(AnimatedSprite sprite, Vector2 startPosition)
    {
        _sprite = sprite;
        Position = startPosition;
    }

    public Rectangle Bounds =>
        new Rectangle(
            (int)Position.X,
            (int)Position.Y,
            (int)_sprite.Width,
            (int)_sprite.Height
        );

    public void Update(GameTime gameTime)
    {
        _sprite.Update(gameTime);
    }

    public void MovePlayer(PlayerInput input, Rectangle bounds)
    {
        float speed = _speed;

        if (input.Sprint)
            speed *= _sprintMultiplier;

        Position += input.Movement * speed;

        ClampToBounds(bounds);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _sprite.Draw(spriteBatch, Position);
    }

    private void ClampToBounds(Rectangle bounds)
    {
        Position = new Vector2(
            MathHelper.Clamp(Position.X, bounds.Left, bounds.Right - _sprite.Width),
            MathHelper.Clamp(Position.Y, bounds.Top, bounds.Bottom - _sprite.Height)
        );
    }
}