// Decompiled with JetBrains decompiler
// Type: Terraria.Wiring
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria
{
  public static class Wiring
  {
    public static bool blockPlayerTeleportationForOneIteration;
    public static bool running;
    private static Dictionary<Point16, bool> _wireSkip;
    private static DoubleStack<Point16> _wireList;
    private static DoubleStack<byte> _wireDirectionList;
    private static Dictionary<Point16, byte> _toProcess;
    private static Queue<Point16> _GatesCurrent;
    private static Queue<Point16> _LampsToCheck;
    private static Queue<Point16> _GatesNext;
    private static Dictionary<Point16, bool> _GatesDone;
    private static Dictionary<Point16, byte> _PixelBoxTriggers;
    private static Vector2[] _teleport;
    private const int MaxPump = 20;
    private static int[] _inPumpX;
    private static int[] _inPumpY;
    private static int _numInPump;
    private static int[] _outPumpX;
    private static int[] _outPumpY;
    private static int _numOutPump;
    private const int MaxMech = 1000;
    private static int[] _mechX;
    private static int[] _mechY;
    private static int _numMechs;
    private static int[] _mechTime;
    private static int _currentWireColor;
    private static int CurrentUser = (int) byte.MaxValue;

    public static void SetCurrentUser(int plr = -1)
    {
      if (plr < 0 || plr > (int) byte.MaxValue)
        plr = (int) byte.MaxValue;
      if (Main.netMode == 0)
        plr = Main.myPlayer;
      Wiring.CurrentUser = plr;
    }

    public static void Initialize()
    {
      Wiring._wireSkip = new Dictionary<Point16, bool>();
      Wiring._wireList = new DoubleStack<Point16>();
      Wiring._wireDirectionList = new DoubleStack<byte>();
      Wiring._toProcess = new Dictionary<Point16, byte>();
      Wiring._GatesCurrent = new Queue<Point16>();
      Wiring._GatesNext = new Queue<Point16>();
      Wiring._GatesDone = new Dictionary<Point16, bool>();
      Wiring._LampsToCheck = new Queue<Point16>();
      Wiring._PixelBoxTriggers = new Dictionary<Point16, byte>();
      Wiring._inPumpX = new int[20];
      Wiring._inPumpY = new int[20];
      Wiring._outPumpX = new int[20];
      Wiring._outPumpY = new int[20];
      Wiring._teleport = new Vector2[2]
      {
        Vector2.One * -1f,
        Vector2.One * -1f
      };
      Wiring._mechX = new int[1000];
      Wiring._mechY = new int[1000];
      Wiring._mechTime = new int[1000];
    }

    public static void SkipWire(int x, int y) => Wiring._wireSkip[new Point16(x, y)] = true;

    public static void SkipWire(Point16 point) => Wiring._wireSkip[point] = true;

    public static void ClearAll()
    {
      for (int index = 0; index < 20; ++index)
      {
        Wiring._inPumpX[index] = 0;
        Wiring._inPumpY[index] = 0;
        Wiring._outPumpX[index] = 0;
        Wiring._outPumpY[index] = 0;
      }
      Wiring._numInPump = 0;
      Wiring._numOutPump = 0;
      for (int index = 0; index < 1000; ++index)
      {
        Wiring._mechTime[index] = 0;
        Wiring._mechX[index] = 0;
        Wiring._mechY[index] = 0;
      }
      Wiring._numMechs = 0;
    }

    public static void UpdateMech()
    {
      Wiring.SetCurrentUser();
      for (int index1 = Wiring._numMechs - 1; index1 >= 0; --index1)
      {
        --Wiring._mechTime[index1];
        int x1 = Wiring._mechX[index1];
        int y1 = Wiring._mechY[index1];
        if (!WorldGen.InWorld(x1, y1, 1))
        {
          --Wiring._numMechs;
        }
        else
        {
          Tile tile1 = Main.tile[x1, y1];
          if (tile1 == null)
          {
            --Wiring._numMechs;
          }
          else
          {
            if (tile1.active() && tile1.type == (ushort) 144)
            {
              if (tile1.frameY == (short) 0)
              {
                Wiring._mechTime[index1] = 0;
              }
              else
              {
                int y2 = (int) tile1.frameX / 18;
                switch (y2)
                {
                  case 0:
                    y2 = 60;
                    break;
                  case 1:
                    y2 = 180;
                    break;
                  case 2:
                    y2 = 300;
                    break;
                  case 3:
                    y2 = 30;
                    break;
                  case 4:
                    y2 = 15;
                    break;
                }
                if (Math.IEEERemainder((double) Wiring._mechTime[index1], (double) y2) == 0.0)
                {
                  Wiring._mechTime[index1] = 18000;
                  Wiring.TripWire(Wiring._mechX[index1], Wiring._mechY[index1], 1, 1);
                }
              }
            }
            if (Wiring._mechTime[index1] <= 0)
            {
              if (tile1.active() && tile1.type == (ushort) 144)
              {
                tile1.frameY = (short) 0;
                NetMessage.SendTileSquare(-1, Wiring._mechX[index1], Wiring._mechY[index1]);
              }
              if (tile1.active() && tile1.type == (ushort) 411)
              {
                int num1 = (int) tile1.frameX % 36 / 18;
                int num2 = (int) tile1.frameY % 36 / 18;
                int tileX = Wiring._mechX[index1] - num1;
                int tileY = Wiring._mechY[index1] - num2;
                int num3 = 36;
                if (Main.tile[tileX, tileY].frameX >= (short) 36)
                  num3 = -36;
                for (int x2 = tileX; x2 < tileX + 2; ++x2)
                {
                  for (int y3 = tileY; y3 < tileY + 2; ++y3)
                  {
                    if (WorldGen.InWorld(x2, y3, 1))
                    {
                      Tile tile2 = Main.tile[x2, y3];
                      if (tile2 != null)
                        tile2.frameX += (short) num3;
                    }
                  }
                }
                NetMessage.SendTileSquare(-1, tileX, tileY, 2, 2);
              }
              for (int index2 = index1; index2 < Wiring._numMechs; ++index2)
              {
                Wiring._mechX[index2] = Wiring._mechX[index2 + 1];
                Wiring._mechY[index2] = Wiring._mechY[index2 + 1];
                Wiring._mechTime[index2] = Wiring._mechTime[index2 + 1];
              }
              --Wiring._numMechs;
            }
          }
        }
      }
    }

    public static void HitSwitch(int i, int j)
    {
      if (!WorldGen.InWorld(i, j) || Main.tile[i, j] == null)
        return;
      if (Main.tile[i, j].type == (ushort) 135 || Main.tile[i, j].type == (ushort) 314 || Main.tile[i, j].type == (ushort) 423 || Main.tile[i, j].type == (ushort) 428 || Main.tile[i, j].type == (ushort) 442 || Main.tile[i, j].type == (ushort) 476)
      {
        SoundEngine.PlaySound(28, i * 16, j * 16, 0);
        Wiring.TripWire(i, j, 1, 1);
      }
      else if (Main.tile[i, j].type == (ushort) 440)
      {
        SoundEngine.PlaySound(28, i * 16 + 16, j * 16 + 16, 0);
        Wiring.TripWire(i, j, 3, 3);
      }
      else if (Main.tile[i, j].type == (ushort) 136)
      {
        Main.tile[i, j].frameY = Main.tile[i, j].frameY != (short) 0 ? (short) 0 : (short) 18;
        SoundEngine.PlaySound(28, i * 16, j * 16, 0);
        Wiring.TripWire(i, j, 1, 1);
      }
      else if (Main.tile[i, j].type == (ushort) 443)
        Wiring.GeyserTrap(i, j);
      else if (Main.tile[i, j].type == (ushort) 144)
      {
        if (Main.tile[i, j].frameY == (short) 0)
        {
          Main.tile[i, j].frameY = (short) 18;
          if (Main.netMode != 1)
            Wiring.CheckMech(i, j, 18000);
        }
        else
          Main.tile[i, j].frameY = (short) 0;
        SoundEngine.PlaySound(28, i * 16, j * 16, 0);
      }
      else if (Main.tile[i, j].type == (ushort) 441 || Main.tile[i, j].type == (ushort) 468)
      {
        int num1 = (int) Main.tile[i, j].frameX / 18 * -1;
        int num2 = (int) Main.tile[i, j].frameY / 18 * -1;
        int num3 = num1 % 4;
        if (num3 < -1)
          num3 += 2;
        int left = num3 + i;
        int top = num2 + j;
        SoundEngine.PlaySound(28, i * 16, j * 16, 0);
        Wiring.TripWire(left, top, 2, 2);
      }
      else if (Main.tile[i, j].type == (ushort) 467)
      {
        if ((int) Main.tile[i, j].frameX / 36 != 4)
          return;
        int num4 = (int) Main.tile[i, j].frameX / 18 * -1;
        int num5 = (int) Main.tile[i, j].frameY / 18 * -1;
        int num6 = num4 % 4;
        if (num6 < -1)
          num6 += 2;
        int left = num6 + i;
        int top = num5 + j;
        SoundEngine.PlaySound(28, i * 16, j * 16, 0);
        Wiring.TripWire(left, top, 2, 2);
      }
      else
      {
        if (Main.tile[i, j].type != (ushort) 132 && Main.tile[i, j].type != (ushort) 411)
          return;
        short num7 = 36;
        int num8 = (int) Main.tile[i, j].frameX / 18 * -1;
        int num9 = (int) Main.tile[i, j].frameY / 18 * -1;
        int num10 = num8 % 4;
        if (num10 < -1)
        {
          num10 += 2;
          num7 = (short) -36;
        }
        int index1 = num10 + i;
        int index2 = num9 + j;
        if (Main.netMode != 1 && Main.tile[index1, index2].type == (ushort) 411)
          Wiring.CheckMech(index1, index2, 60);
        for (int index3 = index1; index3 < index1 + 2; ++index3)
        {
          for (int index4 = index2; index4 < index2 + 2; ++index4)
          {
            if (Main.tile[index3, index4].type == (ushort) 132 || Main.tile[index3, index4].type == (ushort) 411)
              Main.tile[index3, index4].frameX += num7;
          }
        }
        WorldGen.TileFrame(index1, index2);
        SoundEngine.PlaySound(28, i * 16, j * 16, 0);
        Wiring.TripWire(index1, index2, 2, 2);
      }
    }

    public static void PokeLogicGate(int lampX, int lampY)
    {
      if (Main.netMode == 1)
        return;
      Wiring._LampsToCheck.Enqueue(new Point16(lampX, lampY));
      Wiring.LogicGatePass();
    }

    public static bool Actuate(int i, int j)
    {
      Tile tile = Main.tile[i, j];
      if (!tile.actuator())
        return false;
      if (tile.inActive())
        Wiring.ReActive(i, j);
      else
        Wiring.DeActive(i, j);
      return true;
    }

    public static void ActuateForced(int i, int j)
    {
      if (Main.tile[i, j].inActive())
        Wiring.ReActive(i, j);
      else
        Wiring.DeActive(i, j);
    }

    public static void MassWireOperation(Point ps, Point pe, Player master)
    {
      int wireCount = 0;
      int actuatorCount = 0;
      for (int index = 0; index < 58; ++index)
      {
        if (master.inventory[index].type == 530)
          wireCount += master.inventory[index].stack;
        if (master.inventory[index].type == 849)
          actuatorCount += master.inventory[index].stack;
      }
      int num1 = wireCount;
      int num2 = actuatorCount;
      Wiring.MassWireOperationInner(master, ps, pe, master.Center, master.direction == 1, ref wireCount, ref actuatorCount);
      int num3 = wireCount;
      int number2_1 = num1 - num3;
      int number2_2 = num2 - actuatorCount;
      if (Main.netMode == 2)
      {
        NetMessage.SendData(110, master.whoAmI, number: 530, number2: ((float) number2_1), number3: ((float) master.whoAmI));
        NetMessage.SendData(110, master.whoAmI, number: 849, number2: ((float) number2_2), number3: ((float) master.whoAmI));
      }
      else
      {
        for (int index = 0; index < number2_1; ++index)
          master.ConsumeItem(530);
        for (int index = 0; index < number2_2; ++index)
          master.ConsumeItem(849);
      }
    }

    private static bool CheckMech(int i, int j, int time)
    {
      for (int index = 0; index < Wiring._numMechs; ++index)
      {
        if (Wiring._mechX[index] == i && Wiring._mechY[index] == j)
          return false;
      }
      if (Wiring._numMechs >= 999)
        return false;
      Wiring._mechX[Wiring._numMechs] = i;
      Wiring._mechY[Wiring._numMechs] = j;
      Wiring._mechTime[Wiring._numMechs] = time;
      ++Wiring._numMechs;
      return true;
    }

    private static void XferWater()
    {
      for (int index1 = 0; index1 < Wiring._numInPump; ++index1)
      {
        int i1 = Wiring._inPumpX[index1];
        int j1 = Wiring._inPumpY[index1];
        int liquid1 = (int) Main.tile[i1, j1].liquid;
        if (liquid1 > 0)
        {
          byte liquidType = Main.tile[i1, j1].liquidType();
          for (int index2 = 0; index2 < Wiring._numOutPump; ++index2)
          {
            int i2 = Wiring._outPumpX[index2];
            int j2 = Wiring._outPumpY[index2];
            int liquid2 = (int) Main.tile[i2, j2].liquid;
            if (liquid2 < (int) byte.MaxValue)
            {
              byte num1 = Main.tile[i2, j2].liquidType();
              if (liquid2 == 0)
                num1 = liquidType;
              if ((int) num1 == (int) liquidType)
              {
                int num2 = liquid1;
                if (num2 + liquid2 > (int) byte.MaxValue)
                  num2 = (int) byte.MaxValue - liquid2;
                Main.tile[i2, j2].liquid += (byte) num2;
                Main.tile[i1, j1].liquid -= (byte) num2;
                liquid1 = (int) Main.tile[i1, j1].liquid;
                Main.tile[i2, j2].liquidType((int) liquidType);
                WorldGen.SquareTileFrame(i2, j2);
                if (Main.tile[i1, j1].liquid == (byte) 0)
                {
                  Main.tile[i1, j1].liquidType(0);
                  WorldGen.SquareTileFrame(i1, j1);
                  break;
                }
              }
            }
          }
          WorldGen.SquareTileFrame(i1, j1);
        }
      }
    }

    private static void TripWire(int left, int top, int width, int height)
    {
      if (Main.netMode == 1)
        return;
      Wiring.running = true;
      if (Wiring._wireList.Count != 0)
        Wiring._wireList.Clear(true);
      if (Wiring._wireDirectionList.Count != 0)
        Wiring._wireDirectionList.Clear(true);
      Vector2[] vector2Array1 = new Vector2[8];
      int num1 = 0;
      Point16 back;
      for (int X = left; X < left + width; ++X)
      {
        for (int Y = top; Y < top + height; ++Y)
        {
          back = new Point16(X, Y);
          Tile tile = Main.tile[X, Y];
          if (tile != null && tile.wire())
            Wiring._wireList.PushBack(back);
        }
      }
      Wiring._teleport[0].X = -1f;
      Wiring._teleport[0].Y = -1f;
      Wiring._teleport[1].X = -1f;
      Wiring._teleport[1].Y = -1f;
      if (Wiring._wireList.Count > 0)
      {
        Wiring._numInPump = 0;
        Wiring._numOutPump = 0;
        Wiring.HitWire(Wiring._wireList, 1);
        if (Wiring._numInPump > 0 && Wiring._numOutPump > 0)
          Wiring.XferWater();
      }
      Vector2[] vector2Array2 = vector2Array1;
      int index1 = num1;
      int num2 = index1 + 1;
      Vector2 vector2_1 = Wiring._teleport[0];
      vector2Array2[index1] = vector2_1;
      Vector2[] vector2Array3 = vector2Array1;
      int index2 = num2;
      int num3 = index2 + 1;
      Vector2 vector2_2 = Wiring._teleport[1];
      vector2Array3[index2] = vector2_2;
      for (int X = left; X < left + width; ++X)
      {
        for (int Y = top; Y < top + height; ++Y)
        {
          back = new Point16(X, Y);
          Tile tile = Main.tile[X, Y];
          if (tile != null && tile.wire2())
            Wiring._wireList.PushBack(back);
        }
      }
      Wiring._teleport[0].X = -1f;
      Wiring._teleport[0].Y = -1f;
      Wiring._teleport[1].X = -1f;
      Wiring._teleport[1].Y = -1f;
      if (Wiring._wireList.Count > 0)
      {
        Wiring._numInPump = 0;
        Wiring._numOutPump = 0;
        Wiring.HitWire(Wiring._wireList, 2);
        if (Wiring._numInPump > 0 && Wiring._numOutPump > 0)
          Wiring.XferWater();
      }
      Vector2[] vector2Array4 = vector2Array1;
      int index3 = num3;
      int num4 = index3 + 1;
      Vector2 vector2_3 = Wiring._teleport[0];
      vector2Array4[index3] = vector2_3;
      Vector2[] vector2Array5 = vector2Array1;
      int index4 = num4;
      int num5 = index4 + 1;
      Vector2 vector2_4 = Wiring._teleport[1];
      vector2Array5[index4] = vector2_4;
      Wiring._teleport[0].X = -1f;
      Wiring._teleport[0].Y = -1f;
      Wiring._teleport[1].X = -1f;
      Wiring._teleport[1].Y = -1f;
      for (int X = left; X < left + width; ++X)
      {
        for (int Y = top; Y < top + height; ++Y)
        {
          back = new Point16(X, Y);
          Tile tile = Main.tile[X, Y];
          if (tile != null && tile.wire3())
            Wiring._wireList.PushBack(back);
        }
      }
      if (Wiring._wireList.Count > 0)
      {
        Wiring._numInPump = 0;
        Wiring._numOutPump = 0;
        Wiring.HitWire(Wiring._wireList, 3);
        if (Wiring._numInPump > 0 && Wiring._numOutPump > 0)
          Wiring.XferWater();
      }
      Vector2[] vector2Array6 = vector2Array1;
      int index5 = num5;
      int num6 = index5 + 1;
      Vector2 vector2_5 = Wiring._teleport[0];
      vector2Array6[index5] = vector2_5;
      Vector2[] vector2Array7 = vector2Array1;
      int index6 = num6;
      int num7 = index6 + 1;
      Vector2 vector2_6 = Wiring._teleport[1];
      vector2Array7[index6] = vector2_6;
      Wiring._teleport[0].X = -1f;
      Wiring._teleport[0].Y = -1f;
      Wiring._teleport[1].X = -1f;
      Wiring._teleport[1].Y = -1f;
      for (int X = left; X < left + width; ++X)
      {
        for (int Y = top; Y < top + height; ++Y)
        {
          back = new Point16(X, Y);
          Tile tile = Main.tile[X, Y];
          if (tile != null && tile.wire4())
            Wiring._wireList.PushBack(back);
        }
      }
      if (Wiring._wireList.Count > 0)
      {
        Wiring._numInPump = 0;
        Wiring._numOutPump = 0;
        Wiring.HitWire(Wiring._wireList, 4);
        if (Wiring._numInPump > 0 && Wiring._numOutPump > 0)
          Wiring.XferWater();
      }
      Vector2[] vector2Array8 = vector2Array1;
      int index7 = num7;
      int num8 = index7 + 1;
      Vector2 vector2_7 = Wiring._teleport[0];
      vector2Array8[index7] = vector2_7;
      Vector2[] vector2Array9 = vector2Array1;
      int index8 = num8;
      int num9 = index8 + 1;
      Vector2 vector2_8 = Wiring._teleport[1];
      vector2Array9[index8] = vector2_8;
      Wiring.running = false;
      for (int index9 = 0; index9 < 8; index9 += 2)
      {
        Wiring._teleport[0] = vector2Array1[index9];
        Wiring._teleport[1] = vector2Array1[index9 + 1];
        if ((double) Wiring._teleport[0].X >= 0.0 && (double) Wiring._teleport[1].X >= 0.0)
          Wiring.Teleport();
      }
      Wiring.PixelBoxPass();
      Wiring.LogicGatePass();
    }

    private static void PixelBoxPass()
    {
      foreach (KeyValuePair<Point16, byte> pixelBoxTrigger in Wiring._PixelBoxTriggers)
      {
        if (pixelBoxTrigger.Value == (byte) 3)
        {
          Tile tile = Main.tile[(int) pixelBoxTrigger.Key.X, (int) pixelBoxTrigger.Key.Y];
          tile.frameX = tile.frameX == (short) 18 ? (short) 0 : (short) 18;
          NetMessage.SendTileSquare(-1, (int) pixelBoxTrigger.Key.X, (int) pixelBoxTrigger.Key.Y);
        }
      }
      Wiring._PixelBoxTriggers.Clear();
    }

    private static void LogicGatePass()
    {
      if (Wiring._GatesCurrent.Count != 0)
        return;
      Wiring._GatesDone.Clear();
      while (Wiring._LampsToCheck.Count > 0)
      {
        while (Wiring._LampsToCheck.Count > 0)
        {
          Point16 point16 = Wiring._LampsToCheck.Dequeue();
          Wiring.CheckLogicGate((int) point16.X, (int) point16.Y);
        }
        while (Wiring._GatesNext.Count > 0)
        {
          Utils.Swap<Queue<Point16>>(ref Wiring._GatesCurrent, ref Wiring._GatesNext);
          while (Wiring._GatesCurrent.Count > 0)
          {
            Point16 key = Wiring._GatesCurrent.Peek();
            bool flag;
            if (Wiring._GatesDone.TryGetValue(key, out flag) && flag)
            {
              Wiring._GatesCurrent.Dequeue();
            }
            else
            {
              Wiring._GatesDone.Add(key, true);
              Wiring.TripWire((int) key.X, (int) key.Y, 1, 1);
              Wiring._GatesCurrent.Dequeue();
            }
          }
        }
      }
      Wiring._GatesDone.Clear();
      if (!Wiring.blockPlayerTeleportationForOneIteration)
        return;
      Wiring.blockPlayerTeleportationForOneIteration = false;
    }

    private static void CheckLogicGate(int lampX, int lampY)
    {
      if (!WorldGen.InWorld(lampX, lampY, 1))
        return;
      for (int index1 = lampY; index1 < Main.maxTilesY; ++index1)
      {
        Tile tile1 = Main.tile[lampX, index1];
        if (!tile1.active())
          break;
        if (tile1.type == (ushort) 420)
        {
          bool flag1;
          Wiring._GatesDone.TryGetValue(new Point16(lampX, index1), out flag1);
          int num1 = (int) tile1.frameY / 18;
          bool flag2 = tile1.frameX == (short) 18;
          bool flag3 = tile1.frameX == (short) 36;
          if (num1 < 0)
            break;
          int num2 = 0;
          int num3 = 0;
          bool flag4 = false;
          for (int index2 = index1 - 1; index2 > 0; --index2)
          {
            Tile tile2 = Main.tile[lampX, index2];
            if (tile2.active() && tile2.type == (ushort) 419)
            {
              if (tile2.frameX == (short) 36)
              {
                flag4 = true;
                break;
              }
              ++num2;
              num3 += (tile2.frameX == (short) 18).ToInt();
            }
            else
              break;
          }
          bool flag5;
          switch (num1)
          {
            case 0:
              flag5 = num2 == num3;
              break;
            case 1:
              flag5 = num3 > 0;
              break;
            case 2:
              flag5 = num2 != num3;
              break;
            case 3:
              flag5 = num3 == 0;
              break;
            case 4:
              flag5 = num3 == 1;
              break;
            case 5:
              flag5 = num3 != 1;
              break;
            default:
              return;
          }
          bool flag6 = !flag4 & flag3;
          bool flag7 = false;
          if (flag4 && Framing.GetTileSafely(lampX, lampY).frameX == (short) 36)
            flag7 = true;
          if (!(flag5 != flag2 | flag6 | flag7))
            break;
          int num4 = (int) tile1.frameX % 18 / 18;
          tile1.frameX = (short) (18 * flag5.ToInt());
          if (flag4)
            tile1.frameX = (short) 36;
          Wiring.SkipWire(lampX, index1);
          WorldGen.SquareTileFrame(lampX, index1);
          NetMessage.SendTileSquare(-1, lampX, index1);
          bool flag8 = !flag4 | flag7;
          if (flag7)
          {
            if (num3 == 0 || num2 == 0)
              ;
            flag8 = (double) Main.rand.NextFloat() < (double) num3 / (double) num2;
          }
          if (flag6)
            flag8 = false;
          if (!flag8)
            break;
          if (!flag1)
          {
            Wiring._GatesNext.Enqueue(new Point16(lampX, index1));
            break;
          }
          Vector2 position = new Vector2((float) lampX, (float) index1) * 16f - new Vector2(10f);
          Utils.PoofOfSmoke(position);
          NetMessage.SendData(106, number: ((int) position.X), number2: position.Y);
          break;
        }
        if (tile1.type != (ushort) 419)
          break;
      }
    }

    private static void HitWire(DoubleStack<Point16> next, int wireType)
    {
      Wiring._wireDirectionList.Clear(true);
      for (int index = 0; index < next.Count; ++index)
      {
        Point16 point16 = next.PopFront();
        Wiring.SkipWire(point16);
        Wiring._toProcess.Add(point16, (byte) 4);
        next.PushBack(point16);
        Wiring._wireDirectionList.PushBack((byte) 0);
      }
      Wiring._currentWireColor = wireType;
      while (next.Count > 0)
      {
        Point16 key = next.PopFront();
        int num1 = (int) Wiring._wireDirectionList.PopFront();
        int x = (int) key.X;
        int y = (int) key.Y;
        if (!Wiring._wireSkip.ContainsKey(key))
          Wiring.HitWireSingle(x, y);
        for (int back = 0; back < 4; ++back)
        {
          int X;
          int Y;
          switch (back)
          {
            case 0:
              X = x;
              Y = y + 1;
              break;
            case 1:
              X = x;
              Y = y - 1;
              break;
            case 2:
              X = x + 1;
              Y = y;
              break;
            case 3:
              X = x - 1;
              Y = y;
              break;
            default:
              X = x;
              Y = y + 1;
              break;
          }
          if (X >= 2 && X < Main.maxTilesX - 2 && Y >= 2 && Y < Main.maxTilesY - 2)
          {
            Tile tile1 = Main.tile[X, Y];
            if (tile1 != null)
            {
              Tile tile2 = Main.tile[x, y];
              if (tile2 != null)
              {
                byte num2 = 3;
                if (tile1.type == (ushort) 424 || tile1.type == (ushort) 445)
                  num2 = (byte) 0;
                if (tile2.type == (ushort) 424)
                {
                  switch ((int) tile2.frameX / 18)
                  {
                    case 0:
                      if (back == num1)
                        break;
                      continue;
                    case 1:
                      if (num1 == 0 && back == 3 || num1 == 3 && back == 0 || num1 == 1 && back == 2 || num1 == 2 && back == 1)
                        break;
                      continue;
                    case 2:
                      if (num1 == 0 && back == 2 || num1 == 2 && back == 0 || num1 == 1 && back == 3 || num1 == 3 && back == 1)
                        break;
                      continue;
                  }
                }
                if (tile2.type == (ushort) 445)
                {
                  if (back == num1)
                  {
                    if (Wiring._PixelBoxTriggers.ContainsKey(key))
                      Wiring._PixelBoxTriggers[key] |= back == 0 | back == 1 ? (byte) 2 : (byte) 1;
                    else
                      Wiring._PixelBoxTriggers[key] = back == 0 | back == 1 ? (byte) 2 : (byte) 1;
                  }
                  else
                    continue;
                }
                bool flag;
                switch (wireType)
                {
                  case 1:
                    flag = tile1.wire();
                    break;
                  case 2:
                    flag = tile1.wire2();
                    break;
                  case 3:
                    flag = tile1.wire3();
                    break;
                  case 4:
                    flag = tile1.wire4();
                    break;
                  default:
                    flag = false;
                    break;
                }
                if (flag)
                {
                  Point16 point16 = new Point16(X, Y);
                  byte num3;
                  if (Wiring._toProcess.TryGetValue(point16, out num3))
                  {
                    --num3;
                    if (num3 == (byte) 0)
                      Wiring._toProcess.Remove(point16);
                    else
                      Wiring._toProcess[point16] = num3;
                  }
                  else
                  {
                    next.PushBack(point16);
                    Wiring._wireDirectionList.PushBack((byte) back);
                    if (num2 > (byte) 0)
                      Wiring._toProcess.Add(point16, num2);
                  }
                }
              }
            }
          }
        }
      }
      Wiring._wireSkip.Clear();
      Wiring._toProcess.Clear();
    }

    public static IEntitySource GetProjectileSource(int sourceTileX, int sourceTileY) => (IEntitySource) new EntitySource_Wiring(sourceTileX, sourceTileY);

    public static IEntitySource GetNPCSource(int sourceTileX, int sourceTileY) => (IEntitySource) new EntitySource_Wiring(sourceTileX, sourceTileY);

    public static IEntitySource GetItemSource(int sourceTileX, int sourceTileY) => (IEntitySource) new EntitySource_Wiring(sourceTileX, sourceTileY);

    private static void HitWireSingle(int i, int j)
    {
      Tile tile1 = Main.tile[i, j];
      bool? forcedStateWhereTrueIsOn = new bool?();
      bool doSkipWires = true;
      int type = (int) tile1.type;
      if (tile1.actuator())
        Wiring.ActuateForced(i, j);
      if (!tile1.active())
        return;
      switch (type)
      {
        case 144:
          Wiring.HitSwitch(i, j);
          WorldGen.SquareTileFrame(i, j);
          NetMessage.SendTileSquare(-1, i, j);
          break;
        case 421:
          if (!tile1.actuator())
          {
            tile1.type = (ushort) 422;
            WorldGen.SquareTileFrame(i, j);
            NetMessage.SendTileSquare(-1, i, j);
            break;
          }
          break;
        default:
          if (type == 422 && !tile1.actuator())
          {
            tile1.type = (ushort) 421;
            WorldGen.SquareTileFrame(i, j);
            NetMessage.SendTileSquare(-1, i, j);
            break;
          }
          break;
      }
      if (type >= (int) byte.MaxValue && type <= 268)
      {
        if (tile1.actuator())
          return;
        if (type >= 262)
          tile1.type -= (ushort) 7;
        else
          tile1.type += (ushort) 7;
        WorldGen.SquareTileFrame(i, j);
        NetMessage.SendTileSquare(-1, i, j);
      }
      else
      {
        switch (type)
        {
          case 130:
            if (Main.tile[i, j - 1] != null && Main.tile[i, j - 1].active() && (TileID.Sets.BasicChest[(int) Main.tile[i, j - 1].type] || TileID.Sets.BasicChestFake[(int) Main.tile[i, j - 1].type] || Main.tile[i, j - 1].type == (ushort) 88))
              break;
            tile1.type = (ushort) 131;
            WorldGen.SquareTileFrame(i, j);
            NetMessage.SendTileSquare(-1, i, j);
            break;
          case 131:
            tile1.type = (ushort) 130;
            WorldGen.SquareTileFrame(i, j);
            NetMessage.SendTileSquare(-1, i, j);
            break;
          case 209:
            int num1 = (int) tile1.frameX % 72 / 18;
            int num2 = (int) tile1.frameY % 54 / 18;
            int num3 = i - num1;
            int num4 = j - num2;
            int angle = (int) tile1.frameY / 54;
            int num5 = (int) tile1.frameX / 72;
            int num6 = -1;
            if (num1 == 1 || num1 == 2)
              num6 = num2;
            int num7 = 0;
            if (num1 == 3)
              num7 = -54;
            if (num1 == 0)
              num7 = 54;
            if (angle >= 8 && num7 > 0)
              num7 = 0;
            if (angle == 0 && num7 < 0)
              num7 = 0;
            bool flag1 = false;
            if (num7 != 0)
            {
              for (int x = num3; x < num3 + 4; ++x)
              {
                for (int y = num4; y < num4 + 3; ++y)
                {
                  Wiring.SkipWire(x, y);
                  Main.tile[x, y].frameY += (short) num7;
                }
              }
              flag1 = true;
            }
            if ((num5 == 3 || num5 == 4) && (num6 == 0 || num6 == 1))
            {
              int num8 = num5 == 3 ? 72 : -72;
              for (int x = num3; x < num3 + 4; ++x)
              {
                for (int y = num4; y < num4 + 3; ++y)
                {
                  Wiring.SkipWire(x, y);
                  Main.tile[x, y].frameX += (short) num8;
                }
              }
              flag1 = true;
            }
            if (flag1)
              NetMessage.SendTileSquare(-1, num3, num4, 4, 3);
            if (num6 == -1)
              break;
            bool flag2 = true;
            if ((num5 == 3 || num5 == 4) && num6 < 2)
              flag2 = false;
            if (!(Wiring.CheckMech(num3, num4, 30) & flag2))
              break;
            WorldGen.ShootFromCannon(num3, num4, angle, num5 + 1, 0, 0.0f, Wiring.CurrentUser, true);
            break;
          case 212:
            int num9 = (int) tile1.frameX % 54 / 18;
            int num10 = (int) tile1.frameY % 54 / 18;
            int num11 = i - num9;
            int num12 = j - num10;
            int num13 = (int) tile1.frameX / 54;
            int num14 = -1;
            if (num9 == 1)
              num14 = num10;
            int num15 = 0;
            if (num9 == 0)
              num15 = -54;
            if (num9 == 2)
              num15 = 54;
            if (num13 >= 1 && num15 > 0)
              num15 = 0;
            if (num13 == 0 && num15 < 0)
              num15 = 0;
            bool flag3 = false;
            if (num15 != 0)
            {
              for (int x = num11; x < num11 + 3; ++x)
              {
                for (int y = num12; y < num12 + 3; ++y)
                {
                  Wiring.SkipWire(x, y);
                  Main.tile[x, y].frameX += (short) num15;
                }
              }
              flag3 = true;
            }
            if (flag3)
              NetMessage.SendTileSquare(-1, num11, num12, 3, 3);
            if (num14 == -1 || !Wiring.CheckMech(num11, num12, 10))
              break;
            double num16 = 12.0 + (double) Main.rand.Next(450) * 0.0099999997764825821;
            float num17 = (float) Main.rand.Next(85, 105);
            double num18 = (double) Main.rand.Next(-35, 11);
            int Type1 = 166;
            int Damage1 = 0;
            float KnockBack1 = 0.0f;
            Vector2 vector2_1 = new Vector2((float) ((num11 + 2) * 16 - 8), (float) ((num12 + 2) * 16 - 8));
            if ((int) tile1.frameX / 54 == 0)
            {
              num17 *= -1f;
              vector2_1.X -= 12f;
            }
            else
              vector2_1.X += 12f;
            float num19 = num17;
            float num20 = (float) num18;
            double num21 = Math.Sqrt((double) num19 * (double) num19 + (double) num20 * (double) num20);
            float num22 = (float) (num16 / num21);
            float SpeedX1 = num19 * num22;
            float SpeedY1 = num20 * num22;
            Projectile.NewProjectile(Wiring.GetProjectileSource(num11, num12), vector2_1.X, vector2_1.Y, SpeedX1, SpeedY1, Type1, Damage1, KnockBack1, Wiring.CurrentUser);
            break;
          case 215:
            Wiring.ToggleCampFire(i, j, tile1, forcedStateWhereTrueIsOn, doSkipWires);
            break;
          case 356:
            int num23 = (int) tile1.frameX % 36 / 18;
            int num24 = (int) tile1.frameY % 54 / 18;
            int tileX1 = i - num23;
            int tileY1 = j - num24;
            for (int x = tileX1; x < tileX1 + 2; ++x)
            {
              for (int y = tileY1; y < tileY1 + 3; ++y)
                Wiring.SkipWire(x, y);
            }
            if (!Main.fastForwardTimeToDawn && Main.sundialCooldown == 0)
              Main.Sundialing();
            NetMessage.SendTileSquare(-1, tileX1, tileY1, 2, 2);
            break;
          case 405:
            Wiring.ToggleFirePlace(i, j, tile1, forcedStateWhereTrueIsOn, doSkipWires);
            break;
          case 406:
            int num25 = (int) tile1.frameX % 54 / 18;
            int num26 = (int) tile1.frameY % 54 / 18;
            int index1 = i - num25;
            int index2 = j - num26;
            int num27 = 54;
            if (Main.tile[index1, index2].frameY >= (short) 108)
              num27 = -108;
            for (int x = index1; x < index1 + 3; ++x)
            {
              for (int y = index2; y < index2 + 3; ++y)
              {
                Wiring.SkipWire(x, y);
                Main.tile[x, y].frameY += (short) num27;
              }
            }
            NetMessage.SendTileSquare(-1, index1 + 1, index2 + 1, 3);
            break;
          case 411:
            int num28 = (int) tile1.frameX % 36 / 18;
            int num29 = (int) tile1.frameY % 36 / 18;
            int tileX2 = i - num28;
            int tileY2 = j - num29;
            int num30 = 36;
            if (Main.tile[tileX2, tileY2].frameX >= (short) 36)
              num30 = -36;
            for (int x = tileX2; x < tileX2 + 2; ++x)
            {
              for (int y = tileY2; y < tileY2 + 2; ++y)
              {
                Wiring.SkipWire(x, y);
                Main.tile[x, y].frameX += (short) num30;
              }
            }
            NetMessage.SendTileSquare(-1, tileX2, tileY2, 2, 2);
            break;
          case 419:
            int num31 = 18;
            if ((int) tile1.frameX >= num31)
              num31 = -num31;
            if (tile1.frameX == (short) 36)
              num31 = 0;
            Wiring.SkipWire(i, j);
            tile1.frameX += (short) num31;
            WorldGen.SquareTileFrame(i, j);
            NetMessage.SendTileSquare(-1, i, j);
            Wiring._LampsToCheck.Enqueue(new Point16(i, j));
            break;
          case 425:
            int num32 = (int) tile1.frameX % 36 / 18;
            int num33 = (int) tile1.frameY % 36 / 18;
            int i1 = i - num32;
            int j1 = j - num33;
            for (int x = i1; x < i1 + 2; ++x)
            {
              for (int y = j1; y < j1 + 2; ++y)
                Wiring.SkipWire(x, y);
            }
            if (Main.AnnouncementBoxDisabled)
              break;
            Color pink = Color.Pink;
            int index3 = Sign.ReadSign(i1, j1, false);
            if (index3 == -1 || Main.sign[index3] == null || string.IsNullOrWhiteSpace(Main.sign[index3].text))
              break;
            if (Main.AnnouncementBoxRange == -1)
            {
              if (Main.netMode == 0)
              {
                Main.NewTextMultiline(Main.sign[index3].text, c: pink, WidthLimit: 460);
                break;
              }
              if (Main.netMode != 2)
                break;
              NetMessage.SendData(107, text: NetworkText.FromLiteral(Main.sign[index3].text), number: ((int) byte.MaxValue), number2: ((float) pink.R), number3: ((float) pink.G), number4: ((float) pink.B), number5: 460);
              break;
            }
            switch (Main.netMode)
            {
              case 0:
                if ((double) Main.player[Main.myPlayer].Distance(new Vector2((float) (i1 * 16 + 16), (float) (j1 * 16 + 16))) > (double) Main.AnnouncementBoxRange)
                  return;
                Main.NewTextMultiline(Main.sign[index3].text, c: pink, WidthLimit: 460);
                return;
              case 2:
                for (int remoteClient = 0; remoteClient < (int) byte.MaxValue; ++remoteClient)
                {
                  if (Main.player[remoteClient].active && (double) Main.player[remoteClient].Distance(new Vector2((float) (i1 * 16 + 16), (float) (j1 * 16 + 16))) <= (double) Main.AnnouncementBoxRange)
                    NetMessage.SendData(107, remoteClient, text: NetworkText.FromLiteral(Main.sign[index3].text), number: ((int) byte.MaxValue), number2: ((float) pink.R), number3: ((float) pink.G), number4: ((float) pink.B), number5: 460);
                }
                return;
              default:
                return;
            }
          case 452:
            int num34 = (int) tile1.frameX % 54 / 18;
            int num35 = (int) tile1.frameY % 54 / 18;
            int index4 = i - num34;
            int index5 = j - num35;
            int num36 = 54;
            if (Main.tile[index4, index5].frameX >= (short) 54)
              num36 = -54;
            for (int x = index4; x < index4 + 3; ++x)
            {
              for (int y = index5; y < index5 + 3; ++y)
              {
                Wiring.SkipWire(x, y);
                Main.tile[x, y].frameX += (short) num36;
              }
            }
            NetMessage.SendTileSquare(-1, index4 + 1, index5 + 1, 3);
            break;
          case 663:
            int num37 = (int) tile1.frameX % 36 / 18;
            int num38 = (int) tile1.frameY % 54 / 18;
            int tileX3 = i - num37;
            int tileY3 = j - num38;
            for (int x = tileX3; x < tileX3 + 2; ++x)
            {
              for (int y = tileY3; y < tileY3 + 3; ++y)
                Wiring.SkipWire(x, y);
            }
            if (!Main.fastForwardTimeToDusk && Main.moondialCooldown == 0)
              Main.Moondialing();
            NetMessage.SendTileSquare(-1, tileX3, tileY3, 2, 2);
            break;
          default:
            if (type == 387 || type == 386)
            {
              bool flag4 = type == 387;
              int number4 = WorldGen.ShiftTrapdoor(i, j, true).ToInt();
              if (number4 == 0)
                number4 = -WorldGen.ShiftTrapdoor(i, j, false).ToInt();
              if (number4 == 0)
                break;
              NetMessage.SendData(19, number: (3 - flag4.ToInt()), number2: ((float) i), number3: ((float) j), number4: ((float) number4));
              break;
            }
            if (type == 389 || type == 388)
            {
              bool closing = type == 389;
              WorldGen.ShiftTallGate(i, j, closing);
              NetMessage.SendData(19, number: (4 + closing.ToInt()), number2: ((float) i), number3: ((float) j));
              break;
            }
            switch (type)
            {
              case 10:
                int num39 = 1;
                if (Main.rand.Next(2) == 0)
                  num39 = -1;
                if (!WorldGen.OpenDoor(i, j, num39))
                {
                  if (!WorldGen.OpenDoor(i, j, -num39))
                    return;
                  NetMessage.SendData(19, number2: ((float) i), number3: ((float) j), number4: ((float) -num39));
                  return;
                }
                NetMessage.SendData(19, number2: ((float) i), number3: ((float) j), number4: ((float) num39));
                return;
              case 11:
                if (!WorldGen.CloseDoor(i, j, true))
                  return;
                NetMessage.SendData(19, number: 1, number2: ((float) i), number3: ((float) j));
                return;
              case 216:
                WorldGen.LaunchRocket(i, j, true);
                Wiring.SkipWire(i, j);
                return;
              default:
                if (type == 497 || type == 15 && (int) tile1.frameY / 40 == 1 || type == 15 && (int) tile1.frameY / 40 == 20)
                {
                  int num40 = j - (int) tile1.frameY % 40 / 18;
                  int num41 = i;
                  Wiring.SkipWire(num41, num40);
                  Wiring.SkipWire(num41, num40 + 1);
                  if (!Wiring.CheckMech(num41, num40, 60))
                    return;
                  Projectile.NewProjectile(Wiring.GetProjectileSource(num41, num40), (float) (num41 * 16 + 8), (float) (num40 * 16 + 12), 0.0f, 0.0f, 733, 0, 0.0f, Main.myPlayer);
                  return;
                }
                switch (type)
                {
                  case 4:
                    Wiring.ToggleTorch(i, j, tile1, forcedStateWhereTrueIsOn);
                    return;
                  case 42:
                    Wiring.ToggleHangingLantern(i, j, tile1, forcedStateWhereTrueIsOn, doSkipWires);
                    return;
                  case 93:
                    Wiring.ToggleLamp(i, j, tile1, forcedStateWhereTrueIsOn, doSkipWires);
                    return;
                  case 149:
                    Wiring.ToggleHolidayLight(i, j, tile1, forcedStateWhereTrueIsOn);
                    return;
                  case 235:
                    int num42 = i - (int) tile1.frameX / 18;
                    if (tile1.wall == (ushort) 87 && (double) j > Main.worldSurface && !NPC.downedPlantBoss)
                      return;
                    if ((double) Wiring._teleport[0].X == -1.0)
                    {
                      Wiring._teleport[0].X = (float) num42;
                      Wiring._teleport[0].Y = (float) j;
                      if (!tile1.halfBrick())
                        return;
                      Wiring._teleport[0].Y += 0.5f;
                      return;
                    }
                    if ((double) Wiring._teleport[0].X == (double) num42 && (double) Wiring._teleport[0].Y == (double) j)
                      return;
                    Wiring._teleport[1].X = (float) num42;
                    Wiring._teleport[1].Y = (float) j;
                    if (!tile1.halfBrick())
                      return;
                    Wiring._teleport[1].Y += 0.5f;
                    return;
                  case 244:
                    int num43 = (int) tile1.frameX / 18;
                    while (num43 >= 3)
                      num43 -= 3;
                    int num44 = (int) tile1.frameY / 18;
                    while (num44 >= 3)
                      num44 -= 3;
                    int tileX4 = i - num43;
                    int tileY4 = j - num44;
                    int num45 = 54;
                    if (Main.tile[tileX4, tileY4].frameX >= (short) 54)
                      num45 = -54;
                    for (int x = tileX4; x < tileX4 + 3; ++x)
                    {
                      for (int y = tileY4; y < tileY4 + 2; ++y)
                      {
                        Wiring.SkipWire(x, y);
                        Main.tile[x, y].frameX += (short) num45;
                      }
                    }
                    NetMessage.SendTileSquare(-1, tileX4, tileY4, 3, 2);
                    return;
                  case 335:
                    int num46 = j - (int) tile1.frameY / 18;
                    int num47 = i - (int) tile1.frameX / 18;
                    Wiring.SkipWire(num47, num46);
                    Wiring.SkipWire(num47, num46 + 1);
                    Wiring.SkipWire(num47 + 1, num46);
                    Wiring.SkipWire(num47 + 1, num46 + 1);
                    if (!Wiring.CheckMech(num47, num46, 30))
                      return;
                    WorldGen.LaunchRocketSmall(num47, num46, true);
                    return;
                  case 338:
                    int num48 = j - (int) tile1.frameY / 18;
                    int num49 = i - (int) tile1.frameX / 18;
                    Wiring.SkipWire(num49, num48);
                    Wiring.SkipWire(num49, num48 + 1);
                    if (!Wiring.CheckMech(num49, num48, 30))
                      return;
                    bool flag5 = false;
                    for (int index6 = 0; index6 < 1000; ++index6)
                    {
                      if (Main.projectile[index6].active && Main.projectile[index6].aiStyle == 73 && (double) Main.projectile[index6].ai[0] == (double) num49 && (double) Main.projectile[index6].ai[1] == (double) num48)
                      {
                        flag5 = true;
                        break;
                      }
                    }
                    if (flag5)
                      return;
                    int Type2 = 419 + Main.rand.Next(4);
                    Projectile.NewProjectile(Wiring.GetProjectileSource(num49, num48), (float) (num49 * 16 + 8), (float) (num48 * 16 + 2), 0.0f, 0.0f, Type2, 0, 0.0f, Main.myPlayer, (float) num49, (float) num48);
                    return;
                  case 429:
                    int num50 = (int) Main.tile[i, j].frameX / 18;
                    bool flag6 = num50 % 2 >= 1;
                    bool flag7 = num50 % 4 >= 2;
                    bool flag8 = num50 % 8 >= 4;
                    bool flag9 = num50 % 16 >= 8;
                    bool flag10 = false;
                    short num51 = 0;
                    switch (Wiring._currentWireColor)
                    {
                      case 1:
                        num51 = (short) 18;
                        flag10 = !flag6;
                        break;
                      case 2:
                        num51 = (short) 72;
                        flag10 = !flag8;
                        break;
                      case 3:
                        num51 = (short) 36;
                        flag10 = !flag7;
                        break;
                      case 4:
                        num51 = (short) 144;
                        flag10 = !flag9;
                        break;
                    }
                    if (flag10)
                      tile1.frameX += num51;
                    else
                      tile1.frameX -= num51;
                    NetMessage.SendTileSquare(-1, i, j);
                    return;
                  case 565:
                    int num52 = (int) tile1.frameX / 18;
                    while (num52 >= 2)
                      num52 -= 2;
                    int num53 = (int) tile1.frameY / 18;
                    while (num53 >= 2)
                      num53 -= 2;
                    int tileX5 = i - num52;
                    int tileY5 = j - num53;
                    int num54 = 36;
                    if (Main.tile[tileX5, tileY5].frameX >= (short) 36)
                      num54 = -36;
                    for (int x = tileX5; x < tileX5 + 2; ++x)
                    {
                      for (int y = tileY5; y < tileY5 + 2; ++y)
                      {
                        Wiring.SkipWire(x, y);
                        Main.tile[x, y].frameX += (short) num54;
                      }
                    }
                    NetMessage.SendTileSquare(-1, tileX5, tileY5, 2, 2);
                    return;
                  default:
                    if (type == 126 || type == 95 || type == 100 || type == 173 || type == 564)
                    {
                      Wiring.Toggle2x2Light(i, j, tile1, forcedStateWhereTrueIsOn, doSkipWires);
                      return;
                    }
                    switch (type)
                    {
                      case 34:
                        Wiring.ToggleChandelier(i, j, tile1, forcedStateWhereTrueIsOn, doSkipWires);
                        return;
                      case 314:
                        if (!Wiring.CheckMech(i, j, 5))
                          return;
                        Minecart.FlipSwitchTrack(i, j);
                        return;
                      case 593:
                        int index7 = i;
                        int index8 = j;
                        Wiring.SkipWire(index7, index8);
                        short num55 = Main.tile[index7, index8].frameX != (short) 0 ? (short) -18 : (short) 18;
                        Main.tile[index7, index8].frameX += num55;
                        if (Main.netMode == 2)
                          NetMessage.SendTileSquare(-1, index7, index8, 1, 1);
                        int num56 = num55 > (short) 0 ? 4 : 3;
                        Animation.NewTemporaryAnimation(num56, (ushort) 593, index7, index8);
                        NetMessage.SendTemporaryAnimation(-1, num56, 593, index7, index8);
                        return;
                      case 594:
                        int num57 = (int) tile1.frameY / 18;
                        while (num57 >= 2)
                          num57 -= 2;
                        int index9 = j - num57;
                        int num58 = (int) tile1.frameX / 18;
                        if (num58 > 1)
                          num58 -= 2;
                        int index10 = i - num58;
                        Wiring.SkipWire(index10, index9);
                        Wiring.SkipWire(index10, index9 + 1);
                        Wiring.SkipWire(index10 + 1, index9);
                        Wiring.SkipWire(index10 + 1, index9 + 1);
                        short num59 = Main.tile[index10, index9].frameX != (short) 0 ? (short) -36 : (short) 36;
                        for (int index11 = 0; index11 < 2; ++index11)
                        {
                          for (int index12 = 0; index12 < 2; ++index12)
                            Main.tile[index10 + index11, index9 + index12].frameX += num59;
                        }
                        if (Main.netMode == 2)
                          NetMessage.SendTileSquare(-1, index10, index9, 2, 2);
                        int num60 = num59 > (short) 0 ? 4 : 3;
                        Animation.NewTemporaryAnimation(num60, (ushort) 594, index10, index9);
                        NetMessage.SendTemporaryAnimation(-1, num60, 594, index10, index9);
                        return;
                      default:
                        if (type == 33 || type == 174 || type == 49 || type == 372 || type == 646)
                        {
                          Wiring.ToggleCandle(i, j, tile1, forcedStateWhereTrueIsOn);
                          return;
                        }
                        switch (type)
                        {
                          case 92:
                            Wiring.ToggleLampPost(i, j, tile1, forcedStateWhereTrueIsOn, doSkipWires);
                            return;
                          case 137:
                            int num61 = (int) tile1.frameY / 18;
                            Vector2 vector2_2 = Vector2.Zero;
                            float SpeedX2 = 0.0f;
                            float SpeedY2 = 0.0f;
                            int Type3 = 0;
                            int Damage2 = 0;
                            switch (num61)
                            {
                              case 0:
                              case 1:
                              case 2:
                              case 5:
                                if (Wiring.CheckMech(i, j, 200))
                                {
                                  int num62 = tile1.frameX == (short) 0 ? -1 : (tile1.frameX == (short) 18 ? 1 : 0);
                                  int num63 = tile1.frameX < (short) 36 ? 0 : (tile1.frameX < (short) 72 ? -1 : 1);
                                  vector2_2 = new Vector2((float) (i * 16 + 8 + 10 * num62), (float) (j * 16 + 8 + 10 * num63));
                                  float num64 = 3f;
                                  if (num61 == 0)
                                  {
                                    Type3 = 98;
                                    Damage2 = 20;
                                    num64 = 12f;
                                  }
                                  if (num61 == 1)
                                  {
                                    Type3 = 184;
                                    Damage2 = 40;
                                    num64 = 12f;
                                  }
                                  if (num61 == 2)
                                  {
                                    Type3 = 187;
                                    Damage2 = 40;
                                    num64 = 5f;
                                  }
                                  if (num61 == 5)
                                  {
                                    Type3 = 980;
                                    Damage2 = 30;
                                    num64 = 12f;
                                  }
                                  SpeedX2 = (float) num62 * num64;
                                  SpeedY2 = (float) num63 * num64;
                                  break;
                                }
                                break;
                              case 3:
                                if (Wiring.CheckMech(i, j, 300))
                                {
                                  int num65 = 200;
                                  for (int index13 = 0; index13 < 1000; ++index13)
                                  {
                                    if (Main.projectile[index13].active && Main.projectile[index13].type == Type3)
                                    {
                                      float num66 = (new Vector2((float) (i * 16 + 8), (float) (j * 18 + 8)) - Main.projectile[index13].Center).Length();
                                      if ((double) num66 < 50.0)
                                        num65 -= 50;
                                      else if ((double) num66 < 100.0)
                                        num65 -= 15;
                                      else if ((double) num66 < 200.0)
                                        num65 -= 10;
                                      else if ((double) num66 < 300.0)
                                        num65 -= 8;
                                      else if ((double) num66 < 400.0)
                                        num65 -= 6;
                                      else if ((double) num66 < 500.0)
                                        num65 -= 5;
                                      else if ((double) num66 < 700.0)
                                        num65 -= 4;
                                      else if ((double) num66 < 900.0)
                                        num65 -= 3;
                                      else if ((double) num66 < 1200.0)
                                        num65 -= 2;
                                      else
                                        --num65;
                                    }
                                  }
                                  if (num65 > 0)
                                  {
                                    Type3 = 185;
                                    Damage2 = 40;
                                    int num67 = 0;
                                    int num68 = 0;
                                    switch ((int) tile1.frameX / 18)
                                    {
                                      case 0:
                                      case 1:
                                        num67 = 0;
                                        num68 = 1;
                                        break;
                                      case 2:
                                        num67 = 0;
                                        num68 = -1;
                                        break;
                                      case 3:
                                        num67 = -1;
                                        num68 = 0;
                                        break;
                                      case 4:
                                        num67 = 1;
                                        num68 = 0;
                                        break;
                                    }
                                    SpeedX2 = (float) (4 * num67) + (float) Main.rand.Next((num67 == 1 ? 20 : 0) - 20, 21 - (num67 == -1 ? 20 : 0)) * 0.05f;
                                    SpeedY2 = (float) (4 * num68) + (float) Main.rand.Next((num68 == 1 ? 20 : 0) - 20, 21 - (num68 == -1 ? 20 : 0)) * 0.05f;
                                    vector2_2 = new Vector2((float) (i * 16 + 8 + 14 * num67), (float) (j * 16 + 8 + 14 * num68));
                                    break;
                                  }
                                  break;
                                }
                                break;
                              case 4:
                                if (Wiring.CheckMech(i, j, 90))
                                {
                                  int num69 = 0;
                                  int num70 = 0;
                                  switch ((int) tile1.frameX / 18)
                                  {
                                    case 0:
                                    case 1:
                                      num69 = 0;
                                      num70 = 1;
                                      break;
                                    case 2:
                                      num69 = 0;
                                      num70 = -1;
                                      break;
                                    case 3:
                                      num69 = -1;
                                      num70 = 0;
                                      break;
                                    case 4:
                                      num69 = 1;
                                      num70 = 0;
                                      break;
                                  }
                                  SpeedX2 = (float) (8 * num69);
                                  SpeedY2 = (float) (8 * num70);
                                  Damage2 = 60;
                                  Type3 = 186;
                                  vector2_2 = new Vector2((float) (i * 16 + 8 + 18 * num69), (float) (j * 16 + 8 + 18 * num70));
                                  break;
                                }
                                break;
                            }
                            switch (num61 + 10)
                            {
                              case 0:
                                if (Wiring.CheckMech(i, j, 200))
                                {
                                  int num71 = -1;
                                  if (tile1.frameX != (short) 0)
                                    num71 = 1;
                                  SpeedX2 = (float) (12 * num71);
                                  Damage2 = 20;
                                  Type3 = 98;
                                  vector2_2 = new Vector2((float) (i * 16 + 8), (float) (j * 16 + 7));
                                  vector2_2.X += (float) (10 * num71);
                                  vector2_2.Y += 2f;
                                  break;
                                }
                                break;
                              case 1:
                                if (Wiring.CheckMech(i, j, 200))
                                {
                                  int num72 = -1;
                                  if (tile1.frameX != (short) 0)
                                    num72 = 1;
                                  SpeedX2 = (float) (12 * num72);
                                  Damage2 = 40;
                                  Type3 = 184;
                                  vector2_2 = new Vector2((float) (i * 16 + 8), (float) (j * 16 + 7));
                                  vector2_2.X += (float) (10 * num72);
                                  vector2_2.Y += 2f;
                                  break;
                                }
                                break;
                              case 2:
                                if (Wiring.CheckMech(i, j, 200))
                                {
                                  int num73 = -1;
                                  if (tile1.frameX != (short) 0)
                                    num73 = 1;
                                  SpeedX2 = (float) (5 * num73);
                                  Damage2 = 40;
                                  Type3 = 187;
                                  vector2_2 = new Vector2((float) (i * 16 + 8), (float) (j * 16 + 7));
                                  vector2_2.X += (float) (10 * num73);
                                  vector2_2.Y += 2f;
                                  break;
                                }
                                break;
                              case 3:
                                if (Wiring.CheckMech(i, j, 300))
                                {
                                  Type3 = 185;
                                  int num74 = 200;
                                  for (int index14 = 0; index14 < 1000; ++index14)
                                  {
                                    if (Main.projectile[index14].active && Main.projectile[index14].type == Type3)
                                    {
                                      float num75 = (new Vector2((float) (i * 16 + 8), (float) (j * 18 + 8)) - Main.projectile[index14].Center).Length();
                                      if ((double) num75 < 50.0)
                                        num74 -= 50;
                                      else if ((double) num75 < 100.0)
                                        num74 -= 15;
                                      else if ((double) num75 < 200.0)
                                        num74 -= 10;
                                      else if ((double) num75 < 300.0)
                                        num74 -= 8;
                                      else if ((double) num75 < 400.0)
                                        num74 -= 6;
                                      else if ((double) num75 < 500.0)
                                        num74 -= 5;
                                      else if ((double) num75 < 700.0)
                                        num74 -= 4;
                                      else if ((double) num75 < 900.0)
                                        num74 -= 3;
                                      else if ((double) num75 < 1200.0)
                                        num74 -= 2;
                                      else
                                        --num74;
                                    }
                                  }
                                  if (num74 > 0)
                                  {
                                    SpeedX2 = (float) Main.rand.Next(-20, 21) * 0.05f;
                                    SpeedY2 = (float) (4.0 + (double) Main.rand.Next(0, 21) * 0.05000000074505806);
                                    Damage2 = 40;
                                    vector2_2 = new Vector2((float) (i * 16 + 8), (float) (j * 16 + 16));
                                    vector2_2.Y += 6f;
                                    Projectile.NewProjectile(Wiring.GetProjectileSource(i, j), (float) (int) vector2_2.X, (float) (int) vector2_2.Y, SpeedX2, SpeedY2, Type3, Damage2, 2f, Main.myPlayer);
                                    break;
                                  }
                                  break;
                                }
                                break;
                              case 4:
                                if (Wiring.CheckMech(i, j, 90))
                                {
                                  SpeedX2 = 0.0f;
                                  SpeedY2 = 8f;
                                  Damage2 = 60;
                                  Type3 = 186;
                                  vector2_2 = new Vector2((float) (i * 16 + 8), (float) (j * 16 + 16));
                                  vector2_2.Y += 10f;
                                  break;
                                }
                                break;
                            }
                            if (Type3 == 0)
                              return;
                            Projectile.NewProjectile(Wiring.GetProjectileSource(i, j), (float) (int) vector2_2.X, (float) (int) vector2_2.Y, SpeedX2, SpeedY2, Type3, Damage2, 2f, Main.myPlayer);
                            return;
                          case 443:
                            Wiring.GeyserTrap(i, j);
                            return;
                          case 531:
                            int num76 = (int) tile1.frameX / 36;
                            int num77 = (int) tile1.frameY / 54;
                            int num78 = i - ((int) tile1.frameX - num76 * 36) / 18;
                            int num79 = j - ((int) tile1.frameY - num77 * 54) / 18;
                            if (!Wiring.CheckMech(num78, num79, 900))
                              return;
                            Vector2 vector2_3 = new Vector2((float) (num78 + 1), (float) num79) * 16f;
                            vector2_3.Y += 28f;
                            int Type4 = 99;
                            int Damage3 = 70;
                            float KnockBack2 = 10f;
                            if (Type4 == 0)
                              return;
                            Projectile.NewProjectile(Wiring.GetProjectileSource(num78, num79), (float) (int) vector2_3.X, (float) (int) vector2_3.Y, 0.0f, 0.0f, Type4, Damage3, KnockBack2, Main.myPlayer);
                            return;
                          default:
                            if (type == 139 || type == 35)
                            {
                              WorldGen.SwitchMB(i, j);
                              return;
                            }
                            if (type == 207)
                            {
                              WorldGen.SwitchFountain(i, j);
                              return;
                            }
                            if (type == 410 || type == 480 || type == 509 || type == 657 || type == 658)
                            {
                              WorldGen.SwitchMonolith(i, j);
                              return;
                            }
                            switch (type)
                            {
                              case 141:
                                WorldGen.KillTile(i, j, noItem: true);
                                NetMessage.SendTileSquare(-1, i, j);
                                Projectile.NewProjectile(Wiring.GetProjectileSource(i, j), (float) (i * 16 + 8), (float) (j * 16 + 8), 0.0f, 0.0f, 108, 500, 10f, Main.myPlayer);
                                return;
                              case 210:
                                WorldGen.ExplodeMine(i, j, true);
                                return;
                              case 455:
                                BirthdayParty.ToggleManualParty();
                                return;
                              default:
                                if (type == 142 || type == 143)
                                {
                                  int y = j - (int) tile1.frameY / 18;
                                  int num80 = (int) tile1.frameX / 18;
                                  if (num80 > 1)
                                    num80 -= 2;
                                  int x = i - num80;
                                  Wiring.SkipWire(x, y);
                                  Wiring.SkipWire(x, y + 1);
                                  Wiring.SkipWire(x + 1, y);
                                  Wiring.SkipWire(x + 1, y + 1);
                                  if (type == 142)
                                  {
                                    for (int index15 = 0; index15 < 4 && Wiring._numInPump < 19; ++index15)
                                    {
                                      int num81;
                                      int num82;
                                      switch (index15)
                                      {
                                        case 0:
                                          num81 = x;
                                          num82 = y + 1;
                                          break;
                                        case 1:
                                          num81 = x + 1;
                                          num82 = y + 1;
                                          break;
                                        case 2:
                                          num81 = x;
                                          num82 = y;
                                          break;
                                        default:
                                          num81 = x + 1;
                                          num82 = y;
                                          break;
                                      }
                                      Wiring._inPumpX[Wiring._numInPump] = num81;
                                      Wiring._inPumpY[Wiring._numInPump] = num82;
                                      ++Wiring._numInPump;
                                    }
                                    return;
                                  }
                                  for (int index16 = 0; index16 < 4 && Wiring._numOutPump < 19; ++index16)
                                  {
                                    int num83;
                                    int num84;
                                    switch (index16)
                                    {
                                      case 0:
                                        num83 = x;
                                        num84 = y + 1;
                                        break;
                                      case 1:
                                        num83 = x + 1;
                                        num84 = y + 1;
                                        break;
                                      case 2:
                                        num83 = x;
                                        num84 = y;
                                        break;
                                      default:
                                        num83 = x + 1;
                                        num84 = y;
                                        break;
                                    }
                                    Wiring._outPumpX[Wiring._numOutPump] = num83;
                                    Wiring._outPumpY[Wiring._numOutPump] = num84;
                                    ++Wiring._numOutPump;
                                  }
                                  return;
                                }
                                switch (type)
                                {
                                  case 105:
                                    int num85 = j - (int) tile1.frameY / 18;
                                    int num86 = (int) tile1.frameX / 18;
                                    int num87 = 0;
                                    while (num86 >= 2)
                                    {
                                      num86 -= 2;
                                      ++num87;
                                    }
                                    int num88 = i - num86;
                                    int num89 = i - (int) tile1.frameX % 36 / 18;
                                    int num90 = j - (int) tile1.frameY % 54 / 18;
                                    int num91 = (int) tile1.frameY / 54 % 3;
                                    int num92 = (int) tile1.frameX / 36 + num91 * 55;
                                    Wiring.SkipWire(num89, num90);
                                    Wiring.SkipWire(num89, num90 + 1);
                                    Wiring.SkipWire(num89, num90 + 2);
                                    Wiring.SkipWire(num89 + 1, num90);
                                    Wiring.SkipWire(num89 + 1, num90 + 1);
                                    Wiring.SkipWire(num89 + 1, num90 + 2);
                                    int num93 = num89 * 16 + 16;
                                    int num94 = (num90 + 3) * 16;
                                    int index17 = -1;
                                    int num95 = -1;
                                    bool flag11 = true;
                                    bool flag12 = false;
                                    switch (num92)
                                    {
                                      case 5:
                                        num95 = 73;
                                        break;
                                      case 13:
                                        num95 = 24;
                                        break;
                                      case 30:
                                        num95 = 6;
                                        break;
                                      case 35:
                                        num95 = 2;
                                        break;
                                      case 51:
                                        num95 = (int) Utils.SelectRandom<short>(Main.rand, (short) 299, (short) 538);
                                        break;
                                      case 52:
                                        num95 = 356;
                                        break;
                                      case 53:
                                        num95 = 357;
                                        break;
                                      case 54:
                                        num95 = (int) Utils.SelectRandom<short>(Main.rand, (short) 355, (short) 358);
                                        break;
                                      case 55:
                                        num95 = (int) Utils.SelectRandom<short>(Main.rand, (short) 367, (short) 366);
                                        break;
                                      case 56:
                                        num95 = (int) Utils.SelectRandom<short>(Main.rand, (short) 359, (short) 359, (short) 359, (short) 359, (short) 360);
                                        break;
                                      case 57:
                                        num95 = 377;
                                        break;
                                      case 58:
                                        num95 = 300;
                                        break;
                                      case 59:
                                        num95 = (int) Utils.SelectRandom<short>(Main.rand, (short) 364, (short) 362);
                                        break;
                                      case 60:
                                        num95 = 148;
                                        break;
                                      case 61:
                                        num95 = 361;
                                        break;
                                      case 62:
                                        num95 = (int) Utils.SelectRandom<short>(Main.rand, (short) 487, (short) 486, (short) 485);
                                        break;
                                      case 63:
                                        num95 = 164;
                                        flag11 &= NPC.MechSpawn((float) num93, (float) num94, 165);
                                        break;
                                      case 64:
                                        num95 = 86;
                                        flag12 = true;
                                        break;
                                      case 65:
                                        num95 = 490;
                                        break;
                                      case 66:
                                        num95 = 82;
                                        break;
                                      case 67:
                                        num95 = 449;
                                        break;
                                      case 68:
                                        num95 = 167;
                                        break;
                                      case 69:
                                        num95 = 480;
                                        break;
                                      case 70:
                                        num95 = 48;
                                        break;
                                      case 71:
                                        num95 = (int) Utils.SelectRandom<short>(Main.rand, (short) 170, (short) 180, (short) 171);
                                        flag12 = true;
                                        break;
                                      case 72:
                                        num95 = 481;
                                        break;
                                      case 73:
                                        num95 = 482;
                                        break;
                                      case 74:
                                        num95 = 430;
                                        break;
                                      case 75:
                                        num95 = 489;
                                        break;
                                      case 76:
                                        num95 = 611;
                                        break;
                                      case 77:
                                        num95 = 602;
                                        break;
                                      case 78:
                                        num95 = (int) Utils.SelectRandom<short>(Main.rand, (short) 595, (short) 596, (short) 599, (short) 597, (short) 600, (short) 598);
                                        break;
                                      case 79:
                                        num95 = (int) Utils.SelectRandom<short>(Main.rand, (short) 616, (short) 617);
                                        break;
                                      case 80:
                                        num95 = (int) Utils.SelectRandom<short>(Main.rand, (short) 671, (short) 672);
                                        break;
                                      case 81:
                                        num95 = 673;
                                        break;
                                      case 82:
                                        num95 = (int) Utils.SelectRandom<short>(Main.rand, (short) 674, (short) 675);
                                        break;
                                    }
                                    if (((num95 == -1 || !Wiring.CheckMech(num89, num90, 30) ? 0 : (NPC.MechSpawn((float) num93, (float) num94, num95) ? 1 : 0)) & (flag11 ? 1 : 0)) != 0)
                                    {
                                      if (!flag12 || !Collision.SolidTiles(num89 - 2, num89 + 3, num90, num90 + 2))
                                      {
                                        index17 = NPC.NewNPC(Wiring.GetNPCSource(num89, num90), num93, num94, num95);
                                      }
                                      else
                                      {
                                        Vector2 position = new Vector2((float) (num93 - 4), (float) (num94 - 22)) - new Vector2(10f);
                                        Utils.PoofOfSmoke(position);
                                        NetMessage.SendData(106, number: ((int) position.X), number2: position.Y);
                                      }
                                    }
                                    if (index17 <= -1)
                                    {
                                      switch (num92)
                                      {
                                        case 2:
                                          if (Wiring.CheckMech(num89, num90, 600) && Item.MechSpawn((float) num93, (float) num94, 184) && Item.MechSpawn((float) num93, (float) num94, 1735) && Item.MechSpawn((float) num93, (float) num94, 1868))
                                          {
                                            Item.NewItem(Wiring.GetItemSource(num93, num94), num93, num94 - 16, 0, 0, 184);
                                            break;
                                          }
                                          break;
                                        case 4:
                                          if (Wiring.CheckMech(num89, num90, 30) && NPC.MechSpawn((float) num93, (float) num94, 1))
                                          {
                                            index17 = NPC.NewNPC(Wiring.GetNPCSource(num89, num90), num93, num94 - 12, 1);
                                            break;
                                          }
                                          break;
                                        case 7:
                                          if (Wiring.CheckMech(num89, num90, 30) && NPC.MechSpawn((float) num93, (float) num94, 49))
                                          {
                                            index17 = NPC.NewNPC(Wiring.GetNPCSource(num89, num90), num93 - 4, num94 - 6, 49);
                                            break;
                                          }
                                          break;
                                        case 8:
                                          if (Wiring.CheckMech(num89, num90, 30) && NPC.MechSpawn((float) num93, (float) num94, 55))
                                          {
                                            index17 = NPC.NewNPC(Wiring.GetNPCSource(num89, num90), num93, num94 - 12, 55);
                                            break;
                                          }
                                          break;
                                        case 9:
                                          int num96 = 46;
                                          if (BirthdayParty.PartyIsUp)
                                            num96 = 540;
                                          if (Wiring.CheckMech(num89, num90, 30) && NPC.MechSpawn((float) num93, (float) num94, num96))
                                          {
                                            index17 = NPC.NewNPC(Wiring.GetNPCSource(num89, num90), num93, num94 - 12, num96);
                                            break;
                                          }
                                          break;
                                        case 10:
                                          if (Wiring.CheckMech(num89, num90, 30) && NPC.MechSpawn((float) num93, (float) num94, 21))
                                          {
                                            index17 = NPC.NewNPC(Wiring.GetNPCSource(num89, num90), num93, num94, 21);
                                            break;
                                          }
                                          break;
                                        case 16:
                                          if (Wiring.CheckMech(num89, num90, 30) && NPC.MechSpawn((float) num93, (float) num94, 42))
                                          {
                                            if (!Collision.SolidTiles(num89 - 1, num89 + 1, num90, num90 + 1))
                                            {
                                              index17 = NPC.NewNPC(Wiring.GetNPCSource(num89, num90), num93, num94 - 12, 42);
                                              break;
                                            }
                                            Vector2 position = new Vector2((float) (num93 - 4), (float) (num94 - 22)) - new Vector2(10f);
                                            Utils.PoofOfSmoke(position);
                                            NetMessage.SendData(106, number: ((int) position.X), number2: position.Y);
                                            break;
                                          }
                                          break;
                                        case 17:
                                          if (Wiring.CheckMech(num89, num90, 600) && Item.MechSpawn((float) num93, (float) num94, 166))
                                          {
                                            Item.NewItem(Wiring.GetItemSource(num93, num94), num93, num94 - 20, 0, 0, 166);
                                            break;
                                          }
                                          break;
                                        case 18:
                                          if (Wiring.CheckMech(num89, num90, 30) && NPC.MechSpawn((float) num93, (float) num94, 67))
                                          {
                                            index17 = NPC.NewNPC(Wiring.GetNPCSource(num89, num90), num93, num94 - 12, 67);
                                            break;
                                          }
                                          break;
                                        case 23:
                                          if (Wiring.CheckMech(num89, num90, 30) && NPC.MechSpawn((float) num93, (float) num94, 63))
                                          {
                                            index17 = NPC.NewNPC(Wiring.GetNPCSource(num89, num90), num93, num94 - 12, 63);
                                            break;
                                          }
                                          break;
                                        case 27:
                                          if (Wiring.CheckMech(num89, num90, 30) && NPC.MechSpawn((float) num93, (float) num94, 85))
                                          {
                                            index17 = NPC.NewNPC(Wiring.GetNPCSource(num89, num90), num93 - 9, num94, 85);
                                            break;
                                          }
                                          break;
                                        case 28:
                                          if (Wiring.CheckMech(num89, num90, 30) && NPC.MechSpawn((float) num93, (float) num94, 74))
                                          {
                                            index17 = NPC.NewNPC(Wiring.GetNPCSource(num89, num90), num93, num94 - 12, (int) Utils.SelectRandom<short>(Main.rand, (short) 74, (short) 297, (short) 298));
                                            break;
                                          }
                                          break;
                                        case 34:
                                          for (int index18 = 0; index18 < 2; ++index18)
                                          {
                                            for (int index19 = 0; index19 < 3; ++index19)
                                            {
                                              Tile tile2 = Main.tile[num89 + index18, num90 + index19];
                                              tile2.type = (ushort) 349;
                                              tile2.frameX = (short) (index18 * 18 + 216);
                                              tile2.frameY = (short) (index19 * 18);
                                            }
                                          }
                                          Animation.NewTemporaryAnimation(0, (ushort) 349, num89, num90);
                                          if (Main.netMode == 2)
                                          {
                                            NetMessage.SendTileSquare(-1, num89, num90, 2, 3);
                                            break;
                                          }
                                          break;
                                        case 37:
                                          if (Wiring.CheckMech(num89, num90, 600) && Item.MechSpawn((float) num93, (float) num94, 58) && Item.MechSpawn((float) num93, (float) num94, 1734) && Item.MechSpawn((float) num93, (float) num94, 1867))
                                          {
                                            Item.NewItem(Wiring.GetItemSource(num93, num94), num93, num94 - 16, 0, 0, 58);
                                            break;
                                          }
                                          break;
                                        case 40:
                                          if (Wiring.CheckMech(num89, num90, 300))
                                          {
                                            int length = 50;
                                            int[] numArray = new int[length];
                                            int maxValue = 0;
                                            for (int index20 = 0; index20 < 200; ++index20)
                                            {
                                              if (Main.npc[index20].active && (Main.npc[index20].type == 17 || Main.npc[index20].type == 19 || Main.npc[index20].type == 22 || Main.npc[index20].type == 38 || Main.npc[index20].type == 54 || Main.npc[index20].type == 107 || Main.npc[index20].type == 108 || Main.npc[index20].type == 142 || Main.npc[index20].type == 160 || Main.npc[index20].type == 207 || Main.npc[index20].type == 209 || Main.npc[index20].type == 227 || Main.npc[index20].type == 228 || Main.npc[index20].type == 229 || Main.npc[index20].type == 368 || Main.npc[index20].type == 369 || Main.npc[index20].type == 550 || Main.npc[index20].type == 441 || Main.npc[index20].type == 588))
                                              {
                                                numArray[maxValue] = index20;
                                                ++maxValue;
                                                if (maxValue >= length)
                                                  break;
                                              }
                                            }
                                            if (maxValue > 0)
                                            {
                                              int number = numArray[Main.rand.Next(maxValue)];
                                              Main.npc[number].position.X = (float) (num93 - Main.npc[number].width / 2);
                                              Main.npc[number].position.Y = (float) (num94 - Main.npc[number].height - 1);
                                              NetMessage.SendData(23, number: number);
                                              break;
                                            }
                                            break;
                                          }
                                          break;
                                        case 41:
                                          if (Wiring.CheckMech(num89, num90, 300))
                                          {
                                            int length = 50;
                                            int[] numArray = new int[length];
                                            int maxValue = 0;
                                            for (int index21 = 0; index21 < 200; ++index21)
                                            {
                                              if (Main.npc[index21].active && (Main.npc[index21].type == 18 || Main.npc[index21].type == 20 || Main.npc[index21].type == 124 || Main.npc[index21].type == 178 || Main.npc[index21].type == 208 || Main.npc[index21].type == 353 || Main.npc[index21].type == 633 || Main.npc[index21].type == 663))
                                              {
                                                numArray[maxValue] = index21;
                                                ++maxValue;
                                                if (maxValue >= length)
                                                  break;
                                              }
                                            }
                                            if (maxValue > 0)
                                            {
                                              int number = numArray[Main.rand.Next(maxValue)];
                                              Main.npc[number].position.X = (float) (num93 - Main.npc[number].width / 2);
                                              Main.npc[number].position.Y = (float) (num94 - Main.npc[number].height - 1);
                                              NetMessage.SendData(23, number: number);
                                              break;
                                            }
                                            break;
                                          }
                                          break;
                                        case 42:
                                          if (Wiring.CheckMech(num89, num90, 30) && NPC.MechSpawn((float) num93, (float) num94, 58))
                                          {
                                            index17 = NPC.NewNPC(Wiring.GetNPCSource(num89, num90), num93, num94 - 12, 58);
                                            break;
                                          }
                                          break;
                                        case 50:
                                          if (Wiring.CheckMech(num89, num90, 30) && NPC.MechSpawn((float) num93, (float) num94, 65))
                                          {
                                            if (!Collision.SolidTiles(num89 - 2, num89 + 3, num90, num90 + 2))
                                            {
                                              index17 = NPC.NewNPC(Wiring.GetNPCSource(num89, num90), num93, num94 - 12, 65);
                                              break;
                                            }
                                            Vector2 position = new Vector2((float) (num93 - 4), (float) (num94 - 22)) - new Vector2(10f);
                                            Utils.PoofOfSmoke(position);
                                            NetMessage.SendData(106, number: ((int) position.X), number2: position.Y);
                                            break;
                                          }
                                          break;
                                      }
                                    }
                                    if (index17 < 0)
                                      return;
                                    Main.npc[index17].value = 0.0f;
                                    Main.npc[index17].npcSlots = 0.0f;
                                    Main.npc[index17].SpawnedFromStatue = true;
                                    Main.npc[index17].CanBeReplacedByOtherNPCs = true;
                                    return;
                                  case 349:
                                    int num97 = (int) tile1.frameY / 18 % 3;
                                    int index22 = j - num97;
                                    int num98 = (int) tile1.frameX / 18;
                                    while (num98 >= 2)
                                      num98 -= 2;
                                    int index23 = i - num98;
                                    Wiring.SkipWire(index23, index22);
                                    Wiring.SkipWire(index23, index22 + 1);
                                    Wiring.SkipWire(index23, index22 + 2);
                                    Wiring.SkipWire(index23 + 1, index22);
                                    Wiring.SkipWire(index23 + 1, index22 + 1);
                                    Wiring.SkipWire(index23 + 1, index22 + 2);
                                    short num99 = Main.tile[index23, index22].frameX != (short) 0 ? (short) -216 : (short) 216;
                                    for (int index24 = 0; index24 < 2; ++index24)
                                    {
                                      for (int index25 = 0; index25 < 3; ++index25)
                                        Main.tile[index23 + index24, index22 + index25].frameX += num99;
                                    }
                                    if (Main.netMode == 2)
                                      NetMessage.SendTileSquare(-1, index23, index22, 2, 3);
                                    Animation.NewTemporaryAnimation(num99 > (short) 0 ? 0 : 1, (ushort) 349, index23, index22);
                                    return;
                                  case 506:
                                    int num100 = (int) tile1.frameY / 18 % 3;
                                    int index26 = j - num100;
                                    int num101 = (int) tile1.frameX / 18;
                                    while (num101 >= 2)
                                      num101 -= 2;
                                    int index27 = i - num101;
                                    Wiring.SkipWire(index27, index26);
                                    Wiring.SkipWire(index27, index26 + 1);
                                    Wiring.SkipWire(index27, index26 + 2);
                                    Wiring.SkipWire(index27 + 1, index26);
                                    Wiring.SkipWire(index27 + 1, index26 + 1);
                                    Wiring.SkipWire(index27 + 1, index26 + 2);
                                    short num102 = Main.tile[index27, index26].frameX >= (short) 72 ? (short) -72 : (short) 72;
                                    for (int index28 = 0; index28 < 2; ++index28)
                                    {
                                      for (int index29 = 0; index29 < 3; ++index29)
                                        Main.tile[index27 + index28, index26 + index29].frameX += num102;
                                    }
                                    if (Main.netMode != 2)
                                      return;
                                    NetMessage.SendTileSquare(-1, index27, index26, 2, 3);
                                    return;
                                  case 546:
                                    tile1.type = (ushort) 557;
                                    WorldGen.SquareTileFrame(i, j);
                                    NetMessage.SendTileSquare(-1, i, j);
                                    return;
                                  case 557:
                                    tile1.type = (ushort) 546;
                                    WorldGen.SquareTileFrame(i, j);
                                    NetMessage.SendTileSquare(-1, i, j);
                                    return;
                                  default:
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
      }
    }

    public static void ToggleHolidayLight(
      int i,
      int j,
      Tile tileCache,
      bool? forcedStateWhereTrueIsOn)
    {
      bool flag = tileCache.frameX >= (short) 54;
      if (forcedStateWhereTrueIsOn.HasValue && !forcedStateWhereTrueIsOn.Value == flag)
        return;
      if (tileCache.frameX < (short) 54)
        tileCache.frameX += (short) 54;
      else
        tileCache.frameX -= (short) 54;
      NetMessage.SendTileSquare(-1, i, j);
    }

    public static void ToggleHangingLantern(
      int i,
      int j,
      Tile tileCache,
      bool? forcedStateWhereTrueIsOn,
      bool doSkipWires)
    {
      int num1 = (int) tileCache.frameY / 18;
      while (num1 >= 2)
        num1 -= 2;
      int y = j - num1;
      short num2 = 18;
      if (tileCache.frameX > (short) 0)
        num2 = (short) -18;
      bool flag = tileCache.frameX > (short) 0;
      if (forcedStateWhereTrueIsOn.HasValue && !forcedStateWhereTrueIsOn.Value == flag)
        return;
      Main.tile[i, y].frameX += num2;
      Main.tile[i, y + 1].frameX += num2;
      if (doSkipWires)
      {
        Wiring.SkipWire(i, y);
        Wiring.SkipWire(i, y + 1);
      }
      NetMessage.SendTileSquare(-1, i, j, 1, 2);
    }

    public static void Toggle2x2Light(
      int i,
      int j,
      Tile tileCache,
      bool? forcedStateWhereTrueIsOn,
      bool doSkipWires)
    {
      int num1 = (int) tileCache.frameY / 18;
      while (num1 >= 2)
        num1 -= 2;
      int index1 = j - num1;
      int num2 = (int) tileCache.frameX / 18;
      if (num2 > 1)
        num2 -= 2;
      int index2 = i - num2;
      short num3 = 36;
      if (Main.tile[index2, index1].frameX > (short) 0)
        num3 = (short) -36;
      bool flag = Main.tile[index2, index1].frameX > (short) 0;
      if (forcedStateWhereTrueIsOn.HasValue && !forcedStateWhereTrueIsOn.Value == flag)
        return;
      Main.tile[index2, index1].frameX += num3;
      Main.tile[index2, index1 + 1].frameX += num3;
      Main.tile[index2 + 1, index1].frameX += num3;
      Main.tile[index2 + 1, index1 + 1].frameX += num3;
      if (doSkipWires)
      {
        Wiring.SkipWire(index2, index1);
        Wiring.SkipWire(index2 + 1, index1);
        Wiring.SkipWire(index2, index1 + 1);
        Wiring.SkipWire(index2 + 1, index1 + 1);
      }
      NetMessage.SendTileSquare(-1, index2, index1, 2, 2);
    }

    public static void ToggleLampPost(
      int i,
      int j,
      Tile tileCache,
      bool? forcedStateWhereTrueIsOn,
      bool doSkipWires)
    {
      int tileY = j - (int) tileCache.frameY / 18;
      short num = 18;
      if (tileCache.frameX > (short) 0)
        num = (short) -18;
      bool flag = tileCache.frameX > (short) 0;
      if (forcedStateWhereTrueIsOn.HasValue && !forcedStateWhereTrueIsOn.Value == flag)
        return;
      for (int y = tileY; y < tileY + 6; ++y)
      {
        Main.tile[i, y].frameX += num;
        if (doSkipWires)
          Wiring.SkipWire(i, y);
      }
      NetMessage.SendTileSquare(-1, i, tileY, 1, 6);
    }

    public static void ToggleTorch(int i, int j, Tile tileCache, bool? forcedStateWhereTrueIsOn)
    {
      bool flag = tileCache.frameX >= (short) 66;
      if (forcedStateWhereTrueIsOn.HasValue && !forcedStateWhereTrueIsOn.Value == flag)
        return;
      if (tileCache.frameX < (short) 66)
        tileCache.frameX += (short) 66;
      else
        tileCache.frameX -= (short) 66;
      NetMessage.SendTileSquare(-1, i, j);
    }

    public static void ToggleCandle(int i, int j, Tile tileCache, bool? forcedStateWhereTrueIsOn)
    {
      short num = 18;
      if (tileCache.frameX > (short) 0)
        num = (short) -18;
      bool flag = tileCache.frameX > (short) 0;
      if (forcedStateWhereTrueIsOn.HasValue && !forcedStateWhereTrueIsOn.Value == flag)
        return;
      tileCache.frameX += num;
      NetMessage.SendTileSquare(-1, i, j, 3);
    }

    public static void ToggleLamp(
      int i,
      int j,
      Tile tileCache,
      bool? forcedStateWhereTrueIsOn,
      bool doSkipWires)
    {
      int num1 = (int) tileCache.frameY / 18;
      while (num1 >= 3)
        num1 -= 3;
      int index = j - num1;
      short num2 = 18;
      if (tileCache.frameX > (short) 0)
        num2 = (short) -18;
      bool flag = tileCache.frameX > (short) 0;
      if (forcedStateWhereTrueIsOn.HasValue && !forcedStateWhereTrueIsOn.Value == flag)
        return;
      Main.tile[i, index].frameX += num2;
      Main.tile[i, index + 1].frameX += num2;
      Main.tile[i, index + 2].frameX += num2;
      if (doSkipWires)
      {
        Wiring.SkipWire(i, index);
        Wiring.SkipWire(i, index + 1);
        Wiring.SkipWire(i, index + 2);
      }
      NetMessage.SendTileSquare(-1, i, index, 1, 3);
    }

    public static void ToggleChandelier(
      int i,
      int j,
      Tile tileCache,
      bool? forcedStateWhereTrueIsOn,
      bool doSkipWires)
    {
      int num1 = (int) tileCache.frameY / 18;
      while (num1 >= 3)
        num1 -= 3;
      int index1 = j - num1;
      int num2 = (int) tileCache.frameX % 108 / 18;
      if (num2 > 2)
        num2 -= 3;
      int index2 = i - num2;
      short num3 = 54;
      if ((int) Main.tile[index2, index1].frameX % 108 > 0)
        num3 = (short) -54;
      bool flag = (int) Main.tile[index2, index1].frameX % 108 > 0;
      if (forcedStateWhereTrueIsOn.HasValue && !forcedStateWhereTrueIsOn.Value == flag)
        return;
      for (int x = index2; x < index2 + 3; ++x)
      {
        for (int y = index1; y < index1 + 3; ++y)
        {
          Main.tile[x, y].frameX += num3;
          if (doSkipWires)
            Wiring.SkipWire(x, y);
        }
      }
      NetMessage.SendTileSquare(-1, index2 + 1, index1 + 1, 3);
    }

    public static void ToggleCampFire(
      int i,
      int j,
      Tile tileCache,
      bool? forcedStateWhereTrueIsOn,
      bool doSkipWires)
    {
      int num1 = (int) tileCache.frameX % 54 / 18;
      int num2 = (int) tileCache.frameY % 36 / 18;
      int tileX = i - num1;
      int tileY = j - num2;
      bool flag = Main.tile[tileX, tileY].frameY >= (short) 36;
      if (forcedStateWhereTrueIsOn.HasValue && !forcedStateWhereTrueIsOn.Value == flag)
        return;
      int num3 = 36;
      if (Main.tile[tileX, tileY].frameY >= (short) 36)
        num3 = -36;
      for (int x = tileX; x < tileX + 3; ++x)
      {
        for (int y = tileY; y < tileY + 2; ++y)
        {
          if (doSkipWires)
            Wiring.SkipWire(x, y);
          Main.tile[x, y].frameY += (short) num3;
        }
      }
      NetMessage.SendTileSquare(-1, tileX, tileY, 3, 2);
    }

    public static void ToggleFirePlace(
      int i,
      int j,
      Tile theBlock,
      bool? forcedStateWhereTrueIsOn,
      bool doSkipWires)
    {
      int num1 = (int) theBlock.frameX % 54 / 18;
      int num2 = (int) theBlock.frameY % 36 / 18;
      int tileX = i - num1;
      int tileY = j - num2;
      bool flag = Main.tile[tileX, tileY].frameX >= (short) 54;
      if (forcedStateWhereTrueIsOn.HasValue && !forcedStateWhereTrueIsOn.Value == flag)
        return;
      int num3 = 54;
      if (Main.tile[tileX, tileY].frameX >= (short) 54)
        num3 = -54;
      for (int x = tileX; x < tileX + 3; ++x)
      {
        for (int y = tileY; y < tileY + 2; ++y)
        {
          if (doSkipWires)
            Wiring.SkipWire(x, y);
          Main.tile[x, y].frameX += (short) num3;
        }
      }
      NetMessage.SendTileSquare(-1, tileX, tileY, 3, 2);
    }

    private static void GeyserTrap(int i, int j)
    {
      Tile tile = Main.tile[i, j];
      if (tile.type != (ushort) 443)
        return;
      int num1 = (int) tile.frameX / 36;
      int num2 = i - ((int) tile.frameX - num1 * 36) / 18;
      int num3 = j;
      if (!Wiring.CheckMech(num2, num3, 200))
        return;
      Vector2 zero = Vector2.Zero;
      Vector2 vector2_1 = Vector2.Zero;
      int Type = 654;
      int Damage = 20;
      Vector2 vector2_2;
      if (num1 < 2)
      {
        vector2_2 = new Vector2((float) (num2 + 1), (float) num3) * 16f;
        vector2_1 = new Vector2(0.0f, -8f);
      }
      else
      {
        vector2_2 = new Vector2((float) (num2 + 1), (float) (num3 + 1)) * 16f;
        vector2_1 = new Vector2(0.0f, 8f);
      }
      if (Type == 0)
        return;
      Projectile.NewProjectile(Wiring.GetProjectileSource(num2, num3), (float) (int) vector2_2.X, (float) (int) vector2_2.Y, vector2_1.X, vector2_1.Y, Type, Damage, 2f, Main.myPlayer);
    }

    private static void Teleport()
    {
      if ((double) Wiring._teleport[0].X < (double) Wiring._teleport[1].X + 3.0 && (double) Wiring._teleport[0].X > (double) Wiring._teleport[1].X - 3.0 && (double) Wiring._teleport[0].Y > (double) Wiring._teleport[1].Y - 3.0 && (double) Wiring._teleport[0].Y < (double) Wiring._teleport[1].Y)
        return;
      Rectangle[] rectangleArray = new Rectangle[2];
      rectangleArray[0].X = (int) ((double) Wiring._teleport[0].X * 16.0);
      rectangleArray[0].Width = 48;
      rectangleArray[0].Height = 48;
      rectangleArray[0].Y = (int) ((double) Wiring._teleport[0].Y * 16.0 - (double) rectangleArray[0].Height);
      rectangleArray[1].X = (int) ((double) Wiring._teleport[1].X * 16.0);
      rectangleArray[1].Width = 48;
      rectangleArray[1].Height = 48;
      rectangleArray[1].Y = (int) ((double) Wiring._teleport[1].Y * 16.0 - (double) rectangleArray[1].Height);
      for (int index1 = 0; index1 < 2; ++index1)
      {
        Vector2 vector2_1 = new Vector2((float) (rectangleArray[1].X - rectangleArray[0].X), (float) (rectangleArray[1].Y - rectangleArray[0].Y));
        if (index1 == 1)
          vector2_1 = new Vector2((float) (rectangleArray[0].X - rectangleArray[1].X), (float) (rectangleArray[0].Y - rectangleArray[1].Y));
        if (!Wiring.blockPlayerTeleportationForOneIteration)
        {
          for (int index2 = 0; index2 < (int) byte.MaxValue; ++index2)
          {
            if (Main.player[index2].active && !Main.player[index2].dead && !Main.player[index2].teleporting && Wiring.TeleporterHitboxIntersects(rectangleArray[index1], Main.player[index2].Hitbox))
            {
              Vector2 vector2_2 = Main.player[index2].position + vector2_1;
              Main.player[index2].teleporting = true;
              if (Main.netMode == 2)
                RemoteClient.CheckSection(index2, vector2_2);
              Main.player[index2].Teleport(vector2_2);
              if (Main.netMode == 2)
                NetMessage.SendData(65, number2: ((float) index2), number3: vector2_2.X, number4: vector2_2.Y);
            }
          }
        }
        for (int index3 = 0; index3 < 200; ++index3)
        {
          if (Main.npc[index3].active && !Main.npc[index3].teleporting && Main.npc[index3].lifeMax > 5 && !Main.npc[index3].boss && !Main.npc[index3].noTileCollide)
          {
            int type = Main.npc[index3].type;
            if (!NPCID.Sets.TeleportationImmune[type] && Wiring.TeleporterHitboxIntersects(rectangleArray[index1], Main.npc[index3].Hitbox))
            {
              Main.npc[index3].teleporting = true;
              Main.npc[index3].Teleport(Main.npc[index3].position + vector2_1);
            }
          }
        }
      }
      for (int index = 0; index < (int) byte.MaxValue; ++index)
        Main.player[index].teleporting = false;
      for (int index = 0; index < 200; ++index)
        Main.npc[index].teleporting = false;
    }

    private static bool TeleporterHitboxIntersects(Rectangle teleporter, Rectangle entity)
    {
      Rectangle rectangle = Rectangle.Union(teleporter, entity);
      return rectangle.Width <= teleporter.Width + entity.Width && rectangle.Height <= teleporter.Height + entity.Height;
    }

    private static void DeActive(int i, int j)
    {
      if (!Main.tile[i, j].active() || Main.tile[i, j].type == (ushort) 226 && (double) j > Main.worldSurface && !NPC.downedPlantBoss)
        return;
      bool flag = Main.tileSolid[(int) Main.tile[i, j].type] && !TileID.Sets.NotReallySolid[(int) Main.tile[i, j].type];
      switch (Main.tile[i, j].type)
      {
        case 314:
        case 386:
        case 387:
        case 388:
        case 389:
        case 476:
          flag = false;
          break;
      }
      if (!flag || Main.tile[i, j - 1].active() && (TileID.Sets.BasicChest[(int) Main.tile[i, j - 1].type] || Main.tile[i, j - 1].type == (ushort) 26 || Main.tile[i, j - 1].type == (ushort) 77 || Main.tile[i, j - 1].type == (ushort) 88 || Main.tile[i, j - 1].type == (ushort) 470 || Main.tile[i, j - 1].type == (ushort) 475 || Main.tile[i, j - 1].type == (ushort) 237 || Main.tile[i, j - 1].type == (ushort) 597 || !WorldGen.CanKillTile(i, j)))
        return;
      Main.tile[i, j].inActive(true);
      WorldGen.SquareTileFrame(i, j, false);
      if (Main.netMode == 1)
        return;
      NetMessage.SendTileSquare(-1, i, j);
    }

    private static void ReActive(int i, int j)
    {
      Main.tile[i, j].inActive(false);
      WorldGen.SquareTileFrame(i, j, false);
      if (Main.netMode == 1)
        return;
      NetMessage.SendTileSquare(-1, i, j);
    }

    private static void MassWireOperationInner(
      Player user,
      Point ps,
      Point pe,
      Vector2 dropPoint,
      bool dir,
      ref int wireCount,
      ref int actuatorCount)
    {
      Math.Abs(ps.X - pe.X);
      Math.Abs(ps.Y - pe.Y);
      int num1 = Math.Sign(pe.X - ps.X);
      int num2 = Math.Sign(pe.Y - ps.Y);
      WiresUI.Settings.MultiToolMode toolMode = WiresUI.Settings.ToolMode;
      Point pt = new Point();
      bool flag1 = false;
      Item.StartCachingType(530);
      Item.StartCachingType(849);
      bool flag2 = dir;
      int num3;
      int num4;
      int num5;
      if (flag2)
      {
        pt.X = ps.X;
        num3 = ps.Y;
        num4 = pe.Y;
        num5 = num2;
      }
      else
      {
        pt.Y = ps.Y;
        num3 = ps.X;
        num4 = pe.X;
        num5 = num1;
      }
      for (int index = num3; index != num4 && !flag1; index += num5)
      {
        if (flag2)
          pt.Y = index;
        else
          pt.X = index;
        bool? nullable = Wiring.MassWireOperationStep(user, pt, toolMode, ref wireCount, ref actuatorCount);
        if (nullable.HasValue && !nullable.Value)
        {
          flag1 = true;
          break;
        }
      }
      int num6;
      int num7;
      int num8;
      if (flag2)
      {
        pt.Y = pe.Y;
        num6 = ps.X;
        num7 = pe.X;
        num8 = num1;
      }
      else
      {
        pt.X = pe.X;
        num6 = ps.Y;
        num7 = pe.Y;
        num8 = num2;
      }
      for (int index = num6; index != num7 && !flag1; index += num8)
      {
        if (!flag2)
          pt.Y = index;
        else
          pt.X = index;
        bool? nullable = Wiring.MassWireOperationStep(user, pt, toolMode, ref wireCount, ref actuatorCount);
        if (nullable.HasValue && !nullable.Value)
        {
          flag1 = true;
          break;
        }
      }
      if (!flag1)
        Wiring.MassWireOperationStep(user, pe, toolMode, ref wireCount, ref actuatorCount);
      EntitySource_ByItemSourceId reason = new EntitySource_ByItemSourceId((Entity) user, 5);
      Item.DropCache((IEntitySource) reason, dropPoint, Vector2.Zero, 530);
      Item.DropCache((IEntitySource) reason, dropPoint, Vector2.Zero, 849);
    }

    private static bool? MassWireOperationStep(
      Player user,
      Point pt,
      WiresUI.Settings.MultiToolMode mode,
      ref int wiresLeftToConsume,
      ref int actuatorsLeftToConstume)
    {
      if (!WorldGen.InWorld(pt.X, pt.Y, 1))
        return new bool?();
      Tile tile = Main.tile[pt.X, pt.Y];
      if (tile == null)
        return new bool?();
      if (user != null && !user.CanDoWireStuffHere(pt.X, pt.Y))
        return new bool?();
      if (!mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Cutter))
      {
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Red) && !tile.wire())
        {
          if (wiresLeftToConsume <= 0)
            return new bool?(false);
          --wiresLeftToConsume;
          WorldGen.PlaceWire(pt.X, pt.Y);
          NetMessage.SendData(17, number: 5, number2: ((float) pt.X), number3: ((float) pt.Y));
        }
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Green) && !tile.wire3())
        {
          if (wiresLeftToConsume <= 0)
            return new bool?(false);
          --wiresLeftToConsume;
          WorldGen.PlaceWire3(pt.X, pt.Y);
          NetMessage.SendData(17, number: 12, number2: ((float) pt.X), number3: ((float) pt.Y));
        }
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Blue) && !tile.wire2())
        {
          if (wiresLeftToConsume <= 0)
            return new bool?(false);
          --wiresLeftToConsume;
          WorldGen.PlaceWire2(pt.X, pt.Y);
          NetMessage.SendData(17, number: 10, number2: ((float) pt.X), number3: ((float) pt.Y));
        }
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Yellow) && !tile.wire4())
        {
          if (wiresLeftToConsume <= 0)
            return new bool?(false);
          --wiresLeftToConsume;
          WorldGen.PlaceWire4(pt.X, pt.Y);
          NetMessage.SendData(17, number: 16, number2: ((float) pt.X), number3: ((float) pt.Y));
        }
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Actuator) && !tile.actuator())
        {
          if (actuatorsLeftToConstume <= 0)
            return new bool?(false);
          --actuatorsLeftToConstume;
          WorldGen.PlaceActuator(pt.X, pt.Y);
          NetMessage.SendData(17, number: 8, number2: ((float) pt.X), number3: ((float) pt.Y));
        }
      }
      if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Cutter))
      {
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Red) && tile.wire() && WorldGen.KillWire(pt.X, pt.Y))
          NetMessage.SendData(17, number: 6, number2: ((float) pt.X), number3: ((float) pt.Y));
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Green) && tile.wire3() && WorldGen.KillWire3(pt.X, pt.Y))
          NetMessage.SendData(17, number: 13, number2: ((float) pt.X), number3: ((float) pt.Y));
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Blue) && tile.wire2() && WorldGen.KillWire2(pt.X, pt.Y))
          NetMessage.SendData(17, number: 11, number2: ((float) pt.X), number3: ((float) pt.Y));
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Yellow) && tile.wire4() && WorldGen.KillWire4(pt.X, pt.Y))
          NetMessage.SendData(17, number: 17, number2: ((float) pt.X), number3: ((float) pt.Y));
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Actuator) && tile.actuator() && WorldGen.KillActuator(pt.X, pt.Y))
          NetMessage.SendData(17, number: 9, number2: ((float) pt.X), number3: ((float) pt.Y));
      }
      return new bool?(true);
    }
  }
}
