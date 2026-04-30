using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;

namespace MonoGameTemplate.OOP.Scenes
{
   public class GameScene : IScene
{
    private AnimatedSprite _slime;
    private Vector2 _position;

    public void Load(GameContext context)
    {
        var atlas = TextureAtlas.FromFile(context.Content, "atlas-definition.xml");
        _slime = atlas.CreateAnimatedSprite("slime");
        _position = new Vector2(100, 100);
    }

    public void Update(GameContext context, GameTime gameTime)
    {
        if (context.Input.Keyboard.IsKeyDown(Keys.W))
            _position.Y -= 3;
    }

    public void Draw(GameContext context, GameTime gameTime)
    {
        context.SpriteBatch.Begin();
        _slime.Draw(context.SpriteBatch, _position);
        context.SpriteBatch.End();
    }

    public void Unload() { }
}
}