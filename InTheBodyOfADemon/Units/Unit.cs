using InTheBodyOfADemon.Magicks;
using InTheBodyOfADemon.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace InTheBodyOfADemon.Units
{
    class Unit
    {
        public Vector2 Position;
        Dictionary<UnitState, IUnitSprite> _sprites;
        private UnitState _state;
        private Route _route;
        private Rectangle _rPosition;
        private UnitMoverPosition _unitMoverPosition;
        private UnitCollision _unitCollision;
        private Queue<Bullet> _createdObject = new Queue<Bullet>();
        public Rectangle RPosition
        {
            get
            {
                return _rPosition;
            }
            set
            {
                _rPosition = value;
            }
        }

        private float _amountAttackSecond = 0;
        private float _attackSeconds = 0.3f;

        private float _amountJumpSecond = 0;
        private float _jumpSeconds = 0.3f;

        private float _amountMagickSecond = 0;
        private float _magickSeconds = 0.2f;

        public StatusUpDown Status { get; set; }

        public Unit(Vector2 position, Dictionary<UnitState, IUnitSprite> sprites)
        {
            _state = UnitState.IDLE;
            Status = StatusUpDown.NONE;
            _route = Route.RIGHT;
            Position = position;
            _sprites = sprites;
            PositionRect();
            _unitCollision = new UnitCollision();
            _unitMoverPosition = new UnitMoverPosition(_rPosition, _unitCollision);
        }

        public void AddCollisionObject(List<IBox> boxs)
        {
            _unitCollision.AddCollisionObject(boxs);
        }

        public Queue<Bullet> GetCreatedObject()
        {
            return _createdObject;
        }
        private void PositionRect()
        {
            RPosition = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                60,
                82
            );
        }
        public void Update(GameTime gameTime)
        {
            bool downCollision = _unitCollision.isCollisionDown(RPosition);
            if (Status != StatusUpDown.UP && !downCollision)
            {
                Status = StatusUpDown.DOWN;
                RPosition = _unitMoverPosition.Down(gameTime);
            }
            else if (Status != StatusUpDown.UP && downCollision)
            {
                Status = StatusUpDown.NONE;
            }

            if (Status == StatusUpDown.UP && _amountJumpSecond < _jumpSeconds)
            {
                _amountJumpSecond += (float)gameTime.ElapsedGameTime.TotalSeconds;
                RPosition = _unitMoverPosition.Up(gameTime);
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
            //Debug.AddRectC(RPosition, Color.Red);
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
                Position.X += 40;
            }
            _state = UnitState.RUN;
            _route = Route.RIGHT;
            RPosition = _unitMoverPosition.Right(gameTime);
        }
        public void MoveLeft(GameTime gameTime)
        {
            if (_route == Route.RIGHT)
            {
                Position.X -= 40;
            }
            _state = UnitState.RUN;
            _route = Route.LEFT;
            RPosition = _unitMoverPosition.Left(gameTime);
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
                    new Rectangle(RPosition.X + 20, RPosition.Y + 30, 10, 10),
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
                Position.Y += Y;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (_route == Route.RIGHT)
            {
                _sprites[_state].IsFlipHorizontally = false;
                _sprites[_state].Draw(spriteBatch, (int)RPosition.X - 15, (int)RPosition.Y - 24);
            }

            if (_route == Route.LEFT)
            {
                _sprites[_state].IsFlipHorizontally = true;
                _sprites[_state].Draw(spriteBatch, (int)RPosition.X - 55, (int)RPosition.Y - 24);

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
