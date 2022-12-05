// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.AWorldListItem
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public abstract class AWorldListItem : UIPanel
  {
    protected WorldFileData _data;
    protected int _glitchFrameCounter;
    protected int _glitchFrame;
    protected int _glitchVariation;

    private void UpdateGlitchAnimation(UIElement affectedElement)
    {
      int glitchFrame = this._glitchFrame;
      int minValue = 3;
      int num = 3;
      if (this._glitchFrame == 0)
      {
        minValue = 15;
        num = 120;
      }
      if (++this._glitchFrameCounter >= Main.rand.Next(minValue, num + 1))
      {
        this._glitchFrameCounter = 0;
        this._glitchFrame = (this._glitchFrame + 1) % 16;
        if ((this._glitchFrame == 4 || this._glitchFrame == 8 ? 1 : (this._glitchFrame == 12 ? 1 : 0)) != 0 && Main.rand.Next(3) == 0)
          this._glitchVariation = Main.rand.Next(7);
      }
      (affectedElement as UIImageFramed).SetFrame(7, 16, this._glitchVariation, this._glitchFrame, 0, 0);
    }

    protected void GetDifficulty(out string expertText, out Color gameModeColor)
    {
      expertText = "";
      gameModeColor = Color.White;
      if (this._data.GameMode == 3)
      {
        expertText = Language.GetTextValue("UI.Creative");
        gameModeColor = Main.creativeModeColor;
      }
      else
      {
        int num = 1;
        switch (this._data.GameMode)
        {
          case 1:
            num = 2;
            break;
          case 2:
            num = 3;
            break;
        }
        if (this._data.ForTheWorthy)
          ++num;
        switch (num)
        {
          case 2:
            expertText = Language.GetTextValue("UI.Expert");
            gameModeColor = Main.mcColor;
            break;
          case 3:
            expertText = Language.GetTextValue("UI.Master");
            gameModeColor = Main.hcColor;
            break;
          case 4:
            expertText = Language.GetTextValue("UI.Legendary");
            gameModeColor = Main.legendaryModeColor;
            break;
          default:
            expertText = Language.GetTextValue("UI.Normal");
            break;
        }
      }
    }

    protected Asset<Texture2D> GetIcon()
    {
      if (this._data.ZenithWorld)
        return Main.Assets.Request<Texture2D>("Images/UI/Icon" + (this._data.IsHardMode ? "Hallow" : "") + "Everything", (AssetRequestMode) 1);
      if (this._data.DrunkWorld)
        return Main.Assets.Request<Texture2D>("Images/UI/Icon" + (this._data.IsHardMode ? "Hallow" : "") + "CorruptionCrimson", (AssetRequestMode) 1);
      if (this._data.ForTheWorthy)
        return this.GetSeedIcon("FTW");
      if (this._data.NotTheBees)
        return this.GetSeedIcon("NotTheBees");
      if (this._data.Anniversary)
        return this.GetSeedIcon("Anniversary");
      if (this._data.DontStarve)
        return this.GetSeedIcon("DontStarve");
      if (this._data.RemixWorld)
        return this.GetSeedIcon("Remix");
      return this._data.NoTrapsWorld ? this.GetSeedIcon("Traps") : Main.Assets.Request<Texture2D>("Images/UI/Icon" + (this._data.IsHardMode ? "Hallow" : "") + (this._data.HasCorruption ? "Corruption" : "Crimson"), (AssetRequestMode) 1);
    }

    protected UIElement GetIconElement()
    {
      if (this._data.DrunkWorld && this._data.RemixWorld)
      {
        Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/IconEverythingAnimated", (AssetRequestMode) 1);
        UIImageFramed iconElement = new UIImageFramed(asset, asset.Frame(7, 16));
        iconElement.Left = new StyleDimension(4f, 0.0f);
        iconElement.OnUpdate += new UIElement.ElementEvent(this.UpdateGlitchAnimation);
        return (UIElement) iconElement;
      }
      UIImage iconElement1 = new UIImage(this.GetIcon());
      iconElement1.Left = new StyleDimension(4f, 0.0f);
      return (UIElement) iconElement1;
    }

    private Asset<Texture2D> GetSeedIcon(string seed) => Main.Assets.Request<Texture2D>("Images/UI/Icon" + (this._data.IsHardMode ? "Hallow" : "") + (this._data.HasCorruption ? "Corruption" : "Crimson") + seed, (AssetRequestMode) 1);
  }
}
