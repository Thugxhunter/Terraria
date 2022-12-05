// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.Desert.DesertHive
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using Terraria.Utilities;

namespace Terraria.GameContent.Biomes.Desert
{
  public static class DesertHive
  {
    public static void Place(DesertDescription description)
    {
      DesertHive.ClusterGroup clusters = DesertHive.ClusterGroup.FromDescription(description);
      DesertHive.PlaceClusters(description, clusters);
      DesertHive.AddTileVariance(description);
    }

    private static void PlaceClusters(
      DesertDescription description,
      DesertHive.ClusterGroup clusters)
    {
      Rectangle hive = description.Hive;
      hive.Inflate(20, 20);
      DesertHive.PostPlacementEffect[,] postEffectMap = new DesertHive.PostPlacementEffect[hive.Width, hive.Height];
      DesertHive.PlaceClustersArea(description, clusters, hive, postEffectMap, Point.Zero);
      for (int left = hive.Left; left < hive.Right; ++left)
      {
        for (int top = hive.Top; top < hive.Bottom; ++top)
        {
          if (postEffectMap[left - hive.Left, top - hive.Top].HasFlag((Enum) DesertHive.PostPlacementEffect.Smooth))
            Tile.SmoothSlope(left, top, false);
        }
      }
    }

    private static void PlaceClustersArea(
      DesertDescription description,
      DesertHive.ClusterGroup clusters,
      Rectangle area,
      DesertHive.PostPlacementEffect[,] postEffectMap,
      Point postEffectMapOffset)
    {
      FastRandom fastRandom1 = new FastRandom(Main.ActiveWorldFileData.Seed).WithModifier(57005UL);
      Vector2D vector2D1;
      // ISSUE: explicit constructor call
      ((Vector2D) ref vector2D1).\u002Ector((double) description.Hive.Width, (double) description.Hive.Height);
      Vector2D vector2D2;
      // ISSUE: explicit constructor call
      ((Vector2D) ref vector2D2).\u002Ector((double) clusters.Width, (double) clusters.Height);
      Vector2D vector2D3 = Vector2D.op_Division(description.BlockScale, 2.0);
      for (int left = area.Left; left < area.Right; ++left)
      {
        for (int top = area.Top; top < area.Bottom; ++top)
        {
          byte liquid = Main.tile[left, top].liquid;
          if (WorldGen.InWorld(left, top, 1))
          {
            double num1 = 0.0;
            int num2 = -1;
            double num3 = 0.0;
            ushort type = 53;
            if (fastRandom1.Next(3) == 0)
              type = (ushort) 397;
            int x = left - description.Hive.X;
            int y = top - description.Hive.Y;
            Vector2D vector2D4 = Vector2D.op_Multiply(Vector2D.op_Division(Vector2D.op_Subtraction(new Vector2D((double) x, (double) y), vector2D3), vector2D1), vector2D2);
            for (int index = 0; index < clusters.Count; ++index)
            {
              DesertHive.Cluster cluster = clusters[index];
              if (Math.Abs(cluster[0].Position.X - vector2D4.X) <= 10.0 && Math.Abs(cluster[0].Position.Y - vector2D4.Y) <= 10.0)
              {
                double num4 = 0.0;
                foreach (DesertHive.Block block in (List<DesertHive.Block>) cluster)
                  num4 += 1.0 / Vector2D.DistanceSquared(block.Position, vector2D4);
                if (num4 > num1)
                {
                  if (num1 > num3)
                    num3 = num1;
                  num1 = num4;
                  num2 = index;
                }
                else if (num4 > num3)
                  num3 = num4;
              }
            }
            double num5 = num1 + num3;
            Tile tile = Main.tile[left, top];
            Vector2D vector2D5 = Vector2D.op_Subtraction(Vector2D.op_Multiply(Vector2D.op_Division(Vector2D.op_Subtraction(new Vector2D((double) x, (double) y), vector2D3), vector2D1), 2.0), Vector2D.One);
            bool flag1 = ((Vector2D) ref vector2D5).Length() >= 0.8;
            DesertHive.PostPlacementEffect postPlacementEffect = DesertHive.PostPlacementEffect.None;
            bool flag2 = true;
            if (num5 > 3.5)
            {
              postPlacementEffect = DesertHive.PostPlacementEffect.Smooth;
              tile.ClearEverything();
              if (!WorldGen.remixWorldGen || (double) top <= Main.rockLayer + (double) WorldGen.genRand.Next(-1, 2))
              {
                tile.wall = (ushort) 187;
                if (num2 % 15 == 2)
                  tile.ResetToType((ushort) 404);
              }
            }
            else if (num5 > 1.8)
            {
              if (!WorldGen.remixWorldGen || (double) top <= Main.rockLayer + (double) WorldGen.genRand.Next(-1, 2))
                tile.wall = (ushort) 187;
              if ((double) top < Main.worldSurface)
                tile.liquid = (byte) 0;
              else if (!WorldGen.remixWorldGen)
                tile.lava(true);
              if (!flag1 || tile.active())
              {
                tile.ResetToType((ushort) 396);
                postPlacementEffect = DesertHive.PostPlacementEffect.Smooth;
              }
            }
            else if (num5 > 0.7 || !flag1)
            {
              if (!WorldGen.remixWorldGen || (double) top <= Main.rockLayer + (double) WorldGen.genRand.Next(-1, 2))
              {
                tile.wall = (ushort) 216;
                tile.liquid = (byte) 0;
              }
              if (!flag1 || tile.active())
              {
                tile.ResetToType(type);
                postPlacementEffect = DesertHive.PostPlacementEffect.Smooth;
              }
            }
            else if (num5 > 0.25)
            {
              FastRandom fastRandom2 = fastRandom1.WithModifier(x, y);
              double num6 = (num5 - 0.25) / 0.45;
              if (fastRandom2.NextDouble() < num6)
              {
                if (!WorldGen.remixWorldGen || (double) top <= Main.rockLayer + (double) WorldGen.genRand.Next(-1, 2))
                  tile.wall = (ushort) 187;
                if ((double) top < Main.worldSurface)
                  tile.liquid = (byte) 0;
                else if (!WorldGen.remixWorldGen)
                  tile.lava(true);
                if (tile.active())
                {
                  tile.ResetToType(type);
                  postPlacementEffect = DesertHive.PostPlacementEffect.Smooth;
                }
              }
            }
            else
              flag2 = false;
            if (flag2)
              WorldGen.UpdateDesertHiveBounds(left, top);
            postEffectMap[left - area.X + postEffectMapOffset.X, top - area.Y + postEffectMapOffset.Y] = postPlacementEffect;
            if (WorldGen.remixWorldGen)
              Main.tile[left, top].liquid = liquid;
          }
        }
      }
    }

    private static void AddTileVariance(DesertDescription description)
    {
      for (int index1 = -20; index1 < description.Hive.Width + 20; ++index1)
      {
        for (int index2 = -20; index2 < description.Hive.Height + 20; ++index2)
        {
          int x = index1 + description.Hive.X;
          int y = index2 + description.Hive.Y;
          if (WorldGen.InWorld(x, y, 1))
          {
            Tile tile = Main.tile[x, y];
            Tile testTile1 = Main.tile[x, y + 1];
            Tile testTile2 = Main.tile[x, y + 2];
            if (tile.type == (ushort) 53 && (!WorldGen.SolidTile(testTile1) || !WorldGen.SolidTile(testTile2)))
              tile.type = (ushort) 397;
          }
        }
      }
      for (int index3 = -20; index3 < description.Hive.Width + 20; ++index3)
      {
        for (int index4 = -20; index4 < description.Hive.Height + 20; ++index4)
        {
          int index5 = index3 + description.Hive.X;
          int y = index4 + description.Hive.Y;
          if (WorldGen.InWorld(index5, y, 1))
          {
            Tile tile = Main.tile[index5, y];
            if (tile.active() && tile.type == (ushort) 396)
            {
              bool flag1 = true;
              for (int index6 = -1; index6 >= -3; --index6)
              {
                if (Main.tile[index5, y + index6].active())
                {
                  flag1 = false;
                  break;
                }
              }
              bool flag2 = true;
              for (int index7 = 1; index7 <= 3; ++index7)
              {
                if (Main.tile[index5, y + index7].active())
                {
                  flag2 = false;
                  break;
                }
              }
              if (!WorldGen.remixWorldGen || (double) y <= Main.rockLayer + (double) WorldGen.genRand.Next(-1, 2))
              {
                if (flag1 && WorldGen.genRand.Next(20) == 0)
                  WorldGen.PlaceTile(index5, y - 1, 485, true, true, style: WorldGen.genRand.Next(4));
                else if (flag1 && WorldGen.genRand.Next(5) == 0)
                  WorldGen.PlaceTile(index5, y - 1, 484, true, true);
                else if (flag1 ^ flag2 && WorldGen.genRand.Next(5) == 0)
                  WorldGen.PlaceTile(index5, y + (flag1 ? -1 : 1), 165, true, true);
                else if (flag1 && WorldGen.genRand.Next(5) == 0)
                  WorldGen.PlaceTile(index5, y - 1, 187, true, true, style: (29 + WorldGen.genRand.Next(6)));
              }
            }
          }
        }
      }
    }

    private struct Block
    {
      public Vector2D Position;

      public Block(double x, double y) => this.Position = new Vector2D(x, y);
    }

    private class Cluster : List<DesertHive.Block>
    {
    }

    private class ClusterGroup : List<DesertHive.Cluster>
    {
      public readonly int Width;
      public readonly int Height;

      private ClusterGroup(int width, int height)
      {
        this.Width = width;
        this.Height = height;
        this.Generate();
      }

      public static DesertHive.ClusterGroup FromDescription(DesertDescription description) => new DesertHive.ClusterGroup(description.BlockColumnCount, description.BlockRowCount);

      private static void SearchForCluster(
        bool[,] blockMap,
        List<Point> pointCluster,
        int x,
        int y,
        int level = 2)
      {
        pointCluster.Add(new Point(x, y));
        blockMap[x, y] = false;
        --level;
        if (level == -1)
          return;
        if (x > 0 && blockMap[x - 1, y])
          DesertHive.ClusterGroup.SearchForCluster(blockMap, pointCluster, x - 1, y, level);
        if (x < blockMap.GetLength(0) - 1 && blockMap[x + 1, y])
          DesertHive.ClusterGroup.SearchForCluster(blockMap, pointCluster, x + 1, y, level);
        if (y > 0 && blockMap[x, y - 1])
          DesertHive.ClusterGroup.SearchForCluster(blockMap, pointCluster, x, y - 1, level);
        if (y >= blockMap.GetLength(1) - 1 || !blockMap[x, y + 1])
          return;
        DesertHive.ClusterGroup.SearchForCluster(blockMap, pointCluster, x, y + 1, level);
      }

      private static void AttemptClaim(
        int x,
        int y,
        int[,] clusterIndexMap,
        List<List<Point>> pointClusters,
        int index)
      {
        int clusterIndex = clusterIndexMap[x, y];
        if (clusterIndex == -1 || clusterIndex == index)
          return;
        int num = WorldGen.genRand.Next(2) == 0 ? -1 : index;
        foreach (Point point in pointClusters[clusterIndex])
          clusterIndexMap[point.X, point.Y] = num;
      }

      private void Generate()
      {
        this.Clear();
        bool[,] blockMap = new bool[this.Width, this.Height];
        int num1 = this.Width / 2 - 1;
        int y1 = this.Height / 2 - 1;
        int num2 = (num1 + 1) * (num1 + 1);
        Point point1 = new Point(num1, y1);
        for (int index1 = point1.Y - y1; index1 <= point1.Y + y1; ++index1)
        {
          double num3 = (double) num1 / (double) y1 * (double) (index1 - point1.Y);
          int num4 = Math.Min(num1, (int) Math.Sqrt((double) num2 - num3 * num3));
          for (int index2 = point1.X - num4; index2 <= point1.X + num4; ++index2)
            blockMap[index2, index1] = WorldGen.genRand.Next(2) == 0;
        }
        List<List<Point>> pointClusters = new List<List<Point>>();
        for (int x = 0; x < blockMap.GetLength(0); ++x)
        {
          for (int y2 = 0; y2 < blockMap.GetLength(1); ++y2)
          {
            if (blockMap[x, y2] && WorldGen.genRand.Next(2) == 0)
            {
              List<Point> pointCluster = new List<Point>();
              DesertHive.ClusterGroup.SearchForCluster(blockMap, pointCluster, x, y2);
              if (pointCluster.Count > 2)
                pointClusters.Add(pointCluster);
            }
          }
        }
        int[,] clusterIndexMap = new int[blockMap.GetLength(0), blockMap.GetLength(1)];
        for (int index3 = 0; index3 < clusterIndexMap.GetLength(0); ++index3)
        {
          for (int index4 = 0; index4 < clusterIndexMap.GetLength(1); ++index4)
            clusterIndexMap[index3, index4] = -1;
        }
        for (int index = 0; index < pointClusters.Count; ++index)
        {
          foreach (Point point2 in pointClusters[index])
            clusterIndexMap[point2.X, point2.Y] = index;
        }
        for (int index5 = 0; index5 < pointClusters.Count; ++index5)
        {
          foreach (Point point3 in pointClusters[index5])
          {
            int x = point3.X;
            int y3 = point3.Y;
            if (clusterIndexMap[x, y3] != -1)
            {
              int index6 = clusterIndexMap[x, y3];
              if (x > 0)
                DesertHive.ClusterGroup.AttemptClaim(x - 1, y3, clusterIndexMap, pointClusters, index6);
              if (x < clusterIndexMap.GetLength(0) - 1)
                DesertHive.ClusterGroup.AttemptClaim(x + 1, y3, clusterIndexMap, pointClusters, index6);
              if (y3 > 0)
                DesertHive.ClusterGroup.AttemptClaim(x, y3 - 1, clusterIndexMap, pointClusters, index6);
              if (y3 < clusterIndexMap.GetLength(1) - 1)
                DesertHive.ClusterGroup.AttemptClaim(x, y3 + 1, clusterIndexMap, pointClusters, index6);
            }
            else
              break;
          }
        }
        foreach (List<Point> pointList in pointClusters)
          pointList.Clear();
        for (int x = 0; x < clusterIndexMap.GetLength(0); ++x)
        {
          for (int y4 = 0; y4 < clusterIndexMap.GetLength(1); ++y4)
          {
            if (clusterIndexMap[x, y4] != -1)
              pointClusters[clusterIndexMap[x, y4]].Add(new Point(x, y4));
          }
        }
        foreach (List<Point> pointList in pointClusters)
        {
          if (pointList.Count < 4)
            pointList.Clear();
        }
        foreach (List<Point> pointList in pointClusters)
        {
          DesertHive.Cluster cluster = new DesertHive.Cluster();
          if (pointList.Count > 0)
          {
            foreach (Point point4 in pointList)
              cluster.Add(new DesertHive.Block((double) point4.X + (WorldGen.genRand.NextDouble() - 0.5) * 0.5, (double) point4.Y + (WorldGen.genRand.NextDouble() - 0.5) * 0.5));
            this.Add(cluster);
          }
        }
      }
    }

    [Flags]
    private enum PostPlacementEffect : byte
    {
      None = 0,
      Smooth = 1,
    }
  }
}
