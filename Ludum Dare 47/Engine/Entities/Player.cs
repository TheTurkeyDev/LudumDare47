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
    class Player : Entity
    {
        public Inventory Inventory { get; private set; } = new Inventory();

        public Player() : base(new Rectangle(100, 1300, 64, 64))
        {

        }

        public override void Reset()
        {
            base.Reset();
            Inventory = new Inventory();
        }

        public void Jump()
        {
            if (!InAir() || Inventory.HasItem(Item.DOUBLE_JUMP))
            {
                if (InAir())
                    Inventory.RemoveItem(Item.DOUBLE_JUMP);
                velocity.Y = -15;
                inAir = true;
            }
        }

        public override void Draw(int offsetX, int offsetY)
        {
            Universal.SpriteBatch.Draw(Textures.Null, new Rectangle((int)Position.X + offsetX, (int)Position.Y + offsetY, Position.Width, Position.Height), Color.Red);
        }
    }
}
