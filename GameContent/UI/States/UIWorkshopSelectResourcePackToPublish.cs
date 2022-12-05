// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.States.UIWorkshopSelectResourcePackToPublish
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
using Terraria.Initializers;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
  public class UIWorkshopSelectResourcePackToPublish : UIState, IHaveBackButtonCommand
  {
    private UIList _entryList;
    private UITextPanel<LocalizedText> _backPanel;
    private UIPanel _containerPanel;
    private UIScrollbar _scrollbar;
    private bool _isScrollbarAttached;
    private UIState _menuToGoBackTo;
    private List<ResourcePack> _entries = new List<ResourcePack>();
    private bool skipDraw;

    public UIWorkshopSelectResourcePackToPublish(UIState menuToGoBackTo) => this._menuToGoBackTo = menuToGoBackTo;

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
      this._entryList = new UIList();
      this._entryList.Width.Set(0.0f, 1f);
      this._entryList.Height.Set(0.0f, 1f);
      this._entryList.ListPadding = 5f;
      element2.Append((UIElement) this._entryList);
      this._scrollbar = new UIScrollbar();
      this._scrollbar.SetView(100f, 1000f);
      this._scrollbar.Height.Set(0.0f, 1f);
      this._scrollbar.HAlign = 1f;
      this._entryList.SetScrollbar(this._scrollbar);
      UITextPanel<LocalizedText> element3 = new UITextPanel<LocalizedText>(Language.GetText("UI.WorkshopSelectResourcePackToPublishMenuTitle"), 0.8f, true);
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
          this._entryList.Width.Set(0.0f, 1f);
        }
        else if (!this._isScrollbarAttached && this._scrollbar.CanScroll)
        {
          this._containerPanel.Append((UIElement) this._scrollbar);
          this._isScrollbarAttached = true;
          this._entryList.Width.Set(-25f, 1f);
        }
      }
      base.Recalculate();
    }

    private void GoBackClick(UIMouseEvent evt, UIElement listeningElement) => this.HandleBackButtonUsage();

    public void HandleBackButtonUsage()
    {
      SoundEngine.PlaySound(11);
      Main.MenuUI.SetState(this._menuToGoBackTo);
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
      this.PopulateEntries();
      if (!PlayerInput.UsingGamepadUI)
        return;
      UILinkPointNavigator.ChangePoint(3000 + (this._entryList.Count == 0 ? 0 : 1));
    }

    public void PopulateEntries()
    {
      this._entries.Clear();
      this._entries.AddRange((IEnumerable<ResourcePack>) AssetInitializer.CreatePublishableResourcePacksList((IServiceProvider) Main.instance.Services).AllPacks.Where<ResourcePack>((Func<ResourcePack, bool>) (x => x.Branding == ResourcePack.BrandingType.None)).OrderBy<ResourcePack, bool>((Func<ResourcePack, bool>) (x => x.IsCompressed)));
      this._entryList.Clear();
      int num = 0;
      foreach (ResourcePack entry in this._entries)
        this._entryList.Add((UIElement) new UIWorkshopPublishResourcePackListItem((UIState) this, entry, num++, !entry.IsCompressed));
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
      UILinkPoint point1 = UILinkPointNavigator.Points[key1];
      point1.Unlink();
      point1.Right = key1;
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
      int length = 1;
      SnapPoint[,] snapPointArray = new SnapPoint[this._entryList.Count, length];
      foreach (SnapPoint snapPoint in snapPoints.Where<SnapPoint>((Func<SnapPoint, bool>) (a => a.Name == "Publish")))
        snapPointArray[snapPoint.Id, 0] = snapPoint;
      int num3 = num1 + 1;
      int[] numArray = new int[this._entryList.Count];
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = -1;
      for (int index1 = 0; index1 < length; ++index1)
      {
        int key2 = -1;
        for (int index2 = 0; index2 < snapPointArray.GetLength(0); ++index2)
        {
          if (snapPointArray[index2, index1] != null)
          {
            UILinkPoint point2 = UILinkPointNavigator.Points[num3];
            point2.Unlink();
            UILinkPointNavigator.SetPosition(num3, snapPointArray[index2, index1].Position);
            if (key2 != -1)
            {
              point2.Up = key2;
              UILinkPointNavigator.Points[key2].Down = num3;
            }
            if (numArray[index2] != -1)
            {
              point2.Left = numArray[index2];
              UILinkPointNavigator.Points[numArray[index2]].Right = num3;
            }
            point2.Down = num1;
            if (index1 == 0)
              UILinkPointNavigator.Points[num1].Up = UILinkPointNavigator.Points[num1 + 1].Up = num3;
            key2 = num3;
            numArray[index2] = num3;
            UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX = num3;
            ++num3;
          }
        }
      }
      if (!PlayerInput.UsingGamepadUI || this._entryList.Count != 0 || UILinkPointNavigator.CurrentPoint <= 3000)
        return;
      UILinkPointNavigator.ChangePoint(3000);
    }
  }
}
