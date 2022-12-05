// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Metadata.TileGolfPhysics
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Newtonsoft.Json;

namespace Terraria.GameContent.Metadata
{
  public class TileGolfPhysics
  {
    [JsonProperty]
    public float DirectImpactDampening { get; private set; }

    [JsonProperty]
    public float SideImpactDampening { get; private set; }

    [JsonProperty]
    public float ClubImpactDampening { get; private set; }

    [JsonProperty]
    public float PassThroughDampening { get; private set; }

    [JsonProperty]
    public float ImpactDampeningResistanceEfficiency { get; private set; }
  }
}
