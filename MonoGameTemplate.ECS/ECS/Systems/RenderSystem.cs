namespace MonoGameTemplate.ECS.Systems
{
    public class RenderSystem : IGameSystem
    {
        private readonly EntityManager _entities;

        public RenderSystem(EntityManager entities)
        {
            _entities = entities;
        }

        public void Update(GameContext context, GameTime gameTime)
        {
            foreach (var entity in _entities.GetAll())
            {
                if (entity.Has<SpriteComponent>())
                {
                    entity.Get<SpriteComponent>().Sprite.Update(gameTime);
                }
            }
        }

        public void Draw(GameContext context, GameTime gameTime)
        {
            context.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            foreach (var entity in _entities.GetAll())
            {
                if (!entity.Has<SpriteComponent>() || !entity.Has<PositionComponent>())
                    continue;

                var sprite = entity.Get<SpriteComponent>().Sprite;
                var pos = entity.Get<PositionComponent>().Position;

                sprite.Draw(context.SpriteBatch, pos);
            }

            context.SpriteBatch.End();
        }
    }
}