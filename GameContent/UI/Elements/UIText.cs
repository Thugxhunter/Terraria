// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIText
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Elements
{
  public class UIText : UIElement
  {
    private object _text = (object) "";
    private float _textScale = 1f;
    private Vector2 _textSize = Vector2.Zero;
    private bool _isLarge;
    private Color _color = Color.White;
    private Color _shadowColor = Color.Black;
    private bool _isWrapped;
    public bool DynamicallyScaleDownToWidth;
    private string _visibleText;
    private string _lastTextReference;

    public string Text => this._text.ToString();

    public float TextOriginX { get; set; }

    public float TextOriginY { get; set; }

    public float WrappedTextBottomPadding { get; set; }

    public bool IsWrapped
    {
      get => this._isWrapped;
      set
      {
        this._isWrapped = value;
        this.InternalSetText(this._text, this._textScale, this._isLarge);
      }
    }

    public event Action OnInternalTextChange;

    public Color TextColor
    {
      get => this._color;
      set => this._color = value;
    }

    public Color ShadowColor
    {
      get => this._shadowColor;
      set => this._shadowColor = value;
    }

    public UIText(string text, float textScale = 1f, bool large = false)
    {
      this.TextOriginX = 0.5f;
      this.TextOriginY = 0.0f;
      this.IsWrapped = false;
      this.WrappedTextBottomPadding = 20f;
      this.InternalSetText((object) text, textScale, large);
    }

    public UIText(LocalizedText text, float textScale = 1f, bool large = false)
    {
      this.TextOriginX = 0.5f;
      this.TextOriginY = 0.0f;
      this.IsWrapped = false;
      this.WrappedTextBottomPadding = 20f;
      this.InternalSetText((object) text, textScale, large);
    }

    public override void Recalculate()
    {
      this.InternalSetText(this._text, this._textScale, this._isLarge);
      base.Recalculate();
    }

    public void SetText(string text) => this.InternalSetText((object) text, this._textScale, this._isLarge);

    public void SetText(LocalizedText text) => this.InternalSetText((object) text, this._textScale, this._isLarge);

    public void SetText(string text, float textScale, bool large) => this.InternalSetText((object) text, textScale, large);

    public void SetText(LocalizedText text, float textScale, bool large) => this.InternalSetText((object) text, textScale, large);

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      base.DrawSelf(spriteBatch);
      this.VerifyTextState();
      CalculatedStyle innerDimensions = this.GetInnerDimensions();
      Vector2 position = innerDimensions.Position();
      if (this._isLarge)
        position.Y -= 10f * this._textScale;
      else
        position.Y -= 2f * this._textScale;
      position.X += (innerDimensions.Width - this._textSize.X) * this.TextOriginX;
      position.Y += (innerDimensions.Height - this._textSize.Y) * this.TextOriginY;
      float textScale = this._textScale;
      if (this.DynamicallyScaleDownToWidth && (double) this._textSize.X > (double) innerDimensions.Width)
        textScale *= innerDimensions.Width / this._textSize.X;
      DynamicSpriteFont font = (this._isLarge ? FontAssets.DeathText : FontAssets.MouseText).Value;
      Vector2 vector2 = font.MeasureString(this._visibleText);
      Color baseColor = this._shadowColor * ((float) this._color.A / (float) byte.MaxValue);
      Vector2 origin = new Vector2(0.0f, 0.0f) * vector2;
      Vector2 baseScale = new Vector2(textScale);
      TextSnippet[] array = ChatManager.ParseMessage(this._visibleText, this._color).ToArray();
      ChatManager.ConvertNormalSnippets(array);
      ChatManager.DrawColorCodedStringShadow(spriteBatch, font, array, position, baseColor, 0.0f, origin, baseScale, spread: 1.5f);
      ChatManager.DrawColorCodedString(spriteBatch, font, array, position, Color.White, 0.0f, origin, baseScale, out int _, -1f);
    }

    private void VerifyTextState()
    {
      if ((object) this._lastTextReference == (object) this.Text)
        return;
      this.InternalSetText(this._text, this._textScale, this._isLarge);
    }

    private void InternalSetText(object text, float textScale, bool large)
    {
      DynamicSpriteFont dynamicSpriteFont = large ? FontAssets.DeathText.Value : FontAssets.MouseText.Value;
      this._text = text;
      this._isLarge = large;
      this._textScale = textScale;
      this._lastTextReference = this._text.ToString();
      this._visibleText = !this.IsWrapped ? this._lastTextReference : dynamicSpriteFont.CreateWrappedText(this._lastTextReference, this.GetInnerDimensions().Width / this._textScale);
      Vector2 vector2_1 = dynamicSpriteFont.MeasureString(this._visibleText);
      Vector2 vector2_2 = !this.IsWrapped ? new Vector2(vector2_1.X, large ? 32f : 16f) * textScale : new Vector2(vector2_1.X, vector2_1.Y + this.WrappedTextBottomPadding) * textScale;
      this._textSize = vector2_2;
      this.MinWidth.Set(vector2_2.X + this.PaddingLeft + this.PaddingRight, 0.0f);
      this.MinHeight.Set(vector2_2.Y + this.PaddingTop + this.PaddingBottom, 0.0f);
      if (this.OnInternalTextChange == null)
        return;
      this.OnInternalTextChange();
    }
  }
}
