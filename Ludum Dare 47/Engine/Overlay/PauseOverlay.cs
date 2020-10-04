using EG2DCS.Engine.Animation;
using EG2DCS.Engine.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG2DCS.Engine.Overlay
{
    public class PauseOverlay : BaseOverlay
    {
        public string Message { get; set; }
        public SpriteFont Font { get; set; } = Fonts.MyFont_58;
        public PauseOverlay(string message, int x, int y, int width, int height) : base(x, y, width, height)
        {
            Message = message;
        }

        public override void Draw()
        {
            base.Draw();
            int messageWidth = (int)Font.MeasureString(Message).X;
            Vector2 pos = new Vector2(Rectangle.X + ((Rectangle.Width - messageWidth) / 2), Rectangle.Y + 20);
            Universal.SpriteBatch.DrawString(Font, Message, pos, Color.White);
        }
    }
}
