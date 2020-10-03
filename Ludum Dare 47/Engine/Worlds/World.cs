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

        private List<Rectangle> _walls = new List<Rectangle>();

        private Vector2 _worldMax = new Vector2(1400, 1400);
        private Vector2 _sideBuffer = new Vector2(100, 150);

        private List<Entity> _ents = new List<Entity>();
        public Player Player;

        public World()
        {
            _walls.Add(new Rectangle(0, 0, 20, (int)_worldMax.Y));
            _walls.Add(new Rectangle(0, (int)_worldMax.Y, (int)_worldMax.X, 20));
            _walls.Add(new Rectangle((int)_worldMax.X, 0, 20, (int)_worldMax.Y));
            _walls.Add(new Rectangle(0, 0, (int)_worldMax.X, 20));
            _walls.Add(new Rectangle(500, 1250, 1000, 22));

            Player = new Player();

            _ents.Add(new DoubleJump(new Rectangle(200, 1350, 16, 16)));
            Key key = new Key(new Rectangle(300, 1300, 16, 16));
            key.Gravity = false;
            _ents.Add(key);
            _ents.Add(new Door(new Rectangle(600, 1272, 64, 128)));
            _ents.Add(new Door(new Rectangle(1100, 1272, 64, 128)));
            key = new Key(new Rectangle(700, 1300, 16, 16));
            key.Gravity = false;
            _ents.Add(key);

            Task task = new Task();
            _ents.Add(new Button(new Rectangle(900, 1350, 64, 16), false, task));
            Tasks.Add(task);
            task = new Task();
            _ents.Add(new Button(new Rectangle(20, 1300, 16, 64), true, task));
            Tasks.Add(task);

            _ents.Add(new ExitDoor(new Rectangle(1300, 1272, 64, 128), this));

        }

        public void Reset()
        {
            TimeLeft = 100 * 60;
            foreach (Entity ent in _ents)
                ent.Reset();
            Player.Reset();
        }

        public void Update()
        {
            foreach (Entity ent in _ents)
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
            if (TimeLeft == 0)
            {
                Reset();
            }
        }

        public void Draw()
        {
            float screenWidth = Universal.GameSize.X;
            float screenHeight = Universal.GameSize.Y;

            int offsetX = MathHelper.Clamp((int)((screenWidth / 2) - Player.Position.X), (int)(Universal.GameSize.X - _sideBuffer.X - _worldMax.X), (int)_sideBuffer.X);
            int offsetY = MathHelper.Clamp((int)((screenHeight / 2) - Player.Position.Y), (int)(Universal.GameSize.Y - _sideBuffer.Y - _worldMax.Y), (int)_sideBuffer.Y);

            Universal.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            foreach (Rectangle wall in _walls)
            {
                Rectangle rectMoved = new Rectangle(wall.Location, wall.Size);
                rectMoved.X += offsetX;
                rectMoved.Y += offsetY;
                Universal.SpriteBatch.Draw(Textures.Null, rectMoved, Color.Black);
            }

            foreach (Entity ent in _ents)
                if (!ent.IsDead)
                    ent.Draw(offsetX, offsetY);
            Player.Draw(offsetX, offsetY);

            Universal.SpriteBatch.Draw(Textures.Null, new Rectangle(200 + offsetX, 200 + offsetY, 40, 40), Color.Black);
            Universal.SpriteBatch.Draw(Textures.Null, new Rectangle(300 + offsetX, 100 + offsetY, 40, 40), Color.Black);
            Universal.SpriteBatch.Draw(Textures.Null, new Rectangle(-200 + offsetX, -200 + offsetY, 40, 40), Color.Black);

            Universal.SpriteBatch.End();
        }


        public void PlayerJump() => Player.Jump();
        
        public void MoveEntity(Entity ent, float xMov, float yMov)
        {
            Rectangle temp = new Rectangle((int)(ent.Position.X + xMov), ent.Position.Y, ent.Position.Width, ent.Position.Height);

            bool valid = true;

            foreach (Rectangle wall in _walls)
            {
                if (temp.Intersects(wall))
                {
                    valid = false;
                    break;
                }
            }

            if (valid)
            {
                foreach (Entity entCheck in _ents)
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

            foreach (Rectangle wall in _walls)
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
                foreach (Entity entCheck in _ents)
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
