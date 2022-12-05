// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Bestiary.FilterProviderInfoElement
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
  public class FilterProviderInfoElement : 
    IFilterInfoProvider,
    IProvideSearchFilterString,
    IBestiaryInfoElement
  {
    private const int framesPerRow = 16;
    private const int framesPerColumn = 5;
    private Point _filterIconFrame;
    private string _key;

    public int DisplayTextPriority { get; set; }

    public bool HideInPortraitInfo { get; set; }

    public FilterProviderInfoElement(string nameLanguageKey, int filterIconFrame)
    {
      this._key = nameLanguageKey;
      this._filterIconFrame.X = filterIconFrame % 16;
      this._filterIconFrame.Y = filterIconFrame / 16;
    }

    public UIElement GetFilterImage()
    {
      Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Tags_Shadow", (AssetRequestMode) 1);
      UIImageFramed filterImage = new UIImageFramed(asset, asset.Frame(16, 5, this._filterIconFrame.X, this._filterIconFrame.Y));
      filterImage.HAlign = 0.5f;
      filterImage.VAlign = 0.5f;
      return (UIElement) filterImage;
    }

    public string GetSearchString(ref BestiaryUICollectionInfo info) => info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0 ? (string) null : Language.GetText(this._key).Value;

    public string GetDisplayNameKey() => this._key;

    public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
    {
      if (this.HideInPortraitInfo)
        return (UIElement) null;
      if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
        return (UIElement) null;
      UIPanel uiPanel = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel", (AssetRequestMode) 1), (Asset<Texture2D>) null, customBarSize: 7);
      uiPanel.Width = new StyleDimension(-14f, 1f);
      uiPanel.Height = new StyleDimension(34f, 0.0f);
      uiPanel.BackgroundColor = new Color(43, 56, 101);
      uiPanel.BorderColor = Color.Transparent;
      uiPanel.Left = new StyleDimension(5f, 0.0f);
      UIElement button = (UIElement) uiPanel;
      button.SetPadding(0.0f);
      button.PaddingRight = 5f;
      UIElement filterImage = this.GetFilterImage();
      filterImage.HAlign = 0.0f;
      filterImage.Left = new StyleDimension(5f, 0.0f);
      UIText uiText = new UIText(Language.GetText(this.GetDisplayNameKey()), 0.8f);
      uiText.HAlign = 0.0f;
      uiText.Left = new StyleDimension(38f, 0.0f);
      uiText.TextOriginX = 0.0f;
      uiText.TextOriginY = 0.0f;
      uiText.VAlign = 0.5f;
      uiText.DynamicallyScaleDownToWidth = true;
      UIText element = uiText;
      if (filterImage != null)
        button.Append(filterImage);
      button.Append((UIElement) element);
      this.AddOnHover(button);
      return button;
    }

    private void AddOnHover(UIElement button) => button.OnUpdate += (UIElement.ElementEvent) (e => this.ShowButtonName(e));

    private void ShowButtonName(UIElement element)
    {
      if (!element.IsMouseHovering)
        return;
      string textValue = Language.GetTextValue(this.GetDisplayNameKey());
      Main.instance.MouseText(textValue);
    }
  }
}
