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
        private Color backgroundColor;

        private int borderWidth = 0;
        private Color borderColor;

        public Widget(int x, int y, int width, int height) : base(new Rectangle(x, y, width, height))
        {
            backgroundColor = new Color(Universal.rnd.Next(256), Universal.rnd.Next(256), Universal.rnd.Next(256), 255);
        }

        public override void Update()
        {
            base.Update();
        }

        public virtual void Draw()
        {
            if (borderWidth > 0)
            {
                Rectangle borderRect = new Rectangle(rectangle.Location, rectangle.Size);
                borderRect.Inflate(borderWidth, borderWidth);
                Universal.SpriteBatch.Draw(Textures.Null, borderRect, borderColor);
            }
            Universal.SpriteBatch.Draw(Textures.Null, rectangle, backgroundColor);
        }

        public virtual void Remove()
        {
        }

        public virtual void OnHover()
        {
        }

        public virtual void OnUnHover()
        {
        }

        public virtual void OnClick(bool lmb)
        {
        }

        public Color setBackgroundColor(Color color)
        {
            Color old = backgroundColor;
            backgroundColor = color;
            return old;
        }

        public void setBorder(int width, Color color)
        {
            setBorderWidth(width);
            setBorderColor(color);
        }

        public void setBorderWidth(int width)
        {
            this.borderWidth = width;
        }
        public void setBorderColor(Color color)
        {
            this.borderColor = color;
        }
    }
}
