using Microsoft.Xna.Framework;

namespace MonoGameLibrary.Scenes;

public interface IScene
{
    void Load(GameContext context);
    void Unload();

    void OnEnter();
    void OnExit();

    void Update(GameContext context, GameTime gameTime);
    void Draw(GameContext context, GameTime gameTime);

}