// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.EmpressBladeDrawer
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics
{
  public struct EmpressBladeDrawer
  {
    public const int TotalIllusions = 1;
    public const int FramesPerImportantTrail = 60;
    private static VertexStrip _vertexStrip = new VertexStrip();
    public Color ColorStart;
    public Color ColorEnd;

    public void Draw(Projectile proj)
    {
      double num = (double) proj.ai[1];
      MiscShaderData miscShaderData = GameShaders.Misc["EmpressBlade"];
      miscShaderData.UseShaderSpecificData(new Vector4(1f, 0.0f, 0.0f, 0.6f));
      miscShaderData.Apply(new DrawData?());
      EmpressBladeDrawer._vertexStrip.PrepareStrip(proj.oldPos, proj.oldRot, new VertexStrip.StripColorFunction(this.StripColors), new VertexStrip.StripHalfWidthFunction(this.StripWidth), -Main.screenPosition + proj.Size / 2f, new int?(proj.oldPos.Length), true);
      EmpressBladeDrawer._vertexStrip.DrawTrail();
      Main.pixelShader.CurrentTechnique.Passes[0].Apply();
    }

    private Color StripColors(float progressOnStrip)
    {
      Color color = Color.Lerp(this.ColorStart, this.ColorEnd, Utils.GetLerpValue(0.0f, 0.7f, progressOnStrip, true)) * (1f - Utils.GetLerpValue(0.0f, 0.98f, progressOnStrip, true));
      color.A /= (byte) 2;
      return color;
    }

    private float StripWidth(float progressOnStrip) => 36f;
  }
}
