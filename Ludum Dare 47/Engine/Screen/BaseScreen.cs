using EG2DCS.Engine.Globals;
using EG2DCS.Engine.Overlay;
using EG2DCS.Engine.Toast;
using EG2DCS.Engine.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace EG2DCS.Engine.Screen_Manager
{
    public abstract class BaseScreen : KeyListener
    {
        public string Name = "N/a";
        public ScreenState State;
        public ScreenState LastState;
        public float Position;
        public bool Focused;
        public bool GrabFocus;
        public bool Overridable = true;

        public IFocusable FocusedWidget;
        public KeyListener CachedListner;

        private List<BaseOverlay> Overlays { get; } = new List<BaseOverlay>();
        private List<BaseToast> Toasts { get; } = new List<BaseToast>();

        private List<Widget> Widgets { get; } = new List<Widget>();
        private List<Widget> Hovered { get; } = new List<Widget>();

        public virtual void HandleInput()
        {
            if (Overlays.Count > 0)
            {
                Overlays[0].HandleInput();
            }

            MouseState mouseState = Mouse.GetState();
            Point point = mouseState.Position;
            foreach (Widget widget in Widgets)
            {
                if (widget.Rectangle.Contains(point) && !Hovered.Contains(widget))
                {
                    widget.OnHover();
                    Hovered.Add(widget);
                }
                else if (!widget.Rectangle.Contains(point) && Hovered.Contains(widget))
                {
                    widget.OnUnHover();
                    Hovered.Remove(widget);
                }
            }
        }
        public virtual void Update()
        {
            for (int i = Overlays.Count - 1; i >= 0; i--)
            {
                Overlays[i].Update();
            }

            for (int i = Toasts.Count - 1; i >= 0; i--)
            {
                BaseToast toast = Toasts[i];
                toast.Update();
                if (toast.IsComplete())
                {
                    Toasts.RemoveAt(i);
                }
            }

            for (int i = Widgets.Count - 1; i >= 0; i--)
            {
                Widget widget = Widgets[i];
                widget.Update();
            }
        }
        public virtual void Draw()
        {
            for (int i = Overlays.Count - 1; i >= 0; i--)
            {
                Universal.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
                Overlays[i].Draw();
                Universal.SpriteBatch.End();
            }

            for (int i = Toasts.Count - 1; i >= 0; i--)
            {
                Universal.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
                Toasts[i].Draw();
                Universal.SpriteBatch.End();
            }

            for (int i = Widgets.Count - 1; i >= 0; i--)
            {
                Universal.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
                Widgets[i].Draw();
                Universal.SpriteBatch.End();
            }
        }
        public virtual void Remove()
        {
            for (int i = Overlays.Count - 1; i >= 0; i--)
            {
                Overlays[i].Remove();
            }
            Overlays.Clear();
            for (int i = Toasts.Count - 1; i >= 0; i--)
            {
                Toasts[i].Remove();
            }
            Toasts.Clear();
            for (int i = Widgets.Count - 1; i >= 0; i--)
            {
                Widgets[i].Remove();
            }
            Widgets.Clear();
            State = ScreenState.Shutdown;
        }

        public virtual void OnClick(bool left, int x, int y)
        {
            bool focusChange = false;
            Point point = new Point(x, y);
            foreach (Widget widget in Widgets)
            {
                if (widget.Rectangle.Contains(point))
                {
                    if (widget is IFocusable)
                    {
                        focusChange = true;
                        if (FocusedWidget != widget)
                        {
                            if (FocusedWidget != null)
                                FocusedWidget.OnUnFocus();
                            FocusedWidget = (IFocusable)widget;
                            CachedListner = Input.getCurrentKeyListener();
                            Input.setCurrentKeyListener(this);

                            FocusedWidget.OnFocus();
                        }
                    }
                    widget.OnClick(left);
                }
            }

            if (!focusChange && FocusedWidget != null)
            {
                FocusedWidget.OnUnFocus();
                FocusedWidget = null;
                Input.setCurrentKeyListener(CachedListner);
                CachedListner = null;
            }
        }

        public virtual void OnUnClick(int x, int y)
        {

        }

        public void PushOverlay(BaseOverlay overlay)
        {
            Overlays.Insert(0, overlay);
        }

        public BaseOverlay PopOverlay()
        {
            if (Overlays.Count == 0)
                return null;

            BaseOverlay overlay = Overlays[0];
            Overlays.Remove(overlay);
            return overlay;
        }

        public void PushToast(BaseToast toast)
        {
            Toasts.Insert(0, toast);
            toast.Start();
        }

        public void AddWidget(Widget widget)
        {
            Widgets.Add(widget);
        }

        public virtual bool onKeyPress(Keys key)
        {
            if (FocusedWidget != null)
                return FocusedWidget.OnKeyPress(key);
            return false;
        }

        public virtual bool onKeyRelease(Keys key)
        {
            if (FocusedWidget != null)
                return FocusedWidget.OnKeyRelease(key);
            return false;
        }
    }
}
