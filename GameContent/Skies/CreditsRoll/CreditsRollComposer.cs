// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Skies.CreditsRoll.CreditsRollComposer
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.GameContent.Animations;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.Skies.CreditsRoll
{
  public class CreditsRollComposer
  {
    private Vector2 _originAtBottom = new Vector2(0.5f, 1f);
    private Vector2 _emoteBubbleOffsetWhenOnLeft = new Vector2(-14f, -38f);
    private Vector2 _emoteBubbleOffsetWhenOnRight = new Vector2(14f, -38f);
    private Vector2 _backgroundOffset = new Vector2(76f, 166f);
    private int _endTime;
    private List<IAnimationSegment> _segments;

    public void FillSegments_Test(List<IAnimationSegment> segmentsList, out int endTime)
    {
      this._segments = segmentsList;
      int startTime = 0;
      Vector2 sceneAnchorPosition = Vector2.UnitY * -1f * 80f;
      this._endTime = startTime + this.PlaySegment_PrincessAndEveryoneThanksPlayer(startTime, sceneAnchorPosition).totalTime + 20;
      endTime = this._endTime;
    }

    public void FillSegments(List<IAnimationSegment> segmentsList, out int endTime, bool inGame)
    {
      this._segments = segmentsList;
      int num1 = 0;
      Vector2 vector2_1 = Vector2.UnitY * -1f * 80f;
      int num2 = 210;
      Vector2 vector2_2 = Vector2.UnitX * 200f;
      Vector2 vector2_3 = vector2_1 + vector2_2;
      Vector2 vector2_4 = vector2_3;
      if (!inGame)
        vector2_4 = vector2_3 = Vector2.UnitY * 80f;
      int num3 = num2 * 3;
      int num4 = num2 * 3;
      int num5 = num3 - num4;
      if (!inGame)
      {
        num4 = 180;
        num5 = num3 - num4;
      }
      int startTime1 = num1 + num4;
      int startTime2 = startTime1 + this.PlaySegment_TextRoll(startTime1, "CreditsRollCategory_Creator", vector2_3).totalTime + num2;
      int startTime3 = startTime2 + this.PlaySegment_Grox_GuideRunningFromZombie(startTime2, vector2_3).totalTime + num2;
      vector2_3.X *= -1f;
      int startTime4 = startTime3 + this.PlaySegment_TextRoll(startTime3, "CreditsRollCategory_ExecutiveProducer", vector2_3).totalTime + num2;
      int startTime5 = startTime4 + this.PlaySegment_Grox_MerchantAndTravelingMerchantTryingToSellJunk(startTime4, vector2_3).totalTime + num2;
      vector2_3.X *= -1f;
      int startTime6 = startTime5 + this.PlaySegment_TextRoll(startTime5, "CreditsRollCategory_Designer", vector2_3).totalTime + num2;
      int startTime7 = startTime6 + this.PlaySegment_Grox_DemolitionistAndArmsDealerArguingThenNurseComes(startTime6, vector2_3).totalTime + num2;
      vector2_3.X *= -1f;
      int startTime8 = startTime7 + this.PlaySegment_TextRoll(startTime7, "CreditsRollCategory_Programming", vector2_3).totalTime + num2;
      int startTime9 = startTime8 + this.PlaySegment_TinkererAndMechanic(startTime8, vector2_3).totalTime + num2;
      vector2_3.X *= 0.0f;
      int startTime10 = startTime9 + this.PlaySegment_TextRoll(startTime9, "CreditsRollCategory_Graphics", vector2_3).totalTime + num2;
      int startTime11 = startTime10 + this.PlaySegment_Grox_DryadSayingByeToTavernKeep(startTime10, vector2_3).totalTime + num2;
      vector2_3 = vector2_4;
      vector2_3.X *= -1f;
      int startTime12 = startTime11 + this.PlaySegment_TextRoll(startTime11, "CreditsRollCategory_Music", vector2_3).totalTime + num2;
      int startTime13 = startTime12 + this.PlaySegment_Grox_WizardPartyGirlDyeTraderAndPainterPartyWithBunnies(startTime12, vector2_3).totalTime + num2;
      vector2_3.X *= -1f;
      int startTime14 = startTime13 + this.PlaySegment_TextRoll(startTime13, "CreditsRollCategory_Sound", vector2_3).totalTime + num2;
      int startTime15 = startTime14 + this.PlaySegment_ClothierChasingTruffle(startTime14, vector2_3).totalTime + num2;
      vector2_3.X *= -1f;
      int startTime16 = startTime15 + this.PlaySegment_TextRoll(startTime15, "CreditsRollCategory_Dialog", vector2_3).totalTime + num2;
      int startTime17 = startTime16 + this.PlaySegment_Grox_AnglerAndPirateTalkAboutFish(startTime16, vector2_3).totalTime + num2;
      vector2_3.X *= 0.0f;
      int startTime18 = startTime17 + this.PlaySegment_TextRoll(startTime17, "CreditsRollCategory_QualityAssurance", vector2_3).totalTime + num2;
      int startTime19 = startTime18 + this.PlaySegment_Grox_ZoologistAndPetsAnnoyGolfer(startTime18, vector2_3).totalTime + num2;
      vector2_3 = vector2_4;
      vector2_3.X *= -1f;
      int startTime20 = startTime19 + this.PlaySegment_TextRoll(startTime19, "CreditsRollCategory_BusinessDevelopment", vector2_3).totalTime + num2;
      int startTime21 = startTime20 + this.PlaySegment_Grox_SkeletonMerchantSearchesThroughBones(startTime20, vector2_3).totalTime + num2;
      vector2_3.X *= -1f;
      int startTime22 = startTime21 + this.PlaySegment_TextRoll(startTime21, "CreditsRollCategory_Marketing", vector2_3).totalTime + num2;
      int startTime23 = startTime22 + this.PlaySegment_DryadTurningToTree(startTime22, vector2_3).totalTime + num2;
      vector2_3.X *= -1f;
      int startTime24 = startTime23 + this.PlaySegment_TextRoll(startTime23, "CreditsRollCategory_PublicRelations", vector2_3).totalTime + num2;
      int startTime25 = startTime24 + this.PlaySegment_Grox_SteampunkerRepairingCyborg(startTime24, vector2_3).totalTime + num2;
      vector2_3.X *= 0.0f;
      int startTime26 = startTime25 + this.PlaySegment_TextRoll(startTime25, "CreditsRollCategory_Webmaster", vector2_3).totalTime + num2;
      int startTime27 = startTime26 + this.PlaySegment_Grox_SantaAndTaxCollectorThrowingPresents(startTime26, vector2_3).totalTime + num2;
      int startTime28 = startTime27 + this.PlaySegment_TextRoll(startTime27, "CreditsRollCategory_Playtesting", vector2_3).totalTime + num2;
      int startTime29 = startTime28 + this.PlaySegment_Grox_WitchDoctorGoingToHisPeople(startTime28, vector2_3).totalTime + num2;
      int startTime30 = startTime29 + this.PlaySegment_TextRoll(startTime29, "CreditsRollCategory_SpecialThanksto", vector2_3).totalTime + num2;
      int startTime31 = startTime30 + this.PlaySegment_PrincessAndEveryoneThanksPlayer(startTime30, vector2_3).totalTime + num2;
      this._endTime = startTime31 + this.PlaySegment_TextRoll(startTime31, "CreditsRollCategory_EndingNotes", vector2_3).totalTime + num5 + 10;
      endTime = this._endTime;
    }

    private SegmentInforReport PlaySegment_PrincessAndEveryoneThanksPlayer(
      int startTime,
      Vector2 sceneAnchorPosition)
    {
      sceneAnchorPosition.Y += 40f;
      int num1 = -2;
      int num2 = 2;
      List<int> intList1 = new List<int>()
      {
        228,
        178,
        550,
        208,
        160,
        209
      };
      List<int> intList2 = new List<int>()
      {
        353,
        633,
        207,
        588,
        227,
        368
      };
      List<int> intList3 = new List<int>()
      {
        22,
        19,
        18,
        17,
        38,
        54,
        108
      };
      List<int> intList4 = new List<int>()
      {
        663,
        20,
        441,
        107,
        124,
        229,
        369
      };
      List<CreditsRollComposer.SimplifiedNPCInfo> simplifiedNpcInfoList = new List<CreditsRollComposer.SimplifiedNPCInfo>();
      for (int index = 0; index < intList1.Count; ++index)
      {
        int npcType = intList1[index];
        simplifiedNpcInfoList.Add(new CreditsRollComposer.SimplifiedNPCInfo(npcType, new Vector2((float) (num1 - index), -1f)));
      }
      for (int index = 0; index < intList3.Count; ++index)
      {
        int npcType = intList3[index];
        simplifiedNpcInfoList.Add(new CreditsRollComposer.SimplifiedNPCInfo(npcType, new Vector2((float) (num1 - index) + 0.5f, 0.0f)));
      }
      for (int index = 0; index < intList2.Count; ++index)
      {
        int npcType = intList2[index];
        simplifiedNpcInfoList.Add(new CreditsRollComposer.SimplifiedNPCInfo(npcType, new Vector2((float) (num2 + index), -1f)));
      }
      for (int index = 0; index < intList4.Count; ++index)
      {
        int npcType = intList4[index];
        simplifiedNpcInfoList.Add(new CreditsRollComposer.SimplifiedNPCInfo(npcType, new Vector2((float) (num2 + index) - 0.5f, 0.0f)));
      }
      int num3 = 240;
      int num4 = 400;
      int num5 = num4 + num3;
      Asset<Texture2D> asset = TextureAssets.Extra[241];
      Rectangle r = asset.Frame();
      DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(r), Color.White, 0.0f, r.Size() * new Vector2(0.5f, 1f) + new Vector2(0.0f, -92f), 1f, SpriteEffects.None);
      this._segments.Add((IAnimationSegment) new Segments.SpriteSegment(asset, startTime, data, sceneAnchorPosition).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 51)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num5)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, 85)));
      foreach (CreditsRollComposer.SimplifiedNPCInfo simplifiedNpcInfo in simplifiedNpcInfoList)
        simplifiedNpcInfo.SpawnNPC(new CreditsRollComposer.AddNPCMethod(this.AddWavingNPC), sceneAnchorPosition, startTime, num5);
      float y1 = 3f;
      float y2 = -0.05f;
      int num6 = 60;
      float num7 = (float) ((double) y1 * (double) num6 + (double) y2 * ((double) (num6 * num6) * 0.5));
      int num8 = startTime + num3;
      int num9 = 51;
      int num10 = num6;
      this._segments.Add((IAnimationSegment) new Segments.PlayerSegment(num8 - num10 + num9, sceneAnchorPosition + new Vector2(0.0f, -num7), this._originAtBottom).UseShaderEffect((Segments.PlayerSegment.IShaderEffect) new Segments.PlayerSegment.ImmediateSpritebatchForPlayerDyesEffect()).Then((IAnimationSegmentAction<Player>) new Actions.Players.Fade(0.0f)).With((IAnimationSegmentAction<Player>) new Actions.Players.LookAt(1)).With((IAnimationSegmentAction<Player>) new Actions.Players.Fade(1f, num6)).Then((IAnimationSegmentAction<Player>) new Actions.Players.Wait(num4 / 2)).With((IAnimationSegmentAction<Player>) new Actions.Players.MoveWithAcceleration(new Vector2(0.0f, y1), new Vector2(0.0f, y2), num6)).Then((IAnimationSegmentAction<Player>) new Actions.Players.Wait(num4 / 2 - 60)).With((IAnimationSegmentAction<Player>) new Actions.Players.LookAt(-1)).Then((IAnimationSegmentAction<Player>) new Actions.Players.Wait(120)).With((IAnimationSegmentAction<Player>) new Actions.Players.LookAt(1)).Then((IAnimationSegmentAction<Player>) new Actions.Players.Fade(0.0f, 85)));
      return new SegmentInforReport()
      {
        totalTime = num5 + 85 + 60
      };
    }

    private void AddWavingNPC(
      int npcType,
      Vector2 sceneAnchoePosition,
      int lookDirection,
      int fromTime,
      int duration,
      int timeToJumpAt)
    {
      int num1 = 0;
      float num2 = 4f;
      float y = 0.2f;
      float durationInFrames1 = num2 * 2f / y;
      int blinkTime = NPCID.Sets.AttackType[npcType] * 6 + npcType % 13 * 2 + 20;
      int durationInFrames2 = 0;
      if (npcType % 7 != 0)
        durationInFrames2 = 0;
      int num3 = npcType == 663 ? 1 : (npcType == 108 ? 1 : 0);
      bool flag = false;
      if (num3 != 0)
        durationInFrames2 = 180;
      int durationInFrames3 = 240;
      int direction = lookDirection;
      int emoteId = -1;
      int duration1 = 0;
      if (npcType <= 227)
      {
        if (npcType != 54 && npcType != 107 && npcType != 227)
          goto label_10;
      }
      else if (npcType <= 353)
      {
        if (npcType != 229 && npcType != 353)
          goto label_10;
      }
      else if (npcType != 550 && npcType != 663)
        goto label_10;
      direction *= -1;
label_10:
      if ((uint) (npcType - 207) <= 2U || npcType == 228 || (uint) (npcType - 368) <= 1U)
        flag = true;
      switch (npcType)
      {
        case 54:
          emoteId = 126;
          break;
        case 107:
          emoteId = 0;
          break;
        case 208:
          emoteId = (int) sbyte.MaxValue;
          break;
        case 229:
          emoteId = 85;
          break;
        case 353:
          emoteId = 136;
          break;
        case 368:
          emoteId = 15;
          break;
      }
      if (emoteId != -1)
        duration1 = npcType % 6 * 20 + 60;
      int num4 = duration - timeToJumpAt - num1 - durationInFrames3;
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions = new Segments.NPCSegment(fromTime, npcType, sceneAnchoePosition, this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(direction));
      if (flag)
        segmentWithActions.With((IAnimationSegmentAction<NPC>) new Actions.NPCs.PartyHard());
      segmentWithActions.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames3)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(lookDirection)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(timeToJumpAt)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.MoveWithAcceleration(new Vector2(0.0f, -num2), new Vector2(0.0f, y), (int) durationInFrames1)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.0f, 1E-05f), (int) durationInFrames1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num4 - 90 + blinkTime)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(90 - blinkTime));
      if (durationInFrames2 > 0)
        segmentWithActions.With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Blink(durationInFrames2));
      segmentWithActions.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(3, 85));
      if (npcType == 663)
        this.AddEmote(sceneAnchoePosition, fromTime, duration, blinkTime, 17, lookDirection);
      if (emoteId != -1)
        this.AddEmote(sceneAnchoePosition, fromTime, duration1, 0, emoteId, direction);
      this._segments.Add((IAnimationSegment) segmentWithActions);
    }

    private void AddEmote(
      Vector2 sceneAnchoePosition,
      int fromTime,
      int duration,
      int blinkTime,
      int emoteId,
      int direction)
    {
      this._segments.Add((IAnimationSegment) new Segments.EmoteSegment(emoteId, fromTime + duration - blinkTime, 60, sceneAnchoePosition + this._emoteBubbleOffsetWhenOnRight, direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None));
    }

    private SegmentInforReport PlaySegment_TextRoll(
      int startTime,
      string sourceCategory,
      Vector2 anchorOffset = default (Vector2))
    {
      anchorOffset.Y -= 40f;
      int num = 80;
      LocalizedText[] all = Language.FindAll(Lang.CreateDialogFilter(sourceCategory + ".", (object) null));
      for (int index = 0; index < all.Length; ++index)
        this._segments.Add((IAnimationSegment) new Segments.LocalizedTextSegment((float) (startTime + index * num), all[index], anchorOffset));
      return new SegmentInforReport()
      {
        totalTime = all.Length * num + num * -1
      };
    }

    private SegmentInforReport PlaySegment_GuideEmotingAtRainbowPanel(
      int startTime)
    {
      Asset<Texture2D> asset = TextureAssets.Extra[156];
      DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(), Color.White, 0.0f, asset.Size() / 2f, 0.25f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions = new Segments.SpriteSegment(asset, startTime, data, new Vector2(0.0f, -60f)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, 0)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 60)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(60)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, 60));
      this._segments.Add((IAnimationSegment) segmentWithActions);
      return new SegmentInforReport()
      {
        totalTime = (int) segmentWithActions.DedicatedTimeNeeded
      };
    }

    private SegmentInforReport PlaySegment_Grox_DryadSayingByeToTavernKeep(
      int startTime,
      Vector2 sceneAnchorPosition)
    {
      sceneAnchorPosition += this.GetSceneFixVector();
      int targetTime1 = startTime;
      int x1 = 0;
      sceneAnchorPosition.X += (float) x1;
      int num1 = 30;
      int x2 = 10;
      Asset<Texture2D> asset1 = TextureAssets.Extra[235];
      Rectangle r1 = asset1.Frame();
      DrawData data1 = new DrawData(asset1.Value, Vector2.Zero, new Rectangle?(r1), Color.White, 0.0f, r1.Size() * new Vector2(0.5f, 1f) + new Vector2((float) x1, -42f), 1f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions1 = new Segments.SpriteSegment(asset1, targetTime1, data1, sceneAnchorPosition + new Vector2((float) x2, 0.0f) + this._backgroundOffset).UseShaderEffect((Segments.SpriteSegment.IShaderEffect) new Segments.SpriteSegment.MaskedFadeEffect()).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 60)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(120));
      this._segments.Add((IAnimationSegment) segmentWithActions1);
      int num2 = 300;
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions2 = new Segments.NPCSegment(startTime, 20, sceneAnchorPosition + new Vector2((float) (x2 + num2), 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-1f, 0.0f), 120));
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions3 = new Segments.NPCSegment(startTime, 550, sceneAnchorPosition + new Vector2((float) (x2 + num2 - 16 - num1), 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-1f, 0.0f), 120));
      Asset<Texture2D> asset2 = TextureAssets.Extra[240];
      Rectangle r2 = asset2.Frame(verticalFrames: 8);
      DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(r2), Color.White, 0.0f, r2.Size() * new Vector2(0.5f, 1f), 1f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions4 = new Segments.SpriteSegment(asset2, startTime, data2, sceneAnchorPosition + new Vector2((float) x2, 2f)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 51));
      int num3 = targetTime1 + (int) segmentWithActions3.DedicatedTimeNeeded;
      int num4 = 90;
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-1f, 0.0f), 90));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-1f, 0.0f), 30));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(60));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(90));
      int targetTime2 = num3 + 90;
      int durationInFrames = num4 * 5;
      int x3 = x2 + num2 - 120 - 30;
      int x4 = x2 + num2 - 120 - 106 - num1;
      Segments.EmoteSegment emoteSegment1 = new Segments.EmoteSegment(14, targetTime2, num4, sceneAnchorPosition + new Vector2((float) x3, 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      Segments.EmoteSegment emoteSegment2 = new Segments.EmoteSegment(133, targetTime2 + num4, num4, sceneAnchorPosition + new Vector2((float) x4, 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      Segments.EmoteSegment emoteSegment3 = new Segments.EmoteSegment(78, targetTime2 + num4 * 2, num4, sceneAnchorPosition + new Vector2((float) x3, 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      Segments.EmoteSegment emoteSegment4 = new Segments.EmoteSegment(15, targetTime2 + num4 * 4, num4, sceneAnchorPosition + new Vector2((float) x4, 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      Segments.EmoteSegment emoteSegment5 = new Segments.EmoteSegment(15, targetTime2 + num4 * 4, num4, sceneAnchorPosition + new Vector2((float) x3, 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(1));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num4 * 3));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.ShowItem(num4, 353));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num4));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames));
      int num5 = targetTime2 + durationInFrames;
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-1f, 0.0f), 30));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(30));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(30));
      int targetTime3 = num5 + 30;
      Main.instance.LoadNPC(550);
      Asset<Texture2D> asset3 = TextureAssets.Npc[550];
      Rectangle r3 = asset3.Frame(verticalFrames: Main.npcFrameCount[550]);
      DrawData data3 = new DrawData(asset3.Value, Vector2.Zero, new Rectangle?(r3), Color.White, 0.0f, r3.Size() * new Vector2(0.5f, 1f), 1f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions5 = new Segments.SpriteSegment(asset3, targetTime3, data3, sceneAnchorPosition + new Vector2((float) (x4 - 30), 2f)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f));
      segmentWithActions5.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SimulateGravity(new Vector2(-0.2f, -0.35f), Vector2.Zero, 0.0f, 80)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SetFrameSequence(80, new Point[13]
      {
        new Point(0, 2),
        new Point(0, 3),
        new Point(0, 4),
        new Point(0, 5),
        new Point(0, 6),
        new Point(0, 7),
        new Point(0, 8),
        new Point(0, 9),
        new Point(0, 10),
        new Point(0, 11),
        new Point(0, 12),
        new Point(0, 13),
        new Point(0, 14)
      }, 4, 0, 0)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, 85));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(80));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(80));
      int targetTime4 = targetTime3 + 80;
      segmentWithActions4.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SetFrameSequence(targetTime4 - startTime, new Point[8]
      {
        new Point(0, 0),
        new Point(0, 1),
        new Point(0, 2),
        new Point(0, 3),
        new Point(0, 4),
        new Point(0, 5),
        new Point(0, 6),
        new Point(0, 7)
      }, 5, 0, 0));
      Segments.EmoteSegment emoteSegment6 = new Segments.EmoteSegment(10, targetTime4, num4, sceneAnchorPosition + new Vector2((float) x3, 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      segmentWithActions4.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, num4 - 30));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num4));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num4));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      int num6 = targetTime4 + 187;
      this._segments.Add((IAnimationSegment) segmentWithActions4);
      this._segments.Add((IAnimationSegment) segmentWithActions3);
      this._segments.Add((IAnimationSegment) segmentWithActions5);
      this._segments.Add((IAnimationSegment) segmentWithActions2);
      this._segments.Add((IAnimationSegment) emoteSegment1);
      this._segments.Add((IAnimationSegment) emoteSegment2);
      this._segments.Add((IAnimationSegment) emoteSegment3);
      this._segments.Add((IAnimationSegment) emoteSegment5);
      this._segments.Add((IAnimationSegment) emoteSegment4);
      this._segments.Add((IAnimationSegment) emoteSegment6);
      return new SegmentInforReport()
      {
        totalTime = num6 - startTime
      };
    }

    private SegmentInforReport PlaySegment_Grox_SteampunkerRepairingCyborg(
      int startTime,
      Vector2 sceneAnchorPosition)
    {
      sceneAnchorPosition += this.GetSceneFixVector();
      int targetTime = startTime;
      int x1 = 30;
      sceneAnchorPosition.X += (float) x1;
      int x2 = 60;
      Asset<Texture2D> asset1 = TextureAssets.Extra[232];
      Rectangle r1 = asset1.Frame();
      DrawData data1 = new DrawData(asset1.Value, Vector2.Zero, new Rectangle?(r1), Color.White, 0.0f, r1.Size() * new Vector2(0.5f, 1f) + new Vector2((float) x1, -42f), 1f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions1 = new Segments.SpriteSegment(asset1, targetTime, data1, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect((Segments.SpriteSegment.IShaderEffect) new Segments.SpriteSegment.MaskedFadeEffect()).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 60)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(60));
      this._segments.Add((IAnimationSegment) segmentWithActions1);
      Asset<Texture2D> asset2 = TextureAssets.Extra[233];
      Rectangle r2 = asset2.Frame();
      data1 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(r2), Color.White, 0.0f, r2.Size() * new Vector2(0.5f, 1f) + new Vector2((float) x1, -42f), 1f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions2 = new Segments.SpriteSegment(asset2, targetTime, data1, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect((Segments.SpriteSegment.IShaderEffect) new Segments.SpriteSegment.MaskedFadeEffect()).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 60)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(60));
      this._segments.Add((IAnimationSegment) segmentWithActions1);
      this._segments.Add((IAnimationSegment) segmentWithActions2);
      Asset<Texture2D> asset3 = TextureAssets.Extra[230];
      Rectangle r3 = asset3.Frame(verticalFrames: 21);
      DrawData data2 = new DrawData(asset3.Value, Vector2.Zero, new Rectangle?(r3), Color.White, 0.0f, r3.Size() * new Vector2(0.5f, 1f), 1f, SpriteEffects.None);
      Segments.SpriteSegment spriteSegment1 = new Segments.SpriteSegment(asset3, targetTime, data2, sceneAnchorPosition + new Vector2(0.0f, 4f));
      spriteSegment1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 60)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(60));
      Asset<Texture2D> asset4 = TextureAssets.Extra[229];
      Rectangle r4 = asset4.Frame(verticalFrames: 2);
      DrawData data3 = new DrawData(asset4.Value, Vector2.Zero, new Rectangle?(r4), Color.White, 0.0f, r4.Size() * new Vector2(0.5f, 1f), 1f, SpriteEffects.None);
      Segments.SpriteSegment spriteSegment2 = new Segments.SpriteSegment(asset4, targetTime, data3, sceneAnchorPosition + new Vector2((float) x2, 4f));
      spriteSegment2.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 60)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(60));
      int num1 = targetTime + (int) spriteSegment1.DedicatedTimeNeeded;
      int num2 = 120;
      spriteSegment1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SetFrameSequence(num2, new Point[2]
      {
        new Point(0, 0),
        new Point(0, 1)
      }, 10, 0, 0));
      spriteSegment2.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num2));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num2));
      segmentWithActions2.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num2));
      int num3 = num1 + num2;
      Point[] frameIndices1 = new Point[29]
      {
        new Point(0, 2),
        new Point(0, 3),
        new Point(0, 4),
        new Point(0, 5),
        new Point(0, 6),
        new Point(0, 7),
        new Point(0, 8),
        new Point(0, 9),
        new Point(0, 10),
        new Point(0, 11),
        new Point(0, 12),
        new Point(0, 13),
        new Point(0, 14),
        new Point(0, 15),
        new Point(0, 16),
        new Point(0, 17),
        new Point(0, 18),
        new Point(0, 19),
        new Point(0, 20),
        new Point(0, 15),
        new Point(0, 16),
        new Point(0, 17),
        new Point(0, 18),
        new Point(0, 19),
        new Point(0, 20),
        new Point(0, 17),
        new Point(0, 18),
        new Point(0, 19),
        new Point(0, 20)
      };
      int timePerFrame = 6;
      int durationInFrames1 = timePerFrame * frameIndices1.Length;
      spriteSegment1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SetFrameSequence(frameIndices1, timePerFrame, 0, 0));
      int durationInFrames2 = durationInFrames1 / 2;
      spriteSegment2.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames2));
      spriteSegment2.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SetFrame(0, 1, 0, 0));
      spriteSegment2.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames2));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames1));
      segmentWithActions2.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames1));
      int num4 = num3 + durationInFrames1;
      Point[] frameIndices2 = new Point[4]
      {
        new Point(0, 17),
        new Point(0, 18),
        new Point(0, 19),
        new Point(0, 20)
      };
      spriteSegment1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SetFrameSequence(187, frameIndices2, timePerFrame, 0, 0)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      spriteSegment2.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      int num5 = num4 + 187;
      this._segments.Add((IAnimationSegment) spriteSegment1);
      this._segments.Add((IAnimationSegment) spriteSegment2);
      return new SegmentInforReport()
      {
        totalTime = num5 - startTime
      };
    }

    private SegmentInforReport PlaySegment_Grox_SantaAndTaxCollectorThrowingPresents(
      int startTime,
      Vector2 sceneAnchorPosition)
    {
      sceneAnchorPosition += this.GetSceneFixVector();
      int targetTime1 = startTime;
      int x1 = 0;
      sceneAnchorPosition.X += (float) x1;
      int x2 = 120;
      Asset<Texture2D> asset1 = TextureAssets.Extra[236];
      Rectangle r1 = asset1.Frame();
      DrawData data1 = new DrawData(asset1.Value, Vector2.Zero, new Rectangle?(r1), Color.White, 0.0f, r1.Size() * new Vector2(0.5f, 1f) + new Vector2((float) x1, -42f), 1f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions1 = new Segments.SpriteSegment(asset1, startTime, data1, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect((Segments.SpriteSegment.IShaderEffect) new Segments.SpriteSegment.MaskedFadeEffect()).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 60)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(120));
      this._segments.Add((IAnimationSegment) segmentWithActions1);
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions2 = new Segments.NPCSegment(startTime, 142, sceneAnchorPosition + new Vector2(-30f, 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.5f, 0.0f), 120));
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions3 = new Segments.NPCSegment(startTime, 441, sceneAnchorPosition + new Vector2((float) x2, 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(120));
      Asset<Texture2D> asset2 = TextureAssets.Extra[239];
      Rectangle r2 = asset2.Frame(verticalFrames: 8);
      DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(r2), Color.White, 0.0f, r2.Size() * new Vector2(0.5f, 1f), 1f, SpriteEffects.FlipHorizontally);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions4 = new Segments.SpriteSegment(asset2, targetTime1, data2, sceneAnchorPosition + new Vector2((float) (x1 - 44), 4f)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 51));
      int targetTime2 = targetTime1 + (int) segmentWithActions2.DedicatedTimeNeeded;
      int num1 = 120;
      int durationInFrames1 = 90;
      Segments.EmoteSegment emoteSegment1 = new Segments.EmoteSegment(125, targetTime2, num1, sceneAnchorPosition + new Vector2(30f, 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num1));
      int targetTime3 = targetTime2 + num1;
      Segments.EmoteSegment emoteSegment2 = new Segments.EmoteSegment(10, targetTime3, num1, sceneAnchorPosition + new Vector2((float) x2, 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      int durationInFrames2 = num1 + 30;
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames2));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(-1));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames2));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames2));
      int num2 = targetTime3 + durationInFrames2;
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames1));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames1));
      int targetTime4 = num2 + durationInFrames1;
      Segments.EmoteSegment emoteSegment3 = new Segments.EmoteSegment(3, targetTime4, num1, sceneAnchorPosition + new Vector2((float) x2, 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num1));
      int targetTime5 = targetTime4 + num1;
      Segments.EmoteSegment emoteSegment4 = new Segments.EmoteSegment(136, targetTime5, num1, sceneAnchorPosition + new Vector2(30f, 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      Segments.EmoteSegment emoteSegment5 = new Segments.EmoteSegment(15, targetTime5, num1, sceneAnchorPosition + new Vector2((float) x2, 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num1));
      int num3 = targetTime5 + num1;
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.ShowItem(durationInFrames1 + num1 + num1, 3749));
      segmentWithActions4.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SetFrameSequence(num3 - startTime, new Point[8]
      {
        new Point(0, 0),
        new Point(0, 1),
        new Point(0, 2),
        new Point(0, 3),
        new Point(0, 4),
        new Point(0, 5),
        new Point(0, 6),
        new Point(0, 7)
      }, 5, 0, 0));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions4.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      int num4 = num3 + 187;
      this._segments.Add((IAnimationSegment) segmentWithActions4);
      this._segments.Add((IAnimationSegment) segmentWithActions2);
      this._segments.Add((IAnimationSegment) segmentWithActions3);
      this._segments.Add((IAnimationSegment) emoteSegment1);
      this._segments.Add((IAnimationSegment) emoteSegment2);
      this._segments.Add((IAnimationSegment) emoteSegment3);
      this._segments.Add((IAnimationSegment) emoteSegment4);
      this._segments.Add((IAnimationSegment) emoteSegment5);
      return new SegmentInforReport()
      {
        totalTime = num4 - startTime
      };
    }

    private SegmentInforReport PlaySegment_Grox_WitchDoctorGoingToHisPeople(
      int startTime,
      Vector2 sceneAnchorPosition)
    {
      sceneAnchorPosition += this.GetSceneFixVector();
      int num1 = startTime;
      int x = 0;
      sceneAnchorPosition.X += (float) x;
      int num2 = 60;
      Asset<Texture2D> asset1 = TextureAssets.Extra[231];
      Rectangle r1 = asset1.Frame();
      DrawData data1 = new DrawData(asset1.Value, Vector2.Zero, new Rectangle?(r1), Color.White, 0.0f, r1.Size() * new Vector2(0.5f, 1f) + new Vector2((float) x, -42f), 1f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions1 = new Segments.SpriteSegment(asset1, startTime, data1, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect((Segments.SpriteSegment.IShaderEffect) new Segments.SpriteSegment.MaskedFadeEffect()).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 60)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(120));
      this._segments.Add((IAnimationSegment) segmentWithActions1);
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions2 = new Segments.NPCSegment(startTime, 228, sceneAnchorPosition + new Vector2(-60f, 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.5f, 0.0f), 120));
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions3 = new Segments.NPCSegment(startTime, 663, sceneAnchorPosition + new Vector2(-110f, 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.5f, 0.0f), 120));
      Point[] frameIndices1 = new Point[5]
      {
        new Point(0, 3),
        new Point(0, 4),
        new Point(0, 5),
        new Point(0, 6),
        new Point(0, 7)
      };
      Point[] frameIndices2 = new Point[4]
      {
        new Point(0, 3),
        new Point(0, 2),
        new Point(0, 1),
        new Point(0, 0)
      };
      Main.instance.LoadNPC(199);
      Asset<Texture2D> asset2 = TextureAssets.Npc[199];
      Rectangle r2 = asset2.Frame(verticalFrames: Main.npcFrameCount[199]);
      DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(r2), Color.White, 0.0f, r2.Size() * new Vector2(0.5f, 1f), 1f, SpriteEffects.None);
      DrawData drawData = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(r2), Color.White, 0.0f, r2.Size() * new Vector2(0.5f, 1f), 1f, SpriteEffects.FlipHorizontally);
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions4 = new Segments.NPCSegment(startTime, 198, sceneAnchorPosition + new Vector2((float) (num2 * 2), 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(120));
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions5 = new Segments.SpriteSegment(asset2, startTime, data2, sceneAnchorPosition + new Vector2((float) (num2 * 3 - 20 + 120), 4f)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SetFrame(0, 3, 0, 0)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 25)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SimulateGravity(new Vector2(-1f, 0.0f), Vector2.Zero, 0.0f, 120)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SetFrameSequence(120, frameIndices1, 6, 0, 0));
      int targetTime1 = num1 + (int) segmentWithActions2.DedicatedTimeNeeded;
      int num3 = 120;
      Segments.EmoteSegment emoteSegment1 = new Segments.EmoteSegment(10, targetTime1, num3, sceneAnchorPosition + new Vector2(0.0f, 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      int timePerFrame = 6;
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions5.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SetFrameSequence(frameIndices2, timePerFrame, 0, 0));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num3));
      int targetTime2 = targetTime1 + num3;
      int durationInFrames1 = num3 - timePerFrame * 4;
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions6 = new Segments.NPCSegment(targetTime2 - num3 + timePerFrame * 4, 198, sceneAnchorPosition + new Vector2((float) (num2 * 3 - 20), 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(-1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames1));
      Segments.EmoteSegment emoteSegment2 = new Segments.EmoteSegment(92, targetTime2, num3, sceneAnchorPosition + new Vector2(-50f, 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(-1));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions6.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num3));
      int num4 = targetTime2 + num3;
      int durationInFrames2 = 60;
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames2));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.5f, 0.0f), durationInFrames2));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames2));
      segmentWithActions6.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames2));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames2));
      int targetTime3 = num4 + durationInFrames2;
      Segments.EmoteSegment emoteSegment3 = new Segments.EmoteSegment(87, targetTime3, num3, sceneAnchorPosition + new Vector2((float) (num2 * 2), 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(-1));
      segmentWithActions6.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(-1));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num3));
      int targetTime4 = targetTime3 + num3;
      Segments.EmoteSegment emoteSegment4 = new Segments.EmoteSegment(49, targetTime4, num3, sceneAnchorPosition + new Vector2(30f, 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions6.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num3));
      int targetTime5 = targetTime4 + num3;
      int durationInFrames3 = num3 + num3 / 2;
      Segments.EmoteSegment emoteSegment5 = new Segments.EmoteSegment(10, targetTime5, num3, sceneAnchorPosition + new Vector2((float) (num2 * 2), 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      Segments.EmoteSegment emoteSegment6 = new Segments.EmoteSegment(0, targetTime5 + num3 / 2, num3, sceneAnchorPosition + new Vector2((float) (num2 * 3 - 20), 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames3));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames3));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames3));
      segmentWithActions6.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames3));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames3));
      int targetTime6 = targetTime5 + durationInFrames3;
      Segments.EmoteSegment emoteSegment7 = new Segments.EmoteSegment(17, targetTime6, num3, sceneAnchorPosition + new Vector2(-50f, 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      Segments.EmoteSegment emoteSegment8 = new Segments.EmoteSegment(3, targetTime6, num3, sceneAnchorPosition + new Vector2(30f, 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions6.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num3));
      int num5 = targetTime6 + num3;
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-0.4f, 0.0f), 160)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-0.5f, 0.0f), 160)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-0.8f, 0.0f), 160)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions6.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-0.8f, 0.0f), 160)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      int num6 = num5 + 187;
      this._segments.Add((IAnimationSegment) segmentWithActions4);
      this._segments.Add((IAnimationSegment) segmentWithActions6);
      this._segments.Add((IAnimationSegment) segmentWithActions5);
      this._segments.Add((IAnimationSegment) segmentWithActions2);
      this._segments.Add((IAnimationSegment) segmentWithActions3);
      this._segments.Add((IAnimationSegment) emoteSegment1);
      this._segments.Add((IAnimationSegment) emoteSegment2);
      this._segments.Add((IAnimationSegment) emoteSegment3);
      this._segments.Add((IAnimationSegment) emoteSegment4);
      this._segments.Add((IAnimationSegment) emoteSegment5);
      this._segments.Add((IAnimationSegment) emoteSegment6);
      this._segments.Add((IAnimationSegment) emoteSegment8);
      this._segments.Add((IAnimationSegment) emoteSegment7);
      return new SegmentInforReport()
      {
        totalTime = num6 - startTime
      };
    }

    private Vector2 GetSceneFixVector() => new Vector2(-this._backgroundOffset.X, 0.0f);

    private SegmentInforReport PlaySegment_DryadTurningToTree(
      int startTime,
      Vector2 sceneAnchorPosition)
    {
      sceneAnchorPosition += this.GetSceneFixVector();
      int targetTime1 = startTime;
      Asset<Texture2D> asset1 = TextureAssets.Extra[217];
      Rectangle r1 = asset1.Frame();
      DrawData data1 = new DrawData(asset1.Value, Vector2.Zero, new Rectangle?(r1), Color.White, 0.0f, r1.Size() * new Vector2(0.5f, 1f) + new Vector2(0.0f, -42f), 1f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions1 = new Segments.SpriteSegment(asset1, targetTime1, data1, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect((Segments.SpriteSegment.IShaderEffect) new Segments.SpriteSegment.MaskedFadeEffect()).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 60));
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions2 = new Segments.NPCSegment(startTime, 20, sceneAnchorPosition, this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(1f, 0.0f), 60)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(10)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(0));
      int targetTime2 = targetTime1 + (int) segmentWithActions2.DedicatedTimeNeeded;
      Asset<Texture2D> asset2 = TextureAssets.Extra[215];
      Rectangle r2 = asset2.Frame(verticalFrames: 9);
      DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(r2), Color.White, 0.0f, r2.Size() * new Vector2(0.5f, 1f), 1f, SpriteEffects.FlipHorizontally);
      Vector2 vector2 = new Vector2(1f, 0.0f) * 60f + new Vector2(2f, 4f);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions3 = new Segments.SpriteSegment(asset2, targetTime2, data2, sceneAnchorPosition + vector2).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SetFrameSequence(new Point[9]
      {
        new Point(0, 0),
        new Point(0, 1),
        new Point(0, 2),
        new Point(0, 3),
        new Point(0, 4),
        new Point(0, 5),
        new Point(0, 6),
        new Point(0, 7),
        new Point(0, 8)
      }, 8, 0, 0)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(30));
      int targetTime3 = targetTime2 + (int) segmentWithActions3.DedicatedTimeNeeded;
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions4 = new Segments.NPCSegment(targetTime3, 46, sceneAnchorPosition + new Vector2(-100f, 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(1f, 0.0f), 60)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(90)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(1f, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(3, 85));
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions5 = new Segments.NPCSegment(targetTime3 + 60, 299, sceneAnchorPosition + new Vector2(170f, 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-1f, 0.0f), 60)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(60)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-1f, 0.0f), 90)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-1f, 0.0f), 85)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(3, 85));
      float x = 1.5f;
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions6 = new Segments.NPCSegment(targetTime3 + 45, 74, sceneAnchorPosition + new Vector2(-80f, -70f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(x, 0.0f), 85)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.MoveWithRotor(new Vector2(10f, 0.0f), 0.07391983f, new Vector2(0.0f, 1f), 85)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(x, 0.0f), 85)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.MoveWithRotor(new Vector2(4f, 0.0f), 0.07391983f, new Vector2(0.0f, 1f), 85)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(3, 85));
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions7 = new Segments.NPCSegment(targetTime3 + 180, 656, sceneAnchorPosition + new Vector2(20f, 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Variant(1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(60)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.5f, 0.0f), 60)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(60)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.DoBunnyRestAnimation(90)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(90)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(3, 120));
      Segments.EmoteSegment emoteSegment = new Segments.EmoteSegment(0, targetTime3 + 360, 60, sceneAnchorPosition + new Vector2(36f, -10f), SpriteEffects.FlipHorizontally, Vector2.Zero);
      segmentWithActions3.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(420)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, 120));
      int num = targetTime3 + 620;
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num - startTime - 180)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, 120));
      this._segments.Add((IAnimationSegment) segmentWithActions1);
      this._segments.Add((IAnimationSegment) segmentWithActions2);
      this._segments.Add((IAnimationSegment) segmentWithActions3);
      this._segments.Add((IAnimationSegment) segmentWithActions4);
      this._segments.Add((IAnimationSegment) segmentWithActions5);
      this._segments.Add((IAnimationSegment) segmentWithActions6);
      this._segments.Add((IAnimationSegment) segmentWithActions7);
      this._segments.Add((IAnimationSegment) emoteSegment);
      return new SegmentInforReport()
      {
        totalTime = num - startTime
      };
    }

    private SegmentInforReport PlaySegment_SantaItemExample(
      int startTime,
      Vector2 sceneAnchorPosition)
    {
      sceneAnchorPosition += this.GetSceneFixVector();
      int num = 0;
      for (int index = 0; index < num; ++index)
      {
        int i = (int) Main.rand.NextFromList<short>((short) 599, (short) 1958, (short) 3749, (short) 1869);
        Main.instance.LoadItem(i);
        Asset<Texture2D> asset = TextureAssets.Item[i];
        DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(), Color.White, 0.0f, asset.Size() / 2f, 1f, SpriteEffects.None);
        Vector2 initialVelocity = Vector2.UnitY * -12f + Main.rand.NextVector2Circular(6f, 3f).RotatedBy((double) (index - num / 2) * 6.2831854820251465 * 0.10000000149011612);
        Vector2 gravityPerFrame = Vector2.UnitY * 0.2f;
        int targetTime = startTime;
        this._segments.Add((IAnimationSegment) new Segments.SpriteSegment(asset, targetTime, data, sceneAnchorPosition).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SimulateGravity(initialVelocity, gravityPerFrame, Main.rand.NextFloatDirection() * 0.2f, 60)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, 60)));
      }
      this._segments.Add((IAnimationSegment) new Segments.NPCSegment(startTime, 142, sceneAnchorPosition, this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.ShowItem(30, 267)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(10)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.ShowItem(30, 600)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(10)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.ShowItem(30, 2)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(10)));
      return new SegmentInforReport() { totalTime = 170 };
    }

    private SegmentInforReport PlaySegment_Grox_SkeletonMerchantSearchesThroughBones(
      int startTime,
      Vector2 sceneAnchorPosition)
    {
      sceneAnchorPosition += this.GetSceneFixVector();
      int targetTime1 = startTime;
      int x1 = 30;
      sceneAnchorPosition.X += (float) x1;
      int x2 = 100;
      Asset<Texture2D> asset1 = TextureAssets.Extra[220];
      Rectangle r1 = asset1.Frame();
      DrawData data1 = new DrawData(asset1.Value, Vector2.Zero, new Rectangle?(r1), Color.White, 0.0f, r1.Size() * new Vector2(0.5f, 1f) + new Vector2((float) x1, -42f), 1f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions1 = new Segments.SpriteSegment(asset1, targetTime1, data1, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect((Segments.SpriteSegment.IShaderEffect) new Segments.SpriteSegment.MaskedFadeEffect()).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 60)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(60));
      this._segments.Add((IAnimationSegment) segmentWithActions1);
      int x3 = 10;
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions2 = new Segments.NPCSegment(startTime, 453, sceneAnchorPosition + new Vector2((float) x3, 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(1f, 0.0f), 60));
      Asset<Texture2D> asset2 = TextureAssets.Extra[227];
      DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(), Color.White, 0.0f, asset2.Size() * new Vector2(0.5f, 1f), 1f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions3 = new Segments.SpriteSegment(asset2, startTime, data2, sceneAnchorPosition + new Vector2((float) x2, 2f)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 51)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(60));
      int targetTime2 = targetTime1 + (int) segmentWithActions2.DedicatedTimeNeeded;
      int num1 = 90;
      Segments.EmoteSegment emoteSegment1 = new Segments.EmoteSegment(87, targetTime2, num1, sceneAnchorPosition + new Vector2((float) (60 + x3), 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions3.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num1));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num1));
      int targetTime3 = targetTime2 + num1;
      Asset<Texture2D> asset3 = TextureAssets.Extra[228];
      Rectangle r2 = asset3.Frame(verticalFrames: 14);
      DrawData data3 = new DrawData(asset3.Value, Vector2.Zero, new Rectangle?(r2), Color.White, 0.0f, r2.Size() * new Vector2(0.5f, 1f), 1f, SpriteEffects.FlipHorizontally);
      Segments.SpriteSegment spriteSegment = new Segments.SpriteSegment(asset3, targetTime3, data3, sceneAnchorPosition + new Vector2((float) (x2 - 10), 4f));
      spriteSegment.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SetFrameSequence(new Point[4]
      {
        new Point(0, 1),
        new Point(0, 2),
        new Point(0, 3),
        new Point(0, 4)
      }, 5, 0, 0));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(20)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue));
      segmentWithActions3.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(20));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(20));
      int num2 = targetTime3 + 20;
      int num3 = 10;
      Main.instance.LoadItem(154);
      Asset<Texture2D> tex1 = TextureAssets.Item[154];
      DrawData drawData1 = new DrawData(tex1.Value, Vector2.Zero, new Rectangle?(), Color.White, 0.0f, tex1.Size() / 2f, 1f, SpriteEffects.None);
      Main.instance.LoadItem(1274);
      Asset<Texture2D> tex2 = TextureAssets.Item[1274];
      DrawData drawData2 = new DrawData(tex2.Value, Vector2.Zero, new Rectangle?(), Color.White, 0.0f, tex2.Size() / 2f, 1f, SpriteEffects.None);
      Vector2 anchorOffset = sceneAnchorPosition + new Vector2((float) x2, -8f);
      for (int index = 0; index < num3; ++index)
      {
        Vector2 initialVelocity = Vector2.UnitY * -5f + Main.rand.NextVector2Circular(2.5f, (float) (0.30000001192092896 + (double) Main.rand.NextFloat() * 0.20000000298023224)).RotatedBy((double) (index - num3 / 2) * 6.2831854820251465 * 0.10000000149011612);
        Vector2 gravityPerFrame = Vector2.UnitY * 0.1f;
        int targetTime4 = num2 + index * 10;
        DrawData data4 = drawData1;
        Asset<Texture2D> asset4 = tex1;
        if (index == num3 - 3)
        {
          data4 = drawData2;
          asset4 = tex2;
        }
        this._segments.Add((IAnimationSegment) new Segments.SpriteSegment(asset4, targetTime4, data4, anchorOffset).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SimulateGravity(initialVelocity, gravityPerFrame, Main.rand.NextFloatDirection() * 0.2f, 60)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, 60)));
      }
      int num4 = 30 + num3 * 10;
      spriteSegment.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SetFrameSequence(num4, new Point[4]
      {
        new Point(0, 5),
        new Point(0, 6),
        new Point(0, 7),
        new Point(0, 8)
      }, 5, 0, 0));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num4));
      segmentWithActions3.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num4));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num4));
      int targetTime5 = num2 + num4;
      Segments.EmoteSegment emoteSegment2 = new Segments.EmoteSegment(3, targetTime5, num1, sceneAnchorPosition + new Vector2((float) (80 + x3), 4f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      spriteSegment.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num1)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SetFrame(0, 5, 0, 0));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions3.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num1));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num1));
      int num5 = targetTime5 + num1;
      spriteSegment.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SetFrameSequence(new Point[4]
      {
        new Point(0, 9),
        new Point(0, 10),
        new Point(0, 11),
        new Point(0, 13)
      }, 5, 0, 0));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(20));
      segmentWithActions3.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(20));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(20));
      int num6 = num5 + 20;
      int durationInFrames = 90;
      spriteSegment.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.ShowItem(durationInFrames, 3258)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-255)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(-1));
      segmentWithActions3.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames));
      int targetTime6 = num6 + durationInFrames;
      Segments.EmoteSegment emoteSegment3 = new Segments.EmoteSegment(136, targetTime6, num1, sceneAnchorPosition + new Vector2((float) (60 + x3), 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None, new Vector2(-1f, 0.0f));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-1f, 0.0f), num1));
      segmentWithActions3.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num1));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num1));
      int num7 = targetTime6 + num1;
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-0.5f, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions3.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      int num8 = num7 + 187;
      this._segments.Add((IAnimationSegment) segmentWithActions3);
      this._segments.Add((IAnimationSegment) segmentWithActions2);
      this._segments.Add((IAnimationSegment) spriteSegment);
      this._segments.Add((IAnimationSegment) emoteSegment1);
      this._segments.Add((IAnimationSegment) emoteSegment2);
      this._segments.Add((IAnimationSegment) emoteSegment3);
      return new SegmentInforReport()
      {
        totalTime = num8 - startTime
      };
    }

    private SegmentInforReport PlaySegment_Grox_MerchantAndTravelingMerchantTryingToSellJunk(
      int startTime,
      Vector2 sceneAnchorPosition)
    {
      sceneAnchorPosition += this.GetSceneFixVector();
      int targetTime1 = startTime;
      int x1 = 40;
      sceneAnchorPosition.X += (float) x1;
      int x2 = 62;
      Asset<Texture2D> asset1 = TextureAssets.Extra[223];
      Rectangle r1 = asset1.Frame();
      DrawData data1 = new DrawData(asset1.Value, Vector2.Zero, new Rectangle?(r1), Color.White, 0.0f, r1.Size() * new Vector2(0.5f, 1f) + new Vector2((float) x1, -42f), 1f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions1 = new Segments.SpriteSegment(asset1, targetTime1, data1, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect((Segments.SpriteSegment.IShaderEffect) new Segments.SpriteSegment.MaskedFadeEffect()).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 60)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(60));
      this._segments.Add((IAnimationSegment) segmentWithActions1);
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions2 = new Segments.NPCSegment(startTime, 17, sceneAnchorPosition, this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(60));
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions3 = new Segments.NPCSegment(startTime, 368, sceneAnchorPosition + new Vector2((float) x2, 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(60));
      Asset<Texture2D> asset2 = TextureAssets.Extra[239];
      Rectangle r2 = asset2.Frame(verticalFrames: 8);
      DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(r2), Color.White, 0.0f, r2.Size() * new Vector2(0.5f, 1f), 1f, SpriteEffects.FlipHorizontally);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions4 = new Segments.SpriteSegment(asset2, targetTime1, data2, sceneAnchorPosition + new Vector2((float) (x1 - 128), 4f)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 51));
      int num1 = targetTime1 + (int) segmentWithActions2.DedicatedTimeNeeded;
      int num2 = 90;
      int durationInFrames1 = 60;
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.ShowItem(durationInFrames1, 8));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames1));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames1));
      int targetTime2 = num1 + durationInFrames1;
      Segments.EmoteSegment emoteSegment1 = new Segments.EmoteSegment(11, targetTime2, num2, sceneAnchorPosition + new Vector2((float) x2, 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num2));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num2));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num2));
      int num3 = targetTime2 + num2;
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames1));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.ShowItem(durationInFrames1, 2242));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames1));
      int targetTime3 = num3 + durationInFrames1;
      Segments.EmoteSegment emoteSegment2 = new Segments.EmoteSegment(11, targetTime3, num2, sceneAnchorPosition + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num2));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num2));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num2));
      int num4 = targetTime3 + num2;
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.ShowItem(durationInFrames1, 88));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames1));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames1));
      int targetTime4 = num4 + durationInFrames1;
      Segments.EmoteSegment emoteSegment3 = new Segments.EmoteSegment(11, targetTime4, num2, sceneAnchorPosition + new Vector2((float) x2, 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num2));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num2));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num2));
      int num5 = targetTime4 + num2;
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames1));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.ShowItem(durationInFrames1, 4761));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames1));
      int targetTime5 = num5 + durationInFrames1;
      Segments.EmoteSegment emoteSegment4 = new Segments.EmoteSegment(11, targetTime5, num2, sceneAnchorPosition + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num2));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num2));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num2));
      int num6 = targetTime5 + num2;
      int durationInFrames2 = durationInFrames1 + 30;
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.ShowItem(durationInFrames2, 2));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames2));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames2));
      int targetTime6 = num6 + durationInFrames2;
      Segments.EmoteSegment emoteSegment5 = new Segments.EmoteSegment(10, targetTime6, num2, sceneAnchorPosition + new Vector2((float) x2, 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num2));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num2));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num2));
      int num7 = targetTime6 + num2;
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames2));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.ShowItem(durationInFrames2, 52));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames2));
      int targetTime7 = num7 + durationInFrames2;
      Segments.EmoteSegment emoteSegment6 = new Segments.EmoteSegment(85, targetTime7, num2, sceneAnchorPosition + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      Segments.EmoteSegment emoteSegment7 = new Segments.EmoteSegment(85, targetTime7, num2, sceneAnchorPosition + new Vector2((float) x2, 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num2));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num2));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num2));
      int num8 = targetTime7 + num2;
      segmentWithActions4.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SetFrameSequence(num8 - startTime, new Point[8]
      {
        new Point(0, 0),
        new Point(0, 1),
        new Point(0, 2),
        new Point(0, 3),
        new Point(0, 4),
        new Point(0, 5),
        new Point(0, 6),
        new Point(0, 7)
      }, 5, 0, 0));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions4.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      int num9 = num8 + 187;
      this._segments.Add((IAnimationSegment) segmentWithActions4);
      this._segments.Add((IAnimationSegment) segmentWithActions2);
      this._segments.Add((IAnimationSegment) segmentWithActions3);
      this._segments.Add((IAnimationSegment) emoteSegment1);
      this._segments.Add((IAnimationSegment) emoteSegment2);
      this._segments.Add((IAnimationSegment) emoteSegment3);
      this._segments.Add((IAnimationSegment) emoteSegment4);
      this._segments.Add((IAnimationSegment) emoteSegment5);
      this._segments.Add((IAnimationSegment) emoteSegment6);
      this._segments.Add((IAnimationSegment) emoteSegment7);
      return new SegmentInforReport()
      {
        totalTime = num9 - startTime
      };
    }

    private SegmentInforReport PlaySegment_Grox_GuideRunningFromZombie(
      int startTime,
      Vector2 sceneAnchorPosition)
    {
      sceneAnchorPosition += this.GetSceneFixVector();
      int targetTime1 = startTime;
      int x = 12;
      sceneAnchorPosition.X += (float) x;
      int num1 = 24;
      Asset<Texture2D> asset1 = TextureAssets.Extra[218];
      Rectangle r1 = asset1.Frame();
      DrawData data = new DrawData(asset1.Value, Vector2.Zero, new Rectangle?(r1), Color.White, 0.0f, r1.Size() * new Vector2(0.5f, 1f) + new Vector2((float) x, -42f), 1f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions1 = new Segments.SpriteSegment(asset1, targetTime1, data, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect((Segments.SpriteSegment.IShaderEffect) new Segments.SpriteSegment.MaskedFadeEffect()).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 60));
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions2 = new Segments.NPCSegment(startTime, 3, sceneAnchorPosition + new Vector2((float) (num1 + 60), 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-0.5f, 0.0f), 60));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait((int) segmentWithActions2.DedicatedTimeNeeded));
      int num2 = targetTime1 + (int) segmentWithActions2.DedicatedTimeNeeded;
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.ZombieKnockOnDoor(60));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(60));
      int targetTime2 = num2 + 60;
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions3 = new Segments.NPCSegment(targetTime2, 22, sceneAnchorPosition + new Vector2(-30f, 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.5f, 0.0f), 60));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.ZombieKnockOnDoor((int) segmentWithActions3.DedicatedTimeNeeded));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait((int) segmentWithActions3.DedicatedTimeNeeded));
      int targetTime3 = targetTime2 + (int) segmentWithActions3.DedicatedTimeNeeded;
      int num3 = 90;
      Segments.EmoteSegment emoteSegment1 = new Segments.EmoteSegment(87, targetTime3, num3, sceneAnchorPosition + new Vector2(-4f, 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.ZombieKnockOnDoor(num3));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num3 - 1));
      int targetTime4 = targetTime3 + num3;
      int num4 = 50;
      Segments.EmoteSegment emoteSegment2 = new Segments.EmoteSegment(3, targetTime4, num4, sceneAnchorPosition + new Vector2(-4f, 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      Asset<Texture2D> asset2 = TextureAssets.Extra[219];
      Rectangle r2 = asset2.Frame();
      data = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(r2), Color.White, 0.0f, r2.Size() * new Vector2(0.5f, 1f) + new Vector2((float) x, -42f), 1f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions4 = new Segments.SpriteSegment(asset2, targetTime4, data, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect((Segments.SpriteSegment.IShaderEffect) new Segments.SpriteSegment.MaskedFadeEffect()).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num4));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-0.4f, 0.0f), num4));
      segmentWithActions4.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num4));
      int targetTime5 = targetTime4 + num4;
      Segments.EmoteSegment emoteSegment3 = new Segments.EmoteSegment(134, targetTime5, num3, sceneAnchorPosition + new Vector2(0.0f, 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None, new Vector2(-0.6f, 0.0f));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-0.6f, 0.0f), num3));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-0.4f, 0.0f), num3));
      segmentWithActions4.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num3));
      int num5 = targetTime5 + num3;
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-0.6f, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-0.4f, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions4.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      int num6 = num5 + 187;
      this._segments.Add((IAnimationSegment) segmentWithActions1);
      this._segments.Add((IAnimationSegment) segmentWithActions4);
      this._segments.Add((IAnimationSegment) segmentWithActions3);
      this._segments.Add((IAnimationSegment) segmentWithActions2);
      this._segments.Add((IAnimationSegment) emoteSegment1);
      this._segments.Add((IAnimationSegment) emoteSegment2);
      this._segments.Add((IAnimationSegment) emoteSegment3);
      return new SegmentInforReport()
      {
        totalTime = num6 - startTime
      };
    }

    private SegmentInforReport PlaySegment_Grox_ZoologistAndPetsAnnoyGolfer(
      int startTime,
      Vector2 sceneAnchorPosition)
    {
      sceneAnchorPosition += this.GetSceneFixVector();
      int targetTime1 = startTime;
      int x = -28;
      sceneAnchorPosition.X += (float) x;
      int num1 = 40;
      Asset<Texture2D> asset1 = TextureAssets.Extra[224];
      Rectangle r = asset1.Frame();
      DrawData data1 = new DrawData(asset1.Value, Vector2.Zero, new Rectangle?(r), Color.White, 0.0f, r.Size() * new Vector2(0.5f, 1f) + new Vector2((float) x, -42f), 1f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions1 = new Segments.SpriteSegment(asset1, targetTime1, data1, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect((Segments.SpriteSegment.IShaderEffect) new Segments.SpriteSegment.MaskedFadeEffect()).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 60)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(60));
      this._segments.Add((IAnimationSegment) segmentWithActions1);
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions2 = new Segments.NPCSegment(startTime, 633, sceneAnchorPosition + new Vector2(-60f, 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.ForceAltTexture(3)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.5f, 0.0f), 60));
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions3 = new Segments.NPCSegment(startTime, 656, sceneAnchorPosition + new Vector2((float) (num1 - 60), 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Variant(3)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.5f, 0.0f), 60));
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions4 = new Segments.NPCSegment(startTime, 638, sceneAnchorPosition + new Vector2((float) (num1 * 2 - 60), 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Variant(2)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.5f, 0.0f), 60));
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions5 = new Segments.NPCSegment(startTime, 637, sceneAnchorPosition + new Vector2((float) (num1 * 3 - 60), 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Variant(4)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.5f, 0.0f), 60));
      Main.instance.LoadProjectile(748);
      Asset<Texture2D> asset2 = TextureAssets.Projectile[748];
      DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(), Color.White, 0.0f, asset2.Size() / 2f, 1f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions6 = new Segments.SpriteSegment(asset2, startTime, data2, sceneAnchorPosition + new Vector2((float) (num1 * 3 - 20), 0.0f)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 51)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(60));
      int num2 = targetTime1 + (int) segmentWithActions2.DedicatedTimeNeeded;
      int num3 = 90;
      float num4 = 0.5f;
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(1f * num4, 0.0f), num3));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.5f * num4, 0.0f), num3));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.6f * num4, 0.0f), num3));
      segmentWithActions5.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.8f * num4, 0.0f), num3));
      segmentWithActions6.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SimulateGravity(new Vector2(0.82f * num4, 0.0f), Vector2.Zero, 0.07f, num3));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num3));
      int targetTime2 = num2 + num3;
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions7 = new Segments.NPCSegment(targetTime2, 588, sceneAnchorPosition + new Vector2(-70f, 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.7f * num4, 0.0f), 60));
      int dedicatedTimeNeeded = (int) segmentWithActions7.DedicatedTimeNeeded;
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(1f * num4, 0.0f), dedicatedTimeNeeded));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.85f * num4, 0.0f), dedicatedTimeNeeded));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.7f * num4, 0.0f), dedicatedTimeNeeded));
      segmentWithActions5.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.65f * num4, 0.0f), dedicatedTimeNeeded));
      segmentWithActions6.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SimulateGravity(new Vector2(1f * num4, 0.0f), Vector2.Zero, 0.07f, dedicatedTimeNeeded));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(dedicatedTimeNeeded));
      int targetTime3 = targetTime2 + dedicatedTimeNeeded;
      int timeToPlay = 90;
      int num5 = timeToPlay * 2 + timeToPlay / 2;
      Segments.EmoteSegment emoteSegment1 = new Segments.EmoteSegment(1, targetTime3, timeToPlay, sceneAnchorPosition + new Vector2((float) (42.0 * (double) num4 - 70.0), 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally, new Vector2(1f * num4, 0.0f));
      Segments.EmoteSegment emoteSegment2 = new Segments.EmoteSegment(15, targetTime3 + timeToPlay / 2, timeToPlay, sceneAnchorPosition + new Vector2((float) (80 + dedicatedTimeNeeded) * num4, 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally, new Vector2(1f * num4, 0.0f));
      segmentWithActions7.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(1f * num4, 0.0f), num5));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(1f * num4, 0.0f), num5));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.72f * num4, 0.0f), num5));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.7f * num4, 0.0f), num5));
      segmentWithActions5.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.8f * num4, 0.0f), num5));
      segmentWithActions6.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SimulateGravity(new Vector2(0.85f * num4, 0.0f), Vector2.Zero, 0.07f, num5));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num5));
      int num6 = targetTime3 + num5;
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.5f * num4, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions7.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.5f * num4, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.6f * num4, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.7f * num4, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions5.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.6f * num4, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions6.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SimulateGravity(new Vector2(0.5f * num4, 0.0f), Vector2.Zero, 0.05f, 120)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      int num7 = num6 + 187;
      this._segments.Add((IAnimationSegment) segmentWithActions2);
      this._segments.Add((IAnimationSegment) segmentWithActions7);
      this._segments.Add((IAnimationSegment) segmentWithActions3);
      this._segments.Add((IAnimationSegment) segmentWithActions4);
      this._segments.Add((IAnimationSegment) segmentWithActions5);
      this._segments.Add((IAnimationSegment) segmentWithActions6);
      this._segments.Add((IAnimationSegment) emoteSegment2);
      this._segments.Add((IAnimationSegment) emoteSegment1);
      return new SegmentInforReport()
      {
        totalTime = num7 - startTime
      };
    }

    private SegmentInforReport PlaySegment_Grox_AnglerAndPirateTalkAboutFish(
      int startTime,
      Vector2 sceneAnchorPosition)
    {
      sceneAnchorPosition += this.GetSceneFixVector();
      int targetTime1 = startTime;
      int x1 = 30;
      sceneAnchorPosition.X += (float) x1;
      int x2 = 90;
      Asset<Texture2D> asset1 = TextureAssets.Extra[222];
      Rectangle r1 = asset1.Frame();
      DrawData data1 = new DrawData(asset1.Value, Vector2.Zero, new Rectangle?(r1), Color.White, 0.0f, r1.Size() * new Vector2(0.5f, 1f) + new Vector2((float) x1, -42f), 1f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions1 = new Segments.SpriteSegment(asset1, targetTime1, data1, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect((Segments.SpriteSegment.IShaderEffect) new Segments.SpriteSegment.MaskedFadeEffect()).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 60)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(60));
      this._segments.Add((IAnimationSegment) segmentWithActions1);
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions2 = new Segments.NPCSegment(startTime, 369, sceneAnchorPosition, this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(60));
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions3 = new Segments.NPCSegment(startTime, 229, sceneAnchorPosition + new Vector2((float) (x2 + 60), 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(-1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-1f, 0.0f), 60));
      Asset<Texture2D> asset2 = TextureAssets.Extra[226];
      Rectangle r2 = asset2.Frame(verticalFrames: 8);
      DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(r2), Color.White, 0.0f, r2.Size() * new Vector2(0.5f, 1f), 1f, SpriteEffects.FlipHorizontally);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions4 = new Segments.SpriteSegment(asset2, targetTime1, data2, sceneAnchorPosition + new Vector2((float) (x2 / 2), 4f)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 51));
      int targetTime2 = targetTime1 + (int) segmentWithActions2.DedicatedTimeNeeded;
      int num1 = 90;
      int durationInFrames = num1 * 8;
      Segments.EmoteSegment emoteSegment1 = new Segments.EmoteSegment(79, targetTime2, num1, sceneAnchorPosition + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      Segments.EmoteSegment emoteSegment2 = new Segments.EmoteSegment(65, targetTime2 + num1, num1, sceneAnchorPosition + new Vector2((float) x2, 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      Segments.EmoteSegment emoteSegment3 = new Segments.EmoteSegment(136, targetTime2 + num1 * 3, num1, sceneAnchorPosition + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      Segments.EmoteSegment emoteSegment4 = new Segments.EmoteSegment(3, targetTime2 + num1 * 5, num1, sceneAnchorPosition + new Vector2((float) x2, 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      Segments.EmoteSegment emoteSegment5 = new Segments.EmoteSegment(50, targetTime2 + num1 * 6, num1, sceneAnchorPosition + new Vector2((float) x2, 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      Segments.EmoteSegment emoteSegment6 = new Segments.EmoteSegment(15, targetTime2 + num1 * 6, num1, sceneAnchorPosition + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None, new Vector2(-1f, 0.0f));
      Segments.EmoteSegment emoteSegment7 = new Segments.EmoteSegment(2, targetTime2 + num1 * 7, num1, sceneAnchorPosition + new Vector2((float) x2, 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None, new Vector2(-1.25f, 0.0f));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1 * 4)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.ShowItem(num1, 2673)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-1f, 0.0f), num1));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1 * 2)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.ShowItem(num1, 2480)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1 * 4)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-1.25f, 0.0f), num1));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames));
      segmentWithActions4.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SetFrameSequence(durationInFrames + 60, new Point[8]
      {
        new Point(0, 0),
        new Point(0, 1),
        new Point(0, 2),
        new Point(0, 3),
        new Point(0, 4),
        new Point(0, 5),
        new Point(0, 6),
        new Point(0, 7)
      }, 5, 0, 0));
      int num2 = targetTime2 + durationInFrames;
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-0.4f, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-0.75f, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions4.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      int num3 = num2 + 187;
      this._segments.Add((IAnimationSegment) segmentWithActions4);
      this._segments.Add((IAnimationSegment) segmentWithActions2);
      this._segments.Add((IAnimationSegment) segmentWithActions3);
      this._segments.Add((IAnimationSegment) emoteSegment1);
      this._segments.Add((IAnimationSegment) emoteSegment2);
      this._segments.Add((IAnimationSegment) emoteSegment3);
      this._segments.Add((IAnimationSegment) emoteSegment4);
      this._segments.Add((IAnimationSegment) emoteSegment5);
      this._segments.Add((IAnimationSegment) emoteSegment6);
      this._segments.Add((IAnimationSegment) emoteSegment7);
      return new SegmentInforReport()
      {
        totalTime = num3 - startTime
      };
    }

    private SegmentInforReport PlaySegment_Grox_WizardPartyGirlDyeTraderAndPainterPartyWithBunnies(
      int startTime,
      Vector2 sceneAnchorPosition)
    {
      sceneAnchorPosition += this.GetSceneFixVector();
      int targetTime1 = startTime;
      int x1 = -35;
      sceneAnchorPosition.X += (float) x1;
      int x2 = 34;
      Asset<Texture2D> asset1 = TextureAssets.Extra[221];
      Rectangle r1 = asset1.Frame();
      DrawData data1 = new DrawData(asset1.Value, Vector2.Zero, new Rectangle?(r1), Color.White, 0.0f, r1.Size() * new Vector2(0.5f, 1f) + new Vector2((float) x1, -42f), 1f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions1 = new Segments.SpriteSegment(asset1, targetTime1, data1, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect((Segments.SpriteSegment.IShaderEffect) new Segments.SpriteSegment.MaskedFadeEffect()).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 60)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(60));
      this._segments.Add((IAnimationSegment) segmentWithActions1);
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions2 = new Segments.NPCSegment(startTime, 227, sceneAnchorPosition, this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.PartyHard()).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(60));
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions3 = new Segments.NPCSegment(startTime, 108, sceneAnchorPosition + new Vector2((float) x2, 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.PartyHard()).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(-1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(60));
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions4 = new Segments.NPCSegment(startTime, 207, sceneAnchorPosition + new Vector2((float) (x2 * 2 + 60), 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.PartyHard()).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(-1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-1f, 0.0f), 60));
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions5 = new Segments.NPCSegment(startTime, 656, sceneAnchorPosition + new Vector2((float) (x2 * 2), 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Variant(1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.PartyHard()).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(1f, 0.0f), 60));
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions6 = new Segments.NPCSegment(startTime, 540, sceneAnchorPosition + new Vector2((float) (x2 * 4 + 100), 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(-1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-1f, 0.0f), 60));
      Asset<Texture2D> asset2 = TextureAssets.Extra[238];
      Rectangle r2 = asset2.Frame(verticalFrames: 4);
      DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(r2), Color.White, 0.0f, r2.Size() * new Vector2(0.5f, 1f), 1f, SpriteEffects.FlipHorizontally);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions7 = new Segments.SpriteSegment(asset2, targetTime1, data2, sceneAnchorPosition + new Vector2(60f, 2f)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 51));
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions8 = new Segments.SpriteSegment(asset2, targetTime1, data2, sceneAnchorPosition + new Vector2(150f, 2f)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 51));
      int targetTime2 = targetTime1 + (int) segmentWithActions2.DedicatedTimeNeeded;
      int num1 = 90;
      int durationInFrames1 = num1 * 4;
      Segments.EmoteSegment emoteSegment1 = new Segments.EmoteSegment((int) sbyte.MaxValue, targetTime2, num1, sceneAnchorPosition + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      Segments.EmoteSegment emoteSegment2 = new Segments.EmoteSegment(6, targetTime2 + num1, num1, sceneAnchorPosition + new Vector2((float) x2, 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      Segments.EmoteSegment emoteSegment3 = new Segments.EmoteSegment(136, targetTime2 + num1 * 2, num1, sceneAnchorPosition + new Vector2((float) (x2 * 2), 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      Segments.EmoteSegment emoteSegment4 = new Segments.EmoteSegment(129, targetTime2 + num1 * 3, num1, sceneAnchorPosition + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames1));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1 * 2)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(-1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames1));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames1));
      segmentWithActions6.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-0.5f, 0.0f), durationInFrames1 / 3)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames1 / 3)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.4f, 0.0f), durationInFrames1 / 3));
      segmentWithActions5.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-0.6f, 0.0f), durationInFrames1 / 3)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.5f, 0.0f), durationInFrames1 / 3)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames1 / 3));
      int num2 = targetTime2 + durationInFrames1;
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions9 = new Segments.NPCSegment(num2 - 60, 208, sceneAnchorPosition + new Vector2((float) (x2 * 5 + 100), 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.PartyHard()).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(-1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-1f, 0.0f), 60));
      int durationInFrames2 = (int) segmentWithActions9.DedicatedTimeNeeded - 60;
      int targetTime3 = num2 + durationInFrames2;
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames2));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames2));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames2));
      segmentWithActions6.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames2));
      segmentWithActions5.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames2));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames2));
      Segments.EmoteSegment emoteSegment5 = new Segments.EmoteSegment(128, targetTime3, num1, sceneAnchorPosition + new Vector2((float) (x2 * 5 + 40), 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      int targetTime4 = targetTime3 + num1;
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions6.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions5.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions9.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num1));
      Segments.EmoteSegment emoteSegment6 = new Segments.EmoteSegment(128, targetTime4, num1, sceneAnchorPosition + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      Segments.EmoteSegment emoteSegment7 = new Segments.EmoteSegment(128, targetTime4, num1, sceneAnchorPosition + new Vector2((float) x2, 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      Segments.EmoteSegment emoteSegment8 = new Segments.EmoteSegment(128, targetTime4, num1, sceneAnchorPosition + new Vector2((float) (x2 * 2), 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      Segments.EmoteSegment emoteSegment9 = new Segments.EmoteSegment(3, targetTime4, num1, sceneAnchorPosition + new Vector2((float) (x2 * 5 - 10), 24f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      Segments.EmoteSegment emoteSegment10 = new Segments.EmoteSegment(0, targetTime4, num1, sceneAnchorPosition + new Vector2((float) (x2 * 4 - 20), 24f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions6.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions5.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions9.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num1));
      int num3 = targetTime4 + num1;
      segmentWithActions7.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SetFrameSequence(num3 - startTime, new Point[4]
      {
        new Point(0, 0),
        new Point(0, 1),
        new Point(0, 2),
        new Point(0, 3)
      }, 10, 0, 0));
      segmentWithActions8.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SetFrameSequence(num3 - startTime, new Point[4]
      {
        new Point(0, 2),
        new Point(0, 3),
        new Point(0, 0),
        new Point(0, 1)
      }, 10, 0, 0));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(1f, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(1f, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(1f, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions6.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.5f, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions5.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.75f, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions9.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(0.5f, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions7.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      segmentWithActions8.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      int num4 = num3 + 187;
      this._segments.Add((IAnimationSegment) segmentWithActions7);
      this._segments.Add((IAnimationSegment) segmentWithActions8);
      this._segments.Add((IAnimationSegment) segmentWithActions2);
      this._segments.Add((IAnimationSegment) segmentWithActions3);
      this._segments.Add((IAnimationSegment) segmentWithActions4);
      this._segments.Add((IAnimationSegment) segmentWithActions6);
      this._segments.Add((IAnimationSegment) segmentWithActions5);
      this._segments.Add((IAnimationSegment) segmentWithActions9);
      this._segments.Add((IAnimationSegment) emoteSegment1);
      this._segments.Add((IAnimationSegment) emoteSegment2);
      this._segments.Add((IAnimationSegment) emoteSegment3);
      this._segments.Add((IAnimationSegment) emoteSegment4);
      this._segments.Add((IAnimationSegment) emoteSegment5);
      this._segments.Add((IAnimationSegment) emoteSegment6);
      this._segments.Add((IAnimationSegment) emoteSegment7);
      this._segments.Add((IAnimationSegment) emoteSegment8);
      this._segments.Add((IAnimationSegment) emoteSegment9);
      this._segments.Add((IAnimationSegment) emoteSegment10);
      return new SegmentInforReport()
      {
        totalTime = num4 - startTime
      };
    }

    private SegmentInforReport PlaySegment_Grox_DemolitionistAndArmsDealerArguingThenNurseComes(
      int startTime,
      Vector2 sceneAnchorPosition)
    {
      sceneAnchorPosition += this.GetSceneFixVector();
      int targetTime1 = startTime;
      int x = -30;
      sceneAnchorPosition.X += (float) x;
      Asset<Texture2D> asset = TextureAssets.Extra[234];
      Rectangle r = asset.Frame();
      DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(r), Color.White, 0.0f, r.Size() * new Vector2(0.5f, 1f) + new Vector2((float) x, -42f), 1f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions1 = new Segments.SpriteSegment(asset, targetTime1, data, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect((Segments.SpriteSegment.IShaderEffect) new Segments.SpriteSegment.MaskedFadeEffect()).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 60)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(120));
      this._segments.Add((IAnimationSegment) segmentWithActions1);
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions2 = new Segments.NPCSegment(startTime, 38, sceneAnchorPosition, this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(1f, 0.0f), 60)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(60));
      int num1 = 90;
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions3 = new Segments.NPCSegment(startTime, 19, sceneAnchorPosition + new Vector2((float) (120 + num1), 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-1f, 0.0f), 60)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(60));
      int targetTime2 = targetTime1 + (int) segmentWithActions2.DedicatedTimeNeeded;
      int num2 = 90;
      int durationInFrames1 = num2 * 4;
      int num3 = num2;
      int num4 = num2 / 2;
      int num5 = num2 + num2 / 2;
      int num6 = num2 * 2;
      Segments.EmoteSegment emoteSegment1 = new Segments.EmoteSegment(81, targetTime2, num3, sceneAnchorPosition + new Vector2(60f, 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      Segments.EmoteSegment emoteSegment2 = new Segments.EmoteSegment(82, targetTime2 + num4, num3, sceneAnchorPosition + new Vector2((float) (60 + num1), 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      Segments.EmoteSegment emoteSegment3 = new Segments.EmoteSegment(135, targetTime2 + num5, num3, sceneAnchorPosition + new Vector2(60f, 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      Segments.EmoteSegment emoteSegment4 = new Segments.EmoteSegment(135, targetTime2 + num6, num3, sceneAnchorPosition + new Vector2((float) (60 + num1), 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames1));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames1));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames1));
      int num7 = targetTime2 + durationInFrames1;
      int num8 = num1 - 30;
      int num9 = 120;
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions4 = new Segments.NPCSegment(num7 - num9, 18, sceneAnchorPosition + new Vector2((float) (120 + num8), 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-1f, 0.0f), 60)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(20));
      int durationInFrames2 = (int) segmentWithActions4.DedicatedTimeNeeded - num9;
      int targetTime3 = num7 + durationInFrames2;
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames2));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(durationInFrames2));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames2));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(-1));
      Segments.EmoteSegment emoteSegment5 = new Segments.EmoteSegment(77, targetTime3, num3, sceneAnchorPosition + new Vector2((float) (60 + num8), 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      Segments.EmoteSegment emoteSegment6 = new Segments.EmoteSegment(15, targetTime3 + num3, num3, sceneAnchorPosition + new Vector2((float) (60 + num8), 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num3));
      int num10 = targetTime3 + num3;
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(1));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num3));
      int targetTime4 = num10 + num3;
      Segments.EmoteSegment emoteSegment7 = new Segments.EmoteSegment(10, targetTime4, num3, sceneAnchorPosition + new Vector2(60f, 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      Segments.EmoteSegment emoteSegment8 = new Segments.EmoteSegment(10, targetTime4, num3, sceneAnchorPosition + new Vector2((float) (60 + num1), 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num3));
      int targetTime5 = targetTime4 + num3;
      Vector2 vector2 = new Vector2(-1f, 0.0f);
      Segments.EmoteSegment emoteSegment9 = new Segments.EmoteSegment(77, targetTime5, num3, sceneAnchorPosition + new Vector2((float) (60 + num1), 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None, vector2);
      Segments.EmoteSegment emoteSegment10 = new Segments.EmoteSegment(77, targetTime5 + num3 / 2, num3, sceneAnchorPosition + new Vector2(60f, 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None, vector2);
      int durationInFrames3 = num3 + num3 / 2;
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(vector2, durationInFrames3));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3 / 2)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(vector2, num3));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num3 / 2 + 20)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(vector2, num3 - 20));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames3));
      int num11 = targetTime5 + durationInFrames3;
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-0.5f, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-0.5f, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions4.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-0.5f, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      int num12 = num11 + 187;
      this._segments.Add((IAnimationSegment) segmentWithActions4);
      this._segments.Add((IAnimationSegment) segmentWithActions2);
      this._segments.Add((IAnimationSegment) segmentWithActions3);
      this._segments.Add((IAnimationSegment) emoteSegment1);
      this._segments.Add((IAnimationSegment) emoteSegment2);
      this._segments.Add((IAnimationSegment) emoteSegment3);
      this._segments.Add((IAnimationSegment) emoteSegment4);
      this._segments.Add((IAnimationSegment) emoteSegment5);
      this._segments.Add((IAnimationSegment) emoteSegment6);
      this._segments.Add((IAnimationSegment) emoteSegment7);
      this._segments.Add((IAnimationSegment) emoteSegment8);
      this._segments.Add((IAnimationSegment) emoteSegment10);
      this._segments.Add((IAnimationSegment) emoteSegment9);
      return new SegmentInforReport()
      {
        totalTime = num12 - startTime
      };
    }

    private SegmentInforReport PlaySegment_TinkererAndMechanic(
      int startTime,
      Vector2 sceneAnchorPosition)
    {
      sceneAnchorPosition += this.GetSceneFixVector();
      int targetTime1 = startTime;
      Asset<Texture2D> asset = TextureAssets.Extra[237];
      Rectangle r = asset.Frame();
      DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(r), Color.White, 0.0f, r.Size() * new Vector2(0.5f, 1f) + new Vector2(0.0f, -42f), 1f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions1 = new Segments.SpriteSegment(asset, targetTime1, data, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect((Segments.SpriteSegment.IShaderEffect) new Segments.SpriteSegment.MaskedFadeEffect()).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 60));
      this._segments.Add((IAnimationSegment) segmentWithActions1);
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions2 = new Segments.NPCSegment(startTime, 107, sceneAnchorPosition, this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(1f, 0.0f), 60)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(60));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait((int) segmentWithActions2.DedicatedTimeNeeded));
      int targetTime2 = targetTime1 + (int) segmentWithActions2.DedicatedTimeNeeded;
      int num1 = 24;
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions3 = new Segments.NPCSegment(targetTime2, 124, sceneAnchorPosition + new Vector2((float) (120 + num1), 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(-1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-1f, 0.0f), 60));
      int targetTime3 = targetTime2 + (int) segmentWithActions3.DedicatedTimeNeeded;
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait((int) segmentWithActions3.DedicatedTimeNeeded));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait((int) segmentWithActions3.DedicatedTimeNeeded));
      int num2 = 120;
      Segments.EmoteSegment emoteSegment1 = new Segments.EmoteSegment(0, targetTime3, num2, sceneAnchorPosition + new Vector2(60f, 0.0f) + this._emoteBubbleOffsetWhenOnLeft, SpriteEffects.FlipHorizontally);
      Segments.EmoteSegment emoteSegment2 = new Segments.EmoteSegment(0, targetTime3, num2, sceneAnchorPosition + new Vector2((float) (60 + num1), 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num2));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num2));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num2));
      int num3 = targetTime3 + num2;
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-0.5f, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions3.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(new Vector2(-0.5f, 0.0f), 120)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      int num4 = num3 + 187;
      this._segments.Add((IAnimationSegment) segmentWithActions2);
      this._segments.Add((IAnimationSegment) segmentWithActions3);
      this._segments.Add((IAnimationSegment) emoteSegment1);
      this._segments.Add((IAnimationSegment) emoteSegment2);
      return new SegmentInforReport()
      {
        totalTime = num4 - startTime
      };
    }

    private SegmentInforReport PlaySegment_ClothierChasingTruffle(
      int startTime,
      Vector2 sceneAnchorPosition)
    {
      sceneAnchorPosition += this.GetSceneFixVector();
      int targetTime1 = startTime;
      int x = 10;
      sceneAnchorPosition.X += (float) x;
      Asset<Texture2D> asset = TextureAssets.Extra[225];
      Rectangle r = asset.Frame();
      DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(r), Color.White, 0.0f, r.Size() * new Vector2(0.5f, 1f) + new Vector2((float) x, -42f), 1f, SpriteEffects.None);
      Segments.AnimationSegmentWithActions<Segments.LooseSprite> segmentWithActions1 = new Segments.SpriteSegment(asset, targetTime1, data, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect((Segments.SpriteSegment.IShaderEffect) new Segments.SpriteSegment.MaskedFadeEffect()).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(1f, 60));
      this._segments.Add((IAnimationSegment) segmentWithActions1);
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions2 = new Segments.NPCSegment(startTime, 160, sceneAnchorPosition + new Vector2(20f, 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(1)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(60)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.LookAt(-1));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait((int) segmentWithActions2.DedicatedTimeNeeded));
      int targetTime2 = targetTime1 + (int) segmentWithActions2.DedicatedTimeNeeded;
      int num1 = 60;
      Segments.EmoteSegment emoteSegment1 = new Segments.EmoteSegment(10, targetTime2, num1, sceneAnchorPosition + new Vector2(20f, 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num1));
      int targetTime3 = targetTime2 + num1;
      Segments.EmoteSegment emoteSegment2 = new Segments.EmoteSegment(3, targetTime3, num1, sceneAnchorPosition + new Vector2(20f, 0.0f) + this._emoteBubbleOffsetWhenOnRight, SpriteEffects.None);
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Wait(num1));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(num1));
      int targetTime4 = targetTime3 + num1;
      Vector2 vector2_1 = new Vector2(1.2f, 0.0f);
      Vector2 vector2_2 = new Vector2(1f, 0.0f);
      Segments.AnimationSegmentWithActions<NPC> segmentWithActions3 = new Segments.NPCSegment(targetTime4, 54, sceneAnchorPosition + new Vector2(-100f, 0.0f), this._originAtBottom).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade((int) byte.MaxValue)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(-5, 51)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(vector2_1, 60)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(vector2_1, 130)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions2.Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(vector2_2, 60)).Then((IAnimationSegmentAction<NPC>) new Actions.NPCs.Move(vector2_2, 130)).With((IAnimationSegmentAction<NPC>) new Actions.NPCs.Fade(2, (int) sbyte.MaxValue));
      segmentWithActions1.Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(60)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(130)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Fade(0.0f, (int) sbyte.MaxValue));
      int num2 = 10;
      int num3 = 40;
      int timeToPlay = 70;
      Segments.EmoteSegment emoteSegment3 = new Segments.EmoteSegment(134, targetTime4 + num2, timeToPlay, sceneAnchorPosition + new Vector2(20f, 0.0f) + this._emoteBubbleOffsetWhenOnLeft + vector2_2 * (float) num2, SpriteEffects.FlipHorizontally, vector2_2);
      Segments.EmoteSegment emoteSegment4 = new Segments.EmoteSegment(15, targetTime4 + num3, timeToPlay, sceneAnchorPosition + new Vector2(-100f, 0.0f) + this._emoteBubbleOffsetWhenOnLeft + vector2_1 * (float) num3, SpriteEffects.FlipHorizontally, vector2_1);
      this._segments.Add((IAnimationSegment) segmentWithActions3);
      this._segments.Add((IAnimationSegment) segmentWithActions2);
      this._segments.Add((IAnimationSegment) emoteSegment1);
      this._segments.Add((IAnimationSegment) emoteSegment2);
      this._segments.Add((IAnimationSegment) emoteSegment3);
      this._segments.Add((IAnimationSegment) emoteSegment4);
      int num4 = targetTime4 + 200;
      return new SegmentInforReport()
      {
        totalTime = num4 - startTime
      };
    }

    private struct SimplifiedNPCInfo
    {
      private Vector2 _simplifiedPosition;
      private int _npcType;

      public SimplifiedNPCInfo(int npcType, Vector2 simplifiedPosition)
      {
        this._simplifiedPosition = simplifiedPosition;
        this._npcType = npcType;
      }

      public void SpawnNPC(
        CreditsRollComposer.AddNPCMethod methodToUse,
        Vector2 baseAnchor,
        int startTime,
        int totalSceneTime)
      {
        Vector2 properPosition = this.GetProperPosition(baseAnchor);
        int lookDirection = (double) this._simplifiedPosition.X > 0.0 ? -1 : 1;
        int num = 240;
        int timeToJumpAt = (totalSceneTime - num) / 2 - 20 + (int) ((double) this._simplifiedPosition.X * -8.0);
        methodToUse(this._npcType, properPosition, lookDirection, startTime, totalSceneTime, timeToJumpAt);
      }

      private Vector2 GetProperPosition(Vector2 baseAnchor) => baseAnchor + this._simplifiedPosition * new Vector2(26f, 24f);
    }

    private delegate void AddNPCMethod(
      int npcType,
      Vector2 sceneAnchoePosition,
      int lookDirection,
      int fromTime,
      int duration,
      int timeToJumpAt);
  }
}
