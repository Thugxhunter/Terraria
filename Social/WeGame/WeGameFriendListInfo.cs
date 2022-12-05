// Decompiled with JetBrains decompiler
// Type: Terraria.Social.WeGame.WeGameFriendListInfo
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using rail;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Terraria.Social.WeGame
{
  [DataContract]
  public class WeGameFriendListInfo
  {
    [DataMember]
    public List<RailFriendInfo> _friendList;
  }
}
