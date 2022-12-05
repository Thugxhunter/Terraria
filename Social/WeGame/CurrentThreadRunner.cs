// Decompiled with JetBrains decompiler
// Type: Terraria.Social.WeGame.CurrentThreadRunner
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using System.Windows.Threading;

namespace Terraria.Social.WeGame
{
  public class CurrentThreadRunner
  {
    private Dispatcher _dsipatcher;

    public CurrentThreadRunner() => this._dsipatcher = Dispatcher.CurrentDispatcher;

    public void Run(Action f) => this._dsipatcher.BeginInvoke((Delegate) f);
  }
}
