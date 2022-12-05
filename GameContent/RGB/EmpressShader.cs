// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.RGB.EmpressShader
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;
using System;

namespace Terraria.GameContent.RGB
{
  public class EmpressShader : ChromaShader
  {
    private static readonly Vector4[] _colors = new Vector4[12]
    {
      new Vector4(1f, 0.1f, 0.1f, 1f),
      new Vector4(1f, 0.5f, 0.1f, 1f),
      new Vector4(1f, 1f, 0.1f, 1f),
      new Vector4(0.5f, 1f, 0.1f, 1f),
      new Vector4(0.1f, 1f, 0.1f, 1f),
      new Vector4(0.1f, 1f, 0.5f, 1f),
      new Vector4(0.1f, 1f, 1f, 1f),
      new Vector4(0.1f, 0.5f, 1f, 1f),
      new Vector4(0.1f, 0.1f, 1f, 1f),
      new Vector4(0.5f, 0.1f, 1f, 1f),
      new Vector4(1f, 0.1f, 1f, 1f),
      new Vector4(1f, 0.1f, 0.5f, 1f)
    };

    [RgbProcessor]
    private void ProcessHighDetail(
      RgbDevice device,
      Fragment fragment,
      EffectDetailLevel quality,
      float time)
    {
      float num1 = time * 2f;
      for (int index = 0; index < fragment.Count; ++index)
      {
        Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(index);
        Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(index);
        double num2 = (double) MathHelper.Max(0.0f, (float) Math.Cos(((double) NoiseHelper.GetStaticNoise(gridPositionOfIndex.X) + (double) num1) * 6.2831854820251465 * 0.20000000298023224));
        Color color = Color.Lerp(Color.Black, Color.Indigo, 0.5f);
        Vector4 vector4_1 = color.ToVector4();
        Math.Max(0.0f, (float) Math.Sin((double) Main.GlobalTimeWrappedHourly * 2.0 + (double) canvasPositionOfIndex.X * 1.0));
        float amount1 = 0.0f;
        Vector4 vector4_2 = Vector4.Lerp(vector4_1, new Vector4(1f, 0.1f, 0.1f, 1f), amount1);
        double x = (double) canvasPositionOfIndex.X;
        float amount2 = (float) ((num2 + x + (double) canvasPositionOfIndex.Y) % 1.0);
        if ((double) amount2 > 0.0)
        {
          int num3 = (gridPositionOfIndex.X + gridPositionOfIndex.Y) % EmpressShader._colors.Length;
          if (num3 < 0)
          {
            int num4 = num3 + EmpressShader._colors.Length;
          }
          color = Main.hslToRgb((float) ((((double) canvasPositionOfIndex.X + (double) canvasPositionOfIndex.Y) * 0.15000000596046448 + (double) time * 0.10000000149011612) % 1.0), 1f, 0.5f);
          Vector4 vector4_3 = color.ToVector4();
          vector4_2 = Vector4.Lerp(vector4_2, vector4_3, amount2);
        }
        fragment.SetColor(index, vector4_2);
      }
    }

    private static void RedsVersion(Fragment fragment, float time)
    {
      time *= 3f;
      for (int index1 = 0; index1 < fragment.Count; ++index1)
      {
        Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(index1);
        Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(index1);
        float num = (float) (((double) NoiseHelper.GetStaticNoise(gridPositionOfIndex.X) * 7.0 + (double) time * 0.40000000596046448) % 7.0) - canvasPositionOfIndex.Y;
        Vector4 vector4 = new Vector4();
        if ((double) num > 0.0)
        {
          float amount = Math.Max(0.0f, 1.4f - num);
          if ((double) num < 0.40000000596046448)
            amount = num / 0.4f;
          int index2 = (gridPositionOfIndex.X + EmpressShader._colors.Length + (int) ((double) time / 6.0)) % EmpressShader._colors.Length;
          vector4 = Vector4.Lerp(vector4, EmpressShader._colors[index2], amount);
        }
        fragment.SetColor(index1, vector4);
      }
    }
  }
}
