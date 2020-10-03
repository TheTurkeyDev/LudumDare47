﻿using EG2DCS.Engine.Globals;
using Ludum_Dare_47.Engine.Items;
using Ludum_Dare_47.Engine.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace EG2DCS.Engine.Screen_Manager
{
    class GameScreen : BaseScreen
    {
        private World CurrentWorld { get; set; }

        public GameScreen()
        {
            Name = "Game";
            State = ScreenState.Active;
            Input.setCurrentKeyListener(this);
            CurrentWorld = WorldManager.Worlds[0];
        }
        public override void HandleInput()
        {
            base.HandleInput();

            if (base.focusedWidget != null)
                return;

            CurrentWorld.player.Moving = false;

            if (Input.KeyDown(Keys.A))
            {
                CurrentWorld.player.Moving = CurrentWorld.MoveEntity(CurrentWorld.player, -4, 0);
                CurrentWorld.player.FacingRight = false;
            }

            if (Input.KeyDown(Keys.D))
            {
                CurrentWorld.player.Moving = CurrentWorld.MoveEntity(CurrentWorld.player, 4, 0);
                CurrentWorld.player.FacingRight = true;
            }

            if (Input.KeyPressed(Keys.Escape))
            {
                if (base.PopOverlay() == null)
                {
                    ScreenManager.KillAll(false, "Gametest");
                }
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
            base.Draw();
            CurrentWorld.Draw();

            Universal.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            Universal.SpriteBatch.Draw(Textures.Null, new Rectangle(0, (int)Universal.GameSize.Y - 120, (int)Universal.GameSize.X, 120), Color.Gray);

            //Inventory
            Universal.SpriteBatch.Draw(Textures.Null, new Rectangle(100, (int)Universal.GameSize.Y - 120, 5, 120), Color.DarkGray);
            List<ItemStack> stacks = CurrentWorld.player.Inventory.Items;
            for (int i = 0; i < stacks.Count; i++)
            {
                ItemStack stack = stacks[i];
                Universal.SpriteBatch.Draw(stack.Item.Texture, new Rectangle(120 + (80 * i), (int)Universal.GameSize.Y - 80, 64, 64), Color.White);
                string[] parts = stack.Item.Name.Split(' ');
                parts[parts.Length - 1] += " (" + stack.Count + ")";
                for (int j = 0; j < parts.Length; j++)
                    Universal.SpriteBatch.DrawString(Fonts.Arial_12, parts[j], new Vector2(120 + (80 * i), (int)Universal.GameSize.Y - (80 + ((parts.Length - j) * 20))), Color.White);
            }

            //Tasks
            Universal.SpriteBatch.Draw(Textures.Null, new Rectangle(700, (int)Universal.GameSize.Y - 120, 5, 120), Color.DarkGray);


            Universal.SpriteBatch.End();
            Universal.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            Universal.SpriteBatch.DrawString(Fonts.Arial_12, "Time Left:", new Vector2(10, (int)Universal.GameSize.Y - 100), Color.Green);
            Universal.SpriteBatch.DrawString(Fonts.Arial_12, "" + (CurrentWorld.TimeLeft / 60), new Vector2(30, (int)Universal.GameSize.Y - 60), CurrentWorld.TimeLeft > 10 ? Color.Green : Color.Red);
            Universal.SpriteBatch.End();

        }
    }
}
