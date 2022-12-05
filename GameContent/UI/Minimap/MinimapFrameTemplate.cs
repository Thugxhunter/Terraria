// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Minimap.MinimapFrameTemplate
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.UI.Minimap
{
  public class MinimapFrameTemplate
  {
    private string name;
    private Vector2 frameOffset;
    private Vector2 resetPosition;
    private Vector2 zoomInPosition;
    private Vector2 zoomOutPosition;

    public MinimapFrameTemplate(
      string name,
      Vector2 frameOffset,
      Vector2 resetPosition,
      Vector2 zoomInPosition,
      Vector2 zoomOutPosition)
    {
      this.name = name;
      this.frameOffset = frameOffset;
      this.resetPosition = resetPosition;
      this.zoomInPosition = zoomInPosition;
      this.zoomOutPosition = zoomOutPosition;
    }

    public MinimapFrame CreateInstance(AssetRequestMode mode)
    {
      MinimapFrame instance = new MinimapFrame(MinimapFrameTemplate.LoadAsset<Texture2D>("Images\\UI\\Minimap\\" + this.name + "\\MinimapFrame", mode), this.frameOffset);
      instance.NameKey = this.name;
      instance.ConfigKey = this.name;
      instance.SetResetButton(MinimapFrameTemplate.LoadAsset<Texture2D>("Images\\UI\\Minimap\\" + this.name + "\\MinimapButton_Reset", mode), this.resetPosition);
      instance.SetZoomOutButton(MinimapFrameTemplate.LoadAsset<Texture2D>("Images\\UI\\Minimap\\" + this.name + "\\MinimapButton_ZoomOut", mode), this.zoomOutPosition);
      instance.SetZoomInButton(MinimapFrameTemplate.LoadAsset<Texture2D>("Images\\UI\\Minimap\\" + this.name + "\\MinimapButton_ZoomIn", mode), this.zoomInPosition);
      return instance;
    }

    private static Asset<T> LoadAsset<T>(string assetName, AssetRequestMode mode) where T : class => Main.Assets.Request<T>(assetName, mode);
  }
}
