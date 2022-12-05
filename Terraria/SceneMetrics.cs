// Decompiled with JetBrains decompiler
// Type: Terraria.SceneMetrics
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Terraria
{
  public class SceneMetrics
  {
    public static int ShimmerTileThreshold = 300;
    public static int CorruptionTileThreshold = 300;
    public static int CorruptionTileMax = 1000;
    public static int CrimsonTileThreshold = 300;
    public static int CrimsonTileMax = 1000;
    public static int HallowTileThreshold = 125;
    public static int HallowTileMax = 600;
    public static int JungleTileThreshold = 140;
    public static int JungleTileMax = 700;
    public static int SnowTileThreshold = 1500;
    public static int SnowTileMax = 6000;
    public static int DesertTileThreshold = 1500;
    public static int MushroomTileThreshold = 100;
    public static int MushroomTileMax = 160;
    public static int MeteorTileThreshold = 75;
    public static int GraveyardTileMax = 36;
    public static int GraveyardTileMin = 16;
    public static int GraveyardTileThreshold = 28;
    public bool CanPlayCreditsRoll;
    public bool[] NPCBannerBuff = new bool[290];
    public bool hasBanner;
    private readonly int[] _tileCounts = new int[(int) TileID.Count];
    private readonly int[] _liquidCounts = new int[(int) LiquidID.Count];
    private readonly List<Point> _oreFinderTileLocations = new List<Point>(512);
    public int bestOre;

    public Point? ClosestOrePosition { get; private set; }

    public int ShimmerTileCount { get; set; }

    public int EvilTileCount { get; set; }

    public int HolyTileCount { get; set; }

    public int HoneyBlockCount { get; set; }

    public int ActiveMusicBox { get; set; }

    public int SandTileCount { get; private set; }

    public int MushroomTileCount { get; private set; }

    public int SnowTileCount { get; private set; }

    public int WaterCandleCount { get; private set; }

    public int PeaceCandleCount { get; private set; }

    public int ShadowCandleCount { get; private set; }

    public int PartyMonolithCount { get; private set; }

    public int MeteorTileCount { get; private set; }

    public int BloodTileCount { get; private set; }

    public int JungleTileCount { get; private set; }

    public int DungeonTileCount { get; private set; }

    public bool HasSunflower { get; private set; }

    public bool HasGardenGnome { get; private set; }

    public bool HasClock { get; private set; }

    public bool HasCampfire { get; private set; }

    public bool HasStarInBottle { get; private set; }

    public bool HasHeartLantern { get; private set; }

    public int ActiveFountainColor { get; private set; }

    public int ActiveMonolithType { get; private set; }

    public bool BloodMoonMonolith { get; private set; }

    public bool MoonLordMonolith { get; private set; }

    public bool EchoMonolith { get; private set; }

    public int ShimmerMonolithState { get; private set; }

    public bool HasCatBast { get; private set; }

    public int GraveyardTileCount { get; private set; }

    public bool EnoughTilesForShimmer => this.ShimmerTileCount >= SceneMetrics.ShimmerTileThreshold;

    public bool EnoughTilesForJungle => this.JungleTileCount >= SceneMetrics.JungleTileThreshold;

    public bool EnoughTilesForHallow => this.HolyTileCount >= SceneMetrics.HallowTileThreshold;

    public bool EnoughTilesForSnow => this.SnowTileCount >= SceneMetrics.SnowTileThreshold;

    public bool EnoughTilesForGlowingMushroom => this.MushroomTileCount >= SceneMetrics.MushroomTileThreshold;

    public bool EnoughTilesForDesert => this.SandTileCount >= SceneMetrics.DesertTileThreshold;

    public bool EnoughTilesForCorruption => this.EvilTileCount >= SceneMetrics.CorruptionTileThreshold;

    public bool EnoughTilesForCrimson => this.BloodTileCount >= SceneMetrics.CrimsonTileThreshold;

    public bool EnoughTilesForMeteor => this.MeteorTileCount >= SceneMetrics.MeteorTileThreshold;

    public bool EnoughTilesForGraveyard => this.GraveyardTileCount >= SceneMetrics.GraveyardTileThreshold;

    public SceneMetrics() => this.Reset();

    public void ScanAndExportToMain(SceneMetricsScanSettings settings)
    {
      this.Reset();
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      if (settings.ScanOreFinderData)
        this._oreFinderTileLocations.Clear();
      if (settings.BiomeScanCenterPositionInWorld.HasValue)
      {
        Point tileCoordinates = settings.BiomeScanCenterPositionInWorld.Value.ToTileCoordinates();
        Microsoft.Xna.Framework.Rectangle tileRectangle = new Microsoft.Xna.Framework.Rectangle(tileCoordinates.X - Main.buffScanAreaWidth / 2, tileCoordinates.Y - Main.buffScanAreaHeight / 2, Main.buffScanAreaWidth, Main.buffScanAreaHeight);
        tileRectangle = WorldUtils.ClampToWorld(tileRectangle);
        for (int left = tileRectangle.Left; left < tileRectangle.Right; ++left)
        {
          for (int top = tileRectangle.Top; top < tileRectangle.Bottom; ++top)
          {
            if (tileRectangle.Contains(left, top))
            {
              Tile tile = Main.tile[left, top];
              if (tile != null)
              {
                if (!tile.active())
                {
                  if (tile.liquid > (byte) 0)
                    ++this._liquidCounts[(int) tile.liquidType()];
                }
                else
                {
                  tileRectangle.Contains(left, top);
                  if (!TileID.Sets.isDesertBiomeSand[(int) tile.type] || !WorldGen.oceanDepths(left, top))
                    ++this._tileCounts[(int) tile.type];
                  if (tile.type == (ushort) 215 && tile.frameY < (short) 36)
                    this.HasCampfire = true;
                  if (tile.type == (ushort) 49 && tile.frameX < (short) 18)
                    ++num1;
                  if (tile.type == (ushort) 372 && tile.frameX < (short) 18)
                    ++num2;
                  if (tile.type == (ushort) 646 && tile.frameX < (short) 18)
                    ++num3;
                  if (tile.type == (ushort) 405 && tile.frameX < (short) 54)
                    this.HasCampfire = true;
                  if (tile.type == (ushort) 506 && tile.frameX < (short) 72)
                    this.HasCatBast = true;
                  if (tile.type == (ushort) 42 && tile.frameY >= (short) 324 && tile.frameY <= (short) 358)
                    this.HasHeartLantern = true;
                  if (tile.type == (ushort) 42 && tile.frameY >= (short) 252 && tile.frameY <= (short) 286)
                    this.HasStarInBottle = true;
                  if (tile.type == (ushort) 91 && (tile.frameX >= (short) 396 || tile.frameY >= (short) 54))
                  {
                    int banner = (int) tile.frameX / 18 - 21;
                    for (int frameY = (int) tile.frameY; frameY >= 54; frameY -= 54)
                      banner = banner + 90 + 21;
                    int index = Item.BannerToItem(banner);
                    if (ItemID.Sets.BannerStrength.IndexInRange<ItemID.BannerEffect>(index) && ItemID.Sets.BannerStrength[index].Enabled)
                    {
                      this.NPCBannerBuff[banner] = true;
                      this.hasBanner = true;
                    }
                  }
                  if (settings.ScanOreFinderData && Main.tileOreFinderPriority[(int) tile.type] != (short) 0)
                    this._oreFinderTileLocations.Add(new Point(left, top));
                }
              }
            }
          }
        }
      }
      if (settings.VisualScanArea.HasValue)
      {
        Microsoft.Xna.Framework.Rectangle world = WorldUtils.ClampToWorld(settings.VisualScanArea.Value);
        for (int left = world.Left; left < world.Right; ++left)
        {
          for (int top = world.Top; top < world.Bottom; ++top)
          {
            Tile tile = Main.tile[left, top];
            if (tile != null && tile.active())
            {
              if (tile.type == (ushort) 104)
                this.HasClock = true;
              switch (tile.type)
              {
                case 139:
                  if (tile.frameX >= (short) 36)
                  {
                    this.ActiveMusicBox = (int) tile.frameY / 36;
                    continue;
                  }
                  continue;
                case 207:
                  if (tile.frameY >= (short) 72)
                  {
                    switch ((int) tile.frameX / 36)
                    {
                      case 0:
                        this.ActiveFountainColor = 0;
                        continue;
                      case 1:
                        this.ActiveFountainColor = 12;
                        continue;
                      case 2:
                        this.ActiveFountainColor = 3;
                        continue;
                      case 3:
                        this.ActiveFountainColor = 5;
                        continue;
                      case 4:
                        this.ActiveFountainColor = 2;
                        continue;
                      case 5:
                        this.ActiveFountainColor = 10;
                        continue;
                      case 6:
                        this.ActiveFountainColor = 4;
                        continue;
                      case 7:
                        this.ActiveFountainColor = 9;
                        continue;
                      case 8:
                        this.ActiveFountainColor = 8;
                        continue;
                      case 9:
                        this.ActiveFountainColor = 6;
                        continue;
                      default:
                        this.ActiveFountainColor = -1;
                        continue;
                    }
                  }
                  else
                    continue;
                case 410:
                  if (tile.frameY >= (short) 56)
                  {
                    this.ActiveMonolithType = (int) tile.frameX / 36;
                    continue;
                  }
                  continue;
                case 480:
                  if (tile.frameY >= (short) 54)
                  {
                    this.BloodMoonMonolith = true;
                    continue;
                  }
                  continue;
                case 509:
                  if (tile.frameY >= (short) 56)
                  {
                    this.ActiveMonolithType = 4;
                    continue;
                  }
                  continue;
                case 657:
                  if (tile.frameY >= (short) 54)
                  {
                    this.EchoMonolith = true;
                    continue;
                  }
                  continue;
                case 658:
                  this.ShimmerMonolithState = (int) tile.frameY / 54;
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
      this.WaterCandleCount = num1;
      this.PeaceCandleCount = num2;
      this.ShadowCandleCount = num3;
      this.ExportTileCountsToMain();
      this.CanPlayCreditsRoll = this.ActiveMusicBox == 85;
      if (!settings.ScanOreFinderData)
        return;
      this.UpdateOreFinderData();
    }

    private void ExportTileCountsToMain()
    {
      if (this._tileCounts[27] > 0)
        this.HasSunflower = true;
      if (this._tileCounts[567] > 0)
        this.HasGardenGnome = true;
      this.ShimmerTileCount = this._liquidCounts[3];
      this.HoneyBlockCount = this._tileCounts[229];
      this.HolyTileCount = this._tileCounts[109] + this._tileCounts[492] + this._tileCounts[110] + this._tileCounts[113] + this._tileCounts[117] + this._tileCounts[116] + this._tileCounts[164] + this._tileCounts[403] + this._tileCounts[402];
      this.SnowTileCount = this._tileCounts[147] + this._tileCounts[148] + this._tileCounts[161] + this._tileCounts[162] + this._tileCounts[164] + this._tileCounts[163] + this._tileCounts[200];
      if (Main.remixWorld)
      {
        this.JungleTileCount = this._tileCounts[60] + this._tileCounts[61] + this._tileCounts[62] + this._tileCounts[74] + this._tileCounts[225];
        this.EvilTileCount = this._tileCounts[23] + this._tileCounts[661] + this._tileCounts[24] + this._tileCounts[25] + this._tileCounts[32] + this._tileCounts[112] + this._tileCounts[163] + this._tileCounts[400] + this._tileCounts[398] + -10 * this._tileCounts[27] + this._tileCounts[474];
        this.BloodTileCount = this._tileCounts[199] + this._tileCounts[662] + this._tileCounts[203] + this._tileCounts[200] + this._tileCounts[401] + this._tileCounts[399] + this._tileCounts[234] + this._tileCounts[352] - 10 * this._tileCounts[27] + this._tileCounts[195];
      }
      else
      {
        this.JungleTileCount = this._tileCounts[60] + this._tileCounts[61] + this._tileCounts[62] + this._tileCounts[74] + this._tileCounts[226] + this._tileCounts[225];
        this.EvilTileCount = this._tileCounts[23] + this._tileCounts[661] + this._tileCounts[24] + this._tileCounts[25] + this._tileCounts[32] + this._tileCounts[112] + this._tileCounts[163] + this._tileCounts[400] + this._tileCounts[398] + -10 * this._tileCounts[27];
        this.BloodTileCount = this._tileCounts[199] + this._tileCounts[662] + this._tileCounts[203] + this._tileCounts[200] + this._tileCounts[401] + this._tileCounts[399] + this._tileCounts[234] + this._tileCounts[352] - 10 * this._tileCounts[27];
      }
      this.MushroomTileCount = this._tileCounts[70] + this._tileCounts[71] + this._tileCounts[72] + this._tileCounts[528];
      this.MeteorTileCount = this._tileCounts[37];
      this.DungeonTileCount = this._tileCounts[41] + this._tileCounts[43] + this._tileCounts[44] + this._tileCounts[481] + this._tileCounts[482] + this._tileCounts[483];
      this.SandTileCount = this._tileCounts[53] + this._tileCounts[112] + this._tileCounts[116] + this._tileCounts[234] + this._tileCounts[397] + this._tileCounts[398] + this._tileCounts[402] + this._tileCounts[399] + this._tileCounts[396] + this._tileCounts[400] + this._tileCounts[403] + this._tileCounts[401];
      this.PartyMonolithCount = this._tileCounts[455];
      this.GraveyardTileCount = this._tileCounts[85];
      this.GraveyardTileCount -= this._tileCounts[27] / 2;
      if (this._tileCounts[27] > 0)
        this.HasSunflower = true;
      if (this.GraveyardTileCount > SceneMetrics.GraveyardTileMin)
        this.HasSunflower = false;
      if (this.GraveyardTileCount < 0)
        this.GraveyardTileCount = 0;
      if (this.HolyTileCount < 0)
        this.HolyTileCount = 0;
      if (this.EvilTileCount < 0)
        this.EvilTileCount = 0;
      if (this.BloodTileCount < 0)
        this.BloodTileCount = 0;
      int holyTileCount = this.HolyTileCount;
      this.HolyTileCount -= this.EvilTileCount;
      this.HolyTileCount -= this.BloodTileCount;
      this.EvilTileCount -= holyTileCount;
      this.BloodTileCount -= holyTileCount;
      if (this.HolyTileCount < 0)
        this.HolyTileCount = 0;
      if (this.EvilTileCount < 0)
        this.EvilTileCount = 0;
      if (this.BloodTileCount >= 0)
        return;
      this.BloodTileCount = 0;
    }

    public int GetTileCount(ushort tileId) => this._tileCounts[(int) tileId];

    public void Reset()
    {
      Array.Clear((Array) this._tileCounts, 0, this._tileCounts.Length);
      Array.Clear((Array) this._liquidCounts, 0, this._liquidCounts.Length);
      this.SandTileCount = 0;
      this.EvilTileCount = 0;
      this.BloodTileCount = 0;
      this.GraveyardTileCount = 0;
      this.MushroomTileCount = 0;
      this.SnowTileCount = 0;
      this.HolyTileCount = 0;
      this.MeteorTileCount = 0;
      this.JungleTileCount = 0;
      this.DungeonTileCount = 0;
      this.HasCampfire = false;
      this.HasSunflower = false;
      this.HasGardenGnome = false;
      this.HasStarInBottle = false;
      this.HasHeartLantern = false;
      this.HasClock = false;
      this.HasCatBast = false;
      this.ActiveMusicBox = -1;
      this.WaterCandleCount = 0;
      this.ActiveFountainColor = -1;
      this.ActiveMonolithType = -1;
      this.bestOre = -1;
      this.BloodMoonMonolith = false;
      this.MoonLordMonolith = false;
      this.EchoMonolith = false;
      this.ShimmerMonolithState = 0;
      Array.Clear((Array) this.NPCBannerBuff, 0, this.NPCBannerBuff.Length);
      this.hasBanner = false;
      this.CanPlayCreditsRoll = false;
    }

    private void UpdateOreFinderData()
    {
      int index = -1;
      foreach (Point finderTileLocation in this._oreFinderTileLocations)
      {
        Tile t = Main.tile[finderTileLocation.X, finderTileLocation.Y];
        if (SceneMetrics.IsValidForOreFinder(t) && (index < 0 || (int) Main.tileOreFinderPriority[(int) t.type] > (int) Main.tileOreFinderPriority[index]))
        {
          index = (int) t.type;
          this.ClosestOrePosition = new Point?(finderTileLocation);
        }
      }
      this.bestOre = index;
    }

    public static bool IsValidForOreFinder(Tile t) => (t.type != (ushort) 227 || t.frameX >= (short) 272 && t.frameX <= (short) 374) && (t.type != (ushort) 129 || t.frameX >= (short) 324) && Main.tileOreFinderPriority[(int) t.type] > (short) 0;
  }
}
