// Decompiled with JetBrains decompiler
// Type: Terraria.WorldBuilding.Shapes
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using System;

namespace Terraria.WorldBuilding
{
  public static class Shapes
  {
    public class Circle : GenShape
    {
      private int _verticalRadius;
      private int _horizontalRadius;

      public Circle(int radius)
      {
        this._verticalRadius = radius;
        this._horizontalRadius = radius;
      }

      public Circle(int horizontalRadius, int verticalRadius)
      {
        this._horizontalRadius = horizontalRadius;
        this._verticalRadius = verticalRadius;
      }

      public void SetRadius(int radius)
      {
        this._verticalRadius = radius;
        this._horizontalRadius = radius;
      }

      public override bool Perform(Point origin, GenAction action)
      {
        int num1 = (this._horizontalRadius + 1) * (this._horizontalRadius + 1);
        for (int y = origin.Y - this._verticalRadius; y <= origin.Y + this._verticalRadius; ++y)
        {
          double num2 = (double) this._horizontalRadius / (double) this._verticalRadius * (double) (y - origin.Y);
          int num3 = Math.Min(this._horizontalRadius, (int) Math.Sqrt((double) num1 - num2 * num2));
          for (int x = origin.X - num3; x <= origin.X + num3; ++x)
          {
            if (!this.UnitApply(action, origin, x, y) && this._quitOnFail)
              return false;
          }
        }
        return true;
      }
    }

    public class HalfCircle : GenShape
    {
      private int _radius;

      public HalfCircle(int radius) => this._radius = radius;

      public override bool Perform(Point origin, GenAction action)
      {
        int num1 = (this._radius + 1) * (this._radius + 1);
        for (int y = origin.Y - this._radius; y <= origin.Y; ++y)
        {
          int num2 = Math.Min(this._radius, (int) Math.Sqrt((double) (num1 - (y - origin.Y) * (y - origin.Y))));
          for (int x = origin.X - num2; x <= origin.X + num2; ++x)
          {
            if (!this.UnitApply(action, origin, x, y) && this._quitOnFail)
              return false;
          }
        }
        return true;
      }
    }

    public class Slime : GenShape
    {
      private int _radius;
      private double _xScale;
      private double _yScale;

      public Slime(int radius)
      {
        this._radius = radius;
        this._xScale = 1.0;
        this._yScale = 1.0;
      }

      public Slime(int radius, double xScale, double yScale)
      {
        this._radius = radius;
        this._xScale = xScale;
        this._yScale = yScale;
      }

      public override bool Perform(Point origin, GenAction action)
      {
        double radius = (double) this._radius;
        int num1 = (this._radius + 1) * (this._radius + 1);
        for (int y = origin.Y - (int) (radius * this._yScale); y <= origin.Y; ++y)
        {
          double num2 = (double) (y - origin.Y) / this._yScale;
          int num3 = (int) Math.Min((double) this._radius * this._xScale, this._xScale * Math.Sqrt((double) num1 - num2 * num2));
          for (int x = origin.X - num3; x <= origin.X + num3; ++x)
          {
            if (!this.UnitApply(action, origin, x, y) && this._quitOnFail)
              return false;
          }
        }
        for (int y = origin.Y + 1; y <= origin.Y + (int) (radius * this._yScale * 0.5) - 1; ++y)
        {
          double num4 = (double) (y - origin.Y) * (2.0 / this._yScale);
          int num5 = (int) Math.Min((double) this._radius * this._xScale, this._xScale * Math.Sqrt((double) num1 - num4 * num4));
          for (int x = origin.X - num5; x <= origin.X + num5; ++x)
          {
            if (!this.UnitApply(action, origin, x, y) && this._quitOnFail)
              return false;
          }
        }
        return true;
      }
    }

    public class Rectangle : GenShape
    {
      private Microsoft.Xna.Framework.Rectangle _area;

      public Rectangle(Microsoft.Xna.Framework.Rectangle area) => this._area = area;

      public Rectangle(int width, int height) => this._area = new Microsoft.Xna.Framework.Rectangle(0, 0, width, height);

      public void SetArea(Microsoft.Xna.Framework.Rectangle area) => this._area = area;

      public override bool Perform(Point origin, GenAction action)
      {
        for (int x = origin.X + this._area.Left; x < origin.X + this._area.Right; ++x)
        {
          for (int y = origin.Y + this._area.Top; y < origin.Y + this._area.Bottom; ++y)
          {
            if (!this.UnitApply(action, origin, x, y) && this._quitOnFail)
              return false;
          }
        }
        return true;
      }
    }

    public class Tail : GenShape
    {
      private double _width;
      private Vector2D _endOffset;

      public Tail(double width, Vector2D endOffset)
      {
        this._width = width * 16.0;
        this._endOffset = Vector2D.op_Multiply(endOffset, 16.0);
      }

      public override bool Perform(Point origin, GenAction action)
      {
        Vector2D start = new Vector2D((double) (origin.X << 4), (double) (origin.Y << 4));
        return Utils.PlotTileTale(start, Vector2D.op_Addition(start, this._endOffset), this._width, (Utils.TileActionAttempt) ((x, y) => this.UnitApply(action, origin, x, y) || !this._quitOnFail));
      }
    }

    public class Mound : GenShape
    {
      private int _halfWidth;
      private int _height;

      public Mound(int halfWidth, int height)
      {
        this._halfWidth = halfWidth;
        this._height = height;
      }

      public override bool Perform(Point origin, GenAction action)
      {
        int height = this._height;
        double halfWidth = (double) this._halfWidth;
        for (int index1 = -this._halfWidth; index1 <= this._halfWidth; ++index1)
        {
          int num = Math.Min(this._height, (int) (-((double) (this._height + 1) / (halfWidth * halfWidth)) * ((double) index1 + halfWidth) * ((double) index1 - halfWidth)));
          for (int index2 = 0; index2 < num; ++index2)
          {
            if (!this.UnitApply(action, origin, index1 + origin.X, origin.Y - index2) && this._quitOnFail)
              return false;
          }
        }
        return true;
      }
    }
  }
}
