// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.IPersistentPerWorldContent
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.IO;

namespace Terraria.GameContent
{
  public interface IPersistentPerWorldContent
  {
    void Save(BinaryWriter writer);

    void Load(BinaryReader reader, int gameVersionSaveWasMadeOn);

    void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn);

    void Reset();
  }
}
