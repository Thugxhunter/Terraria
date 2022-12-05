// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.TreePaintSystemData
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.GameContent
{
  public static class TreePaintSystemData
  {
    private static TreePaintingSettings DefaultNoSpecialGroups = new TreePaintingSettings()
    {
      UseSpecialGroups = false
    };
    private static TreePaintingSettings DefaultNoSpecialGroups_ForWalls = new TreePaintingSettings()
    {
      UseSpecialGroups = false,
      UseWallShaderHacks = true
    };
    private static TreePaintingSettings DefaultDirt = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.03f,
      SpecialGroupMaximumHueValue = 0.08f,
      SpecialGroupMinimumSaturationValue = 0.38f,
      SpecialGroupMaximumSaturationValue = 0.53f,
      InvertSpecialGroupResult = true
    };
    private static TreePaintingSettings CullMud = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      HueTestOffset = 0.5f,
      SpecialGroupMinimalHueValue = 0.42f,
      SpecialGroupMaximumHueValue = 0.55f,
      SpecialGroupMinimumSaturationValue = 0.2f,
      SpecialGroupMaximumSaturationValue = 0.27f,
      InvertSpecialGroupResult = true
    };
    private static TreePaintingSettings WoodPurity = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.166666672f,
      SpecialGroupMaximumHueValue = 0.8333333f,
      SpecialGroupMinimumSaturationValue = 0.0f,
      SpecialGroupMaximumSaturationValue = 1f
    };
    private static TreePaintingSettings WoodCorruption = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.5f,
      SpecialGroupMaximumHueValue = 1f,
      SpecialGroupMinimumSaturationValue = 0.27f,
      SpecialGroupMaximumSaturationValue = 1f
    };
    private static TreePaintingSettings WoodJungle = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.166666672f,
      SpecialGroupMaximumHueValue = 0.8333333f,
      SpecialGroupMinimumSaturationValue = 0.0f,
      SpecialGroupMaximumSaturationValue = 1f
    };
    private static TreePaintingSettings WoodHallow = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.0f,
      SpecialGroupMaximumHueValue = 1f,
      SpecialGroupMinimumSaturationValue = 0.0f,
      SpecialGroupMaximumSaturationValue = 0.34f,
      InvertSpecialGroupResult = true
    };
    private static TreePaintingSettings WoodSnow = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.0f,
      SpecialGroupMaximumHueValue = 0.06944445f,
      SpecialGroupMinimumSaturationValue = 0.0f,
      SpecialGroupMaximumSaturationValue = 1f
    };
    private static TreePaintingSettings WoodCrimson = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.333333343f,
      SpecialGroupMaximumHueValue = 0.6666667f,
      SpecialGroupMinimumSaturationValue = 0.0f,
      SpecialGroupMaximumSaturationValue = 1f,
      InvertSpecialGroupResult = true
    };
    private static TreePaintingSettings WoodJungleUnderground = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.166666672f,
      SpecialGroupMaximumHueValue = 0.8333333f,
      SpecialGroupMinimumSaturationValue = 0.0f,
      SpecialGroupMaximumSaturationValue = 1f
    };
    private static TreePaintingSettings WoodGlowingMushroom = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.5f,
      SpecialGroupMaximumHueValue = 0.8333333f,
      SpecialGroupMinimumSaturationValue = 0.0f,
      SpecialGroupMaximumSaturationValue = 1f
    };
    private static TreePaintingSettings VanityCherry = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.8333333f,
      SpecialGroupMaximumHueValue = 1f,
      SpecialGroupMinimumSaturationValue = 0.0f,
      SpecialGroupMaximumSaturationValue = 1f
    };
    private static TreePaintingSettings VanityYellowWillow = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.0f,
      SpecialGroupMaximumHueValue = 0.025f,
      SpecialGroupMinimumSaturationValue = 0.0f,
      SpecialGroupMaximumSaturationValue = 1f,
      InvertSpecialGroupResult = true
    };
    private static TreePaintingSettings TreeAsh = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.0f,
      SpecialGroupMaximumHueValue = 0.025f,
      SpecialGroupMinimumSaturationValue = 0.0f,
      SpecialGroupMaximumSaturationValue = 1f,
      InvertSpecialGroupResult = true
    };
    private static TreePaintingSettings GemTreeRuby = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.0f,
      SpecialGroupMaximumHueValue = 1f,
      SpecialGroupMinimumSaturationValue = 0.0f,
      SpecialGroupMaximumSaturationValue = 1f / 360f,
      InvertSpecialGroupResult = true
    };
    private static TreePaintingSettings GemTreeAmber = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.0f,
      SpecialGroupMaximumHueValue = 1f,
      SpecialGroupMinimumSaturationValue = 0.0f,
      SpecialGroupMaximumSaturationValue = 1f / 360f,
      InvertSpecialGroupResult = true
    };
    private static TreePaintingSettings GemTreeSapphire = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.0f,
      SpecialGroupMaximumHueValue = 1f,
      SpecialGroupMinimumSaturationValue = 0.0f,
      SpecialGroupMaximumSaturationValue = 1f / 360f,
      InvertSpecialGroupResult = true
    };
    private static TreePaintingSettings GemTreeEmerald = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.0f,
      SpecialGroupMaximumHueValue = 1f,
      SpecialGroupMinimumSaturationValue = 0.0f,
      SpecialGroupMaximumSaturationValue = 1f / 360f,
      InvertSpecialGroupResult = true
    };
    private static TreePaintingSettings GemTreeAmethyst = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.0f,
      SpecialGroupMaximumHueValue = 1f,
      SpecialGroupMinimumSaturationValue = 0.0f,
      SpecialGroupMaximumSaturationValue = 1f / 360f,
      InvertSpecialGroupResult = true
    };
    private static TreePaintingSettings GemTreeTopaz = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.0f,
      SpecialGroupMaximumHueValue = 1f,
      SpecialGroupMinimumSaturationValue = 0.0f,
      SpecialGroupMaximumSaturationValue = 1f / 360f,
      InvertSpecialGroupResult = true
    };
    private static TreePaintingSettings GemTreeDiamond = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.0f,
      SpecialGroupMaximumHueValue = 1f,
      SpecialGroupMinimumSaturationValue = 0.0f,
      SpecialGroupMaximumSaturationValue = 1f / 360f,
      InvertSpecialGroupResult = true
    };
    private static TreePaintingSettings PalmTreePurity = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.152777776f,
      SpecialGroupMaximumHueValue = 0.25f,
      SpecialGroupMinimumSaturationValue = 0.88f,
      SpecialGroupMaximumSaturationValue = 1f
    };
    private static TreePaintingSettings PalmTreeCorruption = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.0f,
      SpecialGroupMaximumHueValue = 1f,
      SpecialGroupMinimumSaturationValue = 0.4f,
      SpecialGroupMaximumSaturationValue = 1f
    };
    private static TreePaintingSettings PalmTreeCrimson = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      HueTestOffset = 0.5f,
      SpecialGroupMinimalHueValue = 0.333333343f,
      SpecialGroupMaximumHueValue = 0.5277778f,
      SpecialGroupMinimumSaturationValue = 0.0f,
      SpecialGroupMaximumSaturationValue = 1f
    };
    private static TreePaintingSettings PalmTreeHallow = new TreePaintingSettings()
    {
      UseSpecialGroups = true,
      SpecialGroupMinimalHueValue = 0.5f,
      SpecialGroupMaximumHueValue = 0.6111111f,
      SpecialGroupMinimumSaturationValue = 0.0f,
      SpecialGroupMaximumSaturationValue = 1f
    };

    public static TreePaintingSettings GetTileSettings(
      int tileType,
      int tileStyle)
    {
      switch (tileType)
      {
        case 0:
        case 2:
        case 23:
        case 109:
        case 199:
        case 477:
        case 492:
        case 633:
          return TreePaintSystemData.DefaultDirt;
        case 5:
          switch (tileStyle)
          {
            case 0:
              return TreePaintSystemData.WoodCorruption;
            case 1:
              return TreePaintSystemData.WoodJungle;
            case 2:
              return TreePaintSystemData.WoodHallow;
            case 3:
              return TreePaintSystemData.WoodSnow;
            case 4:
              return TreePaintSystemData.WoodCrimson;
            case 5:
              return TreePaintSystemData.WoodJungleUnderground;
            case 6:
              return TreePaintSystemData.WoodGlowingMushroom;
            default:
              return TreePaintSystemData.WoodPurity;
          }
        case 59:
        case 60:
        case 70:
          return TreePaintSystemData.CullMud;
        case 323:
          switch (tileStyle)
          {
            case 0:
            case 4:
              return TreePaintSystemData.PalmTreePurity;
            case 1:
            case 5:
              return TreePaintSystemData.PalmTreeCrimson;
            case 2:
            case 6:
              return TreePaintSystemData.PalmTreeHallow;
            case 3:
            case 7:
              return TreePaintSystemData.PalmTreeCorruption;
            default:
              return TreePaintSystemData.WoodPurity;
          }
        case 583:
          return TreePaintSystemData.GemTreeTopaz;
        case 584:
          return TreePaintSystemData.GemTreeAmethyst;
        case 585:
          return TreePaintSystemData.GemTreeSapphire;
        case 586:
          return TreePaintSystemData.GemTreeEmerald;
        case 587:
          return TreePaintSystemData.GemTreeRuby;
        case 588:
          return TreePaintSystemData.GemTreeDiamond;
        case 589:
          return TreePaintSystemData.GemTreeAmber;
        case 595:
        case 596:
          return TreePaintSystemData.VanityCherry;
        case 615:
        case 616:
          return TreePaintSystemData.VanityYellowWillow;
        case 634:
          return TreePaintSystemData.TreeAsh;
        default:
          return TreePaintSystemData.DefaultNoSpecialGroups;
      }
    }

    public static TreePaintingSettings GetTreeFoliageSettings(
      int foliageIndex,
      int foliageStyle)
    {
      switch (foliageIndex)
      {
        case 0:
        case 6:
        case 7:
        case 8:
        case 9:
        case 10:
          return TreePaintSystemData.WoodPurity;
        case 1:
          return TreePaintSystemData.WoodCorruption;
        case 2:
        case 11:
        case 13:
          return TreePaintSystemData.WoodJungle;
        case 3:
        case 19:
        case 20:
          return TreePaintSystemData.WoodHallow;
        case 4:
        case 12:
        case 16:
        case 17:
        case 18:
          return TreePaintSystemData.WoodSnow;
        case 5:
          return TreePaintSystemData.WoodCrimson;
        case 14:
          return TreePaintSystemData.WoodGlowingMushroom;
        case 15:
        case 21:
          switch (foliageStyle)
          {
            case 0:
            case 4:
              return TreePaintSystemData.PalmTreePurity;
            case 1:
            case 5:
              return TreePaintSystemData.PalmTreeCrimson;
            case 2:
            case 6:
              return TreePaintSystemData.PalmTreeHallow;
            case 3:
            case 7:
              return TreePaintSystemData.PalmTreeCorruption;
            default:
              return TreePaintSystemData.WoodPurity;
          }
        case 22:
          return TreePaintSystemData.GemTreeTopaz;
        case 23:
          return TreePaintSystemData.GemTreeAmethyst;
        case 24:
          return TreePaintSystemData.GemTreeSapphire;
        case 25:
          return TreePaintSystemData.GemTreeEmerald;
        case 26:
          return TreePaintSystemData.GemTreeRuby;
        case 27:
          return TreePaintSystemData.GemTreeDiamond;
        case 28:
          return TreePaintSystemData.GemTreeAmber;
        case 29:
          return TreePaintSystemData.VanityCherry;
        case 30:
          return TreePaintSystemData.VanityYellowWillow;
        default:
          return TreePaintSystemData.DefaultDirt;
      }
    }

    public static TreePaintingSettings GetWallSettings(int wallType) => TreePaintSystemData.DefaultNoSpecialGroups_ForWalls;
  }
}
