// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Bestiary.UnlockProgressDisplayBestiaryInfoElement
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
  public class UnlockProgressDisplayBestiaryInfoElement : IBestiaryInfoElement
  {
    private BestiaryUnlockProgressReport _progressReport;
    private UIElement _text1;
    private UIElement _text2;

    public UnlockProgressDisplayBestiaryInfoElement(BestiaryUnlockProgressReport progressReport) => this._progressReport = progressReport;

    public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
    {
      UIPanel uiPanel = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel", (AssetRequestMode) 1), (Asset<Texture2D>) null, customBarSize: 7);
      uiPanel.Width = new StyleDimension(-11f, 1f);
      uiPanel.Height = new StyleDimension(109f, 0.0f);
      uiPanel.BackgroundColor = new Color(43, 56, 101);
      uiPanel.BorderColor = Color.Transparent;
      uiPanel.Left = new StyleDimension(3f, 0.0f);
      UIElement container = (UIElement) uiPanel;
      container.PaddingLeft = 4f;
      container.PaddingRight = 4f;
      string text1 = string.Format("{0} Entry Collected", (object) Utils.PrettifyPercentDisplay((float) info.UnlockState / 4f, "P2"));
      string text2 = string.Format("{0} Bestiary Collected", (object) Utils.PrettifyPercentDisplay(this._progressReport.CompletionPercent, "P2"));
      int num = 8;
      UIText uiText1 = new UIText(text1, 0.8f);
      uiText1.HAlign = 0.0f;
      uiText1.VAlign = 0.0f;
      uiText1.Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiText1.Height = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiText1.IsWrapped = true;
      uiText1.PaddingTop = (float) -num;
      uiText1.PaddingBottom = (float) -num;
      UIText uiText2 = uiText1;
      UIText uiText3 = new UIText(text2, 0.8f);
      uiText3.HAlign = 0.0f;
      uiText3.VAlign = 0.0f;
      uiText3.Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiText3.Height = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiText3.IsWrapped = true;
      uiText3.PaddingTop = (float) -num;
      uiText3.PaddingBottom = (float) -num;
      UIText element = uiText3;
      this._text1 = (UIElement) uiText2;
      this._text2 = (UIElement) element;
      this.AddDynamicResize(container, uiText2);
      container.Append((UIElement) uiText2);
      container.Append((UIElement) element);
      return container;
    }

    private void AddDynamicResize(UIElement container, UIText text) => text.OnInternalTextChange += (Action) (() =>
    {
      container.Height = new StyleDimension(this._text1.MinHeight.Pixels + 4f + this._text2.MinHeight.Pixels, 0.0f);
      this._text2.Top = new StyleDimension(this._text1.MinHeight.Pixels + 4f, 0.0f);
    });
  }
}
