using EG2DCS.Engine.Globals;
using EG2DCS.Engine.Widgets;
using Ludum_Dare_47.Engine.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EG2DCS.Engine.Screen_Manager
{
    public class MainScreen : BaseScreen
    {

        public MainScreen()
        {
            Name = "Main";
            State = ScreenState.Active;
            Input.setCurrentKeyListener(this);

            int centerX = (int)Universal.GameSize.X / 2;

            Button btn = new Button(centerX - 75, 300, 200, 50, "Play", () =>
            {
                GameScreen.StartingLevel = "world_1";
                State = ScreenState.Inactive;
                ScreenManager.SetState(ScreenState.Active, "Game");
                return true;
            });
            btn.BackgroundColor = Colors.PrimaryMain;
            btn.HighlightColor = Colors.PrimaryLight;
            btn.TextColor = Colors.TextPrimary;
            btn.TextFont = Fonts.MyFont_24;
            btn.CenterText = true;
            AddWidget(btn);

            btn = new Button(centerX - 75, 400, 200, 50, "Tutorial", () =>
            {
                GameScreen.StartingLevel = "intro_1";
                State = ScreenState.Inactive;
                ScreenManager.SetState(ScreenState.Active, "Game");
                return true;
            });
            btn.BackgroundColor = Colors.PrimaryMain;
            btn.HighlightColor = Colors.PrimaryLight;
            btn.TextColor = Colors.TextPrimary;
            btn.TextFont = Fonts.MyFont_24;
            btn.CenterText = true;
            AddWidget(btn);

            btn = new Button(centerX - 75, 500, 200, 50, "Level Select", () =>
            {
                State = ScreenState.Inactive;
                ScreenManager.SetState(ScreenState.Active, "Level_Select");
                return true;
            });
            btn.BackgroundColor = Colors.PrimaryMain;
            btn.HighlightColor = Colors.PrimaryLight;
            btn.TextColor = Colors.TextPrimary;
            btn.TextFont = Fonts.MyFont_24;
            btn.CenterText = true;
            AddWidget(btn);

            btn = new Button(centerX - 75, 600, 200, 50, "Credits", () =>
            {
                State = ScreenState.Inactive;
                ScreenManager.SetState(ScreenState.Active, "Credits");
                return true;
            });
            btn.BackgroundColor = Colors.PrimaryMain;
            btn.HighlightColor = Colors.PrimaryLight;
            btn.TextColor = Colors.TextPrimary;
            btn.TextFont = Fonts.MyFont_24;
            btn.CenterText = true;
            AddWidget(btn);
        }
        public override void HandleInput()
        {
            base.HandleInput();

            if (base.focusedWidget != null)
                return;
        }

        public override bool onKeyPress(Keys key)
        {
            if (!base.onKeyPress(key))
            {

            }

            return false;
        }

        public override void Update()
        {
            base.Update();
        }
        public override void Draw()
        {

            Universal.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            Universal.SpriteBatch.Draw(Textures.Null, new Rectangle(0, 0, (int)Universal.GameSize.X, (int)Universal.GameSize.Y), Colors.BackgroundPrimary);
            Universal.SpriteBatch.Draw(Textures.Twtich, new Rectangle(25, 627, 13, 16), new Rectangle(0, 0, 64, 75), Colors.TwitchPurple);
            Universal.SpriteBatch.Draw(Textures.YouTube, new Rectangle(25, 653, 16, 11), new Rectangle(0, 0, 64, 45), Color.White);
            Universal.SpriteBatch.Draw(Textures.Twitter, new Rectangle(25, 675, 16, 16), new Rectangle(0, 0, 400, 400), Color.White);
            Universal.SpriteBatch.Draw(Textures.Github, new Rectangle(25, 700, 16, 16), new Rectangle(0, 0, 32, 32), Color.White);
            Universal.SpriteBatch.End();

            Universal.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            string text = "A Timed Loop";
            int textWidth = (int)Fonts.MyFont_58.MeasureString(text).X;
            Universal.SpriteBatch.DrawString(Fonts.MyFont_58, text, new Vector2((Universal.GameSize.X - textWidth) / 2, 30), Colors.TextPrimary);
            Universal.SpriteBatch.DrawString(Fonts.MyFont_12, "A Game By: TurkeyDev", new Vector2(25, 600), Colors.TextPrimary);
            Universal.SpriteBatch.DrawString(Fonts.MyFont_12, "TurkeyDev", new Vector2(55, 625), Colors.TextPrimary);
            Universal.SpriteBatch.DrawString(Fonts.MyFont_12, "TurkeyDev", new Vector2(55, 650), Colors.TextPrimary);
            Universal.SpriteBatch.DrawString(Fonts.MyFont_12, "@TurkeyDev", new Vector2(55, 675), Colors.TextPrimary);
            Universal.SpriteBatch.DrawString(Fonts.MyFont_12, "TheTurkeyDev", new Vector2(55, 700), Colors.TextPrimary);
            Universal.SpriteBatch.End();
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
