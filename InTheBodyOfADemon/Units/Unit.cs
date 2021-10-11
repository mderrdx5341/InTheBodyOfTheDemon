using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace InTheBodyOfADemon.Units
{
    public class Unit
    {
        public Vector2 Position; 
        //public IUnitSprite _sprite;
        Dictionary<UnitState, IUnitSprite> _sprites;
        private UnitState _state;
        private Route _route;

        public Unit(Vector2 position, Dictionary<UnitState, IUnitSprite> sprites)
        {
            _state = UnitState.IDLE;
            _route = Route.RIGHT;
            Position = position;
            _sprites = sprites;
        }

        public void Update(GameTime gameTime)
        {
            _sprites[_state].Update(gameTime);
        }
        public void Stop(GameTime gameTime)
        {
            _state = UnitState.IDLE;
        }
        public void MoveRight(GameTime gameTime)
        {
            _state = UnitState.RUN;
            _route = Route.RIGHT;
            int X = 3 * gameTime.ElapsedGameTime.Milliseconds / 10;
            Position.X += X;
        }
        public void MoveLeft(GameTime gameTime)
        {
            _state = UnitState.RUN;
            _route = Route.LEFT;
            int X = 3 * gameTime.ElapsedGameTime.Milliseconds / 10;
            Position.X -=X;
        }
        public void Attack(GameTime gameTime)
        {
            _state = UnitState.ATTACK;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_route == Route.RIGHT)
            {
                _sprites[_state].IsFlipHorizontally = false;
                _sprites[_state].Draw(spriteBatch, (int)Position.X, (int)Position.Y);
            }

            if (_route == Route.LEFT)
            {
                _sprites[_state].IsFlipHorizontally = true;
                _sprites[_state].Draw(spriteBatch, (int)Position.X, (int)Position.Y);

            }
        }
    }

    public enum UnitState
    {
        IDLE,
        RUN,
        ATTACK
    }

    public enum Route
    {
        LEFT,
        RIGHT
    }
}
