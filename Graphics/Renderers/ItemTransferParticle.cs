// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Renderers.ItemTransferParticle
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.Graphics.Renderers
{
  public class ItemTransferParticle : IPooledParticle, IParticle
  {
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public Vector2 BezierHelper1;
    public Vector2 BezierHelper2;
    private Item _itemInstance;
    private int _lifeTimeCounted;
    private int _lifeTimeTotal;

    public bool ShouldBeRemovedFromRenderer { get; private set; }

    public ItemTransferParticle() => this._itemInstance = new Item();

    public void Update(ref ParticleRendererSettings settings)
    {
      if (++this._lifeTimeCounted < this._lifeTimeTotal)
        return;
      this.ShouldBeRemovedFromRenderer = true;
    }

    public void Prepare(
      int itemType,
      int lifeTimeTotal,
      Vector2 playerPosition,
      Vector2 chestPosition)
    {
      this._itemInstance.SetDefaults(itemType);
      this._lifeTimeTotal = lifeTimeTotal;
      this.StartPosition = playerPosition;
      this.EndPosition = chestPosition;
      Vector2 vector2 = (this.EndPosition - this.StartPosition).SafeNormalize(Vector2.UnitY).RotatedBy(1.5707963705062866);
      int num1 = (double) vector2.Y < 0.0 ? 1 : 0;
      bool flag = (double) vector2.Y == 0.0;
      if (num1 == 0 || flag && Main.rand.Next(2) == 0)
        vector2 *= -1f;
      vector2 = new Vector2(0.0f, -1f);
      float num2 = Vector2.Distance(this.EndPosition, this.StartPosition);
      this.BezierHelper1 = vector2 * num2 + Main.rand.NextVector2Circular(32f, 32f);
      this.BezierHelper2 = -vector2 * num2 + Main.rand.NextVector2Circular(32f, 32f);
    }

    public void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
    {
      float fromValue = (float) this._lifeTimeCounted / (float) this._lifeTimeTotal;
      float toMin1 = Utils.Remap(fromValue, 0.1f, 0.5f, 0.0f, 0.85f);
      Vector2 result;
      Vector2.Hermite(ref this.StartPosition, ref this.BezierHelper1, ref this.EndPosition, ref this.BezierHelper2, Utils.Remap(fromValue, 0.5f, 0.9f, toMin1, 1f), out result);
      float toMin2 = Utils.Remap(fromValue, 0.0f, 0.1f, 0.0f, 1f);
      float num1 = Utils.Remap(fromValue, 0.85f, 0.95f, toMin2, 0.0f);
      float num2 = Utils.Remap(fromValue, 0.0f, 0.25f, 0.0f, 1f) * Utils.Remap(fromValue, 0.85f, 0.95f, 1f, 0.0f);
      double num3 = (double) ItemSlot.DrawItemIcon(this._itemInstance, 31, Main.spriteBatch, settings.AnchorPosition + result, this._itemInstance.scale * num1, 100f, Color.White * num2);
    }

    public bool IsRestingInPool { get; private set; }

    public void RestInPool() => this.IsRestingInPool = true;

    public virtual void FetchFromPool()
    {
      this._lifeTimeCounted = 0;
      this._lifeTimeTotal = 0;
      this.IsRestingInPool = false;
      this.ShouldBeRemovedFromRenderer = false;
      this.StartPosition = this.EndPosition = this.BezierHelper1 = this.BezierHelper2 = Vector2.Zero;
    }
  }
}
