// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Renderers.ParticlePool`1
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;

namespace Terraria.Graphics.Renderers
{
  public class ParticlePool<T> where T : IPooledParticle
  {
    private ParticlePool<T>.ParticleInstantiator _instantiator;
    private List<T> _particles;

    public int CountParticlesInUse()
    {
      int num = 0;
      for (int index = 0; index < num; ++index)
      {
        if (!this._particles[index].IsRestingInPool)
          ++num;
      }
      return num;
    }

    public ParticlePool(int initialPoolSize, ParticlePool<T>.ParticleInstantiator instantiator)
    {
      this._particles = new List<T>(initialPoolSize);
      this._instantiator = instantiator;
    }

    public T RequestParticle()
    {
      int count = this._particles.Count;
      for (int index = 0; index < count; ++index)
      {
        T particle = this._particles[index];
        if (particle.IsRestingInPool)
        {
          particle = this._particles[index];
          particle.FetchFromPool();
          return this._particles[index];
        }
      }
      T obj = this._instantiator();
      this._particles.Add(obj);
      obj.FetchFromPool();
      return obj;
    }

    public delegate T ParticleInstantiator() where T : IPooledParticle;
  }
}
