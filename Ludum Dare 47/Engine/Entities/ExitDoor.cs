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
        private World world;
        public ExitDoor(Rectangle rect, World world) : base(rect)
        {
            this.world = world;
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Draw(int offsetX, int offsetY)
        {
            Universal.SpriteBatch.Draw(Textures.Null, new Rectangle((int)Position.X + offsetX, (int)Position.Y + offsetY, Position.Width, Position.Height), Color.LightGreen);
        }

        public override bool OnCollide(Entity ent)
        {
            bool complete = true;
            if (ent is Player)
            {
                foreach (Task task in world.Tasks)
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
                    world.Reset();
                }
            }


            return complete;
        }
    }
}
