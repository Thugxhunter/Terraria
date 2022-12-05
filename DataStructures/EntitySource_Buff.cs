// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.EntitySource_Buff
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.DataStructures
{
  public class EntitySource_Buff : IEntitySource
  {
    public readonly Entity Entity;
    public readonly int BuffId;
    public readonly int BuffIndex;

    public EntitySource_Buff(Entity entity, int buffId, int buffIndex)
    {
      this.Entity = entity;
      this.BuffId = buffId;
      this.BuffIndex = buffIndex;
    }
  }
}
