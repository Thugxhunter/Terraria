// Decompiled with JetBrains decompiler
// Type: Terraria.Audio.SoundEngine
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using ReLogic.Utilities;
using System;
using System.IO;

namespace Terraria.Audio
{
  public static class SoundEngine
  {
    public static LegacySoundPlayer LegacySoundPlayer;
    public static SoundPlayer SoundPlayer;
    public static bool AreSoundsPaused;

    public static bool IsAudioSupported { get; private set; }

    public static void Initialize() => SoundEngine.IsAudioSupported = SoundEngine.TestAudioSupport();

    public static void Load(IServiceProvider services)
    {
      if (!SoundEngine.IsAudioSupported)
        return;
      SoundEngine.LegacySoundPlayer = new LegacySoundPlayer(services);
      SoundEngine.SoundPlayer = new SoundPlayer();
    }

    public static void Update()
    {
      if (!SoundEngine.IsAudioSupported)
        return;
      if (Main.audioSystem != null)
        Main.audioSystem.UpdateAudioEngine();
      SoundInstanceGarbageCollector.Update();
      bool flag = (!Main.hasFocus || Main.gamePaused) && Main.netMode == 0;
      if (!SoundEngine.AreSoundsPaused & flag)
        SoundEngine.SoundPlayer.PauseAll();
      else if (SoundEngine.AreSoundsPaused && !flag)
        SoundEngine.SoundPlayer.ResumeAll();
      SoundEngine.AreSoundsPaused = flag;
      SoundEngine.SoundPlayer.Update();
    }

    public static void Reload()
    {
      if (!SoundEngine.IsAudioSupported)
        return;
      if (SoundEngine.LegacySoundPlayer != null)
        SoundEngine.LegacySoundPlayer.Reload();
      if (SoundEngine.SoundPlayer == null)
        return;
      SoundEngine.SoundPlayer.Reload();
    }

    public static void PlaySound(int type, Vector2 position, int style = 1) => SoundEngine.PlaySound(type, (int) position.X, (int) position.Y, style);

    public static SoundEffectInstance PlaySound(
      LegacySoundStyle type,
      Vector2 position)
    {
      return SoundEngine.PlaySound(type, (int) position.X, (int) position.Y);
    }

    public static SoundEffectInstance PlaySound(
      LegacySoundStyle type,
      int x = -1,
      int y = -1)
    {
      return type == null ? (SoundEffectInstance) null : SoundEngine.PlaySound(type.SoundId, x, y, type.Style, type.Volume, type.GetRandomPitch());
    }

    public static SoundEffectInstance PlaySound(
      int type,
      int x = -1,
      int y = -1,
      int Style = 1,
      float volumeScale = 1f,
      float pitchOffset = 0.0f)
    {
      return Main.dedServ || !SoundEngine.IsAudioSupported ? (SoundEffectInstance) null : SoundEngine.LegacySoundPlayer.PlaySound(type, x, y, Style, volumeScale, pitchOffset);
    }

    public static ActiveSound GetActiveSound(SlotId id) => Main.dedServ || !SoundEngine.IsAudioSupported ? (ActiveSound) null : SoundEngine.SoundPlayer.GetActiveSound(id);

    public static SlotId PlayTrackedSound(SoundStyle style, Vector2 position) => Main.dedServ || !SoundEngine.IsAudioSupported ? SlotId.Invalid : SoundEngine.SoundPlayer.Play(style, position);

    public static SlotId PlayTrackedLoopedSound(
      SoundStyle style,
      Vector2 position,
      ActiveSound.LoopedPlayCondition loopingCondition = null)
    {
      return Main.dedServ || !SoundEngine.IsAudioSupported ? SlotId.Invalid : SoundEngine.SoundPlayer.PlayLooped(style, position, loopingCondition);
    }

    public static SlotId PlayTrackedSound(SoundStyle style) => Main.dedServ || !SoundEngine.IsAudioSupported ? SlotId.Invalid : SoundEngine.SoundPlayer.Play(style);

    public static void StopTrackedSounds()
    {
      if (Main.dedServ || !SoundEngine.IsAudioSupported)
        return;
      SoundEngine.SoundPlayer.StopAll();
    }

    public static SoundEffect GetTrackableSoundByStyleId(int id) => Main.dedServ || !SoundEngine.IsAudioSupported ? (SoundEffect) null : SoundEngine.LegacySoundPlayer.GetTrackableSoundByStyleId(id);

    public static void StopAmbientSounds()
    {
      if (Main.dedServ || !SoundEngine.IsAudioSupported || SoundEngine.LegacySoundPlayer == null)
        return;
      SoundEngine.LegacySoundPlayer.StopAmbientSounds();
    }

    public static ActiveSound FindActiveSound(SoundStyle style) => Main.dedServ || !SoundEngine.IsAudioSupported ? (ActiveSound) null : SoundEngine.SoundPlayer.FindActiveSound(style);

    private static bool TestAudioSupport()
    {
      byte[] buffer = new byte[166]
      {
        (byte) 82,
        (byte) 73,
        (byte) 70,
        (byte) 70,
        (byte) 158,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 87,
        (byte) 65,
        (byte) 86,
        (byte) 69,
        (byte) 102,
        (byte) 109,
        (byte) 116,
        (byte) 32,
        (byte) 16,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 1,
        (byte) 0,
        (byte) 1,
        (byte) 0,
        (byte) 68,
        (byte) 172,
        (byte) 0,
        (byte) 0,
        (byte) 136,
        (byte) 88,
        (byte) 1,
        (byte) 0,
        (byte) 2,
        (byte) 0,
        (byte) 16,
        (byte) 0,
        (byte) 76,
        (byte) 73,
        (byte) 83,
        (byte) 84,
        (byte) 26,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 73,
        (byte) 78,
        (byte) 70,
        (byte) 79,
        (byte) 73,
        (byte) 83,
        (byte) 70,
        (byte) 84,
        (byte) 14,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 76,
        (byte) 97,
        (byte) 118,
        (byte) 102,
        (byte) 53,
        (byte) 54,
        (byte) 46,
        (byte) 52,
        (byte) 48,
        (byte) 46,
        (byte) 49,
        (byte) 48,
        (byte) 49,
        (byte) 0,
        (byte) 100,
        (byte) 97,
        (byte) 116,
        (byte) 97,
        (byte) 88,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 126,
        (byte) 4,
        (byte) 240,
        (byte) 8,
        (byte) 64,
        (byte) 13,
        (byte) 95,
        (byte) 17,
        (byte) 67,
        (byte) 21,
        (byte) 217,
        (byte) 24,
        (byte) 23,
        (byte) 28,
        (byte) 240,
        (byte) 30,
        (byte) 94,
        (byte) 33,
        (byte) 84,
        (byte) 35,
        (byte) 208,
        (byte) 36,
        (byte) 204,
        (byte) 37,
        (byte) 71,
        (byte) 38,
        (byte) 64,
        (byte) 38,
        (byte) 183,
        (byte) 37,
        (byte) 180,
        (byte) 36,
        (byte) 58,
        (byte) 35,
        (byte) 79,
        (byte) 33,
        (byte) 1,
        (byte) 31,
        (byte) 86,
        (byte) 28,
        (byte) 92,
        (byte) 25,
        (byte) 37,
        (byte) 22,
        (byte) 185,
        (byte) 18,
        (byte) 42,
        (byte) 15,
        (byte) 134,
        (byte) 11,
        (byte) 222,
        (byte) 7,
        (byte) 68,
        (byte) 4,
        (byte) 196,
        (byte) 0,
        (byte) 112,
        (byte) 253,
        (byte) 86,
        (byte) 250,
        (byte) 132,
        (byte) 247,
        (byte) 6,
        (byte) 245,
        (byte) 230,
        (byte) 242,
        (byte) 47,
        (byte) 241,
        (byte) 232,
        (byte) 239,
        (byte) 25,
        (byte) 239,
        (byte) 194,
        (byte) 238,
        (byte) 231,
        (byte) 238,
        (byte) 139,
        (byte) 239,
        (byte) 169,
        (byte) 240,
        (byte) 61,
        (byte) 242,
        (byte) 67,
        (byte) 244,
        (byte) 180,
        (byte) 246
      };
      try
      {
        using (MemoryStream memoryStream = new MemoryStream(buffer))
          SoundEffect.FromStream((Stream) memoryStream);
      }
      catch (NoAudioHardwareException ex)
      {
        Console.WriteLine("No audio hardware found. Disabling all audio.");
        return false;
      }
      catch
      {
        return false;
      }
      return true;
    }
  }
}
