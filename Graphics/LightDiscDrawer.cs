// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.LightDiscDrawer
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics
{
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct LightDiscDrawer
  {
    private static VertexStrip _vertexStrip = new VertexStrip();

    public void Draw(Projectile proj)
    {
      MiscShaderData miscShaderData = GameShaders.Misc["LightDisc"];
      miscShaderData.UseSaturation(-2.8f);
      miscShaderData.UseOpacity(2f);
      miscShaderData.Apply(new DrawData?());
      LightDiscDrawer._vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, new VertexStrip.StripColorFunction(this.StripColors), new VertexStrip.StripHalfWidthFunction(this.StripWidth), -Main.screenPosition + proj.Size / 2f);
      LightDiscDrawer._vertexStrip.DrawTrail();
      Main.pixelShader.CurrentTechnique.Passes[0].Apply();
    }

    private Color StripColors(float progressOnStrip)
    {
      float num = 1f - progressOnStrip;
      return (new Color(48, 63, 150) * (num * num * num * num) * 0.5f) with
      {
        A = 0
      };
    }

    private float StripWidth(float progressOnStrip) => 16f;
  }
}
