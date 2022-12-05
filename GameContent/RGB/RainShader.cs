// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.RGB.RainShader
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;
using System;

namespace Terraria.GameContent.RGB
{
  public class RainShader : ChromaShader
  {
    private bool _inBloodMoon;

    public virtual void Update(float elapsedTime) => this._inBloodMoon = Main.bloodMoon;

    [RgbProcessor]
    private void ProcessHighDetail(
      RgbDevice device,
      Fragment fragment,
      EffectDetailLevel quality,
      float time)
    {
      Vector4 vector4_1 = !this._inBloodMoon ? new Vector4(0.0f, 0.0f, 1f, 1f) : new Vector4(1f, 0.0f, 0.0f, 1f);
      Vector4 vector4_2 = new Vector4(0.0f, 0.0f, 0.0f, 0.75f);
      for (int index = 0; index < fragment.Count; ++index)
      {
        Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(index);
        Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(index);
        float num = (float) (((double) NoiseHelper.GetStaticNoise(gridPositionOfIndex.X) * 10.0 + (double) time) % 10.0) - canvasPositionOfIndex.Y;
        Vector4 vector4_3 = vector4_2;
        if ((double) num > 0.0)
        {
          float amount = Math.Max(0.0f, 1.2f - num);
          if ((double) num < 0.20000000298023224)
            amount = num * 5f;
          vector4_3 = Vector4.Lerp(vector4_3, vector4_1, amount);
        }
        fragment.SetColor(index, vector4_3);
      }
    }
  }
}
