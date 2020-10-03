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
    public class Label : Widget
    {
        private string _text;
        private Color _textColor;
        private Color _hoverColor;

        private bool _hovered = false;

        public Label(int x, int y, int width, int height, string text) : this(x, y, width, height, text,
            new Color(Universal.rnd.Next(256), Universal.rnd.Next(256), Universal.rnd.Next(256), 255),
            new Color(Universal.rnd.Next(256), Universal.rnd.Next(256), Universal.rnd.Next(256), 255))
        {}

        public Label(int x, int y, int width, int height, string text, Color textColor) : this(x, y, width, height, text, textColor, textColor)
        {}

        public Label(int x, int y, int width, int height, string text, Color textColor, Color hoverColor) : base(x, y, width, height)
        {
            _text = text;
            _textColor = textColor;
            _hoverColor = hoverColor;
        }

        public override void Draw()
        {
            base.Draw();
            Universal.SpriteBatch.DrawString(Fonts.Arial_12, _text, new Vector2(Rectangle.X, Rectangle.Y), _hovered ? _hoverColor : _textColor);
        }

        public override void Remove()
        {
        }

        public override void OnHover() => _hovered = true;
        

        public override void OnUnHover() => _hovered = false;
    }
}
