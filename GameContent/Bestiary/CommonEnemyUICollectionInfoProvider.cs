// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Bestiary.CommonEnemyUICollectionInfoProvider
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
  public class CommonEnemyUICollectionInfoProvider : IBestiaryUICollectionInfoProvider
  {
    private string _persistentIdentifierToCheck;
    private bool _quickUnlock;
    private int _killCountNeededToFullyUnlock;

    public CommonEnemyUICollectionInfoProvider(string persistentId, bool quickUnlock)
    {
      this._persistentIdentifierToCheck = persistentId;
      this._quickUnlock = quickUnlock;
      this._killCountNeededToFullyUnlock = CommonEnemyUICollectionInfoProvider.GetKillCountNeeded(persistentId);
    }

    public static int GetKillCountNeeded(string persistentId)
    {
      int killsForBannerNeeded = ItemID.Sets.DefaultKillsForBannerNeeded;
      int key;
      NPC npc;
      if (!ContentSamples.NpcNetIdsByPersistentIds.TryGetValue(persistentId, out key) || !ContentSamples.NpcsByNetId.TryGetValue(key, out npc))
        return killsForBannerNeeded;
      int index = Item.BannerToItem(Item.NPCtoBanner(npc.BannerID()));
      return ItemID.Sets.KillsToBanner[index];
    }

    public BestiaryUICollectionInfo GetEntryUICollectionInfo()
    {
      BestiaryEntryUnlockState stateByKillCount = this.GetUnlockStateByKillCount(Main.BestiaryTracker.Kills.GetKillCount(this._persistentIdentifierToCheck), this._quickUnlock);
      return new BestiaryUICollectionInfo()
      {
        UnlockState = stateByKillCount
      };
    }

    public BestiaryEntryUnlockState GetUnlockStateByKillCount(
      int killCount,
      bool quickUnlock)
    {
      int neededToFullyUnlock = this._killCountNeededToFullyUnlock;
      return CommonEnemyUICollectionInfoProvider.GetUnlockStateByKillCount(killCount, quickUnlock, neededToFullyUnlock);
    }

    public static BestiaryEntryUnlockState GetUnlockStateByKillCount(
      int killCount,
      bool quickUnlock,
      int fullKillCountNeeded)
    {
      int num1 = fullKillCountNeeded / 2;
      int num2 = fullKillCountNeeded / 5;
      return !quickUnlock || killCount <= 0 ? (killCount < fullKillCountNeeded ? (killCount < num1 ? (killCount < num2 ? (killCount < 1 ? BestiaryEntryUnlockState.NotKnownAtAll_0 : BestiaryEntryUnlockState.CanShowPortraitOnly_1) : BestiaryEntryUnlockState.CanShowStats_2) : BestiaryEntryUnlockState.CanShowDropsWithoutDropRates_3) : BestiaryEntryUnlockState.CanShowDropsWithDropRates_4) : BestiaryEntryUnlockState.CanShowDropsWithDropRates_4;
    }

    public UIElement ProvideUIElement(BestiaryUICollectionInfo info) => (UIElement) null;
  }
}
