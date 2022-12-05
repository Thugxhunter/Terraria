// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ShimmerTransforms
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using Terraria.ID;

namespace Terraria.GameContent
{
  public static class ShimmerTransforms
  {
    public static int GetDecraftingRecipeIndex(int type)
    {
      int num = ItemID.Sets.IsCrafted[type];
      if (num < 0)
        return -1;
      if (WorldGen.crimson && ItemID.Sets.IsCraftedCrimson[type] >= 0)
        return ItemID.Sets.IsCraftedCrimson[type];
      return !WorldGen.crimson && ItemID.Sets.IsCraftedCorruption[type] >= 0 ? ItemID.Sets.IsCraftedCorruption[type] : num;
    }

    public static bool IsItemTransformLocked(int type)
    {
      int decraftingRecipeIndex = ShimmerTransforms.GetDecraftingRecipeIndex(type);
      return decraftingRecipeIndex >= 0 && (!NPC.downedBoss3 && ShimmerTransforms.RecipeSets.PostSkeletron[decraftingRecipeIndex] || !NPC.downedGolemBoss && ShimmerTransforms.RecipeSets.PostGolem[decraftingRecipeIndex]);
    }

    public static void UpdateRecipeSets()
    {
      ShimmerTransforms.RecipeSets.PostSkeletron = Utils.MapArray<Recipe, bool>(Main.recipe, (Func<Recipe, bool>) (r => r.ContainsIngredient(154)));
      ShimmerTransforms.RecipeSets.PostGolem = Utils.MapArray<Recipe, bool>(Main.recipe, (Func<Recipe, bool>) (r => r.ContainsIngredient(1101)));
    }

    public static class RecipeSets
    {
      public static bool[] PostSkeletron;
      public static bool[] PostGolem;
    }
  }
}
