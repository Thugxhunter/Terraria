// Decompiled with JetBrains decompiler
// Type: Terraria.NPCSpawnParams
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Terraria.DataStructures;

namespace Terraria
{
  public struct NPCSpawnParams
  {
    public float? sizeScaleOverride;
    public int? playerCountForMultiplayerDifficultyOverride;
    public GameModeData gameModeData;
    public float? strengthMultiplierOverride;

    public NPCSpawnParams WithScale(float scaleOverride) => new NPCSpawnParams()
    {
      sizeScaleOverride = new float?(scaleOverride),
      playerCountForMultiplayerDifficultyOverride = this.playerCountForMultiplayerDifficultyOverride,
      gameModeData = this.gameModeData,
      strengthMultiplierOverride = this.strengthMultiplierOverride
    };
  }
}
