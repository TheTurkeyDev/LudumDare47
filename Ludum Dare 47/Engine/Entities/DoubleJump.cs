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
    class DoubleJump : Entity
    {
        public DoubleJump(Rectangle rect) : base(rect)
        {

        }

        public override void Draw(int offsetX, int offsetY)
        {
            Universal.SpriteBatch.Draw(Textures.Null, new Rectangle((int)Position.X + offsetX, (int)Position.Y + offsetY, Position.Width, Position.Height), Color.Green);
        }

        public override bool OnCollide(Entity ent)
        {
            if (ent is Player)
            {
                ((Player)ent).Inventory.AddItem(Item.DOUBLE_JUMP);
                IsDead = true;
            }
            return base.OnCollide(ent);
        }
    }
}
