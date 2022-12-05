// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ARenderTargetContentByRequest
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Graphics;
using System;

namespace Terraria.GameContent
{
  public abstract class ARenderTargetContentByRequest : INeedRenderTargetContent
  {
    protected RenderTarget2D _target;
    protected bool _wasPrepared;
    private bool _wasRequested;

    public bool IsReady => this._wasPrepared;

    public void Request() => this._wasRequested = true;

    public RenderTarget2D GetTarget() => this._target;

    public void PrepareRenderTarget(GraphicsDevice device, SpriteBatch spriteBatch)
    {
      this._wasPrepared = false;
      if (!this._wasRequested)
        return;
      this._wasRequested = false;
      this.HandleUseReqest(device, spriteBatch);
    }

    protected abstract void HandleUseReqest(GraphicsDevice device, SpriteBatch spriteBatch);

    protected void PrepareARenderTarget_AndListenToEvents(
      ref RenderTarget2D target,
      GraphicsDevice device,
      int neededWidth,
      int neededHeight,
      RenderTargetUsage usage)
    {
      if (target != null && !target.IsDisposed && target.Width == neededWidth && target.Height == neededHeight)
        return;
      if (target != null)
      {
        target.ContentLost -= new EventHandler<EventArgs>(this.target_ContentLost);
        target.Disposing -= new EventHandler<EventArgs>(this.target_Disposing);
      }
      target = new RenderTarget2D(device, neededWidth, neededHeight, false, device.PresentationParameters.BackBufferFormat, DepthFormat.None, 0, usage);
      target.ContentLost += new EventHandler<EventArgs>(this.target_ContentLost);
      target.Disposing += new EventHandler<EventArgs>(this.target_Disposing);
    }

    private void target_Disposing(object sender, EventArgs e)
    {
      this._wasPrepared = false;
      this._target = (RenderTarget2D) null;
    }

    private void target_ContentLost(object sender, EventArgs e) => this._wasPrepared = false;

    public void Reset()
    {
      this._wasPrepared = false;
      this._wasRequested = false;
      this._target = (RenderTarget2D) null;
    }

    protected void PrepareARenderTarget_WithoutListeningToEvents(
      ref RenderTarget2D target,
      GraphicsDevice device,
      int neededWidth,
      int neededHeight,
      RenderTargetUsage usage)
    {
      if (target != null && !target.IsDisposed && target.Width == neededWidth && target.Height == neededHeight)
        return;
      target = new RenderTarget2D(device, neededWidth, neededHeight, false, device.PresentationParameters.BackBufferFormat, DepthFormat.None, 0, usage);
    }
  }
}
