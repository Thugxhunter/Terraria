// Decompiled with JetBrains decompiler
// Type: Terraria.Social.WeGame.FriendsSocialModule
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using rail;

namespace Terraria.Social.WeGame
{
  public class FriendsSocialModule : Terraria.Social.Base.FriendsSocialModule
  {
    public override void Initialize()
    {
    }

    public override void Shutdown()
    {
    }

    public override string GetUsername()
    {
      string username;
      rail_api.RailFactory().RailPlayer().GetPlayerName(ref username);
      WeGameHelper.WriteDebugString("GetUsername by wegame" + username);
      return username;
    }

    public override void OpenJoinInterface()
    {
      WeGameHelper.WriteDebugString("OpenJoinInterface by wegame");
      rail_api.RailFactory().RailFloatingWindow().AsyncShowRailFloatingWindow((EnumRailWindowType) 10, "");
    }
  }
}
