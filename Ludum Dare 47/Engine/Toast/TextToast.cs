using EG2DCS.Engine.Globals;
using EG2DCS.Engine.Toast;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum_Dare_47.Engine.Toast
{
    public class TextToast : BaseToast
    {
        public string Message { get; set; }
        public TextToast(string message, Vector2 vec) : base(vec)
        {
            Message = message;
        }

        public override void Draw()
        {
            base.Draw();
            if (IsComplete())
                return;

            Vector2 offset = Rectangle.Location.ToVector2();
            offset.X += 50;
            offset.Y += 20;
            Universal.SpriteBatch.DrawString(Fonts.MyFont_12, Message, offset, Color.White);
        }
    }
}
