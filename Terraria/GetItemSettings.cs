// Decompiled with JetBrains decompiler
// Type: Terraria.GetItemSettings
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;

namespace Terraria
{
  public struct GetItemSettings
  {
    public static GetItemSettings InventoryEntityToPlayerInventorySettings = new GetItemSettings(NoText: true);
    public static GetItemSettings NPCEntityToPlayerInventorySettings = new GetItemSettings(true);
    public static GetItemSettings LootAllSettings = new GetItemSettings();
    public static GetItemSettings LootAllSettingsRegularChest = new GetItemSettings(CanGoIntoVoidVault: true);
    public static GetItemSettings PickupItemFromWorld = new GetItemSettings(CanGoIntoVoidVault: true);
    public static GetItemSettings GetItemInDropItemCheck = new GetItemSettings(NoText: true);
    public static GetItemSettings InventoryUIToInventorySettings = new GetItemSettings();
    public static GetItemSettings InventoryUIToInventorySettingsShowAsNew = new GetItemSettings(NoText: true, StepAfterHandlingSlotNormally: new Action<Item>(GetItemSettings.MakeNewAndShiny));
    public static GetItemSettings ItemCreatedFromItemUsage = new GetItemSettings();
    public readonly bool LongText;
    public readonly bool NoText;
    public readonly bool CanGoIntoVoidVault;
    public readonly Action<Item> StepAfterHandlingSlotNormally;

    public GetItemSettings(
      bool LongText = false,
      bool NoText = false,
      bool CanGoIntoVoidVault = false,
      Action<Item> StepAfterHandlingSlotNormally = null)
    {
      this.LongText = LongText;
      this.NoText = NoText;
      this.CanGoIntoVoidVault = CanGoIntoVoidVault;
      this.StepAfterHandlingSlotNormally = StepAfterHandlingSlotNormally;
    }

    public void HandlePostAction(Item item)
    {
      if (this.StepAfterHandlingSlotNormally == null)
        return;
      this.StepAfterHandlingSlotNormally(item);
    }

    private static void MakeNewAndShiny(Item item) => item.newAndShiny = true;
  }
}
