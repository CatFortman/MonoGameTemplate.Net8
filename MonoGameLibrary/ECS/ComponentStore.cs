using System;
using MonoGameLibrary.ECS.Interfaces;

namespace MonoGameLibrary.ECS;
internal class ComponentStore<T> : IComponentStore
{
    private T[] _data = new T[1024];
    private bool[] _hasValue = new bool[1024];

    public void Set(int entityId, T value)
    {
        EnsureCapacity(entityId);
        _data[entityId] = value;
        _hasValue[entityId] = true;
    }

    public T Get(int entityId)
    {
        return _data[entityId];
    }

    public ref T GetRef(int entityId)
    {
        EnsureCapacity(entityId);
        return ref _data[entityId];
    }

    public bool Has(int entityId)
    {
        return entityId < _hasValue.Length && _hasValue[entityId];
    }

    public void Remove(int entityId)
    {
        if (entityId < _hasValue.Length)
        {
            _hasValue[entityId] = false;
            _data[entityId] = default;
        }
    }

    private void EnsureCapacity(int entityId)
    {
        if (entityId < _data.Length) return;

        int newSize = _data.Length * 2;

        while (newSize <= entityId)
            newSize *= 2;

        Array.Resize(ref _data, newSize);
        Array.Resize(ref _hasValue, newSize);
    }
}