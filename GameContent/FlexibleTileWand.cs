// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.FlexibleTileWand
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;
using Terraria.Utilities;

namespace Terraria.GameContent
{
  public class FlexibleTileWand
  {
    public static FlexibleTileWand RubblePlacementSmall = FlexibleTileWand.CreateRubblePlacerSmall();
    public static FlexibleTileWand RubblePlacementMedium = FlexibleTileWand.CreateRubblePlacerMedium();
    public static FlexibleTileWand RubblePlacementLarge = FlexibleTileWand.CreateRubblePlacerLarge();
    private UnifiedRandom _random = new UnifiedRandom();
    private Dictionary<int, FlexibleTileWand.OptionBucket> _options = new Dictionary<int, FlexibleTileWand.OptionBucket>();

    public void AddVariation(int itemType, int tileIdToPlace, int tileStyleToPlace)
    {
      FlexibleTileWand.OptionBucket optionBucket;
      if (!this._options.TryGetValue(itemType, out optionBucket))
        optionBucket = this._options[itemType] = new FlexibleTileWand.OptionBucket(itemType);
      optionBucket.Options.Add(new FlexibleTileWand.PlacementOption()
      {
        TileIdToPlace = tileIdToPlace,
        TileStyleToPlace = tileStyleToPlace
      });
    }

    public void AddVariations(int itemType, int tileIdToPlace, params int[] stylesToPlace)
    {
      for (int index = 0; index < stylesToPlace.Length; ++index)
      {
        int tileStyleToPlace = stylesToPlace[index];
        this.AddVariation(itemType, tileIdToPlace, tileStyleToPlace);
      }
    }

    public void AddVariations_ByRow(
      int itemType,
      int tileIdToPlace,
      int variationsPerRow,
      params int[] rows)
    {
      for (int index1 = 0; index1 < rows.Length; ++index1)
      {
        for (int index2 = 0; index2 < variationsPerRow; ++index2)
        {
          int tileStyleToPlace = rows[index1] * variationsPerRow + index2;
          this.AddVariation(itemType, tileIdToPlace, tileStyleToPlace);
        }
      }
    }

    public bool TryGetPlacementOption(
      Player player,
      int randomSeed,
      int selectCycleOffset,
      out FlexibleTileWand.PlacementOption option,
      out Item itemToConsume)
    {
      option = (FlexibleTileWand.PlacementOption) null;
      itemToConsume = (Item) null;
      Item[] inventory = player.inventory;
      for (int index = 0; index < 58; ++index)
      {
        if (index < 50 || index >= 54)
        {
          Item obj = inventory[index];
          FlexibleTileWand.OptionBucket optionBucket;
          if (!obj.IsAir && this._options.TryGetValue(obj.type, out optionBucket))
          {
            this._random.SetSeed(randomSeed);
            option = optionBucket.GetOptionWithCycling(selectCycleOffset);
            itemToConsume = obj;
            return true;
          }
        }
      }
      return false;
    }

    public static FlexibleTileWand CreateRubblePlacerLarge()
    {
      FlexibleTileWand rubblePlacerLarge = new FlexibleTileWand();
      int tileIdToPlace1 = 647;
      rubblePlacerLarge.AddVariations(154, tileIdToPlace1, 0, 1, 2, 3, 4, 5, 6);
      rubblePlacerLarge.AddVariations(3, tileIdToPlace1, 7, 8, 9, 10, 11, 12, 13, 14, 15);
      rubblePlacerLarge.AddVariations(71, tileIdToPlace1, 16, 17);
      rubblePlacerLarge.AddVariations(72, tileIdToPlace1, 18, 19);
      rubblePlacerLarge.AddVariations(73, tileIdToPlace1, 20, 21);
      rubblePlacerLarge.AddVariations(9, tileIdToPlace1, 22, 23, 24, 25);
      rubblePlacerLarge.AddVariations(593, tileIdToPlace1, 26, 27, 28, 29, 30, 31);
      rubblePlacerLarge.AddVariations(183, tileIdToPlace1, 32, 33, 34);
      int tileIdToPlace2 = 648;
      rubblePlacerLarge.AddVariations(195, tileIdToPlace2, 0, 1, 2);
      rubblePlacerLarge.AddVariations(195, tileIdToPlace2, 3, 4, 5);
      rubblePlacerLarge.AddVariations(174, tileIdToPlace2, 6, 7, 8);
      rubblePlacerLarge.AddVariations(150, tileIdToPlace2, 9, 10, 11, 12, 13);
      rubblePlacerLarge.AddVariations(3, tileIdToPlace2, 14, 15, 16);
      rubblePlacerLarge.AddVariations(989, tileIdToPlace2, 17);
      rubblePlacerLarge.AddVariations(1101, tileIdToPlace2, 18, 19, 20);
      rubblePlacerLarge.AddVariations(9, tileIdToPlace2, 21, 22);
      rubblePlacerLarge.AddVariations(9, tileIdToPlace2, 23, 24, 25, 26, 27, 28);
      rubblePlacerLarge.AddVariations(3271, tileIdToPlace2, 29, 30, 31, 32, 33, 34);
      rubblePlacerLarge.AddVariations(3086, tileIdToPlace2, 35, 36, 37, 38, 39, 40);
      rubblePlacerLarge.AddVariations(3081, tileIdToPlace2, 41, 42, 43, 44, 45, 46);
      rubblePlacerLarge.AddVariations(62, tileIdToPlace2, 47, 48, 49);
      rubblePlacerLarge.AddVariations(62, tileIdToPlace2, 50, 51);
      rubblePlacerLarge.AddVariations(154, tileIdToPlace2, 52, 53, 54);
      int tileIdToPlace3 = 651;
      rubblePlacerLarge.AddVariations(195, tileIdToPlace3, 0, 1, 2);
      rubblePlacerLarge.AddVariations(62, tileIdToPlace3, 3, 4, 5);
      rubblePlacerLarge.AddVariations(331, tileIdToPlace3, 6, 7, 8);
      return rubblePlacerLarge;
    }

    public static FlexibleTileWand CreateRubblePlacerMedium()
    {
      FlexibleTileWand rubblePlacerMedium = new FlexibleTileWand();
      ushort tileIdToPlace1 = 652;
      rubblePlacerMedium.AddVariations(195, (int) tileIdToPlace1, 0, 1, 2);
      rubblePlacerMedium.AddVariations(62, (int) tileIdToPlace1, 3, 4, 5);
      rubblePlacerMedium.AddVariations(331, (int) tileIdToPlace1, 6, 7, 8, 9, 10, 11);
      ushort tileIdToPlace2 = 649;
      rubblePlacerMedium.AddVariations(3, (int) tileIdToPlace2, 0, 1, 2, 3, 4, 5);
      rubblePlacerMedium.AddVariations(154, (int) tileIdToPlace2, 6, 7, 8, 9, 10);
      rubblePlacerMedium.AddVariations(154, (int) tileIdToPlace2, 11, 12, 13, 14, 15);
      rubblePlacerMedium.AddVariations(71, (int) tileIdToPlace2, 16);
      rubblePlacerMedium.AddVariations(72, (int) tileIdToPlace2, 17);
      rubblePlacerMedium.AddVariations(73, (int) tileIdToPlace2, 18);
      rubblePlacerMedium.AddVariations(181, (int) tileIdToPlace2, 19);
      rubblePlacerMedium.AddVariations(180, (int) tileIdToPlace2, 20);
      rubblePlacerMedium.AddVariations(177, (int) tileIdToPlace2, 21);
      rubblePlacerMedium.AddVariations(179, (int) tileIdToPlace2, 22);
      rubblePlacerMedium.AddVariations(178, (int) tileIdToPlace2, 23);
      rubblePlacerMedium.AddVariations(182, (int) tileIdToPlace2, 24);
      rubblePlacerMedium.AddVariations(593, (int) tileIdToPlace2, 25, 26, 27, 28, 29, 30);
      rubblePlacerMedium.AddVariations(9, (int) tileIdToPlace2, 31, 32, 33);
      rubblePlacerMedium.AddVariations(150, (int) tileIdToPlace2, 34, 35, 36, 37);
      rubblePlacerMedium.AddVariations(3, (int) tileIdToPlace2, 38, 39, 40);
      rubblePlacerMedium.AddVariations(3271, (int) tileIdToPlace2, 41, 42, 43, 44, 45, 46);
      rubblePlacerMedium.AddVariations(3086, (int) tileIdToPlace2, 47, 48, 49, 50, 51, 52);
      rubblePlacerMedium.AddVariations(3081, (int) tileIdToPlace2, 53, 54, 55, 56, 57, 58);
      rubblePlacerMedium.AddVariations(62, (int) tileIdToPlace2, 59, 60, 61);
      rubblePlacerMedium.AddVariations(169, (int) tileIdToPlace2, 62, 63, 64);
      return rubblePlacerMedium;
    }

    public static FlexibleTileWand CreateRubblePlacerSmall()
    {
      FlexibleTileWand rubblePlacerSmall = new FlexibleTileWand();
      ushort tileIdToPlace = 650;
      rubblePlacerSmall.AddVariations(3, (int) tileIdToPlace, 0, 1, 2, 3, 4, 5);
      rubblePlacerSmall.AddVariations(2, (int) tileIdToPlace, 6, 7, 8, 9, 10, 11);
      rubblePlacerSmall.AddVariations(154, (int) tileIdToPlace, 12, 13, 14, 15, 16, 17, 18, 19);
      rubblePlacerSmall.AddVariations(154, (int) tileIdToPlace, 20, 21, 22, 23, 24, 25, 26, 27);
      rubblePlacerSmall.AddVariations(9, (int) tileIdToPlace, 28, 29, 30, 31, 32);
      rubblePlacerSmall.AddVariations(9, (int) tileIdToPlace, 33, 34, 35);
      rubblePlacerSmall.AddVariations(593, (int) tileIdToPlace, 36, 37, 38, 39, 40, 41);
      rubblePlacerSmall.AddVariations(664, (int) tileIdToPlace, 42, 43, 44, 45, 46, 47);
      rubblePlacerSmall.AddVariations(150, (int) tileIdToPlace, 48, 49, 50, 51, 52, 53);
      rubblePlacerSmall.AddVariations(3271, (int) tileIdToPlace, 54, 55, 56, 57, 58, 59);
      rubblePlacerSmall.AddVariations(3086, (int) tileIdToPlace, 60, 61, 62, 63, 64, 65);
      rubblePlacerSmall.AddVariations(3081, (int) tileIdToPlace, 66, 67, 68, 69, 70, 71);
      rubblePlacerSmall.AddVariations(62, (int) tileIdToPlace, 72);
      rubblePlacerSmall.AddVariations(169, (int) tileIdToPlace, 73, 74, 75, 76);
      return rubblePlacerSmall;
    }

    public static void ForModders_AddPotsToWand(
      FlexibleTileWand wand,
      ref int echoPileStyle,
      ref ushort tileType)
    {
      int variationsPerRow = 3;
      echoPileStyle = 0;
      tileType = (ushort) 653;
      wand.AddVariations_ByRow(133, (int) tileType, variationsPerRow, 0, 1, 2, 3);
      wand.AddVariations_ByRow(664, (int) tileType, variationsPerRow, 4, 5, 6);
      wand.AddVariations_ByRow(176, (int) tileType, variationsPerRow, 7, 8, 9);
      wand.AddVariations_ByRow(154, (int) tileType, variationsPerRow, 10, 11, 12);
      wand.AddVariations_ByRow(173, (int) tileType, variationsPerRow, 13, 14, 15);
      wand.AddVariations_ByRow(61, (int) tileType, variationsPerRow, 16, 17, 18);
      wand.AddVariations_ByRow(150, (int) tileType, variationsPerRow, 19, 20, 21);
      wand.AddVariations_ByRow(836, (int) tileType, variationsPerRow, 22, 23, 24);
      wand.AddVariations_ByRow(607, (int) tileType, variationsPerRow, 25, 26, 27);
      wand.AddVariations_ByRow(1101, (int) tileType, variationsPerRow, 28, 29, 30);
      wand.AddVariations_ByRow(3081, (int) tileType, variationsPerRow, 31, 32, 33);
      wand.AddVariations_ByRow(607, (int) tileType, variationsPerRow, 34, 35, 36);
    }

    private class OptionBucket
    {
      public int ItemTypeToConsume;
      public List<FlexibleTileWand.PlacementOption> Options;

      public OptionBucket(int itemTypeToConsume)
      {
        this.ItemTypeToConsume = itemTypeToConsume;
        this.Options = new List<FlexibleTileWand.PlacementOption>();
      }

      public FlexibleTileWand.PlacementOption GetRandomOption(UnifiedRandom random) => this.Options[random.Next(this.Options.Count)];

      public FlexibleTileWand.PlacementOption GetOptionWithCycling(int cycleOffset)
      {
        int count = this.Options.Count;
        return this.Options[(cycleOffset % count + count) % count];
      }
    }

    public class PlacementOption
    {
      public int TileIdToPlace;
      public int TileStyleToPlace;
    }
  }
}
