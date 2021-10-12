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
        public bool IsDebug { get; set; }
        public Box(Texture2D texture, Rectangle rect)
        {
            IsDebug = false;
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
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(Texture, Position, SpriteFromTexture(), Color.White);
            if (IsDebug)
            {
                string message = $"{Position.X} - {Position.X + Position.Width}";
                Vector2 _stringOrigin = font.MeasureString(message) / 2;
                spriteBatch.DrawString(font, message, new Vector2(Position.X, Position.Y), Color.Red, 0, _stringOrigin, 1.0f, SpriteEffects.None, 0.5f);
            }
        }
    }
}
