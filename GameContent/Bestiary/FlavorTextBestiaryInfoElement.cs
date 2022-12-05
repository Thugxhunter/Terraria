// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Bestiary.FlavorTextBestiaryInfoElement
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
  public class FlavorTextBestiaryInfoElement : IBestiaryInfoElement
  {
    private string _key;

    public FlavorTextBestiaryInfoElement(string languageKey) => this._key = languageKey;

    public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
    {
      if (info.UnlockState < BestiaryEntryUnlockState.CanShowStats_2)
        return (UIElement) null;
      UIPanel container = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel", (AssetRequestMode) 1), (Asset<Texture2D>) null, customBarSize: 7);
      container.Width = new StyleDimension(-11f, 1f);
      container.Height = new StyleDimension(109f, 0.0f);
      container.BackgroundColor = new Color(43, 56, 101);
      container.BorderColor = Color.Transparent;
      container.Left = new StyleDimension(3f, 0.0f);
      container.PaddingLeft = 4f;
      container.PaddingRight = 4f;
      UIText uiText1 = new UIText(Language.GetText(this._key), 0.8f);
      uiText1.HAlign = 0.0f;
      uiText1.VAlign = 0.0f;
      uiText1.Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiText1.Height = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiText1.IsWrapped = true;
      UIText uiText2 = uiText1;
      FlavorTextBestiaryInfoElement.AddDynamicResize((UIElement) container, uiText2);
      container.Append((UIElement) uiText2);
      return (UIElement) container;
    }

    private static void AddDynamicResize(UIElement container, UIText text) => text.OnInternalTextChange += (Action) (() => container.Height = new StyleDimension(text.MinHeight.Pixels, 0.0f));
  }
}
