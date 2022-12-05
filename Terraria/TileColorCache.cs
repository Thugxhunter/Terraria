// Decompiled with JetBrains decompiler
// Type: Terraria.TileColorCache
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria
{
  public struct TileColorCache
  {
    public byte Color;
    public bool FullBright;
    public bool Invisible;

    public void ApplyToBlock(Tile tile)
    {
      tile.color(this.Color);
      tile.fullbrightBlock(this.FullBright);
      tile.invisibleBlock(this.Invisible);
    }

    public void ApplyToWall(Tile tile)
    {
      tile.wallColor(this.Color);
      tile.fullbrightWall(this.FullBright);
      tile.invisibleWall(this.Invisible);
    }
  }
}
