// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Bestiary.NPCStatsReportInfoElement
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
  public class NPCStatsReportInfoElement : IBestiaryInfoElement, IUpdateBeforeSorting
  {
    public int NpcId;
    public int Damage;
    public int LifeMax;
    public float MonetaryValue;
    public int Defense;
    public float KnockbackResist;
    private NPC _instance;

    public NPCStatsReportInfoElement(int npcNetId)
    {
      this.NpcId = npcNetId;
      this._instance = new NPC();
      this.RefreshStats(Main.GameModeInfo, this._instance);
    }

    public event NPCStatsReportInfoElement.StatAdjustmentStep OnRefreshStats;

    public void UpdateBeforeSorting() => this.RefreshStats(Main.GameModeInfo, this._instance);

    private void RefreshStats(GameModeData gameModeFound, NPC instance)
    {
      instance.SetDefaults(this.NpcId);
      this.Damage = instance.damage;
      this.LifeMax = instance.lifeMax;
      this.MonetaryValue = instance.value;
      this.Defense = instance.defense;
      this.KnockbackResist = instance.knockBackResist;
      if (this.OnRefreshStats == null)
        return;
      this.OnRefreshStats(this);
    }

    public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
    {
      if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
        return (UIElement) null;
      this.RefreshStats(Main.GameModeInfo, this._instance);
      UIElement uiElement = new UIElement()
      {
        Width = new StyleDimension(0.0f, 1f),
        Height = new StyleDimension(109f, 0.0f)
      };
      int num1 = 99;
      int num2 = 35;
      int pixels1 = 3;
      int pixels2 = 0;
      UIImage uiImage1 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_HP", (AssetRequestMode) 1));
      uiImage1.Top = new StyleDimension((float) pixels2, 0.0f);
      uiImage1.Left = new StyleDimension((float) pixels1, 0.0f);
      UIImage element1 = uiImage1;
      UIImage uiImage2 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Attack", (AssetRequestMode) 1));
      uiImage2.Top = new StyleDimension((float) (pixels2 + num2), 0.0f);
      uiImage2.Left = new StyleDimension((float) pixels1, 0.0f);
      UIImage element2 = uiImage2;
      UIImage uiImage3 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Defense", (AssetRequestMode) 1));
      uiImage3.Top = new StyleDimension((float) (pixels2 + num2), 0.0f);
      uiImage3.Left = new StyleDimension((float) (pixels1 + num1), 0.0f);
      UIImage element3 = uiImage3;
      UIImage uiImage4 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Knockback", (AssetRequestMode) 1));
      uiImage4.Top = new StyleDimension((float) pixels2, 0.0f);
      uiImage4.Left = new StyleDimension((float) (pixels1 + num1), 0.0f);
      UIImage element4 = uiImage4;
      uiElement.Append((UIElement) element1);
      uiElement.Append((UIElement) element2);
      uiElement.Append((UIElement) element3);
      uiElement.Append((UIElement) element4);
      int pixels3 = -10;
      int pixels4 = 0;
      int monetaryValue = (int) this.MonetaryValue;
      string text1 = Utils.Clamp<int>(monetaryValue / 1000000, 0, 999).ToString();
      string text2 = Utils.Clamp<int>(monetaryValue % 1000000 / 10000, 0, 99).ToString();
      string text3 = Utils.Clamp<int>(monetaryValue % 10000 / 100, 0, 99).ToString();
      string text4 = Utils.Clamp<int>(monetaryValue % 100 / 1, 0, 99).ToString();
      if (monetaryValue / 1000000 < 1)
        text1 = "-";
      if (monetaryValue / 10000 < 1)
        text2 = "-";
      if (monetaryValue / 100 < 1)
        text3 = "-";
      if (monetaryValue < 1)
        text4 = "-";
      string text5 = this.LifeMax.ToString();
      string text6 = this.Damage.ToString();
      string text7 = this.Defense.ToString();
      string text8 = (double) this.KnockbackResist <= 0.800000011920929 ? ((double) this.KnockbackResist <= 0.40000000596046448 ? ((double) this.KnockbackResist <= 0.0 ? Language.GetText("BestiaryInfo.KnockbackNone").Value : Language.GetText("BestiaryInfo.KnockbackLow").Value) : Language.GetText("BestiaryInfo.KnockbackMedium").Value) : Language.GetText("BestiaryInfo.KnockbackHigh").Value;
      if (info.UnlockState < BestiaryEntryUnlockState.CanShowStats_2)
      {
        string str1;
        text4 = str1 = "?";
        text3 = str1;
        text2 = str1;
        text1 = str1;
        string str2;
        text8 = str2 = "???";
        text7 = str2;
        text6 = str2;
        text5 = str2;
      }
      UIText uiText1 = new UIText(text5);
      uiText1.HAlign = 1f;
      uiText1.VAlign = 0.5f;
      uiText1.Left = new StyleDimension((float) pixels3, 0.0f);
      uiText1.Top = new StyleDimension((float) pixels4, 0.0f);
      uiText1.IgnoresMouseInteraction = true;
      UIText element5 = uiText1;
      UIText uiText2 = new UIText(text8);
      uiText2.HAlign = 1f;
      uiText2.VAlign = 0.5f;
      uiText2.Left = new StyleDimension((float) pixels3, 0.0f);
      uiText2.Top = new StyleDimension((float) pixels4, 0.0f);
      uiText2.IgnoresMouseInteraction = true;
      UIText element6 = uiText2;
      UIText uiText3 = new UIText(text6);
      uiText3.HAlign = 1f;
      uiText3.VAlign = 0.5f;
      uiText3.Left = new StyleDimension((float) pixels3, 0.0f);
      uiText3.Top = new StyleDimension((float) pixels4, 0.0f);
      uiText3.IgnoresMouseInteraction = true;
      UIText element7 = uiText3;
      UIText uiText4 = new UIText(text7);
      uiText4.HAlign = 1f;
      uiText4.VAlign = 0.5f;
      uiText4.Left = new StyleDimension((float) pixels3, 0.0f);
      uiText4.Top = new StyleDimension((float) pixels4, 0.0f);
      uiText4.IgnoresMouseInteraction = true;
      UIText element8 = uiText4;
      element1.Append((UIElement) element5);
      element2.Append((UIElement) element7);
      element3.Append((UIElement) element8);
      element4.Append((UIElement) element6);
      int num3 = 66;
      if (monetaryValue > 0)
      {
        UIHorizontalSeparator horizontalSeparator = new UIHorizontalSeparator();
        horizontalSeparator.Width = StyleDimension.FromPixelsAndPercent(0.0f, 1f);
        horizontalSeparator.Color = new Color(89, 116, 213, (int) byte.MaxValue) * 0.9f;
        horizontalSeparator.Left = new StyleDimension(0.0f, 0.0f);
        horizontalSeparator.Top = new StyleDimension((float) (pixels4 + num2 * 2), 0.0f);
        UIHorizontalSeparator element9 = horizontalSeparator;
        uiElement.Append((UIElement) element9);
        int num4 = num3 + 4;
        int pixels5 = pixels1;
        int pixels6 = num4 + 8;
        int num5 = 49;
        UIImage uiImage5 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Platinum", (AssetRequestMode) 1));
        uiImage5.Top = new StyleDimension((float) pixels6, 0.0f);
        uiImage5.Left = new StyleDimension((float) pixels5, 0.0f);
        UIImage element10 = uiImage5;
        UIImage uiImage6 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Gold", (AssetRequestMode) 1));
        uiImage6.Top = new StyleDimension((float) pixels6, 0.0f);
        uiImage6.Left = new StyleDimension((float) (pixels5 + num5), 0.0f);
        UIImage element11 = uiImage6;
        UIImage uiImage7 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Silver", (AssetRequestMode) 1));
        uiImage7.Top = new StyleDimension((float) pixels6, 0.0f);
        uiImage7.Left = new StyleDimension((float) (pixels5 + num5 * 2 + 1), 0.0f);
        UIImage element12 = uiImage7;
        UIImage uiImage8 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Copper", (AssetRequestMode) 1));
        uiImage8.Top = new StyleDimension((float) pixels6, 0.0f);
        uiImage8.Left = new StyleDimension((float) (pixels5 + num5 * 3 + 1), 0.0f);
        UIImage element13 = uiImage8;
        if (text1 != "-")
          uiElement.Append((UIElement) element10);
        if (text2 != "-")
          uiElement.Append((UIElement) element11);
        if (text3 != "-")
          uiElement.Append((UIElement) element12);
        if (text4 != "-")
          uiElement.Append((UIElement) element13);
        int pixels7 = pixels3 + 3;
        float textScale = 0.85f;
        UIText uiText5 = new UIText(text1, textScale);
        uiText5.HAlign = 1f;
        uiText5.VAlign = 0.5f;
        uiText5.Left = new StyleDimension((float) pixels7, 0.0f);
        uiText5.Top = new StyleDimension((float) pixels4, 0.0f);
        UIText element14 = uiText5;
        UIText uiText6 = new UIText(text2, textScale);
        uiText6.HAlign = 1f;
        uiText6.VAlign = 0.5f;
        uiText6.Left = new StyleDimension((float) pixels7, 0.0f);
        uiText6.Top = new StyleDimension((float) pixels4, 0.0f);
        UIText element15 = uiText6;
        UIText uiText7 = new UIText(text3, textScale);
        uiText7.HAlign = 1f;
        uiText7.VAlign = 0.5f;
        uiText7.Left = new StyleDimension((float) pixels7, 0.0f);
        uiText7.Top = new StyleDimension((float) pixels4, 0.0f);
        UIText element16 = uiText7;
        UIText uiText8 = new UIText(text4, textScale);
        uiText8.HAlign = 1f;
        uiText8.VAlign = 0.5f;
        uiText8.Left = new StyleDimension((float) pixels7, 0.0f);
        uiText8.Top = new StyleDimension((float) pixels4, 0.0f);
        UIText element17 = uiText8;
        element10.Append((UIElement) element14);
        element11.Append((UIElement) element15);
        element12.Append((UIElement) element16);
        element13.Append((UIElement) element17);
        num3 = num4 + 34;
      }
      int num6 = num3 + 4;
      uiElement.Height.Pixels = (float) num6;
      element2.OnUpdate += new UIElement.ElementEvent(this.ShowStats_Attack);
      element3.OnUpdate += new UIElement.ElementEvent(this.ShowStats_Defense);
      element1.OnUpdate += new UIElement.ElementEvent(this.ShowStats_Life);
      element4.OnUpdate += new UIElement.ElementEvent(this.ShowStats_Knockback);
      return uiElement;
    }

    private void ShowStats_Attack(UIElement element)
    {
      if (!element.IsMouseHovering)
        return;
      Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Attack"));
    }

    private void ShowStats_Defense(UIElement element)
    {
      if (!element.IsMouseHovering)
        return;
      Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Defense"));
    }

    private void ShowStats_Knockback(UIElement element)
    {
      if (!element.IsMouseHovering)
        return;
      Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Knockback"));
    }

    private void ShowStats_Life(UIElement element)
    {
      if (!element.IsMouseHovering)
        return;
      Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Life"));
    }

    public delegate void StatAdjustmentStep(NPCStatsReportInfoElement element);
  }
}
