// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.AnOutlinedDrawRenderTargetContent
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Terraria.GameContent
{
  public abstract class AnOutlinedDrawRenderTargetContent : ARenderTargetContentByRequest
  {
    protected int width = 84;
    protected int height = 84;
    public Color _borderColor = Color.White;
    private EffectPass _coloringShader;
    private RenderTarget2D _helperTarget;

    public void UseColor(Color color) => this._borderColor = color;

    protected override void HandleUseReqest(GraphicsDevice device, SpriteBatch spriteBatch)
    {
      Effect pixelShader = Main.pixelShader;
      if (this._coloringShader == null)
        this._coloringShader = pixelShader.CurrentTechnique.Passes["ColorOnly"];
      Rectangle rectangle = new Rectangle(0, 0, this.width, this.height);
      this.PrepareARenderTarget_AndListenToEvents(ref this._target, device, this.width, this.height, RenderTargetUsage.PreserveContents);
      this.PrepareARenderTarget_WithoutListeningToEvents(ref this._helperTarget, device, this.width, this.height, RenderTargetUsage.DiscardContents);
      device.SetRenderTarget(this._helperTarget);
      device.Clear(Color.Transparent);
      spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null);
      this.DrawTheContent(spriteBatch);
      spriteBatch.End();
      device.SetRenderTarget(this._target);
      device.Clear(Color.Transparent);
      spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null);
      this._coloringShader.Apply();
      int num1 = 2;
      int num2 = num1 * 2;
      for (int x = -num2; x <= num2; x += num1)
      {
        for (int y = -num2; y <= num2; y += num1)
        {
          if (Math.Abs(x) + Math.Abs(y) == num2)
            spriteBatch.Draw((Texture2D) this._helperTarget, new Vector2((float) x, (float) y), Color.Black);
        }
      }
      int num3 = num1;
      for (int x = -num3; x <= num3; x += num1)
      {
        for (int y = -num3; y <= num3; y += num1)
        {
          if (Math.Abs(x) + Math.Abs(y) == num3)
            spriteBatch.Draw((Texture2D) this._helperTarget, new Vector2((float) x, (float) y), this._borderColor);
        }
      }
      pixelShader.CurrentTechnique.Passes[0].Apply();
      spriteBatch.Draw((Texture2D) this._helperTarget, Vector2.Zero, Color.White);
      spriteBatch.End();
      device.SetRenderTarget((RenderTarget2D) null);
      this._wasPrepared = true;
    }

    internal abstract void DrawTheContent(SpriteBatch spriteBatch);
  }
}
