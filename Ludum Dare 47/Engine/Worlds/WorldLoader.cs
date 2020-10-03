using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum_Dare_47.Engine.Worlds
{
    class WorldLoader
    {
        public static void Load()
        {
            using (var stream = TitleContainer.OpenStream(@"Worlds\world1.json"))
            {
                var serializer = new JsonSerializer();
                World World = (World)serializer.Deserialize(new StreamReader(stream), typeof(World));
                World.Setup();
                WorldManager.Worlds.Add(World);
            }
        }
    }
}
