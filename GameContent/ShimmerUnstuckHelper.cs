// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ShimmerUnstuckHelper
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Terraria.GameContent.Drawing;

namespace Terraria.GameContent
{
  public struct ShimmerUnstuckHelper
  {
    public int TimeLeftUnstuck;
    public bool IndefiniteProtectionActive;

    public bool ShouldUnstuck => this.IndefiniteProtectionActive || this.TimeLeftUnstuck > 0;

    public void Update(Player player)
    {
      bool flag = !player.shimmering && !player.shimmerWet;
      if (flag)
        this.IndefiniteProtectionActive = false;
      if (this.TimeLeftUnstuck > 0 && !flag)
        this.StartUnstuck();
      if (this.IndefiniteProtectionActive || this.TimeLeftUnstuck <= 0)
        return;
      --this.TimeLeftUnstuck;
      if (this.TimeLeftUnstuck != 0)
        return;
      ParticleOrchestrator.BroadcastOrRequestParticleSpawn(ParticleOrchestraType.ShimmerTownNPC, new ParticleOrchestraSettings()
      {
        PositionInWorld = player.Bottom
      });
    }

    public void StartUnstuck()
    {
      this.IndefiniteProtectionActive = true;
      this.TimeLeftUnstuck = 120;
    }

    public void Clear()
    {
      this.IndefiniteProtectionActive = false;
      this.TimeLeftUnstuck = 0;
    }
  }
}
