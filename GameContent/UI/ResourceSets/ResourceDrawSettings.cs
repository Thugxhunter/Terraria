// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.ResourceSets.ResourceDrawSettings
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.UI.ResourceSets
{
  public struct ResourceDrawSettings
  {
    public Vector2 TopLeftAnchor;
    public int ElementCount;
    public int ElementIndexOffset;
    public ResourceDrawSettings.TextureGetter GetTextureMethod;
    public Vector2 OffsetPerDraw;
    public Vector2 OffsetPerDrawByTexturePercentile;
    public Vector2 OffsetSpriteAnchor;
    public Vector2 OffsetSpriteAnchorByTexturePercentile;

    public void Draw(SpriteBatch spriteBatch, ref bool isHovered)
    {
      int elementCount = this.ElementCount;
      Vector2 topLeftAnchor = this.TopLeftAnchor;
      Point point = Main.MouseScreen.ToPoint();
      for (int index = 0; index < elementCount; ++index)
      {
        Asset<Texture2D> texture;
        Vector2 drawOffset;
        float drawScale;
        Rectangle? sourceRect;
        this.GetTextureMethod(index + this.ElementIndexOffset, this.ElementIndexOffset, this.ElementIndexOffset + elementCount - 1, out texture, out drawOffset, out drawScale, out sourceRect);
        Rectangle r = texture.Frame();
        if (sourceRect.HasValue)
          r = sourceRect.Value;
        Vector2 position = topLeftAnchor + drawOffset;
        Vector2 origin = this.OffsetSpriteAnchor + r.Size() * this.OffsetSpriteAnchorByTexturePercentile;
        Rectangle rectangle = r;
        rectangle.X += (int) ((double) position.X - (double) origin.X);
        rectangle.Y += (int) ((double) position.Y - (double) origin.Y);
        if (rectangle.Contains(point))
          isHovered = true;
        spriteBatch.Draw(texture.Value, position, new Rectangle?(r), Color.White, 0.0f, origin, drawScale, SpriteEffects.None, 0.0f);
        topLeftAnchor += this.OffsetPerDraw + r.Size() * this.OffsetPerDrawByTexturePercentile;
      }
    }

    public delegate void TextureGetter(
      int elementIndex,
      int firstElementIndex,
      int lastElementIndex,
      out Asset<Texture2D> texture,
      out Vector2 drawOffset,
      out float drawScale,
      out Rectangle? sourceRect);
  }
}
