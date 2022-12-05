// Decompiled with JetBrains decompiler
// Type: Terraria.ID.PlayerItemSlotID
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;

namespace Terraria.ID
{
  public static class PlayerItemSlotID
  {
    public static readonly int Inventory0;
    public static readonly int InventoryMouseItem;
    public static readonly int Armor0;
    public static readonly int Dye0;
    public static readonly int Misc0;
    public static readonly int MiscDye0;
    public static readonly int Bank1_0;
    public static readonly int Bank2_0;
    public static readonly int TrashItem;
    public static readonly int Bank3_0;
    public static readonly int Bank4_0;
    public static readonly int Loadout1_Armor_0;
    public static readonly int Loadout1_Dye_0;
    public static readonly int Loadout2_Armor_0;
    public static readonly int Loadout2_Dye_0;
    public static readonly int Loadout3_Armor_0;
    public static readonly int Loadout3_Dye_0;
    public static bool[] CanRelay = new bool[0];
    private static int _nextSlotId;

    static PlayerItemSlotID()
    {
      PlayerItemSlotID.Inventory0 = PlayerItemSlotID.AllocateSlots(58, true);
      PlayerItemSlotID.InventoryMouseItem = PlayerItemSlotID.AllocateSlots(1, true);
      PlayerItemSlotID.Armor0 = PlayerItemSlotID.AllocateSlots(20, true);
      PlayerItemSlotID.Dye0 = PlayerItemSlotID.AllocateSlots(10, true);
      PlayerItemSlotID.Misc0 = PlayerItemSlotID.AllocateSlots(5, true);
      PlayerItemSlotID.MiscDye0 = PlayerItemSlotID.AllocateSlots(5, true);
      PlayerItemSlotID.Bank1_0 = PlayerItemSlotID.AllocateSlots(40, false);
      PlayerItemSlotID.Bank2_0 = PlayerItemSlotID.AllocateSlots(40, false);
      PlayerItemSlotID.TrashItem = PlayerItemSlotID.AllocateSlots(1, false);
      PlayerItemSlotID.Bank3_0 = PlayerItemSlotID.AllocateSlots(40, false);
      PlayerItemSlotID.Bank4_0 = PlayerItemSlotID.AllocateSlots(40, true);
      PlayerItemSlotID.Loadout1_Armor_0 = PlayerItemSlotID.AllocateSlots(20, true);
      PlayerItemSlotID.Loadout1_Dye_0 = PlayerItemSlotID.AllocateSlots(10, true);
      PlayerItemSlotID.Loadout2_Armor_0 = PlayerItemSlotID.AllocateSlots(20, true);
      PlayerItemSlotID.Loadout2_Dye_0 = PlayerItemSlotID.AllocateSlots(10, true);
      PlayerItemSlotID.Loadout3_Armor_0 = PlayerItemSlotID.AllocateSlots(20, true);
      PlayerItemSlotID.Loadout3_Dye_0 = PlayerItemSlotID.AllocateSlots(10, true);
    }

    private static int AllocateSlots(int amount, bool canNetRelay)
    {
      int nextSlotId = PlayerItemSlotID._nextSlotId;
      PlayerItemSlotID._nextSlotId += amount;
      int length = PlayerItemSlotID.CanRelay.Length;
      Array.Resize<bool>(ref PlayerItemSlotID.CanRelay, length + amount);
      for (int index = length; index < PlayerItemSlotID._nextSlotId; ++index)
        PlayerItemSlotID.CanRelay[index] = canNetRelay;
      return nextSlotId;
    }
  }
}
