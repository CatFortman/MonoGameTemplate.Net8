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
    private bool _playerEnemyCollisionLastFrame = false;

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
        _inputBuffer.Capture(context);

        _context.Player.MovePlayer(_inputBuffer.Current, _context.WorldBounds);
        _context.Player.Update(gameTime);

        _context.Enemy.MoveEnemy(_context.WorldBounds);
        _context.Enemy.Update(gameTime);

        if (_context.Enemy.DidBounce)
        {
            _context.Interaction.HandleEnemyWallCollision(_context.Enemy);
        }

        bool isColliding = _context.Collision.Intersects(_context.Player, _context.Enemy);

        if (isColliding && !_playerEnemyCollisionLastFrame)
        {
            _context.Interaction.HandlePlayerEnemyCollision(_context.Player, _context.Enemy);
        }

        _playerEnemyCollisionLastFrame = isColliding;
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
            new Vector2(25, 25),
            Color.MonoGameOrange
        );

        context.SpriteBatch.End();
    }
}