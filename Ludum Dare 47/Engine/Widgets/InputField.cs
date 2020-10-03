using EG2DCS.Engine.Animation;
using EG2DCS.Engine.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG2DCS.Engine.Widgets
{
    public class InputField : Widget, IFocusable
    {
        private string _placeholderText;
        private string _text;
        private Color _textColor;
        private Color _selectedColor;

        private bool _selected = false;

        public InputField(int x, int y, int width, int height, string text) : this(x, y, width, height, text,
            new Color(Universal.rnd.Next(256), Universal.rnd.Next(256), Universal.rnd.Next(256), 255),
            new Color(Universal.rnd.Next(256), Universal.rnd.Next(256), Universal.rnd.Next(256), 255))
        {}

        public InputField(int x, int y, int width, int height, string text, Color textColor) : this(x, y, width, height, text, textColor, textColor)
        {}

        public InputField(int x, int y, int width, int height, string text, Color textColor, Color selectedColor) : base(x, y, width, height)
        {
            _text = text;
            _textColor = textColor;
            _selectedColor = selectedColor;
        }

        public override void Draw()
        {
            base.Draw();
            if (_selected)
                Universal.SpriteBatch.Draw(Textures.Null, Rectangle, _selectedColor);
            Universal.SpriteBatch.DrawString(Fonts.Arial_12, _text.Length == 0 ? _placeholderText : _text, new Vector2(Rectangle.X, Rectangle.Y), _textColor);
        }

        public override void Remove()
        {}

        public void OnFocus() => _selected = true;

        public void OnUnFocus() => _selected = false;
        


        public bool OnKeyPress(Keys key)
        {
            if (key >= Keys.A && key <= Keys.Z)
            {
                if (Input.KeyDown(Keys.LeftShift) || Input.KeyDown(Keys.RightShift))
                    _text += key.ToString();
                else
                    _text += key.ToString().ToLower();
            }
            else if (key == Keys.Back && _text.Length > 0)
            {
                _text = _text.Substring(0, _text.Length - 1);
            }
            return true;
        }

        public bool OnKeyRelease(Keys key) => true;

        public void SetPlaceholderText(string text) =>_placeholderText = text;
    }
}
