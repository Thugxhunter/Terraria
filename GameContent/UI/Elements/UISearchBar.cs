// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UISearchBar
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UISearchBar : UIElement
  {
    private readonly LocalizedText _textToShowWhenEmpty;
    private UITextBox _text;
    private string actualContents;
    private float _textScale;
    private bool isWritingText;

    public event Action<string> OnContentsChanged;

    public event Action OnStartTakingInput;

    public event Action OnEndTakingInput;

    public event Action OnCanceledTakingInput;

    public event Action OnNeedingVirtualKeyboard;

    public bool HasContents => !string.IsNullOrWhiteSpace(this.actualContents);

    public bool IsWritingText => this.isWritingText;

    public UISearchBar(LocalizedText emptyContentText, float scale)
    {
      this._textToShowWhenEmpty = emptyContentText;
      this._textScale = scale;
      UITextBox uiTextBox = new UITextBox("", scale);
      uiTextBox.HAlign = 0.0f;
      uiTextBox.VAlign = 0.5f;
      uiTextBox.BackgroundColor = Color.Transparent;
      uiTextBox.BorderColor = Color.Transparent;
      uiTextBox.Width = new StyleDimension(0.0f, 1f);
      uiTextBox.Height = new StyleDimension(0.0f, 1f);
      uiTextBox.TextHAlign = 0.0f;
      uiTextBox.ShowInputTicker = false;
      UITextBox element = uiTextBox;
      element.SetTextMaxLength(50);
      this.Append((UIElement) element);
      this._text = element;
    }

    public void SetContents(string contents, bool forced = false)
    {
      if (this.actualContents == contents && !forced)
        return;
      this.actualContents = contents;
      if (string.IsNullOrEmpty(this.actualContents))
      {
        this._text.TextColor = Color.Gray;
        this._text.SetText(this._textToShowWhenEmpty.Value, this._textScale, false);
      }
      else
      {
        this._text.TextColor = Color.White;
        this._text.SetText(this.actualContents);
        this.actualContents = this._text.Text;
      }
      this.TrimDisplayIfOverElementDimensions(0);
      if (this.OnContentsChanged == null)
        return;
      this.OnContentsChanged(contents);
    }

    public void TrimDisplayIfOverElementDimensions(int padding)
    {
      CalculatedStyle dimensions1 = this.GetDimensions();
      if ((double) dimensions1.Width == 0.0 && (double) dimensions1.Height == 0.0)
        return;
      Point point1 = new Point((int) dimensions1.X, (int) dimensions1.Y);
      Point point2 = new Point(point1.X + (int) dimensions1.Width, point1.Y + (int) dimensions1.Height);
      Rectangle rectangle1 = new Rectangle(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y);
      CalculatedStyle dimensions2 = this._text.GetDimensions();
      Point point3 = new Point((int) dimensions2.X, (int) dimensions2.Y);
      Point point4 = new Point(point3.X + (int) this._text.MinWidth.Pixels, point3.Y + (int) this._text.MinHeight.Pixels);
      Rectangle rectangle2 = new Rectangle(point3.X, point3.Y, point4.X - point3.X, point4.Y - point3.Y);
      int num = 0;
      while (rectangle2.Right > rectangle1.Right - padding && this._text.Text.Length > 0)
      {
        this._text.SetText(this._text.Text.Substring(0, this._text.Text.Length - 1));
        ++num;
        this.RecalculateChildren();
        CalculatedStyle dimensions3 = this._text.GetDimensions();
        point3 = new Point((int) dimensions3.X, (int) dimensions3.Y);
        point4 = new Point(point3.X + (int) this._text.MinWidth.Pixels, point3.Y + (int) this._text.MinHeight.Pixels);
        rectangle2 = new Rectangle(point3.X, point3.Y, point4.X - point3.X, point4.Y - point3.Y);
        this.actualContents = this._text.Text;
      }
    }

    public override void LeftMouseDown(UIMouseEvent evt) => base.LeftMouseDown(evt);

    public override void MouseOver(UIMouseEvent evt)
    {
      base.MouseOver(evt);
      SoundEngine.PlaySound(12);
    }

    public override void Update(GameTime gameTime)
    {
      if (this.isWritingText)
      {
        if (this.NeedsVirtualkeyboard())
        {
          if (this.OnNeedingVirtualKeyboard == null)
            return;
          this.OnNeedingVirtualKeyboard();
          return;
        }
        PlayerInput.WritingText = true;
        Main.CurrentInputTextTakerOverride = (object) this;
      }
      base.Update(gameTime);
    }

    private bool NeedsVirtualkeyboard() => PlayerInput.SettingsForUI.ShowGamepadHints;

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      base.DrawSelf(spriteBatch);
      if (!this.isWritingText)
        return;
      PlayerInput.WritingText = true;
      Main.instance.HandleIME();
      Vector2 position = new Vector2((float) (Main.screenWidth / 2), (float) (this._text.GetDimensions().ToRectangle().Bottom + 32));
      Main.instance.DrawWindowsIMEPanel(position, 0.5f);
      string inputText = Main.GetInputText(this.actualContents);
      if (Main.inputTextEnter)
        this.ToggleTakingText();
      else if (Main.inputTextEscape)
      {
        this.ToggleTakingText();
        if (this.OnCanceledTakingInput != null)
          this.OnCanceledTakingInput();
      }
      this.SetContents(inputText);
      position = new Vector2((float) (Main.screenWidth / 2), (float) (this._text.GetDimensions().ToRectangle().Bottom + 32));
      Main.instance.DrawWindowsIMEPanel(position, 0.5f);
    }

    public void ToggleTakingText()
    {
      this.isWritingText = !this.isWritingText;
      this._text.ShowInputTicker = this.isWritingText;
      Main.clrInput();
      if (this.isWritingText)
      {
        if (this.OnStartTakingInput == null)
          return;
        this.OnStartTakingInput();
      }
      else
      {
        if (this.OnEndTakingInput == null)
          return;
        this.OnEndTakingInput();
      }
    }
  }
}
