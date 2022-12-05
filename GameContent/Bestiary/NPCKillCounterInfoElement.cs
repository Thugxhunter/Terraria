// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Bestiary.NPCKillCounterInfoElement
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
  public class NPCKillCounterInfoElement : IBestiaryInfoElement
  {
    private NPC _instance;

    public NPCKillCounterInfoElement(int npcNetId)
    {
      this._instance = new NPC();
      this._instance.SetDefaults(npcNetId);
    }

    public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
    {
      int? killCount = this.GetKillCount();
      if (!killCount.HasValue || killCount.Value < 1)
        return (UIElement) null;
      UIElement uiElement = new UIElement();
      uiElement.Width = new StyleDimension(0.0f, 1f);
      uiElement.Height = new StyleDimension(109f, 0.0f);
      int num1 = !killCount.HasValue ? 0 : (killCount.Value > 0 ? 1 : 0);
      int pixels1 = 0;
      int pixels2 = 30;
      int num2 = pixels2;
      string text = killCount.Value.ToString();
      int length = text.Length;
      Math.Max(0, 8 * text.Length - 48);
      int num3 = -3;
      float precent = 1f;
      int num4 = 8;
      UIPanel uiPanel = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel", (AssetRequestMode) 1), (Asset<Texture2D>) null, customBarSize: 7);
      uiPanel.Width = new StyleDimension((float) (num3 - 8), precent);
      uiPanel.Height = new StyleDimension((float) pixels2, 0.0f);
      uiPanel.BackgroundColor = new Color(43, 56, 101);
      uiPanel.BorderColor = Color.Transparent;
      uiPanel.Top = new StyleDimension((float) pixels1, 0.0f);
      uiPanel.Left = new StyleDimension((float) -num4, 0.0f);
      uiPanel.HAlign = 1f;
      UIElement element1 = (UIElement) uiPanel;
      element1.SetPadding(0.0f);
      element1.PaddingRight = 5f;
      uiElement.Append(element1);
      element1.OnUpdate += new UIElement.ElementEvent(this.ShowDescription);
      float textScale = 0.85f;
      UIText uiText = new UIText(text, textScale);
      uiText.HAlign = 1f;
      uiText.VAlign = 0.5f;
      uiText.Left = new StyleDimension(-3f, 0.0f);
      uiText.Top = new StyleDimension(0.0f, 0.0f);
      UIText element2 = uiText;
      Item obj = new Item();
      obj.SetDefaults(321);
      obj.scale = 0.8f;
      UIItemIcon uiItemIcon = new UIItemIcon(obj, false);
      uiItemIcon.IgnoresMouseInteraction = true;
      uiItemIcon.HAlign = 0.0f;
      uiItemIcon.Left = new StyleDimension(-1f, 0.0f);
      UIItemIcon element3 = uiItemIcon;
      uiElement.Height.Pixels = (float) num2;
      element1.Append((UIElement) element3);
      element1.Append((UIElement) element2);
      return uiElement;
    }

    private void ShowDescription(UIElement element)
    {
      if (!element.IsMouseHovering)
        return;
      Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Slain"));
    }

    private int? GetKillCount() => new int?(Main.BestiaryTracker.Kills.GetKillCount(this._instance));
  }
}
