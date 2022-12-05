// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ItemDropRules.SlimeBodyItemDropRule
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;
using Terraria.ID;

namespace Terraria.GameContent.ItemDropRules
{
  public class SlimeBodyItemDropRule : IItemDropRule
  {
    public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

    public SlimeBodyItemDropRule() => this.ChainedRules = new List<IItemDropRuleChainAttempt>();

    public bool CanDrop(DropAttemptInfo info) => info.npc.type == 1 && (double) info.npc.ai[1] > 0.0 && (double) info.npc.ai[1] < (double) ItemID.Count;

    public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
    {
      int itemId = (int) info.npc.ai[1];
      int amountDroppedMinimum;
      int amountDroppedMaximum;
      this.GetDropInfo(itemId, out amountDroppedMinimum, out amountDroppedMaximum);
      CommonCode.DropItemFromNPC(info.npc, itemId, info.rng.Next(amountDroppedMinimum, amountDroppedMaximum + 1));
      return new ItemDropAttemptResult()
      {
        State = ItemDropAttemptResultState.Success
      };
    }

    public void GetDropInfo(int itemId, out int amountDroppedMinimum, out int amountDroppedMaximum)
    {
      amountDroppedMinimum = 1;
      amountDroppedMaximum = 1;
      switch (itemId)
      {
        case 8:
          amountDroppedMinimum = 5;
          amountDroppedMaximum = 10;
          break;
        case 11:
        case 12:
        case 13:
        case 14:
        case 699:
        case 700:
        case 701:
        case 702:
          amountDroppedMinimum = 3;
          amountDroppedMaximum = 13;
          break;
        case 71:
          amountDroppedMinimum = 50;
          amountDroppedMaximum = 99;
          break;
        case 72:
          amountDroppedMinimum = 20;
          amountDroppedMaximum = 99;
          break;
        case 73:
          amountDroppedMinimum = 1;
          amountDroppedMaximum = 2;
          break;
        case 166:
          amountDroppedMinimum = 2;
          amountDroppedMaximum = 6;
          break;
        case 965:
          amountDroppedMinimum = 20;
          amountDroppedMaximum = 45;
          break;
        case 4343:
        case 4344:
          amountDroppedMinimum = 2;
          amountDroppedMaximum = 5;
          break;
      }
    }

    public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo) => Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
  }
}
