// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Dyes.TwilightHairDyeShaderData
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Dyes
{
  public class TwilightHairDyeShaderData : HairShaderData
  {
    public TwilightHairDyeShaderData(Ref<Effect> shader, string passName)
      : base(shader, passName)
    {
    }

    public override void Apply(Player player, DrawData? drawData = null)
    {
      if (drawData.HasValue)
        this.UseTargetPosition(Main.screenPosition + drawData.Value.position);
      base.Apply(player, drawData);
    }
  }
}
