// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Drawing.WindGrid
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;

namespace Terraria.GameContent.Drawing
{
  public class WindGrid
  {
    private WindGrid.WindCoord[,] _grid = new WindGrid.WindCoord[1, 1];
    private int _width = 1;
    private int _height = 1;
    private int _gameTime;

    public void SetSize(int targetWidth, int targetHeight)
    {
      this._width = Math.Max(this._width, targetWidth);
      this._height = Math.Max(this._height, targetHeight);
      this.ResizeGrid();
    }

    public void Update()
    {
      ++this._gameTime;
      if (!Main.SettingsEnabled_TilesSwayInWind)
        return;
      this.ScanPlayers();
    }

    public void GetWindTime(
      int tileX,
      int tileY,
      int timeThreshold,
      out int windTimeLeft,
      out int directionX,
      out int directionY)
    {
      WindGrid.WindCoord windCoord = this._grid[tileX % this._width, tileY % this._height];
      directionX = windCoord.DirectionX;
      directionY = windCoord.DirectionY;
      if (windCoord.Time + timeThreshold < this._gameTime)
        windTimeLeft = 0;
      else
        windTimeLeft = this._gameTime - windCoord.Time;
    }

    private void ResizeGrid()
    {
      if (this._width <= this._grid.GetLength(0) && this._height <= this._grid.GetLength(1))
        return;
      this._grid = new WindGrid.WindCoord[this._width, this._height];
    }

    private void SetWindTime(int tileX, int tileY, int directionX, int directionY)
    {
      int index1 = tileX % this._width;
      int index2 = tileY % this._height;
      this._grid[index1, index2].Time = this._gameTime;
      this._grid[index1, index2].DirectionX = directionX;
      this._grid[index1, index2].DirectionY = directionY;
    }

    private void ScanPlayers()
    {
      switch (Main.netMode)
      {
        case 0:
          this.ScanPlayer(Main.myPlayer);
          break;
        case 1:
          for (int i = 0; i < (int) byte.MaxValue; ++i)
            this.ScanPlayer(i);
          break;
      }
    }

    private void ScanPlayer(int i)
    {
      Player player = Main.player[i];
      if (!player.active || player.dead || (double) player.velocity.X == 0.0 && (double) player.velocity.Y == 0.0 || !Utils.CenteredRectangle(Main.Camera.Center, Main.Camera.UnscaledSize).Intersects(player.Hitbox) || player.velocity.HasNaNs())
        return;
      int directionX = Math.Sign(player.velocity.X);
      int directionY = Math.Sign(player.velocity.Y);
      foreach (Point point in Collision.GetTilesIn(player.TopLeft, player.BottomRight))
        this.SetWindTime(point.X, point.Y, directionX, directionY);
    }

    private struct WindCoord
    {
      public int Time;
      public int DirectionX;
      public int DirectionY;
    }
  }
}
