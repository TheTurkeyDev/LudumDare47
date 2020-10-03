using EG2DCS.Engine.Globals;
using Ludum_Dare_47.Engine.Items;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum_Dare_47.Engine.Entities
{
    class Door : Entity
    {
        private bool Locked { get; set; } = true;
        public Door(Rectangle rect) : base(rect)
        {

        }

        public override void Reset()
        {
            base.Reset();
            Locked = true;
        }

        public override void Draw(int offsetX, int offsetY)
        {
            Universal.SpriteBatch.Draw(Textures.Null, new Rectangle((int)Position.X + offsetX, (int)Position.Y + offsetY, Position.Width, Position.Height), Locked ? Color.Brown : Color.RosyBrown);
        }

        public override bool OnCollide(Entity ent)
        {
            if (Locked && ent is Player && ((Player)ent).Inventory.HasItem(Item.KEY))
            {
                ((Player)ent).Inventory.RemoveItem(Item.KEY);
                Locked = false;
            }
            return !Locked;
        }
    }
}
