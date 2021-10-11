using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace InTheBodyOfADemon.Maps
{
    class Box3 : IBox
    {
        Texture2D Texture;
        public Rectangle Position { get; set; }
        public Box3(Texture2D texture, Rectangle rect)
        {
            this.Texture = texture;
            this.Position = rect;
        }
        public virtual Rectangle SpriteFromTexture()
        {
            return new Rectangle(
                //Смещение в файле
                234, 103,
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
