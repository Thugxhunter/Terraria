// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ItemDropRules.Chains
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
  public static class Chains
  {
    public static void ReportDroprates(
      List<IItemDropRuleChainAttempt> ChainedRules,
      float personalDropRate,
      List<DropRateInfo> drops,
      DropRateInfoChainFeed ratesInfo)
    {
      foreach (IItemDropRuleChainAttempt chainedRule in ChainedRules)
        chainedRule.ReportDroprates(personalDropRate, drops, ratesInfo);
    }

    public static IItemDropRule OnFailedRoll(
      this IItemDropRule rule,
      IItemDropRule ruleToChain,
      bool hideLootReport = false)
    {
      rule.ChainedRules.Add((IItemDropRuleChainAttempt) new Chains.TryIfFailedRandomRoll(ruleToChain, hideLootReport));
      return ruleToChain;
    }

    public static IItemDropRule OnSuccess(
      this IItemDropRule rule,
      IItemDropRule ruleToChain,
      bool hideLootReport = false)
    {
      rule.ChainedRules.Add((IItemDropRuleChainAttempt) new Chains.TryIfSucceeded(ruleToChain, hideLootReport));
      return ruleToChain;
    }

    public static IItemDropRule OnFailedConditions(
      this IItemDropRule rule,
      IItemDropRule ruleToChain,
      bool hideLootReport = false)
    {
      rule.ChainedRules.Add((IItemDropRuleChainAttempt) new Chains.TryIfDoesntFillConditions(ruleToChain, hideLootReport));
      return ruleToChain;
    }

    public class TryIfFailedRandomRoll : IItemDropRuleChainAttempt
    {
      public bool hideLootReport;

      public IItemDropRule RuleToChain { get; private set; }

      public TryIfFailedRandomRoll(IItemDropRule rule, bool hideLootReport = false)
      {
        this.RuleToChain = rule;
        this.hideLootReport = hideLootReport;
      }

      public bool CanChainIntoRule(ItemDropAttemptResult parentResult) => parentResult.State == ItemDropAttemptResultState.FailedRandomRoll;

      public void ReportDroprates(
        float personalDropRate,
        List<DropRateInfo> drops,
        DropRateInfoChainFeed ratesInfo)
      {
        if (this.hideLootReport)
          return;
        this.RuleToChain.ReportDroprates(drops, ratesInfo.With(1f - personalDropRate));
      }
    }

    public class TryIfSucceeded : IItemDropRuleChainAttempt
    {
      public bool hideLootReport;

      public IItemDropRule RuleToChain { get; private set; }

      public TryIfSucceeded(IItemDropRule rule, bool hideLootReport = false)
      {
        this.RuleToChain = rule;
        this.hideLootReport = hideLootReport;
      }

      public bool CanChainIntoRule(ItemDropAttemptResult parentResult) => parentResult.State == ItemDropAttemptResultState.Success;

      public void ReportDroprates(
        float personalDropRate,
        List<DropRateInfo> drops,
        DropRateInfoChainFeed ratesInfo)
      {
        if (this.hideLootReport)
          return;
        this.RuleToChain.ReportDroprates(drops, ratesInfo.With(personalDropRate));
      }
    }

    public class TryIfDoesntFillConditions : IItemDropRuleChainAttempt
    {
      public bool hideLootReport;

      public IItemDropRule RuleToChain { get; private set; }

      public TryIfDoesntFillConditions(IItemDropRule rule, bool hideLootReport = false)
      {
        this.RuleToChain = rule;
        this.hideLootReport = hideLootReport;
      }

      public bool CanChainIntoRule(ItemDropAttemptResult parentResult) => parentResult.State == ItemDropAttemptResultState.DoesntFillConditions;

      public void ReportDroprates(
        float personalDropRate,
        List<DropRateInfo> drops,
        DropRateInfoChainFeed ratesInfo)
      {
        if (this.hideLootReport)
          return;
        this.RuleToChain.ReportDroprates(drops, ratesInfo.With(personalDropRate));
      }
    }
  }
}
