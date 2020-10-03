using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum_Dare_47.Engine.Worlds
{
    public class Wall
    {
        public Rectangle Rectangle { get; set; }

        [JsonConverter(typeof(CustomColorConverter))]
        public Color Color { get; set; }
    }
}
