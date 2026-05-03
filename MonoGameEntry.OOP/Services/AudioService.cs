using Microsoft.Xna.Framework.Audio;

namespace MonoGameTemplate.OOP.Services;

public class AudioService
{
    private SoundEffect _collect;
    private SoundEffect _bounce;

    public AudioService(SoundEffect collect, SoundEffect bounce)
    {
        _collect = collect;
        _bounce = bounce;
    }

    public void PlayCollect() => _collect.Play();
    public void PlayBounce() => _bounce.Play();
}