using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InTheBodyOfADemon.NPCs
{
    class NPCCreateor
    {
        public static First CreateFirst(Rectangle rectangle)
        {
            First First = new First(rectangle);
            return First;
        }
    }
}
