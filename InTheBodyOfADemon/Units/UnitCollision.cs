using InTheBodyOfADemon.Maps;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace InTheBodyOfADemon.Units
{
    class UnitCollision
    {
        private List<IBox> _boxs;
        public UnitCollision()
        {
        }

        public void AddCollisionObject(List<IBox> boxs)
        {
            _boxs = boxs;
        }
        public bool isCollisionTop(Rectangle obj)
        {
            List<IBox> tmpBox = new List<IBox>();
            Rectangle unitPosition = obj;

            unitPosition.X += 4;
            unitPosition.Width -= 8;

            foreach (IBox box in _boxs)
            {

                if (((unitPosition.Y - 10) + unitPosition.Height) >= box.Position.Y)
                {

                    if (
                        unitPosition.X >= box.Position.X && unitPosition.X <= (box.Position.X + box.Position.Width) ||
                         (unitPosition.X + unitPosition.Width) >= box.Position.X && (unitPosition.X + unitPosition.Width) <= (box.Position.X + box.Position.Width)
                    )
                    {
                        bool C = CollisionY(unitPosition, box.Position);
                        if (C)
                        {
                            tmpBox.Add(box);
                        }
                    }
                }
            }

            if (tmpBox.Count > 0)
            {
                return true;
            }
            return false;
        }

        public bool isCollisionDown(Rectangle obj)
        {
            List<IBox> tmpBox = new List<IBox>();
            Rectangle unitPosition = obj;

            unitPosition.X += 4;
            unitPosition.Width -= 8;

            foreach (IBox box in _boxs)
            {

                if ((unitPosition.Y + unitPosition.Height - 20) <= box.Position.Y)
                {

                    if (
                        unitPosition.X >= box.Position.X && unitPosition.X <= (box.Position.X + box.Position.Width) ||
                         (unitPosition.X + unitPosition.Width) >= box.Position.X && (unitPosition.X + unitPosition.Width) <= (box.Position.X + box.Position.Width)
                    )
                    {
                        bool C = CollisionY(unitPosition, box.Position);
                        if (C)
                        {
                            tmpBox.Add(box);
                        }
                    }
                }
            }
            if (tmpBox.Count > 0)
            {
                return true;
            }
            return false;
        }
        public bool isCollisionLeft(Rectangle obj)
        {
            List<IBox> tmpBox = new List<IBox>();
            Rectangle unitPosition = obj;
            unitPosition.Y += 2;
            unitPosition.Height -= 7;
            foreach (IBox box in _boxs)
            {
                if ((unitPosition.X + unitPosition.Width - 10) >= box.Position.X)
                {
                    if (
                        unitPosition.Y >= box.Position.Y && unitPosition.Y <= (box.Position.Y + box.Position.Height) ||
                         (unitPosition.Y + unitPosition.Height) >= box.Position.Y && (unitPosition.Y + unitPosition.Height) <= (box.Position.Y + box.Position.Height)
                    )
                    {
                        bool C = CollisionX(unitPosition, box.Position);
                        if (C)
                        {
                            tmpBox.Add(box);
                        }
                    }
                }
            }
            if (tmpBox.Count > 0)
            {
                return true;
            }
            return false;
        }


        public bool isCollisionRight(Rectangle obj)
        {
            List<IBox> tmpBox = new List<IBox>();
            Rectangle unitPosition = obj;
            unitPosition.Y += 2;
            unitPosition.Height -= 7;
            foreach (IBox box in _boxs)
            {
                if ((unitPosition.X + unitPosition.Width - 10) <= box.Position.X)
                {
                    if (
                        unitPosition.Y >= box.Position.Y && unitPosition.Y <= (box.Position.Y + box.Position.Height) ||
                         (unitPosition.Y + unitPosition.Height) >= box.Position.Y && (unitPosition.Y + unitPosition.Height) <= (box.Position.Y + box.Position.Height)
                    ) {
                        bool C = CollisionX(unitPosition, box.Position);
                        if (C)
                        {
                            tmpBox.Add(box);
                        }
                    }
                }
            }
            if (tmpBox.Count > 0)
            {
                return true;
            }
            return false;
        }

        public bool CollisionX(Rectangle obj1, Rectangle obj2)
        {
            if ((obj1.X + obj1.Width >= obj2.X) && (obj1.X <= obj2.X + obj2.Width))
            {
                return true;
            }
            return false;
        }
        public bool CollisionY(Rectangle obj1, Rectangle obj2)
        {
            if ((obj1.Y + obj1.Height >= obj2.Y) && (obj1.Y <= obj2.Y + obj2.Height))
            {
                return true;
            }
            return false;
        }
    }
}
