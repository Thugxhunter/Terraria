// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.AEntitySource_Tile
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
  public abstract class AEntitySource_Tile : IEntitySource
  {
    public readonly Point TileCoords;

    public AEntitySource_Tile(int tileCoordsX, int tileCoordsY) => this.TileCoords = new Point(tileCoordsX, tileCoordsY);
  }
}
