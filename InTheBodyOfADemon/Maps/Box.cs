using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace InTheBodyOfADemon.Maps
{
    class Box : IBox
    {
        Texture2D Texture;
        public Rectangle Position { get; set; }
        public Box(Texture2D texture, Rectangle rect)
        {
            this.Texture = texture;
            this.Position = rect;
        }
        public virtual Rectangle SpriteFromTexture()
        {
            return new Rectangle(
                //Смещение в файле
                234, 18,
                //Ширина и высота
                77, 77
            );
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, SpriteFromTexture(), Color.White);
        }
    }
}
