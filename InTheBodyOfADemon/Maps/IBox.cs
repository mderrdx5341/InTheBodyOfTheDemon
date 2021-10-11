using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace InTheBodyOfADemon.Maps
{
    interface IBox
    {
        public Rectangle Position { get; set; }
        public void Draw(SpriteBatch spriteBatch);

    }
}
