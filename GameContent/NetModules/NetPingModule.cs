// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.NetModules.NetPingModule
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System.IO;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
  public class NetPingModule : NetModule
  {
    public static NetPacket Serialize(Vector2 position)
    {
      NetPacket packet = NetModule.CreatePacket<NetPingModule>(8);
      packet.Writer.WriteVector2(position);
      return packet;
    }

    public override bool Deserialize(BinaryReader reader, int userId)
    {
      Vector2 position = reader.ReadVector2();
      if (Main.dedServ)
        NetManager.Instance.Broadcast(NetPingModule.Serialize(position), userId);
      else
        Main.Pings.Add(position);
      return true;
    }
  }
}
