// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.DrawData
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics;

namespace Terraria.DataStructures
{
  public struct DrawData
  {
    public Texture2D texture;
    public Vector2 position;
    public Rectangle destinationRectangle;
    public Rectangle? sourceRect;
    public Color color;
    public float rotation;
    public Vector2 origin;
    public Vector2 scale;
    public SpriteEffects effect;
    public int shader;
    public bool ignorePlayerRotation;
    public readonly bool useDestinationRectangle;
    public static Rectangle? nullRectangle;

    public DrawData(Texture2D texture, Vector2 position, Color color)
    {
      this.texture = texture;
      this.position = position;
      this.color = color;
      this.destinationRectangle = new Rectangle();
      this.sourceRect = DrawData.nullRectangle;
      this.rotation = 0.0f;
      this.origin = Vector2.Zero;
      this.scale = Vector2.One;
      this.effect = SpriteEffects.None;
      this.shader = 0;
      this.ignorePlayerRotation = false;
      this.useDestinationRectangle = false;
    }

    public DrawData(Texture2D texture, Vector2 position, Rectangle? sourceRect, Color color)
    {
      this.texture = texture;
      this.position = position;
      this.color = color;
      this.destinationRectangle = new Rectangle();
      this.sourceRect = sourceRect;
      this.rotation = 0.0f;
      this.origin = Vector2.Zero;
      this.scale = Vector2.One;
      this.effect = SpriteEffects.None;
      this.shader = 0;
      this.ignorePlayerRotation = false;
      this.useDestinationRectangle = false;
    }

    public DrawData(
      Texture2D texture,
      Vector2 position,
      Rectangle? sourceRect,
      Color color,
      float rotation,
      Vector2 origin,
      float scale,
      SpriteEffects effect,
      float inactiveLayerDepth = 0.0f)
    {
      this.texture = texture;
      this.position = position;
      this.sourceRect = sourceRect;
      this.color = color;
      this.rotation = rotation;
      this.origin = origin;
      this.scale = new Vector2(scale, scale);
      this.effect = effect;
      this.destinationRectangle = new Rectangle();
      this.shader = 0;
      this.ignorePlayerRotation = false;
      this.useDestinationRectangle = false;
    }

    public DrawData(
      Texture2D texture,
      Vector2 position,
      Rectangle? sourceRect,
      Color color,
      float rotation,
      Vector2 origin,
      Vector2 scale,
      SpriteEffects effect,
      float inactiveLayerDepth = 0.0f)
    {
      this.texture = texture;
      this.position = position;
      this.sourceRect = sourceRect;
      this.color = color;
      this.rotation = rotation;
      this.origin = origin;
      this.scale = scale;
      this.effect = effect;
      this.destinationRectangle = new Rectangle();
      this.shader = 0;
      this.ignorePlayerRotation = false;
      this.useDestinationRectangle = false;
    }

    public DrawData(Texture2D texture, Rectangle destinationRectangle, Color color)
    {
      this.texture = texture;
      this.destinationRectangle = destinationRectangle;
      this.color = color;
      this.position = Vector2.Zero;
      this.sourceRect = DrawData.nullRectangle;
      this.rotation = 0.0f;
      this.origin = Vector2.Zero;
      this.scale = Vector2.One;
      this.effect = SpriteEffects.None;
      this.shader = 0;
      this.ignorePlayerRotation = false;
      this.useDestinationRectangle = false;
    }

    public DrawData(
      Texture2D texture,
      Rectangle destinationRectangle,
      Rectangle? sourceRect,
      Color color)
    {
      this.texture = texture;
      this.destinationRectangle = destinationRectangle;
      this.color = color;
      this.position = Vector2.Zero;
      this.sourceRect = sourceRect;
      this.rotation = 0.0f;
      this.origin = Vector2.Zero;
      this.scale = Vector2.One;
      this.effect = SpriteEffects.None;
      this.shader = 0;
      this.ignorePlayerRotation = false;
      this.useDestinationRectangle = false;
    }

    public DrawData(
      Texture2D texture,
      Rectangle destinationRectangle,
      Rectangle? sourceRect,
      Color color,
      float rotation,
      Vector2 origin,
      SpriteEffects effect,
      float inactiveLayerDepth = 0.0f)
    {
      this.texture = texture;
      this.destinationRectangle = destinationRectangle;
      this.sourceRect = sourceRect;
      this.color = color;
      this.rotation = rotation;
      this.origin = origin;
      this.effect = effect;
      this.position = Vector2.Zero;
      this.scale = Vector2.One;
      this.shader = 0;
      this.ignorePlayerRotation = false;
      this.useDestinationRectangle = false;
    }

    public void Draw(SpriteBatch sb)
    {
      if (this.useDestinationRectangle)
        sb.Draw(this.texture, this.destinationRectangle, this.sourceRect, this.color, this.rotation, this.origin, this.effect, 0.0f);
      else
        sb.Draw(this.texture, this.position, this.sourceRect, this.color, this.rotation, this.origin, this.scale, this.effect, 0.0f);
    }

    public void Draw(SpriteDrawBuffer sb)
    {
      if (this.useDestinationRectangle)
        sb.Draw(this.texture, this.destinationRectangle, this.sourceRect, (VertexColors) this.color, this.rotation, this.origin, this.effect);
      else
        sb.Draw(this.texture, this.position, this.sourceRect, (VertexColors) this.color, this.rotation, this.origin, this.scale, this.effect);
    }
  }
}
