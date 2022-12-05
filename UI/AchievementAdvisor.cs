// Decompiled with JetBrains decompiler
// Type: Terraria.UI.AchievementAdvisor
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.Achievements;
using Terraria.GameInput;

namespace Terraria.UI
{
  public class AchievementAdvisor
  {
    private List<AchievementAdvisorCard> _cards = new List<AchievementAdvisorCard>();
    private Asset<Texture2D> _achievementsTexture;
    private Asset<Texture2D> _achievementsBorderTexture;
    private Asset<Texture2D> _achievementsBorderMouseHoverFatTexture;
    private Asset<Texture2D> _achievementsBorderMouseHoverThinTexture;
    private AchievementAdvisorCard _hoveredCard;

    public bool CanDrawAboveCoins => Main.screenWidth >= 1000 && !PlayerInput.UsingGamepad && !PlayerInput.SteamDeckIsUsed;

    public void LoadContent()
    {
      this._achievementsTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievements", (AssetRequestMode) 1);
      this._achievementsBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders", (AssetRequestMode) 1);
      this._achievementsBorderMouseHoverFatTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders_MouseHover", (AssetRequestMode) 1);
      this._achievementsBorderMouseHoverThinTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders_MouseHoverThin", (AssetRequestMode) 1);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
    }

    public void DrawOneAchievement(SpriteBatch spriteBatch, Vector2 position, bool large)
    {
      List<AchievementAdvisorCard> bestCards = this.GetBestCards(1);
      if (bestCards.Count < 1)
        return;
      AchievementAdvisorCard achievementAdvisorCard = bestCards[0];
      float scale = 0.35f;
      if (large)
        scale = 0.75f;
      this._hoveredCard = (AchievementAdvisorCard) null;
      bool hovered;
      this.DrawCard(bestCards[0], spriteBatch, position + new Vector2(8f) * scale, scale, out hovered);
      if (!hovered)
        return;
      this._hoveredCard = achievementAdvisorCard;
      if (PlayerInput.IgnoreMouseInterface)
        return;
      Main.player[Main.myPlayer].mouseInterface = true;
      if (!Main.mouseLeft || !Main.mouseLeftRelease)
        return;
      Main.ingameOptionsWindow = false;
      IngameFancyUI.OpenAchievementsAndGoto(this._hoveredCard.achievement);
    }

    public void Update() => this._hoveredCard = (AchievementAdvisorCard) null;

    public void DrawOptionsPanel(
      SpriteBatch spriteBatch,
      Vector2 leftPosition,
      Vector2 rightPosition)
    {
      List<AchievementAdvisorCard> bestCards = this.GetBestCards();
      this._hoveredCard = (AchievementAdvisorCard) null;
      int num = bestCards.Count;
      if (num > 5)
        num = 5;
      bool hovered;
      for (int index = 0; index < num; ++index)
      {
        this.DrawCard(bestCards[index], spriteBatch, leftPosition + new Vector2((float) (42 * index), 0.0f), 0.5f, out hovered);
        if (hovered)
          this._hoveredCard = bestCards[index];
      }
      for (int index = 5; index < bestCards.Count; ++index)
      {
        this.DrawCard(bestCards[index], spriteBatch, rightPosition + new Vector2((float) (42 * index), 0.0f), 0.5f, out hovered);
        if (hovered)
          this._hoveredCard = bestCards[index];
      }
      if (this._hoveredCard == null)
        return;
      if (this._hoveredCard.achievement.IsCompleted)
      {
        this._hoveredCard = (AchievementAdvisorCard) null;
      }
      else
      {
        if (PlayerInput.IgnoreMouseInterface)
          return;
        Main.player[Main.myPlayer].mouseInterface = true;
        if (!Main.mouseLeft || !Main.mouseLeftRelease)
          return;
        Main.ingameOptionsWindow = false;
        IngameFancyUI.OpenAchievementsAndGoto(this._hoveredCard.achievement);
      }
    }

    public void DrawMouseHover()
    {
      if (this._hoveredCard == null)
        return;
      Main.spriteBatch.End();
      Main.spriteBatch.Begin(SpriteSortMode.Deferred, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, Main.UIScaleMatrix);
      PlayerInput.SetZoom_UI();
      Item obj = new Item();
      obj.SetDefaults(0, true);
      obj.SetNameOverride(this._hoveredCard.achievement.FriendlyName.Value);
      obj.ToolTip = ItemTooltip.FromLanguageKey(this._hoveredCard.achievement.Description.Key);
      obj.type = 1;
      obj.scale = 0.0f;
      obj.rare = 10;
      obj.value = -1;
      Main.HoverItem = obj;
      Main.instance.MouseText("");
      Main.mouseText = true;
    }

    private void DrawCard(
      AchievementAdvisorCard card,
      SpriteBatch spriteBatch,
      Vector2 position,
      float scale,
      out bool hovered)
    {
      hovered = false;
      if (Main.MouseScreen.Between(position, position + card.frame.Size() * scale))
      {
        Main.LocalPlayer.mouseInterface = true;
        hovered = true;
      }
      Color color = Color.White;
      if (!hovered)
        color = new Color(220, 220, 220, 220);
      Vector2 vector2_1 = new Vector2(-4f) * scale;
      Vector2 vector2_2 = new Vector2(-8f) * scale;
      Texture2D texture = this._achievementsBorderMouseHoverFatTexture.Value;
      if ((double) scale > 0.5)
      {
        texture = this._achievementsBorderMouseHoverThinTexture.Value;
        vector2_2 = new Vector2(-5f) * scale;
      }
      Rectangle frame = card.frame;
      frame.X += 528;
      spriteBatch.Draw(this._achievementsTexture.Value, position, new Rectangle?(frame), color, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f);
      spriteBatch.Draw(this._achievementsBorderTexture.Value, position + vector2_1, new Rectangle?(), color, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f);
      if (!hovered)
        return;
      spriteBatch.Draw(texture, position + vector2_2, new Rectangle?(), Main.OurFavoriteColor, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f);
    }

    private List<AchievementAdvisorCard> GetBestCards(int cardsAmount = 10)
    {
      List<AchievementAdvisorCard> bestCards = new List<AchievementAdvisorCard>();
      for (int index = 0; index < this._cards.Count; ++index)
      {
        AchievementAdvisorCard card = this._cards[index];
        if (!card.achievement.IsCompleted && card.IsAchievableInWorld())
        {
          bestCards.Add(card);
          if (bestCards.Count >= cardsAmount)
            break;
        }
      }
      return bestCards;
    }

    public void Initialize()
    {
      float num1 = 1f;
      List<AchievementAdvisorCard> cards1 = this._cards;
      Achievement achievement1 = Main.Achievements.GetAchievement("TIMBER");
      double order1 = (double) num1;
      float num2 = (float) (order1 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard1 = new AchievementAdvisorCard(achievement1, (float) order1);
      cards1.Add(achievementAdvisorCard1);
      List<AchievementAdvisorCard> cards2 = this._cards;
      Achievement achievement2 = Main.Achievements.GetAchievement("BENCHED");
      double order2 = (double) num2;
      float num3 = (float) (order2 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard2 = new AchievementAdvisorCard(achievement2, (float) order2);
      cards2.Add(achievementAdvisorCard2);
      List<AchievementAdvisorCard> cards3 = this._cards;
      Achievement achievement3 = Main.Achievements.GetAchievement("OBTAIN_HAMMER");
      double order3 = (double) num3;
      float num4 = (float) (order3 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard3 = new AchievementAdvisorCard(achievement3, (float) order3);
      cards3.Add(achievementAdvisorCard3);
      List<AchievementAdvisorCard> cards4 = this._cards;
      Achievement achievement4 = Main.Achievements.GetAchievement("NO_HOBO");
      double order4 = (double) num4;
      float num5 = (float) (order4 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard4 = new AchievementAdvisorCard(achievement4, (float) order4);
      cards4.Add(achievementAdvisorCard4);
      List<AchievementAdvisorCard> cards5 = this._cards;
      Achievement achievement5 = Main.Achievements.GetAchievement("YOU_CAN_DO_IT");
      double order5 = (double) num5;
      float num6 = (float) (order5 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard5 = new AchievementAdvisorCard(achievement5, (float) order5);
      cards5.Add(achievementAdvisorCard5);
      List<AchievementAdvisorCard> cards6 = this._cards;
      Achievement achievement6 = Main.Achievements.GetAchievement("OOO_SHINY");
      double order6 = (double) num6;
      float num7 = (float) (order6 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard6 = new AchievementAdvisorCard(achievement6, (float) order6);
      cards6.Add(achievementAdvisorCard6);
      List<AchievementAdvisorCard> cards7 = this._cards;
      Achievement achievement7 = Main.Achievements.GetAchievement("HEAVY_METAL");
      double order7 = (double) num7;
      float num8 = (float) (order7 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard7 = new AchievementAdvisorCard(achievement7, (float) order7);
      cards7.Add(achievementAdvisorCard7);
      List<AchievementAdvisorCard> cards8 = this._cards;
      Achievement achievement8 = Main.Achievements.GetAchievement("MATCHING_ATTIRE");
      double order8 = (double) num8;
      float num9 = (float) (order8 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard8 = new AchievementAdvisorCard(achievement8, (float) order8);
      cards8.Add(achievementAdvisorCard8);
      List<AchievementAdvisorCard> cards9 = this._cards;
      Achievement achievement9 = Main.Achievements.GetAchievement("HEART_BREAKER");
      double order9 = (double) num9;
      float num10 = (float) (order9 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard9 = new AchievementAdvisorCard(achievement9, (float) order9);
      cards9.Add(achievementAdvisorCard9);
      List<AchievementAdvisorCard> cards10 = this._cards;
      Achievement achievement10 = Main.Achievements.GetAchievement("I_AM_LOOT");
      double order10 = (double) num10;
      float num11 = (float) (order10 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard10 = new AchievementAdvisorCard(achievement10, (float) order10);
      cards10.Add(achievementAdvisorCard10);
      List<AchievementAdvisorCard> cards11 = this._cards;
      Achievement achievement11 = Main.Achievements.GetAchievement("HOLD_ON_TIGHT");
      double order11 = (double) num11;
      float num12 = (float) (order11 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard11 = new AchievementAdvisorCard(achievement11, (float) order11);
      cards11.Add(achievementAdvisorCard11);
      List<AchievementAdvisorCard> cards12 = this._cards;
      Achievement achievement12 = Main.Achievements.GetAchievement("STAR_POWER");
      double order12 = (double) num12;
      float num13 = (float) (order12 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard12 = new AchievementAdvisorCard(achievement12, (float) order12);
      cards12.Add(achievementAdvisorCard12);
      List<AchievementAdvisorCard> cards13 = this._cards;
      Achievement achievement13 = Main.Achievements.GetAchievement("EYE_ON_YOU");
      double order13 = (double) num13;
      float num14 = (float) (order13 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard13 = new AchievementAdvisorCard(achievement13, (float) order13);
      cards13.Add(achievementAdvisorCard13);
      List<AchievementAdvisorCard> cards14 = this._cards;
      Achievement achievement14 = Main.Achievements.GetAchievement("SMASHING_POPPET");
      double order14 = (double) num14;
      float num15 = (float) (order14 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard14 = new AchievementAdvisorCard(achievement14, (float) order14);
      cards14.Add(achievementAdvisorCard14);
      List<AchievementAdvisorCard> cards15 = this._cards;
      Achievement achievement15 = Main.Achievements.GetAchievement("WHERES_MY_HONEY");
      double order15 = (double) num15;
      float num16 = (float) (order15 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard15 = new AchievementAdvisorCard(achievement15, (float) order15);
      cards15.Add(achievementAdvisorCard15);
      List<AchievementAdvisorCard> cards16 = this._cards;
      Achievement achievement16 = Main.Achievements.GetAchievement("STING_OPERATION");
      double order16 = (double) num16;
      float num17 = (float) (order16 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard16 = new AchievementAdvisorCard(achievement16, (float) order16);
      cards16.Add(achievementAdvisorCard16);
      List<AchievementAdvisorCard> cards17 = this._cards;
      Achievement achievement17 = Main.Achievements.GetAchievement("BONED");
      double order17 = (double) num17;
      float num18 = (float) (order17 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard17 = new AchievementAdvisorCard(achievement17, (float) order17);
      cards17.Add(achievementAdvisorCard17);
      List<AchievementAdvisorCard> cards18 = this._cards;
      Achievement achievement18 = Main.Achievements.GetAchievement("DUNGEON_HEIST");
      double order18 = (double) num18;
      float num19 = (float) (order18 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard18 = new AchievementAdvisorCard(achievement18, (float) order18);
      cards18.Add(achievementAdvisorCard18);
      List<AchievementAdvisorCard> cards19 = this._cards;
      Achievement achievement19 = Main.Achievements.GetAchievement("ITS_GETTING_HOT_IN_HERE");
      double order19 = (double) num19;
      float num20 = (float) (order19 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard19 = new AchievementAdvisorCard(achievement19, (float) order19);
      cards19.Add(achievementAdvisorCard19);
      List<AchievementAdvisorCard> cards20 = this._cards;
      Achievement achievement20 = Main.Achievements.GetAchievement("MINER_FOR_FIRE");
      double order20 = (double) num20;
      float num21 = (float) (order20 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard20 = new AchievementAdvisorCard(achievement20, (float) order20);
      cards20.Add(achievementAdvisorCard20);
      List<AchievementAdvisorCard> cards21 = this._cards;
      Achievement achievement21 = Main.Achievements.GetAchievement("STILL_HUNGRY");
      double order21 = (double) num21;
      float num22 = (float) (order21 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard21 = new AchievementAdvisorCard(achievement21, (float) order21);
      cards21.Add(achievementAdvisorCard21);
      List<AchievementAdvisorCard> cards22 = this._cards;
      Achievement achievement22 = Main.Achievements.GetAchievement("ITS_HARD");
      double order22 = (double) num22;
      float num23 = (float) (order22 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard22 = new AchievementAdvisorCard(achievement22, (float) order22);
      cards22.Add(achievementAdvisorCard22);
      List<AchievementAdvisorCard> cards23 = this._cards;
      Achievement achievement23 = Main.Achievements.GetAchievement("BEGONE_EVIL");
      double order23 = (double) num23;
      float num24 = (float) (order23 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard23 = new AchievementAdvisorCard(achievement23, (float) order23);
      cards23.Add(achievementAdvisorCard23);
      List<AchievementAdvisorCard> cards24 = this._cards;
      Achievement achievement24 = Main.Achievements.GetAchievement("EXTRA_SHINY");
      double order24 = (double) num24;
      float num25 = (float) (order24 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard24 = new AchievementAdvisorCard(achievement24, (float) order24);
      cards24.Add(achievementAdvisorCard24);
      List<AchievementAdvisorCard> cards25 = this._cards;
      Achievement achievement25 = Main.Achievements.GetAchievement("HEAD_IN_THE_CLOUDS");
      double order25 = (double) num25;
      float num26 = (float) (order25 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard25 = new AchievementAdvisorCard(achievement25, (float) order25);
      cards25.Add(achievementAdvisorCard25);
      List<AchievementAdvisorCard> cards26 = this._cards;
      Achievement achievement26 = Main.Achievements.GetAchievement("BUCKETS_OF_BOLTS");
      double order26 = (double) num26;
      float num27 = (float) (order26 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard26 = new AchievementAdvisorCard(achievement26, (float) order26);
      cards26.Add(achievementAdvisorCard26);
      List<AchievementAdvisorCard> cards27 = this._cards;
      Achievement achievement27 = Main.Achievements.GetAchievement("DRAX_ATTAX");
      double order27 = (double) num27;
      float num28 = (float) (order27 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard27 = new AchievementAdvisorCard(achievement27, (float) order27);
      cards27.Add(achievementAdvisorCard27);
      List<AchievementAdvisorCard> cards28 = this._cards;
      Achievement achievement28 = Main.Achievements.GetAchievement("PHOTOSYNTHESIS");
      double order28 = (double) num28;
      float num29 = (float) (order28 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard28 = new AchievementAdvisorCard(achievement28, (float) order28);
      cards28.Add(achievementAdvisorCard28);
      List<AchievementAdvisorCard> cards29 = this._cards;
      Achievement achievement29 = Main.Achievements.GetAchievement("GET_A_LIFE");
      double order29 = (double) num29;
      float num30 = (float) (order29 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard29 = new AchievementAdvisorCard(achievement29, (float) order29);
      cards29.Add(achievementAdvisorCard29);
      List<AchievementAdvisorCard> cards30 = this._cards;
      Achievement achievement30 = Main.Achievements.GetAchievement("THE_GREAT_SOUTHERN_PLANTKILL");
      double order30 = (double) num30;
      float num31 = (float) (order30 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard30 = new AchievementAdvisorCard(achievement30, (float) order30);
      cards30.Add(achievementAdvisorCard30);
      List<AchievementAdvisorCard> cards31 = this._cards;
      Achievement achievement31 = Main.Achievements.GetAchievement("TEMPLE_RAIDER");
      double order31 = (double) num31;
      float num32 = (float) (order31 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard31 = new AchievementAdvisorCard(achievement31, (float) order31);
      cards31.Add(achievementAdvisorCard31);
      List<AchievementAdvisorCard> cards32 = this._cards;
      Achievement achievement32 = Main.Achievements.GetAchievement("LIHZAHRDIAN_IDOL");
      double order32 = (double) num32;
      float num33 = (float) (order32 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard32 = new AchievementAdvisorCard(achievement32, (float) order32);
      cards32.Add(achievementAdvisorCard32);
      List<AchievementAdvisorCard> cards33 = this._cards;
      Achievement achievement33 = Main.Achievements.GetAchievement("ROBBING_THE_GRAVE");
      double order33 = (double) num33;
      float num34 = (float) (order33 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard33 = new AchievementAdvisorCard(achievement33, (float) order33);
      cards33.Add(achievementAdvisorCard33);
      List<AchievementAdvisorCard> cards34 = this._cards;
      Achievement achievement34 = Main.Achievements.GetAchievement("OBSESSIVE_DEVOTION");
      double order34 = (double) num34;
      float num35 = (float) (order34 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard34 = new AchievementAdvisorCard(achievement34, (float) order34);
      cards34.Add(achievementAdvisorCard34);
      List<AchievementAdvisorCard> cards35 = this._cards;
      Achievement achievement35 = Main.Achievements.GetAchievement("STAR_DESTROYER");
      double order35 = (double) num35;
      float num36 = (float) (order35 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard35 = new AchievementAdvisorCard(achievement35, (float) order35);
      cards35.Add(achievementAdvisorCard35);
      List<AchievementAdvisorCard> cards36 = this._cards;
      Achievement achievement36 = Main.Achievements.GetAchievement("CHAMPION_OF_TERRARIA");
      double order36 = (double) num36;
      float num37 = (float) (order36 + 1.0);
      AchievementAdvisorCard achievementAdvisorCard36 = new AchievementAdvisorCard(achievement36, (float) order36);
      cards36.Add(achievementAdvisorCard36);
      this._cards.OrderBy<AchievementAdvisorCard, float>((Func<AchievementAdvisorCard, float>) (x => x.order));
    }
  }
}
