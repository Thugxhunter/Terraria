// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Shaders.BlizzardShaderData
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
  public class BlizzardShaderData : ScreenShaderData
  {
    private Vector2 _texturePosition = Vector2.Zero;
    private float windSpeed = 0.1f;

    public BlizzardShaderData(string passName)
      : base(passName)
    {
    }

    public override void Update(GameTime gameTime)
    {
      float num = Main.windSpeedCurrent;
      if ((double) num >= 0.0 && (double) num <= 0.10000000149011612)
        num = 0.1f;
      else if ((double) num <= 0.0 && (double) num >= -0.10000000149011612)
        num = -0.1f;
      this.windSpeed = (float) ((double) num * 0.05000000074505806 + (double) this.windSpeed * 0.949999988079071);
      Vector2 direction = new Vector2(-this.windSpeed, -1f) * new Vector2(10f, 2f);
      direction.Normalize();
      direction *= new Vector2(0.8f, 0.6f);
      if (!Main.gamePaused && Main.hasFocus)
        this._texturePosition += direction * (float) gameTime.ElapsedGameTime.TotalSeconds;
      this._texturePosition.X %= 10f;
      this._texturePosition.Y %= 10f;
      this.UseDirection(direction);
      this.UseTargetPosition(this._texturePosition);
      base.Update(gameTime);
    }

    public override void Apply()
    {
      this.UseTargetPosition(this._texturePosition);
      base.Apply();
    }
  }
}
