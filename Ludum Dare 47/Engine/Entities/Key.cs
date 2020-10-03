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
    class Key : Entity
    {
        public override string EntId { get; } = "key";

        public Key(Rectangle rect) : base(rect)
        {

        }

        public override void Draw(int offsetX, int offsetY)
        {
            Universal.SpriteBatch.Draw(Textures.Key, new Rectangle((int)Position.X + offsetX, (int)Position.Y + offsetY, Position.Width, Position.Height), Color.White);
        }

        public override bool OnCollide(Entity ent)
        {
            if (ent is Player)
            {
                ((Player)ent).Inventory.AddItem(Item.KEY);
                IsDead = true;
                Sounds.KeyPickup.Play();
            }
            return base.OnCollide(ent);
        }
    }
}
