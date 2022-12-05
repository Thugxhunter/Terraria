// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.Desert.SandMound
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;

namespace Terraria.GameContent.Biomes.Desert
{
  public static class SandMound
  {
    public static void Place(DesertDescription description)
    {
      Rectangle desert1 = description.Desert with
      {
        Height = Math.Min(description.Desert.Height, description.Hive.Height / 2)
      };
      Rectangle desert2 = description.Desert with
      {
        Y = desert1.Bottom,
        Height = Math.Max(0, description.Desert.Bottom - desert1.Bottom)
      };
      SurfaceMap surface = description.Surface;
      int num1 = 0;
      int num2 = 0;
      for (int index1 = -5; index1 < desert1.Width + 5; ++index1)
      {
        double num3 = Utils.Clamp<double>(Math.Abs((double) (index1 + 5) / (double) (desert1.Width + 10)) * 2.0 - 1.0, -1.0, 1.0);
        if (index1 % 3 == 0)
          num1 = Utils.Clamp<int>(num1 + WorldGen.genRand.Next(-1, 2), -10, 10);
        num2 = Utils.Clamp<int>(num2 + WorldGen.genRand.Next(-1, 2), -10, 10);
        double num4 = Math.Sqrt(1.0 - num3 * num3 * num3 * num3);
        int num5 = desert1.Bottom - (int) (num4 * (double) desert1.Height) + num1;
        if (Math.Abs(num3) < 1.0)
        {
          double num6 = Utils.UnclampedSmoothStep(0.5, 0.8, Math.Abs(num3));
          double num7 = num6 * num6 * num6;
          int num8 = Math.Min(10 + (int) ((double) desert1.Top - num7 * 20.0) + num2, num5);
          for (int index2 = (int) surface[index1 + desert1.X] - 1; index2 < num8; ++index2)
          {
            int index3 = index1 + desert1.X;
            int index4 = index2;
            Main.tile[index3, index4].active(false);
            Main.tile[index3, index4].wall = (ushort) 0;
          }
        }
        SandMound.PlaceSandColumn(index1 + desert1.X, num5, desert2.Bottom - num5);
      }
    }

    private static void PlaceSandColumn(int startX, int startY, int height)
    {
      for (int index = startY + height - 1; index >= startY; --index)
      {
        int i = startX;
        int j = index;
        Tile tile1 = Main.tile[i, j];
        if (!WorldGen.remixWorldGen)
          tile1.liquid = (byte) 0;
        Tile tile2 = Main.tile[i, j + 1];
        Tile tile3 = Main.tile[i, j + 2];
        tile1.type = (ushort) 53;
        tile1.slope((byte) 0);
        tile1.halfBrick(false);
        tile1.active(true);
        if (index < startY)
          tile1.active(false);
        WorldGen.SquareWallFrame(i, j);
      }
    }
  }
}
