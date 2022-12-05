// Decompiled with JetBrains decompiler
// Type: Terraria.Recipe
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using System.Collections.Generic;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria
{
  public class Recipe
  {
    public static int maxRequirements = 15;
    public static int maxRecipes = 3000;
    public static int numRecipes;
    private static Recipe currentRecipe = new Recipe();
    public Item createItem = new Item();
    public Item[] requiredItem = new Item[Recipe.maxRequirements];
    public int[] requiredTile = new int[Recipe.maxRequirements];
    public int[] acceptedGroups = new int[Recipe.maxRequirements];
    private Recipe.RequiredItemEntry[] requiredItemQuickLookup = new Recipe.RequiredItemEntry[Recipe.maxRequirements];
    public List<Item> customShimmerResults;
    public bool needHoney;
    public bool needWater;
    public bool needLava;
    public bool anyWood;
    public bool anyIronBar;
    public bool anyPressurePlate;
    public bool anySand;
    public bool anyFragment;
    public bool alchemy;
    public bool needSnowBiome;
    public bool needGraveyardBiome;
    public bool needEverythingSeed;
    public bool notDecraftable;
    public bool crimson;
    public bool corruption;
    private static bool _hasDelayedFindRecipes;
    private static Dictionary<int, int> _ownedItems = new Dictionary<int, int>();

    public void RequireGroup(string name)
    {
      int num;
      if (!RecipeGroup.recipeGroupIDs.TryGetValue(name, out num))
        return;
      for (int index = 0; index < Recipe.maxRequirements; ++index)
      {
        if (this.acceptedGroups[index] == -1)
        {
          this.acceptedGroups[index] = num;
          break;
        }
      }
    }

    public void RequireGroup(int id)
    {
      for (int index = 0; index < Recipe.maxRequirements; ++index)
      {
        if (this.acceptedGroups[index] == -1)
        {
          this.acceptedGroups[index] = id;
          break;
        }
      }
    }

    public bool ProcessGroupsForText(int type, out string theText)
    {
      for (int index = 0; index < Recipe.maxRequirements; ++index)
      {
        int acceptedGroup = this.acceptedGroups[index];
        if (acceptedGroup != -1)
        {
          if (RecipeGroup.recipeGroups[acceptedGroup].ValidItems.Contains(type))
          {
            theText = RecipeGroup.recipeGroups[acceptedGroup].GetText();
            return true;
          }
        }
        else
          break;
      }
      theText = "";
      return false;
    }

    public bool AcceptsGroup(int groupId)
    {
      for (int index = 0; index < Recipe.maxRequirements; ++index)
      {
        int acceptedGroup = this.acceptedGroups[index];
        if (acceptedGroup != -1)
        {
          if (acceptedGroup == groupId)
            return true;
        }
        else
          break;
      }
      return false;
    }

    public bool AcceptedByItemGroups(int invType, int reqType)
    {
      for (int index = 0; index < Recipe.maxRequirements; ++index)
      {
        int acceptedGroup = this.acceptedGroups[index];
        if (acceptedGroup != -1)
        {
          if (RecipeGroup.recipeGroups[acceptedGroup].ValidItems.Contains(invType) && RecipeGroup.recipeGroups[acceptedGroup].ValidItems.Contains(reqType))
            return true;
        }
        else
          break;
      }
      return false;
    }

    public Item AddCustomShimmerResult(int itemType, int itemStack = 1)
    {
      if (this.customShimmerResults == null)
        this.customShimmerResults = new List<Item>();
      Item obj = new Item();
      obj.SetDefaults(itemType);
      obj.stack = itemStack;
      this.customShimmerResults.Add(obj);
      return obj;
    }

    public Recipe()
    {
      for (int index = 0; index < Recipe.maxRequirements; ++index)
      {
        this.requiredItem[index] = new Item();
        this.requiredTile[index] = -1;
        this.acceptedGroups[index] = -1;
      }
    }

    public void Create()
    {
      for (int index1 = 0; index1 < Recipe.maxRequirements; ++index1)
      {
        Item compareItem = this.requiredItem[index1];
        if (compareItem.type != 0)
        {
          int num1 = compareItem.stack;
          if (this.alchemy && Main.player[Main.myPlayer].alchemyTable)
          {
            if (num1 > 1)
            {
              int num2 = 0;
              for (int index2 = 0; index2 < num1; ++index2)
              {
                if (Main.rand.Next(3) == 0)
                  ++num2;
              }
              num1 -= num2;
            }
            else if (Main.rand.Next(3) == 0)
              num1 = 0;
          }
          if (num1 > 0)
          {
            Item[] inventory = Main.player[Main.myPlayer].inventory;
            for (int index3 = 0; index3 < 58; ++index3)
            {
              Item obj = inventory[index3];
              if (num1 > 0)
              {
                if (obj.IsTheSameAs(compareItem) || this.useWood(obj.type, compareItem.type) || this.useSand(obj.type, compareItem.type) || this.useFragment(obj.type, compareItem.type) || this.useIronBar(obj.type, compareItem.type) || this.usePressurePlate(obj.type, compareItem.type) || this.AcceptedByItemGroups(obj.type, compareItem.type))
                {
                  if (obj.stack > num1)
                  {
                    obj.stack -= num1;
                    num1 = 0;
                  }
                  else
                  {
                    num1 -= obj.stack;
                    inventory[index3] = new Item();
                  }
                }
              }
              else
                break;
            }
            if (Main.player[Main.myPlayer].chest != -1)
            {
              if (Main.player[Main.myPlayer].chest > -1)
                inventory = Main.chest[Main.player[Main.myPlayer].chest].item;
              else if (Main.player[Main.myPlayer].chest == -2)
                inventory = Main.player[Main.myPlayer].bank.item;
              else if (Main.player[Main.myPlayer].chest == -3)
                inventory = Main.player[Main.myPlayer].bank2.item;
              else if (Main.player[Main.myPlayer].chest == -4)
                inventory = Main.player[Main.myPlayer].bank3.item;
              else if (Main.player[Main.myPlayer].chest == -5)
                inventory = Main.player[Main.myPlayer].bank4.item;
              for (int number2 = 0; number2 < 40; ++number2)
              {
                Item obj = inventory[number2];
                if (num1 > 0)
                {
                  if (obj.IsTheSameAs(compareItem) || this.useWood(obj.type, compareItem.type) || this.useSand(obj.type, compareItem.type) || this.useIronBar(obj.type, compareItem.type) || this.usePressurePlate(obj.type, compareItem.type) || this.useFragment(obj.type, compareItem.type) || this.AcceptedByItemGroups(obj.type, compareItem.type))
                  {
                    if (obj.stack > num1)
                    {
                      obj.stack -= num1;
                      if (Main.netMode == 1 && Main.player[Main.myPlayer].chest >= 0)
                        NetMessage.SendData(32, number: Main.player[Main.myPlayer].chest, number2: ((float) number2));
                      num1 = 0;
                    }
                    else
                    {
                      num1 -= obj.stack;
                      inventory[number2] = new Item();
                      if (Main.netMode == 1 && Main.player[Main.myPlayer].chest >= 0)
                        NetMessage.SendData(32, number: Main.player[Main.myPlayer].chest, number2: ((float) number2));
                    }
                  }
                }
                else
                  break;
              }
            }
            if (Main.player[Main.myPlayer].useVoidBag() && Main.player[Main.myPlayer].chest != -5)
            {
              Item[] objArray = Main.player[Main.myPlayer].bank4.item;
              for (int number2 = 0; number2 < 40; ++number2)
              {
                Item obj = objArray[number2];
                if (num1 > 0)
                {
                  if (obj.IsTheSameAs(compareItem) || this.useWood(obj.type, compareItem.type) || this.useSand(obj.type, compareItem.type) || this.useIronBar(obj.type, compareItem.type) || this.usePressurePlate(obj.type, compareItem.type) || this.useFragment(obj.type, compareItem.type) || this.AcceptedByItemGroups(obj.type, compareItem.type))
                  {
                    if (obj.stack > num1)
                    {
                      obj.stack -= num1;
                      if (Main.netMode == 1 && Main.player[Main.myPlayer].chest >= 0)
                        NetMessage.SendData(32, number: Main.player[Main.myPlayer].chest, number2: ((float) number2));
                      num1 = 0;
                    }
                    else
                    {
                      num1 -= obj.stack;
                      objArray[number2] = new Item();
                      if (Main.netMode == 1 && Main.player[Main.myPlayer].chest >= 0)
                        NetMessage.SendData(32, number: Main.player[Main.myPlayer].chest, number2: ((float) number2));
                    }
                  }
                }
                else
                  break;
              }
            }
          }
        }
        else
          break;
      }
      AchievementsHelper.NotifyItemCraft(this);
      AchievementsHelper.NotifyItemPickup(Main.player[Main.myPlayer], this.createItem);
      Recipe.FindRecipes();
    }

    public bool useWood(int invType, int reqType)
    {
      if (!this.anyWood)
        return false;
      switch (reqType)
      {
        case 9:
        case 619:
        case 620:
        case 621:
        case 911:
        case 1729:
        case 2503:
        case 2504:
        case 5215:
          switch (invType)
          {
            case 9:
            case 619:
            case 620:
            case 621:
            case 911:
            case 1729:
            case 2503:
            case 2504:
            case 5215:
              return true;
            default:
              return false;
          }
        default:
          return false;
      }
    }

    public bool useIronBar(int invType, int reqType) => this.anyIronBar && (reqType == 22 || reqType == 704) && (invType == 22 || invType == 704);

    public bool useSand(int invType, int reqType) => (reqType == 169 || reqType == 408 || reqType == 1246 || reqType == 370 || reqType == 3272 || reqType == 3338 || reqType == 3274 || reqType == 3275) && this.anySand && (invType == 169 || invType == 408 || invType == 1246 || invType == 370 || invType == 3272 || invType == 3338 || invType == 3274 || invType == 3275);

    public bool useFragment(int invType, int reqType) => (reqType == 3458 || reqType == 3456 || reqType == 3457 || reqType == 3459) && this.anyFragment && (invType == 3458 || invType == 3456 || invType == 3457 || invType == 3459);

    public bool usePressurePlate(int invType, int reqType)
    {
      if (!this.anyPressurePlate)
        return false;
      switch (reqType)
      {
        case 529:
        case 541:
        case 542:
        case 543:
        case 852:
        case 853:
        case 1151:
        case 4261:
          switch (invType)
          {
            case 529:
            case 541:
            case 542:
            case 543:
            case 852:
            case 853:
            case 1151:
            case 4261:
              return true;
            default:
              return false;
          }
        default:
          return false;
      }
    }

    public static void GetThroughDelayedFindRecipes()
    {
      if (!Recipe._hasDelayedFindRecipes)
        return;
      Recipe._hasDelayedFindRecipes = false;
      Recipe.FindRecipes();
    }

    public static void FindRecipes(bool canDelayCheck = false)
    {
      if (canDelayCheck)
      {
        Recipe._hasDelayedFindRecipes = true;
      }
      else
      {
        int oldRecipe = Main.availableRecipe[Main.focusRecipe];
        float focusY = Main.availableRecipeY[Main.focusRecipe];
        Recipe.ClearAvailableRecipes();
        if ((Main.guideItem.IsAir ? 0 : (Main.guideItem.Name != "" ? 1 : 0)) != 0)
        {
          Recipe.CollectGuideRecipes();
          Recipe.TryRefocusingRecipe(oldRecipe);
          Recipe.VisuallyRepositionRecipes(focusY);
        }
        else
        {
          Player localPlayer = Main.LocalPlayer;
          Recipe.CollectItemsToCraftWithFrom(localPlayer);
          for (int recipeIndex = 0; recipeIndex < Recipe.maxRecipes; ++recipeIndex)
          {
            Recipe tempRec = Main.recipe[recipeIndex];
            if (tempRec.createItem.type != 0)
            {
              if (Recipe.PlayerMeetsTileRequirements(localPlayer, tempRec) && Recipe.PlayerMeetsEnvironmentConditions(localPlayer, tempRec) && Recipe.CollectedEnoughItemsToCraftRecipeNew(tempRec))
                Recipe.AddToAvailableRecipes(recipeIndex);
            }
            else
              break;
          }
          Recipe.TryRefocusingRecipe(oldRecipe);
          Recipe.VisuallyRepositionRecipes(focusY);
        }
      }
    }

    private static void AddToAvailableRecipes(int recipeIndex)
    {
      Main.availableRecipe[Main.numAvailableRecipes] = recipeIndex;
      ++Main.numAvailableRecipes;
    }

    public static bool CollectedEnoughItemsToCraftRecipeOld(Recipe tempRec)
    {
      for (int index = 0; index < Recipe.maxRequirements; ++index)
      {
        Item obj = tempRec.requiredItem[index];
        if (obj.type != 0)
        {
          int stack = obj.stack;
          bool flag = false;
          foreach (int key in Recipe._ownedItems.Keys)
          {
            if (tempRec.useWood(key, obj.type) || tempRec.useSand(key, obj.type) || tempRec.useIronBar(key, obj.type) || tempRec.useFragment(key, obj.type) || tempRec.usePressurePlate(key, obj.type) || tempRec.AcceptedByItemGroups(key, obj.type))
            {
              stack -= Recipe._ownedItems[key];
              flag = true;
            }
          }
          if (!flag && Recipe._ownedItems.ContainsKey(obj.netID))
            stack -= Recipe._ownedItems[obj.netID];
          if (stack > 0)
            return false;
        }
        else
          break;
      }
      return true;
    }

    public static bool CollectedEnoughItemsToCraftRecipeNew(Recipe tempRec)
    {
      for (int index = 0; index < Recipe.maxRequirements; ++index)
      {
        Recipe.RequiredItemEntry requiredItemEntry = tempRec.requiredItemQuickLookup[index];
        if (requiredItemEntry.itemIdOrRecipeGroup != 0)
        {
          int num;
          if (!Recipe._ownedItems.TryGetValue(requiredItemEntry.itemIdOrRecipeGroup, out num) || num < requiredItemEntry.stack)
            return false;
        }
        else
          break;
      }
      return true;
    }

    private static bool PlayerMeetsEnvironmentConditions(Player player, Recipe tempRec)
    {
      int num1 = !tempRec.needWater || player.adjWater ? 1 : (player.adjTile[172] ? 1 : 0);
      bool flag1 = !tempRec.needHoney || tempRec.needHoney == player.adjHoney;
      bool flag2 = !tempRec.needLava || tempRec.needLava == player.adjLava;
      bool flag3 = !tempRec.needSnowBiome || player.ZoneSnow;
      bool flag4 = !tempRec.needGraveyardBiome || player.ZoneGraveyard;
      bool flag5 = !tempRec.needEverythingSeed || Main.remixWorld && Main.getGoodWorld;
      int num2 = flag1 ? 1 : 0;
      return (num1 & num2 & (flag2 ? 1 : 0) & (flag3 ? 1 : 0) & (flag4 ? 1 : 0) & (flag5 ? 1 : 0)) != 0;
    }

    private static bool PlayerMeetsTileRequirements(Player player, Recipe tempRec)
    {
      for (int index = 0; index < Recipe.maxRequirements && tempRec.requiredTile[index] != -1; ++index)
      {
        if (!player.adjTile[tempRec.requiredTile[index]])
          return false;
      }
      return true;
    }

    private static void CollectItemsToCraftWithFrom(Player player)
    {
      Recipe._ownedItems.Clear();
      Recipe.CollectItems(player.inventory, 58);
      if (player.useVoidBag() && player.chest != -5)
        Recipe.CollectItems(player.bank4.item, 40);
      if (player.chest != -1)
      {
        Item[] currentInventory = (Item[]) null;
        if (player.chest > -1)
          currentInventory = Main.chest[player.chest].item;
        else if (player.chest == -2)
          currentInventory = player.bank.item;
        else if (player.chest == -3)
          currentInventory = player.bank2.item;
        else if (player.chest == -4)
          currentInventory = player.bank3.item;
        else if (player.chest == -5)
          currentInventory = player.bank4.item;
        Recipe.CollectItems(currentInventory, 40);
      }
      Recipe.AddFakeCountsForItemGroups();
    }

    private static void AddFakeCountsForItemGroups()
    {
      foreach (RecipeGroup recipeGroup in RecipeGroup.recipeGroups.Values)
      {
        int groupFakeItemId = recipeGroup.GetGroupFakeItemId();
        Recipe._ownedItems[groupFakeItemId] = recipeGroup.CountUsableItems(Recipe._ownedItems);
      }
    }

    private static void CollectItems(Item[] currentInventory, int slotCap)
    {
      for (int index = 0; index < slotCap; ++index)
      {
        Item obj = currentInventory[index];
        if (obj.stack > 0)
        {
          int stack = obj.stack;
          int num;
          if (Recipe._ownedItems.TryGetValue(obj.netID, out num))
            stack += num;
          Recipe._ownedItems[obj.netID] = stack;
        }
      }
    }

    private static void CollectGuideRecipes()
    {
      int type = Main.guideItem.type;
      for (int index1 = 0; index1 < Recipe.maxRecipes; ++index1)
      {
        Recipe recipe = Main.recipe[index1];
        if (recipe.createItem.type == 0)
          break;
        for (int index2 = 0; index2 < Recipe.maxRequirements; ++index2)
        {
          Item compareItem = recipe.requiredItem[index2];
          if (compareItem.type != 0)
          {
            if (Main.guideItem.IsTheSameAs(compareItem) || recipe.useWood(type, compareItem.type) || recipe.useSand(type, compareItem.type) || recipe.useIronBar(type, compareItem.type) || recipe.useFragment(type, compareItem.type) || recipe.AcceptedByItemGroups(type, compareItem.type) || recipe.usePressurePlate(type, compareItem.type))
            {
              Main.availableRecipe[Main.numAvailableRecipes] = index1;
              ++Main.numAvailableRecipes;
              break;
            }
          }
          else
            break;
        }
      }
    }

    public static void ClearAvailableRecipes()
    {
      for (int index = 0; index < Recipe.maxRecipes; ++index)
        Main.availableRecipe[index] = 0;
      Main.numAvailableRecipes = 0;
    }

    private static void VisuallyRepositionRecipes(float focusY)
    {
      float num = Main.availableRecipeY[Main.focusRecipe] - focusY;
      for (int index = 0; index < Recipe.maxRecipes; ++index)
        Main.availableRecipeY[index] -= num;
    }

    private static void TryRefocusingRecipe(int oldRecipe)
    {
      for (int index = 0; index < Main.numAvailableRecipes; ++index)
      {
        if (oldRecipe == Main.availableRecipe[index])
        {
          Main.focusRecipe = index;
          break;
        }
      }
      if (Main.focusRecipe >= Main.numAvailableRecipes)
        Main.focusRecipe = Main.numAvailableRecipes - 1;
      if (Main.focusRecipe >= 0)
        return;
      Main.focusRecipe = 0;
    }

    public static void SetupRecipeGroups()
    {
      RecipeGroupID.Birds = RecipeGroup.RegisterGroup("Birds", new RecipeGroup((Func<string>) (() => Lang.misc[37].Value + " " + Lang.GetNPCNameValue(74)), new int[3]
      {
        2015,
        2016,
        2017
      }));
      RecipeGroupID.Scorpions = RecipeGroup.RegisterGroup("Scorpions", new RecipeGroup((Func<string>) (() => Lang.misc[37].Value + " " + Lang.GetNPCNameValue(367)), new int[2]
      {
        2157,
        2156
      }));
      RecipeGroupID.Squirrels = RecipeGroup.RegisterGroup("Squirrels", new RecipeGroup((Func<string>) (() => Lang.misc[37].Value + " " + Lang.GetNPCNameValue(299)), new int[2]
      {
        2018,
        3563
      }));
      RecipeGroupID.Bugs = RecipeGroup.RegisterGroup("Bugs", new RecipeGroup((Func<string>) (() => Lang.misc[37].Value + " " + Lang.misc[85].Value), new int[3]
      {
        3194,
        3192,
        3193
      }));
      RecipeGroupID.Ducks = RecipeGroup.RegisterGroup("Ducks", new RecipeGroup((Func<string>) (() => Lang.misc[37].Value + " " + Lang.misc[86].Value), new int[2]
      {
        2123,
        2122
      }));
      RecipeGroupID.Butterflies = RecipeGroup.RegisterGroup("Butterflies", new RecipeGroup((Func<string>) (() => Lang.misc[37].Value + " " + Lang.misc[87].Value), new int[8]
      {
        1998,
        2001,
        1994,
        1995,
        1996,
        1999,
        1997,
        2000
      }));
      RecipeGroupID.Fireflies = RecipeGroup.RegisterGroup("Fireflies", new RecipeGroup((Func<string>) (() => Lang.misc[37].Value + " " + Lang.misc[88].Value), new int[2]
      {
        1992,
        2004
      }));
      RecipeGroupID.Snails = RecipeGroup.RegisterGroup("Snails", new RecipeGroup((Func<string>) (() => Lang.misc[37].Value + " " + Lang.misc[95].Value), new int[2]
      {
        2006,
        2007
      }));
      RecipeGroupID.Dragonflies = RecipeGroup.RegisterGroup("Dragonflies", new RecipeGroup((Func<string>) (() => Lang.misc[37].Value + " " + Lang.misc[105].Value), new int[6]
      {
        4334,
        4335,
        4336,
        4338,
        4339,
        4337
      }));
      RecipeGroupID.Turtles = RecipeGroup.RegisterGroup("Turtles", new RecipeGroup((Func<string>) (() => Lang.misc[37].Value + " " + Lang.GetNPCNameValue(616)), new int[2]
      {
        4464,
        4465
      }));
      RecipeGroupID.Macaws = RecipeGroup.RegisterGroup("Macaws", new RecipeGroup((Func<string>) (() => Lang.misc[37].Value + " " + Language.GetTextValue("Misc.Macaw")), new int[2]
      {
        5212,
        5300
      }));
      RecipeGroupID.Cockatiels = RecipeGroup.RegisterGroup("Cockatiels", new RecipeGroup((Func<string>) (() => Lang.misc[37].Value + " " + Language.GetTextValue("Misc.Cockatiel")), new int[2]
      {
        5312,
        5313
      }));
      RecipeGroupID.CloudBalloons = RecipeGroup.RegisterGroup("Cloud Balloons", new RecipeGroup((Func<string>) (() => Lang.misc[37].Value + " " + Language.GetTextValue("Misc.CloudBalloon")), new int[2]
      {
        399,
        1250
      }));
      RecipeGroupID.BlizzardBalloons = RecipeGroup.RegisterGroup("Blizzard Balloons", new RecipeGroup((Func<string>) (() => Lang.misc[37].Value + " " + Language.GetTextValue("Misc.BlizzardBalloon")), new int[2]
      {
        1163,
        1251
      }));
      RecipeGroupID.SandstormBalloons = RecipeGroup.RegisterGroup("Sandstorm Balloons", new RecipeGroup((Func<string>) (() => Lang.misc[37].Value + " " + Language.GetTextValue("Misc.SandstormBalloon")), new int[2]
      {
        983,
        1252
      }));
      RecipeGroupID.CritterGuides = RecipeGroup.RegisterGroup("Guide to Critter Companionship", new RecipeGroup((Func<string>) (() => Lang.misc[37].Value + " " + Language.GetTextValue("Misc.CritterGuides")), new int[2]
      {
        4767,
        5453
      }));
      RecipeGroupID.NatureGuides = RecipeGroup.RegisterGroup("Guide to Nature Preservation", new RecipeGroup((Func<string>) (() => Lang.misc[37].Value + " " + Language.GetTextValue("Misc.NatureGuides")), new int[2]
      {
        5309,
        5454
      }));
      RecipeGroupID.Fruit = RecipeGroup.RegisterGroup("Fruit", new RecipeGroup((Func<string>) (() => Lang.misc[37].Value + " " + Language.GetTextValue("Misc.Fruit")), new int[19]
      {
        4009,
        4282,
        4283,
        4284,
        4285,
        4286,
        4287,
        4288,
        4289,
        4290,
        4291,
        4292,
        4293,
        4294,
        4295,
        4296,
        4297,
        5277,
        5278
      }));
      RecipeGroupID.Balloons = RecipeGroup.RegisterGroup("Balloons", new RecipeGroup((Func<string>) (() => Lang.misc[37].Value + " " + Language.GetTextValue("Misc.Balloon")), new int[3]
      {
        3738,
        3736,
        3737
      }));
      RecipeGroupID.Wood = RecipeGroup.RegisterGroup("Wood", new RecipeGroup((Func<string>) (() => "replaceme wood"), new int[9]
      {
        9,
        619,
        620,
        621,
        911,
        1729,
        2504,
        2503,
        5215
      }));
      RecipeGroupID.Sand = RecipeGroup.RegisterGroup("Sand", new RecipeGroup((Func<string>) (() => "replaceme sand"), new int[8]
      {
        169,
        408,
        1246,
        370,
        3272,
        3338,
        3274,
        3275
      }));
      RecipeGroupID.IronBar = RecipeGroup.RegisterGroup("IronBar", new RecipeGroup((Func<string>) (() => "replaceme ironbar"), new int[2]
      {
        22,
        704
      }));
      RecipeGroupID.Fragment = RecipeGroup.RegisterGroup("Fragment", new RecipeGroup((Func<string>) (() => "replaceme fragment"), new int[4]
      {
        3458,
        3456,
        3457,
        3459
      }));
      RecipeGroupID.PressurePlate = RecipeGroup.RegisterGroup("PressurePlate", new RecipeGroup((Func<string>) (() => "replaceme pressureplate"), new int[8]
      {
        852,
        543,
        542,
        541,
        1151,
        529,
        853,
        4261
      }));
    }

    public static void UpdateItemVariants()
    {
      for (int index = 0; index < Recipe.maxRecipes; ++index)
      {
        Recipe recipe = Main.recipe[index];
        recipe.createItem.Refresh();
        foreach (Item obj in recipe.requiredItem)
          obj.Refresh();
      }
      if (Main.remixWorld && Main.getGoodWorld)
      {
        ItemID.Sets.IsAMaterial[544] = true;
        ItemID.Sets.IsAMaterial[556] = true;
        ItemID.Sets.IsAMaterial[557] = true;
      }
      else
      {
        ItemID.Sets.IsAMaterial[544] = false;
        ItemID.Sets.IsAMaterial[556] = false;
        ItemID.Sets.IsAMaterial[557] = false;
      }
    }

    public static void SetupRecipes()
    {
      // ISSUE: The method is too long to display (62846 instructions)
    }

    private static void ReplaceItemUseFlagsWithRecipeGroups()
    {
      for (int index = 0; index < Recipe.numRecipes; ++index)
      {
        Recipe recipe = Main.recipe[index];
        recipe.ReplaceItemUseFlagWithGroup(ref recipe.anyWood, RecipeGroupID.Wood);
        recipe.ReplaceItemUseFlagWithGroup(ref recipe.anySand, RecipeGroupID.Sand);
        recipe.ReplaceItemUseFlagWithGroup(ref recipe.anyPressurePlate, RecipeGroupID.PressurePlate);
        recipe.ReplaceItemUseFlagWithGroup(ref recipe.anyIronBar, RecipeGroupID.IronBar);
        recipe.ReplaceItemUseFlagWithGroup(ref recipe.anyFragment, RecipeGroupID.Fragment);
      }
    }

    private void ReplaceItemUseFlagWithGroup(ref bool flag, int groupId)
    {
      if (!flag)
        return;
      this.RequireGroup(groupId);
    }

    private static void CreateRequiredItemQuickLookups()
    {
      for (int index1 = 0; index1 < Recipe.numRecipes; ++index1)
      {
        Recipe recipe = Main.recipe[index1];
        for (int index2 = 0; index2 < Recipe.maxRequirements; ++index2)
        {
          Item obj = recipe.requiredItem[index2];
          if (!obj.IsAir)
          {
            Recipe.RequiredItemEntry requiredItemEntry = new Recipe.RequiredItemEntry()
            {
              itemIdOrRecipeGroup = obj.type,
              stack = obj.stack
            };
            foreach (int acceptedGroup in recipe.acceptedGroups)
            {
              if (acceptedGroup >= 0)
              {
                RecipeGroup recipeGroup = RecipeGroup.recipeGroups[acceptedGroup];
                if (recipeGroup.ValidItems.Contains(obj.type))
                  requiredItemEntry.itemIdOrRecipeGroup = recipeGroup.GetGroupFakeItemId();
              }
              else
                break;
            }
            recipe.requiredItemQuickLookup[index2] = requiredItemEntry;
          }
          else
            break;
        }
      }
    }

    private static void UpdateMaterialFieldForAllRecipes()
    {
      for (int index1 = 0; index1 < Recipe.numRecipes; ++index1)
      {
        for (int index2 = 0; Main.recipe[index1].requiredItem[index2].type > 0; ++index2)
          Main.recipe[index1].requiredItem[index2].material = ItemID.Sets.IsAMaterial[Main.recipe[index1].requiredItem[index2].type];
        Main.recipe[index1].createItem.material = ItemID.Sets.IsAMaterial[Main.recipe[index1].createItem.type];
      }
    }

    public static void UpdateWhichItemsAreMaterials()
    {
      for (int Type = 0; Type < (int) ItemID.Count; ++Type)
      {
        Item obj = new Item();
        obj.SetDefaults(Type, true);
        obj.checkMat();
        ItemID.Sets.IsAMaterial[Type] = obj.material;
      }
    }

    public static void UpdateWhichItemsAreCrafted()
    {
      for (int index = 0; index < Recipe.numRecipes; ++index)
      {
        if (!Main.recipe[index].notDecraftable)
          ItemID.Sets.IsCrafted[Main.recipe[index].createItem.type] = index;
        if (Main.recipe[index].crimson)
          ItemID.Sets.IsCraftedCrimson[Main.recipe[index].createItem.type] = index;
        if (Main.recipe[index].corruption)
          ItemID.Sets.IsCraftedCorruption[Main.recipe[index].createItem.type] = index;
      }
    }

    private static void AddSolarFurniture()
    {
      Recipe.currentRecipe.createItem.SetDefaults(4229);
      Recipe.currentRecipe.createItem.stack = 10;
      Recipe.currentRecipe.SetIngredients(3, 10, 3458, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4233);
      Recipe.currentRecipe.createItem.stack = 4;
      Recipe.currentRecipe.SetIngredients(4229, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4145);
      Recipe.currentRecipe.SetIngredients(4229, 14);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4146);
      Recipe.currentRecipe.SetIngredients(4229, 15, 225, 5);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4147);
      Recipe.currentRecipe.SetIngredients(4229, 20, 149, 10);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4148);
      Recipe.currentRecipe.SetIngredients(4229, 16);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4149);
      Recipe.currentRecipe.SetIngredients(4229, 5, 8, 3);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4150);
      Recipe.currentRecipe.SetIngredients(4229, 4, 8, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4151);
      Recipe.currentRecipe.SetIngredients(4229, 4);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4152);
      Recipe.currentRecipe.SetIngredients(4229, 4, 8, 4, 85, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4153);
      Recipe.currentRecipe.SetIngredients(4229, 8, 22, 2);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4154);
      Recipe.currentRecipe.SetIngredients(4229, 10, 22, 3, 170, 6);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4155);
      Recipe.currentRecipe.SetIngredients(4229, 6);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4156);
      Recipe.currentRecipe.SetIngredients(8, 1, 4229, 3);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4157);
      Recipe.currentRecipe.SetIngredients(4229, 6, 8, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4158);
      Recipe.currentRecipe.SetIngredients(4229, 15, 154, 4, 149, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4160);
      Recipe.currentRecipe.SetIngredients(4229, 6, 206, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4161);
      Recipe.currentRecipe.SetIngredients(4229, 5, 225, 2);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4162);
      Recipe.currentRecipe.SetIngredients(4229, 8);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4163);
      Recipe.currentRecipe.SetIngredients(4229, 10);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4165);
      Recipe.currentRecipe.SetIngredients(4229, 6);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
    }

    private static void AddVortexFurniture()
    {
      Recipe.currentRecipe.createItem.SetDefaults(4230);
      Recipe.currentRecipe.createItem.stack = 10;
      Recipe.currentRecipe.SetIngredients(3, 10, 3456, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4234);
      Recipe.currentRecipe.createItem.stack = 4;
      Recipe.currentRecipe.SetIngredients(4230, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4166);
      Recipe.currentRecipe.SetIngredients(4230, 14);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4167);
      Recipe.currentRecipe.SetIngredients(4230, 15, 225, 5);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4168);
      Recipe.currentRecipe.SetIngredients(4230, 20, 149, 10);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4169);
      Recipe.currentRecipe.SetIngredients(4230, 16);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4170);
      Recipe.currentRecipe.SetIngredients(4230, 5, 8, 3);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4171);
      Recipe.currentRecipe.SetIngredients(4230, 4, 8, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4172);
      Recipe.currentRecipe.SetIngredients(4230, 4);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4173);
      Recipe.currentRecipe.SetIngredients(4230, 4, 8, 4, 85, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4174);
      Recipe.currentRecipe.SetIngredients(4230, 8, 22, 2);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4175);
      Recipe.currentRecipe.SetIngredients(4230, 10, 22, 3, 170, 6);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4176);
      Recipe.currentRecipe.SetIngredients(4230, 6);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4177);
      Recipe.currentRecipe.SetIngredients(8, 1, 4230, 3);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4178);
      Recipe.currentRecipe.SetIngredients(4230, 6, 8, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4179);
      Recipe.currentRecipe.SetIngredients(4230, 15, 154, 4, 149, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4181);
      Recipe.currentRecipe.SetIngredients(4230, 6, 206, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4182);
      Recipe.currentRecipe.SetIngredients(4230, 5, 225, 2);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4183);
      Recipe.currentRecipe.SetIngredients(4230, 8);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4184);
      Recipe.currentRecipe.SetIngredients(4230, 10);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4186);
      Recipe.currentRecipe.SetIngredients(4230, 6);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
    }

    private static void AddNebulaFurniture()
    {
      Recipe.currentRecipe.createItem.SetDefaults(4231);
      Recipe.currentRecipe.createItem.stack = 10;
      Recipe.currentRecipe.SetIngredients(3, 10, 3457, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4235);
      Recipe.currentRecipe.createItem.stack = 4;
      Recipe.currentRecipe.SetIngredients(4231, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4187);
      Recipe.currentRecipe.SetIngredients(4231, 14);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4188);
      Recipe.currentRecipe.SetIngredients(4231, 15, 225, 5);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4189);
      Recipe.currentRecipe.SetIngredients(4231, 20, 149, 10);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4190);
      Recipe.currentRecipe.SetIngredients(4231, 16);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4191);
      Recipe.currentRecipe.SetIngredients(4231, 5, 8, 3);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4192);
      Recipe.currentRecipe.SetIngredients(4231, 4, 8, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4193);
      Recipe.currentRecipe.SetIngredients(4231, 4);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4194);
      Recipe.currentRecipe.SetIngredients(4231, 4, 8, 4, 85, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4195);
      Recipe.currentRecipe.SetIngredients(4231, 8, 22, 2);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4196);
      Recipe.currentRecipe.SetIngredients(4231, 10, 22, 3, 170, 6);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4197);
      Recipe.currentRecipe.SetIngredients(4231, 6);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4198);
      Recipe.currentRecipe.SetIngredients(8, 1, 4231, 3);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4199);
      Recipe.currentRecipe.SetIngredients(4231, 6, 8, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4200);
      Recipe.currentRecipe.SetIngredients(4231, 15, 154, 4, 149, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4202);
      Recipe.currentRecipe.SetIngredients(4231, 6, 206, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4203);
      Recipe.currentRecipe.SetIngredients(4231, 5, 225, 2);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4204);
      Recipe.currentRecipe.SetIngredients(4231, 8);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4205);
      Recipe.currentRecipe.SetIngredients(4231, 10);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4207);
      Recipe.currentRecipe.SetIngredients(4231, 6);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
    }

    private static void AddStardustFurniture()
    {
      Recipe.currentRecipe.createItem.SetDefaults(4232);
      Recipe.currentRecipe.createItem.stack = 10;
      Recipe.currentRecipe.SetIngredients(3, 10, 3459, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4236);
      Recipe.currentRecipe.createItem.stack = 4;
      Recipe.currentRecipe.SetIngredients(4232, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4208);
      Recipe.currentRecipe.SetIngredients(4232, 14);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4209);
      Recipe.currentRecipe.SetIngredients(4232, 15, 225, 5);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4210);
      Recipe.currentRecipe.SetIngredients(4232, 20, 149, 10);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4211);
      Recipe.currentRecipe.SetIngredients(4232, 16);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4212);
      Recipe.currentRecipe.SetIngredients(4232, 5, 8, 3);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4213);
      Recipe.currentRecipe.SetIngredients(4232, 4, 8, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4214);
      Recipe.currentRecipe.SetIngredients(4232, 4);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4215);
      Recipe.currentRecipe.SetIngredients(4232, 4, 8, 4, 85, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4216);
      Recipe.currentRecipe.SetIngredients(4232, 8, 22, 2);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4217);
      Recipe.currentRecipe.SetIngredients(4232, 10, 22, 3, 170, 6);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4218);
      Recipe.currentRecipe.SetIngredients(4232, 6);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4219);
      Recipe.currentRecipe.SetIngredients(8, 1, 4232, 3);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4220);
      Recipe.currentRecipe.SetIngredients(4232, 6, 8, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4221);
      Recipe.currentRecipe.SetIngredients(4232, 15, 154, 4, 149, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4223);
      Recipe.currentRecipe.SetIngredients(4232, 6, 206, 1);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4224);
      Recipe.currentRecipe.SetIngredients(4232, 5, 225, 2);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4225);
      Recipe.currentRecipe.SetIngredients(4232, 8);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4226);
      Recipe.currentRecipe.SetIngredients(4232, 10);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4228);
      Recipe.currentRecipe.SetIngredients(4232, 6);
      Recipe.currentRecipe.SetCraftingStation(412);
      Recipe.AddRecipe();
    }

    private static void AddSpiderFurniture()
    {
      Recipe.currentRecipe.createItem.SetDefaults(4139);
      Recipe.currentRecipe.createItem.stack = 10;
      Recipe.currentRecipe.SetIngredients(150, 10, 2607, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4140);
      Recipe.currentRecipe.createItem.stack = 4;
      Recipe.currentRecipe.SetIngredients(4139, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3931);
      Recipe.currentRecipe.SetIngredients(4139, 14);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3932);
      Recipe.currentRecipe.SetIngredients(4139, 15, 225, 5);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3933);
      Recipe.currentRecipe.SetIngredients(4139, 20, 149, 10);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3934);
      Recipe.currentRecipe.SetIngredients(4139, 16);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3935);
      Recipe.currentRecipe.SetIngredients(4139, 5, 8, 3);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3936);
      Recipe.currentRecipe.SetIngredients(4139, 4, 8, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3937);
      Recipe.currentRecipe.SetIngredients(4139, 4);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3938);
      Recipe.currentRecipe.SetIngredients(4139, 4, 8, 4, 85, 1);
      Recipe.currentRecipe.SetCraftingStation(16);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3939);
      Recipe.currentRecipe.SetIngredients(4139, 8, 22, 2);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3940);
      Recipe.currentRecipe.SetIngredients(4139, 10, 22, 3, 170, 6);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3941);
      Recipe.currentRecipe.SetIngredients(4139, 6);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3942);
      Recipe.currentRecipe.SetIngredients(8, 1, 4139, 3);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3943);
      Recipe.currentRecipe.SetIngredients(4139, 6, 8, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3944);
      Recipe.currentRecipe.SetIngredients(4139, 15, 154, 4, 149, 1);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3946);
      Recipe.currentRecipe.SetIngredients(4139, 6, 206, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3947);
      Recipe.currentRecipe.SetIngredients(4139, 5, 225, 2);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3948);
      Recipe.currentRecipe.SetIngredients(4139, 8);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3949);
      Recipe.currentRecipe.SetIngredients(4139, 10);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4125);
      Recipe.currentRecipe.SetIngredients(4139, 6);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
    }

    private static void AddLesionFurniture()
    {
      int num = 3955;
      Recipe.currentRecipe.createItem.SetDefaults(3955);
      Recipe.currentRecipe.SetIngredients(61, 2);
      Recipe.currentRecipe.SetCraftingStation(218);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3975);
      Recipe.currentRecipe.SetIngredients(num, 10);
      Recipe.currentRecipe.anyWood = true;
      Recipe.currentRecipe.SetCraftingStation(499);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3956);
      Recipe.currentRecipe.createItem.stack = 4;
      Recipe.currentRecipe.SetIngredients(3955, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3967);
      Recipe.currentRecipe.SetIngredients(num, 6);
      Recipe.currentRecipe.anyWood = true;
      Recipe.currentRecipe.SetCraftingStation(499);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3963);
      Recipe.currentRecipe.SetIngredients(num, 4);
      Recipe.currentRecipe.anyWood = true;
      Recipe.currentRecipe.SetCraftingStation(499);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3965);
      Recipe.currentRecipe.SetIngredients(num, 8, 22, 2);
      Recipe.currentRecipe.anyWood = true;
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.currentRecipe.SetCraftingStation(499);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3974);
      Recipe.currentRecipe.SetIngredients(num, 8);
      Recipe.currentRecipe.anyWood = true;
      Recipe.currentRecipe.SetCraftingStation(499);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3972);
      Recipe.currentRecipe.SetIngredients(num, 6, 206, 1);
      Recipe.currentRecipe.anyWood = true;
      Recipe.currentRecipe.SetCraftingStation(499);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3970);
      Recipe.currentRecipe.SetIngredients(num, 6, 8, 1);
      Recipe.currentRecipe.anyWood = true;
      Recipe.currentRecipe.SetCraftingStation(499);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3962);
      Recipe.currentRecipe.SetIngredients(num, 4, 8, 1);
      Recipe.currentRecipe.anyWood = true;
      Recipe.currentRecipe.SetCraftingStation(499);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3969);
      Recipe.currentRecipe.SetIngredients(num, 3, 8, 1);
      Recipe.currentRecipe.anyWood = true;
      Recipe.currentRecipe.SetCraftingStation(499);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3961);
      Recipe.currentRecipe.SetIngredients(num, 5, 8, 3);
      Recipe.currentRecipe.anyWood = true;
      Recipe.currentRecipe.SetCraftingStation(499);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3959);
      Recipe.currentRecipe.SetIngredients(num, 15, 225, 5);
      Recipe.currentRecipe.anyWood = true;
      Recipe.currentRecipe.SetCraftingStation(499);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3968);
      Recipe.currentRecipe.SetIngredients(num, 16);
      Recipe.currentRecipe.anyWood = true;
      Recipe.currentRecipe.SetCraftingStation(499);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3960);
      Recipe.currentRecipe.SetIngredients(num, 20, 149, 10);
      Recipe.currentRecipe.anyWood = true;
      Recipe.currentRecipe.SetCraftingStation(499);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3966);
      Recipe.currentRecipe.SetIngredients(22, 3, 170, 6, num, 10);
      Recipe.currentRecipe.anyWood = true;
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.currentRecipe.SetCraftingStation(499);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3973);
      Recipe.currentRecipe.SetIngredients(num, 5, 225, 2);
      Recipe.currentRecipe.anyWood = true;
      Recipe.currentRecipe.SetCraftingStation(499);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3971);
      Recipe.currentRecipe.SetIngredients(154, 4, num, 15, 149, 1);
      Recipe.currentRecipe.anyWood = true;
      Recipe.currentRecipe.SetCraftingStation(499);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3958);
      Recipe.currentRecipe.SetIngredients(num, 14);
      Recipe.currentRecipe.anyWood = true;
      Recipe.currentRecipe.SetCraftingStation(499);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(3964);
      Recipe.currentRecipe.SetIngredients(num, 4, 8, 4, 85, 1);
      Recipe.currentRecipe.anyWood = true;
      Recipe.currentRecipe.SetCraftingStation(499);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4126);
      Recipe.currentRecipe.SetIngredients(3955, 6);
      Recipe.currentRecipe.SetCraftingStation(499);
      Recipe.AddRecipe();
    }

    private static void AddSandstoneFurniture()
    {
      Recipe.currentRecipe.createItem.SetDefaults(4720);
      Recipe.currentRecipe.createItem.stack = 2;
      Recipe.currentRecipe.SetIngredients(4051, 1);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4298);
      Recipe.currentRecipe.SetIngredients(4051, 14);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4299);
      Recipe.currentRecipe.SetIngredients(4051, 15, 225, 5);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4300);
      Recipe.currentRecipe.SetIngredients(4051, 20, 149, 10);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4301);
      Recipe.currentRecipe.SetIngredients(4051, 16);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4302);
      Recipe.currentRecipe.SetIngredients(4051, 5, 8, 3);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4303);
      Recipe.currentRecipe.SetIngredients(4051, 4, 8, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4304);
      Recipe.currentRecipe.SetIngredients(4051, 4);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4305);
      Recipe.currentRecipe.SetIngredients(4051, 4, 8, 4, 85, 1);
      Recipe.currentRecipe.SetCraftingStation(16);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4267);
      Recipe.currentRecipe.SetIngredients(4051, 8, 22, 2);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4306);
      Recipe.currentRecipe.SetIngredients(4051, 10, 22, 3, 170, 6);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4307);
      Recipe.currentRecipe.SetIngredients(4051, 6);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4308);
      Recipe.currentRecipe.SetIngredients(8, 1, 4051, 3);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4309);
      Recipe.currentRecipe.SetIngredients(4051, 6, 8, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4310);
      Recipe.currentRecipe.SetIngredients(4051, 15, 154, 4, 149, 1);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4312);
      Recipe.currentRecipe.SetIngredients(4051, 6, 206, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4313);
      Recipe.currentRecipe.SetIngredients(4051, 5, 225, 2);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4314);
      Recipe.currentRecipe.SetIngredients(4051, 8);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4315);
      Recipe.currentRecipe.SetIngredients(4051, 10);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4316);
      Recipe.currentRecipe.SetIngredients(4051, 6);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
    }

    private static void AddBambooFurniture()
    {
      Recipe.currentRecipe.createItem.SetDefaults(4566);
      Recipe.currentRecipe.SetIngredients(4564, 14);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4567);
      Recipe.currentRecipe.SetIngredients(4564, 15, 225, 5);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4568);
      Recipe.currentRecipe.SetIngredients(4564, 20, 149, 10);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4569);
      Recipe.currentRecipe.SetIngredients(4564, 16);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4570);
      Recipe.currentRecipe.SetIngredients(4564, 5, 8, 3);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4571);
      Recipe.currentRecipe.SetIngredients(4564, 4, 8, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4572);
      Recipe.currentRecipe.SetIngredients(4564, 4);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4573);
      Recipe.currentRecipe.SetIngredients(4564, 4, 8, 4, 85, 1);
      Recipe.currentRecipe.SetCraftingStation(16);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4574);
      Recipe.currentRecipe.SetIngredients(4564, 8, 22, 2);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4575);
      Recipe.currentRecipe.SetIngredients(4564, 10, 22, 3, 170, 6);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4576);
      Recipe.currentRecipe.SetIngredients(4564, 6);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4577);
      Recipe.currentRecipe.SetIngredients(8, 1, 4564, 3);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4578);
      Recipe.currentRecipe.SetIngredients(4564, 6, 8, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4579);
      Recipe.currentRecipe.SetIngredients(4564, 15, 154, 4, 149, 1);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4581);
      Recipe.currentRecipe.SetIngredients(4564, 6, 206, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4582);
      Recipe.currentRecipe.SetIngredients(4564, 5, 225, 2);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4583);
      Recipe.currentRecipe.SetIngredients(4564, 8);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4584);
      Recipe.currentRecipe.SetIngredients(4564, 10);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(4586);
      Recipe.currentRecipe.SetIngredients(4564, 6);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
    }

    private static void AddCoralFurniture()
    {
      Recipe.currentRecipe.createItem.SetDefaults(5148);
      Recipe.currentRecipe.SetIngredients(5306, 14);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5149);
      Recipe.currentRecipe.SetIngredients(5306, 15, 225, 5);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5150);
      Recipe.currentRecipe.SetIngredients(5306, 20, 149, 10);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5151);
      Recipe.currentRecipe.SetIngredients(5306, 16);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5152);
      Recipe.currentRecipe.SetIngredients(5306, 5, 8, 3);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5153);
      Recipe.currentRecipe.SetIngredients(5306, 4, 8, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5154);
      Recipe.currentRecipe.SetIngredients(5306, 4);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5155);
      Recipe.currentRecipe.SetIngredients(5306, 4, 8, 4, 85, 1);
      Recipe.currentRecipe.SetCraftingStation(16);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5156);
      Recipe.currentRecipe.SetIngredients(5306, 8, 22, 2);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5157);
      Recipe.currentRecipe.SetIngredients(5306, 10, 22, 3, 170, 6);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5158);
      Recipe.currentRecipe.SetIngredients(5306, 6);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5159);
      Recipe.currentRecipe.SetIngredients(8, 1, 5306, 3);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5160);
      Recipe.currentRecipe.SetIngredients(5306, 6, 8, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5161);
      Recipe.currentRecipe.SetIngredients(5306, 15, 154, 4, 149, 1);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5163);
      Recipe.currentRecipe.SetIngredients(5306, 6, 206, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5164);
      Recipe.currentRecipe.SetIngredients(5306, 5, 225, 2);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5165);
      Recipe.currentRecipe.SetIngredients(5306, 8);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5166);
      Recipe.currentRecipe.SetIngredients(5306, 10);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5168);
      Recipe.currentRecipe.SetIngredients(5306, 6);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
    }

    private static void AddBalloonFurniture()
    {
      Recipe.currentRecipe.createItem.SetDefaults(5169);
      Recipe.currentRecipe.SetIngredients(3738, 14);
      Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5170);
      Recipe.currentRecipe.SetIngredients(3738, 15, 225, 5);
      Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5171);
      Recipe.currentRecipe.SetIngredients(3738, 20, 149, 10);
      Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5172);
      Recipe.currentRecipe.SetIngredients(3738, 16);
      Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5173);
      Recipe.currentRecipe.SetIngredients(3738, 5, 8, 3);
      Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5174);
      Recipe.currentRecipe.SetIngredients(3738, 4, 8, 1);
      Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5175);
      Recipe.currentRecipe.SetIngredients(3738, 4);
      Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5176);
      Recipe.currentRecipe.SetIngredients(3738, 4, 8, 4, 85, 1);
      Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
      Recipe.currentRecipe.SetCraftingStation(16);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5177);
      Recipe.currentRecipe.SetIngredients(3738, 8, 22, 2);
      Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5178);
      Recipe.currentRecipe.SetIngredients(3738, 10, 22, 3, 170, 6);
      Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5179);
      Recipe.currentRecipe.SetIngredients(3738, 6);
      Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5180);
      Recipe.currentRecipe.SetIngredients(8, 1, 3738, 3);
      Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5181);
      Recipe.currentRecipe.SetIngredients(3738, 6, 8, 1);
      Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5182);
      Recipe.currentRecipe.SetIngredients(3738, 15, 154, 4, 149, 1);
      Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5184);
      Recipe.currentRecipe.SetIngredients(3738, 6, 206, 1);
      Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5185);
      Recipe.currentRecipe.SetIngredients(3738, 5, 225, 2);
      Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5186);
      Recipe.currentRecipe.SetIngredients(3738, 8);
      Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5187);
      Recipe.currentRecipe.SetIngredients(3738, 10);
      Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5189);
      Recipe.currentRecipe.SetIngredients(3738, 6);
      Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
    }

    private static void AddAshWoodFurnitureArmorAndItems()
    {
      Recipe.currentRecipe.createItem.SetDefaults(5279);
      Recipe.currentRecipe.SetIngredients(5215, 20);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5280);
      Recipe.currentRecipe.SetIngredients(5215, 30);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5281);
      Recipe.currentRecipe.SetIngredients(5215, 25);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5284);
      Recipe.currentRecipe.SetIngredients(5215, 7);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5283);
      Recipe.currentRecipe.SetIngredients(5215, 8);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5282);
      Recipe.currentRecipe.SetIngredients(5215, 10);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5190);
      Recipe.currentRecipe.SetIngredients(5215, 14);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5191);
      Recipe.currentRecipe.SetIngredients(5215, 15, 225, 5);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5192);
      Recipe.currentRecipe.SetIngredients(5215, 20, 149, 10);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5193);
      Recipe.currentRecipe.SetIngredients(5215, 16);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5194);
      Recipe.currentRecipe.SetIngredients(5215, 5, 8, 3);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5195);
      Recipe.currentRecipe.SetIngredients(5215, 4, 8, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5196);
      Recipe.currentRecipe.SetIngredients(5215, 4);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5197);
      Recipe.currentRecipe.SetIngredients(5215, 4, 8, 4, 85, 1);
      Recipe.currentRecipe.SetCraftingStation(16);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5198);
      Recipe.currentRecipe.SetIngredients(5215, 8, 22, 2);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5199);
      Recipe.currentRecipe.SetIngredients(5215, 10, 22, 3, 170, 6);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.currentRecipe.anyIronBar = true;
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5200);
      Recipe.currentRecipe.SetIngredients(5215, 6);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5201);
      Recipe.currentRecipe.SetIngredients(8, 1, 5215, 3);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5202);
      Recipe.currentRecipe.SetIngredients(5215, 6, 8, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5203);
      Recipe.currentRecipe.SetIngredients(5215, 15, 154, 4, 149, 1);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5205);
      Recipe.currentRecipe.SetIngredients(5215, 6, 206, 1);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5206);
      Recipe.currentRecipe.SetIngredients(5215, 5, 225, 2);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5207);
      Recipe.currentRecipe.SetIngredients(5215, 8);
      Recipe.currentRecipe.SetCraftingStation(18);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5208);
      Recipe.currentRecipe.SetIngredients(5215, 10);
      Recipe.AddRecipe();
      Recipe.currentRecipe.createItem.SetDefaults(5210);
      Recipe.currentRecipe.SetIngredients(5215, 6);
      Recipe.currentRecipe.SetCraftingStation(106);
      Recipe.AddRecipe();
    }

    private static void CreateReversePlatformRecipes()
    {
      int numRecipes = Recipe.numRecipes;
      for (int index1 = 0; index1 < numRecipes; ++index1)
      {
        if (Main.recipe[index1].createItem.createTile >= 0 && TileID.Sets.Platforms[Main.recipe[index1].createItem.createTile] && Main.recipe[index1].requiredItem[1].type == 0)
        {
          Recipe.currentRecipe.createItem.SetDefaults(Main.recipe[index1].requiredItem[0].type);
          Recipe.currentRecipe.createItem.stack = Main.recipe[index1].requiredItem[0].stack;
          Recipe.currentRecipe.requiredItem[0].SetDefaults(Main.recipe[index1].createItem.type);
          Recipe.currentRecipe.requiredItem[0].stack = Main.recipe[index1].createItem.stack;
          for (int index2 = 0; index2 < Recipe.currentRecipe.requiredTile.Length; ++index2)
            Recipe.currentRecipe.requiredTile[index2] = Main.recipe[index1].requiredTile[index2];
          Recipe.AddRecipe();
          Recipe recipe = Main.recipe[Recipe.numRecipes - 1];
          for (int index3 = Recipe.numRecipes - 2; index3 > index1; --index3)
            Main.recipe[index3 + 1] = Main.recipe[index3];
          Main.recipe[index1 + 1] = recipe;
          Main.recipe[index1 + 1].notDecraftable = true;
        }
      }
    }

    private static void CreateReverseWallRecipes()
    {
      int numRecipes = Recipe.numRecipes;
      for (int index1 = 0; index1 < numRecipes; ++index1)
      {
        if (Main.recipe[index1].createItem.createWall > 0 && Main.recipe[index1].requiredItem[1].type == 0 && Main.recipe[index1].requiredItem[0].createWall == -1)
        {
          Recipe.currentRecipe.createItem.SetDefaults(Main.recipe[index1].requiredItem[0].type);
          Recipe.currentRecipe.createItem.stack = Main.recipe[index1].requiredItem[0].stack;
          Recipe.currentRecipe.requiredItem[0].SetDefaults(Main.recipe[index1].createItem.type);
          Recipe.currentRecipe.requiredItem[0].stack = Main.recipe[index1].createItem.stack;
          for (int index2 = 0; index2 < Recipe.currentRecipe.requiredTile.Length; ++index2)
            Recipe.currentRecipe.requiredTile[index2] = Main.recipe[index1].requiredTile[index2];
          Recipe.AddRecipe();
          Recipe recipe = Main.recipe[Recipe.numRecipes - 1];
          for (int index3 = Recipe.numRecipes - 2; index3 > index1; --index3)
            Main.recipe[index3 + 1] = Main.recipe[index3];
          Main.recipe[index1 + 1] = recipe;
          Main.recipe[index1 + 1].notDecraftable = true;
        }
      }
    }

    public void SetIngredients(params int[] ingredients)
    {
      if (ingredients.Length == 1)
        ingredients = new int[2]{ ingredients[0], 1 };
      if (ingredients.Length % 2 != 0)
        throw new Exception("Bad ingredients amount");
      for (int index1 = 0; index1 < ingredients.Length; index1 += 2)
      {
        int index2 = index1 / 2;
        this.requiredItem[index2].SetDefaults(ingredients[index1]);
        this.requiredItem[index2].stack = ingredients[index1 + 1];
      }
    }

    public void SetCraftingStation(params int[] tileIDs)
    {
      for (int index = 0; index < tileIDs.Length; ++index)
        this.requiredTile[index] = tileIDs[index];
    }

    private static void AddRecipe()
    {
      if (Recipe.currentRecipe.requiredTile[0] == 13)
        Recipe.currentRecipe.alchemy = true;
      Main.recipe[Recipe.numRecipes] = Recipe.currentRecipe;
      Recipe.currentRecipe = new Recipe();
      ++Recipe.numRecipes;
    }

    public static int GetRequiredTileStyle(int tileID) => tileID == 26 && WorldGen.crimson ? 1 : 0;

    public bool ContainsIngredient(int itemType)
    {
      foreach (Recipe.RequiredItemEntry requiredItemEntry in this.requiredItemQuickLookup)
      {
        if (requiredItemEntry.itemIdOrRecipeGroup != 0)
        {
          if (requiredItemEntry.itemIdOrRecipeGroup == itemType)
            return true;
        }
        else
          break;
      }
      return false;
    }

    private struct RequiredItemEntry
    {
      public int itemIdOrRecipeGroup;
      public int stack;
    }
  }
}
