using Microsoft.Xna.Framework;
using MonoGameLibrary.ECS.Interfaces;

namespace MonoGameTemplate.ECS.Components;

public struct VelocityComponent : IComponent
{
    public Vector2 Value;
}
