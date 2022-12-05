// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Achievements.AchievementsHelper
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;

namespace Terraria.GameContent.Achievements
{
  public class AchievementsHelper
  {
    private static bool _isMining;
    private static bool mayhemOK;
    private static bool mayhem1down;
    private static bool mayhem2down;
    private static bool mayhem3down;

    public static event AchievementsHelper.ItemPickupEvent OnItemPickup;

    public static event AchievementsHelper.ItemCraftEvent OnItemCraft;

    public static event AchievementsHelper.TileDestroyedEvent OnTileDestroyed;

    public static event AchievementsHelper.NPCKilledEvent OnNPCKilled;

    public static event AchievementsHelper.ProgressionEventEvent OnProgressionEvent;

    public static bool CurrentlyMining
    {
      get => AchievementsHelper._isMining;
      set => AchievementsHelper._isMining = value;
    }

    public static void NotifyTileDestroyed(Player player, ushort tile)
    {
      if (Main.gameMenu || !AchievementsHelper._isMining || AchievementsHelper.OnTileDestroyed == null)
        return;
      AchievementsHelper.OnTileDestroyed(player, tile);
    }

    public static void NotifyItemPickup(Player player, Item item)
    {
      if (AchievementsHelper.OnItemPickup == null)
        return;
      AchievementsHelper.OnItemPickup(player, (short) item.netID, item.stack);
    }

    public static void NotifyItemPickup(Player player, Item item, int customStack)
    {
      if (AchievementsHelper.OnItemPickup == null)
        return;
      AchievementsHelper.OnItemPickup(player, (short) item.netID, customStack);
    }

    public static void NotifyItemCraft(Recipe recipe)
    {
      if (AchievementsHelper.OnItemCraft == null)
        return;
      AchievementsHelper.OnItemCraft((short) recipe.createItem.netID, recipe.createItem.stack);
    }

    public static void Initialize() => Player.Hooks.OnEnterWorld += new Action<Player>(AchievementsHelper.OnPlayerEnteredWorld);

    internal static void OnPlayerEnteredWorld(Player player)
    {
      if (AchievementsHelper.OnItemPickup != null)
      {
        for (int index = 0; index < 58; ++index)
          AchievementsHelper.OnItemPickup(player, (short) player.inventory[index].type, player.inventory[index].stack);
        for (int index = 0; index < player.armor.Length; ++index)
          AchievementsHelper.OnItemPickup(player, (short) player.armor[index].type, player.armor[index].stack);
        for (int index = 0; index < player.dye.Length; ++index)
          AchievementsHelper.OnItemPickup(player, (short) player.dye[index].type, player.dye[index].stack);
        for (int index = 0; index < player.miscEquips.Length; ++index)
          AchievementsHelper.OnItemPickup(player, (short) player.miscEquips[index].type, player.miscEquips[index].stack);
        for (int index = 0; index < player.miscDyes.Length; ++index)
          AchievementsHelper.OnItemPickup(player, (short) player.miscDyes[index].type, player.miscDyes[index].stack);
        for (int index = 0; index < player.bank.item.Length; ++index)
          AchievementsHelper.OnItemPickup(player, (short) player.bank.item[index].type, player.bank.item[index].stack);
        for (int index = 0; index < player.bank2.item.Length; ++index)
          AchievementsHelper.OnItemPickup(player, (short) player.bank2.item[index].type, player.bank2.item[index].stack);
        for (int index = 0; index < player.bank3.item.Length; ++index)
          AchievementsHelper.OnItemPickup(player, (short) player.bank3.item[index].type, player.bank3.item[index].stack);
        for (int index = 0; index < player.bank4.item.Length; ++index)
          AchievementsHelper.OnItemPickup(player, (short) player.bank4.item[index].type, player.bank4.item[index].stack);
        for (int index1 = 0; index1 < player.Loadouts.Length; ++index1)
        {
          Item[] armor = player.Loadouts[index1].Armor;
          for (int index2 = 0; index2 < armor.Length; ++index2)
            AchievementsHelper.OnItemPickup(player, (short) armor[index2].type, armor[index2].stack);
          Item[] dye = player.Loadouts[index1].Dye;
          for (int index3 = 0; index3 < dye.Length; ++index3)
            AchievementsHelper.OnItemPickup(player, (short) dye[index3].type, dye[index3].stack);
        }
      }
      if (player.statManaMax > 20)
        Main.Achievements.GetCondition("STAR_POWER", "Use").Complete();
      if (player.statLifeMax == 500 && player.statManaMax == 200)
        Main.Achievements.GetCondition("TOPPED_OFF", "Use").Complete();
      if (player.miscEquips[4].type > 0)
        Main.Achievements.GetCondition("HOLD_ON_TIGHT", "Equip").Complete();
      if (player.miscEquips[3].type > 0)
        Main.Achievements.GetCondition("THE_CAVALRY", "Equip").Complete();
      for (int index = 0; index < player.armor.Length; ++index)
      {
        if (player.armor[index].wingSlot > (sbyte) 0)
        {
          Main.Achievements.GetCondition("HEAD_IN_THE_CLOUDS", "Equip").Complete();
          break;
        }
      }
      if (player.armor[0].stack > 0 && player.armor[1].stack > 0 && player.armor[2].stack > 0)
        Main.Achievements.GetCondition("MATCHING_ATTIRE", "Equip").Complete();
      if (player.armor[10].stack > 0 && player.armor[11].stack > 0 && player.armor[12].stack > 0)
        Main.Achievements.GetCondition("FASHION_STATEMENT", "Equip").Complete();
      bool flag = true;
      for (int slot = 0; slot < 10; ++slot)
      {
        if (player.IsItemSlotUnlockedAndUsable(slot) && (player.dye[slot].type < 1 || player.dye[slot].stack < 1))
          flag = false;
      }
      if (flag)
        Main.Achievements.GetCondition("DYE_HARD", "Equip").Complete();
      if (player.unlockedBiomeTorches)
        Main.Achievements.GetCondition("GAIN_TORCH_GODS_FAVOR", "Use").Complete();
      WorldGen.CheckAchievement_RealEstateAndTownSlimes();
    }

    public static void NotifyNPCKilled(NPC npc)
    {
      if (Main.netMode == 0)
      {
        if (!npc.playerInteraction[Main.myPlayer])
          return;
        AchievementsHelper.NotifyNPCKilledDirect(Main.player[Main.myPlayer], npc.netID);
      }
      else
      {
        for (int remoteClient = 0; remoteClient < (int) byte.MaxValue; ++remoteClient)
        {
          if (npc.playerInteraction[remoteClient])
            NetMessage.SendData(97, remoteClient, number: npc.netID);
        }
      }
    }

    public static void NotifyNPCKilledDirect(Player player, int npcNetID)
    {
      if (AchievementsHelper.OnNPCKilled == null)
        return;
      AchievementsHelper.OnNPCKilled(player, (short) npcNetID);
    }

    public static void NotifyProgressionEvent(int eventID)
    {
      if (Main.netMode == 2)
      {
        NetMessage.SendData(98, number: eventID);
      }
      else
      {
        if (AchievementsHelper.OnProgressionEvent == null)
          return;
        AchievementsHelper.OnProgressionEvent(eventID);
      }
    }

    public static void HandleOnEquip(Player player, Item item, int context)
    {
      if (context == 16)
        Main.Achievements.GetCondition("HOLD_ON_TIGHT", "Equip").Complete();
      if (context == 17)
        Main.Achievements.GetCondition("THE_CAVALRY", "Equip").Complete();
      if ((context == 10 || context == 11) && item.wingSlot > (sbyte) 0)
        Main.Achievements.GetCondition("HEAD_IN_THE_CLOUDS", "Equip").Complete();
      if (context == 8 && player.armor[0].stack > 0 && player.armor[1].stack > 0 && player.armor[2].stack > 0)
        Main.Achievements.GetCondition("MATCHING_ATTIRE", "Equip").Complete();
      if (context == 9 && player.armor[10].stack > 0 && player.armor[11].stack > 0 && player.armor[12].stack > 0)
        Main.Achievements.GetCondition("FASHION_STATEMENT", "Equip").Complete();
      if (context != 12 && context != 33)
        return;
      for (int slot = 0; slot < 10; ++slot)
      {
        if (player.IsItemSlotUnlockedAndUsable(slot) && (player.dye[slot].type < 1 || player.dye[slot].stack < 1))
          return;
      }
      for (int index = 0; index < player.miscDyes.Length; ++index)
      {
        if (player.miscDyes[index].type < 1 || player.miscDyes[index].stack < 1)
          return;
      }
      Main.Achievements.GetCondition("DYE_HARD", "Equip").Complete();
    }

    public static void HandleSpecialEvent(Player player, int eventID)
    {
      if (player.whoAmI != Main.myPlayer)
        return;
      switch (eventID)
      {
        case 1:
          Main.Achievements.GetCondition("STAR_POWER", "Use").Complete();
          if (player.statLifeMax != 500 || player.statManaMax != 200)
            break;
          Main.Achievements.GetCondition("TOPPED_OFF", "Use").Complete();
          break;
        case 2:
          Main.Achievements.GetCondition("GET_A_LIFE", "Use").Complete();
          if (player.statLifeMax != 500 || player.statManaMax != 200)
            break;
          Main.Achievements.GetCondition("TOPPED_OFF", "Use").Complete();
          break;
        case 3:
          Main.Achievements.GetCondition("NOT_THE_BEES", "Use").Complete();
          break;
        case 4:
          Main.Achievements.GetCondition("WATCH_YOUR_STEP", "Hit").Complete();
          break;
        case 5:
          Main.Achievements.GetCondition("RAINBOWS_AND_UNICORNS", "Use").Complete();
          break;
        case 6:
          Main.Achievements.GetCondition("YOU_AND_WHAT_ARMY", "Spawn").Complete();
          break;
        case 7:
          Main.Achievements.GetCondition("THROWING_LINES", "Use").Complete();
          break;
        case 8:
          Main.Achievements.GetCondition("LUCKY_BREAK", "Hit").Complete();
          break;
        case 9:
          Main.Achievements.GetCondition("VEHICULAR_MANSLAUGHTER", "Hit").Complete();
          break;
        case 10:
          Main.Achievements.GetCondition("ROCK_BOTTOM", "Reach").Complete();
          break;
        case 11:
          Main.Achievements.GetCondition("INTO_ORBIT", "Reach").Complete();
          break;
        case 12:
          Main.Achievements.GetCondition("WHERES_MY_HONEY", "Reach").Complete();
          break;
        case 13:
          Main.Achievements.GetCondition("JEEPERS_CREEPERS", "Reach").Complete();
          break;
        case 14:
          Main.Achievements.GetCondition("ITS_GETTING_HOT_IN_HERE", "Reach").Complete();
          break;
        case 15:
          Main.Achievements.GetCondition("FUNKYTOWN", "Reach").Complete();
          break;
        case 16:
          Main.Achievements.GetCondition("I_AM_LOOT", "Peek").Complete();
          break;
        case 17:
          Main.Achievements.GetCondition("FLY_A_KITE_ON_A_WINDY_DAY", "Use").Complete();
          break;
        case 18:
          Main.Achievements.GetCondition("FOUND_GRAVEYARD", "Reach").Complete();
          break;
        case 19:
          Main.Achievements.GetCondition("GO_LAVA_FISHING", "Do").Complete();
          break;
        case 20:
          Main.Achievements.GetCondition("TALK_TO_NPC_AT_MAX_HAPPINESS", "Do").Complete();
          break;
        case 21:
          Main.Achievements.GetCondition("PET_THE_PET", "Do").Complete();
          break;
        case 22:
          Main.Achievements.GetCondition("FIND_A_FAIRY", "Do").Complete();
          break;
        case 23:
          Main.Achievements.GetCondition("DIE_TO_DEAD_MANS_CHEST", "Do").Complete();
          break;
        case 24:
          Main.Achievements.GetCondition("GAIN_TORCH_GODS_FAVOR", "Use").Complete();
          break;
        case 25:
          Main.Achievements.GetCondition("DRINK_BOTTLED_WATER_WHILE_DROWNING", "Use").Complete();
          break;
        case 26:
          Main.Achievements.GetCondition("PLAY_ON_A_SPECIAL_SEED", "Do").Complete();
          break;
        case 27:
          Main.Achievements.GetCondition("PURIFY_ENTIRE_WORLD", "Do").Complete();
          break;
      }
    }

    public static void HandleNurseService(int coinsSpent) => ((CustomFloatCondition) Main.Achievements.GetCondition("FREQUENT_FLYER", "Pay")).Value += (float) coinsSpent;

    public static void HandleAnglerService()
    {
      Main.Achievements.GetCondition("SERVANT_IN_TRAINING", "Finish").Complete();
      ++((CustomIntCondition) Main.Achievements.GetCondition("GOOD_LITTLE_SLAVE", "Finish")).Value;
      ++((CustomIntCondition) Main.Achievements.GetCondition("TROUT_MONKEY", "Finish")).Value;
      ++((CustomIntCondition) Main.Achievements.GetCondition("FAST_AND_FISHIOUS", "Finish")).Value;
      ++((CustomIntCondition) Main.Achievements.GetCondition("SUPREME_HELPER_MINION", "Finish")).Value;
    }

    public static void HandleRunning(float pixelsMoved) => ((CustomFloatCondition) Main.Achievements.GetCondition("MARATHON_MEDALIST", "Move")).Value += pixelsMoved;

    public static void HandleMining() => ++((CustomIntCondition) Main.Achievements.GetCondition("BULLDOZER", "Pick")).Value;

    public static void CheckMechaMayhem(int justKilled = -1)
    {
      if (!AchievementsHelper.mayhemOK)
      {
        if (!NPC.AnyNPCs((int) sbyte.MaxValue) || !NPC.AnyNPCs(134) || !NPC.AnyNPCs(126) || !NPC.AnyNPCs(125))
          return;
        AchievementsHelper.mayhemOK = true;
        AchievementsHelper.mayhem1down = false;
        AchievementsHelper.mayhem2down = false;
        AchievementsHelper.mayhem3down = false;
      }
      else
      {
        if (justKilled == 125 || justKilled == 126)
          AchievementsHelper.mayhem1down = true;
        else if (!NPC.AnyNPCs(125) && !NPC.AnyNPCs(126) && !AchievementsHelper.mayhem1down)
        {
          AchievementsHelper.mayhemOK = false;
          return;
        }
        if (justKilled == 134)
          AchievementsHelper.mayhem2down = true;
        else if (!NPC.AnyNPCs(134) && !AchievementsHelper.mayhem2down)
        {
          AchievementsHelper.mayhemOK = false;
          return;
        }
        if (justKilled == (int) sbyte.MaxValue)
          AchievementsHelper.mayhem3down = true;
        else if (!NPC.AnyNPCs((int) sbyte.MaxValue) && !AchievementsHelper.mayhem3down)
        {
          AchievementsHelper.mayhemOK = false;
          return;
        }
        if (!AchievementsHelper.mayhem1down || !AchievementsHelper.mayhem2down || !AchievementsHelper.mayhem3down)
          return;
        AchievementsHelper.NotifyProgressionEvent(21);
      }
    }

    public delegate void ItemPickupEvent(Player player, short itemId, int count);

    public delegate void ItemCraftEvent(short itemId, int count);

    public delegate void TileDestroyedEvent(Player player, ushort tileId);

    public delegate void NPCKilledEvent(Player player, short npcId);

    public delegate void ProgressionEventEvent(int eventID);
  }
}
