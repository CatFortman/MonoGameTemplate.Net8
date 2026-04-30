namespace MonoGameTemplate.ECS.Components
{
    public class SpriteComponent
    {
        public AnimatedSprite Sprite;

        public SpriteComponent(AnimatedSprite sprite)
        {
            Sprite = sprite;
        }
    }
}