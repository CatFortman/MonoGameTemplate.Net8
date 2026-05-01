using Microsoft.Xna.Framework;
using MonoGameLibrary.Components;

namespace MonoGameTemplate.ECS.Components;

public class BoundsComponent : IComponent
{
    public float Width;
    public float Height;
    public Vector2 Offset;
}