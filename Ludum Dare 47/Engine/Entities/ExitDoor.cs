using EG2DCS.Engine.Globals;
using Ludum_Dare_47.Engine.Worlds;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ludum_Dare_47.Engine.Entities
{
    class ExitDoor : Entity
    {
        public override string EntId { get; } = "exit_door";

        public ExitDoor(Rectangle rect) : base(rect)
        {
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Draw(int offsetX, int offsetY)
        {
            Rectangle sourceRect = new Rectangle(0, 0, 64, 128);
            bool complete = true;
            foreach (Task task in World.Tasks)
            {
                if (!task.Complete)
                {
                    complete = false;
                    break;
                }
            }


            if (complete)
                sourceRect.X = 64;

            Universal.SpriteBatch.Draw(Textures.ExitDoor, new Rectangle((int)Position.X + offsetX, (int)Position.Y + offsetY, Position.Width, Position.Height), sourceRect, Color.White);
        }

        public override bool OnCollide(Entity ent)
        {

            if (ent is Player)
            {
                bool complete = true;
                foreach (Task task in World.Tasks)
                {
                    if (!task.Complete)
                    {
                        complete = false;
                        break;
                    }
                }

                if (complete)
                {
                    Console.WriteLine("Here");
                    World.Reset();
                }
                else
                {
                    Console.WriteLine("Complete all tasks!");
                }
            }


            return true;
        }
    }
}
