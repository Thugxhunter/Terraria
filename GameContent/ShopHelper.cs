// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ShopHelper
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent
{
  public class ShopHelper
  {
    public const float LowestPossiblePriceMultiplier = 0.75f;
    public const float MaxHappinessAchievementPriceMultiplier = 0.82f;
    public const float HighestPossiblePriceMultiplier = 1.5f;
    private string _currentHappiness;
    private float _currentPriceAdjustment;
    private NPC _currentNPCBeingTalkedTo;
    private Player _currentPlayerTalking;
    private PersonalityDatabase _database;
    private AShoppingBiome[] _dangerousBiomes = new AShoppingBiome[3]
    {
      (AShoppingBiome) new CorruptionBiome(),
      (AShoppingBiome) new CrimsonBiome(),
      (AShoppingBiome) new DungeonBiome()
    };
    private const float likeValue = 0.94f;
    private const float dislikeValue = 1.06f;
    private const float loveValue = 0.88f;
    private const float hateValue = 1.12f;

    public ShopHelper()
    {
      this._database = new PersonalityDatabase();
      new PersonalityDatabasePopulator().Populate(this._database);
    }

    public ShoppingSettings GetShoppingSettings(Player player, NPC npc)
    {
      ShoppingSettings shoppingSettings = new ShoppingSettings()
      {
        PriceAdjustment = 1.0,
        HappinessReport = ""
      };
      this._currentNPCBeingTalkedTo = npc;
      this._currentPlayerTalking = player;
      this.ProcessMood(player, npc);
      shoppingSettings.PriceAdjustment = (double) this._currentPriceAdjustment;
      shoppingSettings.HappinessReport = this._currentHappiness;
      return shoppingSettings;
    }

    private float GetSkeletonMerchantPrices(NPC npc)
    {
      float skeletonMerchantPrices = 1f;
      if (Main.moonPhase == 1 || Main.moonPhase == 7)
        skeletonMerchantPrices = 1.1f;
      if (Main.moonPhase == 2 || Main.moonPhase == 6)
        skeletonMerchantPrices = 1.2f;
      if (Main.moonPhase == 3 || Main.moonPhase == 5)
        skeletonMerchantPrices = 1.3f;
      if (Main.moonPhase == 4)
        skeletonMerchantPrices = 1.4f;
      if (Main.dayTime)
        skeletonMerchantPrices += 0.1f;
      return skeletonMerchantPrices;
    }

    private float GetTravelingMerchantPrices(NPC npc) => (float) ((2.0 + (double) (1.5f - Vector2.Distance(npc.Center / 16f, new Vector2((float) Main.spawnTileX, (float) Main.spawnTileY)) / (float) (Main.maxTilesX / 2))) / 3.0);

    private void ProcessMood(Player player, NPC npc)
    {
      this._currentHappiness = "";
      this._currentPriceAdjustment = 1f;
      if (Main.remixWorld)
        return;
      if (npc.type == 368)
        this._currentPriceAdjustment = 1f;
      else if (npc.type == 453)
      {
        this._currentPriceAdjustment = 1f;
      }
      else
      {
        if (NPCID.Sets.IsTownPet[npc.type])
          return;
        if (this.IsNotReallyTownNPC(npc))
        {
          this._currentPriceAdjustment = 1f;
        }
        else
        {
          if (this.RuinMoodIfHomeless(npc))
            this._currentPriceAdjustment = 1000f;
          else if (this.IsFarFromHome(npc))
            this._currentPriceAdjustment = 1000f;
          if (this.IsPlayerInEvilBiomes(player))
            this._currentPriceAdjustment = 1000f;
          int npcsWithinHouse;
          int npcsWithinVillage;
          List<NPC> nearbyResidentNpCs = this.GetNearbyResidentNPCs(npc, out npcsWithinHouse, out npcsWithinVillage);
          bool flag = true;
          float num = 1.05f;
          if (npc.type == 663)
          {
            flag = false;
            num = 1f;
            if (npcsWithinHouse < 2 && npcsWithinVillage < 2)
            {
              this.AddHappinessReportText("HateLonely");
              this._currentPriceAdjustment = 1000f;
            }
          }
          if (npcsWithinHouse > 3)
          {
            for (int index = 3; index < npcsWithinHouse; ++index)
              this._currentPriceAdjustment *= num;
            if (npcsWithinHouse > 6)
              this.AddHappinessReportText("HateCrowded");
            else
              this.AddHappinessReportText("DislikeCrowded");
          }
          if (flag && npcsWithinHouse <= 2 && npcsWithinVillage < 4)
          {
            this.AddHappinessReportText("LoveSpace");
            this._currentPriceAdjustment *= 0.95f;
          }
          bool[] flagArray = new bool[(int) NPCID.Count];
          foreach (NPC npc1 in nearbyResidentNpCs)
            flagArray[npc1.type] = true;
          HelperInfo info = new HelperInfo()
          {
            player = player,
            npc = npc,
            NearbyNPCs = nearbyResidentNpCs,
            nearbyNPCsByType = flagArray
          };
          foreach (IShopPersonalityTrait shopModifier in this._database.GetByNPCID(npc.type).ShopModifiers)
            shopModifier.ModifyShopPrice(info, this);
          new AllPersonalitiesModifier().ModifyShopPrice(info, this);
          if (this._currentHappiness == "")
            this.AddHappinessReportText("Content");
          this._currentPriceAdjustment = this.LimitAndRoundMultiplier(this._currentPriceAdjustment);
        }
      }
    }

    private float LimitAndRoundMultiplier(float priceAdjustment)
    {
      priceAdjustment = MathHelper.Clamp(priceAdjustment, 0.75f, 1.5f);
      priceAdjustment = (float) Math.Round((double) priceAdjustment * 100.0) / 100f;
      return priceAdjustment;
    }

    private static string BiomeNameByKey(string biomeNameKey) => Language.GetTextValue("TownNPCMoodBiomes." + biomeNameKey);

    private void AddHappinessReportText(string textKeyInCategory, object substitutes = null)
    {
      string str = "TownNPCMood_" + NPCID.Search.GetName(this._currentNPCBeingTalkedTo.netID);
      if (this._currentNPCBeingTalkedTo.type == 633 && this._currentNPCBeingTalkedTo.altTexture == 2)
        str += "Transformed";
      this._currentHappiness = this._currentHappiness + Language.GetTextValueWith(str + "." + textKeyInCategory, substitutes) + " ";
    }

    public void LikeBiome(string nameKey)
    {
      this.AddHappinessReportText(nameof (LikeBiome), (object) new
      {
        BiomeName = ShopHelper.BiomeNameByKey(nameKey)
      });
      this._currentPriceAdjustment *= 0.94f;
    }

    public void LoveBiome(string nameKey)
    {
      this.AddHappinessReportText(nameof (LoveBiome), (object) new
      {
        BiomeName = ShopHelper.BiomeNameByKey(nameKey)
      });
      this._currentPriceAdjustment *= 0.88f;
    }

    public void DislikeBiome(string nameKey)
    {
      this.AddHappinessReportText(nameof (DislikeBiome), (object) new
      {
        BiomeName = ShopHelper.BiomeNameByKey(nameKey)
      });
      this._currentPriceAdjustment *= 1.06f;
    }

    public void HateBiome(string nameKey)
    {
      this.AddHappinessReportText(nameof (HateBiome), (object) new
      {
        BiomeName = ShopHelper.BiomeNameByKey(nameKey)
      });
      this._currentPriceAdjustment *= 1.12f;
    }

    public void LikeNPC(int npcType)
    {
      this.AddHappinessReportText(nameof (LikeNPC), (object) new
      {
        NPCName = NPC.GetFullnameByID(npcType)
      });
      this._currentPriceAdjustment *= 0.94f;
    }

    public void LoveNPCByTypeName(int npcType)
    {
      this.AddHappinessReportText("LoveNPC_" + NPCID.Search.GetName(npcType), (object) new
      {
        NPCName = NPC.GetFullnameByID(npcType)
      });
      this._currentPriceAdjustment *= 0.88f;
    }

    public void LikePrincess()
    {
      this.AddHappinessReportText("LikeNPC_Princess", (object) new
      {
        NPCName = NPC.GetFullnameByID(663)
      });
      this._currentPriceAdjustment *= 0.94f;
    }

    public void LoveNPC(int npcType)
    {
      this.AddHappinessReportText(nameof (LoveNPC), (object) new
      {
        NPCName = NPC.GetFullnameByID(npcType)
      });
      this._currentPriceAdjustment *= 0.88f;
    }

    public void DislikeNPC(int npcType)
    {
      this.AddHappinessReportText(nameof (DislikeNPC), (object) new
      {
        NPCName = NPC.GetFullnameByID(npcType)
      });
      this._currentPriceAdjustment *= 1.06f;
    }

    public void HateNPC(int npcType)
    {
      this.AddHappinessReportText(nameof (HateNPC), (object) new
      {
        NPCName = NPC.GetFullnameByID(npcType)
      });
      this._currentPriceAdjustment *= 1.12f;
    }

    private List<NPC> GetNearbyResidentNPCs(
      NPC npc,
      out int npcsWithinHouse,
      out int npcsWithinVillage)
    {
      List<NPC> nearbyResidentNpCs = new List<NPC>();
      npcsWithinHouse = 0;
      npcsWithinVillage = 0;
      Vector2 vector2_1 = new Vector2((float) npc.homeTileX, (float) npc.homeTileY);
      if (npc.homeless)
        vector2_1 = new Vector2(npc.Center.X / 16f, npc.Center.Y / 16f);
      for (int index = 0; index < 200; ++index)
      {
        if (index != npc.whoAmI)
        {
          NPC npc1 = Main.npc[index];
          if (npc1.active && npc1.townNPC && !this.IsNotReallyTownNPC(npc1) && !WorldGen.TownManager.CanNPCsLiveWithEachOther_ShopHelper(npc, npc1))
          {
            Vector2 vector2_2 = new Vector2((float) npc1.homeTileX, (float) npc1.homeTileY);
            if (npc1.homeless)
              vector2_2 = npc1.Center / 16f;
            float num = Vector2.Distance(vector2_1, vector2_2);
            if ((double) num < 25.0)
            {
              nearbyResidentNpCs.Add(npc1);
              ++npcsWithinHouse;
            }
            else if ((double) num < 120.0)
              ++npcsWithinVillage;
          }
        }
      }
      return nearbyResidentNpCs;
    }

    private bool RuinMoodIfHomeless(NPC npc)
    {
      if (npc.homeless)
        this.AddHappinessReportText("NoHome");
      return npc.homeless;
    }

    private bool IsFarFromHome(NPC npc)
    {
      if ((double) Vector2.Distance(new Vector2((float) npc.homeTileX, (float) npc.homeTileY), new Vector2(npc.Center.X / 16f, npc.Center.Y / 16f)) <= 120.0)
        return false;
      this.AddHappinessReportText("FarFromHome");
      return true;
    }

    private bool IsPlayerInEvilBiomes(Player player)
    {
      for (int index = 0; index < this._dangerousBiomes.Length; ++index)
      {
        AShoppingBiome dangerousBiome = this._dangerousBiomes[index];
        if (dangerousBiome.IsInBiome(player))
        {
          this.AddHappinessReportText("HateBiome", (object) new
          {
            BiomeName = ShopHelper.BiomeNameByKey(dangerousBiome.NameKey)
          });
          return true;
        }
      }
      return false;
    }

    private bool IsNotReallyTownNPC(NPC npc)
    {
      switch (npc.type)
      {
        case 37:
        case 368:
        case 453:
          return true;
        default:
          return false;
      }
    }
  }
}
