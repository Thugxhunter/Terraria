// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Renderers.IParticle
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Renderers
{
  public interface IParticle
  {
    bool ShouldBeRemovedFromRenderer { get; }

    void Update(ref ParticleRendererSettings settings);

    void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch);
  }
}
