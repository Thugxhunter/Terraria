// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.CustomCurrencyManager
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.ID;

namespace Terraria.GameContent.UI
{
  public class CustomCurrencyManager
  {
    private static int _nextCurrencyIndex;
    private static Dictionary<int, CustomCurrencySystem> _currencies = new Dictionary<int, CustomCurrencySystem>();

    public static void Initialize()
    {
      CustomCurrencyManager._nextCurrencyIndex = 0;
      CustomCurrencyID.DefenderMedals = CustomCurrencyManager.RegisterCurrency((CustomCurrencySystem) new CustomCurrencySingleCoin(3817, 999L));
    }

    public static int RegisterCurrency(CustomCurrencySystem collection)
    {
      int nextCurrencyIndex = CustomCurrencyManager._nextCurrencyIndex;
      ++CustomCurrencyManager._nextCurrencyIndex;
      CustomCurrencyManager._currencies[nextCurrencyIndex] = collection;
      return nextCurrencyIndex;
    }

    public static void DrawSavings(
      SpriteBatch sb,
      int currencyIndex,
      float shopx,
      float shopy,
      bool horizontal = false)
    {
      CustomCurrencySystem currency = CustomCurrencyManager._currencies[currencyIndex];
      Player player = Main.player[Main.myPlayer];
      bool overFlowing;
      long num1 = currency.CountCurrency(out overFlowing, player.bank.item);
      long num2 = currency.CountCurrency(out overFlowing, player.bank2.item);
      long num3 = currency.CountCurrency(out overFlowing, player.bank3.item);
      long num4 = currency.CountCurrency(out overFlowing, player.bank4.item);
      long totalCoins = currency.CombineStacks(out overFlowing, num1, num2, num3, num4);
      if (totalCoins <= 0L)
        return;
      Texture2D itemTexture1;
      Rectangle itemFrame1;
      Main.GetItemDrawFrame(4076, out itemTexture1, out itemFrame1);
      Texture2D itemTexture2;
      Rectangle itemFrame2;
      Main.GetItemDrawFrame(3813, out itemTexture2, out itemFrame2);
      Texture2D itemTexture3;
      Rectangle itemFrame3;
      Main.GetItemDrawFrame(346, out itemTexture3, out itemFrame3);
      Texture2D itemTexture4;
      Rectangle itemFrame4;
      Main.GetItemDrawFrame(87, out itemTexture4, out itemFrame4);
      if (num4 > 0L)
        sb.Draw(itemTexture1, Utils.CenteredRectangle(new Vector2(shopx + 96f, shopy + 50f), itemFrame1.Size() * 0.65f), new Rectangle?(), Color.White);
      if (num3 > 0L)
        sb.Draw(itemTexture2, Utils.CenteredRectangle(new Vector2(shopx + 80f, shopy + 50f), itemFrame2.Size() * 0.65f), new Rectangle?(), Color.White);
      if (num2 > 0L)
        sb.Draw(itemTexture3, Utils.CenteredRectangle(new Vector2(shopx + 80f, shopy + 50f), itemFrame3.Size() * 0.65f), new Rectangle?(), Color.White);
      if (num1 > 0L)
        sb.Draw(itemTexture4, Utils.CenteredRectangle(new Vector2(shopx + 70f, shopy + 60f), itemFrame4.Size() * 0.65f), new Rectangle?(), Color.White);
      Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, Lang.inter[66].Value, shopx, shopy + 40f, Color.White * ((float) Main.mouseTextColor / (float) byte.MaxValue), Color.Black, Vector2.Zero);
      currency.DrawSavingsMoney(sb, Lang.inter[66].Value, shopx, shopy, totalCoins, horizontal);
    }

    public static void GetPriceText(
      int currencyIndex,
      string[] lines,
      ref int currentLine,
      long price)
    {
      CustomCurrencyManager._currencies[currencyIndex].GetPriceText(lines, ref currentLine, price);
    }

    public static bool BuyItem(Player player, long price, int currencyIndex)
    {
      CustomCurrencySystem currency = CustomCurrencyManager._currencies[currencyIndex];
      bool overFlowing;
      long num1 = currency.CountCurrency(out overFlowing, player.inventory, 58, 57, 56, 55, 54);
      long num2 = currency.CountCurrency(out overFlowing, player.bank.item);
      long num3 = currency.CountCurrency(out overFlowing, player.bank2.item);
      long num4 = currency.CountCurrency(out overFlowing, player.bank3.item);
      long num5 = currency.CountCurrency(out overFlowing, player.bank4.item);
      if (currency.CombineStacks(out overFlowing, num1, num2, num3, num4, num5) < price)
        return false;
      List<Item[]> objArrayList = new List<Item[]>();
      Dictionary<int, List<int>> slotsToIgnore = new Dictionary<int, List<int>>();
      List<Point> pointList1 = new List<Point>();
      List<Point> slotCoins = new List<Point>();
      List<Point> pointList2 = new List<Point>();
      List<Point> pointList3 = new List<Point>();
      List<Point> pointList4 = new List<Point>();
      List<Point> pointList5 = new List<Point>();
      objArrayList.Add(player.inventory);
      objArrayList.Add(player.bank.item);
      objArrayList.Add(player.bank2.item);
      objArrayList.Add(player.bank3.item);
      objArrayList.Add(player.bank4.item);
      for (int key = 0; key < objArrayList.Count; ++key)
        slotsToIgnore[key] = new List<int>();
      slotsToIgnore[0] = new List<int>()
      {
        58,
        57,
        56,
        55,
        54
      };
      for (int index = 0; index < objArrayList.Count; ++index)
      {
        for (int y = 0; y < objArrayList[index].Length; ++y)
        {
          if (!slotsToIgnore[index].Contains(y) && currency.Accepts(objArrayList[index][y]))
            slotCoins.Add(new Point(index, y));
        }
      }
      CustomCurrencyManager.FindEmptySlots(objArrayList, slotsToIgnore, pointList1, 0);
      CustomCurrencyManager.FindEmptySlots(objArrayList, slotsToIgnore, pointList2, 1);
      CustomCurrencyManager.FindEmptySlots(objArrayList, slotsToIgnore, pointList3, 2);
      CustomCurrencyManager.FindEmptySlots(objArrayList, slotsToIgnore, pointList4, 3);
      CustomCurrencyManager.FindEmptySlots(objArrayList, slotsToIgnore, pointList5, 4);
      return currency.TryPurchasing(price, objArrayList, slotCoins, pointList1, pointList2, pointList3, pointList4, pointList5);
    }

    private static void FindEmptySlots(
      List<Item[]> inventories,
      Dictionary<int, List<int>> slotsToIgnore,
      List<Point> emptySlots,
      int currentInventoryIndex)
    {
      for (int y = inventories[currentInventoryIndex].Length - 1; y >= 0; --y)
      {
        if (!slotsToIgnore[currentInventoryIndex].Contains(y) && (inventories[currentInventoryIndex][y].type == 0 || inventories[currentInventoryIndex][y].stack == 0))
          emptySlots.Add(new Point(currentInventoryIndex, y));
      }
    }

    public static bool IsCustomCurrency(Item item)
    {
      foreach (KeyValuePair<int, CustomCurrencySystem> currency in CustomCurrencyManager._currencies)
      {
        if (currency.Value.Accepts(item))
          return true;
      }
      return false;
    }

    public static void GetPrices(Item item, out long calcForSelling, out long calcForBuying) => CustomCurrencyManager._currencies[item.shopSpecialCurrency].GetItemExpectedPrice(item, out calcForSelling, out calcForBuying);
  }
}
