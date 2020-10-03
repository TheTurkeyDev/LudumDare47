using System;
using System.Collections.Generic;
using System.Text;
using Ludum_Dare_47.Engine.Items;

namespace Game1.Engine.Items
{
    public class ItemStack
    {
        public Item Item { get; set; }
        public int Count { get; set; }

        public ItemStack(Item item, int count = 0)
        {
            Item = item;
            Count = count;
        }
    }
}
