using MonoGameLibrary;
using MonoGameLibrary.Scenes;
using MonoGameTemplate.ECS.Game.Bootstrap;

namespace MonoGameTemplate.ECS.Game.Scenes;

public class SceneCompositionRoot
{
    private readonly SceneRegistry _registry = new();
    private readonly SceneFactory _factory;

    public SceneCompositionRoot(SceneFactory factory)
    {
        _factory = factory;
        RegisterScenes();
    }

    private void RegisterScenes()
    {
        _registry.Register(ToKey(SceneType.World), _factory.CreateGameScene);
        //        _registry.Register(ToKey(SceneType.Menu), _factory.CreateMenuScene);
        //      _registry.Register(ToKey(SceneType.Debug), _factory.CreateDebugScene);
    }

    private static SceneKey ToKey(SceneType type)
        => new SceneKey(type);

    public IScene Create(SceneType type, GameContext context)
        => _registry.Create(ToKey(type), context);
}