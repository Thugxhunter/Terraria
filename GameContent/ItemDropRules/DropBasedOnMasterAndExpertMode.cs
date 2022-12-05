// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ItemDropRules.DropBasedOnMasterAndExpertMode
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
  public class DropBasedOnMasterAndExpertMode : IItemDropRule, INestedItemDropRule
  {
    public IItemDropRule ruleForDefault;
    public IItemDropRule ruleForExpertmode;
    public IItemDropRule ruleForMasterMode;

    public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

    public DropBasedOnMasterAndExpertMode(
      IItemDropRule ruleForDefault,
      IItemDropRule ruleForExpertMode,
      IItemDropRule ruleForMasterMode)
    {
      this.ruleForDefault = ruleForDefault;
      this.ruleForExpertmode = ruleForExpertMode;
      this.ruleForMasterMode = ruleForMasterMode;
      this.ChainedRules = new List<IItemDropRuleChainAttempt>();
    }

    public bool CanDrop(DropAttemptInfo info)
    {
      if (info.IsMasterMode)
        return this.ruleForMasterMode.CanDrop(info);
      return info.IsExpertMode ? this.ruleForExpertmode.CanDrop(info) : this.ruleForDefault.CanDrop(info);
    }

    public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info) => new ItemDropAttemptResult()
    {
      State = ItemDropAttemptResultState.DidNotRunCode
    };

    public ItemDropAttemptResult TryDroppingItem(
      DropAttemptInfo info,
      ItemDropRuleResolveAction resolveAction)
    {
      if (info.IsMasterMode)
        return resolveAction(this.ruleForMasterMode, info);
      return info.IsExpertMode ? resolveAction(this.ruleForExpertmode, info) : resolveAction(this.ruleForDefault, info);
    }

    public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
    {
      DropRateInfoChainFeed ratesInfo1 = ratesInfo.With(1f);
      ratesInfo1.AddCondition((IItemDropRuleCondition) new Conditions.IsMasterMode());
      this.ruleForMasterMode.ReportDroprates(drops, ratesInfo1);
      DropRateInfoChainFeed ratesInfo2 = ratesInfo.With(1f);
      ratesInfo2.AddCondition((IItemDropRuleCondition) new Conditions.NotMasterMode());
      ratesInfo2.AddCondition((IItemDropRuleCondition) new Conditions.IsExpert());
      this.ruleForExpertmode.ReportDroprates(drops, ratesInfo2);
      DropRateInfoChainFeed ratesInfo3 = ratesInfo.With(1f);
      ratesInfo3.AddCondition((IItemDropRuleCondition) new Conditions.NotMasterMode());
      ratesInfo3.AddCondition((IItemDropRuleCondition) new Conditions.NotExpert());
      this.ruleForDefault.ReportDroprates(drops, ratesInfo3);
      Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
    }
  }
}
