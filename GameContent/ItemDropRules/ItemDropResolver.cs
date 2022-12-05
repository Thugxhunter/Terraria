// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ItemDropRules.ItemDropResolver
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
  public class ItemDropResolver
  {
    private ItemDropDatabase _database;

    public ItemDropResolver(ItemDropDatabase database) => this._database = database;

    public void TryDropping(DropAttemptInfo info)
    {
      List<IItemDropRule> rulesForNpcid = this._database.GetRulesForNPCID(info.npc.netID);
      for (int index = 0; index < rulesForNpcid.Count; ++index)
        this.ResolveRule(rulesForNpcid[index], info);
    }

    private ItemDropAttemptResult ResolveRule(
      IItemDropRule rule,
      DropAttemptInfo info)
    {
      if (!rule.CanDrop(info))
      {
        ItemDropAttemptResult parentResult = new ItemDropAttemptResult()
        {
          State = ItemDropAttemptResultState.DoesntFillConditions
        };
        this.ResolveRuleChains(rule, info, parentResult);
        return parentResult;
      }
      ItemDropAttemptResult parentResult1 = !(rule is INestedItemDropRule nestedItemDropRule) ? rule.TryDroppingItem(info) : nestedItemDropRule.TryDroppingItem(info, new ItemDropRuleResolveAction(this.ResolveRule));
      this.ResolveRuleChains(rule, info, parentResult1);
      return parentResult1;
    }

    private void ResolveRuleChains(
      IItemDropRule rule,
      DropAttemptInfo info,
      ItemDropAttemptResult parentResult)
    {
      this.ResolveRuleChains(ref info, ref parentResult, rule.ChainedRules);
    }

    private void ResolveRuleChains(
      ref DropAttemptInfo info,
      ref ItemDropAttemptResult parentResult,
      List<IItemDropRuleChainAttempt> ruleChains)
    {
      if (ruleChains == null)
        return;
      for (int index = 0; index < ruleChains.Count; ++index)
      {
        IItemDropRuleChainAttempt ruleChain = ruleChains[index];
        if (ruleChain.CanChainIntoRule(parentResult))
          this.ResolveRule(ruleChain.RuleToChain, info);
      }
    }
  }
}
