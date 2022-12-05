// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.States.UIReportsPage
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
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
  public class UIReportsPage : UIState
  {
    private UIState _previousUIState;
    private int _menuIdToGoBackTo;
    private UIElement _container;
    private UIList _list;
    private UIScrollbar _scrollbar;
    private bool _isScrollbarAttached;
    private const string _backPointName = "GoBack";
    private List<IProvideReports> _reporters;
    private UIGamepadHelper _helper;

    public UIReportsPage(
      UIState stateToGoBackTo,
      int menuIdToGoBackTo,
      List<IProvideReports> reporters)
    {
      this._previousUIState = stateToGoBackTo;
      this._menuIdToGoBackTo = menuIdToGoBackTo;
      this._reporters = reporters;
    }

    public override void OnInitialize() => this.BuildPage();

    private void BuildPage()
    {
      this.RemoveAllChildren();
      UIElement element1 = new UIElement();
      element1.Width.Set(0.0f, 0.8f);
      element1.MaxWidth.Set(500f, 0.0f);
      element1.MinWidth.Set(300f, 0.0f);
      element1.Top.Set(230f, 0.0f);
      element1.Height.Set(-element1.Top.Pixels, 1f);
      element1.HAlign = 0.5f;
      this.Append(element1);
      UIPanel element2 = new UIPanel();
      element2.Width.Set(0.0f, 1f);
      element2.Height.Set(-110f, 1f);
      element2.BackgroundColor = new Color(33, 43, 79) * 0.8f;
      element1.Append((UIElement) element2);
      UIElement element3 = new UIElement()
      {
        Width = StyleDimension.Fill,
        Height = StyleDimension.FromPixelsAndPercent(0.0f, 1f)
      };
      element2.Append(element3);
      UIElement element4 = new UIElement()
      {
        Width = new StyleDimension(0.0f, 1f),
        Height = new StyleDimension(28f, 0.0f)
      };
      element4.SetPadding(0.0f);
      element3.Append(element4);
      UIText element5 = new UIText(Language.GetTextValue("UI.ReportsPage"), 0.7f, true);
      element5.HAlign = 0.5f;
      element5.VAlign = 0.0f;
      element4.Append((UIElement) element5);
      UIElement element6 = new UIElement()
      {
        HAlign = 0.5f,
        VAlign = 1f,
        Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f),
        Height = StyleDimension.FromPixelsAndPercent(-40f, 1f),
        Top = new StyleDimension(-2f, 0.0f)
      };
      element3.Append(element6);
      this._container = element6;
      float num = 0.0f;
      UISlicedImage uiSlicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", (AssetRequestMode) 1));
      uiSlicedImage.HAlign = 0.5f;
      uiSlicedImage.VAlign = 1f;
      uiSlicedImage.Width = StyleDimension.FromPixelsAndPercent((float) (-(double) num * 2.0), 1f);
      uiSlicedImage.Left = StyleDimension.FromPixels(-num);
      uiSlicedImage.Height = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiSlicedImage.Top = StyleDimension.FromPixels(2f);
      UISlicedImage element7 = uiSlicedImage;
      element7.SetSliceDepths(10);
      element7.Color = Color.LightGray * 0.5f;
      element6.Append((UIElement) element7);
      UIList uiList1 = new UIList();
      uiList1.HAlign = 0.5f;
      uiList1.VAlign = 0.0f;
      uiList1.Width = StyleDimension.FromPixelsAndPercent(-10f, 1f);
      uiList1.Height = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiList1.PaddingRight = 20f;
      UIList uiList2 = uiList1;
      uiList2.ListPadding = 40f;
      uiList2.ManualSortMethod = new Action<List<UIElement>>(this.ManualIfnoSortingMethod);
      UIElement uiElement = new UIElement();
      uiList2.Add(uiElement);
      this.PopulateLogs(uiList2);
      element6.Append((UIElement) uiList2);
      this._list = uiList2;
      UIScrollbar scrollbar = new UIScrollbar();
      scrollbar.SetView(100f, 1000f);
      scrollbar.Height.Set(0.0f, 1f);
      scrollbar.HAlign = 1f;
      this._scrollbar = scrollbar;
      uiList2.SetScrollbar(scrollbar);
      scrollbar.GoToBottom();
      UITextPanel<LocalizedText> element8 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
      element8.Width.Set(-10f, 0.5f);
      element8.Height.Set(50f, 0.0f);
      element8.VAlign = 1f;
      element8.HAlign = 0.5f;
      element8.Top.Set(-45f, 0.0f);
      element8.OnMouseOver += new UIElement.MouseEvent(UIReportsPage.FadedMouseOver);
      element8.OnMouseOut += new UIElement.MouseEvent(UIReportsPage.FadedMouseOut);
      element8.OnLeftClick += new UIElement.MouseEvent(this.GoBackClick);
      element8.SetSnapPoint("GoBack", 0);
      element1.Append((UIElement) element8);
    }

    private void ManualIfnoSortingMethod(List<UIElement> list)
    {
    }

    private void PopulateLogs(UIList listContents)
    {
      List<IssueReport> list = this._reporters.SelectMany<IProvideReports, IssueReport>((Func<IProvideReports, IEnumerable<IssueReport>>) (reporter => (IEnumerable<IssueReport>) reporter.GetReports())).OrderBy<IssueReport, DateTime>((Func<IssueReport, DateTime>) (report => report.timeReported)).ToList<IssueReport>();
      if (list.Count == 0)
      {
        UIText uiText1 = new UIText(Language.GetTextValue("Workshop.ReportLogsInitialMessage"));
        uiText1.HAlign = 0.0f;
        uiText1.VAlign = 0.0f;
        uiText1.Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
        uiText1.Height = StyleDimension.FromPixelsAndPercent(0.0f, 0.0f);
        uiText1.IsWrapped = true;
        uiText1.WrappedTextBottomPadding = 0.0f;
        uiText1.TextOriginX = 0.5f;
        uiText1.TextColor = Color.Gray;
        UIText uiText2 = uiText1;
        listContents.Add((UIElement) uiText2);
      }
      for (int index = 0; index < list.Count; ++index)
      {
        UIText uiText3 = new UIText(list[index].reportText);
        uiText3.HAlign = 0.0f;
        uiText3.VAlign = 0.0f;
        uiText3.Width = StyleDimension.FromPixelsAndPercent(-10f, 1f);
        uiText3.Height = StyleDimension.FromPixelsAndPercent(0.0f, 0.0f);
        uiText3.IsWrapped = true;
        uiText3.WrappedTextBottomPadding = 0.0f;
        uiText3.TextOriginX = 0.0f;
        UIText uiText4 = uiText3;
        listContents.Add((UIElement) uiText4);
        Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Divider", (AssetRequestMode) 1);
        UIImage uiImage = new UIImage(asset);
        uiImage.Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
        uiImage.Height = StyleDimension.FromPixels((float) asset.Height());
        uiImage.ScaleToFit = true;
        uiImage.VAlign = 1f;
        UIImage element = uiImage;
        uiText4.Append((UIElement) element);
      }
      UIElement uiElement = new UIElement();
      listContents.Add(uiElement);
    }

    public override void Recalculate()
    {
      if (this._scrollbar != null)
      {
        if (this._isScrollbarAttached && !this._scrollbar.CanScroll)
        {
          this._container.RemoveChild((UIElement) this._scrollbar);
          this._isScrollbarAttached = false;
          this._list.Width.Set(0.0f, 1f);
        }
        else if (!this._isScrollbarAttached && this._scrollbar.CanScroll)
        {
          this._container.Append((UIElement) this._scrollbar);
          this._isScrollbarAttached = true;
          this._list.Width.Set(-25f, 1f);
        }
      }
      base.Recalculate();
    }

    private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
    {
      Main.MenuUI.SetState(this._previousUIState);
      SoundEngine.PlaySound(11);
      Main.menuMode = this._menuIdToGoBackTo;
    }

    private static void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12);
      ((UIPanel) evt.Target).BackgroundColor = new Color(73, 94, 171);
      ((UIPanel) evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
    }

    private static void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
    {
      ((UIPanel) evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
      ((UIPanel) evt.Target).BorderColor = Color.Black;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      this.SetupGamepadPoints(spriteBatch);
    }

    private void SetupGamepadPoints(SpriteBatch spriteBatch)
    {
      UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
      int idRangeStartInclusive = 3000;
      int idRangeEndExclusive = idRangeStartInclusive;
      List<SnapPoint> snapPoints = this.GetSnapPoints();
      for (int index = 0; index < snapPoints.Count; ++index)
      {
        SnapPoint snap = snapPoints[index];
        if (snap.Name == "GoBack")
          this._helper.MakeLinkPointFromSnapPoint(idRangeEndExclusive++, snap);
      }
      this._helper.MoveToVisuallyClosestPoint(idRangeStartInclusive, idRangeEndExclusive);
    }
  }
}
