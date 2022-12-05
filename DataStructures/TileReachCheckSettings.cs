// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.TileReachCheckSettings
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.DataStructures
{
  public struct TileReachCheckSettings
  {
    public int TileRangeMultiplier;
    public int? TileReachLimit;
    public int? OverrideXReach;
    public int? OverrideYReach;

    public void GetRanges(Player player, out int x, out int y)
    {
      x = Player.tileRangeX * this.TileRangeMultiplier;
      y = Player.tileRangeY * this.TileRangeMultiplier;
      int? tileReachLimit = this.TileReachLimit;
      if (tileReachLimit.HasValue)
      {
        int num = tileReachLimit.Value;
        if (x > num)
          x = num;
        if (y > num)
          y = num;
      }
      if (this.OverrideXReach.HasValue)
        x = this.OverrideXReach.Value;
      if (!this.OverrideYReach.HasValue)
        return;
      y = this.OverrideYReach.Value;
    }

    public static TileReachCheckSettings Simple => new TileReachCheckSettings()
    {
      TileRangeMultiplier = 1,
      TileReachLimit = new int?(20)
    };

    public static TileReachCheckSettings Pylons => new TileReachCheckSettings()
    {
      OverrideXReach = new int?(60),
      OverrideYReach = new int?(60)
    };

    public static TileReachCheckSettings QuickStackToNearbyChests => new TileReachCheckSettings()
    {
      OverrideXReach = new int?(39),
      OverrideYReach = new int?(39)
    };
  }
}
