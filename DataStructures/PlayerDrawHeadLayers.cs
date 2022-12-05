// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.PlayerDrawHeadLayers
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.ID;

namespace Terraria.DataStructures
{
  public static class PlayerDrawHeadLayers
  {
    public static void DrawPlayer_0_(ref PlayerDrawHeadSet drawinfo)
    {
    }

    public static void DrawPlayer_00_BackHelmet(ref PlayerDrawHeadSet drawinfo)
    {
      if (drawinfo.drawPlayer.head < 0 || drawinfo.drawPlayer.head >= ArmorIDs.Head.Count)
        return;
      int index = ArmorIDs.Head.Sets.FrontToBackID[drawinfo.drawPlayer.head];
      if (index < 0)
        return;
      Rectangle hairFrame = drawinfo.HairFrame;
      PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cHead, TextureAssets.ArmorHead[index].Value, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float) (drawinfo.bodyFrameMemory.Width / 2) + (float) (drawinfo.drawPlayer.width / 2), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(hairFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
    }

    public static void DrawPlayer_01_FaceSkin(ref PlayerDrawHeadSet drawinfo)
    {
      bool flag = drawinfo.drawPlayer.head == 38 || drawinfo.drawPlayer.head == 135 || drawinfo.drawPlayer.head == 269;
      if (!flag && drawinfo.drawPlayer.faceHead > (sbyte) 0 && (int) drawinfo.drawPlayer.faceHead < (int) ArmorIDs.Face.Count)
      {
        Vector2 offsetFromHelmet = drawinfo.drawPlayer.GetFaceHeadOffsetFromHelmet();
        PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cFaceHead, TextureAssets.AccFace[(int) drawinfo.drawPlayer.faceHead].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float) (drawinfo.bodyFrameMemory.Width / 2) + (float) (drawinfo.drawPlayer.width / 2), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + offsetFromHelmet, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
        if (drawinfo.drawPlayer.face <= (sbyte) 0 || (int) drawinfo.drawPlayer.face >= (int) ArmorIDs.Face.Count || !ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[(int) drawinfo.drawPlayer.face])
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
        PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cFace, TextureAssets.AccFace[(int) drawinfo.drawPlayer.face].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.bodyFrameMemory.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)) + num, (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
      }
      else
      {
        if (flag || drawinfo.drawPlayer.isHatRackDoll)
          return;
        PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.skinDyePacked, TextureAssets.Players[drawinfo.skinVar, 0].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float) (drawinfo.bodyFrameMemory.Width / 2) + (float) (drawinfo.drawPlayer.width / 2), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
        PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, TextureAssets.Players[drawinfo.skinVar, 1].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float) (drawinfo.bodyFrameMemory.Width / 2) + (float) (drawinfo.drawPlayer.width / 2), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorEyeWhites, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
        PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, TextureAssets.Players[drawinfo.skinVar, 2].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float) (drawinfo.bodyFrameMemory.Width / 2) + (float) (drawinfo.drawPlayer.width / 2), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorEyes, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
        if (drawinfo.drawPlayer.yoraiz0rDarkness)
          PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.skinDyePacked, TextureAssets.Extra[67].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float) (drawinfo.bodyFrameMemory.Width / 2) + (float) (drawinfo.drawPlayer.width / 2), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
        if (drawinfo.drawPlayer.face <= (sbyte) 0 || (int) drawinfo.drawPlayer.face >= (int) ArmorIDs.Face.Count || !ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[(int) drawinfo.drawPlayer.face])
          return;
        PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cFace, TextureAssets.AccFace[(int) drawinfo.drawPlayer.face].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.bodyFrameMemory.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
      }
    }

    public static void DrawPlayer_02_DrawArmorWithFullHair(ref PlayerDrawHeadSet drawinfo)
    {
      if (!drawinfo.fullHair)
        return;
      Color color = drawinfo.colorArmorHead;
      int shaderTechnique = drawinfo.cHead;
      if (ArmorIDs.Head.Sets.UseSkinColor[drawinfo.drawPlayer.head])
      {
        color = !drawinfo.drawPlayer.isDisplayDollOrInanimate ? drawinfo.colorHead : drawinfo.colorDisplayDollSkin;
        shaderTechnique = drawinfo.skinDyePacked;
      }
      PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, shaderTechnique, TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float) (drawinfo.bodyFrameMemory.Width / 2) + (float) (drawinfo.drawPlayer.width / 2), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.HairFrame), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
      if (drawinfo.hideHair)
        return;
      Rectangle hairFrame = drawinfo.HairFrame;
      hairFrame.Y -= 336;
      if (hairFrame.Y < 0)
        hairFrame.Y = 0;
      PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.hairShaderPacked, TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float) (drawinfo.bodyFrameMemory.Width / 2) + (float) (drawinfo.drawPlayer.width / 2), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + drawinfo.hairOffset, new Rectangle?(hairFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
    }

    public static void DrawPlayer_03_HelmetHair(ref PlayerDrawHeadSet drawinfo)
    {
      if (drawinfo.hideHair || !drawinfo.hatHair)
        return;
      Rectangle hairFrame = drawinfo.HairFrame;
      hairFrame.Y -= 336;
      if (hairFrame.Y < 0)
        hairFrame.Y = 0;
      if (drawinfo.drawPlayer.invis)
        return;
      PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.hairShaderPacked, TextureAssets.PlayerHairAlt[drawinfo.drawPlayer.hair].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float) (drawinfo.bodyFrameMemory.Width / 2) + (float) (drawinfo.drawPlayer.width / 2), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(hairFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
    }

    public static void DrawPlayer_04_CapricornMask(ref PlayerDrawHeadSet drawinfo)
    {
      Rectangle hairFrame = drawinfo.HairFrame;
      hairFrame.Width += 2;
      PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cHead, TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float) (drawinfo.bodyFrameMemory.Width / 2) + (float) (drawinfo.drawPlayer.width / 2), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(hairFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
    }

    public static void DrawPlayer_04_RabbitOrder(ref PlayerDrawHeadSet drawinfo)
    {
      int verticalFrames = 27;
      Texture2D texture2D = TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value;
      Rectangle r = texture2D.Frame(verticalFrames: verticalFrames, frameY: drawinfo.drawPlayer.rabbitOrderFrame.DisplayFrame);
      Vector2 origin = r.Size() / 2f;
      int usedGravDir = 1;
      Vector2 hatDrawPosition = PlayerDrawHeadLayers.DrawPlayer_04_GetHatDrawPosition(ref drawinfo, new Vector2(1f, -26f), usedGravDir);
      int hatStacks = PlayerDrawHeadLayers.GetHatStacks(ref drawinfo, 4955);
      float num1 = (float) Math.PI / 60f;
      float num2 = (float) ((double) num1 * (double) drawinfo.drawPlayer.position.X % 6.2831854820251465);
      for (int index = hatStacks - 1; index >= 0; --index)
      {
        float x = (float) ((double) Vector2.UnitY.RotatedBy((double) num2 + (double) num1 * (double) index).X * ((double) index / 30.0) * 2.0) - (float) (index * 2 * drawinfo.drawPlayer.direction);
        PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cHead, texture2D, hatDrawPosition + new Vector2(x, (float) (index * -14) * drawinfo.scale), new Rectangle?(r), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, origin, drawinfo.scale, drawinfo.playerEffect, 0.0f);
      }
      if (drawinfo.hideHair)
        return;
      Rectangle hairFrame = drawinfo.HairFrame;
      hairFrame.Y -= 336;
      if (hairFrame.Y < 0)
        hairFrame.Y = 0;
      PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.hairShaderPacked, TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float) (drawinfo.bodyFrameMemory.Width / 2) + (float) (drawinfo.drawPlayer.width / 2), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + drawinfo.hairOffset, new Rectangle?(hairFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
    }

    public static void DrawPlayer_04_BadgersHat(ref PlayerDrawHeadSet drawinfo)
    {
      int verticalFrames = 6;
      Texture2D texture2D = TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value;
      Rectangle r = texture2D.Frame(verticalFrames: verticalFrames, frameY: drawinfo.drawPlayer.rabbitOrderFrame.DisplayFrame);
      Vector2 origin = r.Size() / 2f;
      int usedGravDir = 1;
      Vector2 hatDrawPosition = PlayerDrawHeadLayers.DrawPlayer_04_GetHatDrawPosition(ref drawinfo, new Vector2(0.0f, -9f), usedGravDir);
      int hatStacks = PlayerDrawHeadLayers.GetHatStacks(ref drawinfo, 5004);
      float num1 = (float) Math.PI / 60f;
      float num2 = (float) ((double) num1 * (double) drawinfo.drawPlayer.position.X % 6.2831854820251465);
      int num3 = hatStacks * 4 + 2;
      int num4 = 0;
      bool flag = ((double) Main.GlobalTimeWrappedHourly + 180.0) % 600.0 < 60.0;
      for (int index = num3 - 1; index >= 0; --index)
      {
        int num5 = 0;
        if (index == num3 - 1)
        {
          r.Y = 0;
          num5 = 2;
        }
        else
          r.Y = index != 0 ? r.Height * (num4++ % 4 + 1) : r.Height * 5;
        if (!(r.Y == r.Height * 3 & flag))
        {
          float x = (float) ((double) Vector2.UnitY.RotatedBy((double) num2 + (double) num1 * (double) index).X * ((double) index / 10.0) * 4.0 - (double) index * 0.10000000149011612 * (double) drawinfo.drawPlayer.direction);
          PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cHead, texture2D, hatDrawPosition + new Vector2(x, (float) ((index * -4 + num5) * usedGravDir)) * drawinfo.scale, new Rectangle?(r), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, origin, drawinfo.scale, drawinfo.playerEffect, 0.0f);
        }
      }
    }

    private static Vector2 DrawPlayer_04_GetHatDrawPosition(
      ref PlayerDrawHeadSet drawinfo,
      Vector2 hatOffset,
      int usedGravDir)
    {
      Vector2 vector2 = new Vector2((float) drawinfo.drawPlayer.direction, (float) usedGravDir);
      return drawinfo.Position - Main.screenPosition + new Vector2((float) (-drawinfo.bodyFrameMemory.Width / 2 + drawinfo.drawPlayer.width / 2), (float) (drawinfo.drawPlayer.height - drawinfo.bodyFrameMemory.Height + 4)) + hatOffset * vector2 * drawinfo.scale + (drawinfo.drawPlayer.headPosition + drawinfo.headVect);
    }

    private static int GetHatStacks(ref PlayerDrawHeadSet drawinfo, int itemId)
    {
      int hatStacks = 0;
      int index1 = 0;
      if (drawinfo.drawPlayer.armor[index1] != null && drawinfo.drawPlayer.armor[index1].type == itemId && drawinfo.drawPlayer.armor[index1].stack > 0)
        hatStacks += drawinfo.drawPlayer.armor[index1].stack;
      int index2 = 10;
      if (drawinfo.drawPlayer.armor[index2] != null && drawinfo.drawPlayer.armor[index2].type == itemId && drawinfo.drawPlayer.armor[index2].stack > 0)
        hatStacks += drawinfo.drawPlayer.armor[index2].stack;
      if (hatStacks > 2)
        hatStacks = 2;
      return hatStacks;
    }

    public static void DrawPlayer_04_HatsWithFullHair(ref PlayerDrawHeadSet drawinfo)
    {
      if (drawinfo.drawPlayer.head == 259)
      {
        PlayerDrawHeadLayers.DrawPlayer_04_RabbitOrder(ref drawinfo);
      }
      else
      {
        if (!drawinfo.helmetIsOverFullHair)
          return;
        if (!drawinfo.hideHair)
        {
          Rectangle hairFrame = drawinfo.HairFrame;
          hairFrame.Y -= 336;
          if (hairFrame.Y < 0)
            hairFrame.Y = 0;
          PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.hairShaderPacked, TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float) (drawinfo.bodyFrameMemory.Width / 2) + (float) (drawinfo.drawPlayer.width / 2), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + drawinfo.hairOffset, new Rectangle?(hairFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
        }
        if (drawinfo.drawPlayer.head == 0)
          return;
        Color color = drawinfo.colorArmorHead;
        int shaderTechnique = drawinfo.cHead;
        if (ArmorIDs.Head.Sets.UseSkinColor[drawinfo.drawPlayer.head])
        {
          color = !drawinfo.drawPlayer.isDisplayDollOrInanimate ? drawinfo.colorHead : drawinfo.colorDisplayDollSkin;
          shaderTechnique = drawinfo.skinDyePacked;
        }
        PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, shaderTechnique, TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float) (drawinfo.bodyFrameMemory.Width / 2) + (float) (drawinfo.drawPlayer.width / 2), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
      }
    }

    public static void DrawPlayer_05_TallHats(ref PlayerDrawHeadSet drawinfo)
    {
      if (!drawinfo.helmetIsTall)
        return;
      Rectangle hairFrame = drawinfo.HairFrame;
      if (drawinfo.drawPlayer.head == 158)
        hairFrame.Height -= 2;
      int num = 0;
      if (hairFrame.Y == hairFrame.Height * 6)
        hairFrame.Height -= 2;
      else if (hairFrame.Y == hairFrame.Height * 7)
        num = -2;
      else if (hairFrame.Y == hairFrame.Height * 8)
        num = -2;
      else if (hairFrame.Y == hairFrame.Height * 9)
        num = -2;
      else if (hairFrame.Y == hairFrame.Height * 10)
        num = -2;
      else if (hairFrame.Y == hairFrame.Height * 13)
        hairFrame.Height -= 2;
      else if (hairFrame.Y == hairFrame.Height * 14)
        num = -2;
      else if (hairFrame.Y == hairFrame.Height * 15)
        num = -2;
      else if (hairFrame.Y == hairFrame.Height * 16)
        num = -2;
      hairFrame.Y += num;
      Color color = drawinfo.colorArmorHead;
      int shaderTechnique = drawinfo.cHead;
      if (ArmorIDs.Head.Sets.UseSkinColor[drawinfo.drawPlayer.head])
      {
        color = !drawinfo.drawPlayer.isDisplayDollOrInanimate ? drawinfo.colorHead : drawinfo.colorDisplayDollSkin;
        shaderTechnique = drawinfo.skinDyePacked;
      }
      PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, shaderTechnique, TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float) (drawinfo.bodyFrameMemory.Width / 2) + (float) (drawinfo.drawPlayer.width / 2), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0) + (float) num) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(hairFrame), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
    }

    public static void DrawPlayer_06_NormalHats(ref PlayerDrawHeadSet drawinfo)
    {
      if (drawinfo.drawPlayer.head == 270)
        PlayerDrawHeadLayers.DrawPlayer_04_CapricornMask(ref drawinfo);
      else if (drawinfo.drawPlayer.head == 265)
      {
        PlayerDrawHeadLayers.DrawPlayer_04_BadgersHat(ref drawinfo);
      }
      else
      {
        if (!drawinfo.helmetIsNormal)
          return;
        Rectangle hairFrame = drawinfo.HairFrame;
        Color color = drawinfo.colorArmorHead;
        int shaderTechnique = drawinfo.cHead;
        if (ArmorIDs.Head.Sets.UseSkinColor[drawinfo.drawPlayer.head])
        {
          color = !drawinfo.drawPlayer.isDisplayDollOrInanimate ? drawinfo.colorHead : drawinfo.colorDisplayDollSkin;
          shaderTechnique = drawinfo.skinDyePacked;
        }
        PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, shaderTechnique, TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float) (drawinfo.bodyFrameMemory.Width / 2) + (float) (drawinfo.drawPlayer.width / 2), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(hairFrame), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
        if (drawinfo.drawPlayer.head != 271)
          return;
        int tvScreen = PlayerDrawLayers.DrawPlayer_Head_GetTVScreen(drawinfo.drawPlayer);
        if (tvScreen == 0)
          return;
        Texture2D texture2D = TextureAssets.GlowMask[309].Value;
        int frameY = drawinfo.drawPlayer.miscCounter % 20 / 5;
        if (tvScreen == 5)
        {
          frameY = 0;
          if (drawinfo.drawPlayer.eyeHelper.EyeFrameToShow > 0)
            frameY = 2;
        }
        Rectangle rectangle = texture2D.Frame(6, 4, tvScreen, frameY, -2);
        PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cHead, texture2D, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float) (drawinfo.bodyFrameMemory.Width / 2) + (float) (drawinfo.drawPlayer.width / 2), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(rectangle), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
      }
    }

    public static void DrawPlayer_07_JustHair(ref PlayerDrawHeadSet drawinfo)
    {
      if (drawinfo.helmetIsNormal || drawinfo.helmetIsOverFullHair || drawinfo.helmetIsTall || drawinfo.hideHair)
        return;
      Rectangle hairFrame = drawinfo.HairFrame;
      hairFrame.Y -= 336;
      if (hairFrame.Y < 0)
        hairFrame.Y = 0;
      PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.hairShaderPacked, TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float) (drawinfo.bodyFrameMemory.Width / 2) + (float) (drawinfo.drawPlayer.width / 2), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + drawinfo.hairOffset, new Rectangle?(hairFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
    }

    public static void DrawPlayer_08_FaceAcc(ref PlayerDrawHeadSet drawinfo)
    {
      if (drawinfo.drawPlayer.beard > (sbyte) 0 && (drawinfo.drawPlayer.head < 0 || !ArmorIDs.Head.Sets.PreventBeardDraw[drawinfo.drawPlayer.head]))
      {
        Vector2 offsetFromHelmet = drawinfo.drawPlayer.GetBeardDrawOffsetFromHelmet();
        Color color = drawinfo.colorArmorHead;
        if (ArmorIDs.Beard.Sets.UseHairColor[(int) drawinfo.drawPlayer.beard])
          color = drawinfo.colorHair;
        PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cBeard, TextureAssets.AccBeard[(int) drawinfo.drawPlayer.beard].Value, offsetFromHelmet + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float) (drawinfo.bodyFrameMemory.Width / 2) + (float) (drawinfo.drawPlayer.width / 2), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
      }
      if (drawinfo.drawPlayer.face > (sbyte) 0 && (int) drawinfo.drawPlayer.face < (int) ArmorIDs.Face.Count && !ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[(int) drawinfo.drawPlayer.face])
      {
        Vector2 vector2 = Vector2.Zero;
        if (drawinfo.drawPlayer.face == (sbyte) 19)
          vector2 = new Vector2(0.0f, -6f);
        PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cFace, TextureAssets.AccFace[(int) drawinfo.drawPlayer.face].Value, vector2 + new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.bodyFrameMemory.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
      }
      if (drawinfo.drawPlayer.faceFlower > (sbyte) 0 && (int) drawinfo.drawPlayer.faceFlower < (int) ArmorIDs.Face.Count)
        PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cFaceFlower, TextureAssets.AccFace[(int) drawinfo.drawPlayer.faceFlower].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.bodyFrameMemory.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
      if (drawinfo.drawUnicornHorn)
        PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cUnicornHorn, TextureAssets.Extra[143].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float) (drawinfo.bodyFrameMemory.Width / 2) + (float) (drawinfo.drawPlayer.width / 2), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
      if (!drawinfo.drawAngelHalo)
        return;
      Main.instance.LoadAccFace(7);
      PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cAngelHalo, TextureAssets.AccFace[7].Value, new Vector2((float) (int) ((double) drawinfo.Position.X - (double) Main.screenPosition.X - (double) (drawinfo.bodyFrameMemory.Width / 2) + (double) (drawinfo.drawPlayer.width / 2)), (float) ((double) drawinfo.Position.Y - (double) Main.screenPosition.Y + (double) drawinfo.drawPlayer.height - (double) drawinfo.bodyFrameMemory.Height + 4.0)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), new Color(200, 200, 200, 200), drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0.0f);
    }

    public static void DrawPlayer_RenderAllLayers(ref PlayerDrawHeadSet drawinfo)
    {
      List<DrawData> drawData = drawinfo.DrawData;
      Effect pixelShader = Main.pixelShader;
      Projectile[] projectile = Main.projectile;
      SpriteBatch spriteBatch = Main.spriteBatch;
      for (int index = 0; index < drawData.Count; ++index)
      {
        DrawData cdd = drawData[index];
        if (!cdd.sourceRect.HasValue)
          cdd.sourceRect = new Rectangle?(cdd.texture.Frame());
        PlayerDrawHelper.SetShaderForData(drawinfo.drawPlayer, drawinfo.cHead, ref cdd);
        if (cdd.texture != null)
          cdd.Draw(spriteBatch);
      }
      pixelShader.CurrentTechnique.Passes[0].Apply();
    }

    public static void DrawPlayer_DrawSelectionRect(ref PlayerDrawHeadSet drawinfo)
    {
      Vector2 lowest;
      Vector2 highest;
      SpriteRenderTargetHelper.GetDrawBoundary(drawinfo.DrawData, out lowest, out highest);
      Utils.DrawRect(Main.spriteBatch, lowest + Main.screenPosition, highest + Main.screenPosition, Color.White);
    }

    public static void QuickCDD(
      List<DrawData> drawData,
      Texture2D texture,
      Vector2 position,
      Rectangle? sourceRectangle,
      Color color,
      float rotation,
      Vector2 origin,
      float scale,
      SpriteEffects effects,
      float layerDepth)
    {
      drawData.Add(new DrawData(texture, position, sourceRectangle, color, rotation, origin, scale, effects));
    }

    public static void QuickCDD(
      List<DrawData> drawData,
      int shaderTechnique,
      Texture2D texture,
      Vector2 position,
      Rectangle? sourceRectangle,
      Color color,
      float rotation,
      Vector2 origin,
      float scale,
      SpriteEffects effects,
      float layerDepth)
    {
      drawData.Add(new DrawData(texture, position, sourceRectangle, color, rotation, origin, scale, effects)
      {
        shader = shaderTechnique
      });
    }
  }
}
