// Decompiled with JetBrains decompiler
// Type: Terraria.DelegateMethods
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace Terraria
{
  public static class DelegateMethods
  {
    public static Vector3 v3_1 = Vector3.Zero;
    public static Vector2 v2_1 = Vector2.Zero;
    public static float f_1 = 0.0f;
    public static Color c_1 = Color.Transparent;
    public static int i_1;
    public static bool CheckResultOut;
    public static TileCuttingContext tilecut_0 = TileCuttingContext.Unknown;
    public static bool[] tileCutIgnore = (bool[]) null;

    public static Color ColorLerp_BlackToWhite(float percent) => Color.Lerp(Color.Black, Color.White, percent);

    public static Color ColorLerp_HSL_H(float percent) => Main.hslToRgb(percent, 1f, 0.5f);

    public static Color ColorLerp_HSL_S(float percent) => Main.hslToRgb(DelegateMethods.v3_1.X, percent, DelegateMethods.v3_1.Z);

    public static Color ColorLerp_HSL_L(float percent) => Main.hslToRgb(DelegateMethods.v3_1.X, DelegateMethods.v3_1.Y, (float) (0.15000000596046448 + 0.85000002384185791 * (double) percent));

    public static Color ColorLerp_HSL_O(float percent) => Color.Lerp(Color.White, Main.hslToRgb(DelegateMethods.v3_1.X, DelegateMethods.v3_1.Y, DelegateMethods.v3_1.Z), percent);

    public static bool SpreadDirt(int x, int y)
    {
      if ((double) Vector2.Distance(DelegateMethods.v2_1, new Vector2((float) x, (float) y)) > (double) DelegateMethods.f_1)
        return false;
      WorldGen.TryKillingReplaceableTile(x, y, 0);
      if (WorldGen.PlaceTile(x, y, 0))
      {
        if (Main.netMode != 0)
          NetMessage.SendData(17, number: 1, number2: ((float) x), number3: ((float) y));
        Vector2 Position = new Vector2((float) (x * 16), (float) (y * 16));
        int Type = 0;
        for (int index = 0; index < 3; ++index)
        {
          Dust dust1 = Dust.NewDustDirect(Position, 16, 16, Type, Alpha: 100, newColor: Color.Transparent, Scale: 2.2f);
          dust1.noGravity = true;
          dust1.velocity.Y -= 1.2f;
          dust1.velocity *= 4f;
          Dust dust2 = Dust.NewDustDirect(Position, 16, 16, Type, Alpha: 100, newColor: Color.Transparent, Scale: 1.3f);
          dust2.velocity.Y -= 1.2f;
          dust2.velocity *= 2f;
        }
        int index1 = x;
        int index2 = y + 1;
        if (Main.tile[index1, index2] != null && !TileID.Sets.Platforms[(int) Main.tile[index1, index2].type] && (Main.tile[index1, index2].topSlope() || Main.tile[index1, index2].halfBrick()))
        {
          WorldGen.SlopeTile(index1, index2);
          if (Main.netMode != 0)
            NetMessage.SendData(17, number: 14, number2: ((float) index1), number3: ((float) index2));
        }
        int index3 = y - 1;
        if (Main.tile[index1, index3] != null && !TileID.Sets.Platforms[(int) Main.tile[index1, index3].type] && Main.tile[index1, index3].bottomSlope())
        {
          WorldGen.SlopeTile(index1, index3);
          if (Main.netMode != 0)
            NetMessage.SendData(17, number: 14, number2: ((float) index1), number3: ((float) index3));
        }
        for (int index4 = x - 1; index4 <= x + 1; ++index4)
        {
          for (int index5 = y - 1; index5 <= y + 1; ++index5)
          {
            Tile tile = Main.tile[index4, index5];
            if (tile.active() && Type != (int) tile.type && (tile.type == (ushort) 2 || tile.type == (ushort) 23 || tile.type == (ushort) 60 || tile.type == (ushort) 70 || tile.type == (ushort) 109 || tile.type == (ushort) 199 || tile.type == (ushort) 477 || tile.type == (ushort) 492))
            {
              bool flag = true;
              for (int i = index4 - 1; i <= index4 + 1; ++i)
              {
                for (int j = index5 - 1; j <= index5 + 1; ++j)
                {
                  if (!WorldGen.SolidTile(i, j))
                    flag = false;
                }
              }
              if (flag)
              {
                WorldGen.KillTile(index4, index5, true);
                if (Main.netMode != 0)
                  NetMessage.SendData(17, number2: ((float) index4), number3: ((float) index5), number4: 1f);
              }
            }
          }
        }
        return true;
      }
      Tile tile1 = Main.tile[x, y];
      if (tile1 == null || tile1.type < (ushort) 0 || (int) tile1.type >= (int) TileID.Count)
        return false;
      return !Main.tileSolid[(int) tile1.type] || TileID.Sets.Platforms[(int) tile1.type] || tile1.type == (ushort) 380;
    }

    public static bool SpreadWater(int x, int y)
    {
      if ((double) Vector2.Distance(DelegateMethods.v2_1, new Vector2((float) x, (float) y)) > (double) DelegateMethods.f_1 || !WorldGen.PlaceLiquid(x, y, (byte) 0, byte.MaxValue))
        return false;
      Vector2 Position = new Vector2((float) (x * 16), (float) (y * 16));
      int Type = Dust.dustWater();
      for (int index = 0; index < 3; ++index)
      {
        Dust dust1 = Dust.NewDustDirect(Position, 16, 16, Type, Alpha: 100, newColor: Color.Transparent, Scale: 2.2f);
        dust1.noGravity = true;
        dust1.velocity.Y -= 1.2f;
        dust1.velocity *= 7f;
        Dust dust2 = Dust.NewDustDirect(Position, 16, 16, Type, Alpha: 100, newColor: Color.Transparent, Scale: 1.3f);
        dust2.velocity.Y -= 1.2f;
        dust2.velocity *= 4f;
      }
      return true;
    }

    public static bool SpreadHoney(int x, int y)
    {
      if ((double) Vector2.Distance(DelegateMethods.v2_1, new Vector2((float) x, (float) y)) > (double) DelegateMethods.f_1 || !WorldGen.PlaceLiquid(x, y, (byte) 2, byte.MaxValue))
        return false;
      Vector2 Position = new Vector2((float) (x * 16), (float) (y * 16));
      int Type = 152;
      for (int index = 0; index < 3; ++index)
      {
        Dust dust1 = Dust.NewDustDirect(Position, 16, 16, Type, Alpha: 100, newColor: Color.Transparent, Scale: 2.2f);
        dust1.velocity.Y -= 1.2f;
        dust1.velocity *= 7f;
        Dust dust2 = Dust.NewDustDirect(Position, 16, 16, Type, Alpha: 100, newColor: Color.Transparent, Scale: 1.3f);
        dust2.velocity.Y -= 1.2f;
        dust2.velocity *= 4f;
      }
      return true;
    }

    public static bool SpreadLava(int x, int y)
    {
      if ((double) Vector2.Distance(DelegateMethods.v2_1, new Vector2((float) x, (float) y)) > (double) DelegateMethods.f_1 || !WorldGen.PlaceLiquid(x, y, (byte) 1, byte.MaxValue))
        return false;
      Vector2 Position = new Vector2((float) (x * 16), (float) (y * 16));
      int Type = 35;
      for (int index = 0; index < 3; ++index)
      {
        Dust.NewDustDirect(Position, 16, 16, Type, Alpha: 100, newColor: Color.Transparent, Scale: 1.2f).velocity *= 7f;
        Dust.NewDustDirect(Position, 16, 16, Type, Alpha: 100, newColor: Color.Transparent, Scale: 0.8f).velocity *= 4f;
      }
      return true;
    }

    public static bool SpreadDry(int x, int y)
    {
      if ((double) Vector2.Distance(DelegateMethods.v2_1, new Vector2((float) x, (float) y)) > (double) DelegateMethods.f_1 || !WorldGen.EmptyLiquid(x, y))
        return false;
      Vector2 Position = new Vector2((float) (x * 16), (float) (y * 16));
      int Type = 31;
      for (int index = 0; index < 3; ++index)
      {
        Dust dust = Dust.NewDustDirect(Position, 16, 16, Type, Alpha: 100, newColor: Color.Transparent, Scale: 1.2f);
        dust.noGravity = true;
        dust.velocity *= 7f;
        Dust.NewDustDirect(Position, 16, 16, Type, Alpha: 100, newColor: Color.Transparent, Scale: 0.8f).velocity *= 4f;
      }
      return true;
    }

    public static bool SpreadTest(int x, int y)
    {
      Tile tile = Main.tile[x, y];
      if (!WorldGen.SolidTile(x, y) && tile.wall == (ushort) 0)
        return true;
      tile.active();
      return false;
    }

    public static bool TestDust(int x, int y)
    {
      if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
        return false;
      int index = Dust.NewDust(new Vector2((float) x, (float) y) * 16f + new Vector2(8f), 0, 0, 6);
      Main.dust[index].noGravity = true;
      Main.dust[index].noLight = true;
      return true;
    }

    public static bool CastLight(int x, int y)
    {
      if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY || Main.tile[x, y] == null)
        return false;
      Lighting.AddLight(x, y, DelegateMethods.v3_1.X, DelegateMethods.v3_1.Y, DelegateMethods.v3_1.Z);
      return true;
    }

    public static bool CastLightOpen(int x, int y)
    {
      if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY || Main.tile[x, y] == null)
        return false;
      if (!Main.tile[x, y].active() || Main.tile[x, y].inActive() || Main.tileSolidTop[(int) Main.tile[x, y].type] || !Main.tileSolid[(int) Main.tile[x, y].type])
        Lighting.AddLight(x, y, DelegateMethods.v3_1.X, DelegateMethods.v3_1.Y, DelegateMethods.v3_1.Z);
      return true;
    }

    public static bool CheckStopForSolids(int x, int y)
    {
      if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY || Main.tile[x, y] == null)
        return false;
      if (!Main.tile[x, y].active() || Main.tile[x, y].inActive() || Main.tileSolidTop[(int) Main.tile[x, y].type] || !Main.tileSolid[(int) Main.tile[x, y].type])
        return true;
      DelegateMethods.CheckResultOut = true;
      return false;
    }

    public static bool CastLightOpen_StopForSolids_ScaleWithDistance(int x, int y)
    {
      if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY || Main.tile[x, y] == null || Main.tile[x, y].active() && !Main.tile[x, y].inActive() && !Main.tileSolidTop[(int) Main.tile[x, y].type] && Main.tileSolid[(int) Main.tile[x, y].type])
        return false;
      Vector3 v31 = DelegateMethods.v3_1;
      Vector2 vector2 = new Vector2((float) x, (float) y);
      float num = Vector2.Distance(DelegateMethods.v2_1, vector2);
      Vector3 vector3 = v31 * MathHelper.Lerp(0.65f, 1f, num / DelegateMethods.f_1);
      Lighting.AddLight(x, y, vector3.X, vector3.Y, vector3.Z);
      return true;
    }

    public static bool CastLightOpen_StopForSolids(int x, int y)
    {
      if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY || Main.tile[x, y] == null || Main.tile[x, y].active() && !Main.tile[x, y].inActive() && !Main.tileSolidTop[(int) Main.tile[x, y].type] && Main.tileSolid[(int) Main.tile[x, y].type])
        return false;
      Vector3 v31 = DelegateMethods.v3_1;
      Vector2 vector2 = new Vector2((float) x, (float) y);
      Lighting.AddLight(x, y, v31.X, v31.Y, v31.Z);
      return true;
    }

    public static bool SpreadLightOpen_StopForSolids(int x, int y)
    {
      if ((double) Vector2.Distance(DelegateMethods.v2_1, new Vector2((float) x, (float) y)) > (double) DelegateMethods.f_1 || Main.tile[x, y].active() && !Main.tile[x, y].inActive() && !Main.tileSolidTop[(int) Main.tile[x, y].type] && Main.tileSolid[(int) Main.tile[x, y].type])
        return false;
      Vector3 v31 = DelegateMethods.v3_1;
      Vector2 vector2 = new Vector2((float) x, (float) y);
      Lighting.AddLight(x, y, v31.X, v31.Y, v31.Z);
      return true;
    }

    public static bool EmitGolfCartDust_StopForSolids(int x, int y)
    {
      if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY || Main.tile[x, y] == null || Main.tile[x, y].active() && !Main.tile[x, y].inActive() && !Main.tileSolidTop[(int) Main.tile[x, y].type] && Main.tileSolid[(int) Main.tile[x, y].type])
        return false;
      Dust.NewDustPerfect(new Vector2((float) (x * 16 + 8), (float) (y * 16 + 8)), 260, new Vector2?(Vector2.UnitY * -0.2f));
      return true;
    }

    public static bool NotDoorStand(int x, int y)
    {
      if (Main.tile[x, y] == null || !Main.tile[x, y].active() || Main.tile[x, y].type != (ushort) 11)
        return true;
      return Main.tile[x, y].frameX >= (short) 18 && Main.tile[x, y].frameX < (short) 54;
    }

    public static bool CutTiles(int x, int y)
    {
      if (!WorldGen.InWorld(x, y, 1) || Main.tile[x, y] == null)
        return false;
      if (!Main.tileCut[(int) Main.tile[x, y].type] || DelegateMethods.tileCutIgnore[(int) Main.tile[x, y].type] || !WorldGen.CanCutTile(x, y, DelegateMethods.tilecut_0))
        return true;
      WorldGen.KillTile(x, y);
      if (Main.netMode != 0)
        NetMessage.SendData(17, number2: ((float) x), number3: ((float) y));
      return true;
    }

    public static bool SearchAvoidedByNPCs(int x, int y) => WorldGen.InWorld(x, y, 1) && Main.tile[x, y] != null && (!Main.tile[x, y].active() || !TileID.Sets.AvoidedByNPCs[(int) Main.tile[x, y].type]);

    public static void RainbowLaserDraw(
      int stage,
      Vector2 currentPosition,
      float distanceLeft,
      Rectangle lastFrame,
      out float distCovered,
      out Rectangle frame,
      out Vector2 origin,
      out Color color)
    {
      color = DelegateMethods.c_1;
      switch (stage)
      {
        case 0:
          distCovered = 33f;
          frame = new Rectangle(0, 0, 26, 22);
          origin = frame.Size() / 2f;
          break;
        case 1:
          frame = new Rectangle(0, 25, 26, 28);
          distCovered = (float) frame.Height;
          origin = new Vector2((float) (frame.Width / 2), 0.0f);
          break;
        case 2:
          distCovered = 22f;
          frame = new Rectangle(0, 56, 26, 22);
          origin = new Vector2((float) (frame.Width / 2), 1f);
          break;
        default:
          distCovered = 9999f;
          frame = Rectangle.Empty;
          origin = Vector2.Zero;
          color = Color.Transparent;
          break;
      }
    }

    public static void TurretLaserDraw(
      int stage,
      Vector2 currentPosition,
      float distanceLeft,
      Rectangle lastFrame,
      out float distCovered,
      out Rectangle frame,
      out Vector2 origin,
      out Color color)
    {
      color = DelegateMethods.c_1;
      switch (stage)
      {
        case 0:
          distCovered = 32f;
          frame = new Rectangle(0, 0, 22, 20);
          origin = frame.Size() / 2f;
          break;
        case 1:
          ++DelegateMethods.i_1;
          int num = DelegateMethods.i_1 % 5;
          frame = new Rectangle(0, 22 * (num + 1), 22, 20);
          distCovered = (float) (frame.Height - 1);
          origin = new Vector2((float) (frame.Width / 2), 0.0f);
          break;
        case 2:
          frame = new Rectangle(0, 154, 22, 30);
          distCovered = (float) frame.Height;
          origin = new Vector2((float) (frame.Width / 2), 1f);
          break;
        default:
          distCovered = 9999f;
          frame = Rectangle.Empty;
          origin = Vector2.Zero;
          color = Color.Transparent;
          break;
      }
    }

    public static void LightningLaserDraw(
      int stage,
      Vector2 currentPosition,
      float distanceLeft,
      Rectangle lastFrame,
      out float distCovered,
      out Rectangle frame,
      out Vector2 origin,
      out Color color)
    {
      color = DelegateMethods.c_1 * DelegateMethods.f_1;
      switch (stage)
      {
        case 0:
          distCovered = 0.0f;
          frame = new Rectangle(0, 0, 21, 8);
          origin = frame.Size() / 2f;
          break;
        case 1:
          frame = new Rectangle(0, 8, 21, 6);
          distCovered = (float) frame.Height;
          origin = new Vector2((float) (frame.Width / 2), 0.0f);
          break;
        case 2:
          distCovered = 8f;
          frame = new Rectangle(0, 14, 21, 8);
          origin = new Vector2((float) (frame.Width / 2), 2f);
          break;
        default:
          distCovered = 9999f;
          frame = Rectangle.Empty;
          origin = Vector2.Zero;
          color = Color.Transparent;
          break;
      }
    }

    public static int CompareYReverse(Point a, Point b) => b.Y.CompareTo(a.Y);

    public static int CompareDrawSorterByYScale(DrawData a, DrawData b) => a.scale.Y.CompareTo(b.scale.Y);

    public static class CharacterPreview
    {
      public static void EtsyPet(Projectile proj, bool walking)
      {
        DelegateMethods.CharacterPreview.Float(proj, walking);
        if (walking)
        {
          float num = (float) (Main.timeForVisualEffects % 90.0 / 90.0);
          proj.localAI[1] = 6.28318548f * num;
        }
        else
          proj.localAI[1] = 0.0f;
      }

      public static void CompanionCubePet(Projectile proj, bool walking)
      {
        if (walking)
        {
          double percent1 = Main.timeForVisualEffects % 30.0 / 30.0;
          float percent2 = (float) (Main.timeForVisualEffects % 120.0 / 120.0);
          float[] numArray = new float[8]
          {
            0.0f,
            0.0f,
            16f,
            20f,
            20f,
            16f,
            0.0f,
            0.0f
          };
          float num1 = Utils.MultiLerp((float) percent1, numArray);
          float num2 = Utils.MultiLerp(percent2, 0.0f, 0.0f, 0.25f, 0.25f, 0.5f, 0.5f, 0.75f, 0.75f, 1f, 1f);
          proj.position.Y -= num1;
          proj.rotation = 6.28318548f * num2;
        }
        else
          proj.rotation = 0.0f;
      }

      public static void BerniePet(Projectile proj, bool walking)
      {
        if (!walking)
          return;
        proj.position.X += 6f;
      }

      public static void SlimePet(Projectile proj, bool walking)
      {
        if (!walking)
          return;
        float percent = (float) (Main.timeForVisualEffects % 30.0 / 30.0);
        // ISSUE: explicit reference operation
        ^ref proj.position.Y -= (float) (double) Utils.MultiLerp(percent, 0.0f, 0.0f, 16f, 20f, 20f, 16f, 0.0f, 0.0f);
      }

      public static void WormPet(Projectile proj, bool walking)
      {
        Vector2 vector2 = (Vector2.UnitY * 2f).RotatedBy(-0.39859879016876221);
        Vector2 position = proj.position;
        int num = proj.oldPos.Length;
        if (proj.type == 893)
          num = proj.oldPos.Length - 30;
        for (int index = 0; index < proj.oldPos.Length; ++index)
        {
          position -= vector2;
          if (index < num)
            proj.oldPos[index] = position;
          else if (index > 0)
            proj.oldPos[index] = proj.oldPos[index - 1];
          vector2 = vector2.RotatedBy(-0.052359879016876221);
        }
        proj.rotation = (float) ((double) vector2.ToRotation() + 0.31415927410125732 + 3.1415927410125732);
        if (proj.type == 887)
          proj.rotation += 0.3926991f;
        if (proj.type != 893)
          return;
        proj.rotation += 1.57079637f;
      }

      public static void FloatAndSpinWhenWalking(Projectile proj, bool walking)
      {
        DelegateMethods.CharacterPreview.Float(proj, walking);
        if (walking)
          proj.rotation = (float) (6.2831854820251465 * (Main.timeForVisualEffects % 20.0 / 20.0));
        else
          proj.rotation = 0.0f;
      }

      public static void FloatAndRotateForwardWhenWalking(Projectile proj, bool walking)
      {
        DelegateMethods.CharacterPreview.Float(proj, walking);
        DelegateMethods.CharacterPreview.RotateForwardWhenWalking(proj, walking);
      }

      public static void Float(Projectile proj, bool walking)
      {
        float num1 = 0.5f;
        float num2 = (float) (Main.timeForVisualEffects % 60.0 / 60.0);
        proj.position.Y += -num1 + (float) (Math.Cos((double) num2 * 6.2831854820251465 * 2.0) * ((double) num1 * 2.0));
      }

      public static void RotateForwardWhenWalking(Projectile proj, bool walking)
      {
        if (walking)
          proj.rotation = 0.5235988f;
        else
          proj.rotation = 0.0f;
      }
    }

    public static class Mount
    {
      public static bool NoHandPosition(Player player, out Vector2? position)
      {
        position = new Vector2?();
        return true;
      }

      public static bool WolfMouthPosition(Player player, out Vector2? position)
      {
        Vector2 spinningpoint = new Vector2((float) (player.direction * 22), player.gravDir * -6f);
        position = new Vector2?(player.RotatedRelativePoint(player.MountedCenter, addGfxOffY: false) + spinningpoint.RotatedBy((double) player.fullRotation));
        return true;
      }
    }

    public static class Minecart
    {
      public static Vector2 rotationOrigin;
      public static float rotation;

      public static void Sparks(Vector2 dustPosition)
      {
        dustPosition += new Vector2(Main.rand.Next(2) == 0 ? 13f : -13f, 0.0f).RotatedBy((double) DelegateMethods.Minecart.rotation);
        int index = Dust.NewDust(dustPosition, 1, 1, 213, (float) Main.rand.Next(-2, 3), (float) Main.rand.Next(-2, 3));
        Main.dust[index].noGravity = true;
        Main.dust[index].fadeIn = (float) ((double) Main.dust[index].scale + 1.0 + 0.0099999997764825821 * (double) Main.rand.Next(0, 51));
        Main.dust[index].noGravity = true;
        Main.dust[index].velocity *= (float) Main.rand.Next(15, 51) * 0.01f;
        Main.dust[index].velocity.X *= (float) Main.rand.Next(25, 101) * 0.01f;
        Main.dust[index].velocity.Y -= (float) Main.rand.Next(15, 31) * 0.1f;
        Main.dust[index].position.Y -= 4f;
        if (Main.rand.Next(3) != 0)
          Main.dust[index].noGravity = false;
        else
          Main.dust[index].scale *= 0.6f;
      }

      public static void JumpingSound(Player Player, Vector2 Position, int Width, int Height)
      {
      }

      public static void LandingSound(Player Player, Vector2 Position, int Width, int Height) => SoundEngine.PlaySound(SoundID.Item53, (int) Position.X + Width / 2, (int) Position.Y + Height / 2);

      public static void BumperSound(Player Player, Vector2 Position, int Width, int Height) => SoundEngine.PlaySound(SoundID.Item56, (int) Position.X + Width / 2, (int) Position.Y + Height / 2);

      public static void SpawnFartCloud(
        Player Player,
        Vector2 Position,
        int Width,
        int Height,
        bool useDelay = true)
      {
        if (useDelay)
        {
          if (Player.fartKartCloudDelay > 0)
            return;
          Player.fartKartCloudDelay = 20;
        }
        float x = 10f;
        float y = -4f;
        Vector2 vector2_1 = Position + new Vector2((float) (Width / 2 - 18), (float) (Height - 16));
        Vector2 v = Player.velocity * 0.1f;
        if ((double) v.Length() > 2.0)
          v = v.SafeNormalize(Vector2.Zero) * 2f;
        int index1 = Gore.NewGore(vector2_1 + new Vector2(0.0f, y), Vector2.Zero, Main.rand.Next(435, 438));
        Main.gore[index1].velocity *= 0.2f;
        Main.gore[index1].velocity += v;
        Main.gore[index1].velocity.Y *= 0.75f;
        int index2 = Gore.NewGore(vector2_1 + new Vector2(-x, y), Vector2.Zero, Main.rand.Next(435, 438));
        Main.gore[index2].velocity *= 0.2f;
        Main.gore[index2].velocity += v;
        Main.gore[index2].velocity.Y *= 0.75f;
        int index3 = Gore.NewGore(vector2_1 + new Vector2(x, y), Vector2.Zero, Main.rand.Next(435, 438));
        Main.gore[index3].velocity *= 0.2f;
        Main.gore[index3].velocity += v;
        Main.gore[index3].velocity.Y *= 0.75f;
        if (!Player.mount.Active || Player.mount.Type != 53)
          return;
        Vector2 vector2_2 = Position + new Vector2((float) (Width / 2), (float) (Height + 10));
        float num1 = 30f;
        float num2 = -16f;
        for (int index4 = 0; index4 < 15; ++index4)
        {
          Dust dust = Dust.NewDustPerfect(vector2_2 + new Vector2((float) (-(double) num1 + (double) num1 * 2.0 * (double) Main.rand.NextFloat()), num2 * Main.rand.NextFloat()), 107, new Vector2?(Vector2.Zero), 100, Color.Lerp(new Color(64, 220, 96), Color.White, Main.rand.NextFloat() * 0.3f), 0.6f);
          dust.velocity *= (float) Main.rand.Next(15, 51) * 0.01f;
          dust.velocity.X *= (float) Main.rand.Next(25, 101) * 0.01f;
          dust.velocity.Y -= (float) Main.rand.Next(15, 31) * 0.1f;
          dust.velocity += v;
          dust.velocity.Y *= 0.75f;
          dust.fadeIn = (float) (0.20000000298023224 + (double) Main.rand.NextFloat() * 0.10000000149011612);
          dust.noGravity = Main.rand.Next(3) == 0;
          dust.noLightEmittence = true;
        }
      }

      public static void JumpingSoundFart(Player Player, Vector2 Position, int Width, int Height)
      {
        SoundEngine.PlaySound(SoundID.Item16, (int) Position.X + Width / 2, (int) Position.Y + Height / 2);
        DelegateMethods.Minecart.SpawnFartCloud(Player, Position, Width, Height, false);
      }

      public static void LandingSoundFart(Player Player, Vector2 Position, int Width, int Height)
      {
        SoundEngine.PlaySound(SoundID.Item16, (int) Position.X + Width / 2, (int) Position.Y + Height / 2);
        SoundEngine.PlaySound(SoundID.Item53, (int) Position.X + Width / 2, (int) Position.Y + Height / 2);
        DelegateMethods.Minecart.SpawnFartCloud(Player, Position, Width, Height, false);
      }

      public static void BumperSoundFart(Player Player, Vector2 Position, int Width, int Height)
      {
        SoundEngine.PlaySound(SoundID.Item16, (int) Position.X + Width / 2, (int) Position.Y + Height / 2);
        SoundEngine.PlaySound(SoundID.Item56, (int) Position.X + Width / 2, (int) Position.Y + Height / 2);
        DelegateMethods.Minecart.SpawnFartCloud(Player, Position, Width, Height);
      }

      public static void SparksFart(Vector2 dustPosition)
      {
        dustPosition += new Vector2(Main.rand.Next(2) == 0 ? 13f : -13f, 0.0f).RotatedBy((double) DelegateMethods.Minecart.rotation);
        int index = Dust.NewDust(dustPosition, 1, 1, 211, (float) Main.rand.Next(-2, 3), (float) Main.rand.Next(-2, 3), 50, Scale: 0.8f);
        if (Main.rand.Next(2) == 0)
          Main.dust[index].alpha += 25;
        if (Main.rand.Next(2) == 0)
          Main.dust[index].alpha += 25;
        Main.dust[index].noLight = true;
        Main.dust[index].noGravity = Main.rand.Next(3) == 0;
        Main.dust[index].velocity *= (float) Main.rand.Next(15, 51) * 0.01f;
        Main.dust[index].velocity.X *= (float) Main.rand.Next(25, 101) * 0.01f;
        Main.dust[index].velocity.Y -= (float) Main.rand.Next(15, 31) * 0.1f;
        Main.dust[index].position.Y -= 4f;
      }

      public static void SparksTerraFart(Vector2 dustPosition)
      {
        if (Main.rand.Next(2) == 0)
        {
          DelegateMethods.Minecart.SparksFart(dustPosition);
        }
        else
        {
          dustPosition += new Vector2(Main.rand.Next(2) == 0 ? 13f : -13f, 0.0f).RotatedBy((double) DelegateMethods.Minecart.rotation);
          int index = Dust.NewDust(dustPosition, 1, 1, 107, (float) Main.rand.Next(-2, 3), (float) Main.rand.Next(-2, 3), 100, Color.Lerp(new Color(64, 220, 96), Color.White, Main.rand.NextFloat() * 0.3f), 0.8f);
          if (Main.rand.Next(2) == 0)
            Main.dust[index].alpha += 25;
          if (Main.rand.Next(2) == 0)
            Main.dust[index].alpha += 25;
          Main.dust[index].noLightEmittence = true;
          Main.dust[index].noGravity = Main.rand.Next(3) == 0;
          Main.dust[index].velocity *= (float) Main.rand.Next(15, 51) * 0.01f;
          Main.dust[index].velocity.X *= (float) Main.rand.Next(25, 101) * 0.01f;
          Main.dust[index].velocity.Y -= (float) Main.rand.Next(15, 31) * 0.1f;
          Main.dust[index].position.Y -= 4f;
        }
      }

      public static void SparksMech(Vector2 dustPosition)
      {
        dustPosition += new Vector2(Main.rand.Next(2) == 0 ? 13f : -13f, 0.0f).RotatedBy((double) DelegateMethods.Minecart.rotation);
        int index = Dust.NewDust(dustPosition, 1, 1, 260, (float) Main.rand.Next(-2, 3), (float) Main.rand.Next(-2, 3));
        Main.dust[index].noGravity = true;
        Main.dust[index].fadeIn = (float) ((double) Main.dust[index].scale + 0.5 + 0.0099999997764825821 * (double) Main.rand.Next(0, 51));
        Main.dust[index].noGravity = true;
        Main.dust[index].velocity *= (float) Main.rand.Next(15, 51) * 0.01f;
        Main.dust[index].velocity.X *= (float) Main.rand.Next(25, 101) * 0.01f;
        Main.dust[index].velocity.Y -= (float) Main.rand.Next(15, 31) * 0.1f;
        Main.dust[index].position.Y -= 4f;
        if (Main.rand.Next(3) != 0)
          Main.dust[index].noGravity = false;
        else
          Main.dust[index].scale *= 0.6f;
      }

      public static void SparksMeow(Vector2 dustPosition)
      {
        dustPosition += new Vector2(Main.rand.Next(2) == 0 ? 13f : -13f, 0.0f).RotatedBy((double) DelegateMethods.Minecart.rotation);
        int index = Dust.NewDust(dustPosition, 1, 1, 213, (float) Main.rand.Next(-2, 3), (float) Main.rand.Next(-2, 3));
        Main.dust[index].shader = GameShaders.Armor.GetShaderFromItemId(2870);
        Main.dust[index].noGravity = true;
        Main.dust[index].fadeIn = (float) ((double) Main.dust[index].scale + 1.0 + 0.0099999997764825821 * (double) Main.rand.Next(0, 51));
        Main.dust[index].noGravity = true;
        Main.dust[index].velocity *= (float) Main.rand.Next(15, 51) * 0.01f;
        Main.dust[index].velocity.X *= (float) Main.rand.Next(25, 101) * 0.01f;
        Main.dust[index].velocity.Y -= (float) Main.rand.Next(15, 31) * 0.1f;
        Main.dust[index].position.Y -= 4f;
        if (Main.rand.Next(3) != 0)
          Main.dust[index].noGravity = false;
        else
          Main.dust[index].scale *= 0.6f;
      }
    }
  }
}
