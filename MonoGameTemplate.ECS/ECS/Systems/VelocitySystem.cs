namespace MonoGameTemplate.ECS.Systems
{
    public class VelocitySystem : IGameSystem
    {
        private readonly EntityManager _entities;

        public VelocitySystem(EntityManager entities)
        {
            _entities = entities;
        }

        public void Update(GameContext context, GameTime gameTime)
        {
            foreach (var entity in _entities.GetAll())
            {
                if (!entity.Has<PositionComponent>() || !entity.Has<VelocityComponent>())
                    continue;

                var pos = entity.Get<PositionComponent>();
                var vel = entity.Get<VelocityComponent>();

                pos.Position += vel.Velocity;
            }
        }

        public void Draw(GameContext context, GameTime gameTime) { }
    }
}