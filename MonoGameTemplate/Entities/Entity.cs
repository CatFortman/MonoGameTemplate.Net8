using System;
using System.Collections.Generic;

namespace MonoGameTemplate.Entities
{
    public class Entity
    {
        public int Id { get; }
        private readonly Dictionary<Type, IComponent> _components = new();

        public Entity(int id)
        {
            Id = id;
        }

        public void AddComponent<T>(T component) where T : IComponent
        {
            _components[typeof(T)] = component;
        }

        public bool HasComponent<T>() where T : IComponent => _components.ContainsKey(typeof(T));

        public T GetComponent<T>() where T : IComponent => (T)_components[typeof(T)];
    }
}
