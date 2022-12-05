// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Light.TileLightScanner
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Threading;
using System;
using Terraria.GameContent;
using Terraria.GameContent.Liquid;
using Terraria.ID;
using Terraria.Utilities;

namespace Terraria.Graphics.Light
{
  public class TileLightScanner
  {
    private FastRandom _random = FastRandom.CreateWithRandomSeed();
    private bool _drawInvisibleWalls;

    public void ExportTo(Rectangle area, LightMap outputMap, TileLightScannerOptions options)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      TileLightScanner.\u003C\u003Ec__DisplayClass2_0 cDisplayClass20 = new TileLightScanner.\u003C\u003Ec__DisplayClass2_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass20.area = area;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass20.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass20.outputMap = outputMap;
      this._drawInvisibleWalls = options.DrawInvisibleWalls;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      FastParallel.For(cDisplayClass20.area.Left, cDisplayClass20.area.Right, new ParallelForAction((object) cDisplayClass20, __methodptr(\u003CExportTo\u003Eb__0)), (object) null);
    }

    private bool IsTileNullOrTouchingNull(int x, int y) => !WorldGen.InWorld(x, y, 1) || Main.tile[x, y] == null || Main.tile[x + 1, y] == null || Main.tile[x - 1, y] == null || Main.tile[x, y - 1] == null || Main.tile[x, y + 1] == null;

    public void Update() => this._random.NextSeed();

    public LightMaskMode GetMaskMode(int x, int y) => this.GetTileMask(Main.tile[x, y]);

    private LightMaskMode GetTileMask(Tile tile)
    {
      if ((!this.LightIsBlocked(tile) || tile.type == (ushort) 131 || tile.inActive() ? 0 : (tile.slope() == (byte) 0 ? 1 : 0)) != 0)
        return LightMaskMode.Solid;
      if (tile.lava() || tile.liquid <= (byte) 128)
        return LightMaskMode.None;
      return !tile.honey() ? LightMaskMode.Water : LightMaskMode.Honey;
    }

    public void GetTileLight(int x, int y, out Vector3 outputColor)
    {
      outputColor = Vector3.Zero;
      Tile tile = Main.tile[x, y];
      FastRandom localRandom = this._random.WithModifier(x, y);
      if (y <= (int) Main.worldSurface)
        this.ApplySurfaceLight(tile, x, y, ref outputColor);
      else if (y > Main.UnderworldLayer)
        this.ApplyHellLight(tile, x, y, ref outputColor);
      this.ApplyWallLight(tile, x, y, ref localRandom, ref outputColor);
      if (tile.active())
        this.ApplyTileLight(tile, x, y, ref localRandom, ref outputColor);
      this.ApplyLiquidLight(tile, ref outputColor);
    }

    private void ApplyLiquidLight(Tile tile, ref Vector3 lightColor)
    {
      if (tile.liquid <= (byte) 0)
        return;
      if (tile.lava())
      {
        float num = 0.55f + (float) (270 - (int) Main.mouseTextColor) / 900f;
        if ((double) lightColor.X < (double) num)
          lightColor.X = num;
        if ((double) lightColor.Y < (double) num)
          lightColor.Y = num * 0.6f;
        if ((double) lightColor.Z >= (double) num)
          return;
        lightColor.Z = num * 0.2f;
      }
      else
      {
        if (!tile.shimmer())
          return;
        float num1 = 0.7f;
        float num2 = 0.7f;
        float num3 = num1 + (float) (270 - (int) Main.mouseTextColor) / 900f;
        float num4 = num2 + (float) (270 - (int) Main.mouseTextColor) / 125f;
        if ((double) lightColor.X < (double) num3)
          lightColor.X = num3 * 0.6f;
        if ((double) lightColor.Y < (double) num4)
          lightColor.Y = num4 * 0.25f;
        if ((double) lightColor.Z >= (double) num3)
          return;
        lightColor.Z = num3 * 0.9f;
      }
    }

    private bool LightIsBlocked(Tile tile)
    {
      if (!tile.active() || !Main.tileBlockLight[(int) tile.type])
        return false;
      return !tile.invisibleBlock() || this._drawInvisibleWalls;
    }

    private void ApplyWallLight(
      Tile tile,
      int x,
      int y,
      ref FastRandom localRandom,
      ref Vector3 lightColor)
    {
      float num1 = 0.0f;
      float num2 = 0.0f;
      float num3 = 0.0f;
      switch (tile.wall)
      {
        case 33:
          if (!this.LightIsBlocked(tile))
          {
            num1 = 0.0899999961f;
            num2 = 0.0525000021f;
            num3 = 0.24f;
            break;
          }
          break;
        case 44:
          if (!this.LightIsBlocked(tile))
          {
            num1 = (float) ((double) Main.DiscoR / (double) byte.MaxValue * 0.15000000596046448);
            num2 = (float) ((double) Main.DiscoG / (double) byte.MaxValue * 0.15000000596046448);
            num3 = (float) ((double) Main.DiscoB / (double) byte.MaxValue * 0.15000000596046448);
            break;
          }
          break;
        case 137:
          if (!this.LightIsBlocked(tile))
          {
            float num4 = 0.4f + (float) (270 - (int) Main.mouseTextColor) / 1500f + (float) localRandom.Next(0, 50) * 0.0005f;
            num1 = 1f * num4;
            num2 = 0.5f * num4;
            num3 = 0.1f * num4;
            break;
          }
          break;
        case 153:
          num1 = 0.6f;
          num2 = 0.3f;
          break;
        case 154:
          num1 = 0.6f;
          num3 = 0.6f;
          break;
        case 155:
          num1 = 0.6f;
          num2 = 0.6f;
          num3 = 0.6f;
          break;
        case 156:
          num2 = 0.6f;
          break;
        case 164:
          num1 = 0.6f;
          break;
        case 165:
          num3 = 0.6f;
          break;
        case 166:
          num1 = 0.6f;
          num2 = 0.6f;
          break;
        case 174:
          if (!this.LightIsBlocked(tile))
          {
            num1 = 0.2975f;
            break;
          }
          break;
        case 175:
          if (!this.LightIsBlocked(tile))
          {
            num1 = 0.075f;
            num2 = 0.15f;
            num3 = 0.4f;
            break;
          }
          break;
        case 176:
          if (!this.LightIsBlocked(tile))
          {
            num1 = 0.1f;
            num2 = 0.1f;
            num3 = 0.1f;
            break;
          }
          break;
        case 182:
          if (!this.LightIsBlocked(tile))
          {
            num1 = 0.24f;
            num2 = 0.12f;
            num3 = 0.0899999961f;
            break;
          }
          break;
        case 341:
          if (!this.LightIsBlocked(tile))
          {
            num1 = 0.25f;
            num2 = 0.1f;
            num3 = 0.0f;
            break;
          }
          break;
        case 342:
          if (!this.LightIsBlocked(tile))
          {
            num1 = 0.3f;
            num2 = 0.0f;
            num3 = 0.17f;
            break;
          }
          break;
        case 343:
          if (!this.LightIsBlocked(tile))
          {
            num1 = 0.0f;
            num2 = 0.25f;
            num3 = 0.0f;
            break;
          }
          break;
        case 344:
          if (!this.LightIsBlocked(tile))
          {
            num1 = 0.0f;
            num2 = 0.16f;
            num3 = 0.34f;
            break;
          }
          break;
        case 345:
          if (!this.LightIsBlocked(tile))
          {
            num1 = 0.3f;
            num2 = 0.0f;
            num3 = 0.35f;
            break;
          }
          break;
        case 346:
          if (!this.LightIsBlocked(tile))
          {
            num1 = (float) ((double) Main.DiscoR / (double) byte.MaxValue * 0.25);
            num2 = (float) ((double) Main.DiscoG / (double) byte.MaxValue * 0.25);
            num3 = (float) ((double) Main.DiscoB / (double) byte.MaxValue * 0.25);
            break;
          }
          break;
      }
      if ((double) lightColor.X < (double) num1)
        lightColor.X = num1;
      if ((double) lightColor.Y < (double) num2)
        lightColor.Y = num2;
      if ((double) lightColor.Z >= (double) num3)
        return;
      lightColor.Z = num3;
    }

    private void ApplyTileLight(
      Tile tile,
      int x,
      int y,
      ref FastRandom localRandom,
      ref Vector3 lightColor)
    {
      float R = 0.0f;
      float G = 0.0f;
      float B = 0.0f;
      if (Main.tileLighted[(int) tile.type])
      {
        switch (tile.type)
        {
          case 4:
            if (tile.frameX < (short) 66)
            {
              TorchID.TorchColor((int) tile.frameY / 22, out R, out G, out B);
              break;
            }
            break;
          case 17:
          case 133:
          case 302:
            R = 0.83f;
            G = 0.6f;
            B = 0.5f;
            break;
          case 20:
            switch ((int) tile.frameX / 18)
            {
              case 30:
              case 31:
              case 32:
                R = 0.325f;
                G = 0.15f;
                B = 0.05f;
                break;
            }
            break;
          case 22:
          case 140:
            R = 0.12f;
            G = 0.07f;
            B = 0.32f;
            break;
          case 26:
          case 31:
            if (tile.type == (ushort) 31 && tile.frameX >= (short) 36 || tile.type == (ushort) 26 && tile.frameX >= (short) 54)
            {
              float num = (float) localRandom.Next(-5, 6) * (1f / 400f);
              R = (float) (0.5 + (double) num * 2.0);
              G = 0.2f + num;
              B = 0.1f;
              break;
            }
            float num1 = (float) localRandom.Next(-5, 6) * (1f / 400f);
            R = 0.31f + num1;
            G = 0.1f;
            B = (float) (0.43999999761581421 + (double) num1 * 2.0);
            break;
          case 27:
            if (tile.frameY < (short) 36)
            {
              R = 0.3f;
              G = 0.27f;
              break;
            }
            break;
          case 33:
            if (tile.frameX == (short) 0)
            {
              switch ((int) tile.frameY / 22)
              {
                case 0:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 1:
                  R = 0.55f;
                  G = 0.85f;
                  B = 0.35f;
                  break;
                case 2:
                  R = 0.65f;
                  G = 0.95f;
                  B = 0.5f;
                  break;
                case 3:
                  R = 0.2f;
                  G = 0.75f;
                  B = 1f;
                  break;
                case 5:
                  R = 0.85f;
                  G = 0.6f;
                  B = 1f;
                  break;
                case 7:
                case 8:
                  R = 0.75f;
                  G = 0.85f;
                  B = 1f;
                  break;
                case 9:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 10:
                  R = 1f;
                  G = 0.97f;
                  B = 0.85f;
                  break;
                case 14:
                  R = 1f;
                  G = 1f;
                  B = 0.6f;
                  break;
                case 15:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 18:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 19:
                  R = 0.37f;
                  G = 0.8f;
                  B = 1f;
                  break;
                case 20:
                  R = 0.0f;
                  G = 0.9f;
                  B = 1f;
                  break;
                case 21:
                  R = 0.25f;
                  G = 0.7f;
                  B = 1f;
                  break;
                case 23:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 24:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 25:
                  R = (float) (0.5 * (double) Main.demonTorch + 1.0 * (1.0 - (double) Main.demonTorch));
                  G = 0.3f;
                  B = (float) (1.0 * (double) Main.demonTorch + 0.5 * (1.0 - (double) Main.demonTorch));
                  break;
                case 28:
                  R = 0.9f;
                  G = 0.75f;
                  B = 1f;
                  break;
                case 29:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 30:
                  Vector3 vector3_1 = Main.hslToRgb((float) ((double) Main.demonTorch * 0.11999999731779099 + 0.68999999761581421), 1f, 0.75f).ToVector3() * 1.2f;
                  R = vector3_1.X;
                  G = vector3_1.Y;
                  B = vector3_1.Z;
                  break;
                case 31:
                  R = 1f;
                  G = 0.97f;
                  B = 0.85f;
                  break;
                case 32:
                  R = 0.55f;
                  G = 0.45f;
                  B = 0.95f;
                  break;
                case 33:
                  R = 1f;
                  G = 0.6f;
                  B = 0.1f;
                  break;
                case 34:
                  R = 0.3f;
                  G = 0.75f;
                  B = 0.55f;
                  break;
                case 35:
                  R = 0.9f;
                  G = 0.55f;
                  B = 0.7f;
                  break;
                case 36:
                  R = 0.55f;
                  G = 0.85f;
                  B = 1f;
                  break;
                case 37:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 38:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 39:
                  R = 0.4f;
                  G = 0.8f;
                  B = 0.9f;
                  break;
                case 40:
                  R = 1f;
                  G = 1f;
                  B = 1f;
                  break;
                case 41:
                  R = 0.95f;
                  G = 0.5f;
                  B = 0.4f;
                  break;
                default:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
              }
            }
            else
              break;
            break;
          case 34:
            if ((int) tile.frameX % 108 < 54)
            {
              switch ((int) tile.frameY / 54 + 37 * ((int) tile.frameX / 108))
              {
                case 7:
                  R = 0.95f;
                  G = 0.95f;
                  B = 0.5f;
                  break;
                case 8:
                  R = 0.85f;
                  G = 0.6f;
                  B = 1f;
                  break;
                case 9:
                  R = 1f;
                  G = 0.6f;
                  B = 0.6f;
                  break;
                case 11:
                case 17:
                  R = 0.75f;
                  G = 0.85f;
                  B = 1f;
                  break;
                case 12:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 13:
                  R = 1f;
                  G = 0.97f;
                  B = 0.85f;
                  break;
                case 15:
                  R = 1f;
                  G = 1f;
                  B = 0.7f;
                  break;
                case 16:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 18:
                  R = 1f;
                  G = 1f;
                  B = 0.6f;
                  break;
                case 19:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 23:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 24:
                  R = 0.37f;
                  G = 0.8f;
                  B = 1f;
                  break;
                case 25:
                  R = 0.0f;
                  G = 0.9f;
                  B = 1f;
                  break;
                case 26:
                  R = 0.25f;
                  G = 0.7f;
                  B = 1f;
                  break;
                case 27:
                  R = 0.55f;
                  G = 0.85f;
                  B = 0.35f;
                  break;
                case 28:
                  R = 0.65f;
                  G = 0.95f;
                  B = 0.5f;
                  break;
                case 29:
                  R = 0.2f;
                  G = 0.75f;
                  B = 1f;
                  break;
                case 30:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 32:
                  R = (float) (0.5 * (double) Main.demonTorch + 1.0 * (1.0 - (double) Main.demonTorch));
                  G = 0.3f;
                  B = (float) (1.0 * (double) Main.demonTorch + 0.5 * (1.0 - (double) Main.demonTorch));
                  break;
                case 35:
                  R = 0.9f;
                  G = 0.75f;
                  B = 1f;
                  break;
                case 36:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 37:
                  Vector3 vector3_2 = Main.hslToRgb((float) ((double) Main.demonTorch * 0.11999999731779099 + 0.68999999761581421), 1f, 0.75f).ToVector3() * 1.2f;
                  R = vector3_2.X;
                  G = vector3_2.Y;
                  B = vector3_2.Z;
                  break;
                case 38:
                  R = 1f;
                  G = 0.97f;
                  B = 0.85f;
                  break;
                case 39:
                  R = 0.55f;
                  G = 0.45f;
                  B = 0.95f;
                  break;
                case 40:
                  R = 1f;
                  G = 0.6f;
                  B = 0.1f;
                  break;
                case 41:
                  R = 0.3f;
                  G = 0.75f;
                  B = 0.55f;
                  break;
                case 42:
                  R = 0.9f;
                  G = 0.55f;
                  B = 0.7f;
                  break;
                case 43:
                  R = 0.55f;
                  G = 0.85f;
                  B = 1f;
                  break;
                case 44:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 45:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 46:
                  R = 0.4f;
                  G = 0.8f;
                  B = 0.9f;
                  break;
                case 47:
                  R = 1f;
                  G = 1f;
                  B = 1f;
                  break;
                case 48:
                  R = 0.95f;
                  G = 0.5f;
                  B = 0.4f;
                  break;
                default:
                  R = 1f;
                  G = 0.95f;
                  B = 0.8f;
                  break;
              }
            }
            else
              break;
            break;
          case 35:
            if (tile.frameX < (short) 36)
            {
              R = 0.75f;
              G = 0.6f;
              B = 0.3f;
              break;
            }
            break;
          case 37:
            R = 0.56f;
            G = 0.43f;
            B = 0.15f;
            break;
          case 42:
            if (tile.frameX == (short) 0)
            {
              switch ((int) tile.frameY / 36)
              {
                case 0:
                  R = 0.7f;
                  G = 0.65f;
                  B = 0.55f;
                  break;
                case 1:
                  R = 0.9f;
                  G = 0.75f;
                  B = 0.6f;
                  break;
                case 2:
                  R = 0.8f;
                  G = 0.6f;
                  B = 0.6f;
                  break;
                case 3:
                  R = 0.65f;
                  G = 0.5f;
                  B = 0.2f;
                  break;
                case 4:
                  R = 0.5f;
                  G = 0.7f;
                  B = 0.4f;
                  break;
                case 5:
                  R = 0.9f;
                  G = 0.4f;
                  B = 0.2f;
                  break;
                case 6:
                  R = 0.7f;
                  G = 0.75f;
                  B = 0.3f;
                  break;
                case 7:
                  float num2 = Main.demonTorch * 0.2f;
                  R = 0.9f - num2;
                  G = 0.9f - num2;
                  B = 0.7f + num2;
                  break;
                case 8:
                  R = 0.75f;
                  G = 0.6f;
                  B = 0.3f;
                  break;
                case 9:
                  float num3 = 1f;
                  float num4 = 0.3f;
                  B = 0.5f + Main.demonTorch * 0.2f;
                  R = num3 - Main.demonTorch * 0.1f;
                  G = num4 - Main.demonTorch * 0.2f;
                  break;
                case 11:
                  R = 0.85f;
                  G = 0.6f;
                  B = 1f;
                  break;
                case 14:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 15:
                case 16:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 17:
                  R = 1f;
                  G = 0.97f;
                  B = 0.85f;
                  break;
                case 18:
                  R = 0.75f;
                  G = 0.85f;
                  B = 1f;
                  break;
                case 21:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 22:
                  R = 1f;
                  G = 1f;
                  B = 0.6f;
                  break;
                case 23:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 27:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 28:
                  R = 0.37f;
                  G = 0.8f;
                  B = 1f;
                  break;
                case 29:
                  R = 0.0f;
                  G = 0.9f;
                  B = 1f;
                  break;
                case 30:
                  R = 0.25f;
                  G = 0.7f;
                  B = 1f;
                  break;
                case 32:
                  R = (float) (0.5 * (double) Main.demonTorch + 1.0 * (1.0 - (double) Main.demonTorch));
                  G = 0.3f;
                  B = (float) (1.0 * (double) Main.demonTorch + 0.5 * (1.0 - (double) Main.demonTorch));
                  break;
                case 35:
                  R = 0.7f;
                  G = 0.6f;
                  B = 0.9f;
                  break;
                case 36:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 37:
                  Vector3 vector3_3 = Main.hslToRgb((float) ((double) Main.demonTorch * 0.11999999731779099 + 0.68999999761581421), 1f, 0.75f).ToVector3() * 1.2f;
                  R = vector3_3.X;
                  G = vector3_3.Y;
                  B = vector3_3.Z;
                  break;
                case 38:
                  R = 1f;
                  G = 0.97f;
                  B = 0.85f;
                  break;
                case 39:
                  R = 0.55f;
                  G = 0.45f;
                  B = 0.95f;
                  break;
                case 40:
                  R = 1f;
                  G = 0.6f;
                  B = 0.1f;
                  break;
                case 41:
                  R = 0.3f;
                  G = 0.75f;
                  B = 0.55f;
                  break;
                case 42:
                  R = 0.9f;
                  G = 0.55f;
                  B = 0.7f;
                  break;
                case 43:
                  R = 0.55f;
                  G = 0.85f;
                  B = 1f;
                  break;
                case 44:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 45:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 46:
                  R = 0.4f;
                  G = 0.8f;
                  B = 0.9f;
                  break;
                case 47:
                  R = 1f;
                  G = 1f;
                  B = 1f;
                  break;
                case 48:
                  R = 0.95f;
                  G = 0.5f;
                  B = 0.4f;
                  break;
                default:
                  R = 1f;
                  G = 1f;
                  B = 1f;
                  break;
              }
            }
            else
              break;
            break;
          case 49:
            if (tile.frameX == (short) 0)
            {
              R = 0.0f;
              G = 0.35f;
              B = 0.8f;
              break;
            }
            break;
          case 61:
            if (tile.frameX == (short) 144)
            {
              float num5 = (float) (1.0 + (double) (270 - (int) Main.mouseTextColor) / 400.0);
              float num6 = (float) (0.800000011920929 - (double) (270 - (int) Main.mouseTextColor) / 400.0);
              R = 0.42f * num6;
              G = 0.81f * num5;
              B = 0.52f * num6;
              break;
            }
            break;
          case 70:
          case 71:
          case 72:
          case 190:
          case 348:
          case 349:
          case 528:
          case 578:
            if (tile.type != (ushort) 349 || tile.frameX >= (short) 36)
            {
              float num7 = (float) localRandom.Next(28, 42) * 0.005f + (float) (270 - (int) Main.mouseTextColor) / 1000f;
              if (tile.color() == (byte) 0)
              {
                R = 0.0f;
                G = (float) (0.20000000298023224 + (double) num7 / 2.0);
                B = 1f;
                break;
              }
              Color color = WorldGen.paintColor((int) tile.color());
              R = (float) color.R / (float) byte.MaxValue;
              G = (float) color.G / (float) byte.MaxValue;
              B = (float) color.B / (float) byte.MaxValue;
              break;
            }
            break;
          case 77:
            R = 0.75f;
            G = 0.45f;
            B = 0.25f;
            break;
          case 83:
            if (tile.frameX == (short) 18 && !Main.dayTime)
            {
              R = 0.1f;
              G = 0.4f;
              B = 0.6f;
            }
            if (tile.frameX == (short) 90 && !Main.raining && Main.time > 40500.0)
            {
              R = 0.9f;
              G = 0.72f;
              B = 0.18f;
              break;
            }
            break;
          case 84:
            switch ((int) tile.frameX / 18)
            {
              case 2:
                float num8 = (float) (270 - (int) Main.mouseTextColor) / 800f;
                if ((double) num8 > 1.0)
                  num8 = 1f;
                else if ((double) num8 < 0.0)
                  num8 = 0.0f;
                R = num8 * 0.7f;
                G = num8;
                B = num8 * 0.1f;
                break;
              case 5:
                float num9 = 0.9f;
                R = num9;
                G = num9 * 0.8f;
                B = num9 * 0.2f;
                break;
              case 6:
                float num10 = 0.08f;
                G = num10 * 0.8f;
                B = num10;
                break;
            }
            break;
          case 92:
            if (tile.frameY <= (short) 18 && tile.frameX == (short) 0)
            {
              R = 1f;
              G = 1f;
              B = 1f;
              break;
            }
            break;
          case 93:
            if (tile.frameX == (short) 0)
            {
              switch ((int) tile.frameY / 54)
              {
                case 1:
                  R = 0.95f;
                  G = 0.95f;
                  B = 0.5f;
                  break;
                case 2:
                  R = 0.85f;
                  G = 0.6f;
                  B = 1f;
                  break;
                case 3:
                  R = 0.75f;
                  G = 1f;
                  B = 0.6f;
                  break;
                case 4:
                case 5:
                  R = 0.75f;
                  G = 0.85f;
                  B = 1f;
                  break;
                case 6:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 7:
                  R = 1f;
                  G = 0.97f;
                  B = 0.85f;
                  break;
                case 9:
                  R = 1f;
                  G = 1f;
                  B = 0.7f;
                  break;
                case 10:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 12:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 13:
                  R = 1f;
                  G = 1f;
                  B = 0.6f;
                  break;
                case 14:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 18:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 19:
                  R = 0.37f;
                  G = 0.8f;
                  B = 1f;
                  break;
                case 20:
                  R = 0.0f;
                  G = 0.9f;
                  B = 1f;
                  break;
                case 21:
                  R = 0.25f;
                  G = 0.7f;
                  B = 1f;
                  break;
                case 23:
                  R = (float) (0.5 * (double) Main.demonTorch + 1.0 * (1.0 - (double) Main.demonTorch));
                  G = 0.3f;
                  B = (float) (1.0 * (double) Main.demonTorch + 0.5 * (1.0 - (double) Main.demonTorch));
                  break;
                case 24:
                  R = 0.35f;
                  G = 0.5f;
                  B = 0.3f;
                  break;
                case 25:
                  R = 0.34f;
                  G = 0.4f;
                  B = 0.31f;
                  break;
                case 26:
                  R = 0.25f;
                  G = 0.32f;
                  B = 0.5f;
                  break;
                case 29:
                  R = 0.9f;
                  G = 0.75f;
                  B = 1f;
                  break;
                case 30:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 31:
                  Vector3 vector3_4 = Main.hslToRgb((float) ((double) Main.demonTorch * 0.11999999731779099 + 0.68999999761581421), 1f, 0.75f).ToVector3() * 1.2f;
                  R = vector3_4.X;
                  G = vector3_4.Y;
                  B = vector3_4.Z;
                  break;
                case 32:
                  R = 1f;
                  G = 0.97f;
                  B = 0.85f;
                  break;
                case 33:
                  R = 0.55f;
                  G = 0.45f;
                  B = 0.95f;
                  break;
                case 34:
                  R = 1f;
                  G = 0.6f;
                  B = 0.1f;
                  break;
                case 35:
                  R = 0.3f;
                  G = 0.75f;
                  B = 0.55f;
                  break;
                case 36:
                  R = 0.9f;
                  G = 0.55f;
                  B = 0.7f;
                  break;
                case 37:
                  R = 0.55f;
                  G = 0.85f;
                  B = 1f;
                  break;
                case 38:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 39:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 40:
                  R = 0.4f;
                  G = 0.8f;
                  B = 0.9f;
                  break;
                case 41:
                  R = 1f;
                  G = 1f;
                  B = 1f;
                  break;
                case 42:
                  R = 0.95f;
                  G = 0.5f;
                  B = 0.4f;
                  break;
                default:
                  R = 1f;
                  G = 0.97f;
                  B = 0.85f;
                  break;
              }
            }
            else
              break;
            break;
          case 95:
            if (tile.frameX < (short) 36)
            {
              R = 1f;
              G = 0.95f;
              B = 0.8f;
              break;
            }
            break;
          case 96:
            if (tile.frameX >= (short) 36)
            {
              R = 0.5f;
              G = 0.35f;
              B = 0.1f;
              break;
            }
            break;
          case 98:
            if (tile.frameY == (short) 0)
            {
              R = 1f;
              G = 0.97f;
              B = 0.85f;
              break;
            }
            break;
          case 100:
          case 173:
            if (tile.frameX < (short) 36)
            {
              switch ((int) tile.frameY / 36)
              {
                case 1:
                  R = 0.95f;
                  G = 0.95f;
                  B = 0.5f;
                  break;
                case 2:
                  R = 0.85f;
                  G = 0.6f;
                  B = 1f;
                  break;
                case 3:
                  R = 1f;
                  G = 0.6f;
                  B = 0.6f;
                  break;
                case 5:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 6:
                case 7:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 8:
                  R = 1f;
                  G = 0.97f;
                  B = 0.85f;
                  break;
                case 9:
                  R = 0.75f;
                  G = 0.85f;
                  B = 1f;
                  break;
                case 11:
                  R = 1f;
                  G = 1f;
                  B = 0.7f;
                  break;
                case 12:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 13:
                  R = 1f;
                  G = 1f;
                  B = 0.6f;
                  break;
                case 14:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 18:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 19:
                  R = 0.37f;
                  G = 0.8f;
                  B = 1f;
                  break;
                case 20:
                  R = 0.0f;
                  G = 0.9f;
                  B = 1f;
                  break;
                case 21:
                  R = 0.25f;
                  G = 0.7f;
                  B = 1f;
                  break;
                case 22:
                  R = 0.35f;
                  G = 0.5f;
                  B = 0.3f;
                  break;
                case 23:
                  R = 0.34f;
                  G = 0.4f;
                  B = 0.31f;
                  break;
                case 24:
                  R = 0.25f;
                  G = 0.32f;
                  B = 0.5f;
                  break;
                case 25:
                  R = (float) (0.5 * (double) Main.demonTorch + 1.0 * (1.0 - (double) Main.demonTorch));
                  G = 0.3f;
                  B = (float) (1.0 * (double) Main.demonTorch + 0.5 * (1.0 - (double) Main.demonTorch));
                  break;
                case 29:
                  R = 0.9f;
                  G = 0.75f;
                  B = 1f;
                  break;
                case 30:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 31:
                  Vector3 vector3_5 = Main.hslToRgb((float) ((double) Main.demonTorch * 0.11999999731779099 + 0.68999999761581421), 1f, 0.75f).ToVector3() * 1.2f;
                  R = vector3_5.X;
                  G = vector3_5.Y;
                  B = vector3_5.Z;
                  break;
                case 32:
                  R = 1f;
                  G = 0.97f;
                  B = 0.85f;
                  break;
                case 33:
                  R = 0.55f;
                  G = 0.45f;
                  B = 0.95f;
                  break;
                case 34:
                  R = 1f;
                  G = 0.6f;
                  B = 0.1f;
                  break;
                case 35:
                  R = 0.3f;
                  G = 0.75f;
                  B = 0.55f;
                  break;
                case 36:
                  R = 0.9f;
                  G = 0.55f;
                  B = 0.7f;
                  break;
                case 37:
                  R = 0.55f;
                  G = 0.85f;
                  B = 1f;
                  break;
                case 38:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 39:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
                case 40:
                  R = 0.4f;
                  G = 0.8f;
                  B = 0.9f;
                  break;
                case 41:
                  R = 1f;
                  G = 1f;
                  B = 1f;
                  break;
                case 42:
                  R = 0.95f;
                  G = 0.5f;
                  B = 0.4f;
                  break;
                default:
                  R = 1f;
                  G = 0.95f;
                  B = 0.65f;
                  break;
              }
            }
            else
              break;
            break;
          case 125:
            float num11 = (float) localRandom.Next(28, 42) * 0.01f + (float) (270 - (int) Main.mouseTextColor) / 800f;
            G = lightColor.Y = 0.3f * num11;
            B = lightColor.Z = 0.6f * num11;
            break;
          case 126:
            if (tile.frameX < (short) 36)
            {
              R = (float) Main.DiscoR / (float) byte.MaxValue;
              G = (float) Main.DiscoG / (float) byte.MaxValue;
              B = (float) Main.DiscoB / (float) byte.MaxValue;
              break;
            }
            break;
          case 129:
            switch ((int) tile.frameX / 18 % 3)
            {
              case 0:
                R = 0.0f;
                G = 0.05f;
                B = 0.25f;
                break;
              case 1:
                R = 0.2f;
                G = 0.0f;
                B = 0.15f;
                break;
              case 2:
                R = 0.1f;
                G = 0.0f;
                B = 0.2f;
                break;
            }
            break;
          case 149:
            if (tile.frameX <= (short) 36)
            {
              switch ((int) tile.frameX / 18)
              {
                case 0:
                  R = 0.1f;
                  G = 0.2f;
                  B = 0.5f;
                  break;
                case 1:
                  R = 0.5f;
                  G = 0.1f;
                  B = 0.1f;
                  break;
                case 2:
                  R = 0.2f;
                  G = 0.5f;
                  B = 0.1f;
                  break;
              }
              R *= (float) localRandom.Next(970, 1031) * (1f / 1000f);
              G *= (float) localRandom.Next(970, 1031) * (1f / 1000f);
              B *= (float) localRandom.Next(970, 1031) * (1f / 1000f);
              break;
            }
            break;
          case 160:
            R = (float) ((double) Main.DiscoR / (double) byte.MaxValue * 0.25);
            G = (float) ((double) Main.DiscoG / (double) byte.MaxValue * 0.25);
            B = (float) ((double) Main.DiscoB / (double) byte.MaxValue * 0.25);
            break;
          case 171:
            if (tile.frameX < (short) 10)
            {
              x -= (int) tile.frameX;
              y -= (int) tile.frameY;
            }
            switch (((int) Main.tile[x, y].frameY & 15360) >> 10)
            {
              case 1:
                R = 0.1f;
                G = 0.1f;
                B = 0.1f;
                break;
              case 2:
                R = 0.2f;
                break;
              case 3:
                G = 0.2f;
                break;
              case 4:
                B = 0.2f;
                break;
              case 5:
                R = 0.125f;
                G = 0.125f;
                break;
              case 6:
                R = 0.2f;
                G = 0.1f;
                break;
              case 7:
                R = 0.125f;
                G = 0.125f;
                break;
              case 8:
                R = 0.08f;
                G = 0.175f;
                break;
              case 9:
                G = 0.125f;
                B = 0.125f;
                break;
              case 10:
                R = 0.125f;
                B = 0.125f;
                break;
              case 11:
                R = 0.1f;
                G = 0.1f;
                B = 0.2f;
                break;
              default:
                double num12;
                B = (float) (num12 = 0.0);
                G = (float) num12;
                R = (float) num12;
                break;
            }
            R *= 0.5f;
            G *= 0.5f;
            B *= 0.5f;
            break;
          case 174:
            if (tile.frameX == (short) 0)
            {
              R = 1f;
              G = 0.95f;
              B = 0.65f;
              break;
            }
            break;
          case 184:
            if (tile.frameX == (short) 110)
            {
              R = 0.25f;
              G = 0.1f;
              B = 0.0f;
            }
            if (tile.frameX == (short) 132)
            {
              R = 0.0f;
              G = 0.25f;
              B = 0.0f;
            }
            if (tile.frameX == (short) 154)
            {
              R = 0.0f;
              G = 0.16f;
              B = 0.34f;
            }
            if (tile.frameX == (short) 176)
            {
              R = 0.3f;
              G = 0.0f;
              B = 0.17f;
            }
            if (tile.frameX == (short) 198)
            {
              R = 0.3f;
              G = 0.0f;
              B = 0.35f;
            }
            if (tile.frameX == (short) 220)
            {
              R = (float) ((double) Main.DiscoR / (double) byte.MaxValue * 0.25);
              G = (float) ((double) Main.DiscoG / (double) byte.MaxValue * 0.25);
              B = (float) ((double) Main.DiscoB / (double) byte.MaxValue * 0.25);
              break;
            }
            break;
          case 204:
          case 347:
            R = 0.35f;
            break;
          case 209:
            if (tile.frameX == (short) 234 || tile.frameX == (short) 252)
            {
              Vector3 vector3_6 = PortalHelper.GetPortalColor(Main.myPlayer, 0).ToVector3() * 0.65f;
              R = vector3_6.X;
              G = vector3_6.Y;
              B = vector3_6.Z;
              break;
            }
            if (tile.frameX == (short) 306 || tile.frameX == (short) 324)
            {
              Vector3 vector3_7 = PortalHelper.GetPortalColor(Main.myPlayer, 1).ToVector3() * 0.65f;
              R = vector3_7.X;
              G = vector3_7.Y;
              B = vector3_7.Z;
              break;
            }
            break;
          case 215:
            if (tile.frameY < (short) 36)
            {
              float num13 = (float) localRandom.Next(28, 42) * 0.005f + (float) (270 - (int) Main.mouseTextColor) / 700f;
              switch ((int) tile.frameX / 54)
              {
                case 1:
                  R = 0.7f;
                  G = 1f;
                  B = 0.5f;
                  break;
                case 2:
                  R = (float) (0.5 * (double) Main.demonTorch + 1.0 * (1.0 - (double) Main.demonTorch));
                  G = 0.3f;
                  B = (float) (1.0 * (double) Main.demonTorch + 0.5 * (1.0 - (double) Main.demonTorch));
                  break;
                case 3:
                  R = 0.45f;
                  G = 0.75f;
                  B = 1f;
                  break;
                case 4:
                  R = 1.15f;
                  G = 1.15f;
                  B = 0.5f;
                  break;
                case 5:
                  R = (float) Main.DiscoR / (float) byte.MaxValue;
                  G = (float) Main.DiscoG / (float) byte.MaxValue;
                  B = (float) Main.DiscoB / (float) byte.MaxValue;
                  break;
                case 6:
                  R = 0.75f;
                  G = 1.28249991f;
                  B = 1.2f;
                  break;
                case 7:
                  R = 0.95f;
                  G = 0.65f;
                  B = 1.3f;
                  break;
                case 8:
                  R = 1.4f;
                  G = 0.85f;
                  B = 0.55f;
                  break;
                case 9:
                  R = 0.25f;
                  G = 1.3f;
                  B = 0.8f;
                  break;
                case 10:
                  R = 0.95f;
                  G = 0.4f;
                  B = 1.4f;
                  break;
                case 11:
                  R = 1.4f;
                  G = 0.7f;
                  B = 0.5f;
                  break;
                case 12:
                  R = 1.25f;
                  G = 0.6f;
                  B = 1.2f;
                  break;
                case 13:
                  R = 0.75f;
                  G = 1.45f;
                  B = 0.9f;
                  break;
                case 14:
                  R = 0.25f;
                  G = 0.65f;
                  B = 1f;
                  break;
                case 15:
                  TorchID.TorchColor(23, out R, out G, out B);
                  break;
                default:
                  R = 0.9f;
                  G = 0.3f;
                  B = 0.1f;
                  break;
              }
              R += num13;
              G += num13;
              B += num13;
              break;
            }
            break;
          case 235:
            if ((double) lightColor.X < 0.6)
              lightColor.X = 0.6f;
            if ((double) lightColor.Y < 0.6)
            {
              lightColor.Y = 0.6f;
              break;
            }
            break;
          case 237:
            R = 0.1f;
            G = 0.1f;
            break;
          case 238:
            if ((double) lightColor.X < 0.5)
              lightColor.X = 0.5f;
            if ((double) lightColor.Z < 0.5)
            {
              lightColor.Z = 0.5f;
              break;
            }
            break;
          case 262:
            R = 0.75f;
            B = 0.75f;
            break;
          case 263:
            R = 0.75f;
            G = 0.75f;
            break;
          case 264:
            B = 0.75f;
            break;
          case 265:
            G = 0.75f;
            break;
          case 266:
            R = 0.75f;
            break;
          case 267:
            R = 0.75f;
            G = 0.75f;
            B = 0.75f;
            break;
          case 268:
            R = 0.75f;
            G = 0.375f;
            break;
          case 270:
            R = 0.73f;
            G = 1f;
            B = 0.41f;
            break;
          case 271:
            R = 0.45f;
            G = 0.95f;
            B = 1f;
            break;
          case 286:
          case 619:
            R = 0.1f;
            G = 0.2f;
            B = 0.7f;
            break;
          case 316:
          case 317:
          case 318:
            int index = (x - (int) tile.frameX / 18) / 2 * ((y - (int) tile.frameY / 18) / 3) % Main.cageFrames;
            bool flag1 = Main.jellyfishCageMode[(int) tile.type - 316, index] == (byte) 2;
            if (tile.type == (ushort) 316)
            {
              if (flag1)
              {
                R = 0.2f;
                G = 0.3f;
                B = 0.8f;
              }
              else
              {
                R = 0.1f;
                G = 0.2f;
                B = 0.5f;
              }
            }
            if (tile.type == (ushort) 317)
            {
              if (flag1)
              {
                R = 0.2f;
                G = 0.7f;
                B = 0.3f;
              }
              else
              {
                R = 0.05f;
                G = 0.45f;
                B = 0.1f;
              }
            }
            if (tile.type == (ushort) 318)
            {
              if (flag1)
              {
                R = 0.7f;
                G = 0.2f;
                B = 0.5f;
                break;
              }
              R = 0.4f;
              G = 0.1f;
              B = 0.25f;
              break;
            }
            break;
          case 327:
            float num14 = 0.5f + (float) (270 - (int) Main.mouseTextColor) / 1500f + (float) localRandom.Next(0, 50) * 0.0005f;
            R = 1f * num14;
            G = 0.5f * num14;
            B = 0.1f * num14;
            break;
          case 336:
            R = 0.85f;
            G = 0.5f;
            B = 0.3f;
            break;
          case 340:
            R = 0.45f;
            G = 1f;
            B = 0.45f;
            break;
          case 341:
            R = (float) (0.40000000596046448 * (double) Main.demonTorch + 0.60000002384185791 * (1.0 - (double) Main.demonTorch));
            G = 0.35f;
            B = (float) (1.0 * (double) Main.demonTorch + 0.60000002384185791 * (1.0 - (double) Main.demonTorch));
            break;
          case 342:
            R = 0.5f;
            G = 0.5f;
            B = 1.1f;
            break;
          case 343:
            R = 0.85f;
            G = 0.85f;
            B = 0.3f;
            break;
          case 344:
            R = 0.6f;
            G = 1.026f;
            B = 0.960000038f;
            break;
          case 350:
            double num15 = Main.timeForVisualEffects * 0.08;
            double num16;
            R = (float) (num16 = -Math.Cos((int) (num15 / 6.283) % 3 == 1 ? num15 : 0.0) * 0.1 + 0.1);
            G = (float) num16;
            B = (float) num16;
            break;
          case 354:
            R = 0.65f;
            G = 0.35f;
            B = 0.15f;
            break;
          case 356:
            if (Main.sundialCooldown == 0)
            {
              R = 0.45f;
              G = 0.25f;
              B = 0.0f;
              break;
            }
            break;
          case 370:
            R = 0.32f;
            G = 0.16f;
            B = 0.12f;
            break;
          case 372:
            if (tile.frameX == (short) 0)
            {
              R = 0.9f;
              G = 0.1f;
              B = 0.75f;
              break;
            }
            break;
          case 381:
          case 517:
          case 687:
            R = 0.25f;
            G = 0.1f;
            B = 0.0f;
            break;
          case 390:
            R = 0.4f;
            G = 0.2f;
            B = 0.1f;
            break;
          case 391:
            R = 0.3f;
            G = 0.1f;
            B = 0.25f;
            break;
          case 405:
            if (tile.frameX < (short) 54)
            {
              float num17 = (float) localRandom.Next(28, 42) * 0.005f + (float) (270 - (int) Main.mouseTextColor) / 700f;
              float num18;
              float num19;
              float num20;
              switch ((int) tile.frameX / 54)
              {
                case 1:
                  num18 = 0.7f;
                  num19 = 1f;
                  num20 = 0.5f;
                  break;
                case 2:
                  num18 = (float) (0.5 * (double) Main.demonTorch + 1.0 * (1.0 - (double) Main.demonTorch));
                  num19 = 0.3f;
                  num20 = (float) (1.0 * (double) Main.demonTorch + 0.5 * (1.0 - (double) Main.demonTorch));
                  break;
                case 3:
                  num18 = 0.45f;
                  num19 = 0.75f;
                  num20 = 1f;
                  break;
                case 4:
                  num18 = 1.15f;
                  num19 = 1.15f;
                  num20 = 0.5f;
                  break;
                case 5:
                  num18 = (float) Main.DiscoR / (float) byte.MaxValue;
                  num19 = (float) Main.DiscoG / (float) byte.MaxValue;
                  num20 = (float) Main.DiscoB / (float) byte.MaxValue;
                  break;
                default:
                  num18 = 0.9f;
                  num19 = 0.3f;
                  num20 = 0.1f;
                  break;
              }
              R = num18 + num17;
              G = num19 + num17;
              B = num20 + num17;
              break;
            }
            break;
          case 415:
            R = 0.7f;
            G = 0.5f;
            B = 0.1f;
            break;
          case 416:
            R = 0.0f;
            G = 0.6f;
            B = 0.7f;
            break;
          case 417:
            R = 0.6f;
            G = 0.2f;
            B = 0.6f;
            break;
          case 418:
            R = 0.6f;
            G = 0.6f;
            B = 0.9f;
            break;
          case 429:
            int num21 = (int) tile.frameX / 18;
            bool flag2 = num21 % 2 >= 1;
            bool flag3 = num21 % 4 >= 2;
            bool flag4 = num21 % 8 >= 4;
            int num22 = num21 % 16 >= 8 ? 1 : 0;
            if (flag2)
              R += 0.5f;
            if (flag3)
              G += 0.5f;
            if (flag4)
              B += 0.5f;
            if (num22 != 0)
            {
              R += 0.2f;
              G += 0.2f;
              break;
            }
            break;
          case 463:
            R = 0.2f;
            G = 0.4f;
            B = 0.8f;
            break;
          case 491:
            R = 0.5f;
            G = 0.4f;
            B = 0.7f;
            break;
          case 500:
            R = 0.525f;
            G = 0.375f;
            B = 0.075f;
            break;
          case 501:
            R = 0.0f;
            G = 0.45f;
            B = 0.525f;
            break;
          case 502:
            R = 0.45f;
            G = 0.15f;
            B = 0.45f;
            break;
          case 503:
            R = 0.45f;
            G = 0.45f;
            B = 0.675f;
            break;
          case 519:
            if (tile.frameY == (short) 90)
            {
              if (tile.color() == (byte) 0)
              {
                float num23 = (float) localRandom.Next(28, 42) * 0.005f + (float) (270 - (int) Main.mouseTextColor) / 1000f;
                R = 0.1f;
                G = (float) (0.20000000298023224 + (double) num23 / 2.0);
                B = 0.7f + num23;
                break;
              }
              Color color = WorldGen.paintColor((int) tile.color());
              R = (float) color.R / (float) byte.MaxValue;
              G = (float) color.G / (float) byte.MaxValue;
              B = (float) color.B / (float) byte.MaxValue;
              break;
            }
            break;
          case 534:
          case 535:
          case 689:
            R = 0.0f;
            G = 0.25f;
            B = 0.0f;
            break;
          case 536:
          case 537:
          case 690:
            R = 0.0f;
            G = 0.16f;
            B = 0.34f;
            break;
          case 539:
          case 540:
          case 688:
            R = 0.3f;
            G = 0.0f;
            B = 0.17f;
            break;
          case 548:
            if ((int) tile.frameX / 54 >= 7)
            {
              R = 0.7f;
              G = 0.3f;
              B = 0.2f;
              break;
            }
            break;
          case 564:
            if (tile.frameX < (short) 36)
            {
              R = 0.05f;
              G = 0.3f;
              B = 0.55f;
              break;
            }
            break;
          case 568:
            R = 1f;
            G = 0.61f;
            B = 0.65f;
            break;
          case 569:
            R = 0.12f;
            G = 1f;
            B = 0.66f;
            break;
          case 570:
            R = 0.57f;
            G = 0.57f;
            B = 1f;
            break;
          case 572:
            switch ((int) tile.frameY / 36)
            {
              case 0:
                R = 0.9f;
                G = 0.5f;
                B = 0.7f;
                break;
              case 1:
                R = 0.7f;
                G = 0.55f;
                B = 0.96f;
                break;
              case 2:
                R = 0.45f;
                G = 0.96f;
                B = 0.95f;
                break;
              case 3:
                R = 0.5f;
                G = 0.96f;
                B = 0.62f;
                break;
              case 4:
                R = 0.47f;
                G = 0.69f;
                B = 0.95f;
                break;
              case 5:
                R = 0.92f;
                G = 0.57f;
                B = 0.51f;
                break;
            }
            break;
          case 580:
            R = 0.7f;
            G = 0.3f;
            B = 0.2f;
            break;
          case 581:
            R = 1f;
            G = 0.75f;
            B = 0.5f;
            break;
          case 582:
          case 598:
            R = 0.7f;
            G = 0.2f;
            B = 0.1f;
            break;
          case 592:
            if (tile.frameY > (short) 0)
            {
              float num24 = (float) localRandom.Next(28, 42) * 0.005f + (float) (270 - (int) Main.mouseTextColor) / 700f;
              float num25 = 1.35f;
              float num26 = 0.45f;
              float num27 = 0.15f;
              R = num25 + num24;
              G = num26 + num24;
              B = num27 + num24;
              break;
            }
            break;
          case 593:
            if (tile.frameX < (short) 18)
            {
              R = 0.8f;
              G = 0.3f;
              B = 0.1f;
              break;
            }
            break;
          case 594:
            if (tile.frameX < (short) 36)
            {
              R = 0.8f;
              G = 0.3f;
              B = 0.1f;
              break;
            }
            break;
          case 597:
            switch ((int) tile.frameX / 54)
            {
              case 0:
                R = 0.05f;
                G = 0.8f;
                B = 0.3f;
                break;
              case 1:
                R = 0.7f;
                G = 0.8f;
                B = 0.05f;
                break;
              case 2:
                R = 0.7f;
                G = 0.5f;
                B = 0.9f;
                break;
              case 3:
                R = 0.6f;
                G = 0.6f;
                B = 0.8f;
                break;
              case 4:
                R = 0.4f;
                G = 0.4f;
                B = 1.15f;
                break;
              case 5:
                R = 0.85f;
                G = 0.45f;
                B = 0.1f;
                break;
              case 6:
                R = 0.8f;
                G = 0.8f;
                B = 1f;
                break;
              case 7:
                R = 0.5f;
                G = 0.8f;
                B = 1.2f;
                break;
            }
            R *= 0.75f;
            G *= 0.75f;
            B *= 0.75f;
            break;
          case 613:
          case 614:
            R = 0.7f;
            G = 0.3f;
            B = 0.2f;
            break;
          case 620:
            Color color1 = new Color(230, 230, 230, 0).MultiplyRGBA(Main.hslToRgb((float) ((double) Main.GlobalTimeWrappedHourly * 0.5 % 1.0), 1f, 0.5f)) * 0.4f;
            R = (float) color1.R / (float) byte.MaxValue;
            G = (float) color1.G / (float) byte.MaxValue;
            B = (float) color1.B / (float) byte.MaxValue;
            break;
          case 625:
          case 626:
          case 691:
            R = 0.3f;
            G = 0.0f;
            B = 0.35f;
            break;
          case 627:
          case 628:
          case 692:
            R = (float) ((double) Main.DiscoR / (double) byte.MaxValue * 0.25);
            G = (float) ((double) Main.DiscoG / (double) byte.MaxValue * 0.25);
            B = (float) ((double) Main.DiscoB / (double) byte.MaxValue * 0.25);
            break;
          case 633:
          case 637:
          case 638:
            R = 0.325f;
            G = 0.15f;
            B = 0.05f;
            break;
          case 634:
            R = 0.65f;
            G = 0.3f;
            B = 0.1f;
            break;
          case 646:
            if (tile.frameX == (short) 0)
            {
              R = 0.2f;
              G = 0.3f;
              B = 0.32f;
              break;
            }
            break;
          case 656:
            R = 0.2f;
            G = 0.55f;
            B = 0.5f;
            break;
          case 658:
            if (!tile.invisibleBlock())
            {
              TorchID.TorchColor(23, out R, out G, out B);
              switch ((int) tile.frameY / 54)
              {
                case 1:
                  R *= 0.3f;
                  G *= 0.3f;
                  B *= 0.3f;
                  break;
                case 2:
                  R *= 0.1f;
                  G *= 0.1f;
                  B *= 0.1f;
                  break;
                default:
                  R *= 0.2f;
                  G *= 0.2f;
                  B *= 0.2f;
                  break;
              }
            }
            else
              break;
            break;
          case 659:
          case 667:
            Vector4 shimmerBaseColor = LiquidRenderer.GetShimmerBaseColor((float) x, (float) y);
            R = shimmerBaseColor.X;
            G = shimmerBaseColor.Y;
            B = shimmerBaseColor.Z;
            break;
          case 660:
            TorchID.TorchColor(23, out R, out G, out B);
            break;
          case 663:
            if (Main.moondialCooldown == 0)
            {
              R = 0.0f;
              G = 0.25f;
              B = 0.45f;
              break;
            }
            break;
        }
      }
      if ((double) lightColor.X < (double) R)
        lightColor.X = R;
      if ((double) lightColor.Y < (double) G)
        lightColor.Y = G;
      if ((double) lightColor.Z >= (double) B)
        return;
      lightColor.Z = B;
    }

    private void ApplySurfaceLight(Tile tile, int x, int y, ref Vector3 lightColor)
    {
      float num1 = 0.0f;
      float num2 = 0.0f;
      float num3 = 0.0f;
      float num4 = (float) Main.tileColor.R / (float) byte.MaxValue;
      float num5 = (float) Main.tileColor.G / (float) byte.MaxValue;
      float num6 = (float) Main.tileColor.B / (float) byte.MaxValue;
      float num7 = (float) (((double) num4 + (double) num5 + (double) num6) / 3.0);
      if (tile.active() && TileID.Sets.AllowLightInWater[(int) tile.type])
      {
        if ((double) lightColor.X < (double) num7 && (Main.wallLight[(int) tile.wall] || tile.wall == (ushort) 73 || tile.wall == (ushort) 227 || tile.invisibleWall() && !this._drawInvisibleWalls))
        {
          num1 = num4;
          num2 = num5;
          num3 = num6;
        }
      }
      else if ((!tile.active() || !Main.tileNoSunLight[(int) tile.type] || (tile.slope() != (byte) 0 || tile.halfBrick() || tile.invisibleBlock() && !this._drawInvisibleWalls) && Main.tile[x, y - 1].liquid == (byte) 0 && Main.tile[x, y + 1].liquid == (byte) 0 && Main.tile[x - 1, y].liquid == (byte) 0 && Main.tile[x + 1, y].liquid == (byte) 0) && (double) lightColor.X < (double) num7 && (Main.wallLight[(int) tile.wall] || tile.wall == (ushort) 73 || tile.wall == (ushort) 227 || tile.invisibleWall() && !this._drawInvisibleWalls))
      {
        if (tile.liquid < (byte) 200)
        {
          if (!tile.halfBrick() || Main.tile[x, y - 1].liquid < (byte) 200)
          {
            num1 = num4;
            num2 = num5;
            num3 = num6;
          }
        }
        else if ((double) Main.liquidAlpha[13] > 0.0)
        {
          if (Main.rand == null)
            Main.rand = new UnifiedRandom();
          num3 = (float) ((double) num6 * 0.17499999701976776 * (1.0 + (double) Main.rand.NextFloat() * 0.12999999523162842)) * Main.liquidAlpha[13];
        }
      }
      if ((!tile.active() || tile.halfBrick() || !Main.tileNoSunLight[(int) tile.type]) && (tile.wall >= (ushort) 88 && tile.wall <= (ushort) 93 || tile.wall == (ushort) 241) && tile.liquid < byte.MaxValue)
      {
        num1 = num4;
        num2 = num5;
        num3 = num6;
        int num8 = (int) tile.wall - 88;
        if (tile.wall == (ushort) 241)
          num8 = 6;
        switch (num8)
        {
          case 0:
            num1 *= 0.9f;
            num2 *= 0.15f;
            num3 *= 0.9f;
            break;
          case 1:
            num1 *= 0.9f;
            num2 *= 0.9f;
            num3 *= 0.15f;
            break;
          case 2:
            num1 *= 0.15f;
            num2 *= 0.15f;
            num3 *= 0.9f;
            break;
          case 3:
            num1 *= 0.15f;
            num2 *= 0.9f;
            num3 *= 0.15f;
            break;
          case 4:
            num1 *= 0.9f;
            num2 *= 0.15f;
            num3 *= 0.15f;
            break;
          case 5:
            float num9 = 0.2f;
            float num10 = 0.7f - num9;
            num1 *= num10 + (float) Main.DiscoR / (float) byte.MaxValue * num9;
            num2 *= num10 + (float) Main.DiscoG / (float) byte.MaxValue * num9;
            num3 *= num10 + (float) Main.DiscoB / (float) byte.MaxValue * num9;
            break;
          case 6:
            num1 *= 0.9f;
            num2 *= 0.5f;
            num3 *= 0.0f;
            break;
        }
      }
      float num11 = 1f - Main.shimmerDarken;
      float num12 = num1 * num11;
      float num13 = num2 * num11;
      float num14 = num3 * num11;
      if ((double) lightColor.X < (double) num12)
        lightColor.X = num12;
      if ((double) lightColor.Y < (double) num13)
        lightColor.Y = num13;
      if ((double) lightColor.Z >= (double) num14)
        return;
      lightColor.Z = num14;
    }

    private void ApplyHellLight(Tile tile, int x, int y, ref Vector3 lightColor)
    {
      float num1 = 0.0f;
      float num2 = 0.0f;
      float num3 = 0.0f;
      float num4 = (float) (0.550000011920929 + Math.Sin((double) Main.GlobalTimeWrappedHourly * 2.0) * 0.079999998211860657);
      if ((!tile.active() || !Main.tileNoSunLight[(int) tile.type] || (tile.slope() != (byte) 0 || tile.halfBrick()) && Main.tile[x, y - 1].liquid == (byte) 0 && Main.tile[x, y + 1].liquid == (byte) 0 && Main.tile[x - 1, y].liquid == (byte) 0 && Main.tile[x + 1, y].liquid == (byte) 0) && (double) lightColor.X < (double) num4 && (Main.wallLight[(int) tile.wall] || tile.wall == (ushort) 73 || tile.wall == (ushort) 227 || tile.invisibleWall() && !this._drawInvisibleWalls) && tile.liquid < (byte) 200 && (!tile.halfBrick() || Main.tile[x, y - 1].liquid < (byte) 200))
      {
        num1 = num4;
        num2 = num4 * 0.6f;
        num3 = num4 * 0.2f;
      }
      if ((!tile.active() || tile.halfBrick() || !Main.tileNoSunLight[(int) tile.type]) && tile.wall >= (ushort) 88 && tile.wall <= (ushort) 93 && tile.liquid < byte.MaxValue)
      {
        num1 = num4;
        num2 = num4 * 0.6f;
        num3 = num4 * 0.2f;
        switch (tile.wall)
        {
          case 88:
            num1 *= 0.9f;
            num2 *= 0.15f;
            num3 *= 0.9f;
            break;
          case 89:
            num1 *= 0.9f;
            num2 *= 0.9f;
            num3 *= 0.15f;
            break;
          case 90:
            num1 *= 0.15f;
            num2 *= 0.15f;
            num3 *= 0.9f;
            break;
          case 91:
            num1 *= 0.15f;
            num2 *= 0.9f;
            num3 *= 0.15f;
            break;
          case 92:
            num1 *= 0.9f;
            num2 *= 0.15f;
            num3 *= 0.15f;
            break;
          case 93:
            float num5 = 0.2f;
            float num6 = 0.7f - num5;
            num1 *= num6 + (float) Main.DiscoR / (float) byte.MaxValue * num5;
            num2 *= num6 + (float) Main.DiscoG / (float) byte.MaxValue * num5;
            num3 *= num6 + (float) Main.DiscoB / (float) byte.MaxValue * num5;
            break;
        }
      }
      if ((double) lightColor.X < (double) num1)
        lightColor.X = num1;
      if ((double) lightColor.Y < (double) num2)
        lightColor.Y = num2;
      if ((double) lightColor.Z >= (double) num3)
        return;
      lightColor.Z = num3;
    }
  }
}
