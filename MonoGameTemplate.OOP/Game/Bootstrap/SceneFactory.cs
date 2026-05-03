using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.OOP.Entities;
using MonoGameTemplate.OOP.Game.Scenes;
using MonoGameTemplate.OOP.Services;

namespace MonoGameTemplate.OOP.Game.Bootstrap;

public class SceneFactory : ISceneFactory
{
    public IScene CreateGameScene(GameContext context)
    {
        // --- CONTENT ---
        var tilemap = Tilemap.FromFile(context.Content, "tilemap-definition.xml");
        tilemap.Scale = new Vector2(4f, 4f);

        var font = context.Content.Load<SpriteFont>("Fonts/default");
        var theme = context.Content.Load<Song>("Audio/theme");

        // --- AUDIO ---
        var collectSound = context.Content.Load<SoundEffect>("Audio/collect");
        var bounceSound = context.Content.Load<SoundEffect>("Audio/bounce");

        // --- WORLD ---
        var screen = context.GraphicsDevice.PresentationParameters.Bounds;

        var worldBounds = new Rectangle(
            (int)tilemap.TileWidth,
            (int)tilemap.TileHeight,
            screen.Width - (int)tilemap.TileWidth * 2,
            screen.Height - (int)tilemap.TileHeight * 2
        );

        // --- SERVICES ---
        var collision = new CollisionService();
        var audio = new AudioService(collectSound, bounceSound);
        var interaction = new GameInteractionService(audio);

        // --- ENTITIES ---
        var atlas = TextureAtlas.FromFile(context.Content, "atlas-definition.xml");
        var playerSprite = atlas.CreateAnimatedSprite("slime-animation");
        var enemySprite = atlas.CreateAnimatedSprite("bat-animation");

        playerSprite.Scale = new Vector2(4f, 4f);
        enemySprite.Scale = new Vector2(4f, 4f);

        var player = new Player(playerSprite,  new Vector2(
                tilemap.Columns / 2 * tilemap.TileWidth,
                tilemap.Rows / 2 * tilemap.TileHeight
            ));

        var enemy = new Enemy(enemySprite, new Vector2(worldBounds.Left, worldBounds.Top));

        var sceneContext = new GameSceneContext
        {
            Player = player,
            Enemy = enemy,
            Collision = collision,
            Interaction = interaction,
            Audio = audio,
            Tilemap = tilemap,
            WorldBounds = worldBounds,
            Font = font,
            Theme = theme
        };

        return new GameScene(sceneContext);
    }
}

