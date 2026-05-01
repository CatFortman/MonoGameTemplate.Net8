namespace MonoGameLibrary.ECS;
public readonly struct Entity
{
    public int Id { get; }
    private readonly EntityManager _manager;

    internal Entity(int id, EntityManager manager)
    {
        Id = id;
        _manager = manager;
    }

    public void Add<T>(T component) => _manager.AddComponent(Id, component);
    public T Get<T>() => _manager.Get<T>(Id);
    public ref T GetRef<T>() => ref _manager.GetRef<T>(Id);
    public bool Has<T>() => _manager.HasComponent<T>(Id);
}