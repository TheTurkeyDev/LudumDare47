using EG2DCS.Engine.Globals;
using Ludum_Dare_47.Engine.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ludum_Dare_47.Engine.Entities
{
    class Button : Entity
    {

        public override string EntId { get; } = "button";

        public bool Pressed { get; private set; }
        public bool WallMounted { get; set; } = false;
        public Face AttatchFace { get; set; }
        private Task task;

        public Button(Rectangle rect) : base(rect)
        {

        }

        public override void Setup(World World)
        {
            base.Setup(World);
            task = new Task();
            World.Tasks.Add(task);
        }

        public override void Reset()
        {
            base.Reset();
            Pressed = false;
            task.Complete = false;
        }

        public override void Draw(int offsetX, int offsetY)
        {
            Rectangle sourceRect = new Rectangle(0, 0, 64, 16);
            if (WallMounted)
            {
                sourceRect.Width = 16;
                sourceRect.Height = 64;
                if (Pressed)
                {
                    sourceRect.Y = 64;
                }
            }
            else if (Pressed)
            {
                sourceRect.X = 64;
            }

            Universal.SpriteBatch.Draw(WallMounted ? Textures.Button_Wall : Textures.Button, new Rectangle((int)Position.X + offsetX, (int)Position.Y + offsetY, Position.Width, Position.Height), sourceRect, Color.White, 0f, new Vector2(0, 0), AttatchFace == Face.LEFT ? SpriteEffects.FlipHorizontally : (AttatchFace == Face.BOTTOM ? SpriteEffects.FlipVertically : SpriteEffects.None), 0f);
        }

        public override bool OnCollide(Entity ent)
        {
            if (ent is Player && !Pressed)
            {
                Pressed = true;
                task.Complete = true;
                Sounds.Button.Play();
            }
            return base.OnCollide(ent);
        }
    }
}
