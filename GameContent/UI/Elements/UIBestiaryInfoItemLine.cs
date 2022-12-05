// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIBestiaryInfoItemLine
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIBestiaryInfoItemLine : UIPanel, IManuallyOrderedUIElement
  {
    private Item _infoDisplayItem;
    private bool _hideMouseOver;

    public int OrderInUIList { get; set; }

    public UIBestiaryInfoItemLine(
      DropRateInfo info,
      BestiaryUICollectionInfo uiinfo,
      float textScale = 1f)
    {
      this._infoDisplayItem = new Item();
      this._infoDisplayItem.SetDefaults(info.itemId);
      this.SetBestiaryNotesOnItemCache(info);
      this.SetPadding(0.0f);
      this.PaddingLeft = 10f;
      this.PaddingRight = 10f;
      this.Width.Set(-14f, 1f);
      this.Height.Set(32f, 0.0f);
      this.Left.Set(5f, 0.0f);
      this.OnMouseOver += new UIElement.MouseEvent(this.MouseOver);
      this.OnMouseOut += new UIElement.MouseEvent(this.MouseOut);
      this.BorderColor = new Color(89, 116, 213, (int) byte.MaxValue);
      string stackRange;
      string droprate;
      this.GetDropInfo(info, uiinfo, out stackRange, out droprate);
      if (uiinfo.UnlockState < BestiaryEntryUnlockState.CanShowDropsWithoutDropRates_3)
      {
        this._hideMouseOver = true;
        Asset<Texture2D> texture = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Locked", (AssetRequestMode) 1);
        UIElement element1 = new UIElement()
        {
          Height = new StyleDimension(0.0f, 1f),
          Width = new StyleDimension(0.0f, 1f),
          HAlign = 0.5f,
          VAlign = 0.5f
        };
        element1.SetPadding(0.0f);
        UIImage uiImage = new UIImage(texture);
        uiImage.ImageScale = 0.55f;
        uiImage.HAlign = 0.5f;
        uiImage.VAlign = 0.5f;
        UIImage element2 = uiImage;
        element1.Append((UIElement) element2);
        this.Append(element1);
      }
      else
      {
        UIItemIcon element3 = new UIItemIcon(this._infoDisplayItem, uiinfo.UnlockState < BestiaryEntryUnlockState.CanShowDropsWithoutDropRates_3);
        element3.IgnoresMouseInteraction = true;
        element3.HAlign = 0.0f;
        element3.Left = new StyleDimension(4f, 0.0f);
        this.Append((UIElement) element3);
        if (!string.IsNullOrEmpty(stackRange))
          droprate = stackRange + " " + droprate;
        UITextPanel<string> element4 = new UITextPanel<string>(droprate, textScale);
        element4.IgnoresMouseInteraction = true;
        element4.DrawPanel = false;
        element4.HAlign = 1f;
        element4.Top = new StyleDimension(-4f, 0.0f);
        this.Append((UIElement) element4);
      }
    }

    protected void GetDropInfo(
      DropRateInfo dropRateInfo,
      BestiaryUICollectionInfo uiinfo,
      out string stackRange,
      out string droprate)
    {
      stackRange = dropRateInfo.stackMin == dropRateInfo.stackMax ? (dropRateInfo.stackMin != 1 ? " (" + (object) dropRateInfo.stackMin + ")" : "") : string.Format(" ({0}-{1})", (object) dropRateInfo.stackMin, (object) dropRateInfo.stackMax);
      string originalFormat = "P";
      if ((double) dropRateInfo.dropRate < 0.001)
        originalFormat = "P4";
      droprate = (double) dropRateInfo.dropRate == 1.0 ? "100%" : Utils.PrettifyPercentDisplay(dropRateInfo.dropRate, originalFormat);
      if (uiinfo.UnlockState == BestiaryEntryUnlockState.CanShowDropsWithDropRates_4)
        return;
      droprate = "???";
      stackRange = "";
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      base.DrawSelf(spriteBatch);
      if (!this.IsMouseHovering || this._hideMouseOver)
        return;
      this.DrawMouseOver();
    }

    private void DrawMouseOver()
    {
      Main.HoverItem = this._infoDisplayItem;
      Main.instance.MouseText("");
      Main.mouseText = true;
    }

    public override int CompareTo(object obj) => obj is IManuallyOrderedUIElement orderedUiElement ? this.OrderInUIList.CompareTo(orderedUiElement.OrderInUIList) : base.CompareTo(obj);

    private void SetBestiaryNotesOnItemCache(DropRateInfo info)
    {
      List<string> values = new List<string>();
      if (info.conditions == null)
        return;
      foreach (IProvideItemConditionDescription condition in info.conditions)
      {
        if (condition != null)
        {
          string conditionDescription = condition.GetConditionDescription();
          if (!string.IsNullOrWhiteSpace(conditionDescription))
            values.Add(conditionDescription);
        }
      }
      this._infoDisplayItem.BestiaryNotes = string.Join("\n", (IEnumerable<string>) values);
    }

    private void MouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12);
      this.BorderColor = Colors.FancyUIFatButtonMouseOver;
    }

    private void MouseOut(UIMouseEvent evt, UIElement listeningElement) => this.BorderColor = new Color(89, 116, 213, (int) byte.MaxValue);
  }
}
