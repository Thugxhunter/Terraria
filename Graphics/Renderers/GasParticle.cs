// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Renderers.GasParticle
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace Terraria.Graphics.Renderers
{
  public class GasParticle : ABasicParticle
  {
    public float FadeInNormalizedTime = 0.25f;
    public float FadeOutNormalizedTime = 0.75f;
    public float TimeToLive = 80f;
    public Color ColorTint;
    public float Opacity;
    public float AdditiveAmount = 1f;
    public float FadeInEnd = 20f;
    public float FadeOutStart = 30f;
    public float FadeOutEnd = 45f;
    public float SlowdownScalar = 0.95f;
    private float _timeSinceSpawn;
    public Color LightColorTint;
    private int _internalIndentifier;
    public float InitialScale = 1f;

    public override void FetchFromPool()
    {
      base.FetchFromPool();
      this.ColorTint = Color.Transparent;
      this._timeSinceSpawn = 0.0f;
      this.Opacity = 0.0f;
      this.FadeInNormalizedTime = 0.25f;
      this.FadeOutNormalizedTime = 0.75f;
      this.TimeToLive = 80f;
      this._internalIndentifier = Main.rand.Next((int) byte.MaxValue);
      this.SlowdownScalar = 0.95f;
      this.LightColorTint = Color.Transparent;
      this.InitialScale = 1f;
    }

    public override void Update(ref ParticleRendererSettings settings)
    {
      base.Update(ref settings);
      ++this._timeSinceSpawn;
      float fromValue = this._timeSinceSpawn / this.TimeToLive;
      this.Scale = Vector2.One * this.InitialScale * Utils.Remap(fromValue, 0.0f, 0.95f, 1f, 1.3f);
      this.Opacity = MathHelper.Clamp(Utils.Remap(fromValue, 0.0f, this.FadeInNormalizedTime, 0.0f, 1f) * Utils.Remap(fromValue, this.FadeOutNormalizedTime, 1f, 1f, 0.0f), 0.0f, 1f) * 0.85f;
      this.Rotation = (float) ((double) this._internalIndentifier * 0.40020290017127991 + (double) this._timeSinceSpawn * 6.2831854820251465 / 480.0 * 0.5);
      this.Velocity = this.Velocity * this.SlowdownScalar;
      if (this.LightColorTint != Color.Transparent)
      {
        Color color = this.LightColorTint * this.Opacity;
        Lighting.AddLight(this.LocalPosition, (float) color.R / (float) byte.MaxValue, (float) color.G / (float) byte.MaxValue, (float) color.B / (float) byte.MaxValue);
      }
      if ((double) this._timeSinceSpawn < (double) this.TimeToLive)
        return;
      this.ShouldBeRemovedFromRenderer = true;
    }

    public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
    {
      Main.instance.LoadProjectile(1007);
      Texture2D texture2D = TextureAssets.Projectile[1007].Value;
      Vector2 origin = new Vector2((float) (texture2D.Width / 2), (float) (texture2D.Height / 2));
      Vector2 position = settings.AnchorPosition + this.LocalPosition;
      Color color = Color.Lerp(Lighting.GetColor(this.LocalPosition.ToTileCoordinates()), this.ColorTint, 0.2f) * this.Opacity;
      Vector2 scale = this.Scale;
      spritebatch.Draw(texture2D, position, new Rectangle?(texture2D.Frame()), color, this.Rotation, origin, scale, SpriteEffects.None, 0.0f);
      spritebatch.Draw(texture2D, position, new Rectangle?(texture2D.Frame()), color * 0.25f, this.Rotation, origin, scale * (float) (1.0 + (double) this.Opacity * 1.5), SpriteEffects.None, 0.0f);
    }
  }
}
