// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.EntitySource_Mount
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.DataStructures
{
  public class EntitySource_Mount : IEntitySource
  {
    public readonly Entity Entity;
    public readonly int MountId;

    public EntitySource_Mount(Entity entity, int mountId)
    {
      this.Entity = entity;
      this.MountId = mountId;
    }
  }
}
