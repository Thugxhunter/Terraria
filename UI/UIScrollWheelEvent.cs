// Decompiled with JetBrains decompiler
// Type: Terraria.UI.UIScrollWheelEvent
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.UI
{
  public class UIScrollWheelEvent : UIMouseEvent
  {
    public readonly int ScrollWheelValue;

    public UIScrollWheelEvent(UIElement target, Vector2 mousePosition, int scrollWheelValue)
      : base(target, mousePosition)
    {
      this.ScrollWheelValue = scrollWheelValue;
    }
  }
}
