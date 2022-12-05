// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.EntitySource_ItemUse_WithAmmo
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.DataStructures
{
  public class EntitySource_ItemUse_WithAmmo : EntitySource_ItemUse
  {
    public readonly int AmmoItemIdUsed;

    public EntitySource_ItemUse_WithAmmo(Entity entity, Item item, int ammoItemIdUsed)
      : base(entity, item)
    {
      this.AmmoItemIdUsed = ammoItemIdUsed;
    }
  }
}
