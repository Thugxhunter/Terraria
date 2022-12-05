// Decompiled with JetBrains decompiler
// Type: Terraria.Enums.AnchorType
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;

namespace Terraria.Enums
{
  [Flags]
  public enum AnchorType
  {
    None = 0,
    SolidTile = 1,
    SolidWithTop = 2,
    Table = 4,
    SolidSide = 8,
    Tree = 16, // 0x00000010
    AlternateTile = 32, // 0x00000020
    EmptyTile = 64, // 0x00000040
    SolidBottom = 128, // 0x00000080
    Platform = 256, // 0x00000100
    PlanterBox = 512, // 0x00000200
    PlatformNonHammered = 1024, // 0x00000400
  }
}
