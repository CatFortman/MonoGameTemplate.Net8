using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.ECS;

namespace MonoGameLibrary.Scenes;

public interface IEcsScene : IScene
{
    EntityManager Entities { get; }
    HashSet<int> ActiveEntities { get; }
    SpriteFont Font { get; }

}