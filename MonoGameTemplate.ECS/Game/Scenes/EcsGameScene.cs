using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.ECS;
using MonoGameLibrary.ECS.Systems;
using MonoGameLibrary.Graphics;
using MonoGameTemplate.ECS.Scenes;

namespace MonoGameTemplate.ECS.Game.Scenes;

public class EcsGameScene : ICollisionEventScene
{
    private readonly EntityManager _entities;
    private readonly SystemManager _systems;

    private readonly Rectangle _worldBounds;
    private readonly Tilemap _tilemap;
    private readonly SpriteFont _font;

    public EntityManager Entities => _entities;

    public Rectangle WorldBounds => _worldBounds;
    public Tilemap Tilemap => _tilemap;
    public SpriteFont Font => _font;

    private readonly List<(Entity A, Entity B)> _collisionEvents = new();
    public List<(Entity A, Entity B)> CollisionEvents => _collisionEvents;

    public EcsGameScene(
        EntityManager entities,
        SystemManager systems,
        Rectangle worldBounds,
        Tilemap tilemap,
        SpriteFont font)
    {
        _entities = entities;
        _systems = systems;
        _worldBounds = worldBounds;
        _tilemap = tilemap;
        _font = font;
    }

    public void Load(GameContext context) { }

    public void Update(GameContext context, GameTime gameTime)
    {
        _collisionEvents.Clear();
        _systems.Update(context, gameTime, this);
    }

    public void Draw(GameContext context, GameTime gameTime)
    {
        context.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        _tilemap.Draw(context.SpriteBatch);

        _systems.Draw(context, gameTime, this);

        context.SpriteBatch.End();
    }

    public void OnEnter() { }
    public void OnExit() { }
    public void Unload() { }
}