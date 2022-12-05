// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Bestiary.NamePlateInfoElement
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
  public class NamePlateInfoElement : IBestiaryInfoElement, IProvideSearchFilterString
  {
    private string _key;
    private int _npcNetId;

    public NamePlateInfoElement(string languageKey, int npcNetId)
    {
      this._key = languageKey;
      this._npcNetId = npcNetId;
    }

    public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
    {
      UIElement element = info.UnlockState != BestiaryEntryUnlockState.NotKnownAtAll_0 ? (UIElement) new UIText(Language.GetText(this._key)) : (UIElement) new UIText("???");
      element.HAlign = 0.5f;
      element.VAlign = 0.5f;
      element.Top = new StyleDimension(2f, 0.0f);
      element.IgnoresMouseInteraction = true;
      UIElement uiElement = new UIElement();
      uiElement.Width = new StyleDimension(0.0f, 1f);
      uiElement.Height = new StyleDimension(24f, 0.0f);
      uiElement.Append(element);
      return uiElement;
    }

    public string GetSearchString(ref BestiaryUICollectionInfo info) => info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0 ? (string) null : Language.GetText(this._key).Value;
  }
}
