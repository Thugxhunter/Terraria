// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.DunesBiome
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using ReLogic.Utilities;
using System;
using Terraria.GameContent.Biomes.Desert;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
  public class DunesBiome : MicroBiome
  {
    [JsonProperty("SingleDunesWidth")]
    private WorldGenRange _singleDunesWidth = WorldGenRange.Empty;
    [JsonProperty("HeightScale")]
    private double _heightScale = 1.0;

    public int MaximumWidth => this._singleDunesWidth.ScaledMaximum * 2;

    public override bool Place(Point origin, StructureMap structures)
    {
      int height1 = (int) ((double) GenBase._random.Next(60, 100) * this._heightScale);
      int height2 = (int) ((double) GenBase._random.Next(60, 100) * this._heightScale);
      int random1 = this._singleDunesWidth.GetRandom(GenBase._random);
      int random2 = this._singleDunesWidth.GetRandom(GenBase._random);
      DunesBiome.DunesDescription fromPlacement1 = DunesBiome.DunesDescription.CreateFromPlacement(new Point(origin.X - random1 / 2 + 30, origin.Y), random1, height1);
      DunesBiome.DunesDescription fromPlacement2 = DunesBiome.DunesDescription.CreateFromPlacement(new Point(origin.X + random2 / 2 - 30, origin.Y), random2, height2);
      this.PlaceSingle(fromPlacement1, structures);
      this.PlaceSingle(fromPlacement2, structures);
      return true;
    }

    private void PlaceSingle(DunesBiome.DunesDescription description, StructureMap structures)
    {
      int num1 = GenBase._random.Next(3) + 8;
      for (int index = 0; index < num1 - 1; ++index)
      {
        int num2 = (int) (2.0 / (double) num1 * (double) description.Area.Width);
        int num3 = (int) ((double) index / (double) num1 * (double) description.Area.Width + (double) description.Area.Left) + num2 * 2 / 5 + GenBase._random.Next(-5, 6);
        double num4 = 1.0 - Math.Abs((double) index / (double) (num1 - 2) - 0.5) * 2.0;
        DunesBiome.PlaceHill(num3 - num2 / 2, num3 + num2 / 2, (num4 * 0.3 + 0.2) * this._heightScale, description);
      }
      int num5 = GenBase._random.Next(2) + 1;
      for (int index = 0; index < num5; ++index)
      {
        int num6 = description.Area.Width / 2;
        int num7 = description.Area.Center.X + GenBase._random.Next(-10, 11);
        DunesBiome.PlaceHill(num7 - num6 / 2, num7 + num6 / 2, 0.8 * this._heightScale, description);
      }
      structures.AddStructure(description.Area, 20);
    }

    private static void PlaceHill(
      int startX,
      int endX,
      double scale,
      DunesBiome.DunesDescription description)
    {
      Point startPoint = new Point(startX, (int) description.Surface[startX]);
      Point endPoint = new Point(endX, (int) description.Surface[endX]);
      Point point1 = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2 - (int) (35.0 * scale));
      int num = (endPoint.X - point1.X) / 4;
      int minValue = (endPoint.X - point1.X) / 16;
      if (description.WindDirection == DunesBiome.WindDirection.Left)
        point1.X -= WorldGen.genRand.Next(minValue, num + 1);
      else
        point1.X += WorldGen.genRand.Next(minValue, num + 1);
      Point point2 = new Point(0, (int) (scale * 12.0));
      Point point3 = new Point(point2.X / -2, point2.Y / -2);
      DunesBiome.PlaceCurvedLine(startPoint, point1, description.WindDirection != DunesBiome.WindDirection.Left ? point3 : point2, description);
      DunesBiome.PlaceCurvedLine(point1, endPoint, description.WindDirection == DunesBiome.WindDirection.Left ? point3 : point2, description);
    }

    private static void PlaceCurvedLine(
      Point startPoint,
      Point endPoint,
      Point anchorOffset,
      DunesBiome.DunesDescription description)
    {
      Point p = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
      p.X += anchorOffset.X;
      p.Y += anchorOffset.Y;
      Vector2D vector2D1 = startPoint.ToVector2D();
      Vector2D vector2D2 = endPoint.ToVector2D();
      Vector2D vector2D3 = p.ToVector2D();
      double num1 = 0.5 / (vector2D2.X - vector2D1.X);
      Point point1 = new Point(-1, -1);
      for (double num2 = 0.0; num2 <= 1.0; num2 += num1)
      {
        Point point2 = Vector2D.Lerp(Vector2D.Lerp(vector2D1, vector2D3, num2), Vector2D.Lerp(vector2D3, vector2D2, num2), num2).ToPoint();
        if (!(point2 == point1))
        {
          point1 = point2;
          int d = description.Area.Width / 2 - Math.Abs(point2.X - description.Area.Center.X);
          int num3 = (int) description.Surface[point2.X] + (int) (Math.Sqrt((double) d) * 3.0);
          for (int index = point2.Y - 10; index < point2.Y; ++index)
          {
            if (GenBase._tiles[point2.X, index].active() && GenBase._tiles[point2.X, index].type != (ushort) 53)
              GenBase._tiles[point2.X, index].ClearEverything();
          }
          for (int y = point2.Y; y < num3; ++y)
          {
            GenBase._tiles[point2.X, y].ResetToType((ushort) 53);
            Tile.SmoothSlope(point2.X, y);
          }
        }
      }
    }

    private class DunesDescription
    {
      public bool IsValid { get; private set; }

      public SurfaceMap Surface { get; private set; }

      public Microsoft.Xna.Framework.Rectangle Area { get; private set; }

      public DunesBiome.WindDirection WindDirection { get; private set; }

      private DunesDescription()
      {
      }

      public static DunesBiome.DunesDescription CreateFromPlacement(
        Point origin,
        int width,
        int height)
      {
        Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(origin.X - width / 2, origin.Y - height / 2, width, height);
        return new DunesBiome.DunesDescription()
        {
          Area = rectangle,
          IsValid = true,
          Surface = SurfaceMap.FromArea(rectangle.Left - 20, rectangle.Width + 40),
          WindDirection = WorldGen.genRand.Next(2) == 0 ? DunesBiome.WindDirection.Left : DunesBiome.WindDirection.Right
        };
      }
    }

    private enum WindDirection
    {
      Left,
      Right,
    }
  }
}
