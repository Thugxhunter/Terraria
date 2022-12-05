// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.EntitySource_OnHit_ByProjectileSourceID
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.DataStructures
{
  public class EntitySource_OnHit_ByProjectileSourceID : AEntitySource_OnHit
  {
    public readonly int SourceId;

    public EntitySource_OnHit_ByProjectileSourceID(
      Entity entityStriking,
      Entity entityStruck,
      int projectileSourceId)
      : base(entityStriking, entityStruck)
    {
      this.SourceId = projectileSourceId;
    }
  }
}
