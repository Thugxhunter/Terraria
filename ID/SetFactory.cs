// Decompiled with JetBrains decompiler
// Type: Terraria.ID.SetFactory
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using System.Collections.Generic;

namespace Terraria.ID
{
  public class SetFactory
  {
    protected int _size;
    private readonly Queue<int[]> _intBufferCache = new Queue<int[]>();
    private readonly Queue<ushort[]> _ushortBufferCache = new Queue<ushort[]>();
    private readonly Queue<bool[]> _boolBufferCache = new Queue<bool[]>();
    private readonly Queue<float[]> _floatBufferCache = new Queue<float[]>();
    private object _queueLock = new object();

    public SetFactory(int size) => this._size = size != 0 ? size : throw new ArgumentOutOfRangeException("size cannot be 0, the intializer for Count must run first");

    protected bool[] GetBoolBuffer()
    {
      lock (this._queueLock)
        return this._boolBufferCache.Count == 0 ? new bool[this._size] : this._boolBufferCache.Dequeue();
    }

    protected int[] GetIntBuffer()
    {
      lock (this._queueLock)
        return this._intBufferCache.Count == 0 ? new int[this._size] : this._intBufferCache.Dequeue();
    }

    protected ushort[] GetUshortBuffer()
    {
      lock (this._queueLock)
        return this._ushortBufferCache.Count == 0 ? new ushort[this._size] : this._ushortBufferCache.Dequeue();
    }

    protected float[] GetFloatBuffer()
    {
      lock (this._queueLock)
        return this._floatBufferCache.Count == 0 ? new float[this._size] : this._floatBufferCache.Dequeue();
    }

    public void Recycle<T>(T[] buffer)
    {
      lock (this._queueLock)
      {
        if (typeof (T).Equals(typeof (bool)))
        {
          this._boolBufferCache.Enqueue((bool[]) buffer);
        }
        else
        {
          if (!typeof (T).Equals(typeof (int)))
            return;
          this._intBufferCache.Enqueue((int[]) buffer);
        }
      }
    }

    public bool[] CreateBoolSet(params int[] types) => this.CreateBoolSet(false, types);

    public bool[] CreateBoolSet(bool defaultState, params int[] types)
    {
      bool[] boolBuffer = this.GetBoolBuffer();
      for (int index = 0; index < boolBuffer.Length; ++index)
        boolBuffer[index] = defaultState;
      for (int index = 0; index < types.Length; ++index)
        boolBuffer[types[index]] = !defaultState;
      return boolBuffer;
    }

    public int[] CreateIntSet(params int[] types) => this.CreateIntSet(-1, types);

    public int[] CreateIntSet(int defaultState, params int[] inputs)
    {
      if (inputs.Length % 2 != 0)
        throw new Exception("You have a bad length for inputs on CreateArraySet");
      int[] intBuffer = this.GetIntBuffer();
      for (int index = 0; index < intBuffer.Length; ++index)
        intBuffer[index] = defaultState;
      for (int index = 0; index < inputs.Length; index += 2)
        intBuffer[inputs[index]] = inputs[index + 1];
      return intBuffer;
    }

    public ushort[] CreateUshortSet(ushort defaultState, params ushort[] inputs)
    {
      if (inputs.Length % 2 != 0)
        throw new Exception("You have a bad length for inputs on CreateArraySet");
      ushort[] ushortBuffer = this.GetUshortBuffer();
      for (int index = 0; index < ushortBuffer.Length; ++index)
        ushortBuffer[index] = defaultState;
      for (int index = 0; index < inputs.Length; index += 2)
        ushortBuffer[(int) inputs[index]] = inputs[index + 1];
      return ushortBuffer;
    }

    public float[] CreateFloatSet(float defaultState, params float[] inputs)
    {
      if (inputs.Length % 2 != 0)
        throw new Exception("You have a bad length for inputs on CreateArraySet");
      float[] floatBuffer = this.GetFloatBuffer();
      for (int index = 0; index < floatBuffer.Length; ++index)
        floatBuffer[index] = defaultState;
      for (int index = 0; index < inputs.Length; index += 2)
        floatBuffer[(int) inputs[index]] = inputs[index + 1];
      return floatBuffer;
    }

    public T[] CreateCustomSet<T>(T defaultState, params object[] inputs)
    {
      if (inputs.Length % 2 != 0)
        throw new Exception("You have a bad length for inputs on CreateCustomSet");
      T[] customSet = new T[this._size];
      for (int index = 0; index < customSet.Length; ++index)
        customSet[index] = defaultState;
      if (inputs != null)
      {
        for (int index = 0; index < inputs.Length; index += 2)
        {
          T obj = !typeof (T).IsPrimitive ? (!typeof (T).IsGenericType || !(typeof (T).GetGenericTypeDefinition() == typeof (Nullable<>)) ? (!typeof (T).IsClass ? (T) Convert.ChangeType(inputs[index + 1], typeof (T)) : (T) inputs[index + 1]) : (T) inputs[index + 1]) : (T) inputs[index + 1];
          if (inputs[index] is ushort)
            customSet[(int) (ushort) inputs[index]] = obj;
          else if (inputs[index] is int)
            customSet[(int) inputs[index]] = obj;
          else
            customSet[(int) (short) inputs[index]] = obj;
        }
      }
      return customSet;
    }
  }
}
