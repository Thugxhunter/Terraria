// Decompiled with JetBrains decompiler
// Type: Terraria.Audio.SoundPlayer
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using System.Collections.Generic;

namespace Terraria.Audio
{
  public class SoundPlayer
  {
    private readonly SlotVector<ActiveSound> _trackedSounds = new SlotVector<ActiveSound>(4096);

    public SlotId Play(SoundStyle style, Vector2 position) => Main.dedServ || style == null || !style.IsTrackable || (double) Vector2.DistanceSquared(Main.screenPosition + new Vector2((float) (Main.screenWidth / 2), (float) (Main.screenHeight / 2)), position) > 100000000.0 ? SlotId.Invalid : this._trackedSounds.Add(new ActiveSound(style, position));

    public SlotId PlayLooped(
      SoundStyle style,
      Vector2 position,
      ActiveSound.LoopedPlayCondition loopingCondition)
    {
      return Main.dedServ || style == null || !style.IsTrackable || (double) Vector2.DistanceSquared(Main.screenPosition + new Vector2((float) (Main.screenWidth / 2), (float) (Main.screenHeight / 2)), position) > 100000000.0 ? SlotId.Invalid : this._trackedSounds.Add(new ActiveSound(style, position, loopingCondition));
    }

    public void Reload() => this.StopAll();

    public SlotId Play(SoundStyle style) => Main.dedServ || style == null || !style.IsTrackable ? SlotId.Invalid : this._trackedSounds.Add(new ActiveSound(style));

    public ActiveSound GetActiveSound(SlotId id) => !this._trackedSounds.Has(id) ? (ActiveSound) null : this._trackedSounds[id];

    public void PauseAll()
    {
      foreach (SlotVector<ActiveSound>.ItemPair trackedSound in (IEnumerable<SlotVector<ActiveSound>.ItemPair>) this._trackedSounds)
        trackedSound.Value.Pause();
    }

    public void ResumeAll()
    {
      foreach (SlotVector<ActiveSound>.ItemPair trackedSound in (IEnumerable<SlotVector<ActiveSound>.ItemPair>) this._trackedSounds)
        trackedSound.Value.Resume();
    }

    public void StopAll()
    {
      foreach (SlotVector<ActiveSound>.ItemPair trackedSound in (IEnumerable<SlotVector<ActiveSound>.ItemPair>) this._trackedSounds)
        trackedSound.Value.Stop();
      this._trackedSounds.Clear();
    }

    public void Update()
    {
      foreach (SlotVector<ActiveSound>.ItemPair trackedSound in (IEnumerable<SlotVector<ActiveSound>.ItemPair>) this._trackedSounds)
      {
        try
        {
          trackedSound.Value.Update();
          if (!trackedSound.Value.IsPlaying)
            this._trackedSounds.Remove(trackedSound.Id);
        }
        catch
        {
          this._trackedSounds.Remove(trackedSound.Id);
        }
      }
    }

    public ActiveSound FindActiveSound(SoundStyle style)
    {
      foreach (SlotVector<ActiveSound>.ItemPair trackedSound in (IEnumerable<SlotVector<ActiveSound>.ItemPair>) this._trackedSounds)
      {
        if (trackedSound.Value.Style == style)
          return trackedSound.Value;
      }
      return (ActiveSound) null;
    }
  }
}
