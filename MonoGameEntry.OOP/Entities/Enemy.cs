using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using MonoGameTemplate.OOP.Entities.Interfaces;

namespace MonoGameTemplate.OOP.Entities;

public class Enemy : IGameObject, ICollidable
{
    public Vector2 Position { get; private set; }
    public Vector2 Velocity { get; private set; }
    public bool DidBounce { get; private set; }

    private AnimatedSprite _sprite;

    public Enemy(AnimatedSprite sprite, Vector2 startPosition)
    {
        _sprite = sprite;
        Position = startPosition;
        Velocity = GenerateRandomVelocity();
    }

    public Rectangle Bounds =>
        new Rectangle((int)Position.X, (int)Position.Y, (int)_sprite.Width, (int)_sprite.Height);

    public void Update(GameTime gameTime)
    {
        Position += Velocity;
        _sprite.Update(gameTime);
    }

    public void Respawn(Rectangle bounds)
    {
        Position = new Vector2(
            Random.Shared.Next(bounds.Left, bounds.Right - (int)_sprite.Width),
            Random.Shared.Next(bounds.Top, bounds.Bottom - (int)_sprite.Height)
        );

        Velocity = GenerateRandomVelocity();
    }

    private Vector2 GenerateRandomVelocity()
    {
        float angle = (float)(Random.Shared.NextDouble() * Math.PI * 2);
        return new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * 3f;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _sprite.Draw(spriteBatch, Position);
    }

    public void ApplyBounds(Rectangle bounds)
    {

    }


    public void MoveEnemy(Rectangle bounds)
    {
        DidBounce = false;

        Position += Velocity;

        if (Position.X <= bounds.Left || Position.X + _sprite.Width >= bounds.Right)
        {
            Velocity = new Vector2(-Velocity.X, Velocity.Y);
            DidBounce = true;
        }

        if (Position.Y <= bounds.Top || Position.Y + _sprite.Height >= bounds.Bottom)
        {
            Velocity = new Vector2(Velocity.X, -Velocity.Y);
            DidBounce = true;
        }
    }
}