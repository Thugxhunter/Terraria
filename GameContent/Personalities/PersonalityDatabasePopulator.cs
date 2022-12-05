// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Personalities.PersonalityDatabasePopulator
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.GameContent.Personalities
{
  public class PersonalityDatabasePopulator
  {
    private PersonalityDatabase _currentDatabase;

    public void Populate(PersonalityDatabase database)
    {
      this._currentDatabase = database;
      this.Populate_BiomePreferences(database);
    }

    private void Populate_BiomePreferences(PersonalityDatabase database)
    {
      OceanBiome oceanBiome = new OceanBiome();
      ForestBiome forestBiome = new ForestBiome();
      SnowBiome snowBiome = new SnowBiome();
      DesertBiome desertBiome = new DesertBiome();
      JungleBiome jungleBiome = new JungleBiome();
      UndergroundBiome undergroundBiome = new UndergroundBiome();
      HallowBiome hallowBiome = new HallowBiome();
      MushroomBiome mushroomBiome = new MushroomBiome();
      AffectionLevel affectionLevel1 = AffectionLevel.Love;
      AffectionLevel affectionLevel2 = AffectionLevel.Like;
      AffectionLevel affectionLevel3 = AffectionLevel.Dislike;
      AffectionLevel affectionLevel4 = AffectionLevel.Hate;
      database.Register(22, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) forestBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) oceanBiome
        }
      });
      database.Register(17, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) forestBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) desertBiome
        }
      });
      database.Register(588, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) forestBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) undergroundBiome
        }
      });
      database.Register(633, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) forestBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) desertBiome
        }
      });
      database.Register(441, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) snowBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) hallowBiome
        }
      });
      database.Register(124, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) snowBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) undergroundBiome
        }
      });
      database.Register(209, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) snowBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) jungleBiome
        }
      });
      database.Register(142, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel1,
          (AShoppingBiome) snowBiome
        },
        {
          affectionLevel4,
          (AShoppingBiome) desertBiome
        }
      });
      database.Register(207, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) desertBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) forestBiome
        }
      });
      database.Register(19, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) desertBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) snowBiome
        }
      });
      database.Register(178, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) desertBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) jungleBiome
        }
      });
      database.Register(20, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) jungleBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) desertBiome
        }
      });
      database.Register(228, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) jungleBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) hallowBiome
        }
      });
      database.Register(227, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) jungleBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) forestBiome
        }
      });
      database.Register(369, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) oceanBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) desertBiome
        }
      });
      database.Register(229, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) oceanBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) undergroundBiome
        }
      });
      database.Register(353, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) oceanBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) snowBiome
        }
      });
      database.Register(38, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) undergroundBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) oceanBiome
        }
      });
      database.Register(107, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) undergroundBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) jungleBiome
        }
      });
      database.Register(54, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) undergroundBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) hallowBiome
        }
      });
      database.Register(108, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) hallowBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) oceanBiome
        }
      });
      database.Register(18, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) hallowBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) snowBiome
        }
      });
      database.Register(208, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) hallowBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) undergroundBiome
        }
      });
      database.Register(550, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) hallowBiome
        },
        {
          affectionLevel3,
          (AShoppingBiome) snowBiome
        }
      });
      database.Register(160, (IShopPersonalityTrait) new BiomePreferenceListTrait()
      {
        {
          affectionLevel2,
          (AShoppingBiome) mushroomBiome
        }
      });
    }
  }
}
