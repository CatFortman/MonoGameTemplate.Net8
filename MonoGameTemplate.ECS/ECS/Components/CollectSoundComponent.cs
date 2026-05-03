using Microsoft.Xna.Framework.Audio;
using MonoGameLibrary.ECS.Interfaces;

namespace MonoGameTemplate.ECS.Components;

public class CollectSoundComponent : IComponent
{
    public SoundEffect Sound { get; set; }
}