using InTheBodyOfADemon.Magicks;
using InTheBodyOfADemon.Maps;
using InTheBodyOfADemon.NPCs;
using InTheBodyOfADemon.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace InTheBodyOfADemon
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<IDrawing> _drawingObject = new List<IDrawing>();

        private Unit _player;
        private GameMap _map;
        private UnitControll _unitControll;

        private Door _door;
        private UnitCollision _doorColl;

        private Camera _camera;
        private float _cameraX;
        private float _cameraY;
        private float _cameraSpeed;

        private SpriteFont _font;
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

            _font = Content.Load<SpriteFont>("Font");
            Debug.Load(Content.Load<SpriteFont>("Font"));

            _map = new GameMap(Content.Load<Texture2D>("map"), _font);


            _player = UnitCreater.Create(Content.Load<Texture2D>("knight"), _map);
            _unitControll = new UnitControll(
                _player
            );

            _door = new Door();
            _door.Load(Content.Load<Texture2D>("door"));

            _doorColl = new UnitCollision();
            _doorColl.AddCollisionObject(new List<ICollisioning>() { _player });

            First npc = NPCCreateor.CreateFirst(new Rectangle(800, 500, 60, 80));
            npc.AddCollisionObject(_map.GetBlocks().ConvertAll(b => (ICollisioning) b));

            First npc2 = NPCCreateor.CreateFirst(new Rectangle(1300, 500,60, 80));
            npc2.AddCollisionObject(_map.GetBlocks().ConvertAll(b => (ICollisioning) b));

            ObjectManager.AddNPC(npc);
            ObjectManager.AddNPC(npc2);

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

            ///Door
/*            if (kstate.IsKeyDown(Keys.Q))
            {
                _door.StateToggle(gameTime);
            }
            _door.Update(gameTime);*/
            if(UnitCollision.Collision(_door.Position, _player.Position))
            {
                _door.Animation(gameTime);
            } else
            {
                _door.ReverAnimation(gameTime);
            }
            //_door.Update(gameTime);

            ObjectManager.UpdateNPC(gameTime);

            ///Player
            _unitControll.Update(
                Keyboard.GetState(),
                GamePad.GetState(PlayerIndex.One),
                gameTime
            );

            /**/
            Queue<Bullet> magicksObjects = _player.GetCreatedObject();
            int co = magicksObjects.Count;

            for (int i = 0; i < co; i++)
            {
                Bullet b;
                magicksObjects.TryDequeue(out b);
                _drawingObject.Add(b);
            }

            foreach (IDrawing ob in _drawingObject)
            {
                ob.Update(gameTime);
            }
            /**/


            _cameraX = _player.Position.X;
            _cameraY = _player.Position.Y;
            _camera.Update(new Vector2(_cameraX, _cameraY));
            //camera.Update(new Vector2(player.Position.X, player.Position.Y));

            Debug.Position = new Vector2(
                _cameraX,
                _cameraY
            );

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

            _map.Draw(_spriteBatch, GraphicsDevice);

            _door.Draw(_spriteBatch, GraphicsDevice, _font);

            ObjectManager.DrawNPC(_spriteBatch, GraphicsDevice);

            _player.Draw(_spriteBatch, GraphicsDevice);

            foreach (IDrawing ob in _drawingObject)
            {
                ob.Draw(_spriteBatch, GraphicsDevice);
            }

            Debug.Draw(_spriteBatch, GraphicsDevice);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
