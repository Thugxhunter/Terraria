// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIBestiaryNPCEntryPortrait
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria.GameContent.Bestiary;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIBestiaryNPCEntryPortrait : UIElement
  {
    public BestiaryEntry Entry { get; private set; }

    public UIBestiaryNPCEntryPortrait(
      BestiaryEntry entry,
      Asset<Texture2D> portraitBackgroundAsset,
      Color portraitColor,
      List<IBestiaryBackgroundOverlayAndColorProvider> overlays)
    {
      this.Entry = entry;
      this.Height.Set(112f, 0.0f);
      this.Width.Set(193f, 0.0f);
      this.SetPadding(0.0f);
      UIElement element1 = new UIElement()
      {
        Width = new StyleDimension(-4f, 1f),
        Height = new StyleDimension(-4f, 1f),
        IgnoresMouseInteraction = true,
        OverflowHidden = true,
        HAlign = 0.5f,
        VAlign = 0.5f
      };
      element1.SetPadding(0.0f);
      if (portraitBackgroundAsset != null)
      {
        UIElement uiElement = element1;
        UIImage element2 = new UIImage(portraitBackgroundAsset);
        element2.HAlign = 0.5f;
        element2.VAlign = 0.5f;
        element2.ScaleToFit = true;
        element2.Width = new StyleDimension(0.0f, 1f);
        element2.Height = new StyleDimension(0.0f, 1f);
        element2.Color = portraitColor;
        uiElement.Append((UIElement) element2);
      }
      for (int index = 0; index < overlays.Count; ++index)
      {
        Asset<Texture2D> backgroundOverlayImage = overlays[index].GetBackgroundOverlayImage();
        Color? backgroundOverlayColor = overlays[index].GetBackgroundOverlayColor();
        UIElement uiElement = element1;
        UIImage element3 = new UIImage(backgroundOverlayImage);
        element3.HAlign = 0.5f;
        element3.VAlign = 0.5f;
        element3.ScaleToFit = true;
        element3.Width = new StyleDimension(0.0f, 1f);
        element3.Height = new StyleDimension(0.0f, 1f);
        element3.Color = backgroundOverlayColor.HasValue ? backgroundOverlayColor.Value : Color.Lerp(Color.White, portraitColor, 0.5f);
        uiElement.Append((UIElement) element3);
      }
      UIBestiaryEntryIcon element4 = new UIBestiaryEntryIcon(entry, true);
      element1.Append((UIElement) element4);
      this.Append(element1);
      UIImage element5 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Portrait_Front", (AssetRequestMode) 1));
      element5.VAlign = 0.5f;
      element5.HAlign = 0.5f;
      element5.IgnoresMouseInteraction = true;
      this.Append((UIElement) element5);
    }
  }
}
