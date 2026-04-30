public class Game1 : Core
{
    private EntityManager _entities;
    private SystemManager _systems;

    public Game1() : base("MonoGameTemplate.ECS", 1280, 720, false)
    {
    }

    protected override void Initialize()
    {
        base.Initialize();

        _entities = new EntityManager();
        _systems = new SystemManager();

        _systems.Add(new MovementSystem(_entities));
        _systems.Add(new VelocitySystem(_entities));
        _systems.Add(new CollisionSystem(_entities));
        _systems.Add(new RenderSystem(_entities));
    }

    protected override void LoadContent()
    {
        // 1. Load shared assets
        var atlas = TextureAtlas.FromFile(Content, "atlas-definition.xml");

        var slimeSprite = atlas.CreateAnimatedSprite("slime-animation");
        slimeSprite.Scale = new Vector2(4f);

        var batSprite = atlas.CreateAnimatedSprite("bat-animation");
        batSprite.Scale = new Vector2(4f);

        // 2. Create player entity
        var player = _entities.CreateEntity();
        player.Add(new PositionComponent(new Vector2(300, 300)));
        player.Add(new SpriteComponent(slimeSprite));
        player.Add(new PlayerTag());

        // 3. Create bat entity
        var bat = _entities.CreateEntity();
        bat.Add(new PositionComponent(new Vector2(100, 100)));
        bat.Add(new VelocityComponent(new Vector2(3, 2)));
        bat.Add(new SpriteComponent(batSprite));
        bat.Add(new BatTag());
    }
    protected override void Update(GameTime gameTime)
    {
        _systems.Update(Core.Context, gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _systems.Draw(Core.Context, gameTime);
        base.Draw(gameTime);
    }
}