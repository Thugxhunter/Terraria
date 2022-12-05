// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIIconTextButton
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIIconTextButton : UIElement
  {
    private readonly Asset<Texture2D> _BasePanelTexture;
    private readonly Asset<Texture2D> _hoveredTexture;
    private readonly Asset<Texture2D> _iconTexture;
    private Color _color;
    private Color _hoverColor;
    public float FadeFromBlack = 1f;
    private float _whiteLerp = 0.7f;
    private float _opacity = 0.7f;
    private bool _hovered;
    private bool _soundedHover;
    private UIText _title;

    public UIIconTextButton(
      LocalizedText title,
      Color textColor,
      string iconTexturePath,
      float textSize = 1f,
      float titleAlignmentX = 0.5f,
      float titleWidthReduction = 10f)
    {
      this.Width = StyleDimension.FromPixels(44f);
      this.Height = StyleDimension.FromPixels(34f);
      this._hoverColor = Color.White;
      this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale", (AssetRequestMode) 1);
      this._hoveredTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", (AssetRequestMode) 1);
      if (iconTexturePath != null)
        this._iconTexture = Main.Assets.Request<Texture2D>(iconTexturePath, (AssetRequestMode) 1);
      this.SetColor(Color.Lerp(Color.Black, Colors.InventoryDefaultColor, this.FadeFromBlack), 1f);
      if (title == null)
        return;
      this.SetText(title, textSize, textColor);
    }

    public void SetText(LocalizedText text, float textSize, Color color)
    {
      if (this._title != null)
        this._title.Remove();
      UIText uiText = new UIText(text, textSize);
      uiText.HAlign = 0.0f;
      uiText.VAlign = 0.5f;
      uiText.Top = StyleDimension.FromPixels(0.0f);
      uiText.Left = StyleDimension.FromPixelsAndPercent(10f, 0.0f);
      uiText.IgnoresMouseInteraction = true;
      UIText element = uiText;
      element.TextColor = color;
      this.Append((UIElement) element);
      this._title = element;
      if (this._iconTexture == null)
        return;
      this.Width.Set((float) ((double) this._title.GetDimensions().Width + (double) this._iconTexture.Width() + 26.0), 0.0f);
      this.Height.Set(Math.Max(this._title.GetDimensions().Height, (float) this._iconTexture.Height()) + 16f, 0.0f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      if (this._hovered)
      {
        if (!this._soundedHover)
          SoundEngine.PlaySound(12);
        this._soundedHover = true;
      }
      else
        this._soundedHover = false;
      CalculatedStyle dimensions = this.GetDimensions();
      Color color1 = this._color;
      float opacity = this._opacity;
      Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int) dimensions.X, (int) dimensions.Y, (int) dimensions.Width, (int) dimensions.Height, 10, 10, 10, 10, Color.Lerp(Color.Black, color1, this.FadeFromBlack) * opacity);
      if (this._iconTexture == null)
        return;
      Color color2 = Color.Lerp(color1, Color.White, this._whiteLerp) * opacity;
      spriteBatch.Draw(this._iconTexture.Value, new Vector2((float) ((double) dimensions.X + (double) dimensions.Width - (double) this._iconTexture.Width() - 5.0), dimensions.Center().Y - (float) (this._iconTexture.Height() / 2)), color2);
    }

    public override void LeftMouseDown(UIMouseEvent evt)
    {
      SoundEngine.PlaySound(12);
      base.LeftMouseDown(evt);
    }

    public override void MouseOver(UIMouseEvent evt)
    {
      base.MouseOver(evt);
      this.SetColor(Color.Lerp(Colors.InventoryDefaultColor, Color.White, this._whiteLerp), 0.7f);
      this._hovered = true;
    }

    public override void MouseOut(UIMouseEvent evt)
    {
      base.MouseOut(evt);
      this.SetColor(Color.Lerp(Color.Black, Colors.InventoryDefaultColor, this.FadeFromBlack), 1f);
      this._hovered = false;
    }

    public void SetColor(Color color, float opacity)
    {
      this._color = color;
      this._opacity = opacity;
    }

    public void SetHoverColor(Color color) => this._hoverColor = color;
  }
}
