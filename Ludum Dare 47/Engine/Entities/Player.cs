using EG2DCS.Engine.Animation;
using EG2DCS.Engine.Globals;
using EG2DCS.Engine.Screen_Manager;
using Ludum_Dare_47.Engine.Items;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum_Dare_47.Engine.Entities
{
    public class Player : Entity
    {
        public Inventory Inventory { get; private set; } = new Inventory();

        private int soundDelay = 1;

        private int animDelay = 3;
        private int animStep = 0;
        private int animTotalSteps = 8;

        public bool Moving { get; set; } = false;
        public bool FacingRight { get; set; } = true;

        public Player(Rectangle rect) : base(rect)
        {

        }

        public override void Reset()
        {
            base.Reset();
            Inventory = new Inventory();
        }

        public void Jump()
        {
            if ((!InAir() && Velocity.Y <= 0.7f) || Inventory.HasItem(Item.DOUBLE_JUMP))
            {
                if (InAir())
                    Inventory.RemoveItem(Item.DOUBLE_JUMP);
                velocity.Y = -15;
                inAir = true;
                Sounds.Jump.Play();
            }
        }

        public override void Draw(int offsetX, int offsetY)
        {
            int offset;
            if (Moving)
            {
                offset = animStep / animDelay;
                animStep++;
                if (animStep == animDelay * animTotalSteps)
                    animStep = 0;
            }
            else
            {
                offset = 0;
                animStep = 0;
            }

            if (InAir())
                Universal.SpriteBatch.Draw(Textures.Player, new Rectangle((int)Position.X + offsetX, (int)Position.Y + offsetY, Position.Width, Position.Height), new Rectangle((FacingRight ? 0 : 16), 64, 16, 32), Color.White);
            else
                Universal.SpriteBatch.Draw(Textures.Player, new Rectangle((int)Position.X + offsetX, (int)Position.Y + offsetY, Position.Width, Position.Height), new Rectangle(offset * 16, (FacingRight ? 0 : 32), 16, 32), Color.White);
        }

        public override void Update()
        {
            base.Update();
            if (Moving && !inAir)
            {
                soundDelay--;
                if (soundDelay == 0)
                {
                    Sounds.Step.Play();
                    soundDelay = 15;
                }
            }
            else
            {
                soundDelay = 1;
            }
        }
    }
}
