// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Effects.SkyManager
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Terraria.Graphics.Effects
{
  public class SkyManager : EffectManager<CustomSky>
  {
    public static SkyManager Instance = new SkyManager();
    private float _lastDepth;
    private LinkedList<CustomSky> _activeSkies = new LinkedList<CustomSky>();

    public void Reset()
    {
      foreach (CustomSky customSky in this._effects.Values)
        customSky.Reset();
      this._activeSkies.Clear();
    }

    public void Update(GameTime gameTime)
    {
      int num = Main.dayRate;
      if (num < 1)
        num = 1;
      LinkedListNode<CustomSky> next;
      for (int index = 0; index < num; ++index)
      {
        for (LinkedListNode<CustomSky> node = this._activeSkies.First; node != null; node = next)
        {
          CustomSky customSky = node.Value;
          next = node.Next;
          customSky.Update(gameTime);
          if (!customSky.IsActive())
            this._activeSkies.Remove(node);
        }
      }
    }

    public void Draw(SpriteBatch spriteBatch) => this.DrawDepthRange(spriteBatch, float.MinValue, float.MaxValue);

    public void DrawToDepth(SpriteBatch spriteBatch, float minDepth)
    {
      if ((double) this._lastDepth <= (double) minDepth)
        return;
      this.DrawDepthRange(spriteBatch, minDepth, this._lastDepth);
      this._lastDepth = minDepth;
    }

    public void DrawDepthRange(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
      foreach (CustomSky activeSky in this._activeSkies)
        activeSky.Draw(spriteBatch, minDepth, maxDepth);
    }

    public void DrawRemainingDepth(SpriteBatch spriteBatch)
    {
      this.DrawDepthRange(spriteBatch, float.MinValue, this._lastDepth);
      this._lastDepth = float.MinValue;
    }

    public void ResetDepthTracker() => this._lastDepth = float.MaxValue;

    public void SetStartingDepth(float depth) => this._lastDepth = depth;

    public override void OnActivate(CustomSky effect, Vector2 position)
    {
      this._activeSkies.Remove(effect);
      this._activeSkies.AddLast(effect);
    }

    public Color ProcessTileColor(Color color)
    {
      foreach (CustomSky activeSky in this._activeSkies)
        color = activeSky.OnTileColor(color);
      return color;
    }

    public float ProcessCloudAlpha()
    {
      float num = 1f;
      foreach (CustomSky activeSky in this._activeSkies)
        num *= activeSky.GetCloudAlpha();
      return MathHelper.Clamp(num, 0.0f, 1f);
    }
  }
}
