using EG2DCS.Engine.Animation;
using EG2DCS.Engine.Globals;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG2DCS.Engine.Overlay
{
    public class TextOverlay : BaseOverlay
    {
        public string Message { get; set; }
        public TextOverlay(string message, int x, int y, int width, int height) : base(x, y, width, height)
        {
            Message = message;
        }

        public override void Draw()
        {
            base.Draw();
            Vector2 pos = new Vector2(Rectangle.X + 100, Rectangle.Y + 20);
            Universal.SpriteBatch.DrawString(Fonts.Arial_12, Message, pos, Color.White);
        }
    }
}
