using EG2DCS.Engine.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EG2DCS.Engine.Add_Ons
{
    class DrawLive
    {
        public static RenderTarget2D Screen { get; set; }
        public static GraphicsDeviceManager Freedraw { get; set; }
        public static SpriteBatch Livedraw { get; set; }
        public static Texture2D PermGFX { get; set; }

        public static void Send(Texture2D tex)
        {
            Freedraw = Universal.Graphics;
            Screen = new RenderTarget2D(Freedraw.GraphicsDevice, tex.Width, tex.Height);
            Freedraw.GraphicsDevice.SetRenderTarget(Screen);
            Livedraw = new SpriteBatch(Freedraw.GraphicsDevice);
            Livedraw.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            Livedraw.Draw(tex, new Rectangle(0, 0, tex.Width, tex.Height), new Color(255, 255, 255));
            Livedraw.End();
        }

        public static void Modify(Texture2D tex, Rectangle Drawto, Rectangle Drawfrom, Color col)
        {
            Livedraw.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            Livedraw.Draw(tex, Drawto, Drawfrom, col);
            Livedraw.End();
        }
        public static Texture2D Retrieve()
        {
            Freedraw.GraphicsDevice.SetRenderTarget(null);
            Color[] col = new Color[Screen.Width * Screen.Height - 1];
            Screen.GetData(col);
            PermGFX = new Texture2D(Universal.Graphics.GraphicsDevice, Screen.Width, Screen.Height);
            PermGFX.SetData(col);
            Screen.Dispose();
            Livedraw.Dispose();
            return PermGFX;
        }
        public static Texture2D Transparency(Texture2D td2, Color col)
        {
            Color[] colo = new Color[Screen.Width * Screen.Height - 1];
            td2.GetData(colo);
            for (int q = 0; q < colo.Length - 1; q++)
            {
                if (colo[q] == col)
                {
                    colo[q] = Color.Transparent;
                }
            }
            td2.SetData(colo);
            return td2;
        }
    }
}
