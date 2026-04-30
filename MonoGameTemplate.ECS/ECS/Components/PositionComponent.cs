namespace MonoGameTemplate.ECS.Components
{
    public class PositionComponent : IComponent
    {
        public Vector2 Position;

        public PositionComponent(Vector2 position)
        {
            Position = position;
        }
    }
}
