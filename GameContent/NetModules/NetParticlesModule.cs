// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.NetModules.NetParticlesModule
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.IO;
using Terraria.GameContent.Drawing;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
  public class NetParticlesModule : NetModule
  {
    public static NetPacket Serialize(
      ParticleOrchestraType particleType,
      ParticleOrchestraSettings settings)
    {
      NetPacket packet = NetModule.CreatePacket<NetParticlesModule>(22);
      packet.Writer.Write((byte) particleType);
      settings.Serialize(packet.Writer);
      return packet;
    }

    public override bool Deserialize(BinaryReader reader, int userId)
    {
      ParticleOrchestraType particleOrchestraType = (ParticleOrchestraType) reader.ReadByte();
      ParticleOrchestraSettings settings = new ParticleOrchestraSettings();
      settings.DeserializeFrom(reader);
      if (Main.netMode == 2)
        NetManager.Instance.Broadcast(NetParticlesModule.Serialize(particleOrchestraType, settings), userId);
      else
        ParticleOrchestrator.SpawnParticlesDirect(particleOrchestraType, settings);
      return true;
    }
  }
}
