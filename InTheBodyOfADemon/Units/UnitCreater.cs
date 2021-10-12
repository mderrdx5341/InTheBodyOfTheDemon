using InTheBodyOfADemon.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InTheBodyOfADemon.Units
{
    class UnitCreater
    {
        public static Unit Create(Texture2D texture, GameMap map)
        {
            Dictionary<UnitState, IUnitSprite> sprites = new Dictionary<UnitState, IUnitSprite>();

            sprites.Add(UnitState.IDLE, new UnitSprite(6)
            {
                TopOffset = 0,
                Width = 130,
                Height = 115,
                AmountFrames = 5,
                Texture = texture
            });

            sprites.Add(UnitState.DOWN, new UnitSprite(10)
            {
                TopOffset = 0,
                Width = 130,
                Height = 115,
                AmountFrames = 5,
                Texture = texture
            });

            sprites.Add(UnitState.RUN, new UnitSprite(10)
            {
                TopOffset = 110,
                Width = 130,
                Height = 110,
                AmountFrames = 5,
                Texture = texture
            });
            sprites.Add(UnitState.ATTACK, new UnitSprite(14)
            {
                TopOffset = 337,
                Width = 130,
                Height = 105,
                AmountFrames = 5,
                Texture = texture
            });

            Unit unit = new Unit(
                new Rectangle(1820, 600, 60, 78),
                sprites
            );
            unit.AddCollisionObject(map.GetBlocks().ConvertAll(b => (ICollisioning)b));
            return unit;
        }
    }
}
