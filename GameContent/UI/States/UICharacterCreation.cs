// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.States.UICharacterCreation
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using ReLogic.Content;
using ReLogic.OS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Initializers;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
  public class UICharacterCreation : UIState
  {
    private int[] _validClothStyles = new int[10]
    {
      0,
      2,
      1,
      3,
      8,
      4,
      6,
      5,
      7,
      9
    };
    private readonly Player _player;
    private UIColoredImageButton[] _colorPickers;
    private UICharacterCreation.CategoryId _selectedPicker;
    private Vector3 _currentColorHSL;
    private UIColoredImageButton _clothingStylesCategoryButton;
    private UIColoredImageButton _hairStylesCategoryButton;
    private UIColoredImageButton _charInfoCategoryButton;
    private UIElement _topContainer;
    private UIElement _middleContainer;
    private UIElement _hslContainer;
    private UIElement _hairstylesContainer;
    private UIElement _clothStylesContainer;
    private UIElement _infoContainer;
    private UIText _hslHexText;
    private UIText _difficultyDescriptionText;
    private UIElement _copyHexButton;
    private UIElement _pasteHexButton;
    private UIElement _randomColorButton;
    private UIElement _copyTemplateButton;
    private UIElement _pasteTemplateButton;
    private UIElement _randomizePlayerButton;
    private UIColoredImageButton _genderMale;
    private UIColoredImageButton _genderFemale;
    private UICharacterNameButton _charName;
    private UIText _helpGlyphLeft;
    private UIText _helpGlyphRight;
    public const int MAX_NAME_LENGTH = 20;
    private UIGamepadHelper _helper;
    private List<int> _foundPoints = new List<int>();

    public UICharacterCreation(Player player)
    {
      this._player = player;
      this._player.difficulty = (byte) 0;
      this.BuildPage();
    }

    private void BuildPage()
    {
      this.RemoveAllChildren();
      int num = 4;
      UIElement uiElement1 = new UIElement()
      {
        Width = StyleDimension.FromPixels(500f),
        Height = StyleDimension.FromPixels((float) (380 + num)),
        Top = StyleDimension.FromPixels(220f),
        HAlign = 0.5f,
        VAlign = 0.0f
      };
      uiElement1.SetPadding(0.0f);
      this.Append(uiElement1);
      UIPanel uiPanel1 = new UIPanel();
      uiPanel1.Width = StyleDimension.FromPercent(1f);
      uiPanel1.Height = StyleDimension.FromPixels(uiElement1.Height.Pixels - 150f - (float) num);
      uiPanel1.Top = StyleDimension.FromPixels(50f);
      uiPanel1.BackgroundColor = new Color(33, 43, 79) * 0.8f;
      UIPanel uiPanel2 = uiPanel1;
      uiPanel2.SetPadding(0.0f);
      uiElement1.Append((UIElement) uiPanel2);
      this.MakeBackAndCreatebuttons(uiElement1);
      this.MakeCharPreview(uiPanel2);
      UIElement uiElement2 = new UIElement()
      {
        Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f),
        Height = StyleDimension.FromPixelsAndPercent(50f, 0.0f)
      };
      uiElement2.SetPadding(0.0f);
      uiElement2.PaddingTop = 4f;
      uiElement2.PaddingBottom = 0.0f;
      uiPanel2.Append(uiElement2);
      UIElement uiElement3 = new UIElement()
      {
        Top = StyleDimension.FromPixelsAndPercent(uiElement2.Height.Pixels + 6f, 0.0f),
        Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f),
        Height = StyleDimension.FromPixelsAndPercent(uiPanel2.Height.Pixels - 70f, 0.0f)
      };
      uiElement3.SetPadding(0.0f);
      uiElement3.PaddingTop = 3f;
      uiElement3.PaddingBottom = 0.0f;
      uiPanel2.Append(uiElement3);
      this._topContainer = uiElement2;
      this._middleContainer = uiElement3;
      this.MakeInfoMenu(uiElement3);
      this.MakeHSLMenu(uiElement3);
      this.MakeHairsylesMenu(uiElement3);
      this.MakeClothStylesMenu(uiElement3);
      this.MakeCategoriesBar(uiElement2);
      this.Click_CharInfo((UIMouseEvent) null, (UIElement) null);
    }

    private void MakeCharPreview(UIPanel container)
    {
      float num1 = 70f;
      for (float num2 = 0.0f; (double) num2 <= 1.0; ++num2)
      {
        UICharacter uiCharacter = new UICharacter(this._player, true, false, 1.5f);
        uiCharacter.Width = StyleDimension.FromPixels(80f);
        uiCharacter.Height = StyleDimension.FromPixelsAndPercent(80f, 0.0f);
        uiCharacter.Top = StyleDimension.FromPixelsAndPercent(-num1, 0.0f);
        uiCharacter.VAlign = 0.0f;
        uiCharacter.HAlign = 0.5f;
        UICharacter element = uiCharacter;
        container.Append((UIElement) element);
      }
    }

    private void MakeHairsylesMenu(UIElement middleInnerPanel)
    {
      Main.Hairstyles.UpdateUnlocks();
      UIElement element1 = new UIElement()
      {
        Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
        Height = StyleDimension.FromPixelsAndPercent(0.0f, 1f),
        HAlign = 0.5f,
        VAlign = 0.5f,
        Top = StyleDimension.FromPixels(6f)
      };
      middleInnerPanel.Append(element1);
      element1.SetPadding(0.0f);
      UIList uiList = new UIList();
      uiList.Width = StyleDimension.FromPixelsAndPercent(-18f, 1f);
      uiList.Height = StyleDimension.FromPixelsAndPercent(-6f, 1f);
      UIList element2 = uiList;
      element2.SetPadding(4f);
      element1.Append((UIElement) element2);
      UIScrollbar uiScrollbar1 = new UIScrollbar();
      uiScrollbar1.HAlign = 1f;
      uiScrollbar1.Height = StyleDimension.FromPixelsAndPercent(-30f, 1f);
      uiScrollbar1.Top = StyleDimension.FromPixels(10f);
      UIScrollbar uiScrollbar2 = uiScrollbar1;
      uiScrollbar2.SetView(100f, 1000f);
      element2.SetScrollbar(uiScrollbar2);
      element1.Append((UIElement) uiScrollbar2);
      int count = Main.Hairstyles.AvailableHairstyles.Count;
      UIElement uiElement = new UIElement()
      {
        Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f),
        Height = StyleDimension.FromPixelsAndPercent((float) (48 * (count / 10 + (count % 10 == 0 ? 0 : 1))), 0.0f)
      };
      element2.Add(uiElement);
      uiElement.SetPadding(0.0f);
      for (int index = 0; index < count; ++index)
      {
        UIHairStyleButton uiHairStyleButton = new UIHairStyleButton(this._player, Main.Hairstyles.AvailableHairstyles[index]);
        uiHairStyleButton.Left = StyleDimension.FromPixels((float) ((double) (index % 10) * 46.0 + 6.0));
        uiHairStyleButton.Top = StyleDimension.FromPixels((float) ((double) (index / 10) * 48.0 + 1.0));
        UIHairStyleButton element3 = uiHairStyleButton;
        element3.SetSnapPoint("Middle", index);
        element3.SkipRenderingContent(index);
        uiElement.Append((UIElement) element3);
      }
      this._hairstylesContainer = element1;
    }

    private void MakeClothStylesMenu(UIElement middleInnerPanel)
    {
      UIElement element1 = new UIElement()
      {
        Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
        Height = StyleDimension.FromPixelsAndPercent(0.0f, 1f),
        HAlign = 0.5f,
        VAlign = 0.5f
      };
      middleInnerPanel.Append(element1);
      element1.SetPadding(0.0f);
      int pixels = 15;
      for (int id = 0; id < this._validClothStyles.Length; ++id)
      {
        int num = 0;
        if (id >= this._validClothStyles.Length / 2)
          num = 20;
        UIClothStyleButton clothStyleButton = new UIClothStyleButton(this._player, this._validClothStyles[id]);
        clothStyleButton.Left = StyleDimension.FromPixels((float) ((double) id * 46.0 + (double) num + 6.0));
        clothStyleButton.Top = StyleDimension.FromPixels((float) pixels);
        UIClothStyleButton element2 = clothStyleButton;
        element2.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_CharClothStyle);
        element2.SetSnapPoint("Middle", id);
        element1.Append((UIElement) element2);
      }
      for (int index = 0; index < 2; ++index)
      {
        int num = 0;
        if (index >= 1)
          num = 20;
        UIHorizontalSeparator horizontalSeparator = new UIHorizontalSeparator();
        horizontalSeparator.Left = StyleDimension.FromPixels((float) ((double) index * 230.0 + (double) num + 6.0));
        horizontalSeparator.Top = StyleDimension.FromPixels((float) (pixels + 86));
        horizontalSeparator.Width = StyleDimension.FromPixelsAndPercent(230f, 0.0f);
        horizontalSeparator.Color = Color.Lerp(Color.White, new Color(63, 65, 151, (int) byte.MaxValue), 0.85f) * 0.9f;
        UIHorizontalSeparator element3 = horizontalSeparator;
        element1.Append((UIElement) element3);
        UIColoredImageButton pickerWithoutClick = this.CreatePickerWithoutClick(UICharacterCreation.CategoryId.Clothing, "Images/UI/CharCreation/" + (index == 0 ? "ClothStyleMale" : "ClothStyleFemale"), 0.0f, 0.0f);
        pickerWithoutClick.Top = StyleDimension.FromPixelsAndPercent((float) (pixels + 92), 0.0f);
        pickerWithoutClick.Left = StyleDimension.FromPixels((float) ((double) index * 230.0 + 92.0 + (double) num + 6.0));
        pickerWithoutClick.HAlign = 0.0f;
        pickerWithoutClick.VAlign = 0.0f;
        element1.Append((UIElement) pickerWithoutClick);
        if (index == 0)
        {
          pickerWithoutClick.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_CharGenderMale);
          this._genderMale = pickerWithoutClick;
        }
        else
        {
          pickerWithoutClick.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_CharGenderFemale);
          this._genderFemale = pickerWithoutClick;
        }
        pickerWithoutClick.SetSnapPoint("Low", index * 4);
      }
      UIElement element4 = new UIElement()
      {
        Width = StyleDimension.FromPixels(130f),
        Height = StyleDimension.FromPixels(50f),
        HAlign = 0.5f,
        VAlign = 1f
      };
      element1.Append(element4);
      UIColoredImageButton coloredImageButton1 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Copy", (AssetRequestMode) 1), true);
      coloredImageButton1.VAlign = 0.5f;
      coloredImageButton1.HAlign = 0.0f;
      coloredImageButton1.Left = StyleDimension.FromPixelsAndPercent(0.0f, 0.0f);
      UIColoredImageButton element5 = coloredImageButton1;
      element5.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_CopyPlayerTemplate);
      element4.Append((UIElement) element5);
      this._copyTemplateButton = (UIElement) element5;
      UIColoredImageButton coloredImageButton2 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Paste", (AssetRequestMode) 1), true);
      coloredImageButton2.VAlign = 0.5f;
      coloredImageButton2.HAlign = 0.5f;
      UIColoredImageButton element6 = coloredImageButton2;
      element6.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_PastePlayerTemplate);
      element4.Append((UIElement) element6);
      this._pasteTemplateButton = (UIElement) element6;
      UIColoredImageButton coloredImageButton3 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Randomize", (AssetRequestMode) 1), true);
      coloredImageButton3.VAlign = 0.5f;
      coloredImageButton3.HAlign = 1f;
      UIColoredImageButton element7 = coloredImageButton3;
      element7.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_RandomizePlayer);
      element4.Append((UIElement) element7);
      this._randomizePlayerButton = (UIElement) element7;
      element5.SetSnapPoint("Low", 1);
      element6.SetSnapPoint("Low", 2);
      element7.SetSnapPoint("Low", 3);
      this._clothStylesContainer = element1;
    }

    private void MakeCategoriesBar(UIElement categoryContainer)
    {
      float xPositionStart = -240f;
      float xPositionPerId = 48f;
      this._colorPickers = new UIColoredImageButton[10];
      categoryContainer.Append((UIElement) this.CreateColorPicker(UICharacterCreation.CategoryId.HairColor, "Images/UI/CharCreation/ColorHair", xPositionStart, xPositionPerId));
      categoryContainer.Append((UIElement) this.CreateColorPicker(UICharacterCreation.CategoryId.Eye, "Images/UI/CharCreation/ColorEye", xPositionStart, xPositionPerId));
      categoryContainer.Append((UIElement) this.CreateColorPicker(UICharacterCreation.CategoryId.Skin, "Images/UI/CharCreation/ColorSkin", xPositionStart, xPositionPerId));
      categoryContainer.Append((UIElement) this.CreateColorPicker(UICharacterCreation.CategoryId.Shirt, "Images/UI/CharCreation/ColorShirt", xPositionStart, xPositionPerId));
      categoryContainer.Append((UIElement) this.CreateColorPicker(UICharacterCreation.CategoryId.Undershirt, "Images/UI/CharCreation/ColorUndershirt", xPositionStart, xPositionPerId));
      categoryContainer.Append((UIElement) this.CreateColorPicker(UICharacterCreation.CategoryId.Pants, "Images/UI/CharCreation/ColorPants", xPositionStart, xPositionPerId));
      categoryContainer.Append((UIElement) this.CreateColorPicker(UICharacterCreation.CategoryId.Shoes, "Images/UI/CharCreation/ColorShoes", xPositionStart, xPositionPerId));
      this._colorPickers[4].SetMiddleTexture(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/ColorEyeBack", (AssetRequestMode) 1));
      this._clothingStylesCategoryButton = this.CreatePickerWithoutClick(UICharacterCreation.CategoryId.Clothing, "Images/UI/CharCreation/ClothStyleMale", xPositionStart, xPositionPerId);
      this._clothingStylesCategoryButton.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_ClothStyles);
      this._clothingStylesCategoryButton.SetSnapPoint("Top", 1);
      categoryContainer.Append((UIElement) this._clothingStylesCategoryButton);
      this._hairStylesCategoryButton = this.CreatePickerWithoutClick(UICharacterCreation.CategoryId.HairStyle, "Images/UI/CharCreation/HairStyle_Hair", xPositionStart, xPositionPerId);
      this._hairStylesCategoryButton.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_HairStyles);
      this._hairStylesCategoryButton.SetMiddleTexture(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/HairStyle_Arrow", (AssetRequestMode) 1));
      this._hairStylesCategoryButton.SetSnapPoint("Top", 2);
      categoryContainer.Append((UIElement) this._hairStylesCategoryButton);
      this._charInfoCategoryButton = this.CreatePickerWithoutClick(UICharacterCreation.CategoryId.CharInfo, "Images/UI/CharCreation/CharInfo", xPositionStart, xPositionPerId);
      this._charInfoCategoryButton.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_CharInfo);
      this._charInfoCategoryButton.SetSnapPoint("Top", 0);
      categoryContainer.Append((UIElement) this._charInfoCategoryButton);
      this.UpdateColorPickers();
      UIHorizontalSeparator horizontalSeparator = new UIHorizontalSeparator();
      horizontalSeparator.Width = StyleDimension.FromPixelsAndPercent(-20f, 1f);
      horizontalSeparator.Top = StyleDimension.FromPixels(6f);
      horizontalSeparator.VAlign = 1f;
      horizontalSeparator.HAlign = 0.5f;
      horizontalSeparator.Color = Color.Lerp(Color.White, new Color(63, 65, 151, (int) byte.MaxValue), 0.85f) * 0.9f;
      UIHorizontalSeparator element1 = horizontalSeparator;
      categoryContainer.Append((UIElement) element1);
      int num = 21;
      UIText uiText1 = new UIText(PlayerInput.GenerateInputTag_ForCurrentGamemode(false, "HotbarMinus"));
      uiText1.Left = new StyleDimension((float) -num, 0.0f);
      uiText1.VAlign = 0.5f;
      uiText1.Top = new StyleDimension(-4f, 0.0f);
      UIText element2 = uiText1;
      categoryContainer.Append((UIElement) element2);
      UIText uiText2 = new UIText(PlayerInput.GenerateInputTag_ForCurrentGamemode(false, "HotbarMinus"));
      uiText2.HAlign = 1f;
      uiText2.Left = new StyleDimension((float) (12 + num), 0.0f);
      uiText2.VAlign = 0.5f;
      uiText2.Top = new StyleDimension(-4f, 0.0f);
      UIText element3 = uiText2;
      categoryContainer.Append((UIElement) element3);
      this._helpGlyphLeft = element2;
      this._helpGlyphRight = element3;
      categoryContainer.OnUpdate += new UIElement.ElementEvent(this.UpdateHelpGlyphs);
    }

    private void UpdateHelpGlyphs(UIElement element)
    {
      string text1 = "";
      string text2 = "";
      if (PlayerInput.UsingGamepad)
      {
        text1 = PlayerInput.GenerateInputTag_ForCurrentGamemode(false, "HotbarMinus");
        text2 = PlayerInput.GenerateInputTag_ForCurrentGamemode(false, "HotbarPlus");
      }
      this._helpGlyphLeft.SetText(text1);
      this._helpGlyphRight.SetText(text2);
    }

    private UIColoredImageButton CreateColorPicker(
      UICharacterCreation.CategoryId id,
      string texturePath,
      float xPositionStart,
      float xPositionPerId)
    {
      UIColoredImageButton colorPicker = new UIColoredImageButton(Main.Assets.Request<Texture2D>(texturePath, (AssetRequestMode) 1));
      this._colorPickers[(int) id] = colorPicker;
      colorPicker.VAlign = 0.0f;
      colorPicker.HAlign = 0.0f;
      colorPicker.Left.Set(xPositionStart + (float) id * xPositionPerId, 0.5f);
      colorPicker.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_ColorPicker);
      colorPicker.SetSnapPoint("Top", (int) id);
      return colorPicker;
    }

    private UIColoredImageButton CreatePickerWithoutClick(
      UICharacterCreation.CategoryId id,
      string texturePath,
      float xPositionStart,
      float xPositionPerId)
    {
      UIColoredImageButton pickerWithoutClick = new UIColoredImageButton(Main.Assets.Request<Texture2D>(texturePath, (AssetRequestMode) 1));
      pickerWithoutClick.VAlign = 0.0f;
      pickerWithoutClick.HAlign = 0.0f;
      pickerWithoutClick.Left.Set(xPositionStart + (float) id * xPositionPerId, 0.5f);
      return pickerWithoutClick;
    }

    private void MakeInfoMenu(UIElement parentContainer)
    {
      UIElement element1 = new UIElement()
      {
        Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f),
        Height = StyleDimension.FromPixelsAndPercent(0.0f, 1f),
        HAlign = 0.5f,
        VAlign = 0.0f
      };
      element1.SetPadding(10f);
      element1.PaddingBottom = 0.0f;
      element1.PaddingTop = 0.0f;
      parentContainer.Append(element1);
      UICharacterNameButton element2 = new UICharacterNameButton(Language.GetText("UI.WorldCreationName"), Language.GetText("UI.PlayerEmptyName"));
      element2.Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      element2.HAlign = 0.5f;
      element1.Append((UIElement) element2);
      this._charName = element2;
      element2.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_Naming);
      element2.SetSnapPoint("Middle", 0);
      float num1 = 4f;
      float num2 = 0.0f;
      float percent = 0.4f;
      UIElement element3 = new UIElement()
      {
        HAlign = 0.0f,
        VAlign = 1f,
        Width = StyleDimension.FromPixelsAndPercent(-num1, percent),
        Height = StyleDimension.FromPixelsAndPercent(-50f, 1f)
      };
      element3.SetPadding(0.0f);
      element1.Append(element3);
      UISlicedImage uiSlicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", (AssetRequestMode) 1));
      uiSlicedImage.HAlign = 1f;
      uiSlicedImage.VAlign = 1f;
      uiSlicedImage.Width = StyleDimension.FromPixelsAndPercent((float) (-(double) num1 * 2.0), 1f - percent);
      uiSlicedImage.Left = StyleDimension.FromPixels(-num1);
      uiSlicedImage.Height = StyleDimension.FromPixelsAndPercent(element3.Height.Pixels, element3.Height.Precent);
      UISlicedImage element4 = uiSlicedImage;
      element4.SetSliceDepths(10);
      element4.Color = Color.LightGray * 0.7f;
      element1.Append((UIElement) element4);
      float num3 = 4f;
      UIDifficultyButton difficultyButton1 = new UIDifficultyButton(this._player, Lang.menu[26], Lang.menu[31], (byte) 0, Color.Cyan);
      difficultyButton1.HAlign = 0.0f;
      difficultyButton1.VAlign = (float) (1.0 / ((double) num3 - 1.0));
      difficultyButton1.Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      difficultyButton1.Height = StyleDimension.FromPixelsAndPercent(-num2, 1f / num3);
      UIDifficultyButton element5 = difficultyButton1;
      UIDifficultyButton difficultyButton2 = new UIDifficultyButton(this._player, Lang.menu[25], Lang.menu[30], (byte) 1, Main.mcColor);
      difficultyButton2.HAlign = 0.0f;
      difficultyButton2.VAlign = (float) (2.0 / ((double) num3 - 1.0));
      difficultyButton2.Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      difficultyButton2.Height = StyleDimension.FromPixelsAndPercent(-num2, 1f / num3);
      UIDifficultyButton element6 = difficultyButton2;
      UIDifficultyButton difficultyButton3 = new UIDifficultyButton(this._player, Lang.menu[24], Lang.menu[29], (byte) 2, Main.hcColor);
      difficultyButton3.HAlign = 0.0f;
      difficultyButton3.VAlign = 1f;
      difficultyButton3.Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      difficultyButton3.Height = StyleDimension.FromPixelsAndPercent(-num2, 1f / num3);
      UIDifficultyButton element7 = difficultyButton3;
      UIDifficultyButton difficultyButton4 = new UIDifficultyButton(this._player, Language.GetText("UI.Creative"), Language.GetText("UI.CreativeDescriptionPlayer"), (byte) 3, Main.creativeModeColor);
      difficultyButton4.HAlign = 0.0f;
      difficultyButton4.VAlign = 0.0f;
      difficultyButton4.Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      difficultyButton4.Height = StyleDimension.FromPixelsAndPercent(-num2, 1f / num3);
      UIDifficultyButton element8 = difficultyButton4;
      UIText uiText = new UIText(Lang.menu[26]);
      uiText.HAlign = 0.0f;
      uiText.VAlign = 0.5f;
      uiText.Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiText.Height = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiText.Top = StyleDimension.FromPixelsAndPercent(15f, 0.0f);
      uiText.IsWrapped = true;
      UIText element9 = uiText;
      element9.PaddingLeft = 20f;
      element9.PaddingRight = 20f;
      element4.Append((UIElement) element9);
      element3.Append((UIElement) element8);
      element3.Append((UIElement) element5);
      element3.Append((UIElement) element6);
      element3.Append((UIElement) element7);
      this._infoContainer = element1;
      this._difficultyDescriptionText = element9;
      element8.OnLeftMouseDown += new UIElement.MouseEvent(this.UpdateDifficultyDescription);
      element5.OnLeftMouseDown += new UIElement.MouseEvent(this.UpdateDifficultyDescription);
      element6.OnLeftMouseDown += new UIElement.MouseEvent(this.UpdateDifficultyDescription);
      element7.OnLeftMouseDown += new UIElement.MouseEvent(this.UpdateDifficultyDescription);
      this.UpdateDifficultyDescription((UIMouseEvent) null, (UIElement) null);
      element8.SetSnapPoint("Middle", 1);
      element5.SetSnapPoint("Middle", 2);
      element6.SetSnapPoint("Middle", 3);
      element7.SetSnapPoint("Middle", 4);
    }

    private void UpdateDifficultyDescription(UIMouseEvent evt, UIElement listeningElement)
    {
      LocalizedText text = Lang.menu[31];
      switch (this._player.difficulty)
      {
        case 0:
          text = Lang.menu[31];
          break;
        case 1:
          text = Lang.menu[30];
          break;
        case 2:
          text = Lang.menu[29];
          break;
        case 3:
          text = Language.GetText("UI.CreativeDescriptionPlayer");
          break;
      }
      this._difficultyDescriptionText.SetText(text);
    }

    private void MakeHSLMenu(UIElement parentContainer)
    {
      UIElement element1 = new UIElement()
      {
        Width = StyleDimension.FromPixelsAndPercent(220f, 0.0f),
        Height = StyleDimension.FromPixelsAndPercent(158f, 0.0f),
        HAlign = 0.5f,
        VAlign = 0.0f
      };
      element1.SetPadding(0.0f);
      parentContainer.Append(element1);
      UIPanel uiPanel1 = new UIPanel();
      uiPanel1.Width = StyleDimension.FromPixelsAndPercent(220f, 0.0f);
      uiPanel1.Height = StyleDimension.FromPixelsAndPercent(104f, 0.0f);
      uiPanel1.HAlign = 0.5f;
      uiPanel1.VAlign = 0.0f;
      uiPanel1.Top = StyleDimension.FromPixelsAndPercent(10f, 0.0f);
      UIElement element2 = (UIElement) uiPanel1;
      element2.SetPadding(0.0f);
      element2.PaddingTop = 3f;
      element1.Append(element2);
      element2.Append((UIElement) this.CreateHSLSlider(UICharacterCreation.HSLSliderId.Hue));
      element2.Append((UIElement) this.CreateHSLSlider(UICharacterCreation.HSLSliderId.Saturation));
      element2.Append((UIElement) this.CreateHSLSlider(UICharacterCreation.HSLSliderId.Luminance));
      UIPanel uiPanel2 = new UIPanel();
      uiPanel2.VAlign = 1f;
      uiPanel2.HAlign = 1f;
      uiPanel2.Width = StyleDimension.FromPixelsAndPercent(100f, 0.0f);
      uiPanel2.Height = StyleDimension.FromPixelsAndPercent(32f, 0.0f);
      UIPanel element3 = uiPanel2;
      UIText uiText = new UIText("FFFFFF");
      uiText.VAlign = 0.5f;
      uiText.HAlign = 0.5f;
      UIText element4 = uiText;
      element3.Append((UIElement) element4);
      element1.Append((UIElement) element3);
      UIColoredImageButton coloredImageButton1 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Copy", (AssetRequestMode) 1), true);
      coloredImageButton1.VAlign = 1f;
      coloredImageButton1.HAlign = 0.0f;
      coloredImageButton1.Left = StyleDimension.FromPixelsAndPercent(0.0f, 0.0f);
      UIColoredImageButton element5 = coloredImageButton1;
      element5.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_CopyHex);
      element1.Append((UIElement) element5);
      this._copyHexButton = (UIElement) element5;
      UIColoredImageButton coloredImageButton2 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Paste", (AssetRequestMode) 1), true);
      coloredImageButton2.VAlign = 1f;
      coloredImageButton2.HAlign = 0.0f;
      coloredImageButton2.Left = StyleDimension.FromPixelsAndPercent(40f, 0.0f);
      UIColoredImageButton element6 = coloredImageButton2;
      element6.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_PasteHex);
      element1.Append((UIElement) element6);
      this._pasteHexButton = (UIElement) element6;
      UIColoredImageButton coloredImageButton3 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Randomize", (AssetRequestMode) 1), true);
      coloredImageButton3.VAlign = 1f;
      coloredImageButton3.HAlign = 0.0f;
      coloredImageButton3.Left = StyleDimension.FromPixelsAndPercent(80f, 0.0f);
      UIColoredImageButton element7 = coloredImageButton3;
      element7.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_RandomizeSingleColor);
      element1.Append((UIElement) element7);
      this._randomColorButton = (UIElement) element7;
      this._hslContainer = element1;
      this._hslHexText = element4;
      element5.SetSnapPoint("Low", 0);
      element6.SetSnapPoint("Low", 1);
      element7.SetSnapPoint("Low", 2);
    }

    private UIColoredSlider CreateHSLSlider(UICharacterCreation.HSLSliderId id)
    {
      UIColoredSlider sliderButtonBase = this.CreateHSLSliderButtonBase(id);
      sliderButtonBase.VAlign = 0.0f;
      sliderButtonBase.HAlign = 0.0f;
      sliderButtonBase.Width = StyleDimension.FromPixelsAndPercent(-10f, 1f);
      sliderButtonBase.Top.Set((float) (30 * (int) id), 0.0f);
      sliderButtonBase.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_ColorPicker);
      sliderButtonBase.SetSnapPoint("Middle", (int) id, offset: new Vector2?(new Vector2(0.0f, 20f)));
      return sliderButtonBase;
    }

    private UIColoredSlider CreateHSLSliderButtonBase(
      UICharacterCreation.HSLSliderId id)
    {
      UIColoredSlider sliderButtonBase;
      switch (id)
      {
        case UICharacterCreation.HSLSliderId.Saturation:
          sliderButtonBase = new UIColoredSlider(LocalizedText.Empty, (Func<float>) (() => this.GetHSLSliderPosition(UICharacterCreation.HSLSliderId.Saturation)), (Action<float>) (x => this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Saturation, x)), new Action(this.UpdateHSL_S), (Func<float, Color>) (x => this.GetHSLSliderColorAt(UICharacterCreation.HSLSliderId.Saturation, x)), Color.Transparent);
          break;
        case UICharacterCreation.HSLSliderId.Luminance:
          sliderButtonBase = new UIColoredSlider(LocalizedText.Empty, (Func<float>) (() => this.GetHSLSliderPosition(UICharacterCreation.HSLSliderId.Luminance)), (Action<float>) (x => this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Luminance, x)), new Action(this.UpdateHSL_L), (Func<float, Color>) (x => this.GetHSLSliderColorAt(UICharacterCreation.HSLSliderId.Luminance, x)), Color.Transparent);
          break;
        default:
          sliderButtonBase = new UIColoredSlider(LocalizedText.Empty, (Func<float>) (() => this.GetHSLSliderPosition(UICharacterCreation.HSLSliderId.Hue)), (Action<float>) (x => this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Hue, x)), new Action(this.UpdateHSL_H), (Func<float, Color>) (x => this.GetHSLSliderColorAt(UICharacterCreation.HSLSliderId.Hue, x)), Color.Transparent);
          break;
      }
      return sliderButtonBase;
    }

    private void UpdateHSL_H() => this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Hue, UILinksInitializer.HandleSliderHorizontalInput(this._currentColorHSL.X, 0.0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f));

    private void UpdateHSL_S() => this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Saturation, UILinksInitializer.HandleSliderHorizontalInput(this._currentColorHSL.Y, 0.0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f));

    private void UpdateHSL_L() => this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Luminance, UILinksInitializer.HandleSliderHorizontalInput(this._currentColorHSL.Z, 0.0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f));

    private float GetHSLSliderPosition(UICharacterCreation.HSLSliderId id)
    {
      switch (id)
      {
        case UICharacterCreation.HSLSliderId.Hue:
          return this._currentColorHSL.X;
        case UICharacterCreation.HSLSliderId.Saturation:
          return this._currentColorHSL.Y;
        case UICharacterCreation.HSLSliderId.Luminance:
          return this._currentColorHSL.Z;
        default:
          return 1f;
      }
    }

    private void UpdateHSLValue(UICharacterCreation.HSLSliderId id, float value)
    {
      switch (id)
      {
        case UICharacterCreation.HSLSliderId.Hue:
          this._currentColorHSL.X = value;
          break;
        case UICharacterCreation.HSLSliderId.Saturation:
          this._currentColorHSL.Y = value;
          break;
        case UICharacterCreation.HSLSliderId.Luminance:
          this._currentColorHSL.Z = value;
          break;
      }
      Color rgb = UICharacterCreation.ScaledHslToRgb(this._currentColorHSL.X, this._currentColorHSL.Y, this._currentColorHSL.Z);
      this.ApplyPendingColor(rgb);
      this._colorPickers[(int) this._selectedPicker]?.SetColor(rgb);
      if (this._selectedPicker == UICharacterCreation.CategoryId.HairColor)
        this._hairStylesCategoryButton.SetColor(rgb);
      this.UpdateHexText(rgb);
    }

    private Color GetHSLSliderColorAt(UICharacterCreation.HSLSliderId id, float pointAt)
    {
      switch (id)
      {
        case UICharacterCreation.HSLSliderId.Hue:
          return UICharacterCreation.ScaledHslToRgb(pointAt, 1f, 0.5f);
        case UICharacterCreation.HSLSliderId.Saturation:
          return UICharacterCreation.ScaledHslToRgb(this._currentColorHSL.X, pointAt, this._currentColorHSL.Z);
        case UICharacterCreation.HSLSliderId.Luminance:
          return UICharacterCreation.ScaledHslToRgb(this._currentColorHSL.X, this._currentColorHSL.Y, pointAt);
        default:
          return Color.White;
      }
    }

    private void ApplyPendingColor(Color pendingColor)
    {
      switch (this._selectedPicker)
      {
        case UICharacterCreation.CategoryId.HairColor:
          this._player.hairColor = pendingColor;
          break;
        case UICharacterCreation.CategoryId.Eye:
          this._player.eyeColor = pendingColor;
          break;
        case UICharacterCreation.CategoryId.Skin:
          this._player.skinColor = pendingColor;
          break;
        case UICharacterCreation.CategoryId.Shirt:
          this._player.shirtColor = pendingColor;
          break;
        case UICharacterCreation.CategoryId.Undershirt:
          this._player.underShirtColor = pendingColor;
          break;
        case UICharacterCreation.CategoryId.Pants:
          this._player.pantsColor = pendingColor;
          break;
        case UICharacterCreation.CategoryId.Shoes:
          this._player.shoeColor = pendingColor;
          break;
      }
    }

    private void UpdateHexText(Color pendingColor) => this._hslHexText.SetText(UICharacterCreation.GetHexText(pendingColor));

    private static string GetHexText(Color pendingColor) => "#" + pendingColor.Hex3().ToUpper();

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
      Main.OpenCharacterSelectUI();
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

    private void Click_ColorPicker(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12);
      for (int selection = 0; selection < this._colorPickers.Length; ++selection)
      {
        if (this._colorPickers[selection] == evt.Target)
        {
          this.SelectColorPicker((UICharacterCreation.CategoryId) selection);
          break;
        }
      }
    }

    private void Click_ClothStyles(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12);
      this.UnselectAllCategories();
      this._selectedPicker = UICharacterCreation.CategoryId.Clothing;
      this._middleContainer.Append(this._clothStylesContainer);
      this._clothingStylesCategoryButton.SetSelected(true);
      this.UpdateSelectedGender();
    }

    private void Click_HairStyles(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12);
      this.UnselectAllCategories();
      this._selectedPicker = UICharacterCreation.CategoryId.HairStyle;
      this._middleContainer.Append(this._hairstylesContainer);
      this._hairStylesCategoryButton.SetSelected(true);
    }

    private void Click_CharInfo(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12);
      this.UnselectAllCategories();
      this._selectedPicker = UICharacterCreation.CategoryId.CharInfo;
      this._middleContainer.Append(this._infoContainer);
      this._charInfoCategoryButton.SetSelected(true);
    }

    private void Click_CharClothStyle(UIMouseEvent evt, UIElement listeningElement)
    {
      this._clothingStylesCategoryButton.SetImageWithoutSettingSize(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/" + (this._player.Male ? "ClothStyleMale" : "ClothStyleFemale"), (AssetRequestMode) 1));
      this.UpdateSelectedGender();
    }

    private void Click_CharGenderMale(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12);
      this._player.Male = true;
      this.Click_CharClothStyle(evt, listeningElement);
      this.UpdateSelectedGender();
    }

    private void Click_CharGenderFemale(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12);
      this._player.Male = false;
      this.Click_CharClothStyle(evt, listeningElement);
      this.UpdateSelectedGender();
    }

    private void UpdateSelectedGender()
    {
      this._genderMale.SetSelected(this._player.Male);
      this._genderFemale.SetSelected(!this._player.Male);
    }

    private void Click_CopyHex(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12);
      Platform.Get<IClipboard>().Value = this._hslHexText.Text;
    }

    private void Click_PasteHex(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12);
      Vector3 hsl;
      if (!this.GetHexColor(Platform.Get<IClipboard>().Value, out hsl))
        return;
      this.ApplyPendingColor(UICharacterCreation.ScaledHslToRgb(hsl.X, hsl.Y, hsl.Z));
      this._currentColorHSL = hsl;
      this.UpdateHexText(UICharacterCreation.ScaledHslToRgb(hsl.X, hsl.Y, hsl.Z));
      this.UpdateColorPickers();
    }

    private void Click_CopyPlayerTemplate(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12);
      string text = JsonConvert.SerializeObject((object) new Dictionary<string, object>()
      {
        {
          "version",
          (object) 1
        },
        {
          "hairStyle",
          (object) this._player.hair
        },
        {
          "clothingStyle",
          (object) this._player.skinVariant
        },
        {
          "hairColor",
          (object) UICharacterCreation.GetHexText(this._player.hairColor)
        },
        {
          "eyeColor",
          (object) UICharacterCreation.GetHexText(this._player.eyeColor)
        },
        {
          "skinColor",
          (object) UICharacterCreation.GetHexText(this._player.skinColor)
        },
        {
          "shirtColor",
          (object) UICharacterCreation.GetHexText(this._player.shirtColor)
        },
        {
          "underShirtColor",
          (object) UICharacterCreation.GetHexText(this._player.underShirtColor)
        },
        {
          "pantsColor",
          (object) UICharacterCreation.GetHexText(this._player.pantsColor)
        },
        {
          "shoeColor",
          (object) UICharacterCreation.GetHexText(this._player.shoeColor)
        }
      }, new JsonSerializerSettings()
      {
        TypeNameHandling = (TypeNameHandling) 4,
        MetadataPropertyHandling = (MetadataPropertyHandling) 1,
        Formatting = (Formatting) 1
      });
      PlayerInput.PrettyPrintProfiles(ref text);
      Platform.Get<IClipboard>().Value = text;
    }

    private void Click_PastePlayerTemplate(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12);
      try
      {
        string str1 = Platform.Get<IClipboard>().Value;
        int startIndex = str1.IndexOf("{");
        if (startIndex == -1)
          return;
        string str2 = str1.Substring(startIndex);
        int num1 = str2.LastIndexOf("}");
        if (num1 == -1)
          return;
        Dictionary<string, object> dictionary1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(str2.Substring(0, num1 + 1));
        if (dictionary1 == null)
          return;
        Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
        foreach (KeyValuePair<string, object> keyValuePair in dictionary1)
          dictionary2[keyValuePair.Key.ToLower()] = keyValuePair.Value;
        object hexString;
        if (dictionary2.TryGetValue("version", out hexString))
        {
          long num2 = (long) hexString;
        }
        if (dictionary2.TryGetValue("hairstyle", out hexString))
        {
          int num3 = (int) (long) hexString;
          if (Main.Hairstyles.AvailableHairstyles.Contains(num3))
            this._player.hair = num3;
        }
        if (dictionary2.TryGetValue("clothingstyle", out hexString))
        {
          int num4 = (int) (long) hexString;
          if (((IEnumerable<int>) this._validClothStyles).Contains<int>(num4))
            this._player.skinVariant = num4;
        }
        Vector3 hsl;
        if (dictionary2.TryGetValue("haircolor", out hexString) && this.GetHexColor((string) hexString, out hsl))
          this._player.hairColor = UICharacterCreation.ScaledHslToRgb(hsl);
        if (dictionary2.TryGetValue("eyecolor", out hexString) && this.GetHexColor((string) hexString, out hsl))
          this._player.eyeColor = UICharacterCreation.ScaledHslToRgb(hsl);
        if (dictionary2.TryGetValue("skincolor", out hexString) && this.GetHexColor((string) hexString, out hsl))
          this._player.skinColor = UICharacterCreation.ScaledHslToRgb(hsl);
        if (dictionary2.TryGetValue("shirtcolor", out hexString) && this.GetHexColor((string) hexString, out hsl))
          this._player.shirtColor = UICharacterCreation.ScaledHslToRgb(hsl);
        if (dictionary2.TryGetValue("undershirtcolor", out hexString) && this.GetHexColor((string) hexString, out hsl))
          this._player.underShirtColor = UICharacterCreation.ScaledHslToRgb(hsl);
        if (dictionary2.TryGetValue("pantscolor", out hexString) && this.GetHexColor((string) hexString, out hsl))
          this._player.pantsColor = UICharacterCreation.ScaledHslToRgb(hsl);
        if (dictionary2.TryGetValue("shoecolor", out hexString) && this.GetHexColor((string) hexString, out hsl))
          this._player.shoeColor = UICharacterCreation.ScaledHslToRgb(hsl);
        this.Click_CharClothStyle((UIMouseEvent) null, (UIElement) null);
        this.UpdateColorPickers();
      }
      catch
      {
      }
    }

    private void Click_RandomizePlayer(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12);
      Player player = this._player;
      int index = Main.rand.Next(Main.Hairstyles.AvailableHairstyles.Count);
      player.hair = Main.Hairstyles.AvailableHairstyles[index];
      player.eyeColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
      while ((int) player.eyeColor.R + (int) player.eyeColor.G + (int) player.eyeColor.B > 300)
        player.eyeColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
      float num1 = (float) Main.rand.Next(60, 120) * 0.01f;
      if ((double) num1 > 1.0)
        num1 = 1f;
      player.skinColor.R = (byte) ((double) Main.rand.Next(240, (int) byte.MaxValue) * (double) num1);
      player.skinColor.G = (byte) ((double) Main.rand.Next(110, 140) * (double) num1);
      player.skinColor.B = (byte) ((double) Main.rand.Next(75, 110) * (double) num1);
      player.hairColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
      player.shirtColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
      player.underShirtColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
      player.pantsColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
      player.shoeColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
      player.skinVariant = this._validClothStyles[Main.rand.Next(this._validClothStyles.Length)];
      int num2 = player.hair + 1;
      if (num2 <= 135)
      {
        if (num2 <= 124)
        {
          switch (num2 - 5)
          {
            case 0:
            case 1:
            case 2:
            case 5:
            case 7:
            case 14:
            case 17:
            case 18:
            case 21:
            case 22:
            case 25:
            case 28:
            case 29:
            case 30:
            case 32:
            case 33:
            case 34:
            case 35:
            case 36:
            case 39:
            case 40:
            case 41:
            case 42:
            case 43:
            case 44:
            case 46:
            case 51:
            case 60:
            case 61:
            case 62:
            case 63:
            case 64:
            case 65:
            case 66:
            case 67:
            case 68:
            case 69:
            case 74:
            case 75:
            case 76:
            case 77:
            case 79:
            case 80:
            case 81:
            case 82:
            case 83:
            case 85:
            case 86:
            case 87:
            case 88:
            case 90:
            case 91:
            case 93:
            case 95:
            case 97:
            case 99:
            case 102:
            case 103:
            case 108:
              break;
            case 3:
            case 4:
            case 6:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 15:
            case 16:
            case 19:
            case 20:
            case 23:
            case 24:
            case 26:
            case 27:
            case 31:
            case 37:
            case 38:
            case 45:
            case 47:
            case 48:
            case 49:
            case 50:
            case 52:
            case 53:
            case 54:
            case 55:
            case 56:
            case 57:
            case 58:
            case 59:
            case 70:
            case 71:
            case 72:
            case 73:
            case 78:
            case 84:
            case 89:
            case 92:
            case 94:
            case 96:
            case 98:
            case 100:
            case 101:
            case 104:
            case 105:
            case 106:
            case 107:
              goto label_14;
            default:
              if (num2 == 124)
                break;
              goto label_14;
          }
        }
        else if (num2 != 126 && (uint) (num2 - 133) > 2U)
          goto label_14;
      }
      else if (num2 <= 147)
      {
        if (num2 != 144 && (uint) (num2 - 146) > 1U)
          goto label_14;
      }
      else if (num2 != 163 && num2 != 165)
        goto label_14;
      player.Male = false;
      goto label_15;
label_14:
      player.Male = true;
label_15:
      this.Click_CharClothStyle((UIMouseEvent) null, (UIElement) null);
      this.UpdateSelectedGender();
      this.UpdateColorPickers();
    }

    private void Click_Naming(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(10);
      this._player.name = "";
      Main.clrInput();
      UIVirtualKeyboard state = new UIVirtualKeyboard(Lang.menu[45].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedNaming), new Action(this.OnCanceledNaming), allowEmpty: true);
      state.SetMaxInputLength(20);
      Main.MenuUI.SetState((UIState) state);
    }

    private void Click_NamingAndCreating(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(10);
      if (string.IsNullOrEmpty(this._player.name))
      {
        this._player.name = "";
        Main.clrInput();
        UIVirtualKeyboard state = new UIVirtualKeyboard(Lang.menu[45].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedNamingAndCreating), new Action(this.OnCanceledNaming));
        state.SetMaxInputLength(20);
        Main.MenuUI.SetState((UIState) state);
      }
      else
        this.FinishCreatingCharacter();
    }

    private void OnFinishedNaming(string name)
    {
      this._player.name = name.Trim();
      Main.MenuUI.SetState((UIState) this);
      this._charName.SetContents(this._player.name);
    }

    private void OnCanceledNaming() => Main.MenuUI.SetState((UIState) this);

    private void OnFinishedNamingAndCreating(string name)
    {
      this._player.name = name.Trim();
      Main.MenuUI.SetState((UIState) this);
      this._charName.SetContents(this._player.name);
      this.FinishCreatingCharacter();
    }

    private void FinishCreatingCharacter()
    {
      this.SetupPlayerStatsAndInventoryBasedOnDifficulty();
      PlayerFileData.CreateAndSave(this._player);
      Main.LoadPlayers();
      Main.menuMode = 1;
    }

    private void SetupPlayerStatsAndInventoryBasedOnDifficulty()
    {
      int index1 = 0;
      int num1;
      if (this._player.difficulty == (byte) 3)
      {
        this._player.statLife = this._player.statLifeMax = 100;
        this._player.statMana = this._player.statManaMax = 20;
        this._player.inventory[index1].SetDefaults(6);
        Item[] inventory1 = this._player.inventory;
        int index2 = index1;
        int index3 = index2 + 1;
        inventory1[index2].Prefix(-1);
        this._player.inventory[index3].SetDefaults(1);
        Item[] inventory2 = this._player.inventory;
        int index4 = index3;
        int index5 = index4 + 1;
        inventory2[index4].Prefix(-1);
        this._player.inventory[index5].SetDefaults(10);
        Item[] inventory3 = this._player.inventory;
        int index6 = index5;
        int index7 = index6 + 1;
        inventory3[index6].Prefix(-1);
        this._player.inventory[index7].SetDefaults(7);
        Item[] inventory4 = this._player.inventory;
        int index8 = index7;
        int index9 = index8 + 1;
        inventory4[index8].Prefix(-1);
        this._player.inventory[index9].SetDefaults(4281);
        Item[] inventory5 = this._player.inventory;
        int index10 = index9;
        int index11 = index10 + 1;
        inventory5[index10].Prefix(-1);
        this._player.inventory[index11].SetDefaults(8);
        Item[] inventory6 = this._player.inventory;
        int index12 = index11;
        int index13 = index12 + 1;
        inventory6[index12].stack = 100;
        this._player.inventory[index13].SetDefaults(965);
        Item[] inventory7 = this._player.inventory;
        int index14 = index13;
        int num2 = index14 + 1;
        inventory7[index14].stack = 100;
        Item[] inventory8 = this._player.inventory;
        int index15 = num2;
        int num3 = index15 + 1;
        inventory8[index15].SetDefaults(50);
        Item[] inventory9 = this._player.inventory;
        int index16 = num3;
        num1 = index16 + 1;
        inventory9[index16].SetDefaults(84);
        this._player.armor[3].SetDefaults(4978);
        this._player.armor[3].Prefix(-1);
        if (this._player.name == "Wolf Pet" || this._player.name == "Wolfpet")
          this._player.miscEquips[3].SetDefaults(5130);
        this._player.AddBuff(216, 3600);
      }
      else
      {
        this._player.inventory[index1].SetDefaults(3507);
        Item[] inventory10 = this._player.inventory;
        int index17 = index1;
        int index18 = index17 + 1;
        inventory10[index17].Prefix(-1);
        this._player.inventory[index18].SetDefaults(3509);
        Item[] inventory11 = this._player.inventory;
        int index19 = index18;
        int index20 = index19 + 1;
        inventory11[index19].Prefix(-1);
        this._player.inventory[index20].SetDefaults(3506);
        Item[] inventory12 = this._player.inventory;
        int index21 = index20;
        num1 = index21 + 1;
        inventory12[index21].Prefix(-1);
      }
      if (Main.runningCollectorsEdition)
      {
        Item[] inventory = this._player.inventory;
        int index22 = num1;
        int num4 = index22 + 1;
        inventory[index22].SetDefaults(603);
      }
      this._player.savedPerPlayerFieldsThatArentInThePlayerClass = new Player.SavedPlayerDataWithAnnoyingRules();
      CreativePowerManager.Instance.ResetDataForNewPlayer(this._player);
    }

    private bool GetHexColor(string hexString, out Vector3 hsl)
    {
      if (hexString.StartsWith("#"))
        hexString = hexString.Substring(1);
      uint result;
      if (hexString.Length <= 6 && uint.TryParse(hexString, NumberStyles.HexNumber, (IFormatProvider) CultureInfo.CurrentCulture, out result))
      {
        uint b = result & (uint) byte.MaxValue;
        uint g = result >> 8 & (uint) byte.MaxValue;
        uint r = result >> 16 & (uint) byte.MaxValue;
        hsl = UICharacterCreation.RgbToScaledHsl(new Color((int) r, (int) g, (int) b));
        return true;
      }
      hsl = Vector3.Zero;
      return false;
    }

    private void Click_RandomizeSingleColor(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12);
      Vector3 randomColorVector = UICharacterCreation.GetRandomColorVector();
      this.ApplyPendingColor(UICharacterCreation.ScaledHslToRgb(randomColorVector.X, randomColorVector.Y, randomColorVector.Z));
      this._currentColorHSL = randomColorVector;
      this.UpdateHexText(UICharacterCreation.ScaledHslToRgb(randomColorVector.X, randomColorVector.Y, randomColorVector.Z));
      this.UpdateColorPickers();
    }

    private static Vector3 GetRandomColorVector() => new Vector3(Main.rand.NextFloat(), Main.rand.NextFloat(), Main.rand.NextFloat());

    private void UnselectAllCategories()
    {
      foreach (UIColoredImageButton colorPicker in this._colorPickers)
        colorPicker?.SetSelected(false);
      this._clothingStylesCategoryButton.SetSelected(false);
      this._hairStylesCategoryButton.SetSelected(false);
      this._charInfoCategoryButton.SetSelected(false);
      this._hslContainer.Remove();
      this._hairstylesContainer.Remove();
      this._clothStylesContainer.Remove();
      this._infoContainer.Remove();
    }

    private void SelectColorPicker(UICharacterCreation.CategoryId selection)
    {
      this._selectedPicker = selection;
      switch (selection)
      {
        case UICharacterCreation.CategoryId.CharInfo:
          this.Click_CharInfo((UIMouseEvent) null, (UIElement) null);
          break;
        case UICharacterCreation.CategoryId.Clothing:
          this.Click_ClothStyles((UIMouseEvent) null, (UIElement) null);
          break;
        case UICharacterCreation.CategoryId.HairStyle:
          this.Click_HairStyles((UIMouseEvent) null, (UIElement) null);
          break;
        default:
          this.UnselectAllCategories();
          this._middleContainer.Append(this._hslContainer);
          for (int index = 0; index < this._colorPickers.Length; ++index)
          {
            if (this._colorPickers[index] != null)
              this._colorPickers[index].SetSelected((UICharacterCreation.CategoryId) index == selection);
          }
          Vector3 vector3 = Vector3.One;
          switch (this._selectedPicker)
          {
            case UICharacterCreation.CategoryId.HairColor:
              vector3 = UICharacterCreation.RgbToScaledHsl(this._player.hairColor);
              break;
            case UICharacterCreation.CategoryId.Eye:
              vector3 = UICharacterCreation.RgbToScaledHsl(this._player.eyeColor);
              break;
            case UICharacterCreation.CategoryId.Skin:
              vector3 = UICharacterCreation.RgbToScaledHsl(this._player.skinColor);
              break;
            case UICharacterCreation.CategoryId.Shirt:
              vector3 = UICharacterCreation.RgbToScaledHsl(this._player.shirtColor);
              break;
            case UICharacterCreation.CategoryId.Undershirt:
              vector3 = UICharacterCreation.RgbToScaledHsl(this._player.underShirtColor);
              break;
            case UICharacterCreation.CategoryId.Pants:
              vector3 = UICharacterCreation.RgbToScaledHsl(this._player.pantsColor);
              break;
            case UICharacterCreation.CategoryId.Shoes:
              vector3 = UICharacterCreation.RgbToScaledHsl(this._player.shoeColor);
              break;
          }
          this._currentColorHSL = vector3;
          this.UpdateHexText(UICharacterCreation.ScaledHslToRgb(vector3.X, vector3.Y, vector3.Z));
          break;
      }
    }

    private void UpdateColorPickers()
    {
      int selectedPicker = (int) this._selectedPicker;
      this._colorPickers[3].SetColor(this._player.hairColor);
      this._hairStylesCategoryButton.SetColor(this._player.hairColor);
      this._colorPickers[4].SetColor(this._player.eyeColor);
      this._colorPickers[5].SetColor(this._player.skinColor);
      this._colorPickers[6].SetColor(this._player.shirtColor);
      this._colorPickers[7].SetColor(this._player.underShirtColor);
      this._colorPickers[8].SetColor(this._player.pantsColor);
      this._colorPickers[9].SetColor(this._player.shoeColor);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      string text = (string) null;
      if (this._copyHexButton.IsMouseHovering)
        text = Language.GetTextValue("UI.CopyColorToClipboard");
      if (this._pasteHexButton.IsMouseHovering)
        text = Language.GetTextValue("UI.PasteColorFromClipboard");
      if (this._randomColorButton.IsMouseHovering)
        text = Language.GetTextValue("UI.RandomizeColor");
      if (this._copyTemplateButton.IsMouseHovering)
        text = Language.GetTextValue("UI.CopyPlayerToClipboard");
      if (this._pasteTemplateButton.IsMouseHovering)
        text = Language.GetTextValue("UI.PastePlayerFromClipboard");
      if (this._randomizePlayerButton.IsMouseHovering)
        text = Language.GetTextValue("UI.RandomizePlayer");
      if (text != null)
      {
        float x = FontAssets.MouseText.Value.MeasureString(text).X;
        Vector2 vector2 = new Vector2((float) Main.mouseX, (float) Main.mouseY) + new Vector2(16f);
        if ((double) vector2.Y > (double) (Main.screenHeight - 30))
          vector2.Y = (float) (Main.screenHeight - 30);
        if ((double) vector2.X > (double) Main.screenWidth - (double) x)
          vector2.X = (float) (Main.screenWidth - 460);
        Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, vector2.X, vector2.Y, new Color((int) Main.mouseTextColor, (int) Main.mouseTextColor, (int) Main.mouseTextColor, (int) Main.mouseTextColor), Color.Black, Vector2.Zero);
      }
      this.SetupGamepadPoints(spriteBatch);
    }

    private void SetupGamepadPoints(SpriteBatch spriteBatch)
    {
      UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
      int num1 = 3000;
      int num2 = num1 + 20;
      int num3 = num1;
      List<SnapPoint> snapPoints = this.GetSnapPoints();
      SnapPoint snapPoint1 = snapPoints.First<SnapPoint>((Func<SnapPoint, bool>) (a => a.Name == "Back"));
      SnapPoint snapPoint2 = snapPoints.First<SnapPoint>((Func<SnapPoint, bool>) (a => a.Name == "Create"));
      UILinkPoint point1 = UILinkPointNavigator.Points[num3];
      point1.Unlink();
      UILinkPointNavigator.SetPosition(num3, snapPoint1.Position);
      int num4 = num3 + 1;
      UILinkPoint point2 = UILinkPointNavigator.Points[num4];
      point2.Unlink();
      UILinkPointNavigator.SetPosition(num4, snapPoint2.Position);
      int num5 = num4 + 1;
      point1.Right = point2.ID;
      point2.Left = point1.ID;
      this._foundPoints.Clear();
      this._foundPoints.Add(point1.ID);
      this._foundPoints.Add(point2.ID);
      List<SnapPoint> list1 = snapPoints.Where<SnapPoint>((Func<SnapPoint, bool>) (a => a.Name == "Top")).ToList<SnapPoint>();
      list1.Sort(new Comparison<SnapPoint>(this.SortPoints));
      for (int index = 0; index < list1.Count; ++index)
      {
        UILinkPoint point3 = UILinkPointNavigator.Points[num5];
        point3.Unlink();
        UILinkPointNavigator.SetPosition(num5, list1[index].Position);
        point3.Left = num5 - 1;
        point3.Right = num5 + 1;
        point3.Down = num2;
        if (index == 0)
          point3.Left = -3;
        if (index == list1.Count - 1)
          point3.Right = -4;
        if (this._selectedPicker == UICharacterCreation.CategoryId.HairStyle || this._selectedPicker == UICharacterCreation.CategoryId.Clothing)
          point3.Down = num2 + index;
        this._foundPoints.Add(num5);
        ++num5;
      }
      List<SnapPoint> list2 = snapPoints.Where<SnapPoint>((Func<SnapPoint, bool>) (a => a.Name == "Middle")).ToList<SnapPoint>();
      list2.Sort(new Comparison<SnapPoint>(this.SortPoints));
      int ptid1 = num2;
      switch (this._selectedPicker)
      {
        case UICharacterCreation.CategoryId.CharInfo:
          for (int index = 0; index < list2.Count; ++index)
          {
            UILinkPoint andSet = this.GetAndSet(ptid1, list2[index]);
            andSet.Up = andSet.ID - 1;
            andSet.Down = andSet.ID + 1;
            if (index == 0)
              andSet.Up = num1 + 2;
            if (index == list2.Count - 1)
            {
              andSet.Down = point1.ID;
              point1.Up = andSet.ID;
              point2.Up = andSet.ID;
            }
            this._foundPoints.Add(ptid1);
            ++ptid1;
          }
          break;
        case UICharacterCreation.CategoryId.Clothing:
          List<SnapPoint> list3 = snapPoints.Where<SnapPoint>((Func<SnapPoint, bool>) (a => a.Name == "Low")).ToList<SnapPoint>();
          list3.Sort(new Comparison<SnapPoint>(this.SortPoints));
          int num6 = -2;
          int num7 = -2;
          int ptid2 = num2 + 20;
          for (int index = 0; index < list3.Count; ++index)
          {
            UILinkPoint andSet = this.GetAndSet(ptid2, list3[index]);
            andSet.Up = num2 + index + 2;
            andSet.Down = point1.ID;
            if (index >= 3)
            {
              ++andSet.Up;
              andSet.Down = point2.ID;
            }
            andSet.Left = andSet.ID - 1;
            andSet.Right = andSet.ID + 1;
            if (index == 0)
            {
              num6 = andSet.ID;
              andSet.Left = andSet.ID + 4;
              point1.Up = andSet.ID;
            }
            if (index == list3.Count - 1)
            {
              num7 = andSet.ID;
              andSet.Right = andSet.ID - 4;
              point2.Up = andSet.ID;
            }
            this._foundPoints.Add(ptid2);
            ++ptid2;
          }
          int ptid3 = num2;
          for (int index = 0; index < list2.Count; ++index)
          {
            UILinkPoint andSet = this.GetAndSet(ptid3, list2[index]);
            andSet.Up = num1 + 2 + index;
            andSet.Left = andSet.ID - 1;
            andSet.Right = andSet.ID + 1;
            if (index == 0)
              andSet.Left = andSet.ID + 9;
            if (index == list2.Count - 1)
              andSet.Right = andSet.ID - 9;
            andSet.Down = num6;
            if (index >= 5)
              andSet.Down = num7;
            this._foundPoints.Add(ptid3);
            ++ptid3;
          }
          break;
        case UICharacterCreation.CategoryId.HairStyle:
          if (list2.Count != 0)
          {
            this._helper.CullPointsOutOfElementArea(spriteBatch, list2, this._hairstylesContainer);
            SnapPoint snapPoint3 = list2[list2.Count - 1];
            int num8 = snapPoint3.Id / 10;
            int num9 = snapPoint3.Id % 10;
            int count = Main.Hairstyles.AvailableHairstyles.Count;
            for (int index = 0; index < list2.Count; ++index)
            {
              SnapPoint snap = list2[index];
              UILinkPoint andSet = this.GetAndSet(ptid1, snap);
              andSet.Left = andSet.ID - 1;
              if (snap.Id == 0)
                andSet.Left = -3;
              andSet.Right = andSet.ID + 1;
              if (snap.Id == count - 1)
                andSet.Right = -4;
              andSet.Up = andSet.ID - 10;
              if (index < 10)
                andSet.Up = num1 + 2 + index;
              andSet.Down = andSet.ID + 10;
              if (snap.Id + 10 > snapPoint3.Id)
                andSet.Down = snap.Id % 10 >= 5 ? point2.ID : point1.ID;
              if (index == list2.Count - 1)
              {
                point1.Up = andSet.ID;
                point2.Up = andSet.ID;
              }
              this._foundPoints.Add(ptid1);
              ++ptid1;
            }
            break;
          }
          break;
        default:
          List<SnapPoint> list4 = snapPoints.Where<SnapPoint>((Func<SnapPoint, bool>) (a => a.Name == "Low")).ToList<SnapPoint>();
          list4.Sort(new Comparison<SnapPoint>(this.SortPoints));
          int ptid4 = num2 + 20;
          for (int index = 0; index < list4.Count; ++index)
          {
            UILinkPoint andSet = this.GetAndSet(ptid4, list4[index]);
            andSet.Up = num2 + 2;
            andSet.Down = point1.ID;
            andSet.Left = andSet.ID - 1;
            andSet.Right = andSet.ID + 1;
            if (index == 0)
            {
              andSet.Left = andSet.ID + 2;
              point1.Up = andSet.ID;
            }
            if (index == list4.Count - 1)
            {
              andSet.Right = andSet.ID - 2;
              point2.Up = andSet.ID;
            }
            this._foundPoints.Add(ptid4);
            ++ptid4;
          }
          int ptid5 = num2;
          for (int index = 0; index < list2.Count; ++index)
          {
            UILinkPoint andSet = this.GetAndSet(ptid5, list2[index]);
            andSet.Up = andSet.ID - 1;
            andSet.Down = andSet.ID + 1;
            if (index == 0)
              andSet.Up = num1 + 2 + 5;
            if (index == list2.Count - 1)
              andSet.Down = num2 + 20 + 2;
            this._foundPoints.Add(ptid5);
            ++ptid5;
          }
          break;
      }
      if (!PlayerInput.UsingGamepadUI || this._foundPoints.Contains(UILinkPointNavigator.CurrentPoint))
        return;
      this.MoveToVisuallyClosestPoint();
    }

    private void MoveToVisuallyClosestPoint()
    {
      Dictionary<int, UILinkPoint> points = UILinkPointNavigator.Points;
      Vector2 mouseScreen = Main.MouseScreen;
      UILinkPoint uiLinkPoint1 = (UILinkPoint) null;
      foreach (int foundPoint in this._foundPoints)
      {
        UILinkPoint uiLinkPoint2;
        if (!points.TryGetValue(foundPoint, out uiLinkPoint2))
          return;
        if (uiLinkPoint1 == null || (double) Vector2.Distance(mouseScreen, uiLinkPoint1.Position) > (double) Vector2.Distance(mouseScreen, uiLinkPoint2.Position))
          uiLinkPoint1 = uiLinkPoint2;
      }
      if (uiLinkPoint1 == null)
        return;
      UILinkPointNavigator.ChangePoint(uiLinkPoint1.ID);
    }

    public void TryMovingCategory(int direction)
    {
      int selection = (int) (this._selectedPicker + direction) % 10;
      if (selection < 0)
        selection += 10;
      this.SelectColorPicker((UICharacterCreation.CategoryId) selection);
    }

    private UILinkPoint GetAndSet(int ptid, SnapPoint snap)
    {
      UILinkPoint point = UILinkPointNavigator.Points[ptid];
      point.Unlink();
      UILinkPointNavigator.SetPosition(point.ID, snap.Position);
      return point;
    }

    private bool PointWithName(SnapPoint a, string comp) => a.Name == comp;

    private int SortPoints(SnapPoint a, SnapPoint b) => a.Id.CompareTo(b.Id);

    private static Color ScaledHslToRgb(Vector3 hsl) => UICharacterCreation.ScaledHslToRgb(hsl.X, hsl.Y, hsl.Z);

    private static Color ScaledHslToRgb(float hue, float saturation, float luminosity) => Main.hslToRgb(hue, saturation, (float) ((double) luminosity * 0.85000002384185791 + 0.15000000596046448));

    private static Vector3 RgbToScaledHsl(Color color)
    {
      Vector3 hsl = Main.rgbToHsl(color);
      hsl.Z = (float) (((double) hsl.Z - 0.15000000596046448) / 0.85000002384185791);
      return Vector3.Clamp(hsl, Vector3.Zero, Vector3.One);
    }

    private enum CategoryId
    {
      CharInfo,
      Clothing,
      HairStyle,
      HairColor,
      Eye,
      Skin,
      Shirt,
      Undershirt,
      Pants,
      Shoes,
      Count,
    }

    private enum HSLSliderId
    {
      Hue,
      Saturation,
      Luminance,
    }
  }
}
