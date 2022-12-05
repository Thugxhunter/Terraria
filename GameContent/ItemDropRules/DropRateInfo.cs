// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ItemDropRules.DropRateInfo
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
  public struct DropRateInfo
  {
    public int itemId;
    public int stackMin;
    public int stackMax;
    public float dropRate;
    public List<IItemDropRuleCondition> conditions;

    public DropRateInfo(
      int itemId,
      int stackMin,
      int stackMax,
      float dropRate,
      List<IItemDropRuleCondition> conditions = null)
    {
      this.itemId = itemId;
      this.stackMin = stackMin;
      this.stackMax = stackMax;
      this.dropRate = dropRate;
      this.conditions = (List<IItemDropRuleCondition>) null;
      if (conditions == null || conditions.Count <= 0)
        return;
      this.conditions = new List<IItemDropRuleCondition>((IEnumerable<IItemDropRuleCondition>) conditions);
    }

    public void AddCondition(IItemDropRuleCondition condition)
    {
      if (this.conditions == null)
        this.conditions = new List<IItemDropRuleCondition>();
      this.conditions.Add(condition);
    }
  }
}
