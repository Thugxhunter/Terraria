// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Metadata.TileMaterials
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria.ID;

namespace Terraria.GameContent.Metadata
{
  public static class TileMaterials
  {
    private static Dictionary<string, TileMaterial> _materialsByName;
    private static readonly TileMaterial[] MaterialsByTileId = new TileMaterial[(int) TileID.Count];

    static TileMaterials()
    {
      TileMaterials._materialsByName = TileMaterials.DeserializeEmbeddedResource<Dictionary<string, TileMaterial>>("Terraria.GameContent.Metadata.MaterialData.Materials.json");
      TileMaterial tileMaterial = TileMaterials._materialsByName["Default"];
      for (int index = 0; index < TileMaterials.MaterialsByTileId.Length; ++index)
        TileMaterials.MaterialsByTileId[index] = tileMaterial;
      foreach (KeyValuePair<string, string> keyValuePair in TileMaterials.DeserializeEmbeddedResource<Dictionary<string, string>>("Terraria.GameContent.Metadata.MaterialData.Tiles.json"))
      {
        string key1 = keyValuePair.Key;
        string key2 = keyValuePair.Value;
        TileMaterials.SetForTileId((ushort) TileID.Search.GetId(key1), TileMaterials._materialsByName[key2]);
      }
    }

    private static T DeserializeEmbeddedResource<T>(string path)
    {
      using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
      {
        using (StreamReader streamReader = new StreamReader(manifestResourceStream))
          return JsonConvert.DeserializeObject<T>(streamReader.ReadToEnd());
      }
    }

    public static void SetForTileId(ushort tileId, TileMaterial material) => TileMaterials.MaterialsByTileId[(int) tileId] = material;

    public static TileMaterial GetByTileId(ushort tileId) => TileMaterials.MaterialsByTileId[(int) tileId];
  }
}
