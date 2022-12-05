// Decompiled with JetBrains decompiler
// Type: Terraria.Liquid
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using System.Collections.Generic;
using Terraria.GameContent.NetModules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ObjectData;
using Terraria.WorldBuilding;

namespace Terraria
{
  public class Liquid
  {
    public const int maxLiquidBuffer = 50000;
    public static int maxLiquid = 25000;
    public static int skipCount;
    public static int stuckCount;
    public static int stuckAmount;
    public static int cycles = 10;
    public static int curMaxLiquid = 0;
    public static int numLiquid;
    public static bool stuck;
    public static bool quickFall;
    public static bool quickSettle;
    private static int wetCounter;
    public static int panicCounter;
    public static bool panicMode;
    public static int panicY;
    public int x;
    public int y;
    public int kill;
    public int delay;
    private static HashSet<int> _netChangeSet = new HashSet<int>();
    private static HashSet<int> _swapNetChangeSet = new HashSet<int>();

    public static void NetSendLiquid(int x, int y)
    {
      if (WorldGen.gen)
        return;
      lock (Liquid._netChangeSet)
        Liquid._netChangeSet.Add((x & (int) ushort.MaxValue) << 16 | y & (int) ushort.MaxValue);
    }

    public static void tilesIgnoreWater(bool ignoreSolids)
    {
      Main.tileSolid[138] = !ignoreSolids;
      Main.tileSolid[484] = !ignoreSolids;
      Main.tileSolid[546] = !ignoreSolids;
    }

    public static void worldGenTilesIgnoreWater(bool ignoreSolids)
    {
      Main.tileSolid[10] = !ignoreSolids;
      Main.tileSolid[192] = !ignoreSolids;
      Main.tileSolid[191] = !ignoreSolids;
      Main.tileSolid[190] = !ignoreSolids;
    }

    public static void ReInit()
    {
      Liquid.skipCount = 0;
      Liquid.stuckCount = 0;
      Liquid.stuckAmount = 0;
      Liquid.cycles = 10;
      Liquid.curMaxLiquid = Liquid.maxLiquid;
      Liquid.numLiquid = 0;
      Liquid.stuck = false;
      Liquid.quickFall = false;
      Liquid.quickSettle = false;
      Liquid.wetCounter = 0;
      Liquid.panicCounter = 0;
      Liquid.panicMode = false;
      Liquid.panicY = 0;
      if (!Main.Setting_UseReducedMaxLiquids)
        return;
      Liquid.curMaxLiquid = 5000;
    }

    public static void QuickWater(int verbose = 0, int minY = -1, int maxY = -1)
    {
      if (WorldGen.gen)
      {
        WorldGen.ShimmerRemoveWater();
        if (WorldGen.noTrapsWorldGen)
          Main.tileSolid[138] = false;
      }
      Main.tileSolid[379] = true;
      Liquid.tilesIgnoreWater(true);
      if (minY == -1)
        minY = 3;
      if (maxY == -1)
        maxY = Main.maxTilesY - 3;
      for (int index = maxY; index >= minY; --index)
      {
        Liquid.UpdateProgressDisplay(verbose, minY, maxY, index);
        for (int originX = 4; originX < Main.maxTilesX - 4; ++originX)
        {
          if (Main.tile[originX, index].liquid != (byte) 0)
            Liquid.SettleWaterAt(originX, index);
        }
      }
      Liquid.tilesIgnoreWater(false);
      if (!WorldGen.gen)
        return;
      WorldGen.ShimmerRemoveWater();
      if (!WorldGen.noTrapsWorldGen)
        return;
      Main.tileSolid[138] = true;
    }

    private static void SettleWaterAt(int originX, int originY)
    {
      Tile tile1 = Main.tile[originX, originY];
      Liquid.tilesIgnoreWater(true);
      if (tile1.liquid == (byte) 0)
        return;
      int index1 = originX;
      int index2 = originY;
      bool tileAtXYHasLava = tile1.lava();
      bool tileAtXYHasHoney = tile1.honey();
      bool tileAtXYHasShimmer = tile1.shimmer();
      int liquid = (int) tile1.liquid;
      byte liquidType = tile1.liquidType();
      tile1.liquid = (byte) 0;
      bool flag1 = true;
      while (true)
      {
        Tile tile2 = Main.tile[index1, index2 + 1];
        bool flag2 = false;
        for (; index2 < Main.maxTilesY - 5 && tile2.liquid == (byte) 0 && (!tile2.nactive() || !Main.tileSolid[(int) tile2.type] || Main.tileSolidTop[(int) tile2.type]); tile2 = Main.tile[index1, index2 + 1])
        {
          ++index2;
          flag2 = true;
          flag1 = false;
        }
        if (WorldGen.gen & flag2 && !tileAtXYHasHoney && !tileAtXYHasShimmer)
        {
          if (WorldGen.remixWorldGen)
            liquidType = index2 <= GenVars.lavaLine || (double) index2 >= Main.rockLayer - 80.0 && index2 <= Main.maxTilesY - 350 ? (byte) 0 : (!WorldGen.oceanDepths(index1, index2) ? (byte) 1 : (byte) 0);
          else if (index2 > GenVars.waterLine)
            liquidType = (byte) 1;
        }
        int num1 = -1;
        int num2 = 0;
        int num3 = -1;
        int num4 = 0;
        bool flag3 = false;
        bool flag4 = false;
        bool flag5 = false;
        while (true)
        {
          if (Main.tile[index1 + num2 * num1, index2].liquid == (byte) 0)
          {
            num3 = num1;
            num4 = num2;
          }
          if (num1 == -1 && index1 + num2 * num1 < 5)
            flag4 = true;
          else if (num1 == 1 && index1 + num2 * num1 > Main.maxTilesX - 5)
            flag3 = true;
          Tile tile3 = Main.tile[index1 + num2 * num1, index2 + 1];
          if (tile3.liquid != (byte) 0 && tile3.liquid != byte.MaxValue && (int) tile3.liquidType() == (int) liquidType)
          {
            int num5 = (int) byte.MaxValue - (int) tile3.liquid;
            if (num5 > liquid)
              num5 = liquid;
            tile3.liquid += (byte) num5;
            liquid -= num5;
            if (liquid == 0)
              goto label_37;
          }
          if (index2 >= Main.maxTilesY - 5 || tile3.liquid != (byte) 0 || tile3.nactive() && Main.tileSolid[(int) tile3.type] && !Main.tileSolidTop[(int) tile3.type])
          {
            Tile tile4 = Main.tile[index1 + (num2 + 1) * num1, index2];
            if (tile4.liquid != (byte) 0 && (!flag1 || num1 != 1) || tile4.nactive() && Main.tileSolid[(int) tile4.type] && !Main.tileSolidTop[(int) tile4.type])
            {
              if (num1 == 1)
                flag3 = true;
              else
                flag4 = true;
            }
            if (!(flag4 & flag3))
            {
              if (flag3)
              {
                num1 = -1;
                ++num2;
              }
              else if (flag4)
              {
                if (num1 == 1)
                  ++num2;
                num1 = 1;
              }
              else
              {
                if (num1 == 1)
                  ++num2;
                num1 = -num1;
              }
            }
            else
              goto label_37;
          }
          else
            break;
        }
        flag5 = true;
label_37:
        index1 += num4 * num3;
        if (liquid != 0 && flag5)
          ++index2;
        else
          break;
      }
      Main.tile[index1, index2].liquid = (byte) liquid;
      Main.tile[index1, index2].liquidType((int) liquidType);
      if (Main.tile[index1, index2].liquid > (byte) 0)
      {
        Liquid.AttemptToMoveLava(index1, index2, tileAtXYHasLava);
        Liquid.AttemptToMoveHoney(index1, index2, tileAtXYHasHoney);
        Liquid.AttemptToMoveShimmer(index1, index2, tileAtXYHasShimmer);
      }
      Liquid.tilesIgnoreWater(false);
    }

    private static void AttemptToMoveHoney(int X, int Y, bool tileAtXYHasHoney)
    {
      if (Main.tile[X - 1, Y].liquid > (byte) 0 && Main.tile[X - 1, Y].honey() != tileAtXYHasHoney)
      {
        if (tileAtXYHasHoney)
          Liquid.HoneyCheck(X, Y);
        else
          Liquid.HoneyCheck(X - 1, Y);
      }
      else if (Main.tile[X + 1, Y].liquid > (byte) 0 && Main.tile[X + 1, Y].honey() != tileAtXYHasHoney)
      {
        if (tileAtXYHasHoney)
          Liquid.HoneyCheck(X, Y);
        else
          Liquid.HoneyCheck(X + 1, Y);
      }
      else if (Main.tile[X, Y - 1].liquid > (byte) 0 && Main.tile[X, Y - 1].honey() != tileAtXYHasHoney)
      {
        if (tileAtXYHasHoney)
          Liquid.HoneyCheck(X, Y);
        else
          Liquid.HoneyCheck(X, Y - 1);
      }
      else
      {
        if (Main.tile[X, Y + 1].liquid <= (byte) 0 || Main.tile[X, Y + 1].honey() == tileAtXYHasHoney)
          return;
        if (tileAtXYHasHoney)
          Liquid.HoneyCheck(X, Y);
        else
          Liquid.HoneyCheck(X, Y + 1);
      }
    }

    private static void AttemptToMoveLava(int X, int Y, bool tileAtXYHasLava)
    {
      if (Main.tile[X - 1, Y].liquid > (byte) 0 && Main.tile[X - 1, Y].lava() != tileAtXYHasLava)
      {
        if (tileAtXYHasLava)
          Liquid.LavaCheck(X, Y);
        else
          Liquid.LavaCheck(X - 1, Y);
      }
      else if (Main.tile[X + 1, Y].liquid > (byte) 0 && Main.tile[X + 1, Y].lava() != tileAtXYHasLava)
      {
        if (tileAtXYHasLava)
          Liquid.LavaCheck(X, Y);
        else
          Liquid.LavaCheck(X + 1, Y);
      }
      else if (Main.tile[X, Y - 1].liquid > (byte) 0 && Main.tile[X, Y - 1].lava() != tileAtXYHasLava)
      {
        if (tileAtXYHasLava)
          Liquid.LavaCheck(X, Y);
        else
          Liquid.LavaCheck(X, Y - 1);
      }
      else
      {
        if (Main.tile[X, Y + 1].liquid <= (byte) 0 || Main.tile[X, Y + 1].lava() == tileAtXYHasLava)
          return;
        if (tileAtXYHasLava)
          Liquid.LavaCheck(X, Y);
        else
          Liquid.LavaCheck(X, Y + 1);
      }
    }

    private static void AttemptToMoveShimmer(int X, int Y, bool tileAtXYHasShimmer)
    {
      if (Main.tile[X - 1, Y].liquid > (byte) 0 && Main.tile[X - 1, Y].shimmer() != tileAtXYHasShimmer)
      {
        if (tileAtXYHasShimmer)
          Liquid.ShimmerCheck(X, Y);
        else
          Liquid.ShimmerCheck(X - 1, Y);
      }
      else if (Main.tile[X + 1, Y].liquid > (byte) 0 && Main.tile[X + 1, Y].shimmer() != tileAtXYHasShimmer)
      {
        if (tileAtXYHasShimmer)
          Liquid.ShimmerCheck(X, Y);
        else
          Liquid.ShimmerCheck(X + 1, Y);
      }
      else if (Main.tile[X, Y - 1].liquid > (byte) 0 && Main.tile[X, Y - 1].shimmer() != tileAtXYHasShimmer)
      {
        if (tileAtXYHasShimmer)
          Liquid.ShimmerCheck(X, Y);
        else
          Liquid.ShimmerCheck(X, Y - 1);
      }
      else
      {
        if (Main.tile[X, Y + 1].liquid <= (byte) 0 || Main.tile[X, Y + 1].shimmer() == tileAtXYHasShimmer)
          return;
        if (tileAtXYHasShimmer)
          Liquid.ShimmerCheck(X, Y);
        else
          Liquid.ShimmerCheck(X, Y + 1);
      }
    }

    private static void UpdateProgressDisplay(int verbose, int minY, int maxY, int y)
    {
      if (verbose > 0)
      {
        float num = (float) (maxY - y) / (float) (maxY - minY + 1) / (float) verbose;
        Main.statusText = Lang.gen[27].Value + " " + (object) (int) ((double) num * 100.0 + 1.0) + "%";
      }
      else
      {
        if (verbose >= 0)
          return;
        float num = (float) (maxY - y) / (float) (maxY - minY + 1) / (float) -verbose;
        Main.statusText = Lang.gen[18].Value + " " + (object) (int) ((double) num * 100.0 + 1.0) + "%";
      }
    }

    public void Update()
    {
      Main.tileSolid[379] = true;
      Tile tile1 = Main.tile[this.x - 1, this.y];
      Tile tile2 = Main.tile[this.x + 1, this.y];
      Tile tile3 = Main.tile[this.x, this.y - 1];
      Tile tile4 = Main.tile[this.x, this.y + 1];
      Tile tile5 = Main.tile[this.x, this.y];
      if (tile5.nactive() && Main.tileSolid[(int) tile5.type] && !Main.tileSolidTop[(int) tile5.type])
      {
        int type = (int) tile5.type;
        this.kill = 999;
      }
      else
      {
        byte liquid = tile5.liquid;
        if (this.y > Main.UnderworldLayer && tile5.liquidType() == (byte) 0 && tile5.liquid > (byte) 0)
        {
          byte num = 2;
          if ((int) tile5.liquid < (int) num)
            num = tile5.liquid;
          tile5.liquid -= num;
        }
        if (tile5.liquid == (byte) 0)
        {
          this.kill = 999;
        }
        else
        {
          if (tile5.lava())
          {
            Liquid.LavaCheck(this.x, this.y);
            if (!Liquid.quickFall)
            {
              if (this.delay < 5)
              {
                ++this.delay;
                return;
              }
              this.delay = 0;
            }
          }
          else
          {
            if (tile1.lava())
              Liquid.AddWater(this.x - 1, this.y);
            if (tile2.lava())
              Liquid.AddWater(this.x + 1, this.y);
            if (tile3.lava())
              Liquid.AddWater(this.x, this.y - 1);
            if (tile4.lava())
              Liquid.AddWater(this.x, this.y + 1);
            if (tile5.honey())
            {
              Liquid.HoneyCheck(this.x, this.y);
              if (!Liquid.quickFall)
              {
                if (this.delay < 10)
                {
                  ++this.delay;
                  return;
                }
                this.delay = 0;
              }
            }
            else
            {
              if (tile1.honey())
                Liquid.AddWater(this.x - 1, this.y);
              if (tile2.honey())
                Liquid.AddWater(this.x + 1, this.y);
              if (tile3.honey())
                Liquid.AddWater(this.x, this.y - 1);
              if (tile4.honey())
                Liquid.AddWater(this.x, this.y + 1);
              if (tile5.shimmer())
              {
                Liquid.ShimmerCheck(this.x, this.y);
              }
              else
              {
                if (tile1.shimmer())
                  Liquid.AddWater(this.x - 1, this.y);
                if (tile2.shimmer())
                  Liquid.AddWater(this.x + 1, this.y);
                if (tile3.shimmer())
                  Liquid.AddWater(this.x, this.y - 1);
                if (tile4.shimmer())
                  Liquid.AddWater(this.x, this.y + 1);
              }
            }
          }
          if ((!tile4.nactive() || !Main.tileSolid[(int) tile4.type] || Main.tileSolidTop[(int) tile4.type]) && (tile4.liquid <= (byte) 0 || (int) tile4.liquidType() == (int) tile5.liquidType()) && tile4.liquid < byte.MaxValue)
          {
            bool flag = false;
            float num = (float) ((int) byte.MaxValue - (int) tile4.liquid);
            if ((double) num > (double) tile5.liquid)
              num = (float) tile5.liquid;
            if ((double) num == 1.0 && tile5.liquid == byte.MaxValue)
              flag = true;
            if (!flag)
              tile5.liquid -= (byte) num;
            tile4.liquid += (byte) num;
            tile4.liquidType((int) tile5.liquidType());
            Liquid.AddWater(this.x, this.y + 1);
            tile4.skipLiquid(true);
            tile5.skipLiquid(true);
            if (Liquid.quickSettle && tile5.liquid > (byte) 250)
              tile5.liquid = byte.MaxValue;
            else if (!flag)
            {
              Liquid.AddWater(this.x - 1, this.y);
              Liquid.AddWater(this.x + 1, this.y);
            }
          }
          if (tile5.liquid > (byte) 0)
          {
            bool flag1 = true;
            bool flag2 = true;
            bool flag3 = true;
            bool flag4 = true;
            if (tile1.nactive() && Main.tileSolid[(int) tile1.type] && !Main.tileSolidTop[(int) tile1.type])
              flag1 = false;
            else if (tile1.liquid > (byte) 0 && (int) tile1.liquidType() != (int) tile5.liquidType())
              flag1 = false;
            else if (Main.tile[this.x - 2, this.y].nactive() && Main.tileSolid[(int) Main.tile[this.x - 2, this.y].type] && !Main.tileSolidTop[(int) Main.tile[this.x - 2, this.y].type])
              flag3 = false;
            else if (Main.tile[this.x - 2, this.y].liquid == (byte) 0)
              flag3 = false;
            else if (Main.tile[this.x - 2, this.y].liquid > (byte) 0 && (int) Main.tile[this.x - 2, this.y].liquidType() != (int) tile5.liquidType())
              flag3 = false;
            if (tile2.nactive() && Main.tileSolid[(int) tile2.type] && !Main.tileSolidTop[(int) tile2.type])
              flag2 = false;
            else if (tile2.liquid > (byte) 0 && (int) tile2.liquidType() != (int) tile5.liquidType())
              flag2 = false;
            else if (Main.tile[this.x + 2, this.y].nactive() && Main.tileSolid[(int) Main.tile[this.x + 2, this.y].type] && !Main.tileSolidTop[(int) Main.tile[this.x + 2, this.y].type])
              flag4 = false;
            else if (Main.tile[this.x + 2, this.y].liquid == (byte) 0)
              flag4 = false;
            else if (Main.tile[this.x + 2, this.y].liquid > (byte) 0 && (int) Main.tile[this.x + 2, this.y].liquidType() != (int) tile5.liquidType())
              flag4 = false;
            int num1 = 0;
            if (tile5.liquid < (byte) 3)
              num1 = -1;
            if (tile5.liquid > (byte) 250)
            {
              flag3 = false;
              flag4 = false;
            }
            if (flag1 & flag2)
            {
              if (flag3 & flag4)
              {
                bool flag5 = true;
                bool flag6 = true;
                if (Main.tile[this.x - 3, this.y].nactive() && Main.tileSolid[(int) Main.tile[this.x - 3, this.y].type] && !Main.tileSolidTop[(int) Main.tile[this.x - 3, this.y].type])
                  flag5 = false;
                else if (Main.tile[this.x - 3, this.y].liquid == (byte) 0)
                  flag5 = false;
                else if ((int) Main.tile[this.x - 3, this.y].liquidType() != (int) tile5.liquidType())
                  flag5 = false;
                if (Main.tile[this.x + 3, this.y].nactive() && Main.tileSolid[(int) Main.tile[this.x + 3, this.y].type] && !Main.tileSolidTop[(int) Main.tile[this.x + 3, this.y].type])
                  flag6 = false;
                else if (Main.tile[this.x + 3, this.y].liquid == (byte) 0)
                  flag6 = false;
                else if ((int) Main.tile[this.x + 3, this.y].liquidType() != (int) tile5.liquidType())
                  flag6 = false;
                if (flag5 & flag6)
                {
                  float num2 = (float) Math.Round((double) ((int) tile1.liquid + (int) tile2.liquid + (int) Main.tile[this.x - 2, this.y].liquid + (int) Main.tile[this.x + 2, this.y].liquid + (int) Main.tile[this.x - 3, this.y].liquid + (int) Main.tile[this.x + 3, this.y].liquid + (int) tile5.liquid + num1) / 7.0);
                  int num3 = 0;
                  tile1.liquidType((int) tile5.liquidType());
                  if ((int) tile1.liquid != (int) (byte) num2)
                  {
                    tile1.liquid = (byte) num2;
                    Liquid.AddWater(this.x - 1, this.y);
                  }
                  else
                    ++num3;
                  tile2.liquidType((int) tile5.liquidType());
                  if ((int) tile2.liquid != (int) (byte) num2)
                  {
                    tile2.liquid = (byte) num2;
                    Liquid.AddWater(this.x + 1, this.y);
                  }
                  else
                    ++num3;
                  Main.tile[this.x - 2, this.y].liquidType((int) tile5.liquidType());
                  if ((int) Main.tile[this.x - 2, this.y].liquid != (int) (byte) num2)
                  {
                    Main.tile[this.x - 2, this.y].liquid = (byte) num2;
                    Liquid.AddWater(this.x - 2, this.y);
                  }
                  else
                    ++num3;
                  Main.tile[this.x + 2, this.y].liquidType((int) tile5.liquidType());
                  if ((int) Main.tile[this.x + 2, this.y].liquid != (int) (byte) num2)
                  {
                    Main.tile[this.x + 2, this.y].liquid = (byte) num2;
                    Liquid.AddWater(this.x + 2, this.y);
                  }
                  else
                    ++num3;
                  Main.tile[this.x - 3, this.y].liquidType((int) tile5.liquidType());
                  if ((int) Main.tile[this.x - 3, this.y].liquid != (int) (byte) num2)
                  {
                    Main.tile[this.x - 3, this.y].liquid = (byte) num2;
                    Liquid.AddWater(this.x - 3, this.y);
                  }
                  else
                    ++num3;
                  Main.tile[this.x + 3, this.y].liquidType((int) tile5.liquidType());
                  if ((int) Main.tile[this.x + 3, this.y].liquid != (int) (byte) num2)
                  {
                    Main.tile[this.x + 3, this.y].liquid = (byte) num2;
                    Liquid.AddWater(this.x + 3, this.y);
                  }
                  else
                    ++num3;
                  if ((int) tile1.liquid != (int) (byte) num2 || (int) tile5.liquid != (int) (byte) num2)
                    Liquid.AddWater(this.x - 1, this.y);
                  if ((int) tile2.liquid != (int) (byte) num2 || (int) tile5.liquid != (int) (byte) num2)
                    Liquid.AddWater(this.x + 1, this.y);
                  if ((int) Main.tile[this.x - 2, this.y].liquid != (int) (byte) num2 || (int) tile5.liquid != (int) (byte) num2)
                    Liquid.AddWater(this.x - 2, this.y);
                  if ((int) Main.tile[this.x + 2, this.y].liquid != (int) (byte) num2 || (int) tile5.liquid != (int) (byte) num2)
                    Liquid.AddWater(this.x + 2, this.y);
                  if ((int) Main.tile[this.x - 3, this.y].liquid != (int) (byte) num2 || (int) tile5.liquid != (int) (byte) num2)
                    Liquid.AddWater(this.x - 3, this.y);
                  if ((int) Main.tile[this.x + 3, this.y].liquid != (int) (byte) num2 || (int) tile5.liquid != (int) (byte) num2)
                    Liquid.AddWater(this.x + 3, this.y);
                  if (num3 != 6 || tile3.liquid <= (byte) 0)
                    tile5.liquid = (byte) num2;
                }
                else
                {
                  int num4 = 0;
                  float num5 = (float) Math.Round((double) ((int) tile1.liquid + (int) tile2.liquid + (int) Main.tile[this.x - 2, this.y].liquid + (int) Main.tile[this.x + 2, this.y].liquid + (int) tile5.liquid + num1) / 5.0);
                  tile1.liquidType((int) tile5.liquidType());
                  if ((int) tile1.liquid != (int) (byte) num5)
                  {
                    tile1.liquid = (byte) num5;
                    Liquid.AddWater(this.x - 1, this.y);
                  }
                  else
                    ++num4;
                  tile2.liquidType((int) tile5.liquidType());
                  if ((int) tile2.liquid != (int) (byte) num5)
                  {
                    tile2.liquid = (byte) num5;
                    Liquid.AddWater(this.x + 1, this.y);
                  }
                  else
                    ++num4;
                  Main.tile[this.x - 2, this.y].liquidType((int) tile5.liquidType());
                  if ((int) Main.tile[this.x - 2, this.y].liquid != (int) (byte) num5)
                  {
                    Main.tile[this.x - 2, this.y].liquid = (byte) num5;
                    Liquid.AddWater(this.x - 2, this.y);
                  }
                  else
                    ++num4;
                  Main.tile[this.x + 2, this.y].liquidType((int) tile5.liquidType());
                  if ((int) Main.tile[this.x + 2, this.y].liquid != (int) (byte) num5)
                  {
                    Main.tile[this.x + 2, this.y].liquid = (byte) num5;
                    Liquid.AddWater(this.x + 2, this.y);
                  }
                  else
                    ++num4;
                  if ((int) tile1.liquid != (int) (byte) num5 || (int) tile5.liquid != (int) (byte) num5)
                    Liquid.AddWater(this.x - 1, this.y);
                  if ((int) tile2.liquid != (int) (byte) num5 || (int) tile5.liquid != (int) (byte) num5)
                    Liquid.AddWater(this.x + 1, this.y);
                  if ((int) Main.tile[this.x - 2, this.y].liquid != (int) (byte) num5 || (int) tile5.liquid != (int) (byte) num5)
                    Liquid.AddWater(this.x - 2, this.y);
                  if ((int) Main.tile[this.x + 2, this.y].liquid != (int) (byte) num5 || (int) tile5.liquid != (int) (byte) num5)
                    Liquid.AddWater(this.x + 2, this.y);
                  if (num4 != 4 || tile3.liquid <= (byte) 0)
                    tile5.liquid = (byte) num5;
                }
              }
              else if (flag3)
              {
                float num6 = (float) Math.Round((double) ((int) tile1.liquid + (int) tile2.liquid + (int) Main.tile[this.x - 2, this.y].liquid + (int) tile5.liquid + num1) / 4.0);
                tile1.liquidType((int) tile5.liquidType());
                if ((int) tile1.liquid != (int) (byte) num6 || (int) tile5.liquid != (int) (byte) num6)
                {
                  tile1.liquid = (byte) num6;
                  Liquid.AddWater(this.x - 1, this.y);
                }
                tile2.liquidType((int) tile5.liquidType());
                if ((int) tile2.liquid != (int) (byte) num6 || (int) tile5.liquid != (int) (byte) num6)
                {
                  tile2.liquid = (byte) num6;
                  Liquid.AddWater(this.x + 1, this.y);
                }
                Main.tile[this.x - 2, this.y].liquidType((int) tile5.liquidType());
                if ((int) Main.tile[this.x - 2, this.y].liquid != (int) (byte) num6 || (int) tile5.liquid != (int) (byte) num6)
                {
                  Main.tile[this.x - 2, this.y].liquid = (byte) num6;
                  Liquid.AddWater(this.x - 2, this.y);
                }
                tile5.liquid = (byte) num6;
              }
              else if (flag4)
              {
                float num7 = (float) Math.Round((double) ((int) tile1.liquid + (int) tile2.liquid + (int) Main.tile[this.x + 2, this.y].liquid + (int) tile5.liquid + num1) / 4.0);
                tile1.liquidType((int) tile5.liquidType());
                if ((int) tile1.liquid != (int) (byte) num7 || (int) tile5.liquid != (int) (byte) num7)
                {
                  tile1.liquid = (byte) num7;
                  Liquid.AddWater(this.x - 1, this.y);
                }
                tile2.liquidType((int) tile5.liquidType());
                if ((int) tile2.liquid != (int) (byte) num7 || (int) tile5.liquid != (int) (byte) num7)
                {
                  tile2.liquid = (byte) num7;
                  Liquid.AddWater(this.x + 1, this.y);
                }
                Main.tile[this.x + 2, this.y].liquidType((int) tile5.liquidType());
                if ((int) Main.tile[this.x + 2, this.y].liquid != (int) (byte) num7 || (int) tile5.liquid != (int) (byte) num7)
                {
                  Main.tile[this.x + 2, this.y].liquid = (byte) num7;
                  Liquid.AddWater(this.x + 2, this.y);
                }
                tile5.liquid = (byte) num7;
              }
              else
              {
                float num8 = (float) Math.Round((double) ((int) tile1.liquid + (int) tile2.liquid + (int) tile5.liquid + num1) / 3.0);
                if ((double) num8 == 254.0 && WorldGen.genRand.Next(30) == 0)
                  num8 = (float) byte.MaxValue;
                tile1.liquidType((int) tile5.liquidType());
                if ((int) tile1.liquid != (int) (byte) num8)
                {
                  tile1.liquid = (byte) num8;
                  Liquid.AddWater(this.x - 1, this.y);
                }
                tile2.liquidType((int) tile5.liquidType());
                if ((int) tile2.liquid != (int) (byte) num8)
                {
                  tile2.liquid = (byte) num8;
                  Liquid.AddWater(this.x + 1, this.y);
                }
                tile5.liquid = (byte) num8;
              }
            }
            else if (flag1)
            {
              float num9 = (float) Math.Round((double) ((int) tile1.liquid + (int) tile5.liquid + num1) / 2.0);
              if ((int) tile1.liquid != (int) (byte) num9)
                tile1.liquid = (byte) num9;
              tile1.liquidType((int) tile5.liquidType());
              if ((int) tile5.liquid != (int) (byte) num9 || (int) tile1.liquid != (int) (byte) num9)
                Liquid.AddWater(this.x - 1, this.y);
              tile5.liquid = (byte) num9;
            }
            else if (flag2)
            {
              float num10 = (float) Math.Round((double) ((int) tile2.liquid + (int) tile5.liquid + num1) / 2.0);
              if ((int) tile2.liquid != (int) (byte) num10)
                tile2.liquid = (byte) num10;
              tile2.liquidType((int) tile5.liquidType());
              if ((int) tile5.liquid != (int) (byte) num10 || (int) tile2.liquid != (int) (byte) num10)
                Liquid.AddWater(this.x + 1, this.y);
              tile5.liquid = (byte) num10;
            }
          }
          if ((int) tile5.liquid != (int) liquid)
          {
            if (tile5.liquid == (byte) 254 && liquid == byte.MaxValue)
            {
              if (Liquid.quickSettle)
              {
                tile5.liquid = byte.MaxValue;
                ++this.kill;
              }
              else
                ++this.kill;
            }
            else
            {
              Liquid.AddWater(this.x, this.y - 1);
              this.kill = 0;
            }
          }
          else
            ++this.kill;
        }
      }
    }

    public static void StartPanic()
    {
      if (Liquid.panicMode)
        return;
      GenVars.waterLine = Main.maxTilesY;
      Liquid.numLiquid = 0;
      LiquidBuffer.numLiquidBuffer = 0;
      Liquid.panicCounter = 0;
      Liquid.panicMode = true;
      Liquid.panicY = Main.maxTilesY - 3;
      if (!Main.dedServ)
        return;
      Console.WriteLine(Language.GetTextValue("Misc.ForceWaterSettling"));
    }

    public static void UpdateLiquid()
    {
      int num1 = 8;
      Liquid.tilesIgnoreWater(true);
      if (Main.netMode == 2)
      {
        int num2 = 0;
        for (int index = 0; index < 15; ++index)
        {
          if (Main.player[index].active)
            ++num2;
        }
        Liquid.cycles = 10 + num2 / 3;
        Liquid.curMaxLiquid = Liquid.maxLiquid - num2 * 250;
        num1 = 10 + num2 / 3;
        if (Main.Setting_UseReducedMaxLiquids)
          Liquid.curMaxLiquid = 5000;
      }
      if (!WorldGen.gen)
      {
        if (!Liquid.panicMode)
        {
          if ((double) LiquidBuffer.numLiquidBuffer >= 45000.0)
          {
            ++Liquid.panicCounter;
            if (Liquid.panicCounter > 3600)
              Liquid.StartPanic();
          }
          else
            Liquid.panicCounter = 0;
        }
        if (Liquid.panicMode)
        {
          int num3 = 0;
          while (Liquid.panicY >= 3 && num3 < 5)
          {
            ++num3;
            Liquid.QuickWater(minY: Liquid.panicY, maxY: Liquid.panicY);
            --Liquid.panicY;
            if (Liquid.panicY < 3)
            {
              Console.WriteLine(Language.GetTextValue("Misc.WaterSettled"));
              Liquid.panicCounter = 0;
              Liquid.panicMode = false;
              WorldGen.WaterCheck();
              if (Main.netMode == 2)
              {
                for (int index1 = 0; index1 < (int) byte.MaxValue; ++index1)
                {
                  for (int index2 = 0; index2 < Main.maxSectionsX; ++index2)
                  {
                    for (int index3 = 0; index3 < Main.maxSectionsY; ++index3)
                      Netplay.Clients[index1].TileSections[index2, index3] = false;
                  }
                }
              }
            }
          }
          return;
        }
      }
      bool quickSettle = Liquid.quickSettle;
      if (Main.Setting_UseReducedMaxLiquids)
        quickSettle |= Liquid.numLiquid > 2000;
      Liquid.quickFall = quickSettle;
      ++Liquid.wetCounter;
      int num4 = Liquid.curMaxLiquid / Liquid.cycles;
      int num5 = num4 * (Liquid.wetCounter - 1);
      int num6 = num4 * Liquid.wetCounter;
      if (Liquid.wetCounter == Liquid.cycles)
        num6 = Liquid.numLiquid;
      if (num6 > Liquid.numLiquid)
      {
        num6 = Liquid.numLiquid;
        int netMode = Main.netMode;
        Liquid.wetCounter = Liquid.cycles;
      }
      if (Liquid.quickFall)
      {
        for (int index = num5; index < num6; ++index)
        {
          Main.liquid[index].delay = 10;
          Main.liquid[index].Update();
          Main.tile[Main.liquid[index].x, Main.liquid[index].y].skipLiquid(false);
        }
      }
      else
      {
        for (int index = num5; index < num6; ++index)
        {
          if (!Main.tile[Main.liquid[index].x, Main.liquid[index].y].skipLiquid())
            Main.liquid[index].Update();
          else
            Main.tile[Main.liquid[index].x, Main.liquid[index].y].skipLiquid(false);
        }
      }
      if (Liquid.wetCounter >= Liquid.cycles)
      {
        Liquid.wetCounter = 0;
        for (int l = Liquid.numLiquid - 1; l >= 0; --l)
        {
          if (Main.liquid[l].kill >= num1)
          {
            if (Main.tile[Main.liquid[l].x, Main.liquid[l].y].liquid == (byte) 254)
              Main.tile[Main.liquid[l].x, Main.liquid[l].y].liquid = byte.MaxValue;
            Liquid.DelWater(l);
          }
        }
        int num7 = Liquid.curMaxLiquid - (Liquid.curMaxLiquid - Liquid.numLiquid);
        if (num7 > LiquidBuffer.numLiquidBuffer)
          num7 = LiquidBuffer.numLiquidBuffer;
        for (int index = 0; index < num7; ++index)
        {
          Main.tile[Main.liquidBuffer[0].x, Main.liquidBuffer[0].y].checkingLiquid(false);
          Liquid.AddWater(Main.liquidBuffer[0].x, Main.liquidBuffer[0].y);
          LiquidBuffer.DelBuffer(0);
        }
        if (Liquid.numLiquid > 0 && Liquid.numLiquid > Liquid.stuckAmount - 50 && Liquid.numLiquid < Liquid.stuckAmount + 50)
        {
          ++Liquid.stuckCount;
          if (Liquid.stuckCount >= 10000)
          {
            Liquid.stuck = true;
            for (int l = Liquid.numLiquid - 1; l >= 0; --l)
              Liquid.DelWater(l);
            Liquid.stuck = false;
            Liquid.stuckCount = 0;
          }
        }
        else
        {
          Liquid.stuckCount = 0;
          Liquid.stuckAmount = Liquid.numLiquid;
        }
      }
      if (!WorldGen.gen && Main.netMode == 2 && Liquid._netChangeSet.Count > 0)
      {
        Utils.Swap<HashSet<int>>(ref Liquid._netChangeSet, ref Liquid._swapNetChangeSet);
        NetLiquidModule.CreateAndBroadcastByChunk(Liquid._swapNetChangeSet);
        Liquid._swapNetChangeSet.Clear();
      }
      Liquid.tilesIgnoreWater(false);
    }

    public static void AddWater(int x, int y)
    {
      Tile checkTile = Main.tile[x, y];
      if (Main.tile[x, y] == null || checkTile.checkingLiquid() || x >= Main.maxTilesX - 5 || y >= Main.maxTilesY - 5 || x < 5 || y < 5 || checkTile.liquid == (byte) 0 || checkTile.nactive() && Main.tileSolid[(int) checkTile.type] && checkTile.type != (ushort) 546 && !Main.tileSolidTop[(int) checkTile.type])
        return;
      if (Liquid.numLiquid >= Liquid.curMaxLiquid - 1)
      {
        LiquidBuffer.AddBuffer(x, y);
      }
      else
      {
        checkTile.checkingLiquid(true);
        checkTile.skipLiquid(false);
        Main.liquid[Liquid.numLiquid].kill = 0;
        Main.liquid[Liquid.numLiquid].x = x;
        Main.liquid[Liquid.numLiquid].y = y;
        Main.liquid[Liquid.numLiquid].delay = 0;
        ++Liquid.numLiquid;
        if (Main.netMode == 2)
          Liquid.NetSendLiquid(x, y);
        if (!checkTile.active() || WorldGen.gen)
          return;
        bool flag = false;
        if (checkTile.lava())
        {
          if (TileObjectData.CheckLavaDeath(checkTile))
            flag = true;
        }
        else if (TileObjectData.CheckWaterDeath(checkTile))
          flag = true;
        if (!flag)
          return;
        WorldGen.KillTile(x, y);
        if (Main.netMode != 2)
          return;
        NetMessage.SendData(17, number2: ((float) x), number3: ((float) y));
      }
    }

    private static bool UndergroundDesertCheck(int x, int y)
    {
      int num = 3;
      for (int x1 = x - num; x1 <= x + num; ++x1)
      {
        for (int y1 = y - num; y1 <= y + num; ++y1)
        {
          if (WorldGen.InWorld(x1, y1) && (Main.tile[x1, y1].wall == (ushort) 187 || Main.tile[x1, y1].wall == (ushort) 216))
            return true;
        }
      }
      return false;
    }

    public static void LiquidCheck(int x, int y, int thisLiquidType)
    {
      if (WorldGen.SolidTile(x, y))
        return;
      Tile tile1 = Main.tile[x - 1, y];
      Tile tile2 = Main.tile[x + 1, y];
      Tile tile3 = Main.tile[x, y - 1];
      Tile tile4 = Main.tile[x, y + 1];
      Tile tile5 = Main.tile[x, y];
      if (tile1.liquid > (byte) 0 && (int) tile1.liquidType() != thisLiquidType || tile2.liquid > (byte) 0 && (int) tile2.liquidType() != thisLiquidType || tile3.liquid > (byte) 0 && (int) tile3.liquidType() != thisLiquidType)
      {
        int num = 0;
        if ((int) tile1.liquidType() != thisLiquidType)
        {
          num += (int) tile1.liquid;
          tile1.liquid = (byte) 0;
        }
        if ((int) tile2.liquidType() != thisLiquidType)
        {
          num += (int) tile2.liquid;
          tile2.liquid = (byte) 0;
        }
        if ((int) tile3.liquidType() != thisLiquidType)
        {
          num += (int) tile3.liquid;
          tile3.liquid = (byte) 0;
        }
        int liquidMergeTileType = 56;
        int liquidMergeType = 0;
        bool waterNearby = tile1.liquidType() == (byte) 0 || tile2.liquidType() == (byte) 0 || tile3.liquidType() == (byte) 0;
        bool lavaNearby = tile1.lava() || tile2.lava() || tile3.lava();
        bool honeyNearby = tile1.honey() || tile2.honey() || tile3.honey();
        bool shimmerNearby = tile1.shimmer() || tile2.shimmer() || tile3.shimmer();
        Liquid.GetLiquidMergeTypes(thisLiquidType, out liquidMergeTileType, out liquidMergeType, waterNearby, lavaNearby, honeyNearby, shimmerNearby);
        if (num < 24 || liquidMergeType == thisLiquidType)
          return;
        if (tile5.active() && Main.tileObsidianKill[(int) tile5.type])
        {
          WorldGen.KillTile(x, y);
          if (Main.netMode == 2)
            NetMessage.SendData(17, number2: ((float) x), number3: ((float) y));
        }
        if (tile5.active())
          return;
        tile5.liquid = (byte) 0;
        switch (thisLiquidType)
        {
          case 1:
            tile5.lava(false);
            break;
          case 2:
            tile5.honey(false);
            break;
          case 3:
            tile5.shimmer(false);
            break;
        }
        TileChangeType liquidChangeType = WorldGen.GetLiquidChangeType(thisLiquidType, liquidMergeType);
        if (!WorldGen.gen)
          WorldGen.PlayLiquidChangeSound(liquidChangeType, x, y);
        WorldGen.PlaceTile(x, y, liquidMergeTileType, true, true);
        WorldGen.SquareTileFrame(x, y);
        if (Main.netMode != 2)
          return;
        NetMessage.SendTileSquare(-1, x - 1, y - 1, 3, liquidChangeType);
      }
      else
      {
        if (tile4.liquid <= (byte) 0 || (int) tile4.liquidType() == thisLiquidType)
          return;
        bool flag = false;
        if (tile5.active() && TileID.Sets.IsAContainer[(int) tile5.type] && !TileID.Sets.IsAContainer[(int) tile4.type])
          flag = true;
        if (thisLiquidType != 0 && Main.tileCut[(int) tile4.type])
        {
          WorldGen.KillTile(x, y + 1);
          if (Main.netMode == 2)
            NetMessage.SendData(17, number2: ((float) x), number3: ((float) (y + 1)));
        }
        else if (tile4.active() && Main.tileObsidianKill[(int) tile4.type])
        {
          WorldGen.KillTile(x, y + 1);
          if (Main.netMode == 2)
            NetMessage.SendData(17, number2: ((float) x), number3: ((float) (y + 1)));
        }
        if (!(!tile4.active() | flag))
          return;
        if (tile5.liquid < (byte) 24)
        {
          tile5.liquid = (byte) 0;
          tile5.liquidType(0);
          if (Main.netMode != 2)
            return;
          NetMessage.SendTileSquare(-1, x - 1, y, 3);
        }
        else
        {
          int liquidMergeTileType = 56;
          int liquidMergeType = 0;
          bool waterNearby = tile4.liquidType() == (byte) 0;
          bool lavaNearby = tile4.lava();
          bool honeyNearby = tile4.honey();
          bool shimmerNearby = tile4.shimmer();
          Liquid.GetLiquidMergeTypes(thisLiquidType, out liquidMergeTileType, out liquidMergeType, waterNearby, lavaNearby, honeyNearby, shimmerNearby);
          tile5.liquid = (byte) 0;
          switch (thisLiquidType)
          {
            case 1:
              tile5.lava(false);
              break;
            case 2:
              tile5.honey(false);
              break;
            case 3:
              tile5.shimmer(false);
              break;
          }
          tile4.liquid = (byte) 0;
          TileChangeType liquidChangeType = WorldGen.GetLiquidChangeType(thisLiquidType, liquidMergeType);
          if (!Main.gameMenu)
            WorldGen.PlayLiquidChangeSound(liquidChangeType, x, y);
          WorldGen.PlaceTile(x, y + 1, liquidMergeTileType, true, true);
          WorldGen.SquareTileFrame(x, y + 1);
          if (Main.netMode != 2)
            return;
          NetMessage.SendTileSquare(-1, x - 1, y, 3, liquidChangeType);
        }
      }
    }

    public static void GetLiquidMergeTypes(
      int thisLiquidType,
      out int liquidMergeTileType,
      out int liquidMergeType,
      bool waterNearby,
      bool lavaNearby,
      bool honeyNearby,
      bool shimmerNearby)
    {
      liquidMergeTileType = 56;
      liquidMergeType = thisLiquidType;
      if (thisLiquidType != 0 & waterNearby)
      {
        switch (thisLiquidType)
        {
          case 1:
            liquidMergeTileType = 56;
            break;
          case 2:
            liquidMergeTileType = 229;
            break;
          case 3:
            liquidMergeTileType = 659;
            break;
        }
        liquidMergeType = 0;
      }
      if (thisLiquidType != 1 & lavaNearby)
      {
        switch (thisLiquidType)
        {
          case 0:
            liquidMergeTileType = 56;
            break;
          case 2:
            liquidMergeTileType = 230;
            break;
          case 3:
            liquidMergeTileType = 659;
            break;
        }
        liquidMergeType = 1;
      }
      if (thisLiquidType != 2 & honeyNearby)
      {
        switch (thisLiquidType)
        {
          case 0:
            liquidMergeTileType = 229;
            break;
          case 1:
            liquidMergeTileType = 230;
            break;
          case 3:
            liquidMergeTileType = 659;
            break;
        }
        liquidMergeType = 2;
      }
      if (!(thisLiquidType != 3 & shimmerNearby))
        return;
      switch (thisLiquidType)
      {
        case 0:
          liquidMergeTileType = 659;
          break;
        case 1:
          liquidMergeTileType = 659;
          break;
        case 2:
          liquidMergeTileType = 659;
          break;
      }
      liquidMergeType = 3;
    }

    public static void LavaCheck(int x, int y)
    {
      if (!WorldGen.remixWorldGen && WorldGen.generatingWorld && Liquid.UndergroundDesertCheck(x, y))
      {
        for (int index1 = x - 3; index1 <= x + 3; ++index1)
        {
          for (int index2 = y - 3; index2 <= y + 3; ++index2)
            Main.tile[index1, index2].lava(true);
        }
      }
      Liquid.LiquidCheck(x, y, 1);
    }

    public static void HoneyCheck(int x, int y) => Liquid.LiquidCheck(x, y, 2);

    public static void ShimmerCheck(int x, int y) => Liquid.LiquidCheck(x, y, 3);

    public static void DelWater(int l)
    {
      int x = Main.liquid[l].x;
      int y = Main.liquid[l].y;
      Tile tile1 = Main.tile[x - 1, y];
      Tile tile2 = Main.tile[x + 1, y];
      Tile tile3 = Main.tile[x, y + 1];
      Tile tile4 = Main.tile[x, y];
      byte num = 2;
      if ((int) tile4.liquid < (int) num)
      {
        tile4.liquid = (byte) 0;
        if ((int) tile1.liquid < (int) num)
          tile1.liquid = (byte) 0;
        else
          Liquid.AddWater(x - 1, y);
        if ((int) tile2.liquid < (int) num)
          tile2.liquid = (byte) 0;
        else
          Liquid.AddWater(x + 1, y);
      }
      else if (tile4.liquid < (byte) 20)
      {
        if ((int) tile1.liquid < (int) tile4.liquid && (!tile1.nactive() || !Main.tileSolid[(int) tile1.type] || Main.tileSolidTop[(int) tile1.type]) || (int) tile2.liquid < (int) tile4.liquid && (!tile2.nactive() || !Main.tileSolid[(int) tile2.type] || Main.tileSolidTop[(int) tile2.type]) || tile3.liquid < byte.MaxValue && (!tile3.nactive() || !Main.tileSolid[(int) tile3.type] || Main.tileSolidTop[(int) tile3.type]))
          tile4.liquid = (byte) 0;
      }
      else if (tile3.liquid < byte.MaxValue && (!tile3.nactive() || !Main.tileSolid[(int) tile3.type] || Main.tileSolidTop[(int) tile3.type]) && !Liquid.stuck && (!Main.tile[x, y].nactive() || !Main.tileSolid[(int) Main.tile[x, y].type] || Main.tileSolidTop[(int) Main.tile[x, y].type]))
      {
        Main.liquid[l].kill = 0;
        return;
      }
      if (tile4.liquid < (byte) 250 && Main.tile[x, y - 1].liquid > (byte) 0)
        Liquid.AddWater(x, y - 1);
      if (tile4.liquid == (byte) 0)
      {
        tile4.liquidType(0);
      }
      else
      {
        if (tile2.liquid > (byte) 0 && tile2.liquid < (byte) 250 && (!tile2.nactive() || !Main.tileSolid[(int) tile2.type] || Main.tileSolidTop[(int) tile2.type]) && (int) tile4.liquid != (int) tile2.liquid)
          Liquid.AddWater(x + 1, y);
        if (tile1.liquid > (byte) 0 && tile1.liquid < (byte) 250 && (!tile1.nactive() || !Main.tileSolid[(int) tile1.type] || Main.tileSolidTop[(int) tile1.type]) && (int) tile4.liquid != (int) tile1.liquid)
          Liquid.AddWater(x - 1, y);
        if (tile4.lava())
        {
          Liquid.LavaCheck(x, y);
          for (int i = x - 1; i <= x + 1; ++i)
          {
            for (int j = y - 1; j <= y + 1; ++j)
            {
              Tile tile5 = Main.tile[i, j];
              if (tile5.active())
              {
                if (tile5.type == (ushort) 2 || tile5.type == (ushort) 23 || tile5.type == (ushort) 109 || tile5.type == (ushort) 199 || tile5.type == (ushort) 477 || tile5.type == (ushort) 492)
                {
                  tile5.type = (ushort) 0;
                  WorldGen.SquareTileFrame(i, j);
                  if (Main.netMode == 2)
                    NetMessage.SendTileSquare(-1, x, y, 3);
                }
                else if (tile5.type == (ushort) 60 || tile5.type == (ushort) 70 || tile5.type == (ushort) 661 || tile5.type == (ushort) 662)
                {
                  tile5.type = (ushort) 59;
                  WorldGen.SquareTileFrame(i, j);
                  if (Main.netMode == 2)
                    NetMessage.SendTileSquare(-1, x, y, 3);
                }
              }
            }
          }
        }
        else if (tile4.honey())
          Liquid.HoneyCheck(x, y);
        else if (tile4.shimmer())
          Liquid.ShimmerCheck(x, y);
      }
      if (Main.netMode == 2)
        Liquid.NetSendLiquid(x, y);
      --Liquid.numLiquid;
      Main.tile[Main.liquid[l].x, Main.liquid[l].y].checkingLiquid(false);
      Main.liquid[l].x = Main.liquid[Liquid.numLiquid].x;
      Main.liquid[l].y = Main.liquid[Liquid.numLiquid].y;
      Main.liquid[l].kill = Main.liquid[Liquid.numLiquid].kill;
      if (Main.tileAlch[(int) tile4.type])
      {
        WorldGen.CheckAlch(x, y);
      }
      else
      {
        if (tile4.type != (ushort) 518)
          return;
        if (Liquid.quickFall)
          WorldGen.CheckLilyPad(x, y);
        else if (Main.tile[x, y + 1].liquid < byte.MaxValue || Main.tile[x, y - 1].liquid > (byte) 0)
          WorldGen.SquareTileFrame(x, y);
        else
          WorldGen.CheckLilyPad(x, y);
      }
    }
  }
}
