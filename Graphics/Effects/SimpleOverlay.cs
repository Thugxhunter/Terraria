// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Effects.SimpleOverlay
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics.Effects
{
  public class SimpleOverlay : Overlay
  {
    private Asset<Texture2D> _texture;
    private ScreenShaderData _shader;
    public Vector2 TargetPosition = Vector2.Zero;

    public SimpleOverlay(
      string textureName,
      ScreenShaderData shader,
      EffectPriority priority = EffectPriority.VeryLow,
      RenderLayers layer = RenderLayers.All)
      : base(priority, layer)
    {
      this._texture = Main.Assets.Request<Texture2D>(textureName == null ? "" : textureName, (AssetRequestMode) 1);
      this._shader = shader;
    }

    public SimpleOverlay(
      string textureName,
      string shaderName = "Default",
      EffectPriority priority = EffectPriority.VeryLow,
      RenderLayers layer = RenderLayers.All)
      : base(priority, layer)
    {
      this._texture = Main.Assets.Request<Texture2D>(textureName == null ? "" : textureName, (AssetRequestMode) 1);
      this._shader = new ScreenShaderData(Main.ScreenShaderRef, shaderName);
    }

    public ScreenShaderData GetShader() => this._shader;

    public override void Draw(SpriteBatch spriteBatch)
    {
      this._shader.UseGlobalOpacity(this.Opacity);
      this._shader.UseTargetPosition(this.TargetPosition);
      this._shader.Apply();
      spriteBatch.Draw(this._texture.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Main.ColorOfTheSkies);
    }

    public override void Update(GameTime gameTime) => this._shader.Update(gameTime);

    public override void Activate(Vector2 position, params object[] args)
    {
      this.TargetPosition = position;
      this.Mode = OverlayMode.FadeIn;
    }

    public override void Deactivate(params object[] args) => this.Mode = OverlayMode.FadeOut;

    public override bool IsVisible() => (double) this._shader.CombinedOpacity > 0.0;
  }
}
