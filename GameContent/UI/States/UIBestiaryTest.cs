// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.States.UIBestiaryTest
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
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
  public class UIBestiaryTest : UIState
  {
    private UIElement _bestiarySpace;
    private UIBestiaryEntryInfoPage _infoSpace;
    private UIBestiaryEntryButton _selectedEntryButton;
    private List<BestiaryEntry> _originalEntriesList;
    private List<BestiaryEntry> _workingSetEntries;
    private UIText _indexesRangeText;
    private EntryFilterer<BestiaryEntry, IBestiaryEntryFilter> _filterer = new EntryFilterer<BestiaryEntry, IBestiaryEntryFilter>();
    private EntrySorter<BestiaryEntry, IBestiarySortStep> _sorter = new EntrySorter<BestiaryEntry, IBestiarySortStep>();
    private UIBestiaryEntryGrid _entryGrid;
    private UIBestiarySortingOptionsGrid _sortingGrid;
    private UIBestiaryFilteringOptionsGrid _filteringGrid;
    private UISearchBar _searchBar;
    private UIPanel _searchBoxPanel;
    private UIText _sortingText;
    private UIText _filteringText;
    private string _searchString;
    private BestiaryUnlockProgressReport _progressReport;
    private UIText _progressPercentText;
    private UIColoredSliderSimple _unlocksProgressBar;
    private bool _didClickSomething;
    private bool _didClickSearchBar;

    public UIBestiaryTest(BestiaryDatabase database)
    {
      this._filterer.SetSearchFilterObject<Filters.BySearch>(new Filters.BySearch());
      this._originalEntriesList = new List<BestiaryEntry>((IEnumerable<BestiaryEntry>) database.Entries);
      this._workingSetEntries = new List<BestiaryEntry>((IEnumerable<BestiaryEntry>) this._originalEntriesList);
      this._filterer.AddFilters(database.Filters);
      this._sorter.AddSortSteps(database.SortSteps);
      this.BuildPage();
    }

    public void OnOpenPage() => this.UpdateBestiaryContents();

    private void BuildPage()
    {
      this.RemoveAllChildren();
      int num1 = true.ToInt() * 100;
      UIElement uiElement1 = new UIElement();
      uiElement1.Width.Set(0.0f, 0.875f);
      uiElement1.MaxWidth.Set(800f + (float) num1, 0.0f);
      uiElement1.MinWidth.Set(600f + (float) num1, 0.0f);
      uiElement1.Top.Set(220f, 0.0f);
      uiElement1.Height.Set(-220f, 1f);
      uiElement1.HAlign = 0.5f;
      this.Append(uiElement1);
      this.MakeExitButton(uiElement1);
      UIPanel element1 = new UIPanel();
      element1.Width.Set(0.0f, 1f);
      element1.Height.Set(-90f, 1f);
      element1.BackgroundColor = new Color(33, 43, 79) * 0.8f;
      uiElement1.Append((UIElement) element1);
      element1.PaddingTop -= 4f;
      element1.PaddingBottom -= 4f;
      int pixels = 24;
      UIElement uiElement2 = new UIElement()
      {
        Width = new StyleDimension(0.0f, 1f),
        Height = new StyleDimension((float) pixels, 0.0f),
        VAlign = 0.0f
      };
      uiElement2.SetPadding(0.0f);
      element1.Append(uiElement2);
      UIBestiaryEntryInfoPage bestiaryEntryInfoPage1 = new UIBestiaryEntryInfoPage();
      bestiaryEntryInfoPage1.Height = new StyleDimension(12f, 1f);
      bestiaryEntryInfoPage1.HAlign = 1f;
      UIBestiaryEntryInfoPage bestiaryEntryInfoPage2 = bestiaryEntryInfoPage1;
      this.AddSortAndFilterButtons(uiElement2, bestiaryEntryInfoPage2);
      this.AddSearchBar(uiElement2, bestiaryEntryInfoPage2);
      int num2 = 20;
      UIElement element2 = new UIElement()
      {
        Width = new StyleDimension(0.0f, 1f),
        Height = new StyleDimension((float) (-pixels - 6 - num2), 1f),
        VAlign = 1f,
        Top = new StyleDimension((float) -num2, 0.0f)
      };
      element2.SetPadding(0.0f);
      element1.Append(element2);
      UIElement uiElement3 = new UIElement()
      {
        Width = new StyleDimension(0.0f, 1f),
        Height = new StyleDimension(20f, 0.0f),
        VAlign = 1f
      };
      element1.Append(uiElement3);
      uiElement3.SetPadding(0.0f);
      this.FillProgressBottomBar(uiElement3);
      UIElement element3 = new UIElement()
      {
        Width = new StyleDimension(-12f - bestiaryEntryInfoPage2.Width.Pixels, 1f),
        Height = new StyleDimension(-4f, 1f),
        VAlign = 1f
      };
      element2.Append(element3);
      element3.SetPadding(0.0f);
      this._bestiarySpace = element3;
      UIBestiaryEntryGrid element4 = new UIBestiaryEntryGrid(this._workingSetEntries, new UIElement.MouseEvent(this.Click_SelectEntryButton));
      element3.Append((UIElement) element4);
      this._entryGrid = element4;
      this._entryGrid.OnGridContentsChanged += new Action(this.UpdateBestiaryGridRange);
      element2.Append((UIElement) bestiaryEntryInfoPage2);
      this._infoSpace = bestiaryEntryInfoPage2;
      this.AddBackAndForwardButtons(uiElement2);
      this._sortingGrid = new UIBestiarySortingOptionsGrid(this._sorter);
      this._sortingGrid.OnLeftClick += new UIElement.MouseEvent(this.Click_CloseSortingGrid);
      this._sortingGrid.OnClickingOption += new Action(this.UpdateBestiaryContents);
      this._filteringGrid = new UIBestiaryFilteringOptionsGrid(this._filterer);
      this._filteringGrid.OnLeftClick += new UIElement.MouseEvent(this.Click_CloseFilteringGrid);
      this._filteringGrid.OnClickingOption += new Action(this.UpdateBestiaryContents);
      this._filteringGrid.SetupAvailabilityTest(this._originalEntriesList);
      this._searchBar.SetContents((string) null, true);
      this.UpdateBestiaryContents();
    }

    private void FillProgressBottomBar(UIElement container)
    {
      UIText uiText = new UIText("", 0.8f);
      uiText.HAlign = 0.0f;
      uiText.VAlign = 1f;
      uiText.TextOriginX = 0.0f;
      uiText.TextOriginY = 0.0f;
      this._progressPercentText = uiText;
      UIColoredSliderSimple coloredSliderSimple = new UIColoredSliderSimple();
      coloredSliderSimple.Width = new StyleDimension(0.0f, 1f);
      coloredSliderSimple.Height = new StyleDimension(15f, 0.0f);
      coloredSliderSimple.VAlign = 1f;
      coloredSliderSimple.FilledColor = new Color(51, 137, (int) byte.MaxValue);
      coloredSliderSimple.EmptyColor = new Color(35, 43, 81);
      coloredSliderSimple.FillPercent = 0.0f;
      UIColoredSliderSimple element = coloredSliderSimple;
      element.OnUpdate += new UIElement.ElementEvent(this.ShowStats_Completion);
      this._unlocksProgressBar = element;
      container.Append((UIElement) element);
    }

    private void ShowStats_Completion(UIElement element)
    {
      if (!element.IsMouseHovering)
        return;
      string completionPercentText = this.GetCompletionPercentText();
      Main.instance.MouseText(completionPercentText);
    }

    private string GetCompletionPercentText() => Language.GetTextValueWith("BestiaryInfo.PercentCollected", (object) new
    {
      Percent = Utils.PrettifyPercentDisplay(this.GetProgressPercent(), "P2")
    });

    private float GetProgressPercent() => this._progressReport.CompletionPercent;

    private void EmptyInteraction(float input)
    {
    }

    private void EmptyInteraction2()
    {
    }

    private Color GetColorAtBlip(float percentile) => (double) percentile < (double) this.GetProgressPercent() ? new Color(51, 137, (int) byte.MaxValue) : new Color(35, 40, 83);

    private void AddBackAndForwardButtons(UIElement innerTopContainer)
    {
      UIImageButton element1 = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Back", (AssetRequestMode) 1));
      element1.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Border", (AssetRequestMode) 1));
      element1.SetVisibility(1f, 1f);
      element1.SetSnapPoint("BackPage", 0);
      this._entryGrid.MakeButtonGoByOffset((UIElement) element1, -1);
      innerTopContainer.Append((UIElement) element1);
      UIImageButton uiImageButton = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Forward", (AssetRequestMode) 1));
      uiImageButton.Left = new StyleDimension(element1.Width.Pixels + 1f, 0.0f);
      UIImageButton element2 = uiImageButton;
      element2.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Border", (AssetRequestMode) 1));
      element2.SetVisibility(1f, 1f);
      element2.SetSnapPoint("NextPage", 0);
      this._entryGrid.MakeButtonGoByOffset((UIElement) element2, 1);
      innerTopContainer.Append((UIElement) element2);
      UIPanel uiPanel = new UIPanel();
      uiPanel.Left = new StyleDimension((float) ((double) element1.Width.Pixels + 1.0 + (double) element2.Width.Pixels + 3.0), 0.0f);
      uiPanel.Width = new StyleDimension(135f, 0.0f);
      uiPanel.Height = new StyleDimension(0.0f, 1f);
      uiPanel.VAlign = 0.5f;
      UIPanel element3 = uiPanel;
      element3.BackgroundColor = new Color(35, 40, 83);
      element3.BorderColor = new Color(35, 40, 83);
      element3.SetPadding(0.0f);
      innerTopContainer.Append((UIElement) element3);
      UIText uiText = new UIText("9000-9999 (9001)", 0.8f);
      uiText.HAlign = 0.5f;
      uiText.VAlign = 0.5f;
      UIText element4 = uiText;
      element3.Append((UIElement) element4);
      this._indexesRangeText = element4;
    }

    private void AddSortAndFilterButtons(
      UIElement innerTopContainer,
      UIBestiaryEntryInfoPage infoSpace)
    {
      int num = 17;
      UIImageButton uiImageButton1 = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Filtering", (AssetRequestMode) 1));
      uiImageButton1.Left = new StyleDimension(-infoSpace.Width.Pixels - (float) num, 0.0f);
      uiImageButton1.HAlign = 1f;
      UIImageButton element1 = uiImageButton1;
      element1.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Wide_Border", (AssetRequestMode) 1));
      element1.SetVisibility(1f, 1f);
      element1.SetSnapPoint("FilterButton", 0);
      element1.OnLeftClick += new UIElement.MouseEvent(this.OpenOrCloseFilteringGrid);
      innerTopContainer.Append((UIElement) element1);
      UIText uiText1 = new UIText("", 0.8f);
      uiText1.Left = new StyleDimension(34f, 0.0f);
      uiText1.Top = new StyleDimension(2f, 0.0f);
      uiText1.VAlign = 0.5f;
      uiText1.TextOriginX = 0.0f;
      uiText1.TextOriginY = 0.0f;
      UIText element2 = uiText1;
      element1.Append((UIElement) element2);
      this._filteringText = element2;
      UIImageButton uiImageButton2 = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Sorting", (AssetRequestMode) 1));
      uiImageButton2.Left = new StyleDimension((float) (-(double) infoSpace.Width.Pixels - (double) element1.Width.Pixels - 3.0) - (float) num, 0.0f);
      uiImageButton2.HAlign = 1f;
      UIImageButton element3 = uiImageButton2;
      element3.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Wide_Border", (AssetRequestMode) 1));
      element3.SetVisibility(1f, 1f);
      element3.SetSnapPoint("SortButton", 0);
      element3.OnLeftClick += new UIElement.MouseEvent(this.OpenOrCloseSortingOptions);
      innerTopContainer.Append((UIElement) element3);
      UIText uiText2 = new UIText("", 0.8f);
      uiText2.Left = new StyleDimension(34f, 0.0f);
      uiText2.Top = new StyleDimension(2f, 0.0f);
      uiText2.VAlign = 0.5f;
      uiText2.TextOriginX = 0.0f;
      uiText2.TextOriginY = 0.0f;
      UIText element4 = uiText2;
      element3.Append((UIElement) element4);
      this._sortingText = element4;
    }

    private void AddSearchBar(UIElement innerTopContainer, UIBestiaryEntryInfoPage infoSpace)
    {
      UIImageButton uiImageButton1 = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Search", (AssetRequestMode) 1));
      uiImageButton1.Left = new StyleDimension(-infoSpace.Width.Pixels, 1f);
      uiImageButton1.VAlign = 0.5f;
      UIImageButton element1 = uiImageButton1;
      element1.OnLeftClick += new UIElement.MouseEvent(this.Click_SearchArea);
      element1.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Search_Border", (AssetRequestMode) 1));
      element1.SetVisibility(1f, 1f);
      element1.SetSnapPoint("SearchButton", 0);
      innerTopContainer.Append((UIElement) element1);
      UIPanel uiPanel = new UIPanel();
      uiPanel.Left = new StyleDimension((float) (-(double) infoSpace.Width.Pixels + (double) element1.Width.Pixels + 3.0), 1f);
      uiPanel.Width = new StyleDimension((float) ((double) infoSpace.Width.Pixels - (double) element1.Width.Pixels - 3.0), 0.0f);
      uiPanel.Height = new StyleDimension(0.0f, 1f);
      uiPanel.VAlign = 0.5f;
      UIPanel element2 = uiPanel;
      this._searchBoxPanel = element2;
      element2.BackgroundColor = new Color(35, 40, 83);
      element2.BorderColor = new Color(35, 40, 83);
      element2.SetPadding(0.0f);
      innerTopContainer.Append((UIElement) element2);
      UISearchBar uiSearchBar = new UISearchBar(Language.GetText("UI.PlayerNameSlot"), 0.8f);
      uiSearchBar.Width = new StyleDimension(0.0f, 1f);
      uiSearchBar.Height = new StyleDimension(0.0f, 1f);
      uiSearchBar.HAlign = 0.0f;
      uiSearchBar.VAlign = 0.5f;
      uiSearchBar.Left = new StyleDimension(0.0f, 0.0f);
      uiSearchBar.IgnoresMouseInteraction = true;
      UISearchBar element3 = uiSearchBar;
      this._searchBar = element3;
      element2.OnLeftClick += new UIElement.MouseEvent(this.Click_SearchArea);
      element3.OnContentsChanged += new Action<string>(this.OnSearchContentsChanged);
      element2.Append((UIElement) element3);
      element3.OnStartTakingInput += new Action(this.OnStartTakingInput);
      element3.OnEndTakingInput += new Action(this.OnEndTakingInput);
      element3.OnNeedingVirtualKeyboard += new Action(this.OpenVirtualKeyboardWhenNeeded);
      UIImageButton uiImageButton2 = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/SearchCancel", (AssetRequestMode) 1));
      uiImageButton2.HAlign = 1f;
      uiImageButton2.VAlign = 0.5f;
      uiImageButton2.Left = new StyleDimension(-2f, 0.0f);
      UIImageButton element4 = uiImageButton2;
      element4.OnMouseOver += new UIElement.MouseEvent(this.searchCancelButton_OnMouseOver);
      element4.OnLeftClick += new UIElement.MouseEvent(this.searchCancelButton_OnClick);
      element2.Append((UIElement) element4);
    }

    private void searchCancelButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
    {
      if (this._searchBar.HasContents)
      {
        this._searchBar.SetContents((string) null, true);
        SoundEngine.PlaySound(11);
      }
      else
        SoundEngine.PlaySound(12);
    }

    private void searchCancelButton_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) => SoundEngine.PlaySound(12);

    private void OpenVirtualKeyboardWhenNeeded()
    {
      int length = 40;
      UIVirtualKeyboard state = new UIVirtualKeyboard(Language.GetText("UI.PlayerNameSlot").Value, this._searchString, new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingName), new Action(this.GoBackHere), allowEmpty: true);
      state.SetMaxInputLength(length);
      UserInterface.ActiveInstance.SetState((UIState) state);
    }

    private void OnFinishedSettingName(string name)
    {
      this._searchBar.SetContents(name.Trim());
      this.GoBackHere();
    }

    private void GoBackHere()
    {
      UserInterface.ActiveInstance.SetState((UIState) this);
      this._searchBar.ToggleTakingText();
    }

    private void OnStartTakingInput() => this._searchBoxPanel.BorderColor = Main.OurFavoriteColor;

    private void OnEndTakingInput() => this._searchBoxPanel.BorderColor = new Color(35, 40, 83);

    private void OnSearchContentsChanged(string contents)
    {
      this._searchString = contents;
      this._filterer.SetSearchFilter(contents);
      this.UpdateBestiaryContents();
    }

    private void Click_SearchArea(UIMouseEvent evt, UIElement listeningElement)
    {
      if (evt.Target.Parent == this._searchBoxPanel)
        return;
      this._searchBar.ToggleTakingText();
      this._didClickSearchBar = true;
    }

    public override void LeftClick(UIMouseEvent evt)
    {
      base.LeftClick(evt);
      this.AttemptStoppingUsingSearchbar(evt);
    }

    public override void RightClick(UIMouseEvent evt)
    {
      base.RightClick(evt);
      this.AttemptStoppingUsingSearchbar(evt);
    }

    private void AttemptStoppingUsingSearchbar(UIMouseEvent evt) => this._didClickSomething = true;

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      if (this._didClickSomething && !this._didClickSearchBar && this._searchBar.IsWritingText)
        this._searchBar.ToggleTakingText();
      this._didClickSomething = false;
      this._didClickSearchBar = false;
    }

    private void FilterEntries()
    {
      this._workingSetEntries.Clear();
      this._workingSetEntries.AddRange(this._originalEntriesList.Where<BestiaryEntry>(new Func<BestiaryEntry, bool>(this._filterer.FitsFilter)));
    }

    private void SortEntries()
    {
      foreach (BestiaryEntry workingSetEntry in this._workingSetEntries)
      {
        foreach (IBestiaryInfoElement bestiaryInfoElement in workingSetEntry.Info)
        {
          if (bestiaryInfoElement is IUpdateBeforeSorting updateBeforeSorting)
            updateBeforeSorting.UpdateBeforeSorting();
        }
      }
      this._workingSetEntries.Sort((IComparer<BestiaryEntry>) this._sorter);
    }

    private void FillBestiarySpaceWithEntries()
    {
      if (this._entryGrid == null || this._entryGrid.Parent == null)
        return;
      this.DeselectEntryButton();
      this._progressReport = this.GetUnlockProgress();
      this._entryGrid.FillBestiarySpaceWithEntries();
    }

    public void UpdateBestiaryGridRange() => this._indexesRangeText.SetText(this._entryGrid.GetRangeText());

    public override void Recalculate()
    {
      base.Recalculate();
      this.FillBestiarySpaceWithEntries();
    }

    private void GetEntriesToShow(
      out int maxEntriesWidth,
      out int maxEntriesHeight,
      out int maxEntriesToHave)
    {
      Rectangle rectangle = this._bestiarySpace.GetDimensions().ToRectangle();
      maxEntriesWidth = rectangle.Width / 72;
      maxEntriesHeight = rectangle.Height / 72;
      int num = 0;
      maxEntriesToHave = maxEntriesWidth * maxEntriesHeight - num;
    }

    private void MakeExitButton(UIElement outerContainer)
    {
      UITextPanel<LocalizedText> uiTextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
      uiTextPanel.Width = StyleDimension.FromPixelsAndPercent(-10f, 0.5f);
      uiTextPanel.Height = StyleDimension.FromPixels(50f);
      uiTextPanel.VAlign = 1f;
      uiTextPanel.HAlign = 0.5f;
      uiTextPanel.Top = StyleDimension.FromPixels(-25f);
      UITextPanel<LocalizedText> element = uiTextPanel;
      element.OnMouseOver += new UIElement.MouseEvent(this.FadedMouseOver);
      element.OnMouseOut += new UIElement.MouseEvent(this.FadedMouseOut);
      element.OnLeftMouseDown += new UIElement.MouseEvent(this.Click_GoBack);
      element.SetSnapPoint("ExitButton", 0);
      outerContainer.Append((UIElement) element);
    }

    private void Click_GoBack(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(11);
      if (Main.gameMenu)
        Main.menuMode = 0;
      else
        IngameFancyUI.Close();
    }

    private void OpenOrCloseSortingOptions(UIMouseEvent evt, UIElement listeningElement)
    {
      if (this._sortingGrid.Parent != null)
      {
        this.CloseSortingGrid();
      }
      else
      {
        this._bestiarySpace.RemoveChild((UIElement) this._sortingGrid);
        this._bestiarySpace.RemoveChild((UIElement) this._filteringGrid);
        this._bestiarySpace.Append((UIElement) this._sortingGrid);
      }
    }

    private void OpenOrCloseFilteringGrid(UIMouseEvent evt, UIElement listeningElement)
    {
      if (this._filteringGrid.Parent != null)
      {
        this.CloseFilteringGrid();
      }
      else
      {
        this._bestiarySpace.RemoveChild((UIElement) this._sortingGrid);
        this._bestiarySpace.RemoveChild((UIElement) this._filteringGrid);
        this._bestiarySpace.Append((UIElement) this._filteringGrid);
      }
    }

    private void Click_CloseFilteringGrid(UIMouseEvent evt, UIElement listeningElement)
    {
      if (evt.Target != this._filteringGrid)
        return;
      this.CloseFilteringGrid();
    }

    private void CloseFilteringGrid()
    {
      this.UpdateBestiaryContents();
      this._bestiarySpace.RemoveChild((UIElement) this._filteringGrid);
    }

    private void UpdateBestiaryContents()
    {
      this._filteringGrid.UpdateAvailability();
      this._sortingText.SetText(this._sorter.GetDisplayName());
      this._filteringText.SetText(this._filterer.GetDisplayName());
      this.FilterEntries();
      this.SortEntries();
      this.FillBestiarySpaceWithEntries();
      this._progressReport = this.GetUnlockProgress();
      this._progressPercentText.SetText(this.GetCompletionPercentText());
      this._unlocksProgressBar.FillPercent = this.GetProgressPercent();
    }

    private void Click_CloseSortingGrid(UIMouseEvent evt, UIElement listeningElement)
    {
      if (evt.Target != this._sortingGrid)
        return;
      this.CloseSortingGrid();
    }

    private void CloseSortingGrid()
    {
      this.UpdateBestiaryContents();
      this._bestiarySpace.RemoveChild((UIElement) this._sortingGrid);
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

    private void Click_SelectEntryButton(UIMouseEvent evt, UIElement listeningElement)
    {
      UIBestiaryEntryButton button = (UIBestiaryEntryButton) listeningElement;
      if (button == null)
        return;
      this.SelectEntryButton(button);
    }

    private void SelectEntryButton(UIBestiaryEntryButton button)
    {
      this.DeselectEntryButton();
      this._selectedEntryButton = button;
      this._infoSpace.FillInfoForEntry(button.Entry, new ExtraBestiaryInfoPageInformation()
      {
        BestiaryProgressReport = this._progressReport
      });
    }

    private void DeselectEntryButton() => this._infoSpace.FillInfoForEntry((BestiaryEntry) null, new ExtraBestiaryInfoPageInformation());

    public BestiaryUnlockProgressReport GetUnlockProgress()
    {
      float num1 = 0.0f;
      int num2 = 0;
      List<BestiaryEntry> originalEntriesList = this._originalEntriesList;
      for (int index = 0; index < originalEntriesList.Count; ++index)
      {
        int num3 = originalEntriesList[index].UIInfoProvider.GetEntryUICollectionInfo().UnlockState > BestiaryEntryUnlockState.NotKnownAtAll_0 ? 1 : 0;
        ++num2;
        num1 += (float) num3;
      }
      return new BestiaryUnlockProgressReport()
      {
        EntriesTotal = num2,
        CompletionAmountTotal = num1
      };
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      this.SetupGamepadPoints(spriteBatch);
    }

    private void SetupGamepadPoints(SpriteBatch spriteBatch)
    {
      UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
      int num1 = 3000;
      int num2 = num1;
      List<SnapPoint> snapPoints = this.GetSnapPoints();
      SnapPoint snap1 = (SnapPoint) null;
      SnapPoint snap2 = (SnapPoint) null;
      SnapPoint snap3 = (SnapPoint) null;
      SnapPoint snap4 = (SnapPoint) null;
      SnapPoint snap5 = (SnapPoint) null;
      SnapPoint snap6 = (SnapPoint) null;
      for (int index = 0; index < snapPoints.Count; ++index)
      {
        SnapPoint snapPoint = snapPoints[index];
        string name = snapPoint.Name;
        if (!(name == "BackPage"))
        {
          if (!(name == "NextPage"))
          {
            if (!(name == "ExitButton"))
            {
              if (!(name == "FilterButton"))
              {
                if (!(name == "SortButton"))
                {
                  if (name == "SearchButton")
                    snap5 = snapPoint;
                }
                else
                  snap3 = snapPoint;
              }
              else
                snap4 = snapPoint;
            }
            else
              snap6 = snapPoint;
          }
          else
            snap2 = snapPoint;
        }
        else
          snap1 = snapPoint;
      }
      int id1 = num2;
      int num3 = id1 + 1;
      UILinkPoint uiLinkPoint1 = this.MakeLinkPointFromSnapPoint(id1, snap1);
      int id2 = num3;
      int num4 = id2 + 1;
      UILinkPoint uiLinkPoint2 = this.MakeLinkPointFromSnapPoint(id2, snap2);
      int id3 = num4;
      int num5 = id3 + 1;
      UILinkPoint downSide1 = this.MakeLinkPointFromSnapPoint(id3, snap6);
      int id4 = num5;
      int num6 = id4 + 1;
      UILinkPoint uiLinkPoint3 = this.MakeLinkPointFromSnapPoint(id4, snap4);
      int id5 = num6;
      int num7 = id5 + 1;
      UILinkPoint uiLinkPoint4 = this.MakeLinkPointFromSnapPoint(id5, snap3);
      int id6 = num7;
      int currentID = id6 + 1;
      UILinkPoint rightSide = this.MakeLinkPointFromSnapPoint(id6, snap5);
      this.PairLeftRight(uiLinkPoint1, uiLinkPoint2);
      this.PairLeftRight(uiLinkPoint2, uiLinkPoint4);
      this.PairLeftRight(uiLinkPoint4, uiLinkPoint3);
      this.PairLeftRight(uiLinkPoint3, rightSide);
      downSide1.Up = uiLinkPoint2.ID;
      UILinkPoint[,] gridPoints = new UILinkPoint[1, 1];
      if (this._filteringGrid.Parent != null)
      {
        int gridWidth;
        int gridHeight;
        this.SetupPointsForFilterGrid(ref currentID, snapPoints, out gridWidth, out gridHeight, out gridPoints);
        this.PairUpDown(uiLinkPoint2, downSide1);
        this.PairUpDown(uiLinkPoint1, downSide1);
        for (int index = gridWidth - 1; index >= 0; --index)
        {
          UILinkPoint upSide1 = gridPoints[index, gridHeight - 1];
          if (upSide1 != null)
            this.PairUpDown(upSide1, downSide1);
          UILinkPoint upSide2 = gridPoints[index, gridHeight - 2];
          if (upSide2 != null && upSide1 == null)
            this.PairUpDown(upSide2, downSide1);
          UILinkPoint downSide2 = gridPoints[index, 0];
          if (downSide2 != null)
          {
            if (index < gridWidth - 3)
              this.PairUpDown(uiLinkPoint4, downSide2);
            else
              this.PairUpDown(uiLinkPoint3, downSide2);
          }
        }
      }
      else if (this._sortingGrid.Parent != null)
      {
        int gridWidth;
        int gridHeight;
        this.SetupPointsForSortingGrid(ref currentID, snapPoints, out gridWidth, out gridHeight, out gridPoints);
        this.PairUpDown(uiLinkPoint2, downSide1);
        this.PairUpDown(uiLinkPoint1, downSide1);
        for (int index = gridWidth - 1; index >= 0; --index)
        {
          UILinkPoint upSide = gridPoints[index, gridHeight - 1];
          if (upSide != null)
            this.PairUpDown(upSide, downSide1);
          UILinkPoint downSide3 = gridPoints[index, 0];
          if (downSide3 != null)
          {
            this.PairUpDown(uiLinkPoint3, downSide3);
            this.PairUpDown(uiLinkPoint4, downSide3);
          }
        }
      }
      else if (this._entryGrid.Parent != null)
      {
        int gridWidth;
        int gridHeight;
        this.SetupPointsForEntryGrid(ref currentID, snapPoints, out gridWidth, out gridHeight, out gridPoints);
        for (int index = 0; index < gridWidth; ++index)
        {
          if (gridHeight - 1 >= 0)
          {
            UILinkPoint upSide3 = gridPoints[index, gridHeight - 1];
            if (upSide3 != null)
              this.PairUpDown(upSide3, downSide1);
            if (gridHeight - 2 >= 0)
            {
              UILinkPoint upSide4 = gridPoints[index, gridHeight - 2];
              if (upSide4 != null && upSide3 == null)
                this.PairUpDown(upSide4, downSide1);
            }
          }
          UILinkPoint downSide4 = gridPoints[index, 0];
          if (downSide4 != null)
          {
            if (index < gridWidth / 2)
              this.PairUpDown(uiLinkPoint2, downSide4);
            else if (index == gridWidth - 1)
              this.PairUpDown(uiLinkPoint3, downSide4);
            else
              this.PairUpDown(uiLinkPoint4, downSide4);
          }
        }
        UILinkPoint downSide5 = gridPoints[0, 0];
        if (downSide5 != null)
        {
          this.PairUpDown(uiLinkPoint2, downSide5);
          this.PairUpDown(uiLinkPoint1, downSide5);
        }
        else
        {
          this.PairUpDown(uiLinkPoint2, downSide1);
          this.PairUpDown(uiLinkPoint1, downSide1);
          this.PairUpDown(uiLinkPoint3, downSide1);
          this.PairUpDown(uiLinkPoint4, downSide1);
        }
      }
      List<UILinkPoint> lostrefpoints = new List<UILinkPoint>();
      for (int key = num1; key < currentID; ++key)
        lostrefpoints.Add(UILinkPointNavigator.Points[key]);
      if (!PlayerInput.UsingGamepadUI || UILinkPointNavigator.CurrentPoint < currentID)
        return;
      this.MoveToVisuallyClosestPoint(lostrefpoints);
    }

    private void MoveToVisuallyClosestPoint(List<UILinkPoint> lostrefpoints)
    {
      Dictionary<int, UILinkPoint> points = UILinkPointNavigator.Points;
      Vector2 mouseScreen = Main.MouseScreen;
      UILinkPoint uiLinkPoint = (UILinkPoint) null;
      foreach (UILinkPoint lostrefpoint in lostrefpoints)
      {
        if (uiLinkPoint == null || (double) Vector2.Distance(mouseScreen, uiLinkPoint.Position) > (double) Vector2.Distance(mouseScreen, lostrefpoint.Position))
          uiLinkPoint = lostrefpoint;
      }
      if (uiLinkPoint == null)
        return;
      UILinkPointNavigator.ChangePoint(uiLinkPoint.ID);
    }

    private void SetupPointsForEntryGrid(
      ref int currentID,
      List<SnapPoint> pts,
      out int gridWidth,
      out int gridHeight,
      out UILinkPoint[,] gridPoints)
    {
      List<SnapPoint> pointsByCategoryName = UIBestiaryTest.GetOrderedPointsByCategoryName(pts, "Entries");
      this._entryGrid.GetEntriesToShow(out gridWidth, out gridHeight, out int _);
      gridPoints = new UILinkPoint[gridWidth, gridHeight];
      for (int index1 = 0; index1 < pointsByCategoryName.Count; ++index1)
      {
        int index2 = index1 % gridWidth;
        int index3 = index1 / gridWidth;
        gridPoints[index2, index3] = this.MakeLinkPointFromSnapPoint(currentID++, pointsByCategoryName[index1]);
      }
      for (int index4 = 0; index4 < gridWidth; ++index4)
      {
        for (int index5 = 0; index5 < gridHeight; ++index5)
        {
          UILinkPoint uiLinkPoint = gridPoints[index4, index5];
          if (index4 < gridWidth - 1)
          {
            UILinkPoint rightSide = gridPoints[index4 + 1, index5];
            if (uiLinkPoint != null && rightSide != null)
              this.PairLeftRight(uiLinkPoint, rightSide);
          }
          if (index5 < gridHeight - 1)
          {
            UILinkPoint downSide = gridPoints[index4, index5 + 1];
            if (uiLinkPoint != null && downSide != null)
              this.PairUpDown(uiLinkPoint, downSide);
          }
        }
      }
    }

    private void SetupPointsForFilterGrid(
      ref int currentID,
      List<SnapPoint> pts,
      out int gridWidth,
      out int gridHeight,
      out UILinkPoint[,] gridPoints)
    {
      List<SnapPoint> pointsByCategoryName = UIBestiaryTest.GetOrderedPointsByCategoryName(pts, "Filters");
      this._filteringGrid.GetEntriesToShow(out gridWidth, out gridHeight, out int _);
      gridPoints = new UILinkPoint[gridWidth, gridHeight];
      for (int index1 = 0; index1 < pointsByCategoryName.Count; ++index1)
      {
        int index2 = index1 % gridWidth;
        int index3 = index1 / gridWidth;
        gridPoints[index2, index3] = this.MakeLinkPointFromSnapPoint(currentID++, pointsByCategoryName[index1]);
      }
      for (int index4 = 0; index4 < gridWidth; ++index4)
      {
        for (int index5 = 0; index5 < gridHeight; ++index5)
        {
          UILinkPoint uiLinkPoint = gridPoints[index4, index5];
          if (index4 < gridWidth - 1)
          {
            UILinkPoint rightSide = gridPoints[index4 + 1, index5];
            if (uiLinkPoint != null && rightSide != null)
              this.PairLeftRight(uiLinkPoint, rightSide);
          }
          if (index5 < gridHeight - 1)
          {
            UILinkPoint downSide = gridPoints[index4, index5 + 1];
            if (uiLinkPoint != null && downSide != null)
              this.PairUpDown(uiLinkPoint, downSide);
          }
        }
      }
    }

    private void SetupPointsForSortingGrid(
      ref int currentID,
      List<SnapPoint> pts,
      out int gridWidth,
      out int gridHeight,
      out UILinkPoint[,] gridPoints)
    {
      List<SnapPoint> pointsByCategoryName = UIBestiaryTest.GetOrderedPointsByCategoryName(pts, "SortSteps");
      this._sortingGrid.GetEntriesToShow(out gridWidth, out gridHeight, out int _);
      gridPoints = new UILinkPoint[gridWidth, gridHeight];
      for (int index1 = 0; index1 < pointsByCategoryName.Count; ++index1)
      {
        int index2 = index1 % gridWidth;
        int index3 = index1 / gridWidth;
        gridPoints[index2, index3] = this.MakeLinkPointFromSnapPoint(currentID++, pointsByCategoryName[index1]);
      }
      for (int index4 = 0; index4 < gridWidth; ++index4)
      {
        for (int index5 = 0; index5 < gridHeight; ++index5)
        {
          UILinkPoint uiLinkPoint = gridPoints[index4, index5];
          if (index4 < gridWidth - 1)
          {
            UILinkPoint rightSide = gridPoints[index4 + 1, index5];
            if (uiLinkPoint != null && rightSide != null)
              this.PairLeftRight(uiLinkPoint, rightSide);
          }
          if (index5 < gridHeight - 1)
          {
            UILinkPoint downSide = gridPoints[index4, index5 + 1];
            if (uiLinkPoint != null && downSide != null)
              this.PairUpDown(uiLinkPoint, downSide);
          }
        }
      }
    }

    private static List<SnapPoint> GetOrderedPointsByCategoryName(
      List<SnapPoint> pts,
      string name)
    {
      return pts.Where<SnapPoint>((Func<SnapPoint, bool>) (x => x.Name == name)).OrderBy<SnapPoint, int>((Func<SnapPoint, int>) (x => x.Id)).ToList<SnapPoint>();
    }

    private void PairLeftRight(UILinkPoint leftSide, UILinkPoint rightSide)
    {
      leftSide.Right = rightSide.ID;
      rightSide.Left = leftSide.ID;
    }

    private void PairUpDown(UILinkPoint upSide, UILinkPoint downSide)
    {
      upSide.Down = downSide.ID;
      downSide.Up = upSide.ID;
    }

    private UILinkPoint MakeLinkPointFromSnapPoint(int id, SnapPoint snap)
    {
      UILinkPointNavigator.SetPosition(id, snap.Position);
      UILinkPoint point = UILinkPointNavigator.Points[id];
      point.Unlink();
      return point;
    }

    public override void ScrollWheel(UIScrollWheelEvent evt)
    {
      base.ScrollWheel(evt);
      this._infoSpace.UpdateScrollbar(evt.ScrollWheelValue);
    }

    public void TryMovingPages(int direction) => this._entryGrid.OffsetLibraryByPages(direction);
  }
}
