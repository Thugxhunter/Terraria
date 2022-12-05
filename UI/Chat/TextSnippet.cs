// Decompiled with JetBrains decompiler
// Type: Terraria.UI.Chat.TextSnippet
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;

namespace Terraria.UI.Chat
{
  public class TextSnippet
  {
    public string Text;
    public string TextOriginal;
    public Color Color = Color.White;
    public float Scale = 1f;
    public bool CheckForHover;
    public bool DeleteWhole;

    public TextSnippet(string text = "")
    {
      this.Text = text;
      this.TextOriginal = text;
    }

    public TextSnippet(string text, Color color, float scale = 1f)
    {
      this.Text = text;
      this.TextOriginal = text;
      this.Color = color;
      this.Scale = scale;
    }

    public virtual void Update()
    {
    }

    public virtual void OnHover()
    {
    }

    public virtual void OnClick()
    {
    }

    public virtual Color GetVisibleColor() => ChatManager.WaveColor(this.Color);

    public virtual bool UniqueDraw(
      bool justCheckingString,
      out Vector2 size,
      SpriteBatch spriteBatch,
      Vector2 position = default (Vector2),
      Color color = default (Color),
      float scale = 1f)
    {
      size = Vector2.Zero;
      return false;
    }

    public virtual TextSnippet CopyMorph(string newText)
    {
      TextSnippet textSnippet = (TextSnippet) this.MemberwiseClone();
      textSnippet.Text = newText;
      return textSnippet;
    }

    public virtual float GetStringLength(DynamicSpriteFont font) => font.MeasureString(this.Text).X * this.Scale;

    public override string ToString() => "Text: " + this.Text + " | OriginalText: " + this.TextOriginal;
  }
}
