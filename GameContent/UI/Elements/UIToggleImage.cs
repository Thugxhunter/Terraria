// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIToggleImage
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIToggleImage : UIElement
  {
    private Asset<Texture2D> _onTexture;
    private Asset<Texture2D> _offTexture;
    private int _drawWidth;
    private int _drawHeight;
    private Point _onTextureOffset = Point.Zero;
    private Point _offTextureOffset = Point.Zero;
    private bool _isOn;

    public bool IsOn => this._isOn;

    public UIToggleImage(
      Asset<Texture2D> texture,
      int width,
      int height,
      Point onTextureOffset,
      Point offTextureOffset)
    {
      this._onTexture = texture;
      this._offTexture = texture;
      this._offTextureOffset = offTextureOffset;
      this._onTextureOffset = onTextureOffset;
      this._drawWidth = width;
      this._drawHeight = height;
      this.Width.Set((float) width, 0.0f);
      this.Height.Set((float) height, 0.0f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      CalculatedStyle dimensions = this.GetDimensions();
      Texture2D texture;
      Point point;
      if (this._isOn)
      {
        texture = this._onTexture.Value;
        point = this._onTextureOffset;
      }
      else
      {
        texture = this._offTexture.Value;
        point = this._offTextureOffset;
      }
      Color color = this.IsMouseHovering ? Color.White : Color.Silver;
      spriteBatch.Draw(texture, new Rectangle((int) dimensions.X, (int) dimensions.Y, this._drawWidth, this._drawHeight), new Rectangle?(new Rectangle(point.X, point.Y, this._drawWidth, this._drawHeight)), color);
    }

    public override void LeftClick(UIMouseEvent evt)
    {
      this.Toggle();
      base.LeftClick(evt);
    }

    public void SetState(bool value) => this._isOn = value;

    public void Toggle() => this._isOn = !this._isOn;
  }
}
