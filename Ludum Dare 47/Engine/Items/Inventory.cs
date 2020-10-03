using System;
using System.Collections.Generic;
using System.Linq;

namespace Ludum_Dare_47.Engine.Items
{
    public class Inventory
    {
        public List<ItemStack> Items { get; private set; } = new List<ItemStack>();

        public bool HasItem(Item item)
        {
            foreach (ItemStack s in Items)
                if (s.Item.Equals(item))
                    return true;
            return false;
        }

        public void AddItem(Item item, int count = 1)
        {
            foreach (ItemStack s in Items)
            {
                if (s.Item.Equals(item))
                {
                    s.Count += count;
                    return;
                }
            }
            Items.Add(new ItemStack(item, count));
        }
        public bool RemoveItem(Item item, int count = 1)
        {
            for (int i = Items.Count() - 1; i >= 0; i--)
            {
                ItemStack stack = Items[i];
                if (stack.Item.Equals(item))
                {
                    stack.Count -= count;
                    if (stack.Count == 0)
                        Items.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

    }
}
