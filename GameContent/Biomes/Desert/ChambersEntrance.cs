// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.Desert.ChambersEntrance
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes.Desert
{
  public static class ChambersEntrance
  {
    public static void Place(DesertDescription description)
    {
      int num = description.Desert.Center.X + WorldGen.genRand.Next(-40, 41);
      Point position = new Point(num, (int) description.Surface[num]);
      ChambersEntrance.PlaceAt(description, position);
    }

    private static void PlaceAt(DesertDescription description, Point position)
    {
      ShapeData shapeData = new ShapeData();
      Point origin = new Point(position.X, position.Y + 2);
      WorldUtils.Gen(origin, (GenShape) new Shapes.Circle(24, 12), Actions.Chain((GenAction) new Modifiers.Blotches(), new Actions.SetTile((ushort) 53).Output(shapeData)));
      UnifiedRandom genRand = WorldGen.genRand;
      ShapeData data = new ShapeData();
      int num1 = description.Hive.Top - position.Y;
      int direction = genRand.Next(2) == 0 ? -1 : 1;
      List<ChambersEntrance.PathConnection> pathConnectionList = new List<ChambersEntrance.PathConnection>()
      {
        new ChambersEntrance.PathConnection(new Point(position.X + -direction * 26, position.Y - 8), direction)
      };
      int num2 = genRand.Next(2, 4);
      for (int index = 0; index < num2; ++index)
      {
        int y = (int) ((double) (index + 1) / (double) num2 * (double) num1) + genRand.Next(-8, 9);
        int x = direction * genRand.Next(20, 41);
        int num3 = genRand.Next(18, 29);
        WorldUtils.Gen(position, (GenShape) new Shapes.Circle(num3 / 2, 3), Actions.Chain((GenAction) new Modifiers.Offset(x, y), (GenAction) new Modifiers.Blotches(), new Actions.Clear().Output(data), (GenAction) new Actions.PlaceWall((ushort) 187)));
        pathConnectionList.Add(new ChambersEntrance.PathConnection(new Point(x + num3 / 2 * -direction + position.X, y + position.Y), -direction));
        direction *= -1;
      }
      WorldUtils.Gen(position, (GenShape) new ModShapes.OuterOutline(data), Actions.Chain((GenAction) new Modifiers.Expand(1), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 53
      }), (GenAction) new Actions.SetTile((ushort) 397), (GenAction) new Actions.PlaceWall((ushort) 187)));
      GenShapeActionPair pair = new GenShapeActionPair((GenShape) new Shapes.Rectangle(2, 4), Actions.Chain((GenAction) new Modifiers.IsSolid(), (GenAction) new Modifiers.Blotches(), (GenAction) new Actions.Clear(), (GenAction) new Modifiers.Expand(1), (GenAction) new Actions.PlaceWall((ushort) 187), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 53
      }), (GenAction) new Actions.SetTile((ushort) 397)));
      for (int index = 1; index < pathConnectionList.Count; ++index)
      {
        ChambersEntrance.PathConnection pathConnection1 = pathConnectionList[index - 1];
        ChambersEntrance.PathConnection pathConnection2 = pathConnectionList[index];
        double num4 = Math.Abs(pathConnection2.Position.X - pathConnection1.Position.X) * 1.5;
        for (double num5 = 0.0; num5 <= 1.0; num5 += 0.02)
        {
          Vector2D vector2D1 = new Vector2D(pathConnection1.Position.X + pathConnection1.Direction * num4 * num5, pathConnection1.Position.Y);
          Vector2D vector2D2;
          // ISSUE: explicit constructor call
          ((Vector2D) ref vector2D2).\u002Ector(pathConnection2.Position.X + pathConnection2.Direction * num4 * (1.0 - num5), pathConnection2.Position.Y);
          Vector2D vector2D3 = Vector2D.Lerp(pathConnection1.Position, pathConnection2.Position, num5);
          Vector2D vector2D4 = vector2D3;
          double num6 = num5;
          WorldUtils.Gen(Vector2D.Lerp(Vector2D.Lerp(vector2D1, vector2D4, num6), Vector2D.Lerp(vector2D3, vector2D2, num5), num5).ToPoint(), pair);
        }
      }
      WorldUtils.Gen(origin, (GenShape) new Shapes.Rectangle(new Microsoft.Xna.Framework.Rectangle(-29, -12, 58, 12)), Actions.Chain((GenAction) new Modifiers.NotInShape(shapeData), (GenAction) new Modifiers.Expand(1), (GenAction) new Actions.PlaceWall((ushort) 0)));
    }

    private struct PathConnection
    {
      public readonly Vector2D Position;
      public readonly double Direction;

      public PathConnection(Point position, int direction)
      {
        this.Position = new Vector2D((double) position.X, (double) position.Y);
        this.Direction = (double) direction;
      }
    }
  }
}
