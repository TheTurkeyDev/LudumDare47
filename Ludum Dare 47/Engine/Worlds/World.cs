using EG2DCS.Engine.Globals;
using EG2DCS.Engine.Screen_Manager;
using Ludum_Dare_47.Engine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum_Dare_47.Engine.Worlds
{
    public class World
    {
        private static Vector2 SideBuffer = new Vector2(100, 150);
        public int TimeLeft { get; private set; }

        public List<Task> Tasks { get; } = new List<Task>();

        public string Id { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public string NextLevel { get; set; }
        public int TimeLimit { get; set; }

        public List<Wall> Walls { get; set; }
        public List<Entity> Entities { get; set; } = new List<Entity>();
        [JsonConverter(typeof(CustomVector2Converter))]
        public Vector2 WorldMax { get; set; } = new Vector2(1400, 1400);

        [JsonConverter(typeof(CustomPlayerConverter))]
        public Player Player { get; set; }

        public GameScreen GameScreen { get; set; }

        public World()
        {

        }

        public void Setup()
        {
            foreach (Entity ent in Entities)
                ent.Setup(this);
            Player.Setup(this);
            TimeLeft = (TimeLimit - 1) * 60;
        }

        public void Reset()
        {
            TimeLeft = (TimeLimit - 1) * 60;
            foreach (Entity ent in Entities)
                ent.Reset();
            Player.Reset();
        }

        public void Update()
        {
            foreach (Entity ent in Entities)
            {
                if (!ent.IsDead)
                {
                    ent.Update();
                    MoveEntity(ent, ent.Velocity.X, ent.Velocity.Y);
                }
            }
            Player.Update();
            MoveEntity(Player, Player.Velocity.X, Player.Velocity.Y);

            TimeLeft--;
            if (TimeLeft <= 30)
            {
                Reset();
            }
        }

        public void Draw()
        {
            float screenWidth = Universal.GameSize.X;
            float screenHeight = Universal.GameSize.Y;

            int offsetX = MathHelper.Clamp((int)((screenWidth / 2) - Player.Position.X), (int)(Universal.GameSize.X - SideBuffer.X - WorldMax.X), (int)SideBuffer.X);
            int offsetY = MathHelper.Clamp((int)((screenHeight / 2) - Player.Position.Y), (int)(Universal.GameSize.Y - SideBuffer.Y - WorldMax.Y), (int)SideBuffer.Y);

            Universal.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            foreach (Wall wall in Walls)
            {
                Rectangle rectMoved = new Rectangle(wall.Rectangle.Location, wall.Rectangle.Size);
                rectMoved.X += offsetX;
                rectMoved.Y += offsetY;
                Universal.SpriteBatch.Draw(Textures.Null, rectMoved, wall.Color);
            }

            foreach (Entity ent in Entities)
                if (!ent.IsDead)
                    ent.Draw(offsetX, offsetY);
            Player.Draw(offsetX, offsetY);

            Universal.SpriteBatch.End();
        }


        public void PlayerJump()
        {
            Player.Jump();
        }

        public bool MoveEntity(Entity ent, float xMov, float yMov)
        {
            Rectangle temp = new Rectangle((int)(ent.Position.X + xMov), ent.Position.Y, ent.Position.Width, ent.Position.Height);

            bool moved = false;
            bool valid = true;

            foreach (Wall wall in Walls)
            {
                if (temp.Intersects(wall.Rectangle))
                {
                    valid = false;
                    break;
                }
            }

            if (valid)
            {
                foreach (Entity entCheck in Entities)
                {
                    if (!entCheck.IsDead && temp.Intersects(entCheck.Position) && !entCheck.OnCollide(ent))
                    {
                        valid = false;
                        break;
                    }
                }
            }

            if (valid)
            {
                moved = true;
                ent.MoveEntity((int)xMov, 0);
            }


            temp = new Rectangle(ent.Position.X, (int)(ent.Position.Y + yMov), ent.Position.Width, ent.Position.Height);
            valid = true;

            foreach (Wall wall in Walls)
            {
                if (temp.Intersects(wall.Rectangle))
                {
                    valid = false;
                    ent.SetOnGround();
                    break;
                }
            }

            if (valid)
            {
                foreach (Entity entCheck in Entities)
                {
                    if (!entCheck.IsDead && temp.Intersects(entCheck.Position) && !entCheck.OnCollide(ent))
                    {
                        valid = false;
                        break;
                    }
                }
            }

            if (valid)
            {
                ent.MoveEntity(0, (int)yMov);
            }

            return moved;
        }

        public void Advance()
        {
            GameScreen.AdvanceToLevel(NextLevel);
        }
    }
}
