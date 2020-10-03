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
        private bool _pressed;
        private bool _wallMounted = false;
        private Task _task;

        public Button(Rectangle rect, bool wallMounted, Task task) : base(rect)
        {
            _task = task;
            _wallMounted = wallMounted;
            if (wallMounted)
                Gravity = false;
        }

        public override void Reset()
        {
            base.Reset();
            _pressed = false;
            _task.Complete = false;
        }

        public override void Draw(int offsetX, int offsetY)
        {
            Universal.SpriteBatch.Draw(Textures.Null, new Rectangle((int)Position.X + offsetX, (int)Position.Y + offsetY, Position.Width, Position.Height), _pressed ? Color.DarkRed : Color.Red);
        }

        public override bool OnCollide(Entity ent)
        {
            if (ent is Player && !_pressed)
            {
                if (_wallMounted)
                {
                    _position.Width /= 2;
                }
                else
                {
                    _position.Height /= 2;
                    _position.Y += Position.Height;
                }
                _pressed = true;
                _task.Complete = true;
            }
            return base.OnCollide(ent);
        }
    }
}
