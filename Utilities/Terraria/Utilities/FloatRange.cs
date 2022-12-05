// Decompiled with JetBrains decompiler
// Type: Terraria.Utilities.Terraria.Utilities.FloatRange
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Newtonsoft.Json;

namespace Terraria.Utilities.Terraria.Utilities
{
  public struct FloatRange
  {
    [JsonProperty("Min")]
    public readonly float Minimum;
    [JsonProperty("Max")]
    public readonly float Maximum;

    public FloatRange(float minimum, float maximum)
    {
      this.Minimum = minimum;
      this.Maximum = maximum;
    }

    public static FloatRange operator *(FloatRange range, float scale) => new FloatRange(range.Minimum * scale, range.Maximum * scale);

    public static FloatRange operator *(float scale, FloatRange range) => range * scale;

    public static FloatRange operator /(FloatRange range, float scale) => new FloatRange(range.Minimum / scale, range.Maximum / scale);

    public static FloatRange operator /(float scale, FloatRange range) => range / scale;
  }
}
