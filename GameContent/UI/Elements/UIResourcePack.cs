// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIResourcePack
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
  public class UIResourcePack : UIPanel
  {
    private const int PANEL_PADDING = 5;
    private const int ICON_SIZE = 64;
    private const int ICON_BORDER_PADDING = 4;
    private const int HEIGHT_FLUFF = 10;
    private const float HEIGHT = 102f;
    private const float MIN_WIDTH = 102f;
    private static readonly Color DefaultBackgroundColor = new Color(26, 40, 89) * 0.8f;
    private static readonly Color DefaultBorderColor = new Color(13, 20, 44) * 0.8f;
    private static readonly Color HoverBackgroundColor = new Color(46, 60, 119);
    private static readonly Color HoverBorderColor = new Color(20, 30, 56);
    public readonly ResourcePack ResourcePack;
    private readonly Asset<Texture2D> _iconBorderTexture;

    public int Order { get; set; }

    public UIElement ContentPanel { get; private set; }

    public UIResourcePack(ResourcePack pack, int order)
    {
      this.ResourcePack = pack;
      this.Order = order;
      this.BackgroundColor = UIResourcePack.DefaultBackgroundColor;
      this.BorderColor = UIResourcePack.DefaultBorderColor;
      this.Height = StyleDimension.FromPixels(102f);
      this.MinHeight = this.Height;
      this.MaxHeight = this.Height;
      this.MinWidth = StyleDimension.FromPixels(102f);
      this.Width = StyleDimension.FromPercent(1f);
      this.SetPadding(5f);
      this._iconBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders", (AssetRequestMode) 1);
      this.OverflowHidden = true;
      this.BuildChildren();
    }

    private void BuildChildren()
    {
      StyleDimension styleDimension1 = StyleDimension.FromPixels(77f);
      StyleDimension styleDimension2 = StyleDimension.FromPixels(4f);
      UIText uiText1 = new UIText(this.ResourcePack.Name);
      uiText1.Left = styleDimension1;
      uiText1.Top = styleDimension2;
      UIText element1 = uiText1;
      this.Append((UIElement) element1);
      styleDimension2.Pixels += element1.GetOuterDimensions().Height + 6f;
      UIText uiText2 = new UIText(Language.GetTextValue("UI.Author", (object) this.ResourcePack.Author), 0.7f);
      uiText2.Left = styleDimension1;
      uiText2.Top = styleDimension2;
      UIText element2 = uiText2;
      this.Append((UIElement) element2);
      styleDimension2.Pixels += element2.GetOuterDimensions().Height + 10f;
      Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Divider", (AssetRequestMode) 1);
      UIImage uiImage = new UIImage(asset);
      uiImage.Left = StyleDimension.FromPixels(72f);
      uiImage.Top = styleDimension2;
      uiImage.Height = StyleDimension.FromPixels((float) asset.Height());
      uiImage.Width = StyleDimension.FromPixelsAndPercent(-80f, 1f);
      uiImage.ScaleToFit = true;
      UIImage element3 = uiImage;
      this.Recalculate();
      this.Append((UIElement) element3);
      styleDimension2.Pixels += element3.GetOuterDimensions().Height + 5f;
      UIElement element4 = new UIElement()
      {
        Left = styleDimension1,
        Top = styleDimension2,
        Height = StyleDimension.FromPixels(92f - styleDimension2.Pixels),
        Width = StyleDimension.FromPixelsAndPercent(-styleDimension1.Pixels, 1f)
      };
      this.Append(element4);
      this.ContentPanel = element4;
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      base.DrawSelf(spriteBatch);
      this.DrawIcon(spriteBatch);
      if (this.ResourcePack.Branding != ResourcePack.BrandingType.SteamWorkshop)
        return;
      Asset<Texture2D> asset = TextureAssets.Extra[243];
      spriteBatch.Draw(asset.Value, new Vector2((float) ((double) this.GetDimensions().X + (double) this.GetDimensions().Width - (double) asset.Width() - 3.0), this.GetDimensions().Y + 2f), new Rectangle?(asset.Frame()), Color.White);
    }

    private void DrawIcon(SpriteBatch spriteBatch)
    {
      CalculatedStyle innerDimensions = this.GetInnerDimensions();
      spriteBatch.Draw(this.ResourcePack.Icon, new Rectangle((int) innerDimensions.X + 4, (int) innerDimensions.Y + 4 + 10, 64, 64), Color.White);
      spriteBatch.Draw(this._iconBorderTexture.Value, new Rectangle((int) innerDimensions.X, (int) innerDimensions.Y + 10, 72, 72), Color.White);
    }

    public override int CompareTo(object obj) => this.Order.CompareTo(((UIResourcePack) obj).Order);

    public override void MouseOver(UIMouseEvent evt)
    {
      base.MouseOver(evt);
      this.BackgroundColor = UIResourcePack.HoverBackgroundColor;
      this.BorderColor = UIResourcePack.HoverBorderColor;
    }

    public override void MouseOut(UIMouseEvent evt)
    {
      base.MouseOut(evt);
      this.BackgroundColor = UIResourcePack.DefaultBackgroundColor;
      this.BorderColor = UIResourcePack.DefaultBorderColor;
    }
  }
}
