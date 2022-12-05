// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.EntitySource_OverfullChest
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.DataStructures
{
  public class EntitySource_OverfullChest : AEntitySource_Tile
  {
    public readonly Chest Chest;

    public EntitySource_OverfullChest(int tileCoordsX, int tileCoordsY, Chest chest)
      : base(tileCoordsX, tileCoordsY)
    {
      this.Chest = chest;
    }
  }
}
