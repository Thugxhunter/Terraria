// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.IPersistentPerPlayerContent
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.IO;

namespace Terraria.GameContent
{
  public interface IPersistentPerPlayerContent
  {
    void Save(Player player, BinaryWriter writer);

    void Load(Player player, BinaryReader reader, int gameVersionSaveWasMadeOn);

    void ApplyLoadedDataToOutOfPlayerFields(Player player);

    void ResetDataForNewPlayer(Player player);

    void Reset();
  }
}
