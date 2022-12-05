// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Renderers.CreativeSacrificeParticle
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.Graphics.Renderers
{
  public class CreativeSacrificeParticle : IParticle
  {
    public Vector2 AccelerationPerFrame;
    public Vector2 Velocity;
    public Vector2 LocalPosition;
    public float ScaleOffsetPerFrame;
    public float StopWhenBelowXScale;
    private Asset<Texture2D> _texture;
    private Rectangle _frame;
    private Vector2 _origin;
    private float _scale;

    public bool ShouldBeRemovedFromRenderer { get; private set; }

    public CreativeSacrificeParticle(
      Asset<Texture2D> textureAsset,
      Rectangle? frame,
      Vector2 initialVelocity,
      Vector2 initialLocalPosition)
    {
      this._texture = textureAsset;
      this._frame = frame.HasValue ? frame.Value : this._texture.Frame();
      this._origin = this._frame.Size() / 2f;
      this.Velocity = initialVelocity;
      this.LocalPosition = initialLocalPosition;
      this.StopWhenBelowXScale = 0.0f;
      this.ShouldBeRemovedFromRenderer = false;
      this._scale = 0.6f;
    }

    public void Update(ref ParticleRendererSettings settings)
    {
      this.Velocity += this.AccelerationPerFrame;
      this.LocalPosition += this.Velocity;
      this._scale += this.ScaleOffsetPerFrame;
      if ((double) this._scale > (double) this.StopWhenBelowXScale)
        return;
      this.ShouldBeRemovedFromRenderer = true;
    }

    public void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
    {
      Color color = Color.Lerp(Color.White, new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), Utils.GetLerpValue(0.1f, 0.5f, this._scale, false));
      spritebatch.Draw(this._texture.Value, settings.AnchorPosition + this.LocalPosition, new Rectangle?(this._frame), color, 0.0f, this._origin, this._scale, SpriteEffects.None, 0.0f);
    }
  }
}
