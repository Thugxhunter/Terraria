// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.EmotesGroupListItem
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class EmotesGroupListItem : UIElement
  {
    private const int TITLE_HEIGHT = 20;
    private const int SEPARATOR_HEIGHT = 10;
    private const int SIZE_PER_EMOTE = 36;
    private Asset<Texture2D> _tempTex;
    private int _groupIndex;
    private int _maxEmotesPerRow = 10;

    public EmotesGroupListItem(
      LocalizedText groupTitle,
      int groupIndex,
      int maxEmotesPerRow,
      params int[] emotes)
    {
      maxEmotesPerRow = 14;
      this.SetPadding(0.0f);
      this._groupIndex = groupIndex;
      this._maxEmotesPerRow = maxEmotesPerRow;
      this._tempTex = Main.Assets.Request<Texture2D>("Images/UI/ButtonFavoriteInactive", (AssetRequestMode) 1);
      int num1 = emotes.Length / this._maxEmotesPerRow;
      if (emotes.Length % this._maxEmotesPerRow != 0)
        ++num1;
      this.Height.Set((float) (30 + 36 * num1), 0.0f);
      this.Width.Set(0.0f, 1f);
      UIElement element1 = new UIElement()
      {
        Height = StyleDimension.FromPixels(30f),
        Width = StyleDimension.FromPixelsAndPercent(-20f, 1f),
        HAlign = 0.5f
      };
      element1.SetPadding(0.0f);
      this.Append(element1);
      UIHorizontalSeparator horizontalSeparator = new UIHorizontalSeparator();
      horizontalSeparator.Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
      horizontalSeparator.VAlign = 1f;
      horizontalSeparator.HAlign = 0.5f;
      horizontalSeparator.Color = Color.Lerp(Color.White, new Color(63, 65, 151, (int) byte.MaxValue), 0.85f) * 0.9f;
      UIHorizontalSeparator element2 = horizontalSeparator;
      element1.Append((UIElement) element2);
      UIText uiText = new UIText(groupTitle);
      uiText.VAlign = 1f;
      uiText.HAlign = 0.5f;
      uiText.Top = StyleDimension.FromPixels(-6f);
      UIText element3 = uiText;
      element1.Append((UIElement) element3);
      float num2 = 6f;
      for (int id = 0; id < emotes.Length; ++id)
      {
        int emote = emotes[id];
        int num3 = id / this._maxEmotesPerRow;
        int num4 = id % this._maxEmotesPerRow;
        int num5 = emotes.Length % this._maxEmotesPerRow;
        if (emotes.Length / this._maxEmotesPerRow != num3)
          num5 = this._maxEmotesPerRow;
        if (num5 == 0)
          num5 = this._maxEmotesPerRow;
        float num6 = (float) (36.0 * ((double) num5 / 2.0)) - 16f;
        float num7 = -16f;
        EmoteButton emoteButton = new EmoteButton(emote);
        emoteButton.HAlign = 0.0f;
        emoteButton.VAlign = 0.0f;
        emoteButton.Top = StyleDimension.FromPixels((float) (30 + num3 * 36) + num2);
        emoteButton.Left = StyleDimension.FromPixels((float) (36 * num4) - num7);
        EmoteButton element4 = emoteButton;
        this.Append((UIElement) element4);
        element4.SetSnapPoint("Group " + (object) groupIndex, id);
      }
    }

    public override int CompareTo(object obj) => obj is EmotesGroupListItem emotesGroupListItem ? this._groupIndex.CompareTo(emotesGroupListItem._groupIndex) : base.CompareTo(obj);
  }
}
