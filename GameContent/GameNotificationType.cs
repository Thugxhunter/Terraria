// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.GameNotificationType
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;

namespace Terraria.GameContent
{
  [Flags]
  public enum GameNotificationType
  {
    None = 0,
    Damage = 1,
    SpawnOrDeath = 2,
    WorldGen = 4,
    All = WorldGen | SpawnOrDeath | Damage, // 0x00000007
  }
}
