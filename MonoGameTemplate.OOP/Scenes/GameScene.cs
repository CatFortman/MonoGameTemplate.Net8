using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;
using MonoGameLibrary.Models;
using MonoGameLibrary.Scenes;

namespace MonoGameTemplate.OOP.Scenes
{
    public class GameScene : IScene
    {
        // An animated sprite representing the slime character.
        private AnimatedSprite _slime;
        // The position of the slime in pixels.
        private Vector2 _slimePosition;

        private AnimatedSprite _bat;
        // The position of the bat in pixels.
        private Vector2 _batPosition;
        // The velocity of the bat in pixels per second.
        private Vector2 _batVelocity;
        // The speed at which the slime moves.
        private const float MOVEMENT_SPEED = 3f;
        // Defines the tilemap to draw.
        private Tilemap _tilemap;
        // Defines the bounds of the room that the slime and bat are contained within.
        private Rectangle _roomBounds;
        // The sound effect to play when the bat bounces off the edge of the screen.
        private SoundEffect _bounceSoundEffect;
        // The sound effect to play when the slime eats a bat.
        private SoundEffect _collectSoundEffect;

        public void Load(GameContext context)
        {
            var atlas = TextureAtlas.FromFile(context.Content, "atlas-definition.xml");

            Rectangle screenBounds = Core.Device.PresentationParameters.Bounds;

            // Create the tilemap from the XML configuration file.
            _tilemap = Tilemap.FromFile(Core.ContentManager, "tilemap-definition.xml");
            _tilemap.Scale = new Vector2(4.0f, 4.0f);

            _roomBounds = new Rectangle(
                 (int)_tilemap.TileWidth,
                 (int)_tilemap.TileHeight,
                 screenBounds.Width - (int)_tilemap.TileWidth * 2,
                 screenBounds.Height - (int)_tilemap.TileHeight * 2
             );

            // Initial slime position will be the center tile of the tile map.
            int centerRow = _tilemap.Rows / 2;
            int centerColumn = _tilemap.Columns / 2;
            _slimePosition = new Vector2(centerColumn * _tilemap.TileWidth, centerRow * _tilemap.TileHeight);

            // Initial bat position will be in the top left corner of the room
            _batPosition = new Vector2(_roomBounds.Left, _roomBounds.Top);

            _slime = atlas.CreateAnimatedSprite("slime-animation");
            _bat = atlas.CreateAnimatedSprite("bat-animation");

            AssignRandomBatVelocity();

            // Load the bounce sound effect
            _bounceSoundEffect = Core.ContentManager.Load<SoundEffect>("Audio/bounce");

            // Load the collect sound effect
            _collectSoundEffect = Core.ContentManager.Load<SoundEffect>("Audio/collect");

            // Load the background theme music
            Song theme = Core.ContentManager.Load<Song>("Audio/theme");

            // Ensure media player is not already playing on device, if so, stop it
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }

            // Play the background theme music.
            MediaPlayer.Play(theme);

            // Set the theme music to repeat.
            MediaPlayer.IsRepeating = true;
        }

        private void AssignRandomBatVelocity()
        {
            float angle = (float)(Random.Shared.NextDouble() * Math.PI * 2);

            _batVelocity = new Vector2(
                (float)Math.Cos(angle),
                (float)Math.Sin(angle)
            ) * 3f;
        }

        public void Update(GameContext context, GameTime gameTime)
        {
            // --- Update animations ---
            _slime.Update(gameTime);
            _bat.Update(gameTime);

            // Check for keyboard input and handle it.
            CheckKeyboardInput(context);

            // Check for gamepad input and handle it.
            CheckGamePadInput(context);

            // Create a bounding rectangle for the screen.
            Rectangle screenBounds = new Rectangle(
                0,
                0,
                Core.Device.PresentationParameters.BackBufferWidth,
                Core.Device.PresentationParameters.BackBufferHeight
            );

            // Creating a bounding circle for the slime
            Circle slimeBounds = new Circle(
                (int)(_slimePosition.X + (_slime.Width * 0.5f)),
                (int)(_slimePosition.Y + (_slime.Height * 0.5f)),
                (int)(_slime.Width * 0.5f)
            );

            // Use distance based checks to determine if the slime is within the
            // bounds of the game screen, and if it is outside that screen edge,
            // move it back inside.            
            if (context.Input.Keyboard.IsKeyDown(Keys.W))
                _slimePosition.Y -= MOVEMENT_SPEED;

            if (context.Input.Keyboard.IsKeyDown(Keys.S))
                _slimePosition.Y += MOVEMENT_SPEED;

            if (context.Input.Keyboard.IsKeyDown(Keys.A))
                _slimePosition.X -= MOVEMENT_SPEED;

            if (context.Input.Keyboard.IsKeyDown(Keys.D))
                _slimePosition.X += MOVEMENT_SPEED;


            // Calculate the new position of the bat based on the velocity.
            Vector2 newBatPosition = _batPosition + _batVelocity;

            // Create a bounding circle for the bat.
            Circle batBounds = new Circle(
                (int)(newBatPosition.X + (_bat.Width * 0.5f)),
                (int)(newBatPosition.Y + (_bat.Height * 0.5f)),
                (int)(_bat.Width * 0.5f)
            );

            Vector2 normal = Vector2.Zero;

            // Use distance based checks to determine if the bat is within the
            // bounds of the game screen, and if it is outside that screen edge,
            // reflect it about the screen edge normal
            if (batBounds.Left < _roomBounds.Left)
            {
                normal.X = Vector2.UnitX.X;
                newBatPosition.X = _roomBounds.Left;
            }
            else if (batBounds.Right > _roomBounds.Right)
            {
                normal.X = -Vector2.UnitX.X;
                newBatPosition.X = _roomBounds.Right - _bat.Width;
            }

            if (batBounds.Top < _roomBounds.Top)
            {
                normal.Y = Vector2.UnitY.Y;
                newBatPosition.Y = _roomBounds.Top;
            }
            else if (batBounds.Bottom > _roomBounds.Bottom)
            {
                normal.Y = -Vector2.UnitY.Y;
                newBatPosition.Y = _roomBounds.Bottom - _bat.Height;
            }

            // If the normal is anything but Vector2.Zero, this means the bat had
            // moved outside the screen edge so we should reflect it about the
            // normal.
            if (normal != Vector2.Zero)
            {
                _batVelocity = Vector2.Reflect(_batVelocity, normal);

                // Play the bounce sound effect
                _bounceSoundEffect.Play();
            }

            _batPosition = newBatPosition;

            if (slimeBounds.Intersects(batBounds))
            {
                // Choose a random row and column based on the total number of each
                int column = Random.Shared.Next(1, _tilemap.Columns - 1);
                int row = Random.Shared.Next(1, _tilemap.Rows - 1);

                // Change the bat position by setting the x and y values equal to
                // the column and row multiplied by the width and height.
                _batPosition = new Vector2(column * _bat.Width, row * _bat.Height);

                // Assign a new random velocity to the bat
                AssignRandomBatVelocity();

                // Play the collect sound effect
                _collectSoundEffect.Play();
            }
        }

        private void CheckKeyboardInput(GameContext context)
        {
            // If the space key is held down, the movement speed increases by 1.5
            float speed = MOVEMENT_SPEED;
            if (context.Input.Keyboard.IsKeyDown(Keys.Space))
            {
                speed *= 1.5f;
            }

            // If the W or Up keys are down, move the slime up on the screen.
            if (context.Input.Keyboard.IsKeyDown(Keys.W) || context.Input.Keyboard.IsKeyDown(Keys.Up))
            {
                _slimePosition.Y -= speed;
            }

            // if the S or Down keys are down, move the slime down on the screen.
            if (context.Input.Keyboard.IsKeyDown(Keys.S) || context.Input.Keyboard.IsKeyDown(Keys.Down))
            {
                _slimePosition.Y += speed;
            }

            // If the A or Left keys are down, move the slime left on the screen.
            if (context.Input.Keyboard.IsKeyDown(Keys.A) || context.Input.Keyboard.IsKeyDown(Keys.Left))
            {
                _slimePosition.X -= speed;
            }

            // If the D or Right keys are down, move the slime right on the screen.
            if (context.Input.Keyboard.IsKeyDown(Keys.D) || context.Input.Keyboard.IsKeyDown(Keys.Right))
            {
                _slimePosition.X += speed;
            }
        }

        private void CheckGamePadInput(GameContext context)
        {
            GamePadInfo gamePadOne = context.Input.GamePads[(int)PlayerIndex.One];

            // If the A button is held down, the movement speed increases by 1.5
            // and the gamepad vibrates as feedback to the player.
            float speed = MOVEMENT_SPEED;
            if (gamePadOne.IsButtonDown(Buttons.A))
            {
                speed *= 1.5f;
                gamePadOne.SetVibration(1.0f, TimeSpan.FromSeconds(1));
            }
            else
            {
                gamePadOne.StopVibration();
            }

            // Check thumbstick first since it has priority over which gamepad input
            // is movement.  It has priority since the thumbstick values provide a
            // more granular analog value that can be used for movement.
            if (gamePadOne.LeftThumbStick != Vector2.Zero)
            {
                _slimePosition.X += gamePadOne.LeftThumbStick.X * speed;
                _slimePosition.Y -= gamePadOne.LeftThumbStick.Y * speed;
            }
            else
            {
                // If DPadUp is down, move the slime up on the screen.
                if (gamePadOne.IsButtonDown(Buttons.DPadUp))
                {
                    _slimePosition.Y -= speed;
                }

                // If DPadDown is down, move the slime down on the screen.
                if (gamePadOne.IsButtonDown(Buttons.DPadDown))
                {
                    _slimePosition.Y += speed;
                }

                // If DPapLeft is down, move the slime left on the screen.
                if (gamePadOne.IsButtonDown(Buttons.DPadLeft))
                {
                    _slimePosition.X -= speed;
                }

                // If DPadRight is down, move the slime right on the screen.
                if (gamePadOne.IsButtonDown(Buttons.DPadRight))
                {
                    _slimePosition.X += speed;
                }
            }
        }

        public void Draw(GameContext context, GameTime gameTime)
        {
            Core.Device.Clear(Color.MonoGameOrange);

            // Begin the sprite batch to prepare for rendering.
            context.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            // Draw the tilemap.
            _tilemap.Draw(context.SpriteBatch);

            // Draw the slime sprite.
            _slime.Draw(context.SpriteBatch, _slimePosition);

            // Draw the bat sprite.
            _bat.Draw(context.SpriteBatch, _batPosition);
            _slime.Draw(context.SpriteBatch, _slimePosition);

            _bat.Draw(context.SpriteBatch, _batPosition);
            context.SpriteBatch.End();
        }

        public void Unload() { }

        public void OnEnter()
        {
            throw new NotImplementedException();
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }
    }
}