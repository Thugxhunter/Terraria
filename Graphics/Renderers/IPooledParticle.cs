// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Renderers.IPooledParticle
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.Graphics.Renderers
{
  public interface IPooledParticle : IParticle
  {
    bool IsRestingInPool { get; }

    void RestInPool();

    void FetchFromPool();
  }
}
