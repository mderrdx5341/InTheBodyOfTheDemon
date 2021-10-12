using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace InTheBodyOfADemon
{
    class Debug
    {
        private static List<string> _messages = new List<string>();
        private static List<Tuple<Rectangle, Color>> _rectangles = new List<Tuple<Rectangle, Color>>();
        private static List<Tuple<Rectangle, Color>> _rectanglesC = new List<Tuple<Rectangle, Color>>();

        public static void AddRectC(Rectangle rect, Color color)
        {

            Tuple<Rectangle, Color> t = new Tuple<Rectangle, Color>(rect, color);
            _rectanglesC.Add(t);
        }
        public static void AddRect(Rectangle rect, Color color)
        {

            Tuple<Rectangle, Color> t = new Tuple<Rectangle, Color>(rect, color);
            _rectangles.Add(t);
        }
        public static void AddText(string message)
        {
            _messages.Add(message);
        }

        private static SpriteFont _font;
        private static Vector2 _stringOrigin;
        private static Vector2 _position;
        public static Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }
        public static void Load(SpriteFont font)
        {
            _font = font;
        }
        public static void DrawText(SpriteBatch spriteBatch)
        {
            foreach (string message in _messages)
            {
                _stringOrigin = _font.MeasureString(message) / 2;
                spriteBatch.DrawString(_font, message, _position, Color.Red, 0, _stringOrigin, 1.0f, SpriteEffects.None, 0.5f);
                _position.Y -= 20;
            }
            _messages.Clear();
        }
        public static void DrawRectangles(SpriteBatch spriteBatch, GraphicsDevice gd)
        {
            Texture2D pixel = new Texture2D(gd, 1, 1);
            pixel.SetData(new[] { Color.White });

            foreach ((Rectangle rect, Color c) in _rectangles)
            {
                spriteBatch.Draw(pixel, rect, c);
            }
        }
        public static void DrawRectanglesC(SpriteBatch spriteBatch, GraphicsDevice gd)
        {
            Texture2D pixel = new Texture2D(gd, 1, 1);
            pixel.SetData(new[] { Color.White });

            foreach ((Rectangle rect, Color c) in _rectanglesC)
            {
                spriteBatch.Draw(pixel, rect, c);
            }

            _rectanglesC.Clear();
        }
        public static void Draw(SpriteBatch spriteBatch, GraphicsDevice gd)
        {
            DrawRectangles(spriteBatch, gd);
            DrawRectanglesC(spriteBatch, gd);
            DrawText(spriteBatch);
        }
    }
}
