// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.States.UIWorkshopWorldImport
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
  public class UIWorkshopWorldImport : UIState, IHaveBackButtonCommand
  {
    private UIList _worldList;
    private UITextPanel<LocalizedText> _backPanel;
    private UIPanel _containerPanel;
    private UIScrollbar _scrollbar;
    private bool _isScrollbarAttached;
    private List<Tuple<string, bool>> favoritesCache = new List<Tuple<string, bool>>();
    private UIState _uiStateToGoBackTo;
    public static List<WorldFileData> WorkshopWorldList = new List<WorldFileData>();
    private bool skipDraw;

    public UIWorkshopWorldImport(UIState uiStateToGoBackTo) => this._uiStateToGoBackTo = uiStateToGoBackTo;

    public override void OnInitialize()
    {
      UIElement element1 = new UIElement();
      element1.Width.Set(0.0f, 0.8f);
      element1.MaxWidth.Set(650f, 0.0f);
      element1.Top.Set(220f, 0.0f);
      element1.Height.Set(-220f, 1f);
      element1.HAlign = 0.5f;
      UIPanel element2 = new UIPanel();
      element2.Width.Set(0.0f, 1f);
      element2.Height.Set(-110f, 1f);
      element2.BackgroundColor = new Color(33, 43, 79) * 0.8f;
      element1.Append((UIElement) element2);
      this._containerPanel = element2;
      this._worldList = new UIList();
      this._worldList.Width.Set(0.0f, 1f);
      this._worldList.Height.Set(0.0f, 1f);
      this._worldList.ListPadding = 5f;
      element2.Append((UIElement) this._worldList);
      this._scrollbar = new UIScrollbar();
      this._scrollbar.SetView(100f, 1000f);
      this._scrollbar.Height.Set(0.0f, 1f);
      this._scrollbar.HAlign = 1f;
      this._worldList.SetScrollbar(this._scrollbar);
      UITextPanel<LocalizedText> element3 = new UITextPanel<LocalizedText>(Language.GetText("UI.WorkshopImportWorld"), 0.8f, true);
      element3.HAlign = 0.5f;
      element3.Top.Set(-40f, 0.0f);
      element3.SetPadding(15f);
      element3.BackgroundColor = new Color(73, 94, 171);
      element1.Append((UIElement) element3);
      UITextPanel<LocalizedText> element4 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
      element4.Width.Set(-10f, 0.5f);
      element4.Height.Set(50f, 0.0f);
      element4.VAlign = 1f;
      element4.HAlign = 0.5f;
      element4.Top.Set(-45f, 0.0f);
      element4.OnMouseOver += new UIElement.MouseEvent(this.FadedMouseOver);
      element4.OnMouseOut += new UIElement.MouseEvent(this.FadedMouseOut);
      element4.OnLeftClick += new UIElement.MouseEvent(this.GoBackClick);
      element1.Append((UIElement) element4);
      this._backPanel = element4;
      this.Append(element1);
    }

    public override void Recalculate()
    {
      if (this._scrollbar != null)
      {
        if (this._isScrollbarAttached && !this._scrollbar.CanScroll)
        {
          this._containerPanel.RemoveChild((UIElement) this._scrollbar);
          this._isScrollbarAttached = false;
          this._worldList.Width.Set(0.0f, 1f);
        }
        else if (!this._isScrollbarAttached && this._scrollbar.CanScroll)
        {
          this._containerPanel.Append((UIElement) this._scrollbar);
          this._isScrollbarAttached = true;
          this._worldList.Width.Set(-25f, 1f);
        }
      }
      base.Recalculate();
    }

    private void GoBackClick(UIMouseEvent evt, UIElement listeningElement) => this.HandleBackButtonUsage();

    public void HandleBackButtonUsage()
    {
      SoundEngine.PlaySound(11);
      Main.MenuUI.SetState(this._uiStateToGoBackTo);
    }

    private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12);
      ((UIPanel) evt.Target).BackgroundColor = new Color(73, 94, 171);
      ((UIPanel) evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
    }

    private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
    {
      ((UIPanel) evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.7f;
      ((UIPanel) evt.Target).BorderColor = Color.Black;
    }

    public override void OnActivate()
    {
      Main.LoadWorlds();
      this.UpdateWorkshopWorldList();
      this.UpdateWorldsList();
      if (!PlayerInput.UsingGamepadUI)
        return;
      UILinkPointNavigator.ChangePoint(3000 + (this._worldList.Count == 0 ? 1 : 2));
    }

    public void UpdateWorkshopWorldList()
    {
      UIWorkshopWorldImport.WorkshopWorldList.Clear();
      if (SocialAPI.Workshop == null)
        return;
      foreach (string subscribedWorldPath in SocialAPI.Workshop.GetListOfSubscribedWorldPaths())
      {
        WorldFileData allMetadata = WorldFile.GetAllMetadata(subscribedWorldPath, false);
        if (allMetadata != null)
          UIWorkshopWorldImport.WorkshopWorldList.Add(allMetadata);
        else
          UIWorkshopWorldImport.WorkshopWorldList.Add(WorldFileData.FromInvalidWorld(subscribedWorldPath, false));
      }
    }

    private void UpdateWorldsList()
    {
      this._worldList.Clear();
      IOrderedEnumerable<WorldFileData> orderedEnumerable = new List<WorldFileData>((IEnumerable<WorldFileData>) UIWorkshopWorldImport.WorkshopWorldList).OrderByDescending<WorldFileData, bool>((Func<WorldFileData, bool>) (x => x.IsFavorite)).ThenBy<WorldFileData, string>((Func<WorldFileData, string>) (x => x.Name)).ThenBy<WorldFileData, string>((Func<WorldFileData, string>) (x => x.GetFileName()));
      int num = 0;
      foreach (WorldFileData data in (IEnumerable<WorldFileData>) orderedEnumerable)
        this._worldList.Add((UIElement) new UIWorkshopImportWorldListItem((UIState) this, data, num++));
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      if (this.skipDraw)
      {
        this.skipDraw = false;
      }
      else
      {
        base.Draw(spriteBatch);
        this.SetupGamepadPoints(spriteBatch);
      }
    }

    private void SetupGamepadPoints(SpriteBatch spriteBatch)
    {
      UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
      int num1 = 3000;
      UILinkPointNavigator.SetPosition(num1, this._backPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
      int key1 = num1;
      UILinkPointNavigator.Points[key1].Unlink();
      float num2 = 1f / Main.UIScale;
      Rectangle clippingRectangle = this._containerPanel.GetClippingRectangle(spriteBatch);
      Vector2 minimum = clippingRectangle.TopLeft() * num2;
      Vector2 maximum = clippingRectangle.BottomRight() * num2;
      List<SnapPoint> snapPoints = this.GetSnapPoints();
      for (int index = 0; index < snapPoints.Count; ++index)
      {
        if (!snapPoints[index].Position.Between(minimum, maximum))
        {
          snapPoints.Remove(snapPoints[index]);
          --index;
        }
      }
      SnapPoint[,] snapPointArray = new SnapPoint[this._worldList.Count, 1];
      foreach (SnapPoint snapPoint in snapPoints.Where<SnapPoint>((Func<SnapPoint, bool>) (a => a.Name == "Import")))
        snapPointArray[snapPoint.Id, 0] = snapPoint;
      int num3 = num1 + 2;
      int[] numArray = new int[this._worldList.Count];
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = -1;
      for (int index1 = 0; index1 < 1; ++index1)
      {
        int key2 = -1;
        for (int index2 = 0; index2 < snapPointArray.GetLength(0); ++index2)
        {
          if (snapPointArray[index2, index1] != null)
          {
            UILinkPoint point = UILinkPointNavigator.Points[num3];
            point.Unlink();
            UILinkPointNavigator.SetPosition(num3, snapPointArray[index2, index1].Position);
            if (key2 != -1)
            {
              point.Up = key2;
              UILinkPointNavigator.Points[key2].Down = num3;
            }
            if (numArray[index2] != -1)
            {
              point.Left = numArray[index2];
              UILinkPointNavigator.Points[numArray[index2]].Right = num3;
            }
            point.Down = num1;
            if (index1 == 0)
              UILinkPointNavigator.Points[num1].Up = UILinkPointNavigator.Points[num1 + 1].Up = num3;
            key2 = num3;
            numArray[index2] = num3;
            UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX = num3;
            ++num3;
          }
        }
      }
      if (!PlayerInput.UsingGamepadUI || this._worldList.Count != 0 || UILinkPointNavigator.CurrentPoint <= 3001)
        return;
      UILinkPointNavigator.ChangePoint(3001);
    }
  }
}
