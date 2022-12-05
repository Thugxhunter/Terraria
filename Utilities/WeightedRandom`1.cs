﻿// Decompiled with JetBrains decompiler
// Type: Terraria.Utilities.WeightedRandom`1
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.Utilities
{
  public class WeightedRandom<T>
  {
    public readonly List<Tuple<T, double>> elements = new List<Tuple<T, double>>();
    public readonly UnifiedRandom random;
    public bool needsRefresh = true;
    private double _totalWeight;

    public WeightedRandom() => this.random = new UnifiedRandom();

    public WeightedRandom(int seed) => this.random = new UnifiedRandom(seed);

    public WeightedRandom(UnifiedRandom random) => this.random = random;

    public WeightedRandom(params Tuple<T, double>[] theElements)
    {
      this.random = new UnifiedRandom();
      this.elements = ((IEnumerable<Tuple<T, double>>) theElements).ToList<Tuple<T, double>>();
    }

    public WeightedRandom(int seed, params Tuple<T, double>[] theElements)
    {
      this.random = new UnifiedRandom(seed);
      this.elements = ((IEnumerable<Tuple<T, double>>) theElements).ToList<Tuple<T, double>>();
    }

    public WeightedRandom(UnifiedRandom random, params Tuple<T, double>[] theElements)
    {
      this.random = random;
      this.elements = ((IEnumerable<Tuple<T, double>>) theElements).ToList<Tuple<T, double>>();
    }

    public void Add(T element, double weight = 1.0)
    {
      this.elements.Add(new Tuple<T, double>(element, weight));
      this.needsRefresh = true;
    }

    public T Get()
    {
      if (this.needsRefresh)
        this.CalculateTotalWeight();
      double num = this.random.NextDouble() * this._totalWeight;
      foreach (Tuple<T, double> element in this.elements)
      {
        if (num <= element.Item2)
          return element.Item1;
        num -= element.Item2;
      }
      return default (T);
    }

    public void CalculateTotalWeight()
    {
      this._totalWeight = 0.0;
      foreach (Tuple<T, double> element in this.elements)
        this._totalWeight += element.Item2;
      this.needsRefresh = false;
    }

    public void Clear() => this.elements.Clear();

    public static implicit operator T(WeightedRandom<T> weightedRandom) => weightedRandom.Get();
  }
}
