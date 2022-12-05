// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.RGB.JungleShader
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;
using System;

namespace Terraria.GameContent.RGB
{
  public class JungleShader : ChromaShader
  {
    private readonly Vector4 _backgroundColor = new Color(40, 80, 0).ToVector4();
    private readonly Vector4 _sporeColor = new Color((int) byte.MaxValue, (int) byte.MaxValue, 0).ToVector4();
    private readonly Vector4[] _flowerColors = new Vector4[5]
    {
      Color.Yellow.ToVector4(),
      Color.Pink.ToVector4(),
      Color.Purple.ToVector4(),
      Color.Red.ToVector4(),
      Color.Blue.ToVector4()
    };

    [RgbProcessor]
    private void ProcessLowDetail(
      RgbDevice device,
      Fragment fragment,
      EffectDetailLevel quality,
      float time)
    {
      for (int index = 0; index < fragment.Count; ++index)
      {
        Vector4 vector4 = Vector4.Lerp(this._backgroundColor, this._sporeColor, NoiseHelper.GetDynamicNoise(fragment.GetCanvasPositionOfIndex(index) * 0.3f, time / 5f));
        fragment.SetColor(index, vector4);
      }
    }

    [RgbProcessor]
    private void ProcessHighDetail(
      RgbDevice device,
      Fragment fragment,
      EffectDetailLevel quality,
      float time)
    {
      bool flag = device.Type == null || device.Type == 6;
      for (int index = 0; index < fragment.Count; ++index)
      {
        Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(index);
        Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(index);
        Vector4 vector4 = Vector4.Lerp(this._backgroundColor, this._sporeColor, Math.Max(0.0f, (float) (1.0 - (double) NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.3f, time / 5f) * 2.5)));
        if (flag)
        {
          float amount = Math.Max(0.0f, (float) (1.0 - (double) NoiseHelper.GetDynamicNoise(gridPositionOfIndex.X, gridPositionOfIndex.Y, time / 100f) * 20.0));
          vector4 = Vector4.Lerp(vector4, this._flowerColors[((gridPositionOfIndex.Y * 47 + gridPositionOfIndex.X) % 5 + 5) % 5], amount);
        }
        fragment.SetColor(index, vector4);
      }
    }
  }
}
