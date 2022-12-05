// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.States.UIWorkshopHub
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
  public class UIWorkshopHub : UIState, IHaveBackButtonCommand
  {
    private UIState _previousUIState;
    private UIText _descriptionText;
    private UIElement _buttonUseResourcePacks;
    private UIElement _buttonPublishResourcePacks;
    private UIElement _buttonImportWorlds;
    private UIElement _buttonPublishWorlds;
    private UIElement _buttonBack;
    private UIElement _buttonLogs;
    private UIGamepadHelper _helper;

    public static event Action OnWorkshopHubMenuOpened = () => { };

    public UIWorkshopHub(UIState stateToGoBackTo) => this._previousUIState = stateToGoBackTo;

    public void EnterHub() => UIWorkshopHub.OnWorkshopHubMenuOpened();

    public override void OnInitialize()
    {
      base.OnInitialize();
      int num1 = 20;
      int pixels1 = 250;
      int num2 = 50 + num1 * 2;
      int num3 = 600;
      int pixels2 = num3 - pixels1 - num2;
      UIElement element1 = new UIElement();
      element1.Width.Set(600f, 0.0f);
      element1.Top.Set((float) pixels1, 0.0f);
      element1.Height.Set((float) (num3 - pixels1), 0.0f);
      element1.HAlign = 0.5f;
      int pixels3 = 154;
      UIPanel uiPanel = new UIPanel();
      uiPanel.Width.Set(0.0f, 1f);
      uiPanel.Height.Set((float) pixels2, 0.0f);
      uiPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
      UIElement element2 = new UIElement();
      element2.Width.Set(0.0f, 1f);
      element2.Height.Set((float) pixels3, 0.0f);
      element2.SetPadding(0.0f);
      UITextPanel<LocalizedText> element3 = new UITextPanel<LocalizedText>(Language.GetText("UI.WorkshopHub"), 0.8f, true);
      element3.HAlign = 0.5f;
      element3.Top.Set(-46f, 0.0f);
      element3.SetPadding(15f);
      element3.BackgroundColor = new Color(73, 94, 171);
      UITextPanel<LocalizedText> element4 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
      element4.Width.Set(-10f, 0.5f);
      element4.Height.Set(50f, 0.0f);
      element4.VAlign = 1f;
      element4.HAlign = 0.0f;
      element4.Top.Set((float) -num1, 0.0f);
      element4.OnMouseOver += new UIElement.MouseEvent(this.FadedMouseOver);
      element4.OnMouseOut += new UIElement.MouseEvent(this.FadedMouseOut);
      element4.OnLeftClick += new UIElement.MouseEvent(this.GoBackClick);
      element4.SetSnapPoint("Back", 0);
      element1.Append((UIElement) element4);
      this._buttonBack = (UIElement) element4;
      UITextPanel<LocalizedText> element5 = new UITextPanel<LocalizedText>(Language.GetText("Workshop.ReportLogsButton"), 0.7f, true);
      element5.Width.Set(-10f, 0.5f);
      element5.Height.Set(50f, 0.0f);
      element5.VAlign = 1f;
      element5.HAlign = 1f;
      element5.Top.Set((float) -num1, 0.0f);
      element5.OnMouseOver += new UIElement.MouseEvent(this.FadedMouseOver);
      element5.OnMouseOut += new UIElement.MouseEvent(this.FadedMouseOut);
      element5.OnLeftClick += new UIElement.MouseEvent(this.GoLogsClick);
      element5.SetSnapPoint("Logs", 0);
      element1.Append((UIElement) element5);
      this._buttonLogs = (UIElement) element5;
      UIElement element6 = this.MakeButton_OpenWorkshopWorldsImportMenu();
      element6.HAlign = 0.0f;
      element6.VAlign = 0.0f;
      element2.Append(element6);
      UIElement element7 = this.MakeButton_OpenUseResourcePacksMenu();
      element7.HAlign = 1f;
      element7.VAlign = 0.0f;
      element2.Append(element7);
      UIElement element8 = this.MakeButton_OpenPublishWorldsMenu();
      element8.HAlign = 0.0f;
      element8.VAlign = 1f;
      element2.Append(element8);
      UIElement element9 = this.MakeButton_OpenPublishResourcePacksMenu();
      element9.HAlign = 1f;
      element9.VAlign = 1f;
      element2.Append(element9);
      UIWorkshopHub.AddHorizontalSeparator((UIElement) uiPanel, (float) (pixels3 + 6 + 6));
      this.AddDescriptionPanel((UIElement) uiPanel, (float) (pixels3 + 8 + 6 + 6), (float) (pixels2 - pixels3 - 12 - 12 - 8), "desc");
      uiPanel.Append(element2);
      element1.Append((UIElement) uiPanel);
      element1.Append((UIElement) element3);
      this.Append(element1);
    }

    private UIElement MakeButton_OpenUseResourcePacksMenu()
    {
      UIElement uiElement = this.MakeFancyButton("Images/UI/Workshop/HubResourcepacks", "Workshop.HubResourcePacks");
      uiElement.OnLeftClick += new UIElement.MouseEvent(this.Click_OpenResourcePacksMenu);
      this._buttonUseResourcePacks = uiElement;
      return uiElement;
    }

    private void Click_OpenResourcePacksMenu(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(10);
      Main.OpenResourcePacksMenu((UIState) this);
    }

    private UIElement MakeButton_OpenWorkshopWorldsImportMenu()
    {
      UIElement uiElement = this.MakeFancyButton("Images/UI/Workshop/HubWorlds", "Workshop.HubWorlds");
      uiElement.OnLeftClick += new UIElement.MouseEvent(this.Click_OpenWorldImportMenu);
      this._buttonImportWorlds = uiElement;
      return uiElement;
    }

    private void Click_OpenWorldImportMenu(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(10);
      Main.MenuUI.SetState((UIState) new UIWorkshopWorldImport((UIState) this));
    }

    private UIElement MakeButton_OpenPublishResourcePacksMenu()
    {
      UIElement uiElement = this.MakeFancyButton("Images/UI/Workshop/HubPublishResourcepacks", "Workshop.HubPublishResourcePacks");
      uiElement.OnLeftClick += new UIElement.MouseEvent(this.Click_OpenResourcePackPublishMenu);
      this._buttonPublishResourcePacks = uiElement;
      return uiElement;
    }

    private void Click_OpenResourcePackPublishMenu(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(10);
      Main.MenuUI.SetState((UIState) new UIWorkshopSelectResourcePackToPublish((UIState) this));
    }

    private UIElement MakeButton_OpenPublishWorldsMenu()
    {
      UIElement uiElement = this.MakeFancyButton("Images/UI/Workshop/HubPublishWorlds", "Workshop.HubPublishWorlds");
      uiElement.OnLeftClick += new UIElement.MouseEvent(this.Click_OpenWorldPublishMenu);
      this._buttonPublishWorlds = uiElement;
      return uiElement;
    }

    private void Click_OpenWorldPublishMenu(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(10);
      Main.MenuUI.SetState((UIState) new UIWorkshopSelectWorldToPublish((UIState) this));
    }

    private static void AddHorizontalSeparator(UIElement Container, float accumualtedHeight)
    {
      UIHorizontalSeparator horizontalSeparator = new UIHorizontalSeparator();
      horizontalSeparator.Width = StyleDimension.FromPercent(1f);
      horizontalSeparator.Top = StyleDimension.FromPixels(accumualtedHeight - 8f);
      horizontalSeparator.Color = Color.Lerp(Color.White, new Color(63, 65, 151, (int) byte.MaxValue), 0.85f) * 0.9f;
      UIHorizontalSeparator element = horizontalSeparator;
      Container.Append((UIElement) element);
    }

    public void ShowOptionDescription(UIMouseEvent evt, UIElement listeningElement)
    {
      LocalizedText text = (LocalizedText) null;
      if (listeningElement == this._buttonUseResourcePacks)
        text = Language.GetText("Workshop.HubDescriptionUseResourcePacks");
      if (listeningElement == this._buttonPublishResourcePacks)
        text = Language.GetText("Workshop.HubDescriptionPublishResourcePacks");
      if (listeningElement == this._buttonImportWorlds)
        text = Language.GetText("Workshop.HubDescriptionImportWorlds");
      if (listeningElement == this._buttonPublishWorlds)
        text = Language.GetText("Workshop.HubDescriptionPublishWorlds");
      if (text == null)
        return;
      this._descriptionText.SetText(text);
    }

    public void ClearOptionDescription(UIMouseEvent evt, UIElement listeningElement) => this._descriptionText.SetText(Language.GetText("Workshop.HubDescriptionDefault"));

    private void AddDescriptionPanel(
      UIElement container,
      float accumulatedHeight,
      float height,
      string tagGroup)
    {
      float num = 0.0f;
      UISlicedImage uiSlicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", (AssetRequestMode) 1));
      uiSlicedImage.HAlign = 0.5f;
      uiSlicedImage.VAlign = 1f;
      uiSlicedImage.Width = StyleDimension.FromPixelsAndPercent((float) (-(double) num * 2.0), 1f);
      uiSlicedImage.Left = StyleDimension.FromPixels(-num);
      uiSlicedImage.Height = StyleDimension.FromPixelsAndPercent(height, 0.0f);
      uiSlicedImage.Top = StyleDimension.FromPixels(2f);
      UISlicedImage element1 = uiSlicedImage;
      element1.SetSliceDepths(10);
      element1.Color = Color.LightGray * 0.7f;
      container.Append((UIElement) element1);
      UIText uiText = new UIText(Language.GetText("Workshop.HubDescriptionDefault"));
      uiText.HAlign = 0.0f;
      uiText.VAlign = 0.0f;
      uiText.Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiText.Height = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiText.Top = StyleDimension.FromPixelsAndPercent(5f, 0.0f);
      UIText element2 = uiText;
      element2.PaddingLeft = 20f;
      element2.PaddingRight = 20f;
      element2.PaddingTop = 6f;
      element2.IsWrapped = true;
      element1.Append((UIElement) element2);
      this._descriptionText = element2;
    }

    private UIElement MakeFancyButton(string iconImagePath, string textKey)
    {
      UIPanel uiPanel = new UIPanel();
      int pixels1 = -3;
      int pixels2 = -3;
      uiPanel.Width = StyleDimension.FromPixelsAndPercent((float) pixels1, 0.5f);
      uiPanel.Height = StyleDimension.FromPixelsAndPercent((float) pixels2, 0.5f);
      uiPanel.OnMouseOver += new UIElement.MouseEvent(this.SetColorsToHovered);
      uiPanel.OnMouseOut += new UIElement.MouseEvent(this.SetColorsToNotHovered);
      uiPanel.BackgroundColor = new Color(63, 82, 151) * 0.7f;
      uiPanel.BorderColor = new Color(89, 116, 213) * 0.7f;
      uiPanel.SetPadding(6f);
      UIImage uiImage = new UIImage(Main.Assets.Request<Texture2D>(iconImagePath, (AssetRequestMode) 1));
      uiImage.IgnoresMouseInteraction = true;
      uiImage.VAlign = 0.5f;
      UIImage element1 = uiImage;
      element1.Left.Set(2f, 0.0f);
      uiPanel.Append((UIElement) element1);
      uiPanel.OnMouseOver += new UIElement.MouseEvent(this.ShowOptionDescription);
      uiPanel.OnMouseOut += new UIElement.MouseEvent(this.ClearOptionDescription);
      UIText uiText = new UIText(Language.GetText(textKey), 0.45f, true);
      uiText.HAlign = 0.0f;
      uiText.VAlign = 0.5f;
      uiText.Width = StyleDimension.FromPixelsAndPercent(-80f, 1f);
      uiText.Height = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiText.Top = StyleDimension.FromPixelsAndPercent(5f, 0.0f);
      uiText.Left = StyleDimension.FromPixels(80f);
      uiText.IgnoresMouseInteraction = true;
      uiText.TextOriginX = 0.0f;
      uiText.TextOriginY = 0.0f;
      UIText element2 = uiText;
      element2.PaddingLeft = 0.0f;
      element2.PaddingRight = 20f;
      element2.PaddingTop = 10f;
      element2.IsWrapped = true;
      uiPanel.Append((UIElement) element2);
      uiPanel.SetSnapPoint("Button", 0);
      return (UIElement) uiPanel;
    }

    private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
    {
      this.HandleBackButtonUsage();
      SoundEngine.PlaySound(11);
    }

    private void GoLogsClick(UIMouseEvent evt, UIElement listeningElement)
    {
      Main.IssueReporterIndicator.Hide();
      Main.OpenReportsMenu();
      SoundEngine.PlaySound(10);
    }

    private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12);
      ((UIPanel) evt.Target).BackgroundColor = new Color(73, 94, 171);
      ((UIPanel) evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
    }

    private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
    {
      ((UIPanel) evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
      ((UIPanel) evt.Target).BorderColor = Color.Black;
    }

    private void SetColorsToHovered(UIMouseEvent evt, UIElement listeningElement)
    {
      UIPanel target = (UIPanel) evt.Target;
      target.BackgroundColor = new Color(73, 94, 171);
      target.BorderColor = new Color(89, 116, 213);
    }

    private void SetColorsToNotHovered(UIMouseEvent evt, UIElement listeningElement)
    {
      UIPanel target = (UIPanel) evt.Target;
      target.BackgroundColor = new Color(63, 82, 151) * 0.7f;
      target.BorderColor = new Color(89, 116, 213) * 0.7f;
    }

    public void HandleBackButtonUsage()
    {
      if (this._previousUIState == null)
      {
        Main.menuMode = 0;
      }
      else
      {
        Main.menuMode = 888;
        Main.MenuUI.SetState(this._previousUIState);
        SoundEngine.PlaySound(11);
      }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      this.SetupGamepadPoints(spriteBatch);
    }

    private void SetupGamepadPoints(SpriteBatch spriteBatch)
    {
      UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
      int idRangeStartInclusive = 3000;
      int num1 = idRangeStartInclusive;
      ref UIGamepadHelper local1 = ref this._helper;
      int id1 = num1;
      int num2 = id1 + 1;
      UIElement useResourcePacks = this._buttonUseResourcePacks;
      UILinkPoint linkPoint1 = local1.GetLinkPoint(id1, useResourcePacks);
      ref UIGamepadHelper local2 = ref this._helper;
      int id2 = num2;
      int num3 = id2 + 1;
      UIElement publishResourcePacks = this._buttonPublishResourcePacks;
      UILinkPoint linkPoint2 = local2.GetLinkPoint(id2, publishResourcePacks);
      ref UIGamepadHelper local3 = ref this._helper;
      int id3 = num3;
      int num4 = id3 + 1;
      UIElement buttonImportWorlds = this._buttonImportWorlds;
      UILinkPoint linkPoint3 = local3.GetLinkPoint(id3, buttonImportWorlds);
      ref UIGamepadHelper local4 = ref this._helper;
      int id4 = num4;
      int num5 = id4 + 1;
      UIElement buttonPublishWorlds = this._buttonPublishWorlds;
      UILinkPoint linkPoint4 = local4.GetLinkPoint(id4, buttonPublishWorlds);
      ref UIGamepadHelper local5 = ref this._helper;
      int id5 = num5;
      int num6 = id5 + 1;
      UIElement buttonBack = this._buttonBack;
      UILinkPoint linkPoint5 = local5.GetLinkPoint(id5, buttonBack);
      ref UIGamepadHelper local6 = ref this._helper;
      int id6 = num6;
      int idRangeEndExclusive = id6 + 1;
      UIElement buttonLogs = this._buttonLogs;
      UILinkPoint linkPoint6 = local6.GetLinkPoint(id6, buttonLogs);
      this._helper.PairLeftRight(linkPoint3, linkPoint1);
      this._helper.PairLeftRight(linkPoint4, linkPoint2);
      this._helper.PairLeftRight(linkPoint5, linkPoint6);
      this._helper.PairUpDown(linkPoint3, linkPoint4);
      this._helper.PairUpDown(linkPoint1, linkPoint2);
      this._helper.PairUpDown(linkPoint4, linkPoint5);
      this._helper.PairUpDown(linkPoint2, linkPoint6);
      this._helper.MoveToVisuallyClosestPoint(idRangeStartInclusive, idRangeEndExclusive);
    }
  }
}
