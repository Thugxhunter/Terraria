// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Base.ServerJoinRequestsManager
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Terraria.Social.Base
{
  public class ServerJoinRequestsManager
  {
    private readonly List<UserJoinToServerRequest> _requests;
    public readonly ReadOnlyCollection<UserJoinToServerRequest> CurrentRequests;

    public event ServerJoinRequestEvent OnRequestAdded;

    public event ServerJoinRequestEvent OnRequestRemoved;

    public ServerJoinRequestsManager()
    {
      this._requests = new List<UserJoinToServerRequest>();
      this.CurrentRequests = new ReadOnlyCollection<UserJoinToServerRequest>((IList<UserJoinToServerRequest>) this._requests);
    }

    public void Update()
    {
      for (int index = this._requests.Count - 1; index >= 0; --index)
      {
        if (!this._requests[index].IsValid())
          this.RemoveRequestAtIndex(index);
      }
    }

    public void Add(UserJoinToServerRequest request)
    {
      for (int index = this._requests.Count - 1; index >= 0; --index)
      {
        if (this._requests[index].Equals((object) request))
          this.RemoveRequestAtIndex(index);
      }
      this._requests.Add(request);
      request.OnAccepted += (Action) (() => this.RemoveRequest(request));
      request.OnRejected += (Action) (() => this.RemoveRequest(request));
      if (this.OnRequestAdded == null)
        return;
      this.OnRequestAdded(request);
    }

    private void RemoveRequestAtIndex(int i)
    {
      UserJoinToServerRequest request = this._requests[i];
      this._requests.RemoveAt(i);
      if (this.OnRequestRemoved == null)
        return;
      this.OnRequestRemoved(request);
    }

    private void RemoveRequest(UserJoinToServerRequest request)
    {
      if (!this._requests.Remove(request) || this.OnRequestRemoved == null)
        return;
      this.OnRequestRemoved(request);
    }
  }
}
