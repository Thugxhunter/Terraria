// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.EntitySource_ItemOpen
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.DataStructures
{
  public class EntitySource_ItemOpen : IEntitySource
  {
    public readonly Entity Entity;
    public readonly int ItemType;

    public EntitySource_ItemOpen(Entity entity, int itemType)
    {
      this.Entity = entity;
      this.ItemType = itemType;
    }
  }
}
