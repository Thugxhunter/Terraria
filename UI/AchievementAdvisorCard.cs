// Decompiled with JetBrains decompiler
// Type: Terraria.UI.AchievementAdvisorCard
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.Achievements;

namespace Terraria.UI
{
  public class AchievementAdvisorCard
  {
    private const int _iconSize = 64;
    private const int _iconSizeWithSpace = 66;
    private const int _iconsPerRow = 8;
    public Achievement achievement;
    public float order;
    public Rectangle frame;
    public int achievementIndex;

    public AchievementAdvisorCard(Achievement achievement, float order)
    {
      this.achievement = achievement;
      this.order = order;
      this.achievementIndex = Main.Achievements.GetIconIndex(achievement.Name);
      this.frame = new Rectangle(this.achievementIndex % 8 * 66, this.achievementIndex / 8 * 66, 64, 64);
    }

    public bool IsAchievableInWorld()
    {
      string name = this.achievement.Name;
      if (name == "MASTERMIND")
        return WorldGen.crimson;
      if (name == "WORM_FODDER")
        return !WorldGen.crimson;
      return !(name == "PLAY_ON_A_SPECIAL_SEED") || Main.specialSeedWorld;
    }
  }
}
