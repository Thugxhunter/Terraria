// Decompiled with JetBrains decompiler
// Type: Terraria.RecipeGroup
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using System.Collections.Generic;

namespace Terraria
{
  public class RecipeGroup
  {
    public Func<string> GetText;
    public HashSet<int> ValidItems;
    public int IconicItemId;
    public int RegisteredId;
    public static Dictionary<int, RecipeGroup> recipeGroups = new Dictionary<int, RecipeGroup>();
    public static Dictionary<string, int> recipeGroupIDs = new Dictionary<string, int>();
    public static int nextRecipeGroupIndex;

    public RecipeGroup(Func<string> getName, params int[] validItems)
    {
      this.GetText = getName;
      this.ValidItems = new HashSet<int>((IEnumerable<int>) validItems);
      this.IconicItemId = validItems[0];
    }

    public static int RegisterGroup(string name, RecipeGroup rec)
    {
      int key = RecipeGroup.nextRecipeGroupIndex++;
      rec.RegisteredId = key;
      RecipeGroup.recipeGroups.Add(key, rec);
      RecipeGroup.recipeGroupIDs.Add(name, key);
      return key;
    }

    public int CountUsableItems(Dictionary<int, int> itemStacksAvailable)
    {
      int num1 = 0;
      foreach (int validItem in this.ValidItems)
      {
        int num2;
        if (itemStacksAvailable.TryGetValue(validItem, out num2))
          num1 += num2;
      }
      return num1;
    }

    public int GetGroupFakeItemId() => this.RegisteredId + 1000000;
  }
}
