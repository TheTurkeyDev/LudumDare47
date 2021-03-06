﻿using EG2DCS.Engine.Animation;
using EG2DCS.Engine.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG2DCS.Engine.Widgets
{
    public class Button : Widget
    {
        private string text;
        public SpriteFont TextFont { get; set; } = Fonts.MyFont_12;
        public Color TextColor { get; set; } = Color.Pink;
        public bool CenterText { get; set; } = false;

        public Color HighlightColor { get; set; } = Color.Pink;
        public float highlightWidth = 0;

        private ButtonHighlightAnimation currentAnim;

        private Func<bool> clickHandler;

        public Button(int x, int y, int width, int height, string text, Func<bool> clickHandler) : base(x, y, width, height)
        {
            this.text = text;
            this.clickHandler = clickHandler;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            Rectangle highlightRect = new Rectangle(Rectangle.Location, Rectangle.Size);
            highlightRect.Width = (int)highlightWidth;
            Universal.SpriteBatch.Draw(Textures.Null, highlightRect, HighlightColor);
            Vector2 textMesurements = Fonts.MyFont_24.MeasureString(text);

            if (CenterText)
                Universal.SpriteBatch.DrawString(TextFont, text, new Vector2(Rectangle.X + ((Rectangle.Width - textMesurements.X) / 2), Rectangle.Y + ((Rectangle.Height - textMesurements.Y) / 2)), TextColor);
            else
                Universal.SpriteBatch.DrawString(TextFont, text, new Vector2(Rectangle.X, Rectangle.Y), TextColor);
        }
        public override void Remove()
        {
        }

        public override void OnHover()
        {
            base.AddAnimation(currentAnim = new ButtonHighlightAnimation(this, 20));
        }

        public override void OnUnHover()
        {
            highlightWidth = 0;
            currentAnim.Complete = true;
        }

        public override void OnClick(bool lmb)
        {
            base.OnClick(lmb);
            clickHandler.Invoke();
        }
    }
}
