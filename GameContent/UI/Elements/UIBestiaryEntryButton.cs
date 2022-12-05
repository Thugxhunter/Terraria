// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIBestiaryEntryButton
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIBestiaryEntryButton : UIElement
  {
    private UIImage _bordersGlow;
    private UIImage _bordersOverlay;
    private UIImage _borders;
    private UIBestiaryEntryIcon _icon;

    public BestiaryEntry Entry { get; private set; }

    public UIBestiaryEntryButton(BestiaryEntry entry, bool isAPrettyPortrait)
    {
      this.Entry = entry;
      this.Height.Set(72f, 0.0f);
      this.Width.Set(72f, 0.0f);
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
      UIImage element2 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Slot_Back", (AssetRequestMode) 1));
      element2.VAlign = 0.5f;
      element2.HAlign = 0.5f;
      element1.Append((UIElement) element2);
      if (isAPrettyPortrait)
      {
        Asset<Texture2D> texture = this.TryGettingBackgroundImageProvider(entry);
        if (texture != null)
        {
          UIElement uiElement = element1;
          UIImage element3 = new UIImage(texture);
          element3.HAlign = 0.5f;
          element3.VAlign = 0.5f;
          uiElement.Append((UIElement) element3);
        }
      }
      UIBestiaryEntryIcon element4 = new UIBestiaryEntryIcon(entry, isAPrettyPortrait);
      element1.Append((UIElement) element4);
      this.Append(element1);
      this._icon = element4;
      int? nullable = this.TryGettingDisplayIndex(entry);
      if (nullable.HasValue)
      {
        UIText element5 = new UIText(nullable.Value.ToString(), 0.9f);
        element5.Top = new StyleDimension(10f, 0.0f);
        element5.Left = new StyleDimension(10f, 0.0f);
        element5.IgnoresMouseInteraction = true;
        this.Append((UIElement) element5);
      }
      UIImage uiImage1 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Slot_Selection", (AssetRequestMode) 1));
      uiImage1.VAlign = 0.5f;
      uiImage1.HAlign = 0.5f;
      uiImage1.IgnoresMouseInteraction = true;
      this._bordersGlow = uiImage1;
      UIImage uiImage2 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Slot_Overlay", (AssetRequestMode) 1));
      uiImage2.VAlign = 0.5f;
      uiImage2.HAlign = 0.5f;
      uiImage2.IgnoresMouseInteraction = true;
      uiImage2.Color = Color.White * 0.6f;
      this._bordersOverlay = uiImage2;
      this.Append((UIElement) this._bordersOverlay);
      UIImage element6 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Slot_Front", (AssetRequestMode) 1));
      element6.VAlign = 0.5f;
      element6.HAlign = 0.5f;
      element6.IgnoresMouseInteraction = true;
      this.Append((UIElement) element6);
      this._borders = element6;
      if (isAPrettyPortrait)
        this.RemoveChild((UIElement) this._bordersOverlay);
      if (isAPrettyPortrait)
        return;
      this.OnMouseOver += new UIElement.MouseEvent(this.MouseOver);
      this.OnMouseOut += new UIElement.MouseEvent(this.MouseOut);
    }

    private Asset<Texture2D> TryGettingBackgroundImageProvider(BestiaryEntry entry)
    {
      IEnumerable<IBestiaryBackgroundImagePathAndColorProvider> source = entry.Info.Where<IBestiaryInfoElement>((Func<IBestiaryInfoElement, bool>) (x => x is IBestiaryBackgroundImagePathAndColorProvider)).Select<IBestiaryInfoElement, IBestiaryBackgroundImagePathAndColorProvider>((Func<IBestiaryInfoElement, IBestiaryBackgroundImagePathAndColorProvider>) (x => x as IBestiaryBackgroundImagePathAndColorProvider));
      IEnumerable<IPreferenceProviderElement> preferences = entry.Info.OfType<IPreferenceProviderElement>();
      foreach (IBestiaryBackgroundImagePathAndColorProvider andColorProvider in source.Where<IBestiaryBackgroundImagePathAndColorProvider>((Func<IBestiaryBackgroundImagePathAndColorProvider, bool>) (provider => preferences.Any<IPreferenceProviderElement>((Func<IPreferenceProviderElement, bool>) (preference => preference.Matches(provider))))))
      {
        Asset<Texture2D> backgroundImage = andColorProvider.GetBackgroundImage();
        if (backgroundImage != null)
          return backgroundImage;
      }
      foreach (IBestiaryBackgroundImagePathAndColorProvider andColorProvider in source)
      {
        Asset<Texture2D> backgroundImage = andColorProvider.GetBackgroundImage();
        if (backgroundImage != null)
          return backgroundImage;
      }
      return (Asset<Texture2D>) null;
    }

    private int? TryGettingDisplayIndex(BestiaryEntry entry)
    {
      int? nullable = new int?();
      IBestiaryInfoElement bestiaryInfoElement = entry.Info.FirstOrDefault<IBestiaryInfoElement>((Func<IBestiaryInfoElement, bool>) (x => x is IBestiaryEntryDisplayIndex));
      if (bestiaryInfoElement != null)
        nullable = new int?((bestiaryInfoElement as IBestiaryEntryDisplayIndex).BestiaryDisplayIndex);
      return nullable;
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      if (!this.IsMouseHovering)
        return;
      Main.instance.MouseText(this._icon.GetHoverText());
    }

    private void MouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12);
      this.RemoveChild((UIElement) this._borders);
      this.RemoveChild((UIElement) this._bordersGlow);
      this.RemoveChild((UIElement) this._bordersOverlay);
      this.Append((UIElement) this._borders);
      this.Append((UIElement) this._bordersGlow);
      this._icon.ForceHover = true;
    }

    private void MouseOut(UIMouseEvent evt, UIElement listeningElement)
    {
      this.RemoveChild((UIElement) this._borders);
      this.RemoveChild((UIElement) this._bordersGlow);
      this.RemoveChild((UIElement) this._bordersOverlay);
      this.Append((UIElement) this._bordersOverlay);
      this.Append((UIElement) this._borders);
      this._icon.ForceHover = false;
    }
  }
}
