// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIImageWithBorder
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIImageWithBorder : UIImage
  {
    private Asset<Texture2D> _borderTexture;
    private Texture2D _nonReloadingBorderTexture;

    public UIImageWithBorder(Asset<Texture2D> texture, Asset<Texture2D> borderTexture)
      : base(texture)
    {
      this.SetBorder(borderTexture);
    }

    public UIImageWithBorder(Texture2D nonReloadingTexture, Texture2D nonReloadingBorderTexture)
      : base(nonReloadingTexture)
    {
      this.SetBorder(nonReloadingBorderTexture);
    }

    public void SetBorder(Asset<Texture2D> texture)
    {
      this._borderTexture = texture;
      this._nonReloadingBorderTexture = (Texture2D) null;
      this.Width.Set((float) this._borderTexture.Width(), 0.0f);
      this.Height.Set((float) this._borderTexture.Height(), 0.0f);
    }

    public void SetBorder(Texture2D nonReloadingTexture)
    {
      this._borderTexture = (Asset<Texture2D>) null;
      this._nonReloadingBorderTexture = nonReloadingTexture;
      this.Width.Set((float) this._nonReloadingBorderTexture.Width, 0.0f);
      this.Height.Set((float) this._nonReloadingBorderTexture.Height, 0.0f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      base.DrawSelf(spriteBatch);
      CalculatedStyle dimensions = this.GetDimensions();
      Texture2D texture2D = (Texture2D) null;
      if (this._borderTexture != null)
        texture2D = this._borderTexture.Value;
      if (this._nonReloadingBorderTexture != null)
        texture2D = this._nonReloadingBorderTexture;
      if (this.ScaleToFit)
      {
        spriteBatch.Draw(texture2D, dimensions.ToRectangle(), this.Color);
      }
      else
      {
        Vector2 vector2_1 = texture2D.Size();
        Vector2 vector2_2 = dimensions.Position() + vector2_1 * (1f - this.ImageScale) / 2f + vector2_1 * this.NormalizedOrigin;
        if (this.RemoveFloatingPointsFromDrawPosition)
          vector2_2 = vector2_2.Floor();
        spriteBatch.Draw(texture2D, vector2_2, new Rectangle?(), this.Color, this.Rotation, vector2_1 * this.NormalizedOrigin, this.ImageScale, SpriteEffects.None, 0.0f);
      }
    }
  }
}
