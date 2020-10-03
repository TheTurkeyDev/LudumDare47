using EG2DCS.Engine.Globals;
using Ludum_Dare_47.Engine.Worlds;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ludum_Dare_47.Engine.Entities
{
    class Button : Entity
    {
        private bool Pressed = false;
        private bool wallMounted = false;
        private Task task;

        public Button(Rectangle rect, bool wallMounted, Task task) : base(rect)
        {
            this.task = task;
            this.wallMounted = wallMounted;
            if (wallMounted)
                Gravity = false;

        }

        public override void Reset()
        {
            base.Reset();
            Pressed = false;
            task.Complete = false;
        }

        public override void Draw(int offsetX, int offsetY)
        {
            Universal.SpriteBatch.Draw(Textures.Null, new Rectangle((int)Position.X + offsetX, (int)Position.Y + offsetY, Position.Width, Position.Height), Pressed ? Color.DarkRed : Color.Red);
        }

        public override bool OnCollide(Entity ent)
        {
            if (ent is Player && !Pressed)
            {
                if (wallMounted)
                {
                    position.Width /= 2;
                }
                else
                {
                    position.Height /= 2;
                    position.Y += position.Height;
                }
                Pressed = true;
                task.Complete = true;
            }
            return base.OnCollide(ent);
        }
    }
}
