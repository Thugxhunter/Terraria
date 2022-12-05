// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.NetModules.NetCreativeUnlocksPlayerReportModule
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.IO;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
  public class NetCreativeUnlocksPlayerReportModule : NetModule
  {
    private const byte _requestItemSacrificeId = 0;

    public static NetPacket SerializeSacrificeRequest(int itemId, int amount)
    {
      NetPacket packet = NetModule.CreatePacket<NetCreativeUnlocksPlayerReportModule>(5);
      packet.Writer.Write((byte) 0);
      packet.Writer.Write((ushort) itemId);
      packet.Writer.Write((ushort) amount);
      return packet;
    }

    public override bool Deserialize(BinaryReader reader, int userId)
    {
      if (reader.ReadByte() == (byte) 0)
      {
        int num1 = (int) reader.ReadUInt16();
        int num2 = (int) reader.ReadUInt16();
      }
      return true;
    }
  }
}
