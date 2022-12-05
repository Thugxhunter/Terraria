// Decompiled with JetBrains decompiler
// Type: Terraria.Audio.LegacyAudioSystem
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Audio;
using ReLogic.Content.Sources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Terraria.Audio
{
  public class LegacyAudioSystem : IAudioSystem, IDisposable
  {
    public IAudioTrack[] AudioTracks;
    public int MusicReplayDelay;
    public AudioEngine Engine;
    public SoundBank SoundBank;
    public WaveBank WaveBank;
    public Dictionary<int, string> TrackNamesByIndex;
    public Dictionary<int, IAudioTrack> DefaultTrackByIndex;
    public List<IContentSource> FileSources;

    public void LoadFromSources()
    {
      List<IContentSource> fileSources = this.FileSources;
      for (int key = 0; key < this.AudioTracks.Length; ++key)
      {
        string str;
        if (this.TrackNamesByIndex.TryGetValue(key, out str))
        {
          string assetPath = "Music" + Path.DirectorySeparatorChar.ToString() + str;
          IAudioTrack audioTrack1 = this.DefaultTrackByIndex[key];
          IAudioTrack audioTrack2 = audioTrack1;
          IAudioTrack replacementTrack = this.FindReplacementTrack(fileSources, assetPath);
          if (replacementTrack != null)
            audioTrack2 = replacementTrack;
          if (this.AudioTracks[key] != audioTrack2)
            this.AudioTracks[key].Stop(AudioStopOptions.Immediate);
          if (this.AudioTracks[key] != audioTrack1)
            this.AudioTracks[key].Dispose();
          this.AudioTracks[key] = audioTrack2;
        }
      }
    }

    public void UseSources(List<IContentSource> sourcesFromLowestToHighest)
    {
      this.FileSources = sourcesFromLowestToHighest;
      this.LoadFromSources();
    }

    public void Update()
    {
      if (!this.WaveBank.IsPrepared)
        return;
      for (int index = 0; index < this.AudioTracks.Length; ++index)
      {
        if (this.AudioTracks[index] != null)
          this.AudioTracks[index].Update();
      }
    }

    private IAudioTrack FindReplacementTrack(
      List<IContentSource> sources,
      string assetPath)
    {
      IAudioTrack replacementTrack = (IAudioTrack) null;
      for (int index = 0; index < sources.Count; ++index)
      {
        IContentSource source = sources[index];
        if (source.HasAsset(assetPath))
        {
          string extension = source.GetExtension(assetPath);
          try
          {
            IAudioTrack audioTrack = (IAudioTrack) null;
            if (!(extension == ".ogg"))
            {
              if (!(extension == ".wav"))
              {
                if (extension == ".mp3")
                  audioTrack = (IAudioTrack) new MP3AudioTrack(source.OpenStream(assetPath));
              }
              else
                audioTrack = (IAudioTrack) new WAVAudioTrack(source.OpenStream(assetPath));
            }
            else
              audioTrack = (IAudioTrack) new OGGAudioTrack(source.OpenStream(assetPath));
            if (audioTrack != null)
            {
              replacementTrack?.Dispose();
              replacementTrack = audioTrack;
            }
          }
          catch
          {
            string textToShow = "A resource pack failed to load " + assetPath + "!";
            Main.IssueReporter.AddReport(textToShow);
            Main.IssueReporterIndicator.AttemptLettingPlayerKnow();
          }
        }
      }
      return replacementTrack;
    }

    public LegacyAudioSystem()
    {
      this.Engine = new AudioEngine("Content\\TerrariaMusic.xgs");
      this.SoundBank = new SoundBank(this.Engine, "Content\\Sound Bank.xsb");
      this.Engine.Update();
      this.WaveBank = new WaveBank(this.Engine, "Content\\Wave Bank.xwb", 0, (short) 512);
      this.Engine.Update();
      this.AudioTracks = new IAudioTrack[Main.maxMusic];
      this.TrackNamesByIndex = new Dictionary<int, string>();
      this.DefaultTrackByIndex = new Dictionary<int, IAudioTrack>();
    }

    public IEnumerator PrepareWaveBank()
    {
      while (!this.WaveBank.IsPrepared)
      {
        this.Engine.Update();
        yield return (object) null;
      }
    }

    public void LoadCue(int cueIndex, string cueName)
    {
      CueAudioTrack cueAudioTrack = new CueAudioTrack(this.SoundBank, cueName);
      this.TrackNamesByIndex[cueIndex] = cueName;
      this.DefaultTrackByIndex[cueIndex] = (IAudioTrack) cueAudioTrack;
      this.AudioTracks[cueIndex] = (IAudioTrack) cueAudioTrack;
    }

    public void UpdateMisc()
    {
      if (Main.curMusic != Main.newMusic)
        this.MusicReplayDelay = 0;
      if (this.MusicReplayDelay <= 0)
        return;
      --this.MusicReplayDelay;
    }

    public void PauseAll()
    {
      if (!this.WaveBank.IsPrepared)
        return;
      float[] musicFade = Main.musicFade;
      for (int index = 0; index < this.AudioTracks.Length; ++index)
      {
        if (this.AudioTracks[index] != null && !this.AudioTracks[index].IsPaused && this.AudioTracks[index].IsPlaying)
        {
          if ((double) musicFade[index] > 0.0)
          {
            try
            {
              this.AudioTracks[index].Pause();
            }
            catch (Exception ex)
            {
            }
          }
        }
      }
    }

    public void ResumeAll()
    {
      if (!this.WaveBank.IsPrepared)
        return;
      float[] musicFade = Main.musicFade;
      for (int index = 0; index < this.AudioTracks.Length; ++index)
      {
        if (this.AudioTracks[index] != null && this.AudioTracks[index].IsPaused)
        {
          if ((double) musicFade[index] > 0.0)
          {
            try
            {
              this.AudioTracks[index].Resume();
            }
            catch (Exception ex)
            {
            }
          }
        }
      }
    }

    public void UpdateAmbientCueState(
      int i,
      bool gameIsActive,
      ref float trackVolume,
      float systemVolume)
    {
      if (!this.WaveBank.IsPrepared)
        return;
      if ((double) systemVolume == 0.0)
      {
        if (!this.AudioTracks[i].IsPlaying)
          return;
        this.AudioTracks[i].Stop(AudioStopOptions.Immediate);
      }
      else if (!this.AudioTracks[i].IsPlaying)
      {
        this.AudioTracks[i].Reuse();
        this.AudioTracks[i].Play();
        this.AudioTracks[i].SetVariable("Volume", trackVolume * systemVolume);
      }
      else if (this.AudioTracks[i].IsPaused & gameIsActive)
      {
        this.AudioTracks[i].Resume();
      }
      else
      {
        trackVolume += 0.005f;
        if ((double) trackVolume > 1.0)
          trackVolume = 1f;
        this.AudioTracks[i].SetVariable("Volume", trackVolume * systemVolume);
      }
    }

    public void UpdateAmbientCueTowardStopping(
      int i,
      float stoppingSpeed,
      ref float trackVolume,
      float systemVolume)
    {
      if (!this.WaveBank.IsPrepared)
        return;
      if (!this.AudioTracks[i].IsPlaying)
      {
        trackVolume = 0.0f;
      }
      else
      {
        if ((double) trackVolume > 0.0)
        {
          trackVolume -= stoppingSpeed;
          if ((double) trackVolume < 0.0)
            trackVolume = 0.0f;
        }
        if ((double) trackVolume <= 0.0)
          this.AudioTracks[i].Stop(AudioStopOptions.Immediate);
        else
          this.AudioTracks[i].SetVariable("Volume", trackVolume * systemVolume);
      }
    }

    public bool IsTrackPlaying(int trackIndex) => this.WaveBank.IsPrepared && this.AudioTracks[trackIndex].IsPlaying;

    public void UpdateCommonTrack(bool active, int i, float totalVolume, ref float tempFade)
    {
      if (!this.WaveBank.IsPrepared)
        return;
      tempFade += 0.005f;
      if ((double) tempFade > 1.0)
        tempFade = 1f;
      if (!this.AudioTracks[i].IsPlaying & active)
      {
        if (this.MusicReplayDelay != 0)
          return;
        if (Main.SettingMusicReplayDelayEnabled)
          this.MusicReplayDelay = Main.rand.Next(14400, 21601);
        this.AudioTracks[i].Reuse();
        this.AudioTracks[i].SetVariable("Volume", totalVolume);
        this.AudioTracks[i].Play();
      }
      else
        this.AudioTracks[i].SetVariable("Volume", totalVolume);
    }

    public void UpdateCommonTrackTowardStopping(
      int i,
      float totalVolume,
      ref float tempFade,
      bool isMainTrackAudible)
    {
      if (!this.WaveBank.IsPrepared)
        return;
      if (this.AudioTracks[i].IsPlaying || !this.AudioTracks[i].IsStopped)
      {
        if (isMainTrackAudible)
          tempFade -= 0.005f;
        else if (Main.curMusic == 0)
          tempFade = 0.0f;
        if ((double) tempFade <= 0.0)
        {
          tempFade = 0.0f;
          this.AudioTracks[i].SetVariable("Volume", 0.0f);
          this.AudioTracks[i].Stop(AudioStopOptions.Immediate);
        }
        else
          this.AudioTracks[i].SetVariable("Volume", totalVolume);
      }
      else
        tempFade = 0.0f;
    }

    public void UpdateAudioEngine() => this.Engine.Update();

    public void Dispose()
    {
      this.SoundBank.Dispose();
      this.WaveBank.Dispose();
      this.Engine.Dispose();
    }
  }
}
