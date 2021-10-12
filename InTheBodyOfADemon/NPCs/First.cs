using InTheBodyOfADemon.Maps;
using InTheBodyOfADemon.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InTheBodyOfADemon.NPCs
{
    class First : IUnit
    {
        private UnitMoverPosition _unitMoverPosition;
        private UnitCollision _unitCollision;
        private Rectangle _position;
        private bool _moveLeft = true;
        private Rectangle Position { get { return _position; } }
        public First(Rectangle position)
        {
            _position = position;
            _unitCollision = new UnitCollision();
            _unitMoverPosition = new UnitMoverPosition(_position, _unitCollision);
        }
        public void AddCollisionObject(List<ICollisioning> boxs)
        {
            _unitCollision.AddCollisionObject(boxs);
        }
        public void Draw(SpriteBatch spriteBatch, GraphicsDevice gd)
        {
            Texture2D pixel = new Texture2D(gd, 1, 1);
            pixel.SetData(new[] { Color.White });
            spriteBatch.Draw(pixel, _position, Color.Red);
        }

        public void Update(GameTime gameTime)
        {
            bool downCollision = _unitCollision.isCollisionDown(_position);
            if (!downCollision)
            {
                _position = _unitMoverPosition.Down(gameTime);
            }
            else
            {
                if (_moveLeft)
                {
                    moveLeft(gameTime);
                    if (_unitCollision.isCollisionLeft(_position))
                    {
                        _moveLeft = false;
                    }
                } else
                {
                    moveRight(gameTime);
                    if (_unitCollision.isCollisionRight(_position))
                    {
                        _moveLeft = true;
                    }
                }
            }
        }
        private void moveLeft(GameTime gameTime)
        {
            _position = _unitMoverPosition.Left(gameTime);
        }
        private void moveRight(GameTime gameTime)
        {
            _position = _unitMoverPosition.Right(gameTime);
        }
    }
}
