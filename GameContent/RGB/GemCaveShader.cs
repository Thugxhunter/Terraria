// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.RGB.GemCaveShader
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;
using System;

namespace Terraria.GameContent.RGB
{
  public class GemCaveShader : ChromaShader
  {
    private readonly Vector4 _primaryColor;
    private readonly Vector4 _secondaryColor;
    private readonly Vector4[] _gemColors;
    public float CycleTime;
    public float ColorRarity;
    public float TimeRate;

    public GemCaveShader(Color primaryColor, Color secondaryColor, Vector4[] gemColors)
    {
      this._primaryColor = primaryColor.ToVector4();
      this._secondaryColor = secondaryColor.ToVector4();
      this._gemColors = gemColors;
    }

    [RgbProcessor]
    private void ProcessHighDetail(
      RgbDevice device,
      Fragment fragment,
      EffectDetailLevel quality,
      float time)
    {
      time *= this.TimeRate;
      float num1 = time % 1f;
      int num2 = (double) time % 2.0 > 1.0 ? 1 : 0;
      Vector4 vector4_1 = num2 != 0 ? this._secondaryColor : this._primaryColor;
      Vector4 vector4_2 = num2 != 0 ? this._primaryColor : this._secondaryColor;
      float num3 = num1 * 1.2f;
      for (int index = 0; index < fragment.Count; ++index)
      {
        Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(index);
        Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(index);
        float staticNoise = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.5f + new Vector2(0.0f, time * 0.5f));
        Vector4 vector4_3 = vector4_1;
        float num4 = staticNoise + num3;
        if ((double) num4 > 0.99900001287460327)
        {
          float amount = MathHelper.Clamp((float) (((double) num4 - 0.99900001287460327) / 0.20000000298023224), 0.0f, 1f);
          vector4_3 = Vector4.Lerp(vector4_3, vector4_2, amount);
        }
        float amount1 = Math.Max(0.0f, (float) (1.0 - (double) NoiseHelper.GetDynamicNoise(gridPositionOfIndex.X, gridPositionOfIndex.Y, time / this.CycleTime) * (double) this.ColorRarity));
        Vector4 vector4_4 = Vector4.Lerp(vector4_3, this._gemColors[((gridPositionOfIndex.Y * 47 + gridPositionOfIndex.X) % this._gemColors.Length + this._gemColors.Length) % this._gemColors.Length], amount1);
        fragment.SetColor(index, vector4_4);
        fragment.SetColor(index, vector4_4);
      }
    }
  }
}
