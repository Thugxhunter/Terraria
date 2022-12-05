// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ItemDropRules.CommonDropWithRerolls
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
  public class CommonDropWithRerolls : CommonDrop
  {
    public int timesToRoll;

    public CommonDropWithRerolls(
      int itemId,
      int chanceDenominator,
      int amountDroppedMinimum,
      int amountDroppedMaximum,
      int rerolls)
      : base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum)
    {
      this.timesToRoll = rerolls + 1;
    }

    public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
    {
      bool flag = false;
      for (int index = 0; index < this.timesToRoll; ++index)
        flag = flag || info.player.RollLuck(this.chanceDenominator) < this.chanceNumerator;
      if (flag)
      {
        CommonCode.DropItemFromNPC(info.npc, this.itemId, info.rng.Next(this.amountDroppedMinimum, this.amountDroppedMaximum + 1));
        return new ItemDropAttemptResult()
        {
          State = ItemDropAttemptResultState.Success
        };
      }
      return new ItemDropAttemptResult()
      {
        State = ItemDropAttemptResultState.FailedRandomRoll
      };
    }

    public override void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
    {
      float num1 = 1f - (float) this.chanceNumerator / (float) this.chanceDenominator;
      float num2 = 1f;
      for (int index = 0; index < this.timesToRoll; ++index)
        num2 *= num1;
      float personalDropRate = 1f - num2;
      float dropRate = personalDropRate * ratesInfo.parentDroprateChance;
      drops.Add(new DropRateInfo(this.itemId, this.amountDroppedMinimum, this.amountDroppedMaximum, dropRate, ratesInfo.conditions));
      Chains.ReportDroprates(this.ChainedRules, personalDropRate, drops, ratesInfo);
    }
  }
}
