using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Input;
using MonoGameLibrary.Scenes;

public class Core : Game
{
    public static Core Instance { get; private set; }
    public static GraphicsDeviceManager Graphics { get; private set; }
    public static GraphicsDevice Device { get; private set; }
    public static SpriteBatch SpriteBatch { get; private set; }
    public static ContentManager ContentManager { get; private set; }
    public static InputManager Input { get; private set; }
    public static GameContext Context { get; private set; }
    public static SceneManager SceneManager { get; private set; }

    public bool ExitOnEscape { get; set; }

    public Core(string title, int width, int height, bool fullScreen)
    {
        Instance = this;

        Graphics = new GraphicsDeviceManager(this);

        Graphics.PreferredBackBufferWidth = width;
        Graphics.PreferredBackBufferHeight = height;
        Graphics.IsFullScreen = fullScreen;
        Graphics.ApplyChanges();

        Window.Title = title;
        Content.RootDirectory = "Content";

        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        Device = GraphicsDevice;
        SpriteBatch = new SpriteBatch(Device);
        Input = new InputManager();

        ContentManager = Content;

        Context = new GameContext
        {
            GraphicsDevice = Device,
            SpriteBatch = SpriteBatch,
            Content = ContentManager,
            Input = Input
        };

        SceneManager = new SceneManager(Context);
    }

    protected override void Update(GameTime gameTime)
    {
        Input.Update(gameTime);

        if (ExitOnEscape && Input.Keyboard.IsKeyDown(Keys.Escape))
            Exit();

        SceneManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        Device.Clear(Color.MonoGameOrange);

        SceneManager.Draw(gameTime);

        base.Draw(gameTime);
    }
    
}