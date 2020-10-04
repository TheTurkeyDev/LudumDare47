using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG2DCS.Engine.Globals
{
    class Sounds
    {
        public static SoundEffect Jump;
        public static SoundEffect KeyPickup;
        public static SoundEffect Powerup;
        public static SoundEffect Step;
        public static SoundEffect Door;
        public static SoundEffect Button;
        public static void Load()
        {
            Jump = Universal.Content.Load<SoundEffect>("sounds/jump");
            KeyPickup = Universal.Content.Load<SoundEffect>("sounds/key_pickup");
            Step = Universal.Content.Load<SoundEffect>("sounds/step");
            Powerup = Universal.Content.Load<SoundEffect>("sounds/powerup");
            Door = Universal.Content.Load<SoundEffect>("sounds/door");
            Button = Universal.Content.Load<SoundEffect>("sounds/button");
        }
    }
}
