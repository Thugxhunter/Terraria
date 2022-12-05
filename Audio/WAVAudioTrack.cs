// Decompiled with JetBrains decompiler
// Type: Terraria.Audio.WAVAudioTrack
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace Terraria.Audio
{
  public class WAVAudioTrack : ASoundEffectBasedAudioTrack
  {
    private long _streamContentStartIndex = -1;
    private Stream _stream;
    private const uint JUNK = 1263424842;
    private const uint FMT = 544501094;

    public WAVAudioTrack(Stream stream)
    {
      this._stream = stream;
      BinaryReader reader = new BinaryReader(stream);
      reader.ReadInt32();
      reader.ReadInt32();
      reader.ReadInt32();
      AudioChannels channels = AudioChannels.Mono;
      uint sampleRate = 0;
      bool flag = false;
      int num1 = 0;
      while (!flag && num1 < 10)
      {
        uint num2 = reader.ReadUInt32();
        int chunkSize = reader.ReadInt32();
        switch (num2)
        {
          case 544501094:
            int num3 = (int) reader.ReadInt16();
            channels = (AudioChannels) reader.ReadUInt16();
            sampleRate = reader.ReadUInt32();
            reader.ReadInt32();
            int num4 = (int) reader.ReadInt16();
            int num5 = (int) reader.ReadInt16();
            flag = true;
            break;
          case 1263424842:
            WAVAudioTrack.SkipJunk(reader, chunkSize);
            break;
        }
        if (!flag)
          ++num1;
      }
      reader.ReadInt32();
      reader.ReadInt32();
      this._streamContentStartIndex = stream.Position;
      this.CreateSoundEffect((int) sampleRate, channels);
    }

    private static void SkipJunk(BinaryReader reader, int chunkSize)
    {
      int count = chunkSize;
      if (count % 2 != 0)
        ++count;
      reader.ReadBytes(count);
    }

    protected override void ReadAheadPutAChunkIntoTheBuffer()
    {
      byte[] bufferToSubmit = this._bufferToSubmit;
      if (this._stream.Read(bufferToSubmit, 0, bufferToSubmit.Length) < 1)
        this.Stop(AudioStopOptions.Immediate);
      else
        this._soundEffectInstance.SubmitBuffer(this._bufferToSubmit);
    }

    public override void Reuse() => this._stream.Position = this._streamContentStartIndex;

    public override void Dispose()
    {
      this._soundEffectInstance.Dispose();
      this._stream.Dispose();
    }
  }
}
