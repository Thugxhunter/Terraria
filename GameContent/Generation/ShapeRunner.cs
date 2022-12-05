// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Generation.ShapeRunner
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using System;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
  public class ShapeRunner : GenShape
  {
    private double _startStrength;
    private int _steps;
    private Vector2D _startVelocity;

    public ShapeRunner(double strength, int steps, Vector2D velocity)
    {
      this._startStrength = strength;
      this._steps = steps;
      this._startVelocity = velocity;
    }

    public override bool Perform(Point origin, GenAction action)
    {
      double num1 = (double) this._steps;
      double steps = (double) this._steps;
      double num2 = this._startStrength;
      Vector2D vector2D1;
      // ISSUE: explicit constructor call
      ((Vector2D) ref vector2D1).\u002Ector((double) origin.X, (double) origin.Y);
      Vector2D vector2D2 = Vector2D.op_Equality(this._startVelocity, Vector2D.Zero) ? Utils.RandomVector2D(GenBase._random, -1.0, 1.0) : this._startVelocity;
      while (num1 > 0.0 && num2 > 0.0)
      {
        num2 = this._startStrength * (num1 / steps);
        double num3 = num1 - 1.0;
        int num4 = Math.Max(1, (int) (vector2D1.X - num2 * 0.5));
        int num5 = Math.Max(1, (int) (vector2D1.Y - num2 * 0.5));
        int num6 = Math.Min(GenBase._worldWidth, (int) (vector2D1.X + num2 * 0.5));
        int num7 = Math.Min(GenBase._worldHeight, (int) (vector2D1.Y + num2 * 0.5));
        for (int x = num4; x < num6; ++x)
        {
          for (int y = num5; y < num7; ++y)
          {
            if (Math.Abs((double) x - vector2D1.X) + Math.Abs((double) y - vector2D1.Y) < num2 * 0.5 * (1.0 + (double) GenBase._random.Next(-10, 11) * 0.015))
              this.UnitApply(action, origin, x, y);
          }
        }
        int num8 = (int) (num2 / 50.0) + 1;
        num1 = num3 - (double) num8;
        vector2D1 = Vector2D.op_Addition(vector2D1, vector2D2);
        for (int index = 0; index < num8; ++index)
        {
          vector2D1 = Vector2D.op_Addition(vector2D1, vector2D2);
          vector2D2 = Vector2D.op_Addition(vector2D2, Utils.RandomVector2D(GenBase._random, -0.5, 0.5));
        }
        vector2D2 = Vector2D.Clamp(Vector2D.op_Addition(vector2D2, Utils.RandomVector2D(GenBase._random, -0.5, 0.5)), Vector2D.op_UnaryNegation(Vector2D.One), Vector2D.One);
      }
      return true;
    }
  }
}
