// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.NetModules.NetCreativeUnlocksModule
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.IO;
using Terraria.ID;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
  public class NetCreativeUnlocksModule : NetModule
  {
    public static NetPacket SerializeItemSacrifice(int itemId, int sacrificeCount)
    {
      NetPacket packet = NetModule.CreatePacket<NetCreativeUnlocksModule>(3);
      packet.Writer.Write((short) itemId);
      packet.Writer.Write((ushort) sacrificeCount);
      return packet;
    }

    public override bool Deserialize(BinaryReader reader, int userId)
    {
      if (Main.dedServ)
        return false;
      short key = reader.ReadInt16();
      Main.LocalPlayerCreativeTracker.ItemSacrifices.SetSacrificeCountDirectly(ContentSamples.ItemPersistentIdsByNetIds[(int) key], (int) reader.ReadUInt16());
      return true;
    }
  }
}
