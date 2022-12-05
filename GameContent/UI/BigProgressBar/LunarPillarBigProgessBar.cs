// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.BigProgressBar.LunarPillarBigProgessBar
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
  public abstract class LunarPillarBigProgessBar : IBigProgressBar
  {
    private BigProgressBarCache _cache;
    private int _headIndex;

    public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
    {
      if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > 200)
        return false;
      NPC npc = Main.npc[info.npcIndexToAimAt];
      if (!npc.active)
        return false;
      int headTextureIndex = npc.GetBossHeadTextureIndex();
      if (headTextureIndex == -1 || !this.IsPlayerInCombatArea() || (double) npc.ai[2] == 1.0)
        return false;
      double num1 = (double) Utils.Clamp<float>((float) npc.life / (float) npc.lifeMax, 0.0f, 1f);
      double num2 = (double) (int) MathHelper.Clamp(this.GetCurrentShieldValue(), 0.0f, this.GetMaxShieldValue()) / (double) this.GetMaxShieldValue();
      double num3 = 600.0 * (double) Main.GameModeInfo.EnemyMaxLifeMultiplier * (double) this.GetMaxShieldValue() / (double) npc.lifeMax;
      this._cache.SetLife((float) npc.life, (float) npc.lifeMax);
      this._cache.SetShield(this.GetCurrentShieldValue(), this.GetMaxShieldValue());
      this._headIndex = headTextureIndex;
      return true;
    }

    public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
    {
      Texture2D texture2D = TextureAssets.NpcHeadBoss[this._headIndex].Value;
      Rectangle barIconFrame = texture2D.Frame();
      BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, texture2D, barIconFrame, this._cache.ShieldCurrent, this._cache.ShieldMax);
    }

    internal abstract float GetCurrentShieldValue();

    internal abstract float GetMaxShieldValue();

    internal abstract bool IsPlayerInCombatArea();
  }
}
