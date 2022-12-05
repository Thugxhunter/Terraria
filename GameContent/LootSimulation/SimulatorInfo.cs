// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.LootSimulation.SimulatorInfo
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.GameContent.LootSimulation
{
  public class SimulatorInfo
  {
    public Player player;
    private double _originalDayTimeCounter;
    private bool _originalDayTimeFlag;
    private Vector2 _originalPlayerPosition;
    public bool runningExpertMode;
    public LootSimulationItemCounter itemCounter;
    public NPC npcVictim;

    public SimulatorInfo()
    {
      this.player = new Player();
      this._originalDayTimeCounter = Main.time;
      this._originalDayTimeFlag = Main.dayTime;
      this._originalPlayerPosition = this.player.position;
      this.runningExpertMode = false;
    }

    public void ReturnToOriginalDaytime()
    {
      Main.dayTime = this._originalDayTimeFlag;
      Main.time = this._originalDayTimeCounter;
    }

    public void AddItem(int itemId, int amount) => this.itemCounter.AddItem(itemId, amount, this.runningExpertMode);

    public void ReturnToOriginalPlayerPosition() => this.player.position = this._originalPlayerPosition;
  }
}
