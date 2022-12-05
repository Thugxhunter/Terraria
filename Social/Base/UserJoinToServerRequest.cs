// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Base.UserJoinToServerRequest
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;

namespace Terraria.Social.Base
{
  public abstract class UserJoinToServerRequest
  {
    internal string UserDisplayName { get; private set; }

    internal string UserFullIdentifier { get; private set; }

    public event Action OnAccepted;

    public event Action OnRejected;

    public UserJoinToServerRequest(string userDisplayName, string fullIdentifier)
    {
      this.UserDisplayName = userDisplayName;
      this.UserFullIdentifier = fullIdentifier;
    }

    public void Accept()
    {
      if (this.OnAccepted == null)
        return;
      this.OnAccepted();
    }

    public void Reject()
    {
      if (this.OnRejected == null)
        return;
      this.OnRejected();
    }

    public abstract bool IsValid();

    public abstract string GetUserWrapperText();
  }
}
