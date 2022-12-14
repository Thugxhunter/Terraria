// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Bestiary.BestiaryUnlockProgressReport
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.GameContent.Bestiary
{
  public struct BestiaryUnlockProgressReport
  {
    public int EntriesTotal;
    public float CompletionAmountTotal;

    public float CompletionPercent => this.EntriesTotal == 0 ? 1f : this.CompletionAmountTotal / (float) this.EntriesTotal;
  }
}
