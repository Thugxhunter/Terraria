// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Generation.ShapeRoot
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
  public class ShapeRoot : GenShape
  {
    private double _angle;
    private double _startingSize;
    private double _endingSize;
    private double _distance;

    public ShapeRoot(double angle, double distance = 10.0, double startingSize = 4.0, double endingSize = 1.0)
    {
      this._angle = angle;
      this._distance = distance;
      this._startingSize = startingSize;
      this._endingSize = endingSize;
    }

    private bool DoRoot(
      Point origin,
      GenAction action,
      double angle,
      double distance,
      double startingSize)
    {
      double x = (double) origin.X;
      double y = (double) origin.Y;
      for (double num1 = 0.0; num1 < distance * 0.85; ++num1)
      {
        double amount = num1 / distance;
        double num2 = Utils.Lerp(startingSize, this._endingSize, amount);
        x += Math.Cos(angle);
        y += Math.Sin(angle);
        angle += (double) GenBase._random.NextFloat() - 0.5 + (double) GenBase._random.NextFloat() * (this._angle - 1.5707963705062866) * 0.1 * (1.0 - amount);
        angle = angle * 0.4 + 0.45 * Utils.Clamp<double>(angle, this._angle - 2.0 * (1.0 - 0.5 * amount), this._angle + 2.0 * (1.0 - 0.5 * amount)) + Utils.Lerp(this._angle, 1.5707963705062866, amount) * 0.15;
        for (int index1 = 0; index1 < (int) num2; ++index1)
        {
          for (int index2 = 0; index2 < (int) num2; ++index2)
          {
            if (!this.UnitApply(action, origin, (int) x + index1, (int) y + index2) && this._quitOnFail)
              return false;
          }
        }
      }
      return true;
    }

    public override bool Perform(Point origin, GenAction action) => this.DoRoot(origin, action, this._angle, this._distance, this._startingSize);
  }
}
