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
    public class Widget : Animator
    {
        private Color _backgroundColor;
        private int _borderWidth = 0;
        private Color _borderColor;

        public Widget(int x, int y, int width, int height) : base(new Rectangle(x, y, width, height))
        {
            _backgroundColor = new Color(Universal.rnd.Next(256), Universal.rnd.Next(256), Universal.rnd.Next(256), 255);
        }

        public virtual void Draw()
        {
            if (_borderWidth > 0)
            {
                Rectangle borderRect = new Rectangle(Rectangle.Location, Rectangle.Size);
                borderRect.Inflate(_borderWidth, _borderWidth);
                Universal.SpriteBatch.Draw(Textures.Null, borderRect, _borderColor);
            }
            Universal.SpriteBatch.Draw(Textures.Null, Rectangle, _backgroundColor);
        }

        public virtual void Remove() {}

        public virtual void OnHover() {}

        public virtual void OnUnHover() {}

        public virtual void OnClick(bool lmb) {}

        public Color SetBackgroundColor(Color color)
        {
            Color old = _backgroundColor;
            _backgroundColor = color;
            return old;
        }

        public void SetBorder(int width, Color color)
        {
            SetBorderWidth(width);
            SetBorderColor(color);
        }

        public void SetBorderWidth(int width) => _borderWidth = width;
        

        public void SetBorderColor(Color color) => _borderColor = color;
    }
}
