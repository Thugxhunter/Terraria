// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.BigProgressBar.BrainOfCthuluBigProgressBar
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.GameContent.UI.BigProgressBar
{
  public class BrainOfCthuluBigProgressBar : IBigProgressBar
  {
    private BigProgressBarCache _cache;
    private NPC _creeperForReference;

    public BrainOfCthuluBigProgressBar() => this._creeperForReference = new NPC();

    public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
    {
      if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > 200)
        return false;
      NPC npc1 = Main.npc[info.npcIndexToAimAt];
      if (!npc1.active)
        return false;
      int cthuluCreepersCount = NPC.GetBrainOfCthuluCreepersCount();
      this._creeperForReference.SetDefaults(267, npc1.GetMatchingSpawnParams());
      int num1 = this._creeperForReference.lifeMax * cthuluCreepersCount;
      float num2 = 0.0f;
      for (int index = 0; index < 200; ++index)
      {
        NPC npc2 = Main.npc[index];
        if (npc2.active && npc2.type == this._creeperForReference.type)
          num2 += (float) npc2.life;
      }
      this._cache.SetLife((float) npc1.life + num2, (float) (npc1.lifeMax + num1));
      return true;
    }

    public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
    {
      int bossHeadTexture = NPCID.Sets.BossHeadTextures[266];
      Texture2D texture2D = TextureAssets.NpcHeadBoss[bossHeadTexture].Value;
      Rectangle barIconFrame = texture2D.Frame();
      BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, texture2D, barIconFrame);
    }
  }
}
