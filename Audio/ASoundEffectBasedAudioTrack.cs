// Decompiled with JetBrains decompiler
// Type: Terraria.Audio.ASoundEffectBasedAudioTrack
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Audio;
using System;

namespace Terraria.Audio
{
  public abstract class ASoundEffectBasedAudioTrack : IAudioTrack, IDisposable
  {
    protected const int bufferLength = 4096;
    protected const int bufferCountPerSubmit = 2;
    protected const int buffersToCoverFor = 8;
    protected byte[] _bufferToSubmit = new byte[4096];
    protected float[] _temporaryBuffer = new float[2048];
    private int _sampleRate;
    private AudioChannels _channels;
    protected DynamicSoundEffectInstance _soundEffectInstance;

    protected void CreateSoundEffect(int sampleRate, AudioChannels channels)
    {
      this._sampleRate = sampleRate;
      this._channels = channels;
      this._soundEffectInstance = new DynamicSoundEffectInstance(this._sampleRate, this._channels);
    }

    private void _soundEffectInstance_BufferNeeded(object sender, EventArgs e) => this.PrepareBuffer();

    public void Update()
    {
      if (!this.IsPlaying || this._soundEffectInstance.PendingBufferCount >= 8)
        return;
      this.PrepareBuffer();
    }

    protected void ResetBuffer()
    {
      for (int index = 0; index < this._bufferToSubmit.Length; ++index)
        this._bufferToSubmit[index] = (byte) 0;
    }

    protected void PrepareBuffer()
    {
      for (int index = 0; index < 2; ++index)
        this.ReadAheadPutAChunkIntoTheBuffer();
    }

    public bool IsPlaying => this._soundEffectInstance.State == SoundState.Playing;

    public bool IsStopped => this._soundEffectInstance.State == SoundState.Stopped;

    public bool IsPaused => this._soundEffectInstance.State == SoundState.Paused;

    public void Stop(AudioStopOptions options) => this._soundEffectInstance.Stop(options == AudioStopOptions.Immediate);

    public void Play()
    {
      this.PrepareToPlay();
      this._soundEffectInstance.Play();
    }

    public void Pause() => this._soundEffectInstance.Pause();

    public void SetVariable(string variableName, float value)
    {
      if (!(variableName == "Volume"))
      {
        if (!(variableName == "Pitch"))
        {
          if (!(variableName == "Pan"))
            return;
          this._soundEffectInstance.Pan = value;
        }
        else
          this._soundEffectInstance.Pitch = value;
      }
      else
        this._soundEffectInstance.Volume = this.ReMapVolumeToMatchXact(value);
    }

    private float ReMapVolumeToMatchXact(float musicVolume) => (float) Math.Pow(10.0, (31.0 * (double) musicVolume - 25.0 - 11.94) / 20.0);

    public void Resume() => this._soundEffectInstance.Resume();

    protected virtual void PrepareToPlay() => this.ResetBuffer();

    public abstract void Reuse();

    protected virtual void ReadAheadPutAChunkIntoTheBuffer()
    {
    }

    public abstract void Dispose();
  }
}
