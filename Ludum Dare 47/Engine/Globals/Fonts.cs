using Microsoft.Xna.Framework.Graphics;

namespace EG2DCS.Engine.Globals
{
    class Fonts
    {
        public static SpriteFont MyFont_12 { get; private set; }
        public static SpriteFont MyFont_24 { get; private set; }
        public static SpriteFont MyFont_58 { get; private set; }
        public static void Load()
        {
            MyFont_12 = Universal.Content.Load<SpriteFont>("MyFont_12");
            MyFont_24 = Universal.Content.Load<SpriteFont>("MyFont_24");
            MyFont_58 = Universal.Content.Load<SpriteFont>("MyFont_58");
        }
    }
}
