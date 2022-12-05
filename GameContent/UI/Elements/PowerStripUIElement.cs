// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.PowerStripUIElement
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class PowerStripUIElement : UIElement
  {
    private List<UIElement> _buttonsBySorting;
    private string _gamepadPointGroupname;

    public PowerStripUIElement(string gamepadGroupName, List<UIElement> buttons)
    {
      this._buttonsBySorting = new List<UIElement>((IEnumerable<UIElement>) buttons);
      this._gamepadPointGroupname = gamepadGroupName;
      int count = buttons.Count;
      int num1 = 4;
      int num2 = 40;
      int num3 = 40;
      int num4 = num3 + num1;
      UIPanel uiPanel = new UIPanel();
      uiPanel.Width = new StyleDimension((float) (num2 + num1 * 2), 0.0f);
      uiPanel.Height = new StyleDimension((float) (num3 * count + num1 * (1 + count)), 0.0f);
      UIPanel element = uiPanel;
      this.SetPadding(0.0f);
      this.Width = element.Width;
      this.Height = element.Height;
      element.BorderColor = new Color(89, 116, 213, (int) byte.MaxValue) * 0.9f;
      element.BackgroundColor = new Color(73, 94, 171) * 0.9f;
      element.SetPadding(0.0f);
      this.Append((UIElement) element);
      for (int index = 0; index < count; ++index)
      {
        UIElement button = buttons[index];
        button.HAlign = 0.5f;
        button.Top = new StyleDimension((float) (num1 + num4 * index), 0.0f);
        button.SetSnapPoint(this._gamepadPointGroupname, index);
        element.Append(button);
        this._buttonsBySorting.Add(button);
      }
    }
  }
}
