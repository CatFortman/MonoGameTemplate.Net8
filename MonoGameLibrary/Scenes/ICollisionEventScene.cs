using System.Collections.Generic;
using MonoGameLibrary.ECS;
using MonoGameLibrary.Scenes;

public interface ICollisionEventScene : IEcsScene
{
    List<(Entity A, Entity B)> CollisionEvents { get; }
}