using InTheBodyOfADemon.Maps;
using InTheBodyOfADemon.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InTheBodyOfADemon
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Unit _player;
        private Map _map;

        private Camera _camera;
        private float _cameraX;
        private float _cameraY;
        private float _cameraSpeed;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _cameraX = 300;
            _cameraY = 300;
            _cameraSpeed = 100f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _map = new Map(Content.Load<Texture2D>("map"));
            _player = UnitCreater.Create(Content.Load<Texture2D>("knight"));
            _camera = new Camera(GraphicsDevice.Viewport);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //Camera
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.W))
                _camera.Zoom += 0.01f;
            else if (kstate.IsKeyDown(Keys.S))
                _camera.Zoom -= 0.01f;

            if (kstate.IsKeyDown(Keys.A))
                _camera.Rotation += 0.01f;
            else if (kstate.IsKeyDown(Keys.D))
                _camera.Rotation -= 0.01f;

            if (kstate.IsKeyDown(Keys.Up))
            {
                _cameraY -= _cameraSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (kstate.IsKeyDown(Keys.Down))
            {
                _cameraY += _cameraSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
 
            if (kstate.IsKeyDown(Keys.Left))
            {
                _cameraX -= _cameraSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (kstate.IsKeyDown(Keys.Right))
            {
                _cameraX += _cameraSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            ///Player
            if (kstate.IsKeyDown(Keys.Left))
            {
                _player.MoveLeft(gameTime);
            }
            else if (kstate.IsKeyDown(Keys.Right))
            {
                _player.MoveRight(gameTime);
            }
            else if (kstate.IsKeyDown(Keys.C))
            {
                _player.Attack(gameTime);
            }
            else
            {
                _player.Stop(gameTime);
            }

            _player.Update(gameTime);
            _camera.Update(new Vector2(_cameraX, _cameraY));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null, null, null, null,
                _camera.Transform
            );
            foreach (IBox box in _map.GetBlocks())
            {
                box.Draw(_spriteBatch);
            }
            _player.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
