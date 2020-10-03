using EG2DCS.Engine.Globals;
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

        public int TimeLeft { get; private set; } = 100 * 60;

        public List<Task> Tasks { get; } = new List<Task>();

        public string Name { get; set; }

        public List<Wall> Walls { get; set; }
        public List<Entity> Entities { get; set; } = new List<Entity>();
        [JsonConverter(typeof(CustomVector2Converter))]
        public Vector2 WorldMax { get; set; } = new Vector2(1400, 1400);


        public Player player;

        public World()
        {
            player = new Player();

            //Task task = new Task();
            //Entities.Add(new Button(new Rectangle(900, 1350, 64, 16), false, task));
            //Tasks.Add(task);
            //task = new Task();
            //Entities.Add(new Button(new Rectangle(20, 1300, 16, 64), true, task));
            //Tasks.Add(task);
        }

        public void Setup()
        {
            foreach (Entity ent in Entities)
                ent.Setup(this);
        }

        public void Reset()
        {
            TimeLeft = 100 * 60;
            foreach (Entity ent in Entities)
                ent.Reset();
            player.Reset();
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
            player.Update();
            MoveEntity(player, player.Velocity.X, player.Velocity.Y);

            TimeLeft--;
            if (TimeLeft == 0)
            {
                Reset();
            }
        }

        public void Draw()
        {
            float screenWidth = Universal.GameSize.X;
            float screenHeight = Universal.GameSize.Y;

            int offsetX = MathHelper.Clamp((int)((screenWidth / 2) - player.Position.X), (int)(Universal.GameSize.X - SideBuffer.X - WorldMax.X), (int)SideBuffer.X);
            int offsetY = MathHelper.Clamp((int)((screenHeight / 2) - player.Position.Y), (int)(Universal.GameSize.Y - SideBuffer.Y - WorldMax.Y), (int)SideBuffer.Y);

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
            player.Draw(offsetX, offsetY);

            Universal.SpriteBatch.Draw(Textures.Null, new Rectangle(200 + offsetX, 200 + offsetY, 40, 40), Color.Black);
            Universal.SpriteBatch.Draw(Textures.Null, new Rectangle(300 + offsetX, 100 + offsetY, 40, 40), Color.Black);
            Universal.SpriteBatch.Draw(Textures.Null, new Rectangle(-200 + offsetX, -200 + offsetY, 40, 40), Color.Black);

            Universal.SpriteBatch.End();
        }


        public void PlayerJump()
        {
            player.Jump();
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
    }
}
