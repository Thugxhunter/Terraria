// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Dyes.TwilightDyeShaderData
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Dyes
{
  public class TwilightDyeShaderData : ArmorShaderData
  {
    public TwilightDyeShaderData(Ref<Effect> shader, string passName)
      : base(shader, passName)
    {
    }

    public override void Apply(Entity entity, DrawData? drawData)
    {
      if (drawData.HasValue)
      {
        switch (entity)
        {
          case Player player when !player.isDisplayDollOrInanimate && !player.isHatRackDoll:
            this.UseTargetPosition(Main.screenPosition + drawData.Value.position);
            break;
          case Projectile _:
            this.UseTargetPosition(Main.screenPosition + drawData.Value.position);
            break;
          default:
            this.UseTargetPosition(drawData.Value.position);
            break;
        }
      }
      base.Apply(entity, drawData);
    }
  }
}
