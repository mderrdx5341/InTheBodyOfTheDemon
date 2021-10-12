using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace InTheBodyOfADemon.Maps
{
    interface IBox: ICollisioning
    {
        public void Draw(SpriteBatch spriteBatch, SpriteFont font);
    }
}
