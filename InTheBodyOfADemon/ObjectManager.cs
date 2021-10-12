using InTheBodyOfADemon.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InTheBodyOfADemon
{
    class ObjectManager
    {
        private static List<IDrawing> _drawingObjects = new List<IDrawing>();
        private static List<IUnit> _NPC = new List<IUnit>();
        public static void AddDrawingObject(IDrawing obj)
        {
            _drawingObjects.Add(obj);
        }
        public static void AddNPC(IUnit obj)
        {
            _NPC.Add(obj);
        }
        
        public static void UpdateNPC(GameTime gameTime)
        {
            foreach (IUnit u in _NPC)
            {
                u.Update(gameTime);
            }
        }
        public static void DrawNPC(SpriteBatch spriteBatch, GraphicsDevice gd)
        {
            foreach (IUnit u in _NPC)
            {
                u.Draw(spriteBatch, gd);
            }
        }
    }
}
