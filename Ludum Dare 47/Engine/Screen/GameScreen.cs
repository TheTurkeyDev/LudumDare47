using EG2DCS.Engine.Globals;
using EG2DCS.Engine.Overlay;
using EG2DCS.Engine.Widgets;
using Ludum_Dare_47.Engine.Globals;
using Ludum_Dare_47.Engine.Items;
using Ludum_Dare_47.Engine.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace EG2DCS.Engine.Screen_Manager
{
    public class GameScreen : BaseScreen
    {
        public static string StartingLevel { get; set; } = "intro_1";
        private World CurrentWorld { get; set; }

        private Button menuButton;
        private Button resumeButton;

        public GameScreen()
        {
            Name = "Game";
            State = ScreenState.Inactive;

            int centerX = (int)Universal.GameSize.X / 2;

            menuButton = new Button(centerX - 300, 400, 200, 50, "Main Menu", () =>
            {
                State = ScreenState.Inactive;
                ScreenManager.SetState(ScreenState.Active, "Main");
                return true;
            });
            menuButton.BackgroundColor = Colors.PrimaryMain;
            menuButton.HighlightColor = Colors.PrimaryLight;
            menuButton.TextColor = Colors.TextPrimary;
            menuButton.TextFont = Fonts.MyFont_24;
            menuButton.CenterText = true;
            menuButton.Visible = false;
            AddWidget(menuButton);

            resumeButton = new Button(centerX + 100, 400, 200, 50, "Resume", () =>
            {
                PopOverlay();
                resumeButton.Visible = false;
                menuButton.Visible = false;
                State = ScreenState.Active;
                return true;
            });
            resumeButton.BackgroundColor = Colors.PrimaryMain;
            resumeButton.HighlightColor = Colors.PrimaryLight;
            resumeButton.TextColor = Colors.TextPrimary;
            resumeButton.TextFont = Fonts.MyFont_24;
            resumeButton.CenterText = true;
            resumeButton.Visible = false;
            AddWidget(resumeButton);
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if (base.focusedWidget != null)
                return;

            CurrentWorld.Player.Moving = false;

            if (Input.KeyDown(Keys.A))
            {
                CurrentWorld.Player.Moving = CurrentWorld.MoveEntity(CurrentWorld.Player, -4, 0);
                CurrentWorld.Player.FacingRight = false;
            }

            if (Input.KeyDown(Keys.D))
            {
                CurrentWorld.Player.Moving = CurrentWorld.MoveEntity(CurrentWorld.Player, 4, 0);
                CurrentWorld.Player.FacingRight = true;
            }
        }

        public override bool onKeyPress(Keys key)
        {
            if (!base.onKeyPress(key))
            {
                if (key == Keys.Space)
                {
                    CurrentWorld.PlayerJump();
                    return true;
                }
                else if (key == Keys.R)
                {
                    CurrentWorld.Reset();
                    return true;
                }
                else if (key == Keys.Escape)
                {
                    if (State == ScreenState.Active)
                    {
                        PauseOverlay overlay = new PauseOverlay("Paused", 100, 100, (int)Universal.GameSize.X - 200, (int)Universal.GameSize.Y - 320);
                        overlay.Color = Colors.BackgroundSecondary;
                        PushOverlay(overlay);
                        resumeButton.Visible = true;
                        menuButton.Visible = true;
                        State = ScreenState.Paused;
                    }
                    else
                    {
                        PopOverlay();
                        resumeButton.Visible = false;
                        menuButton.Visible = false;
                        State = ScreenState.Active;
                    }
                    return true;
                }
            }

            return false;
        }

        public override void Update()
        {
            base.Update();
            CurrentWorld.Update();
        }
        public override void Draw()
        {
            CurrentWorld.Draw();

            Universal.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            Universal.SpriteBatch.Draw(Textures.Null, new Rectangle(0, (int)Universal.GameSize.Y - 120, (int)Universal.GameSize.X, 120), Colors.BackgroundPrimary);

            //Inventory
            Universal.SpriteBatch.Draw(Textures.ItemBanner, new Rectangle(100, (int)Universal.GameSize.Y - 120, 32, 120), Color.White);
            List<ItemStack> stacks = CurrentWorld.Player.Inventory.Items;
            for (int i = 0; i < stacks.Count; i++)
            {
                ItemStack stack = stacks[i];
                Universal.SpriteBatch.Draw(stack.Item.Texture, new Rectangle(150 + (80 * i), (int)Universal.GameSize.Y - 80, 64, 64), Color.White);
                string[] parts = stack.Item.Name.Split(' ');
                parts[parts.Length - 1] += " (" + stack.Count + ")";
                for (int j = 0; j < parts.Length; j++)
                    Universal.SpriteBatch.DrawString(Fonts.MyFont_12, parts[j], new Vector2(150 + (80 * i), (int)Universal.GameSize.Y - (80 + ((parts.Length - j) * 20))), Color.White);
            }

            //Tasks
            Universal.SpriteBatch.Draw(Textures.Null, new Rectangle(700, (int)Universal.GameSize.Y - 120, 5, 120), Colors.BackgroundSecondary);


            Universal.SpriteBatch.End();
            Universal.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            string text = "Time Left:";
            int textWidth = (int)Fonts.MyFont_12.MeasureString(text).X;
            Universal.SpriteBatch.DrawString(Fonts.MyFont_12, text, new Vector2((100 - textWidth) / 2, (int)Universal.GameSize.Y - 100), Color.Green);

            text = "" + (CurrentWorld.TimeLeft / 60);
            textWidth = (int)Fonts.MyFont_24.MeasureString(text).X;
            Universal.SpriteBatch.DrawString(Fonts.MyFont_24, text, new Vector2((100 - textWidth) / 2, (int)Universal.GameSize.Y - 60), CurrentWorld.TimeLeft > 300 ? Color.Green : Color.Red);
            Universal.SpriteBatch.End();

            if (State == ScreenState.Paused)
            {
                Universal.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
                Universal.SpriteBatch.Draw(Textures.Null, new Rectangle(0, 0, (int)Universal.GameSize.X, (int)Universal.GameSize.Y - 120), Colors.BackgroundPrimary50);
                Universal.SpriteBatch.End();

                foreach (Widget w in Widgets)
                    w.Update();
            }
            base.Draw();
        }

        public override void OnStateChange()
        {
            if (State == ScreenState.Active)
            {
                PopOverlay();
                resumeButton.Visible = false;
                menuButton.Visible = false;
                AdvanceToLevel(StartingLevel);
                Input.setCurrentKeyListener(this);
            }
        }

        public void AdvanceToLevel(string levelId)
        {
            CurrentWorld = WorldManager.Worlds.Find(w => w.Id.Equals(levelId));
            CurrentWorld.GameScreen = this;
            CurrentWorld.Reset();
        }
    }
}
