// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Events.MysticLogFairiesEvent
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.Enums;

namespace Terraria.GameContent.Events
{
  public class MysticLogFairiesEvent
  {
    private bool _canSpawnFairies;
    private int _delayUntilNextAttempt;
    private const int DELAY_BETWEEN_ATTEMPTS = 60;
    private List<Point> _stumpCoords = new List<Point>();

    public void WorldClear()
    {
      this._canSpawnFairies = false;
      this._delayUntilNextAttempt = 0;
      this._stumpCoords.Clear();
    }

    public void StartWorld()
    {
      if (Main.netMode == 1)
        return;
      this.ScanWholeOverworldForLogs();
    }

    public void StartNight()
    {
      if (Main.netMode == 1)
        return;
      this._canSpawnFairies = true;
      this._delayUntilNextAttempt = 0;
      this.ScanWholeOverworldForLogs();
    }

    public void UpdateTime()
    {
      if (Main.netMode == 1 || !this._canSpawnFairies || !this.IsAGoodTime())
        return;
      this._delayUntilNextAttempt = Math.Max(0, this._delayUntilNextAttempt - Main.dayRate);
      if (this._delayUntilNextAttempt != 0)
        return;
      this._delayUntilNextAttempt = 60;
      this.TrySpawningFairies();
    }

    private bool IsAGoodTime() => !Main.dayTime && (Main.remixWorld || Main.time >= 6480.0000965595245 && Main.time <= 25920.000386238098);

    private void TrySpawningFairies()
    {
      if ((double) Main.maxRaining > 0.0 || Main.bloodMoon || NPC.MoonLordCountdown > 0 || Main.snowMoon || Main.pumpkinMoon || Main.invasionType > 0 || this._stumpCoords.Count == 0)
        return;
      int oneOverSpawnChance = this.GetOneOverSpawnChance();
      bool flag = false;
      for (int index = 0; index < Main.dayRate; ++index)
      {
        if (Main.rand.Next(oneOverSpawnChance) == 0)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        return;
      Point stumpCoord = this._stumpCoords[Main.rand.Next(this._stumpCoords.Count)];
      Vector2 worldCoordinates = stumpCoord.ToWorldCoordinates(24f);
      worldCoordinates.Y -= 50f;
      if (WorldGen.PlayerLOS(stumpCoord.X, stumpCoord.Y))
        return;
      int num1 = Main.rand.Next(1, 4);
      if (Main.rand.Next(7) == 0)
        ++num1;
      int num2 = (int) Utils.SelectRandom<short>(Main.rand, (short) 585, (short) 584, (short) 583);
      for (int index = 0; index < num1; ++index)
      {
        int Type = (int) Utils.SelectRandom<short>(Main.rand, (short) 585, (short) 584, (short) 583);
        if (Main.tenthAnniversaryWorld && Main.rand.Next(4) != 0)
          Type = 583;
        int number = NPC.NewNPC((IEntitySource) new EntitySource_WorldEvent(), (int) worldCoordinates.X, (int) worldCoordinates.Y, Type);
        if (Main.netMode == 2 && number < 200)
          NetMessage.SendData(23, number: number);
      }
      this._canSpawnFairies = false;
    }

    public void FallenLogDestroyed()
    {
      if (Main.netMode == 1)
        return;
      this.ScanWholeOverworldForLogs();
    }

    private void ScanWholeOverworldForLogs()
    {
      this._stumpCoords.Clear();
      NPC.fairyLog = false;
      int num1 = (int) Main.worldSurface - 10;
      int num2 = 100;
      int num3 = Main.maxTilesX - 100;
      if (Main.remixWorld)
      {
        num1 = Main.maxTilesY - 350;
        num2 = (int) Main.rockLayer;
      }
      int num4 = 3;
      int num5 = 2;
      List<Point> pointList = new List<Point>();
      for (int x = 100; x < num3; x += num4)
      {
        for (int y = num1; y >= num2; y -= num5)
        {
          Tile tile = Main.tile[x, y];
          if (tile.active() && tile.type == (ushort) 488 && tile.liquid == (byte) 0)
          {
            pointList.Add(new Point(x, y));
            NPC.fairyLog = true;
          }
        }
      }
      foreach (Point stumpRandomPoint in pointList)
        this._stumpCoords.Add(this.GetStumpTopLeft(stumpRandomPoint));
    }

    private Point GetStumpTopLeft(Point stumpRandomPoint)
    {
      Tile tile = Main.tile[stumpRandomPoint.X, stumpRandomPoint.Y];
      Point stumpTopLeft = stumpRandomPoint;
      stumpTopLeft.X -= (int) tile.frameX / 18;
      stumpTopLeft.Y -= (int) tile.frameY / 18;
      return stumpTopLeft;
    }

    private int GetOneOverSpawnChance()
    {
      int num;
      switch (Main.GetMoonPhase())
      {
        case MoonPhase.Full:
        case MoonPhase.Empty:
          num = 3600;
          break;
        default:
          num = 10800;
          break;
      }
      return num / 60;
    }
  }
}
