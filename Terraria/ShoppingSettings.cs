// Decompiled with JetBrains decompiler
// Type: Terraria.ShoppingSettings
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria
{
  public struct ShoppingSettings
  {
    public double PriceAdjustment;
    public string HappinessReport;

    public static ShoppingSettings NotInShop => new ShoppingSettings()
    {
      PriceAdjustment = 1.0,
      HappinessReport = ""
    };
  }
}
