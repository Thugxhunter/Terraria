// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Creative.ItemFilters
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Creative
{
  public static class ItemFilters
  {
    private const int framesPerRow = 11;
    private const int framesPerColumn = 1;
    private const int frameSizeOffsetX = -2;
    private const int frameSizeOffsetY = 0;

    public class BySearch : IItemEntryFilter, IEntryFilter<Item>, ISearchFilter<Item>
    {
      private const int _tooltipMaxLines = 30;
      private string[] _toolTipLines = new string[30];
      private bool[] _unusedPrefixLine = new bool[30];
      private bool[] _unusedBadPrefixLines = new bool[30];
      private int _unusedYoyoLogo;
      private int _unusedResearchLine;
      private string _search;

      public bool FitsFilter(Item entry)
      {
        if (this._search == null)
          return true;
        int numLines = 1;
        float knockBack = entry.knockBack;
        Main.MouseText_DrawItemTooltip_GetLinesInfo(entry, ref this._unusedYoyoLogo, ref this._unusedResearchLine, knockBack, ref numLines, this._toolTipLines, this._unusedPrefixLine, this._unusedBadPrefixLines);
        for (int index = 0; index < numLines; ++index)
        {
          if (this._toolTipLines[index].ToLower().IndexOf(this._search, StringComparison.OrdinalIgnoreCase) != -1)
            return true;
        }
        return false;
      }

      public string GetDisplayNameKey() => "CreativePowers.TabSearch";

      public UIElement GetImage()
      {
        Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Rank_Light", (AssetRequestMode) 1);
        UIImageFramed image = new UIImageFramed(asset, asset.Frame());
        image.HAlign = 0.5f;
        image.VAlign = 0.5f;
        return (UIElement) image;
      }

      public void SetSearch(string searchText) => this._search = searchText;
    }

    public class BuildingBlock : IItemEntryFilter, IEntryFilter<Item>
    {
      public bool FitsFilter(Item entry)
      {
        if (entry.createWall != -1 || entry.tileWand != -1)
          return true;
        return entry.createTile != -1 && !Main.tileFrameImportant[entry.createTile];
      }

      public string GetDisplayNameKey() => "CreativePowers.TabBlocks";

      public UIElement GetImage()
      {
        Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", (AssetRequestMode) 1);
        UIImageFramed image = new UIImageFramed(asset, asset.Frame(11, frameX: 4).OffsetSize(-2, 0));
        image.HAlign = 0.5f;
        image.VAlign = 0.5f;
        return (UIElement) image;
      }
    }

    public class Furniture : IItemEntryFilter, IEntryFilter<Item>
    {
      public bool FitsFilter(Item entry)
      {
        int createTile = entry.createTile;
        return createTile != -1 && Main.tileFrameImportant[createTile];
      }

      public string GetDisplayNameKey() => "CreativePowers.TabFurniture";

      public UIElement GetImage()
      {
        Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", (AssetRequestMode) 1);
        UIImageFramed image = new UIImageFramed(asset, asset.Frame(11, frameX: 7).OffsetSize(-2, 0));
        image.HAlign = 0.5f;
        image.VAlign = 0.5f;
        return (UIElement) image;
      }
    }

    public class Tools : IItemEntryFilter, IEntryFilter<Item>
    {
      private HashSet<int> _itemIdsThatAreAccepted = new HashSet<int>()
      {
        509,
        850,
        851,
        3612,
        3625,
        3611,
        510,
        849,
        3620,
        1071,
        1543,
        1072,
        1544,
        1100,
        1545,
        50,
        3199,
        3124,
        5358,
        5359,
        5360,
        5361,
        5437,
        1326,
        5335,
        3384,
        4263,
        4819,
        4262,
        946,
        4707,
        205,
        206,
        207,
        1128,
        3031,
        4820,
        5302,
        5364,
        4460,
        4608,
        4872,
        3032,
        5303,
        5304,
        1991,
        4821,
        3183,
        779,
        5134,
        1299,
        4711,
        4049,
        114
      };

      public bool FitsFilter(Item entry) => entry.pick > 0 || entry.axe > 0 || entry.hammer > 0 || entry.fishingPole > 0 || entry.tileWand != -1 || this._itemIdsThatAreAccepted.Contains(entry.type);

      public string GetDisplayNameKey() => "CreativePowers.TabTools";

      public UIElement GetImage()
      {
        Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", (AssetRequestMode) 1);
        UIImageFramed image = new UIImageFramed(asset, asset.Frame(11, frameX: 6).OffsetSize(-2, 0));
        image.HAlign = 0.5f;
        image.VAlign = 0.5f;
        return (UIElement) image;
      }
    }

    public class Weapon : IItemEntryFilter, IEntryFilter<Item>
    {
      public bool FitsFilter(Item entry) => entry.damage > 0;

      public string GetDisplayNameKey() => "CreativePowers.TabWeapons";

      public UIElement GetImage()
      {
        Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", (AssetRequestMode) 1);
        UIImageFramed image = new UIImageFramed(asset, asset.Frame(11).OffsetSize(-2, 0));
        image.HAlign = 0.5f;
        image.VAlign = 0.5f;
        return (UIElement) image;
      }
    }

    public abstract class AArmor
    {
      public bool IsAnArmorThatMatchesSocialState(Item entry, bool shouldBeSocial) => (entry.bodySlot != -1 || entry.headSlot != -1 ? 1 : (entry.legSlot != -1 ? 1 : 0)) != 0 && entry.vanity == shouldBeSocial;
    }

    public class Armor : ItemFilters.AArmor, IItemEntryFilter, IEntryFilter<Item>
    {
      public bool FitsFilter(Item entry) => this.IsAnArmorThatMatchesSocialState(entry, false);

      public string GetDisplayNameKey() => "CreativePowers.TabArmor";

      public UIElement GetImage()
      {
        Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", (AssetRequestMode) 1);
        UIImageFramed image = new UIImageFramed(asset, asset.Frame(11, frameX: 2).OffsetSize(-2, 0));
        image.HAlign = 0.5f;
        image.VAlign = 0.5f;
        return (UIElement) image;
      }
    }

    public class Vanity : ItemFilters.AArmor, IItemEntryFilter, IEntryFilter<Item>
    {
      public bool FitsFilter(Item entry) => this.IsAnArmorThatMatchesSocialState(entry, true);

      public string GetDisplayNameKey() => "CreativePowers.TabVanity";

      public UIElement GetImage()
      {
        Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", (AssetRequestMode) 1);
        UIImageFramed image = new UIImageFramed(asset, asset.Frame(11, frameX: 8).OffsetSize(-2, 0));
        image.HAlign = 0.5f;
        image.VAlign = 0.5f;
        return (UIElement) image;
      }
    }

    public abstract class AAccessories
    {
      public bool IsAnAccessoryOfType(
        Item entry,
        ItemFilters.AAccessories.AccessoriesCategory categoryType)
      {
        bool flag = ItemSlot.IsMiscEquipment(entry);
        return flag && categoryType == ItemFilters.AAccessories.AccessoriesCategory.Misc || !flag && categoryType == ItemFilters.AAccessories.AccessoriesCategory.NonMisc && entry.accessory;
      }

      public enum AccessoriesCategory
      {
        Misc,
        NonMisc,
      }
    }

    public class Accessories : ItemFilters.AAccessories, IItemEntryFilter, IEntryFilter<Item>
    {
      public bool FitsFilter(Item entry) => this.IsAnAccessoryOfType(entry, ItemFilters.AAccessories.AccessoriesCategory.NonMisc);

      public string GetDisplayNameKey() => "CreativePowers.TabAccessories";

      public UIElement GetImage()
      {
        Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", (AssetRequestMode) 1);
        UIImageFramed image = new UIImageFramed(asset, asset.Frame(11, frameX: 1).OffsetSize(-2, 0));
        image.HAlign = 0.5f;
        image.VAlign = 0.5f;
        return (UIElement) image;
      }
    }

    public class MiscAccessories : ItemFilters.AAccessories, IItemEntryFilter, IEntryFilter<Item>
    {
      public bool FitsFilter(Item entry) => this.IsAnAccessoryOfType(entry, ItemFilters.AAccessories.AccessoriesCategory.Misc);

      public string GetDisplayNameKey() => "CreativePowers.TabAccessoriesMisc";

      public UIElement GetImage()
      {
        Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", (AssetRequestMode) 1);
        UIImageFramed image = new UIImageFramed(asset, asset.Frame(11, frameX: 9).OffsetSize(-2, 0));
        image.HAlign = 0.5f;
        image.VAlign = 0.5f;
        return (UIElement) image;
      }
    }

    public class Consumables : IItemEntryFilter, IEntryFilter<Item>
    {
      public bool FitsFilter(Item entry)
      {
        switch (entry.type)
        {
          case 267:
          case 1307:
            return true;
          default:
            bool flag = entry.createTile != -1 || entry.createWall != -1 || entry.tileWand != -1;
            return entry.consumable && !flag;
        }
      }

      public string GetDisplayNameKey() => "CreativePowers.TabConsumables";

      public UIElement GetImage()
      {
        Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", (AssetRequestMode) 1);
        UIImageFramed image = new UIImageFramed(asset, asset.Frame(11, frameX: 3).OffsetSize(-2, 0));
        image.HAlign = 0.5f;
        image.VAlign = 0.5f;
        return (UIElement) image;
      }
    }

    public class GameplayItems : IItemEntryFilter, IEntryFilter<Item>
    {
      public bool FitsFilter(Item entry) => ItemID.Sets.SortingPriorityBossSpawns[entry.type] != -1;

      public string GetDisplayNameKey() => "CreativePowers.TabMisc";

      public UIElement GetImage()
      {
        Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", (AssetRequestMode) 1);
        UIImageFramed image = new UIImageFramed(asset, asset.Frame(11, frameX: 5).OffsetSize(-2, 0));
        image.HAlign = 0.5f;
        image.VAlign = 0.5f;
        return (UIElement) image;
      }
    }

    public class MiscFallback : IItemEntryFilter, IEntryFilter<Item>
    {
      private bool[] _fitsFilterByItemType;

      public MiscFallback(List<IItemEntryFilter> otherFiltersToCheckAgainst)
      {
        short count = ItemID.Count;
        this._fitsFilterByItemType = new bool[(int) count];
        for (int index1 = 1; index1 < (int) count; ++index1)
        {
          this._fitsFilterByItemType[index1] = true;
          Item entry = ContentSamples.ItemsByType[index1];
          if (!CreativeItemSacrificesCatalog.Instance.TryGetSacrificeCountCapToUnlockInfiniteItems(index1, out int _))
          {
            this._fitsFilterByItemType[index1] = false;
          }
          else
          {
            for (int index2 = 0; index2 < otherFiltersToCheckAgainst.Count; ++index2)
            {
              if (otherFiltersToCheckAgainst[index2].FitsFilter(entry))
              {
                this._fitsFilterByItemType[index1] = false;
                break;
              }
            }
          }
        }
      }

      public bool FitsFilter(Item entry) => this._fitsFilterByItemType.IndexInRange<bool>(entry.type) && this._fitsFilterByItemType[entry.type];

      public string GetDisplayNameKey() => "CreativePowers.TabMisc";

      public UIElement GetImage()
      {
        Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", (AssetRequestMode) 1);
        UIImageFramed image = new UIImageFramed(asset, asset.Frame(11, frameX: 5).OffsetSize(-2, 0));
        image.HAlign = 0.5f;
        image.VAlign = 0.5f;
        return (UIElement) image;
      }
    }

    public class Materials : IItemEntryFilter, IEntryFilter<Item>
    {
      public bool FitsFilter(Item entry) => entry.material;

      public string GetDisplayNameKey() => "CreativePowers.TabMaterials";

      public UIElement GetImage()
      {
        Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", (AssetRequestMode) 1);
        UIImageFramed image = new UIImageFramed(asset, asset.Frame(11, frameX: 10).OffsetSize(-2, 0));
        image.HAlign = 0.5f;
        image.VAlign = 0.5f;
        return (UIElement) image;
      }
    }
  }
}
