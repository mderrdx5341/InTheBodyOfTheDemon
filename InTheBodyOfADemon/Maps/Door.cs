using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InTheBodyOfADemon.Maps
{
    class Door
    {
        private float _timeForFrame = 0;//Сколько времени нужно показывать один фрейм (скорость)
        private float _totalTime;//сколько времени прошло с показа предыдущего фрейма
        private int _currentFrame;
        public int SpeedAnimation { get; set; }
        public int AmountFrames { get; set; }
        private Texture2D _texture;
        private Rectangle _position;
        private BoxDoorState _state;
        delegate void ActionDoor(GameTime gameTime);
        private ActionDoor _toggle;
        public Door()
        {
            _position = new Rectangle(1970, 366, 267, 335);
            SpeedAnimation = 10;
            AmountFrames = 6;
            _timeForFrame = (float)1 / SpeedAnimation;
            _state = BoxDoorState.CLOSE;
        }

        public Rectangle Position => _position;

        public void Load(Texture2D texture)
        {
            _texture = texture;
        }


        public void OpenDoor()
        {
            _state = BoxDoorState.OPEN;
        }
        public void CloseDoor()
        {
            _state = BoxDoorState.CLOSE;
        }
        public void Draw(SpriteBatch spriteBatch, GraphicsDevice gd, SpriteFont font)
        {
            Rectangle rectanglеFrame = new Rectangle(
                //Смещение в файле
                Position.Width * _currentFrame + 1, 0,
                //Ширина и высота
                267, 335
            );
            spriteBatch.Draw(_texture, _position, rectanglеFrame, Color.White);
        }

        public void View(GameTime gameTime)
        {
            //spriteBatch.Draw(_texture, _position, rectanglеFrame, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (_state == BoxDoorState.TOGGLE)
            {
                _toggle(gameTime);
            }
        }
        public void StateToggle(GameTime gameTime)
        {
            if (_state != BoxDoorState.TOGGLE)
            {               
                if (_state == BoxDoorState.CLOSE)
                {               
                    _toggle = Animation;
                }
                else
                {
                    _toggle = ReverAnimation;
                }
                _state = BoxDoorState.TOGGLE;
            }
        }
        public void Animation(GameTime gameTime)
        {
            _totalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_totalTime > _timeForFrame)
            {
                if (_currentFrame < (AmountFrames - 1))
                {
                    _currentFrame++;
                    _totalTime -= _timeForFrame;
                } else
                {
                    _totalTime = 0;
                    _state = BoxDoorState.OPEN;
                }
            }
        }
        public void ReverAnimation(GameTime gameTime)
        {
            _totalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_totalTime > _timeForFrame)
            {
                if (_currentFrame > 0)
                {
                    _currentFrame--;
                    _totalTime -= _timeForFrame;
                } else
                {
                    _totalTime = 0;
                    _state = BoxDoorState.CLOSE;
                }
            }

        }
    }

    enum BoxDoorState
    {
        CLOSE,
        OPEN,
        TOGGLE
    }
}
