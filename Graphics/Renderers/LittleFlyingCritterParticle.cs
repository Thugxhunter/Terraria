// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Renderers.LittleFlyingCritterParticle
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameContent;

namespace Terraria.Graphics.Renderers
{
  public class LittleFlyingCritterParticle : IPooledParticle, IParticle
  {
    private int _lifeTimeCounted;
    private int _lifeTimeTotal;
    private Vector2 _spawnPosition;
    private Vector2 _localPosition;
    private Vector2 _velocity;
    private float _neverGoBelowThis;

    public bool IsRestingInPool { get; private set; }

    public bool ShouldBeRemovedFromRenderer { get; private set; }

    public void Prepare(Vector2 position, int duration)
    {
      this._spawnPosition = position;
      this._localPosition = position + Main.rand.NextVector2Circular(4f, 8f);
      this._neverGoBelowThis = position.Y + 8f;
      this.RandomizeVelocity();
      this._lifeTimeCounted = 0;
      this._lifeTimeTotal = 300 + Main.rand.Next(6) * 60;
    }

    private void RandomizeVelocity() => this._velocity = Main.rand.NextVector2Circular(1f, 1f);

    public void RestInPool() => this.IsRestingInPool = true;

    public virtual void FetchFromPool()
    {
      this.IsRestingInPool = false;
      this.ShouldBeRemovedFromRenderer = false;
    }

    public void Update(ref ParticleRendererSettings settings)
    {
      if (++this._lifeTimeCounted >= this._lifeTimeTotal)
        this.ShouldBeRemovedFromRenderer = true;
      this._velocity += new Vector2((float) Math.Sign(this._spawnPosition.X - this._localPosition.X) * 0.02f, (float) Math.Sign(this._spawnPosition.Y - this._localPosition.Y) * 0.02f);
      if (this._lifeTimeCounted % 30 == 0 && Main.rand.Next(2) == 0)
      {
        this.RandomizeVelocity();
        if (Main.rand.Next(2) == 0)
          this._velocity /= 2f;
      }
      this._localPosition += this._velocity;
      if ((double) this._localPosition.Y <= (double) this._neverGoBelowThis)
        return;
      this._localPosition.Y = this._neverGoBelowThis;
      if ((double) this._velocity.Y <= 0.0)
        return;
      this._velocity.Y *= -1f;
    }

    public void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
    {
      Vector2 vector2 = settings.AnchorPosition + this._localPosition;
      if ((double) vector2.X < -10.0 || (double) vector2.X > (double) (Main.screenWidth + 10) || (double) vector2.Y < -10.0 || (double) vector2.Y > (double) (Main.screenHeight + 10))
      {
        this.ShouldBeRemovedFromRenderer = true;
      }
      else
      {
        Texture2D texture2D = TextureAssets.Extra[262].Value;
        int frameY = this._lifeTimeCounted % 6 / 3;
        Rectangle rectangle = texture2D.Frame(verticalFrames: 2, frameY: frameY);
        Vector2 origin = new Vector2((double) this._velocity.X > 0.0 ? 3f : 1f, 3f);
        float num = Utils.Remap((float) this._lifeTimeCounted, 0.0f, 90f, 0.0f, 1f) * Utils.Remap((float) this._lifeTimeCounted, (float) (this._lifeTimeTotal - 90), (float) this._lifeTimeTotal, 1f, 0.0f);
        spritebatch.Draw(texture2D, settings.AnchorPosition + this._localPosition, new Rectangle?(rectangle), Lighting.GetColor(this._localPosition.ToTileCoordinates()) * num, 0.0f, origin, 1f, (double) this._velocity.X > 0.0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0.0f);
      }
    }
  }
}
