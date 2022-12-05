// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Renderers.PrettySparkleParticle
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace Terraria.Graphics.Renderers
{
  public class PrettySparkleParticle : ABasicParticle
  {
    public float FadeInNormalizedTime = 0.05f;
    public float FadeOutNormalizedTime = 0.9f;
    public float TimeToLive = 60f;
    public Color ColorTint;
    public float Opacity;
    public float AdditiveAmount = 1f;
    public float FadeInEnd = 20f;
    public float FadeOutStart = 30f;
    public float FadeOutEnd = 45f;
    public bool DrawHorizontalAxis = true;
    public bool DrawVerticalAxis = true;
    private float _timeSinceSpawn;

    public override void FetchFromPool()
    {
      base.FetchFromPool();
      this.ColorTint = Color.Transparent;
      this._timeSinceSpawn = 0.0f;
      this.Opacity = 0.0f;
      this.FadeInNormalizedTime = 0.05f;
      this.FadeOutNormalizedTime = 0.9f;
      this.TimeToLive = 60f;
      this.AdditiveAmount = 1f;
      this.FadeInEnd = 20f;
      this.FadeOutStart = 30f;
      this.FadeOutEnd = 45f;
      this.DrawVerticalAxis = this.DrawHorizontalAxis = true;
    }

    public override void Update(ref ParticleRendererSettings settings)
    {
      base.Update(ref settings);
      ++this._timeSinceSpawn;
      this.Opacity = Utils.GetLerpValue(0.0f, this.FadeInNormalizedTime, this._timeSinceSpawn / this.TimeToLive, true) * Utils.GetLerpValue(1f, this.FadeOutNormalizedTime, this._timeSinceSpawn / this.TimeToLive, true);
      if ((double) this._timeSinceSpawn < (double) this.TimeToLive)
        return;
      this.ShouldBeRemovedFromRenderer = true;
    }

    public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
    {
      Color color1 = Color.White * this.Opacity * 0.9f;
      color1.A /= (byte) 2;
      Texture2D texture2D = TextureAssets.Extra[98].Value;
      Color color2 = this.ColorTint * this.Opacity * 0.5f;
      color2.A = (byte) ((double) color2.A * (1.0 - (double) this.AdditiveAmount));
      Vector2 origin = texture2D.Size() / 2f;
      Color color3 = color1 * 0.5f;
      float t = (float) ((double) this._timeSinceSpawn / (double) this.TimeToLive * 60.0);
      float num = Utils.GetLerpValue(0.0f, this.FadeInEnd, t, true) * Utils.GetLerpValue(this.FadeOutEnd, this.FadeOutStart, t, true);
      Vector2 scale1 = new Vector2(0.3f, 2f) * num * this.Scale;
      Vector2 scale2 = new Vector2(0.3f, 1f) * num * this.Scale;
      Color color4 = color2 * num;
      Color color5 = color3 * num;
      Vector2 position = settings.AnchorPosition + this.LocalPosition;
      SpriteEffects effects = SpriteEffects.None;
      if (this.DrawHorizontalAxis)
        spritebatch.Draw(texture2D, position, new Rectangle?(), color4, 1.57079637f + this.Rotation, origin, scale1, effects, 0.0f);
      if (this.DrawVerticalAxis)
        spritebatch.Draw(texture2D, position, new Rectangle?(), color4, 0.0f + this.Rotation, origin, scale2, effects, 0.0f);
      if (this.DrawHorizontalAxis)
        spritebatch.Draw(texture2D, position, new Rectangle?(), color5, 1.57079637f + this.Rotation, origin, scale1 * 0.6f, effects, 0.0f);
      if (!this.DrawVerticalAxis)
        return;
      spritebatch.Draw(texture2D, position, new Rectangle?(), color5, 0.0f + this.Rotation, origin, scale2 * 0.6f, effects, 0.0f);
    }
  }
}
