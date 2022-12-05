// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.RGB.BlizzardShader
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
  public class BlizzardShader : ChromaShader
  {
    private readonly Vector4 _backColor = new Vector4(0.1f, 0.1f, 0.3f, 1f);
    private readonly Vector4 _frontColor = new Vector4(1f, 1f, 1f, 1f);
    private readonly float _timeScaleX;
    private readonly float _timeScaleY;

    public BlizzardShader(Vector4 frontColor, Vector4 backColor, float panSpeedX, float panSpeedY)
    {
      this._frontColor = frontColor;
      this._backColor = backColor;
      this._timeScaleX = panSpeedX;
      this._timeScaleY = panSpeedY;
    }

    [RgbProcessor]
    private void ProcessHighDetail(
      RgbDevice device,
      Fragment fragment,
      EffectDetailLevel quality,
      float time)
    {
      if (quality == null)
        time *= 0.25f;
      for (int index = 0; index < fragment.Count; ++index)
      {
        float staticNoise = NoiseHelper.GetStaticNoise(fragment.GetCanvasPositionOfIndex(index) * new Vector2(0.2f, 0.4f) + new Vector2(time * this._timeScaleX, time * this._timeScaleY));
        Vector4 vector4 = Vector4.Lerp(this._backColor, this._frontColor, staticNoise * staticNoise);
        fragment.SetColor(index, vector4);
      }
    }
  }
}
