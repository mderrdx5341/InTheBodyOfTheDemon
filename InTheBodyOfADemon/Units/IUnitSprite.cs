using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace InTheBodyOfADemon.Units
{
    public interface IUnitSprite
    {
        public int TopOffset { get; set; }
        public int LeftOffset { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int AmountFrames { get; set; }
        public Texture2D Texture { get; set; }
        public bool IsFlipHorizontally { get; set; }
        public int SpeedAnimation { get; set; }
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch _spriteBatch, int x, int y);
    }
}
