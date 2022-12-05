// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.WingStats
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.DataStructures
{
  public struct WingStats
  {
    public static readonly WingStats Default;
    public int FlyTime;
    public float AccRunSpeedOverride;
    public float AccRunAccelerationMult;
    public bool HasDownHoverStats;
    public float DownHoverSpeedOverride;
    public float DownHoverAccelerationMult;

    public WingStats(
      int flyTime = 100,
      float flySpeedOverride = -1f,
      float accelerationMultiplier = 1f,
      bool hasHoldDownHoverFeatures = false,
      float hoverFlySpeedOverride = -1f,
      float hoverAccelerationMultiplier = 1f)
    {
      this.FlyTime = flyTime;
      this.AccRunSpeedOverride = flySpeedOverride;
      this.AccRunAccelerationMult = accelerationMultiplier;
      this.HasDownHoverStats = hasHoldDownHoverFeatures;
      this.DownHoverSpeedOverride = hoverFlySpeedOverride;
      this.DownHoverAccelerationMult = hoverAccelerationMultiplier;
    }

    public WingStats WithSpeedBoost(float multiplier) => new WingStats(this.FlyTime, this.AccRunSpeedOverride * multiplier, this.AccRunAccelerationMult, this.HasDownHoverStats, this.DownHoverSpeedOverride * multiplier, this.DownHoverAccelerationMult);
  }
}
