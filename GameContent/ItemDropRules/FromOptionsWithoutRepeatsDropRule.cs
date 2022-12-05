// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ItemDropRules.FromOptionsWithoutRepeatsDropRule
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
  public class FromOptionsWithoutRepeatsDropRule : IItemDropRule
  {
    public int[] dropIds;
    public int dropCount;
    private List<int> _temporaryAvailableItems = new List<int>();

    public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

    public FromOptionsWithoutRepeatsDropRule(int dropCount, params int[] options)
    {
      this.dropCount = dropCount;
      this.dropIds = options;
      this.ChainedRules = new List<IItemDropRuleChainAttempt>();
    }

    public bool CanDrop(DropAttemptInfo info) => true;

    public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
    {
      this._temporaryAvailableItems.Clear();
      this._temporaryAvailableItems.AddRange((IEnumerable<int>) this.dropIds);
      for (int index1 = 0; index1 < this.dropCount && this._temporaryAvailableItems.Count > 0; ++index1)
      {
        int index2 = info.rng.Next(this._temporaryAvailableItems.Count);
        CommonCode.DropItemFromNPC(info.npc, this._temporaryAvailableItems[index2], 1);
        this._temporaryAvailableItems.RemoveAt(index2);
      }
      return new ItemDropAttemptResult()
      {
        State = ItemDropAttemptResultState.Success
      };
    }

    public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
    {
      float parentDroprateChance = ratesInfo.parentDroprateChance;
      int length = this.dropIds.Length;
      float num = 1f;
      for (int index = 0; index < this.dropCount && length > 0; --length)
      {
        num *= (float) (length - 1) / (float) length;
        ++index;
      }
      float dropRate = (1f - num) * parentDroprateChance;
      for (int index = 0; index < this.dropIds.Length; ++index)
        drops.Add(new DropRateInfo(this.dropIds[index], 1, 1, dropRate, ratesInfo.conditions));
      Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
    }
  }
}
