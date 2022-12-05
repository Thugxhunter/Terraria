// Decompiled with JetBrains decompiler
// Type: Terraria.EquipmentLoadout
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.IO;
using Terraria.DataStructures;

namespace Terraria
{
  public class EquipmentLoadout : IFixLoadedData
  {
    public Item[] Armor;
    public Item[] Dye;
    public bool[] Hide;

    public EquipmentLoadout()
    {
      this.Armor = this.CreateItemArray(20);
      this.Dye = this.CreateItemArray(10);
      this.Hide = new bool[10];
    }

    private Item[] CreateItemArray(int length)
    {
      Item[] itemArray = new Item[length];
      for (int index = 0; index < length; ++index)
        itemArray[index] = new Item();
      return itemArray;
    }

    public void Serialize(BinaryWriter writer)
    {
      ItemSerializationContext context = ItemSerializationContext.SavingAndLoading;
      for (int index = 0; index < this.Armor.Length; ++index)
        this.Armor[index].Serialize(writer, context);
      for (int index = 0; index < this.Dye.Length; ++index)
        this.Dye[index].Serialize(writer, context);
      for (int index = 0; index < this.Hide.Length; ++index)
        writer.Write(this.Hide[index]);
    }

    public void Deserialize(BinaryReader reader, int gameVersion)
    {
      ItemSerializationContext context = ItemSerializationContext.SavingAndLoading;
      for (int index = 0; index < this.Armor.Length; ++index)
        this.Armor[index].DeserializeFrom(reader, context);
      for (int index = 0; index < this.Dye.Length; ++index)
        this.Dye[index].DeserializeFrom(reader, context);
      for (int index = 0; index < this.Hide.Length; ++index)
        this.Hide[index] = reader.ReadBoolean();
    }

    public void Swap(Player player)
    {
      Item[] armor = player.armor;
      for (int index = 0; index < armor.Length; ++index)
        Utils.Swap<Item>(ref armor[index], ref this.Armor[index]);
      Item[] dye = player.dye;
      for (int index = 0; index < dye.Length; ++index)
        Utils.Swap<Item>(ref dye[index], ref this.Dye[index]);
      bool[] visibleAccessory = player.hideVisibleAccessory;
      for (int index = 0; index < visibleAccessory.Length; ++index)
        Utils.Swap<bool>(ref visibleAccessory[index], ref this.Hide[index]);
    }

    public void TryDroppingItems(Player player, IEntitySource source)
    {
      for (int index = 0; index < this.Armor.Length; ++index)
        player.TryDroppingSingleItem(source, this.Armor[index]);
      for (int index = 0; index < this.Dye.Length; ++index)
        player.TryDroppingSingleItem(source, this.Dye[index]);
    }

    public void FixLoadedData()
    {
      for (int index = 0; index < this.Armor.Length; ++index)
        this.Armor[index].FixAgainstExploit();
      for (int index = 0; index < this.Dye.Length; ++index)
        this.Dye[index].FixAgainstExploit();
      Player.FixLoadedData_EliminiateDuplicateAccessories(this.Armor);
    }
  }
}
