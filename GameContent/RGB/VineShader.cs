// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.RGB.VineShader
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
  public class VineShader : ChromaShader
  {
    private readonly Vector4 _backgroundColor = new Color(46, 17, 6).ToVector4();
    private readonly Vector4 _vineColor = Color.Green.ToVector4();

    [RgbProcessor]
    private void ProcessLowDetail(
      RgbDevice device,
      Fragment fragment,
      EffectDetailLevel quality,
      float time)
    {
      for (int index = 0; index < fragment.Count; ++index)
      {
        fragment.GetCanvasPositionOfIndex(index);
        fragment.SetColor(index, this._backgroundColor);
      }
    }

    [RgbProcessor]
    private void ProcessHighDetail(
      RgbDevice device,
      Fragment fragment,
      EffectDetailLevel quality,
      float time)
    {
      for (int index = 0; index < fragment.Count; ++index)
      {
        Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(index);
        Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(index);
        float num1 = (float) (((double) NoiseHelper.GetStaticNoise(gridPositionOfIndex.X) * 10.0 + (double) time * 0.40000000596046448) % 10.0);
        float num2 = 1f;
        if ((double) num1 > 1.0)
        {
          num2 = 1f - MathHelper.Clamp((float) (((double) num1 - 0.40000000596046448 - 1.0) / 0.40000000596046448), 0.0f, 1f);
          num1 = 1f;
        }
        float num3 = num1 - canvasPositionOfIndex.Y / 1f;
        Vector4 vector4 = this._backgroundColor;
        if ((double) num3 > 0.0)
        {
          float num4 = 1f;
          if ((double) num3 < 0.20000000298023224)
            num4 = num3 / 0.2f;
          vector4 = Vector4.Lerp(this._backgroundColor, this._vineColor, num4 * num2);
        }
        fragment.SetColor(index, vector4);
      }
    }
  }
}
