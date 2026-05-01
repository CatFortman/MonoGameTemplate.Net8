using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Models;
using MonoGameLibrary.Scenes;

namespace MonoGameTemplate.OOP.Scenes;

public class GameScene : IScene
{
    // Slime controlled by player, moves with WASD or Arrow Keys
    private AnimatedSprite _slime;
    private Vector2 _slimePosition;
    private Circle _slimeBounds;
    // Bat that bounces around the room, player must avoid it
    private AnimatedSprite _bat;
    private Vector2 _batPosition;
    private Vector2 _batVelocity;

    // Speed at which the slime moves
    private const float MOVEMENT_SPEED = 2f;

    // Tilemap for the room, defines the boundaries and background
    private Tilemap _tilemap;
    private Rectangle _roomBounds;

    // Sound effects for bouncing and collecting
    private SoundEffect _bounceSoundEffect;
    private SoundEffect _collectSoundEffect;

    // Background music for the scene
    private Song _theme;

    // Reference to the game context for accessing content, scene, etc.
    private GameContext _context;

    // ---------------- LOAD (once) ----------------
    public void Load(GameContext context)
    {
        _context = context;

        var atlas = TextureAtlas.FromFile(context.Content, "atlas-definition.xml");

        _tilemap = Tilemap.FromFile(context.Content, "tilemap-definition.xml");
        _tilemap.Scale = new Vector2(4f, 4f);

        var screenBounds = context.GraphicsDevice.PresentationParameters.Bounds;

        _roomBounds = new Rectangle(
            (int)_tilemap.TileWidth,
            (int)_tilemap.TileHeight,
            screenBounds.Width - (int)_tilemap.TileWidth * 2,
            screenBounds.Height - (int)_tilemap.TileHeight * 2
        );

        _slime = atlas.CreateAnimatedSprite("slime-animation");
        _bat = atlas.CreateAnimatedSprite("bat-animation");

        _slime.Scale = new Vector2(4.0f, 4.0f);
        _bat.Scale = new Vector2(4.0f, 4.0f);

        _bounceSoundEffect = context.Content.Load<SoundEffect>("Audio/bounce");
        _collectSoundEffect = context.Content.Load<SoundEffect>("Audio/collect");
        _theme = context.Content.Load<Song>("Audio/theme");

        _slimePosition = new Vector2(
            _tilemap.Columns / 2 * _tilemap.TileWidth,
            _tilemap.Rows / 2 * _tilemap.TileHeight
        );

        _batPosition = new Vector2(_roomBounds.Left, _roomBounds.Top);

        AssignRandomBatVelocity();
    }

    // ---------------- ENTER (runtime activation) ----------------
    public void OnEnter()
    {
        if (MediaPlayer.State == MediaState.Playing)
            MediaPlayer.Stop();

        MediaPlayer.Play(_theme);
        MediaPlayer.IsRepeating = true;

        AssignRandomBatVelocity();
    }

    // ---------------- UPDATE (logic only) ----------------
    public void Update(GameContext context, GameTime gameTime)
    {
        _slime.Update(gameTime);
        _bat.Update(gameTime);

        HandleInput(context);
        UpdateBatMovement();
        HandleCollision();
    }

    // ---------------- DRAW (render only) ----------------
    public void Draw(GameContext context, GameTime gameTime)
    {
        context.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        context.SpriteBatch.DrawString(
                    context.Content.Load<SpriteFont>("Fonts/default"),
                    "Use WASD or Arrow Keys to Move. Hold Space to Speed Up.",
                    new Vector2(10, 10),
                    Color.MonoGameOrange
                );
                
        _tilemap.Draw(context.SpriteBatch);
        _slime.Draw(context.SpriteBatch, _slimePosition);
        _bat.Draw(context.SpriteBatch, _batPosition);

        context.SpriteBatch.End();
    }

    // ---------------- INPUT ----------------
    private void HandleInput(GameContext context)
    {
        float speed = MOVEMENT_SPEED;

        if (context.Input.Keyboard.IsKeyDown(Keys.Space))
            speed *= 1.5f;

        var k = context.Input.Keyboard;

        if (k.IsKeyDown(Keys.W) || k.IsKeyDown(Keys.Up)) _slimePosition.Y -= speed;
        if (k.IsKeyDown(Keys.S) || k.IsKeyDown(Keys.Down)) _slimePosition.Y += speed;
        if (k.IsKeyDown(Keys.A) || k.IsKeyDown(Keys.Left)) _slimePosition.X -= speed;
        if (k.IsKeyDown(Keys.D) || k.IsKeyDown(Keys.Right)) _slimePosition.X += speed;

        // Creating a bounding circle for the slime
        _slimeBounds = new Circle(
            (int)(_slimePosition.X + (_slime.Width * 0.5f)),
            (int)(_slimePosition.Y + (_slime.Height * 0.5f)),
            (int)(_slime.Width * 0.5f)
        );

        // Use distance based checks to determine if the slime is within the
        // bounds of the game screen, and if it is outside that screen edge,
        // move it back inside.
        if (_slimeBounds.Left < _roomBounds.Left)
        {
            _slimePosition.X = _roomBounds.Left;
        }
        else if (_slimeBounds.Right > _roomBounds.Right)
        {
            _slimePosition.X = _roomBounds.Right - _slime.Width;
        }

        if (_slimeBounds.Top < _roomBounds.Top)
        {
            _slimePosition.Y = _roomBounds.Top;
        }
        else if (_slimeBounds.Bottom > _roomBounds.Bottom)
        {
            _slimePosition.Y = _roomBounds.Bottom - _slime.Height;
        }
    }

    // ---------------- BAT LOGIC ----------------
    private void UpdateBatMovement()
    {
        Vector2 newPos = _batPosition + _batVelocity;

        Vector2 normal = Vector2.Zero;

        if (newPos.X <= _roomBounds.Left)
        {
            newPos.X = _roomBounds.Left;
            normal.X = 1;
        }
        else if (newPos.X + _bat.Width >= _roomBounds.Right)
        {
            newPos.X = _roomBounds.Right - _bat.Width;
            normal.X = -1;
        }

        if (newPos.Y <= _roomBounds.Top)
        {
            newPos.Y = _roomBounds.Top;
            normal.Y = 1;
        }
        else if (newPos.Y + _bat.Height >= _roomBounds.Bottom)
        {
            newPos.Y = _roomBounds.Bottom - _bat.Height;
            normal.Y = -1;
        }

        if (normal != Vector2.Zero)
        {
            _batVelocity = Vector2.Reflect(_batVelocity, normal);
            _bounceSoundEffect.Play();
        }

        _batPosition = newPos;
    }

    // ---------------- COLLISION ----------------
    private void HandleCollision()
    {
        var slimeRect = new Rectangle(
            (int)_slimePosition.X,
            (int)_slimePosition.Y,
            (int)_slime.Width,
            (int)_slime.Height
        );

        var batRect = new Rectangle(
            (int)_batPosition.X,
            (int)_batPosition.Y,
            (int)_bat.Width,
            (int)_bat.Height
        );

        if (slimeRect.Intersects(batRect))
        {
            _batPosition = new Vector2(
                Random.Shared.Next(1, _tilemap.Columns - 1) * (int)_bat.Width,
                Random.Shared.Next(1, _tilemap.Rows - 1) * (int)_bat.Height
            );

            AssignRandomBatVelocity();
            _collectSoundEffect.Play();
        }
    }

    // ---------------- UTIL ----------------
    private void AssignRandomBatVelocity()
    {
        float angle = (float)(Random.Shared.NextDouble() * Math.PI * 2);

        _batVelocity = new Vector2(
            MathF.Cos(angle),
            MathF.Sin(angle)
        ) * 3f;
    }

    public void OnExit()
    {
        if (MediaPlayer.State == MediaState.Playing)
            MediaPlayer.Pause();
    }

    public void Unload() { }
}