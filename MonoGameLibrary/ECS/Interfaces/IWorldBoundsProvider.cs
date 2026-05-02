
using Microsoft.Xna.Framework;

namespace MonoGameTemplate.ECS.Interfaces;

public interface IWorldBoundsProvider
{
    Rectangle WorldBounds { get; }
}