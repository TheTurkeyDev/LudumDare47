using Ludum_Dare_47.Engine.Entities;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum_Dare_47.Engine.Worlds
{
    public abstract class CustomJsonConverter : JsonConverter
    {
        public static string GetStringTokenValue(JObject o, string tokenName)
        {
            JToken t;
            return o.TryGetValue(tokenName, StringComparison.InvariantCultureIgnoreCase, out t) ? (string)t : "";
        }

        public static int? GetIntTokenValue(JObject o, string tokenName)
        {
            JToken t;
            return o.TryGetValue(tokenName, StringComparison.InvariantCultureIgnoreCase, out t) ? (int)t : (int?)null;
        }

        public static int GetIntTokenValue(JObject o, string tokenName, int defaultInt)
        {
            JToken t;
            return o.TryGetValue(tokenName, StringComparison.InvariantCultureIgnoreCase, out t) ? (int)t : defaultInt;
        }

        public static bool GetBoolTokenValue(JObject o, string tokenName, bool defaultBool)
        {
            JToken t;
            return o.TryGetValue(tokenName, StringComparison.InvariantCultureIgnoreCase, out t) ? (bool)t : defaultBool;
        }

        public static List<string> GetStringListTokenValue(JObject o, string tokenName)
        {
            JToken t;
            o.TryGetValue(tokenName, StringComparison.InvariantCultureIgnoreCase, out t);

            List<string> toReturn = new List<string>();
            foreach (JToken token in t.Children())
                toReturn.Add((string)token);

            return toReturn;
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }

    public class CustomVector2Converter : CustomJsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var rectangle = (Vector2)value;

            var x = rectangle.X;
            var y = rectangle.Y;

            var o = JObject.FromObject(new { x, y });

            o.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var o = JObject.Load(reader);

            var x = GetIntTokenValue(o, "x") ?? 0;
            var y = GetIntTokenValue(o, "y") ?? 0;

            return new Vector2(x, y);
        }
    }

    public class CustomColorConverter : CustomJsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var col = (Color)value;

            var r = col.R;
            var g = col.G;
            var b = col.B;
            var a = col.A;

            var o = JObject.FromObject(new { r, g, b, a });

            o.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var o = JObject.Load(reader);

            var r = GetIntTokenValue(o, "r") ?? 0;
            var g = GetIntTokenValue(o, "g") ?? 0;
            var b = GetIntTokenValue(o, "b") ?? 0;
            var a = GetIntTokenValue(o, "a") ?? 0;

            return new Color(r, g, b, a);
        }
    }

    public class CustomEntityConverter : CustomJsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var ent = (Entity)value;

            var id = ent.EntId;

            var o = JObject.FromObject(new { id });

            o.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var o = JObject.Load(reader);

            var id = GetStringTokenValue(o, "id");
            var x = GetIntTokenValue(o, "x") ?? 0;
            var y = GetIntTokenValue(o, "y") ?? 0;
            var width = GetIntTokenValue(o, "width") ?? 0;
            var height = GetIntTokenValue(o, "height") ?? 0;
            var rect = new Rectangle(x, y, width, height);


            Entity entity;
            switch (id)
            {
                case "key":
                    entity = new Key(rect);
                    break;
                case "double_jump":
                    entity = new DoubleJump(rect);
                    break;
                case "door":
                    entity = new Door(rect);
                    break;
                case "button":
                    entity = new Button(rect);
                    ((Button)entity).WallMounted = GetBoolTokenValue(o, "wallMounted", false);
                    ((Button)entity).AttatchFace = (Face)GetIntTokenValue(o, "Face", 0);
                    break;
                case "exit_door":
                    entity = new ExitDoor(rect);
                    break;
                case "note":
                    entity = new Note(rect);
                    ((Note)entity).Messages = GetStringListTokenValue(o, "Messages");
                    break;
                default:
                    entity = new Entity(rect);
                    break;
            }

            entity.Gravity = GetBoolTokenValue(o, "gravity", true);

            return entity;
        }
    }

    public class CustomPlayerConverter : CustomJsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var player = (Player)value;

            var id = player.EntId;

            var o = JObject.FromObject(new { id });

            o.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var o = JObject.Load(reader);

            var x = GetIntTokenValue(o, "x") ?? 0;
            var y = GetIntTokenValue(o, "y") ?? 0;
            var width = GetIntTokenValue(o, "width") ?? 0;
            var height = GetIntTokenValue(o, "height") ?? 0;
            var rect = new Rectangle(x, y, width, height);

            Player player = new Player(rect);

            return player;
        }
    }
}
