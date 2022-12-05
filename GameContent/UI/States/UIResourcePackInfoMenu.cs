// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.States.UIResourcePackInfoMenu
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
  public class UIResourcePackInfoMenu : UIState
  {
    private UIResourcePackSelectionMenu _resourceMenu;
    private ResourcePack _pack;
    private UIElement _container;
    private UIList _list;
    private UIScrollbar _scrollbar;
    private bool _isScrollbarAttached;
    private const string _backPointName = "GoBack";
    private UIGamepadHelper _helper;

    public UIResourcePackInfoMenu(UIResourcePackSelectionMenu parent, ResourcePack pack)
    {
      this._resourceMenu = parent;
      this._pack = pack;
      this.BuildPage();
    }

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
        Height = new StyleDimension(52f, 0.0f)
      };
      element4.SetPadding(0.0f);
      element3.Append(element4);
      UIText element5 = new UIText(this._pack.Name, 0.7f, true)
      {
        TextColor = Color.Gold
      };
      element5.HAlign = 0.5f;
      element5.VAlign = 0.0f;
      element4.Append((UIElement) element5);
      UIText uiText1 = new UIText(Language.GetTextValue("UI.Author", (object) this._pack.Author), 0.9f);
      uiText1.HAlign = 0.0f;
      uiText1.VAlign = 1f;
      UIText element6 = uiText1;
      element6.Top.Set(-6f, 0.0f);
      element4.Append((UIElement) element6);
      UIText uiText2 = new UIText(Language.GetTextValue("UI.Version", (object) this._pack.Version.GetFormattedVersion()), 0.9f);
      uiText2.HAlign = 1f;
      uiText2.VAlign = 1f;
      uiText2.TextColor = Color.Silver;
      UIText element7 = uiText2;
      element7.Top.Set(-6f, 0.0f);
      element4.Append((UIElement) element7);
      Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Divider", (AssetRequestMode) 1);
      UIImage uiImage = new UIImage(asset);
      uiImage.Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiImage.Height = StyleDimension.FromPixels((float) asset.Height());
      uiImage.ScaleToFit = true;
      UIImage element8 = uiImage;
      element8.Top.Set(52f, 0.0f);
      element8.SetPadding(6f);
      element3.Append((UIElement) element8);
      UIElement element9 = new UIElement()
      {
        HAlign = 0.5f,
        VAlign = 1f,
        Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f),
        Height = StyleDimension.FromPixelsAndPercent(-74f, 1f)
      };
      element3.Append(element9);
      this._container = element9;
      UIText uiText3 = new UIText(this._pack.Description);
      uiText3.HAlign = 0.5f;
      uiText3.VAlign = 0.0f;
      uiText3.Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiText3.Height = StyleDimension.FromPixelsAndPercent(0.0f, 0.0f);
      uiText3.IsWrapped = true;
      uiText3.WrappedTextBottomPadding = 0.0f;
      UIText uiText4 = uiText3;
      UIList uiList = new UIList();
      uiList.HAlign = 0.5f;
      uiList.VAlign = 0.0f;
      uiList.Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiList.Height = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      uiList.PaddingRight = 20f;
      UIList element10 = uiList;
      element10.ListPadding = 5f;
      element10.Add((UIElement) uiText4);
      element9.Append((UIElement) element10);
      this._list = element10;
      UIScrollbar scrollbar = new UIScrollbar();
      scrollbar.SetView(100f, 1000f);
      scrollbar.Height.Set(0.0f, 1f);
      scrollbar.HAlign = 1f;
      this._scrollbar = scrollbar;
      element10.SetScrollbar(scrollbar);
      UITextPanel<LocalizedText> element11 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
      element11.Width.Set(-10f, 0.5f);
      element11.Height.Set(50f, 0.0f);
      element11.VAlign = 1f;
      element11.HAlign = 0.5f;
      element11.Top.Set(-45f, 0.0f);
      element11.OnMouseOver += new UIElement.MouseEvent(UIResourcePackInfoMenu.FadedMouseOver);
      element11.OnMouseOut += new UIElement.MouseEvent(UIResourcePackInfoMenu.FadedMouseOut);
      element11.OnLeftClick += new UIElement.MouseEvent(this.GoBackClick);
      element11.SetSnapPoint("GoBack", 0);
      element1.Append((UIElement) element11);
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

    private void GoBackClick(UIMouseEvent evt, UIElement listeningElement) => Main.MenuUI.SetState((UIState) this._resourceMenu);

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
