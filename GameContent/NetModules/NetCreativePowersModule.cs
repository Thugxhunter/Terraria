// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.NetModules.NetCreativePowersModule
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.IO;
using Terraria.GameContent.Creative;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
  public class NetCreativePowersModule : NetModule
  {
    public static NetPacket PreparePacket(
      ushort powerId,
      int specificInfoBytesInPacketCount)
    {
      NetPacket packet = NetModule.CreatePacket<NetCreativePowersModule>(specificInfoBytesInPacketCount + 2);
      packet.Writer.Write(powerId);
      return packet;
    }

    public override bool Deserialize(BinaryReader reader, int userId)
    {
      ushort id = reader.ReadUInt16();
      ICreativePower power;
      if (!CreativePowerManager.Instance.TryGetPower(id, out power))
        return false;
      power.DeserializeNetMessage(reader, userId);
      return true;
    }
  }
}
