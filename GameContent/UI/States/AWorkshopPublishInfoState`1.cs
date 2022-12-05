// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.States.AWorkshopPublishInfoState`1
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.OS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Social;
using Terraria.Social.Base;
using Terraria.UI;
using Terraria.UI.Gamepad;
using Terraria.Utilities.FileBrowser;

namespace Terraria.GameContent.UI.States
{
  public abstract class AWorkshopPublishInfoState<TPublishedObjectType> : 
    UIState,
    IHaveBackButtonCommand
  {
    protected UIState _previousUIState;
    protected TPublishedObjectType _dataObject;
    protected string _publishedObjectNameDescriptorTexKey;
    protected string _instructionsTextKey;
    private UIElement _uiListContainer;
    private UIElement _uiListRect;
    private UIScrollbar _scrollbar;
    private bool _isScrollbarAttached;
    private UIText _descriptionText;
    private UIElement _listContainer;
    private UIElement _backButton;
    private UIElement _publishButton;
    private WorkshopItemPublicSettingId _optionPublicity = WorkshopItemPublicSettingId.Public;
    private GroupOptionButton<WorkshopItemPublicSettingId>[] _publicityOptions;
    private List<GroupOptionButton<WorkshopTagOption>> _tagOptions;
    private UICharacterNameButton _previewImagePathPlate;
    private Texture2D _previewImageTransientTexture;
    private UIImage _previewImageUIElement;
    private string _previewImagePath;
    private Asset<Texture2D> _defaultPreviewImageTexture;
    private UIElement _steamDisclaimerButton;
    private UIText _disclaimerText;
    private UIGamepadHelper _helper;

    public AWorkshopPublishInfoState(UIState stateToGoBackTo, TPublishedObjectType dataObject)
    {
      this._previousUIState = stateToGoBackTo;
      this._dataObject = dataObject;
    }

    public override void OnInitialize()
    {
      base.OnInitialize();
      int backButtonYLift = 40;
      int pixels = 200;
      int num1 = 50 + backButtonYLift + 10;
      int num2 = 70;
      UIElement uiElement = new UIElement();
      uiElement.Width.Set(600f, 0.0f);
      uiElement.Top.Set((float) pixels, 0.0f);
      uiElement.Height.Set((float) -pixels, 1f);
      uiElement.HAlign = 0.5f;
      UIPanel uiPanel = new UIPanel();
      uiPanel.Width.Set(0.0f, 1f);
      uiPanel.Height.Set((float) -num1, 1f);
      uiPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
      this.AddBackButton(backButtonYLift, uiElement);
      this.AddPublishButton(backButtonYLift, uiElement);
      int antiHeight = 6 + num2;
      this.FillUIList(this.AddUIList((UIElement) uiPanel, (float) antiHeight));
      this.AddHorizontalSeparator((UIElement) uiPanel, 0.0f).Top = new StyleDimension((float) (-num2 + 3), 1f);
      this.AddDescriptionPanel((UIElement) uiPanel, (float) (num2 - 6), "desc");
      uiElement.Append((UIElement) uiPanel);
      this.Append(uiElement);
      this.SetDefaultOptions();
    }

    private void SetDefaultOptions()
    {
      this._optionPublicity = WorkshopItemPublicSettingId.Public;
      foreach (GroupOptionButton<WorkshopItemPublicSettingId> publicityOption in this._publicityOptions)
        publicityOption.SetCurrentOption(this._optionPublicity);
      this.SetTagsFromFoundEntry();
      this.UpdateImagePreview();
    }

    private void FillUIList(UIList uiList)
    {
      UIElement uiElement = new UIElement()
      {
        Width = new StyleDimension(0.0f, 0.0f),
        Height = new StyleDimension(0.0f, 0.0f)
      };
      uiElement.SetPadding(0.0f);
      uiList.Add(uiElement);
      uiList.Add(this.CreateSteamDisclaimer("disclaimer"));
      uiList.Add(this.CreatePreviewImageSelectionPanel("image"));
      uiList.Add(this.CreatePublicSettingsRow(0.0f, 44f, "public"));
      uiList.Add(this.CreateTagOptionsPanel(0.0f, 44, "tags"));
    }

    private UIElement CreatePreviewImageSelectionPanel(string tagGroup)
    {
      UIElement imageSelectionPanel = new UIElement();
      imageSelectionPanel.Width = new StyleDimension(0.0f, 1f);
      imageSelectionPanel.Height = new StyleDimension(80f, 0.0f);
      UIElement element1 = new UIElement()
      {
        Width = new StyleDimension(72f, 0.0f),
        Height = new StyleDimension(72f, 0.0f),
        HAlign = 1f,
        VAlign = 0.5f,
        Left = new StyleDimension(-6f, 0.0f),
        Top = new StyleDimension(0.0f, 0.0f)
      };
      element1.SetPadding(0.0f);
      imageSelectionPanel.Append(element1);
      float num = 86f;
      this._defaultPreviewImageTexture = Main.Assets.Request<Texture2D>("Images/UI/Workshop/DefaultPreviewImage", (AssetRequestMode) 1);
      UIImage uiImage1 = new UIImage(this._defaultPreviewImageTexture);
      uiImage1.Width = new StyleDimension(-4f, 1f);
      uiImage1.Height = new StyleDimension(-4f, 1f);
      uiImage1.HAlign = 0.5f;
      uiImage1.VAlign = 0.5f;
      uiImage1.ScaleToFit = true;
      uiImage1.AllowResizingDimensions = false;
      UIImage element2 = uiImage1;
      UIImage uiImage2 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders", (AssetRequestMode) 1));
      uiImage2.HAlign = 0.5f;
      uiImage2.VAlign = 0.5f;
      UIImage element3 = uiImage2;
      element1.Append((UIElement) element2);
      element1.Append((UIElement) element3);
      this._previewImageUIElement = element2;
      UICharacterNameButton characterNameButton = new UICharacterNameButton(Language.GetText("Workshop.PreviewImagePathTitle"), Language.GetText("Workshop.PreviewImagePathEmpty"), Language.GetText("Workshop.PreviewImagePathDescription"));
      characterNameButton.Width = StyleDimension.FromPixelsAndPercent(-num, 1f);
      characterNameButton.Height = new StyleDimension(0.0f, 1f);
      UICharacterNameButton element4 = characterNameButton;
      element4.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_SetPreviewImage);
      element4.OnMouseOver += new UIElement.MouseEvent(this.ShowOptionDescription);
      element4.OnMouseOut += new UIElement.MouseEvent(this.ClearOptionDescription);
      element4.SetSnapPoint(tagGroup, 0);
      imageSelectionPanel.Append((UIElement) element4);
      this._previewImagePathPlate = element4;
      return imageSelectionPanel;
    }

    private void SetTagsFromFoundEntry()
    {
      FoundWorkshopEntryInfo info;
      if (!this.TryFindingTags(out info))
        return;
      if (info.tags != null)
      {
        foreach (GroupOptionButton<WorkshopTagOption> tagOption in this._tagOptions)
        {
          bool flag = ((IEnumerable<string>) info.tags).Contains<string>(tagOption.OptionValue.InternalNameForAPIs);
          tagOption.SetCurrentOption(flag ? tagOption.OptionValue : (WorkshopTagOption) null);
          tagOption.SetColor(tagOption.IsSelected ? new Color(152, 175, 235) : Colors.InventoryDefaultColor, 1f);
        }
      }
      foreach (GroupOptionButton<WorkshopItemPublicSettingId> publicityOption in this._publicityOptions)
        publicityOption.SetCurrentOption(info.publicity);
    }

    protected abstract bool TryFindingTags(out FoundWorkshopEntryInfo info);

    private void Click_SetPreviewImage(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(10);
      this.OpenFileDialogueToSelectPreviewImage();
    }

    private UIElement CreateSteamDisclaimer(string tagGroup)
    {
      float pixels = 60f;
      float num = 0.0f + pixels;
      GroupOptionButton<bool> steamDisclaimer = new GroupOptionButton<bool>(true, (LocalizedText) null, (LocalizedText) null, Color.White, (string) null, titleWidthReduction: 16f);
      steamDisclaimer.HAlign = 0.5f;
      steamDisclaimer.VAlign = 0.0f;
      steamDisclaimer.Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      steamDisclaimer.Left = StyleDimension.FromPixels(0.0f);
      steamDisclaimer.Height = StyleDimension.FromPixelsAndPercent(num + 4f, 0.0f);
      steamDisclaimer.Top = StyleDimension.FromPixels(0.0f);
      steamDisclaimer.ShowHighlightWhenSelected = false;
      steamDisclaimer.SetCurrentOption(false);
      steamDisclaimer.Width.Set(0.0f, 1f);
      UIElement element1 = new UIElement()
      {
        HAlign = 0.5f,
        VAlign = 1f,
        Width = new StyleDimension(0.0f, 1f),
        Height = new StyleDimension(pixels, 0.0f)
      };
      steamDisclaimer.Append(element1);
      UIText uiText = new UIText(Language.GetText("Workshop.SteamDisclaimer"));
      uiText.HAlign = 0.0f;
      uiText.VAlign = 0.0f;
      uiText.Width = StyleDimension.FromPixelsAndPercent(-40f, 1f);
      uiText.Height = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiText.TextColor = Color.Cyan;
      uiText.IgnoresMouseInteraction = true;
      UIText element2 = uiText;
      element2.PaddingLeft = 20f;
      element2.PaddingRight = 20f;
      element2.PaddingTop = 4f;
      element2.IsWrapped = true;
      this._disclaimerText = element2;
      steamDisclaimer.OnLeftClick += new UIElement.MouseEvent(this.steamDisclaimerText_OnClick);
      steamDisclaimer.OnMouseOver += new UIElement.MouseEvent(this.steamDisclaimerText_OnMouseOver);
      steamDisclaimer.OnMouseOut += new UIElement.MouseEvent(this.steamDisclaimerText_OnMouseOut);
      element1.Append((UIElement) element2);
      element2.SetSnapPoint(tagGroup, 0);
      this._steamDisclaimerButton = (UIElement) element2;
      return (UIElement) steamDisclaimer;
    }

    private void steamDisclaimerText_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
    {
      this._disclaimerText.TextColor = Color.Cyan;
      this.ClearOptionDescription(evt, listeningElement);
    }

    private void steamDisclaimerText_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12);
      this._disclaimerText.TextColor = Color.LightCyan;
      this.ShowOptionDescription(evt, listeningElement);
    }

    private void steamDisclaimerText_OnClick(UIMouseEvent evt, UIElement listeningElement)
    {
      try
      {
        Platform.Get<IPathService>().OpenURL("https://steamcommunity.com/sharedfiles/workshoplegalagreement");
      }
      catch (Exception ex)
      {
      }
    }

    public override void Recalculate()
    {
      this.UpdateScrollbar();
      base.Recalculate();
    }

    private void UpdateScrollbar()
    {
      if (this._scrollbar == null)
        return;
      if (this._isScrollbarAttached && !this._scrollbar.CanScroll)
      {
        this._uiListContainer.RemoveChild((UIElement) this._scrollbar);
        this._isScrollbarAttached = false;
        this._uiListRect.Width.Set(0.0f, 1f);
      }
      else
      {
        if (this._isScrollbarAttached || !this._scrollbar.CanScroll)
          return;
        this._uiListContainer.Append((UIElement) this._scrollbar);
        this._isScrollbarAttached = true;
        this._uiListRect.Width.Set(-25f, 1f);
      }
    }

    private UIList AddUIList(UIElement container, float antiHeight)
    {
      this._uiListContainer = container;
      float num = 0.0f;
      UIElement element1 = new UIElement()
      {
        HAlign = 0.0f,
        VAlign = 0.0f,
        Width = StyleDimension.FromPixelsAndPercent((float) (-(double) num * 2.0), 1f),
        Left = StyleDimension.FromPixels(-num),
        Height = StyleDimension.FromPixelsAndPercent(-2f - antiHeight, 1f),
        OverflowHidden = true
      };
      this._listContainer = element1;
      UISlicedImage uiSlicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/Workshop/ListBackground", (AssetRequestMode) 1));
      uiSlicedImage.Width = new StyleDimension(0.0f, 1f);
      uiSlicedImage.Height = new StyleDimension(0.0f, 1f);
      uiSlicedImage.Color = Color.White * 0.7f;
      UISlicedImage element2 = uiSlicedImage;
      element2.SetSliceDepths(4);
      container.Append(element1);
      element1.Append((UIElement) element2);
      UIList uiList = new UIList();
      uiList.Width = StyleDimension.FromPixelsAndPercent(-10f, 1f);
      uiList.Height = StyleDimension.FromPixelsAndPercent(-4f, 1f);
      uiList.HAlign = 0.5f;
      uiList.VAlign = 0.5f;
      uiList.OverflowHidden = true;
      UIList element3 = uiList;
      element3.ManualSortMethod = new Action<List<UIElement>>(this.ManualIfnoSortingMethod);
      element3.ListPadding = 5f;
      element1.Append((UIElement) element3);
      UIScrollbar uiScrollbar = new UIScrollbar();
      uiScrollbar.HAlign = 1f;
      uiScrollbar.VAlign = 0.0f;
      uiScrollbar.Width = StyleDimension.FromPixelsAndPercent((float) (-(double) num * 2.0), 1f);
      uiScrollbar.Left = StyleDimension.FromPixels(-num);
      uiScrollbar.Height = StyleDimension.FromPixelsAndPercent(-14f - antiHeight, 1f);
      uiScrollbar.Top = StyleDimension.FromPixels(6f);
      UIScrollbar scrollbar = uiScrollbar;
      scrollbar.SetView(100f, 1000f);
      element3.SetScrollbar(scrollbar);
      this._uiListRect = element1;
      this._scrollbar = scrollbar;
      return element3;
    }

    private void ManualIfnoSortingMethod(List<UIElement> list)
    {
    }

    private UIElement CreatePublicSettingsRow(
      float accumulatedHeight,
      float height,
      string tagGroup)
    {
      UIElement entirePanel;
      UIElement innerPanel;
      this.CreateStylizedCategoryPanel(height, "Workshop.CategoryTitlePublicity", out entirePanel, out innerPanel);
      WorkshopItemPublicSettingId[] itemPublicSettingIdArray = new WorkshopItemPublicSettingId[3]
      {
        WorkshopItemPublicSettingId.Public,
        WorkshopItemPublicSettingId.FriendsOnly,
        WorkshopItemPublicSettingId.Private
      };
      LocalizedText[] localizedTextArray1 = new LocalizedText[3]
      {
        Language.GetText("Workshop.SettingsPublicityPublic"),
        Language.GetText("Workshop.SettingsPublicityFriendsOnly"),
        Language.GetText("Workshop.SettingsPublicityPrivate")
      };
      LocalizedText[] localizedTextArray2 = new LocalizedText[3]
      {
        Language.GetText("Workshop.SettingsPublicityPublicDescription"),
        Language.GetText("Workshop.SettingsPublicityFriendsOnlyDescription"),
        Language.GetText("Workshop.SettingsPublicityPrivateDescription")
      };
      Color[] colorArray = new Color[3]
      {
        Color.White,
        Color.White,
        Color.White
      };
      string[] strArray = new string[3]
      {
        "Images/UI/Workshop/PublicityPublic",
        "Images/UI/Workshop/PublicityFriendsOnly",
        "Images/UI/Workshop/PublicityPrivate"
      };
      float num = 0.98f;
      GroupOptionButton<WorkshopItemPublicSettingId>[] groupOptionButtonArray = new GroupOptionButton<WorkshopItemPublicSettingId>[itemPublicSettingIdArray.Length];
      for (int id = 0; id < groupOptionButtonArray.Length; ++id)
      {
        GroupOptionButton<WorkshopItemPublicSettingId> element = new GroupOptionButton<WorkshopItemPublicSettingId>(itemPublicSettingIdArray[id], localizedTextArray1[id], localizedTextArray2[id], colorArray[id], strArray[id], titleAlignmentX: 1f, titleWidthReduction: 16f);
        element.Width = StyleDimension.FromPixelsAndPercent((float) (-4 * (groupOptionButtonArray.Length - 1)), 1f / (float) groupOptionButtonArray.Length * num);
        element.HAlign = (float) id / (float) (groupOptionButtonArray.Length - 1);
        element.Left = StyleDimension.FromPercent((float) ((1.0 - (double) num) * (1.0 - (double) element.HAlign * 2.0)));
        element.Top.Set(accumulatedHeight, 0.0f);
        element.OnLeftMouseDown += new UIElement.MouseEvent(this.ClickPublicityOption);
        element.OnMouseOver += new UIElement.MouseEvent(this.ShowOptionDescription);
        element.OnMouseOut += new UIElement.MouseEvent(this.ClearOptionDescription);
        element.SetSnapPoint(tagGroup, id);
        innerPanel.Append((UIElement) element);
        groupOptionButtonArray[id] = element;
      }
      this._publicityOptions = groupOptionButtonArray;
      return entirePanel;
    }

    private UIElement CreateTagOptionsPanel(
      float accumulatedHeight,
      int heightPerRow,
      string tagGroup)
    {
      List<WorkshopTagOption> tagsToShow = this.GetTagsToShow();
      int num1 = 3;
      int num2 = (int) Math.Ceiling((double) tagsToShow.Count / (double) num1);
      UIElement entirePanel;
      UIElement innerPanel;
      this.CreateStylizedCategoryPanel((float) (heightPerRow * num2), "Workshop.CategoryTitleTags", out entirePanel, out innerPanel);
      float num3 = 0.98f;
      List<GroupOptionButton<WorkshopTagOption>> groupOptionButtonList = new List<GroupOptionButton<WorkshopTagOption>>();
      for (int index = 0; index < tagsToShow.Count; ++index)
      {
        WorkshopTagOption option = tagsToShow[index];
        GroupOptionButton<WorkshopTagOption> element = new GroupOptionButton<WorkshopTagOption>(option, Language.GetText(option.NameKey), Language.GetText(option.NameKey + "Description"), Color.White, (string) null, titleWidthReduction: 16f);
        element.ShowHighlightWhenSelected = false;
        element.SetCurrentOption((WorkshopTagOption) null);
        int num4 = index / num1;
        int num5 = index - num4 * num1;
        element.Width = StyleDimension.FromPixelsAndPercent((float) (-4 * (num1 - 1)), 1f / (float) num1 * num3);
        element.HAlign = (float) num5 / (float) (num1 - 1);
        element.Left = StyleDimension.FromPercent((float) ((1.0 - (double) num3) * (1.0 - (double) element.HAlign * 2.0)));
        element.Top.Set((float) (num4 * heightPerRow), 0.0f);
        element.OnLeftMouseDown += new UIElement.MouseEvent(this.ClickTagOption);
        element.OnMouseOver += new UIElement.MouseEvent(this.ShowOptionDescription);
        element.OnMouseOut += new UIElement.MouseEvent(this.ClearOptionDescription);
        element.SetSnapPoint(tagGroup, index);
        innerPanel.Append((UIElement) element);
        groupOptionButtonList.Add(element);
      }
      this._tagOptions = groupOptionButtonList;
      return entirePanel;
    }

    private void CreateStylizedCategoryPanel(
      float height,
      string titleTextKey,
      out UIElement entirePanel,
      out UIElement innerPanel)
    {
      float num = 44f;
      UISlicedImage uiSlicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanel", (AssetRequestMode) 1));
      uiSlicedImage.HAlign = 0.5f;
      uiSlicedImage.VAlign = 0.0f;
      uiSlicedImage.Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiSlicedImage.Left = StyleDimension.FromPixels(0.0f);
      uiSlicedImage.Height = StyleDimension.FromPixelsAndPercent((float) ((double) height + (double) num + 4.0), 0.0f);
      uiSlicedImage.Top = StyleDimension.FromPixels(0.0f);
      UISlicedImage Container = uiSlicedImage;
      Container.SetSliceDepths(8);
      Container.Color = Color.White * 0.7f;
      innerPanel = new UIElement()
      {
        HAlign = 0.5f,
        VAlign = 1f,
        Width = new StyleDimension(0.0f, 1f),
        Height = new StyleDimension(height, 0.0f)
      };
      Container.Append(innerPanel);
      this.AddHorizontalSeparator((UIElement) Container, num, 4);
      UIText uiText = new UIText(Language.GetText(titleTextKey));
      uiText.HAlign = 0.0f;
      uiText.VAlign = 0.0f;
      uiText.Width = StyleDimension.FromPixelsAndPercent(-40f, 1f);
      uiText.Height = StyleDimension.FromPixelsAndPercent(num, 0.0f);
      uiText.Top = StyleDimension.FromPixelsAndPercent(5f, 0.0f);
      UIText element = uiText;
      element.PaddingLeft = 20f;
      element.PaddingRight = 20f;
      element.PaddingTop = 6f;
      element.IsWrapped = false;
      Container.Append((UIElement) element);
      entirePanel = (UIElement) Container;
    }

    private void ClickTagOption(UIMouseEvent evt, UIElement listeningElement)
    {
      GroupOptionButton<WorkshopTagOption> groupOptionButton = (GroupOptionButton<WorkshopTagOption>) listeningElement;
      groupOptionButton.SetCurrentOption(groupOptionButton.IsSelected ? (WorkshopTagOption) null : groupOptionButton.OptionValue);
      groupOptionButton.SetColor(groupOptionButton.IsSelected ? new Color(152, 175, 235) : Colors.InventoryDefaultColor, 1f);
    }

    private void ClickPublicityOption(UIMouseEvent evt, UIElement listeningElement)
    {
      GroupOptionButton<WorkshopItemPublicSettingId> groupOptionButton = (GroupOptionButton<WorkshopItemPublicSettingId>) listeningElement;
      this._optionPublicity = groupOptionButton.OptionValue;
      foreach (GroupOptionButton<WorkshopItemPublicSettingId> publicityOption in this._publicityOptions)
        publicityOption.SetCurrentOption(groupOptionButton.OptionValue);
    }

    public void ShowOptionDescription(UIMouseEvent evt, UIElement listeningElement)
    {
      LocalizedText text = (LocalizedText) null;
      if (listeningElement is GroupOptionButton<WorkshopItemPublicSettingId> groupOptionButton1)
        text = groupOptionButton1.Description;
      if (listeningElement is UICharacterNameButton characterNameButton)
        text = characterNameButton.Description;
      if (listeningElement is GroupOptionButton<bool> groupOptionButton2)
        text = groupOptionButton2.Description;
      if (listeningElement is GroupOptionButton<WorkshopTagOption> groupOptionButton3)
        text = groupOptionButton3.Description;
      if (listeningElement == this._steamDisclaimerButton)
        text = Language.GetText("Workshop.SteamDisclaimerDescrpition");
      if (text == null)
        return;
      this._descriptionText.SetText(text);
    }

    public void ClearOptionDescription(UIMouseEvent evt, UIElement listeningElement) => this._descriptionText.SetText(Language.GetText("Workshop.InfoDescriptionDefault"));

    private UIElement CreateInsturctionsPanel(
      float accumulatedHeight,
      float height,
      string tagGroup)
    {
      float num = 0.0f;
      UISlicedImage insturctionsPanel = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", (AssetRequestMode) 1));
      insturctionsPanel.HAlign = 0.5f;
      insturctionsPanel.VAlign = 0.0f;
      insturctionsPanel.Width = StyleDimension.FromPixelsAndPercent((float) (-(double) num * 2.0), 1f);
      insturctionsPanel.Left = StyleDimension.FromPixels(-num);
      insturctionsPanel.Height = StyleDimension.FromPixelsAndPercent(height, 0.0f);
      insturctionsPanel.Top = StyleDimension.FromPixels(accumulatedHeight);
      insturctionsPanel.SetSliceDepths(10);
      insturctionsPanel.Color = Color.LightGray * 0.7f;
      UIText uiText = new UIText(Language.GetText(this._instructionsTextKey));
      uiText.HAlign = 0.0f;
      uiText.VAlign = 0.0f;
      uiText.Width = StyleDimension.FromPixelsAndPercent(-40f, 1f);
      uiText.Height = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiText.Top = StyleDimension.FromPixelsAndPercent(5f, 0.0f);
      UIText element = uiText;
      element.PaddingLeft = 20f;
      element.PaddingRight = 20f;
      element.PaddingTop = 6f;
      element.IsWrapped = true;
      insturctionsPanel.Append((UIElement) element);
      return (UIElement) insturctionsPanel;
    }

    private void AddDescriptionPanel(UIElement container, float height, string tagGroup)
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
      UIText uiText = new UIText(Language.GetText("Workshop.InfoDescriptionDefault"), 0.85f);
      uiText.HAlign = 0.0f;
      uiText.VAlign = 1f;
      uiText.Width = new StyleDimension(0.0f, 1f);
      uiText.Height = new StyleDimension(0.0f, 1f);
      UIText element2 = uiText;
      element2.PaddingLeft = 4f;
      element2.PaddingRight = 4f;
      element2.PaddingTop = 4f;
      element2.IsWrapped = true;
      element1.Append((UIElement) element2);
      this._descriptionText = element2;
    }

    protected abstract string GetPublishedObjectDisplayName();

    protected abstract List<WorkshopTagOption> GetTagsToShow();

    private void Click_GoBack(UIMouseEvent evt, UIElement listeningElement) => this.HandleBackButtonUsage();

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
      }
    }

    private void Click_Publish(UIMouseEvent evt, UIElement listeningElement) => this.GoToPublishConfirmation();

    protected abstract void GoToPublishConfirmation();

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

    private void AddPublishButton(int backButtonYLift, UIElement outerContainer)
    {
      UITextPanel<LocalizedText> element = new UITextPanel<LocalizedText>(Language.GetText("Workshop.Publish"), 0.7f, true);
      element.Width.Set(-10f, 0.5f);
      element.Height.Set(50f, 0.0f);
      element.VAlign = 1f;
      element.Top.Set((float) -backButtonYLift, 0.0f);
      element.HAlign = 1f;
      element.OnMouseOver += new UIElement.MouseEvent(this.FadedMouseOver);
      element.OnMouseOut += new UIElement.MouseEvent(this.FadedMouseOut);
      element.OnLeftClick += new UIElement.MouseEvent(this.Click_Publish);
      element.SetSnapPoint("publish", 0);
      outerContainer.Append((UIElement) element);
      this._publishButton = (UIElement) element;
    }

    private void AddBackButton(int backButtonYLift, UIElement outerContainer)
    {
      UITextPanel<LocalizedText> element = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
      element.Width.Set(-10f, 0.5f);
      element.Height.Set(50f, 0.0f);
      element.VAlign = 1f;
      element.Top.Set((float) -backButtonYLift, 0.0f);
      element.HAlign = 0.0f;
      element.OnMouseOver += new UIElement.MouseEvent(this.FadedMouseOver);
      element.OnMouseOut += new UIElement.MouseEvent(this.FadedMouseOut);
      element.OnLeftClick += new UIElement.MouseEvent(this.Click_GoBack);
      element.SetSnapPoint("back", 0);
      outerContainer.Append((UIElement) element);
      this._backButton = (UIElement) element;
    }

    private UIElement AddHorizontalSeparator(
      UIElement Container,
      float accumualtedHeight,
      int widthReduction = 0)
    {
      UIHorizontalSeparator horizontalSeparator = new UIHorizontalSeparator();
      horizontalSeparator.Width = StyleDimension.FromPixelsAndPercent((float) -widthReduction, 1f);
      horizontalSeparator.HAlign = 0.5f;
      horizontalSeparator.Top = StyleDimension.FromPixels(accumualtedHeight - 8f);
      horizontalSeparator.Color = Color.Lerp(Color.White, new Color(63, 65, 151, (int) byte.MaxValue), 0.85f) * 0.9f;
      UIHorizontalSeparator element = horizontalSeparator;
      Container.Append((UIElement) element);
      return (UIElement) element;
    }

    protected WorkshopItemPublishSettings GetPublishSettings() => new WorkshopItemPublishSettings()
    {
      Publicity = this._optionPublicity,
      UsedTags = this._tagOptions.Where<GroupOptionButton<WorkshopTagOption>>((Func<GroupOptionButton<WorkshopTagOption>, bool>) (x => x.IsSelected)).Select<GroupOptionButton<WorkshopTagOption>, WorkshopTagOption>((Func<GroupOptionButton<WorkshopTagOption>, WorkshopTagOption>) (x => x.OptionValue)).ToArray<WorkshopTagOption>(),
      PreviewImagePath = this._previewImagePath
    };

    private void OpenFileDialogueToSelectPreviewImage()
    {
      string str = Terraria.Utilities.FileBrowser.FileBrowser.OpenFilePanel("Open icon", new ExtensionFilter[1]
      {
        new ExtensionFilter("Image files", new string[3]
        {
          "png",
          "jpg",
          "jpeg"
        })
      });
      if (str == null)
        return;
      this._previewImagePath = str;
      this.UpdateImagePreview();
    }

    private string PrettifyPath(string path)
    {
      if (path == null)
        return path;
      char[] anyOf = new char[2]
      {
        Path.DirectorySeparatorChar,
        Path.AltDirectorySeparatorChar
      };
      int num = path.LastIndexOfAny(anyOf);
      if (num != -1)
        path = path.Substring(num + 1);
      if (path.Length > 30)
        path = path.Substring(0, 30) + "…";
      return path;
    }

    private void UpdateImagePreview()
    {
      Texture2D nonReloadingTexture = (Texture2D) null;
      this._previewImagePathPlate.SetContents(this.PrettifyPath(this._previewImagePath));
      if (this._previewImagePath != null)
      {
        try
        {
          FileStream fileStream = File.OpenRead(this._previewImagePath);
          nonReloadingTexture = Texture2D.FromStream(Main.graphics.GraphicsDevice, (Stream) fileStream);
        }
        catch (Exception ex)
        {
          string previewImagePath = this._previewImagePath;
          FancyErrorPrinter.ShowFailedToLoadAssetError(ex, previewImagePath);
        }
      }
      if (nonReloadingTexture != null && (nonReloadingTexture.Width > 512 || nonReloadingTexture.Height > 512))
      {
        string textValueWith = Language.GetTextValueWith("Workshop.ReportIssue_FailedToPublish_ImageSizeIsTooLarge", (object) new
        {
          Width = nonReloadingTexture.Width,
          Height = nonReloadingTexture.Height
        });
        if (SocialAPI.Workshop != null)
          SocialAPI.Workshop.IssueReporter.ReportInstantUploadProblemFromValue(textValueWith);
        this._previewImagePath = (string) null;
        this._previewImagePathPlate.SetContents((string) null);
        this._previewImageUIElement.SetImage(this._defaultPreviewImageTexture);
      }
      else
      {
        if (this._previewImageTransientTexture != null)
        {
          this._previewImageTransientTexture.Dispose();
          this._previewImageTransientTexture = (Texture2D) null;
        }
        if (nonReloadingTexture != null)
        {
          this._previewImageUIElement.SetImage(nonReloadingTexture);
          this._previewImageTransientTexture = nonReloadingTexture;
        }
        else
          this._previewImageUIElement.SetImage(this._defaultPreviewImageTexture);
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
      List<SnapPoint> snapPoints = this.GetSnapPoints();
      this._helper.RemovePointsOutOfView(snapPoints, this._listContainer, spriteBatch);
      ref UIGamepadHelper local1 = ref this._helper;
      int id1 = num1;
      int num2 = id1 + 1;
      UIElement backButton = this._backButton;
      UILinkPoint linkPoint1 = local1.GetLinkPoint(id1, backButton);
      ref UIGamepadHelper local2 = ref this._helper;
      int id2 = num2;
      int idRangeEndExclusive = id2 + 1;
      UIElement publishButton = this._publishButton;
      UILinkPoint linkPoint2 = local2.GetLinkPoint(id2, publishButton);
      SnapPoint snap1 = (SnapPoint) null;
      SnapPoint snap2 = (SnapPoint) null;
      for (int index = 0; index < snapPoints.Count; ++index)
      {
        SnapPoint snapPoint = snapPoints[index];
        string name = snapPoint.Name;
        if (!(name == "disclaimer"))
        {
          if (name == "image")
            snap2 = snapPoint;
        }
        else
          snap1 = snapPoint;
      }
      UILinkPoint upSide = this._helper.TryMakeLinkPoint(ref idRangeEndExclusive, snap1);
      UILinkPoint uiLinkPoint = this._helper.TryMakeLinkPoint(ref idRangeEndExclusive, snap2);
      this._helper.PairLeftRight(linkPoint1, linkPoint2);
      this._helper.PairUpDown(upSide, uiLinkPoint);
      UILinkPoint[] linkStripHorizontal = this._helper.CreateUILinkStripHorizontal(ref idRangeEndExclusive, snapPoints.Where<SnapPoint>((Func<SnapPoint, bool>) (x => x.Name == "public")).ToList<SnapPoint>());
      if (linkStripHorizontal.Length != 0)
        this._helper.LinkHorizontalStripUpSideToSingle(linkStripHorizontal, uiLinkPoint);
      UILinkPoint topLinkPoint = linkStripHorizontal.Length != 0 ? linkStripHorizontal[0] : (UILinkPoint) null;
      UILinkPoint bottomLinkPoint = linkPoint1;
      List<SnapPoint> list = snapPoints.Where<SnapPoint>((Func<SnapPoint, bool>) (x => x.Name == "tags")).ToList<SnapPoint>();
      UILinkPoint[,] uiLinkPointGrid = this._helper.CreateUILinkPointGrid(ref idRangeEndExclusive, list, 3, topLinkPoint, (UILinkPoint) null, (UILinkPoint) null, bottomLinkPoint);
      int index1 = uiLinkPointGrid.GetLength(1) - 1;
      if (index1 >= 0)
      {
        this._helper.LinkHorizontalStripBottomSideToSingle(linkStripHorizontal, uiLinkPointGrid[0, 0]);
        for (int index2 = uiLinkPointGrid.GetLength(0) - 1; index2 >= 0; --index2)
        {
          if (uiLinkPointGrid[index2, index1] != null)
          {
            this._helper.PairUpDown(uiLinkPointGrid[index2, index1], linkPoint2);
            break;
          }
        }
      }
      this._helper.PairUpDown(UILinkPointNavigator.Points[idRangeEndExclusive - 1], linkPoint1);
      this._helper.MoveToVisuallyClosestPoint(idRangeStartInclusive, idRangeEndExclusive);
    }
  }
}
