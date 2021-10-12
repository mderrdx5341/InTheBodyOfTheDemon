using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace InTheBodyOfADemon.Maps
{
    class GameMap
    {
        protected SpriteFont _font;
        protected int[,] _map;
        protected Texture2D _texture;
        protected List<IBox> boxs = new List<IBox>();
        protected int _width = 0;
        protected int _height = 0;
        public GameMap(Texture2D texture, SpriteFont font)
        {
            _font = font;
            _texture = texture;
            _map = new int[,]
                {{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,1,1,1,1,1},
                {3,0,0,0,4,0,0,0,0,0,2,2,2,2,2,2,2,0,0,3,0,0,0,0,3,3,3,3,3,3},
                {3,0,0,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,3,3,3,3,3,3},
                {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,3},
                {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,3},
                {3,0,0,0,0,0,0,0,0,0,2,2,2,2,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,3},
                {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,0,0,0,0,0,0,0,0,0,3},
                {3,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,3,3,2,2,0,0,0,0,0,0,0,3},
                {3,0,0,0,0,2,2,3,0,0,0,0,0,0,0,1,0,0,0,3,0,0,0,0,0,0,0,0,0,3},
                {3,0,0,1,0,0,0,0,0,0,0,0,0,0,1,3,0,0,0,0,0,0,4,1,1,1,1,1,1,3},
                {3,1,1,3,1,1,1,1,1,1,1,1,1,1,1,3,1,1,1,1,1,1,1,3,3,3,3,3,3,3}};

            Initialize();
        }
        public List<IBox> GetBlocks()
        {
            return boxs;
        }
        public void Initialize()
        {
            int x = 0;
            int y = 0;

            _width = _map.GetLength(1) * 78;
            _height = _map.GetLength(0) * 78;

            for (int i = 0; i < _map.GetLength(0); i++)
            {
                for (int j = 0; j < _map.GetLength(1); j++)
                {
                    Rectangle rect = new Rectangle(x, y, 78, 78);
                    int a = _map[i, j];

                    if (a == 1)
                    {
                        boxs.Add(new Box(_texture, rect));
                    }
                    else if (a == 2)
                    {
                        boxs.Add(new Box2(_texture, rect));
                    }
                    else if (a == 3)
                    {
                        boxs.Add(new Box3(_texture, rect));
                    }
                    else if (a == 4)
                    {
                        rect.Y += 52;
                        rect.Width = 70;
                        rect.Height = 26;
                        boxs.Add(new Box4(_texture, rect));
                    }
                    x += 78;
                }
                x = 0;
                y += 78;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice gd)
        {
/*            Texture2D pixel = new Texture2D(gd, 1, 1);
            pixel.SetData(new[] { Color.White });

            spriteBatch.Draw(pixel, new Rectangle(0,0,_width, _height), Color.LightGreen);*/

            foreach (IBox box in boxs)
            {
                box.Draw(spriteBatch, _font);
            }
        }
    }
}
