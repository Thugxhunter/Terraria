// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Renderers.ParticleRenderer
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Terraria.Graphics.Renderers
{
  public class ParticleRenderer
  {
    public ParticleRendererSettings Settings;
    public List<IParticle> Particles = new List<IParticle>();

    public ParticleRenderer() => this.Settings = new ParticleRendererSettings();

    public void Add(IParticle particle) => this.Particles.Add(particle);

    public void Clear() => this.Particles.Clear();

    public void Update()
    {
      for (int index = 0; index < this.Particles.Count; ++index)
      {
        if (this.Particles[index].ShouldBeRemovedFromRenderer)
        {
          if (this.Particles[index] is IPooledParticle particle)
            particle.RestInPool();
          this.Particles.RemoveAt(index);
          --index;
        }
        else
          this.Particles[index].Update(ref this.Settings);
      }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      for (int index = 0; index < this.Particles.Count; ++index)
      {
        if (!this.Particles[index].ShouldBeRemovedFromRenderer)
          this.Particles[index].Draw(ref this.Settings, spriteBatch);
      }
    }
  }
}
