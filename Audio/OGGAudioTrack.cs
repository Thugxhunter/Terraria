// Decompiled with JetBrains decompiler
// Type: Terraria.Audio.OGGAudioTrack
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Audio;
using NVorbis;
using System.Collections.Generic;
using System.IO;

namespace Terraria.Audio
{
  public class OGGAudioTrack : ASoundEffectBasedAudioTrack
  {
    private VorbisReader _vorbisReader;
    private int _loopStart;
    private int _loopEnd;

    public OGGAudioTrack(Stream streamToRead)
    {
      this._vorbisReader = new VorbisReader(streamToRead, true);
      this.FindLoops();
      this.CreateSoundEffect(this._vorbisReader.SampleRate, (AudioChannels) this._vorbisReader.Channels);
    }

    protected override void ReadAheadPutAChunkIntoTheBuffer()
    {
      this.PrepareBufferToSubmit();
      this._soundEffectInstance.SubmitBuffer(this._bufferToSubmit);
    }

    private void PrepareBufferToSubmit()
    {
      byte[] bufferToSubmit = this._bufferToSubmit;
      float[] temporaryBuffer = this._temporaryBuffer;
      VorbisReader vorbisReader = this._vorbisReader;
      int num = vorbisReader.ReadSamples(temporaryBuffer, 0, temporaryBuffer.Length);
      if (((this._loopEnd <= 0 ? 0 : (vorbisReader.DecodedPosition >= (long) this._loopEnd ? 1 : 0)) | (num < temporaryBuffer.Length ? 1 : 0)) != 0)
      {
        vorbisReader.DecodedPosition = (long) this._loopStart;
        vorbisReader.ReadSamples(temporaryBuffer, num, temporaryBuffer.Length - num);
      }
      OGGAudioTrack.ApplyTemporaryBufferTo(temporaryBuffer, bufferToSubmit);
    }

    private static void ApplyTemporaryBufferTo(float[] temporaryBuffer, byte[] samplesBuffer)
    {
      for (int index = 0; index < temporaryBuffer.Length; ++index)
      {
        short num = (short) ((double) temporaryBuffer[index] * (double) short.MaxValue);
        samplesBuffer[index * 2] = (byte) num;
        samplesBuffer[index * 2 + 1] = (byte) ((uint) num >> 8);
      }
    }

    public override void Reuse() => this._vorbisReader.SeekTo(0L, SeekOrigin.Begin);

    private void FindLoops()
    {
      IDictionary<string, IList<string>> all = this._vorbisReader.Tags.All;
      this.TryReadingTag(all, "LOOPSTART", ref this._loopStart);
      this.TryReadingTag(all, "LOOPEND", ref this._loopEnd);
    }

    private void TryReadingTag(
      IDictionary<string, IList<string>> tags,
      string entryName,
      ref int result)
    {
      IList<string> stringList;
      int result1;
      if (!tags.TryGetValue(entryName, out stringList) || stringList.Count <= 0 || !int.TryParse(stringList[0], out result1))
        return;
      result = result1;
    }

    public override void Dispose()
    {
      this._soundEffectInstance.Dispose();
      this._vorbisReader.Dispose();
    }
  }
}
