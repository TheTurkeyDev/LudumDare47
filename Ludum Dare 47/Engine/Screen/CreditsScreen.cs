using EG2DCS.Engine.Globals;
using EG2DCS.Engine.Widgets;
using Ludum_Dare_47.Engine.Globals;
using Ludum_Dare_47.Engine.Items;
using Ludum_Dare_47.Engine.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace EG2DCS.Engine.Screen_Manager
{
    public class CreditsScreen : BaseScreen
    {

        private static string[] Supporters = new string[] { "shinauko33", "mrlubert", "carloseddy100", "hippodippos", "enigmagaming2k", "mjrlegends", "quirkygeek17", "mongotheelder", "burtekd", "jackruby1", "jackyy" };

        public CreditsScreen()
        {
            Name = "Credits";
            State = ScreenState.Inactive;
            Input.setCurrentKeyListener(this);

            Button back = new Button(25, 650, 200, 50, "Back", () =>
            {
                State = ScreenState.Inactive;
                ScreenManager.SetState(ScreenState.Active, "Main");
                return true;
            });
            back.BackgroundColor = Colors.PrimaryMain;
            back.HighlightColor = Colors.PrimaryLight;
            back.TextColor = Colors.TextPrimary;
            back.TextFont = Fonts.MyFont_24;
            back.CenterText = true;
            AddWidget(back);

            Array.Sort(Supporters);
        }

        public override void Draw()
        {
            int centerX = (int)Universal.GameSize.X / 2;

            Universal.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            Universal.SpriteBatch.Draw(Textures.Null, new Rectangle(0, 0, (int)Universal.GameSize.X, (int)Universal.GameSize.Y), Colors.BackgroundPrimary);

            string text = "- My Supporters -";
            int textWidth = (int)Fonts.MyFont_24.MeasureString(text).X;
            Universal.SpriteBatch.DrawString(Fonts.MyFont_24, text, new Vector2(centerX - (textWidth / 2), 100), Colors.TextPrimary);
            Universal.SpriteBatch.End();

            for (int i = 0; i < Supporters.Length; i++)
            {
                string user = Supporters[i];

                textWidth = (int)Fonts.MyFont_24.MeasureString(user).X + 35;
                Universal.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
                Universal.SpriteBatch.Draw(Textures.Twtich, new Rectangle(centerX - (textWidth / 2), 153 + (i * 40), 25, 32), Colors.TwitchPurple);
                Universal.SpriteBatch.DrawString(Fonts.MyFont_24, user, new Vector2(centerX - (textWidth / 2) + 35, 150 + (i * 40)), Colors.TwitchPurple);
                Universal.SpriteBatch.End();
            }

            base.Draw();
        }

        public override void OnStateChange()
        {
            if (State == ScreenState.Active)
            {
                Input.setCurrentKeyListener(this);
            }
        }
    }
}
