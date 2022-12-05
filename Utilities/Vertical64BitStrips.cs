// Decompiled with JetBrains decompiler
// Type: Terraria.Utilities.Vertical64BitStrips
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using System.Text;

namespace Terraria.Utilities
{
  public struct Vertical64BitStrips
  {
    private Bits64[] arr;

    public Vertical64BitStrips(int len) => this.arr = new Bits64[len];

    public void Clear() => Array.Clear((Array) this.arr, 0, this.arr.Length);

    public Bits64 this[int x]
    {
      get => this.arr[x];
      set => this.arr[x] = value;
    }

    public void Expand3x3()
    {
      for (int index = 0; index < this.arr.Length - 1; ++index)
      {
        ref Bits64 local = ref this.arr[index];
        local = (Bits64) ((ulong) local | (ulong) this.arr[index + 1]);
      }
      for (int index = this.arr.Length - 1; index > 0; --index)
      {
        ref Bits64 local = ref this.arr[index];
        local = (Bits64) ((ulong) local | (ulong) this.arr[index - 1]);
      }
      for (int index = 0; index < this.arr.Length; ++index)
      {
        Bits64 bits64 = this.arr[index];
        this.arr[index] = (Bits64) ((ulong) bits64 << 1 | (ulong) bits64 | (ulong) bits64 >> 1);
      }
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder(this.arr.Length * 65);
      for (int i = 0; i < 64; ++i)
      {
        if (i > 0)
          stringBuilder.Append('\n');
        for (int x = 0; x < this.arr.Length; ++x)
          stringBuilder.Append(this[x][i] ? 'x' : ' ');
      }
      return stringBuilder.ToString();
    }
  }
}
