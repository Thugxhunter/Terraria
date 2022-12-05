// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.PlayerDrawLayers
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.GameContent.Liquid;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.DataStructures
{
  public static class PlayerDrawLayers
  {
    private const int DEFAULT_MAX_SPRITES = 200;
    private static SpriteDrawBuffer spriteBuffer;

    public static void DrawPlayer_extra_TorsoPlus(ref PlayerDrawSet drawinfo)
    {
      drawinfo.Position.Y += drawinfo.torsoOffset;
      drawinfo.ItemLocation.Y += drawinfo.torsoOffset;
    }

    public static void DrawPlayer_extra_TorsoMinus(ref PlayerDrawSet drawinfo)
    {
      drawinfo.Position.Y -= drawinfo.torsoOffset;
      drawinfo.ItemLocation.Y -= drawinfo.torsoOffset;
    }

    public static void DrawPlayer_extra_MountPlus(ref PlayerDrawSet drawinfo) => drawinfo.Position.Y += (float) ((int) drawinfo.mountOffSet / 2);

    public static void DrawPlayer_extra_MountMinus(ref PlayerDrawSet drawinfo) => drawinfo.Position.Y -= (float) ((int) drawinfo.mountOffSet / 2);

    public static void DrawCompositeArmorPiece(
      ref PlayerDrawSet drawinfo,
      CompositePlayerDrawContext context,
      DrawData data)
    {
      drawinfo.DrawDataCache.Add(data);
      switch (context)
      {
        case CompositePlayerDrawContext.BackShoulder:
        case CompositePlayerDrawContext.BackArm:
        case CompositePlayerDrawContext.FrontArm:
        case CompositePlayerDrawContext.FrontShoulder:
          if (drawinfo.armGlowColor.PackedValue > 0U)
          {
            DrawData drawData = data with
            {
              color = drawinfo.armGlowColor
            };
            Rectangle rectangle = drawData.sourceRect.Value;
            rectangle.Y += 224;
            drawData.sourceRect = new Rectangle?(rectangle);
            if (drawinfo.drawPlayer.body == 227)
            {
              Vector2 position = drawData.position;
              for (int index = 0; index < 2; ++index)
              {
                Vector2 vector2 = new Vector2((float) Main.rand.Next(-10, 10) * 0.125f, (float) Main.rand.Next(-10, 10) * 0.125f);
                drawData.position = position + vector2;
                if (index == 0)
                  drawinfo.DrawDataCache.Add(drawData);
              }
            }
            drawinfo.DrawDataCache.Add(drawData);
            break;
          }
          break;
        case CompositePlayerDrawContext.Torso:
          if (drawinfo.bodyGlowColor.PackedValue > 0U)
          {
            DrawData drawData = data with
            {
              color = drawinfo.bodyGlowColor
            };
            Rectangle rectangle = drawData.sourceRect.Value;
            rectangle.Y += 224;
            drawData.sourceRect = new Rectangle?(rectangle);
            if (drawinfo.drawPlayer.body == 227)
            {
              Vector2 position = drawData.position;
              for (int index = 0; index < 2; ++index)
              {
                Vector2 vector2 = new Vector2((float) Main.rand.Next(-10, 10) * 0.125f, (float) Main.rand.Next(-10, 10) * 0.125f);
                drawData.position = position + vector2;
                if (index == 0)
                  drawinfo.DrawDataCache.Add(drawData);
              }
            }
            drawinfo.DrawDataCache.Add(drawData);
            break;
          }
          break;
      }
      if (context == CompositePlayerDrawContext.FrontShoulder && drawinfo.drawPlayer.head == 269)
      {
        Vector2 position = drawinfo.helmetOffset + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect;
        DrawData drawData = new DrawData(TextureAssets.Extra[214].Value, position, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cHead;
        drawinfo.DrawDataCache.Add(drawData);
        drawData = new DrawData(TextureAssets.GlowMask[308].Value, position, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cHead;
        drawinfo.DrawDataCache.Add(drawData);
      }
      if (context != CompositePlayerDrawContext.FrontArm || drawinfo.drawPlayer.body != 205)
        return;
      Color color = new Color(100, 100, 100, 0);
      ulong seed = (ulong) (drawinfo.drawPlayer.miscCounter / 4);
      int num1 = 4;
      for (int index = 0; index < num1; ++index)
      {
        float num2 = (float) Utils.RandomInt(ref seed, -10, 11) * 0.2f;
        float num3 = (float) Utils.RandomInt(ref seed, -10, 1) * 0.15f;
        DrawData drawData = data;
        Rectangle rectangle = drawData.sourceRect.Value;
        rectangle.Y += 224;
        drawData.sourceRect = new Rectangle?(rectangle);
        drawData.position.X += num2;
        drawData.position.Y += num3;
        drawData.color = color;
        drawinfo.DrawDataCache.Add(drawData);
      }
    }

    public static void DrawPlayer_01_BackHair(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.hideHair || !drawinfo.backHairDraw)
        return;
      Vector2 position = new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + drawinfo.hairOffset;
      if (drawinfo.drawPlayer.head == -1 || drawinfo.fullHair || drawinfo.drawsBackHairWithoutHeadgear)
      {
        drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, position, new Rectangle?(drawinfo.hairBackFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect)
        {
          shader = drawinfo.hairDyePacked
        });
      }
      else
      {
        if (!drawinfo.hatHair)
          return;
        drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.PlayerHairAlt[drawinfo.drawPlayer.hair].Value, position, new Rectangle?(drawinfo.hairBackFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect)
        {
          shader = drawinfo.hairDyePacked
        });
      }
    }

    public static void DrawPlayer_02_MountBehindPlayer(ref PlayerDrawSet drawinfo)
    {
      if (!drawinfo.drawPlayer.mount.Active)
        return;
      PlayerDrawLayers.DrawMeowcartTrail(ref drawinfo);
      PlayerDrawLayers.DrawTiedBalloons(ref drawinfo);
      drawinfo.drawPlayer.mount.Draw(drawinfo.DrawDataCache, 0, drawinfo.drawPlayer, drawinfo.Position, drawinfo.colorMount, drawinfo.playerEffect, drawinfo.shadow);
      drawinfo.drawPlayer.mount.Draw(drawinfo.DrawDataCache, 1, drawinfo.drawPlayer, drawinfo.Position, drawinfo.colorMount, drawinfo.playerEffect, drawinfo.shadow);
    }

    public static void DrawPlayer_03_Carpet(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.carpetFrame < 0)
        return;
      Color colorArmorLegs = drawinfo.colorArmorLegs;
      float num = 0.0f;
      if ((double) drawinfo.drawPlayer.gravDir == -1.0)
        num = 10f;
      drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.FlyingCarpet.Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) (drawinfo.drawPlayer.height / 2) + 28.0 * (double) drawinfo.drawPlayer.gravDir + (double) num)), new Rectangle?(new Rectangle(0, TextureAssets.FlyingCarpet.Height() / 6 * drawinfo.drawPlayer.carpetFrame, TextureAssets.FlyingCarpet.Width(), TextureAssets.FlyingCarpet.Height() / 6)), colorArmorLegs, drawinfo.drawPlayer.bodyRotation, new Vector2((float) (TextureAssets.FlyingCarpet.Width() / 2), (float) (TextureAssets.FlyingCarpet.Height() / 8)), 1f, drawinfo.playerEffect)
      {
        shader = drawinfo.cCarpet
      });
    }

    public static void DrawPlayer_03_PortableStool(ref PlayerDrawSet drawinfo)
    {
      if (!drawinfo.drawPlayer.portableStoolInfo.IsInUse)
        return;
      Texture2D texture2D = TextureAssets.Extra[102].Value;
      Vector2 position = new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height + 28.0));
      Rectangle r = texture2D.Frame();
      Vector2 origin = r.Size() * new Vector2(0.5f, 1f);
      drawinfo.DrawDataCache.Add(new DrawData(texture2D, position, new Rectangle?(r), drawinfo.colorArmorLegs, drawinfo.drawPlayer.bodyRotation, origin, 1f, drawinfo.playerEffect)
      {
        shader = drawinfo.cPortableStool
      });
    }

    public static void DrawPlayer_04_ElectrifiedDebuffBack(ref PlayerDrawSet drawinfo)
    {
      if (!drawinfo.drawPlayer.electrified || (double) drawinfo.shadow != 0.0)
        return;
      Texture2D texture = TextureAssets.GlowMask[25].Value;
      int num1 = drawinfo.drawPlayer.miscCounter / 5;
      for (int index = 0; index < 2; ++index)
      {
        int num2 = num1 % 7;
        if (num2 <= 1 || num2 >= 5)
        {
          DrawData drawData = new DrawData(texture, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(new Rectangle(0, num2 * texture.Height / 7, texture.Width, texture.Height / 7)), drawinfo.colorElectricity, drawinfo.drawPlayer.bodyRotation, new Vector2((float) (texture.Width / 2), (float) (texture.Height / 14)), 1f, drawinfo.playerEffect);
          drawinfo.DrawDataCache.Add(drawData);
        }
        num1 = num2 + 3;
      }
    }

    public static void DrawPlayer_05_ForbiddenSetRing(ref PlayerDrawSet drawinfo)
    {
      if (!drawinfo.drawPlayer.setForbidden || (double) drawinfo.shadow != 0.0)
        return;
      Color color1 = Color.Lerp(drawinfo.colorArmorBody, Color.White, 0.7f);
      Texture2D texture2D = TextureAssets.Extra[74].Value;
      Texture2D texture = TextureAssets.GlowMask[217].Value;
      int num1 = !drawinfo.drawPlayer.setForbiddenCooldownLocked ? 1 : 0;
      int num2 = (int) ((double) ((float) ((double) drawinfo.drawPlayer.miscCounter / 300.0 * 6.2831854820251465)).ToRotationVector2().Y * 6.0);
      float num3 = ((float) ((double) drawinfo.drawPlayer.miscCounter / 75.0 * 6.2831854820251465)).ToRotationVector2().X * 4f;
      Color color2 = new Color(80, 70, 40, 0) * (float) ((double) num3 / 8.0 + 0.5) * 0.8f;
      if (num1 == 0)
      {
        num2 = 0;
        num3 = 2f;
        color2 = new Color(80, 70, 40, 0) * 0.3f;
        color1 = color1.MultiplyRGB(new Color(0.5f, 0.5f, 1f));
      }
      Vector2 vector2 = new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2));
      int num4 = 10;
      int num5 = 20;
      if (drawinfo.drawPlayer.head == 238)
      {
        num4 += 4;
        num5 += 4;
      }
      Vector2 position = vector2 + new Vector2((float) (-drawinfo.drawPlayer.direction * num4), (float) ((double) -num5 * (double) drawinfo.drawPlayer.gravDir + (double) num2 * (double) drawinfo.drawPlayer.gravDir));
      DrawData drawData = new DrawData(texture2D, position, new Rectangle?(), color1, drawinfo.drawPlayer.bodyRotation, texture2D.Size() / 2f, 1f, drawinfo.playerEffect);
      drawData.shader = drawinfo.cBody;
      drawinfo.DrawDataCache.Add(drawData);
      for (float num6 = 0.0f; (double) num6 < 4.0; ++num6)
      {
        drawData = new DrawData(texture, position + (num6 * 1.57079637f).ToRotationVector2() * num3, new Rectangle?(), color2, drawinfo.drawPlayer.bodyRotation, texture2D.Size() / 2f, 1f, drawinfo.playerEffect);
        drawinfo.DrawDataCache.Add(drawData);
      }
    }

    public static void DrawPlayer_01_3_BackHead(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.head < 0 || drawinfo.drawPlayer.head >= ArmorIDs.Head.Count)
        return;
      int index = ArmorIDs.Head.Sets.FrontToBackID[drawinfo.drawPlayer.head];
      if (index < 0)
        return;
      Vector2 helmetOffset = drawinfo.helmetOffset;
      drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.ArmorHead[index].Value, helmetOffset + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect)
      {
        shader = drawinfo.cHead
      });
    }

    public static void DrawPlayer_01_2_JimsCloak(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.legs != 60 || drawinfo.isSitting || drawinfo.drawPlayer.invis || PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo) && !drawinfo.drawPlayer.wearsRobe)
        return;
      drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Extra[153].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.legFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.legFrame.Height + 4.0)) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect)
      {
        shader = drawinfo.cLegs
      });
    }

    public static void DrawPlayer_05_2_SafemanSun(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.head != 238 || (double) drawinfo.shadow != 0.0)
        return;
      Color color1 = Color.Lerp(drawinfo.colorArmorBody, Color.White, 0.7f);
      Texture2D texture2D = TextureAssets.Extra[152].Value;
      Texture2D texture = TextureAssets.Extra[152].Value;
      int num1 = (int) ((double) ((float) ((double) drawinfo.drawPlayer.miscCounter / 300.0 * 6.2831854820251465)).ToRotationVector2().Y * 6.0);
      float num2 = ((float) ((double) drawinfo.drawPlayer.miscCounter / 75.0 * 6.2831854820251465)).ToRotationVector2().X * 4f;
      Color color2 = new Color(80, 70, 40, 0) * (float) ((double) num2 / 8.0 + 0.5) * 0.8f;
      Vector2 vector2 = new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2));
      int num3 = 8;
      int num4 = 20;
      int num5 = num3 + 4;
      int num6 = num4 + 4;
      Vector2 position = vector2 + new Vector2((float) (-drawinfo.drawPlayer.direction * num5), (float) ((double) -num6 * (double) drawinfo.drawPlayer.gravDir + (double) num1 * (double) drawinfo.drawPlayer.gravDir));
      DrawData drawData = new DrawData(texture2D, position, new Rectangle?(), color1, drawinfo.drawPlayer.bodyRotation, texture2D.Size() / 2f, 1f, drawinfo.playerEffect);
      drawData.shader = drawinfo.cHead;
      drawinfo.DrawDataCache.Add(drawData);
      for (float num7 = 0.0f; (double) num7 < 4.0; ++num7)
      {
        drawData = new DrawData(texture, position + (num7 * 1.57079637f).ToRotationVector2() * num2, new Rectangle?(), color2, drawinfo.drawPlayer.bodyRotation, texture2D.Size() / 2f, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cHead;
        drawinfo.DrawDataCache.Add(drawData);
      }
    }

    public static void DrawPlayer_06_WebbedDebuffBack(ref PlayerDrawSet drawinfo)
    {
      if (!drawinfo.drawPlayer.webbed || (double) drawinfo.shadow != 0.0 || (double) drawinfo.drawPlayer.velocity.Y == 0.0)
        return;
      Color color = drawinfo.colorArmorBody * 0.75f;
      Texture2D texture2D = TextureAssets.Extra[32].Value;
      DrawData drawData = new DrawData(texture2D, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(), color, drawinfo.drawPlayer.bodyRotation, texture2D.Size() / 2f, 1f, drawinfo.playerEffect);
      drawinfo.DrawDataCache.Add(drawData);
    }

    public static void DrawPlayer_07_LeinforsHairShampoo(ref PlayerDrawSet drawinfo)
    {
      if (!drawinfo.drawPlayer.leinforsHair || !drawinfo.fullHair && !drawinfo.hatHair && !drawinfo.drawsBackHairWithoutHeadgear && drawinfo.drawPlayer.head != -1 && drawinfo.drawPlayer.head != 0 || drawinfo.drawPlayer.hair == 12 || (double) drawinfo.shadow != 0.0 || (double) Main.rgbToHsl(drawinfo.colorHead).Z <= 0.20000000298023224)
        return;
      if (Main.rand.Next(20) == 0 && !drawinfo.hatHair)
      {
        Rectangle r = Utils.CenteredRectangle(drawinfo.Position + drawinfo.drawPlayer.Size / 2f + new Vector2(0.0f, drawinfo.drawPlayer.gravDir * -20f), new Vector2(20f, 14f));
        int index = Dust.NewDust(r.TopLeft(), r.Width, r.Height, 204, Alpha: 150, Scale: 0.3f);
        Main.dust[index].fadeIn = 1f;
        Main.dust[index].velocity *= 0.1f;
        Main.dust[index].noLight = true;
        Main.dust[index].shader = GameShaders.Armor.GetSecondaryShader(drawinfo.drawPlayer.cLeinShampoo, drawinfo.drawPlayer);
        drawinfo.DustCache.Add(index);
      }
      if (Main.rand.Next(40) == 0 && drawinfo.hatHair)
      {
        Rectangle r = Utils.CenteredRectangle(drawinfo.Position + drawinfo.drawPlayer.Size / 2f + new Vector2((float) (drawinfo.drawPlayer.direction * -10), drawinfo.drawPlayer.gravDir * -10f), new Vector2(5f, 5f));
        int index = Dust.NewDust(r.TopLeft(), r.Width, r.Height, 204, Alpha: 150, Scale: 0.3f);
        Main.dust[index].fadeIn = 1f;
        Main.dust[index].velocity *= 0.1f;
        Main.dust[index].noLight = true;
        Main.dust[index].shader = GameShaders.Armor.GetSecondaryShader(drawinfo.drawPlayer.cLeinShampoo, drawinfo.drawPlayer);
        drawinfo.DustCache.Add(index);
      }
      if ((double) drawinfo.drawPlayer.velocity.X == 0.0 || !drawinfo.backHairDraw || Main.rand.Next(15) != 0)
        return;
      Rectangle r1 = Utils.CenteredRectangle(drawinfo.Position + drawinfo.drawPlayer.Size / 2f + new Vector2((float) (drawinfo.drawPlayer.direction * -14), 0.0f), new Vector2(4f, 30f));
      int index1 = Dust.NewDust(r1.TopLeft(), r1.Width, r1.Height, 204, Alpha: 150, Scale: 0.3f);
      Main.dust[index1].fadeIn = 1f;
      Main.dust[index1].velocity *= 0.1f;
      Main.dust[index1].noLight = true;
      Main.dust[index1].shader = GameShaders.Armor.GetSecondaryShader(drawinfo.drawPlayer.cLeinShampoo, drawinfo.drawPlayer);
      drawinfo.DustCache.Add(index1);
    }

    public static bool DrawPlayer_08_PlayerVisuallyHasFullArmorSet(
      PlayerDrawSet drawinfo,
      int head,
      int body,
      int legs)
    {
      return drawinfo.drawPlayer.head == head && drawinfo.drawPlayer.body == body && drawinfo.drawPlayer.legs == legs;
    }

    public static void DrawPlayer_08_Backpacks(ref PlayerDrawSet drawinfo)
    {
      DrawData drawData;
      if (PlayerDrawLayers.DrawPlayer_08_PlayerVisuallyHasFullArmorSet(drawinfo, 266, 235, 218))
      {
        Vector2 position = (new Vector2((float) (-2.0 * (double) drawinfo.drawPlayer.Directions.X - 2.0), 0.0f) + drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.width / 2), (float) (drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2))).Floor();
        Texture2D texture2D = TextureAssets.Extra[212].Value;
        Rectangle rectangle = texture2D.Frame(verticalFrames: 5, frameY: (drawinfo.drawPlayer.miscCounter % 25 / 5));
        Color color = drawinfo.drawPlayer.GetImmuneAlphaPure(new Color(250, 250, 250, 200), drawinfo.shadow) * drawinfo.drawPlayer.stealth;
        drawData = new DrawData(texture2D, position, new Rectangle?(rectangle), color, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cBody;
        drawinfo.DrawDataCache.Add(drawData);
      }
      if (PlayerDrawLayers.DrawPlayer_08_PlayerVisuallyHasFullArmorSet(drawinfo, 268, 237, 222))
      {
        Vector2 position = (new Vector2((float) (1.0 * (double) drawinfo.drawPlayer.Directions.X - 9.0), -4f * drawinfo.drawPlayer.Directions.Y) + drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.width / 2), (float) (drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2))).Floor();
        Texture2D texture2D = TextureAssets.Extra[213].Value;
        Rectangle rectangle = texture2D.Frame(verticalFrames: 5, frameY: (drawinfo.drawPlayer.miscCounter % 25 / 5));
        Color color = drawinfo.drawPlayer.GetImmuneAlphaPure(new Color(250, 250, 250, 200), drawinfo.shadow) * drawinfo.drawPlayer.stealth;
        drawData = new DrawData(texture2D, position, new Rectangle?(rectangle), color, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cBody;
        drawinfo.DrawDataCache.Add(drawData);
      }
      if (drawinfo.heldItem.type == 4818 && drawinfo.drawPlayer.ownedProjectileCounts[902] == 0)
      {
        int index = 8;
        Vector2 vector2 = new Vector2(0.0f, 8f);
        Vector2 position = (drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.width / 2), (float) (drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2(0.0f, -4f) + vector2).Floor();
        drawData = new DrawData(TextureAssets.BackPack[index].Value, position, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
        drawinfo.DrawDataCache.Add(drawData);
      }
      if (drawinfo.drawPlayer.backpack > (sbyte) 0 && (int) drawinfo.drawPlayer.backpack < ArmorIDs.Back.Count && !drawinfo.drawPlayer.mount.Active)
      {
        Vector2 vector2 = new Vector2(0.0f, 8f);
        Vector2 position = (drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.width / 2), (float) (drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2(0.0f, -4f) + vector2).Floor();
        drawData = new DrawData(TextureAssets.AccBack[(int) drawinfo.drawPlayer.backpack].Value, position, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cBackpack;
        drawinfo.DrawDataCache.Add(drawData);
      }
      else
      {
        if (drawinfo.heldItem.type != 1178 && drawinfo.heldItem.type != 779 && drawinfo.heldItem.type != 5134 && drawinfo.heldItem.type != 1295 && drawinfo.heldItem.type != 1910 && !drawinfo.drawPlayer.turtleArmor && drawinfo.drawPlayer.body != 106 && drawinfo.drawPlayer.body != 170)
          return;
        int type = drawinfo.heldItem.type;
        int index = 1;
        float num1 = -4f;
        float num2 = -8f;
        int num3 = 0;
        if (drawinfo.drawPlayer.turtleArmor)
        {
          index = 4;
          num3 = drawinfo.cBody;
        }
        else if (drawinfo.drawPlayer.body == 106)
        {
          index = 6;
          num3 = drawinfo.cBody;
        }
        else if (drawinfo.drawPlayer.body == 170)
        {
          index = 7;
          num3 = drawinfo.cBody;
        }
        else
        {
          switch (type)
          {
            case 779:
              index = 2;
              break;
            case 1178:
              index = 1;
              break;
            case 1295:
              index = 3;
              break;
            case 1910:
              index = 5;
              break;
            case 5134:
              index = 9;
              break;
          }
        }
        Vector2 vector2 = new Vector2(0.0f, 8f);
        Vector2 position1 = (drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.width / 2), (float) (drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2(0.0f, -4f) + vector2).Floor();
        Vector2 position2 = (drawinfo.Position - Main.screenPosition + new Vector2((float) (drawinfo.drawPlayer.width / 2), (float) (drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2((num1 - 9f) * (float) drawinfo.drawPlayer.direction, (2f + num2) * drawinfo.drawPlayer.gravDir) + vector2).Floor();
        switch (index)
        {
          case 4:
          case 6:
            drawData = new DrawData(TextureAssets.BackPack[index].Value, position1, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
            drawData.shader = num3;
            drawinfo.DrawDataCache.Add(drawData);
            break;
          case 7:
            drawData = new DrawData(TextureAssets.BackPack[index].Value, position1, new Rectangle?(new Rectangle(0, drawinfo.drawPlayer.bodyFrame.Y, TextureAssets.BackPack[index].Width(), drawinfo.drawPlayer.bodyFrame.Height)), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float) TextureAssets.BackPack[index].Width() * 0.5f, drawinfo.bodyVect.Y), 1f, drawinfo.playerEffect);
            drawData.shader = num3;
            drawinfo.DrawDataCache.Add(drawData);
            break;
          default:
            drawData = new DrawData(TextureAssets.BackPack[index].Value, position2, new Rectangle?(new Rectangle(0, 0, TextureAssets.BackPack[index].Width(), TextureAssets.BackPack[index].Height())), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float) (TextureAssets.BackPack[index].Width() / 2), (float) (TextureAssets.BackPack[index].Height() / 2)), 1f, drawinfo.playerEffect);
            drawData.shader = num3;
            drawinfo.DrawDataCache.Add(drawData);
            break;
        }
      }
    }

    public static void DrawPlayer_08_1_Tails(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.tail <= (sbyte) 0 || (int) drawinfo.drawPlayer.tail >= ArmorIDs.Back.Count || drawinfo.drawPlayer.mount.Active)
        return;
      Vector2 zero = Vector2.Zero;
      if (drawinfo.isSitting)
        zero.Y += -2f;
      Vector2 vector2 = new Vector2(0.0f, 8f);
      Vector2 position = (zero + drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.width / 2), (float) (drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2(0.0f, -4f) + vector2).Floor();
      drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.AccBack[(int) drawinfo.drawPlayer.tail].Value, position, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect)
      {
        shader = drawinfo.cTail
      });
    }

    public static void DrawPlayer_10_BackAcc(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.back <= (sbyte) 0 || (int) drawinfo.drawPlayer.back >= ArmorIDs.Back.Count)
        return;
      if (drawinfo.drawPlayer.front >= (sbyte) 1 && drawinfo.drawPlayer.front <= (sbyte) 4)
      {
        int num = drawinfo.drawPlayer.bodyFrame.Y / 56;
        if (num < 1 || num > 5)
        {
          drawinfo.armorAdjust = 10;
        }
        else
        {
          if (drawinfo.drawPlayer.front == (sbyte) 1)
            drawinfo.armorAdjust = 0;
          if (drawinfo.drawPlayer.front == (sbyte) 2)
            drawinfo.armorAdjust = 8;
          if (drawinfo.drawPlayer.front == (sbyte) 3)
            drawinfo.armorAdjust = 0;
          if (drawinfo.drawPlayer.front == (sbyte) 4)
            drawinfo.armorAdjust = 8;
        }
      }
      Vector2 zero = Vector2.Zero;
      Vector2 vector2 = new Vector2(0.0f, 8f);
      Vector2 position1 = drawinfo.Position;
      Vector2 position2 = (zero + position1 - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.width / 2), (float) (drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2(0.0f, -4f) + vector2).Floor();
      DrawData drawData = new DrawData(TextureAssets.AccBack[(int) drawinfo.drawPlayer.back].Value, position2, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
      drawData.shader = drawinfo.cBack;
      drawinfo.DrawDataCache.Add(drawData);
      if (drawinfo.drawPlayer.back != (sbyte) 36)
        return;
      Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
      Rectangle rectangle = bodyFrame with
      {
        Width = 2
      };
      int num1 = 0;
      int num2 = bodyFrame.Width / 2;
      int num3 = 2;
      if (drawinfo.playerEffect.HasFlag((Enum) SpriteEffects.FlipHorizontally))
      {
        num1 = bodyFrame.Width - 2;
        num3 = -2;
      }
      for (int index = 0; index < num2; ++index)
      {
        rectangle.X = bodyFrame.X + 2 * index;
        Color color = drawinfo.drawPlayer.GetImmuneAlpha(LiquidRenderer.GetShimmerGlitterColor(true, (float) index / 16f, 0.0f), drawinfo.shadow) * ((float) drawinfo.colorArmorBody.A / (float) byte.MaxValue);
        drawData = new DrawData(TextureAssets.GlowMask[332].Value, position2 + new Vector2((float) (num1 + index * num3), 0.0f), new Rectangle?(rectangle), color, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cBack;
        drawinfo.DrawDataCache.Add(drawData);
      }
    }

    public static void DrawPlayer_09_Wings(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.dead || drawinfo.hideEntirePlayer)
        return;
      Vector2 directions = drawinfo.drawPlayer.Directions;
      Vector2 vector2_1 = drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.Size / 2f;
      Vector2 vector2_2 = new Vector2(0.0f, 7f);
      Vector2 commonWingPosPreFloor = drawinfo.Position - Main.screenPosition + new Vector2((float) (drawinfo.drawPlayer.width / 2), (float) (drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + vector2_2;
      if (drawinfo.drawPlayer.wings <= 0)
        return;
      Main.instance.LoadWings(drawinfo.drawPlayer.wings);
      if (drawinfo.drawPlayer.wings == 22)
      {
        if (!drawinfo.drawPlayer.ShouldDrawWingsThatAreAlwaysAnimated())
          return;
        Main.instance.LoadItemFlames(1866);
        Color colorArmorBody = drawinfo.colorArmorBody;
        int y = 26;
        int x = -9;
        Vector2 vec = commonWingPosPreFloor + new Vector2((float) x, (float) y) * directions;
        DrawData drawData;
        if ((double) drawinfo.shadow == 0.0 && drawinfo.drawPlayer.grappling[0] == -1)
        {
          for (int index = 0; index < 7; ++index)
          {
            Color color = new Color(250 - index * 10, 250 - index * 10, 250 - index * 10, 150 - index * 10);
            Vector2 vector2_3 = new Vector2((float) Main.rand.Next(-10, 11) * 0.2f, (float) Main.rand.Next(-10, 11) * 0.2f);
            drawinfo.stealth *= drawinfo.stealth;
            drawinfo.stealth *= 1f - drawinfo.shadow;
            color = new Color((int) ((double) color.R * (double) drawinfo.stealth), (int) ((double) color.G * (double) drawinfo.stealth), (int) ((double) color.B * (double) drawinfo.stealth), (int) ((double) color.A * (double) drawinfo.stealth));
            vector2_3.X = drawinfo.drawPlayer.itemFlamePos[index].X;
            vector2_3.Y = -drawinfo.drawPlayer.itemFlamePos[index].Y;
            vector2_3 *= 0.5f;
            Vector2 position = (vec + vector2_3).Floor();
            drawData = new DrawData(TextureAssets.ItemFlame[1866].Value, position, new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7 - 2)), color, drawinfo.drawPlayer.bodyRotation, new Vector2((float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 14)), 1f, drawinfo.playerEffect);
            drawData.shader = drawinfo.cWings;
            drawinfo.DrawDataCache.Add(drawData);
          }
        }
        drawData = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vec.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7)), colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 14)), 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cWings;
        drawinfo.DrawDataCache.Add(drawData);
      }
      else if (drawinfo.drawPlayer.wings == 28)
      {
        if (!drawinfo.drawPlayer.ShouldDrawWingsThatAreAlwaysAnimated())
          return;
        Color colorArmorBody = drawinfo.colorArmorBody;
        Vector2 vector2_4 = new Vector2(0.0f, 19f);
        Vector2 vec = commonWingPosPreFloor + vector2_4 * directions;
        Texture2D texture2D = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
        Rectangle r = texture2D.Frame(verticalFrames: 4, frameY: (drawinfo.drawPlayer.miscCounter / 5 % 4));
        r.Width -= 2;
        r.Height -= 2;
        DrawData drawData = new DrawData(texture2D, vec.Floor(), new Rectangle?(r), Color.Lerp(colorArmorBody, Color.White, 1f), drawinfo.drawPlayer.bodyRotation, r.Size() / 2f, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cWings;
        drawinfo.DrawDataCache.Add(drawData);
        drawData = new DrawData(TextureAssets.Extra[38].Value, vec.Floor(), new Rectangle?(r), Color.Lerp(colorArmorBody, Color.White, 0.5f), drawinfo.drawPlayer.bodyRotation, r.Size() / 2f, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cWings;
        drawinfo.DrawDataCache.Add(drawData);
      }
      else if (drawinfo.drawPlayer.wings == 45)
      {
        if (!drawinfo.drawPlayer.ShouldDrawWingsThatAreAlwaysAnimated())
          return;
        PlayerDrawLayers.DrawStarboardRainbowTrail(ref drawinfo, commonWingPosPreFloor, directions);
        Color color1 = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        int y = 22;
        int x = 0;
        Vector2 vec = commonWingPosPreFloor + new Vector2((float) x, (float) y) * directions;
        double num1 = 1.0 - (double) drawinfo.shadow;
        Color color2 = color1 * (float) num1;
        DrawData drawData = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vec.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 6 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 6)), color2, drawinfo.drawPlayer.bodyRotation, new Vector2((float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 12)), 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cWings;
        drawinfo.DrawDataCache.Add(drawData);
        if ((double) drawinfo.shadow != 0.0)
          return;
        float num2 = ((float) ((double) drawinfo.drawPlayer.miscCounter / 75.0 * 6.2831854820251465)).ToRotationVector2().X * 4f;
        Color color3 = new Color(70, 70, 70, 0) * (float) ((double) num2 / 8.0 + 0.5) * 0.4f;
        for (float f = 0.0f; (double) f < 6.2831854820251465; f += 1.57079637f)
        {
          drawData = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vec.Floor() + f.ToRotationVector2() * num2, new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 6 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 6)), color3, drawinfo.drawPlayer.bodyRotation, new Vector2((float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 12)), 1f, drawinfo.playerEffect);
          drawData.shader = drawinfo.cWings;
          drawinfo.DrawDataCache.Add(drawData);
        }
      }
      else if (drawinfo.drawPlayer.wings == 34)
      {
        if (!drawinfo.drawPlayer.ShouldDrawWingsThatAreAlwaysAnimated())
          return;
        drawinfo.stealth *= drawinfo.stealth;
        drawinfo.stealth *= 1f - drawinfo.shadow;
        Color color = new Color((int) (250.0 * (double) drawinfo.stealth), (int) (250.0 * (double) drawinfo.stealth), (int) (250.0 * (double) drawinfo.stealth), (int) (100.0 * (double) drawinfo.stealth));
        Vector2 vector2_5 = new Vector2(0.0f, 0.0f);
        Texture2D texture2D = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
        Vector2 vec = drawinfo.Position + drawinfo.drawPlayer.Size / 2f - Main.screenPosition + vector2_5 * drawinfo.drawPlayer.Directions - Vector2.UnitX * (float) drawinfo.drawPlayer.direction * 4f;
        Rectangle r = texture2D.Frame(verticalFrames: 6, frameY: drawinfo.drawPlayer.wingFrame);
        r.Width -= 2;
        r.Height -= 2;
        drawinfo.DrawDataCache.Add(new DrawData(texture2D, vec.Floor(), new Rectangle?(r), color, drawinfo.drawPlayer.bodyRotation, r.Size() / 2f, 1f, drawinfo.playerEffect)
        {
          shader = drawinfo.cWings
        });
      }
      else if (drawinfo.drawPlayer.wings == 40)
      {
        drawinfo.stealth *= drawinfo.stealth;
        drawinfo.stealth *= 1f - drawinfo.shadow;
        Color color = new Color((int) (250.0 * (double) drawinfo.stealth), (int) (250.0 * (double) drawinfo.stealth), (int) (250.0 * (double) drawinfo.stealth), (int) (100.0 * (double) drawinfo.stealth));
        Vector2 vector2_6 = new Vector2(-4f, 0.0f);
        Texture2D texture2D = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
        Vector2 vector2_7 = commonWingPosPreFloor + vector2_6 * directions;
        for (int index = 0; index < 1; ++index)
        {
          SpriteEffects playerEffect = drawinfo.playerEffect;
          Vector2 scale = new Vector2(1f);
          Vector2 zero = Vector2.Zero with
          {
            X = (float) (drawinfo.drawPlayer.direction * 3)
          };
          if (index == 1)
          {
            playerEffect ^= SpriteEffects.FlipHorizontally;
            scale = new Vector2(0.7f, 1f);
            zero.X += (float) -drawinfo.drawPlayer.direction * 6f;
          }
          Vector2 vector2_8 = drawinfo.drawPlayer.velocity * -1.5f;
          int num3 = 0;
          int num4 = 8;
          float num5 = 4f;
          if ((double) drawinfo.drawPlayer.velocity.Y == 0.0)
          {
            num3 = 8;
            num4 = 14;
            num5 = 3f;
          }
          for (int frameY = num3; frameY < num4; ++frameY)
          {
            Vector2 vector2_9 = vector2_7;
            Rectangle r = texture2D.Frame(verticalFrames: 14, frameY: frameY);
            r.Width -= 2;
            r.Height -= 2;
            int num6 = (frameY - num3) % (int) num5;
            Vector2 vector2_10 = new Vector2(0.0f, 0.5f).RotatedBy(((double) drawinfo.drawPlayer.miscCounterNormalized * (2.0 + (double) num6) + (double) num6 * 0.5 + (double) index * 1.2999999523162842) * 6.2831854820251465) * (float) (num6 + 1);
            Vector2 vec = vector2_9 + vector2_10 + vector2_8 * ((float) num6 / num5) + zero;
            drawinfo.DrawDataCache.Add(new DrawData(texture2D, vec.Floor(), new Rectangle?(r), color, drawinfo.drawPlayer.bodyRotation, r.Size() / 2f, scale, playerEffect)
            {
              shader = drawinfo.cWings
            });
          }
        }
      }
      else if (drawinfo.drawPlayer.wings == 39)
      {
        if (!drawinfo.drawPlayer.ShouldDrawWingsThatAreAlwaysAnimated())
          return;
        drawinfo.stealth *= drawinfo.stealth;
        drawinfo.stealth *= 1f - drawinfo.shadow;
        Color colorArmorBody = drawinfo.colorArmorBody;
        Vector2 vector2_11 = new Vector2(-6f, -7f);
        Texture2D texture2D = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
        Vector2 vec = commonWingPosPreFloor + vector2_11 * directions;
        Rectangle r = texture2D.Frame(verticalFrames: 6, frameY: drawinfo.drawPlayer.wingFrame);
        r.Width -= 2;
        r.Height -= 2;
        drawinfo.DrawDataCache.Add(new DrawData(texture2D, vec.Floor(), new Rectangle?(r), colorArmorBody, drawinfo.drawPlayer.bodyRotation, r.Size() / 2f, 1f, drawinfo.playerEffect)
        {
          shader = drawinfo.cWings
        });
      }
      else
      {
        int num7 = 0;
        int num8 = 0;
        int num9 = 4;
        if (drawinfo.drawPlayer.wings == 43)
        {
          num8 = -5;
          num7 = -7;
          num9 = 7;
        }
        else if (drawinfo.drawPlayer.wings == 44)
          num9 = 7;
        else if (drawinfo.drawPlayer.wings == 5)
        {
          num8 = 4;
          num7 -= 4;
        }
        else if (drawinfo.drawPlayer.wings == 27)
          num8 = 4;
        Color color4 = drawinfo.colorArmorBody;
        if (drawinfo.drawPlayer.wings == 9 || drawinfo.drawPlayer.wings == 29)
        {
          drawinfo.stealth *= drawinfo.stealth;
          drawinfo.stealth *= 1f - drawinfo.shadow;
          color4 = new Color((int) (250.0 * (double) drawinfo.stealth), (int) (250.0 * (double) drawinfo.stealth), (int) (250.0 * (double) drawinfo.stealth), (int) (100.0 * (double) drawinfo.stealth));
        }
        if (drawinfo.drawPlayer.wings == 10)
        {
          drawinfo.stealth *= drawinfo.stealth;
          drawinfo.stealth *= 1f - drawinfo.shadow;
          color4 = new Color((int) (250.0 * (double) drawinfo.stealth), (int) (250.0 * (double) drawinfo.stealth), (int) (250.0 * (double) drawinfo.stealth), (int) (175.0 * (double) drawinfo.stealth));
        }
        if (drawinfo.drawPlayer.wings == 11 && (int) color4.A > (int) Main.gFade)
          color4.A = Main.gFade;
        if (drawinfo.drawPlayer.wings == 31)
          color4.A = (byte) (220.0 * (double) drawinfo.stealth);
        if (drawinfo.drawPlayer.wings == 32)
          color4.A = (byte) ((double) sbyte.MaxValue * (double) drawinfo.stealth);
        if (drawinfo.drawPlayer.wings == 6)
        {
          color4.A = (byte) (160.0 * (double) drawinfo.stealth);
          color4 *= 0.9f;
        }
        Vector2 vec = commonWingPosPreFloor + new Vector2((float) (num8 - 9), (float) (num7 + 2)) * directions;
        DrawData drawData = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vec.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num9 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num9)), color4, drawinfo.drawPlayer.bodyRotation, new Vector2((float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num9 / 2)), 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cWings;
        drawinfo.DrawDataCache.Add(drawData);
        if (drawinfo.drawPlayer.wings == 43 && (double) drawinfo.shadow == 0.0)
        {
          Vector2 vector2_12 = vec;
          Vector2 origin = new Vector2((float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num9 / 2));
          Rectangle rectangle = new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num9 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num9);
          for (int index = 0; index < 2; ++index)
          {
            Vector2 vector2_13 = new Vector2((float) Main.rand.Next(-10, 10) * 0.125f, (float) Main.rand.Next(-10, 10) * 0.125f);
            drawData = new DrawData(TextureAssets.GlowMask[272].Value, vector2_12 + vector2_13, new Rectangle?(rectangle), new Color(230, 230, 230, 60), drawinfo.drawPlayer.bodyRotation, origin, 1f, drawinfo.playerEffect);
            drawData.shader = drawinfo.cWings;
            drawinfo.DrawDataCache.Add(drawData);
          }
        }
        if (drawinfo.drawPlayer.wings == 23)
        {
          drawinfo.stealth *= drawinfo.stealth;
          drawinfo.stealth *= 1f - drawinfo.shadow;
          color4 = new Color((int) (200.0 * (double) drawinfo.stealth), (int) (200.0 * (double) drawinfo.stealth), (int) (200.0 * (double) drawinfo.stealth), (int) (200.0 * (double) drawinfo.stealth));
          drawData = new DrawData(TextureAssets.Flames[8].Value, vec.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), color4, drawinfo.drawPlayer.bodyRotation, new Vector2((float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect);
          drawData.shader = drawinfo.cWings;
          drawinfo.DrawDataCache.Add(drawData);
        }
        else if (drawinfo.drawPlayer.wings == 27)
        {
          drawData = new DrawData(TextureAssets.GlowMask[92].Value, vec.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) sbyte.MaxValue) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect);
          drawData.shader = drawinfo.cWings;
          drawinfo.DrawDataCache.Add(drawData);
        }
        else if (drawinfo.drawPlayer.wings == 44)
        {
          PlayerRainbowWingsTextureContent playerRainbowWings = TextureAssets.RenderTargets.PlayerRainbowWings;
          playerRainbowWings.Request();
          if (!playerRainbowWings.IsReady)
            return;
          drawData = new DrawData((Texture2D) playerRainbowWings.GetTarget(), vec.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7)), new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 14)), 1f, drawinfo.playerEffect);
          drawData.shader = drawinfo.cWings;
          drawinfo.DrawDataCache.Add(drawData);
        }
        else if (drawinfo.drawPlayer.wings == 30)
        {
          drawData = new DrawData(TextureAssets.GlowMask[181].Value, vec.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) sbyte.MaxValue) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect);
          drawData.shader = drawinfo.cWings;
          drawinfo.DrawDataCache.Add(drawData);
        }
        else if (drawinfo.drawPlayer.wings == 38)
        {
          Color color5 = drawinfo.ArkhalisColor * drawinfo.stealth * (1f - drawinfo.shadow);
          drawData = new DrawData(TextureAssets.GlowMask[251].Value, vec.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), color5, drawinfo.drawPlayer.bodyRotation, new Vector2((float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect);
          drawData.shader = drawinfo.cWings;
          drawinfo.DrawDataCache.Add(drawData);
          for (int index = drawinfo.drawPlayer.shadowPos.Length - 2; index >= 0; --index)
          {
            Color color6 = color5 with { A = (byte) 0 } * MathHelper.Lerp(1f, 0.0f, (float) index / 3f) * 0.1f;
            Vector2 vector2_14 = drawinfo.drawPlayer.shadowPos[index] - drawinfo.drawPlayer.position;
            for (float num10 = 0.0f; (double) num10 < 1.0; num10 += 0.01f)
            {
              Vector2 vector2_15 = new Vector2(2f, 0.0f).RotatedBy((double) num10 / 0.039999999105930328 * 6.2831854820251465);
              drawData = new DrawData(TextureAssets.GlowMask[251].Value, vector2_15 + vector2_14 * num10 + vec, new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), color6 * (1f - num10), drawinfo.drawPlayer.bodyRotation, new Vector2((float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect);
              drawData.shader = drawinfo.cWings;
              drawinfo.DrawDataCache.Add(drawData);
            }
          }
        }
        else if (drawinfo.drawPlayer.wings == 29)
        {
          drawData = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vec.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0) * drawinfo.stealth * (1f - drawinfo.shadow) * 0.5f, drawinfo.drawPlayer.bodyRotation, new Vector2((float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1.06f, drawinfo.playerEffect);
          drawData.shader = drawinfo.cWings;
          drawinfo.DrawDataCache.Add(drawData);
        }
        else if (drawinfo.drawPlayer.wings == 36)
        {
          drawData = new DrawData(TextureAssets.GlowMask[213].Value, vec.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1.06f, drawinfo.playerEffect);
          drawData.shader = drawinfo.cWings;
          drawinfo.DrawDataCache.Add(drawData);
          Vector2 spinningpoint = new Vector2(0.0f, (float) (2.0 - (double) drawinfo.shadow * 2.0));
          for (int index = 0; index < 4; ++index)
          {
            drawData = new DrawData(TextureAssets.GlowMask[213].Value, spinningpoint.RotatedBy(1.5707963705062866 * (double) index) + vec.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color((int) sbyte.MaxValue, (int) sbyte.MaxValue, (int) sbyte.MaxValue, (int) sbyte.MaxValue) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect);
            drawData.shader = drawinfo.cWings;
            drawinfo.DrawDataCache.Add(drawData);
          }
        }
        else if (drawinfo.drawPlayer.wings == 31)
        {
          Color color7 = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
          Color color8 = Color.Lerp(Color.HotPink, Color.Crimson, (float) (Math.Cos(6.2831854820251465 * ((double) drawinfo.drawPlayer.miscCounter / 100.0)) * 0.40000000596046448 + 0.5)) with
          {
            A = 0
          };
          for (int index = 0; index < 4; ++index)
          {
            Vector2 vector2_16 = new Vector2((float) (Math.Cos(6.2831854820251465 * ((double) drawinfo.drawPlayer.miscCounter / 60.0)) * 0.5 + 0.5), 0.0f).RotatedBy((double) index * 1.5707963705062866) * 1f;
            drawData = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vec.Floor() + vector2_16, new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), color8 * drawinfo.stealth * (1f - drawinfo.shadow) * 0.5f, drawinfo.drawPlayer.bodyRotation, new Vector2((float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect);
            drawData.shader = drawinfo.cWings;
            drawinfo.DrawDataCache.Add(drawData);
          }
          drawData = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vec.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), color8 * drawinfo.stealth * (1f - drawinfo.shadow) * 1f, drawinfo.drawPlayer.bodyRotation, new Vector2((float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect);
          drawData.shader = drawinfo.cWings;
          drawinfo.DrawDataCache.Add(drawData);
        }
        else
        {
          if (drawinfo.drawPlayer.wings != 32)
            return;
          drawData = new DrawData(TextureAssets.GlowMask[183].Value, vec.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float) (TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1.06f, drawinfo.playerEffect);
          drawData.shader = drawinfo.cWings;
          drawinfo.DrawDataCache.Add(drawData);
        }
      }
    }

    public static void DrawPlayer_12_1_BalloonFronts(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.balloonFront <= (sbyte) 0 || (int) drawinfo.drawPlayer.balloonFront >= ArmorIDs.Balloon.Count)
        return;
      if (ArmorIDs.Balloon.Sets.UsesTorsoFraming[(int) drawinfo.drawPlayer.balloonFront])
      {
        drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.AccBalloon[(int) drawinfo.drawPlayer.balloonFront].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + drawinfo.bodyVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect)
        {
          shader = drawinfo.cBalloonFront
        });
      }
      else
      {
        int num = !Main.hasFocus || Main.ingameOptionsWindow && Main.autoPause ? 0 : DateTime.Now.Millisecond % 800 / 200;
        Vector2 vector2_1 = Main.OffsetsPlayerOffhand[drawinfo.drawPlayer.bodyFrame.Y / 56];
        if (drawinfo.drawPlayer.direction != 1)
          vector2_1.X = (float) drawinfo.drawPlayer.width - vector2_1.X;
        if ((double) drawinfo.drawPlayer.gravDir != 1.0)
          vector2_1.Y -= (float) drawinfo.drawPlayer.height;
        Vector2 vector2_2 = new Vector2(0.0f, 8f) + new Vector2(0.0f, 6f);
        Vector2 vector2_3 = new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X + (double) vector2_1.X), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) vector2_1.Y * (double) drawinfo.drawPlayer.gravDir));
        Vector2 position = (drawinfo.Position - Main.screenPosition + vector2_1 * new Vector2(1f, drawinfo.drawPlayer.gravDir) + new Vector2(0.0f, (float) (drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height)) + vector2_2).Floor();
        drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.AccBalloon[(int) drawinfo.drawPlayer.balloonFront].Value, position, new Rectangle?(new Rectangle(0, TextureAssets.AccBalloon[(int) drawinfo.drawPlayer.balloonFront].Height() / 4 * num, TextureAssets.AccBalloon[(int) drawinfo.drawPlayer.balloonFront].Width(), TextureAssets.AccBalloon[(int) drawinfo.drawPlayer.balloonFront].Height() / 4)), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float) (26 + drawinfo.drawPlayer.direction * 4), (float) (28.0 + (double) drawinfo.drawPlayer.gravDir * 6.0)), 1f, drawinfo.playerEffect)
        {
          shader = drawinfo.cBalloonFront
        });
      }
    }

    public static void DrawPlayer_11_Balloons(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.balloon <= (sbyte) 0 || (int) drawinfo.drawPlayer.balloon >= ArmorIDs.Balloon.Count)
        return;
      if (ArmorIDs.Balloon.Sets.UsesTorsoFraming[(int) drawinfo.drawPlayer.balloon])
      {
        drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.AccBalloon[(int) drawinfo.drawPlayer.balloon].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + drawinfo.bodyVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect)
        {
          shader = drawinfo.cBalloon
        });
      }
      else
      {
        int num = !Main.hasFocus || Main.ingameOptionsWindow && Main.autoPause ? 0 : DateTime.Now.Millisecond % 800 / 200;
        Vector2 vector2_1 = Main.OffsetsPlayerOffhand[drawinfo.drawPlayer.bodyFrame.Y / 56];
        if (drawinfo.drawPlayer.direction != 1)
          vector2_1.X = (float) drawinfo.drawPlayer.width - vector2_1.X;
        if ((double) drawinfo.drawPlayer.gravDir != 1.0)
          vector2_1.Y -= (float) drawinfo.drawPlayer.height;
        Vector2 vector2_2 = new Vector2(0.0f, 8f) + new Vector2(0.0f, 6f);
        Vector2 vector2_3 = new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X + (double) vector2_1.X), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) vector2_1.Y * (double) drawinfo.drawPlayer.gravDir));
        Vector2 position = (drawinfo.Position - Main.screenPosition + vector2_1 * new Vector2(1f, drawinfo.drawPlayer.gravDir) + new Vector2(0.0f, (float) (drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height)) + vector2_2).Floor();
        drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.AccBalloon[(int) drawinfo.drawPlayer.balloon].Value, position, new Rectangle?(new Rectangle(0, TextureAssets.AccBalloon[(int) drawinfo.drawPlayer.balloon].Height() / 4 * num, TextureAssets.AccBalloon[(int) drawinfo.drawPlayer.balloon].Width(), TextureAssets.AccBalloon[(int) drawinfo.drawPlayer.balloon].Height() / 4)), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float) (26 + drawinfo.drawPlayer.direction * 4), (float) (28.0 + (double) drawinfo.drawPlayer.gravDir * 6.0)), 1f, drawinfo.playerEffect)
        {
          shader = drawinfo.cBalloon
        });
      }
    }

    public static void DrawPlayer_12_Skin(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.usesCompositeTorso)
      {
        PlayerDrawLayers.DrawPlayer_12_Skin_Composite(ref drawinfo);
      }
      else
      {
        if (drawinfo.isSitting)
          drawinfo.hidesBottomSkin = true;
        DrawData drawData;
        if (!drawinfo.hidesTopSkin)
        {
          drawinfo.Position.Y += drawinfo.torsoOffset;
          drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 3].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect)
          {
            shader = drawinfo.skinDyePacked
          };
          drawinfo.DrawDataCache.Add(drawData);
          drawinfo.Position.Y -= drawinfo.torsoOffset;
        }
        if (drawinfo.hidesBottomSkin || PlayerDrawLayers.IsBottomOverridden(ref drawinfo))
          return;
        if (drawinfo.isSitting)
        {
          PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.Players[drawinfo.skinVar, 10].Value, drawinfo.colorLegs);
        }
        else
        {
          drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 10].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorLegs, drawinfo.drawPlayer.legRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
          drawinfo.DrawDataCache.Add(drawData);
        }
      }
    }

    public static bool IsBottomOverridden(ref PlayerDrawSet drawinfo) => PlayerDrawLayers.ShouldOverrideLegs_CheckPants(ref drawinfo) || PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo);

    public static bool ShouldOverrideLegs_CheckPants(ref PlayerDrawSet drawinfo)
    {
      switch (drawinfo.drawPlayer.legs)
      {
        case 67:
        case 106:
        case 138:
        case 140:
        case 143:
        case 217:
        case 222:
        case 226:
        case 228:
          return true;
        default:
          return false;
      }
    }

    public static bool ShouldOverrideLegs_CheckShoes(ref PlayerDrawSet drawinfo) => drawinfo.drawPlayer.shoe == (sbyte) 15;

    public static void DrawPlayer_12_Skin_Composite(ref PlayerDrawSet drawinfo)
    {
      DrawData drawData1;
      if (!drawinfo.hidesTopSkin && !drawinfo.drawPlayer.invis)
      {
        Vector2 vector2_1 = new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2));
        vector2_1.Y += drawinfo.torsoOffset;
        Vector2 vector2_2 = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
        vector2_2.Y -= 2f;
        Vector2 position = vector2_1 + vector2_2 * (float) -drawinfo.playerEffect.HasFlag((Enum) SpriteEffects.FlipVertically).ToDirectionInt();
        float bodyRotation = drawinfo.drawPlayer.bodyRotation;
        Vector2 vector2_3 = position;
        Vector2 vector2_4 = position;
        Vector2 bodyVect1 = drawinfo.bodyVect;
        Vector2 bodyVect2 = drawinfo.bodyVect;
        Vector2 compositeOffsetBackArm = PlayerDrawLayers.GetCompositeOffset_BackArm(ref drawinfo);
        Vector2 vector2_5 = vector2_3 + compositeOffsetBackArm;
        Vector2 vector2_6 = compositeOffsetBackArm;
        Vector2 vector2_7 = bodyVect1 + vector2_6;
        Vector2 compositeOffsetFrontArm = PlayerDrawLayers.GetCompositeOffset_FrontArm(ref drawinfo);
        Vector2 vector2_8 = bodyVect2 + compositeOffsetFrontArm;
        Vector2 vector2_9 = compositeOffsetFrontArm;
        Vector2 vector2_10 = vector2_4 + vector2_9;
        if (drawinfo.drawFloatingTube)
        {
          List<DrawData> drawDataCache = drawinfo.DrawDataCache;
          drawData1 = new DrawData(TextureAssets.Extra[105].Value, position, new Rectangle?(new Rectangle(0, 0, 40, 56)), drawinfo.floatingTubeColor, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
          drawData1.shader = drawinfo.cFloatingTube;
          DrawData drawData2 = drawData1;
          drawDataCache.Add(drawData2);
        }
        List<DrawData> drawDataCache1 = drawinfo.DrawDataCache;
        drawData1 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 3].Value, position, new Rectangle?(drawinfo.compTorsoFrame), drawinfo.colorBodySkin, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
        drawData1.shader = drawinfo.skinDyePacked;
        DrawData drawData3 = drawData1;
        drawDataCache1.Add(drawData3);
      }
      if (!drawinfo.hidesBottomSkin && !drawinfo.drawPlayer.invis && !PlayerDrawLayers.IsBottomOverridden(ref drawinfo))
      {
        if (drawinfo.isSitting)
        {
          PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.Players[drawinfo.skinVar, 10].Value, drawinfo.colorLegs, drawinfo.skinDyePacked);
        }
        else
        {
          drawData1 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 10].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorLegs, drawinfo.drawPlayer.legRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
          drawData1.shader = drawinfo.skinDyePacked;
          DrawData drawData4 = drawData1;
          drawinfo.DrawDataCache.Add(drawData4);
        }
      }
      PlayerDrawLayers.DrawPlayer_12_SkinComposite_BackArmShirt(ref drawinfo);
    }

    public static void DrawPlayer_12_SkinComposite_BackArmShirt(ref PlayerDrawSet drawinfo)
    {
      Vector2 vector2_1 = new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2));
      Vector2 vector2_2 = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
      vector2_2.Y -= 2f;
      Vector2 vector2_3 = vector2_1 + vector2_2 * (float) -drawinfo.playerEffect.HasFlag((Enum) SpriteEffects.FlipVertically).ToDirectionInt();
      vector2_3.Y += drawinfo.torsoOffset;
      float bodyRotation = drawinfo.drawPlayer.bodyRotation;
      Vector2 vector2_4 = vector2_3;
      Vector2 vector2_5 = vector2_3;
      Vector2 bodyVect = drawinfo.bodyVect;
      Vector2 compositeOffsetBackArm = PlayerDrawLayers.GetCompositeOffset_BackArm(ref drawinfo);
      Vector2 position1 = vector2_4 + compositeOffsetBackArm;
      Vector2 position2 = vector2_5 + drawinfo.backShoulderOffset;
      Vector2 origin1 = bodyVect + compositeOffsetBackArm;
      float rotation = bodyRotation + drawinfo.compositeBackArmRotation;
      bool flag1 = !drawinfo.drawPlayer.invis;
      bool flag2 = !drawinfo.drawPlayer.invis;
      bool flag3 = drawinfo.drawPlayer.body > 0 && drawinfo.drawPlayer.body < ArmorIDs.Body.Count;
      bool flag4 = !drawinfo.hidesTopSkin;
      bool flag5 = false;
      DrawData drawData1;
      if (flag3)
      {
        flag1 &= drawinfo.missingHand;
        if (flag2 && drawinfo.missingArm)
        {
          if (flag4)
          {
            List<DrawData> drawDataCache = drawinfo.DrawDataCache;
            drawData1 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, position1, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorBodySkin, rotation, origin1, 1f, drawinfo.playerEffect);
            drawData1.shader = drawinfo.skinDyePacked;
            DrawData drawData2 = drawData1;
            drawDataCache.Add(drawData2);
          }
          if (!flag5 & flag4)
          {
            List<DrawData> drawDataCache = drawinfo.DrawDataCache;
            drawData1 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 5].Value, position1, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorBodySkin, rotation, origin1, 1f, drawinfo.playerEffect);
            drawData1.shader = drawinfo.skinDyePacked;
            DrawData drawData3 = drawData1;
            drawDataCache.Add(drawData3);
            flag5 = true;
          }
          flag2 = false;
        }
        if (!drawinfo.drawPlayer.invis || PlayerDrawLayers.IsArmorDrawnWhenInvisible(drawinfo.drawPlayer.body))
        {
          Texture2D texture = TextureAssets.ArmorBodyComposite[drawinfo.drawPlayer.body].Value;
          if (!drawinfo.hideCompositeShoulders)
          {
            ref PlayerDrawSet local = ref drawinfo;
            drawData1 = new DrawData(texture, position2, new Rectangle?(drawinfo.compBackShoulderFrame), drawinfo.colorArmorBody, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
            drawData1.shader = drawinfo.cBody;
            DrawData data = drawData1;
            PlayerDrawLayers.DrawCompositeArmorPiece(ref local, CompositePlayerDrawContext.BackShoulder, data);
          }
          PlayerDrawLayers.DrawPlayer_12_1_BalloonFronts(ref drawinfo);
          ref PlayerDrawSet local1 = ref drawinfo;
          drawData1 = new DrawData(texture, position1, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorArmorBody, rotation, origin1, 1f, drawinfo.playerEffect);
          drawData1.shader = drawinfo.cBody;
          DrawData data1 = drawData1;
          PlayerDrawLayers.DrawCompositeArmorPiece(ref local1, CompositePlayerDrawContext.BackArm, data1);
        }
      }
      if (flag1)
      {
        if (flag4)
        {
          if (flag2)
          {
            List<DrawData> drawDataCache = drawinfo.DrawDataCache;
            drawData1 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, position1, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorBodySkin, rotation, origin1, 1f, drawinfo.playerEffect);
            drawData1.shader = drawinfo.skinDyePacked;
            DrawData drawData4 = drawData1;
            drawDataCache.Add(drawData4);
          }
          if (!flag5 & flag4)
          {
            List<DrawData> drawDataCache = drawinfo.DrawDataCache;
            drawData1 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 5].Value, position1, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorBodySkin, rotation, origin1, 1f, drawinfo.playerEffect);
            drawData1.shader = drawinfo.skinDyePacked;
            DrawData drawData5 = drawData1;
            drawDataCache.Add(drawData5);
          }
        }
        if (!flag3)
        {
          drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 8].Value, position1, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorUnderShirt, rotation, origin1, 1f, drawinfo.playerEffect));
          PlayerDrawLayers.DrawPlayer_12_1_BalloonFronts(ref drawinfo);
          drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 13].Value, position1, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorShirt, rotation, origin1, 1f, drawinfo.playerEffect));
        }
      }
      if (drawinfo.drawPlayer.handoff > (sbyte) 0 && (int) drawinfo.drawPlayer.handoff < ArmorIDs.HandOff.Count)
      {
        Texture2D texture = TextureAssets.AccHandsOffComposite[(int) drawinfo.drawPlayer.handoff].Value;
        ref PlayerDrawSet local = ref drawinfo;
        drawData1 = new DrawData(texture, position1, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorArmorBody, rotation, origin1, 1f, drawinfo.playerEffect);
        drawData1.shader = drawinfo.cHandOff;
        DrawData data = drawData1;
        PlayerDrawLayers.DrawCompositeArmorPiece(ref local, CompositePlayerDrawContext.BackArmAccessory, data);
      }
      if (!drawinfo.drawPlayer.drawingFootball)
        return;
      Main.instance.LoadProjectile(861);
      Texture2D texture2D = TextureAssets.Projectile[861].Value;
      Rectangle r = texture2D.Frame(verticalFrames: 4);
      Vector2 origin2 = r.Size() / 2f;
      Vector2 position3 = position1 + new Vector2((float) (drawinfo.drawPlayer.direction * -2), drawinfo.drawPlayer.gravDir * 4f);
      drawinfo.DrawDataCache.Add(new DrawData(texture2D, position3, new Rectangle?(r), drawinfo.colorArmorBody, bodyRotation + 0.7853982f * (float) drawinfo.drawPlayer.direction, origin2, 0.8f, drawinfo.playerEffect));
    }

    public static void DrawPlayer_13_Leggings(ref PlayerDrawSet drawinfo)
    {
      Vector2 legsOffset = drawinfo.legsOffset;
      if (drawinfo.drawPlayer.legs == 169)
        return;
      if (drawinfo.isSitting && drawinfo.drawPlayer.legs != 140 && drawinfo.drawPlayer.legs != 217)
      {
        if (drawinfo.drawPlayer.legs > 0 && drawinfo.drawPlayer.legs < ArmorIDs.Legs.Count && (!PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo) || drawinfo.drawPlayer.wearsRobe))
        {
          if (drawinfo.drawPlayer.invis)
            return;
          PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.ArmorLeg[drawinfo.drawPlayer.legs].Value, drawinfo.colorArmorLegs, drawinfo.cLegs);
          if (drawinfo.legsGlowMask == -1)
            return;
          PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.GlowMask[drawinfo.legsGlowMask].Value, drawinfo.legsGlowColor, drawinfo.cLegs);
        }
        else
        {
          if (drawinfo.drawPlayer.invis || PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo))
            return;
          PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.Players[drawinfo.skinVar, 11].Value, drawinfo.colorPants);
          PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.Players[drawinfo.skinVar, 12].Value, drawinfo.colorShoes);
        }
      }
      else if (drawinfo.drawPlayer.legs == 140)
      {
        if (drawinfo.drawPlayer.invis || drawinfo.drawPlayer.mount.Active)
          return;
        Texture2D texture = TextureAssets.Extra[73].Value;
        bool flag = drawinfo.drawPlayer.legFrame.Y != drawinfo.drawPlayer.legFrame.Height || Main.gameMenu;
        int num1 = drawinfo.drawPlayer.miscCounter / 3 % 8;
        if (flag)
          num1 = drawinfo.drawPlayer.miscCounter / 4 % 8;
        Rectangle r = new Rectangle(18 * flag.ToInt(), num1 * 26, 16, 24);
        float num2 = 12f;
        if (drawinfo.drawPlayer.bodyFrame.Height != 0)
          num2 = 12f - Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height].Y;
        if ((double) drawinfo.drawPlayer.Directions.Y == -1.0)
          num2 -= 6f;
        Vector2 scale = new Vector2(1f, 1f);
        Vector2 vector2_1 = drawinfo.Position + drawinfo.drawPlayer.Size * new Vector2(0.5f, (float) (0.5 + 0.5 * (double) drawinfo.drawPlayer.gravDir));
        int direction = drawinfo.drawPlayer.direction;
        Vector2 vector2_2 = new Vector2(0.0f, -num2 * drawinfo.drawPlayer.gravDir);
        Vector2 vector2_3 = vector2_1 + vector2_2 - Main.screenPosition + drawinfo.drawPlayer.legPosition;
        if (drawinfo.isSitting)
          vector2_3.Y += drawinfo.seatYOffset;
        Vector2 position = (vector2_3 + legsOffset).Floor();
        drawinfo.DrawDataCache.Add(new DrawData(texture, position, new Rectangle?(r), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, r.Size() * new Vector2(0.5f, (float) (0.5 - (double) drawinfo.drawPlayer.gravDir * 0.5)), scale, drawinfo.playerEffect)
        {
          shader = drawinfo.cLegs
        });
      }
      else if (drawinfo.drawPlayer.legs > 0 && drawinfo.drawPlayer.legs < ArmorIDs.Legs.Count && (!PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo) || drawinfo.drawPlayer.wearsRobe))
      {
        if (drawinfo.drawPlayer.invis)
          return;
        DrawData drawData = new DrawData(TextureAssets.ArmorLeg[drawinfo.drawPlayer.legs].Value, legsOffset + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.legFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.legFrame.Height + 4.0)) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cLegs;
        drawinfo.DrawDataCache.Add(drawData);
        if (drawinfo.legsGlowMask == -1)
          return;
        if (drawinfo.legsGlowMask == 274)
        {
          for (int index = 0; index < 2; ++index)
          {
            Vector2 vector2 = new Vector2((float) Main.rand.Next(-10, 10) * 0.125f, (float) Main.rand.Next(-10, 10) * 0.125f);
            drawData = new DrawData(TextureAssets.GlowMask[drawinfo.legsGlowMask].Value, legsOffset + vector2 + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.legFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.legFrame.Height + 4.0)) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.legsGlowColor, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect);
            drawData.shader = drawinfo.cLegs;
            drawinfo.DrawDataCache.Add(drawData);
          }
        }
        else
        {
          drawData = new DrawData(TextureAssets.GlowMask[drawinfo.legsGlowMask].Value, legsOffset + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.legFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.legFrame.Height + 4.0)) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.legsGlowColor, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect);
          drawData.shader = drawinfo.cLegs;
          drawinfo.DrawDataCache.Add(drawData);
        }
      }
      else
      {
        if (drawinfo.drawPlayer.invis || PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo))
          return;
        DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 11].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.legFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.legFrame.Height + 4.0)) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorPants, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect);
        drawinfo.DrawDataCache.Add(drawData);
        drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 12].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.legFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.legFrame.Height + 4.0)) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorShoes, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect);
        drawinfo.DrawDataCache.Add(drawData);
      }
    }

    private static void DrawSittingLongCoats(
      ref PlayerDrawSet drawinfo,
      int specialLegCoat,
      Texture2D textureToDraw,
      Color matchingColor,
      int shaderIndex = 0,
      bool glowmask = false)
    {
      Vector2 legsOffset = drawinfo.legsOffset;
      Vector2 vector2 = new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.legFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.legFrame.Height + 4.0)) + drawinfo.drawPlayer.legPosition + drawinfo.legVect;
      Rectangle legFrame = drawinfo.drawPlayer.legFrame;
      Vector2 position = vector2 + legsOffset;
      position.X += (float) (2 * drawinfo.drawPlayer.direction);
      legFrame.Y = legFrame.Height * 5;
      if (specialLegCoat == 160 || specialLegCoat == 173)
        legFrame = drawinfo.drawPlayer.legFrame;
      drawinfo.DrawDataCache.Add(new DrawData(textureToDraw, position, new Rectangle?(legFrame), matchingColor, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect)
      {
        shader = shaderIndex
      });
    }

    private static void DrawSittingLegs(
      ref PlayerDrawSet drawinfo,
      Texture2D textureToDraw,
      Color matchingColor,
      int shaderIndex = 0,
      bool glowmask = false)
    {
      Vector2 legsOffset = drawinfo.legsOffset;
      Vector2 vector2_1 = new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.legFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.legFrame.Height + 4.0)) + drawinfo.drawPlayer.legPosition + drawinfo.legVect;
      Rectangle legFrame = drawinfo.drawPlayer.legFrame;
      vector2_1.Y -= 2f;
      vector2_1.Y += drawinfo.seatYOffset;
      Vector2 vector2_2 = vector2_1 + legsOffset;
      int x = 2;
      int num1 = 42;
      int num2 = 2;
      int num3 = 2;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      bool flag = drawinfo.drawPlayer.legs == 101 || drawinfo.drawPlayer.legs == 102 || drawinfo.drawPlayer.legs == 118 || drawinfo.drawPlayer.legs == 99;
      if (drawinfo.drawPlayer.wearsRobe && !flag)
      {
        x = 0;
        num3 = 0;
        num1 = 6;
        vector2_2.Y += 4f;
        legFrame.Y = legFrame.Height * 5;
      }
      switch (drawinfo.drawPlayer.legs)
      {
        case 106:
        case 143:
        case 226:
          x = 0;
          num3 = 0;
          num1 = 6;
          vector2_2.Y += 4f;
          legFrame.Y = legFrame.Height * 5;
          break;
        case 132:
          x = -2;
          num6 = 2;
          break;
        case 193:
        case 194:
          if (drawinfo.drawPlayer.body == 218)
          {
            x = -2;
            num6 = 2;
            vector2_2.Y += 2f;
            break;
          }
          break;
        case 210:
          if (glowmask)
          {
            Vector2 vector2_3 = new Vector2((float) Main.rand.Next(-10, 10) * 0.125f, (float) Main.rand.Next(-10, 10) * 0.125f);
            vector2_2 += vector2_3;
            break;
          }
          break;
        case 214:
        case 215:
        case 216:
          x = -6;
          num3 = 2;
          num4 = 2;
          num2 = 4;
          num1 = 6;
          legFrame = drawinfo.drawPlayer.legFrame;
          vector2_2.Y += 2f;
          break;
      }
      for (int index = num2; index >= 0; --index)
      {
        Vector2 position = vector2_2 + new Vector2((float) x, 2f) * new Vector2((float) drawinfo.drawPlayer.direction, 1f);
        Rectangle rectangle = legFrame;
        rectangle.Y += index * 2;
        rectangle.Y += num1;
        rectangle.Height -= num1;
        rectangle.Height -= index * 2;
        if (index != num2)
          rectangle.Height = 2;
        position.X += (float) (drawinfo.drawPlayer.direction * num3 * index + num5 * drawinfo.drawPlayer.direction);
        if (index != 0)
          position.X += (float) (num6 * drawinfo.drawPlayer.direction);
        position.Y += (float) num1;
        position.Y += (float) num4;
        drawinfo.DrawDataCache.Add(new DrawData(textureToDraw, position, new Rectangle?(rectangle), matchingColor, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect)
        {
          shader = shaderIndex
        });
      }
    }

    public static void DrawPlayer_14_Shoes(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.shoe <= (sbyte) 0 || (int) drawinfo.drawPlayer.shoe >= ArmorIDs.Shoe.Count || PlayerDrawLayers.ShouldOverrideLegs_CheckPants(ref drawinfo))
        return;
      int shaderIndex = drawinfo.cShoe;
      if (drawinfo.drawPlayer.shoe == (sbyte) 22 || drawinfo.drawPlayer.shoe == (sbyte) 23)
        shaderIndex = drawinfo.cFlameWaker;
      if (drawinfo.isSitting)
      {
        PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.AccShoes[(int) drawinfo.drawPlayer.shoe].Value, drawinfo.colorArmorLegs, shaderIndex);
      }
      else
      {
        drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.AccShoes[(int) drawinfo.drawPlayer.shoe].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.legFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.legFrame.Height + 4.0)) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect)
        {
          shader = shaderIndex
        });
        if (drawinfo.drawPlayer.shoe != (sbyte) 25 && drawinfo.drawPlayer.shoe != (sbyte) 26)
          return;
        PlayerDrawLayers.DrawPlayer_14_2_GlassSlipperSparkles(ref drawinfo);
      }
    }

    public static void DrawPlayer_14_2_GlassSlipperSparkles(ref PlayerDrawSet drawinfo)
    {
      if ((double) drawinfo.shadow != 0.0)
        return;
      if (Main.rand.Next(60) == 0)
      {
        Rectangle r = Utils.CenteredRectangle(drawinfo.Position + drawinfo.drawPlayer.Size / 2f + new Vector2(0.0f, drawinfo.drawPlayer.gravDir * 16f), new Vector2(20f, 8f));
        int index = Dust.NewDust(r.TopLeft(), r.Width, r.Height, 204, Alpha: 150, Scale: 0.3f);
        Main.dust[index].fadeIn = 1f;
        Main.dust[index].velocity *= 0.1f;
        Main.dust[index].noLight = true;
        Main.dust[index].shader = GameShaders.Armor.GetSecondaryShader(drawinfo.drawPlayer.cShoe, drawinfo.drawPlayer);
        drawinfo.DustCache.Add(index);
      }
      if ((double) drawinfo.drawPlayer.velocity.X == 0.0 || Main.rand.Next(10) != 0)
        return;
      Rectangle r1 = Utils.CenteredRectangle(drawinfo.Position + drawinfo.drawPlayer.Size / 2f + new Vector2((float) (drawinfo.drawPlayer.direction * -2), drawinfo.drawPlayer.gravDir * 16f), new Vector2(6f, 8f));
      int index1 = Dust.NewDust(r1.TopLeft(), r1.Width, r1.Height, 204, Alpha: 150, Scale: 0.3f);
      Main.dust[index1].fadeIn = 1f;
      Main.dust[index1].velocity *= 0.1f;
      Main.dust[index1].noLight = true;
      Main.dust[index1].shader = GameShaders.Armor.GetSecondaryShader(drawinfo.drawPlayer.cShoe, drawinfo.drawPlayer);
      drawinfo.DustCache.Add(index1);
    }

    public static void DrawPlayer_15_SkinLongCoat(ref PlayerDrawSet drawinfo)
    {
      if ((drawinfo.skinVar == 3 || drawinfo.skinVar == 8 ? 1 : (drawinfo.skinVar == 7 ? 1 : 0)) == 0 || drawinfo.drawPlayer.body > 0 && drawinfo.drawPlayer.body < ArmorIDs.Body.Count || drawinfo.drawPlayer.invis)
        return;
      if (drawinfo.isSitting)
      {
        PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.Players[drawinfo.skinVar, 14].Value, drawinfo.colorShirt);
      }
      else
      {
        DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 14].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.legFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.legFrame.Height + 4.0)) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorShirt, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect);
        drawinfo.DrawDataCache.Add(drawData);
      }
    }

    public static void DrawPlayer_16_ArmorLongCoat(ref PlayerDrawSet drawinfo)
    {
      int index = -1;
      switch (drawinfo.drawPlayer.body)
      {
        case 52:
          index = !drawinfo.drawPlayer.Male ? 172 : 171;
          break;
        case 53:
          index = !drawinfo.drawPlayer.Male ? 176 : 175;
          break;
        case 73:
          index = 170;
          break;
        case 81:
          index = 169;
          break;
        case 89:
          index = 186;
          break;
        case 168:
          index = 164;
          break;
        case 182:
          index = 163;
          break;
        case 187:
          index = 173;
          break;
        case 198:
          index = 162;
          break;
        case 200:
          index = 149;
          break;
        case 201:
          index = 150;
          break;
        case 202:
          index = 151;
          break;
        case 205:
          index = 174;
          break;
        case 207:
          index = 161;
          break;
        case 209:
          index = 160;
          break;
        case 210:
          index = !drawinfo.drawPlayer.Male ? 177 : 178;
          break;
        case 211:
          index = !drawinfo.drawPlayer.Male ? 181 : 182;
          break;
        case 218:
          index = 195;
          break;
        case 222:
          index = !drawinfo.drawPlayer.Male ? 200 : 201;
          break;
        case 225:
          index = 206;
          break;
        case 236:
          index = 221;
          break;
        case 237:
          index = 223;
          break;
      }
      if (index == -1)
        return;
      Main.instance.LoadArmorLegs(index);
      if (drawinfo.isSitting && index != 195)
        PlayerDrawLayers.DrawSittingLongCoats(ref drawinfo, index, TextureAssets.ArmorLeg[index].Value, drawinfo.colorArmorBody, drawinfo.cBody);
      else
        drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.ArmorLeg[index].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.legFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.legFrame.Height + 4.0)) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect)
        {
          shader = drawinfo.cBody
        });
    }

    public static void DrawPlayer_17_Torso(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.usesCompositeTorso)
        PlayerDrawLayers.DrawPlayer_17_TorsoComposite(ref drawinfo);
      else if (drawinfo.drawPlayer.body > 0 && drawinfo.drawPlayer.body < ArmorIDs.Body.Count)
      {
        Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
        int num = drawinfo.armorAdjust;
        bodyFrame.X += num;
        bodyFrame.Width -= num;
        if (drawinfo.drawPlayer.direction == -1)
          num = 0;
        if (!drawinfo.drawPlayer.invis || drawinfo.drawPlayer.body != 21 && drawinfo.drawPlayer.body != 22)
        {
          DrawData drawData = new DrawData(drawinfo.drawPlayer.Male ? TextureAssets.ArmorBody[drawinfo.drawPlayer.body].Value : TextureAssets.FemaleBody[drawinfo.drawPlayer.body].Value, new Vector2((float) ((int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)) + num), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
          drawData.shader = drawinfo.cBody;
          drawinfo.DrawDataCache.Add(drawData);
          if (drawinfo.bodyGlowMask != -1)
          {
            drawData = new DrawData(TextureAssets.GlowMask[drawinfo.bodyGlowMask].Value, new Vector2((float) ((int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)) + num), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(bodyFrame), drawinfo.bodyGlowColor, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
            drawData.shader = drawinfo.cBody;
            drawinfo.DrawDataCache.Add(drawData);
          }
        }
        if (!drawinfo.missingHand || drawinfo.drawPlayer.invis)
          return;
        DrawData drawData1 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 5].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect)
        {
          shader = drawinfo.skinDyePacked
        };
        drawinfo.DrawDataCache.Add(drawData1);
      }
      else
      {
        if (drawinfo.drawPlayer.invis)
          return;
        if (!drawinfo.drawPlayer.Male)
        {
          DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 4].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorUnderShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
          drawinfo.DrawDataCache.Add(drawData);
          drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
          drawinfo.DrawDataCache.Add(drawData);
        }
        else
        {
          DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 4].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorUnderShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
          drawinfo.DrawDataCache.Add(drawData);
          drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
          drawinfo.DrawDataCache.Add(drawData);
        }
        DrawData drawData2 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 5].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect)
        {
          shader = drawinfo.skinDyePacked
        };
        drawinfo.DrawDataCache.Add(drawData2);
      }
    }

    public static void DrawPlayer_17_TorsoComposite(ref PlayerDrawSet drawinfo)
    {
      Vector2 vector2_1 = new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2));
      Vector2 vector2_2 = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
      vector2_2.Y -= 2f;
      Vector2 position = vector2_1 + vector2_2 * (float) -drawinfo.playerEffect.HasFlag((Enum) SpriteEffects.FlipVertically).ToDirectionInt();
      float bodyRotation = drawinfo.drawPlayer.bodyRotation;
      Vector2 vector2_3 = position;
      Vector2 bodyVect = drawinfo.bodyVect;
      Vector2 compositeOffsetBackArm = PlayerDrawLayers.GetCompositeOffset_BackArm(ref drawinfo);
      Vector2 vector2_4 = compositeOffsetBackArm;
      Vector2 vector2_5 = vector2_3 + vector2_4;
      Vector2 vector2_6 = bodyVect + compositeOffsetBackArm;
      DrawData drawData1;
      if (drawinfo.drawPlayer.body > 0 && drawinfo.drawPlayer.body < ArmorIDs.Body.Count)
      {
        if (!drawinfo.drawPlayer.invis || PlayerDrawLayers.IsArmorDrawnWhenInvisible(drawinfo.drawPlayer.body))
        {
          Texture2D texture = TextureAssets.ArmorBodyComposite[drawinfo.drawPlayer.body].Value;
          ref PlayerDrawSet local = ref drawinfo;
          drawData1 = new DrawData(texture, position, new Rectangle?(drawinfo.compTorsoFrame), drawinfo.colorArmorBody, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
          drawData1.shader = drawinfo.cBody;
          DrawData data = drawData1;
          PlayerDrawLayers.DrawCompositeArmorPiece(ref local, CompositePlayerDrawContext.Torso, data);
        }
      }
      else if (!drawinfo.drawPlayer.invis)
      {
        drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 4].Value, position, new Rectangle?(drawinfo.compBackShoulderFrame), drawinfo.colorUnderShirt, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect));
        drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, position, new Rectangle?(drawinfo.compBackShoulderFrame), drawinfo.colorShirt, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect));
        drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 4].Value, position, new Rectangle?(drawinfo.compTorsoFrame), drawinfo.colorUnderShirt, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect));
        drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, position, new Rectangle?(drawinfo.compTorsoFrame), drawinfo.colorShirt, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect));
      }
      if (!drawinfo.drawFloatingTube)
        return;
      List<DrawData> drawDataCache = drawinfo.DrawDataCache;
      drawData1 = new DrawData(TextureAssets.Extra[105].Value, position, new Rectangle?(new Rectangle(0, 56, 40, 56)), drawinfo.floatingTubeColor, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
      drawData1.shader = drawinfo.cFloatingTube;
      DrawData drawData2 = drawData1;
      drawDataCache.Add(drawData2);
    }

    public static void DrawPlayer_18_OffhandAcc(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.usesCompositeBackHandAcc || drawinfo.drawPlayer.handoff <= (sbyte) 0 || (int) drawinfo.drawPlayer.handoff >= ArmorIDs.HandOff.Count)
        return;
      drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.AccHandsOff[(int) drawinfo.drawPlayer.handoff].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect)
      {
        shader = drawinfo.cHandOff
      });
    }

    public static void DrawPlayer_JimsDroneRadio(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.HeldItem.type != 5451 || drawinfo.drawPlayer.itemAnimation != 0)
        return;
      Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
      drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Extra[261].Value, new Vector2((float) ((int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)) + drawinfo.drawPlayer.direction * 2), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0 + 14.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(bodyFrame), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect)
      {
        shader = drawinfo.cWaist
      });
    }

    public static void DrawPlayer_19_WaistAcc(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.waist <= (sbyte) 0 || (int) drawinfo.drawPlayer.waist >= ArmorIDs.Waist.Count)
        return;
      Rectangle rectangle = drawinfo.drawPlayer.legFrame;
      if (ArmorIDs.Waist.Sets.UsesTorsoFraming[(int) drawinfo.drawPlayer.waist])
        rectangle = drawinfo.drawPlayer.bodyFrame;
      drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.AccWaist[(int) drawinfo.drawPlayer.waist].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.legFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.legFrame.Height + 4.0)) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(rectangle), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect)
      {
        shader = drawinfo.cWaist
      });
    }

    public static void DrawPlayer_20_NeckAcc(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.neck <= (sbyte) 0 || (int) drawinfo.drawPlayer.neck >= ArmorIDs.Neck.Count)
        return;
      drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.AccNeck[(int) drawinfo.drawPlayer.neck].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect)
      {
        shader = drawinfo.cNeck
      });
    }

    public static void DrawPlayer_21_Head(ref PlayerDrawSet drawinfo)
    {
      Vector2 helmetOffset = drawinfo.helmetOffset;
      PlayerDrawLayers.DrawPlayer_21_Head_TheFace(ref drawinfo);
      bool flag1 = drawinfo.drawPlayer.head == 14 || drawinfo.drawPlayer.head == 56 || drawinfo.drawPlayer.head == 114 || drawinfo.drawPlayer.head == 158 || drawinfo.drawPlayer.head == 69 || drawinfo.drawPlayer.head == 180;
      bool flag2 = drawinfo.drawPlayer.head == 28;
      bool flag3 = drawinfo.drawPlayer.head == 39 || drawinfo.drawPlayer.head == 38;
      Vector2 vector2_1 = new Vector2((float) (-drawinfo.drawPlayer.bodyFrame.Width / 2 + drawinfo.drawPlayer.width / 2), (float) (drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height + 4));
      Vector2 position = (drawinfo.Position - Main.screenPosition + vector2_1).Floor() + drawinfo.drawPlayer.headPosition + drawinfo.headVect;
      if (drawinfo.playerEffect.HasFlag((Enum) SpriteEffects.FlipVertically))
      {
        int num = drawinfo.drawPlayer.bodyFrame.Height - drawinfo.hairFrontFrame.Height;
        position.Y += (float) num;
      }
      position += drawinfo.hairOffset;
      DrawData drawData;
      if (drawinfo.fullHair)
      {
        Color color = drawinfo.colorArmorHead;
        int num = drawinfo.cHead;
        if (ArmorIDs.Head.Sets.UseSkinColor[drawinfo.drawPlayer.head])
        {
          color = !drawinfo.drawPlayer.isDisplayDollOrInanimate ? drawinfo.colorHead : drawinfo.colorDisplayDollSkin;
          num = drawinfo.skinDyePacked;
        }
        drawData = new DrawData(TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, helmetOffset + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
        drawData.shader = num;
        drawinfo.DrawDataCache.Add(drawData);
        if (!drawinfo.drawPlayer.invis)
        {
          drawData = new DrawData(TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, position, new Rectangle?(drawinfo.hairFrontFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
          drawData.shader = drawinfo.hairDyePacked;
          drawinfo.DrawDataCache.Add(drawData);
        }
      }
      if (drawinfo.hatHair && !drawinfo.drawPlayer.invis)
      {
        drawData = new DrawData(TextureAssets.PlayerHairAlt[drawinfo.drawPlayer.hair].Value, position, new Rectangle?(drawinfo.hairFrontFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.hairDyePacked;
        drawinfo.DrawDataCache.Add(drawData);
      }
      if (drawinfo.drawPlayer.head == 270)
      {
        Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
        bodyFrame.Width += 2;
        drawData = new DrawData(TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, helmetOffset + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cHead;
        drawinfo.DrawDataCache.Add(drawData);
        drawData = new DrawData(TextureAssets.GlowMask[drawinfo.headGlowMask].Value, helmetOffset + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cHead;
        drawinfo.DrawDataCache.Add(drawData);
      }
      else if (flag1)
      {
        Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
        Vector2 headVect = drawinfo.headVect;
        if ((double) drawinfo.drawPlayer.gravDir == 1.0)
        {
          if (bodyFrame.Y != 0)
          {
            bodyFrame.Y -= 2;
            headVect.Y += 2f;
          }
          bodyFrame.Height -= 8;
        }
        else if (bodyFrame.Y != 0)
        {
          bodyFrame.Y -= 2;
          headVect.Y -= 10f;
          bodyFrame.Height -= 8;
        }
        Color color = drawinfo.colorArmorHead;
        int num = drawinfo.cHead;
        if (ArmorIDs.Head.Sets.UseSkinColor[drawinfo.drawPlayer.head])
        {
          color = !drawinfo.drawPlayer.isDisplayDollOrInanimate ? drawinfo.colorHead : drawinfo.colorDisplayDollSkin;
          num = drawinfo.skinDyePacked;
        }
        drawData = new DrawData(TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, helmetOffset + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame), color, drawinfo.drawPlayer.headRotation, headVect, 1f, drawinfo.playerEffect);
        drawData.shader = num;
        drawinfo.DrawDataCache.Add(drawData);
      }
      else if (drawinfo.drawPlayer.head == 259)
      {
        int verticalFrames = 27;
        Texture2D texture2D = TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value;
        Rectangle r = texture2D.Frame(verticalFrames: verticalFrames, frameY: drawinfo.drawPlayer.rabbitOrderFrame.DisplayFrame);
        Vector2 origin = r.Size() / 2f;
        int num1 = drawinfo.drawPlayer.babyBird.ToInt();
        Vector2 specialHatDrawPosition = PlayerDrawLayers.DrawPlayer_21_Head_GetSpecialHatDrawPosition(ref drawinfo, ref helmetOffset, new Vector2((float) (1 + num1 * 2), (float) (drawinfo.drawPlayer.babyBird.ToInt() * -6 - 26)));
        int hatStacks = PlayerDrawLayers.GetHatStacks(ref drawinfo, 4955);
        float num2 = (float) Math.PI / 60f;
        float num3 = (float) ((double) num2 * (double) drawinfo.drawPlayer.position.X % 6.2831854820251465);
        for (int index = hatStacks - 1; index >= 0; --index)
        {
          float x = (float) ((double) Vector2.UnitY.RotatedBy((double) num3 + (double) num2 * (double) index).X * ((double) index / 30.0) * 2.0) - (float) (index * 2 * drawinfo.drawPlayer.direction);
          drawData = new DrawData(texture2D, specialHatDrawPosition + new Vector2(x, (float) (index * -14) * drawinfo.drawPlayer.gravDir), new Rectangle?(r), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, origin, 1f, drawinfo.playerEffect);
          drawData.shader = drawinfo.cHead;
          drawinfo.DrawDataCache.Add(drawData);
        }
        if (!drawinfo.drawPlayer.invis)
        {
          drawData = new DrawData(TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, position, new Rectangle?(drawinfo.hairFrontFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
          drawData.shader = drawinfo.hairDyePacked;
          drawinfo.DrawDataCache.Add(drawData);
        }
      }
      else if (drawinfo.drawPlayer.head > 0 && drawinfo.drawPlayer.head < ArmorIDs.Head.Count && !flag2)
      {
        if (!(drawinfo.drawPlayer.invis & flag3))
        {
          if (drawinfo.drawPlayer.head == 13)
          {
            int hatStacks = PlayerDrawLayers.GetHatStacks(ref drawinfo, 205);
            float num4 = (float) Math.PI / 60f;
            float num5 = (float) ((double) num4 * (double) drawinfo.drawPlayer.position.X % 6.2831854820251465);
            for (int index = 0; index < hatStacks; ++index)
            {
              float num6 = (float) ((double) Vector2.UnitY.RotatedBy((double) num5 + (double) num4 * (double) index).X * ((double) index / 30.0) * 2.0);
              drawData = new DrawData(TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, helmetOffset + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)) + num6, (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0 - (double) (4 * index))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
              drawData.shader = drawinfo.cHead;
              drawinfo.DrawDataCache.Add(drawData);
            }
          }
          else if (drawinfo.drawPlayer.head == 265)
          {
            int verticalFrames = 6;
            Texture2D texture2D = TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value;
            Rectangle r = texture2D.Frame(verticalFrames: verticalFrames, frameY: drawinfo.drawPlayer.rabbitOrderFrame.DisplayFrame);
            Vector2 origin = r.Size() / 2f;
            Vector2 specialHatDrawPosition = PlayerDrawLayers.DrawPlayer_21_Head_GetSpecialHatDrawPosition(ref drawinfo, ref helmetOffset, new Vector2(0.0f, -9f));
            int hatStacks = PlayerDrawLayers.GetHatStacks(ref drawinfo, 5004);
            float num7 = (float) Math.PI / 60f;
            float num8 = (float) ((double) num7 * (double) drawinfo.drawPlayer.position.X % 6.2831854820251465);
            int num9 = hatStacks * 4 + 2;
            int num10 = 0;
            bool flag4 = ((double) Main.GlobalTimeWrappedHourly + 180.0) % 600.0 < 60.0;
            for (int index = num9 - 1; index >= 0; --index)
            {
              int num11 = 0;
              if (index == num9 - 1)
              {
                r.Y = 0;
                num11 = 2;
              }
              else
                r.Y = index != 0 ? r.Height * (num10++ % 4 + 1) : r.Height * 5;
              if (!(r.Y == r.Height * 3 & flag4))
              {
                float x = (float) ((double) Vector2.UnitY.RotatedBy((double) num8 + (double) num7 * (double) index).X * ((double) index / 10.0) * 4.0 - (double) index * 0.10000000149011612 * (double) drawinfo.drawPlayer.direction);
                drawData = new DrawData(texture2D, specialHatDrawPosition + new Vector2(x, (float) (index * -4 + num11) * drawinfo.drawPlayer.gravDir), new Rectangle?(r), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, origin, 1f, drawinfo.playerEffect);
                drawData.shader = drawinfo.cHead;
                drawinfo.DrawDataCache.Add(drawData);
              }
            }
          }
          else
          {
            Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
            Vector2 headVect = drawinfo.headVect;
            if ((double) drawinfo.drawPlayer.gravDir == 1.0)
            {
              bodyFrame.Height -= 4;
            }
            else
            {
              headVect.Y -= 4f;
              bodyFrame.Height -= 4;
            }
            Color color1 = drawinfo.colorArmorHead;
            int num12 = drawinfo.cHead;
            if (ArmorIDs.Head.Sets.UseSkinColor[drawinfo.drawPlayer.head])
            {
              color1 = !drawinfo.drawPlayer.isDisplayDollOrInanimate ? drawinfo.colorHead : drawinfo.colorDisplayDollSkin;
              num12 = drawinfo.skinDyePacked;
            }
            drawData = new DrawData(TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, helmetOffset + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame), color1, drawinfo.drawPlayer.headRotation, headVect, 1f, drawinfo.playerEffect);
            drawData.shader = num12;
            drawinfo.DrawDataCache.Add(drawData);
            if (drawinfo.headGlowMask != -1)
            {
              if (drawinfo.headGlowMask == 309)
              {
                int tvScreen = PlayerDrawLayers.DrawPlayer_Head_GetTVScreen(drawinfo.drawPlayer);
                if (tvScreen != 0)
                {
                  int index = 0 + drawinfo.drawPlayer.bodyFrame.Y / 56;
                  if (index >= Main.OffsetsPlayerHeadgear.Length)
                    index = 0;
                  Vector2 vector2_2 = Main.OffsetsPlayerHeadgear[index];
                  vector2_2.Y -= 2f;
                  vector2_2 *= (float) -drawinfo.playerEffect.HasFlag((Enum) SpriteEffects.FlipVertically).ToDirectionInt();
                  Texture2D texture2D = TextureAssets.GlowMask[drawinfo.headGlowMask].Value;
                  int frameY = drawinfo.drawPlayer.miscCounter % 20 / 5;
                  if (tvScreen == 5)
                  {
                    frameY = 0;
                    if (drawinfo.drawPlayer.eyeHelper.EyeFrameToShow > 0)
                      frameY = 2;
                  }
                  Rectangle rectangle = texture2D.Frame(6, 4, tvScreen, frameY, -2);
                  drawData = new DrawData(texture2D, vector2_2 + helmetOffset + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(rectangle), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
                  drawData.shader = drawinfo.cHead;
                  drawinfo.DrawDataCache.Add(drawData);
                }
              }
              else if (drawinfo.headGlowMask == 273)
              {
                for (int index = 0; index < 2; ++index)
                {
                  Vector2 vector2_3 = new Vector2((float) Main.rand.Next(-10, 10) * 0.125f, (float) Main.rand.Next(-10, 10) * 0.125f);
                  drawData = new DrawData(TextureAssets.GlowMask[drawinfo.headGlowMask].Value, vector2_3 + helmetOffset + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, headVect, 1f, drawinfo.playerEffect);
                  drawData.shader = drawinfo.cHead;
                  drawinfo.DrawDataCache.Add(drawData);
                }
              }
              else
              {
                drawData = new DrawData(TextureAssets.GlowMask[drawinfo.headGlowMask].Value, helmetOffset + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, headVect, 1f, drawinfo.playerEffect);
                drawData.shader = drawinfo.cHead;
                drawinfo.DrawDataCache.Add(drawData);
              }
            }
            if (drawinfo.drawPlayer.head == 211)
            {
              Color color2 = new Color(100, 100, 100, 0);
              ulong seed = (ulong) (drawinfo.drawPlayer.miscCounter / 4 + 100);
              int num13 = 4;
              for (int index = 0; index < num13; ++index)
              {
                float x = (float) Utils.RandomInt(ref seed, -10, 11) * 0.2f;
                float y = (float) Utils.RandomInt(ref seed, -14, 1) * 0.15f;
                drawData = new DrawData(TextureAssets.GlowMask[241].Value, helmetOffset + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + new Vector2(x, y), new Rectangle?(drawinfo.drawPlayer.bodyFrame), color2, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
                drawData.shader = drawinfo.cHead;
                drawinfo.DrawDataCache.Add(drawData);
              }
            }
          }
        }
      }
      else if (!drawinfo.drawPlayer.invis && (drawinfo.drawPlayer.face < (sbyte) 0 || !ArmorIDs.Face.Sets.PreventHairDraw[(int) drawinfo.drawPlayer.face]))
      {
        drawData = new DrawData(TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, position, new Rectangle?(drawinfo.hairFrontFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.hairDyePacked;
        drawinfo.DrawDataCache.Add(drawData);
      }
      if (drawinfo.drawPlayer.beard > (sbyte) 0 && (drawinfo.drawPlayer.head < 0 || !ArmorIDs.Head.Sets.PreventBeardDraw[drawinfo.drawPlayer.head]))
      {
        Vector2 offsetFromHelmet = drawinfo.drawPlayer.GetBeardDrawOffsetFromHelmet();
        Color color = drawinfo.colorArmorHead;
        if (ArmorIDs.Beard.Sets.UseHairColor[(int) drawinfo.drawPlayer.beard])
          color = drawinfo.colorHair;
        drawData = new DrawData(TextureAssets.AccBeard[(int) drawinfo.drawPlayer.beard].Value, offsetFromHelmet + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cBeard;
        drawinfo.DrawDataCache.Add(drawData);
      }
      if (drawinfo.drawPlayer.head == 205)
      {
        drawData = new DrawData(TextureAssets.Extra[77].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect)
        {
          shader = drawinfo.skinDyePacked
        };
        drawinfo.DrawDataCache.Add(drawData);
      }
      if (drawinfo.drawPlayer.head == 214 && !drawinfo.drawPlayer.invis)
      {
        Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame with
        {
          Y = 0
        };
        float t = (float) drawinfo.drawPlayer.miscCounter / 300f;
        Color color = new Color(0, 0, 0, 0);
        float from = 0.8f;
        float to = 0.9f;
        if ((double) t >= (double) from)
          color = Color.Lerp(Color.Transparent, new Color(200, 200, 200, 0), Utils.GetLerpValue(from, to, t, true));
        if ((double) t >= (double) to)
          color = Color.Lerp(Color.Transparent, new Color(200, 200, 200, 0), Utils.GetLerpValue(1f, to, t, true));
        color *= drawinfo.stealth * (1f - drawinfo.shadow);
        drawData = new DrawData(TextureAssets.Extra[90].Value, helmetOffset + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect - Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height], new Rectangle?(bodyFrame), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
        drawinfo.DrawDataCache.Add(drawData);
      }
      if (drawinfo.drawPlayer.head == 137)
      {
        drawData = new DrawData(TextureAssets.JackHat.Value, helmetOffset + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
        drawinfo.DrawDataCache.Add(drawData);
        for (int index = 0; index < 7; ++index)
        {
          Color color = new Color(110 - index * 10, 110 - index * 10, 110 - index * 10, 110 - index * 10);
          Vector2 vector2_4 = new Vector2((float) Main.rand.Next(-10, 11) * 0.2f, (float) Main.rand.Next(-10, 11) * 0.2f);
          vector2_4.X = drawinfo.drawPlayer.itemFlamePos[index].X;
          vector2_4.Y = drawinfo.drawPlayer.itemFlamePos[index].Y;
          vector2_4 *= 0.5f;
          drawData = new DrawData(TextureAssets.JackHat.Value, helmetOffset + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + vector2_4, new Rectangle?(drawinfo.drawPlayer.bodyFrame), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
          drawinfo.DrawDataCache.Add(drawData);
        }
      }
      if (!drawinfo.drawPlayer.babyBird)
        return;
      Rectangle bodyFrame1 = drawinfo.drawPlayer.bodyFrame with
      {
        Y = 0
      };
      Vector2 vector2_5 = Vector2.Zero;
      Color color3 = drawinfo.colorArmorHead;
      if (drawinfo.drawPlayer.mount.Active && drawinfo.drawPlayer.mount.Type == 52)
      {
        Vector2 mountedCenter = drawinfo.drawPlayer.MountedCenter;
        color3 = drawinfo.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int) mountedCenter.X / 16, (int) mountedCenter.Y / 16, Color.White), drawinfo.shadow);
        vector2_5 = new Vector2(0.0f, 6f) * drawinfo.drawPlayer.Directions;
      }
      drawData = new DrawData(TextureAssets.Extra[100].Value, vector2_5 + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height] * drawinfo.drawPlayer.gravDir, new Rectangle?(bodyFrame1), color3, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
      drawinfo.DrawDataCache.Add(drawData);
    }

    public static int DrawPlayer_Head_GetTVScreen(Player plr)
    {
      if (NPC.AnyDanger())
        return 4;
      if (plr.statLife <= plr.statLifeMax2 / 4)
        return 1;
      if (plr.ZoneCorrupt || plr.ZoneCrimson || plr.ZoneGraveyard)
        return 0;
      if (plr.wet)
        return 2;
      return (double) plr.townNPCs >= 3.0 || BirthdayParty.PartyIsUp || LanternNight.LanternsUp ? 5 : 3;
    }

    private static int GetHatStacks(ref PlayerDrawSet drawinfo, int hatItemId)
    {
      int hatStacks = 0;
      int index1 = 0;
      if (drawinfo.drawPlayer.armor[index1] != null && drawinfo.drawPlayer.armor[index1].type == hatItemId && drawinfo.drawPlayer.armor[index1].stack > 0)
        hatStacks += drawinfo.drawPlayer.armor[index1].stack;
      int index2 = 10;
      if (drawinfo.drawPlayer.armor[index2] != null && drawinfo.drawPlayer.armor[index2].type == hatItemId && drawinfo.drawPlayer.armor[index2].stack > 0)
        hatStacks += drawinfo.drawPlayer.armor[index2].stack;
      if (hatStacks > 2)
        hatStacks = 2;
      return hatStacks;
    }

    private static Vector2 DrawPlayer_21_Head_GetSpecialHatDrawPosition(
      ref PlayerDrawSet drawinfo,
      ref Vector2 helmetOffset,
      Vector2 hatOffset)
    {
      Vector2 vector2 = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height] * drawinfo.drawPlayer.Directions;
      Vector2 vec = (drawinfo.Position - Main.screenPosition + helmetOffset + new Vector2((float) (-drawinfo.drawPlayer.bodyFrame.Width / 2 + drawinfo.drawPlayer.width / 2), (float) (drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height + 4)) + hatOffset * drawinfo.drawPlayer.Directions + vector2).Floor() + (drawinfo.drawPlayer.headPosition + drawinfo.headVect);
      if ((double) drawinfo.drawPlayer.gravDir == -1.0)
        vec.Y += 12f;
      vec = vec.Floor();
      return vec;
    }

    private static void DrawPlayer_21_Head_TheFace(ref PlayerDrawSet drawinfo)
    {
      bool flag = drawinfo.drawPlayer.head == 38 || drawinfo.drawPlayer.head == 135 || drawinfo.drawPlayer.head == 269;
      if (!flag && drawinfo.drawPlayer.faceHead > (sbyte) 0 && (int) drawinfo.drawPlayer.faceHead < (int) ArmorIDs.Face.Count)
      {
        Vector2 offsetFromHelmet = drawinfo.drawPlayer.GetFaceHeadOffsetFromHelmet();
        DrawData drawData = new DrawData(TextureAssets.AccFace[(int) drawinfo.drawPlayer.faceHead].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + offsetFromHelmet, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cFaceHead;
        drawinfo.DrawDataCache.Add(drawData);
        if (drawinfo.drawPlayer.face <= (sbyte) 0 || !ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[(int) drawinfo.drawPlayer.face])
          return;
        float num = 0.0f;
        if (drawinfo.drawPlayer.face == (sbyte) 5)
        {
          switch (drawinfo.drawPlayer.faceHead)
          {
            case 10:
            case 11:
            case 12:
            case 13:
              num = (float) (2 * drawinfo.drawPlayer.direction);
              break;
          }
        }
        drawData = new DrawData(TextureAssets.AccFace[(int) drawinfo.drawPlayer.face].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)) + num, (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cFace;
        drawinfo.DrawDataCache.Add(drawData);
      }
      else
      {
        if (drawinfo.drawPlayer.invis || flag)
          return;
        DrawData drawData1 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 0].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
        drawData1.shader = drawinfo.skinDyePacked;
        DrawData drawData2 = drawData1;
        drawinfo.DrawDataCache.Add(drawData2);
        drawData2 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 1].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorEyeWhites, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
        drawinfo.DrawDataCache.Add(drawData2);
        drawData2 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 2].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorEyes, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
        drawinfo.DrawDataCache.Add(drawData2);
        Asset<Texture2D> player = TextureAssets.Players[drawinfo.skinVar, 15];
        if (player.IsLoaded)
        {
          Vector2 vector2 = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
          vector2.Y -= 2f;
          vector2 *= (float) -drawinfo.playerEffect.HasFlag((Enum) SpriteEffects.FlipVertically).ToDirectionInt();
          Rectangle rectangle = player.Frame(verticalFrames: 3, frameY: drawinfo.drawPlayer.eyeHelper.EyeFrameToShow);
          drawData1 = new DrawData(player.Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + vector2, new Rectangle?(rectangle), drawinfo.colorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
          drawData1.shader = drawinfo.skinDyePacked;
          drawData2 = drawData1;
          drawinfo.DrawDataCache.Add(drawData2);
        }
        if (drawinfo.drawPlayer.yoraiz0rDarkness)
        {
          drawData1 = new DrawData(TextureAssets.Extra[67].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
          drawData1.shader = drawinfo.skinDyePacked;
          drawData2 = drawData1;
          drawinfo.DrawDataCache.Add(drawData2);
        }
        if (drawinfo.drawPlayer.face <= (sbyte) 0 || !ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[(int) drawinfo.drawPlayer.face])
          return;
        drawData2 = new DrawData(TextureAssets.AccFace[(int) drawinfo.drawPlayer.face].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
        drawData2.shader = drawinfo.cFace;
        drawinfo.DrawDataCache.Add(drawData2);
      }
    }

    public static void DrawPlayer_21_1_Magiluminescence(ref PlayerDrawSet drawinfo)
    {
      if ((double) drawinfo.shadow != 0.0 || drawinfo.drawPlayer.neck != (sbyte) 11 || drawinfo.hideEntirePlayer)
        return;
      Color colorArmorBody = drawinfo.colorArmorBody;
      Color color = new Color(140, 140, 35, 12);
      float amount = (float) ((double) ((int) colorArmorBody.R + (int) colorArmorBody.G + (int) colorArmorBody.B) / 3.0 / (double) byte.MaxValue);
      color = Color.Lerp(color, Color.Transparent, amount);
      if (color == Color.Transparent)
        return;
      drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.GlowMask[310].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), color, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect)
      {
        shader = drawinfo.cNeck
      });
    }

    public static void DrawPlayer_22_FaceAcc(ref PlayerDrawSet drawinfo)
    {
      Vector2 vector2_1 = Vector2.Zero;
      if (drawinfo.drawPlayer.mount.Active && drawinfo.drawPlayer.mount.Type == 52)
        vector2_1 = new Vector2(28f, -2f);
      Vector2 vector2_2 = vector2_1 * drawinfo.drawPlayer.Directions;
      DrawData drawData;
      if (drawinfo.drawPlayer.face > (sbyte) 0 && (int) drawinfo.drawPlayer.face < (int) ArmorIDs.Face.Count && !ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[(int) drawinfo.drawPlayer.face])
      {
        Vector2 vector2_3 = Vector2.Zero;
        if (drawinfo.drawPlayer.face == (sbyte) 19)
          vector2_3 = new Vector2(0.0f, -6f) * drawinfo.drawPlayer.Directions;
        drawData = new DrawData(TextureAssets.AccFace[(int) drawinfo.drawPlayer.face].Value, vector2_3 + vector2_2 + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cFace;
        drawinfo.DrawDataCache.Add(drawData);
      }
      if (drawinfo.drawPlayer.faceFlower > (sbyte) 0 && (int) drawinfo.drawPlayer.faceFlower < (int) ArmorIDs.Face.Count)
      {
        drawData = new DrawData(TextureAssets.AccFace[(int) drawinfo.drawPlayer.faceFlower].Value, vector2_2 + drawinfo.helmetOffset + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cFaceFlower;
        drawinfo.DrawDataCache.Add(drawData);
      }
      if (drawinfo.drawUnicornHorn)
      {
        drawData = new DrawData(TextureAssets.Extra[143].Value, vector2_2 + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cUnicornHorn;
        drawinfo.DrawDataCache.Add(drawData);
      }
      if (!drawinfo.drawAngelHalo)
        return;
      Color color = drawinfo.drawPlayer.GetImmuneAlphaPure(new Color(200, 200, 200, 150), drawinfo.shadow) * drawinfo.drawPlayer.stealth;
      Main.instance.LoadAccFace(7);
      drawData = new DrawData(TextureAssets.AccFace[7].Value, vector2_2 + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
      drawData.shader = drawinfo.cAngelHalo;
      drawinfo.DrawDataCache.Add(drawData);
    }

    public static void DrawTiedBalloons(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.mount.Type != 34)
        return;
      Texture2D texture2D = TextureAssets.Extra[141].Value;
      Vector2 vector2 = new Vector2(0.0f, 4f);
      Color colorMount = drawinfo.colorMount;
      int frameY = (int) ((double) Main.GlobalTimeWrappedHourly * 3.0 + (double) drawinfo.drawPlayer.position.X / 50.0) % 3;
      Rectangle rectangle = texture2D.Frame(verticalFrames: 3, frameY: frameY);
      Vector2 origin = new Vector2((float) (rectangle.Width / 2), (float) rectangle.Height);
      float rotation = (float) (-(double) drawinfo.drawPlayer.velocity.X * 0.10000000149011612) - drawinfo.drawPlayer.fullRotation;
      DrawData drawData = new DrawData(texture2D, drawinfo.drawPlayer.MountedCenter + vector2 - Main.screenPosition, new Rectangle?(rectangle), colorMount, rotation, origin, 1f, drawinfo.playerEffect);
      drawinfo.DrawDataCache.Add(drawData);
    }

    public static void DrawStarboardRainbowTrail(
      ref PlayerDrawSet drawinfo,
      Vector2 commonWingPosPreFloor,
      Vector2 dirsVec)
    {
      if ((double) drawinfo.shadow != 0.0)
        return;
      int num1 = Math.Min(drawinfo.drawPlayer.availableAdvancedShadowsCount - 1, 30);
      float num2 = 0.0f;
      for (int shadowIndex = num1; shadowIndex > 0; --shadowIndex)
      {
        EntityShadowInfo advancedShadow1 = drawinfo.drawPlayer.GetAdvancedShadow(shadowIndex);
        EntityShadowInfo advancedShadow2 = drawinfo.drawPlayer.GetAdvancedShadow(shadowIndex - 1);
        num2 += Vector2.Distance(advancedShadow1.Position, advancedShadow2.Position);
      }
      float num3 = MathHelper.Clamp(num2 / 160f, 0.0f, 1f);
      Main.instance.LoadProjectile(250);
      Texture2D texture = TextureAssets.Projectile[250].Value;
      float x = 1.7f;
      Vector2 origin = new Vector2((float) (texture.Width / 2), (float) (texture.Height / 2));
      Vector2 vector2_1 = new Vector2((float) drawinfo.drawPlayer.width, (float) drawinfo.drawPlayer.height) / 2f;
      Color white = Color.White with { A = 64 };
      Vector2 vector2_2 = drawinfo.drawPlayer.DefaultSize * new Vector2(0.5f, 1f) + new Vector2(0.0f, -4f);
      if ((double) dirsVec.Y < 0.0)
        vector2_2 = drawinfo.drawPlayer.DefaultSize * new Vector2(0.5f, 0.0f) + new Vector2(0.0f, 4f);
      for (int shadowIndex = num1; shadowIndex > 0; --shadowIndex)
      {
        EntityShadowInfo advancedShadow3 = drawinfo.drawPlayer.GetAdvancedShadow(shadowIndex);
        EntityShadowInfo advancedShadow4 = drawinfo.drawPlayer.GetAdvancedShadow(shadowIndex - 1);
        Vector2 pos1 = advancedShadow3.Position + vector2_2 + advancedShadow3.HeadgearOffset;
        Vector2 pos2 = advancedShadow4.Position + vector2_2 + advancedShadow4.HeadgearOffset;
        Vector2 vector2_3 = drawinfo.drawPlayer.RotatedRelativePoint(pos1, true, false);
        Vector2 vector2_4 = drawinfo.drawPlayer.RotatedRelativePoint(pos2, true, false);
        float num4 = (vector2_4 - vector2_3).ToRotation() - 1.57079637f;
        float rotation = 1.57079637f * (float) drawinfo.drawPlayer.direction;
        float t = Math.Abs(vector2_4.X - vector2_3.X);
        Vector2 scale = new Vector2(x, t / (float) texture.Height);
        float num5 = (float) (1.0 - (double) shadowIndex / (double) num1);
        float num6 = num5 * num5 * Utils.GetLerpValue(0.0f, 4f, t, true) * 0.5f;
        float num7 = num6 * num6;
        Color color = white * num7 * num3;
        if (!(color == Color.Transparent))
        {
          DrawData drawData = new DrawData(texture, vector2_3 - Main.screenPosition, new Rectangle?(), color, rotation, origin, scale, drawinfo.playerEffect);
          drawData.shader = drawinfo.cWings;
          drawinfo.DrawDataCache.Add(drawData);
          for (float amount = 0.25f; (double) amount < 1.0; amount += 0.25f)
          {
            drawData = new DrawData(texture, Vector2.Lerp(vector2_3, vector2_4, amount) - Main.screenPosition, new Rectangle?(), color, rotation, origin, scale, drawinfo.playerEffect);
            drawData.shader = drawinfo.cWings;
            drawinfo.DrawDataCache.Add(drawData);
          }
        }
      }
    }

    public static void DrawMeowcartTrail(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.mount.Type != 33 || (double) drawinfo.shadow > 0.0)
        return;
      int num1 = Math.Min(drawinfo.drawPlayer.availableAdvancedShadowsCount - 1, 20);
      float num2 = 0.0f;
      for (int shadowIndex = num1; shadowIndex > 0; --shadowIndex)
      {
        EntityShadowInfo advancedShadow1 = drawinfo.drawPlayer.GetAdvancedShadow(shadowIndex);
        EntityShadowInfo advancedShadow2 = drawinfo.drawPlayer.GetAdvancedShadow(shadowIndex - 1);
        num2 += Vector2.Distance(advancedShadow1.Position, advancedShadow2.Position);
      }
      float num3 = MathHelper.Clamp(num2 / 160f, 0.0f, 1f);
      Main.instance.LoadProjectile(250);
      Texture2D texture = TextureAssets.Projectile[250].Value;
      float x = 1.5f;
      Vector2 origin = new Vector2((float) (texture.Width / 2), 0.0f);
      Vector2 vector2_1 = new Vector2((float) drawinfo.drawPlayer.width, (float) drawinfo.drawPlayer.height) / 2f;
      Vector2 vector2_2 = new Vector2((float) (-drawinfo.drawPlayer.direction * 10), 15f);
      Color white = Color.White with { A = 127 };
      Vector2 vector2_3 = vector2_2;
      Vector2 vector2_4 = vector2_1 + vector2_3;
      Vector2 zero = Vector2.Zero;
      Vector2 vector2_5 = drawinfo.drawPlayer.RotatedRelativePoint(drawinfo.drawPlayer.Center + zero + vector2_2) - drawinfo.drawPlayer.position;
      for (int shadowIndex = num1; shadowIndex > 0; --shadowIndex)
      {
        EntityShadowInfo advancedShadow3 = drawinfo.drawPlayer.GetAdvancedShadow(shadowIndex);
        EntityShadowInfo advancedShadow4 = drawinfo.drawPlayer.GetAdvancedShadow(shadowIndex - 1);
        Vector2 vector2_6 = advancedShadow3.Position + zero;
        Vector2 vector2_7 = advancedShadow4.Position + zero;
        Vector2 pos1 = vector2_6 + vector2_5;
        Vector2 pos2 = vector2_7 + vector2_5;
        Vector2 vector2_8 = drawinfo.drawPlayer.RotatedRelativePoint(pos1, true, false);
        Vector2 vector2_9 = drawinfo.drawPlayer.RotatedRelativePoint(pos2, true, false);
        float rotation = (vector2_9 - vector2_8).ToRotation() - 1.57079637f;
        float num4 = Vector2.Distance(vector2_8, vector2_9);
        Vector2 scale = new Vector2(x, num4 / (float) texture.Height);
        float num5 = (float) (1.0 - (double) shadowIndex / (double) num1);
        float num6 = num5 * num5;
        Color color = white * num6 * num3;
        DrawData drawData = new DrawData(texture, vector2_8 - Main.screenPosition, new Rectangle?(), color, rotation, origin, scale, drawinfo.playerEffect);
        drawinfo.DrawDataCache.Add(drawData);
      }
    }

    public static void DrawPlayer_23_MountFront(ref PlayerDrawSet drawinfo)
    {
      if (!drawinfo.drawPlayer.mount.Active)
        return;
      drawinfo.drawPlayer.mount.Draw(drawinfo.DrawDataCache, 2, drawinfo.drawPlayer, drawinfo.Position, drawinfo.colorMount, drawinfo.playerEffect, drawinfo.shadow);
      drawinfo.drawPlayer.mount.Draw(drawinfo.DrawDataCache, 3, drawinfo.drawPlayer, drawinfo.Position, drawinfo.colorMount, drawinfo.playerEffect, drawinfo.shadow);
    }

    public static void DrawPlayer_24_Pulley(ref PlayerDrawSet drawinfo)
    {
      if (!drawinfo.drawPlayer.pulley || drawinfo.drawPlayer.itemAnimation != 0)
        return;
      if (drawinfo.drawPlayer.pulleyDir == (byte) 2)
      {
        int num1 = -25;
        int num2 = 0;
        float rotation = 0.0f;
        DrawData drawData = new DrawData(TextureAssets.Pulley.Value, new Vector2((float) ((int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X + (double) (drawinfo.drawPlayer.width / 2) - (double) (9 * drawinfo.drawPlayer.direction)) + num2 * drawinfo.drawPlayer.direction), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) (drawinfo.drawPlayer.height / 2) + 2.0 * (double) drawinfo.drawPlayer.gravDir + (double) num1 * (double) drawinfo.drawPlayer.gravDir)), new Rectangle?(new Rectangle(0, TextureAssets.Pulley.Height() / 2 * drawinfo.drawPlayer.pulleyFrame, TextureAssets.Pulley.Width(), TextureAssets.Pulley.Height() / 2)), drawinfo.colorArmorHead, rotation, new Vector2((float) (TextureAssets.Pulley.Width() / 2), (float) (TextureAssets.Pulley.Height() / 4)), 1f, drawinfo.playerEffect);
        drawinfo.DrawDataCache.Add(drawData);
      }
      else
      {
        int num3 = -26;
        int num4 = 10;
        float rotation = 0.35f * (float) -drawinfo.drawPlayer.direction;
        DrawData drawData = new DrawData(TextureAssets.Pulley.Value, new Vector2((float) ((int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X + (double) (drawinfo.drawPlayer.width / 2) - (double) (9 * drawinfo.drawPlayer.direction)) + num4 * drawinfo.drawPlayer.direction), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) (drawinfo.drawPlayer.height / 2) + 2.0 * (double) drawinfo.drawPlayer.gravDir + (double) num3 * (double) drawinfo.drawPlayer.gravDir)), new Rectangle?(new Rectangle(0, TextureAssets.Pulley.Height() / 2 * drawinfo.drawPlayer.pulleyFrame, TextureAssets.Pulley.Width(), TextureAssets.Pulley.Height() / 2)), drawinfo.colorArmorHead, rotation, new Vector2((float) (TextureAssets.Pulley.Width() / 2), (float) (TextureAssets.Pulley.Height() / 4)), 1f, drawinfo.playerEffect);
        drawinfo.DrawDataCache.Add(drawData);
      }
    }

    public static void DrawPlayer_25_Shield(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.shield <= (sbyte) 0 || (int) drawinfo.drawPlayer.shield >= ArmorIDs.Shield.Count)
        return;
      Vector2 zero1 = Vector2.Zero;
      if (drawinfo.drawPlayer.shieldRaised)
        zero1.Y -= 4f * drawinfo.drawPlayer.gravDir;
      Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
      Vector2 zero2 = Vector2.Zero;
      Vector2 bodyVect = drawinfo.bodyVect;
      if (bodyFrame.Width != TextureAssets.AccShield[(int) drawinfo.drawPlayer.shield].Value.Width)
      {
        bodyFrame.Width = TextureAssets.AccShield[(int) drawinfo.drawPlayer.shield].Value.Width;
        bodyVect.X += (float) (bodyFrame.Width - TextureAssets.AccShield[(int) drawinfo.drawPlayer.shield].Value.Width);
        if (drawinfo.playerEffect.HasFlag((Enum) SpriteEffects.FlipHorizontally))
          bodyVect.X = (float) bodyFrame.Width - bodyVect.X;
      }
      DrawData drawData;
      if (drawinfo.drawPlayer.shieldRaised)
      {
        float num1 = (float) Math.Sin((double) Main.GlobalTimeWrappedHourly * 6.2831854820251465);
        float x = (float) (2.5 + 1.5 * (double) num1);
        Color colorArmorBody = drawinfo.colorArmorBody with
        {
          A = 0
        };
        colorArmorBody *= (float) (0.44999998807907104 - (double) num1 * 0.15000000596046448);
        for (float num2 = 0.0f; (double) num2 < 4.0; ++num2)
        {
          drawData = new DrawData(TextureAssets.AccShield[(int) drawinfo.drawPlayer.shield].Value, zero2 + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)) + zero1 + new Vector2(x, 0.0f).RotatedBy((double) num2 / 4.0 * 6.2831854820251465), new Rectangle?(bodyFrame), colorArmorBody, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect);
          drawData.shader = drawinfo.cShield;
          drawinfo.DrawDataCache.Add(drawData);
        }
      }
      drawData = new DrawData(TextureAssets.AccShield[(int) drawinfo.drawPlayer.shield].Value, zero2 + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)) + zero1, new Rectangle?(bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect);
      drawData.shader = drawinfo.cShield;
      drawinfo.DrawDataCache.Add(drawData);
      if (drawinfo.drawPlayer.shieldRaised)
      {
        Color colorArmorBody = drawinfo.colorArmorBody;
        float num = (float) Math.Sin((double) Main.GlobalTimeWrappedHourly * 3.1415927410125732);
        colorArmorBody.A = (byte) ((double) colorArmorBody.A * (0.5 + 0.5 * (double) num));
        colorArmorBody *= (float) (0.5 + 0.5 * (double) num);
        drawData = new DrawData(TextureAssets.AccShield[(int) drawinfo.drawPlayer.shield].Value, zero2 + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)) + zero1, new Rectangle?(bodyFrame), colorArmorBody, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cShield;
      }
      if (drawinfo.drawPlayer.shieldRaised && drawinfo.drawPlayer.shieldParryTimeLeft > 0)
      {
        float num3 = (float) drawinfo.drawPlayer.shieldParryTimeLeft / 20f;
        float num4 = 1.5f * num3;
        Vector2 vector2_1 = zero2 + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)) + zero1;
        Color colorArmorBody = drawinfo.colorArmorBody;
        float num5 = 1f;
        Vector2 vector2_2 = drawinfo.Position + drawinfo.drawPlayer.Size / 2f - Main.screenPosition;
        Vector2 vector2_3 = vector2_1 - vector2_2;
        Vector2 position = vector2_1 + vector2_3 * num4;
        float scale = num5 + num4;
        colorArmorBody.A = (byte) ((double) colorArmorBody.A * (1.0 - (double) num3));
        Color color = colorArmorBody * (1f - num3);
        drawData = new DrawData(TextureAssets.AccShield[(int) drawinfo.drawPlayer.shield].Value, position, new Rectangle?(bodyFrame), color, drawinfo.drawPlayer.bodyRotation, bodyVect, scale, drawinfo.playerEffect);
        drawData.shader = drawinfo.cShield;
        drawinfo.DrawDataCache.Add(drawData);
      }
      if (!drawinfo.drawPlayer.mount.Cart)
        return;
      drawinfo.DrawDataCache.Reverse(drawinfo.DrawDataCache.Count - 2, 2);
    }

    public static void DrawPlayer_26_SolarShield(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.solarShields <= 0 || (double) drawinfo.shadow != 0.0 || drawinfo.drawPlayer.dead)
        return;
      Texture2D texture2D = TextureAssets.Extra[61 + drawinfo.drawPlayer.solarShields - 1].Value;
      Color color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) sbyte.MaxValue);
      float rotation1 = (drawinfo.drawPlayer.solarShieldPos[0] * new Vector2(1f, 0.5f)).ToRotation();
      if (drawinfo.drawPlayer.direction == -1)
        rotation1 += 3.14159274f;
      float rotation2 = rotation1 + 0.06283186f * (float) drawinfo.drawPlayer.direction;
      drawinfo.DrawDataCache.Add(new DrawData(texture2D, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) (drawinfo.drawPlayer.height / 2))) + drawinfo.drawPlayer.solarShieldPos[0], new Rectangle?(), color, rotation2, texture2D.Size() / 2f, 1f, drawinfo.playerEffect)
      {
        shader = drawinfo.cBody
      });
    }

    public static void DrawPlayer_27_HeldItem(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.JustDroppedAnItem)
        return;
      if (drawinfo.drawPlayer.heldProj >= 0 && (double) drawinfo.shadow == 0.0 && !drawinfo.heldProjOverHand)
        drawinfo.projectileDrawPosition = drawinfo.DrawDataCache.Count;
      Item heldItem = drawinfo.heldItem;
      int index1 = heldItem.type;
      if (drawinfo.drawPlayer.UsingBiomeTorches)
      {
        switch (index1)
        {
          case 8:
            index1 = drawinfo.drawPlayer.BiomeTorchHoldStyle(index1);
            break;
          case 966:
            index1 = drawinfo.drawPlayer.BiomeCampfireHoldStyle(index1);
            break;
        }
      }
      float adjustedItemScale = drawinfo.drawPlayer.GetAdjustedItemScale(heldItem);
      Main.instance.LoadItem(index1);
      Texture2D texture = TextureAssets.Item[index1].Value;
      Vector2 position = new Vector2((float) (int) ((double) drawinfo.ItemLocation.X - (double) Main.screenPosition.X), (float) (int) ((double) drawinfo.ItemLocation.Y - (double) Main.screenPosition.Y));
      Rectangle itemDrawFrame = drawinfo.drawPlayer.GetItemDrawFrame(index1);
      drawinfo.itemColor = Lighting.GetColor((int) ((double) drawinfo.Position.X + (double) drawinfo.drawPlayer.width * 0.5) / 16, (int) (((double) drawinfo.Position.Y + (double) drawinfo.drawPlayer.height * 0.5) / 16.0));
      if (index1 == 678)
        drawinfo.itemColor = Color.White;
      if (drawinfo.drawPlayer.shroomiteStealth && heldItem.ranged)
      {
        float num1 = drawinfo.drawPlayer.stealth;
        if ((double) num1 < 0.03)
          num1 = 0.03f;
        float num2 = (float) ((1.0 + (double) num1 * 10.0) / 11.0);
        drawinfo.itemColor = new Color((int) (byte) ((double) drawinfo.itemColor.R * (double) num1), (int) (byte) ((double) drawinfo.itemColor.G * (double) num1), (int) (byte) ((double) drawinfo.itemColor.B * (double) num2), (int) (byte) ((double) drawinfo.itemColor.A * (double) num1));
      }
      if (drawinfo.drawPlayer.setVortex && heldItem.ranged)
      {
        float num3 = drawinfo.drawPlayer.stealth;
        if ((double) num3 < 0.03)
          num3 = 0.03f;
        double num4 = (1.0 + (double) num3 * 10.0) / 11.0;
        drawinfo.itemColor = drawinfo.itemColor.MultiplyRGBA(new Color(Vector4.Lerp(Vector4.One, new Vector4(0.0f, 0.12f, 0.16f, 0.0f), 1f - num3)));
      }
      bool flag1 = drawinfo.drawPlayer.itemAnimation > 0 && heldItem.useStyle != 0;
      bool flag2 = heldItem.holdStyle != 0 && !drawinfo.drawPlayer.pulley;
      if (!drawinfo.drawPlayer.CanVisuallyHoldItem(heldItem))
        flag2 = false;
      if ((double) drawinfo.shadow != 0.0 || drawinfo.drawPlayer.frozen || !(flag1 | flag2) || index1 <= 0 || drawinfo.drawPlayer.dead || heldItem.noUseGraphic || drawinfo.drawPlayer.wet && heldItem.noWet || drawinfo.drawPlayer.happyFunTorchTime && drawinfo.drawPlayer.inventory[drawinfo.drawPlayer.selectedItem].createTile == 4 && drawinfo.drawPlayer.itemAnimation == 0)
        return;
      string name = drawinfo.drawPlayer.name;
      Color color1 = new Color(250, 250, 250, heldItem.alpha);
      Vector2 vector2_1 = Vector2.Zero;
      if (index1 <= 426)
      {
        if (index1 <= 104)
        {
          if (index1 != 46)
          {
            if (index1 != 104)
              goto label_37;
          }
          else
          {
            color1 = Color.Lerp(Color.Transparent, new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) sbyte.MaxValue) * 0.7f, Utils.Remap(drawinfo.itemColor.ToVector3().Length() / 1.731f, 0.3f, 0.5f, 1f, 0.0f));
            goto label_37;
          }
        }
        else if (index1 != 204)
        {
          if (index1 == 426)
            goto label_33;
          else
            goto label_37;
        }
        else
        {
          vector2_1 = new Vector2(4f, -6f) * drawinfo.drawPlayer.Directions;
          goto label_37;
        }
      }
      else if (index1 <= 1506)
      {
        if (index1 == 797 || index1 == 1506)
          goto label_33;
        else
          goto label_37;
      }
      else if (index1 != 3349)
      {
        if ((uint) (index1 - 5094) > 1U)
        {
          if ((uint) (index1 - 5096) <= 1U)
            goto label_33;
          else
            goto label_37;
        }
      }
      else
      {
        vector2_1 = new Vector2(2f, -2f) * drawinfo.drawPlayer.Directions;
        goto label_37;
      }
      vector2_1 = new Vector2(4f, -4f) * drawinfo.drawPlayer.Directions;
      goto label_37;
label_33:
      vector2_1 = new Vector2(6f, -6f) * drawinfo.drawPlayer.Directions;
label_37:
      if (index1 == 3823)
        vector2_1 = new Vector2((float) (7 * drawinfo.drawPlayer.direction), -7f * drawinfo.drawPlayer.gravDir);
      if (index1 == 3827)
      {
        vector2_1 = new Vector2((float) (13 * drawinfo.drawPlayer.direction), -13f * drawinfo.drawPlayer.gravDir);
        color1 = heldItem.GetAlpha(drawinfo.itemColor);
        color1 = Color.Lerp(color1, Color.White, 0.6f) with
        {
          A = (byte) 66
        };
      }
      Vector2 vector2_2 = new Vector2((float) ((double) itemDrawFrame.Width * 0.5 - (double) itemDrawFrame.Width * 0.5 * (double) drawinfo.drawPlayer.direction), (float) itemDrawFrame.Height);
      if (heldItem.useStyle == 9 && drawinfo.drawPlayer.itemAnimation > 0)
      {
        Vector2 vector2_3 = new Vector2(0.5f, 0.4f);
        if (heldItem.type == 5009 || heldItem.type == 5042)
        {
          vector2_3 = new Vector2(0.26f, 0.5f);
          if (drawinfo.drawPlayer.direction == -1)
            vector2_3.X = 1f - vector2_3.X;
        }
        vector2_2 = itemDrawFrame.Size() * vector2_3;
      }
      if ((double) drawinfo.drawPlayer.gravDir == -1.0)
        vector2_2.Y = (float) itemDrawFrame.Height - vector2_2.Y;
      Vector2 origin1 = vector2_2 + vector2_1;
      float rotation1 = drawinfo.drawPlayer.itemRotation;
      if (heldItem.useStyle == 8)
      {
        ref float local = ref position.X;
        double num = (double) local;
        int direction = drawinfo.drawPlayer.direction;
        local = (float) (num - 0.0);
        rotation1 -= 1.57079637f * (float) drawinfo.drawPlayer.direction;
        origin1.Y = 2f;
        origin1.X += (float) (2 * drawinfo.drawPlayer.direction);
      }
      if (index1 == 425 || index1 == 507)
        drawinfo.itemEffect = (double) drawinfo.drawPlayer.gravDir != 1.0 ? (drawinfo.drawPlayer.direction != 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None) : (drawinfo.drawPlayer.direction != 1 ? SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically : SpriteEffects.FlipVertically);
      if ((index1 == 946 || index1 == 4707) && (double) rotation1 != 0.0)
      {
        position.Y -= 22f * drawinfo.drawPlayer.gravDir;
        rotation1 = -1.57f * (float) -drawinfo.drawPlayer.direction * drawinfo.drawPlayer.gravDir;
      }
      ItemSlot.GetItemLight(ref drawinfo.itemColor, heldItem);
      switch (index1)
      {
        case 3476:
          Texture2D texture2D1 = TextureAssets.Extra[64].Value;
          Rectangle r1 = texture2D1.Frame(verticalFrames: 9, frameY: (drawinfo.drawPlayer.miscCounter % 54 / 6));
          Vector2 vector2_4 = new Vector2((float) (r1.Width / 2 * drawinfo.drawPlayer.direction), 0.0f);
          Vector2 origin2 = r1.Size() / 2f;
          DrawData drawData1 = new DrawData(texture2D1, (drawinfo.ItemLocation - Main.screenPosition + vector2_4).Floor(), new Rectangle?(r1), heldItem.GetAlpha(drawinfo.itemColor).MultiplyRGBA(new Color(new Vector4(0.5f, 0.5f, 0.5f, 0.8f))), drawinfo.drawPlayer.itemRotation, origin2, adjustedItemScale, drawinfo.itemEffect);
          drawinfo.DrawDataCache.Add(drawData1);
          drawData1 = new DrawData(TextureAssets.GlowMask[195].Value, (drawinfo.ItemLocation - Main.screenPosition + vector2_4).Floor(), new Rectangle?(r1), new Color(250, 250, 250, heldItem.alpha) * 0.5f, drawinfo.drawPlayer.itemRotation, origin2, adjustedItemScale, drawinfo.itemEffect);
          drawinfo.DrawDataCache.Add(drawData1);
          break;
        case 3779:
          Texture2D texture2D2 = texture;
          Rectangle r2 = texture2D2.Frame();
          Vector2 vector2_5 = new Vector2((float) (r2.Width / 2 * drawinfo.drawPlayer.direction), 0.0f);
          Vector2 origin3 = r2.Size() / 2f;
          Color color2 = new Color(120, 40, 222, 0) * (float) (((double) ((float) ((double) drawinfo.drawPlayer.miscCounter / 75.0 * 6.2831854820251465)).ToRotationVector2().X * 1.0 + 0.0) / 2.0 * 0.30000001192092896 + 0.85000002384185791) * 0.5f;
          float num5 = 2f;
          DrawData drawData2;
          for (float num6 = 0.0f; (double) num6 < 4.0; ++num6)
          {
            drawData2 = new DrawData(TextureAssets.GlowMask[218].Value, (drawinfo.ItemLocation - Main.screenPosition + vector2_5).Floor() + (num6 * 1.57079637f).ToRotationVector2() * num5, new Rectangle?(r2), color2, drawinfo.drawPlayer.itemRotation, origin3, adjustedItemScale, drawinfo.itemEffect);
            drawinfo.DrawDataCache.Add(drawData2);
          }
          drawData2 = new DrawData(texture2D2, (drawinfo.ItemLocation - Main.screenPosition + vector2_5).Floor(), new Rectangle?(r2), heldItem.GetAlpha(drawinfo.itemColor).MultiplyRGBA(new Color(new Vector4(0.5f, 0.5f, 0.5f, 0.8f))), drawinfo.drawPlayer.itemRotation, origin3, adjustedItemScale, drawinfo.itemEffect);
          drawinfo.DrawDataCache.Add(drawData2);
          break;
        case 4049:
          Texture2D texture2D3 = TextureAssets.Extra[92].Value;
          Rectangle r3 = texture2D3.Frame(verticalFrames: 4, frameY: (drawinfo.drawPlayer.miscCounter % 20 / 5));
          Vector2 vector2_6 = new Vector2((float) (r3.Width / 2 * drawinfo.drawPlayer.direction), 0.0f) + new Vector2((float) (-10 * drawinfo.drawPlayer.direction), 8f * drawinfo.drawPlayer.gravDir);
          Vector2 origin4 = r3.Size() / 2f;
          DrawData drawData3 = new DrawData(texture2D3, (drawinfo.ItemLocation - Main.screenPosition + vector2_6).Floor(), new Rectangle?(r3), heldItem.GetAlpha(drawinfo.itemColor), drawinfo.drawPlayer.itemRotation, origin4, adjustedItemScale, drawinfo.itemEffect);
          drawinfo.DrawDataCache.Add(drawData3);
          break;
        default:
          if (heldItem.useStyle == 5)
          {
            if (Item.staff[index1])
            {
              float num7 = drawinfo.drawPlayer.itemRotation + 0.785f * (float) drawinfo.drawPlayer.direction;
              float num8 = 0.0f;
              float num9 = 0.0f;
              Vector2 origin5 = new Vector2(0.0f, (float) itemDrawFrame.Height);
              if (index1 == 3210)
              {
                num8 = (float) (8 * -drawinfo.drawPlayer.direction);
                num9 = (float) (2 * (int) drawinfo.drawPlayer.gravDir);
              }
              if (index1 == 3870)
              {
                Vector2 vector2_7 = (drawinfo.drawPlayer.itemRotation + 0.7853982f * (float) drawinfo.drawPlayer.direction).ToRotationVector2() * new Vector2((float) -drawinfo.drawPlayer.direction * 1.5f, drawinfo.drawPlayer.gravDir) * 3f;
                num8 = (float) (int) vector2_7.X;
                num9 = (float) (int) vector2_7.Y;
              }
              if (index1 == 3787)
                num9 = (float) (int) ((double) (8 * (int) drawinfo.drawPlayer.gravDir) * Math.Cos((double) num7));
              if (index1 == 3209)
              {
                Vector2 vector2_8 = (new Vector2(-8f, 0.0f) * drawinfo.drawPlayer.Directions).RotatedBy((double) drawinfo.drawPlayer.itemRotation);
                num8 = vector2_8.X;
                num9 = vector2_8.Y;
              }
              if ((double) drawinfo.drawPlayer.gravDir == -1.0)
              {
                if (drawinfo.drawPlayer.direction == -1)
                {
                  num7 += 1.57f;
                  origin5 = new Vector2((float) itemDrawFrame.Width, 0.0f);
                  num8 -= (float) itemDrawFrame.Width;
                }
                else
                {
                  num7 -= 1.57f;
                  origin5 = Vector2.Zero;
                }
              }
              else if (drawinfo.drawPlayer.direction == -1)
              {
                origin5 = new Vector2((float) itemDrawFrame.Width, (float) itemDrawFrame.Height);
                num8 -= (float) itemDrawFrame.Width;
              }
              DrawData drawData4 = new DrawData(texture, new Vector2((float) (int) ((double) drawinfo.ItemLocation.X - (double) Main.screenPosition.X + (double) origin5.X + (double) num8), (float) (int) ((double) drawinfo.ItemLocation.Y - (double) Main.screenPosition.Y + (double) num9)), new Rectangle?(itemDrawFrame), heldItem.GetAlpha(drawinfo.itemColor), num7, origin5, adjustedItemScale, drawinfo.itemEffect);
              drawinfo.DrawDataCache.Add(drawData4);
              if (index1 != 3870)
                break;
              drawData4 = new DrawData(TextureAssets.GlowMask[238].Value, new Vector2((float) (int) ((double) drawinfo.ItemLocation.X - (double) Main.screenPosition.X + (double) origin5.X + (double) num8), (float) (int) ((double) drawinfo.ItemLocation.Y - (double) Main.screenPosition.Y + (double) num9)), new Rectangle?(itemDrawFrame), new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) sbyte.MaxValue), num7, origin5, adjustedItemScale, drawinfo.itemEffect);
              drawinfo.DrawDataCache.Add(drawData4);
              break;
            }
            if (index1 == 5118)
            {
              float rotation2 = drawinfo.drawPlayer.itemRotation + 1.57f * (float) drawinfo.drawPlayer.direction;
              Vector2 vector2_9 = new Vector2((float) itemDrawFrame.Width * 0.5f, (float) itemDrawFrame.Height * 0.5f);
              Vector2 origin6 = new Vector2((float) itemDrawFrame.Width * 0.5f, (float) itemDrawFrame.Height);
              Vector2 vector2_10 = (new Vector2(10f, 4f) * drawinfo.drawPlayer.Directions).RotatedBy((double) drawinfo.drawPlayer.itemRotation);
              DrawData drawData5 = new DrawData(texture, new Vector2((float) (int) ((double) drawinfo.ItemLocation.X - (double) Main.screenPosition.X + (double) vector2_9.X + (double) vector2_10.X), (float) (int) ((double) drawinfo.ItemLocation.Y - (double) Main.screenPosition.Y + (double) vector2_9.Y + (double) vector2_10.Y)), new Rectangle?(itemDrawFrame), heldItem.GetAlpha(drawinfo.itemColor), rotation2, origin6, adjustedItemScale, drawinfo.itemEffect);
              drawinfo.DrawDataCache.Add(drawData5);
              break;
            }
            Vector2 vector2_11 = new Vector2((float) (itemDrawFrame.Width / 2), (float) (itemDrawFrame.Height / 2));
            Vector2 vector2_12 = Main.DrawPlayerItemPos(drawinfo.drawPlayer.gravDir, index1);
            int x = (int) vector2_12.X;
            vector2_11.Y = vector2_12.Y;
            Vector2 origin7 = new Vector2((float) -x, (float) (itemDrawFrame.Height / 2));
            if (drawinfo.drawPlayer.direction == -1)
              origin7 = new Vector2((float) (itemDrawFrame.Width + x), (float) (itemDrawFrame.Height / 2));
            DrawData drawData6 = new DrawData(texture, new Vector2((float) (int) ((double) drawinfo.ItemLocation.X - (double) Main.screenPosition.X + (double) vector2_11.X), (float) (int) ((double) drawinfo.ItemLocation.Y - (double) Main.screenPosition.Y + (double) vector2_11.Y)), new Rectangle?(itemDrawFrame), heldItem.GetAlpha(drawinfo.itemColor), drawinfo.drawPlayer.itemRotation, origin7, adjustedItemScale, drawinfo.itemEffect);
            drawinfo.DrawDataCache.Add(drawData6);
            if (heldItem.color != new Color())
            {
              drawData6 = new DrawData(texture, new Vector2((float) (int) ((double) drawinfo.ItemLocation.X - (double) Main.screenPosition.X + (double) vector2_11.X), (float) (int) ((double) drawinfo.ItemLocation.Y - (double) Main.screenPosition.Y + (double) vector2_11.Y)), new Rectangle?(itemDrawFrame), heldItem.GetColor(drawinfo.itemColor), drawinfo.drawPlayer.itemRotation, origin7, adjustedItemScale, drawinfo.itemEffect);
              drawinfo.DrawDataCache.Add(drawData6);
            }
            if (heldItem.glowMask != (short) -1)
            {
              drawData6 = new DrawData(TextureAssets.GlowMask[(int) heldItem.glowMask].Value, new Vector2((float) (int) ((double) drawinfo.ItemLocation.X - (double) Main.screenPosition.X + (double) vector2_11.X), (float) (int) ((double) drawinfo.ItemLocation.Y - (double) Main.screenPosition.Y + (double) vector2_11.Y)), new Rectangle?(itemDrawFrame), new Color(250, 250, 250, heldItem.alpha), drawinfo.drawPlayer.itemRotation, origin7, adjustedItemScale, drawinfo.itemEffect);
              drawinfo.DrawDataCache.Add(drawData6);
            }
            if (index1 != 3788)
              break;
            float num10 = (float) ((double) ((float) ((double) drawinfo.drawPlayer.miscCounter / 75.0 * 6.2831854820251465)).ToRotationVector2().X * 1.0 + 0.0);
            Color color3 = new Color(80, 40, 252, 0) * (float) ((double) num10 / 2.0 * 0.30000001192092896 + 0.85000002384185791) * 0.5f;
            for (float num11 = 0.0f; (double) num11 < 4.0; ++num11)
            {
              drawData6 = new DrawData(TextureAssets.GlowMask[220].Value, new Vector2((float) (int) ((double) drawinfo.ItemLocation.X - (double) Main.screenPosition.X + (double) vector2_11.X), (float) (int) ((double) drawinfo.ItemLocation.Y - (double) Main.screenPosition.Y + (double) vector2_11.Y)) + (num11 * 1.57079637f + drawinfo.drawPlayer.itemRotation).ToRotationVector2() * num10, new Rectangle?(), color3, drawinfo.drawPlayer.itemRotation, origin7, adjustedItemScale, drawinfo.itemEffect);
              drawinfo.DrawDataCache.Add(drawData6);
            }
            break;
          }
          if ((double) drawinfo.drawPlayer.gravDir == -1.0)
          {
            DrawData drawData7 = new DrawData(texture, position, new Rectangle?(itemDrawFrame), heldItem.GetAlpha(drawinfo.itemColor), rotation1, origin1, adjustedItemScale, drawinfo.itemEffect);
            drawinfo.DrawDataCache.Add(drawData7);
            if (heldItem.color != new Color())
            {
              drawData7 = new DrawData(texture, position, new Rectangle?(itemDrawFrame), heldItem.GetColor(drawinfo.itemColor), rotation1, origin1, adjustedItemScale, drawinfo.itemEffect);
              drawinfo.DrawDataCache.Add(drawData7);
            }
            if (heldItem.glowMask == (short) -1)
              break;
            drawData7 = new DrawData(TextureAssets.GlowMask[(int) heldItem.glowMask].Value, position, new Rectangle?(itemDrawFrame), new Color(250, 250, 250, heldItem.alpha), rotation1, origin1, adjustedItemScale, drawinfo.itemEffect);
            drawinfo.DrawDataCache.Add(drawData7);
            break;
          }
          DrawData drawData8 = new DrawData(texture, position, new Rectangle?(itemDrawFrame), heldItem.GetAlpha(drawinfo.itemColor), rotation1, origin1, adjustedItemScale, drawinfo.itemEffect);
          drawinfo.DrawDataCache.Add(drawData8);
          if (heldItem.color != new Color())
          {
            drawData8 = new DrawData(texture, position, new Rectangle?(itemDrawFrame), heldItem.GetColor(drawinfo.itemColor), rotation1, origin1, adjustedItemScale, drawinfo.itemEffect);
            drawinfo.DrawDataCache.Add(drawData8);
          }
          if (heldItem.glowMask != (short) -1)
          {
            drawData8 = new DrawData(TextureAssets.GlowMask[(int) heldItem.glowMask].Value, position, new Rectangle?(itemDrawFrame), color1, rotation1, origin1, adjustedItemScale, drawinfo.itemEffect);
            drawinfo.DrawDataCache.Add(drawData8);
          }
          if (!heldItem.flame)
            break;
          if ((double) drawinfo.shadow != 0.0)
            break;
          try
          {
            Main.instance.LoadItemFlames(index1);
            if (!TextureAssets.ItemFlame[index1].IsLoaded)
              break;
            Color color4 = new Color(100, 100, 100, 0);
            int num12 = 7;
            float num13 = 1f;
            float num14 = 0.0f;
            switch (index1)
            {
              case 3045:
                color4 = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
                break;
              case 4952:
                num12 = 3;
                num13 = 0.6f;
                color4 = new Color(50, 50, 50, 0);
                break;
              case 5293:
                color4 = new Color(50, 50, 100, 20);
                break;
              case 5322:
                color4 = new Color(100, 100, 100, 150);
                num14 = (float) (-2 * drawinfo.drawPlayer.direction);
                break;
              case 5353:
                color4 = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 200);
                break;
            }
            for (int index2 = 0; index2 < num12; ++index2)
            {
              float num15 = drawinfo.drawPlayer.itemFlamePos[index2].X * adjustedItemScale * num13;
              float num16 = drawinfo.drawPlayer.itemFlamePos[index2].Y * adjustedItemScale * num13;
              DrawData drawData9 = new DrawData(TextureAssets.ItemFlame[index1].Value, new Vector2((float) (int) ((double) position.X + (double) num15 + (double) num14), (float) (int) ((double) position.Y + (double) num16)), new Rectangle?(itemDrawFrame), color4, rotation1, origin1, adjustedItemScale, drawinfo.itemEffect);
              drawinfo.DrawDataCache.Add(drawData9);
            }
            break;
          }
          catch
          {
            break;
          }
      }
    }

    public static void DrawPlayer_28_ArmOverItem(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.usesCompositeTorso)
        PlayerDrawLayers.DrawPlayer_28_ArmOverItemComposite(ref drawinfo);
      else if (drawinfo.drawPlayer.body > 0 && drawinfo.drawPlayer.body < ArmorIDs.Body.Count)
      {
        Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
        int num1 = drawinfo.armorAdjust;
        bodyFrame.X += num1;
        bodyFrame.Width -= num1;
        if (drawinfo.drawPlayer.direction == -1)
          num1 = 0;
        if (drawinfo.drawPlayer.invis && (drawinfo.drawPlayer.body == 21 || drawinfo.drawPlayer.body == 22))
          return;
        DrawData drawData1;
        if (drawinfo.missingHand && !drawinfo.drawPlayer.invis)
        {
          int body = drawinfo.drawPlayer.body;
          DrawData drawData2;
          if (drawinfo.missingArm)
          {
            drawData2 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
            drawData2.shader = drawinfo.skinDyePacked;
            DrawData drawData3 = drawData2;
            drawinfo.DrawDataCache.Add(drawData3);
          }
          drawData2 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 9].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
          drawData2.shader = drawinfo.skinDyePacked;
          drawData1 = drawData2;
          drawinfo.DrawDataCache.Add(drawData1);
        }
        drawData1 = new DrawData(TextureAssets.ArmorArm[drawinfo.drawPlayer.body].Value, new Vector2((float) ((int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)) + num1), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
        drawData1.shader = drawinfo.cBody;
        drawinfo.DrawDataCache.Add(drawData1);
        if (drawinfo.armGlowMask != -1)
        {
          drawData1 = new DrawData(TextureAssets.GlowMask[drawinfo.armGlowMask].Value, new Vector2((float) ((int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)) + num1), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(bodyFrame), drawinfo.armGlowColor, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
          drawData1.shader = drawinfo.cBody;
          drawinfo.DrawDataCache.Add(drawData1);
        }
        if (drawinfo.drawPlayer.body != 205)
          return;
        Color color = new Color(100, 100, 100, 0);
        ulong seed = (ulong) (drawinfo.drawPlayer.miscCounter / 4);
        int num2 = 4;
        for (int index = 0; index < num2; ++index)
        {
          float num3 = (float) Utils.RandomInt(ref seed, -10, 11) * 0.2f;
          float num4 = (float) Utils.RandomInt(ref seed, -10, 1) * 0.15f;
          drawData1 = new DrawData(TextureAssets.GlowMask[240].Value, new Vector2((float) ((int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)) + num1), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2) + num3, (float) (drawinfo.drawPlayer.bodyFrame.Height / 2) + num4), new Rectangle?(bodyFrame), color, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
          drawData1.shader = drawinfo.cBody;
          drawinfo.DrawDataCache.Add(drawData1);
        }
      }
      else
      {
        if (drawinfo.drawPlayer.invis)
          return;
        DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect)
        {
          shader = drawinfo.skinDyePacked
        };
        drawinfo.DrawDataCache.Add(drawData);
        drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 8].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorUnderShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
        drawinfo.DrawDataCache.Add(drawData);
        drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 13].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect);
        drawinfo.DrawDataCache.Add(drawData);
      }
    }

    public static void DrawPlayer_28_ArmOverItemComposite(ref PlayerDrawSet drawinfo)
    {
      Vector2 vector2_1 = new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2));
      Vector2 vector2_2 = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
      vector2_2.Y -= 2f;
      Vector2 vector2_3 = vector2_1 + vector2_2 * (float) -drawinfo.playerEffect.HasFlag((Enum) SpriteEffects.FlipVertically).ToDirectionInt();
      float bodyRotation = drawinfo.drawPlayer.bodyRotation;
      float rotation = drawinfo.drawPlayer.bodyRotation + drawinfo.compositeFrontArmRotation;
      Vector2 bodyVect = drawinfo.bodyVect;
      Vector2 compositeOffsetFrontArm = PlayerDrawLayers.GetCompositeOffset_FrontArm(ref drawinfo);
      Vector2 origin = bodyVect + compositeOffsetFrontArm;
      Vector2 position1 = vector2_3 + compositeOffsetFrontArm;
      Vector2 position2 = position1 + drawinfo.frontShoulderOffset;
      if (drawinfo.compFrontArmFrame.X / drawinfo.compFrontArmFrame.Width >= 7)
        position1 += new Vector2(drawinfo.playerEffect.HasFlag((Enum) SpriteEffects.FlipHorizontally) ? -1f : 1f, drawinfo.playerEffect.HasFlag((Enum) SpriteEffects.FlipVertically) ? -1f : 1f);
      int num1 = drawinfo.drawPlayer.invis ? 1 : 0;
      int num2 = drawinfo.drawPlayer.body <= 0 ? 0 : (drawinfo.drawPlayer.body < ArmorIDs.Body.Count ? 1 : 0);
      int num3 = drawinfo.compShoulderOverFrontArm ? 1 : 0;
      int num4 = drawinfo.compShoulderOverFrontArm ? 0 : 1;
      int num5 = drawinfo.compShoulderOverFrontArm ? 0 : 1;
      bool flag = !drawinfo.hidesTopSkin;
      DrawData drawData1;
      if (num2 != 0)
      {
        if (!drawinfo.drawPlayer.invis || PlayerDrawLayers.IsArmorDrawnWhenInvisible(drawinfo.drawPlayer.body))
        {
          Texture2D texture = TextureAssets.ArmorBodyComposite[drawinfo.drawPlayer.body].Value;
          for (int index = 0; index < 2; ++index)
          {
            if (((drawinfo.drawPlayer.invis ? 0 : (index == num5 ? 1 : 0)) & (flag ? 1 : 0)) != 0)
            {
              if (drawinfo.missingArm)
              {
                List<DrawData> drawDataCache = drawinfo.DrawDataCache;
                drawData1 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, position1, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorBodySkin, rotation, origin, 1f, drawinfo.playerEffect);
                drawData1.shader = drawinfo.skinDyePacked;
                DrawData drawData2 = drawData1;
                drawDataCache.Add(drawData2);
              }
              if (drawinfo.missingHand)
              {
                List<DrawData> drawDataCache = drawinfo.DrawDataCache;
                drawData1 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 9].Value, position1, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorBodySkin, rotation, origin, 1f, drawinfo.playerEffect);
                drawData1.shader = drawinfo.skinDyePacked;
                DrawData drawData3 = drawData1;
                drawDataCache.Add(drawData3);
              }
            }
            if (index == num3 && !drawinfo.hideCompositeShoulders)
            {
              ref PlayerDrawSet local = ref drawinfo;
              drawData1 = new DrawData(texture, position2, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorArmorBody, bodyRotation, origin, 1f, drawinfo.playerEffect);
              drawData1.shader = drawinfo.cBody;
              DrawData data = drawData1;
              PlayerDrawLayers.DrawCompositeArmorPiece(ref local, CompositePlayerDrawContext.FrontShoulder, data);
            }
            if (index == num4)
            {
              ref PlayerDrawSet local = ref drawinfo;
              drawData1 = new DrawData(texture, position1, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorArmorBody, rotation, origin, 1f, drawinfo.playerEffect);
              drawData1.shader = drawinfo.cBody;
              DrawData data = drawData1;
              PlayerDrawLayers.DrawCompositeArmorPiece(ref local, CompositePlayerDrawContext.FrontArm, data);
            }
          }
        }
      }
      else if (!drawinfo.drawPlayer.invis)
      {
        for (int index = 0; index < 2; ++index)
        {
          if (index == num3)
          {
            if (flag)
            {
              List<DrawData> drawDataCache = drawinfo.DrawDataCache;
              drawData1 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, position2, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorBodySkin, bodyRotation, origin, 1f, drawinfo.playerEffect);
              drawData1.shader = drawinfo.skinDyePacked;
              DrawData drawData4 = drawData1;
              drawDataCache.Add(drawData4);
            }
            drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 8].Value, position2, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorUnderShirt, bodyRotation, origin, 1f, drawinfo.playerEffect));
            drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 13].Value, position2, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorShirt, bodyRotation, origin, 1f, drawinfo.playerEffect));
            drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, position2, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorShirt, bodyRotation, origin, 1f, drawinfo.playerEffect));
            if (drawinfo.drawPlayer.head == 269)
            {
              Vector2 position3 = drawinfo.helmetOffset + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect;
              DrawData drawData5 = new DrawData(TextureAssets.Extra[214].Value, position3, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
              drawData5.shader = drawinfo.cHead;
              drawinfo.DrawDataCache.Add(drawData5);
              drawData5 = new DrawData(TextureAssets.GlowMask[308].Value, position3, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
              drawData5.shader = drawinfo.cHead;
              drawinfo.DrawDataCache.Add(drawData5);
            }
          }
          if (index == num4)
          {
            if (flag)
            {
              List<DrawData> drawDataCache = drawinfo.DrawDataCache;
              drawData1 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, position1, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorBodySkin, rotation, origin, 1f, drawinfo.playerEffect);
              drawData1.shader = drawinfo.skinDyePacked;
              DrawData drawData6 = drawData1;
              drawDataCache.Add(drawData6);
            }
            drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 8].Value, position1, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorUnderShirt, rotation, origin, 1f, drawinfo.playerEffect));
            drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 13].Value, position1, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorShirt, rotation, origin, 1f, drawinfo.playerEffect));
            drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, position1, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorShirt, rotation, origin, 1f, drawinfo.playerEffect));
          }
        }
      }
      if (drawinfo.drawPlayer.handon <= (sbyte) 0 || (int) drawinfo.drawPlayer.handon >= ArmorIDs.HandOn.Count)
        return;
      Texture2D texture1 = TextureAssets.AccHandsOnComposite[(int) drawinfo.drawPlayer.handon].Value;
      ref PlayerDrawSet local1 = ref drawinfo;
      drawData1 = new DrawData(texture1, position1, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorArmorBody, rotation, origin, 1f, drawinfo.playerEffect);
      drawData1.shader = drawinfo.cHandOn;
      DrawData data1 = drawData1;
      PlayerDrawLayers.DrawCompositeArmorPiece(ref local1, CompositePlayerDrawContext.FrontArmAccessory, data1);
    }

    public static void DrawPlayer_29_OnhandAcc(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.usesCompositeFrontHandAcc || drawinfo.drawPlayer.handon <= (sbyte) 0 || (int) drawinfo.drawPlayer.handon >= ArmorIDs.HandOn.Count)
        return;
      drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.AccHandsOn[(int) drawinfo.drawPlayer.handon].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect)
      {
        shader = drawinfo.cHandOn
      });
    }

    public static void DrawPlayer_30_BladedGlove(ref PlayerDrawSet drawinfo)
    {
      Item heldItem = drawinfo.heldItem;
      if (heldItem.type <= -1 || !Item.claw[heldItem.type] || (double) drawinfo.shadow != 0.0)
        return;
      Main.instance.LoadItem(heldItem.type);
      Asset<Texture2D> asset = TextureAssets.Item[heldItem.type];
      if (drawinfo.drawPlayer.frozen || drawinfo.drawPlayer.itemAnimation <= 0 && (heldItem.holdStyle == 0 || drawinfo.drawPlayer.pulley) || heldItem.type <= 0 || drawinfo.drawPlayer.dead || heldItem.noUseGraphic || drawinfo.drawPlayer.wet && heldItem.noWet)
        return;
      if ((double) drawinfo.drawPlayer.gravDir == -1.0)
      {
        DrawData drawData = new DrawData(asset.Value, new Vector2((float) (int) ((double) drawinfo.ItemLocation.X - (double) Main.screenPosition.X), (float) (int) ((double) drawinfo.ItemLocation.Y - (double) Main.screenPosition.Y)), new Rectangle?(new Rectangle(0, 0, asset.Width(), asset.Height())), heldItem.GetAlpha(drawinfo.itemColor), drawinfo.drawPlayer.itemRotation, new Vector2((float) ((double) asset.Width() * 0.5 - (double) asset.Width() * 0.5 * (double) drawinfo.drawPlayer.direction), 0.0f), drawinfo.drawPlayer.GetAdjustedItemScale(heldItem), drawinfo.itemEffect);
        drawinfo.DrawDataCache.Add(drawData);
      }
      else
      {
        DrawData drawData = new DrawData(asset.Value, new Vector2((float) (int) ((double) drawinfo.ItemLocation.X - (double) Main.screenPosition.X), (float) (int) ((double) drawinfo.ItemLocation.Y - (double) Main.screenPosition.Y)), new Rectangle?(new Rectangle(0, 0, asset.Width(), asset.Height())), heldItem.GetAlpha(drawinfo.itemColor), drawinfo.drawPlayer.itemRotation, new Vector2((float) ((double) asset.Width() * 0.5 - (double) asset.Width() * 0.5 * (double) drawinfo.drawPlayer.direction), (float) asset.Height()), drawinfo.drawPlayer.GetAdjustedItemScale(heldItem), drawinfo.itemEffect);
        drawinfo.DrawDataCache.Add(drawData);
      }
    }

    public static void DrawPlayer_31_ProjectileOverArm(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.heldProj < 0 || (double) drawinfo.shadow != 0.0 || !drawinfo.heldProjOverHand)
        return;
      drawinfo.projectileDrawPosition = drawinfo.DrawDataCache.Count;
    }

    public static void DrawPlayer_32_FrontAcc(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.front <= (sbyte) 0 || (int) drawinfo.drawPlayer.front >= ArmorIDs.Front.Count || drawinfo.drawPlayer.mount.Active)
        return;
      Vector2 zero = Vector2.Zero;
      drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.AccFront[(int) drawinfo.drawPlayer.front].Value, zero + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect)
      {
        shader = drawinfo.cFront
      });
    }

    public static void DrawPlayer_32_FrontAcc_FrontPart(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.front <= (sbyte) 0 || (int) drawinfo.drawPlayer.front >= ArmorIDs.Front.Count)
        return;
      Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
      int num1 = bodyFrame.Width / 2;
      bodyFrame.Width -= num1;
      Vector2 bodyVect = drawinfo.bodyVect;
      if (drawinfo.playerEffect.HasFlag((Enum) SpriteEffects.FlipHorizontally))
        bodyVect.X -= (float) num1;
      Vector2 position = Vector2.Zero + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2));
      DrawData drawData = new DrawData(TextureAssets.AccFront[(int) drawinfo.drawPlayer.front].Value, position, new Rectangle?(bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect);
      drawData.shader = drawinfo.cFront;
      drawinfo.DrawDataCache.Add(drawData);
      if (drawinfo.drawPlayer.front != (sbyte) 12)
        return;
      Rectangle rectangle1 = bodyFrame;
      Rectangle rectangle2 = rectangle1 with
      {
        Width = 2
      };
      int num2 = 0;
      int num3 = rectangle1.Width / 2;
      int num4 = 2;
      if (drawinfo.playerEffect.HasFlag((Enum) SpriteEffects.FlipHorizontally))
      {
        num2 = rectangle1.Width - 2;
        num4 = -2;
      }
      for (int index = 0; index < num3; ++index)
      {
        rectangle2.X = rectangle1.X + 2 * index;
        Color color = drawinfo.drawPlayer.GetImmuneAlpha(LiquidRenderer.GetShimmerGlitterColor(true, (float) index / 16f, 0.0f), drawinfo.shadow) * ((float) drawinfo.colorArmorBody.A / (float) byte.MaxValue);
        drawData = new DrawData(TextureAssets.GlowMask[331].Value, position + new Vector2((float) (num2 + index * num4), 0.0f), new Rectangle?(rectangle2), color, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cFront;
        drawinfo.DrawDataCache.Add(drawData);
      }
    }

    public static void DrawPlayer_32_FrontAcc_BackPart(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.front <= (sbyte) 0 || (int) drawinfo.drawPlayer.front >= ArmorIDs.Front.Count)
        return;
      Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
      int num1 = bodyFrame.Width / 2;
      bodyFrame.Width -= num1;
      bodyFrame.X += num1;
      Vector2 bodyVect = drawinfo.bodyVect;
      if (!drawinfo.playerEffect.HasFlag((Enum) SpriteEffects.FlipHorizontally))
        bodyVect.X -= (float) num1;
      Vector2 position = Vector2.Zero + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2));
      DrawData drawData = new DrawData(TextureAssets.AccFront[(int) drawinfo.drawPlayer.front].Value, position, new Rectangle?(bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect);
      drawData.shader = drawinfo.cFront;
      drawinfo.DrawDataCache.Add(drawData);
      if (drawinfo.drawPlayer.front != (sbyte) 12)
        return;
      Rectangle rectangle1 = bodyFrame;
      Rectangle rectangle2 = rectangle1 with
      {
        Width = 2
      };
      int num2 = 0;
      int num3 = rectangle1.Width / 2;
      int num4 = 2;
      if (drawinfo.playerEffect.HasFlag((Enum) SpriteEffects.FlipHorizontally))
      {
        num2 = rectangle1.Width - 2;
        num4 = -2;
      }
      for (int index = 0; index < num3; ++index)
      {
        rectangle2.X = rectangle1.X + 2 * index;
        Color color = drawinfo.drawPlayer.GetImmuneAlpha(LiquidRenderer.GetShimmerGlitterColor(true, (float) index / 16f, 0.0f), drawinfo.shadow) * ((float) drawinfo.colorArmorBody.A / (float) byte.MaxValue);
        drawData = new DrawData(TextureAssets.GlowMask[331].Value, position + new Vector2((float) (num2 + index * num4), 0.0f), new Rectangle?(rectangle2), color, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect);
        drawData.shader = drawinfo.cFront;
        drawinfo.DrawDataCache.Add(drawData);
      }
    }

    public static void DrawPlayer_33_FrozenOrWebbedDebuff(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.drawPlayer.shimmering)
        return;
      if (drawinfo.drawPlayer.frozen && (double) drawinfo.shadow == 0.0)
      {
        Color colorArmorBody = drawinfo.colorArmorBody;
        colorArmorBody.R = (byte) ((double) colorArmorBody.R * 0.55);
        colorArmorBody.G = (byte) ((double) colorArmorBody.G * 0.55);
        colorArmorBody.B = (byte) ((double) colorArmorBody.B * 0.55);
        colorArmorBody.A = (byte) ((double) colorArmorBody.A * 0.55);
        DrawData drawData = new DrawData(TextureAssets.Frozen.Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(new Rectangle(0, 0, TextureAssets.Frozen.Width(), TextureAssets.Frozen.Height())), colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float) (TextureAssets.Frozen.Width() / 2), (float) (TextureAssets.Frozen.Height() / 2)), 1f, drawinfo.playerEffect);
        drawinfo.DrawDataCache.Add(drawData);
      }
      else
      {
        if (!drawinfo.drawPlayer.webbed || (double) drawinfo.shadow != 0.0 || (double) drawinfo.drawPlayer.velocity.Y != 0.0)
          return;
        Color color = drawinfo.colorArmorBody * 0.75f;
        Texture2D texture2D = TextureAssets.Extra[31].Value;
        int num = drawinfo.drawPlayer.height / 2;
        DrawData drawData = new DrawData(texture2D, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0 + (double) num)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(), color, drawinfo.drawPlayer.bodyRotation, texture2D.Size() / 2f, 1f, drawinfo.playerEffect);
        drawinfo.DrawDataCache.Add(drawData);
      }
    }

    public static void DrawPlayer_34_ElectrifiedDebuffFront(ref PlayerDrawSet drawinfo)
    {
      if (!drawinfo.drawPlayer.electrified || (double) drawinfo.shadow != 0.0)
        return;
      Texture2D texture = TextureAssets.GlowMask[25].Value;
      int num1 = drawinfo.drawPlayer.miscCounter / 5;
      for (int index = 0; index < 2; ++index)
      {
        int num2 = num1 % 7;
        if (num2 > 1 && num2 < 5)
        {
          DrawData drawData = new DrawData(texture, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(new Rectangle(0, num2 * texture.Height / 7, texture.Width, texture.Height / 7)), drawinfo.colorElectricity, drawinfo.drawPlayer.bodyRotation, new Vector2((float) (texture.Width / 2), (float) (texture.Height / 14)), 1f, drawinfo.playerEffect);
          drawinfo.DrawDataCache.Add(drawData);
        }
        num1 = num2 + 3;
      }
    }

    public static void DrawPlayer_35_IceBarrier(ref PlayerDrawSet drawinfo)
    {
      if (!drawinfo.drawPlayer.iceBarrier || (double) drawinfo.shadow != 0.0)
        return;
      int height = TextureAssets.IceBarrier.Height() / 12;
      Color white = Color.White;
      DrawData drawData = new DrawData(TextureAssets.IceBarrier.Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.drawPlayer.bodyFrame.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.drawPlayer.bodyFrame.Height + 4.0)) + drawinfo.drawPlayer.bodyPosition + new Vector2((float) (drawinfo.drawPlayer.bodyFrame.Width / 2), (float) (drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(new Rectangle(0, height * (int) drawinfo.drawPlayer.iceBarrierFrame, TextureAssets.IceBarrier.Width(), height)), white, 0.0f, new Vector2((float) (TextureAssets.Frozen.Width() / 2), (float) (TextureAssets.Frozen.Height() / 2)), 1f, drawinfo.playerEffect);
      drawinfo.DrawDataCache.Add(drawData);
    }

    public static void DrawPlayer_36_CTG(ref PlayerDrawSet drawinfo)
    {
      if ((double) drawinfo.shadow != 0.0 || (byte) drawinfo.drawPlayer.ownedLargeGems <= (byte) 0)
        return;
      bool flag = false;
      BitsByte ownedLargeGems = drawinfo.drawPlayer.ownedLargeGems;
      float num1 = 0.0f;
      for (int key = 0; key < 7; ++key)
      {
        if (ownedLargeGems[key])
          ++num1;
      }
      float num2 = (float) (1.0 - (double) num1 * 0.059999998658895493);
      float num3 = (float) (((double) num1 - 1.0) * 4.0);
      switch (num1)
      {
        case 2f:
          num3 += 10f;
          break;
        case 3f:
          num3 += 8f;
          break;
        case 4f:
          num3 += 6f;
          break;
        case 5f:
          num3 += 6f;
          break;
        case 6f:
          num3 += 2f;
          break;
        case 7f:
          num3 += 0.0f;
          break;
      }
      float num4 = (float) ((double) drawinfo.drawPlayer.miscCounter / 300.0 * 6.2831854820251465);
      if ((double) num1 <= 0.0)
        return;
      float num5 = 6.28318548f / num1;
      float num6 = 0.0f;
      Vector2 vector2 = new Vector2(1.3f, 0.65f);
      if (!flag)
        vector2 = Vector2.One;
      List<DrawData> collection = new List<DrawData>();
      for (int key = 0; key < 7; ++key)
      {
        if (!ownedLargeGems[key])
        {
          ++num6;
        }
        else
        {
          Vector2 rotationVector2 = (num4 + num5 * ((float) key - num6)).ToRotationVector2();
          float num7 = num2;
          if (flag)
            num7 = MathHelper.Lerp(num2 * 0.7f, 1f, (float) ((double) rotationVector2.Y / 2.0 + 0.5));
          Texture2D texture2D = TextureAssets.Gem[key].Value;
          DrawData drawData = new DrawData(texture2D, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - 80.0)) + rotationVector2 * vector2 * num3, new Rectangle?(), new Color(250, 250, 250, (int) Main.mouseTextColor / 2), 0.0f, texture2D.Size() / 2f, (float) ((double) Main.mouseTextColor / 1000.0 + 0.800000011920929) * num7, SpriteEffects.None);
          collection.Add(drawData);
        }
      }
      if (flag)
        collection.Sort(new Comparison<DrawData>(DelegateMethods.CompareDrawSorterByYScale));
      drawinfo.DrawDataCache.AddRange((IEnumerable<DrawData>) collection);
    }

    public static void DrawPlayer_37_BeetleBuff(ref PlayerDrawSet drawinfo)
    {
      if (!drawinfo.drawPlayer.beetleOffense && !drawinfo.drawPlayer.beetleDefense || (double) drawinfo.shadow != 0.0)
        return;
      for (int index1 = 0; index1 < drawinfo.drawPlayer.beetleOrbs; ++index1)
      {
        DrawData drawData;
        for (int index2 = 0; index2 < 5; ++index2)
        {
          Color colorArmorBody = drawinfo.colorArmorBody;
          float num = 0.5f - (float) index2 * 0.1f;
          colorArmorBody.R = (byte) ((double) colorArmorBody.R * (double) num);
          colorArmorBody.G = (byte) ((double) colorArmorBody.G * (double) num);
          colorArmorBody.B = (byte) ((double) colorArmorBody.B * (double) num);
          colorArmorBody.A = (byte) ((double) colorArmorBody.A * (double) num);
          Vector2 vector2 = -drawinfo.drawPlayer.beetleVel[index1] * (float) index2;
          drawData = new DrawData(TextureAssets.Beetle.Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) (drawinfo.drawPlayer.height / 2))) + drawinfo.drawPlayer.beetlePos[index1] + vector2, new Rectangle?(new Rectangle(0, TextureAssets.Beetle.Height() / 3 * drawinfo.drawPlayer.beetleFrame + 1, TextureAssets.Beetle.Width(), TextureAssets.Beetle.Height() / 3 - 2)), colorArmorBody, 0.0f, new Vector2((float) (TextureAssets.Beetle.Width() / 2), (float) (TextureAssets.Beetle.Height() / 6)), 1f, drawinfo.playerEffect);
          drawinfo.DrawDataCache.Add(drawData);
        }
        drawData = new DrawData(TextureAssets.Beetle.Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X + (double) (drawinfo.drawPlayer.width / 2)), (float) (int) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) (drawinfo.drawPlayer.height / 2))) + drawinfo.drawPlayer.beetlePos[index1], new Rectangle?(new Rectangle(0, TextureAssets.Beetle.Height() / 3 * drawinfo.drawPlayer.beetleFrame + 1, TextureAssets.Beetle.Width(), TextureAssets.Beetle.Height() / 3 - 2)), drawinfo.colorArmorBody, 0.0f, new Vector2((float) (TextureAssets.Beetle.Width() / 2), (float) (TextureAssets.Beetle.Height() / 6)), 1f, drawinfo.playerEffect);
        drawinfo.DrawDataCache.Add(drawData);
      }
    }

    public static void DrawPlayer_38_EyebrellaCloud(ref PlayerDrawSet drawinfo)
    {
      if (!drawinfo.drawPlayer.eyebrellaCloud || (double) drawinfo.shadow != 0.0)
        return;
      Texture2D texture2D = TextureAssets.Projectile[238].Value;
      int frameY = drawinfo.drawPlayer.miscCounter % 18 / 6;
      Rectangle rectangle = texture2D.Frame(verticalFrames: 6, frameY: frameY);
      Vector2 origin = new Vector2((float) (rectangle.Width / 2), (float) (rectangle.Height / 2));
      Vector2 vector2_1 = new Vector2(0.0f, -70f);
      Vector2 position = drawinfo.drawPlayer.MountedCenter - new Vector2(0.0f, (float) drawinfo.drawPlayer.height * 0.5f) + vector2_1 - Main.screenPosition;
      Color color1 = Lighting.GetColor((drawinfo.drawPlayer.Top + new Vector2(0.0f, -30f)).ToTileCoordinates());
      int num1 = 170;
      int num2;
      int b = num2 = num1;
      int g = num2;
      int r = num2;
      if ((int) color1.R < num1)
        r = (int) color1.R;
      if ((int) color1.G < num1)
        g = (int) color1.G;
      if ((int) color1.B < num1)
        b = (int) color1.B;
      Color color2 = new Color(r, g, b, 100);
      float num3 = (float) (drawinfo.drawPlayer.miscCounter % 50) / 50f;
      float num4 = 3f;
      DrawData drawData;
      for (int index = 0; index < 2; ++index)
      {
        Vector2 vector2_2 = new Vector2(index == 0 ? -num4 : num4, 0.0f).RotatedBy((double) num3 * 6.2831854820251465 * (index == 0 ? 1.0 : -1.0));
        drawData = new DrawData(texture2D, position + vector2_2, new Rectangle?(rectangle), color2 * 0.65f, 0.0f, origin, 1f, (double) drawinfo.drawPlayer.gravDir == -1.0 ? SpriteEffects.FlipVertically : SpriteEffects.None);
        drawData.shader = drawinfo.cHead;
        drawData.ignorePlayerRotation = true;
        drawinfo.DrawDataCache.Add(drawData);
      }
      drawData = new DrawData(texture2D, position, new Rectangle?(rectangle), color2, 0.0f, origin, 1f, (double) drawinfo.drawPlayer.gravDir == -1.0 ? SpriteEffects.FlipVertically : SpriteEffects.None);
      drawData.shader = drawinfo.cHead;
      drawData.ignorePlayerRotation = true;
      drawinfo.DrawDataCache.Add(drawData);
    }

    private static Vector2 GetCompositeOffset_BackArm(ref PlayerDrawSet drawinfo) => new Vector2((float) (6 * (drawinfo.playerEffect.HasFlag((Enum) SpriteEffects.FlipHorizontally) ? -1 : 1)), (float) (2 * (drawinfo.playerEffect.HasFlag((Enum) SpriteEffects.FlipVertically) ? -1 : 1)));

    private static Vector2 GetCompositeOffset_FrontArm(ref PlayerDrawSet drawinfo) => new Vector2((float) (-5 * (drawinfo.playerEffect.HasFlag((Enum) SpriteEffects.FlipHorizontally) ? -1 : 1)), 0.0f);

    public static void DrawPlayer_TransformDrawData(ref PlayerDrawSet drawinfo)
    {
      double rotation = (double) drawinfo.rotation;
      Vector2 vector2_1 = drawinfo.Position - Main.screenPosition + drawinfo.rotationOrigin;
      Vector2 vector2_2 = drawinfo.drawPlayer.position + drawinfo.rotationOrigin;
      Matrix rotationZ = Matrix.CreateRotationZ(drawinfo.rotation);
      for (int index = 0; index < drawinfo.DustCache.Count; ++index)
      {
        Vector2 vector2_3 = Vector2.Transform(Main.dust[drawinfo.DustCache[index]].position - vector2_2, rotationZ);
        Main.dust[drawinfo.DustCache[index]].position = vector2_3 + vector2_2;
      }
      for (int index = 0; index < drawinfo.GoreCache.Count; ++index)
      {
        Vector2 vector2_4 = Vector2.Transform(Main.gore[drawinfo.GoreCache[index]].position - vector2_2, rotationZ);
        Main.gore[drawinfo.GoreCache[index]].position = vector2_4 + vector2_2;
      }
      for (int index = 0; index < drawinfo.DrawDataCache.Count; ++index)
      {
        DrawData drawData = drawinfo.DrawDataCache[index];
        if (!drawData.ignorePlayerRotation)
        {
          Vector2 vector2_5 = Vector2.Transform(drawData.position - vector2_1, rotationZ);
          drawData.position = vector2_5 + vector2_1;
          drawData.rotation += drawinfo.rotation;
          drawinfo.DrawDataCache[index] = drawData;
        }
      }
    }

    public static void DrawPlayer_ScaleDrawData(ref PlayerDrawSet drawinfo, float scale)
    {
      if ((double) scale == 1.0)
        return;
      Vector2 vector2_1 = drawinfo.Position + drawinfo.drawPlayer.Size * new Vector2(0.5f, 1f) - Main.screenPosition;
      for (int index = 0; index < drawinfo.DrawDataCache.Count; ++index)
      {
        DrawData drawData = drawinfo.DrawDataCache[index];
        Vector2 vector2_2 = drawData.position - vector2_1;
        drawData.position = vector2_1 + vector2_2 * scale;
        drawData.scale *= scale;
        drawinfo.DrawDataCache[index] = drawData;
      }
    }

    public static void DrawPlayer_AddSelectionGlow(ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.selectionGlowColor == Color.Transparent)
        return;
      Color selectionGlowColor = drawinfo.selectionGlowColor;
      List<DrawData> drawDataList = new List<DrawData>();
      drawDataList.AddRange((IEnumerable<DrawData>) PlayerDrawLayers.GetFlatColoredCloneData(ref drawinfo, new Vector2(0.0f, -2f), selectionGlowColor));
      drawDataList.AddRange((IEnumerable<DrawData>) PlayerDrawLayers.GetFlatColoredCloneData(ref drawinfo, new Vector2(0.0f, 2f), selectionGlowColor));
      drawDataList.AddRange((IEnumerable<DrawData>) PlayerDrawLayers.GetFlatColoredCloneData(ref drawinfo, new Vector2(2f, 0.0f), selectionGlowColor));
      drawDataList.AddRange((IEnumerable<DrawData>) PlayerDrawLayers.GetFlatColoredCloneData(ref drawinfo, new Vector2(-2f, 0.0f), selectionGlowColor));
      drawDataList.AddRange((IEnumerable<DrawData>) drawinfo.DrawDataCache);
      drawinfo.DrawDataCache = drawDataList;
    }

    public static void DrawPlayer_MakeIntoFirstFractalAfterImage(ref PlayerDrawSet drawinfo)
    {
      if (!drawinfo.drawPlayer.isFirstFractalAfterImage)
      {
        int num = drawinfo.drawPlayer.HeldItem.type != 4722 ? 0 : (drawinfo.drawPlayer.itemAnimation > 0 ? 1 : 0);
      }
      else
      {
        for (int index = 0; index < drawinfo.DrawDataCache.Count; ++index)
        {
          DrawData drawData = drawinfo.DrawDataCache[index];
          drawData.color *= drawinfo.drawPlayer.firstFractalAfterImageOpacity;
          drawData.color.A = (byte) ((double) drawData.color.A * 0.800000011920929);
          drawinfo.DrawDataCache[index] = drawData;
        }
      }
    }

    public static void DrawPlayer_RenderAllLayers(ref PlayerDrawSet drawinfo)
    {
      List<DrawData> drawDataCache = drawinfo.DrawDataCache;
      if (PlayerDrawLayers.spriteBuffer == null)
        PlayerDrawLayers.spriteBuffer = new SpriteDrawBuffer(Main.graphics.GraphicsDevice, 200);
      else
        PlayerDrawLayers.spriteBuffer.CheckGraphicsDevice(Main.graphics.GraphicsDevice);
      foreach (DrawData drawData in drawDataCache)
      {
        if (drawData.texture != null)
          drawData.Draw(PlayerDrawLayers.spriteBuffer);
      }
      PlayerDrawLayers.spriteBuffer.UploadAndBind();
      DrawData cdd = new DrawData();
      int num = 0;
      for (int index = 0; index <= drawDataCache.Count; ++index)
      {
        if (drawinfo.projectileDrawPosition == index)
        {
          if (cdd.shader != 0)
            Main.pixelShader.CurrentTechnique.Passes[0].Apply();
          PlayerDrawLayers.spriteBuffer.Unbind();
          PlayerDrawLayers.DrawHeldProj(drawinfo, Main.projectile[drawinfo.drawPlayer.heldProj]);
          PlayerDrawLayers.spriteBuffer.Bind();
        }
        if (index != drawDataCache.Count)
        {
          cdd = drawDataCache[index];
          if (!cdd.sourceRect.HasValue)
            cdd.sourceRect = new Rectangle?(cdd.texture.Frame());
          PlayerDrawHelper.SetShaderForData(drawinfo.drawPlayer, drawinfo.cHead, ref cdd);
          if (cdd.texture != null)
            PlayerDrawLayers.spriteBuffer.DrawSingle(num++);
        }
      }
      PlayerDrawLayers.spriteBuffer.Unbind();
      Main.pixelShader.CurrentTechnique.Passes[0].Apply();
    }

    private static void DrawHeldProj(PlayerDrawSet drawinfo, Projectile proj)
    {
      if (!ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[proj.type])
        proj.gfxOffY = drawinfo.drawPlayer.gfxOffY;
      try
      {
        Main.instance.DrawProjDirect(proj);
      }
      catch
      {
        proj.active = false;
      }
    }

    public static void DrawPlayer_RenderAllLayersSlow(ref PlayerDrawSet drawinfo)
    {
      int num = -1;
      List<DrawData> drawDataCache = drawinfo.DrawDataCache;
      Effect pixelShader = Main.pixelShader;
      Projectile[] projectile = Main.projectile;
      SpriteBatch spriteBatch = Main.spriteBatch;
      for (int index = 0; index <= drawDataCache.Count; ++index)
      {
        if (drawinfo.projectileDrawPosition == index)
        {
          if (!ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[projectile[drawinfo.drawPlayer.heldProj].type])
            projectile[drawinfo.drawPlayer.heldProj].gfxOffY = drawinfo.drawPlayer.gfxOffY;
          if (num != 0)
          {
            pixelShader.CurrentTechnique.Passes[0].Apply();
            num = 0;
          }
          try
          {
            Main.instance.DrawProj(drawinfo.drawPlayer.heldProj);
          }
          catch
          {
            projectile[drawinfo.drawPlayer.heldProj].active = false;
          }
        }
        if (index != drawDataCache.Count)
        {
          DrawData cdd = drawDataCache[index];
          if (!cdd.sourceRect.HasValue)
            cdd.sourceRect = new Rectangle?(cdd.texture.Frame());
          PlayerDrawHelper.SetShaderForData(drawinfo.drawPlayer, drawinfo.cHead, ref cdd);
          num = cdd.shader;
          if (cdd.texture != null)
            cdd.Draw(spriteBatch);
        }
      }
      pixelShader.CurrentTechnique.Passes[0].Apply();
    }

    public static void DrawPlayer_DrawSelectionRect(ref PlayerDrawSet drawinfo)
    {
      Vector2 lowest;
      Vector2 highest;
      SpriteRenderTargetHelper.GetDrawBoundary(drawinfo.DrawDataCache, out lowest, out highest);
      Utils.DrawRect(Main.spriteBatch, lowest + Main.screenPosition, highest + Main.screenPosition, Color.White);
    }

    private static bool IsArmorDrawnWhenInvisible(int torsoID)
    {
      switch (torsoID)
      {
        case 21:
        case 22:
          return false;
        default:
          return true;
      }
    }

    private static DrawData[] GetFlatColoredCloneData(
      ref PlayerDrawSet drawinfo,
      Vector2 offset,
      Color color)
    {
      int colorOnlyShaderIndex = ContentSamples.CommonlyUsedContentSamples.ColorOnlyShaderIndex;
      DrawData[] coloredCloneData = new DrawData[drawinfo.DrawDataCache.Count];
      for (int index = 0; index < drawinfo.DrawDataCache.Count; ++index)
      {
        DrawData drawData = drawinfo.DrawDataCache[index];
        drawData.position += offset;
        drawData.shader = colorOnlyShaderIndex;
        drawData.color = color;
        coloredCloneData[index] = drawData;
      }
      return coloredCloneData;
    }
  }
}
