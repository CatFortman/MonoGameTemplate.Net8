using System;
using System.Collections.Generic;

namespace MonoGameLibrary.Scenes;

public class SceneRegistry
{
    private readonly Dictionary<SceneKey, Func<GameContext, IScene>> _registry = new();

    public void Register(SceneKey key, Func<GameContext, IScene> factory)
    {
        _registry[key] = factory;
    }

    public IScene Create(SceneKey key, GameContext context)
    {
        if (!_registry.TryGetValue(key, out var factory))
            throw new Exception($"Scene '{key}' not registered.");

        return factory(context);
    }
}