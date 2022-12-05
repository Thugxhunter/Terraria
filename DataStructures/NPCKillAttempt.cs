// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.NPCKillAttempt
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.DataStructures
{
  public struct NPCKillAttempt
  {
    public readonly NPC npc;
    public readonly int netId;
    public readonly bool active;

    public NPCKillAttempt(NPC target)
    {
      this.npc = target;
      this.netId = target.netID;
      this.active = target.active;
    }

    public bool DidNPCDie() => !this.npc.active;

    public bool DidNPCDieOrTransform() => this.DidNPCDie() || this.npc.netID != this.netId;
  }
}
