using EG2DCS.Engine.Globals;
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
    public class LevelSelectScreen : BaseScreen
    {

        public LevelSelectScreen()
        {
            Name = "Level_Select";
            State = ScreenState.Inactive;

            int centerX = (int)Universal.GameSize.X / 2;

            int i = 0;
            int x = 100;
            int y = 25;

            Dictionary<string, List<World>> worlds = new Dictionary<string, List<World>>();

            foreach (World world in WorldManager.Worlds)
            {
                List<World> worldsList;
                if (!worlds.TryGetValue(world.Group, out worldsList))
                {
                    worldsList = new List<World>();
                    worlds.Add(world.Group, worldsList);
                }

                worldsList.Add(world);
            }

            foreach (string group in worlds.Keys)
            {
                Label groupLabel = new Label(x, y, 200, 40, group);
                groupLabel.HoverColor = Colors.TextPrimary;
                groupLabel.BackgroundColor = Colors.Clear;
                groupLabel.TextColor = Colors.TextPrimary;
                groupLabel.TextFont = Fonts.MyFont_24;
                AddWidget(groupLabel);

                y += 50;

                foreach (World world in worlds[group])
                {
                    if (i >= 5)
                    {
                        i = 0;
                        x = 100;
                        y += 75;
                    }

                    Button btn = new Button(x, y, 200, 50, world.Name, () =>
                    {
                        GameScreen.StartingLevel = world.Id;
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

                    x += 225;
                    i++;
                }

                i = 0;
                x = 100;
                y += 100;
            }

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
            Universal.SpriteBatch.End();
            base.Draw();
        }
    }
}
