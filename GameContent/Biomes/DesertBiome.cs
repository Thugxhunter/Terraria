// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.DesertBiome
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Terraria.GameContent.Biomes.Desert;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
  public class DesertBiome : MicroBiome
  {
    [JsonProperty("ChanceOfEntrance")]
    public double ChanceOfEntrance = 0.3333;

    public override bool Place(Point origin, StructureMap structures)
    {
      DesertDescription fromPlacement = DesertDescription.CreateFromPlacement(origin);
      if (!fromPlacement.IsValid)
        return false;
      DesertBiome.ExportDescriptionToEngine(fromPlacement);
      SandMound.Place(fromPlacement);
      fromPlacement.UpdateSurfaceMap();
      if (!Main.tenthAnniversaryWorld && GenBase._random.NextDouble() <= this.ChanceOfEntrance)
      {
        switch (GenBase._random.Next(4))
        {
          case 0:
            ChambersEntrance.Place(fromPlacement);
            break;
          case 1:
            AnthillEntrance.Place(fromPlacement);
            break;
          case 2:
            LarvaHoleEntrance.Place(fromPlacement);
            break;
          case 3:
            PitEntrance.Place(fromPlacement);
            break;
        }
      }
      DesertHive.Place(fromPlacement);
      DesertBiome.CleanupArea(fromPlacement.Hive);
      Microsoft.Xna.Framework.Rectangle area = new Microsoft.Xna.Framework.Rectangle(fromPlacement.CombinedArea.X, 50, fromPlacement.CombinedArea.Width, fromPlacement.CombinedArea.Bottom - 20);
      structures.AddStructure(area, 10);
      return true;
    }

    private static void ExportDescriptionToEngine(DesertDescription description)
    {
      GenVars.UndergroundDesertLocation = description.CombinedArea;
      GenVars.UndergroundDesertLocation.Inflate(10, 10);
      GenVars.UndergroundDesertHiveLocation = description.Hive;
    }

    private static void CleanupArea(Microsoft.Xna.Framework.Rectangle area)
    {
      for (int index1 = area.Left - 20; index1 < area.Right + 20; ++index1)
      {
        for (int index2 = area.Top - 20; index2 < area.Bottom + 20; ++index2)
        {
          if (index1 > 0 && index1 < Main.maxTilesX - 1 && index2 > 0 && index2 < Main.maxTilesY - 1)
          {
            WorldGen.SquareWallFrame(index1, index2);
            WorldUtils.TileFrame(index1, index2, true);
          }
        }
      }
    }
  }
}
