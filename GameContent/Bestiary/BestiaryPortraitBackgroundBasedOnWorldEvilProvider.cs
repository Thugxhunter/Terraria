// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Bestiary.BestiaryPortraitBackgroundBasedOnWorldEvilProviderPreferenceInfoElement
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
  public class BestiaryPortraitBackgroundBasedOnWorldEvilProviderPreferenceInfoElement : 
    IPreferenceProviderElement,
    IBestiaryInfoElement
  {
    private IBestiaryBackgroundImagePathAndColorProvider _preferredProviderCorrupt;
    private IBestiaryBackgroundImagePathAndColorProvider _preferredProviderCrimson;

    public BestiaryPortraitBackgroundBasedOnWorldEvilProviderPreferenceInfoElement(
      IBestiaryBackgroundImagePathAndColorProvider preferredProviderCorrupt,
      IBestiaryBackgroundImagePathAndColorProvider preferredProviderCrimson)
    {
      this._preferredProviderCorrupt = preferredProviderCorrupt;
      this._preferredProviderCrimson = preferredProviderCrimson;
    }

    public UIElement ProvideUIElement(BestiaryUICollectionInfo info) => (UIElement) null;

    public bool Matches(
      IBestiaryBackgroundImagePathAndColorProvider provider)
    {
      return Main.ActiveWorldFileData == null || !WorldGen.crimson ? provider == this._preferredProviderCorrupt : provider == this._preferredProviderCrimson;
    }

    public IBestiaryBackgroundImagePathAndColorProvider GetPreferredProvider() => Main.ActiveWorldFileData == null || !WorldGen.crimson ? this._preferredProviderCorrupt : this._preferredProviderCrimson;
  }
}
