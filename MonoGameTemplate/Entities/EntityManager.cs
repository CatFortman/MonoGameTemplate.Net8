using System.Collections.Generic;

namespace MonoGameTemplate.Entities
{
    public class EntityManager
    {
        private readonly Dictionary<int, Entity> _entities = new();
        private int _nextId = 1;

        public Entity CreateEntity()
        {
            var entity = new Entity(_nextId++);
            _entities[entity.Id] = entity;
            return entity;
        }

        public IEnumerable<Entity> GetAll() => _entities.Values;
    }
}
