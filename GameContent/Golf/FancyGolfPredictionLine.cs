// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Golf.FancyGolfPredictionLine
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using Terraria.Graphics;

namespace Terraria.GameContent.Golf
{
  public class FancyGolfPredictionLine
  {
    private readonly List<Vector2> _positions;
    private readonly Entity _entity = (Entity) new FancyGolfPredictionLine.PredictionEntity();
    private readonly int _iterations;
    private readonly Color[] _colors = new Color[2]
    {
      Color.White,
      Color.Gray
    };
    private readonly BasicDebugDrawer _drawer = new BasicDebugDrawer(Main.instance.GraphicsDevice);
    private float _time;

    public FancyGolfPredictionLine(int iterations)
    {
      this._positions = new List<Vector2>(iterations * 2 + 1);
      this._iterations = iterations;
    }

    public void Update(Entity golfBall, Vector2 impactVelocity, float roughLandResistance)
    {
      bool flag = Main.tileSolid[379];
      Main.tileSolid[379] = false;
      this._positions.Clear();
      this._time += 0.0166666675f;
      this._entity.position = golfBall.position;
      this._entity.width = golfBall.width;
      this._entity.height = golfBall.height;
      GolfHelper.HitGolfBall(this._entity, impactVelocity, roughLandResistance);
      this._positions.Add(this._entity.position);
      float angularVelocity = 0.0f;
      for (int index = 0; index < this._iterations; ++index)
      {
        GolfHelper.StepGolfBall(this._entity, ref angularVelocity);
        this._positions.Add(this._entity.position);
      }
      Main.tileSolid[379] = flag;
    }

    public void Draw(Camera camera, SpriteBatch spriteBatch, float chargeProgress)
    {
      this._drawer.Begin(camera.GameViewMatrix.TransformationMatrix);
      int count = this._positions.Count;
      Texture2D texture2D = TextureAssets.Extra[33].Value;
      Vector2 vector2_1 = new Vector2(3.5f, 3.5f);
      Vector2 origin = texture2D.Size() / 2f;
      Vector2 unscaledPosition = camera.UnscaledPosition;
      Vector2 vector2_2 = vector2_1 - unscaledPosition;
      float travelledLength = 0.0f;
      float num = 0.0f;
      for (int startIndex = 0; startIndex < this._positions.Count - 1; ++startIndex)
      {
        float length;
        this.GetSectionLength(startIndex, out length, out float _);
        if ((double) length != 0.0)
        {
          for (; (double) travelledLength < (double) num + (double) length; travelledLength += 4f)
          {
            float index = (travelledLength - num) / length + (float) startIndex;
            Vector2 position = this.GetPosition((travelledLength - num) / length + (float) startIndex);
            Color color = this.GetColor2(index) * MathHelper.Clamp((float) (2.0 - 2.0 * (double) index / (double) (this._positions.Count - 1)), 0.0f, 1f);
            spriteBatch.Draw(texture2D, position + vector2_2, new Rectangle?(), color, 0.0f, origin, this.GetScale(travelledLength), SpriteEffects.None, 0.0f);
          }
          num += length;
        }
      }
      this._drawer.End();
    }

    private Color GetColor(float travelledLength)
    {
      float d = ((float) ((double) travelledLength % 200.0 / 200.0) * (float) this._colors.Length - (float) ((double) this._time * 3.1415927410125732 * 1.5)) % (float) this._colors.Length;
      if ((double) d < 0.0)
        d += (float) this._colors.Length;
      int num1 = (int) Math.Floor((double) d);
      int num2 = num1 + 1;
      int index1 = Utils.Clamp<int>(num1 % this._colors.Length, 0, this._colors.Length - 1);
      int index2 = Utils.Clamp<int>(num2 % this._colors.Length, 0, this._colors.Length - 1);
      float amount = d - (float) index1;
      return Color.Lerp(this._colors[index1], this._colors[index2], amount) with
      {
        A = (byte) 64
      } * 0.6f;
    }

    private Color GetColor2(float index)
    {
      double d;
      int index1 = (int) Math.Floor(d = (double) index * 0.5 - (double) this._time * 3.1415927410125732 * 1.5) % this._colors.Length;
      if (index1 < 0)
        index1 += this._colors.Length;
      int index2 = (index1 + 1) % this._colors.Length;
      float amount = (float) d - (float) Math.Floor(d);
      return Color.Lerp(this._colors[index1], this._colors[index2], amount) with
      {
        A = (byte) 64
      } * 0.6f;
    }

    private float GetScale(float travelledLength) => (float) (0.20000000298023224 + (double) Utils.GetLerpValue(0.8f, 1f, (float) (Math.Cos((double) travelledLength / 50.0 + (double) this._time * -3.1415927410125732) * 0.5 + 0.5), true) * 0.15000000596046448);

    private void GetSectionLength(int startIndex, out float length, out float rotation)
    {
      int index = startIndex + 1;
      if (index >= this._positions.Count)
        index = this._positions.Count - 1;
      length = Vector2.Distance(this._positions[startIndex], this._positions[index]);
      rotation = (this._positions[index] - this._positions[startIndex]).ToRotation();
    }

    private Vector2 GetPosition(float indexProgress)
    {
      int index1 = (int) Math.Floor((double) indexProgress);
      int index2 = index1 + 1;
      if (index2 >= this._positions.Count)
        index2 = this._positions.Count - 1;
      float amount = indexProgress - (float) index1;
      return Vector2.Lerp(this._positions[index1], this._positions[index2], amount);
    }

    private class PredictionEntity : Entity
    {
    }
  }
}
