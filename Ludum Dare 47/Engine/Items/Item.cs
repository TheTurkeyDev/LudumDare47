using EG2DCS.Engine.Globals;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum_Dare_47.Engine.Items
{
    public class Item
    {
        public static Item KEY = new Item("key", "Key", Textures.Key);
        public static Item DOUBLE_JUMP = new Item("double_jump", "Double Jump", Textures.DoubleJump);

        public string Id { get; set; }
        public string Name { get; set; }
        public Texture2D Texture { get; set; }

        private Item(string id, string name, Texture2D texture)
        {
            Id = id;
            Name = name;
            Texture = texture;
        }
    }

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
