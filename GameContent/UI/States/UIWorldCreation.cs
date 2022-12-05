// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.States.UIWorldCreation
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
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
  public class UIWorldCreation : UIState
  {
    private UIWorldCreation.WorldSizeId _optionSize;
    private UIWorldCreation.WorldDifficultyId _optionDifficulty;
    private UIWorldCreation.WorldEvilId _optionEvil;
    private string _optionwWorldName;
    private string _optionSeed;
    private UICharacterNameButton _namePlate;
    private UICharacterNameButton _seedPlate;
    private UIWorldCreationPreview _previewPlate;
    private GroupOptionButton<UIWorldCreation.WorldSizeId>[] _sizeButtons;
    private GroupOptionButton<UIWorldCreation.WorldDifficultyId>[] _difficultyButtons;
    private GroupOptionButton<UIWorldCreation.WorldEvilId>[] _evilButtons;
    private UIText _descriptionText;
    public const int MAX_NAME_LENGTH = 27;
    public const int MAX_SEED_LENGTH = 40;

    public UIWorldCreation() => this.BuildPage();

    private void BuildPage()
    {
      int num = 18;
      this.RemoveAllChildren();
      UIElement uiElement1 = new UIElement()
      {
        Width = StyleDimension.FromPixels(500f),
        Height = StyleDimension.FromPixels(434f + (float) num),
        Top = StyleDimension.FromPixels(170f - (float) num),
        HAlign = 0.5f,
        VAlign = 0.0f
      };
      uiElement1.SetPadding(0.0f);
      this.Append(uiElement1);
      UIPanel uiPanel = new UIPanel();
      uiPanel.Width = StyleDimension.FromPercent(1f);
      uiPanel.Height = StyleDimension.FromPixels((float) (280 + num));
      uiPanel.Top = StyleDimension.FromPixels(50f);
      uiPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
      UIPanel element = uiPanel;
      element.SetPadding(0.0f);
      uiElement1.Append((UIElement) element);
      this.MakeBackAndCreatebuttons(uiElement1);
      UIElement uiElement2 = new UIElement()
      {
        Top = StyleDimension.FromPixelsAndPercent(0.0f, 0.0f),
        Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f),
        Height = StyleDimension.FromPixelsAndPercent(0.0f, 1f),
        HAlign = 1f
      };
      uiElement2.SetPadding(0.0f);
      uiElement2.PaddingTop = 8f;
      uiElement2.PaddingBottom = 12f;
      element.Append(uiElement2);
      this.MakeInfoMenu(uiElement2);
    }

    private void MakeInfoMenu(UIElement parentContainer)
    {
      UIElement uiElement = new UIElement()
      {
        Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f),
        Height = StyleDimension.FromPixelsAndPercent(0.0f, 1f),
        HAlign = 0.5f,
        VAlign = 0.0f
      };
      uiElement.SetPadding(10f);
      uiElement.PaddingBottom = 0.0f;
      uiElement.PaddingTop = 0.0f;
      parentContainer.Append(uiElement);
      float pixels1 = 0.0f;
      float num1 = 44f;
      float num2 = (float) (88.0 + (double) num1);
      float pixels2 = num1;
      GroupOptionButton<bool> groupOptionButton1 = new GroupOptionButton<bool>(true, (LocalizedText) null, Language.GetText("UI.WorldCreationRandomizeNameDescription"), Color.White, "Images/UI/WorldCreation/IconRandomName");
      groupOptionButton1.Width = StyleDimension.FromPixelsAndPercent(40f, 0.0f);
      groupOptionButton1.Height = new StyleDimension(40f, 0.0f);
      groupOptionButton1.HAlign = 0.0f;
      groupOptionButton1.Top = StyleDimension.FromPixelsAndPercent(pixels1, 0.0f);
      groupOptionButton1.ShowHighlightWhenSelected = false;
      GroupOptionButton<bool> element1 = groupOptionButton1;
      element1.OnLeftMouseDown += new UIElement.MouseEvent(this.ClickRandomizeName);
      element1.OnMouseOver += new UIElement.MouseEvent(this.ShowOptionDescription);
      element1.OnMouseOut += new UIElement.MouseEvent(this.ClearOptionDescription);
      element1.SetSnapPoint("RandomizeName", 0);
      uiElement.Append((UIElement) element1);
      UICharacterNameButton characterNameButton1 = new UICharacterNameButton(Language.GetText("UI.WorldCreationName"), Language.GetText("UI.WorldCreationNameEmpty"), Language.GetText("UI.WorldDescriptionName"));
      characterNameButton1.Width = StyleDimension.FromPixelsAndPercent(-num2, 1f);
      characterNameButton1.HAlign = 0.0f;
      characterNameButton1.Left = new StyleDimension(pixels2, 0.0f);
      characterNameButton1.Top = StyleDimension.FromPixelsAndPercent(pixels1, 0.0f);
      UICharacterNameButton element2 = characterNameButton1;
      element2.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_SetName);
      element2.OnMouseOver += new UIElement.MouseEvent(this.ShowOptionDescription);
      element2.OnMouseOut += new UIElement.MouseEvent(this.ClearOptionDescription);
      element2.SetSnapPoint("Name", 0);
      uiElement.Append((UIElement) element2);
      this._namePlate = element2;
      CalculatedStyle dimensions1 = element2.GetDimensions();
      float pixels3 = pixels1 + (dimensions1.Height + 4f);
      GroupOptionButton<bool> groupOptionButton2 = new GroupOptionButton<bool>(true, (LocalizedText) null, Language.GetText("UI.WorldCreationRandomizeSeedDescription"), Color.White, "Images/UI/WorldCreation/IconRandomSeed");
      groupOptionButton2.Width = StyleDimension.FromPixelsAndPercent(40f, 0.0f);
      groupOptionButton2.Height = new StyleDimension(40f, 0.0f);
      groupOptionButton2.HAlign = 0.0f;
      groupOptionButton2.Top = StyleDimension.FromPixelsAndPercent(pixels3, 0.0f);
      groupOptionButton2.ShowHighlightWhenSelected = false;
      GroupOptionButton<bool> element3 = groupOptionButton2;
      element3.OnLeftMouseDown += new UIElement.MouseEvent(this.ClickRandomizeSeed);
      element3.OnMouseOver += new UIElement.MouseEvent(this.ShowOptionDescription);
      element3.OnMouseOut += new UIElement.MouseEvent(this.ClearOptionDescription);
      element3.SetSnapPoint("RandomizeSeed", 0);
      uiElement.Append((UIElement) element3);
      UICharacterNameButton characterNameButton2 = new UICharacterNameButton(Language.GetText("UI.WorldCreationSeed"), Language.GetText("UI.WorldCreationSeedEmpty"), Language.GetText("UI.WorldDescriptionSeed"));
      characterNameButton2.Width = StyleDimension.FromPixelsAndPercent(-num2, 1f);
      characterNameButton2.HAlign = 0.0f;
      characterNameButton2.Left = new StyleDimension(pixels2, 0.0f);
      characterNameButton2.Top = StyleDimension.FromPixelsAndPercent(pixels3, 0.0f);
      characterNameButton2.DistanceFromTitleToOption = 29f;
      UICharacterNameButton element4 = characterNameButton2;
      element4.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_SetSeed);
      element4.OnMouseOver += new UIElement.MouseEvent(this.ShowOptionDescription);
      element4.OnMouseOut += new UIElement.MouseEvent(this.ClearOptionDescription);
      element4.SetSnapPoint("Seed", 0);
      uiElement.Append((UIElement) element4);
      this._seedPlate = element4;
      UIWorldCreationPreview worldCreationPreview = new UIWorldCreationPreview();
      worldCreationPreview.Width = StyleDimension.FromPixels(84f);
      worldCreationPreview.Height = StyleDimension.FromPixels(84f);
      worldCreationPreview.HAlign = 1f;
      worldCreationPreview.VAlign = 0.0f;
      UIWorldCreationPreview element5 = worldCreationPreview;
      uiElement.Append((UIElement) element5);
      this._previewPlate = element5;
      CalculatedStyle dimensions2 = element4.GetDimensions();
      float accumualtedHeight1 = pixels3 + (dimensions2.Height + 10f);
      UIWorldCreation.AddHorizontalSeparator(uiElement, accumualtedHeight1 + 2f);
      float usableWidthPercent = 1f;
      this.AddWorldSizeOptions(uiElement, accumualtedHeight1, new UIElement.MouseEvent(this.ClickSizeOption), "size", usableWidthPercent);
      float accumualtedHeight2 = accumualtedHeight1 + 48f;
      UIWorldCreation.AddHorizontalSeparator(uiElement, accumualtedHeight2);
      this.AddWorldDifficultyOptions(uiElement, accumualtedHeight2, new UIElement.MouseEvent(this.ClickDifficultyOption), "difficulty", usableWidthPercent);
      float accumualtedHeight3 = accumualtedHeight2 + 48f;
      UIWorldCreation.AddHorizontalSeparator(uiElement, accumualtedHeight3);
      this.AddWorldEvilOptions(uiElement, accumualtedHeight3, new UIElement.MouseEvent(this.ClickEvilOption), "evil", usableWidthPercent);
      float num3 = accumualtedHeight3 + 48f;
      UIWorldCreation.AddHorizontalSeparator(uiElement, num3);
      this.AddDescriptionPanel(uiElement, num3, "desc");
      this.SetDefaultOptions();
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

    private void SetDefaultOptions()
    {
      this.AssignRandomWorldName();
      this.AssignRandomWorldSeed();
      this.UpdateInputFields();
      foreach (GroupOptionButton<UIWorldCreation.WorldSizeId> sizeButton in this._sizeButtons)
        sizeButton.SetCurrentOption(UIWorldCreation.WorldSizeId.Medium);
      this._optionSize = UIWorldCreation.WorldSizeId.Medium;
      if (Main.ActivePlayerFileData.Player.difficulty == (byte) 3)
      {
        foreach (GroupOptionButton<UIWorldCreation.WorldDifficultyId> difficultyButton in this._difficultyButtons)
          difficultyButton.SetCurrentOption(UIWorldCreation.WorldDifficultyId.Creative);
        this._optionDifficulty = UIWorldCreation.WorldDifficultyId.Creative;
        this.UpdatePreviewPlate();
      }
      else
      {
        foreach (GroupOptionButton<UIWorldCreation.WorldDifficultyId> difficultyButton in this._difficultyButtons)
          difficultyButton.SetCurrentOption(UIWorldCreation.WorldDifficultyId.Normal);
      }
      foreach (GroupOptionButton<UIWorldCreation.WorldEvilId> evilButton in this._evilButtons)
        evilButton.SetCurrentOption(UIWorldCreation.WorldEvilId.Random);
    }

    private void AddDescriptionPanel(UIElement container, float accumulatedHeight, string tagGroup)
    {
      float num = 0.0f;
      UISlicedImage uiSlicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", (AssetRequestMode) 1));
      uiSlicedImage.HAlign = 0.5f;
      uiSlicedImage.VAlign = 1f;
      uiSlicedImage.Width = StyleDimension.FromPixelsAndPercent((float) (-(double) num * 2.0), 1f);
      uiSlicedImage.Left = StyleDimension.FromPixels(-num);
      uiSlicedImage.Height = StyleDimension.FromPixelsAndPercent(40f, 0.0f);
      uiSlicedImage.Top = StyleDimension.FromPixels(2f);
      UISlicedImage element1 = uiSlicedImage;
      element1.SetSliceDepths(10);
      element1.Color = Color.LightGray * 0.7f;
      container.Append((UIElement) element1);
      UIText uiText = new UIText(Language.GetText("UI.WorldDescriptionDefault"), 0.82f);
      uiText.HAlign = 0.0f;
      uiText.VAlign = 0.0f;
      uiText.Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiText.Height = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiText.Top = StyleDimension.FromPixelsAndPercent(5f, 0.0f);
      UIText element2 = uiText;
      element2.PaddingLeft = 20f;
      element2.PaddingRight = 20f;
      element2.PaddingTop = 6f;
      element1.Append((UIElement) element2);
      this._descriptionText = element2;
    }

    private void AddWorldSizeOptions(
      UIElement container,
      float accumualtedHeight,
      UIElement.MouseEvent clickEvent,
      string tagGroup,
      float usableWidthPercent)
    {
      UIWorldCreation.WorldSizeId[] worldSizeIdArray = new UIWorldCreation.WorldSizeId[3]
      {
        UIWorldCreation.WorldSizeId.Small,
        UIWorldCreation.WorldSizeId.Medium,
        UIWorldCreation.WorldSizeId.Large
      };
      LocalizedText[] localizedTextArray1 = new LocalizedText[3]
      {
        Lang.menu[92],
        Lang.menu[93],
        Lang.menu[94]
      };
      LocalizedText[] localizedTextArray2 = new LocalizedText[3]
      {
        Language.GetText("UI.WorldDescriptionSizeSmall"),
        Language.GetText("UI.WorldDescriptionSizeMedium"),
        Language.GetText("UI.WorldDescriptionSizeLarge")
      };
      Color[] colorArray = new Color[3]
      {
        Color.Cyan,
        Color.Lerp(Color.Cyan, Color.LimeGreen, 0.5f),
        Color.LimeGreen
      };
      string[] strArray = new string[3]
      {
        "Images/UI/WorldCreation/IconSizeSmall",
        "Images/UI/WorldCreation/IconSizeMedium",
        "Images/UI/WorldCreation/IconSizeLarge"
      };
      GroupOptionButton<UIWorldCreation.WorldSizeId>[] groupOptionButtonArray = new GroupOptionButton<UIWorldCreation.WorldSizeId>[worldSizeIdArray.Length];
      for (int id = 0; id < groupOptionButtonArray.Length; ++id)
      {
        GroupOptionButton<UIWorldCreation.WorldSizeId> element = new GroupOptionButton<UIWorldCreation.WorldSizeId>(worldSizeIdArray[id], localizedTextArray1[id], localizedTextArray2[id], colorArray[id], strArray[id], titleAlignmentX: 1f, titleWidthReduction: 16f);
        element.Width = StyleDimension.FromPixelsAndPercent((float) (-4 * (groupOptionButtonArray.Length - 1)), 1f / (float) groupOptionButtonArray.Length * usableWidthPercent);
        element.Left = StyleDimension.FromPercent(1f - usableWidthPercent);
        element.HAlign = (float) id / (float) (groupOptionButtonArray.Length - 1);
        element.Top.Set(accumualtedHeight, 0.0f);
        element.OnLeftMouseDown += clickEvent;
        element.OnMouseOver += new UIElement.MouseEvent(this.ShowOptionDescription);
        element.OnMouseOut += new UIElement.MouseEvent(this.ClearOptionDescription);
        element.SetSnapPoint(tagGroup, id);
        container.Append((UIElement) element);
        groupOptionButtonArray[id] = element;
      }
      this._sizeButtons = groupOptionButtonArray;
    }

    private void AddWorldDifficultyOptions(
      UIElement container,
      float accumualtedHeight,
      UIElement.MouseEvent clickEvent,
      string tagGroup,
      float usableWidthPercent)
    {
      UIWorldCreation.WorldDifficultyId[] worldDifficultyIdArray = new UIWorldCreation.WorldDifficultyId[4]
      {
        UIWorldCreation.WorldDifficultyId.Creative,
        UIWorldCreation.WorldDifficultyId.Normal,
        UIWorldCreation.WorldDifficultyId.Expert,
        UIWorldCreation.WorldDifficultyId.Master
      };
      LocalizedText[] localizedTextArray1 = new LocalizedText[4]
      {
        Language.GetText("UI.Creative"),
        Language.GetText("UI.Normal"),
        Language.GetText("UI.Expert"),
        Language.GetText("UI.Master")
      };
      LocalizedText[] localizedTextArray2 = new LocalizedText[4]
      {
        Language.GetText("UI.WorldDescriptionCreative"),
        Language.GetText("UI.WorldDescriptionNormal"),
        Language.GetText("UI.WorldDescriptionExpert"),
        Language.GetText("UI.WorldDescriptionMaster")
      };
      Color[] colorArray = new Color[4]
      {
        Main.creativeModeColor,
        Color.White,
        Main.mcColor,
        Main.hcColor
      };
      string[] strArray = new string[4]
      {
        "Images/UI/WorldCreation/IconDifficultyCreative",
        "Images/UI/WorldCreation/IconDifficultyNormal",
        "Images/UI/WorldCreation/IconDifficultyExpert",
        "Images/UI/WorldCreation/IconDifficultyMaster"
      };
      GroupOptionButton<UIWorldCreation.WorldDifficultyId>[] groupOptionButtonArray = new GroupOptionButton<UIWorldCreation.WorldDifficultyId>[worldDifficultyIdArray.Length];
      for (int id = 0; id < groupOptionButtonArray.Length; ++id)
      {
        GroupOptionButton<UIWorldCreation.WorldDifficultyId> element = new GroupOptionButton<UIWorldCreation.WorldDifficultyId>(worldDifficultyIdArray[id], localizedTextArray1[id], localizedTextArray2[id], colorArray[id], strArray[id], titleAlignmentX: 1f, titleWidthReduction: 16f);
        element.Width = StyleDimension.FromPixelsAndPercent((float) (-1 * (groupOptionButtonArray.Length - 1)), 1f / (float) groupOptionButtonArray.Length * usableWidthPercent);
        element.Left = StyleDimension.FromPercent(1f - usableWidthPercent);
        element.HAlign = (float) id / (float) (groupOptionButtonArray.Length - 1);
        element.Top.Set(accumualtedHeight, 0.0f);
        element.OnLeftMouseDown += clickEvent;
        element.OnMouseOver += new UIElement.MouseEvent(this.ShowOptionDescription);
        element.OnMouseOut += new UIElement.MouseEvent(this.ClearOptionDescription);
        element.SetSnapPoint(tagGroup, id);
        container.Append((UIElement) element);
        groupOptionButtonArray[id] = element;
      }
      this._difficultyButtons = groupOptionButtonArray;
    }

    private void AddWorldEvilOptions(
      UIElement container,
      float accumualtedHeight,
      UIElement.MouseEvent clickEvent,
      string tagGroup,
      float usableWidthPercent)
    {
      UIWorldCreation.WorldEvilId[] worldEvilIdArray = new UIWorldCreation.WorldEvilId[3]
      {
        UIWorldCreation.WorldEvilId.Random,
        UIWorldCreation.WorldEvilId.Corruption,
        UIWorldCreation.WorldEvilId.Crimson
      };
      LocalizedText[] localizedTextArray1 = new LocalizedText[3]
      {
        Lang.misc[103],
        Lang.misc[101],
        Lang.misc[102]
      };
      LocalizedText[] localizedTextArray2 = new LocalizedText[3]
      {
        Language.GetText("UI.WorldDescriptionEvilRandom"),
        Language.GetText("UI.WorldDescriptionEvilCorrupt"),
        Language.GetText("UI.WorldDescriptionEvilCrimson")
      };
      Color[] colorArray = new Color[3]
      {
        Color.White,
        Color.MediumPurple,
        Color.IndianRed
      };
      string[] strArray = new string[3]
      {
        "Images/UI/WorldCreation/IconEvilRandom",
        "Images/UI/WorldCreation/IconEvilCorruption",
        "Images/UI/WorldCreation/IconEvilCrimson"
      };
      GroupOptionButton<UIWorldCreation.WorldEvilId>[] groupOptionButtonArray = new GroupOptionButton<UIWorldCreation.WorldEvilId>[worldEvilIdArray.Length];
      for (int id = 0; id < groupOptionButtonArray.Length; ++id)
      {
        GroupOptionButton<UIWorldCreation.WorldEvilId> element = new GroupOptionButton<UIWorldCreation.WorldEvilId>(worldEvilIdArray[id], localizedTextArray1[id], localizedTextArray2[id], colorArray[id], strArray[id], titleAlignmentX: 1f, titleWidthReduction: 16f);
        element.Width = StyleDimension.FromPixelsAndPercent((float) (-4 * (groupOptionButtonArray.Length - 1)), 1f / (float) groupOptionButtonArray.Length * usableWidthPercent);
        element.Left = StyleDimension.FromPercent(1f - usableWidthPercent);
        element.HAlign = (float) id / (float) (groupOptionButtonArray.Length - 1);
        element.Top.Set(accumualtedHeight, 0.0f);
        element.OnLeftMouseDown += clickEvent;
        element.OnMouseOver += new UIElement.MouseEvent(this.ShowOptionDescription);
        element.OnMouseOut += new UIElement.MouseEvent(this.ClearOptionDescription);
        element.SetSnapPoint(tagGroup, id);
        container.Append((UIElement) element);
        groupOptionButtonArray[id] = element;
      }
      this._evilButtons = groupOptionButtonArray;
    }

    private void ClickRandomizeName(UIMouseEvent evt, UIElement listeningElement)
    {
      this.AssignRandomWorldName();
      this.UpdateInputFields();
      this.UpdateSliders();
      this.UpdatePreviewPlate();
    }

    private void ClickRandomizeSeed(UIMouseEvent evt, UIElement listeningElement)
    {
      this.AssignRandomWorldSeed();
      this.UpdateInputFields();
      this.UpdateSliders();
      this.UpdatePreviewPlate();
    }

    private void ClickSizeOption(UIMouseEvent evt, UIElement listeningElement)
    {
      GroupOptionButton<UIWorldCreation.WorldSizeId> groupOptionButton = (GroupOptionButton<UIWorldCreation.WorldSizeId>) listeningElement;
      this._optionSize = groupOptionButton.OptionValue;
      foreach (GroupOptionButton<UIWorldCreation.WorldSizeId> sizeButton in this._sizeButtons)
        sizeButton.SetCurrentOption(groupOptionButton.OptionValue);
      this.UpdatePreviewPlate();
    }

    private void ClickDifficultyOption(UIMouseEvent evt, UIElement listeningElement)
    {
      GroupOptionButton<UIWorldCreation.WorldDifficultyId> groupOptionButton = (GroupOptionButton<UIWorldCreation.WorldDifficultyId>) listeningElement;
      this._optionDifficulty = groupOptionButton.OptionValue;
      foreach (GroupOptionButton<UIWorldCreation.WorldDifficultyId> difficultyButton in this._difficultyButtons)
        difficultyButton.SetCurrentOption(groupOptionButton.OptionValue);
      this.UpdatePreviewPlate();
    }

    private void ClickEvilOption(UIMouseEvent evt, UIElement listeningElement)
    {
      GroupOptionButton<UIWorldCreation.WorldEvilId> groupOptionButton = (GroupOptionButton<UIWorldCreation.WorldEvilId>) listeningElement;
      this._optionEvil = groupOptionButton.OptionValue;
      foreach (GroupOptionButton<UIWorldCreation.WorldEvilId> evilButton in this._evilButtons)
        evilButton.SetCurrentOption(groupOptionButton.OptionValue);
      this.UpdatePreviewPlate();
    }

    private void UpdatePreviewPlate() => this._previewPlate.UpdateOption((byte) this._optionDifficulty, (byte) this._optionEvil, (byte) this._optionSize);

    private void UpdateSliders()
    {
      foreach (GroupOptionButton<UIWorldCreation.WorldSizeId> sizeButton in this._sizeButtons)
        sizeButton.SetCurrentOption(this._optionSize);
      foreach (GroupOptionButton<UIWorldCreation.WorldDifficultyId> difficultyButton in this._difficultyButtons)
        difficultyButton.SetCurrentOption(this._optionDifficulty);
      foreach (GroupOptionButton<UIWorldCreation.WorldEvilId> evilButton in this._evilButtons)
        evilButton.SetCurrentOption(this._optionEvil);
    }

    public void ShowOptionDescription(UIMouseEvent evt, UIElement listeningElement)
    {
      LocalizedText text = (LocalizedText) null;
      if (listeningElement is GroupOptionButton<UIWorldCreation.WorldSizeId> groupOptionButton1)
        text = groupOptionButton1.Description;
      if (listeningElement is GroupOptionButton<UIWorldCreation.WorldDifficultyId> groupOptionButton2)
        text = groupOptionButton2.Description;
      if (listeningElement is GroupOptionButton<UIWorldCreation.WorldEvilId> groupOptionButton3)
        text = groupOptionButton3.Description;
      if (listeningElement is UICharacterNameButton characterNameButton)
        text = characterNameButton.Description;
      if (listeningElement is GroupOptionButton<bool> groupOptionButton4)
        text = groupOptionButton4.Description;
      if (text == null)
        return;
      this._descriptionText.SetText(text);
    }

    public void ClearOptionDescription(UIMouseEvent evt, UIElement listeningElement) => this._descriptionText.SetText(Language.GetText("UI.WorldDescriptionDefault"));

    private void MakeBackAndCreatebuttons(UIElement outerContainer)
    {
      UITextPanel<LocalizedText> uiTextPanel1 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
      uiTextPanel1.Width = StyleDimension.FromPixelsAndPercent(-10f, 0.5f);
      uiTextPanel1.Height = StyleDimension.FromPixels(50f);
      uiTextPanel1.VAlign = 1f;
      uiTextPanel1.HAlign = 0.0f;
      uiTextPanel1.Top = StyleDimension.FromPixels(-45f);
      UITextPanel<LocalizedText> element1 = uiTextPanel1;
      element1.OnMouseOver += new UIElement.MouseEvent(this.FadedMouseOver);
      element1.OnMouseOut += new UIElement.MouseEvent(this.FadedMouseOut);
      element1.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_GoBack);
      element1.SetSnapPoint("Back", 0);
      outerContainer.Append((UIElement) element1);
      UITextPanel<LocalizedText> uiTextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Create"), 0.7f, true);
      uiTextPanel2.Width = StyleDimension.FromPixelsAndPercent(-10f, 0.5f);
      uiTextPanel2.Height = StyleDimension.FromPixels(50f);
      uiTextPanel2.VAlign = 1f;
      uiTextPanel2.HAlign = 1f;
      uiTextPanel2.Top = StyleDimension.FromPixels(-45f);
      UITextPanel<LocalizedText> element2 = uiTextPanel2;
      element2.OnMouseOver += new UIElement.MouseEvent(this.FadedMouseOver);
      element2.OnMouseOut += new UIElement.MouseEvent(this.FadedMouseOut);
      element2.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_NamingAndCreating);
      element2.SetSnapPoint("Create", 0);
      outerContainer.Append((UIElement) element2);
    }

    private void Click_GoBack(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(11);
      Main.OpenWorldSelectUI();
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

    private void Click_SetName(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(10);
      Main.clrInput();
      UIVirtualKeyboard state = new UIVirtualKeyboard(Lang.menu[48].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingName), new Action(this.GoBackHere), allowEmpty: true);
      state.SetMaxInputLength(27);
      Main.MenuUI.SetState((UIState) state);
    }

    private void Click_SetSeed(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(10);
      Main.clrInput();
      UIVirtualKeyboard state = new UIVirtualKeyboard(Language.GetTextValue("UI.EnterSeed"), "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingSeed), new Action(this.GoBackHere), allowEmpty: true);
      state.SetMaxInputLength(40);
      Main.MenuUI.SetState((UIState) state);
    }

    private void Click_NamingAndCreating(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(10);
      if (string.IsNullOrEmpty(this._optionwWorldName))
      {
        this._optionwWorldName = "";
        Main.clrInput();
        UIVirtualKeyboard state = new UIVirtualKeyboard(Lang.menu[48].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedNamingAndCreating), new Action(this.GoBackHere));
        state.SetMaxInputLength(27);
        Main.MenuUI.SetState((UIState) state);
      }
      else
        this.FinishCreatingWorld();
    }

    private void OnFinishedSettingName(string name)
    {
      this._optionwWorldName = name.Trim();
      this.UpdateInputFields();
      this.GoBackHere();
    }

    private void UpdateInputFields()
    {
      this._namePlate.SetContents(this._optionwWorldName);
      this._namePlate.Recalculate();
      this._namePlate.TrimDisplayIfOverElementDimensions(27);
      this._namePlate.Recalculate();
      this._seedPlate.SetContents(this._optionSeed);
      this._seedPlate.Recalculate();
      this._seedPlate.TrimDisplayIfOverElementDimensions(40);
      this._seedPlate.Recalculate();
    }

    private void OnFinishedSettingSeed(string seed)
    {
      this._optionSeed = seed.Trim();
      string processedSeed;
      this.ProcessSeed(out processedSeed);
      this._optionSeed = processedSeed;
      this.UpdateInputFields();
      this.UpdateSliders();
      this.UpdatePreviewPlate();
      this.GoBackHere();
    }

    private void GoBackHere() => Main.MenuUI.SetState((UIState) this);

    private void OnFinishedNamingAndCreating(string name)
    {
      this.OnFinishedSettingName(name);
      this.FinishCreatingWorld();
    }

    private void FinishCreatingWorld()
    {
      string processedSeed;
      this.ProcessSeed(out processedSeed);
      switch (this._optionSize)
      {
        case UIWorldCreation.WorldSizeId.Small:
          Main.maxTilesX = 4200;
          Main.maxTilesY = 1200;
          break;
        case UIWorldCreation.WorldSizeId.Medium:
          Main.maxTilesX = 6400;
          Main.maxTilesY = 1800;
          break;
        case UIWorldCreation.WorldSizeId.Large:
          Main.maxTilesX = 8400;
          Main.maxTilesY = 2400;
          break;
      }
      WorldGen.setWorldSize();
      switch (this._optionDifficulty)
      {
        case UIWorldCreation.WorldDifficultyId.Normal:
          Main.GameMode = 0;
          break;
        case UIWorldCreation.WorldDifficultyId.Expert:
          Main.GameMode = 1;
          break;
        case UIWorldCreation.WorldDifficultyId.Master:
          Main.GameMode = 2;
          break;
        case UIWorldCreation.WorldDifficultyId.Creative:
          Main.GameMode = 3;
          break;
      }
      switch (this._optionEvil)
      {
        case UIWorldCreation.WorldEvilId.Random:
          WorldGen.WorldGenParam_Evil = -1;
          break;
        case UIWorldCreation.WorldEvilId.Corruption:
          WorldGen.WorldGenParam_Evil = 0;
          break;
        case UIWorldCreation.WorldEvilId.Crimson:
          WorldGen.WorldGenParam_Evil = 1;
          break;
      }
      Main.ActiveWorldFileData = WorldFile.CreateMetadata(Main.worldName = this._optionwWorldName.Trim(), SocialAPI.Cloud != null && SocialAPI.Cloud.EnabledByDefault, Main.GameMode);
      if (processedSeed.Length == 0)
        Main.ActiveWorldFileData.SetSeedToRandom();
      else
        Main.ActiveWorldFileData.SetSeed(processedSeed);
      Main.menuMode = 10;
      WorldGen.CreateNewWorld();
    }

    public static void ProcessSpecialWorldSeeds(string processedSeed)
    {
      WorldGen.noTrapsWorldGen = false;
      WorldGen.notTheBees = false;
      WorldGen.getGoodWorldGen = false;
      WorldGen.tenthAnniversaryWorldGen = false;
      WorldGen.dontStarveWorldGen = false;
      WorldGen.tempRemixWorldGen = false;
      WorldGen.tempTenthAnniversaryWorldGen = false;
      WorldGen.everythingWorldGen = false;
      if (processedSeed.ToLower() == "no traps" || processedSeed.ToLower() == "notraps")
        WorldGen.noTrapsWorldGen = true;
      if (processedSeed.ToLower() == "not the bees" || processedSeed.ToLower() == "not the bees!" || processedSeed.ToLower() == "notthebees")
        WorldGen.notTheBees = true;
      if (processedSeed.ToLower() == "for the worthy" || processedSeed.ToLower() == "fortheworthy")
        WorldGen.getGoodWorldGen = true;
      if (processedSeed.ToLower() == "don't dig up" || processedSeed.ToLower() == "dont dig up" || processedSeed.ToLower() == "dontdigup")
        WorldGen.tempRemixWorldGen = true;
      if (processedSeed.ToLower() == "celebrationmk10")
        WorldGen.tempTenthAnniversaryWorldGen = true;
      if (processedSeed.ToLower() == "constant" || processedSeed.ToLower() == "theconstant" || processedSeed.ToLower() == "the constant" || processedSeed.ToLower() == "eye4aneye" || processedSeed.ToLower() == "eyeforaneye")
        WorldGen.dontStarveWorldGen = true;
      if (!(processedSeed.ToLower() == "get fixed boi") && !(processedSeed.ToLower() == "getfixedboi"))
        return;
      WorldGen.noTrapsWorldGen = true;
      WorldGen.notTheBees = true;
      WorldGen.getGoodWorldGen = true;
      WorldGen.tempTenthAnniversaryWorldGen = true;
      WorldGen.dontStarveWorldGen = true;
      WorldGen.tempRemixWorldGen = true;
      WorldGen.everythingWorldGen = true;
    }

    private void ProcessSeed(out string processedSeed)
    {
      processedSeed = this._optionSeed;
      UIWorldCreation.ProcessSpecialWorldSeeds(processedSeed);
      string[] strArray = this._optionSeed.Split('.');
      if (strArray.Length != 4)
        return;
      int result;
      if (int.TryParse(strArray[0], out result))
      {
        switch (result)
        {
          case 1:
            this._optionSize = UIWorldCreation.WorldSizeId.Small;
            break;
          case 2:
            this._optionSize = UIWorldCreation.WorldSizeId.Medium;
            break;
          case 3:
            this._optionSize = UIWorldCreation.WorldSizeId.Large;
            break;
        }
      }
      if (int.TryParse(strArray[1], out result))
      {
        switch (result)
        {
          case 1:
            this._optionDifficulty = UIWorldCreation.WorldDifficultyId.Normal;
            break;
          case 2:
            this._optionDifficulty = UIWorldCreation.WorldDifficultyId.Expert;
            break;
          case 3:
            this._optionDifficulty = UIWorldCreation.WorldDifficultyId.Master;
            break;
          case 4:
            this._optionDifficulty = UIWorldCreation.WorldDifficultyId.Creative;
            break;
        }
      }
      if (int.TryParse(strArray[2], out result))
      {
        switch (result)
        {
          case 1:
            this._optionEvil = UIWorldCreation.WorldEvilId.Corruption;
            break;
          case 2:
            this._optionEvil = UIWorldCreation.WorldEvilId.Crimson;
            break;
        }
      }
      processedSeed = strArray[3];
    }

    private void AssignRandomWorldName()
    {
      do
      {
        LocalizedText localizedText = Language.SelectRandom(Lang.CreateDialogFilter("RandomWorldName_Composition."));
        var data = new
        {
          Adjective = Language.SelectRandom(Lang.CreateDialogFilter("RandomWorldName_Adjective.")).Value,
          Location = Language.SelectRandom(Lang.CreateDialogFilter("RandomWorldName_Location.")).Value,
          Noun = Language.SelectRandom(Lang.CreateDialogFilter("RandomWorldName_Noun.")).Value
        };
        this._optionwWorldName = localizedText.FormatWith((object) data);
        if (Main.rand.Next(10000) == 0)
          this._optionwWorldName = Language.GetTextValue("SpecialWorldName.TheConstant");
      }
      while (this._optionwWorldName.Length > 27);
    }

    private void AssignRandomWorldSeed() => this._optionSeed = Main.rand.Next().ToString();

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      this.SetupGamepadPoints(spriteBatch);
    }

    private void SetupGamepadPoints(SpriteBatch spriteBatch)
    {
      UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
      int num1 = 3000;
      List<SnapPoint> snapPoints = this.GetSnapPoints();
      SnapPoint snapPoint1 = (SnapPoint) null;
      SnapPoint snapPoint2 = (SnapPoint) null;
      SnapPoint snapPoint3 = (SnapPoint) null;
      SnapPoint snapPoint4 = (SnapPoint) null;
      SnapPoint snapPoint5 = (SnapPoint) null;
      SnapPoint snapPoint6 = (SnapPoint) null;
      for (int index = 0; index < snapPoints.Count; ++index)
      {
        SnapPoint snapPoint7 = snapPoints[index];
        string name = snapPoint7.Name;
        if (!(name == "Back"))
        {
          if (!(name == "Create"))
          {
            if (!(name == "Name"))
            {
              if (!(name == "Seed"))
              {
                if (!(name == "RandomizeName"))
                {
                  if (name == "RandomizeSeed")
                    snapPoint6 = snapPoint7;
                }
                else
                  snapPoint5 = snapPoint7;
              }
              else
                snapPoint4 = snapPoint7;
            }
            else
              snapPoint3 = snapPoint7;
          }
          else
            snapPoint2 = snapPoint7;
        }
        else
          snapPoint1 = snapPoint7;
      }
      List<SnapPoint> snapGroup1 = this.GetSnapGroup(snapPoints, "size");
      List<SnapPoint> snapGroup2 = this.GetSnapGroup(snapPoints, "difficulty");
      List<SnapPoint> snapGroup3 = this.GetSnapGroup(snapPoints, "evil");
      UILinkPointNavigator.SetPosition(num1, snapPoint1.Position);
      UILinkPoint point1 = UILinkPointNavigator.Points[num1];
      point1.Unlink();
      UILinkPoint uiLinkPoint1 = point1;
      int num2 = num1 + 1;
      UILinkPointNavigator.SetPosition(num2, snapPoint2.Position);
      UILinkPoint point2 = UILinkPointNavigator.Points[num2];
      point2.Unlink();
      UILinkPoint uiLinkPoint2 = point2;
      int num3 = num2 + 1;
      UILinkPointNavigator.SetPosition(num3, snapPoint5.Position);
      UILinkPoint point3 = UILinkPointNavigator.Points[num3];
      point3.Unlink();
      UILinkPoint uiLinkPoint3 = point3;
      int num4 = num3 + 1;
      UILinkPointNavigator.SetPosition(num4, snapPoint3.Position);
      UILinkPoint point4 = UILinkPointNavigator.Points[num4];
      point4.Unlink();
      UILinkPoint uiLinkPoint4 = point4;
      int num5 = num4 + 1;
      UILinkPointNavigator.SetPosition(num5, snapPoint6.Position);
      UILinkPoint point5 = UILinkPointNavigator.Points[num5];
      point5.Unlink();
      UILinkPoint uiLinkPoint5 = point5;
      int num6 = num5 + 1;
      UILinkPointNavigator.SetPosition(num6, snapPoint4.Position);
      UILinkPoint point6 = UILinkPointNavigator.Points[num6];
      point6.Unlink();
      UILinkPoint uiLinkPoint6 = point6;
      int num7 = num6 + 1;
      UILinkPoint[] uiLinkPointArray1 = new UILinkPoint[snapGroup1.Count];
      for (int index = 0; index < snapGroup1.Count; ++index)
      {
        UILinkPointNavigator.SetPosition(num7, snapGroup1[index].Position);
        UILinkPoint point7 = UILinkPointNavigator.Points[num7];
        point7.Unlink();
        uiLinkPointArray1[index] = point7;
        ++num7;
      }
      UILinkPoint[] uiLinkPointArray2 = new UILinkPoint[snapGroup2.Count];
      for (int index = 0; index < snapGroup2.Count; ++index)
      {
        UILinkPointNavigator.SetPosition(num7, snapGroup2[index].Position);
        UILinkPoint point8 = UILinkPointNavigator.Points[num7];
        point8.Unlink();
        uiLinkPointArray2[index] = point8;
        ++num7;
      }
      UILinkPoint[] uiLinkPointArray3 = new UILinkPoint[snapGroup3.Count];
      for (int index = 0; index < snapGroup3.Count; ++index)
      {
        UILinkPointNavigator.SetPosition(num7, snapGroup3[index].Position);
        UILinkPoint point9 = UILinkPointNavigator.Points[num7];
        point9.Unlink();
        uiLinkPointArray3[index] = point9;
        ++num7;
      }
      this.LoopHorizontalLineLinks(uiLinkPointArray1);
      this.LoopHorizontalLineLinks(uiLinkPointArray2);
      this.EstablishUpDownRelationship(uiLinkPointArray1, uiLinkPointArray2);
      for (int index = 0; index < uiLinkPointArray1.Length; ++index)
        uiLinkPointArray1[index].Up = uiLinkPoint6.ID;
      if (true)
      {
        this.LoopHorizontalLineLinks(uiLinkPointArray3);
        this.EstablishUpDownRelationship(uiLinkPointArray2, uiLinkPointArray3);
        for (int index = 0; index < uiLinkPointArray3.Length; ++index)
          uiLinkPointArray3[index].Down = uiLinkPoint1.ID;
        uiLinkPointArray3[uiLinkPointArray3.Length - 1].Down = uiLinkPoint2.ID;
        uiLinkPoint2.Up = uiLinkPointArray3[uiLinkPointArray3.Length - 1].ID;
        uiLinkPoint1.Up = uiLinkPointArray3[0].ID;
      }
      else
      {
        for (int index = 0; index < uiLinkPointArray2.Length; ++index)
          uiLinkPointArray2[index].Down = uiLinkPoint1.ID;
        uiLinkPointArray2[uiLinkPointArray2.Length - 1].Down = uiLinkPoint2.ID;
        uiLinkPoint2.Up = uiLinkPointArray2[uiLinkPointArray2.Length - 1].ID;
        uiLinkPoint1.Up = uiLinkPointArray2[0].ID;
      }
      uiLinkPoint2.Left = uiLinkPoint1.ID;
      uiLinkPoint1.Right = uiLinkPoint2.ID;
      uiLinkPoint4.Down = uiLinkPoint6.ID;
      uiLinkPoint4.Left = uiLinkPoint3.ID;
      uiLinkPoint3.Right = uiLinkPoint4.ID;
      uiLinkPoint6.Up = uiLinkPoint4.ID;
      uiLinkPoint6.Down = uiLinkPointArray1[0].ID;
      uiLinkPoint6.Left = uiLinkPoint5.ID;
      uiLinkPoint5.Right = uiLinkPoint6.ID;
      uiLinkPoint5.Up = uiLinkPoint3.ID;
      uiLinkPoint5.Down = uiLinkPointArray1[0].ID;
      uiLinkPoint3.Down = uiLinkPoint5.ID;
    }

    private void EstablishUpDownRelationship(UILinkPoint[] topSide, UILinkPoint[] bottomSide)
    {
      int num = Math.Max(topSide.Length, bottomSide.Length);
      for (int val1 = 0; val1 < num; ++val1)
      {
        int index1 = Math.Min(val1, topSide.Length - 1);
        int index2 = Math.Min(val1, bottomSide.Length - 1);
        topSide[index1].Down = bottomSide[index2].ID;
        bottomSide[index2].Up = topSide[index1].ID;
      }
    }

    private void LoopHorizontalLineLinks(UILinkPoint[] pointsLine)
    {
      for (int index = 1; index < pointsLine.Length - 1; ++index)
      {
        pointsLine[index - 1].Right = pointsLine[index].ID;
        pointsLine[index].Left = pointsLine[index - 1].ID;
        pointsLine[index].Right = pointsLine[index + 1].ID;
        pointsLine[index + 1].Left = pointsLine[index].ID;
      }
    }

    private List<SnapPoint> GetSnapGroup(List<SnapPoint> ptsOnPage, string groupName)
    {
      List<SnapPoint> list = ptsOnPage.Where<SnapPoint>((Func<SnapPoint, bool>) (a => a.Name == groupName)).ToList<SnapPoint>();
      list.Sort(new Comparison<SnapPoint>(this.SortPoints));
      return list;
    }

    private int SortPoints(SnapPoint a, SnapPoint b) => a.Id.CompareTo(b.Id);

    private enum WorldSizeId
    {
      Small,
      Medium,
      Large,
    }

    private enum WorldDifficultyId
    {
      Normal,
      Expert,
      Master,
      Creative,
    }

    private enum WorldEvilId
    {
      Random,
      Corruption,
      Crimson,
    }
  }
}
