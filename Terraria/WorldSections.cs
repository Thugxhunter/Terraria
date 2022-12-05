// Decompiled with JetBrains decompiler
// Type: Terraria.WorldSections
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Terraria
{
  public class WorldSections
  {
    public const int BitIndex_SectionLoaded = 0;
    public const int BitIndex_SectionFramed = 1;
    public const int BitIndex_SectionMapDrawn = 2;
    public const int BitIndex_SectionNeedsRefresh = 3;
    private int width;
    private int height;
    private BitsByte[] data;
    private int mapSectionsLeft;
    private int frameSectionsLeft;
    private int _sectionsNeedingRefresh;
    private WorldSections.IterationState prevFrame;
    private WorldSections.IterationState prevMap;

    public WorldSections(int numSectionsX, int numSectionsY)
    {
      this.width = numSectionsX;
      this.height = numSectionsY;
      this.data = new BitsByte[this.width * this.height];
      this.mapSectionsLeft = this.width * this.height;
      this.prevFrame.Reset();
      this.prevMap.Reset();
    }

    public bool AnyUnfinishedSections => this.frameSectionsLeft > 0;

    public bool AnyNeedRefresh => this._sectionsNeedingRefresh > 0;

    public void SetSectionAsRefreshed(int x, int y)
    {
      if (x >= 0)
      {
        int width = this.width;
      }
      if (y >= 0)
      {
        int height = this.height;
      }
      if (!this.data[y * this.width + x][3])
        return;
      this.data[y * this.width + x][3] = false;
      --this._sectionsNeedingRefresh;
    }

    public bool SectionNeedsRefresh(int x, int y) => x >= 0 && x < this.width && y >= 0 && y < this.height && this.data[y * this.width + x][3];

    public void SetAllFramedSectionsAsNeedingRefresh()
    {
      for (int index = 0; index < this.data.Length; ++index)
      {
        if (this.data[index][1])
        {
          this.data[index][3] = true;
          ++this._sectionsNeedingRefresh;
        }
      }
    }

    public bool SectionLoaded(int x, int y) => x >= 0 && x < this.width && y >= 0 && y < this.height && this.data[y * this.width + x][0];

    public bool SectionFramed(int x, int y) => x >= 0 && x < this.width && y >= 0 && y < this.height && this.data[y * this.width + x][1];

    public bool MapSectionDrawn(int x, int y) => x >= 0 && x < this.width && y >= 0 && y < this.height && this.data[y * this.width + x][2];

    public void ClearMapDraw()
    {
      for (int index = 0; index < this.data.Length; ++index)
        this.data[index][2] = false;
      this.prevMap.Reset();
      this.mapSectionsLeft = this.data.Length;
    }

    public void SetSectionFramed(int x, int y)
    {
      if (x < 0 || x >= this.width || y < 0 || y >= this.height)
        return;
      BitsByte bitsByte = this.data[y * this.width + x];
      if (!bitsByte[0] || bitsByte[1])
        return;
      bitsByte[1] = true;
      this.data[y * this.width + x] = bitsByte;
      --this.frameSectionsLeft;
    }

    public void SetSectionLoaded(int x, int y)
    {
      if (x < 0 || x >= this.width || y < 0 || y >= this.height)
        return;
      this.SetSectionLoaded(ref this.data[y * this.width + x]);
    }

    private void SetSectionLoaded(ref BitsByte section)
    {
      if (!section[0])
      {
        section[0] = true;
        ++this.frameSectionsLeft;
      }
      else
      {
        if (!section[1])
          return;
        section[1] = false;
        ++this.frameSectionsLeft;
      }
    }

    public void SetAllSectionsLoaded()
    {
      for (int index = 0; index < this.data.Length; ++index)
        this.SetSectionLoaded(ref this.data[index]);
    }

    public void SetTilesLoaded(int startX, int startY, int endXInclusive, int endYInclusive)
    {
      int sectionX1 = Netplay.GetSectionX(startX);
      int sectionY1 = Netplay.GetSectionY(startY);
      int sectionX2 = Netplay.GetSectionX(endXInclusive);
      int sectionY2 = Netplay.GetSectionY(endYInclusive);
      for (int x = sectionX1; x <= sectionX2; ++x)
      {
        for (int y = sectionY1; y <= sectionY2; ++y)
          this.SetSectionLoaded(x, y);
      }
    }

    public bool GetNextMapDraw(Vector2 playerPos, out int x, out int y)
    {
      if (this.mapSectionsLeft <= 0)
      {
        x = -1;
        y = -1;
        return false;
      }
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();
      int num1 = 0;
      int num2 = 0;
      Vector2 vector2 = this.prevMap.centerPos;
      playerPos *= 1f / 16f;
      int sectionX = Netplay.GetSectionX((int) playerPos.X);
      int sectionY = Netplay.GetSectionY((int) playerPos.Y);
      int num3 = Netplay.GetSectionX((int) vector2.X);
      int num4 = Netplay.GetSectionY((int) vector2.Y);
      int num5;
      if (num3 != sectionX || num4 != sectionY)
      {
        vector2 = playerPos;
        num3 = sectionX;
        num4 = sectionY;
        num5 = 4;
        x = sectionX;
        y = sectionY;
      }
      else
      {
        num5 = this.prevMap.leg;
        x = this.prevMap.X;
        y = this.prevMap.Y;
        num1 = this.prevMap.xDir;
        num2 = this.prevMap.yDir;
      }
      int num6 = (int) ((double) playerPos.X - ((double) num3 + 0.5) * 200.0);
      int num7 = (int) ((double) playerPos.Y - ((double) num4 + 0.5) * 150.0);
      if (num1 == 0)
      {
        num1 = num6 <= 0 ? 1 : -1;
        num2 = num7 <= 0 ? 1 : -1;
      }
      int num8 = 0;
      bool flag1 = false;
      bool flag2 = false;
      while (true)
      {
        if (num8 == 4)
        {
          if (!flag2)
          {
            flag2 = true;
            x = num3;
            y = num4;
            num6 = (int) ((double) vector2.X - ((double) num3 + 0.5) * 200.0);
            num7 = (int) ((double) vector2.Y - ((double) num4 + 0.5) * 150.0);
            num1 = num6 <= 0 ? 1 : -1;
            num2 = num7 <= 0 ? 1 : -1;
            num5 = 4;
            num8 = 0;
          }
          else
            break;
        }
        if (y >= 0 && y < this.height && x >= 0 && x < this.width)
        {
          flag1 = false;
          if (!this.data[y * this.width + x][2])
            goto label_14;
        }
        int num9 = x - num3;
        int num10 = y - num4;
        if (num9 == 0 || num10 == 0)
        {
          if (num5 == 4)
          {
            if (num9 == 0 && num10 == 0)
            {
              if (Math.Abs(num6) > Math.Abs(num7))
                y -= num2;
              else
                x -= num1;
            }
            else
            {
              if (num9 != 0)
                x += num9 / Math.Abs(num9);
              if (num10 != 0)
                y += num10 / Math.Abs(num10);
            }
            num5 = 0;
            num8 = -2;
            flag1 = true;
          }
          else
          {
            if (num9 == 0)
              num2 = num10 <= 0 ? 1 : -1;
            else
              num1 = num9 <= 0 ? 1 : -1;
            x += num1;
            y += num2;
            ++num5;
          }
          if (flag1)
            ++num8;
          else
            flag1 = true;
        }
        else
        {
          x += num1;
          y += num2;
        }
      }
      throw new Exception("Infinite loop in WorldSections.GetNextMapDraw");
label_14:
      this.data[y * this.width + x][2] = true;
      --this.mapSectionsLeft;
      this.prevMap.centerPos = playerPos;
      this.prevMap.X = x;
      this.prevMap.Y = y;
      this.prevMap.leg = num5;
      this.prevMap.xDir = num1;
      this.prevMap.yDir = num2;
      stopwatch.Stop();
      return true;
    }

    private struct IterationState
    {
      public Vector2 centerPos;
      public int X;
      public int Y;
      public int leg;
      public int xDir;
      public int yDir;

      public void Reset()
      {
        this.centerPos = new Vector2(-3200f, -2400f);
        this.X = 0;
        this.Y = 0;
        this.leg = 0;
        this.xDir = 0;
        this.yDir = 0;
      }
    }
  }
}
