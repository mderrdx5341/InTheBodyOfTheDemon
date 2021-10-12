using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace InTheBodyOfADemon.Units
{
    class UnitMoverPosition
    {
        private Rectangle _position;
        private UnitCollision _unitCollision;
        public UnitMoverPosition(Rectangle position, UnitCollision unitCollision)
        {
            _position = position;
            _unitCollision = unitCollision;
        }
        public Rectangle Up(GameTime gameTime)
        {
            if (!_unitCollision.isCollisionTop(_position))
            {
                int X = 4 * gameTime.ElapsedGameTime.Milliseconds / 10;
                _position.Y -= X;
            }
            return _position;
        }
        public Rectangle Down(GameTime gameTime)
        {
            int X = 4 * gameTime.ElapsedGameTime.Milliseconds / 10;
            _position.Y += X;
            return _position;
        }

        public Rectangle Right(GameTime gameTime)
        {
            if (!_unitCollision.isCollisionRight(_position))
            {
                int X = 3 * gameTime.ElapsedGameTime.Milliseconds / 10;
                _position.X += X;
            }
            return _position;
        }

        public Rectangle Left(GameTime gameTime)
        {
            if (!_unitCollision.isCollisionLeft(_position))
            {
                int X = 3 * gameTime.ElapsedGameTime.Milliseconds / 10;
                _position.X -= X;
            }
            return _position;
        }
    }
}
