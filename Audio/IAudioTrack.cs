// Decompiled with JetBrains decompiler
// Type: Terraria.Audio.IAudioTrack
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Audio;
using System;

namespace Terraria.Audio
{
  public interface IAudioTrack : IDisposable
  {
    bool IsPlaying { get; }

    bool IsStopped { get; }

    bool IsPaused { get; }

    void Stop(AudioStopOptions options);

    void Play();

    void Pause();

    void SetVariable(string variableName, float value);

    void Resume();

    void Reuse();

    void Update();
  }
}
