using InTheBodyOfADemon.Magicks;
using InTheBodyOfADemon.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace InTheBodyOfADemon.Units
{
    class Unit : IUnit, ICollisioning
    {
        Dictionary<UnitState, IUnitSprite> _sprites;
        private UnitState _state;
        private Route _route;
        private Rectangle _position;
        private UnitMoverPosition _unitMoverPosition;
        private UnitCollision _unitCollision;
        private Queue<Bullet> _createdObject = new Queue<Bullet>();
        public Rectangle Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        private float _amountAttackSecond = 0;
        private float _attackSeconds = 0.3f;

        private float _amountJumpSecond = 0;
        private float _jumpSeconds = 0.3f;

        private float _amountMagickSecond = 0;
        private float _magickSeconds = 0.2f;

        public StatusUpDown Status { get; set; }

        public Unit(Rectangle position, Dictionary<UnitState, IUnitSprite> sprites)
        {
            _state = UnitState.IDLE;
            Status = StatusUpDown.NONE;
            _route = Route.RIGHT;
            Position = position;
            _sprites = sprites;
            _unitCollision = new UnitCollision();
            _unitMoverPosition = new UnitMoverPosition(_position, _unitCollision);
        }

        public void AddCollisionObject(List<ICollisioning> boxs)
        {
            _unitCollision.AddCollisionObject(boxs);
        }

        public Queue<Bullet> GetCreatedObject()
        {
            return _createdObject;
        }
        public void Update(GameTime gameTime)
        {
            bool downCollision = _unitCollision.isCollisionDown(_position);
            if (Status != StatusUpDown.UP && !downCollision)
            {
                Status = StatusUpDown.DOWN;
                _position = _unitMoverPosition.Down(gameTime);
            }
            else if (Status != StatusUpDown.UP && downCollision)
            {
                Status = StatusUpDown.NONE;
            }

            if (Status == StatusUpDown.UP && _amountJumpSecond < _jumpSeconds)
            {
                _amountJumpSecond += (float)gameTime.ElapsedGameTime.TotalSeconds;
                _position = _unitMoverPosition.Up(gameTime);
            }
            else if (Status == StatusUpDown.UP)
            {
                Status = StatusUpDown.NONE;
                _amountJumpSecond = 0;
            }

            if (_state == UnitState.ATTACK && _amountAttackSecond < _attackSeconds)
            {
                _amountAttackSecond += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (_state == UnitState.ATTACK && _amountAttackSecond > _attackSeconds)
            {
                _amountAttackSecond = 0;
                _state = UnitState.IDLE;
            }
            _sprites[_state].Update(gameTime);
        }
        public void Stop(GameTime gameTime)
        {
            if (_state != UnitState.ATTACK && _state != UnitState.JUMP)
            {
                _state = UnitState.IDLE;
            }
        }
        public void MoveRight(GameTime gameTime)
        {
            if (_route == Route.LEFT)
            {
                _position.X += 40;
            }
            _state = UnitState.RUN;
            _route = Route.RIGHT;
            _position = _unitMoverPosition.Right(gameTime);
        }
        public void MoveLeft(GameTime gameTime)
        {
            if (_route == Route.RIGHT)
            {
                _position.X -= 40;
            }
            _state = UnitState.RUN;
            _route = Route.LEFT;
            _position = _unitMoverPosition.Left(gameTime);
        }
        public void Attack(GameTime gameTime)
        {
            if (_state != UnitState.ATTACK)
            {
                _state = UnitState.ATTACK;
                _amountAttackSecond += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
        public void Magick(GameTime gameTime)
        {
            if (_amountMagickSecond == 0)
            {
                _amountAttackSecond += (float)gameTime.ElapsedGameTime.TotalSeconds;
                Bullet bullet = new Bullet(
                    new Rectangle(Position.X + 20, Position.Y + 30, 10, 10),
                    _route
                );
                _createdObject.Enqueue(bullet);
            }
            else if (_amountMagickSecond < _jumpSeconds)
            {
                _amountAttackSecond += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (_amountMagickSecond > _jumpSeconds)
            {
                _amountAttackSecond = 0;
            }
        }
        public void Jump(GameTime gameTime)
        {
            if (Status == StatusUpDown.NONE)
            {
                Status = StatusUpDown.UP;
                _amountJumpSecond += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void Down(GameTime gameTime)
        {
            if (Status != StatusUpDown.UP)
            {
                Status = StatusUpDown.DOWN;
                int Y = 3 * gameTime.ElapsedGameTime.Milliseconds / 10;
                _position.Y += Y;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice gd)
        {
            if (_route == Route.RIGHT)
            {
                _sprites[_state].IsFlipHorizontally = false;
                _sprites[_state].Draw(spriteBatch, (int)Position.X - 15, (int)Position.Y - 24);
            }

            if (_route == Route.LEFT)
            {
                _sprites[_state].IsFlipHorizontally = true;
                _sprites[_state].Draw(spriteBatch, (int)Position.X - 55, (int)Position.Y - 24);

            }

            if (_state == UnitState.ATTACK)
            {
                Texture2D pixel = new Texture2D(gd, 1, 1);
                pixel.SetData(new[] { Color.White });
                Rectangle weapon = new Rectangle(Position.X + 20, Position.Y + 50, 100, 10);
                spriteBatch.Draw(pixel, weapon, Color.Red);
            }
        }
    }

    public enum UnitState
    {
        IDLE,
        RUN,
        ATTACK,
        DOWN,
        JUMP
    }
    public enum StatusUpDown
    {
        NONE,
        UP,
        DOWN,
    }
    public enum Route
    {
        LEFT,
        RIGHT
    }
}
