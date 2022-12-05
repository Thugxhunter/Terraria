// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.TerrainPass
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
  public class TerrainPass : GenPass
  {
    public TerrainPass()
      : base("Terrain", 449.3721923828125)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
      int num1 = configuration.Get<int>("FlatBeachPadding");
      progress.Message = Lang.gen[0].Value;
      TerrainPass.TerrainFeatureType featureType = TerrainPass.TerrainFeatureType.Plateau;
      double num2 = (double) Main.maxTilesY * 0.3 * ((double) GenBase._random.Next(90, 110) * 0.005);
      double num3 = (num2 + (double) Main.maxTilesY * 0.2) * ((double) GenBase._random.Next(90, 110) * 0.01);
      if (WorldGen.remixWorldGen)
      {
        double num4 = (double) Main.maxTilesY * 0.5;
        if (Main.maxTilesX > 2500)
          num4 = (double) Main.maxTilesY * 0.6;
        num3 = num4 * ((double) GenBase._random.Next(95, 106) * 0.01);
      }
      double val2_1 = num2;
      double val2_2 = num2;
      double val2_3 = num3;
      double val2_4 = num3;
      double num5 = (double) Main.maxTilesY * 0.23;
      TerrainPass.SurfaceHistory history = new TerrainPass.SurfaceHistory(500);
      int num6 = GenVars.leftBeachEnd + num1;
      for (int index = 0; index < Main.maxTilesX; ++index)
      {
        progress.Set((double) index / (double) Main.maxTilesX);
        val2_1 = Math.Min(num2, val2_1);
        val2_2 = Math.Max(num2, val2_2);
        val2_3 = Math.Min(num3, val2_3);
        val2_4 = Math.Max(num3, val2_4);
        if (num6 <= 0)
        {
          featureType = (TerrainPass.TerrainFeatureType) GenBase._random.Next(0, 5);
          num6 = GenBase._random.Next(5, 40);
          if (featureType == TerrainPass.TerrainFeatureType.Plateau)
            num6 *= (int) ((double) GenBase._random.Next(5, 30) * 0.2);
        }
        --num6;
        if ((double) index > (double) Main.maxTilesX * 0.45 && (double) index < (double) Main.maxTilesX * 0.55 && (featureType == TerrainPass.TerrainFeatureType.Mountain || featureType == TerrainPass.TerrainFeatureType.Valley))
          featureType = (TerrainPass.TerrainFeatureType) GenBase._random.Next(3);
        if ((double) index > (double) Main.maxTilesX * 0.48 && (double) index < (double) Main.maxTilesX * 0.52)
          featureType = TerrainPass.TerrainFeatureType.Plateau;
        num2 += TerrainPass.GenerateWorldSurfaceOffset(featureType);
        double num7 = 0.17;
        double num8 = 0.26;
        if (WorldGen.drunkWorldGen)
        {
          num7 = 0.15;
          num8 = 0.28;
        }
        if (index < GenVars.leftBeachEnd + num1 || index > GenVars.rightBeachStart - num1)
          num2 = Utils.Clamp<double>(num2, (double) Main.maxTilesY * 0.17, num5);
        else if (num2 < (double) Main.maxTilesY * num7)
        {
          num2 = (double) Main.maxTilesY * num7;
          num6 = 0;
        }
        else if (num2 > (double) Main.maxTilesY * num8)
        {
          num2 = (double) Main.maxTilesY * num8;
          num6 = 0;
        }
        while (GenBase._random.Next(0, 3) == 0)
          num3 += (double) GenBase._random.Next(-2, 3);
        if (WorldGen.remixWorldGen)
        {
          if (Main.maxTilesX > 2500)
          {
            if (num3 > (double) Main.maxTilesY * 0.7)
              --num3;
          }
          else if (num3 > (double) Main.maxTilesY * 0.6)
            --num3;
        }
        else
        {
          if (num3 < num2 + (double) Main.maxTilesY * 0.06)
            ++num3;
          if (num3 > num2 + (double) Main.maxTilesY * 0.35)
            --num3;
        }
        history.Record(num2);
        TerrainPass.FillColumn(index, num2, num3);
        if (index == GenVars.rightBeachStart - num1)
        {
          if (num2 > num5)
            TerrainPass.RetargetSurfaceHistory(history, index, num5);
          featureType = TerrainPass.TerrainFeatureType.Plateau;
          num6 = Main.maxTilesX - index;
        }
      }
      Main.worldSurface = (double) (int) (val2_2 + 25.0);
      Main.rockLayer = val2_4;
      double num9 = (double) ((int) ((Main.rockLayer - Main.worldSurface) / 6.0) * 6);
      Main.rockLayer = (double) (int) (Main.worldSurface + num9);
      int num10 = (int) (Main.rockLayer + (double) Main.maxTilesY) / 2 + GenBase._random.Next(-100, 20);
      int num11 = num10 + GenBase._random.Next(50, 80);
      if (WorldGen.remixWorldGen)
        num11 = (int) (Main.worldSurface * 4.0 + num3) / 5;
      int num12 = 20;
      if (val2_3 < val2_2 + (double) num12)
      {
        double num13 = (val2_3 + val2_2) / 2.0;
        double num14 = Math.Abs(val2_3 - val2_2);
        if (num14 < (double) num12)
          num14 = (double) num12;
        val2_3 = num13 + num14 / 2.0;
        val2_2 = num13 - num14 / 2.0;
      }
      GenVars.rockLayer = num3;
      GenVars.rockLayerHigh = val2_4;
      GenVars.rockLayerLow = val2_3;
      GenVars.worldSurface = num2;
      GenVars.worldSurfaceHigh = val2_2;
      GenVars.worldSurfaceLow = val2_1;
      GenVars.waterLine = num10;
      GenVars.lavaLine = num11;
    }

    private static void FillColumn(int x, double worldSurface, double rockLayer)
    {
      for (int index = 0; (double) index < worldSurface; ++index)
      {
        Main.tile[x, index].active(false);
        Main.tile[x, index].frameX = (short) -1;
        Main.tile[x, index].frameY = (short) -1;
      }
      for (int index = (int) worldSurface; index < Main.maxTilesY; ++index)
      {
        if ((double) index < rockLayer)
        {
          Main.tile[x, index].active(true);
          Main.tile[x, index].type = (ushort) 0;
          Main.tile[x, index].frameX = (short) -1;
          Main.tile[x, index].frameY = (short) -1;
        }
        else
        {
          Main.tile[x, index].active(true);
          Main.tile[x, index].type = (ushort) 1;
          Main.tile[x, index].frameX = (short) -1;
          Main.tile[x, index].frameY = (short) -1;
        }
      }
    }

    private static void RetargetColumn(int x, double worldSurface)
    {
      for (int index = 0; (double) index < worldSurface; ++index)
      {
        Main.tile[x, index].active(false);
        Main.tile[x, index].frameX = (short) -1;
        Main.tile[x, index].frameY = (short) -1;
      }
      for (int index = (int) worldSurface; index < Main.maxTilesY; ++index)
      {
        if (Main.tile[x, index].type != (ushort) 1 || !Main.tile[x, index].active())
        {
          Main.tile[x, index].active(true);
          Main.tile[x, index].type = (ushort) 0;
          Main.tile[x, index].frameX = (short) -1;
          Main.tile[x, index].frameY = (short) -1;
        }
      }
    }

    private static double GenerateWorldSurfaceOffset(TerrainPass.TerrainFeatureType featureType)
    {
      double worldSurfaceOffset = 0.0;
      if ((WorldGen.drunkWorldGen || WorldGen.getGoodWorldGen || WorldGen.remixWorldGen) && WorldGen.genRand.Next(2) == 0)
      {
        switch (featureType)
        {
          case TerrainPass.TerrainFeatureType.Plateau:
            while (GenBase._random.Next(0, 6) == 0)
              worldSurfaceOffset += (double) GenBase._random.Next(-1, 2);
            break;
          case TerrainPass.TerrainFeatureType.Hill:
            while (GenBase._random.Next(0, 3) == 0)
              --worldSurfaceOffset;
            while (GenBase._random.Next(0, 10) == 0)
              ++worldSurfaceOffset;
            break;
          case TerrainPass.TerrainFeatureType.Dale:
            while (GenBase._random.Next(0, 3) == 0)
              ++worldSurfaceOffset;
            while (GenBase._random.Next(0, 10) == 0)
              --worldSurfaceOffset;
            break;
          case TerrainPass.TerrainFeatureType.Mountain:
            while (GenBase._random.Next(0, 3) != 0)
              --worldSurfaceOffset;
            while (GenBase._random.Next(0, 6) == 0)
              ++worldSurfaceOffset;
            break;
          case TerrainPass.TerrainFeatureType.Valley:
            while (GenBase._random.Next(0, 3) != 0)
              ++worldSurfaceOffset;
            while (GenBase._random.Next(0, 5) == 0)
              --worldSurfaceOffset;
            break;
        }
      }
      else
      {
        switch (featureType)
        {
          case TerrainPass.TerrainFeatureType.Plateau:
            while (GenBase._random.Next(0, 7) == 0)
              worldSurfaceOffset += (double) GenBase._random.Next(-1, 2);
            break;
          case TerrainPass.TerrainFeatureType.Hill:
            while (GenBase._random.Next(0, 4) == 0)
              --worldSurfaceOffset;
            while (GenBase._random.Next(0, 10) == 0)
              ++worldSurfaceOffset;
            break;
          case TerrainPass.TerrainFeatureType.Dale:
            while (GenBase._random.Next(0, 4) == 0)
              ++worldSurfaceOffset;
            while (GenBase._random.Next(0, 10) == 0)
              --worldSurfaceOffset;
            break;
          case TerrainPass.TerrainFeatureType.Mountain:
            while (GenBase._random.Next(0, 2) == 0)
              --worldSurfaceOffset;
            while (GenBase._random.Next(0, 6) == 0)
              ++worldSurfaceOffset;
            break;
          case TerrainPass.TerrainFeatureType.Valley:
            while (GenBase._random.Next(0, 2) == 0)
              ++worldSurfaceOffset;
            while (GenBase._random.Next(0, 5) == 0)
              --worldSurfaceOffset;
            break;
        }
      }
      return worldSurfaceOffset;
    }

    private static void RetargetSurfaceHistory(
      TerrainPass.SurfaceHistory history,
      int targetX,
      double targetHeight)
    {
      for (int index1 = 0; index1 < history.Length / 2 && history[history.Length - 1] > targetHeight; ++index1)
      {
        for (int index2 = 0; index2 < history.Length - index1 * 2; ++index2)
        {
          double num = history[history.Length - index2 - 1] - 1.0;
          history[history.Length - index2 - 1] = num;
          if (num <= targetHeight)
            break;
        }
      }
      for (int index = 0; index < history.Length; ++index)
      {
        double worldSurface = history[history.Length - index - 1];
        TerrainPass.RetargetColumn(targetX - index, worldSurface);
      }
    }

    private enum TerrainFeatureType
    {
      Plateau,
      Hill,
      Dale,
      Mountain,
      Valley,
    }

    private class SurfaceHistory
    {
      private readonly double[] _heights;
      private int _index;

      public double this[int index]
      {
        get => this._heights[(index + this._index) % this._heights.Length];
        set => this._heights[(index + this._index) % this._heights.Length] = value;
      }

      public int Length => this._heights.Length;

      public SurfaceHistory(int size) => this._heights = new double[size];

      public void Record(double height)
      {
        this._heights[this._index] = height;
        this._index = (this._index + 1) % this._heights.Length;
      }
    }
  }
}
