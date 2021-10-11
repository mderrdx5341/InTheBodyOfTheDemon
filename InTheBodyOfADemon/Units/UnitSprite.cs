using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace InTheBodyOfADemon.Units
{
    public class UnitSprite : IUnitSprite
    {
        private float _timeForFrame = 0;//Сколько времени нужно показывать один фрейм (скорость)
        private float _totalTime;//сколько времени прошло с показа предыдущего фрейма 
        public int TopOffset { get ; set; }
        public int LeftOffset { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int AmountFrames { get; set; }
        public bool IsFlipHorizontally { get; set; }
        public Texture2D Texture { get; set; }
        public int currentFrame { get; set; }
        public int SpeedAnimation { get; set; }
        public UnitSprite(int speadAnimation)
        {
            SpeedAnimation = speadAnimation;
            _timeForFrame = (float)1 / SpeedAnimation;
        }


        public void Update(GameTime gameTime)
        {

            _totalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_totalTime > _timeForFrame)
            {
                currentFrame++;
                currentFrame = currentFrame % (AmountFrames - 1);
                _totalTime -= _timeForFrame;
            }
        }
        public void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            //Прямоугольник
            Rectangle rectanglеPosition = new Rectangle(
                x,
                y,
                Width,
                Height
            );
            //Изоражение
            Rectangle rectanglеFrame = new Rectangle(
                //Смещение в файле
                Width * currentFrame, TopOffset,
                //Ширина и высота
                Width, Height
            );

            if (IsFlipHorizontally)
            {
                SpriteEffects effect = new SpriteEffects();
                effect = SpriteEffects.FlipHorizontally;
                spriteBatch.Draw(Texture, rectanglеPosition, rectanglеFrame, Color.White, 0, Vector2.Zero, effect, 0);
            }
            else
            {
                spriteBatch.Draw(Texture, rectanglеPosition, rectanglеFrame, Color.White);
            }
        }
    }
}
