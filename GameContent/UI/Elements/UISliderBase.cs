// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UISliderBase
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UISliderBase : UIElement
  {
    internal const int UsageLevel_NotSelected = 0;
    internal const int UsageLevel_SelectedAndLocked = 1;
    internal const int UsageLevel_OtherElementIsLocked = 2;
    internal static UIElement CurrentLockedSlider;
    internal static UIElement CurrentAimedSlider;

    internal int GetUsageLevel()
    {
      int usageLevel = 0;
      if (UISliderBase.CurrentLockedSlider == this)
        usageLevel = 1;
      else if (UISliderBase.CurrentLockedSlider != null)
        usageLevel = 2;
      return usageLevel;
    }

    public static void EscapeElements()
    {
      UISliderBase.CurrentLockedSlider = (UIElement) null;
      UISliderBase.CurrentAimedSlider = (UIElement) null;
    }
  }
}
