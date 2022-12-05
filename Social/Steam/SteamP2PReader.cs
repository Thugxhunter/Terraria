// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Steam.SteamP2PReader
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Steamworks;
using System;
using System.Collections.Generic;

namespace Terraria.Social.Steam
{
  public class SteamP2PReader
  {
    public object SteamLock = new object();
    private const int BUFFER_SIZE = 4096;
    private Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>> _pendingReadBuffers = new Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>>();
    private Queue<CSteamID> _deletionQueue = new Queue<CSteamID>();
    private Queue<byte[]> _bufferPool = new Queue<byte[]>();
    private int _channel;
    private SteamP2PReader.OnReadEvent _readEvent;

    public SteamP2PReader(int channel) => this._channel = channel;

    public void ClearUser(CSteamID id)
    {
      lock (this._pendingReadBuffers)
        this._deletionQueue.Enqueue(id);
    }

    public bool IsDataAvailable(CSteamID id)
    {
      lock (this._pendingReadBuffers)
      {
        if (!this._pendingReadBuffers.ContainsKey(id))
          return false;
        Queue<SteamP2PReader.ReadResult> pendingReadBuffer = this._pendingReadBuffers[id];
        return pendingReadBuffer.Count != 0 && pendingReadBuffer.Peek().Size != 0U;
      }
    }

    public void SetReadEvent(SteamP2PReader.OnReadEvent method) => this._readEvent = method;

    private bool IsPacketAvailable(out uint size)
    {
      lock (this.SteamLock)
        return SteamNetworking.IsP2PPacketAvailable(ref size, this._channel);
    }

    public void ReadTick()
    {
      lock (this._pendingReadBuffers)
      {
        while (this._deletionQueue.Count > 0)
          this._pendingReadBuffers.Remove(this._deletionQueue.Dequeue());
        uint size1;
        while (this.IsPacketAvailable(out size1))
        {
          byte[] data = this._bufferPool.Count != 0 ? this._bufferPool.Dequeue() : new byte[(int) Math.Max(size1, 4096U)];
          uint size2;
          CSteamID csteamId;
          bool flag;
          lock (this.SteamLock)
            flag = SteamNetworking.ReadP2PPacket(data, (uint) data.Length, ref size2, ref csteamId, this._channel);
          if (flag)
          {
            if (this._readEvent == null || this._readEvent(data, (int) size2, csteamId))
            {
              if (!this._pendingReadBuffers.ContainsKey(csteamId))
                this._pendingReadBuffers[csteamId] = new Queue<SteamP2PReader.ReadResult>();
              this._pendingReadBuffers[csteamId].Enqueue(new SteamP2PReader.ReadResult(data, size2));
            }
            else
              this._bufferPool.Enqueue(data);
          }
        }
      }
    }

    public int Receive(CSteamID user, byte[] buffer, int bufferOffset, int bufferSize)
    {
      uint num = 0;
      lock (this._pendingReadBuffers)
      {
        if (!this._pendingReadBuffers.ContainsKey(user))
          return 0;
        Queue<SteamP2PReader.ReadResult> pendingReadBuffer = this._pendingReadBuffers[user];
        while (pendingReadBuffer.Count > 0)
        {
          SteamP2PReader.ReadResult readResult = pendingReadBuffer.Peek();
          uint length = Math.Min((uint) bufferSize - num, readResult.Size - readResult.Offset);
          if (length == 0U)
            return (int) num;
          Array.Copy((Array) readResult.Data, (long) readResult.Offset, (Array) buffer, (long) bufferOffset + (long) num, (long) length);
          if ((int) length == (int) readResult.Size - (int) readResult.Offset)
            this._bufferPool.Enqueue(pendingReadBuffer.Dequeue().Data);
          else
            readResult.Offset += length;
          num += length;
        }
      }
      return (int) num;
    }

    public class ReadResult
    {
      public byte[] Data;
      public uint Size;
      public uint Offset;

      public ReadResult(byte[] data, uint size)
      {
        this.Data = data;
        this.Size = size;
        this.Offset = 0U;
      }
    }

    public delegate bool OnReadEvent(byte[] data, int size, CSteamID user);
  }
}
