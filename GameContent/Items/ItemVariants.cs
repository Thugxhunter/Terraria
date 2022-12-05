// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Items.ItemVariants
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.Items
{
  public static class ItemVariants
  {
    private static List<ItemVariants.VariantEntry>[] _variants = new List<ItemVariants.VariantEntry>[(int) ItemID.Count];
    public static ItemVariant StrongerVariant = new ItemVariant(NetworkText.FromKey("ItemVariant.Stronger"));
    public static ItemVariant WeakerVariant = new ItemVariant(NetworkText.FromKey("ItemVariant.Weaker"));
    public static ItemVariant RebalancedVariant = new ItemVariant(NetworkText.FromKey("ItemVariant.Rebalanced"));
    public static ItemVariant EnabledVariant = new ItemVariant(NetworkText.FromKey("ItemVariant.Enabled"));
    public static ItemVariant DisabledBossSummonVariant = new ItemVariant(NetworkText.FromKey("ItemVariant.DisabledBossSummon"));
    public static ItemVariantCondition RemixWorld = new ItemVariantCondition(NetworkText.FromKey("ItemVariantCondition.RemixWorld"), (ItemVariantCondition.Condition) (() => Main.remixWorld));
    public static ItemVariantCondition GetGoodWorld = new ItemVariantCondition(NetworkText.FromKey("ItemVariantCondition.GetGoodWorld"), (ItemVariantCondition.Condition) (() => Main.getGoodWorld));
    public static ItemVariantCondition EverythingWorld = new ItemVariantCondition(NetworkText.FromKey("ItemVariantCondition.EverythingWorld"), (ItemVariantCondition.Condition) (() => Main.getGoodWorld && Main.remixWorld));

    public static IEnumerable<ItemVariants.VariantEntry> GetVariants(
      int itemId)
    {
      return !ItemVariants._variants.IndexInRange<List<ItemVariants.VariantEntry>>(itemId) ? Enumerable.Empty<ItemVariants.VariantEntry>() : (IEnumerable<ItemVariants.VariantEntry>) ItemVariants._variants[itemId] ?? Enumerable.Empty<ItemVariants.VariantEntry>();
    }

    private static ItemVariants.VariantEntry GetEntry(int itemId, ItemVariant variant) => ItemVariants.GetVariants(itemId).SingleOrDefault<ItemVariants.VariantEntry>((Func<ItemVariants.VariantEntry, bool>) (v => v.Variant == variant));

    public static void AddVariant(
      int itemId,
      ItemVariant variant,
      params ItemVariantCondition[] conditions)
    {
      ItemVariants.VariantEntry variantEntry = ItemVariants.GetEntry(itemId, variant);
      if (variantEntry == null)
      {
        List<ItemVariants.VariantEntry> variantEntryList = ItemVariants._variants[itemId];
        if (variantEntryList == null)
          ItemVariants._variants[itemId] = variantEntryList = new List<ItemVariants.VariantEntry>();
        variantEntryList.Add(variantEntry = new ItemVariants.VariantEntry(variant));
      }
      variantEntry.AddConditions((IEnumerable<ItemVariantCondition>) conditions);
    }

    public static bool HasVariant(int itemId, ItemVariant variant) => ItemVariants.GetEntry(itemId, variant) != null;

    public static ItemVariant SelectVariant(int itemId)
    {
      if (!ItemVariants._variants.IndexInRange<List<ItemVariants.VariantEntry>>(itemId))
        return (ItemVariant) null;
      List<ItemVariants.VariantEntry> variant = ItemVariants._variants[itemId];
      if (variant == null)
        return (ItemVariant) null;
      foreach (ItemVariants.VariantEntry variantEntry in variant)
      {
        if (variantEntry.AnyConditionMet())
          return variantEntry.Variant;
      }
      return (ItemVariant) null;
    }

    static ItemVariants()
    {
      ItemVariants.AddVariant(112, ItemVariants.StrongerVariant, ItemVariants.RemixWorld);
      ItemVariants.AddVariant(157, ItemVariants.StrongerVariant, ItemVariants.RemixWorld);
      ItemVariants.AddVariant(1319, ItemVariants.StrongerVariant, ItemVariants.RemixWorld);
      ItemVariants.AddVariant(1325, ItemVariants.StrongerVariant, ItemVariants.RemixWorld);
      ItemVariants.AddVariant(2273, ItemVariants.StrongerVariant, ItemVariants.RemixWorld);
      ItemVariants.AddVariant(3069, ItemVariants.StrongerVariant, ItemVariants.RemixWorld);
      ItemVariants.AddVariant(5147, ItemVariants.StrongerVariant, ItemVariants.RemixWorld);
      ItemVariants.AddVariant(517, ItemVariants.WeakerVariant, ItemVariants.RemixWorld);
      ItemVariants.AddVariant(671, ItemVariants.WeakerVariant, ItemVariants.RemixWorld);
      ItemVariants.AddVariant(683, ItemVariants.WeakerVariant, ItemVariants.RemixWorld);
      ItemVariants.AddVariant(725, ItemVariants.WeakerVariant, ItemVariants.RemixWorld);
      ItemVariants.AddVariant(1314, ItemVariants.WeakerVariant, ItemVariants.RemixWorld);
      ItemVariants.AddVariant(2623, ItemVariants.WeakerVariant, ItemVariants.RemixWorld);
      ItemVariants.AddVariant(5279, ItemVariants.WeakerVariant, ItemVariants.RemixWorld);
      ItemVariants.AddVariant(5280, ItemVariants.WeakerVariant, ItemVariants.RemixWorld);
      ItemVariants.AddVariant(5281, ItemVariants.WeakerVariant, ItemVariants.RemixWorld);
      ItemVariants.AddVariant(5282, ItemVariants.WeakerVariant, ItemVariants.RemixWorld);
      ItemVariants.AddVariant(5283, ItemVariants.WeakerVariant, ItemVariants.RemixWorld);
      ItemVariants.AddVariant(5284, ItemVariants.WeakerVariant, ItemVariants.RemixWorld);
      ItemVariants.AddVariant(197, ItemVariants.RebalancedVariant, ItemVariants.GetGoodWorld);
      ItemVariants.AddVariant(4060, ItemVariants.RebalancedVariant, ItemVariants.GetGoodWorld);
      ItemVariants.AddVariant(556, ItemVariants.DisabledBossSummonVariant, ItemVariants.EverythingWorld);
      ItemVariants.AddVariant(557, ItemVariants.DisabledBossSummonVariant, ItemVariants.EverythingWorld);
      ItemVariants.AddVariant(544, ItemVariants.DisabledBossSummonVariant, ItemVariants.EverythingWorld);
      ItemVariants.AddVariant(5334, ItemVariants.EnabledVariant, ItemVariants.EverythingWorld);
    }

    public class VariantEntry
    {
      public readonly ItemVariant Variant;
      private readonly List<ItemVariantCondition> _conditions = new List<ItemVariantCondition>();

      public IEnumerable<ItemVariantCondition> Conditions => (IEnumerable<ItemVariantCondition>) this._conditions;

      public VariantEntry(ItemVariant variant) => this.Variant = variant;

      internal void AddConditions(IEnumerable<ItemVariantCondition> conditions) => this._conditions.AddRange(conditions);

      public bool AnyConditionMet() => this.Conditions.Any<ItemVariantCondition>((Func<ItemVariantCondition, bool>) (c => c.IsMet()));
    }
  }
}
