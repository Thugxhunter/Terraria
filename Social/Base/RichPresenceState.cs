// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Base.RichPresenceState
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using Terraria.GameContent.UI.States;

namespace Terraria.Social.Base
{
  public class RichPresenceState : IEquatable<RichPresenceState>
  {
    public RichPresenceState.GameModeState GameMode;

    public bool Equals(RichPresenceState other) => this.GameMode == other.GameMode;

    public static RichPresenceState GetCurrentState()
    {
      RichPresenceState currentState = new RichPresenceState();
      if (Main.gameMenu)
      {
        int num = Main.MenuUI.CurrentState is UICharacterCreation ? 1 : 0;
        bool flag = Main.MenuUI.CurrentState is UIWorldCreation;
        currentState.GameMode = num == 0 ? (!flag ? RichPresenceState.GameModeState.InMainMenu : RichPresenceState.GameModeState.CreatingWorld) : RichPresenceState.GameModeState.CreatingPlayer;
      }
      else
        currentState.GameMode = Main.netMode != 0 ? RichPresenceState.GameModeState.PlayingMulti : RichPresenceState.GameModeState.PlayingSingle;
      return currentState;
    }

    public enum GameModeState
    {
      InMainMenu,
      CreatingPlayer,
      CreatingWorld,
      PlayingSingle,
      PlayingMulti,
    }
  }
}
