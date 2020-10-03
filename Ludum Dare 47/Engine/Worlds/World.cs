using EG2DCS.Engine.Globals;
using Ludum_Dare_47.Engine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum_Dare_47.Engine.Worlds
{
    class World
    {
        public int TimeLeft { get; private set; } = 100 * 60;

        public List<Task> Tasks { get; } = new List<Task>();

        private List<Rectangle> walls = new List<Rectangle>();

        private Vector2 WorldMax = new Vector2(1400, 1400);
        private Vector2 SideBuffer = new Vector2(100, 150);

        private List<Entity> ents = new List<Entity>();
        public Player player;

        public World()
        {
            walls.Add(new Rectangle(0, 0, 20, (int)WorldMax.Y));
            walls.Add(new Rectangle(0, (int)WorldMax.Y, (int)WorldMax.X, 20));
            walls.Add(new Rectangle((int)WorldMax.X, 0, 20, (int)WorldMax.Y));
            walls.Add(new Rectangle(0, 0, (int)WorldMax.X, 20));
            walls.Add(new Rectangle(500, 1250, 1000, 22));

            player = new Player();

            ents.Add(new DoubleJump(new Rectangle(200, 1350, 16, 16)));
            Key key = new Key(new Rectangle(300, 1300, 16, 16));
            key.Gravity = false;
            ents.Add(key);
            ents.Add(new Door(new Rectangle(600, 1272, 64, 128)));
            ents.Add(new Door(new Rectangle(1100, 1272, 64, 128)));
            key = new Key(new Rectangle(700, 1300, 16, 16));
            key.Gravity = false;
            ents.Add(key);

            Task task = new Task();
            ents.Add(new Button(new Rectangle(900, 1350, 64, 16), false, task));
            Tasks.Add(task);
            task = new Task();
            ents.Add(new Button(new Rectangle(20, 1300, 16, 64), true, task));
            Tasks.Add(task);

            ents.Add(new ExitDoor(new Rectangle(1300, 1272, 64, 128), this));

        }

        public void Reset()
        {
            TimeLeft = 100 * 60;
            foreach (Entity ent in ents)
                ent.Reset();
            player.Reset();
        }

        public void Update()
        {
            foreach (Entity ent in ents)
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

            foreach (Rectangle wall in walls)
            {
                Rectangle rectMoved = new Rectangle(wall.Location, wall.Size);
                rectMoved.X += offsetX;
                rectMoved.Y += offsetY;
                Universal.SpriteBatch.Draw(Textures.Null, rectMoved, Color.Black);
            }

            foreach (Entity ent in ents)
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

        public void MoveEntity(Entity ent, float xMov, float yMov)
        {
            Rectangle temp = new Rectangle((int)(ent.Position.X + xMov), ent.Position.Y, ent.Position.Width, ent.Position.Height);

            bool valid = true;

            foreach (Rectangle wall in walls)
            {
                if (temp.Intersects(wall))
                {
                    valid = false;
                    break;
                }
            }

            if (valid)
            {
                foreach (Entity entCheck in ents)
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
                ent.MoveEntity((int)xMov, 0);
            }


            temp = new Rectangle(ent.Position.X, (int)(ent.Position.Y + yMov), ent.Position.Width, ent.Position.Height);
            valid = true;

            foreach (Rectangle wall in walls)
            {
                if (temp.Intersects(wall))
                {
                    valid = false;
                    ent.SetOnGround();
                    break;
                }
            }

            if (valid)
            {
                foreach (Entity entCheck in ents)
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
        }
    }
}
