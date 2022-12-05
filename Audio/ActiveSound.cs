// Decompiled with JetBrains decompiler
// Type: Terraria.Audio.ActiveSound
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
  public class ActiveSound
  {
    public readonly bool IsGlobal;
    public Vector2 Position;
    public float Volume;
    public float Pitch;
    public ActiveSound.LoopedPlayCondition Condition;

    public SoundEffectInstance Sound { get; private set; }

    public SoundStyle Style { get; private set; }

    public bool IsPlaying => this.Sound.State == SoundState.Playing;

    public ActiveSound(SoundStyle style, Vector2 position)
    {
      this.Position = position;
      this.Volume = 1f;
      this.Pitch = style.PitchVariance;
      this.IsGlobal = false;
      this.Style = style;
      this.Play();
    }

    public ActiveSound(SoundStyle style)
    {
      this.Position = Vector2.Zero;
      this.Volume = 1f;
      this.Pitch = style.PitchVariance;
      this.IsGlobal = true;
      this.Style = style;
      this.Play();
    }

    public ActiveSound(
      SoundStyle style,
      Vector2 position,
      ActiveSound.LoopedPlayCondition condition)
    {
      this.Position = position;
      this.Volume = 1f;
      this.Pitch = style.PitchVariance;
      this.IsGlobal = false;
      this.Style = style;
      this.PlayLooped(condition);
    }

    private void Play()
    {
      SoundEffectInstance instance = this.Style.GetRandomSound().CreateInstance();
      instance.Pitch += this.Style.GetRandomPitch();
      this.Pitch = instance.Pitch;
      instance.Play();
      SoundInstanceGarbageCollector.Track(instance);
      this.Sound = instance;
      this.Update();
    }

    private void PlayLooped(ActiveSound.LoopedPlayCondition condition)
    {
      SoundEffectInstance instance = this.Style.GetRandomSound().CreateInstance();
      instance.Pitch += this.Style.GetRandomPitch();
      this.Pitch = instance.Pitch;
      instance.IsLooped = true;
      this.Condition = condition;
      instance.Play();
      SoundInstanceGarbageCollector.Track(instance);
      this.Sound = instance;
      this.Update();
    }

    public void Stop()
    {
      if (this.Sound == null)
        return;
      this.Sound.Stop();
    }

    public void Pause()
    {
      if (this.Sound == null || this.Sound.State != SoundState.Playing)
        return;
      this.Sound.Pause();
    }

    public void Resume()
    {
      if (this.Sound == null || this.Sound.State != SoundState.Paused)
        return;
      this.Sound.Resume();
    }

    public void Update()
    {
      if (this.Sound == null)
        return;
      if (this.Condition != null && !this.Condition())
      {
        this.Sound.Stop(true);
      }
      else
      {
        Vector2 vector2 = Main.screenPosition + new Vector2((float) (Main.screenWidth / 2), (float) (Main.screenHeight / 2));
        float num1 = 1f;
        if (!this.IsGlobal)
        {
          this.Sound.Pan = MathHelper.Clamp((float) (((double) this.Position.X - (double) vector2.X) / ((double) Main.screenWidth * 0.5)), -1f, 1f);
          num1 = (float) (1.0 - (double) Vector2.Distance(this.Position, vector2) / ((double) Main.screenWidth * 1.5));
        }
        float num2 = num1 * (this.Style.Volume * this.Volume);
        switch (this.Style.Type)
        {
          case SoundType.Sound:
            num2 *= Main.soundVolume;
            break;
          case SoundType.Ambient:
            num2 *= Main.ambientVolume;
            break;
          case SoundType.Music:
            num2 *= Main.musicVolume;
            break;
        }
        this.Sound.Volume = MathHelper.Clamp(num2, 0.0f, 1f);
        this.Sound.Pitch = this.Pitch;
      }
    }

    public delegate bool LoopedPlayCondition();
  }
}
