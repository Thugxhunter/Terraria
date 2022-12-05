// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Shaders.BloodMoonScreenShaderData
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
  public class BloodMoonScreenShaderData : ScreenShaderData
  {
    public BloodMoonScreenShaderData(string passName)
      : base(passName)
    {
    }

    public override void Update(GameTime gameTime)
    {
      float num = 1f - Utils.SmoothStep((float) Main.worldSurface + 50f, (float) Main.rockLayer + 100f, (float) (((double) Main.screenPosition.Y + (double) (Main.screenHeight / 2)) / 16.0));
      if (Main.remixWorld)
        num = Utils.SmoothStep((float) (Main.rockLayer + Main.worldSurface) / 2f, (float) Main.rockLayer, (float) (((double) Main.screenPosition.Y + (double) (Main.screenHeight / 2)) / 16.0));
      if ((double) Main.shimmerAlpha > 0.0)
        num *= 1f - Main.shimmerAlpha;
      this.UseOpacity(num * 0.75f);
    }
  }
}
