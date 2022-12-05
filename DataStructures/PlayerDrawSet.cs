// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.PlayerDrawSet
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.GameContent.Golf;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace Terraria.DataStructures
{
  public struct PlayerDrawSet
  {
    public List<DrawData> DrawDataCache;
    public List<int> DustCache;
    public List<int> GoreCache;
    public Player drawPlayer;
    public float shadow;
    public Vector2 Position;
    public int projectileDrawPosition;
    public Vector2 ItemLocation;
    public int armorAdjust;
    public bool missingHand;
    public bool missingArm;
    public bool heldProjOverHand;
    public int skinVar;
    public bool fullHair;
    public bool drawsBackHairWithoutHeadgear;
    public bool hatHair;
    public bool hideHair;
    public int hairDyePacked;
    public int skinDyePacked;
    public float mountOffSet;
    public int cHead;
    public int cBody;
    public int cLegs;
    public int cHandOn;
    public int cHandOff;
    public int cBack;
    public int cFront;
    public int cShoe;
    public int cFlameWaker;
    public int cWaist;
    public int cShield;
    public int cNeck;
    public int cFace;
    public int cBalloon;
    public int cWings;
    public int cCarpet;
    public int cPortableStool;
    public int cFloatingTube;
    public int cUnicornHorn;
    public int cAngelHalo;
    public int cBeard;
    public int cLeinShampoo;
    public int cBackpack;
    public int cTail;
    public int cFaceHead;
    public int cFaceFlower;
    public int cBalloonFront;
    public SpriteEffects playerEffect;
    public SpriteEffects itemEffect;
    public Color colorHair;
    public Color colorEyeWhites;
    public Color colorEyes;
    public Color colorHead;
    public Color colorBodySkin;
    public Color colorLegs;
    public Color colorShirt;
    public Color colorUnderShirt;
    public Color colorPants;
    public Color colorShoes;
    public Color colorArmorHead;
    public Color colorArmorBody;
    public Color colorMount;
    public Color colorArmorLegs;
    public Color colorElectricity;
    public Color colorDisplayDollSkin;
    public int headGlowMask;
    public int bodyGlowMask;
    public int armGlowMask;
    public int legsGlowMask;
    public Color headGlowColor;
    public Color bodyGlowColor;
    public Color armGlowColor;
    public Color legsGlowColor;
    public Color ArkhalisColor;
    public float stealth;
    public Vector2 legVect;
    public Vector2 bodyVect;
    public Vector2 headVect;
    public Color selectionGlowColor;
    public float torsoOffset;
    public bool hidesTopSkin;
    public bool hidesBottomSkin;
    public float rotation;
    public Vector2 rotationOrigin;
    public Rectangle hairFrontFrame;
    public Rectangle hairBackFrame;
    public bool backHairDraw;
    public Color itemColor;
    public bool usesCompositeTorso;
    public bool usesCompositeFrontHandAcc;
    public bool usesCompositeBackHandAcc;
    public bool compShoulderOverFrontArm;
    public Rectangle compBackShoulderFrame;
    public Rectangle compFrontShoulderFrame;
    public Rectangle compBackArmFrame;
    public Rectangle compFrontArmFrame;
    public Rectangle compTorsoFrame;
    public float compositeBackArmRotation;
    public float compositeFrontArmRotation;
    public bool hideCompositeShoulders;
    public Vector2 frontShoulderOffset;
    public Vector2 backShoulderOffset;
    public WeaponDrawOrder weaponDrawOrder;
    public bool weaponOverFrontArm;
    public bool isSitting;
    public bool isSleeping;
    public float seatYOffset;
    public int sittingIndex;
    public bool drawFrontAccInNeckAccLayer;
    public Item heldItem;
    public bool drawFloatingTube;
    public bool drawUnicornHorn;
    public bool drawAngelHalo;
    public Color floatingTubeColor;
    public Vector2 hairOffset;
    public Vector2 helmetOffset;
    public Vector2 legsOffset;
    public bool hideEntirePlayer;

    public Vector2 Center => new Vector2(this.Position.X + (float) (this.drawPlayer.width / 2), this.Position.Y + (float) (this.drawPlayer.height / 2));

    public void BoringSetup(
      Player player,
      List<DrawData> drawData,
      List<int> dust,
      List<int> gore,
      Vector2 drawPosition,
      float shadowOpacity,
      float rotation,
      Vector2 rotationOrigin)
    {
      this.DrawDataCache = drawData;
      this.DustCache = dust;
      this.GoreCache = gore;
      this.drawPlayer = player;
      this.shadow = shadowOpacity;
      this.rotation = rotation;
      this.rotationOrigin = rotationOrigin;
      this.heldItem = player.lastVisualizedSelectedItem;
      this.cHead = this.drawPlayer.cHead;
      this.cBody = this.drawPlayer.cBody;
      this.cLegs = this.drawPlayer.cLegs;
      if (this.drawPlayer.wearsRobe)
        this.cLegs = this.cBody;
      this.cHandOn = this.drawPlayer.cHandOn;
      this.cHandOff = this.drawPlayer.cHandOff;
      this.cBack = this.drawPlayer.cBack;
      this.cFront = this.drawPlayer.cFront;
      this.cShoe = this.drawPlayer.cShoe;
      this.cFlameWaker = this.drawPlayer.cFlameWaker;
      this.cWaist = this.drawPlayer.cWaist;
      this.cShield = this.drawPlayer.cShield;
      this.cNeck = this.drawPlayer.cNeck;
      this.cFace = this.drawPlayer.cFace;
      this.cBalloon = this.drawPlayer.cBalloon;
      this.cWings = this.drawPlayer.cWings;
      this.cCarpet = this.drawPlayer.cCarpet;
      this.cPortableStool = this.drawPlayer.cPortableStool;
      this.cFloatingTube = this.drawPlayer.cFloatingTube;
      this.cUnicornHorn = this.drawPlayer.cUnicornHorn;
      this.cAngelHalo = this.drawPlayer.cAngelHalo;
      this.cLeinShampoo = this.drawPlayer.cLeinShampoo;
      this.cBackpack = this.drawPlayer.cBackpack;
      this.cTail = this.drawPlayer.cTail;
      this.cFaceHead = this.drawPlayer.cFaceHead;
      this.cFaceFlower = this.drawPlayer.cFaceFlower;
      this.cBalloonFront = this.drawPlayer.cBalloonFront;
      this.cBeard = this.drawPlayer.cBeard;
      this.isSitting = this.drawPlayer.sitting.isSitting;
      this.seatYOffset = 0.0f;
      this.sittingIndex = 0;
      Vector2 posOffset1 = Vector2.Zero;
      this.drawPlayer.sitting.GetSittingOffsetInfo(this.drawPlayer, out posOffset1, out this.seatYOffset);
      if (this.isSitting)
        this.sittingIndex = this.drawPlayer.sitting.sittingIndex;
      if (this.drawPlayer.mount.Active && this.drawPlayer.mount.Type == 17)
        this.isSitting = true;
      if (this.drawPlayer.mount.Active && this.drawPlayer.mount.Type == 23)
        this.isSitting = true;
      if (this.drawPlayer.mount.Active && this.drawPlayer.mount.Type == 45)
        this.isSitting = true;
      this.isSleeping = this.drawPlayer.sleeping.isSleeping;
      this.Position = drawPosition;
      this.Position += new Vector2(this.drawPlayer.MountXOffset * (float) this.drawPlayer.direction, 0.0f);
      if (this.isSitting)
      {
        this.torsoOffset = this.seatYOffset;
        this.Position += posOffset1;
      }
      else
        this.sittingIndex = -1;
      if (this.isSleeping)
      {
        this.rotationOrigin = player.Size / 2f;
        Vector2 posOffset2;
        this.drawPlayer.sleeping.GetSleepingOffsetInfo(this.drawPlayer, out posOffset2);
        this.Position += posOffset2;
      }
      this.weaponDrawOrder = WeaponDrawOrder.BehindFrontArm;
      if (this.heldItem.type == 4952)
        this.weaponDrawOrder = WeaponDrawOrder.BehindBackArm;
      if (GolfHelper.IsPlayerHoldingClub(player) && player.itemAnimation > player.itemAnimationMax)
        this.weaponDrawOrder = WeaponDrawOrder.OverFrontArm;
      this.projectileDrawPosition = -1;
      this.ItemLocation = this.Position + (this.drawPlayer.itemLocation - this.drawPlayer.position);
      this.armorAdjust = 0;
      this.missingHand = false;
      this.missingArm = false;
      this.heldProjOverHand = false;
      this.skinVar = this.drawPlayer.skinVariant;
      if (this.drawPlayer.body == 77 || this.drawPlayer.body == 103 || this.drawPlayer.body == 41 || this.drawPlayer.body == 100 || this.drawPlayer.body == 10 || this.drawPlayer.body == 11 || this.drawPlayer.body == 12 || this.drawPlayer.body == 13 || this.drawPlayer.body == 14 || this.drawPlayer.body == 43 || this.drawPlayer.body == 15 || this.drawPlayer.body == 16 || this.drawPlayer.body == 20 || this.drawPlayer.body == 39 || this.drawPlayer.body == 50 || this.drawPlayer.body == 38 || this.drawPlayer.body == 40 || this.drawPlayer.body == 57 || this.drawPlayer.body == 44 || this.drawPlayer.body == 52 || this.drawPlayer.body == 53 || this.drawPlayer.body == 68 || this.drawPlayer.body == 81 || this.drawPlayer.body == 85 || this.drawPlayer.body == 88 || this.drawPlayer.body == 98 || this.drawPlayer.body == 86 || this.drawPlayer.body == 87 || this.drawPlayer.body == 99 || this.drawPlayer.body == 165 || this.drawPlayer.body == 166 || this.drawPlayer.body == 167 || this.drawPlayer.body == 171 || this.drawPlayer.body == 45 || this.drawPlayer.body == 168 || this.drawPlayer.body == 169 || this.drawPlayer.body == 42 || this.drawPlayer.body == 180 || this.drawPlayer.body == 181 || this.drawPlayer.body == 183 || this.drawPlayer.body == 186 || this.drawPlayer.body == 187 || this.drawPlayer.body == 188 || this.drawPlayer.body == 64 || this.drawPlayer.body == 189 || this.drawPlayer.body == 191 || this.drawPlayer.body == 192 || this.drawPlayer.body == 198 || this.drawPlayer.body == 199 || this.drawPlayer.body == 202 || this.drawPlayer.body == 203 || this.drawPlayer.body == 58 || this.drawPlayer.body == 59 || this.drawPlayer.body == 60 || this.drawPlayer.body == 61 || this.drawPlayer.body == 62 || this.drawPlayer.body == 63 || this.drawPlayer.body == 36 || this.drawPlayer.body == 104 || this.drawPlayer.body == 184 || this.drawPlayer.body == 74 || this.drawPlayer.body == 78 || this.drawPlayer.body == 185 || this.drawPlayer.body == 196 || this.drawPlayer.body == 197 || this.drawPlayer.body == 182 || this.drawPlayer.body == 87 || this.drawPlayer.body == 76 || this.drawPlayer.body == 209 || this.drawPlayer.body == 168 || this.drawPlayer.body == 210 || this.drawPlayer.body == 211 || this.drawPlayer.body == 213)
        this.missingHand = true;
      this.missingArm = this.drawPlayer.body != 83;
      if (this.drawPlayer.heldProj >= 0 && (double) this.shadow == 0.0)
      {
        switch (Main.projectile[this.drawPlayer.heldProj].type)
        {
          case 460:
          case 535:
          case 600:
            this.heldProjOverHand = true;
            break;
        }
      }
      this.drawPlayer.GetHairSettings(out this.fullHair, out this.hatHair, out this.hideHair, out this.backHairDraw, out this.drawsBackHairWithoutHeadgear);
      this.hairDyePacked = PlayerDrawHelper.PackShader((int) this.drawPlayer.hairDye, PlayerDrawHelper.ShaderConfiguration.HairShader);
      if (this.drawPlayer.head == 0 && this.drawPlayer.hairDye == (byte) 0)
        this.hairDyePacked = PlayerDrawHelper.PackShader(1, PlayerDrawHelper.ShaderConfiguration.HairShader);
      this.skinDyePacked = player.skinDyePacked;
      if (this.drawPlayer.mount.Active && this.drawPlayer.mount.Type == 52)
        this.AdjustmentsForWolfMount();
      if (this.drawPlayer.isDisplayDollOrInanimate)
      {
        Point tileCoordinates = this.Center.ToTileCoordinates();
        bool actuallySelected;
        if (Main.InSmartCursorHighlightArea(tileCoordinates.X, tileCoordinates.Y, out actuallySelected))
        {
          Color color = Lighting.GetColor(tileCoordinates.X, tileCoordinates.Y);
          int averageTileLighting = ((int) color.R + (int) color.G + (int) color.B) / 3;
          if (averageTileLighting > 10)
            this.selectionGlowColor = Colors.GetSelectionGlowColor(actuallySelected, averageTileLighting);
        }
      }
      this.mountOffSet = this.drawPlayer.HeightOffsetVisual;
      this.Position.Y -= this.mountOffSet;
      Mount.currentShader = !this.drawPlayer.mount.Active ? 0 : (this.drawPlayer.mount.Cart ? this.drawPlayer.cMinecart : this.drawPlayer.cMount);
      this.playerEffect = SpriteEffects.None;
      this.itemEffect = SpriteEffects.FlipHorizontally;
      this.colorHair = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.GetHairColor(), this.shadow);
      this.colorEyeWhites = this.drawPlayer.GetImmuneAlpha(Lighting.GetColorClamped((int) ((double) this.Position.X + (double) this.drawPlayer.width * 0.5) / 16, (int) (((double) this.Position.Y + (double) this.drawPlayer.height * 0.25) / 16.0), Color.White), this.shadow);
      this.colorEyes = this.drawPlayer.GetImmuneAlpha(Lighting.GetColorClamped((int) ((double) this.Position.X + (double) this.drawPlayer.width * 0.5) / 16, (int) (((double) this.Position.Y + (double) this.drawPlayer.height * 0.25) / 16.0), this.drawPlayer.eyeColor), this.shadow);
      this.colorHead = this.drawPlayer.GetImmuneAlpha(Lighting.GetColorClamped((int) ((double) this.Position.X + (double) this.drawPlayer.width * 0.5) / 16, (int) (((double) this.Position.Y + (double) this.drawPlayer.height * 0.25) / 16.0), this.drawPlayer.skinColor), this.shadow);
      this.colorBodySkin = this.drawPlayer.GetImmuneAlpha(Lighting.GetColorClamped((int) ((double) this.Position.X + (double) this.drawPlayer.width * 0.5) / 16, (int) (((double) this.Position.Y + (double) this.drawPlayer.height * 0.5) / 16.0), this.drawPlayer.skinColor), this.shadow);
      this.colorLegs = this.drawPlayer.GetImmuneAlpha(Lighting.GetColorClamped((int) ((double) this.Position.X + (double) this.drawPlayer.width * 0.5) / 16, (int) (((double) this.Position.Y + (double) this.drawPlayer.height * 0.75) / 16.0), this.drawPlayer.skinColor), this.shadow);
      this.colorShirt = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int) ((double) this.Position.X + (double) this.drawPlayer.width * 0.5) / 16, (int) (((double) this.Position.Y + (double) this.drawPlayer.height * 0.5) / 16.0), this.drawPlayer.shirtColor), this.shadow);
      this.colorUnderShirt = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int) ((double) this.Position.X + (double) this.drawPlayer.width * 0.5) / 16, (int) (((double) this.Position.Y + (double) this.drawPlayer.height * 0.5) / 16.0), this.drawPlayer.underShirtColor), this.shadow);
      this.colorPants = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int) ((double) this.Position.X + (double) this.drawPlayer.width * 0.5) / 16, (int) (((double) this.Position.Y + (double) this.drawPlayer.height * 0.75) / 16.0), this.drawPlayer.pantsColor), this.shadow);
      this.colorShoes = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int) ((double) this.Position.X + (double) this.drawPlayer.width * 0.5) / 16, (int) (((double) this.Position.Y + (double) this.drawPlayer.height * 0.75) / 16.0), this.drawPlayer.shoeColor), this.shadow);
      this.colorArmorHead = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int) ((double) this.Position.X + (double) this.drawPlayer.width * 0.5) / 16, (int) ((double) this.Position.Y + (double) this.drawPlayer.height * 0.25) / 16, Color.White), this.shadow);
      this.colorArmorBody = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int) ((double) this.Position.X + (double) this.drawPlayer.width * 0.5) / 16, (int) ((double) this.Position.Y + (double) this.drawPlayer.height * 0.5) / 16, Color.White), this.shadow);
      this.colorMount = this.colorArmorBody;
      this.colorArmorLegs = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int) ((double) this.Position.X + (double) this.drawPlayer.width * 0.5) / 16, (int) ((double) this.Position.Y + (double) this.drawPlayer.height * 0.75) / 16, Color.White), this.shadow);
      this.floatingTubeColor = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int) ((double) this.Position.X + (double) this.drawPlayer.width * 0.5) / 16, (int) ((double) this.Position.Y + (double) this.drawPlayer.height * 0.75) / 16, Color.White), this.shadow);
      this.colorElectricity = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 100);
      this.colorDisplayDollSkin = this.colorBodySkin;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      this.headGlowMask = -1;
      this.bodyGlowMask = -1;
      this.armGlowMask = -1;
      this.legsGlowMask = -1;
      this.headGlowColor = Color.Transparent;
      this.bodyGlowColor = Color.Transparent;
      this.armGlowColor = Color.Transparent;
      this.legsGlowColor = Color.Transparent;
      switch (this.drawPlayer.head)
      {
        case 169:
          ++num1;
          break;
        case 170:
          ++num2;
          break;
        case 171:
          ++num3;
          break;
        case 189:
          ++num4;
          break;
      }
      switch (this.drawPlayer.body)
      {
        case 175:
          ++num1;
          break;
        case 176:
          ++num2;
          break;
        case 177:
          ++num3;
          break;
        case 190:
          ++num4;
          break;
      }
      switch (this.drawPlayer.legs)
      {
        case 110:
          int num5 = num1 + 1;
          break;
        case 111:
          int num6 = num2 + 1;
          break;
        case 112:
          int num7 = num3 + 1;
          break;
        case 130:
          int num8 = num4 + 1;
          break;
      }
      int num9 = 3;
      int num10 = 3;
      int num11 = 3;
      int num12 = 3;
      this.ArkhalisColor = this.drawPlayer.underShirtColor;
      this.ArkhalisColor.A = (byte) 180;
      if (this.drawPlayer.head == 169)
      {
        this.headGlowMask = 15;
        byte num13 = (byte) (62.5 * (double) (1 + num9));
        this.headGlowColor = new Color((int) num13, (int) num13, (int) num13, 0);
      }
      else if (this.drawPlayer.head == 216)
      {
        this.headGlowMask = 256;
        byte num14 = 127;
        this.headGlowColor = new Color((int) num14, (int) num14, (int) num14, 0);
      }
      else if (this.drawPlayer.head == 210)
      {
        this.headGlowMask = 242;
        byte num15 = 127;
        this.headGlowColor = new Color((int) num15, (int) num15, (int) num15, 0);
      }
      else if (this.drawPlayer.head == 214)
      {
        this.headGlowMask = 245;
        this.headGlowColor = this.ArkhalisColor;
      }
      else if (this.drawPlayer.head == 240)
      {
        this.headGlowMask = 273;
        this.headGlowColor = new Color(230, 230, 230, 60);
      }
      else if (this.drawPlayer.head == 267)
      {
        this.headGlowMask = 301;
        this.headGlowColor = new Color(230, 230, 230, 60);
      }
      else if (this.drawPlayer.head == 268)
      {
        this.headGlowMask = 302;
        float num16 = (float) Main.mouseTextColor / (float) byte.MaxValue;
        this.headGlowColor = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue) * (num16 * num16);
      }
      else if (this.drawPlayer.head == 269)
      {
        this.headGlowMask = 304;
        this.headGlowColor = new Color(200, 200, 200);
      }
      else if (this.drawPlayer.head == 270)
      {
        this.headGlowMask = 305;
        this.headGlowColor = new Color(200, 200, 200, 150);
      }
      else if (this.drawPlayer.head == 271)
      {
        this.headGlowMask = 309;
        this.headGlowColor = Color.White;
      }
      else if (this.drawPlayer.head == 170)
      {
        this.headGlowMask = 16;
        byte num17 = (byte) (62.5 * (double) (1 + num10));
        this.headGlowColor = new Color((int) num17, (int) num17, (int) num17, 0);
      }
      else if (this.drawPlayer.head == 189)
      {
        this.headGlowMask = 184;
        byte num18 = (byte) (62.5 * (double) (1 + num12));
        this.headGlowColor = new Color((int) num18, (int) num18, (int) num18, 0);
        this.colorArmorHead = this.drawPlayer.GetImmuneAlphaPure(new Color((int) num18, (int) num18, (int) num18, (int) byte.MaxValue), this.shadow);
      }
      else if (this.drawPlayer.head == 171)
      {
        byte num19 = (byte) (62.5 * (double) (1 + num11));
        this.colorArmorHead = this.drawPlayer.GetImmuneAlphaPure(new Color((int) num19, (int) num19, (int) num19, (int) byte.MaxValue), this.shadow);
      }
      else if (this.drawPlayer.head == 175)
      {
        this.headGlowMask = 41;
        this.headGlowColor = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
      }
      else if (this.drawPlayer.head == 193)
      {
        this.headGlowMask = 209;
        this.headGlowColor = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) sbyte.MaxValue);
      }
      else if (this.drawPlayer.head == 109)
      {
        this.headGlowMask = 208;
        this.headGlowColor = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
      }
      else if (this.drawPlayer.head == 178)
      {
        this.headGlowMask = 96;
        this.headGlowColor = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
      }
      if (this.drawPlayer.body == 175)
      {
        this.bodyGlowMask = !this.drawPlayer.Male ? 18 : 13;
        byte num20 = (byte) (62.5 * (double) (1 + num9));
        this.bodyGlowColor = new Color((int) num20, (int) num20, (int) num20, 0);
      }
      else if (this.drawPlayer.body == 208)
      {
        this.bodyGlowMask = !this.drawPlayer.Male ? 247 : 246;
        this.armGlowMask = 248;
        this.bodyGlowColor = this.ArkhalisColor;
        this.armGlowColor = this.ArkhalisColor;
      }
      else if (this.drawPlayer.body == 227)
      {
        this.bodyGlowColor = new Color(230, 230, 230, 60);
        this.armGlowColor = new Color(230, 230, 230, 60);
      }
      else if (this.drawPlayer.body == 237)
      {
        float num21 = (float) Main.mouseTextColor / (float) byte.MaxValue;
        this.bodyGlowColor = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue) * (num21 * num21);
      }
      else if (this.drawPlayer.body == 238)
      {
        this.bodyGlowColor = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        this.armGlowColor = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      }
      else if (this.drawPlayer.body == 239)
      {
        this.bodyGlowColor = new Color(200, 200, 200, 150);
        this.armGlowColor = new Color(200, 200, 200, 150);
      }
      else if (this.drawPlayer.body == 190)
      {
        this.bodyGlowMask = !this.drawPlayer.Male ? 186 : 185;
        this.armGlowMask = 188;
        byte num22 = (byte) (62.5 * (double) (1 + num12));
        this.bodyGlowColor = new Color((int) num22, (int) num22, (int) num22, 0);
        this.armGlowColor = new Color((int) num22, (int) num22, (int) num22, 0);
        this.colorArmorBody = this.drawPlayer.GetImmuneAlphaPure(new Color((int) num22, (int) num22, (int) num22, (int) byte.MaxValue), this.shadow);
      }
      else if (this.drawPlayer.body == 176)
      {
        this.bodyGlowMask = !this.drawPlayer.Male ? 19 : 14;
        this.armGlowMask = 12;
        byte num23 = (byte) (62.5 * (double) (1 + num10));
        this.bodyGlowColor = new Color((int) num23, (int) num23, (int) num23, 0);
        this.armGlowColor = new Color((int) num23, (int) num23, (int) num23, 0);
      }
      else if (this.drawPlayer.body == 194)
      {
        this.bodyGlowMask = 210;
        this.armGlowMask = 211;
        this.bodyGlowColor = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) sbyte.MaxValue);
        this.armGlowColor = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) sbyte.MaxValue);
      }
      else if (this.drawPlayer.body == 177)
      {
        byte num24 = (byte) (62.5 * (double) (1 + num11));
        this.colorArmorBody = this.drawPlayer.GetImmuneAlphaPure(new Color((int) num24, (int) num24, (int) num24, (int) byte.MaxValue), this.shadow);
      }
      else if (this.drawPlayer.body == 179)
      {
        this.bodyGlowMask = !this.drawPlayer.Male ? 43 : 42;
        this.armGlowMask = 44;
        this.bodyGlowColor = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
        this.armGlowColor = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
      }
      if (this.drawPlayer.legs == 111)
      {
        this.legsGlowMask = 17;
        byte num25 = (byte) (62.5 * (double) (1 + num10));
        this.legsGlowColor = new Color((int) num25, (int) num25, (int) num25, 0);
      }
      else if (this.drawPlayer.legs == 157)
      {
        this.legsGlowMask = 249;
        this.legsGlowColor = this.ArkhalisColor;
      }
      else if (this.drawPlayer.legs == 158)
      {
        this.legsGlowMask = 250;
        this.legsGlowColor = this.ArkhalisColor;
      }
      else if (this.drawPlayer.legs == 210)
      {
        this.legsGlowMask = 274;
        this.legsGlowColor = new Color(230, 230, 230, 60);
      }
      else if (this.drawPlayer.legs == 222)
      {
        this.legsGlowMask = 303;
        float num26 = (float) Main.mouseTextColor / (float) byte.MaxValue;
        this.legsGlowColor = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue) * (num26 * num26);
      }
      else if (this.drawPlayer.legs == 225)
      {
        this.legsGlowMask = 306;
        this.legsGlowColor = new Color(200, 200, 200, 150);
      }
      else if (this.drawPlayer.legs == 226)
      {
        this.legsGlowMask = 307;
        this.legsGlowColor = new Color(200, 200, 200, 150);
      }
      else if (this.drawPlayer.legs == 110)
      {
        this.legsGlowMask = 199;
        byte num27 = (byte) (62.5 * (double) (1 + num9));
        this.legsGlowColor = new Color((int) num27, (int) num27, (int) num27, 0);
      }
      else if (this.drawPlayer.legs == 112)
      {
        byte num28 = (byte) (62.5 * (double) (1 + num11));
        this.colorArmorLegs = this.drawPlayer.GetImmuneAlphaPure(new Color((int) num28, (int) num28, (int) num28, (int) byte.MaxValue), this.shadow);
      }
      else if (this.drawPlayer.legs == 134)
      {
        this.legsGlowMask = 212;
        this.legsGlowColor = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) sbyte.MaxValue);
      }
      else if (this.drawPlayer.legs == 130)
      {
        byte num29 = (byte) ((int) sbyte.MaxValue * (1 + num12));
        this.legsGlowMask = 187;
        this.legsGlowColor = new Color((int) num29, (int) num29, (int) num29, 0);
        this.colorArmorLegs = this.drawPlayer.GetImmuneAlphaPure(new Color((int) num29, (int) num29, (int) num29, (int) byte.MaxValue), this.shadow);
      }
      float shadow = this.shadow;
      this.headGlowColor = this.drawPlayer.GetImmuneAlphaPure(this.headGlowColor, shadow);
      this.bodyGlowColor = this.drawPlayer.GetImmuneAlphaPure(this.bodyGlowColor, shadow);
      this.armGlowColor = this.drawPlayer.GetImmuneAlphaPure(this.armGlowColor, shadow);
      this.legsGlowColor = this.drawPlayer.GetImmuneAlphaPure(this.legsGlowColor, shadow);
      if (this.drawPlayer.head > 0 && this.drawPlayer.head < ArmorIDs.Head.Count)
      {
        Main.instance.LoadArmorHead(this.drawPlayer.head);
        int i = ArmorIDs.Head.Sets.FrontToBackID[this.drawPlayer.head];
        if (i >= 0)
          Main.instance.LoadArmorHead(i);
      }
      if (this.drawPlayer.body > 0 && this.drawPlayer.body < ArmorIDs.Body.Count)
        Main.instance.LoadArmorBody(this.drawPlayer.body);
      if (this.drawPlayer.legs > 0 && this.drawPlayer.legs < ArmorIDs.Legs.Count)
        Main.instance.LoadArmorLegs(this.drawPlayer.legs);
      if (this.drawPlayer.handon > (sbyte) 0 && (int) this.drawPlayer.handon < ArmorIDs.HandOn.Count)
        Main.instance.LoadAccHandsOn((int) this.drawPlayer.handon);
      if (this.drawPlayer.handoff > (sbyte) 0 && (int) this.drawPlayer.handoff < ArmorIDs.HandOff.Count)
        Main.instance.LoadAccHandsOff((int) this.drawPlayer.handoff);
      if (this.drawPlayer.back > (sbyte) 0 && (int) this.drawPlayer.back < ArmorIDs.Back.Count)
        Main.instance.LoadAccBack((int) this.drawPlayer.back);
      if (this.drawPlayer.front > (sbyte) 0 && (int) this.drawPlayer.front < ArmorIDs.Front.Count)
        Main.instance.LoadAccFront((int) this.drawPlayer.front);
      if (this.drawPlayer.shoe > (sbyte) 0 && (int) this.drawPlayer.shoe < ArmorIDs.Shoe.Count)
        Main.instance.LoadAccShoes((int) this.drawPlayer.shoe);
      if (this.drawPlayer.waist > (sbyte) 0 && (int) this.drawPlayer.waist < ArmorIDs.Waist.Count)
        Main.instance.LoadAccWaist((int) this.drawPlayer.waist);
      if (this.drawPlayer.shield > (sbyte) 0 && (int) this.drawPlayer.shield < ArmorIDs.Shield.Count)
        Main.instance.LoadAccShield((int) this.drawPlayer.shield);
      if (this.drawPlayer.neck > (sbyte) 0 && (int) this.drawPlayer.neck < ArmorIDs.Neck.Count)
        Main.instance.LoadAccNeck((int) this.drawPlayer.neck);
      if (this.drawPlayer.face > (sbyte) 0 && (int) this.drawPlayer.face < (int) ArmorIDs.Face.Count)
        Main.instance.LoadAccFace((int) this.drawPlayer.face);
      if (this.drawPlayer.balloon > (sbyte) 0 && (int) this.drawPlayer.balloon < ArmorIDs.Balloon.Count)
        Main.instance.LoadAccBalloon((int) this.drawPlayer.balloon);
      if (this.drawPlayer.backpack > (sbyte) 0 && (int) this.drawPlayer.backpack < ArmorIDs.Back.Count)
        Main.instance.LoadAccBack((int) this.drawPlayer.backpack);
      if (this.drawPlayer.tail > (sbyte) 0 && (int) this.drawPlayer.tail < ArmorIDs.Back.Count)
        Main.instance.LoadAccBack((int) this.drawPlayer.tail);
      if (this.drawPlayer.faceHead > (sbyte) 0 && (int) this.drawPlayer.faceHead < (int) ArmorIDs.Face.Count)
        Main.instance.LoadAccFace((int) this.drawPlayer.faceHead);
      if (this.drawPlayer.faceFlower > (sbyte) 0 && (int) this.drawPlayer.faceFlower < (int) ArmorIDs.Face.Count)
        Main.instance.LoadAccFace((int) this.drawPlayer.faceFlower);
      if (this.drawPlayer.balloonFront > (sbyte) 0 && (int) this.drawPlayer.balloonFront < ArmorIDs.Balloon.Count)
        Main.instance.LoadAccBalloon((int) this.drawPlayer.balloonFront);
      if (this.drawPlayer.beard > (sbyte) 0 && (int) this.drawPlayer.beard < (int) ArmorIDs.Beard.Count)
        Main.instance.LoadAccBeard((int) this.drawPlayer.beard);
      Main.instance.LoadHair(this.drawPlayer.hair);
      if (this.drawPlayer.eyebrellaCloud)
        Main.instance.LoadProjectile(238);
      if (this.drawPlayer.isHatRackDoll)
      {
        this.colorLegs = Color.Transparent;
        this.colorBodySkin = Color.Transparent;
        this.colorHead = Color.Transparent;
        this.colorHair = Color.Transparent;
        this.colorEyes = Color.Transparent;
        this.colorEyeWhites = Color.Transparent;
      }
      if (this.drawPlayer.isDisplayDollOrInanimate)
      {
        if (this.drawPlayer.isFullbright)
        {
          this.colorHead = Color.White;
          this.colorBodySkin = Color.White;
          this.colorLegs = Color.White;
          this.colorEyes = Color.White;
          this.colorEyeWhites = Color.White;
          this.colorArmorHead = Color.White;
          this.colorArmorBody = Color.White;
          this.colorArmorLegs = Color.White;
          this.colorDisplayDollSkin = PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR;
        }
        else
          this.colorDisplayDollSkin = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int) ((double) this.Position.X + (double) this.drawPlayer.width * 0.5) / 16, (int) ((double) this.Position.Y + (double) this.drawPlayer.height * 0.5) / 16, PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR), this.shadow);
      }
      if (!this.drawPlayer.isDisplayDollOrInanimate)
      {
        if ((this.drawPlayer.head == 78 || this.drawPlayer.head == 79 || this.drawPlayer.head == 80) && this.drawPlayer.body == 51 && this.drawPlayer.legs == 47)
        {
          float num30 = (float) ((double) Main.mouseTextColor / 200.0 - 0.30000001192092896);
          if ((double) this.shadow != 0.0)
            num30 = 0.0f;
          this.colorArmorHead.R = (byte) ((double) this.colorArmorHead.R * (double) num30);
          this.colorArmorHead.G = (byte) ((double) this.colorArmorHead.G * (double) num30);
          this.colorArmorHead.B = (byte) ((double) this.colorArmorHead.B * (double) num30);
          this.colorArmorBody.R = (byte) ((double) this.colorArmorBody.R * (double) num30);
          this.colorArmorBody.G = (byte) ((double) this.colorArmorBody.G * (double) num30);
          this.colorArmorBody.B = (byte) ((double) this.colorArmorBody.B * (double) num30);
          this.colorArmorLegs.R = (byte) ((double) this.colorArmorLegs.R * (double) num30);
          this.colorArmorLegs.G = (byte) ((double) this.colorArmorLegs.G * (double) num30);
          this.colorArmorLegs.B = (byte) ((double) this.colorArmorLegs.B * (double) num30);
        }
        if (this.drawPlayer.head == 193 && this.drawPlayer.body == 194 && this.drawPlayer.legs == 134)
        {
          float num31 = (float) (0.60000002384185791 - (double) this.drawPlayer.ghostFade * 0.30000001192092896);
          if ((double) this.shadow != 0.0)
            num31 = 0.0f;
          this.colorArmorHead.R = (byte) ((double) this.colorArmorHead.R * (double) num31);
          this.colorArmorHead.G = (byte) ((double) this.colorArmorHead.G * (double) num31);
          this.colorArmorHead.B = (byte) ((double) this.colorArmorHead.B * (double) num31);
          this.colorArmorBody.R = (byte) ((double) this.colorArmorBody.R * (double) num31);
          this.colorArmorBody.G = (byte) ((double) this.colorArmorBody.G * (double) num31);
          this.colorArmorBody.B = (byte) ((double) this.colorArmorBody.B * (double) num31);
          this.colorArmorLegs.R = (byte) ((double) this.colorArmorLegs.R * (double) num31);
          this.colorArmorLegs.G = (byte) ((double) this.colorArmorLegs.G * (double) num31);
          this.colorArmorLegs.B = (byte) ((double) this.colorArmorLegs.B * (double) num31);
        }
        if ((double) this.shadow > 0.0)
        {
          this.colorLegs = Color.Transparent;
          this.colorBodySkin = Color.Transparent;
          this.colorHead = Color.Transparent;
          this.colorHair = Color.Transparent;
          this.colorEyes = Color.Transparent;
          this.colorEyeWhites = Color.Transparent;
        }
      }
      float R = 1f;
      float G = 1f;
      float B = 1f;
      float A = 1f;
      if (this.drawPlayer.honey && Main.rand.Next(30) == 0 && (double) this.shadow == 0.0)
      {
        Dust dust1 = Dust.NewDustDirect(this.Position, this.drawPlayer.width, this.drawPlayer.height, 152, Alpha: 150);
        dust1.velocity.Y = 0.3f;
        dust1.velocity.X *= 0.1f;
        dust1.scale += (float) Main.rand.Next(3, 4) * 0.1f;
        dust1.alpha = 100;
        dust1.noGravity = true;
        dust1.velocity += this.drawPlayer.velocity * 0.1f;
        this.DustCache.Add(dust1.dustIndex);
      }
      if (this.drawPlayer.dryadWard && (double) this.drawPlayer.velocity.X != 0.0 && Main.rand.Next(4) == 0)
      {
        Dust dust2 = Dust.NewDustDirect(new Vector2(this.drawPlayer.position.X - 2f, (float) ((double) this.drawPlayer.position.Y + (double) this.drawPlayer.height - 2.0)), this.drawPlayer.width + 4, 4, 163, Alpha: 100, Scale: 1.5f);
        dust2.noGravity = true;
        dust2.noLight = true;
        dust2.velocity *= 0.0f;
        this.DustCache.Add(dust2.dustIndex);
      }
      if (this.drawPlayer.poisoned)
      {
        if (Main.rand.Next(50) == 0 && (double) this.shadow == 0.0)
        {
          Dust dust3 = Dust.NewDustDirect(this.Position, this.drawPlayer.width, this.drawPlayer.height, 46, Alpha: 150, Scale: 0.2f);
          dust3.noGravity = true;
          dust3.fadeIn = 1.9f;
          this.DustCache.Add(dust3.dustIndex);
        }
        R *= 0.65f;
        B *= 0.75f;
      }
      if (this.drawPlayer.venom)
      {
        if (Main.rand.Next(10) == 0 && (double) this.shadow == 0.0)
        {
          Dust dust4 = Dust.NewDustDirect(this.Position, this.drawPlayer.width, this.drawPlayer.height, 171, Alpha: 100, Scale: 0.5f);
          dust4.noGravity = true;
          dust4.fadeIn = 1.5f;
          this.DustCache.Add(dust4.dustIndex);
        }
        G *= 0.45f;
        R *= 0.75f;
      }
      if (this.drawPlayer.onFire)
      {
        if (Main.rand.Next(4) == 0 && (double) this.shadow == 0.0)
        {
          Dust dust5 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 6, this.drawPlayer.velocity.X * 0.4f, this.drawPlayer.velocity.Y * 0.4f, 100, Scale: 3f);
          dust5.noGravity = true;
          dust5.velocity *= 1.8f;
          dust5.velocity.Y -= 0.5f;
          this.DustCache.Add(dust5.dustIndex);
        }
        B *= 0.6f;
        G *= 0.7f;
      }
      if (this.drawPlayer.onFire3)
      {
        if (Main.rand.Next(4) == 0 && (double) this.shadow == 0.0)
        {
          Dust dust6 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 6, this.drawPlayer.velocity.X * 0.4f, this.drawPlayer.velocity.Y * 0.4f, 100, Scale: 3f);
          dust6.noGravity = true;
          dust6.velocity *= 1.8f;
          dust6.velocity.Y -= 0.5f;
          this.DustCache.Add(dust6.dustIndex);
        }
        B *= 0.6f;
        G *= 0.7f;
      }
      if (this.drawPlayer.dripping && (double) this.shadow == 0.0 && Main.rand.Next(4) != 0)
      {
        Vector2 position = this.Position;
        position.X -= 2f;
        position.Y -= 2f;
        if (Main.rand.Next(2) == 0)
        {
          Dust dust7 = Dust.NewDustDirect(position, this.drawPlayer.width + 4, this.drawPlayer.height + 2, 211, Alpha: 50, Scale: 0.8f);
          if (Main.rand.Next(2) == 0)
            dust7.alpha += 25;
          if (Main.rand.Next(2) == 0)
            dust7.alpha += 25;
          dust7.noLight = true;
          dust7.velocity *= 0.2f;
          dust7.velocity.Y += 0.2f;
          dust7.velocity += this.drawPlayer.velocity;
          this.DustCache.Add(dust7.dustIndex);
        }
        else
        {
          Dust dust8 = Dust.NewDustDirect(position, this.drawPlayer.width + 8, this.drawPlayer.height + 8, 211, Alpha: 50, Scale: 1.1f);
          if (Main.rand.Next(2) == 0)
            dust8.alpha += 25;
          if (Main.rand.Next(2) == 0)
            dust8.alpha += 25;
          dust8.noLight = true;
          dust8.noGravity = true;
          dust8.velocity *= 0.2f;
          ++dust8.velocity.Y;
          dust8.velocity += this.drawPlayer.velocity;
          this.DustCache.Add(dust8.dustIndex);
        }
      }
      if (this.drawPlayer.drippingSlime)
      {
        int Alpha = 175;
        Color newColor = new Color(0, 80, (int) byte.MaxValue, 100);
        if (Main.rand.Next(4) != 0 && (double) this.shadow == 0.0)
        {
          Vector2 position = this.Position;
          position.X -= 2f;
          position.Y -= 2f;
          if (Main.rand.Next(2) == 0)
          {
            Dust dust9 = Dust.NewDustDirect(position, this.drawPlayer.width + 4, this.drawPlayer.height + 2, 4, Alpha: Alpha, newColor: newColor, Scale: 1.4f);
            if (Main.rand.Next(2) == 0)
              dust9.alpha += 25;
            if (Main.rand.Next(2) == 0)
              dust9.alpha += 25;
            dust9.noLight = true;
            dust9.velocity *= 0.2f;
            dust9.velocity.Y += 0.2f;
            dust9.velocity += this.drawPlayer.velocity;
            this.DustCache.Add(dust9.dustIndex);
          }
        }
        R *= 0.8f;
        G *= 0.8f;
      }
      if (this.drawPlayer.drippingSparkleSlime)
      {
        int Alpha = 100;
        if (Main.rand.Next(4) != 0 && (double) this.shadow == 0.0)
        {
          Vector2 position = this.Position;
          position.X -= 2f;
          position.Y -= 2f;
          if (Main.rand.Next(4) == 0)
          {
            Color rgb = Main.hslToRgb((float) (0.699999988079071 + 0.20000000298023224 * (double) Main.rand.NextFloat()), 1f, 0.5f);
            rgb.A /= (byte) 2;
            Dust dust10 = Dust.NewDustDirect(position, this.drawPlayer.width + 4, this.drawPlayer.height + 2, 4, Alpha: Alpha, newColor: rgb, Scale: 0.65f);
            if (Main.rand.Next(2) == 0)
              dust10.alpha += 25;
            if (Main.rand.Next(2) == 0)
              dust10.alpha += 25;
            dust10.noLight = true;
            dust10.velocity *= 0.2f;
            dust10.velocity += this.drawPlayer.velocity * 0.7f;
            dust10.fadeIn = 0.8f;
            this.DustCache.Add(dust10.dustIndex);
          }
          if (Main.rand.Next(30) == 0)
          {
            Main.hslToRgb((float) (0.699999988079071 + 0.20000000298023224 * (double) Main.rand.NextFloat()), 1f, 0.5f).A /= (byte) 2;
            Dust dust11 = Dust.NewDustDirect(position, this.drawPlayer.width + 4, this.drawPlayer.height + 2, 43, Alpha: 254, newColor: new Color((int) sbyte.MaxValue, (int) sbyte.MaxValue, (int) sbyte.MaxValue, 0), Scale: 0.45f);
            dust11.noLight = true;
            dust11.velocity.X *= 0.0f;
            dust11.velocity *= 0.03f;
            dust11.fadeIn = 0.6f;
            this.DustCache.Add(dust11.dustIndex);
          }
        }
        R *= 0.94f;
        G *= 0.82f;
      }
      if (this.drawPlayer.ichor)
        B = 0.0f;
      if (this.drawPlayer.electrified && (double) this.shadow == 0.0 && Main.rand.Next(3) == 0)
      {
        Dust dust12 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 226, Alpha: 100, Scale: 0.5f);
        dust12.velocity *= 1.6f;
        --dust12.velocity.Y;
        dust12.position = Vector2.Lerp(dust12.position, this.drawPlayer.Center, 0.5f);
        this.DustCache.Add(dust12.dustIndex);
      }
      if (this.drawPlayer.burned)
      {
        if ((double) this.shadow == 0.0)
        {
          Dust dust13 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 6, this.drawPlayer.velocity.X * 0.4f, this.drawPlayer.velocity.Y * 0.4f, 100, Scale: 2f);
          dust13.noGravity = true;
          dust13.velocity *= 1.8f;
          dust13.velocity.Y -= 0.75f;
          this.DustCache.Add(dust13.dustIndex);
        }
        R = 1f;
        B *= 0.6f;
        G *= 0.7f;
      }
      if (this.drawPlayer.onFrostBurn)
      {
        if (Main.rand.Next(4) == 0 && (double) this.shadow == 0.0)
        {
          Dust dust14 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 135, this.drawPlayer.velocity.X * 0.4f, this.drawPlayer.velocity.Y * 0.4f, 100, Scale: 3f);
          dust14.noGravity = true;
          dust14.velocity *= 1.8f;
          dust14.velocity.Y -= 0.5f;
          this.DustCache.Add(dust14.dustIndex);
        }
        R *= 0.5f;
        G *= 0.7f;
      }
      if (this.drawPlayer.onFrostBurn2)
      {
        if (Main.rand.Next(4) == 0 && (double) this.shadow == 0.0)
        {
          Dust dust15 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 135, this.drawPlayer.velocity.X * 0.4f, this.drawPlayer.velocity.Y * 0.4f, 100, Scale: 3f);
          dust15.noGravity = true;
          dust15.velocity *= 1.8f;
          dust15.velocity.Y -= 0.5f;
          this.DustCache.Add(dust15.dustIndex);
        }
        R *= 0.5f;
        G *= 0.7f;
      }
      if (this.drawPlayer.onFire2)
      {
        if (Main.rand.Next(4) == 0 && (double) this.shadow == 0.0)
        {
          Dust dust16 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 75, this.drawPlayer.velocity.X * 0.4f, this.drawPlayer.velocity.Y * 0.4f, 100, Scale: 3f);
          dust16.noGravity = true;
          dust16.velocity *= 1.8f;
          dust16.velocity.Y -= 0.5f;
          this.DustCache.Add(dust16.dustIndex);
        }
        B *= 0.6f;
        G *= 0.7f;
      }
      if (this.drawPlayer.noItems)
      {
        G *= 0.8f;
        R *= 0.65f;
      }
      if (this.drawPlayer.blind)
      {
        G *= 0.65f;
        R *= 0.7f;
      }
      if (this.drawPlayer.bleed)
      {
        G *= 0.9f;
        B *= 0.9f;
        if (!this.drawPlayer.dead && Main.rand.Next(30) == 0 && (double) this.shadow == 0.0)
        {
          Dust dust17 = Dust.NewDustDirect(this.Position, this.drawPlayer.width, this.drawPlayer.height, 5);
          dust17.velocity.Y += 0.5f;
          dust17.velocity *= 0.25f;
          this.DustCache.Add(dust17.dustIndex);
        }
      }
      if ((double) this.shadow == 0.0 && this.drawPlayer.palladiumRegen && this.drawPlayer.statLife < this.drawPlayer.statLifeMax2 && Main.instance.IsActive && !Main.gamePaused && this.drawPlayer.miscCounter % 10 == 0 && (double) this.shadow == 0.0)
      {
        Vector2 Position;
        Position.X = this.Position.X + (float) Main.rand.Next(this.drawPlayer.width);
        Position.Y = this.Position.Y + (float) Main.rand.Next(this.drawPlayer.height);
        Position.X = (float) ((double) this.Position.X + (double) (this.drawPlayer.width / 2) - 6.0);
        Position.Y = (float) ((double) this.Position.Y + (double) (this.drawPlayer.height / 2) - 6.0);
        Position.X -= (float) Main.rand.Next(-10, 11);
        Position.Y -= (float) Main.rand.Next(-20, 21);
        this.GoreCache.Add(Gore.NewGore(Position, new Vector2((float) Main.rand.Next(-10, 11) * 0.1f, (float) Main.rand.Next(-20, -10) * 0.1f), 331, (float) Main.rand.Next(80, 120) * 0.01f));
      }
      if ((double) this.shadow == 0.0 && this.drawPlayer.loveStruck && Main.instance.IsActive && !Main.gamePaused && Main.rand.Next(5) == 0)
      {
        Vector2 vector2 = new Vector2((float) Main.rand.Next(-10, 11), (float) Main.rand.Next(-10, 11));
        vector2.Normalize();
        vector2.X *= 0.66f;
        int index = Gore.NewGore(this.Position + new Vector2((float) Main.rand.Next(this.drawPlayer.width + 1), (float) Main.rand.Next(this.drawPlayer.height + 1)), vector2 * (float) Main.rand.Next(3, 6) * 0.33f, 331, (float) Main.rand.Next(40, 121) * 0.01f);
        Main.gore[index].sticky = false;
        Main.gore[index].velocity *= 0.4f;
        Main.gore[index].velocity.Y -= 0.6f;
        this.GoreCache.Add(index);
      }
      if (this.drawPlayer.stinky && Main.instance.IsActive && !Main.gamePaused)
      {
        R *= 0.7f;
        B *= 0.55f;
        if (Main.rand.Next(5) == 0 && (double) this.shadow == 0.0)
        {
          Vector2 vector2_1 = new Vector2((float) Main.rand.Next(-10, 11), (float) Main.rand.Next(-10, 11));
          vector2_1.Normalize();
          vector2_1.X *= 0.66f;
          vector2_1.Y = Math.Abs(vector2_1.Y);
          Vector2 vector2_2 = vector2_1 * (float) Main.rand.Next(3, 5) * 0.25f;
          int index = Dust.NewDust(this.Position, this.drawPlayer.width, this.drawPlayer.height, 188, vector2_2.X, vector2_2.Y * 0.5f, 100, Scale: 1.5f);
          Main.dust[index].velocity *= 0.1f;
          Main.dust[index].velocity.Y -= 0.5f;
          this.DustCache.Add(index);
        }
      }
      if (this.drawPlayer.slowOgreSpit && Main.instance.IsActive && !Main.gamePaused)
      {
        R *= 0.6f;
        B *= 0.45f;
        if (Main.rand.Next(5) == 0 && (double) this.shadow == 0.0)
        {
          int Type = Utils.SelectRandom<int>(Main.rand, 4, 256);
          Dust dust18 = Main.dust[Dust.NewDust(this.Position, this.drawPlayer.width, this.drawPlayer.height, Type, Alpha: 100)];
          dust18.scale = (float) (0.800000011920929 + (double) Main.rand.NextFloat() * 0.60000002384185791);
          dust18.fadeIn = 0.5f;
          dust18.velocity *= 0.05f;
          dust18.noLight = true;
          if (dust18.type == 4)
            dust18.color = new Color(80, 170, 40, 120);
          this.DustCache.Add(dust18.dustIndex);
        }
        if (Main.rand.Next(5) == 0 && (double) this.shadow == 0.0)
        {
          int index = Gore.NewGore(this.Position + new Vector2(Main.rand.NextFloat(), Main.rand.NextFloat()) * this.drawPlayer.Size, Vector2.Zero, Utils.SelectRandom<int>(Main.rand, 1024, 1025, 1026), 0.65f);
          Main.gore[index].velocity *= 0.05f;
          this.GoreCache.Add(index);
        }
      }
      if (Main.instance.IsActive && !Main.gamePaused && (double) this.shadow == 0.0)
      {
        float num32 = (float) this.drawPlayer.miscCounter / 180f;
        float num33 = 0.0f;
        float num34 = 10f;
        int Type = 90;
        int num35 = 0;
        for (int index1 = 0; index1 < 3; ++index1)
        {
          switch (index1)
          {
            case 0:
              if (this.drawPlayer.nebulaLevelLife >= 1)
              {
                num33 = 6.28318548f / (float) this.drawPlayer.nebulaLevelLife;
                num35 = this.drawPlayer.nebulaLevelLife;
                goto default;
              }
              else
                break;
            case 1:
              if (this.drawPlayer.nebulaLevelMana >= 1)
              {
                num33 = -6.28318548f / (float) this.drawPlayer.nebulaLevelMana;
                num35 = this.drawPlayer.nebulaLevelMana;
                num32 = (float) -this.drawPlayer.miscCounter / 180f;
                num34 = 20f;
                Type = 88;
                goto default;
              }
              else
                break;
            case 2:
              if (this.drawPlayer.nebulaLevelDamage >= 1)
              {
                num33 = 6.28318548f / (float) this.drawPlayer.nebulaLevelDamage;
                num35 = this.drawPlayer.nebulaLevelDamage;
                num32 = (float) this.drawPlayer.miscCounter / 180f;
                num34 = 30f;
                Type = 86;
                goto default;
              }
              else
                break;
            default:
              for (int index2 = 0; index2 < num35; ++index2)
              {
                Dust dust19 = Dust.NewDustDirect(this.Position, this.drawPlayer.width, this.drawPlayer.height, Type, Alpha: 100, Scale: 1.5f);
                dust19.noGravity = true;
                dust19.velocity = Vector2.Zero;
                dust19.position = this.drawPlayer.Center + Vector2.UnitY * this.drawPlayer.gfxOffY + ((float) ((double) num32 * 6.2831854820251465 + (double) num33 * (double) index2)).ToRotationVector2() * num34;
                dust19.customData = (object) this.drawPlayer;
                this.DustCache.Add(dust19.dustIndex);
              }
              break;
          }
        }
      }
      if (this.drawPlayer.witheredArmor && Main.instance.IsActive && !Main.gamePaused)
      {
        G *= 0.5f;
        R *= 0.75f;
      }
      if (this.drawPlayer.witheredWeapon && this.drawPlayer.itemAnimation > 0 && this.heldItem.damage > 0 && Main.instance.IsActive && !Main.gamePaused && Main.rand.Next(3) == 0)
      {
        Dust dust20 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 272, Alpha: 50, Scale: 0.5f);
        dust20.velocity *= 1.6f;
        --dust20.velocity.Y;
        dust20.position = Vector2.Lerp(dust20.position, this.drawPlayer.Center, 0.5f);
        this.DustCache.Add(dust20.dustIndex);
      }
      int num36 = this.drawPlayer.shimmering ? 1 : 0;
      if ((double) R != 1.0 || (double) G != 1.0 || (double) B != 1.0 || (double) A != 1.0)
      {
        if (this.drawPlayer.onFire || this.drawPlayer.onFire2 || this.drawPlayer.onFrostBurn || this.drawPlayer.onFire3 || this.drawPlayer.onFrostBurn2)
        {
          this.colorEyeWhites = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
          this.colorEyes = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.eyeColor, this.shadow);
          this.colorHair = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.GetHairColor(false), this.shadow);
          this.colorHead = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.skinColor, this.shadow);
          this.colorBodySkin = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.skinColor, this.shadow);
          this.colorShirt = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.shirtColor, this.shadow);
          this.colorUnderShirt = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.underShirtColor, this.shadow);
          this.colorPants = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.pantsColor, this.shadow);
          this.colorLegs = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.skinColor, this.shadow);
          this.colorShoes = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.shoeColor, this.shadow);
          this.colorArmorHead = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
          this.colorArmorBody = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
          this.colorArmorLegs = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
          if (this.drawPlayer.isDisplayDollOrInanimate)
            this.colorDisplayDollSkin = this.drawPlayer.GetImmuneAlpha(PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR, this.shadow);
        }
        else
        {
          this.colorEyeWhites = Main.buffColor(this.colorEyeWhites, R, G, B, A);
          this.colorEyes = Main.buffColor(this.colorEyes, R, G, B, A);
          this.colorHair = Main.buffColor(this.colorHair, R, G, B, A);
          this.colorHead = Main.buffColor(this.colorHead, R, G, B, A);
          this.colorBodySkin = Main.buffColor(this.colorBodySkin, R, G, B, A);
          this.colorShirt = Main.buffColor(this.colorShirt, R, G, B, A);
          this.colorUnderShirt = Main.buffColor(this.colorUnderShirt, R, G, B, A);
          this.colorPants = Main.buffColor(this.colorPants, R, G, B, A);
          this.colorLegs = Main.buffColor(this.colorLegs, R, G, B, A);
          this.colorShoes = Main.buffColor(this.colorShoes, R, G, B, A);
          this.colorArmorHead = Main.buffColor(this.colorArmorHead, R, G, B, A);
          this.colorArmorBody = Main.buffColor(this.colorArmorBody, R, G, B, A);
          this.colorArmorLegs = Main.buffColor(this.colorArmorLegs, R, G, B, A);
          if (this.drawPlayer.isDisplayDollOrInanimate)
            this.colorDisplayDollSkin = Main.buffColor(PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR, R, G, B, A);
        }
      }
      if (this.drawPlayer.socialGhost)
      {
        this.colorEyeWhites = Color.Transparent;
        this.colorEyes = Color.Transparent;
        this.colorHair = Color.Transparent;
        this.colorHead = Color.Transparent;
        this.colorBodySkin = Color.Transparent;
        this.colorShirt = Color.Transparent;
        this.colorUnderShirt = Color.Transparent;
        this.colorPants = Color.Transparent;
        this.colorShoes = Color.Transparent;
        this.colorLegs = Color.Transparent;
        if ((int) this.colorArmorHead.A > (int) Main.gFade)
          this.colorArmorHead.A = Main.gFade;
        if ((int) this.colorArmorBody.A > (int) Main.gFade)
          this.colorArmorBody.A = Main.gFade;
        if ((int) this.colorArmorLegs.A > (int) Main.gFade)
          this.colorArmorLegs.A = Main.gFade;
        if (this.drawPlayer.isDisplayDollOrInanimate)
          this.colorDisplayDollSkin = Color.Transparent;
      }
      if (this.drawPlayer.socialIgnoreLight)
      {
        float num37 = 1f;
        this.colorEyeWhites = Color.White * num37;
        this.colorEyes = this.drawPlayer.eyeColor * num37;
        this.colorHair = GameShaders.Hair.GetColor((short) this.drawPlayer.hairDye, this.drawPlayer, Color.White);
        this.colorHead = this.drawPlayer.skinColor * num37;
        this.colorBodySkin = this.drawPlayer.skinColor * num37;
        this.colorShirt = this.drawPlayer.shirtColor * num37;
        this.colorUnderShirt = this.drawPlayer.underShirtColor * num37;
        this.colorPants = this.drawPlayer.pantsColor * num37;
        this.colorShoes = this.drawPlayer.shoeColor * num37;
        this.colorLegs = this.drawPlayer.skinColor * num37;
        this.colorArmorHead = Color.White;
        this.colorArmorBody = Color.White;
        this.colorArmorLegs = Color.White;
        if (this.drawPlayer.isDisplayDollOrInanimate)
          this.colorDisplayDollSkin = PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR * num37;
      }
      if ((double) this.drawPlayer.opacityForAnimation != 1.0)
      {
        this.shadow = 1f - this.drawPlayer.opacityForAnimation;
        float opacityForAnimation = this.drawPlayer.opacityForAnimation;
        float num38 = opacityForAnimation * opacityForAnimation;
        this.colorEyeWhites = Color.White * num38;
        this.colorEyes = this.drawPlayer.eyeColor * num38;
        this.colorHair = GameShaders.Hair.GetColor((short) this.drawPlayer.hairDye, this.drawPlayer, Color.White) * num38;
        this.colorHead = this.drawPlayer.skinColor * num38;
        this.colorBodySkin = this.drawPlayer.skinColor * num38;
        this.colorShirt = this.drawPlayer.shirtColor * num38;
        this.colorUnderShirt = this.drawPlayer.underShirtColor * num38;
        this.colorPants = this.drawPlayer.pantsColor * num38;
        this.colorShoes = this.drawPlayer.shoeColor * num38;
        this.colorLegs = this.drawPlayer.skinColor * num38;
        this.colorArmorHead = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
        this.colorArmorBody = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
        this.colorArmorLegs = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
        if (this.drawPlayer.isDisplayDollOrInanimate)
          this.colorDisplayDollSkin = PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR * num38;
      }
      this.stealth = 1f;
      if (this.heldItem.type == 3106)
      {
        float num39 = this.drawPlayer.stealth;
        if ((double) num39 < 0.03)
          num39 = 0.03f;
        float num40 = (float) ((1.0 + (double) num39 * 10.0) / 11.0);
        if ((double) num39 < 0.0)
          num39 = 0.0f;
        if ((double) num39 >= 1.0 - (double) this.shadow && (double) this.shadow > 0.0)
          num39 = this.shadow * 0.5f;
        this.stealth = num40;
        this.colorArmorHead = new Color((int) (byte) ((double) this.colorArmorHead.R * (double) num39), (int) (byte) ((double) this.colorArmorHead.G * (double) num39), (int) (byte) ((double) this.colorArmorHead.B * (double) num40), (int) (byte) ((double) this.colorArmorHead.A * (double) num39));
        this.colorArmorBody = new Color((int) (byte) ((double) this.colorArmorBody.R * (double) num39), (int) (byte) ((double) this.colorArmorBody.G * (double) num39), (int) (byte) ((double) this.colorArmorBody.B * (double) num40), (int) (byte) ((double) this.colorArmorBody.A * (double) num39));
        this.colorArmorLegs = new Color((int) (byte) ((double) this.colorArmorLegs.R * (double) num39), (int) (byte) ((double) this.colorArmorLegs.G * (double) num39), (int) (byte) ((double) this.colorArmorLegs.B * (double) num40), (int) (byte) ((double) this.colorArmorLegs.A * (double) num39));
        float scale = num39 * num39;
        this.colorEyeWhites = Color.Multiply(this.colorEyeWhites, scale);
        this.colorEyes = Color.Multiply(this.colorEyes, scale);
        this.colorHair = Color.Multiply(this.colorHair, scale);
        this.colorHead = Color.Multiply(this.colorHead, scale);
        this.colorBodySkin = Color.Multiply(this.colorBodySkin, scale);
        this.colorShirt = Color.Multiply(this.colorShirt, scale);
        this.colorUnderShirt = Color.Multiply(this.colorUnderShirt, scale);
        this.colorPants = Color.Multiply(this.colorPants, scale);
        this.colorShoes = Color.Multiply(this.colorShoes, scale);
        this.colorLegs = Color.Multiply(this.colorLegs, scale);
        this.colorMount = Color.Multiply(this.colorMount, scale);
        this.headGlowColor = Color.Multiply(this.headGlowColor, scale);
        this.bodyGlowColor = Color.Multiply(this.bodyGlowColor, scale);
        this.armGlowColor = Color.Multiply(this.armGlowColor, scale);
        this.legsGlowColor = Color.Multiply(this.legsGlowColor, scale);
        if (this.drawPlayer.isDisplayDollOrInanimate)
          this.colorDisplayDollSkin = Color.Multiply(this.colorDisplayDollSkin, scale);
      }
      else if (this.drawPlayer.shroomiteStealth)
      {
        float num41 = this.drawPlayer.stealth;
        if ((double) num41 < 0.03)
          num41 = 0.03f;
        float num42 = (float) ((1.0 + (double) num41 * 10.0) / 11.0);
        if ((double) num41 < 0.0)
          num41 = 0.0f;
        if ((double) num41 >= 1.0 - (double) this.shadow && (double) this.shadow > 0.0)
          num41 = this.shadow * 0.5f;
        this.stealth = num42;
        this.colorArmorHead = new Color((int) (byte) ((double) this.colorArmorHead.R * (double) num41), (int) (byte) ((double) this.colorArmorHead.G * (double) num41), (int) (byte) ((double) this.colorArmorHead.B * (double) num42), (int) (byte) ((double) this.colorArmorHead.A * (double) num41));
        this.colorArmorBody = new Color((int) (byte) ((double) this.colorArmorBody.R * (double) num41), (int) (byte) ((double) this.colorArmorBody.G * (double) num41), (int) (byte) ((double) this.colorArmorBody.B * (double) num42), (int) (byte) ((double) this.colorArmorBody.A * (double) num41));
        this.colorArmorLegs = new Color((int) (byte) ((double) this.colorArmorLegs.R * (double) num41), (int) (byte) ((double) this.colorArmorLegs.G * (double) num41), (int) (byte) ((double) this.colorArmorLegs.B * (double) num42), (int) (byte) ((double) this.colorArmorLegs.A * (double) num41));
        float scale = num41 * num41;
        this.colorEyeWhites = Color.Multiply(this.colorEyeWhites, scale);
        this.colorEyes = Color.Multiply(this.colorEyes, scale);
        this.colorHair = Color.Multiply(this.colorHair, scale);
        this.colorHead = Color.Multiply(this.colorHead, scale);
        this.colorBodySkin = Color.Multiply(this.colorBodySkin, scale);
        this.colorShirt = Color.Multiply(this.colorShirt, scale);
        this.colorUnderShirt = Color.Multiply(this.colorUnderShirt, scale);
        this.colorPants = Color.Multiply(this.colorPants, scale);
        this.colorShoes = Color.Multiply(this.colorShoes, scale);
        this.colorLegs = Color.Multiply(this.colorLegs, scale);
        this.colorMount = Color.Multiply(this.colorMount, scale);
        this.headGlowColor = Color.Multiply(this.headGlowColor, scale);
        this.bodyGlowColor = Color.Multiply(this.bodyGlowColor, scale);
        this.armGlowColor = Color.Multiply(this.armGlowColor, scale);
        this.legsGlowColor = Color.Multiply(this.legsGlowColor, scale);
        if (this.drawPlayer.isDisplayDollOrInanimate)
          this.colorDisplayDollSkin = Color.Multiply(this.colorDisplayDollSkin, scale);
      }
      else if (this.drawPlayer.setVortex)
      {
        float num43 = this.drawPlayer.stealth;
        if ((double) num43 < 0.03)
          num43 = 0.03f;
        if ((double) num43 < 0.0)
          num43 = 0.0f;
        if ((double) num43 >= 1.0 - (double) this.shadow && (double) this.shadow > 0.0)
          num43 = this.shadow * 0.5f;
        this.stealth = num43;
        Color secondColor = new Color(Vector4.Lerp(Vector4.One, new Vector4(0.0f, 0.12f, 0.16f, 0.0f), 1f - num43));
        this.colorArmorHead = this.colorArmorHead.MultiplyRGBA(secondColor);
        this.colorArmorBody = this.colorArmorBody.MultiplyRGBA(secondColor);
        this.colorArmorLegs = this.colorArmorLegs.MultiplyRGBA(secondColor);
        float scale = num43 * num43;
        this.colorEyeWhites = Color.Multiply(this.colorEyeWhites, scale);
        this.colorEyes = Color.Multiply(this.colorEyes, scale);
        this.colorHair = Color.Multiply(this.colorHair, scale);
        this.colorHead = Color.Multiply(this.colorHead, scale);
        this.colorBodySkin = Color.Multiply(this.colorBodySkin, scale);
        this.colorShirt = Color.Multiply(this.colorShirt, scale);
        this.colorUnderShirt = Color.Multiply(this.colorUnderShirt, scale);
        this.colorPants = Color.Multiply(this.colorPants, scale);
        this.colorShoes = Color.Multiply(this.colorShoes, scale);
        this.colorLegs = Color.Multiply(this.colorLegs, scale);
        this.colorMount = Color.Multiply(this.colorMount, scale);
        this.headGlowColor = Color.Multiply(this.headGlowColor, scale);
        this.bodyGlowColor = Color.Multiply(this.bodyGlowColor, scale);
        this.armGlowColor = Color.Multiply(this.armGlowColor, scale);
        this.legsGlowColor = Color.Multiply(this.legsGlowColor, scale);
        if (this.drawPlayer.isDisplayDollOrInanimate)
          this.colorDisplayDollSkin = Color.Multiply(this.colorDisplayDollSkin, scale);
      }
      if (this.hideEntirePlayer)
      {
        this.stealth = 1f;
        Color transparent = Color.Transparent;
        this.colorArmorHead = transparent;
        this.colorArmorBody = transparent;
        this.colorArmorLegs = transparent;
        this.colorEyeWhites = transparent;
        this.colorEyes = transparent;
        this.colorHair = transparent;
        this.colorHead = transparent;
        this.colorBodySkin = transparent;
        this.colorShirt = transparent;
        this.colorUnderShirt = transparent;
        this.colorPants = transparent;
        this.colorShoes = transparent;
        this.colorLegs = transparent;
        this.headGlowColor = transparent;
        this.bodyGlowColor = transparent;
        this.armGlowColor = transparent;
        this.legsGlowColor = transparent;
        this.colorDisplayDollSkin = transparent;
      }
      if ((double) this.drawPlayer.gravDir == 1.0)
      {
        if (this.drawPlayer.direction == 1)
        {
          this.playerEffect = SpriteEffects.None;
          this.itemEffect = SpriteEffects.None;
        }
        else
        {
          this.playerEffect = SpriteEffects.FlipHorizontally;
          this.itemEffect = SpriteEffects.FlipHorizontally;
        }
        if (!this.drawPlayer.dead)
        {
          this.drawPlayer.legPosition.Y = 0.0f;
          this.drawPlayer.headPosition.Y = 0.0f;
          this.drawPlayer.bodyPosition.Y = 0.0f;
        }
      }
      else
      {
        if (this.drawPlayer.direction == 1)
        {
          this.playerEffect = SpriteEffects.FlipVertically;
          this.itemEffect = SpriteEffects.FlipVertically;
        }
        else
        {
          this.playerEffect = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
          this.itemEffect = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
        }
        if (!this.drawPlayer.dead)
        {
          this.drawPlayer.legPosition.Y = 6f;
          this.drawPlayer.headPosition.Y = 6f;
          this.drawPlayer.bodyPosition.Y = 6f;
        }
      }
      switch (this.heldItem.type)
      {
        case 3182:
        case 3184:
        case 3185:
        case 3782:
          this.itemEffect ^= SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
          break;
        case 5118:
          if ((double) player.gravDir < 0.0)
          {
            this.itemEffect ^= SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
            break;
          }
          break;
      }
      this.legVect = new Vector2((float) this.drawPlayer.legFrame.Width * 0.5f, (float) this.drawPlayer.legFrame.Height * 0.75f);
      this.bodyVect = new Vector2((float) this.drawPlayer.legFrame.Width * 0.5f, (float) this.drawPlayer.legFrame.Height * 0.5f);
      this.headVect = new Vector2((float) this.drawPlayer.legFrame.Width * 0.5f, (float) this.drawPlayer.legFrame.Height * 0.4f);
      if ((this.drawPlayer.merman || this.drawPlayer.forceMerman) && !this.drawPlayer.hideMerman)
      {
        this.drawPlayer.headRotation = (float) ((double) this.drawPlayer.velocity.Y * (double) this.drawPlayer.direction * 0.10000000149011612);
        if ((double) this.drawPlayer.headRotation < -0.3)
          this.drawPlayer.headRotation = -0.3f;
        if ((double) this.drawPlayer.headRotation > 0.3)
          this.drawPlayer.headRotation = 0.3f;
      }
      else if (!this.drawPlayer.dead)
        this.drawPlayer.headRotation = 0.0f;
      Rectangle bodyFrame1 = this.drawPlayer.bodyFrame;
      Rectangle bodyFrame2 = this.drawPlayer.bodyFrame;
      bodyFrame2.Y -= 336;
      if (bodyFrame2.Y < 0)
        bodyFrame2.Y = 0;
      this.hairFrontFrame = bodyFrame2;
      this.hairBackFrame = bodyFrame2;
      if (this.hideHair)
      {
        this.hairFrontFrame.Height = 0;
        this.hairBackFrame.Height = 0;
      }
      else if (this.backHairDraw)
        this.hairFrontFrame.Height = 26;
      this.hidesTopSkin = this.drawPlayer.body == 82 || this.drawPlayer.body == 83 || this.drawPlayer.body == 93 || this.drawPlayer.body == 21 || this.drawPlayer.body == 22;
      this.hidesBottomSkin = this.drawPlayer.body == 93 || this.drawPlayer.legs == 20 || this.drawPlayer.legs == 21;
      this.drawFloatingTube = this.drawPlayer.hasFloatingTube && !this.hideEntirePlayer;
      this.drawUnicornHorn = this.drawPlayer.hasUnicornHorn;
      this.drawAngelHalo = this.drawPlayer.hasAngelHalo;
      this.drawFrontAccInNeckAccLayer = false;
      if (this.drawPlayer.bodyFrame.Y / this.drawPlayer.bodyFrame.Height == 5)
        this.drawFrontAccInNeckAccLayer = this.drawPlayer.front > (sbyte) 0 && (int) this.drawPlayer.front < ArmorIDs.Front.Count && ArmorIDs.Front.Sets.DrawsInNeckLayer[(int) this.drawPlayer.front];
      this.hairOffset = this.drawPlayer.GetHairDrawOffset(this.drawPlayer.hair, this.hatHair);
      this.helmetOffset = this.drawPlayer.GetHelmetDrawOffset();
      this.legsOffset = this.drawPlayer.GetLegsDrawOffset();
      this.CreateCompositeData();
    }

    private void AdjustmentsForWolfMount()
    {
      this.hideEntirePlayer = true;
      this.weaponDrawOrder = WeaponDrawOrder.BehindBackArm;
      Vector2 vector2 = this.Position + new Vector2((float) (10 + this.drawPlayer.direction * 14), 12f);
      this.Position.X -= (float) (this.drawPlayer.direction * 10);
      bool flag1 = this.drawPlayer.heldProj != -1 || this.heldItem.useStyle == 5;
      int num = this.heldItem.useStyle == 2 ? 1 : 0;
      bool flag2 = this.heldItem.useStyle == 9;
      bool flag3 = this.drawPlayer.itemAnimation > 0;
      bool flag4 = this.heldItem.fishingPole != 0;
      bool flag5 = this.heldItem.useStyle == 14;
      bool flag6 = this.heldItem.useStyle == 8;
      bool flag7 = this.heldItem.holdStyle == 1;
      bool flag8 = this.heldItem.holdStyle == 2;
      bool flag9 = this.heldItem.holdStyle == 5;
      if (num != 0)
      {
        this.ItemLocation += new Vector2((float) (this.drawPlayer.direction * 14), -4f);
      }
      else
      {
        if (flag4)
          return;
        if (flag2)
          this.ItemLocation += flag3 ? new Vector2((float) (this.drawPlayer.direction * 18), -4f) : new Vector2((float) (this.drawPlayer.direction * 14), -18f);
        else if (flag9)
          this.ItemLocation += new Vector2((float) (this.drawPlayer.direction * 17), -8f);
        else if (flag7 && this.drawPlayer.itemAnimation == 0)
          this.ItemLocation += new Vector2((float) (this.drawPlayer.direction * 14), -6f);
        else if (flag8 && this.drawPlayer.itemAnimation == 0)
          this.ItemLocation += new Vector2((float) (this.drawPlayer.direction * 17), 4f);
        else if (flag6)
          this.ItemLocation = vector2 + new Vector2((float) (this.drawPlayer.direction * 12), 2f);
        else if (flag5)
          this.ItemLocation += new Vector2((float) (this.drawPlayer.direction * 5), -2f);
        else if (flag1)
          this.ItemLocation += new Vector2((float) (this.drawPlayer.direction * 4), -4f);
        else
          this.ItemLocation = vector2;
      }
    }

    private void CreateCompositeData()
    {
      this.frontShoulderOffset = Vector2.Zero;
      this.backShoulderOffset = Vector2.Zero;
      this.usesCompositeTorso = this.drawPlayer.body > 0 && this.drawPlayer.body < ArmorIDs.Body.Count && ArmorIDs.Body.Sets.UsesNewFramingCode[this.drawPlayer.body];
      this.usesCompositeFrontHandAcc = this.drawPlayer.handon > (sbyte) 0 && (int) this.drawPlayer.handon < ArmorIDs.HandOn.Count && ArmorIDs.HandOn.Sets.UsesNewFramingCode[(int) this.drawPlayer.handon];
      this.usesCompositeBackHandAcc = this.drawPlayer.handoff > (sbyte) 0 && (int) this.drawPlayer.handoff < ArmorIDs.HandOff.Count && ArmorIDs.HandOff.Sets.UsesNewFramingCode[(int) this.drawPlayer.handoff];
      if (this.drawPlayer.body < 1)
        this.usesCompositeTorso = true;
      if (!this.usesCompositeTorso)
        return;
      Point pt1 = new Point(1, 1);
      Point pt2 = new Point(0, 1);
      Point pt3 = new Point();
      Point frameIndex1 = new Point();
      Point frameIndex2 = new Point();
      int targetFrameNumber = this.drawPlayer.bodyFrame.Y / this.drawPlayer.bodyFrame.Height;
      this.compShoulderOverFrontArm = true;
      this.hideCompositeShoulders = false;
      bool flag1 = true;
      if (this.drawPlayer.body > 0)
        flag1 = ArmorIDs.Body.Sets.showsShouldersWhileJumping[this.drawPlayer.body];
      bool flag2 = false;
      if (this.drawPlayer.handon > (sbyte) 0)
        flag2 = ArmorIDs.HandOn.Sets.UsesOldFramingTexturesForWalking[(int) this.drawPlayer.handon];
      bool flag3 = !flag2;
      switch (targetFrameNumber)
      {
        case 0:
          frameIndex2.X = 2;
          flag3 = true;
          break;
        case 1:
          frameIndex2.X = 3;
          this.compShoulderOverFrontArm = false;
          flag3 = true;
          break;
        case 2:
          frameIndex2.X = 4;
          this.compShoulderOverFrontArm = false;
          flag3 = true;
          break;
        case 3:
          frameIndex2.X = 5;
          this.compShoulderOverFrontArm = true;
          flag3 = true;
          break;
        case 4:
          frameIndex2.X = 6;
          this.compShoulderOverFrontArm = true;
          flag3 = true;
          break;
        case 5:
          frameIndex2.X = 2;
          frameIndex2.Y = 1;
          pt3.X = 1;
          this.compShoulderOverFrontArm = false;
          flag3 = true;
          if (!flag1)
          {
            this.hideCompositeShoulders = true;
            break;
          }
          break;
        case 6:
          frameIndex2.X = 3;
          frameIndex2.Y = 1;
          break;
        case 7:
        case 8:
        case 9:
        case 10:
          frameIndex2.X = 4;
          frameIndex2.Y = 1;
          break;
        case 11:
        case 12:
        case 13:
          frameIndex2.X = 3;
          frameIndex2.Y = 1;
          break;
        case 14:
          frameIndex2.X = 5;
          frameIndex2.Y = 1;
          break;
        case 15:
        case 16:
          frameIndex2.X = 6;
          frameIndex2.Y = 1;
          break;
        case 17:
          frameIndex2.X = 5;
          frameIndex2.Y = 1;
          break;
        case 18:
        case 19:
          frameIndex2.X = 3;
          frameIndex2.Y = 1;
          break;
      }
      this.CreateCompositeData_DetermineShoulderOffsets(this.drawPlayer.body, targetFrameNumber);
      this.backShoulderOffset *= new Vector2((float) this.drawPlayer.direction, this.drawPlayer.gravDir);
      this.frontShoulderOffset *= new Vector2((float) this.drawPlayer.direction, this.drawPlayer.gravDir);
      if (this.drawPlayer.body > 0 && ArmorIDs.Body.Sets.shouldersAreAlwaysInTheBack[this.drawPlayer.body])
        this.compShoulderOverFrontArm = false;
      this.usesCompositeFrontHandAcc = flag3;
      frameIndex1.X = frameIndex2.X;
      frameIndex1.Y = frameIndex2.Y + 2;
      this.UpdateCompositeArm(this.drawPlayer.compositeFrontArm, ref this.compositeFrontArmRotation, ref frameIndex2, 7);
      this.UpdateCompositeArm(this.drawPlayer.compositeBackArm, ref this.compositeBackArmRotation, ref frameIndex1, 8);
      if (!this.drawPlayer.Male)
      {
        pt1.Y += 2;
        pt2.Y += 2;
        pt3.Y += 2;
      }
      this.compBackShoulderFrame = this.CreateCompositeFrameRect(pt1);
      this.compFrontShoulderFrame = this.CreateCompositeFrameRect(pt2);
      this.compBackArmFrame = this.CreateCompositeFrameRect(frameIndex1);
      this.compFrontArmFrame = this.CreateCompositeFrameRect(frameIndex2);
      this.compTorsoFrame = this.CreateCompositeFrameRect(pt3);
    }

    private void CreateCompositeData_DetermineShoulderOffsets(int armor, int targetFrameNumber)
    {
      int num = 0;
      switch (armor)
      {
        case 55:
          num = 1;
          break;
        case 71:
          num = 2;
          break;
        case 101:
          num = 6;
          break;
        case 183:
          num = 4;
          break;
        case 201:
          num = 5;
          break;
        case 204:
          num = 3;
          break;
        case 207:
          num = 7;
          break;
      }
      switch (num)
      {
        case 1:
          switch (targetFrameNumber)
          {
            case 6:
              this.frontShoulderOffset.X = -2f;
              return;
            case 7:
            case 8:
            case 9:
            case 10:
              this.frontShoulderOffset.X = -4f;
              return;
            case 11:
            case 12:
            case 13:
            case 14:
              this.frontShoulderOffset.X = -2f;
              return;
            case 15:
              return;
            case 16:
              return;
            case 17:
              return;
            case 18:
            case 19:
              this.frontShoulderOffset.X = -2f;
              return;
            default:
              return;
          }
        case 2:
          switch (targetFrameNumber)
          {
            case 6:
              this.frontShoulderOffset.X = -2f;
              return;
            case 7:
            case 8:
            case 9:
            case 10:
              this.frontShoulderOffset.X = -4f;
              return;
            case 11:
            case 12:
            case 13:
            case 14:
              this.frontShoulderOffset.X = -2f;
              return;
            case 15:
              return;
            case 16:
              return;
            case 17:
              return;
            case 18:
            case 19:
              this.frontShoulderOffset.X = -2f;
              return;
            default:
              return;
          }
        case 3:
          switch (targetFrameNumber)
          {
            case 7:
            case 8:
            case 9:
              this.frontShoulderOffset.X = -2f;
              return;
            case 15:
            case 16:
            case 17:
              this.frontShoulderOffset.X = 2f;
              return;
            default:
              return;
          }
        case 4:
          switch (targetFrameNumber)
          {
            case 6:
              this.frontShoulderOffset.X = -2f;
              return;
            case 7:
            case 8:
            case 9:
            case 10:
              this.frontShoulderOffset.X = -4f;
              return;
            case 11:
            case 12:
            case 13:
              this.frontShoulderOffset.X = -2f;
              return;
            case 14:
              return;
            case 15:
            case 16:
              this.frontShoulderOffset.X = 2f;
              return;
            case 17:
              return;
            case 18:
            case 19:
              this.frontShoulderOffset.X = -2f;
              return;
            default:
              return;
          }
        case 5:
          switch (targetFrameNumber)
          {
            case 7:
            case 8:
            case 9:
            case 10:
              this.frontShoulderOffset.X = -2f;
              return;
            case 15:
            case 16:
              this.frontShoulderOffset.X = 2f;
              return;
            default:
              return;
          }
        case 6:
          switch (targetFrameNumber)
          {
            case 7:
            case 8:
            case 9:
            case 10:
              this.frontShoulderOffset.X = -2f;
              return;
            case 14:
            case 15:
            case 16:
            case 17:
              this.frontShoulderOffset.X = 2f;
              return;
            default:
              return;
          }
        case 7:
          switch (targetFrameNumber)
          {
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
              this.frontShoulderOffset.X = -2f;
              return;
            case 11:
            case 12:
            case 13:
            case 14:
              this.frontShoulderOffset.X = -2f;
              return;
            case 15:
              return;
            case 16:
              return;
            case 17:
              return;
            case 18:
            case 19:
              this.frontShoulderOffset.X = -2f;
              return;
            default:
              return;
          }
      }
    }

    private Rectangle CreateCompositeFrameRect(Point pt) => new Rectangle(pt.X * 40, pt.Y * 56, 40, 56);

    private void UpdateCompositeArm(
      Player.CompositeArmData data,
      ref float rotation,
      ref Point frameIndex,
      int targetX)
    {
      if (data.enabled)
      {
        rotation = data.rotation;
        switch (data.stretch)
        {
          case Player.CompositeArmStretchAmount.Full:
            frameIndex.X = targetX;
            frameIndex.Y = 0;
            break;
          case Player.CompositeArmStretchAmount.None:
            frameIndex.X = targetX;
            frameIndex.Y = 3;
            break;
          case Player.CompositeArmStretchAmount.Quarter:
            frameIndex.X = targetX;
            frameIndex.Y = 2;
            break;
          case Player.CompositeArmStretchAmount.ThreeQuarters:
            frameIndex.X = targetX;
            frameIndex.Y = 1;
            break;
        }
      }
      else
        rotation = 0.0f;
    }
  }
}
