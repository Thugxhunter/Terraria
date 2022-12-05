// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.RGB.CultistShader
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;
using System;

namespace Terraria.GameContent.RGB
{
  public class CultistShader : ChromaShader
  {
    private readonly Vector4 _lightningDarkColor = new Color(23, 11, 23).ToVector4();
    private readonly Vector4 _lightningBrightColor = new Color(249, 140, (int) byte.MaxValue).ToVector4();
    private readonly Vector4 _fireDarkColor = Color.Red.ToVector4();
    private readonly Vector4 _fireBrightColor = new Color((int) byte.MaxValue, 196, 0).ToVector4();
    private readonly Vector4 _iceDarkColor = new Color(4, 4, 148).ToVector4();
    private readonly Vector4 _iceBrightColor = new Color(208, 233, (int) byte.MaxValue).ToVector4();
    private readonly Vector4 _backgroundColor = Color.Black.ToVector4();

    [RgbProcessor]
    private void ProcessHighDetail(
      RgbDevice device,
      Fragment fragment,
      EffectDetailLevel quality,
      float time)
    {
      time *= 2f;
      for (int index = 0; index < fragment.Count; ++index)
      {
        Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(index);
        Vector4 backgroundColor = this._backgroundColor;
        float d = time * 0.5f + canvasPositionOfIndex.X + canvasPositionOfIndex.Y;
        float amount = MathHelper.Clamp((float) (Math.Cos((double) d) * 2.0 + 2.0), 0.0f, 1f);
        float num = (float) (((double) d + 3.1415927410125732) % 18.849555969238281);
        Vector4 vector4_1;
        if ((double) num < 6.2831854820251465)
        {
          float staticNoise = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.3f + new Vector2(12.5f, time * 0.2f));
          vector4_1 = Vector4.Lerp(this._fireDarkColor, this._fireBrightColor, MathHelper.Clamp(Math.Max(0.0f, (float) (1.0 - (double) staticNoise * (double) staticNoise * 4.0 * (double) staticNoise)), 0.0f, 1f));
        }
        else
          vector4_1 = (double) num >= 12.566370964050293 ? Vector4.Lerp(this._lightningDarkColor, this._lightningBrightColor, Math.Max(0.0f, (float) (1.0 - 5.0 * (Math.Sin((double) NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.15f, time * 0.05f) * 15.0) * 0.5 + 0.5)))) : Vector4.Lerp(this._iceDarkColor, this._iceBrightColor, Math.Max(0.0f, (float) (1.0 - (double) NoiseHelper.GetDynamicNoise(new Vector2((float) (((double) canvasPositionOfIndex.X + (double) canvasPositionOfIndex.Y) * 0.20000000298023224), 0.0f), time / 5f) * 1.5)));
        Vector4 vector4_2 = Vector4.Lerp(backgroundColor, vector4_1, amount);
        fragment.SetColor(index, vector4_2);
      }
    }
  }
}
