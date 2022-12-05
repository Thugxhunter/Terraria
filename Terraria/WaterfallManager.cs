// Decompiled with JetBrains decompiler
// Type: Terraria.WaterfallManager
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.GameContent.Liquid;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.IO;

namespace Terraria
{
  public class WaterfallManager
  {
    private const int minWet = 160;
    private const int maxWaterfallCountDefault = 1000;
    private const int maxLength = 100;
    private const int maxTypes = 26;
    public int maxWaterfallCount = 1000;
    private int qualityMax;
    private int currentMax;
    private WaterfallManager.WaterfallData[] waterfalls = new WaterfallManager.WaterfallData[1000];
    private Asset<Texture2D>[] waterfallTexture = new Asset<Texture2D>[26];
    private int wFallFrCounter;
    private int regularFrame;
    private int wFallFrCounter2;
    private int slowFrame;
    private int rainFrameCounter;
    private int rainFrameForeground;
    private int rainFrameBackground;
    private int snowFrameCounter;
    private int snowFrameForeground;
    private int findWaterfallCount;
    private int waterfallDist = 100;

    public void BindTo(Preferences preferences) => preferences.OnLoad += new Action<Preferences>(this.Configuration_OnLoad);

    private void Configuration_OnLoad(Preferences preferences)
    {
      this.maxWaterfallCount = Math.Max(0, preferences.Get<int>("WaterfallDrawLimit", 1000));
      this.waterfalls = new WaterfallManager.WaterfallData[this.maxWaterfallCount];
    }

    public void LoadContent()
    {
      for (int index = 0; index < 26; ++index)
        this.waterfallTexture[index] = Main.Assets.Request<Texture2D>("Images/Waterfall_" + (object) index, (AssetRequestMode) 2);
    }

    public bool CheckForWaterfall(int i, int j)
    {
      for (int index = 0; index < this.currentMax; ++index)
      {
        if (this.waterfalls[index].x == i && this.waterfalls[index].y == j)
          return true;
      }
      return false;
    }

    public void FindWaterfalls(bool forced = false)
    {
      ++this.findWaterfallCount;
      if (this.findWaterfallCount < 30 && !forced)
        return;
      this.findWaterfallCount = 0;
      this.waterfallDist = (int) (75.0 * (double) Main.gfxQuality) + 25;
      this.qualityMax = (int) ((double) this.maxWaterfallCount * (double) Main.gfxQuality);
      this.currentMax = 0;
      int num1 = (int) ((double) Main.screenPosition.X / 16.0 - 1.0);
      int num2 = (int) (((double) Main.screenPosition.X + (double) Main.screenWidth) / 16.0) + 2;
      int num3 = (int) ((double) Main.screenPosition.Y / 16.0 - 1.0);
      int num4 = (int) (((double) Main.screenPosition.Y + (double) Main.screenHeight) / 16.0) + 2;
      int num5 = num1 - this.waterfallDist;
      int num6 = num2 + this.waterfallDist;
      int num7 = num3 - this.waterfallDist;
      int num8 = num4 + 20;
      if (num5 < 0)
        num5 = 0;
      if (num6 > Main.maxTilesX)
        num6 = Main.maxTilesX;
      if (num7 < 0)
        num7 = 0;
      if (num8 > Main.maxTilesY)
        num8 = Main.maxTilesY;
      for (int index1 = num5; index1 < num6; ++index1)
      {
        for (int index2 = num7; index2 < num8; ++index2)
        {
          Tile tile = Main.tile[index1, index2];
          if (tile == null)
          {
            tile = new Tile();
            Main.tile[index1, index2] = tile;
          }
          if (tile.active())
          {
            if (tile.halfBrick())
            {
              Tile testTile1 = Main.tile[index1, index2 - 1];
              if (testTile1 == null)
              {
                testTile1 = new Tile();
                Main.tile[index1, index2 - 1] = testTile1;
              }
              if (testTile1.liquid < (byte) 16 || WorldGen.SolidTile(testTile1))
              {
                Tile testTile2 = Main.tile[index1 - 1, index2];
                if (testTile2 == null)
                {
                  testTile2 = new Tile();
                  Main.tile[index1 - 1, index2] = testTile2;
                }
                Tile testTile3 = Main.tile[index1 + 1, index2];
                if (testTile3 == null)
                {
                  testTile3 = new Tile();
                  Main.tile[index1 + 1, index2] = testTile3;
                }
                if ((testTile2.liquid > (byte) 160 || testTile3.liquid > (byte) 160) && (testTile2.liquid == (byte) 0 && !WorldGen.SolidTile(testTile2) && testTile2.slope() == (byte) 0 || testTile3.liquid == (byte) 0 && !WorldGen.SolidTile(testTile3) && testTile3.slope() == (byte) 0) && this.currentMax < this.qualityMax)
                {
                  this.waterfalls[this.currentMax].type = 0;
                  this.waterfalls[this.currentMax].type = testTile1.lava() || testTile3.lava() || testTile2.lava() ? 1 : (testTile1.honey() || testTile3.honey() || testTile2.honey() ? 14 : (testTile1.shimmer() || testTile3.shimmer() || testTile2.shimmer() ? 25 : 0));
                  this.waterfalls[this.currentMax].x = index1;
                  this.waterfalls[this.currentMax].y = index2;
                  ++this.currentMax;
                }
              }
            }
            if (tile.type == (ushort) 196)
            {
              Tile testTile = Main.tile[index1, index2 + 1];
              if (testTile == null)
              {
                testTile = new Tile();
                Main.tile[index1, index2 + 1] = testTile;
              }
              if (!WorldGen.SolidTile(testTile) && testTile.slope() == (byte) 0 && this.currentMax < this.qualityMax)
              {
                this.waterfalls[this.currentMax].type = 11;
                this.waterfalls[this.currentMax].x = index1;
                this.waterfalls[this.currentMax].y = index2 + 1;
                ++this.currentMax;
              }
            }
            if (tile.type == (ushort) 460)
            {
              Tile testTile = Main.tile[index1, index2 + 1];
              if (testTile == null)
              {
                testTile = new Tile();
                Main.tile[index1, index2 + 1] = testTile;
              }
              if (!WorldGen.SolidTile(testTile) && testTile.slope() == (byte) 0 && this.currentMax < this.qualityMax)
              {
                this.waterfalls[this.currentMax].type = 22;
                this.waterfalls[this.currentMax].x = index1;
                this.waterfalls[this.currentMax].y = index2 + 1;
                ++this.currentMax;
              }
            }
          }
        }
      }
    }

    public void UpdateFrame()
    {
      ++this.wFallFrCounter;
      if (this.wFallFrCounter > 2)
      {
        this.wFallFrCounter = 0;
        ++this.regularFrame;
        if (this.regularFrame > 15)
          this.regularFrame = 0;
      }
      ++this.wFallFrCounter2;
      if (this.wFallFrCounter2 > 6)
      {
        this.wFallFrCounter2 = 0;
        ++this.slowFrame;
        if (this.slowFrame > 15)
          this.slowFrame = 0;
      }
      ++this.rainFrameCounter;
      if (this.rainFrameCounter > 0)
      {
        ++this.rainFrameForeground;
        if (this.rainFrameForeground > 7)
          this.rainFrameForeground -= 8;
        if (this.rainFrameCounter > 2)
        {
          this.rainFrameCounter = 0;
          --this.rainFrameBackground;
          if (this.rainFrameBackground < 0)
            this.rainFrameBackground = 7;
        }
      }
      if (++this.snowFrameCounter <= 3)
        return;
      this.snowFrameCounter = 0;
      if (++this.snowFrameForeground <= 7)
        return;
      this.snowFrameForeground = 0;
    }

    private void DrawWaterfall(int Style = 0, float Alpha = 1f)
    {
      Main.tileSolid[546] = false;
      float num1 = 0.0f;
      float num2 = 99999f;
      float num3 = 99999f;
      int num4 = -1;
      int num5 = -1;
      float num6 = 0.0f;
      float num7 = 99999f;
      float num8 = 99999f;
      int num9 = -1;
      int num10 = -1;
      for (int index1 = 0; index1 < this.currentMax; ++index1)
      {
        int num11 = 0;
        int waterfallType1 = this.waterfalls[index1].type;
        int x1 = this.waterfalls[index1].x;
        int y = this.waterfalls[index1].y;
        int num12 = 0;
        int num13 = 0;
        int direction = 0;
        int num14 = 0;
        int num15 = 0;
        int waterfallType2 = 0;
        int x2;
        switch (waterfallType1)
        {
          case 0:
            waterfallType1 = Style;
            goto default;
          case 1:
          case 14:
          case 25:
            if (!Main.drewLava && this.waterfalls[index1].stopAtStep != 0)
            {
              x2 = 32 * this.slowFrame;
              break;
            }
            continue;
          case 2:
            if (Main.drewLava)
              continue;
            goto default;
          case 11:
          case 22:
            if (!Main.drewLava)
            {
              int num16 = this.waterfallDist / 4;
              if (waterfallType1 == 22)
                num16 = this.waterfallDist / 2;
              if (this.waterfalls[index1].stopAtStep > num16)
                this.waterfalls[index1].stopAtStep = num16;
              if (this.waterfalls[index1].stopAtStep != 0 && (double) (y + num16) >= (double) Main.screenPosition.Y / 16.0 && (double) x1 >= (double) Main.screenPosition.X / 16.0 - 20.0 && (double) x1 <= ((double) Main.screenPosition.X + (double) Main.screenWidth) / 16.0 + 20.0)
              {
                int num17;
                int num18;
                if (x1 % 2 == 0)
                {
                  num17 = this.rainFrameForeground + 3;
                  if (num17 > 7)
                    num17 -= 8;
                  num18 = this.rainFrameBackground + 2;
                  if (num18 > 7)
                    num18 -= 8;
                  if (waterfallType1 == 22)
                  {
                    num17 = this.snowFrameForeground + 3;
                    if (num17 > 7)
                      num17 -= 8;
                  }
                }
                else
                {
                  num17 = this.rainFrameForeground;
                  num18 = this.rainFrameBackground;
                  if (waterfallType1 == 22)
                    num17 = this.snowFrameForeground;
                }
                Rectangle rectangle1 = new Rectangle(num18 * 18, 0, 16, 16);
                Rectangle rectangle2 = new Rectangle(num17 * 18, 0, 16, 16);
                Vector2 origin = new Vector2(8f, 8f);
                Vector2 position = y % 2 != 0 ? new Vector2((float) (x1 * 16 + 8), (float) (y * 16 + 8)) - Main.screenPosition : new Vector2((float) (x1 * 16 + 9), (float) (y * 16 + 8)) - Main.screenPosition;
                Tile tile = Main.tile[x1, y - 1];
                if (tile.active() && tile.bottomSlope())
                  position.Y -= 16f;
                bool flag = false;
                float rotation = 0.0f;
                for (int index2 = 0; index2 < num16; ++index2)
                {
                  Color color1 = Lighting.GetColor(x1, y);
                  float num19 = 0.6f;
                  float num20 = 0.3f;
                  if (index2 > num16 - 8)
                  {
                    float num21 = (float) (num16 - index2) / 8f;
                    num19 *= num21;
                    num20 *= num21;
                  }
                  Color color2 = color1 * num19;
                  Color color3 = color1 * num20;
                  if (waterfallType1 == 22)
                  {
                    Main.spriteBatch.Draw(this.waterfallTexture[22].Value, position, new Rectangle?(rectangle2), color2, 0.0f, origin, 1f, SpriteEffects.None, 0.0f);
                  }
                  else
                  {
                    Main.spriteBatch.Draw(this.waterfallTexture[12].Value, position, new Rectangle?(rectangle1), color3, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                    Main.spriteBatch.Draw(this.waterfallTexture[11].Value, position, new Rectangle?(rectangle2), color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                  }
                  if (!flag)
                  {
                    ++y;
                    Tile testTile = Main.tile[x1, y];
                    if (WorldGen.SolidTile(testTile))
                      flag = true;
                    if (testTile.liquid > (byte) 0)
                    {
                      int num22 = (int) (16.0 * ((double) testTile.liquid / (double) byte.MaxValue)) & 254;
                      if (num22 < 15)
                      {
                        rectangle2.Height -= num22;
                        rectangle1.Height -= num22;
                      }
                      else
                        break;
                    }
                    if (y % 2 == 0)
                      ++position.X;
                    else
                      --position.X;
                    position.Y += 16f;
                  }
                  else
                    break;
                }
                this.waterfalls[index1].stopAtStep = 0;
                continue;
              }
              continue;
            }
            continue;
          default:
            x2 = 32 * this.regularFrame;
            break;
        }
        int num23 = 0;
        int maxSteps = this.waterfallDist;
        Color color4 = Color.White;
        for (int s = 0; s < maxSteps && num23 < 2; ++s)
        {
          WaterfallManager.AddLight(waterfallType1, x1, y);
          Tile tileCache = Main.tile[x1, y];
          if (tileCache == null)
          {
            tileCache = new Tile();
            Main.tile[x1, y] = tileCache;
          }
          if (!tileCache.nactive() || !Main.tileSolid[(int) tileCache.type] || Main.tileSolidTop[(int) tileCache.type] || TileID.Sets.Platforms[(int) tileCache.type] || tileCache.blockType() != 0)
          {
            Tile testTile1 = Main.tile[x1 - 1, y];
            if (testTile1 == null)
            {
              testTile1 = new Tile();
              Main.tile[x1 - 1, y] = testTile1;
            }
            Tile testTile2 = Main.tile[x1, y + 1];
            if (testTile2 == null)
            {
              testTile2 = new Tile();
              Main.tile[x1, y + 1] = testTile2;
            }
            Tile testTile3 = Main.tile[x1 + 1, y];
            if (testTile3 == null)
            {
              testTile3 = new Tile();
              Main.tile[x1 + 1, y] = testTile3;
            }
            if (WorldGen.SolidTile(testTile2) && !tileCache.halfBrick())
              num11 = 8;
            else if (num13 != 0)
              num11 = 0;
            int num24 = 0;
            int num25 = num14;
            bool flag = false;
            int num26;
            int num27;
            if (testTile2.topSlope() && !tileCache.halfBrick() && testTile2.type != (ushort) 19)
            {
              flag = true;
              if (testTile2.slope() == (byte) 1)
              {
                num24 = 1;
                num26 = 1;
                direction = 1;
                num14 = direction;
              }
              else
              {
                num24 = -1;
                num26 = -1;
                direction = -1;
                num14 = direction;
              }
              num27 = 1;
            }
            else if (!WorldGen.SolidTile(testTile2) && !testTile2.bottomSlope() && !tileCache.halfBrick() || !testTile2.active() && !tileCache.halfBrick())
            {
              num23 = 0;
              num27 = 1;
              num26 = 0;
            }
            else if ((WorldGen.SolidTile(testTile1) || testTile1.topSlope() || testTile1.liquid > (byte) 0) && !WorldGen.SolidTile(testTile3) && testTile3.liquid == (byte) 0)
            {
              if (direction == -1)
                ++num23;
              num26 = 1;
              num27 = 0;
              direction = 1;
            }
            else if ((WorldGen.SolidTile(testTile3) || testTile3.topSlope() || testTile3.liquid > (byte) 0) && !WorldGen.SolidTile(testTile1) && testTile1.liquid == (byte) 0)
            {
              if (direction == 1)
                ++num23;
              num26 = -1;
              num27 = 0;
              direction = -1;
            }
            else if ((!WorldGen.SolidTile(testTile3) && !tileCache.topSlope() || testTile3.liquid == (byte) 0) && !WorldGen.SolidTile(testTile1) && !tileCache.topSlope() && testTile1.liquid == (byte) 0)
            {
              num27 = 0;
              num26 = direction;
            }
            else
            {
              ++num23;
              num27 = 0;
              num26 = 0;
            }
            if (num23 >= 2)
            {
              direction *= -1;
              num26 *= -1;
            }
            int num28 = -1;
            if (waterfallType1 != 1 && waterfallType1 != 14 && waterfallType1 != 25)
            {
              if (testTile2.active())
                num28 = (int) testTile2.type;
              if (tileCache.active())
                num28 = (int) tileCache.type;
            }
            switch (num28)
            {
              case 160:
                waterfallType1 = 2;
                break;
              case 262:
              case 263:
              case 264:
              case 265:
              case 266:
              case 267:
              case 268:
                waterfallType1 = 15 + num28 - 262;
                break;
            }
            Color color5 = Lighting.GetColor(x1, y);
            if (s > 50)
              WaterfallManager.TrySparkling(x1, y, direction, color5);
            float alpha = WaterfallManager.GetAlpha(Alpha, maxSteps, waterfallType1, y, s, tileCache);
            Color color6 = WaterfallManager.StylizeColor(alpha, maxSteps, waterfallType1, y, s, tileCache, color5);
            if (waterfallType1 == 1)
            {
              float num29 = Math.Abs((float) (x1 * 16 + 8) - (Main.screenPosition.X + (float) (Main.screenWidth / 2)));
              float num30 = Math.Abs((float) (y * 16 + 8) - (Main.screenPosition.Y + (float) (Main.screenHeight / 2)));
              if ((double) num29 < (double) (Main.screenWidth * 2) && (double) num30 < (double) (Main.screenHeight * 2))
              {
                float num31 = (float) (1.0 - Math.Sqrt((double) num29 * (double) num29 + (double) num30 * (double) num30) / ((double) Main.screenWidth * 0.75));
                if ((double) num31 > 0.0)
                  num6 += num31;
              }
              if ((double) num29 < (double) num7)
              {
                num7 = num29;
                num9 = x1 * 16 + 8;
              }
              if ((double) num30 < (double) num8)
              {
                num8 = num29;
                num10 = y * 16 + 8;
              }
            }
            else if (waterfallType1 != 1 && waterfallType1 != 14 && waterfallType1 != 25 && waterfallType1 != 11 && waterfallType1 != 12 && waterfallType1 != 22)
            {
              float num32 = Math.Abs((float) (x1 * 16 + 8) - (Main.screenPosition.X + (float) (Main.screenWidth / 2)));
              float num33 = Math.Abs((float) (y * 16 + 8) - (Main.screenPosition.Y + (float) (Main.screenHeight / 2)));
              if ((double) num32 < (double) (Main.screenWidth * 2) && (double) num33 < (double) (Main.screenHeight * 2))
              {
                float num34 = (float) (1.0 - Math.Sqrt((double) num32 * (double) num32 + (double) num33 * (double) num33) / ((double) Main.screenWidth * 0.75));
                if ((double) num34 > 0.0)
                  num1 += num34;
              }
              if ((double) num32 < (double) num2)
              {
                num2 = num32;
                num4 = x1 * 16 + 8;
              }
              if ((double) num33 < (double) num3)
              {
                num3 = num32;
                num5 = y * 16 + 8;
              }
            }
            int num35 = (int) tileCache.liquid / 16;
            if (flag && direction != num25)
            {
              int num36 = 2;
              if (num25 == 1)
                this.DrawWaterfall(waterfallType1, x1, y, alpha, new Vector2((float) (x1 * 16 - 16), (float) (y * 16 + 16 - num36)) - Main.screenPosition, new Rectangle(x2, 24, 32, 16 - num35 - num36), color6, SpriteEffects.FlipHorizontally);
              else
                this.DrawWaterfall(waterfallType1, x1, y, alpha, new Vector2((float) (x1 * 16), (float) (y * 16 + 16 - num36)) - Main.screenPosition, new Rectangle(x2, 24, 32, 16 - num35 - num36), color6, SpriteEffects.None);
            }
            if (num12 == 0 && num24 != 0 && num13 == 1 && direction != num14)
            {
              num24 = 0;
              direction = num14;
              color6 = Color.White;
              if (direction == 1)
                this.DrawWaterfall(waterfallType1, x1, y, alpha, new Vector2((float) (x1 * 16 - 16), (float) (y * 16 + 16)) - Main.screenPosition, new Rectangle(x2, 24, 32, 16 - num35), color6, SpriteEffects.FlipHorizontally);
              else
                this.DrawWaterfall(waterfallType1, x1, y, alpha, new Vector2((float) (x1 * 16 - 16), (float) (y * 16 + 16)) - Main.screenPosition, new Rectangle(x2, 24, 32, 16 - num35), color6, SpriteEffects.FlipHorizontally);
            }
            if (num15 != 0 && num26 == 0 && num27 == 1)
            {
              if (direction == 1)
              {
                if (waterfallType2 != waterfallType1)
                  this.DrawWaterfall(waterfallType1, x1, y, alpha, new Vector2((float) (x1 * 16), (float) (y * 16 + num11 + 8)) - Main.screenPosition, new Rectangle(x2, 0, 16, 16 - num35 - 8), color4, SpriteEffects.FlipHorizontally);
                else
                  this.DrawWaterfall(waterfallType1, x1, y, alpha, new Vector2((float) (x1 * 16), (float) (y * 16 + num11 + 8)) - Main.screenPosition, new Rectangle(x2, 0, 16, 16 - num35 - 8), color6, SpriteEffects.FlipHorizontally);
              }
              else
                this.DrawWaterfall(waterfallType1, x1, y, alpha, new Vector2((float) (x1 * 16), (float) (y * 16 + num11 + 8)) - Main.screenPosition, new Rectangle(x2, 0, 16, 16 - num35 - 8), color6, SpriteEffects.None);
            }
            if (num11 == 8 && num13 == 1 && num15 == 0)
            {
              if (num14 == -1)
              {
                if (waterfallType2 != waterfallType1)
                  this.DrawWaterfall(waterfallType2, x1, y, alpha, new Vector2((float) (x1 * 16), (float) (y * 16)) - Main.screenPosition, new Rectangle(x2, 24, 32, 8), color4, SpriteEffects.None);
                else
                  this.DrawWaterfall(waterfallType1, x1, y, alpha, new Vector2((float) (x1 * 16), (float) (y * 16)) - Main.screenPosition, new Rectangle(x2, 24, 32, 8), color6, SpriteEffects.None);
              }
              else if (waterfallType2 != waterfallType1)
                this.DrawWaterfall(waterfallType2, x1, y, alpha, new Vector2((float) (x1 * 16 - 16), (float) (y * 16)) - Main.screenPosition, new Rectangle(x2, 24, 32, 8), color4, SpriteEffects.FlipHorizontally);
              else
                this.DrawWaterfall(waterfallType1, x1, y, alpha, new Vector2((float) (x1 * 16 - 16), (float) (y * 16)) - Main.screenPosition, new Rectangle(x2, 24, 32, 8), color6, SpriteEffects.FlipHorizontally);
            }
            if (num24 != 0 && num12 == 0)
            {
              if (num25 == 1)
              {
                if (waterfallType2 != waterfallType1)
                  this.DrawWaterfall(waterfallType2, x1, y, alpha, new Vector2((float) (x1 * 16 - 16), (float) (y * 16)) - Main.screenPosition, new Rectangle(x2, 24, 32, 16 - num35), color4, SpriteEffects.FlipHorizontally);
                else
                  this.DrawWaterfall(waterfallType1, x1, y, alpha, new Vector2((float) (x1 * 16 - 16), (float) (y * 16)) - Main.screenPosition, new Rectangle(x2, 24, 32, 16 - num35), color6, SpriteEffects.FlipHorizontally);
              }
              else if (waterfallType2 != waterfallType1)
                this.DrawWaterfall(waterfallType2, x1, y, alpha, new Vector2((float) (x1 * 16), (float) (y * 16)) - Main.screenPosition, new Rectangle(x2, 24, 32, 16 - num35), color4, SpriteEffects.None);
              else
                this.DrawWaterfall(waterfallType1, x1, y, alpha, new Vector2((float) (x1 * 16), (float) (y * 16)) - Main.screenPosition, new Rectangle(x2, 24, 32, 16 - num35), color6, SpriteEffects.None);
            }
            if (num27 == 1 && num24 == 0 && num15 == 0)
            {
              if (direction == -1)
              {
                if (num13 == 0)
                  this.DrawWaterfall(waterfallType1, x1, y, alpha, new Vector2((float) (x1 * 16), (float) (y * 16 + num11)) - Main.screenPosition, new Rectangle(x2, 0, 16, 16 - num35), color6, SpriteEffects.None);
                else if (waterfallType2 != waterfallType1)
                  this.DrawWaterfall(waterfallType2, x1, y, alpha, new Vector2((float) (x1 * 16), (float) (y * 16)) - Main.screenPosition, new Rectangle(x2, 24, 32, 16 - num35), color4, SpriteEffects.None);
                else
                  this.DrawWaterfall(waterfallType1, x1, y, alpha, new Vector2((float) (x1 * 16), (float) (y * 16)) - Main.screenPosition, new Rectangle(x2, 24, 32, 16 - num35), color6, SpriteEffects.None);
              }
              else if (num13 == 0)
                this.DrawWaterfall(waterfallType1, x1, y, alpha, new Vector2((float) (x1 * 16), (float) (y * 16 + num11)) - Main.screenPosition, new Rectangle(x2, 0, 16, 16 - num35), color6, SpriteEffects.FlipHorizontally);
              else if (waterfallType2 != waterfallType1)
                this.DrawWaterfall(waterfallType2, x1, y, alpha, new Vector2((float) (x1 * 16 - 16), (float) (y * 16)) - Main.screenPosition, new Rectangle(x2, 24, 32, 16 - num35), color4, SpriteEffects.FlipHorizontally);
              else
                this.DrawWaterfall(waterfallType1, x1, y, alpha, new Vector2((float) (x1 * 16 - 16), (float) (y * 16)) - Main.screenPosition, new Rectangle(x2, 24, 32, 16 - num35), color6, SpriteEffects.FlipHorizontally);
            }
            else
            {
              switch (num26)
              {
                case -1:
                  if (Main.tile[x1, y].liquid <= (byte) 0 || Main.tile[x1, y].halfBrick())
                  {
                    if (num24 == -1)
                    {
                      for (int index3 = 0; index3 < 8; ++index3)
                      {
                        int num37 = index3 * 2;
                        int num38 = index3 * 2;
                        int num39 = 14 - index3 * 2;
                        num11 = 8;
                        if (num12 == 0 && index3 > 5)
                          num39 = 4;
                        this.DrawWaterfall(waterfallType1, x1, y, alpha, new Vector2((float) (x1 * 16 + num37), (float) (y * 16 + num11 + num39)) - Main.screenPosition, new Rectangle(16 + x2 + num38, 0, 2, 16 - num11), color6, SpriteEffects.FlipHorizontally);
                      }
                      break;
                    }
                    int height = 16;
                    if (TileID.Sets.BlocksWaterDrawingBehindSelf[(int) Main.tile[x1, y].type])
                      height = 8;
                    else if (TileID.Sets.BlocksWaterDrawingBehindSelf[(int) Main.tile[x1, y + 1].type])
                      height = 8;
                    this.DrawWaterfall(waterfallType1, x1, y, alpha, new Vector2((float) (x1 * 16), (float) (y * 16 + num11)) - Main.screenPosition, new Rectangle(16 + x2, 0, 16, height), color6, SpriteEffects.None);
                    break;
                  }
                  break;
                case 0:
                  if (num27 == 0)
                  {
                    if (Main.tile[x1, y].liquid <= (byte) 0 || Main.tile[x1, y].halfBrick())
                      this.DrawWaterfall(waterfallType1, x1, y, alpha, new Vector2((float) (x1 * 16), (float) (y * 16 + num11)) - Main.screenPosition, new Rectangle(16 + x2, 0, 16, 16), color6, SpriteEffects.None);
                    s = 1000;
                    break;
                  }
                  break;
                case 1:
                  if (Main.tile[x1, y].liquid <= (byte) 0 || Main.tile[x1, y].halfBrick())
                  {
                    if (num24 == 1)
                    {
                      for (int index4 = 0; index4 < 8; ++index4)
                      {
                        int num40 = index4 * 2;
                        int num41 = 14 - index4 * 2;
                        int num42 = num40;
                        num11 = 8;
                        if (num12 == 0 && index4 < 2)
                          num42 = 4;
                        this.DrawWaterfall(waterfallType1, x1, y, alpha, new Vector2((float) (x1 * 16 + num40), (float) (y * 16 + num11 + num42)) - Main.screenPosition, new Rectangle(16 + x2 + num41, 0, 2, 16 - num11), color6, SpriteEffects.FlipHorizontally);
                      }
                      break;
                    }
                    int height = 16;
                    if (TileID.Sets.BlocksWaterDrawingBehindSelf[(int) Main.tile[x1, y].type])
                      height = 8;
                    else if (TileID.Sets.BlocksWaterDrawingBehindSelf[(int) Main.tile[x1, y + 1].type])
                      height = 8;
                    this.DrawWaterfall(waterfallType1, x1, y, alpha, new Vector2((float) (x1 * 16), (float) (y * 16 + num11)) - Main.screenPosition, new Rectangle(16 + x2, 0, 16, height), color6, SpriteEffects.FlipHorizontally);
                    break;
                  }
                  break;
              }
            }
            if (tileCache.liquid > (byte) 0 && !tileCache.halfBrick())
              s = 1000;
            num13 = num27;
            num14 = direction;
            num12 = num26;
            x1 += num26;
            y += num27;
            num15 = num24;
            color4 = color6;
            if (waterfallType2 != waterfallType1)
              waterfallType2 = waterfallType1;
            if (testTile1.active() && (testTile1.type == (ushort) 189 || testTile1.type == (ushort) 196) || testTile3.active() && (testTile3.type == (ushort) 189 || testTile3.type == (ushort) 196) || testTile2.active() && (testTile2.type == (ushort) 189 || testTile2.type == (ushort) 196))
              maxSteps = (int) (40.0 * ((double) Main.maxTilesX / 4200.0) * (double) Main.gfxQuality);
          }
          else
            break;
        }
      }
      Main.ambientWaterfallX = (float) num4;
      Main.ambientWaterfallY = (float) num5;
      Main.ambientWaterfallStrength = num1;
      Main.ambientLavafallX = (float) num9;
      Main.ambientLavafallY = (float) num10;
      Main.ambientLavafallStrength = num6;
      Main.tileSolid[546] = true;
    }

    private void DrawWaterfall(
      int waterfallType,
      int x,
      int y,
      float opacity,
      Vector2 position,
      Rectangle sourceRect,
      Color color,
      SpriteEffects effects)
    {
      Texture2D texture = this.waterfallTexture[waterfallType].Value;
      if (waterfallType == 25)
      {
        VertexColors vertices;
        Lighting.GetCornerColors(x, y, out vertices);
        LiquidRenderer.SetShimmerVertexColors(ref vertices, opacity, x, y);
        Main.tileBatch.Draw(texture, position + new Vector2(0.0f, 0.0f), new Rectangle?(sourceRect), vertices, new Vector2(), 1f, effects);
        sourceRect.Y += 42;
        LiquidRenderer.SetShimmerVertexColors_Sparkle(ref vertices, opacity, x, y, true);
        Main.tileBatch.Draw(texture, position + new Vector2(0.0f, 0.0f), new Rectangle?(sourceRect), vertices, new Vector2(), 1f, effects);
      }
      else
        Main.spriteBatch.Draw(texture, position, new Rectangle?(sourceRect), color, 0.0f, new Vector2(), 1f, effects, 0.0f);
    }

    private static Color StylizeColor(
      float alpha,
      int maxSteps,
      int waterfallType,
      int y,
      int s,
      Tile tileCache,
      Color aColor)
    {
      float r = (float) aColor.R * alpha;
      float g = (float) aColor.G * alpha;
      float b = (float) aColor.B * alpha;
      float a = (float) aColor.A * alpha;
      switch (waterfallType)
      {
        case 1:
          if ((double) r < 190.0 * (double) alpha)
            r = 190f * alpha;
          if ((double) g < 190.0 * (double) alpha)
            g = 190f * alpha;
          if ((double) b < 190.0 * (double) alpha)
          {
            b = 190f * alpha;
            break;
          }
          break;
        case 2:
          r = (float) Main.DiscoR * alpha;
          g = (float) Main.DiscoG * alpha;
          b = (float) Main.DiscoB * alpha;
          break;
        case 15:
        case 16:
        case 17:
        case 18:
        case 19:
        case 20:
        case 21:
          r = (float) byte.MaxValue * alpha;
          g = (float) byte.MaxValue * alpha;
          b = (float) byte.MaxValue * alpha;
          break;
      }
      aColor = new Color((int) r, (int) g, (int) b, (int) a);
      return aColor;
    }

    private static float GetAlpha(
      float Alpha,
      int maxSteps,
      int waterfallType,
      int y,
      int s,
      Tile tileCache)
    {
      float alpha;
      switch (waterfallType)
      {
        case 1:
          alpha = 1f;
          break;
        case 14:
          alpha = 0.8f;
          break;
        case 25:
          alpha = 0.75f;
          break;
        default:
          alpha = tileCache.wall != (ushort) 0 || (double) y >= Main.worldSurface ? 0.6f * Alpha : Alpha;
          break;
      }
      if (s > maxSteps - 10)
        alpha *= (float) (maxSteps - s) / 10f;
      return alpha;
    }

    private static void TrySparkling(int x, int y, int direction, Color aColor2)
    {
      if (aColor2.R <= (byte) 20 && aColor2.B <= (byte) 20 && aColor2.G <= (byte) 20)
        return;
      float num = (float) aColor2.R;
      if ((double) aColor2.G > (double) num)
        num = (float) aColor2.G;
      if ((double) aColor2.B > (double) num)
        num = (float) aColor2.B;
      if ((double) Main.rand.Next(20000) >= (double) num / 30.0)
        return;
      int index = Dust.NewDust(new Vector2((float) (x * 16 - direction * 7), (float) (y * 16 + 6)), 10, 8, 43, Alpha: 254, newColor: Color.White, Scale: 0.5f);
      Main.dust[index].velocity *= 0.0f;
    }

    private static void AddLight(int waterfallType, int x, int y)
    {
      switch (waterfallType)
      {
        case 1:
          double num1;
          float r1 = (float) (num1 = (0.550000011920929 + (double) (270 - (int) Main.mouseTextColor) / 900.0) * 0.40000000596046448);
          float g1 = (float) (num1 * 0.30000001192092896);
          float b1 = (float) (num1 * 0.10000000149011612);
          Lighting.AddLight(x, y, r1, g1, b1);
          break;
        case 2:
          float num2 = (float) Main.DiscoR / (float) byte.MaxValue;
          float num3 = (float) Main.DiscoG / (float) byte.MaxValue;
          float num4 = (float) Main.DiscoB / (float) byte.MaxValue;
          float r2 = num2 * 0.2f;
          float g2 = num3 * 0.2f;
          float b2 = num4 * 0.2f;
          Lighting.AddLight(x, y, r2, g2, b2);
          break;
        case 15:
          float r3 = 0.0f;
          float g3 = 0.0f;
          float b3 = 0.2f;
          Lighting.AddLight(x, y, r3, g3, b3);
          break;
        case 16:
          float r4 = 0.0f;
          float g4 = 0.2f;
          float b4 = 0.0f;
          Lighting.AddLight(x, y, r4, g4, b4);
          break;
        case 17:
          float r5 = 0.0f;
          float g5 = 0.0f;
          float b5 = 0.2f;
          Lighting.AddLight(x, y, r5, g5, b5);
          break;
        case 18:
          float r6 = 0.0f;
          float g6 = 0.2f;
          float b6 = 0.0f;
          Lighting.AddLight(x, y, r6, g6, b6);
          break;
        case 19:
          float r7 = 0.2f;
          float g7 = 0.0f;
          float b7 = 0.0f;
          Lighting.AddLight(x, y, r7, g7, b7);
          break;
        case 20:
          Lighting.AddLight(x, y, 0.2f, 0.2f, 0.2f);
          break;
        case 21:
          float r8 = 0.2f;
          float g8 = 0.0f;
          float b8 = 0.0f;
          Lighting.AddLight(x, y, r8, g8, b8);
          break;
        case 25:
          float num5 = 0.7f;
          float num6 = 0.7f;
          float num7 = num5 + (float) (270 - (int) Main.mouseTextColor) / 900f;
          float num8 = num6 + (float) (270 - (int) Main.mouseTextColor) / 125f;
          Lighting.AddLight(x, y, num7 * 0.6f, num8 * 0.25f, num7 * 0.9f);
          break;
      }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      for (int index = 0; index < this.currentMax; ++index)
        this.waterfalls[index].stopAtStep = this.waterfallDist;
      Main.drewLava = false;
      if ((double) Main.liquidAlpha[0] > 0.0)
        this.DrawWaterfall(Alpha: Main.liquidAlpha[0]);
      if ((double) Main.liquidAlpha[2] > 0.0)
        this.DrawWaterfall(3, Main.liquidAlpha[2]);
      if ((double) Main.liquidAlpha[3] > 0.0)
        this.DrawWaterfall(4, Main.liquidAlpha[3]);
      if ((double) Main.liquidAlpha[4] > 0.0)
        this.DrawWaterfall(5, Main.liquidAlpha[4]);
      if ((double) Main.liquidAlpha[5] > 0.0)
        this.DrawWaterfall(6, Main.liquidAlpha[5]);
      if ((double) Main.liquidAlpha[6] > 0.0)
        this.DrawWaterfall(7, Main.liquidAlpha[6]);
      if ((double) Main.liquidAlpha[7] > 0.0)
        this.DrawWaterfall(8, Main.liquidAlpha[7]);
      if ((double) Main.liquidAlpha[8] > 0.0)
        this.DrawWaterfall(9, Main.liquidAlpha[8]);
      if ((double) Main.liquidAlpha[9] > 0.0)
        this.DrawWaterfall(10, Main.liquidAlpha[9]);
      if ((double) Main.liquidAlpha[10] > 0.0)
        this.DrawWaterfall(13, Main.liquidAlpha[10]);
      if ((double) Main.liquidAlpha[12] > 0.0)
        this.DrawWaterfall(23, Main.liquidAlpha[12]);
      if ((double) Main.liquidAlpha[13] <= 0.0)
        return;
      this.DrawWaterfall(24, Main.liquidAlpha[13]);
    }

    public struct WaterfallData
    {
      public int x;
      public int y;
      public int type;
      public int stopAtStep;
    }
  }
}
