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
    class Note : Entity
    {
        public override string EntId { get; } = "note";
        public List<string> Messages { get; set; } = new List<string>();
        private bool collided = false;

        public Note(Rectangle rect) : base(rect)
        {
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Draw(int offsetX, int offsetY)
        {
            Universal.SpriteBatch.Draw(Textures.Note, new Rectangle((int)Position.X + offsetX, (int)Position.Y + offsetY, Position.Width, Position.Height), Color.White);

            if (collided)
            {
                int height = (Messages.Count() * 25) + 15;
                int width = 100;
                foreach (string message in Messages)
                {
                    int textWidth = (int)Fonts.Arial_12.MeasureString(message).X;
                    if (textWidth + 20 > width)
                    {
                        width = textWidth + 20;
                    }
                }

                Vector2 topLeft = new Vector2(Position.X + offsetX - (width / 2), Position.Y + offsetY - (height + 20));
                Universal.SpriteBatch.Draw(Textures.Null, new Rectangle((int)topLeft.X, (int)topLeft.Y, width, height), Color.Gray);
                Universal.SpriteBatch.End();
                Universal.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
                for (int i = 0; i < Messages.Count(); i++)
                {
                    Universal.SpriteBatch.DrawString(Fonts.Arial_12, Messages[i], new Vector2((int)topLeft.X + 10, (int)topLeft.Y + 10 + (i * 25)), Color.Black);
                }
                Universal.SpriteBatch.End();
                Universal.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            }

            collided = false;
        }

        public override bool OnCollide(Entity ent)
        {
            if (ent is Player)
                collided = true;

            return true;
        }
    }
}
