// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.TownNPCProfiles
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;

namespace Terraria.GameContent
{
  public class TownNPCProfiles
  {
    private const string DefaultNPCFileFolderPath = "Images/TownNPCs/";
    private const string ShimmeredNPCFileFolderPath = "Images/TownNPCs/Shimmered/";
    private static readonly int[] CatHeadIDs = new int[6]
    {
      27,
      28,
      29,
      30,
      31,
      32
    };
    private static readonly int[] DogHeadIDs = new int[6]
    {
      33,
      34,
      35,
      36,
      37,
      38
    };
    private static readonly int[] BunnyHeadIDs = new int[6]
    {
      39,
      40,
      41,
      42,
      43,
      44
    };
    private static readonly int[] SlimeHeadIDs = new int[8]
    {
      46,
      47,
      48,
      49,
      50,
      51,
      52,
      53
    };
    private Dictionary<int, ITownNPCProfile> _townNPCProfiles = new Dictionary<int, ITownNPCProfile>()
    {
      {
        22,
        TownNPCProfiles.LegacyWithSimpleShimmer("Guide", 1, 72, false, false)
      },
      {
        20,
        TownNPCProfiles.LegacyWithSimpleShimmer("Dryad", 5, 73, false, false)
      },
      {
        19,
        TownNPCProfiles.LegacyWithSimpleShimmer("ArmsDealer", 6, 74, false, false)
      },
      {
        107,
        TownNPCProfiles.LegacyWithSimpleShimmer("GoblinTinkerer", 9, 75, false, false)
      },
      {
        160,
        TownNPCProfiles.LegacyWithSimpleShimmer("Truffle", 12, 76, false, false)
      },
      {
        208,
        TownNPCProfiles.LegacyWithSimpleShimmer("PartyGirl", 15, 77, false, false)
      },
      {
        228,
        TownNPCProfiles.LegacyWithSimpleShimmer("WitchDoctor", 18, 78, false, false)
      },
      {
        550,
        TownNPCProfiles.LegacyWithSimpleShimmer("Tavernkeep", 24, 79, false, false)
      },
      {
        369,
        TownNPCProfiles.LegacyWithSimpleShimmer("Angler", 22, 55, uniquePartyTextureShimmered: false)
      },
      {
        54,
        TownNPCProfiles.LegacyWithSimpleShimmer("Clothier", 7, 57, uniquePartyTextureShimmered: false)
      },
      {
        209,
        TownNPCProfiles.LegacyWithSimpleShimmer("Cyborg", 16, 58)
      },
      {
        38,
        TownNPCProfiles.LegacyWithSimpleShimmer("Demolitionist", 4, 59)
      },
      {
        207,
        TownNPCProfiles.LegacyWithSimpleShimmer("DyeTrader", 14, 60)
      },
      {
        588,
        TownNPCProfiles.LegacyWithSimpleShimmer("Golfer", 25, 61, uniquePartyTextureShimmered: false)
      },
      {
        124,
        TownNPCProfiles.LegacyWithSimpleShimmer("Mechanic", 8, 62)
      },
      {
        17,
        TownNPCProfiles.LegacyWithSimpleShimmer("Merchant", 2, 63)
      },
      {
        18,
        TownNPCProfiles.LegacyWithSimpleShimmer("Nurse", 3, 64)
      },
      {
        227,
        TownNPCProfiles.LegacyWithSimpleShimmer("Painter", 17, 65, uniquePartyTextureShimmered: false)
      },
      {
        229,
        TownNPCProfiles.LegacyWithSimpleShimmer("Pirate", 19, 66)
      },
      {
        142,
        TownNPCProfiles.LegacyWithSimpleShimmer("Santa", 11, 67)
      },
      {
        178,
        TownNPCProfiles.LegacyWithSimpleShimmer("Steampunker", 13, 68, uniquePartyTextureShimmered: false)
      },
      {
        353,
        TownNPCProfiles.LegacyWithSimpleShimmer("Stylist", 20, 69)
      },
      {
        441,
        TownNPCProfiles.LegacyWithSimpleShimmer("TaxCollector", 23, 70)
      },
      {
        108,
        TownNPCProfiles.LegacyWithSimpleShimmer("Wizard", 10, 71)
      },
      {
        663,
        TownNPCProfiles.LegacyWithSimpleShimmer("Princess", 45, 54)
      },
      {
        633,
        TownNPCProfiles.TransformableWithSimpleShimmer("BestiaryGirl", 26, 56, uniqueCreditTextureShimmered: false)
      },
      {
        37,
        TownNPCProfiles.LegacyWithSimpleShimmer("OldMan", -1, -1, false, false)
      },
      {
        453,
        TownNPCProfiles.LegacyWithSimpleShimmer("SkeletonMerchant", -1, -1)
      },
      {
        368,
        TownNPCProfiles.LegacyWithSimpleShimmer("TravelingMerchant", 21, 80)
      },
      {
        637,
        (ITownNPCProfile) new Profiles.VariantNPCProfile("Images/TownNPCs/Cat", "Cat", TownNPCProfiles.CatHeadIDs, new string[6]
        {
          "Siamese",
          "Black",
          "OrangeTabby",
          "RussianBlue",
          "Silver",
          "White"
        })
      },
      {
        638,
        (ITownNPCProfile) new Profiles.VariantNPCProfile("Images/TownNPCs/Dog", "Dog", TownNPCProfiles.DogHeadIDs, new string[6]
        {
          "Labrador",
          "PitBull",
          "Beagle",
          "Corgi",
          "Dalmation",
          "Husky"
        })
      },
      {
        656,
        (ITownNPCProfile) new Profiles.VariantNPCProfile("Images/TownNPCs/Bunny", "Bunny", TownNPCProfiles.BunnyHeadIDs, new string[6]
        {
          "White",
          "Angora",
          "Dutch",
          "Flemish",
          "Lop",
          "Silver"
        })
      },
      {
        670,
        (ITownNPCProfile) new Profiles.LegacyNPCProfile("Images/TownNPCs/SlimeBlue", 46, uniquePartyTexture: false)
      },
      {
        678,
        (ITownNPCProfile) new Profiles.LegacyNPCProfile("Images/TownNPCs/SlimeGreen", 47)
      },
      {
        679,
        (ITownNPCProfile) new Profiles.LegacyNPCProfile("Images/TownNPCs/SlimeOld", 48)
      },
      {
        680,
        (ITownNPCProfile) new Profiles.LegacyNPCProfile("Images/TownNPCs/SlimePurple", 49)
      },
      {
        681,
        (ITownNPCProfile) new Profiles.LegacyNPCProfile("Images/TownNPCs/SlimeRainbow", 50)
      },
      {
        682,
        (ITownNPCProfile) new Profiles.LegacyNPCProfile("Images/TownNPCs/SlimeRed", 51)
      },
      {
        683,
        (ITownNPCProfile) new Profiles.LegacyNPCProfile("Images/TownNPCs/SlimeYellow", 52)
      },
      {
        684,
        (ITownNPCProfile) new Profiles.LegacyNPCProfile("Images/TownNPCs/SlimeCopper", 53)
      }
    };
    public static TownNPCProfiles Instance = new TownNPCProfiles();

    public bool GetProfile(int npcId, out ITownNPCProfile profile) => this._townNPCProfiles.TryGetValue(npcId, out profile);

    public static ITownNPCProfile LegacyWithSimpleShimmer(
      string subPath,
      int headIdNormal,
      int headIdShimmered,
      bool uniquePartyTexture = true,
      bool uniquePartyTextureShimmered = true)
    {
      return (ITownNPCProfile) new Profiles.StackedNPCProfile(new ITownNPCProfile[2]
      {
        (ITownNPCProfile) new Profiles.LegacyNPCProfile("Images/TownNPCs/" + subPath, headIdNormal, uniquePartyTexture: uniquePartyTexture),
        (ITownNPCProfile) new Profiles.LegacyNPCProfile("Images/TownNPCs/Shimmered/" + subPath, headIdShimmered, uniquePartyTexture: uniquePartyTextureShimmered)
      });
    }

    public static ITownNPCProfile TransformableWithSimpleShimmer(
      string subPath,
      int headIdNormal,
      int headIdShimmered,
      bool uniqueCreditTexture = true,
      bool uniqueCreditTextureShimmered = true)
    {
      return (ITownNPCProfile) new Profiles.StackedNPCProfile(new ITownNPCProfile[2]
      {
        (ITownNPCProfile) new Profiles.TransformableNPCProfile("Images/TownNPCs/" + subPath, headIdNormal, uniqueCreditTexture),
        (ITownNPCProfile) new Profiles.TransformableNPCProfile("Images/TownNPCs/Shimmered/" + subPath, headIdShimmered, uniqueCreditTextureShimmered)
      });
    }

    public static int GetHeadIndexSafe(NPC npc)
    {
      ITownNPCProfile profile;
      return TownNPCProfiles.Instance.GetProfile(npc.type, out profile) ? profile.GetHeadTextureIndex(npc) : NPC.TypeToDefaultHeadIndex(npc.type);
    }
  }
}
