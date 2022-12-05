// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.GraniteBiome
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
  public class GraniteBiome : MicroBiome
  {
    private const int MAX_MAGMA_ITERATIONS = 300;
    private GraniteBiome.Magma[,] _sourceMagmaMap = new GraniteBiome.Magma[200, 200];
    private GraniteBiome.Magma[,] _targetMagmaMap = new GraniteBiome.Magma[200, 200];
    private static Vector2D[] _normalisedVectors = new Vector2D[9]
    {
      Vector2D.Normalize(new Vector2D(-1.0, -1.0)),
      Vector2D.Normalize(new Vector2D(-1.0, 0.0)),
      Vector2D.Normalize(new Vector2D(-1.0, 1.0)),
      Vector2D.Normalize(new Vector2D(0.0, -1.0)),
      new Vector2D(0.0, 0.0),
      Vector2D.Normalize(new Vector2D(0.0, 1.0)),
      Vector2D.Normalize(new Vector2D(1.0, -1.0)),
      Vector2D.Normalize(new Vector2D(1.0, 0.0)),
      Vector2D.Normalize(new Vector2D(1.0, 1.0))
    };

    public static bool CanPlace(Point origin, StructureMap structures) => !WorldGen.BiomeTileCheck(origin.X, origin.Y) && !GenBase._tiles[origin.X, origin.Y].active();

    public override bool Place(Point origin, StructureMap structures)
    {
      if (GenBase._tiles[origin.X, origin.Y].active())
        return false;
      origin.X -= this._sourceMagmaMap.GetLength(0) / 2;
      origin.Y -= this._sourceMagmaMap.GetLength(1) / 2;
      this.BuildMagmaMap(origin);
      Microsoft.Xna.Framework.Rectangle effectedMapArea;
      this.SimulatePressure(out effectedMapArea);
      this.PlaceGranite(origin, effectedMapArea);
      this.CleanupTiles(origin, effectedMapArea);
      this.PlaceDecorations(origin, effectedMapArea);
      structures.AddStructure(effectedMapArea, 8);
      return true;
    }

    private void BuildMagmaMap(Point tileOrigin)
    {
      this._sourceMagmaMap = new GraniteBiome.Magma[200, 200];
      this._targetMagmaMap = new GraniteBiome.Magma[200, 200];
      for (int index1 = 0; index1 < this._sourceMagmaMap.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < this._sourceMagmaMap.GetLength(1); ++index2)
        {
          int i = index1 + tileOrigin.X;
          int j = index2 + tileOrigin.Y;
          this._sourceMagmaMap[index1, index2] = GraniteBiome.Magma.CreateEmpty(WorldGen.SolidTile(i, j) ? 4.0 : 1.0);
          this._targetMagmaMap[index1, index2] = this._sourceMagmaMap[index1, index2];
        }
      }
    }

    private void SimulatePressure(out Microsoft.Xna.Framework.Rectangle effectedMapArea)
    {
      int length1 = this._sourceMagmaMap.GetLength(0);
      int length2 = this._sourceMagmaMap.GetLength(1);
      int index1 = length1 / 2;
      int index2 = length2 / 2;
      int num1 = index1;
      int min1 = num1;
      int num2 = index2;
      int min2 = num2;
      for (int index3 = 0; index3 < 300; ++index3)
      {
        for (int index4 = num1; index4 <= min1; ++index4)
        {
          for (int index5 = num2; index5 <= min2; ++index5)
          {
            GraniteBiome.Magma sourceMagma1 = this._sourceMagmaMap[index4, index5];
            if (sourceMagma1.IsActive)
            {
              double num3 = 0.0;
              Vector2D vector2D = Vector2D.Zero;
              for (int index6 = -1; index6 <= 1; ++index6)
              {
                for (int index7 = -1; index7 <= 1; ++index7)
                {
                  if (index6 != 0 || index7 != 0)
                  {
                    Vector2D normalisedVector = GraniteBiome._normalisedVectors[(index6 + 1) * 3 + (index7 + 1)];
                    GraniteBiome.Magma sourceMagma2 = this._sourceMagmaMap[index4 + index6, index5 + index7];
                    if (sourceMagma1.Pressure > 0.01 && !sourceMagma2.IsActive)
                    {
                      if (index6 == -1)
                        num1 = Utils.Clamp<int>(index4 + index6, 1, num1);
                      else
                        min1 = Utils.Clamp<int>(index4 + index6, min1, length1 - 2);
                      if (index7 == -1)
                        num2 = Utils.Clamp<int>(index5 + index7, 1, num2);
                      else
                        min2 = Utils.Clamp<int>(index5 + index7, min2, length2 - 2);
                      this._targetMagmaMap[index4 + index6, index5 + index7] = sourceMagma2.ToFlow();
                    }
                    double pressure = sourceMagma2.Pressure;
                    num3 += pressure;
                    vector2D = Vector2D.op_Addition(vector2D, Vector2D.op_Multiply(pressure, normalisedVector));
                  }
                }
              }
              double num4 = num3 / 8.0;
              if (num4 > sourceMagma1.Resistance)
              {
                double num5 = ((Vector2D) ref vector2D).Length() / 8.0;
                double pressure = Math.Max(0.0, Math.Max(num4 - num5 - sourceMagma1.Pressure, 0.0) + num5 + sourceMagma1.Pressure * 0.875 - sourceMagma1.Resistance);
                this._targetMagmaMap[index4, index5] = GraniteBiome.Magma.CreateFlow(pressure, Math.Max(0.0, sourceMagma1.Resistance - pressure * 0.02));
              }
            }
          }
        }
        if (index3 < 2)
          this._targetMagmaMap[index1, index2] = GraniteBiome.Magma.CreateFlow(25.0);
        Utils.Swap<GraniteBiome.Magma[,]>(ref this._sourceMagmaMap, ref this._targetMagmaMap);
      }
      effectedMapArea = new Microsoft.Xna.Framework.Rectangle(num1, num2, min1 - num1 + 1, min2 - num2 + 1);
    }

    private bool ShouldUseLava(Point tileOrigin)
    {
      int length1 = this._sourceMagmaMap.GetLength(0);
      int length2 = this._sourceMagmaMap.GetLength(1);
      int num1 = length1 / 2;
      int num2 = length2 / 2;
      if (tileOrigin.Y + num2 <= GenVars.lavaLine - 30)
        return false;
      for (int index1 = -50; index1 < 50; ++index1)
      {
        for (int index2 = -50; index2 < 50; ++index2)
        {
          if (GenBase._tiles[tileOrigin.X + num1 + index1, tileOrigin.Y + num2 + index2].active())
          {
            switch (GenBase._tiles[tileOrigin.X + num1 + index1, tileOrigin.Y + num2 + index2].type)
            {
              case 147:
              case 161:
              case 162:
              case 163:
              case 200:
                return false;
              default:
                continue;
            }
          }
        }
      }
      return true;
    }

    private void PlaceGranite(Point tileOrigin, Microsoft.Xna.Framework.Rectangle magmaMapArea)
    {
      bool flag = this.ShouldUseLava(tileOrigin);
      ushort type = 368;
      ushort num = 180;
      if (WorldGen.drunkWorldGen)
      {
        type = (ushort) 367;
        num = (ushort) 178;
      }
      for (int left = magmaMapArea.Left; left < magmaMapArea.Right; ++left)
      {
        for (int top = magmaMapArea.Top; top < magmaMapArea.Bottom; ++top)
        {
          GraniteBiome.Magma sourceMagma = this._sourceMagmaMap[left, top];
          if (sourceMagma.IsActive)
          {
            Tile tile = GenBase._tiles[tileOrigin.X + left, tileOrigin.Y + top];
            if (Math.Max(1.0 - Math.Max(0.0, (Math.Sin((double) (tileOrigin.Y + top) * 0.4) * 0.7 + 1.2) * (0.2 + 0.5 / Math.Sqrt(Math.Max(0.0, sourceMagma.Pressure - sourceMagma.Resistance)))), sourceMagma.Pressure / 15.0) > 0.35 + (WorldGen.SolidTile(tileOrigin.X + left, tileOrigin.Y + top) ? 0.0 : 0.5))
            {
              if (TileID.Sets.Ore[(int) tile.type])
                tile.ResetToType(tile.type);
              else
                tile.ResetToType(type);
              tile.wall = num;
            }
            else if (sourceMagma.Resistance < 0.01)
            {
              WorldUtils.ClearTile(tileOrigin.X + left, tileOrigin.Y + top);
              tile.wall = num;
            }
            if (tile.liquid > (byte) 0 & flag)
              tile.liquidType(1);
          }
        }
      }
    }

    private void CleanupTiles(Point tileOrigin, Microsoft.Xna.Framework.Rectangle magmaMapArea)
    {
      ushort num1 = 180;
      if (WorldGen.drunkWorldGen)
        num1 = (ushort) 178;
      List<Point16> point16List = new List<Point16>();
      for (int left = magmaMapArea.Left; left < magmaMapArea.Right; ++left)
      {
        for (int top = magmaMapArea.Top; top < magmaMapArea.Bottom; ++top)
        {
          if (this._sourceMagmaMap[left, top].IsActive)
          {
            int num2 = 0;
            int num3 = left + tileOrigin.X;
            int num4 = top + tileOrigin.Y;
            if (WorldGen.SolidTile(num3, num4))
            {
              for (int index1 = -1; index1 <= 1; ++index1)
              {
                for (int index2 = -1; index2 <= 1; ++index2)
                {
                  if (WorldGen.SolidTile(num3 + index1, num4 + index2))
                    ++num2;
                }
              }
              if (num2 < 3)
                point16List.Add(new Point16(num3, num4));
            }
          }
        }
      }
      foreach (Point16 point16 in point16List)
      {
        int x = (int) point16.X;
        int y = (int) point16.Y;
        WorldUtils.ClearTile(x, y, true);
        GenBase._tiles[x, y].wall = num1;
      }
      point16List.Clear();
    }

    private void PlaceDecorations(Point tileOrigin, Microsoft.Xna.Framework.Rectangle magmaMapArea)
    {
      FastRandom fastRandom1 = new FastRandom(Main.ActiveWorldFileData.Seed).WithModifier(65440UL);
      for (int left = magmaMapArea.Left; left < magmaMapArea.Right; ++left)
      {
        for (int top = magmaMapArea.Top; top < magmaMapArea.Bottom; ++top)
        {
          GraniteBiome.Magma sourceMagma = this._sourceMagmaMap[left, top];
          int index1 = left + tileOrigin.X;
          int index2 = top + tileOrigin.Y;
          if (sourceMagma.IsActive)
          {
            WorldUtils.TileFrame(index1, index2);
            WorldGen.SquareWallFrame(index1, index2);
            FastRandom fastRandom2 = fastRandom1.WithModifier(index1, index2);
            if (fastRandom2.Next(8) == 0 && GenBase._tiles[index1, index2].active())
            {
              if (!GenBase._tiles[index1, index2 + 1].active())
                WorldGen.PlaceUncheckedStalactite(index1, index2 + 1, fastRandom2.Next(2) == 0, fastRandom2.Next(3), false);
              if (!GenBase._tiles[index1, index2 - 1].active())
                WorldGen.PlaceUncheckedStalactite(index1, index2 - 1, fastRandom2.Next(2) == 0, fastRandom2.Next(3), false);
            }
            if (fastRandom2.Next(2) == 0)
              Tile.SmoothSlope(index1, index2);
          }
        }
      }
    }

    private struct Magma
    {
      public readonly double Pressure;
      public readonly double Resistance;
      public readonly bool IsActive;

      private Magma(double pressure, double resistance, bool active)
      {
        this.Pressure = pressure;
        this.Resistance = resistance;
        this.IsActive = active;
      }

      public GraniteBiome.Magma ToFlow() => new GraniteBiome.Magma(this.Pressure, this.Resistance, true);

      public static GraniteBiome.Magma CreateFlow(double pressure, double resistance = 0.0) => new GraniteBiome.Magma(pressure, resistance, true);

      public static GraniteBiome.Magma CreateEmpty(double resistance = 0.0) => new GraniteBiome.Magma(0.0, resistance, false);
    }
  }
}
