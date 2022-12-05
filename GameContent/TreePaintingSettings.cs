// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.TreePaintingSettings
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent
{
  public class TreePaintingSettings
  {
    public float SpecialGroupMinimalHueValue;
    public float SpecialGroupMaximumHueValue;
    public float SpecialGroupMinimumSaturationValue;
    public float SpecialGroupMaximumSaturationValue;
    public float HueTestOffset;
    public bool UseSpecialGroups;
    public bool UseWallShaderHacks;
    public bool InvertSpecialGroupResult;

    public void ApplyShader(int paintColor, Effect shader)
    {
      shader.Parameters["leafHueTestOffset"].SetValue(this.HueTestOffset);
      shader.Parameters["leafMinHue"].SetValue(this.SpecialGroupMinimalHueValue);
      shader.Parameters["leafMaxHue"].SetValue(this.SpecialGroupMaximumHueValue);
      shader.Parameters["leafMinSat"].SetValue(this.SpecialGroupMinimumSaturationValue);
      shader.Parameters["leafMaxSat"].SetValue(this.SpecialGroupMaximumSaturationValue);
      shader.Parameters["invertSpecialGroupResult"].SetValue(this.InvertSpecialGroupResult);
      int tileShaderIndex = Main.ConvertPaintIdToTileShaderIndex(paintColor, this.UseSpecialGroups, this.UseWallShaderHacks);
      shader.CurrentTechnique.Passes[tileShaderIndex].Apply();
    }
  }
}
