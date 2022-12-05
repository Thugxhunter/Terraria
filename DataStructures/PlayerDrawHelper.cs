// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.PlayerDrawHelper
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.DataStructures
{
  public class PlayerDrawHelper
  {
    public static Color DISPLAY_DOLL_DEFAULT_SKIN_COLOR = new Color(163, 121, 92);

    public static int PackShader(
      int localShaderIndex,
      PlayerDrawHelper.ShaderConfiguration shaderType)
    {
      return localShaderIndex + (int) shaderType * 1000;
    }

    public static void UnpackShader(
      int packedShaderIndex,
      out int localShaderIndex,
      out PlayerDrawHelper.ShaderConfiguration shaderType)
    {
      shaderType = (PlayerDrawHelper.ShaderConfiguration) (packedShaderIndex / 1000);
      localShaderIndex = packedShaderIndex % 1000;
    }

    public static void SetShaderForData(Player player, int cHead, ref DrawData cdd)
    {
      int localShaderIndex;
      PlayerDrawHelper.ShaderConfiguration shaderType;
      PlayerDrawHelper.UnpackShader(cdd.shader, out localShaderIndex, out shaderType);
      switch (shaderType)
      {
        case PlayerDrawHelper.ShaderConfiguration.ArmorShader:
          GameShaders.Hair.Apply((short) 0, player, new DrawData?(cdd));
          GameShaders.Armor.Apply(localShaderIndex, (Entity) player, new DrawData?(cdd));
          break;
        case PlayerDrawHelper.ShaderConfiguration.HairShader:
          if (player.head == 0)
          {
            GameShaders.Hair.Apply((short) 0, player, new DrawData?(cdd));
            GameShaders.Armor.Apply(cHead, (Entity) player, new DrawData?(cdd));
            break;
          }
          GameShaders.Armor.Apply(0, (Entity) player, new DrawData?(cdd));
          GameShaders.Hair.Apply((short) localShaderIndex, player, new DrawData?(cdd));
          break;
        case PlayerDrawHelper.ShaderConfiguration.TileShader:
          Main.tileShader.CurrentTechnique.Passes[localShaderIndex].Apply();
          break;
        case PlayerDrawHelper.ShaderConfiguration.TilePaintID:
          Main.tileShader.CurrentTechnique.Passes[Main.ConvertPaintIdToTileShaderIndex(localShaderIndex, false, false)].Apply();
          break;
      }
    }

    public enum ShaderConfiguration
    {
      ArmorShader,
      HairShader,
      TileShader,
      TilePaintID,
    }
  }
}
