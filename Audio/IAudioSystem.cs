// Decompiled with JetBrains decompiler
// Type: Terraria.Audio.IAudioSystem
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using ReLogic.Content.Sources;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Terraria.Audio
{
  public interface IAudioSystem : IDisposable
  {
    void LoadCue(int cueIndex, string cueName);

    void PauseAll();

    void ResumeAll();

    void UpdateMisc();

    void UpdateAudioEngine();

    void UpdateAmbientCueState(
      int i,
      bool gameIsActive,
      ref float trackVolume,
      float systemVolume);

    void UpdateAmbientCueTowardStopping(
      int i,
      float stoppingSpeed,
      ref float trackVolume,
      float systemVolume);

    void UpdateCommonTrack(bool active, int i, float totalVolume, ref float tempFade);

    void UpdateCommonTrackTowardStopping(
      int i,
      float totalVolume,
      ref float tempFade,
      bool isMainTrackAudible);

    bool IsTrackPlaying(int trackIndex);

    void UseSources(List<IContentSource> sources);

    IEnumerator PrepareWaveBank();

    void LoadFromSources();

    void Update();
  }
}
