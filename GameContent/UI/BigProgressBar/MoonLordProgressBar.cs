// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.BigProgressBar.MoonLordProgressBar
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.ID;

namespace Terraria.GameContent.UI.BigProgressBar
{
  public class MoonLordProgressBar : IBigProgressBar
  {
    private BigProgressBarCache _cache;
    private NPC _referenceDummy;
    private HashSet<int> ValidIds = new HashSet<int>()
    {
      396,
      397,
      398
    };

    public MoonLordProgressBar() => this._referenceDummy = new NPC();

    public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
    {
      if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > 200)
        return false;
      NPC npc1 = Main.npc[info.npcIndexToAimAt];
      if ((!npc1.active || this.IsInBadAI(npc1)) && !this.TryFindingAnotherMoonLordPiece(ref info))
        return false;
      int num1 = 0;
      NPCSpawnParams spawnparams = new NPCSpawnParams()
      {
        strengthMultiplierOverride = new float?(npc1.strengthMultiplier),
        playerCountForMultiplayerDifficultyOverride = new int?(npc1.statsAreScaledForThisManyPlayers)
      };
      this._referenceDummy.SetDefaults(398, spawnparams);
      int num2 = num1 + this._referenceDummy.lifeMax;
      this._referenceDummy.SetDefaults(396, spawnparams);
      int num3 = num2 + this._referenceDummy.lifeMax;
      this._referenceDummy.SetDefaults(397, spawnparams);
      int max = num3 + this._referenceDummy.lifeMax + this._referenceDummy.lifeMax;
      float current = 0.0f;
      for (int index = 0; index < 200; ++index)
      {
        NPC npc2 = Main.npc[index];
        if (npc2.active && this.ValidIds.Contains(npc2.type) && !this.IsInBadAI(npc2))
          current += (float) npc2.life;
      }
      this._cache.SetLife(current, (float) max);
      return true;
    }

    private bool IsInBadAI(NPC npc) => npc.type == 398 && ((double) npc.ai[0] == 2.0 || (double) npc.ai[0] == -1.0) || npc.type == 398 && (double) npc.localAI[3] == 0.0 || (double) npc.ai[0] == -2.0 || (double) npc.ai[0] == -3.0;

    public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
    {
      int bossHeadTexture = NPCID.Sets.BossHeadTextures[396];
      Texture2D texture2D = TextureAssets.NpcHeadBoss[bossHeadTexture].Value;
      Rectangle barIconFrame = texture2D.Frame();
      BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, texture2D, barIconFrame);
    }

    private bool TryFindingAnotherMoonLordPiece(ref BigProgressBarInfo info)
    {
      for (int index = 0; index < 200; ++index)
      {
        NPC npc = Main.npc[index];
        if (npc.active && this.ValidIds.Contains(npc.type) && !this.IsInBadAI(npc))
        {
          info.npcIndexToAimAt = index;
          return true;
        }
      }
      return false;
    }
  }
}
