// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.RGB.DrowningShader
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
  public class DrowningShader : ChromaShader
  {
    private float _breath = 1f;

    public virtual void Update(float elapsedTime)
    {
      Player player = Main.player[Main.myPlayer];
      this._breath = (float) (player.breath * player.breathCDMax - player.breathCD) / (float) (player.breathMax * player.breathCDMax);
    }

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
        Vector4 vector4 = new Vector4(0.0f, 0.0f, 1f, 1f - this._breath);
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
      float num = (float) ((double) this._breath * 1.2000000476837158 - 0.10000000149011612);
      for (int index = 0; index < fragment.Count; ++index)
      {
        Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(index);
        Vector4 vector4 = Vector4.Zero;
        if ((double) canvasPositionOfIndex.Y > (double) num)
          vector4 = new Vector4(0.0f, 0.0f, 1f, MathHelper.Clamp((float) (((double) canvasPositionOfIndex.Y - (double) num) * 5.0), 0.0f, 1f));
        fragment.SetColor(index, vector4);
      }
    }
  }
}
