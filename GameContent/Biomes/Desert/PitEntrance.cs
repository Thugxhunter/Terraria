// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.Desert.PitEntrance
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;

namespace Terraria.GameContent.Biomes.Desert
{
  public static class PitEntrance
  {
    public static void Place(DesertDescription description)
    {
      int holeRadius = WorldGen.genRand.Next(6, 9);
      Point center = description.CombinedArea.Center;
      center.Y = (int) description.Surface[center.X];
      PitEntrance.PlaceAt(description, center, holeRadius);
    }

    private static void PlaceAt(DesertDescription description, Point position, int holeRadius)
    {
      for (int index = -holeRadius - 3; index < holeRadius + 3; ++index)
      {
        int j = (int) description.Surface[index + position.X];
        while (true)
        {
          int num1 = j;
          Rectangle rectangle = description.Hive;
          int num2 = rectangle.Top + 10;
          if (num1 <= num2)
          {
            double num3 = (double) (j - (int) description.Surface[index + position.X]);
            rectangle = description.Hive;
            int top1 = rectangle.Top;
            rectangle = description.Desert;
            int top2 = rectangle.Top;
            double num4 = (double) (top1 - top2);
            double yProgress = Utils.Clamp<double>(num3 / num4, 0.0, 1.0);
            int num5 = (int) (PitEntrance.GetHoleRadiusScaleAt(yProgress) * (double) holeRadius);
            if (Math.Abs(index) < num5)
              Main.tile[index + position.X, j].ClearEverything();
            else if (Math.Abs(index) < num5 + 3 && yProgress > 0.35)
              Main.tile[index + position.X, j].ResetToType((ushort) 397);
            double num6 = Math.Abs((double) index / (double) holeRadius);
            double num7 = num6 * num6;
            if (Math.Abs(index) < num5 + 3 && (double) (j - position.Y) > 15.0 - 3.0 * num7)
            {
              Main.tile[index + position.X, j].wall = (ushort) 187;
              WorldGen.SquareWallFrame(index + position.X, j - 1);
              WorldGen.SquareWallFrame(index + position.X, j);
            }
            ++j;
          }
          else
            break;
        }
      }
      holeRadius += 4;
      for (int index1 = -holeRadius; index1 < holeRadius; ++index1)
      {
        int num8 = holeRadius - Math.Abs(index1);
        int num9 = Math.Min(10, num8 * num8);
        for (int index2 = 0; index2 < num9; ++index2)
          Main.tile[index1 + position.X, index2 + (int) description.Surface[index1 + position.X]].ClearEverything();
      }
    }

    private static double GetHoleRadiusScaleAt(double yProgress) => yProgress < 0.6 ? 1.0 : (1.0 - PitEntrance.SmootherStep((yProgress - 0.6) / 0.4)) * 0.5 + 0.5;

    private static double SmootherStep(double delta)
    {
      delta = Utils.Clamp<double>(delta, 0.0, 1.0);
      return 1.0 - Math.Cos(delta * 3.1415927410125732) * 0.5 - 0.5;
    }
  }
}
