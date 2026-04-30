using MonoGameLibrary.Components;
using MonoGameLibrary.Graphics;

namespace MonoGameTemplate.ECS.Components
{
    public class SpriteComponent : IComponent
    {
        public AnimatedSprite Sprite;

        public SpriteComponent(AnimatedSprite sprite)
        {
            Sprite = sprite;
        }
    }
}