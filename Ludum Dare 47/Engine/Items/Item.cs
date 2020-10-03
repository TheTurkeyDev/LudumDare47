using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum_Dare_47.Engine.Items
{
    public class Item
    {
        public static Item KEY = new Item("key", "Key");
        public static Item DOUBLE_JUMP = new Item("double_jump", "Double Jump");

        public string Id { get; set; }
        public string Name { get; set; }

        private Item(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
