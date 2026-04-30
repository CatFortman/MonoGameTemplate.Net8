namespace MonoGameTemplate.ECS.Systems
{
    public class CollisionSystem : IGameSystem
    {
        private readonly EntityManager _entities;

        public CollisionSystem(EntityManager entities)
        {
            _entities = entities;
        }

        public void Update(GameContext context, GameTime gameTime)
        {
            Entity player = null;
            Entity bat = null;

            foreach (var e in _entities.GetAll())
            {
                if (e.Has<PlayerTag>()) player = e;
                if (e.Has<BatTag>()) bat = e;
            }

            if (player == null || bat == null) return;

            var pPos = player.Get<PositionComponent>();
            var bPos = bat.Get<PositionComponent>();

            float distance = Vector2.Distance(pPos.Position, bPos.Position);

            if (distance < 40f)
            {
                // reposition bat randomly
                bPos.Position = new Vector2(
                    Random.Shared.Next(100, 1000),
                    Random.Shared.Next(100, 600)
                );
            }
        }

        public void Draw(GameContext context, GameTime gameTime) { }
    }
}