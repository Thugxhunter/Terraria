// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.TileDataType
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;

namespace Terraria.DataStructures
{
  [Flags]
  public enum TileDataType
  {
    Tile = 1,
    TilePaint = 2,
    Wall = 4,
    WallPaint = 8,
    Liquid = 16, // 0x00000010
    Wiring = 32, // 0x00000020
    Actuator = 64, // 0x00000040
    Slope = 128, // 0x00000080
    All = Slope | Actuator | Wiring | Liquid | WallPaint | Wall | TilePaint | Tile, // 0x000000FF
  }
}
