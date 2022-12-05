// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UITextPanel`1
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UITextPanel<T> : UIPanel
  {
    protected T _text;
    protected float _textScale = 1f;
    protected Vector2 _textSize = Vector2.Zero;
    protected bool _isLarge;
    protected Color _color = Color.White;
    protected bool _drawPanel = true;
    public float TextHAlign = 0.5f;
    public bool HideContents;
    private string _asterisks;

    public bool IsLarge => this._isLarge;

    public bool DrawPanel
    {
      get => this._drawPanel;
      set => this._drawPanel = value;
    }

    public float TextScale
    {
      get => this._textScale;
      set => this._textScale = value;
    }

    public Vector2 TextSize => this._textSize;

    public string Text => (object) this._text != null ? this._text.ToString() : "";

    public Color TextColor
    {
      get => this._color;
      set => this._color = value;
    }

    public UITextPanel(T text, float textScale = 1f, bool large = false) => this.SetText(text, textScale, large);

    public override void Recalculate()
    {
      this.SetText(this._text, this._textScale, this._isLarge);
      base.Recalculate();
    }

    public void SetText(T text) => this.SetText(text, this._textScale, this._isLarge);

    public virtual void SetText(T text, float textScale, bool large)
    {
      Vector2 vector2 = new Vector2((large ? FontAssets.DeathText.Value : FontAssets.MouseText.Value).MeasureString(text.ToString()).X, large ? 32f : 16f) * textScale;
      this._text = text;
      this._textScale = textScale;
      this._textSize = vector2;
      this._isLarge = large;
      this.MinWidth.Set(vector2.X + this.PaddingLeft + this.PaddingRight, 0.0f);
      this.MinHeight.Set(vector2.Y + this.PaddingTop + this.PaddingBottom, 0.0f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      if (this._drawPanel)
        base.DrawSelf(spriteBatch);
      this.DrawText(spriteBatch);
    }

    protected void DrawText(SpriteBatch spriteBatch)
    {
      CalculatedStyle innerDimensions = this.GetInnerDimensions();
      Vector2 pos = innerDimensions.Position();
      if (this._isLarge)
        pos.Y -= 10f * this._textScale * this._textScale;
      else
        pos.Y -= 2f * this._textScale;
      pos.X += (innerDimensions.Width - this._textSize.X) * this.TextHAlign;
      string text = this.Text;
      if (this.HideContents)
      {
        if (this._asterisks == null || this._asterisks.Length != text.Length)
          this._asterisks = new string('*', text.Length);
        text = this._asterisks;
      }
      if (this._isLarge)
        Utils.DrawBorderStringBig(spriteBatch, text, pos, this._color, this._textScale);
      else
        Utils.DrawBorderString(spriteBatch, text, pos, this._color, this._textScale);
    }
  }
}
