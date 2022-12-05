// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Creative.ItemsSacrificedUnlocksTracker
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;
using System.IO;
using Terraria.ID;

namespace Terraria.GameContent.Creative
{
  public class ItemsSacrificedUnlocksTracker : IPersistentPerWorldContent, IOnPlayerJoining
  {
    public const int POSITIVE_SACRIFICE_COUNT_CAP = 9999;
    private Dictionary<string, int> _sacrificeCountByItemPersistentId;
    private Dictionary<int, int> _sacrificesCountByItemIdCache;

    public int LastEditId { get; private set; }

    public ItemsSacrificedUnlocksTracker()
    {
      this._sacrificeCountByItemPersistentId = new Dictionary<string, int>();
      this._sacrificesCountByItemIdCache = new Dictionary<int, int>();
      this.LastEditId = 0;
    }

    public int GetSacrificeCount(int itemId)
    {
      int num;
      if (ContentSamples.CreativeResearchItemPersistentIdOverride.TryGetValue(itemId, out num))
        itemId = num;
      int sacrificeCount;
      this._sacrificesCountByItemIdCache.TryGetValue(itemId, out sacrificeCount);
      return sacrificeCount;
    }

    public void FillListOfItemsThatCanBeObtainedInfinitely(List<int> toObtainInfinitely)
    {
      foreach (KeyValuePair<int, int> keyValuePair in this._sacrificesCountByItemIdCache)
      {
        int amountNeededTotal;
        if (this.TryGetSacrificeNumbers(keyValuePair.Key, out int _, out amountNeededTotal) && keyValuePair.Value >= amountNeededTotal)
          toObtainInfinitely.Add(keyValuePair.Key);
      }
    }

    public bool TryGetSacrificeNumbers(int itemId, out int amountWeHave, out int amountNeededTotal)
    {
      int num;
      if (ContentSamples.CreativeResearchItemPersistentIdOverride.TryGetValue(itemId, out num))
        itemId = num;
      amountWeHave = amountNeededTotal = 0;
      if (!CreativeItemSacrificesCatalog.Instance.TryGetSacrificeCountCapToUnlockInfiniteItems(itemId, out amountNeededTotal))
        return false;
      this._sacrificesCountByItemIdCache.TryGetValue(itemId, out amountWeHave);
      return true;
    }

    public void RegisterItemSacrifice(int itemId, int amount)
    {
      int num1;
      if (ContentSamples.CreativeResearchItemPersistentIdOverride.TryGetValue(itemId, out num1))
        itemId = num1;
      string key;
      if (!ContentSamples.ItemPersistentIdsByNetIds.TryGetValue(itemId, out key))
        return;
      int num2;
      this._sacrificeCountByItemPersistentId.TryGetValue(key, out num2);
      int num3 = Utils.Clamp<int>(num2 + amount, 0, 9999);
      this._sacrificeCountByItemPersistentId[key] = num3;
      this._sacrificesCountByItemIdCache[itemId] = num3;
      this.MarkContentsDirty();
    }

    public void SetSacrificeCountDirectly(string persistentId, int sacrificeCount)
    {
      int num = Utils.Clamp<int>(sacrificeCount, 0, 9999);
      this._sacrificeCountByItemPersistentId[persistentId] = num;
      int key;
      if (!ContentSamples.ItemNetIdsByPersistentIds.TryGetValue(persistentId, out key))
        return;
      this._sacrificesCountByItemIdCache[key] = num;
      this.MarkContentsDirty();
    }

    public void Save(BinaryWriter writer)
    {
      Dictionary<string, int> dictionary = new Dictionary<string, int>((IDictionary<string, int>) this._sacrificeCountByItemPersistentId);
      writer.Write(dictionary.Count);
      foreach (KeyValuePair<string, int> keyValuePair in dictionary)
      {
        writer.Write(keyValuePair.Key);
        writer.Write(keyValuePair.Value);
      }
    }

    public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
    {
      int num1 = reader.ReadInt32();
      for (int index = 0; index < num1; ++index)
      {
        string key1 = reader.ReadString();
        int num2 = reader.ReadInt32();
        int key2;
        if (ContentSamples.ItemNetIdsByPersistentIds.TryGetValue(key1, out key2))
        {
          int num3;
          if (ContentSamples.CreativeResearchItemPersistentIdOverride.TryGetValue(key2, out num3))
            key2 = num3;
          this._sacrificesCountByItemIdCache[key2] = num2;
          string str;
          if (ContentSamples.ItemPersistentIdsByNetIds.TryGetValue(key2, out str))
            key1 = str;
        }
        this._sacrificeCountByItemPersistentId[key1] = num2;
      }
    }

    public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
    {
      int num = reader.ReadInt32();
      for (int index = 0; index < num; ++index)
      {
        reader.ReadString();
        reader.ReadInt32();
      }
    }

    public void Reset()
    {
      this._sacrificeCountByItemPersistentId.Clear();
      this._sacrificesCountByItemIdCache.Clear();
      this.MarkContentsDirty();
    }

    public void OnPlayerJoining(int playerIndex)
    {
    }

    public void MarkContentsDirty() => ++this.LastEditId;
  }
}
