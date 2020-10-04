using EG2DCS.Engine.Animation;
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
    public class Label : Widget
    {
        private string text;
        public SpriteFont TextFont { get; set; } = Fonts.MyFont_12;
        public Color TextColor { get; set; } = Color.White;

        public Color HoverColor { get; set; } = Color.White;

        private bool hovered = false;

        public Label(int x, int y, int width, int height, string text) : base(x, y, width, height)
        {
            this.text = text;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            Universal.SpriteBatch.DrawString(TextFont, text, new Vector2(Rectangle.X, Rectangle.Y), hovered ? HoverColor : TextColor);
        }

        public override void Remove()
        {
        }

        public override void OnHover()
        {
            hovered = true;
        }

        public override void OnUnHover()
        {
            hovered = false;
        }
    }
}
