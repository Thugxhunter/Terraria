// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes.StackedConditionSetter
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes
{
  public class StackedConditionSetter : ISimulationConditionSetter
  {
    private ISimulationConditionSetter[] _setters;

    public StackedConditionSetter(params ISimulationConditionSetter[] setters) => this._setters = setters;

    public void Setup(SimulatorInfo info)
    {
      for (int index = 0; index < this._setters.Length; ++index)
        this._setters[index].Setup(info);
    }

    public void TearDown(SimulatorInfo info)
    {
      for (int index = 0; index < this._setters.Length; ++index)
        this._setters[index].TearDown(info);
    }

    public int GetTimesToRunMultiplier(SimulatorInfo info) => 1;
  }
}
