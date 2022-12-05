// Decompiled with JetBrains decompiler
// Type: Terraria.Achievements.ConditionIntTracker
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Terraria.Social;

namespace Terraria.Achievements
{
  public class ConditionIntTracker : AchievementTracker<int>
  {
    public ConditionIntTracker()
      : base(TrackerType.Int)
    {
    }

    public ConditionIntTracker(int maxValue)
      : base(TrackerType.Int)
    {
      this._maxValue = maxValue;
    }

    public override void ReportUpdate()
    {
      if (SocialAPI.Achievements == null || this._name == null)
        return;
      SocialAPI.Achievements.UpdateIntStat(this._name, this._value);
    }

    protected override void Load()
    {
    }
  }
}
