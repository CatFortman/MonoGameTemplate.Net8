using Microsoft.Xna.Framework.Audio;
using MonoGameLibrary.Components;

namespace MonoGameTemplate.ECS.Components;

public class BounceSoundComponent : IComponent
{
    public SoundEffect Sound { get; set; }
}