// Decompiled with JetBrains decompiler
// Type: Terraria.Audio.SoundInstanceGarbageCollector
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Terraria.Audio
{
  public static class SoundInstanceGarbageCollector
  {
    private static readonly List<SoundEffectInstance> _activeSounds = new List<SoundEffectInstance>(128);

    public static void Track(SoundEffectInstance sound)
    {
      if (!Program.IsFna)
        return;
      SoundInstanceGarbageCollector._activeSounds.Add(sound);
    }

    public static void Update()
    {
      for (int index = 0; index < SoundInstanceGarbageCollector._activeSounds.Count; ++index)
      {
        if (SoundInstanceGarbageCollector._activeSounds[index] == null)
        {
          SoundInstanceGarbageCollector._activeSounds.RemoveAt(index);
          --index;
        }
        else if (SoundInstanceGarbageCollector._activeSounds[index].State == SoundState.Stopped)
        {
          SoundInstanceGarbageCollector._activeSounds[index].Dispose();
          SoundInstanceGarbageCollector._activeSounds.RemoveAt(index);
          --index;
        }
      }
    }
  }
}
