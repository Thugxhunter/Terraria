// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Personalities.MushroomBiome
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.GameContent.Personalities
{
  public class MushroomBiome : AShoppingBiome
  {
    public MushroomBiome() => this.NameKey = "Mushroom";

    public override bool IsInBiome(Player player) => player.ZoneGlowshroom;
  }
}
