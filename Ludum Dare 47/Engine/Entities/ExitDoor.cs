using EG2DCS.Engine.Globals;
using EG2DCS.Engine.Screen_Manager;
using Ludum_Dare_47.Engine.Toast;
using Ludum_Dare_47.Engine.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ludum_Dare_47.Engine.Entities
{
    class ExitDoor : Entity
    {
        public override string EntId { get; } = "exit_door";

        private bool collided = false;

        public ExitDoor(Rectangle rect) : base(rect)
        {
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Draw(int offsetX, int offsetY)
        {
            Rectangle sourceRect = new Rectangle(0, 0, 64, 128);
            bool complete = true;
            foreach (Task task in World.Tasks)
            {
                if (!task.Complete)
                {
                    complete = false;
                    break;
                }
            }


            if (complete)
                sourceRect.X = 64;

            Universal.SpriteBatch.Draw(Textures.ExitDoor, new Rectangle((int)Position.X + offsetX, (int)Position.Y + offsetY, Position.Width, Position.Height), sourceRect, Color.White);

            if (collided)
            {
                Vector2 topLeft = new Vector2(Position.X + offsetX - 50, Position.Y + offsetY - 100);
                Universal.SpriteBatch.Draw(Textures.Null, new Rectangle((int)topLeft.X, (int)topLeft.Y, 100 + Position.Width, 75), sourceRect, Color.Gray);
                Universal.SpriteBatch.End();
                Universal.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
                Universal.SpriteBatch.DrawString(Fonts.Arial_12, "You must complete", new Vector2((int)topLeft.X + 10, (int)topLeft.Y + 10), Color.Black);
                Universal.SpriteBatch.DrawString(Fonts.Arial_12, "all tasks!", new Vector2((int)topLeft.X + 10, (int)topLeft.Y + 50), Color.Black);
                Universal.SpriteBatch.End();
                Universal.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            }

            collided = false;
        }

        public override bool OnCollide(Entity ent)
        {
            if (ent is Player)
            {
                bool complete = true;
                foreach (Task task in World.Tasks)
                {
                    if (!task.Complete)
                    {
                        complete = false;
                        break;
                    }
                }

                if (complete)
                    World.Advance();
                else
                    collided = true;
            }


            return true;
        }
    }
}
