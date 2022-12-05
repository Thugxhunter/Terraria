// Decompiled with JetBrains decompiler
// Type: Terraria.Initializers.NetworkInitializer
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Terraria.GameContent.NetModules;
using Terraria.Net;

namespace Terraria.Initializers
{
  public static class NetworkInitializer
  {
    public static void Load()
    {
      NetManager.Instance.Register<NetLiquidModule>();
      NetManager.Instance.Register<NetTextModule>();
      NetManager.Instance.Register<NetPingModule>();
      NetManager.Instance.Register<NetAmbienceModule>();
      NetManager.Instance.Register<NetBestiaryModule>();
      NetManager.Instance.Register<NetCreativeUnlocksModule>();
      NetManager.Instance.Register<NetCreativePowersModule>();
      NetManager.Instance.Register<NetCreativeUnlocksPlayerReportModule>();
      NetManager.Instance.Register<NetTeleportPylonModule>();
      NetManager.Instance.Register<NetParticlesModule>();
      NetManager.Instance.Register<NetCreativePowerPermissionsModule>();
    }
  }
}
