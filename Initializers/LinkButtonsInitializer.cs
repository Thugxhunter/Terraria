// Decompiled with JetBrains decompiler
// Type: Terraria.Initializers.LinkButtonsInitializer
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace Terraria.Initializers
{
  public class LinkButtonsInitializer
  {
    public static void Load()
    {
      List<TitleLinkButton> titleLinks = Main.TitleLinks;
      titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Discord", "https://discord.gg/terraria", 0));
      titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Instagram", "https://www.instagram.com/terraria_logic/", 1));
      titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Reddit", "https://www.reddit.com/r/Terraria/", 2));
      titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Twitter", "https://twitter.com/Terraria_Logic", 3));
      titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Forums", "https://forums.terraria.org/index.php", 4));
      titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Merch", "https://terraria.org/store", 5));
      titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Wiki", "https://terraria.wiki.gg/", 6));
    }

    private static TitleLinkButton MakeSimpleButton(
      string textKey,
      string linkUrl,
      int horizontalFrameIndex)
    {
      Asset<Texture2D> tex = Main.Assets.Request<Texture2D>("Images/UI/TitleLinkButtons", (AssetRequestMode) 1);
      Rectangle rectangle1 = tex.Frame(7, 2, horizontalFrameIndex);
      Rectangle rectangle2 = tex.Frame(7, 2, horizontalFrameIndex, 1);
      --rectangle1.Width;
      --rectangle1.Height;
      --rectangle2.Width;
      --rectangle2.Height;
      return new TitleLinkButton()
      {
        TooltipTextKey = textKey,
        LinkUrl = linkUrl,
        FrameWehnSelected = new Rectangle?(rectangle2),
        FrameWhenNotSelected = new Rectangle?(rectangle1),
        Image = tex
      };
    }
  }
}
