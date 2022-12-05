// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Achievements.CustomIntCondition
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
  public class CustomIntCondition : AchievementCondition
  {
    [JsonProperty("Value")]
    private int _value;
    private int _maxValue;

    public int Value
    {
      get => this._value;
      set
      {
        int newValue = Utils.Clamp<int>(value, 0, this._maxValue);
        if (this._tracker != null)
          ((AchievementTracker<int>) this._tracker).SetValue(newValue);
        this._value = newValue;
        if (this._value != this._maxValue)
          return;
        this.Complete();
      }
    }

    private CustomIntCondition(string name, int maxValue)
      : base(name)
    {
      this._maxValue = maxValue;
      this._value = 0;
    }

    public override void Clear()
    {
      this._value = 0;
      base.Clear();
    }

    public override void Load(JObject state)
    {
      base.Load(state);
      this._value = JToken.op_Explicit(state["Value"]);
      if (this._tracker == null)
        return;
      ((AchievementTracker<int>) this._tracker).SetValue(this._value, false);
    }

    protected override IAchievementTracker CreateAchievementTracker() => (IAchievementTracker) new ConditionIntTracker(this._maxValue);

    public static AchievementCondition Create(string name, int maxValue) => (AchievementCondition) new CustomIntCondition(name, maxValue);

    public override void Complete()
    {
      if (this._tracker != null)
        ((AchievementTracker<int>) this._tracker).SetValue(this._maxValue);
      this._value = this._maxValue;
      base.Complete();
    }
  }
}
