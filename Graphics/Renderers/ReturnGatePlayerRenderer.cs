// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Renderers.ReturnGatePlayerRenderer
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;

namespace Terraria.Graphics.Renderers
{
  internal class ReturnGatePlayerRenderer : IPlayerRenderer
  {
    private List<DrawData> _voidLensData = new List<DrawData>();
    private PotionOfReturnGateInteractionChecker _interactionChecker = new PotionOfReturnGateInteractionChecker();

    public void DrawPlayers(Camera camera, IEnumerable<Player> players)
    {
      foreach (Player player in players)
        this.DrawReturnGateInWorld(camera, player);
    }

    public void DrawPlayerHead(
      Camera camera,
      Player drawPlayer,
      Vector2 position,
      float alpha = 1f,
      float scale = 1f,
      Color borderColor = default (Color))
    {
      this.DrawReturnGateInMap(camera, drawPlayer);
    }

    public void DrawPlayer(
      Camera camera,
      Player drawPlayer,
      Vector2 position,
      float rotation,
      Vector2 rotationOrigin,
      float shadow = 0.0f,
      float scale = 1f)
    {
      this.DrawReturnGateInWorld(camera, drawPlayer);
    }

    private void DrawReturnGateInMap(Camera camera, Player player)
    {
    }

    private void DrawReturnGateInWorld(Camera camera, Player player)
    {
      Rectangle homeHitbox = Rectangle.Empty;
      if (!PotionOfReturnHelper.TryGetGateHitbox(player, out homeHitbox))
        return;
      AHoverInteractionChecker.HoverStatus hoverStatus = AHoverInteractionChecker.HoverStatus.NotSelectable;
      if (player == Main.LocalPlayer)
      {
        int num = (int) this._interactionChecker.AttemptInteraction(player, homeHitbox);
      }
      if (Main.SmartInteractPotionOfReturn)
        hoverStatus = AHoverInteractionChecker.HoverStatus.Selected;
      int selectionMode = (int) hoverStatus;
      if (!player.PotionOfReturnOriginalUsePosition.HasValue)
        return;
      SpriteBatch spriteBatch = camera.SpriteBatch;
      SamplerState sampler = camera.Sampler;
      spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, sampler, DepthStencilState.None, camera.Rasterizer, (Effect) null, camera.GameViewMatrix.TransformationMatrix);
      float opacity = player.whoAmI == Main.myPlayer ? 1f : 0.1f;
      Vector2 worldPosition = player.PotionOfReturnOriginalUsePosition.Value + new Vector2(0.0f, -21f);
      Vector2 vector2 = homeHitbox.Center.ToVector2();
      PotionOfReturnGateHelper returnGateHelper1 = new PotionOfReturnGateHelper(PotionOfReturnGateHelper.GateType.ExitPoint, worldPosition, opacity);
      PotionOfReturnGateHelper returnGateHelper2 = new PotionOfReturnGateHelper(PotionOfReturnGateHelper.GateType.EntryPoint, vector2, opacity);
      if (!Main.gamePaused)
      {
        returnGateHelper1.Update();
        returnGateHelper2.Update();
      }
      this._voidLensData.Clear();
      returnGateHelper1.DrawToDrawData(this._voidLensData, 0);
      returnGateHelper2.DrawToDrawData(this._voidLensData, selectionMode);
      foreach (DrawData drawData in this._voidLensData)
        drawData.Draw(spriteBatch);
      spriteBatch.End();
    }
  }
}
