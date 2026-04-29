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

        RegisterSystems();
        CreateEntities();
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