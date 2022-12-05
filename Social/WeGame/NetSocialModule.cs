// Decompiled with JetBrains decompiler
// Type: Terraria.Social.WeGame.NetSocialModule
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using rail;
using System;
using System.Collections.Concurrent;
using Terraria.Net;

namespace Terraria.Social.WeGame
{
  public abstract class NetSocialModule : Terraria.Social.Base.NetSocialModule
  {
    protected const int LobbyMessageJoin = 1;
    protected Lobby _lobby = new Lobby();
    protected WeGameP2PReader _reader;
    protected WeGameP2PWriter _writer;
    protected ConcurrentDictionary<RailID, NetSocialModule.ConnectionState> _connectionStateMap = new ConcurrentDictionary<RailID, NetSocialModule.ConnectionState>();
    protected static readonly byte[] _handshake = new byte[10]
    {
      (byte) 10,
      (byte) 0,
      (byte) 93,
      (byte) 114,
      (byte) 101,
      (byte) 108,
      (byte) 111,
      (byte) 103,
      (byte) 105,
      (byte) 99
    };

    protected NetSocialModule()
    {
      this._reader = new WeGameP2PReader();
      this._writer = new WeGameP2PWriter();
    }

    public override void Initialize()
    {
      CoreSocialModule.OnTick += new Action(this._reader.ReadTick);
      CoreSocialModule.OnTick += new Action(this._writer.SendAll);
    }

    public override void Shutdown() => this._lobby.Leave();

    public override bool IsConnected(RemoteAddress address)
    {
      if (address == null)
        return false;
      RailID railId = this.RemoteAddressToRailId(address);
      return this._connectionStateMap.ContainsKey(railId) && this._connectionStateMap[railId] == NetSocialModule.ConnectionState.Connected;
    }

    protected RailID GetLocalPeer() => rail_api.RailFactory().RailPlayer().GetRailID();

    protected bool GetSessionState(RailID userId, RailNetworkSessionState state)
    {
      if (rail_api.RailFactory().RailNetworkHelper().GetSessionState(userId, state) == null)
        return true;
      WeGameHelper.WriteDebugString("GetSessionState Failed user:{0}", (object) ((RailComparableID) userId).id_);
      return false;
    }

    protected RailID RemoteAddressToRailId(RemoteAddress address) => ((WeGameAddress) address).rail_id;

    public override bool Send(RemoteAddress address, byte[] data, int length)
    {
      this._writer.QueueSend(this.RemoteAddressToRailId(address), data, length);
      return true;
    }

    public override int Receive(RemoteAddress address, byte[] data, int offset, int length) => address == null ? 0 : this._reader.Receive(this.RemoteAddressToRailId(address), data, offset, length);

    public override bool IsDataAvailable(RemoteAddress address) => this._reader.IsDataAvailable(this.RemoteAddressToRailId(address));

    public enum ConnectionState
    {
      Inactive,
      Authenticating,
      Connected,
    }
  }
}
