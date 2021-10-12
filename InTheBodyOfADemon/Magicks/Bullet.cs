using InTheBodyOfADemon.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InTheBodyOfADemon.Magicks
{
    class Bullet : IDrawing
    {
        private Rectangle _position;
        private Route _route;
        public Bullet(Rectangle position, Route route)
        {
            _position = position;
            _route = route;
        }
        public void Update(GameTime gameTime)
        {
            if (_route == Route.RIGHT)
            {
                _position.X += 4 * gameTime.ElapsedGameTime.Milliseconds / 10;
            } else
            {
                _position.X -= 4 * gameTime.ElapsedGameTime.Milliseconds / 10;
            }
        }
        public void Draw(SpriteBatch spriteBatch, GraphicsDevice gd)
        {
            Texture2D pixel = new Texture2D(gd, 1, 1);
            pixel.SetData(new[] { Color.White });
            spriteBatch.Draw(pixel, _position, Color.Red);
        }
    }
}
