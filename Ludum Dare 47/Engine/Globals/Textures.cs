using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG2DCS.Engine.Globals
{
    class Textures
    {
        public static Texture2D Null;
        public static Texture2D Key;
        public static Texture2D DoubleJump;
        public static Texture2D Player;
        public static Texture2D Door;
        public static Texture2D ExitDoor;
        public static Texture2D Button;
        public static Texture2D Button_Wall;
        public static Texture2D Note;

        public static Texture2D ItemBanner;


        public static Texture2D Twtich;
        public static Texture2D YouTube;
        public static Texture2D Twitter;
        public static Texture2D Github;

        public static void Load()
        {
            Null = Universal.Content.Load<Texture2D>("Null");
            Key = Universal.Content.Load<Texture2D>("items/key");
            DoubleJump = Universal.Content.Load<Texture2D>("items/double_jump");
            Player = Universal.Content.Load<Texture2D>("entities/player");
            Door = Universal.Content.Load<Texture2D>("entities/door");
            ExitDoor = Universal.Content.Load<Texture2D>("entities/exit_door");
            Button = Universal.Content.Load<Texture2D>("entities/button");
            Button_Wall = Universal.Content.Load<Texture2D>("entities/button_wall");
            Note = Universal.Content.Load<Texture2D>("entities/note");
            ItemBanner = Universal.Content.Load<Texture2D>("item_banner");
            Twtich = Universal.Content.Load<Texture2D>("socials/twitch");
            YouTube = Universal.Content.Load<Texture2D>("socials/youtube");
            Twitter = Universal.Content.Load<Texture2D>("socials/twitter");
            Github = Universal.Content.Load<Texture2D>("socials/github");
        }

    }
}
