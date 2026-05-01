using System.Collections.Generic;
using MonoGameLibrary.ECS;

namespace MonoGameLibrary.Scenes;

public interface IEcsScene : IScene
{
    EntityManager Entities { get; }
    HashSet<int> ActiveEntities { get; }
}