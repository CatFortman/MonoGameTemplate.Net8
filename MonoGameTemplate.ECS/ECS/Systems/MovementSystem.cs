namespace MonoGameTemplate.ECS.Systems
{
    public class MovementSystem : IGameSystem
    {
        private readonly EntityManager _entities;

        public MovementSystem(EntityManager entities)
        {
            _entities = entities;
        }

        public void Update(GameContext context, GameTime gameTime)
        {
            foreach (var entity in _entities.GetAll())
            {
                if (!entity.Has<PositionComponent>() || !entity.Has<PlayerTag>())
                    continue;

                var position = entity.Get<PositionComponent>();

                float speed = 5f;

                if (context.Input.Keyboard.IsKeyDown(Keys.Space))
                    speed *= 1.5f;

                if (context.Input.Keyboard.IsKeyDown(Keys.W))
                    position.Position.Y -= speed;

                if (context.Input.Keyboard.IsKeyDown(Keys.S))
                    position.Position.Y += speed;

                if (context.Input.Keyboard.IsKeyDown(Keys.A))
                    position.Position.X -= speed;

                if (context.Input.Keyboard.IsKeyDown(Keys.D))
                    position.Position.X += speed;
            }
        }

        public void Draw(GameContext context, GameTime gameTime) { }
    }
}