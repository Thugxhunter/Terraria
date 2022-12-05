// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ItemDropRules.ItemDropRule
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.GameContent.ItemDropRules
{
  public class ItemDropRule
  {
    public static IItemDropRule Common(
      int itemId,
      int chanceDenominator = 1,
      int minimumDropped = 1,
      int maximumDropped = 1)
    {
      return (IItemDropRule) new CommonDrop(itemId, chanceDenominator, minimumDropped, maximumDropped);
    }

    public static IItemDropRule BossBag(int itemId) => (IItemDropRule) new DropBasedOnExpertMode(ItemDropRule.DropNothing(), (IItemDropRule) new DropLocalPerClientAndResetsNPCMoneyTo0(itemId, 1, 1, 1, (IItemDropRuleCondition) null));

    public static IItemDropRule BossBagByCondition(
      IItemDropRuleCondition condition,
      int itemId)
    {
      return (IItemDropRule) new DropBasedOnExpertMode(ItemDropRule.DropNothing(), (IItemDropRule) new DropLocalPerClientAndResetsNPCMoneyTo0(itemId, 1, 1, 1, condition));
    }

    public static IItemDropRule ExpertGetsRerolls(
      int itemId,
      int chanceDenominator,
      int expertRerolls)
    {
      return (IItemDropRule) new DropBasedOnExpertMode(ItemDropRule.WithRerolls(itemId, 0, chanceDenominator), ItemDropRule.WithRerolls(itemId, expertRerolls, chanceDenominator));
    }

    public static IItemDropRule MasterModeCommonDrop(int itemId) => ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.IsMasterMode(), itemId);

    public static IItemDropRule MasterModeDropOnAllPlayers(
      int itemId,
      int chanceDenominator = 1)
    {
      return (IItemDropRule) new DropBasedOnMasterMode(ItemDropRule.DropNothing(), (IItemDropRule) new DropPerPlayerOnThePlayer(itemId, chanceDenominator, 1, 1, (IItemDropRuleCondition) new Conditions.IsMasterMode()));
    }

    public static IItemDropRule WithRerolls(
      int itemId,
      int rerolls,
      int chanceDenominator = 1,
      int minimumDropped = 1,
      int maximumDropped = 1)
    {
      return (IItemDropRule) new CommonDropWithRerolls(itemId, chanceDenominator, minimumDropped, maximumDropped, rerolls);
    }

    public static IItemDropRule ByCondition(
      IItemDropRuleCondition condition,
      int itemId,
      int chanceDenominator = 1,
      int minimumDropped = 1,
      int maximumDropped = 1,
      int chanceNumerator = 1)
    {
      return (IItemDropRule) new ItemDropWithConditionRule(itemId, chanceDenominator, minimumDropped, maximumDropped, condition, chanceNumerator);
    }

    public static IItemDropRule NotScalingWithLuck(
      int itemId,
      int chanceDenominator = 1,
      int minimumDropped = 1,
      int maximumDropped = 1)
    {
      return (IItemDropRule) new CommonDropNotScalingWithLuck(itemId, chanceDenominator, minimumDropped, maximumDropped);
    }

    public static IItemDropRule OneFromOptionsNotScalingWithLuck(
      int chanceDenominator,
      params int[] options)
    {
      return (IItemDropRule) new OneFromOptionsNotScaledWithLuckDropRule(chanceDenominator, 1, options);
    }

    public static IItemDropRule OneFromOptionsNotScalingWithLuckWithX(
      int chanceDenominator,
      int chanceNumerator,
      params int[] options)
    {
      return (IItemDropRule) new OneFromOptionsNotScaledWithLuckDropRule(chanceDenominator, chanceNumerator, options);
    }

    public static IItemDropRule OneFromOptions(
      int chanceDenominator,
      params int[] options)
    {
      return (IItemDropRule) new OneFromOptionsDropRule(chanceDenominator, 1, options);
    }

    public static IItemDropRule OneFromOptionsWithNumerator(
      int chanceDenominator,
      int chanceNumerator,
      params int[] options)
    {
      return (IItemDropRule) new OneFromOptionsDropRule(chanceDenominator, chanceNumerator, options);
    }

    public static IItemDropRule DropNothing() => (IItemDropRule) new Terraria.GameContent.ItemDropRules.DropNothing();

    public static IItemDropRule NormalvsExpert(
      int itemId,
      int chanceDenominatorInNormal,
      int chanceDenominatorInExpert)
    {
      return (IItemDropRule) new DropBasedOnExpertMode(ItemDropRule.Common(itemId, chanceDenominatorInNormal), ItemDropRule.Common(itemId, chanceDenominatorInExpert));
    }

    public static IItemDropRule NormalvsExpertNotScalingWithLuck(
      int itemId,
      int chanceDenominatorInNormal,
      int chanceDenominatorInExpert)
    {
      return (IItemDropRule) new DropBasedOnExpertMode(ItemDropRule.NotScalingWithLuck(itemId, chanceDenominatorInNormal), ItemDropRule.NotScalingWithLuck(itemId, chanceDenominatorInExpert));
    }

    public static IItemDropRule NormalvsExpertOneFromOptionsNotScalingWithLuck(
      int chanceDenominatorInNormal,
      int chanceDenominatorInExpert,
      params int[] options)
    {
      return (IItemDropRule) new DropBasedOnExpertMode(ItemDropRule.OneFromOptionsNotScalingWithLuck(chanceDenominatorInNormal, options), ItemDropRule.OneFromOptionsNotScalingWithLuck(chanceDenominatorInExpert, options));
    }

    public static IItemDropRule NormalvsExpertOneFromOptions(
      int chanceDenominatorInNormal,
      int chanceDenominatorInExpert,
      params int[] options)
    {
      return (IItemDropRule) new DropBasedOnExpertMode(ItemDropRule.OneFromOptions(chanceDenominatorInNormal, options), ItemDropRule.OneFromOptions(chanceDenominatorInExpert, options));
    }

    public static IItemDropRule Food(
      int itemId,
      int chanceDenominator,
      int minimumDropped = 1,
      int maximumDropped = 1)
    {
      return (IItemDropRule) new ItemDropWithConditionRule(itemId, chanceDenominator, minimumDropped, maximumDropped, (IItemDropRuleCondition) new Conditions.NotFromStatue());
    }

    public static IItemDropRule StatusImmunityItem(int itemId, int dropsOutOfX) => ItemDropRule.ExpertGetsRerolls(itemId, dropsOutOfX, 1);
  }
}
