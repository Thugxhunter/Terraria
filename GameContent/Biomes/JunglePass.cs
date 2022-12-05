// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.JunglePass
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using System;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
  public class JunglePass : GenPass
  {
    private double _worldScale;

    public JunglePass()
      : base("Jungle", 10154.65234375)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
      progress.Message = Lang.gen[11].Value;
      this._worldScale = (double) Main.maxTilesX / 4200.0 * 1.5;
      double worldScale = this._worldScale;
      Point startPoint = this.CreateStartPoint();
      int x = startPoint.X;
      int y = startPoint.Y;
      Point zero = Point.Zero;
      this.ApplyRandomMovement(ref x, ref y, 100, 100);
      zero.X += x;
      zero.Y += y;
      this.PlaceFirstPassMud(x, y, 3);
      this.PlaceGemsAt(x, y, (ushort) 63, 2);
      progress.Set(0.15);
      this.ApplyRandomMovement(ref x, ref y, 250, 150);
      zero.X += x;
      zero.Y += y;
      this.PlaceFirstPassMud(x, y, 0);
      this.PlaceGemsAt(x, y, (ushort) 65, 2);
      progress.Set(0.3);
      int oldX = x;
      int oldY = y;
      this.ApplyRandomMovement(ref x, ref y, 400, 150);
      zero.X += x;
      zero.Y += y;
      this.PlaceFirstPassMud(x, y, -3);
      this.PlaceGemsAt(x, y, (ushort) 67, 2);
      progress.Set(0.45);
      int num1 = zero.X / 3;
      int j = zero.Y / 3;
      int strength = GenBase._random.Next((int) (400.0 * worldScale), (int) (600.0 * worldScale));
      int num2 = (int) (25.0 * worldScale);
      int i = Utils.Clamp<int>(num1, GenVars.leftBeachEnd + strength / 2 + num2, GenVars.rightBeachStart - strength / 2 - num2);
      GenVars.mudWall = true;
      WorldGen.TileRunner(i, j, (double) strength, 10000, 59, speedY: -20.0, noYChange: true);
      this.GenerateTunnelToSurface(i, j);
      GenVars.mudWall = false;
      progress.Set(0.6);
      this.GenerateHolesInMudWalls();
      this.GenerateFinishingTouches(progress, oldX, oldY);
    }

    private void PlaceGemsAt(int x, int y, ushort baseGem, int gemVariants)
    {
      for (int index = 0; (double) index < 6.0 * this._worldScale; ++index)
        WorldGen.TileRunner(x + GenBase._random.Next(-(int) (125.0 * this._worldScale), (int) (125.0 * this._worldScale)), y + GenBase._random.Next(-(int) (125.0 * this._worldScale), (int) (125.0 * this._worldScale)), (double) GenBase._random.Next(3, 7), GenBase._random.Next(3, 8), GenBase._random.Next((int) baseGem, (int) baseGem + gemVariants));
    }

    private void PlaceFirstPassMud(int x, int y, int xSpeedScale)
    {
      GenVars.mudWall = true;
      WorldGen.TileRunner(x, y, (double) GenBase._random.Next((int) (250.0 * this._worldScale), (int) (500.0 * this._worldScale)), GenBase._random.Next(50, 150), 59, speedX: ((double) (GenVars.dungeonSide * xSpeedScale)));
      GenVars.mudWall = false;
    }

    private Point CreateStartPoint() => new Point(GenVars.jungleOriginX, (int) ((double) Main.maxTilesY + Main.rockLayer) / 2);

    private void ApplyRandomMovement(ref int x, ref int y, int xRange, int yRange)
    {
      x += GenBase._random.Next((int) ((double) -xRange * this._worldScale), 1 + (int) ((double) xRange * this._worldScale));
      y += GenBase._random.Next((int) ((double) -yRange * this._worldScale), 1 + (int) ((double) yRange * this._worldScale));
      y = Utils.Clamp<int>(y, (int) Main.rockLayer, Main.maxTilesY);
    }

    private void GenerateTunnelToSurface(int i, int j)
    {
      double num1 = (double) GenBase._random.Next(5, 11);
      Vector2D vector2D1;
      vector2D1.X = (double) i;
      vector2D1.Y = (double) j;
      Vector2D vector2D2;
      vector2D2.X = (double) GenBase._random.Next(-10, 11) * 0.1;
      vector2D2.Y = (double) GenBase._random.Next(10, 20) * 0.1;
      int num2 = 0;
      bool flag = true;
      while (flag)
      {
        if (vector2D1.Y < Main.worldSurface)
        {
          if (WorldGen.drunkWorldGen)
            flag = false;
          int x = (int) vector2D1.X;
          int y = (int) vector2D1.Y;
          int index1 = Utils.Clamp<int>(x, 10, Main.maxTilesX - 10);
          int index2 = Utils.Clamp<int>(y, 10, Main.maxTilesY - 10);
          if (index2 < 5)
            index2 = 5;
          if (Main.tile[index1, index2].wall == (ushort) 0 && !Main.tile[index1, index2].active() && Main.tile[index1, index2 - 3].wall == (ushort) 0 && !Main.tile[index1, index2 - 3].active() && Main.tile[index1, index2 - 1].wall == (ushort) 0 && !Main.tile[index1, index2 - 1].active() && Main.tile[index1, index2 - 4].wall == (ushort) 0 && !Main.tile[index1, index2 - 4].active() && Main.tile[index1, index2 - 2].wall == (ushort) 0 && !Main.tile[index1, index2 - 2].active() && Main.tile[index1, index2 - 5].wall == (ushort) 0 && !Main.tile[index1, index2 - 5].active())
            flag = false;
        }
        GenVars.JungleX = (int) vector2D1.X;
        num1 += (double) GenBase._random.Next(-20, 21) * 0.1;
        if (num1 < 5.0)
          num1 = 5.0;
        if (num1 > 10.0)
          num1 = 10.0;
        int num3 = (int) (vector2D1.X - num1 * 0.5);
        int num4 = (int) (vector2D1.X + num1 * 0.5);
        int num5 = (int) (vector2D1.Y - num1 * 0.5);
        int num6 = (int) (vector2D1.Y + num1 * 0.5);
        int max = Main.maxTilesX - 10;
        int num7 = Utils.Clamp<int>(num3, 10, max);
        int num8 = Utils.Clamp<int>(num4, 10, Main.maxTilesX - 10);
        int num9 = Utils.Clamp<int>(num5, 10, Main.maxTilesY - 10);
        int num10 = Utils.Clamp<int>(num6, 10, Main.maxTilesY - 10);
        for (int i1 = num7; i1 < num8; ++i1)
        {
          for (int j1 = num9; j1 < num10; ++j1)
          {
            if (Math.Abs((double) i1 - vector2D1.X) + Math.Abs((double) j1 - vector2D1.Y) < num1 * 0.5 * (1.0 + (double) GenBase._random.Next(-10, 11) * 0.015))
              WorldGen.KillTile(i1, j1);
          }
        }
        ++num2;
        if (num2 > 10 && GenBase._random.Next(50) < num2)
        {
          num2 = 0;
          int speedX = -2;
          if (GenBase._random.Next(2) == 0)
            speedX = 2;
          WorldGen.TileRunner((int) vector2D1.X, (int) vector2D1.Y, (double) GenBase._random.Next(3, 20), GenBase._random.Next(10, 100), -1, speedX: ((double) speedX));
        }
        vector2D1 = Vector2D.op_Addition(vector2D1, vector2D2);
        vector2D2.Y += (double) GenBase._random.Next(-10, 11) * 0.01;
        if (vector2D2.Y > 0.0)
          vector2D2.Y = 0.0;
        if (vector2D2.Y < -2.0)
          vector2D2.Y = -2.0;
        vector2D2.X += (double) GenBase._random.Next(-10, 11) * 0.1;
        if (vector2D1.X < (double) (i - 200))
          vector2D2.X += (double) GenBase._random.Next(5, 21) * 0.1;
        if (vector2D1.X > (double) (i + 200))
          vector2D2.X -= (double) GenBase._random.Next(5, 21) * 0.1;
        if (vector2D2.X > 1.5)
          vector2D2.X = 1.5;
        if (vector2D2.X < -1.5)
          vector2D2.X = -1.5;
      }
    }

    private void GenerateHolesInMudWalls()
    {
      for (int index = 0; index < Main.maxTilesX / 4; ++index)
      {
        int i = GenBase._random.Next(20, Main.maxTilesX - 20);
        int j;
        for (j = GenBase._random.Next((int) GenVars.worldSurface + 10, Main.UnderworldLayer); Main.tile[i, j].wall != (ushort) 64 && Main.tile[i, j].wall != (ushort) 15; j = GenBase._random.Next((int) GenVars.worldSurface + 10, Main.UnderworldLayer))
          i = GenBase._random.Next(20, Main.maxTilesX - 20);
        WorldGen.MudWallRunner(i, j);
      }
    }

    private void GenerateFinishingTouches(GenerationProgress progress, int oldX, int oldY)
    {
      int i1 = oldX;
      int j1 = oldY;
      double worldScale = this._worldScale;
      for (int index = 0; (double) index <= 20.0 * worldScale; ++index)
      {
        progress.Set((60.0 + (double) index / worldScale) * 0.01);
        i1 += GenBase._random.Next((int) (-5.0 * worldScale), (int) (6.0 * worldScale));
        j1 += GenBase._random.Next((int) (-5.0 * worldScale), (int) (6.0 * worldScale));
        WorldGen.TileRunner(i1, j1, (double) GenBase._random.Next(40, 100), GenBase._random.Next(300, 500), 59);
      }
      for (int index1 = 0; (double) index1 <= 10.0 * worldScale; ++index1)
      {
        progress.Set((80.0 + (double) index1 / worldScale * 2.0) * 0.01);
        int i2 = oldX + GenBase._random.Next((int) (-600.0 * worldScale), (int) (600.0 * worldScale));
        int j2;
        for (j2 = oldY + GenBase._random.Next((int) (-200.0 * worldScale), (int) (200.0 * worldScale)); i2 < 1 || i2 >= Main.maxTilesX - 1 || j2 < 1 || j2 >= Main.maxTilesY - 1 || Main.tile[i2, j2].type != (ushort) 59; j2 = oldY + GenBase._random.Next((int) (-200.0 * worldScale), (int) (200.0 * worldScale)))
          i2 = oldX + GenBase._random.Next((int) (-600.0 * worldScale), (int) (600.0 * worldScale));
        for (int index2 = 0; (double) index2 < 8.0 * worldScale; ++index2)
        {
          i2 += GenBase._random.Next(-30, 31);
          j2 += GenBase._random.Next(-30, 31);
          int type = -1;
          if (GenBase._random.Next(7) == 0)
            type = -2;
          WorldGen.TileRunner(i2, j2, (double) GenBase._random.Next(10, 20), GenBase._random.Next(30, 70), type);
        }
      }
      for (int index = 0; (double) index <= 300.0 * worldScale; ++index)
      {
        int i3 = oldX + GenBase._random.Next((int) (-600.0 * worldScale), (int) (600.0 * worldScale));
        int j3;
        for (j3 = oldY + GenBase._random.Next((int) (-200.0 * worldScale), (int) (200.0 * worldScale)); i3 < 1 || i3 >= Main.maxTilesX - 1 || j3 < 1 || j3 >= Main.maxTilesY - 1 || Main.tile[i3, j3].type != (ushort) 59; j3 = oldY + GenBase._random.Next((int) (-200.0 * worldScale), (int) (200.0 * worldScale)))
          i3 = oldX + GenBase._random.Next((int) (-600.0 * worldScale), (int) (600.0 * worldScale));
        WorldGen.TileRunner(i3, j3, (double) GenBase._random.Next(4, 10), GenBase._random.Next(5, 30), 1);
        if (GenBase._random.Next(4) == 0)
        {
          int type = GenBase._random.Next(63, 69);
          WorldGen.TileRunner(i3 + GenBase._random.Next(-1, 2), j3 + GenBase._random.Next(-1, 2), (double) GenBase._random.Next(3, 7), GenBase._random.Next(4, 8), type);
        }
      }
    }
  }
}
