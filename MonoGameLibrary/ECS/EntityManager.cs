using System;
using System.Collections.Generic;
using MonoGameLibrary.ECS.Interfaces;

namespace MonoGameLibrary.ECS;

public class EntityManager
{
    private int _nextId = 0;

    private readonly Dictionary<int, Entity> _entities = new();
    private readonly Dictionary<Type, IComponentStore> _stores = new();

    // -------------------------
    // ENTITY LIFECYCLE
    // -------------------------

    public Entity CreateEntity()
    {
        int id = _nextId++;
        var entity = new Entity(id, this);

        _entities[id] = entity;

        return entity;
    }

    public void DestroyEntity(int entityId)
    {
        _entities.Remove(entityId);

        foreach (var store in _stores.Values)
        {
            store.Remove(entityId);
        }
    }

    // -------------------------
    // COMPONENT API
    // -------------------------

    public void AddComponent<T>(int entityId, T component)
    {
        GetOrCreateStore<T>().Set(entityId, component);
    }

    public bool HasComponent<T>(int entityId)
    {
        return _stores.TryGetValue(typeof(T), out var store) &&
               ((ComponentStore<T>)store).Has(entityId);
    }

    public T Get<T>(int entityId)
    {
        return GetStore<T>().Get(entityId);
    }

    public ref T GetRef<T>(int entityId)
    {
        return ref GetStore<T>().GetRef(entityId);
    }

    public void RemoveComponent<T>(int entityId)
    {
        if (_stores.TryGetValue(typeof(T), out var store))
        {
            ((ComponentStore<T>)store).Remove(entityId);
        }
    }

    // -------------------------
    // QUERY API (GLOBAL)
    // -------------------------

    public IEnumerable<Entity> GetAll()
    {
        return _entities.Values;
    }

    public IEnumerable<Entity> With<T1>()
    {
        foreach (var entity in _entities.Values)
        {
            if (HasComponent<T1>(entity.Id))
                yield return entity;
        }
    }

    public IEnumerable<Entity> With<T1, T2>()
    {
        foreach (var entity in _entities.Values)
        {
            if (HasComponent<T1>(entity.Id) &&
                HasComponent<T2>(entity.Id))
                yield return entity;
        }
    }

    public IEnumerable<Entity> With<T1, T2, T3>()
    {
        foreach (var entity in _entities.Values)
        {
            if (HasComponent<T1>(entity.Id) &&
                HasComponent<T2>(entity.Id) &&
                HasComponent<T3>(entity.Id))
                yield return entity;
        }
    }

    public IEnumerable<Entity> With<T1, T2, T3, T4>()
    {
        foreach (var entity in _entities.Values)
        {
            if (entity.Has<T1>() &&
                entity.Has<T2>() && 
                entity.Has<T3>() &&
                entity.Has<T4>())
                yield return entity;
        }
    }

    public IEnumerable<Entity> Query<T1>()
    {
        foreach (var entity in _entities.Values)
        {
            if (entity.Has<T1>())
                yield return entity;
        }
    }

    public IEnumerable<Entity> Query<T1, T2>()
    {
        foreach (var entity in _entities.Values)
        {
            if (entity.Has<T1>() &&
                entity.Has<T2>())
            {
                yield return entity;
            }
        }
    }

    public IEnumerable<Entity> Query<T1, T2, T3>()
    {
        foreach (var entity in _entities.Values)
        {
            if (entity.Has<T1>() &&
                entity.Has<T2>() &&
                entity.Has<T3>())
            {
                yield return entity;
            }
        }
    }

    public IEnumerable<Entity> Query<T1, T2, T3, T4>()
    {
        foreach (var entity in _entities.Values)
        {
            if (entity.Has<T1>() &&
                entity.Has<T2>() &&
                entity.Has<T3>() &&
                entity.Has<T4>())
            {
                yield return entity;
            }
        }
    }

    // -------------------------
    // INTERNAL STORAGE
    // -------------------------

    private ComponentStore<T> GetStore<T>()
    {
        return (ComponentStore<T>)_stores[typeof(T)];
    }

    private ComponentStore<T> GetOrCreateStore<T>()
    {
        if (!_stores.TryGetValue(typeof(T), out var store))
        {
            store = new ComponentStore<T>();
            _stores[typeof(T)] = store;
        }

        return (ComponentStore<T>)store;
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }
}