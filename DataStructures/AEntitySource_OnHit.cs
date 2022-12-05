// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.AEntitySource_OnHit
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.DataStructures
{
  public abstract class AEntitySource_OnHit : IEntitySource
  {
    public readonly Entity EntityStriking;
    public readonly Entity EntityStruck;

    public AEntitySource_OnHit(Entity entityStriking, Entity entityStruck)
    {
      this.EntityStriking = entityStriking;
      this.EntityStruck = entityStruck;
    }
  }
}
