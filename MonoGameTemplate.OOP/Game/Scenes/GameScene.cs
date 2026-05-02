using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.OOP.Entities;

namespace MonoGameTemplate.OOP.Game.Scenes;

public class GameScene : IScene
{
    private GameSceneContext _context;

    private InputBuffer _inputBuffer = new InputBuffer();

    public GameScene(GameSceneContext context)
    {
        _context = context;
    }

    public void Load(GameContext context)
    {

    }

    public void OnEnter()
    {
        if (MediaPlayer.State == MediaState.Paused)
        {
            MediaPlayer.Resume();
        }
        else
        {
            MediaPlayer.Play(_context.Theme);
            MediaPlayer.IsRepeating = true;
        }
    }
    public void OnExit()
    {
        if (MediaPlayer.State == MediaState.Playing)
            MediaPlayer.Pause();
    }

    public void Unload()
    {
        throw new System.NotImplementedException();
    }

    public void Update(GameContext context, GameTime gameTime)
    {
        var keyboard = context.Input.Keyboard;

        var input = new PlayerInput
        {
            Sprint = keyboard.IsKeyDown(Keys.Space),
            Movement = new Vector2(
                (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right) ? 1 : 0) -
                (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left) ? 1 : 0),

                (keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down) ? 1 : 0) -
                (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up) ? 1 : 0)
            )
        };

        _inputBuffer.Capture(context);

        _context.Player.MovePlayer(_inputBuffer.Current, _context.WorldBounds);
        _context.Player.Update(gameTime);

        _context.Enemy.Update(gameTime);
        _context.Enemy.ApplyBounds(_context.WorldBounds);

        if (_context.Collision.Intersects(_context.Player, _context.Enemy))
        {
            _context.Interaction.HandlePlayerEnemyCollision(_context.Player, _context.Enemy);
        }
    }

    public void Draw(GameContext context, GameTime gameTime)
    {
        context.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        _context.Tilemap.Draw(context.SpriteBatch);

        _context.Player.Draw(context.SpriteBatch);
        _context.Enemy.Draw(context.SpriteBatch);

        context.SpriteBatch.DrawString(
            _context.Font,
            "Use WASD or Arrow Keys. Hold Space to sprint.",
            new Vector2(20, 20),
            Color.White
        );

        context.SpriteBatch.End();
    }
}