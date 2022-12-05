// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.NetModules.NetAmbienceModule
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using System.IO;
using Terraria.GameContent.Ambience;
using Terraria.GameContent.Skies;
using Terraria.Graphics.Effects;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
  public class NetAmbienceModule : NetModule
  {
    public static NetPacket SerializeSkyEntitySpawn(Player player, SkyEntityType type)
    {
      int num = Main.rand.Next();
      NetPacket packet = NetModule.CreatePacket<NetAmbienceModule>(6);
      packet.Writer.Write((byte) player.whoAmI);
      packet.Writer.Write(num);
      packet.Writer.Write((byte) type);
      return packet;
    }

    public override bool Deserialize(BinaryReader reader, int userId)
    {
      if (Main.dedServ)
        return false;
      byte playerId = reader.ReadByte();
      int seed = reader.ReadInt32();
      SkyEntityType type = (SkyEntityType) reader.ReadByte();
      if (Main.remixWorld)
        return true;
      Main.QueueMainThreadAction((Action) (() => ((AmbientSky) SkyManager.Instance["Ambience"]).Spawn(Main.player[(int) playerId], type, seed)));
      return true;
    }
  }
}
