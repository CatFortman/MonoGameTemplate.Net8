using System;
using System.Collections.Generic;

namespace MonoGameTemplate.ECS
{
    public class EntityManager
    {
        private readonly List<Entity> _entities = new();

        public Entity CreateEntity()
        {
            var entity = new Entity();
            _entities.Add(entity);
            return entity;
        }

        public IEnumerable<Entity> GetAll() => _entities;

        internal void Clear()
        {
            throw new NotImplementedException();
        }
    }
}