// Decompiled with JetBrains decompiler
// Type: Terraria.WorldBuilding.WorldGenRange
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Terraria.Utilities;

namespace Terraria.WorldBuilding
{
  public class WorldGenRange
  {
    public static readonly WorldGenRange Empty = new WorldGenRange(0, 0);
    [JsonProperty("Min")]
    public readonly int Minimum;
    [JsonProperty("Max")]
    public readonly int Maximum;
    [JsonProperty]
    [JsonConverter(typeof (StringEnumConverter))]
    public readonly WorldGenRange.ScalingMode ScaleWith;

    public int ScaledMinimum => this.ScaleValue(this.Minimum);

    public int ScaledMaximum => this.ScaleValue(this.Maximum);

    public WorldGenRange(int minimum, int maximum)
    {
      this.Minimum = minimum;
      this.Maximum = maximum;
    }

    public int GetRandom(UnifiedRandom random) => random.Next(this.ScaledMinimum, this.ScaledMaximum + 1);

    private int ScaleValue(int value)
    {
      double num = 1.0;
      switch (this.ScaleWith)
      {
        case WorldGenRange.ScalingMode.None:
          num = 1.0;
          break;
        case WorldGenRange.ScalingMode.WorldArea:
          num = (double) (Main.maxTilesX * Main.maxTilesY) / 5040000.0;
          break;
        case WorldGenRange.ScalingMode.WorldWidth:
          num = (double) Main.maxTilesX / 4200.0;
          break;
      }
      return (int) (num * (double) value);
    }

    public enum ScalingMode
    {
      None,
      WorldArea,
      WorldWidth,
    }
  }
}
