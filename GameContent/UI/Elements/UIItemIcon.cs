// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIItemIcon
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIItemIcon : UIElement
  {
    private Item _item;
    private bool _blackedOut;

    public UIItemIcon(Item item, bool blackedOut)
    {
      this._item = item;
      this.Width.Set(32f, 0.0f);
      this.Height.Set(32f, 0.0f);
      this._blackedOut = blackedOut;
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      CalculatedStyle dimensions = this.GetDimensions();
      double num = (double) ItemSlot.DrawItemIcon(this._item, 31, spriteBatch, dimensions.Center(), this._item.scale, 32f, this._blackedOut ? Color.Black : Color.White);
    }
  }
}
