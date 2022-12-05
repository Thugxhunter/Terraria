// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ItemDropRules.DropAttemptInfo
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Terraria.Utilities;

namespace Terraria.GameContent.ItemDropRules
{
  public struct DropAttemptInfo
  {
    public NPC npc;
    public Player player;
    public UnifiedRandom rng;
    public bool IsInSimulation;
    public bool IsExpertMode;
    public bool IsMasterMode;
  }
}
