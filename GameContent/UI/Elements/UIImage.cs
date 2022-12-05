// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIImage
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIImage : UIElement
  {
    private Asset<Texture2D> _texture;
    public float ImageScale = 1f;
    public float Rotation;
    public bool ScaleToFit;
    public bool AllowResizingDimensions = true;
    public Color Color = Color.White;
    public Vector2 NormalizedOrigin = Vector2.Zero;
    public bool RemoveFloatingPointsFromDrawPosition;
    private Texture2D _nonReloadingTexture;

    public UIImage(Asset<Texture2D> texture) => this.SetImage(texture);

    public UIImage(Texture2D nonReloadingTexture) => this.SetImage(nonReloadingTexture);

    public void SetImage(Asset<Texture2D> texture)
    {
      this._texture = texture;
      this._nonReloadingTexture = (Texture2D) null;
      if (!this.AllowResizingDimensions)
        return;
      this.Width.Set((float) this._texture.Width(), 0.0f);
      this.Height.Set((float) this._texture.Height(), 0.0f);
    }

    public void SetImage(Texture2D nonReloadingTexture)
    {
      this._texture = (Asset<Texture2D>) null;
      this._nonReloadingTexture = nonReloadingTexture;
      if (!this.AllowResizingDimensions)
        return;
      this.Width.Set((float) this._nonReloadingTexture.Width, 0.0f);
      this.Height.Set((float) this._nonReloadingTexture.Height, 0.0f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      CalculatedStyle dimensions = this.GetDimensions();
      Texture2D texture2D = (Texture2D) null;
      if (this._texture != null)
        texture2D = this._texture.Value;
      if (this._nonReloadingTexture != null)
        texture2D = this._nonReloadingTexture;
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
