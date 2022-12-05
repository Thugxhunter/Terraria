// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIDifficultyButton
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIDifficultyButton : UIElement
  {
    private readonly Player _player;
    private readonly Asset<Texture2D> _BasePanelTexture;
    private readonly Asset<Texture2D> _selectedBorderTexture;
    private readonly Asset<Texture2D> _hoveredBorderTexture;
    private readonly byte _difficulty;
    private readonly Color _color;
    private bool _hovered;
    private bool _soundedHover;

    public UIDifficultyButton(
      Player player,
      LocalizedText title,
      LocalizedText description,
      byte difficulty,
      Color color)
    {
      this._player = player;
      this._difficulty = difficulty;
      this.Width = StyleDimension.FromPixels(44f);
      this.Height = StyleDimension.FromPixels(110f);
      this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale", (AssetRequestMode) 1);
      this._selectedBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", (AssetRequestMode) 1);
      this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder", (AssetRequestMode) 1);
      this._color = color;
      UIText element = new UIText(title, 0.9f);
      element.HAlign = 0.5f;
      element.VAlign = 0.0f;
      element.Width = StyleDimension.FromPixelsAndPercent(-10f, 1f);
      element.Top = StyleDimension.FromPixels(5f);
      this.Append((UIElement) element);
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
      int num1 = 7;
      if ((double) dimensions.Height < 30.0)
        num1 = 5;
      int num2 = 10;
      int num3 = 10;
      int num4 = (int) this._difficulty == (int) this._player.difficulty ? 1 : 0;
      Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int) dimensions.X, (int) dimensions.Y, (int) dimensions.Width, (int) dimensions.Height, num2, num2, num3, num3, Color.Lerp(Color.Black, this._color, 0.8f) * 0.5f);
      if (num4 != 0)
        Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int) dimensions.X + num1, (int) dimensions.Y + num1 - 2, (int) dimensions.Width - num1 * 2, (int) dimensions.Height - num1 * 2, num2, num2, num3, num3, Color.Lerp(this._color, Color.White, 0.7f) * 0.5f);
      if (!this._hovered)
        return;
      Utils.DrawSplicedPanel(spriteBatch, this._hoveredBorderTexture.Value, (int) dimensions.X, (int) dimensions.Y, (int) dimensions.Width, (int) dimensions.Height, num2, num2, num3, num3, Color.White);
    }

    public override void LeftMouseDown(UIMouseEvent evt)
    {
      this._player.difficulty = this._difficulty;
      SoundEngine.PlaySound(12);
      base.LeftMouseDown(evt);
    }

    public override void MouseOver(UIMouseEvent evt)
    {
      base.MouseOver(evt);
      this._hovered = true;
    }

    public override void MouseOut(UIMouseEvent evt)
    {
      base.MouseOut(evt);
      this._hovered = false;
    }
  }
}
