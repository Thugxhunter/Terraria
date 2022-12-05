// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.PlayerDrawHeadSet
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.ID;

namespace Terraria.DataStructures
{
  public struct PlayerDrawHeadSet
  {
    public List<Terraria.DataStructures.DrawData> DrawData;
    public List<int> Dust;
    public List<int> Gore;
    public Player drawPlayer;
    public int cHead;
    public int cFace;
    public int cFaceHead;
    public int cFaceFlower;
    public int cUnicornHorn;
    public int cAngelHalo;
    public int cBeard;
    public int skinVar;
    public int hairShaderPacked;
    public int skinDyePacked;
    public float scale;
    public Color colorEyeWhites;
    public Color colorEyes;
    public Color colorHair;
    public Color colorHead;
    public Color colorArmorHead;
    public Color colorDisplayDollSkin;
    public SpriteEffects playerEffect;
    public Vector2 headVect;
    public Rectangle bodyFrameMemory;
    public bool fullHair;
    public bool hatHair;
    public bool hideHair;
    public bool helmetIsTall;
    public bool helmetIsOverFullHair;
    public bool helmetIsNormal;
    public bool drawUnicornHorn;
    public bool drawAngelHalo;
    public Vector2 Position;
    public Vector2 hairOffset;
    public Vector2 helmetOffset;

    public Rectangle HairFrame
    {
      get
      {
        Rectangle bodyFrameMemory = this.bodyFrameMemory;
        --bodyFrameMemory.Height;
        return bodyFrameMemory;
      }
    }

    public void BoringSetup(
      Player drawPlayer2,
      List<Terraria.DataStructures.DrawData> drawData,
      List<int> dust,
      List<int> gore,
      float X,
      float Y,
      float Alpha,
      float Scale)
    {
      this.DrawData = drawData;
      this.Dust = dust;
      this.Gore = gore;
      this.drawPlayer = drawPlayer2;
      this.Position = this.drawPlayer.position;
      this.cHead = 0;
      this.cFace = 0;
      this.cUnicornHorn = 0;
      this.cAngelHalo = 0;
      this.cBeard = 0;
      this.drawUnicornHorn = false;
      this.drawAngelHalo = false;
      this.skinVar = this.drawPlayer.skinVariant;
      this.hairShaderPacked = PlayerDrawHelper.PackShader((int) this.drawPlayer.hairDye, PlayerDrawHelper.ShaderConfiguration.HairShader);
      if (this.drawPlayer.head == 0 && this.drawPlayer.hairDye == (byte) 0)
        this.hairShaderPacked = PlayerDrawHelper.PackShader(1, PlayerDrawHelper.ShaderConfiguration.HairShader);
      this.skinDyePacked = this.drawPlayer.skinDyePacked;
      if (this.drawPlayer.face > (sbyte) 0 && (int) this.drawPlayer.face < (int) ArmorIDs.Face.Count)
        Main.instance.LoadAccFace((int) this.drawPlayer.face);
      this.cHead = this.drawPlayer.cHead;
      this.cFace = this.drawPlayer.cFace;
      this.cFaceHead = this.drawPlayer.cFaceHead;
      this.cFaceFlower = this.drawPlayer.cFaceFlower;
      this.cUnicornHorn = this.drawPlayer.cUnicornHorn;
      this.cAngelHalo = this.drawPlayer.cAngelHalo;
      this.cBeard = this.drawPlayer.cBeard;
      this.drawUnicornHorn = this.drawPlayer.hasUnicornHorn;
      this.drawAngelHalo = this.drawPlayer.hasAngelHalo;
      Main.instance.LoadHair(this.drawPlayer.hair);
      this.scale = Scale;
      this.colorEyeWhites = Main.quickAlpha(Color.White, Alpha);
      this.colorEyes = Main.quickAlpha(this.drawPlayer.eyeColor, Alpha);
      this.colorHair = Main.quickAlpha(this.drawPlayer.GetHairColor(false), Alpha);
      this.colorHead = Main.quickAlpha(this.drawPlayer.skinColor, Alpha);
      this.colorArmorHead = Main.quickAlpha(Color.White, Alpha);
      this.colorDisplayDollSkin = !this.drawPlayer.isDisplayDollOrInanimate ? this.colorHead : Main.quickAlpha(PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR, Alpha);
      this.playerEffect = SpriteEffects.None;
      if (this.drawPlayer.direction < 0)
        this.playerEffect = SpriteEffects.FlipHorizontally;
      this.headVect = new Vector2((float) this.drawPlayer.legFrame.Width * 0.5f, (float) this.drawPlayer.legFrame.Height * 0.4f);
      this.bodyFrameMemory = this.drawPlayer.bodyFrame;
      this.bodyFrameMemory.Y = 0;
      this.Position = Main.screenPosition;
      this.Position.X += X;
      this.Position.Y += Y;
      this.Position.X -= 6f;
      this.Position.Y -= 4f;
      this.Position.Y -= (float) this.drawPlayer.HeightMapOffset;
      if (this.drawPlayer.head > 0 && this.drawPlayer.head < ArmorIDs.Head.Count)
      {
        Main.instance.LoadArmorHead(this.drawPlayer.head);
        int i = ArmorIDs.Head.Sets.FrontToBackID[this.drawPlayer.head];
        if (i >= 0)
          Main.instance.LoadArmorHead(i);
      }
      if (this.drawPlayer.face > (sbyte) 0 && (int) this.drawPlayer.face < (int) ArmorIDs.Face.Count)
        Main.instance.LoadAccFace((int) this.drawPlayer.face);
      if (this.drawPlayer.faceHead > (sbyte) 0 && (int) this.drawPlayer.faceHead < (int) ArmorIDs.Face.Count)
        Main.instance.LoadAccFace((int) this.drawPlayer.faceHead);
      if (this.drawPlayer.faceFlower > (sbyte) 0 && (int) this.drawPlayer.faceFlower < (int) ArmorIDs.Face.Count)
        Main.instance.LoadAccFace((int) this.drawPlayer.faceFlower);
      if (this.drawPlayer.beard > (sbyte) 0 && (int) this.drawPlayer.beard < (int) ArmorIDs.Beard.Count)
        Main.instance.LoadAccBeard((int) this.drawPlayer.beard);
      this.drawPlayer.GetHairSettings(out this.fullHair, out this.hatHair, out this.hideHair, out bool _, out this.helmetIsOverFullHair);
      this.hairOffset = this.drawPlayer.GetHairDrawOffset(this.drawPlayer.hair, this.hatHair);
      this.hairOffset.Y *= this.drawPlayer.Directions.Y;
      this.helmetOffset = this.drawPlayer.GetHelmetDrawOffset();
      this.helmetOffset.Y *= this.drawPlayer.Directions.Y;
      this.helmetIsTall = this.drawPlayer.head == 14 || this.drawPlayer.head == 56 || this.drawPlayer.head == 158;
      this.helmetIsNormal = !this.helmetIsTall && !this.helmetIsOverFullHair && this.drawPlayer.head > 0 && this.drawPlayer.head < ArmorIDs.Head.Count && this.drawPlayer.head != 28;
    }
  }
}
