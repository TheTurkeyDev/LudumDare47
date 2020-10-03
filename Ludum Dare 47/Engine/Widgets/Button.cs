using EG2DCS.Engine.Animation;
using EG2DCS.Engine.Globals;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG2DCS.Engine.Widgets
{
    public class Button : Widget
    {
        private string _text;

        private Color _highlightColor;
        public float HighlightWidth = 0;

        private ButtonHighlightAnimation _currentAnim;

        public Button(int x, int y, int width, int height, string text) : this(x, y, width, height, text, Color.White, new Color(Universal.rnd.Next(256), Universal.rnd.Next(256), Universal.rnd.Next(256), 255))
        {
        }

        public Button(int x, int y, int width, int height, string text, Color textColor, Color highlightColor) : base(x, y, width, height)
        {
            _text = text;
            _highlightColor = highlightColor;
        }

        public override void Draw()
        {
            base.Draw();
            Rectangle highlightRect = new Rectangle(Rectangle.Location, Rectangle.Size);
            highlightRect.Width = (int)HighlightWidth;
            Universal.SpriteBatch.Draw(Textures.Null, highlightRect, _highlightColor);
            Universal.SpriteBatch.DrawString(Fonts.Arial_12, _text, new Vector2(Rectangle.X, Rectangle.Y), Color.White);
        }

        public override void Remove()
        {
        }

        public override void OnHover() =>AddAnimation(_currentAnim = new ButtonHighlightAnimation(this, 20));

        public override void OnUnHover()
        {
            HighlightWidth = 0;
            _currentAnim.SetComplete();
        }

        public override void OnClick(bool lmb)
        {
            Console.WriteLine("Button Clicked!");
            base.OnClick(lmb);
        }
    }
}
