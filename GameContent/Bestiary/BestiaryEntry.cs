// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Bestiary.BestiaryEntry
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.Bestiary
{
  public class BestiaryEntry
  {
    public IEntryIcon Icon;
    public IBestiaryUICollectionInfoProvider UIInfoProvider;

    public List<IBestiaryInfoElement> Info { get; private set; }

    public BestiaryEntry() => this.Info = new List<IBestiaryInfoElement>();

    public static BestiaryEntry Enemy(int npcNetId)
    {
      NPC npc = ContentSamples.NpcsByNetId[npcNetId];
      List<IBestiaryInfoElement> bestiaryInfoElementList = new List<IBestiaryInfoElement>()
      {
        (IBestiaryInfoElement) new NPCNetIdBestiaryInfoElement(npcNetId),
        (IBestiaryInfoElement) new NamePlateInfoElement(Lang.GetNPCName(npcNetId).Key, npcNetId),
        (IBestiaryInfoElement) new NPCPortraitInfoElement(new int?(ContentSamples.NpcBestiaryRarityStars[npcNetId])),
        (IBestiaryInfoElement) new NPCKillCounterInfoElement(npcNetId)
      };
      bestiaryInfoElementList.Add((IBestiaryInfoElement) new NPCStatsReportInfoElement(npcNetId));
      if (npc.rarity != 0)
        bestiaryInfoElementList.Add((IBestiaryInfoElement) new RareSpawnBestiaryInfoElement(npc.rarity));
      IBestiaryUICollectionInfoProvider collectionInfoProvider;
      if (npc.boss || NPCID.Sets.ShouldBeCountedAsBoss[npc.type])
      {
        bestiaryInfoElementList.Add((IBestiaryInfoElement) new BossBestiaryInfoElement());
        collectionInfoProvider = (IBestiaryUICollectionInfoProvider) new CommonEnemyUICollectionInfoProvider(npc.GetBestiaryCreditId(), true);
      }
      else
        collectionInfoProvider = (IBestiaryUICollectionInfoProvider) new CommonEnemyUICollectionInfoProvider(npc.GetBestiaryCreditId(), false);
      string str = "Bestiary_FlavorText.npc_" + Lang.GetNPCName(npc.netID).Key.Replace("NPCName.", "");
      if (Language.Exists(str))
        bestiaryInfoElementList.Add((IBestiaryInfoElement) new FlavorTextBestiaryInfoElement(str));
      return new BestiaryEntry()
      {
        Icon = (IEntryIcon) new UnlockableNPCEntryIcon(npcNetId),
        Info = bestiaryInfoElementList,
        UIInfoProvider = collectionInfoProvider
      };
    }

    public static BestiaryEntry TownNPC(int npcNetId)
    {
      NPC npc = ContentSamples.NpcsByNetId[npcNetId];
      List<IBestiaryInfoElement> bestiaryInfoElementList = new List<IBestiaryInfoElement>()
      {
        (IBestiaryInfoElement) new NPCNetIdBestiaryInfoElement(npcNetId),
        (IBestiaryInfoElement) new NamePlateInfoElement(Lang.GetNPCName(npcNetId).Key, npcNetId),
        (IBestiaryInfoElement) new NPCPortraitInfoElement(new int?(ContentSamples.NpcBestiaryRarityStars[npcNetId])),
        (IBestiaryInfoElement) new NPCKillCounterInfoElement(npcNetId)
      };
      string str = "Bestiary_FlavorText.npc_" + Lang.GetNPCName(npc.netID).Key.Replace("NPCName.", "");
      if (Language.Exists(str))
        bestiaryInfoElementList.Add((IBestiaryInfoElement) new FlavorTextBestiaryInfoElement(str));
      return new BestiaryEntry()
      {
        Icon = (IEntryIcon) new UnlockableNPCEntryIcon(npcNetId),
        Info = bestiaryInfoElementList,
        UIInfoProvider = (IBestiaryUICollectionInfoProvider) new TownNPCUICollectionInfoProvider(npc.GetBestiaryCreditId())
      };
    }

    public static BestiaryEntry Critter(int npcNetId)
    {
      NPC npc = ContentSamples.NpcsByNetId[npcNetId];
      List<IBestiaryInfoElement> bestiaryInfoElementList = new List<IBestiaryInfoElement>()
      {
        (IBestiaryInfoElement) new NPCNetIdBestiaryInfoElement(npcNetId),
        (IBestiaryInfoElement) new NamePlateInfoElement(Lang.GetNPCName(npcNetId).Key, npcNetId),
        (IBestiaryInfoElement) new NPCPortraitInfoElement(new int?(ContentSamples.NpcBestiaryRarityStars[npcNetId])),
        (IBestiaryInfoElement) new NPCKillCounterInfoElement(npcNetId)
      };
      string str = "Bestiary_FlavorText.npc_" + Lang.GetNPCName(npc.netID).Key.Replace("NPCName.", "");
      if (Language.Exists(str))
        bestiaryInfoElementList.Add((IBestiaryInfoElement) new FlavorTextBestiaryInfoElement(str));
      return new BestiaryEntry()
      {
        Icon = (IEntryIcon) new UnlockableNPCEntryIcon(npcNetId),
        Info = bestiaryInfoElementList,
        UIInfoProvider = (IBestiaryUICollectionInfoProvider) new CritterUICollectionInfoProvider(npc.GetBestiaryCreditId())
      };
    }

    public static BestiaryEntry Biome(
      string nameLanguageKey,
      string texturePath,
      Func<bool> unlockCondition)
    {
      return new BestiaryEntry()
      {
        Icon = (IEntryIcon) new CustomEntryIcon(nameLanguageKey, texturePath, unlockCondition),
        Info = new List<IBestiaryInfoElement>()
      };
    }

    public void AddTags(params IBestiaryInfoElement[] elements) => this.Info.AddRange((IEnumerable<IBestiaryInfoElement>) elements);
  }
}
