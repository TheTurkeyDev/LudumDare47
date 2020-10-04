﻿using EG2DCS.Engine.Animation;
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
        private string placeholderText;
        private string text;
        private Color textColor;
        private Color selectedColor;

        private bool selected = false;

        public InputField(int x, int y, int width, int height, string text) : this(x, y, width, height, text,
            new Color(Universal.rnd.Next(256), Universal.rnd.Next(256), Universal.rnd.Next(256), 255),
            new Color(Universal.rnd.Next(256), Universal.rnd.Next(256), Universal.rnd.Next(256), 255))
        {
        }

        public InputField(int x, int y, int width, int height, string text, Color textColor) : this(x, y, width, height, text, textColor, textColor)
        {
        }

        public InputField(int x, int y, int width, int height, string text, Color textColor, Color selectedColor) : base(x, y, width, height)
        {
            this.text = text;
            this.textColor = textColor;
            this.selectedColor = selectedColor;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            if (selected)
                Universal.SpriteBatch.Draw(Textures.Null, Rectangle, selectedColor);
            Universal.SpriteBatch.DrawString(Fonts.MyFont_12, text.Length == 0 ? placeholderText : text, new Vector2(Rectangle.X, Rectangle.Y), textColor);
        }

        public override void Remove()
        {
        }

        public void onFocus()
        {
            selected = true;
        }

        public void onUnFocus()
        {
            selected = false;
        }


        public bool onKeyPress(Keys key)
        {
            if (key >= Keys.A && key <= Keys.Z)
            {
                if (Input.KeyDown(Keys.LeftShift) || Input.KeyDown(Keys.RightShift))
                    text += key.ToString();
                else
                    text += key.ToString().ToLower();
            }
            else if (key == Keys.Back && text.Length > 0)
            {
                text = text.Substring(0, text.Length - 1);
            }
            return true;
        }

        public bool onKeyRelease(Keys key)
        {
            return true;
        }

        public void setPlaceholderText(string text)
        {
            this.placeholderText = text;
        }
    }
}
