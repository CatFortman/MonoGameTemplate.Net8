namespace MonoGameTemplate.ECS
{
    public class Entity
    {
        private readonly Dictionary<Type, object> _components = new();

        public void Add<T>(T component)
        {
            _components[typeof(T)] = component!;
        }

        public T Get<T>()
        {
            return (T)_components[typeof(T)];
        }

        public bool Has<T>()
        {
            return _components.ContainsKey(typeof(T));
        }
    }
}