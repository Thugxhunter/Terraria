// Decompiled with JetBrains decompiler
// Type: Terraria.Map.MapHelper
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Ionic.Zlib;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Terraria.ID;
using Terraria.IO;
using Terraria.Social;
using Terraria.Utilities;

namespace Terraria.Map
{
  public static class MapHelper
  {
    public const int drawLoopMilliseconds = 5;
    private const int HeaderEmpty = 0;
    private const int HeaderTile = 1;
    private const int HeaderWall = 2;
    private const int HeaderWater = 3;
    private const int HeaderLava = 4;
    private const int HeaderHoney = 5;
    private const int HeaderHeavenAndHell = 6;
    private const int HeaderBackground = 7;
    private const int Header2_ReadHeader3Bit = 1;
    private const int Header2Color1 = 2;
    private const int Header2Color2 = 4;
    private const int Header2Color3 = 8;
    private const int Header2Color4 = 16;
    private const int Header2Color5 = 32;
    private const int Header2ShimmerBit = 64;
    private const int Header2_UnusedBit8 = 128;
    private const int Header3_ReservedForHeader4Bit = 1;
    private const int Header3_UnusudBit2 = 2;
    private const int Header3_UnusudBit3 = 4;
    private const int Header3_UnusudBit4 = 8;
    private const int Header3_UnusudBit5 = 16;
    private const int Header3_UnusudBit6 = 32;
    private const int Header3_UnusudBit7 = 64;
    private const int Header3_UnusudBit8 = 128;
    private const int maxTileOptions = 12;
    private const int maxWallOptions = 2;
    private const int maxLiquidTypes = 4;
    private const int maxSkyGradients = 256;
    private const int maxDirtGradients = 256;
    private const int maxRockGradients = 256;
    public static int maxUpdateTile = 1000;
    public static int numUpdateTile = 0;
    public static short[] updateTileX = new short[MapHelper.maxUpdateTile];
    public static short[] updateTileY = new short[MapHelper.maxUpdateTile];
    private static object IOLock = new object();
    public static int[] tileOptionCounts;
    public static int[] wallOptionCounts;
    public static ushort[] tileLookup;
    public static ushort[] wallLookup;
    private static ushort tilePosition;
    private static ushort wallPosition;
    private static ushort liquidPosition;
    private static ushort skyPosition;
    private static ushort dirtPosition;
    private static ushort rockPosition;
    private static ushort hellPosition;
    private static Color[] colorLookup;
    private static ushort[] snowTypes;
    private static ushort wallRangeStart;
    private static ushort wallRangeEnd;
    public static bool noStatusText = false;

    public static void Initialize()
    {
      Color[][] colorArray1 = new Color[(int) TileID.Count][];
      for (int index = 0; index < (int) TileID.Count; ++index)
        colorArray1[index] = new Color[12];
      colorArray1[656][0] = new Color(21, 124, 212);
      colorArray1[624][0] = new Color(210, 91, 77);
      colorArray1[621][0] = new Color(250, 250, 250);
      colorArray1[622][0] = new Color(235, 235, 249);
      colorArray1[518][0] = new Color(26, 196, 84);
      colorArray1[518][1] = new Color(48, 208, 234);
      colorArray1[518][2] = new Color(135, 196, 26);
      colorArray1[519][0] = new Color(28, 216, 109);
      colorArray1[519][1] = new Color(107, 182, 0);
      colorArray1[519][2] = new Color(75, 184, 230);
      colorArray1[519][3] = new Color(208, 80, 80);
      colorArray1[519][4] = new Color(141, 137, 223);
      colorArray1[519][5] = new Color(182, 175, 130);
      colorArray1[549][0] = new Color(54, 83, 20);
      colorArray1[528][0] = new Color(182, 175, 130);
      colorArray1[529][0] = new Color(99, 150, 8);
      colorArray1[529][1] = new Color(139, 154, 64);
      colorArray1[529][2] = new Color(34, 129, 168);
      colorArray1[529][3] = new Color(180, 82, 82);
      colorArray1[529][4] = new Color(113, 108, 205);
      Color color1 = new Color(151, 107, 75);
      colorArray1[0][0] = color1;
      colorArray1[668][0] = color1;
      colorArray1[5][0] = color1;
      colorArray1[5][1] = new Color(182, 175, 130);
      Color color2 = new Color((int) sbyte.MaxValue, (int) sbyte.MaxValue, (int) sbyte.MaxValue);
      colorArray1[583][0] = color2;
      colorArray1[584][0] = color2;
      colorArray1[585][0] = color2;
      colorArray1[586][0] = color2;
      colorArray1[587][0] = color2;
      colorArray1[588][0] = color2;
      colorArray1[589][0] = color2;
      colorArray1[590][0] = color2;
      colorArray1[595][0] = color1;
      colorArray1[596][0] = color1;
      colorArray1[615][0] = color1;
      colorArray1[616][0] = color1;
      colorArray1[634][0] = new Color(145, 120, 120);
      colorArray1[633][0] = new Color(210, 140, 100);
      colorArray1[637][0] = new Color(200, 120, 75);
      colorArray1[638][0] = new Color(200, 120, 75);
      colorArray1[30][0] = color1;
      colorArray1[191][0] = color1;
      colorArray1[272][0] = new Color(121, 119, 101);
      color1 = new Color(128, 128, 128);
      colorArray1[1][0] = color1;
      colorArray1[38][0] = color1;
      colorArray1[48][0] = color1;
      colorArray1[130][0] = color1;
      colorArray1[138][0] = color1;
      colorArray1[664][0] = color1;
      colorArray1[273][0] = color1;
      colorArray1[283][0] = color1;
      colorArray1[618][0] = color1;
      colorArray1[654][0] = new Color(200, 44, 28);
      colorArray1[2][0] = new Color(28, 216, 94);
      colorArray1[477][0] = new Color(28, 216, 94);
      colorArray1[492][0] = new Color(78, 193, 227);
      color1 = new Color(26, 196, 84);
      colorArray1[3][0] = color1;
      colorArray1[192][0] = color1;
      colorArray1[73][0] = new Color(27, 197, 109);
      colorArray1[52][0] = new Color(23, 177, 76);
      colorArray1[353][0] = new Color(28, 216, 94);
      colorArray1[20][0] = new Color(163, 116, 81);
      colorArray1[6][0] = new Color(140, 101, 80);
      color1 = new Color(150, 67, 22);
      colorArray1[7][0] = color1;
      colorArray1[47][0] = color1;
      colorArray1[284][0] = color1;
      colorArray1[682][0] = color1;
      colorArray1[560][0] = color1;
      color1 = new Color(185, 164, 23);
      colorArray1[8][0] = color1;
      colorArray1[45][0] = color1;
      colorArray1[680][0] = color1;
      colorArray1[560][2] = color1;
      color1 = new Color(185, 194, 195);
      colorArray1[9][0] = color1;
      colorArray1[46][0] = color1;
      colorArray1[681][0] = color1;
      colorArray1[560][1] = color1;
      color1 = new Color(98, 95, 167);
      colorArray1[22][0] = color1;
      colorArray1[140][0] = color1;
      colorArray1[23][0] = new Color(141, 137, 223);
      colorArray1[24][0] = new Color(122, 116, 218);
      colorArray1[636][0] = new Color(122, 116, 218);
      colorArray1[25][0] = new Color(109, 90, 128);
      colorArray1[37][0] = new Color(104, 86, 84);
      colorArray1[39][0] = new Color(181, 62, 59);
      colorArray1[40][0] = new Color(146, 81, 68);
      colorArray1[41][0] = new Color(66, 84, 109);
      colorArray1[677][0] = new Color(66, 84, 109);
      colorArray1[481][0] = new Color(66, 84, 109);
      colorArray1[43][0] = new Color(84, 100, 63);
      colorArray1[678][0] = new Color(84, 100, 63);
      colorArray1[482][0] = new Color(84, 100, 63);
      colorArray1[44][0] = new Color(107, 68, 99);
      colorArray1[679][0] = new Color(107, 68, 99);
      colorArray1[483][0] = new Color(107, 68, 99);
      colorArray1[53][0] = new Color(186, 168, 84);
      color1 = new Color(190, 171, 94);
      colorArray1[151][0] = color1;
      colorArray1[154][0] = color1;
      colorArray1[274][0] = color1;
      colorArray1[328][0] = new Color(200, 246, 254);
      colorArray1[329][0] = new Color(15, 15, 15);
      colorArray1[54][0] = new Color(200, 246, 254);
      colorArray1[56][0] = new Color(43, 40, 84);
      colorArray1[75][0] = new Color(26, 26, 26);
      colorArray1[683][0] = new Color(100, 90, 190);
      colorArray1[57][0] = new Color(68, 68, 76);
      color1 = new Color(142, 66, 66);
      colorArray1[58][0] = color1;
      colorArray1[76][0] = color1;
      colorArray1[684][0] = color1;
      color1 = new Color(92, 68, 73);
      colorArray1[59][0] = color1;
      colorArray1[120][0] = color1;
      colorArray1[60][0] = new Color(143, 215, 29);
      colorArray1[61][0] = new Color(135, 196, 26);
      colorArray1[74][0] = new Color(96, 197, 27);
      colorArray1[62][0] = new Color(121, 176, 24);
      colorArray1[233][0] = new Color(107, 182, 29);
      colorArray1[652][0] = colorArray1[233][0];
      colorArray1[651][0] = colorArray1[233][0];
      colorArray1[63][0] = new Color(110, 140, 182);
      colorArray1[64][0] = new Color(196, 96, 114);
      colorArray1[65][0] = new Color(56, 150, 97);
      colorArray1[66][0] = new Color(160, 118, 58);
      colorArray1[67][0] = new Color(140, 58, 166);
      colorArray1[68][0] = new Color(125, 191, 197);
      colorArray1[566][0] = new Color(233, 180, 90);
      colorArray1[70][0] = new Color(93, (int) sbyte.MaxValue, (int) byte.MaxValue);
      color1 = new Color(182, 175, 130);
      colorArray1[71][0] = color1;
      colorArray1[72][0] = color1;
      colorArray1[190][0] = color1;
      colorArray1[578][0] = new Color(172, 155, 110);
      color1 = new Color(73, 120, 17);
      colorArray1[80][0] = color1;
      colorArray1[484][0] = color1;
      colorArray1[188][0] = color1;
      colorArray1[80][1] = new Color(87, 84, 151);
      colorArray1[80][2] = new Color(34, 129, 168);
      colorArray1[80][3] = new Color(130, 56, 55);
      color1 = new Color(11, 80, 143);
      colorArray1[107][0] = color1;
      colorArray1[121][0] = color1;
      colorArray1[685][0] = color1;
      color1 = new Color(91, 169, 169);
      colorArray1[108][0] = color1;
      colorArray1[122][0] = color1;
      colorArray1[686][0] = color1;
      color1 = new Color(128, 26, 52);
      colorArray1[111][0] = color1;
      colorArray1[150][0] = color1;
      colorArray1[109][0] = new Color(78, 193, 227);
      colorArray1[110][0] = new Color(48, 186, 135);
      colorArray1[113][0] = new Color(48, 208, 234);
      colorArray1[115][0] = new Color(33, 171, 207);
      colorArray1[112][0] = new Color(103, 98, 122);
      color1 = new Color(238, 225, 218);
      colorArray1[116][0] = color1;
      colorArray1[118][0] = color1;
      colorArray1[117][0] = new Color(181, 172, 190);
      colorArray1[119][0] = new Color(107, 92, 108);
      colorArray1[123][0] = new Color(106, 107, 118);
      colorArray1[124][0] = new Color(73, 51, 36);
      colorArray1[131][0] = new Color(52, 52, 52);
      colorArray1[145][0] = new Color(192, 30, 30);
      colorArray1[146][0] = new Color(43, 192, 30);
      color1 = new Color(211, 236, 241);
      colorArray1[147][0] = color1;
      colorArray1[148][0] = color1;
      colorArray1[152][0] = new Color(128, 133, 184);
      colorArray1[153][0] = new Color(239, 141, 126);
      colorArray1[155][0] = new Color(131, 162, 161);
      colorArray1[156][0] = new Color(170, 171, 157);
      colorArray1[157][0] = new Color(104, 100, 126);
      color1 = new Color(145, 81, 85);
      colorArray1[158][0] = color1;
      colorArray1[232][0] = color1;
      colorArray1[575][0] = new Color(125, 61, 65);
      colorArray1[159][0] = new Color(148, 133, 98);
      colorArray1[161][0] = new Color(144, 195, 232);
      colorArray1[162][0] = new Color(184, 219, 240);
      colorArray1[163][0] = new Color(174, 145, 214);
      colorArray1[164][0] = new Color(218, 182, 204);
      colorArray1[170][0] = new Color(27, 109, 69);
      colorArray1[171][0] = new Color(33, 135, 85);
      color1 = new Color(129, 125, 93);
      colorArray1[166][0] = color1;
      colorArray1[175][0] = color1;
      colorArray1[167][0] = new Color(62, 82, 114);
      color1 = new Color(132, 157, (int) sbyte.MaxValue);
      colorArray1[168][0] = color1;
      colorArray1[176][0] = color1;
      color1 = new Color(152, 171, 198);
      colorArray1[169][0] = color1;
      colorArray1[177][0] = color1;
      colorArray1[179][0] = new Color(49, 134, 114);
      colorArray1[180][0] = new Color(126, 134, 49);
      colorArray1[181][0] = new Color(134, 59, 49);
      colorArray1[182][0] = new Color(43, 86, 140);
      colorArray1[183][0] = new Color(121, 49, 134);
      colorArray1[381][0] = new Color(254, 121, 2);
      colorArray1[687][0] = new Color(254, 121, 2);
      colorArray1[534][0] = new Color(114, 254, 2);
      colorArray1[689][0] = new Color(114, 254, 2);
      colorArray1[536][0] = new Color(0, 197, 208);
      colorArray1[690][0] = new Color(0, 197, 208);
      colorArray1[539][0] = new Color(208, 0, 126);
      colorArray1[688][0] = new Color(208, 0, 126);
      colorArray1[625][0] = new Color(220, 12, 237);
      colorArray1[691][0] = new Color(220, 12, 237);
      colorArray1[627][0] = new Color((int) byte.MaxValue, 76, 76);
      colorArray1[627][1] = new Color((int) byte.MaxValue, 195, 76);
      colorArray1[627][2] = new Color(195, (int) byte.MaxValue, 76);
      colorArray1[627][3] = new Color(76, (int) byte.MaxValue, 76);
      colorArray1[627][4] = new Color(76, (int) byte.MaxValue, 195);
      colorArray1[627][5] = new Color(76, 195, (int) byte.MaxValue);
      colorArray1[627][6] = new Color(77, 76, (int) byte.MaxValue);
      colorArray1[627][7] = new Color(196, 76, (int) byte.MaxValue);
      colorArray1[627][8] = new Color((int) byte.MaxValue, 76, 195);
      colorArray1[512][0] = new Color(49, 134, 114);
      colorArray1[513][0] = new Color(126, 134, 49);
      colorArray1[514][0] = new Color(134, 59, 49);
      colorArray1[515][0] = new Color(43, 86, 140);
      colorArray1[516][0] = new Color(121, 49, 134);
      colorArray1[517][0] = new Color(254, 121, 2);
      colorArray1[535][0] = new Color(114, 254, 2);
      colorArray1[537][0] = new Color(0, 197, 208);
      colorArray1[540][0] = new Color(208, 0, 126);
      colorArray1[626][0] = new Color(220, 12, 237);
      for (int index = 0; index < colorArray1[628].Length; ++index)
        colorArray1[628][index] = colorArray1[627][index];
      for (int index = 0; index < colorArray1[692].Length; ++index)
        colorArray1[692][index] = colorArray1[627][index];
      for (int index = 0; index < colorArray1[160].Length; ++index)
        colorArray1[160][index] = colorArray1[627][index];
      colorArray1[184][0] = new Color(29, 106, 88);
      colorArray1[184][1] = new Color(94, 100, 36);
      colorArray1[184][2] = new Color(96, 44, 40);
      colorArray1[184][3] = new Color(34, 63, 102);
      colorArray1[184][4] = new Color(79, 35, 95);
      colorArray1[184][5] = new Color(253, 62, 3);
      colorArray1[184][6] = new Color(22, 123, 62);
      colorArray1[184][7] = new Color(0, 106, 148);
      colorArray1[184][8] = new Color(148, 0, 132);
      colorArray1[184][9] = new Color(122, 24, 168);
      colorArray1[184][10] = new Color(220, 20, 20);
      colorArray1[189][0] = new Color(223, (int) byte.MaxValue, (int) byte.MaxValue);
      colorArray1[193][0] = new Color(56, 121, (int) byte.MaxValue);
      colorArray1[194][0] = new Color(157, 157, 107);
      colorArray1[195][0] = new Color(134, 22, 34);
      colorArray1[196][0] = new Color(147, 144, 178);
      colorArray1[197][0] = new Color(97, 200, 225);
      colorArray1[198][0] = new Color(62, 61, 52);
      colorArray1[199][0] = new Color(208, 80, 80);
      colorArray1[201][0] = new Color(203, 61, 64);
      colorArray1[205][0] = new Color(186, 50, 52);
      colorArray1[200][0] = new Color(216, 152, 144);
      colorArray1[202][0] = new Color(213, 178, 28);
      colorArray1[203][0] = new Color(128, 44, 45);
      colorArray1[204][0] = new Color(125, 55, 65);
      colorArray1[206][0] = new Color(124, 175, 201);
      colorArray1[208][0] = new Color(88, 105, 118);
      colorArray1[211][0] = new Color(191, 233, 115);
      colorArray1[213][0] = new Color(137, 120, 67);
      colorArray1[214][0] = new Color(103, 103, 103);
      colorArray1[221][0] = new Color(239, 90, 50);
      colorArray1[222][0] = new Color(231, 96, 228);
      colorArray1[223][0] = new Color(57, 85, 101);
      colorArray1[224][0] = new Color(107, 132, 139);
      colorArray1[225][0] = new Color(227, 125, 22);
      colorArray1[226][0] = new Color(141, 56, 0);
      colorArray1[229][0] = new Color((int) byte.MaxValue, 156, 12);
      colorArray1[659][0] = new Color(247, 228, 254);
      colorArray1[230][0] = new Color(131, 79, 13);
      colorArray1[234][0] = new Color(53, 44, 41);
      colorArray1[235][0] = new Color(214, 184, 46);
      colorArray1[236][0] = new Color(149, 232, 87);
      colorArray1[237][0] = new Color((int) byte.MaxValue, 241, 51);
      colorArray1[238][0] = new Color(225, 128, 206);
      colorArray1[655][0] = new Color(225, 128, 206);
      colorArray1[243][0] = new Color(198, 196, 170);
      colorArray1[248][0] = new Color(219, 71, 38);
      colorArray1[249][0] = new Color(235, 38, 231);
      colorArray1[250][0] = new Color(86, 85, 92);
      colorArray1[251][0] = new Color(235, 150, 23);
      colorArray1[252][0] = new Color(153, 131, 44);
      colorArray1[253][0] = new Color(57, 48, 97);
      colorArray1[254][0] = new Color(248, 158, 92);
      colorArray1[(int) byte.MaxValue][0] = new Color(107, 49, 154);
      colorArray1[256][0] = new Color(154, 148, 49);
      colorArray1[257][0] = new Color(49, 49, 154);
      colorArray1[258][0] = new Color(49, 154, 68);
      colorArray1[259][0] = new Color(154, 49, 77);
      colorArray1[260][0] = new Color(85, 89, 118);
      colorArray1[261][0] = new Color(154, 83, 49);
      colorArray1[262][0] = new Color(221, 79, (int) byte.MaxValue);
      colorArray1[263][0] = new Color(250, (int) byte.MaxValue, 79);
      colorArray1[264][0] = new Color(79, 102, (int) byte.MaxValue);
      colorArray1[265][0] = new Color(79, (int) byte.MaxValue, 89);
      colorArray1[266][0] = new Color((int) byte.MaxValue, 79, 79);
      colorArray1[267][0] = new Color(240, 240, 247);
      colorArray1[268][0] = new Color((int) byte.MaxValue, 145, 79);
      colorArray1[287][0] = new Color(79, 128, 17);
      color1 = new Color(122, 217, 232);
      colorArray1[275][0] = color1;
      colorArray1[276][0] = color1;
      colorArray1[277][0] = color1;
      colorArray1[278][0] = color1;
      colorArray1[279][0] = color1;
      colorArray1[280][0] = color1;
      colorArray1[281][0] = color1;
      colorArray1[282][0] = color1;
      colorArray1[285][0] = color1;
      colorArray1[286][0] = color1;
      colorArray1[288][0] = color1;
      colorArray1[289][0] = color1;
      colorArray1[290][0] = color1;
      colorArray1[291][0] = color1;
      colorArray1[292][0] = color1;
      colorArray1[293][0] = color1;
      colorArray1[294][0] = color1;
      colorArray1[295][0] = color1;
      colorArray1[296][0] = color1;
      colorArray1[297][0] = color1;
      colorArray1[298][0] = color1;
      colorArray1[299][0] = color1;
      colorArray1[309][0] = color1;
      colorArray1[310][0] = color1;
      colorArray1[413][0] = color1;
      colorArray1[339][0] = color1;
      colorArray1[542][0] = color1;
      colorArray1[632][0] = color1;
      colorArray1[640][0] = color1;
      colorArray1[643][0] = color1;
      colorArray1[644][0] = color1;
      colorArray1[645][0] = color1;
      colorArray1[358][0] = color1;
      colorArray1[359][0] = color1;
      colorArray1[360][0] = color1;
      colorArray1[361][0] = color1;
      colorArray1[362][0] = color1;
      colorArray1[363][0] = color1;
      colorArray1[364][0] = color1;
      colorArray1[391][0] = color1;
      colorArray1[392][0] = color1;
      colorArray1[393][0] = color1;
      colorArray1[394][0] = color1;
      colorArray1[414][0] = color1;
      colorArray1[505][0] = color1;
      colorArray1[543][0] = color1;
      colorArray1[598][0] = color1;
      colorArray1[521][0] = color1;
      colorArray1[522][0] = color1;
      colorArray1[523][0] = color1;
      colorArray1[524][0] = color1;
      colorArray1[525][0] = color1;
      colorArray1[526][0] = color1;
      colorArray1[527][0] = color1;
      colorArray1[532][0] = color1;
      colorArray1[533][0] = color1;
      colorArray1[538][0] = color1;
      colorArray1[544][0] = color1;
      colorArray1[629][0] = color1;
      colorArray1[550][0] = color1;
      colorArray1[551][0] = color1;
      colorArray1[553][0] = color1;
      colorArray1[554][0] = color1;
      colorArray1[555][0] = color1;
      colorArray1[556][0] = color1;
      colorArray1[558][0] = color1;
      colorArray1[559][0] = color1;
      colorArray1[580][0] = color1;
      colorArray1[582][0] = color1;
      colorArray1[599][0] = color1;
      colorArray1[600][0] = color1;
      colorArray1[601][0] = color1;
      colorArray1[602][0] = color1;
      colorArray1[603][0] = color1;
      colorArray1[604][0] = color1;
      colorArray1[605][0] = color1;
      colorArray1[606][0] = color1;
      colorArray1[607][0] = color1;
      colorArray1[608][0] = color1;
      colorArray1[609][0] = color1;
      colorArray1[610][0] = color1;
      colorArray1[611][0] = color1;
      colorArray1[612][0] = color1;
      colorArray1[619][0] = color1;
      colorArray1[620][0] = color1;
      colorArray1[630][0] = new Color(117, 145, 73);
      colorArray1[631][0] = new Color(122, 234, 225);
      colorArray1[552][0] = colorArray1[53][0];
      colorArray1[564][0] = new Color(87, (int) sbyte.MaxValue, 220);
      colorArray1[408][0] = new Color(85, 83, 82);
      colorArray1[409][0] = new Color(85, 83, 82);
      colorArray1[669][0] = new Color(83, 46, 57);
      colorArray1[670][0] = new Color(91, 87, 167);
      colorArray1[671][0] = new Color(23, 33, 81);
      colorArray1[672][0] = new Color(53, 133, 103);
      colorArray1[673][0] = new Color(11, 67, 80);
      colorArray1[674][0] = new Color(40, 49, 60);
      colorArray1[675][0] = new Color(21, 13, 77);
      colorArray1[676][0] = new Color(195, 201, 215);
      colorArray1[415][0] = new Color(249, 75, 7);
      colorArray1[416][0] = new Color(0, 160, 170);
      colorArray1[417][0] = new Color(160, 87, 234);
      colorArray1[418][0] = new Color(22, 173, 254);
      colorArray1[489][0] = new Color((int) byte.MaxValue, 29, 136);
      colorArray1[490][0] = new Color(211, 211, 211);
      colorArray1[311][0] = new Color(117, 61, 25);
      colorArray1[312][0] = new Color(204, 93, 73);
      colorArray1[313][0] = new Color(87, 150, 154);
      colorArray1[4][0] = new Color(253, 221, 3);
      colorArray1[4][1] = new Color(253, 221, 3);
      color1 = new Color(253, 221, 3);
      colorArray1[93][0] = color1;
      colorArray1[33][0] = color1;
      colorArray1[174][0] = color1;
      colorArray1[100][0] = color1;
      colorArray1[98][0] = color1;
      colorArray1[173][0] = color1;
      color1 = new Color(119, 105, 79);
      colorArray1[11][0] = color1;
      colorArray1[10][0] = color1;
      colorArray1[593][0] = color1;
      colorArray1[594][0] = color1;
      color1 = new Color(191, 142, 111);
      colorArray1[14][0] = color1;
      colorArray1[469][0] = color1;
      colorArray1[486][0] = color1;
      colorArray1[488][0] = new Color((int) sbyte.MaxValue, 92, 69);
      colorArray1[487][0] = color1;
      colorArray1[487][1] = color1;
      colorArray1[15][0] = color1;
      colorArray1[15][1] = color1;
      colorArray1[497][0] = color1;
      colorArray1[18][0] = color1;
      colorArray1[19][0] = color1;
      colorArray1[19][1] = Color.Black;
      colorArray1[55][0] = color1;
      colorArray1[79][0] = color1;
      colorArray1[86][0] = color1;
      colorArray1[87][0] = color1;
      colorArray1[88][0] = color1;
      colorArray1[89][0] = color1;
      colorArray1[89][1] = color1;
      colorArray1[89][2] = new Color(105, 107, 125);
      colorArray1[94][0] = color1;
      colorArray1[101][0] = color1;
      colorArray1[104][0] = color1;
      colorArray1[106][0] = color1;
      colorArray1[114][0] = color1;
      colorArray1[128][0] = color1;
      colorArray1[139][0] = color1;
      colorArray1[172][0] = color1;
      colorArray1[216][0] = color1;
      colorArray1[269][0] = color1;
      colorArray1[334][0] = color1;
      colorArray1[471][0] = color1;
      colorArray1[470][0] = color1;
      colorArray1[475][0] = color1;
      colorArray1[377][0] = color1;
      colorArray1[380][0] = color1;
      colorArray1[395][0] = color1;
      colorArray1[573][0] = color1;
      colorArray1[12][0] = new Color(174, 24, 69);
      colorArray1[665][0] = new Color(174, 24, 69);
      colorArray1[639][0] = new Color(110, 105, (int) byte.MaxValue);
      colorArray1[13][0] = new Color(133, 213, 247);
      color1 = new Color(144, 148, 144);
      colorArray1[17][0] = color1;
      colorArray1[90][0] = color1;
      colorArray1[96][0] = color1;
      colorArray1[97][0] = color1;
      colorArray1[99][0] = color1;
      colorArray1[132][0] = color1;
      colorArray1[142][0] = color1;
      colorArray1[143][0] = color1;
      colorArray1[144][0] = color1;
      colorArray1[207][0] = color1;
      colorArray1[209][0] = color1;
      colorArray1[212][0] = color1;
      colorArray1[217][0] = color1;
      colorArray1[218][0] = color1;
      colorArray1[219][0] = color1;
      colorArray1[220][0] = color1;
      colorArray1[228][0] = color1;
      colorArray1[300][0] = color1;
      colorArray1[301][0] = color1;
      colorArray1[302][0] = color1;
      colorArray1[303][0] = color1;
      colorArray1[304][0] = color1;
      colorArray1[305][0] = color1;
      colorArray1[306][0] = color1;
      colorArray1[307][0] = color1;
      colorArray1[308][0] = color1;
      colorArray1[567][0] = color1;
      colorArray1[349][0] = new Color(144, 148, 144);
      colorArray1[531][0] = new Color(144, 148, 144);
      colorArray1[105][0] = new Color(144, 148, 144);
      colorArray1[105][1] = new Color(177, 92, 31);
      colorArray1[105][2] = new Color(201, 188, 170);
      colorArray1[137][0] = new Color(144, 148, 144);
      colorArray1[137][1] = new Color(141, 56, 0);
      colorArray1[137][2] = new Color(144, 148, 144);
      colorArray1[16][0] = new Color(140, 130, 116);
      colorArray1[26][0] = new Color(119, 101, 125);
      colorArray1[26][1] = new Color(214, (int) sbyte.MaxValue, 133);
      colorArray1[36][0] = new Color(230, 89, 92);
      colorArray1[28][0] = new Color(151, 79, 80);
      colorArray1[28][1] = new Color(90, 139, 140);
      colorArray1[28][2] = new Color(192, 136, 70);
      colorArray1[28][3] = new Color(203, 185, 151);
      colorArray1[28][4] = new Color(73, 56, 41);
      colorArray1[28][5] = new Color(148, 159, 67);
      colorArray1[28][6] = new Color(138, 172, 67);
      colorArray1[28][7] = new Color(226, 122, 47);
      colorArray1[28][8] = new Color(198, 87, 93);
      for (int index = 0; index < colorArray1[653].Length; ++index)
        colorArray1[653][index] = colorArray1[28][index];
      colorArray1[29][0] = new Color(175, 105, 128);
      colorArray1[51][0] = new Color(192, 202, 203);
      colorArray1[31][0] = new Color(141, 120, 168);
      colorArray1[31][1] = new Color(212, 105, 105);
      colorArray1[32][0] = new Color(151, 135, 183);
      colorArray1[42][0] = new Color(251, 235, (int) sbyte.MaxValue);
      colorArray1[50][0] = new Color(170, 48, 114);
      colorArray1[85][0] = new Color(192, 192, 192);
      colorArray1[69][0] = new Color(190, 150, 92);
      colorArray1[77][0] = new Color(238, 85, 70);
      colorArray1[81][0] = new Color(245, 133, 191);
      colorArray1[78][0] = new Color(121, 110, 97);
      colorArray1[141][0] = new Color(192, 59, 59);
      colorArray1[129][0] = new Color((int) byte.MaxValue, 117, 224);
      colorArray1[129][1] = new Color((int) byte.MaxValue, 117, 224);
      colorArray1[126][0] = new Color(159, 209, 229);
      colorArray1[125][0] = new Color(141, 175, (int) byte.MaxValue);
      colorArray1[103][0] = new Color(141, 98, 77);
      colorArray1[95][0] = new Color((int) byte.MaxValue, 162, 31);
      colorArray1[92][0] = new Color(213, 229, 237);
      colorArray1[91][0] = new Color(13, 88, 130);
      colorArray1[215][0] = new Color(254, 121, 2);
      colorArray1[592][0] = new Color(254, 121, 2);
      colorArray1[316][0] = new Color(157, 176, 226);
      colorArray1[317][0] = new Color(118, 227, 129);
      colorArray1[318][0] = new Color(227, 118, 215);
      colorArray1[319][0] = new Color(96, 68, 48);
      colorArray1[320][0] = new Color(203, 185, 151);
      colorArray1[321][0] = new Color(96, 77, 64);
      colorArray1[574][0] = new Color(76, 57, 44);
      colorArray1[322][0] = new Color(198, 170, 104);
      colorArray1[635][0] = new Color(145, 120, 120);
      colorArray1[149][0] = new Color(220, 50, 50);
      colorArray1[149][1] = new Color(0, 220, 50);
      colorArray1[149][2] = new Color(50, 50, 220);
      colorArray1[133][0] = new Color(231, 53, 56);
      colorArray1[133][1] = new Color(192, 189, 221);
      colorArray1[134][0] = new Color(166, 187, 153);
      colorArray1[134][1] = new Color(241, 129, 249);
      colorArray1[102][0] = new Color(229, 212, 73);
      colorArray1[35][0] = new Color(226, 145, 30);
      colorArray1[34][0] = new Color(235, 166, 135);
      colorArray1[136][0] = new Color(213, 203, 204);
      colorArray1[231][0] = new Color(224, 194, 101);
      colorArray1[239][0] = new Color(224, 194, 101);
      colorArray1[240][0] = new Color(120, 85, 60);
      colorArray1[240][1] = new Color(99, 50, 30);
      colorArray1[240][2] = new Color(153, 153, 117);
      colorArray1[240][3] = new Color(112, 84, 56);
      colorArray1[240][4] = new Color(234, 231, 226);
      colorArray1[241][0] = new Color(77, 74, 72);
      colorArray1[244][0] = new Color(200, 245, 253);
      color1 = new Color(99, 50, 30);
      colorArray1[242][0] = color1;
      colorArray1[245][0] = color1;
      colorArray1[246][0] = color1;
      colorArray1[242][1] = new Color(185, 142, 97);
      colorArray1[247][0] = new Color(140, 150, 150);
      colorArray1[271][0] = new Color(107, 250, (int) byte.MaxValue);
      colorArray1[270][0] = new Color(187, (int) byte.MaxValue, 107);
      colorArray1[581][0] = new Color((int) byte.MaxValue, 150, 150);
      colorArray1[660][0] = new Color((int) byte.MaxValue, 150, 150);
      colorArray1[572][0] = new Color((int) byte.MaxValue, 186, 212);
      colorArray1[572][1] = new Color(209, 201, (int) byte.MaxValue);
      colorArray1[572][2] = new Color(200, 254, (int) byte.MaxValue);
      colorArray1[572][3] = new Color(199, (int) byte.MaxValue, 211);
      colorArray1[572][4] = new Color(180, 209, (int) byte.MaxValue);
      colorArray1[572][5] = new Color((int) byte.MaxValue, 220, 214);
      colorArray1[314][0] = new Color(181, 164, 125);
      colorArray1[324][0] = new Color(228, 213, 173);
      colorArray1[351][0] = new Color(31, 31, 31);
      colorArray1[424][0] = new Color(146, 155, 187);
      colorArray1[429][0] = new Color(220, 220, 220);
      colorArray1[445][0] = new Color(240, 240, 240);
      colorArray1[21][0] = new Color(174, 129, 92);
      colorArray1[21][1] = new Color(233, 207, 94);
      colorArray1[21][2] = new Color(137, 128, 200);
      colorArray1[21][3] = new Color(160, 160, 160);
      colorArray1[21][4] = new Color(106, 210, (int) byte.MaxValue);
      colorArray1[441][0] = colorArray1[21][0];
      colorArray1[441][1] = colorArray1[21][1];
      colorArray1[441][2] = colorArray1[21][2];
      colorArray1[441][3] = colorArray1[21][3];
      colorArray1[441][4] = colorArray1[21][4];
      colorArray1[27][0] = new Color(54, 154, 54);
      colorArray1[27][1] = new Color(226, 196, 49);
      color1 = new Color(246, 197, 26);
      colorArray1[82][0] = color1;
      colorArray1[83][0] = color1;
      colorArray1[84][0] = color1;
      color1 = new Color(76, 150, 216);
      colorArray1[82][1] = color1;
      colorArray1[83][1] = color1;
      colorArray1[84][1] = color1;
      color1 = new Color(185, 214, 42);
      colorArray1[82][2] = color1;
      colorArray1[83][2] = color1;
      colorArray1[84][2] = color1;
      color1 = new Color(167, 203, 37);
      colorArray1[82][3] = color1;
      colorArray1[83][3] = color1;
      colorArray1[84][3] = color1;
      colorArray1[591][6] = color1;
      color1 = new Color(32, 168, 117);
      colorArray1[82][4] = color1;
      colorArray1[83][4] = color1;
      colorArray1[84][4] = color1;
      color1 = new Color(177, 69, 49);
      colorArray1[82][5] = color1;
      colorArray1[83][5] = color1;
      colorArray1[84][5] = color1;
      color1 = new Color(40, 152, 240);
      colorArray1[82][6] = color1;
      colorArray1[83][6] = color1;
      colorArray1[84][6] = color1;
      colorArray1[591][1] = new Color(246, 197, 26);
      colorArray1[591][2] = new Color(76, 150, 216);
      colorArray1[591][3] = new Color(32, 168, 117);
      colorArray1[591][4] = new Color(40, 152, 240);
      colorArray1[591][5] = new Color(114, 81, 56);
      colorArray1[591][6] = new Color(141, 137, 223);
      colorArray1[591][7] = new Color(208, 80, 80);
      colorArray1[591][8] = new Color(177, 69, 49);
      colorArray1[165][0] = new Color(115, 173, 229);
      colorArray1[165][1] = new Color(100, 100, 100);
      colorArray1[165][2] = new Color(152, 152, 152);
      colorArray1[165][3] = new Color(227, 125, 22);
      colorArray1[178][0] = new Color(208, 94, 201);
      colorArray1[178][1] = new Color(233, 146, 69);
      colorArray1[178][2] = new Color(71, 146, 251);
      colorArray1[178][3] = new Color(60, 226, 133);
      colorArray1[178][4] = new Color(250, 30, 71);
      colorArray1[178][5] = new Color(166, 176, 204);
      colorArray1[178][6] = new Color((int) byte.MaxValue, 217, 120);
      color1 = new Color(99, 99, 99);
      colorArray1[185][0] = color1;
      colorArray1[186][0] = color1;
      colorArray1[187][0] = color1;
      colorArray1[565][0] = color1;
      colorArray1[579][0] = color1;
      color1 = new Color(114, 81, 56);
      colorArray1[185][1] = color1;
      colorArray1[186][1] = color1;
      colorArray1[187][1] = color1;
      colorArray1[591][0] = color1;
      color1 = new Color(133, 133, 101);
      colorArray1[185][2] = color1;
      colorArray1[186][2] = color1;
      colorArray1[187][2] = color1;
      color1 = new Color(151, 200, 211);
      colorArray1[185][3] = color1;
      colorArray1[186][3] = color1;
      colorArray1[187][3] = color1;
      color1 = new Color(177, 183, 161);
      colorArray1[185][4] = color1;
      colorArray1[186][4] = color1;
      colorArray1[187][4] = color1;
      color1 = new Color(134, 114, 38);
      colorArray1[185][5] = color1;
      colorArray1[186][5] = color1;
      colorArray1[187][5] = color1;
      color1 = new Color(82, 62, 66);
      colorArray1[185][6] = color1;
      colorArray1[186][6] = color1;
      colorArray1[187][6] = color1;
      color1 = new Color(143, 117, 121);
      colorArray1[185][7] = color1;
      colorArray1[186][7] = color1;
      colorArray1[187][7] = color1;
      color1 = new Color(177, 92, 31);
      colorArray1[185][8] = color1;
      colorArray1[186][8] = color1;
      colorArray1[187][8] = color1;
      color1 = new Color(85, 73, 87);
      colorArray1[185][9] = color1;
      colorArray1[186][9] = color1;
      colorArray1[187][9] = color1;
      color1 = new Color(26, 196, 84);
      colorArray1[185][10] = color1;
      colorArray1[186][10] = color1;
      colorArray1[187][10] = color1;
      Color[] colorArray2 = colorArray1[647];
      for (int index = 0; index < colorArray2.Length; ++index)
        colorArray2[index] = colorArray1[186][index];
      Color[] colorArray3 = colorArray1[648];
      for (int index = 0; index < colorArray3.Length; ++index)
        colorArray3[index] = colorArray1[187][index];
      Color[] colorArray4 = colorArray1[650];
      for (int index = 0; index < colorArray4.Length; ++index)
        colorArray4[index] = colorArray1[185][index];
      Color[] colorArray5 = colorArray1[649];
      for (int index = 0; index < colorArray5.Length; ++index)
        colorArray5[index] = colorArray1[185][index];
      colorArray1[227][0] = new Color(74, 197, 155);
      colorArray1[227][1] = new Color(54, 153, 88);
      colorArray1[227][2] = new Color(63, 126, 207);
      colorArray1[227][3] = new Color(240, 180, 4);
      colorArray1[227][4] = new Color(45, 68, 168);
      colorArray1[227][5] = new Color(61, 92, 0);
      colorArray1[227][6] = new Color(216, 112, 152);
      colorArray1[227][7] = new Color(200, 40, 24);
      colorArray1[227][8] = new Color(113, 45, 133);
      colorArray1[227][9] = new Color(235, 137, 2);
      colorArray1[227][10] = new Color(41, 152, 135);
      colorArray1[227][11] = new Color(198, 19, 78);
      colorArray1[373][0] = new Color(9, 61, 191);
      colorArray1[374][0] = new Color(253, 32, 3);
      colorArray1[375][0] = new Color((int) byte.MaxValue, 156, 12);
      colorArray1[461][0] = new Color(212, 192, 100);
      colorArray1[461][1] = new Color(137, 132, 156);
      colorArray1[461][2] = new Color(148, 122, 112);
      colorArray1[461][3] = new Color(221, 201, 206);
      colorArray1[323][0] = new Color(182, 141, 86);
      colorArray1[325][0] = new Color(129, 125, 93);
      colorArray1[326][0] = new Color(9, 61, 191);
      colorArray1[327][0] = new Color(253, 32, 3);
      colorArray1[507][0] = new Color(5, 5, 5);
      colorArray1[508][0] = new Color(5, 5, 5);
      colorArray1[330][0] = new Color(226, 118, 76);
      colorArray1[331][0] = new Color(161, 172, 173);
      colorArray1[332][0] = new Color(204, 181, 72);
      colorArray1[333][0] = new Color(190, 190, 178);
      colorArray1[335][0] = new Color(217, 174, 137);
      colorArray1[336][0] = new Color(253, 62, 3);
      colorArray1[337][0] = new Color(144, 148, 144);
      colorArray1[338][0] = new Color(85, (int) byte.MaxValue, 160);
      colorArray1[315][0] = new Color(235, 114, 80);
      colorArray1[641][0] = new Color(235, 125, 150);
      colorArray1[340][0] = new Color(96, 248, 2);
      colorArray1[341][0] = new Color(105, 74, 202);
      colorArray1[342][0] = new Color(29, 240, (int) byte.MaxValue);
      colorArray1[343][0] = new Color(254, 202, 80);
      colorArray1[344][0] = new Color(131, 252, 245);
      colorArray1[345][0] = new Color((int) byte.MaxValue, 156, 12);
      colorArray1[346][0] = new Color(149, 212, 89);
      colorArray1[642][0] = new Color(149, 212, 89);
      colorArray1[347][0] = new Color(236, 74, 79);
      colorArray1[348][0] = new Color(44, 26, 233);
      colorArray1[350][0] = new Color(55, 97, 155);
      colorArray1[352][0] = new Color(238, 97, 94);
      colorArray1[354][0] = new Color(141, 107, 89);
      colorArray1[355][0] = new Color(141, 107, 89);
      colorArray1[463][0] = new Color(155, 214, 240);
      colorArray1[491][0] = new Color(60, 20, 160);
      colorArray1[464][0] = new Color(233, 183, 128);
      colorArray1[465][0] = new Color(51, 84, 195);
      colorArray1[466][0] = new Color(205, 153, 73);
      colorArray1[356][0] = new Color(233, 203, 24);
      colorArray1[663][0] = new Color(24, 203, 233);
      colorArray1[357][0] = new Color(168, 178, 204);
      colorArray1[367][0] = new Color(168, 178, 204);
      colorArray1[561][0] = new Color(148, 158, 184);
      colorArray1[365][0] = new Color(146, 136, 205);
      colorArray1[366][0] = new Color(223, 232, 233);
      colorArray1[368][0] = new Color(50, 46, 104);
      colorArray1[369][0] = new Color(50, 46, 104);
      colorArray1[576][0] = new Color(30, 26, 84);
      colorArray1[370][0] = new Color((int) sbyte.MaxValue, 116, 194);
      colorArray1[49][0] = new Color(89, 201, (int) byte.MaxValue);
      colorArray1[372][0] = new Color(252, 128, 201);
      colorArray1[646][0] = new Color(108, 133, 140);
      colorArray1[371][0] = new Color(249, 101, 189);
      colorArray1[376][0] = new Color(160, 120, 92);
      colorArray1[378][0] = new Color(160, 120, 100);
      colorArray1[379][0] = new Color(251, 209, 240);
      colorArray1[382][0] = new Color(28, 216, 94);
      colorArray1[383][0] = new Color(221, 136, 144);
      colorArray1[384][0] = new Color(131, 206, 12);
      colorArray1[385][0] = new Color(87, 21, 144);
      colorArray1[386][0] = new Color((int) sbyte.MaxValue, 92, 69);
      colorArray1[387][0] = new Color((int) sbyte.MaxValue, 92, 69);
      colorArray1[388][0] = new Color((int) sbyte.MaxValue, 92, 69);
      colorArray1[389][0] = new Color((int) sbyte.MaxValue, 92, 69);
      colorArray1[390][0] = new Color(253, 32, 3);
      colorArray1[397][0] = new Color(212, 192, 100);
      colorArray1[396][0] = new Color(198, 124, 78);
      colorArray1[577][0] = new Color(178, 104, 58);
      colorArray1[398][0] = new Color(100, 82, 126);
      colorArray1[399][0] = new Color(77, 76, 66);
      colorArray1[400][0] = new Color(96, 68, 117);
      colorArray1[401][0] = new Color(68, 60, 51);
      colorArray1[402][0] = new Color(174, 168, 186);
      colorArray1[403][0] = new Color(205, 152, 186);
      colorArray1[404][0] = new Color(212, 148, 88);
      colorArray1[405][0] = new Color(140, 140, 140);
      colorArray1[406][0] = new Color(120, 120, 120);
      colorArray1[407][0] = new Color((int) byte.MaxValue, 227, 132);
      colorArray1[411][0] = new Color(227, 46, 46);
      colorArray1[494][0] = new Color(227, 227, 227);
      colorArray1[421][0] = new Color(65, 75, 90);
      colorArray1[422][0] = new Color(65, 75, 90);
      colorArray1[425][0] = new Color(146, 155, 187);
      colorArray1[426][0] = new Color(168, 38, 47);
      colorArray1[430][0] = new Color(39, 168, 96);
      colorArray1[431][0] = new Color(39, 94, 168);
      colorArray1[432][0] = new Color(242, 221, 100);
      colorArray1[433][0] = new Color(224, 100, 242);
      colorArray1[434][0] = new Color(197, 193, 216);
      colorArray1[427][0] = new Color(183, 53, 62);
      colorArray1[435][0] = new Color(54, 183, 111);
      colorArray1[436][0] = new Color(54, 109, 183);
      colorArray1[437][0] = new Color((int) byte.MaxValue, 236, 115);
      colorArray1[438][0] = new Color(239, 115, (int) byte.MaxValue);
      colorArray1[439][0] = new Color(212, 208, 231);
      colorArray1[440][0] = new Color(238, 51, 53);
      colorArray1[440][1] = new Color(13, 107, 216);
      colorArray1[440][2] = new Color(33, 184, 115);
      colorArray1[440][3] = new Color((int) byte.MaxValue, 221, 62);
      colorArray1[440][4] = new Color(165, 0, 236);
      colorArray1[440][5] = new Color(223, 230, 238);
      colorArray1[440][6] = new Color(207, 101, 0);
      colorArray1[419][0] = new Color(88, 95, 114);
      colorArray1[419][1] = new Color(214, 225, 236);
      colorArray1[419][2] = new Color(25, 131, 205);
      colorArray1[423][0] = new Color(245, 197, 1);
      colorArray1[423][1] = new Color(185, 0, 224);
      colorArray1[423][2] = new Color(58, 240, 111);
      colorArray1[423][3] = new Color(50, 107, 197);
      colorArray1[423][4] = new Color(253, 91, 3);
      colorArray1[423][5] = new Color(254, 194, 20);
      colorArray1[423][6] = new Color(174, 195, 215);
      colorArray1[420][0] = new Color(99, (int) byte.MaxValue, 107);
      colorArray1[420][1] = new Color(99, (int) byte.MaxValue, 107);
      colorArray1[420][4] = new Color(99, (int) byte.MaxValue, 107);
      colorArray1[420][2] = new Color(218, 2, 5);
      colorArray1[420][3] = new Color(218, 2, 5);
      colorArray1[420][5] = new Color(218, 2, 5);
      colorArray1[476][0] = new Color(160, 160, 160);
      colorArray1[410][0] = new Color(75, 139, 166);
      colorArray1[480][0] = new Color(120, 50, 50);
      colorArray1[509][0] = new Color(50, 50, 60);
      colorArray1[657][0] = new Color(35, 205, 215);
      colorArray1[658][0] = new Color(200, 105, 230);
      colorArray1[412][0] = new Color(75, 139, 166);
      colorArray1[443][0] = new Color(144, 148, 144);
      colorArray1[442][0] = new Color(3, 144, 201);
      colorArray1[444][0] = new Color(191, 176, 124);
      colorArray1[446][0] = new Color((int) byte.MaxValue, 66, 152);
      colorArray1[447][0] = new Color(179, 132, (int) byte.MaxValue);
      colorArray1[448][0] = new Color(0, 206, 180);
      colorArray1[449][0] = new Color(91, 186, 240);
      colorArray1[450][0] = new Color(92, 240, 91);
      colorArray1[451][0] = new Color(240, 91, 147);
      colorArray1[452][0] = new Color((int) byte.MaxValue, 150, 181);
      colorArray1[453][0] = new Color(179, 132, (int) byte.MaxValue);
      colorArray1[453][1] = new Color(0, 206, 180);
      colorArray1[453][2] = new Color((int) byte.MaxValue, 66, 152);
      colorArray1[454][0] = new Color(174, 16, 176);
      colorArray1[455][0] = new Color(48, 225, 110);
      colorArray1[456][0] = new Color(179, 132, (int) byte.MaxValue);
      colorArray1[457][0] = new Color(150, 164, 206);
      colorArray1[457][1] = new Color((int) byte.MaxValue, 132, 184);
      colorArray1[457][2] = new Color(74, (int) byte.MaxValue, 232);
      colorArray1[457][3] = new Color(215, 159, (int) byte.MaxValue);
      colorArray1[457][4] = new Color(229, 219, 234);
      colorArray1[458][0] = new Color(211, 198, 111);
      colorArray1[459][0] = new Color(190, 223, 232);
      colorArray1[460][0] = new Color(141, 163, 181);
      colorArray1[462][0] = new Color(231, 178, 28);
      colorArray1[467][0] = new Color(129, 56, 121);
      colorArray1[467][1] = new Color((int) byte.MaxValue, 249, 59);
      colorArray1[467][2] = new Color(161, 67, 24);
      colorArray1[467][3] = new Color(89, 70, 72);
      colorArray1[467][4] = new Color(233, 207, 94);
      colorArray1[467][5] = new Color(254, 158, 35);
      colorArray1[467][6] = new Color(34, 221, 151);
      colorArray1[467][7] = new Color(249, 170, 236);
      colorArray1[467][8] = new Color(35, 200, 254);
      colorArray1[467][9] = new Color(190, 200, 200);
      colorArray1[467][10] = new Color(230, 170, 100);
      colorArray1[467][11] = new Color(165, 168, 26);
      for (int index = 0; index < 12; ++index)
        colorArray1[468][index] = colorArray1[467][index];
      colorArray1[472][0] = new Color(190, 160, 140);
      colorArray1[473][0] = new Color(85, 114, 123);
      colorArray1[474][0] = new Color(116, 94, 97);
      colorArray1[478][0] = new Color(108, 34, 35);
      colorArray1[479][0] = new Color(178, 114, 68);
      colorArray1[485][0] = new Color(198, 134, 88);
      colorArray1[492][0] = new Color(78, 193, 227);
      colorArray1[492][0] = new Color(78, 193, 227);
      colorArray1[493][0] = new Color(250, 249, 252);
      colorArray1[493][1] = new Color(240, 90, 90);
      colorArray1[493][2] = new Color(98, 230, 92);
      colorArray1[493][3] = new Color(95, 197, 238);
      colorArray1[493][4] = new Color(241, 221, 100);
      colorArray1[493][5] = new Color(213, 92, 237);
      colorArray1[494][0] = new Color(224, 219, 236);
      colorArray1[495][0] = new Color(253, 227, 215);
      colorArray1[496][0] = new Color(165, 159, 153);
      colorArray1[498][0] = new Color(202, 174, 165);
      colorArray1[499][0] = new Color(160, 187, 142);
      colorArray1[500][0] = new Color(254, 158, 35);
      colorArray1[501][0] = new Color(34, 221, 151);
      colorArray1[502][0] = new Color(249, 170, 236);
      colorArray1[503][0] = new Color(35, 200, 254);
      colorArray1[506][0] = new Color(61, 61, 61);
      colorArray1[510][0] = new Color(191, 142, 111);
      colorArray1[511][0] = new Color(187, 68, 74);
      colorArray1[520][0] = new Color(224, 219, 236);
      colorArray1[545][0] = new Color((int) byte.MaxValue, 126, 145);
      colorArray1[530][0] = new Color(107, 182, 0);
      colorArray1[530][1] = new Color(23, 154, 209);
      colorArray1[530][2] = new Color(238, 97, 94);
      colorArray1[530][3] = new Color(113, 108, 205);
      colorArray1[546][0] = new Color(60, 60, 60);
      colorArray1[557][0] = new Color(60, 60, 60);
      colorArray1[547][0] = new Color(120, 110, 100);
      colorArray1[548][0] = new Color(120, 110, 100);
      colorArray1[562][0] = new Color(165, 168, 26);
      colorArray1[563][0] = new Color(165, 168, 26);
      colorArray1[571][0] = new Color(165, 168, 26);
      colorArray1[568][0] = new Color(248, 203, 233);
      colorArray1[569][0] = new Color(203, 248, 218);
      colorArray1[570][0] = new Color(160, 242, (int) byte.MaxValue);
      colorArray1[597][0] = new Color(28, 216, 94);
      colorArray1[597][1] = new Color(183, 237, 20);
      colorArray1[597][2] = new Color(185, 83, 200);
      colorArray1[597][3] = new Color(131, 128, 168);
      colorArray1[597][4] = new Color(38, 142, 214);
      colorArray1[597][5] = new Color(229, 154, 9);
      colorArray1[597][6] = new Color(142, 227, 234);
      colorArray1[597][7] = new Color(98, 111, 223);
      colorArray1[597][8] = new Color(241, 233, 158);
      colorArray1[617][0] = new Color(233, 207, 94);
      Color color3 = new Color(250, 100, 50);
      colorArray1[548][1] = color3;
      colorArray1[613][0] = color3;
      colorArray1[614][0] = color3;
      colorArray1[623][0] = new Color(220, 210, 245);
      colorArray1[661][0] = new Color(141, 137, 223);
      colorArray1[662][0] = new Color(208, 80, 80);
      colorArray1[666][0] = new Color(115, 60, 40);
      colorArray1[667][0] = new Color(247, 228, 254);
      Color[] colorArray6 = new Color[4]
      {
        new Color(9, 61, 191),
        new Color(253, 32, 3),
        new Color(254, 194, 20),
        new Color(161, (int) sbyte.MaxValue, (int) byte.MaxValue)
      };
      Color[][] colorArray7 = new Color[(int) WallID.Count][];
      for (int index = 0; index < (int) WallID.Count; ++index)
        colorArray7[index] = new Color[2];
      colorArray7[158][0] = new Color(107, 49, 154);
      colorArray7[163][0] = new Color(154, 148, 49);
      colorArray7[162][0] = new Color(49, 49, 154);
      colorArray7[160][0] = new Color(49, 154, 68);
      colorArray7[161][0] = new Color(154, 49, 77);
      colorArray7[159][0] = new Color(85, 89, 118);
      colorArray7[157][0] = new Color(154, 83, 49);
      colorArray7[154][0] = new Color(221, 79, (int) byte.MaxValue);
      colorArray7[166][0] = new Color(250, (int) byte.MaxValue, 79);
      colorArray7[165][0] = new Color(79, 102, (int) byte.MaxValue);
      colorArray7[156][0] = new Color(79, (int) byte.MaxValue, 89);
      colorArray7[164][0] = new Color((int) byte.MaxValue, 79, 79);
      colorArray7[155][0] = new Color(240, 240, 247);
      colorArray7[153][0] = new Color((int) byte.MaxValue, 145, 79);
      colorArray7[169][0] = new Color(5, 5, 5);
      colorArray7[224][0] = new Color(57, 55, 52);
      colorArray7[323][0] = new Color(55, 25, 33);
      colorArray7[324][0] = new Color(60, 55, 145);
      colorArray7[325][0] = new Color(10, 5, 50);
      colorArray7[326][0] = new Color(30, 105, 75);
      colorArray7[327][0] = new Color(5, 45, 55);
      colorArray7[328][0] = new Color(20, 25, 35);
      colorArray7[329][0] = new Color(15, 10, 50);
      colorArray7[330][0] = new Color(153, 164, 187);
      colorArray7[225][0] = new Color(68, 68, 68);
      colorArray7[226][0] = new Color(148, 138, 74);
      colorArray7[227][0] = new Color(95, 137, 191);
      colorArray7[170][0] = new Color(59, 39, 22);
      colorArray7[171][0] = new Color(59, 39, 22);
      color1 = new Color(52, 52, 52);
      colorArray7[1][0] = color1;
      colorArray7[53][0] = color1;
      colorArray7[52][0] = color1;
      colorArray7[51][0] = color1;
      colorArray7[50][0] = color1;
      colorArray7[49][0] = color1;
      colorArray7[48][0] = color1;
      colorArray7[44][0] = color1;
      colorArray7[346][0] = color1;
      colorArray7[5][0] = color1;
      color1 = new Color(88, 61, 46);
      colorArray7[2][0] = color1;
      colorArray7[16][0] = color1;
      colorArray7[59][0] = color1;
      colorArray7[3][0] = new Color(61, 58, 78);
      colorArray7[4][0] = new Color(73, 51, 36);
      colorArray7[6][0] = new Color(91, 30, 30);
      color1 = new Color(27, 31, 42);
      colorArray7[7][0] = color1;
      colorArray7[17][0] = color1;
      colorArray7[331][0] = color1;
      color1 = new Color(32, 40, 45);
      colorArray7[94][0] = color1;
      colorArray7[100][0] = color1;
      color1 = new Color(44, 41, 50);
      colorArray7[95][0] = color1;
      colorArray7[101][0] = color1;
      color1 = new Color(31, 39, 26);
      colorArray7[8][0] = color1;
      colorArray7[18][0] = color1;
      colorArray7[332][0] = color1;
      color1 = new Color(36, 45, 44);
      colorArray7[98][0] = color1;
      colorArray7[104][0] = color1;
      color1 = new Color(38, 49, 50);
      colorArray7[99][0] = color1;
      colorArray7[105][0] = color1;
      color1 = new Color(41, 28, 36);
      colorArray7[9][0] = color1;
      colorArray7[19][0] = color1;
      colorArray7[333][0] = color1;
      color1 = new Color(72, 50, 77);
      colorArray7[96][0] = color1;
      colorArray7[102][0] = color1;
      color1 = new Color(78, 50, 69);
      colorArray7[97][0] = color1;
      colorArray7[103][0] = color1;
      colorArray7[10][0] = new Color(74, 62, 12);
      colorArray7[334][0] = new Color(74, 62, 12);
      colorArray7[11][0] = new Color(46, 56, 59);
      colorArray7[335][0] = new Color(46, 56, 59);
      colorArray7[12][0] = new Color(75, 32, 11);
      colorArray7[336][0] = new Color(75, 32, 11);
      colorArray7[13][0] = new Color(67, 37, 37);
      colorArray7[338][0] = new Color(67, 37, 37);
      color1 = new Color(15, 15, 15);
      colorArray7[14][0] = color1;
      colorArray7[337][0] = color1;
      colorArray7[20][0] = color1;
      colorArray7[15][0] = new Color(52, 43, 45);
      colorArray7[22][0] = new Color(113, 99, 99);
      colorArray7[23][0] = new Color(38, 38, 43);
      colorArray7[24][0] = new Color(53, 39, 41);
      colorArray7[25][0] = new Color(11, 35, 62);
      colorArray7[339][0] = new Color(11, 35, 62);
      colorArray7[26][0] = new Color(21, 63, 70);
      colorArray7[340][0] = new Color(21, 63, 70);
      colorArray7[27][0] = new Color(88, 61, 46);
      colorArray7[27][1] = new Color(52, 52, 52);
      colorArray7[28][0] = new Color(81, 84, 101);
      colorArray7[29][0] = new Color(88, 23, 23);
      colorArray7[30][0] = new Color(28, 88, 23);
      colorArray7[31][0] = new Color(78, 87, 99);
      color1 = new Color(69, 67, 41);
      colorArray7[34][0] = color1;
      colorArray7[37][0] = color1;
      colorArray7[32][0] = new Color(86, 17, 40);
      colorArray7[33][0] = new Color(49, 47, 83);
      colorArray7[35][0] = new Color(51, 51, 70);
      colorArray7[36][0] = new Color(87, 59, 55);
      colorArray7[38][0] = new Color(49, 57, 49);
      colorArray7[39][0] = new Color(78, 79, 73);
      colorArray7[45][0] = new Color(60, 59, 51);
      colorArray7[46][0] = new Color(48, 57, 47);
      colorArray7[47][0] = new Color(71, 77, 85);
      colorArray7[40][0] = new Color(85, 102, 103);
      colorArray7[41][0] = new Color(52, 50, 62);
      colorArray7[42][0] = new Color(71, 42, 44);
      colorArray7[43][0] = new Color(73, 66, 50);
      colorArray7[54][0] = new Color(40, 56, 50);
      colorArray7[55][0] = new Color(49, 48, 36);
      colorArray7[56][0] = new Color(43, 33, 32);
      colorArray7[57][0] = new Color(31, 40, 49);
      colorArray7[58][0] = new Color(48, 35, 52);
      colorArray7[60][0] = new Color(1, 52, 20);
      colorArray7[61][0] = new Color(55, 39, 26);
      colorArray7[62][0] = new Color(39, 33, 26);
      colorArray7[69][0] = new Color(43, 42, 68);
      colorArray7[70][0] = new Color(30, 70, 80);
      colorArray7[341][0] = new Color(100, 40, 1);
      colorArray7[342][0] = new Color(92, 30, 72);
      colorArray7[343][0] = new Color(42, 81, 1);
      colorArray7[344][0] = new Color(1, 81, 109);
      colorArray7[345][0] = new Color(56, 22, 97);
      color1 = new Color(30, 80, 48);
      colorArray7[63][0] = color1;
      colorArray7[65][0] = color1;
      colorArray7[66][0] = color1;
      colorArray7[68][0] = color1;
      color1 = new Color(53, 80, 30);
      colorArray7[64][0] = color1;
      colorArray7[67][0] = color1;
      colorArray7[78][0] = new Color(63, 39, 26);
      colorArray7[244][0] = new Color(63, 39, 26);
      colorArray7[71][0] = new Color(78, 105, 135);
      colorArray7[72][0] = new Color(52, 84, 12);
      colorArray7[73][0] = new Color(190, 204, 223);
      color1 = new Color(64, 62, 80);
      colorArray7[74][0] = color1;
      colorArray7[80][0] = color1;
      colorArray7[75][0] = new Color(65, 65, 35);
      colorArray7[76][0] = new Color(20, 46, 104);
      colorArray7[77][0] = new Color(61, 13, 16);
      colorArray7[79][0] = new Color(51, 47, 96);
      colorArray7[81][0] = new Color(101, 51, 51);
      colorArray7[82][0] = new Color(77, 64, 34);
      colorArray7[83][0] = new Color(62, 38, 41);
      colorArray7[234][0] = new Color(60, 36, 39);
      colorArray7[84][0] = new Color(48, 78, 93);
      colorArray7[85][0] = new Color(54, 63, 69);
      color1 = new Color(138, 73, 38);
      colorArray7[86][0] = color1;
      colorArray7[108][0] = color1;
      color1 = new Color(50, 15, 8);
      colorArray7[87][0] = color1;
      colorArray7[112][0] = color1;
      colorArray7[109][0] = new Color(94, 25, 17);
      colorArray7[110][0] = new Color(125, 36, 122);
      colorArray7[111][0] = new Color(51, 35, 27);
      colorArray7[113][0] = new Color(135, 58, 0);
      colorArray7[114][0] = new Color(65, 52, 15);
      colorArray7[115][0] = new Color(39, 42, 51);
      colorArray7[116][0] = new Color(89, 26, 27);
      colorArray7[117][0] = new Color(126, 123, 115);
      colorArray7[118][0] = new Color(8, 50, 19);
      colorArray7[119][0] = new Color(95, 21, 24);
      colorArray7[120][0] = new Color(17, 31, 65);
      colorArray7[121][0] = new Color(192, 173, 143);
      colorArray7[122][0] = new Color(114, 114, 131);
      colorArray7[123][0] = new Color(136, 119, 7);
      colorArray7[124][0] = new Color(8, 72, 3);
      colorArray7[125][0] = new Color(117, 132, 82);
      colorArray7[126][0] = new Color(100, 102, 114);
      colorArray7[(int) sbyte.MaxValue][0] = new Color(30, 118, 226);
      colorArray7[128][0] = new Color(93, 6, 102);
      colorArray7[129][0] = new Color(64, 40, 169);
      colorArray7[130][0] = new Color(39, 34, 180);
      colorArray7[131][0] = new Color(87, 94, 125);
      colorArray7[132][0] = new Color(6, 6, 6);
      colorArray7[133][0] = new Color(69, 72, 186);
      colorArray7[134][0] = new Color(130, 62, 16);
      colorArray7[135][0] = new Color(22, 123, 163);
      colorArray7[136][0] = new Color(40, 86, 151);
      colorArray7[137][0] = new Color(183, 75, 15);
      colorArray7[138][0] = new Color(83, 80, 100);
      colorArray7[139][0] = new Color(115, 65, 68);
      colorArray7[140][0] = new Color(119, 108, 81);
      colorArray7[141][0] = new Color(59, 67, 71);
      colorArray7[142][0] = new Color(222, 216, 202);
      colorArray7[143][0] = new Color(90, 112, 105);
      colorArray7[144][0] = new Color(62, 28, 87);
      colorArray7[146][0] = new Color(120, 59, 19);
      colorArray7[147][0] = new Color(59, 59, 59);
      colorArray7[148][0] = new Color(229, 218, 161);
      colorArray7[149][0] = new Color(73, 59, 50);
      colorArray7[151][0] = new Color(102, 75, 34);
      colorArray7[167][0] = new Color(70, 68, 51);
      Color color4 = new Color(125, 100, 100);
      colorArray7[316][0] = color4;
      colorArray7[317][0] = color4;
      colorArray7[172][0] = new Color(163, 96, 0);
      colorArray7[242][0] = new Color(5, 5, 5);
      colorArray7[243][0] = new Color(5, 5, 5);
      colorArray7[173][0] = new Color(94, 163, 46);
      colorArray7[174][0] = new Color(117, 32, 59);
      colorArray7[175][0] = new Color(20, 11, 203);
      colorArray7[176][0] = new Color(74, 69, 88);
      colorArray7[177][0] = new Color(60, 30, 30);
      colorArray7[183][0] = new Color(111, 117, 135);
      colorArray7[179][0] = new Color(111, 117, 135);
      colorArray7[178][0] = new Color(111, 117, 135);
      colorArray7[184][0] = new Color(25, 23, 54);
      colorArray7[181][0] = new Color(25, 23, 54);
      colorArray7[180][0] = new Color(25, 23, 54);
      colorArray7[182][0] = new Color(74, 71, 129);
      colorArray7[185][0] = new Color(52, 52, 52);
      colorArray7[186][0] = new Color(38, 9, 66);
      colorArray7[216][0] = new Color(158, 100, 64);
      colorArray7[217][0] = new Color(62, 45, 75);
      colorArray7[218][0] = new Color(57, 14, 12);
      colorArray7[219][0] = new Color(96, 72, 133);
      colorArray7[187][0] = new Color(149, 80, 51);
      colorArray7[235][0] = new Color(140, 75, 48);
      colorArray7[220][0] = new Color(67, 55, 80);
      colorArray7[221][0] = new Color(64, 37, 29);
      colorArray7[222][0] = new Color(70, 51, 91);
      colorArray7[188][0] = new Color(82, 63, 80);
      colorArray7[189][0] = new Color(65, 61, 77);
      colorArray7[190][0] = new Color(64, 65, 92);
      colorArray7[191][0] = new Color(76, 53, 84);
      colorArray7[192][0] = new Color(144, 67, 52);
      colorArray7[193][0] = new Color(149, 48, 48);
      colorArray7[194][0] = new Color(111, 32, 36);
      colorArray7[195][0] = new Color(147, 48, 55);
      colorArray7[196][0] = new Color(97, 67, 51);
      colorArray7[197][0] = new Color(112, 80, 62);
      colorArray7[198][0] = new Color(88, 61, 46);
      colorArray7[199][0] = new Color((int) sbyte.MaxValue, 94, 76);
      colorArray7[200][0] = new Color(143, 50, 123);
      colorArray7[201][0] = new Color(136, 120, 131);
      colorArray7[202][0] = new Color(219, 92, 143);
      colorArray7[203][0] = new Color(113, 64, 150);
      colorArray7[204][0] = new Color(74, 67, 60);
      colorArray7[205][0] = new Color(60, 78, 59);
      colorArray7[206][0] = new Color(0, 54, 21);
      colorArray7[207][0] = new Color(74, 97, 72);
      colorArray7[208][0] = new Color(40, 37, 35);
      colorArray7[209][0] = new Color(77, 63, 66);
      colorArray7[210][0] = new Color(111, 6, 6);
      colorArray7[211][0] = new Color(88, 67, 59);
      colorArray7[212][0] = new Color(88, 87, 80);
      colorArray7[213][0] = new Color(71, 71, 67);
      colorArray7[214][0] = new Color(76, 52, 60);
      colorArray7[215][0] = new Color(89, 48, 59);
      colorArray7[223][0] = new Color(51, 18, 4);
      colorArray7[228][0] = new Color(160, 2, 75);
      colorArray7[229][0] = new Color(100, 55, 164);
      colorArray7[230][0] = new Color(0, 117, 101);
      colorArray7[236][0] = new Color((int) sbyte.MaxValue, 49, 44);
      colorArray7[231][0] = new Color(110, 90, 78);
      colorArray7[232][0] = new Color(47, 69, 75);
      colorArray7[233][0] = new Color(91, 67, 70);
      colorArray7[237][0] = new Color(200, 44, 18);
      colorArray7[238][0] = new Color(24, 93, 66);
      colorArray7[239][0] = new Color(160, 87, 234);
      colorArray7[240][0] = new Color(6, 106, (int) byte.MaxValue);
      colorArray7[245][0] = new Color(102, 102, 102);
      colorArray7[315][0] = new Color(181, 230, 29);
      colorArray7[246][0] = new Color(61, 58, 78);
      colorArray7[247][0] = new Color(52, 43, 45);
      colorArray7[248][0] = new Color(81, 84, 101);
      colorArray7[249][0] = new Color(85, 102, 103);
      colorArray7[250][0] = new Color(52, 52, 52);
      colorArray7[251][0] = new Color(52, 52, 52);
      colorArray7[252][0] = new Color(52, 52, 52);
      colorArray7[253][0] = new Color(52, 52, 52);
      colorArray7[254][0] = new Color(52, 52, 52);
      colorArray7[(int) byte.MaxValue][0] = new Color(52, 52, 52);
      colorArray7[314][0] = new Color(52, 52, 52);
      colorArray7[256][0] = new Color(40, 56, 50);
      colorArray7[257][0] = new Color(49, 48, 36);
      colorArray7[258][0] = new Color(43, 33, 32);
      colorArray7[259][0] = new Color(31, 40, 49);
      colorArray7[260][0] = new Color(48, 35, 52);
      colorArray7[261][0] = new Color(88, 61, 46);
      colorArray7[262][0] = new Color(55, 39, 26);
      colorArray7[263][0] = new Color(39, 33, 26);
      colorArray7[264][0] = new Color(43, 42, 68);
      colorArray7[265][0] = new Color(30, 70, 80);
      colorArray7[266][0] = new Color(78, 105, 135);
      colorArray7[267][0] = new Color(51, 47, 96);
      colorArray7[268][0] = new Color(101, 51, 51);
      colorArray7[269][0] = new Color(62, 38, 41);
      colorArray7[270][0] = new Color(59, 39, 22);
      colorArray7[271][0] = new Color(59, 39, 22);
      colorArray7[272][0] = new Color(111, 117, 135);
      colorArray7[273][0] = new Color(25, 23, 54);
      colorArray7[274][0] = new Color(52, 52, 52);
      colorArray7[275][0] = new Color(149, 80, 51);
      colorArray7[276][0] = new Color(82, 63, 80);
      colorArray7[277][0] = new Color(65, 61, 77);
      colorArray7[278][0] = new Color(64, 65, 92);
      colorArray7[279][0] = new Color(76, 53, 84);
      colorArray7[280][0] = new Color(144, 67, 52);
      colorArray7[281][0] = new Color(149, 48, 48);
      colorArray7[282][0] = new Color(111, 32, 36);
      colorArray7[283][0] = new Color(147, 48, 55);
      colorArray7[284][0] = new Color(97, 67, 51);
      colorArray7[285][0] = new Color(112, 80, 62);
      colorArray7[286][0] = new Color(88, 61, 46);
      colorArray7[287][0] = new Color((int) sbyte.MaxValue, 94, 76);
      colorArray7[288][0] = new Color(143, 50, 123);
      colorArray7[289][0] = new Color(136, 120, 131);
      colorArray7[290][0] = new Color(219, 92, 143);
      colorArray7[291][0] = new Color(113, 64, 150);
      colorArray7[292][0] = new Color(74, 67, 60);
      colorArray7[293][0] = new Color(60, 78, 59);
      colorArray7[294][0] = new Color(0, 54, 21);
      colorArray7[295][0] = new Color(74, 97, 72);
      colorArray7[296][0] = new Color(40, 37, 35);
      colorArray7[297][0] = new Color(77, 63, 66);
      colorArray7[298][0] = new Color(111, 6, 6);
      colorArray7[299][0] = new Color(88, 67, 59);
      colorArray7[300][0] = new Color(88, 87, 80);
      colorArray7[301][0] = new Color(71, 71, 67);
      colorArray7[302][0] = new Color(76, 52, 60);
      colorArray7[303][0] = new Color(89, 48, 59);
      colorArray7[304][0] = new Color(158, 100, 64);
      colorArray7[305][0] = new Color(62, 45, 75);
      colorArray7[306][0] = new Color(57, 14, 12);
      colorArray7[307][0] = new Color(96, 72, 133);
      colorArray7[308][0] = new Color(67, 55, 80);
      colorArray7[309][0] = new Color(64, 37, 29);
      colorArray7[310][0] = new Color(70, 51, 91);
      colorArray7[311][0] = new Color(51, 18, 4);
      colorArray7[312][0] = new Color(78, 110, 51);
      colorArray7[313][0] = new Color(78, 110, 51);
      colorArray7[319][0] = new Color(105, 51, 108);
      colorArray7[320][0] = new Color(75, 30, 15);
      colorArray7[321][0] = new Color(91, 108, 130);
      colorArray7[322][0] = new Color(91, 108, 130);
      Color[] colorArray8 = new Color[256];
      Color color5 = new Color(50, 40, (int) byte.MaxValue);
      Color color6 = new Color(145, 185, (int) byte.MaxValue);
      for (int index = 0; index < colorArray8.Length; ++index)
      {
        float num1 = (float) index / (float) colorArray8.Length;
        float num2 = 1f - num1;
        colorArray8[index] = new Color((int) (byte) ((double) color5.R * (double) num2 + (double) color6.R * (double) num1), (int) (byte) ((double) color5.G * (double) num2 + (double) color6.G * (double) num1), (int) (byte) ((double) color5.B * (double) num2 + (double) color6.B * (double) num1));
      }
      Color[] colorArray9 = new Color[256];
      Color color7 = new Color(88, 61, 46);
      Color color8 = new Color(37, 78, 123);
      for (int index = 0; index < colorArray9.Length; ++index)
      {
        float num3 = (float) index / (float) byte.MaxValue;
        float num4 = 1f - num3;
        colorArray9[index] = new Color((int) (byte) ((double) color7.R * (double) num4 + (double) color8.R * (double) num3), (int) (byte) ((double) color7.G * (double) num4 + (double) color8.G * (double) num3), (int) (byte) ((double) color7.B * (double) num4 + (double) color8.B * (double) num3));
      }
      Color[] colorArray10 = new Color[256];
      Color color9 = new Color(74, 67, 60);
      color8 = new Color(53, 70, 97);
      for (int index = 0; index < colorArray10.Length; ++index)
      {
        float num5 = (float) index / (float) byte.MaxValue;
        float num6 = 1f - num5;
        colorArray10[index] = new Color((int) (byte) ((double) color9.R * (double) num6 + (double) color8.R * (double) num5), (int) (byte) ((double) color9.G * (double) num6 + (double) color8.G * (double) num5), (int) (byte) ((double) color9.B * (double) num6 + (double) color8.B * (double) num5));
      }
      Color color10 = new Color(50, 44, 38);
      int num = 0;
      MapHelper.tileOptionCounts = new int[(int) TileID.Count];
      for (int index1 = 0; index1 < (int) TileID.Count; ++index1)
      {
        Color[] colorArray11 = colorArray1[index1];
        int index2 = 0;
        while (index2 < 12 && !(colorArray11[index2] == Color.Transparent))
          ++index2;
        MapHelper.tileOptionCounts[index1] = index2;
        num += index2;
      }
      MapHelper.wallOptionCounts = new int[(int) WallID.Count];
      for (int index3 = 0; index3 < (int) WallID.Count; ++index3)
      {
        Color[] colorArray12 = colorArray7[index3];
        int index4 = 0;
        while (index4 < 2 && !(colorArray12[index4] == Color.Transparent))
          ++index4;
        MapHelper.wallOptionCounts[index3] = index4;
        num += index4;
      }
      MapHelper.colorLookup = new Color[num + 774];
      MapHelper.colorLookup[0] = Color.Transparent;
      ushort index5 = 1;
      MapHelper.tilePosition = index5;
      MapHelper.tileLookup = new ushort[(int) TileID.Count];
      for (int index6 = 0; index6 < (int) TileID.Count; ++index6)
      {
        if (MapHelper.tileOptionCounts[index6] > 0)
        {
          Color[] colorArray13 = colorArray1[index6];
          MapHelper.tileLookup[index6] = index5;
          for (int index7 = 0; index7 < MapHelper.tileOptionCounts[index6]; ++index7)
          {
            MapHelper.colorLookup[(int) index5] = colorArray1[index6][index7];
            ++index5;
          }
        }
        else
          MapHelper.tileLookup[index6] = (ushort) 0;
      }
      MapHelper.wallPosition = index5;
      MapHelper.wallLookup = new ushort[(int) WallID.Count];
      MapHelper.wallRangeStart = index5;
      for (int index8 = 0; index8 < (int) WallID.Count; ++index8)
      {
        if (MapHelper.wallOptionCounts[index8] > 0)
        {
          Color[] colorArray14 = colorArray7[index8];
          MapHelper.wallLookup[index8] = index5;
          for (int index9 = 0; index9 < MapHelper.wallOptionCounts[index8]; ++index9)
          {
            MapHelper.colorLookup[(int) index5] = colorArray7[index8][index9];
            ++index5;
          }
        }
        else
          MapHelper.wallLookup[index8] = (ushort) 0;
      }
      MapHelper.wallRangeEnd = index5;
      MapHelper.liquidPosition = index5;
      for (int index10 = 0; index10 < 4; ++index10)
      {
        MapHelper.colorLookup[(int) index5] = colorArray6[index10];
        ++index5;
      }
      MapHelper.skyPosition = index5;
      for (int index11 = 0; index11 < 256; ++index11)
      {
        MapHelper.colorLookup[(int) index5] = colorArray8[index11];
        ++index5;
      }
      MapHelper.dirtPosition = index5;
      for (int index12 = 0; index12 < 256; ++index12)
      {
        MapHelper.colorLookup[(int) index5] = colorArray9[index12];
        ++index5;
      }
      MapHelper.rockPosition = index5;
      for (int index13 = 0; index13 < 256; ++index13)
      {
        MapHelper.colorLookup[(int) index5] = colorArray10[index13];
        ++index5;
      }
      MapHelper.hellPosition = index5;
      MapHelper.colorLookup[(int) index5] = color10;
      MapHelper.snowTypes = new ushort[6];
      MapHelper.snowTypes[0] = MapHelper.tileLookup[147];
      MapHelper.snowTypes[1] = MapHelper.tileLookup[161];
      MapHelper.snowTypes[2] = MapHelper.tileLookup[162];
      MapHelper.snowTypes[3] = MapHelper.tileLookup[163];
      MapHelper.snowTypes[4] = MapHelper.tileLookup[164];
      MapHelper.snowTypes[5] = MapHelper.tileLookup[200];
      Lang.BuildMapAtlas();
    }

    public static void ResetMapData() => MapHelper.numUpdateTile = 0;

    public static bool HasOption(int tileType, int option) => option < MapHelper.tileOptionCounts[tileType];

    public static int TileToLookup(int tileType, int option) => (int) MapHelper.tileLookup[tileType] + option;

    public static int LookupCount() => MapHelper.colorLookup.Length;

    private static void MapColor(ushort type, ref Color oldColor, byte colorType)
    {
      Color color = WorldGen.paintColor((int) colorType);
      float num1 = (float) oldColor.R / (float) byte.MaxValue;
      float num2 = (float) oldColor.G / (float) byte.MaxValue;
      float num3 = (float) oldColor.B / (float) byte.MaxValue;
      if ((double) num2 > (double) num1)
        num1 = num2;
      if ((double) num3 > (double) num1)
      {
        double num4 = (double) num1;
        num1 = num3;
        num3 = (float) num4;
      }
      switch (colorType)
      {
        case 29:
          float num5 = num3 * 0.3f;
          oldColor.R = (byte) ((double) color.R * (double) num5);
          oldColor.G = (byte) ((double) color.G * (double) num5);
          oldColor.B = (byte) ((double) color.B * (double) num5);
          break;
        case 30:
          if ((int) type >= (int) MapHelper.wallRangeStart && (int) type <= (int) MapHelper.wallRangeEnd)
          {
            oldColor.R = (byte) ((double) ((int) byte.MaxValue - (int) oldColor.R) * 0.5);
            oldColor.G = (byte) ((double) ((int) byte.MaxValue - (int) oldColor.G) * 0.5);
            oldColor.B = (byte) ((double) ((int) byte.MaxValue - (int) oldColor.B) * 0.5);
            break;
          }
          oldColor.R = (byte) ((uint) byte.MaxValue - (uint) oldColor.R);
          oldColor.G = (byte) ((uint) byte.MaxValue - (uint) oldColor.G);
          oldColor.B = (byte) ((uint) byte.MaxValue - (uint) oldColor.B);
          break;
        default:
          float num6 = num1;
          oldColor.R = (byte) ((double) color.R * (double) num6);
          oldColor.G = (byte) ((double) color.G * (double) num6);
          oldColor.B = (byte) ((double) color.B * (double) num6);
          break;
      }
    }

    public static Color GetMapTileXnaColor(ref MapTile tile)
    {
      Color oldColor = MapHelper.colorLookup[(int) tile.Type];
      byte color = tile.Color;
      if (color > (byte) 0)
        MapHelper.MapColor(tile.Type, ref oldColor, color);
      if (tile.Light == byte.MaxValue)
        return oldColor;
      float num = (float) tile.Light / (float) byte.MaxValue;
      oldColor.R = (byte) ((double) oldColor.R * (double) num);
      oldColor.G = (byte) ((double) oldColor.G * (double) num);
      oldColor.B = (byte) ((double) oldColor.B * (double) num);
      return oldColor;
    }

    public static MapTile CreateMapTile(int i, int j, byte Light)
    {
      Tile tileCache = Main.tile[i, j];
      if (tileCache == null)
        return new MapTile();
      int color = 0;
      int light = (int) Light;
      MapTile mapTile = Main.Map[i, j];
      int num1 = 0;
      int baseOption = 0;
      if (tileCache.active())
      {
        int tileType = (int) tileCache.type;
        num1 = (int) MapHelper.tileLookup[tileType];
        bool flag = tileCache.invisibleBlock();
        if (tileCache.fullbrightBlock() && !flag)
          light = (int) byte.MaxValue;
        if (flag)
        {
          num1 = 0;
        }
        else
        {
          switch (tileType)
          {
            case 5:
              if (WorldGen.IsThisAMushroomTree(i, j))
                baseOption = 1;
              color = (int) tileCache.color();
              goto label_19;
            case 19:
              if (tileCache.frameY == (short) 864)
              {
                num1 = 0;
                break;
              }
              break;
            case 51:
              if ((i + j) % 2 == 0)
              {
                num1 = 0;
                break;
              }
              break;
            case 184:
              if ((int) tileCache.frameX / 22 == 10)
              {
                tileType = 627;
                num1 = (int) MapHelper.tileLookup[tileType];
                break;
              }
              break;
          }
          if (num1 != 0)
          {
            MapHelper.GetTileBaseOption(i, j, tileType, tileCache, ref baseOption);
            color = tileType != 160 ? (int) tileCache.color() : 0;
          }
        }
      }
label_19:
      if (num1 == 0)
      {
        bool flag = tileCache.invisibleWall();
        if (tileCache.wall > (ushort) 0 && tileCache.fullbrightWall() && !flag)
          light = (int) byte.MaxValue;
        if (tileCache.liquid > (byte) 32)
        {
          int num2 = (int) tileCache.liquidType();
          num1 = (int) MapHelper.liquidPosition + num2;
        }
        else if (!tileCache.invisibleWall() && tileCache.wall > (ushort) 0 && (int) tileCache.wall < (int) WallID.Count)
        {
          int wall = (int) tileCache.wall;
          num1 = (int) MapHelper.wallLookup[wall];
          color = (int) tileCache.wallColor();
          switch (wall)
          {
            case 21:
            case 88:
            case 89:
            case 90:
            case 91:
            case 92:
            case 93:
            case 168:
            case 241:
              color = 0;
              break;
            case 27:
              baseOption = i % 2;
              break;
            default:
              baseOption = 0;
              break;
          }
        }
      }
      if (num1 == 0)
      {
        if ((double) j < Main.worldSurface)
        {
          if (Main.remixWorld)
          {
            light = 5;
            num1 = 100;
          }
          else
          {
            int num3 = (int) (byte) ((double) byte.MaxValue * ((double) j / Main.worldSurface));
            num1 = (int) MapHelper.skyPosition + num3;
            light = (int) byte.MaxValue;
            color = 0;
          }
        }
        else if (j < Main.UnderworldLayer)
        {
          color = 0;
          byte num4 = 0;
          float num5 = (float) ((double) Main.screenPosition.X / 16.0 - 5.0);
          float num6 = (float) (((double) Main.screenPosition.X + (double) Main.screenWidth) / 16.0 + 5.0);
          float num7 = (float) ((double) Main.screenPosition.Y / 16.0 - 5.0);
          float num8 = (float) (((double) Main.screenPosition.Y + (double) Main.screenHeight) / 16.0 + 5.0);
          if (((double) i < (double) num5 || (double) i > (double) num6 || (double) j < (double) num7 || (double) j > (double) num8) && i > 40 && i < Main.maxTilesX - 40 && j > 40 && j < Main.maxTilesY - 40)
          {
            for (int x = i - 36; x <= i + 30; x += 10)
            {
              for (int y = j - 36; y <= j + 30; y += 10)
              {
                int type = (int) Main.Map[x, y].Type;
                for (int index = 0; index < MapHelper.snowTypes.Length; ++index)
                {
                  if ((int) MapHelper.snowTypes[index] == type)
                  {
                    num4 = byte.MaxValue;
                    x = i + 31;
                    y = j + 31;
                    break;
                  }
                }
              }
            }
          }
          else
          {
            float num9 = (float) Main.SceneMetrics.SnowTileCount / (float) SceneMetrics.SnowTileMax * (float) byte.MaxValue;
            if ((double) num9 > (double) byte.MaxValue)
              num9 = (float) byte.MaxValue;
            num4 = (byte) num9;
          }
          num1 = (double) j >= Main.rockLayer ? (int) MapHelper.rockPosition + (int) num4 : (int) MapHelper.dirtPosition + (int) num4;
        }
        else
          num1 = (int) MapHelper.hellPosition;
      }
      return MapTile.Create((ushort) (num1 + baseOption), (byte) light, (byte) color);
    }

    public static void GetTileBaseOption(
      int x,
      int y,
      int tileType,
      Tile tileCache,
      ref int baseOption)
    {
      switch (tileType)
      {
        case 4:
          if (tileCache.frameX < (short) 66)
            baseOption = 1;
          baseOption = 0;
          break;
        case 15:
          int num1 = (int) tileCache.frameY / 40;
          baseOption = 0;
          if (num1 != 1 && num1 != 20)
            break;
          baseOption = 1;
          break;
        case 19:
          int num2 = (int) tileCache.frameY / 18;
          baseOption = 0;
          if (num2 != 48)
            break;
          baseOption = 1;
          break;
        case 21:
        case 441:
          switch ((int) tileCache.frameX / 36)
          {
            case 1:
            case 2:
            case 10:
            case 13:
            case 15:
              baseOption = 1;
              return;
            case 3:
            case 4:
              baseOption = 2;
              return;
            case 6:
              baseOption = 3;
              return;
            case 11:
            case 17:
              baseOption = 4;
              return;
            default:
              baseOption = 0;
              return;
          }
        case 26:
          if (tileCache.frameX >= (short) 54)
          {
            baseOption = 1;
            break;
          }
          baseOption = 0;
          break;
        case 27:
          if (tileCache.frameY < (short) 34)
          {
            baseOption = 1;
            break;
          }
          baseOption = 0;
          break;
        case 28:
        case 653:
          if (tileCache.frameY < (short) 144)
          {
            baseOption = 0;
            break;
          }
          if (tileCache.frameY < (short) 252)
          {
            baseOption = 1;
            break;
          }
          if (tileCache.frameY < (short) 360 || tileCache.frameY > (short) 900 && tileCache.frameY < (short) 1008)
          {
            baseOption = 2;
            break;
          }
          if (tileCache.frameY < (short) 468)
          {
            baseOption = 3;
            break;
          }
          if (tileCache.frameY < (short) 576)
          {
            baseOption = 4;
            break;
          }
          if (tileCache.frameY < (short) 684)
          {
            baseOption = 5;
            break;
          }
          if (tileCache.frameY < (short) 792)
          {
            baseOption = 6;
            break;
          }
          if (tileCache.frameY < (short) 898)
          {
            baseOption = 8;
            break;
          }
          if (tileCache.frameY < (short) 1006)
          {
            baseOption = 7;
            break;
          }
          if (tileCache.frameY < (short) 1114)
          {
            baseOption = 0;
            break;
          }
          if (tileCache.frameY < (short) 1222)
          {
            baseOption = 3;
            break;
          }
          baseOption = 7;
          break;
        case 31:
          if (tileCache.frameX >= (short) 36)
          {
            baseOption = 1;
            break;
          }
          baseOption = 0;
          break;
        case 80:
          bool evil;
          bool good;
          bool crimson;
          WorldGen.GetCactusType(x, y, (int) tileCache.frameX, (int) tileCache.frameY, out evil, out good, out crimson);
          if (evil)
          {
            baseOption = 1;
            break;
          }
          if (good)
          {
            baseOption = 2;
            break;
          }
          if (crimson)
          {
            baseOption = 3;
            break;
          }
          baseOption = 0;
          break;
        case 82:
        case 83:
        case 84:
          if (tileCache.frameX < (short) 18)
          {
            baseOption = 0;
            break;
          }
          if (tileCache.frameX < (short) 36)
          {
            baseOption = 1;
            break;
          }
          if (tileCache.frameX < (short) 54)
          {
            baseOption = 2;
            break;
          }
          if (tileCache.frameX < (short) 72)
          {
            baseOption = 3;
            break;
          }
          if (tileCache.frameX < (short) 90)
          {
            baseOption = 4;
            break;
          }
          if (tileCache.frameX < (short) 108)
          {
            baseOption = 5;
            break;
          }
          baseOption = 6;
          break;
        case 89:
          switch ((int) tileCache.frameX / 54)
          {
            case 0:
            case 21:
            case 23:
              baseOption = 0;
              return;
            case 43:
              baseOption = 2;
              return;
            default:
              baseOption = 1;
              return;
          }
        case 105:
          if (tileCache.frameX >= (short) 1548 && tileCache.frameX <= (short) 1654)
          {
            baseOption = 1;
            break;
          }
          if (tileCache.frameX >= (short) 1656 && tileCache.frameX <= (short) 1798)
          {
            baseOption = 2;
            break;
          }
          baseOption = 0;
          break;
        case 129:
          if (tileCache.frameX >= (short) 324)
          {
            baseOption = 1;
            break;
          }
          baseOption = 0;
          break;
        case 133:
          if (tileCache.frameX < (short) 52)
          {
            baseOption = 0;
            break;
          }
          baseOption = 1;
          break;
        case 134:
          if (tileCache.frameX < (short) 28)
          {
            baseOption = 0;
            break;
          }
          baseOption = 1;
          break;
        case 137:
          switch ((int) tileCache.frameY / 18)
          {
            case 1:
            case 2:
            case 3:
            case 4:
              baseOption = 1;
              return;
            case 5:
              baseOption = 2;
              return;
            default:
              baseOption = 0;
              return;
          }
        case 149:
          baseOption = y % 3;
          break;
        case 160:
        case 627:
        case 628:
        case 692:
          baseOption = (x + y) % 9;
          break;
        case 165:
          if (tileCache.frameX < (short) 54)
          {
            baseOption = 0;
            break;
          }
          if (tileCache.frameX < (short) 106)
          {
            baseOption = 1;
            break;
          }
          if (tileCache.frameX >= (short) 216)
          {
            baseOption = 1;
            break;
          }
          if (tileCache.frameX < (short) 162)
          {
            baseOption = 2;
            break;
          }
          baseOption = 3;
          break;
        case 178:
          if (tileCache.frameX < (short) 18)
          {
            baseOption = 0;
            break;
          }
          if (tileCache.frameX < (short) 36)
          {
            baseOption = 1;
            break;
          }
          if (tileCache.frameX < (short) 54)
          {
            baseOption = 2;
            break;
          }
          if (tileCache.frameX < (short) 72)
          {
            baseOption = 3;
            break;
          }
          if (tileCache.frameX < (short) 90)
          {
            baseOption = 4;
            break;
          }
          if (tileCache.frameX < (short) 108)
          {
            baseOption = 5;
            break;
          }
          baseOption = 6;
          break;
        case 184:
          if (tileCache.frameX < (short) 22)
          {
            baseOption = 0;
            break;
          }
          if (tileCache.frameX < (short) 44)
          {
            baseOption = 1;
            break;
          }
          if (tileCache.frameX < (short) 66)
          {
            baseOption = 2;
            break;
          }
          if (tileCache.frameX < (short) 88)
          {
            baseOption = 3;
            break;
          }
          if (tileCache.frameX < (short) 110)
          {
            baseOption = 4;
            break;
          }
          if (tileCache.frameX < (short) 132)
          {
            baseOption = 5;
            break;
          }
          if (tileCache.frameX < (short) 154)
          {
            baseOption = 6;
            break;
          }
          if (tileCache.frameX < (short) 176)
          {
            baseOption = 7;
            break;
          }
          if (tileCache.frameX < (short) 198)
          {
            baseOption = 8;
            break;
          }
          if (tileCache.frameX < (short) 220)
          {
            baseOption = 9;
            break;
          }
          if (tileCache.frameX >= (short) 242)
            break;
          baseOption = 10;
          break;
        case 185:
          if (tileCache.frameY < (short) 18)
          {
            int num3 = (int) tileCache.frameX / 18;
            if (num3 < 6 || num3 == 28 || num3 == 29 || num3 == 30 || num3 == 31 || num3 == 32)
            {
              baseOption = 0;
              break;
            }
            if (num3 < 12 || num3 == 33 || num3 == 34 || num3 == 35)
            {
              baseOption = 1;
              break;
            }
            if (num3 < 28)
            {
              baseOption = 2;
              break;
            }
            if (num3 < 48)
            {
              baseOption = 3;
              break;
            }
            if (num3 < 54)
            {
              baseOption = 4;
              break;
            }
            if (num3 < 72)
            {
              baseOption = 0;
              break;
            }
            if (num3 != 72)
              break;
            baseOption = 1;
            break;
          }
          int num4 = (int) tileCache.frameX / 36 + ((int) tileCache.frameY / 18 - 1) * 18;
          if (num4 < 6 || num4 == 19 || num4 == 20 || num4 == 21 || num4 == 22 || num4 == 23 || num4 == 24 || num4 == 33 || num4 == 38 || num4 == 39 || num4 == 40)
          {
            baseOption = 0;
            break;
          }
          if (num4 < 16)
          {
            baseOption = 2;
            break;
          }
          if (num4 < 19 || num4 == 31 || num4 == 32)
          {
            baseOption = 1;
            break;
          }
          if (num4 < 31)
          {
            baseOption = 3;
            break;
          }
          if (num4 < 38)
          {
            baseOption = 4;
            break;
          }
          if (num4 < 59)
          {
            baseOption = 0;
            break;
          }
          if (num4 >= 62)
            break;
          baseOption = 1;
          break;
        case 186:
        case 647:
          int num5 = (int) tileCache.frameX / 54;
          if (num5 < 7)
          {
            baseOption = 2;
            break;
          }
          if (num5 < 22 || num5 == 33 || num5 == 34 || num5 == 35)
          {
            baseOption = 0;
            break;
          }
          if (num5 < 25)
          {
            baseOption = 1;
            break;
          }
          if (num5 == 25)
          {
            baseOption = 5;
            break;
          }
          if (num5 >= 32)
            break;
          baseOption = 3;
          break;
        case 187:
        case 648:
          int num6 = (int) tileCache.frameX / 54 + (int) tileCache.frameY / 36 * 36;
          if (num6 < 3 || num6 == 14 || num6 == 15 || num6 == 16)
          {
            baseOption = 0;
            break;
          }
          if (num6 < 6)
          {
            baseOption = 6;
            break;
          }
          if (num6 < 9)
          {
            baseOption = 7;
            break;
          }
          if (num6 < 14)
          {
            baseOption = 4;
            break;
          }
          if (num6 < 18)
          {
            baseOption = 4;
            break;
          }
          if (num6 < 23)
          {
            baseOption = 8;
            break;
          }
          if (num6 < 25)
          {
            baseOption = 0;
            break;
          }
          if (num6 < 29)
          {
            baseOption = 1;
            break;
          }
          if (num6 < 47)
          {
            baseOption = 0;
            break;
          }
          if (num6 < 50)
          {
            baseOption = 1;
            break;
          }
          if (num6 < 52)
          {
            baseOption = 10;
            break;
          }
          if (num6 >= 55)
            break;
          baseOption = 2;
          break;
        case 227:
          baseOption = (int) tileCache.frameX / 34;
          break;
        case 240:
          int num7 = (int) tileCache.frameX / 54 + (int) tileCache.frameY / 54 * 36;
          if (num7 >= 0 && num7 <= 11 || num7 >= 47 && num7 <= 53 || num7 == 72 || num7 == 73 || num7 == 75)
          {
            baseOption = 0;
            break;
          }
          if (num7 >= 12 && num7 <= 15)
          {
            baseOption = 1;
            break;
          }
          if (num7 == 16 || num7 == 17)
          {
            baseOption = 2;
            break;
          }
          if (num7 >= 18 && num7 <= 35 || num7 >= 63 && num7 <= 71 || num7 == 74 || num7 >= 76 && num7 <= 92)
          {
            baseOption = 1;
            break;
          }
          if (num7 >= 41 && num7 <= 45)
          {
            baseOption = 3;
            break;
          }
          if (num7 != 46)
            break;
          baseOption = 4;
          break;
        case 242:
          int num8 = (int) tileCache.frameY / 72;
          if ((int) tileCache.frameX / 106 == 0 && num8 >= 22 && num8 <= 24)
          {
            baseOption = 1;
            break;
          }
          baseOption = 0;
          break;
        case 419:
          int num9 = (int) tileCache.frameX / 18;
          if (num9 > 2)
            num9 = 2;
          baseOption = num9;
          break;
        case 420:
          int num10 = (int) tileCache.frameY / 18;
          if (num10 > 5)
            num10 = 5;
          baseOption = num10;
          break;
        case 423:
          int num11 = (int) tileCache.frameY / 18;
          if (num11 > 6)
            num11 = 6;
          baseOption = num11;
          break;
        case 428:
          int num12 = (int) tileCache.frameY / 18;
          if (num12 > 3)
            num12 = 3;
          baseOption = num12;
          break;
        case 440:
          int num13 = (int) tileCache.frameX / 54;
          if (num13 > 6)
            num13 = 6;
          baseOption = num13;
          break;
        case 453:
          int num14 = (int) tileCache.frameX / 36;
          if (num14 > 2)
            num14 = 2;
          baseOption = num14;
          break;
        case 457:
          int num15 = (int) tileCache.frameX / 36;
          if (num15 > 4)
            num15 = 4;
          baseOption = num15;
          break;
        case 461:
          if (Main.player[Main.myPlayer].ZoneCorrupt)
          {
            baseOption = 1;
            break;
          }
          if (Main.player[Main.myPlayer].ZoneCrimson)
          {
            baseOption = 2;
            break;
          }
          if (!Main.player[Main.myPlayer].ZoneHallow)
            break;
          baseOption = 3;
          break;
        case 467:
        case 468:
          int num16 = (int) tileCache.frameX / 36;
          switch (num16)
          {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
              baseOption = num16;
              return;
            case 12:
            case 13:
              baseOption = 10;
              return;
            case 14:
              baseOption = 0;
              return;
            case 15:
              baseOption = 10;
              return;
            case 16:
              baseOption = 3;
              return;
            default:
              baseOption = 0;
              return;
          }
        case 493:
          if (tileCache.frameX < (short) 18)
          {
            baseOption = 0;
            break;
          }
          if (tileCache.frameX < (short) 36)
          {
            baseOption = 1;
            break;
          }
          if (tileCache.frameX < (short) 54)
          {
            baseOption = 2;
            break;
          }
          if (tileCache.frameX < (short) 72)
          {
            baseOption = 3;
            break;
          }
          if (tileCache.frameX < (short) 90)
          {
            baseOption = 4;
            break;
          }
          baseOption = 5;
          break;
        case 518:
        case 519:
          baseOption = (int) tileCache.frameY / 18;
          break;
        case 529:
          int num17 = y + 1;
          int num18 = x;
          int corruptCount1;
          int crimsonCount1;
          int hallowedCount1;
          WorldGen.GetBiomeInfluence(num18, num18, num17, num17, out corruptCount1, out crimsonCount1, out hallowedCount1);
          int num19 = corruptCount1;
          if (num19 < crimsonCount1)
            num19 = crimsonCount1;
          if (num19 < hallowedCount1)
            num19 = hallowedCount1;
          int num20 = corruptCount1 != 0 || crimsonCount1 != 0 || hallowedCount1 != 0 ? (hallowedCount1 != num19 ? (crimsonCount1 != num19 ? 4 : 3) : 2) : (x < WorldGen.beachDistance || x > Main.maxTilesX - WorldGen.beachDistance ? 1 : 0);
          baseOption = num20;
          break;
        case 530:
          int num21 = y - (int) tileCache.frameY % 36 / 18 + 2;
          int startX = x - (int) tileCache.frameX % 54 / 18;
          int corruptCount2;
          int crimsonCount2;
          int hallowedCount2;
          WorldGen.GetBiomeInfluence(startX, startX + 3, num21, num21, out corruptCount2, out crimsonCount2, out hallowedCount2);
          int num22 = corruptCount2;
          if (num22 < crimsonCount2)
            num22 = crimsonCount2;
          if (num22 < hallowedCount2)
            num22 = hallowedCount2;
          int num23 = corruptCount2 != 0 || crimsonCount2 != 0 || hallowedCount2 != 0 ? (hallowedCount2 != num22 ? (crimsonCount2 != num22 ? 3 : 2) : 1) : 0;
          baseOption = num23;
          break;
        case 548:
          if ((int) tileCache.frameX / 54 < 7)
          {
            baseOption = 0;
            break;
          }
          baseOption = 1;
          break;
        case 560:
          int num24 = (int) tileCache.frameX / 36;
          switch (num24)
          {
            case 0:
            case 1:
            case 2:
              baseOption = num24;
              return;
            default:
              baseOption = 0;
              return;
          }
        case 572:
          baseOption = (int) tileCache.frameY / 36;
          break;
        case 591:
          baseOption = (int) tileCache.frameX / 36;
          break;
        case 597:
          int num25 = (int) tileCache.frameX / 54;
          switch (num25)
          {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
              baseOption = num25;
              return;
            default:
              baseOption = 0;
              return;
          }
        case 649:
          int num26 = (int) tileCache.frameX / 18;
          if (num26 < 6 || num26 == 28 || num26 == 29 || num26 == 30 || num26 == 31 || num26 == 32)
          {
            baseOption = 0;
            break;
          }
          if (num26 < 12 || num26 == 33 || num26 == 34 || num26 == 35)
          {
            baseOption = 1;
            break;
          }
          if (num26 < 28)
          {
            baseOption = 2;
            break;
          }
          if (num26 < 48)
          {
            baseOption = 3;
            break;
          }
          if (num26 < 54)
          {
            baseOption = 4;
            break;
          }
          if (num26 < 72)
          {
            baseOption = 0;
            break;
          }
          if (num26 != 72)
            break;
          baseOption = 1;
          break;
        case 650:
          int num27 = (int) tileCache.frameX / 36 + ((int) tileCache.frameY / 18 - 1) * 18;
          if (num27 < 6 || num27 == 19 || num27 == 20 || num27 == 21 || num27 == 22 || num27 == 23 || num27 == 24 || num27 == 33 || num27 == 38 || num27 == 39 || num27 == 40)
          {
            baseOption = 0;
            break;
          }
          if (num27 < 16)
          {
            baseOption = 2;
            break;
          }
          if (num27 < 19 || num27 == 31 || num27 == 32)
          {
            baseOption = 1;
            break;
          }
          if (num27 < 31)
          {
            baseOption = 3;
            break;
          }
          if (num27 < 38)
          {
            baseOption = 4;
            break;
          }
          if (num27 < 59)
          {
            baseOption = 0;
            break;
          }
          if (num27 >= 62)
            break;
          baseOption = 1;
          break;
        default:
          baseOption = 0;
          break;
      }
    }

    public static void SaveMap()
    {
      if (Main.ActivePlayerFileData.IsCloudSave && SocialAPI.Cloud == null || !Main.mapEnabled)
        return;
      if (!Monitor.TryEnter(MapHelper.IOLock))
        return;
      try
      {
        FileUtilities.ProtectedInvoke(new Action(MapHelper.InternalSaveMap));
      }
      catch (Exception ex)
      {
        using (StreamWriter streamWriter = new StreamWriter("client-crashlog.txt", true))
        {
          streamWriter.WriteLine((object) DateTime.Now);
          streamWriter.WriteLine((object) ex);
          streamWriter.WriteLine("");
        }
      }
      finally
      {
        Monitor.Exit(MapHelper.IOLock);
      }
    }

    private static void InternalSaveMap()
    {
      bool isCloudSave = Main.ActivePlayerFileData.IsCloudSave;
      string folderPath = Main.playerPathName.Substring(0, Main.playerPathName.Length - 4);
      if (!isCloudSave)
        Utils.TryCreatingDirectory(folderPath);
      string str = folderPath + Path.DirectorySeparatorChar.ToString();
      string path = !Main.ActiveWorldFileData.UseGuidAsMapName ? str + (object) Main.worldID + ".map" : str + (object) Main.ActiveWorldFileData.UniqueId + ".map";
      new Stopwatch().Start();
      if (!Main.gameMenu)
        MapHelper.noStatusText = true;
      using (MemoryStream output = new MemoryStream(4000))
      {
        using (BinaryWriter writer = new BinaryWriter((Stream) output))
        {
          using (DeflateStream deflateStream = new DeflateStream((Stream) output, (CompressionMode) 0))
          {
            int count = 0;
            byte[] buffer = new byte[16384];
            writer.Write(279);
            Main.MapFileMetadata.IncrementAndWrite(writer);
            writer.Write(Main.worldName);
            writer.Write(Main.worldID);
            writer.Write(Main.maxTilesY);
            writer.Write(Main.maxTilesX);
            writer.Write((short) TileID.Count);
            writer.Write((short) WallID.Count);
            writer.Write((short) 4);
            writer.Write((short) 256);
            writer.Write((short) 256);
            writer.Write((short) 256);
            byte num1 = 1;
            byte num2 = 0;
            for (int index = 0; index < (int) TileID.Count; ++index)
            {
              if (MapHelper.tileOptionCounts[index] != 1)
                num2 |= num1;
              if (num1 == (byte) 128)
              {
                writer.Write(num2);
                num2 = (byte) 0;
                num1 = (byte) 1;
              }
              else
                num1 <<= 1;
            }
            if (num1 != (byte) 1)
              writer.Write(num2);
            int index1 = 0;
            byte num3 = 1;
            byte num4 = 0;
            for (; index1 < (int) WallID.Count; ++index1)
            {
              if (MapHelper.wallOptionCounts[index1] != 1)
                num4 |= num3;
              if (num3 == (byte) 128)
              {
                writer.Write(num4);
                num4 = (byte) 0;
                num3 = (byte) 1;
              }
              else
                num3 <<= 1;
            }
            if (num3 != (byte) 1)
              writer.Write(num4);
            for (int index2 = 0; index2 < (int) TileID.Count; ++index2)
            {
              if (MapHelper.tileOptionCounts[index2] != 1)
                writer.Write((byte) MapHelper.tileOptionCounts[index2]);
            }
            for (int index3 = 0; index3 < (int) WallID.Count; ++index3)
            {
              if (MapHelper.wallOptionCounts[index3] != 1)
                writer.Write((byte) MapHelper.wallOptionCounts[index3]);
            }
            writer.Flush();
            for (int y = 0; y < Main.maxTilesY; ++y)
            {
              if (!MapHelper.noStatusText)
              {
                float num5 = (float) y / (float) Main.maxTilesY;
                Main.statusText = Lang.gen[66].Value + " " + (object) (int) ((double) num5 * 100.0 + 1.0) + "%";
              }
              int num6;
              for (int x1 = 0; x1 < Main.maxTilesX; x1 = num6 + 1)
              {
                MapTile mapTile = Main.Map[x1, y];
                int num7;
                byte num8 = (byte) (num7 = 0);
                byte num9 = (byte) num7;
                byte num10 = (byte) num7;
                bool flag1 = true;
                bool flag2 = true;
                int num11 = 0;
                int num12 = 0;
                byte num13 = 0;
                int num14;
                ushort num15;
                int num16;
                if (mapTile.Light <= (byte) 18)
                {
                  flag2 = false;
                  flag1 = false;
                  num14 = 0;
                  num15 = (ushort) 0;
                  num16 = 0;
                  int x2 = x1 + 1;
                  for (int index4 = Main.maxTilesX - x1 - 1; index4 > 0 && Main.Map[x2, y].Light <= (byte) 18; ++x2)
                  {
                    ++num16;
                    --index4;
                  }
                }
                else
                {
                  num13 = mapTile.Color;
                  num15 = mapTile.Type;
                  if ((int) num15 < (int) MapHelper.wallPosition)
                  {
                    num14 = 1;
                    num15 -= MapHelper.tilePosition;
                  }
                  else if ((int) num15 < (int) MapHelper.liquidPosition)
                  {
                    num14 = 2;
                    num15 -= MapHelper.wallPosition;
                  }
                  else if ((int) num15 < (int) MapHelper.skyPosition)
                  {
                    int num17 = (int) num15 - (int) MapHelper.liquidPosition;
                    if (num17 == 3)
                    {
                      num9 |= (byte) 64;
                      num17 = 0;
                    }
                    num14 = 3 + num17;
                    flag1 = false;
                  }
                  else if ((int) num15 < (int) MapHelper.dirtPosition)
                  {
                    num14 = 6;
                    flag2 = false;
                    flag1 = false;
                  }
                  else if ((int) num15 < (int) MapHelper.hellPosition)
                  {
                    num14 = 7;
                    if ((int) num15 < (int) MapHelper.rockPosition)
                      num15 -= MapHelper.dirtPosition;
                    else
                      num15 -= MapHelper.rockPosition;
                  }
                  else
                  {
                    num14 = 6;
                    flag1 = false;
                  }
                  if (mapTile.Light == byte.MaxValue)
                    flag2 = false;
                  if (flag2)
                  {
                    num16 = 0;
                    int x3 = x1 + 1;
                    int num18 = Main.maxTilesX - x1 - 1;
                    num11 = x3;
                    while (num18 > 0)
                    {
                      MapTile other = Main.Map[x3, y];
                      if (mapTile.EqualsWithoutLight(ref other))
                      {
                        --num18;
                        ++num16;
                        ++x3;
                      }
                      else
                      {
                        num12 = x3;
                        break;
                      }
                    }
                  }
                  else
                  {
                    num16 = 0;
                    int x4 = x1 + 1;
                    int num19 = Main.maxTilesX - x1 - 1;
                    while (num19 > 0)
                    {
                      MapTile other = Main.Map[x4, y];
                      if (mapTile.Equals(ref other))
                      {
                        --num19;
                        ++num16;
                        ++x4;
                      }
                      else
                        break;
                    }
                  }
                }
                if (num13 > (byte) 0)
                  num9 |= (byte) ((uint) num13 << 1);
                if (num8 != (byte) 0)
                  num9 |= (byte) 1;
                if (num9 != (byte) 0)
                  num10 |= (byte) 1;
                byte num20 = (byte) ((uint) num10 | (uint) (byte) (num14 << 1));
                if (flag1 && num15 > (ushort) byte.MaxValue)
                  num20 |= (byte) 16;
                if (flag2)
                  num20 |= (byte) 32;
                if (num16 > 0)
                {
                  if (num16 > (int) byte.MaxValue)
                    num20 |= (byte) 128;
                  else
                    num20 |= (byte) 64;
                }
                buffer[count] = num20;
                ++count;
                if (num9 != (byte) 0)
                {
                  buffer[count] = num9;
                  ++count;
                }
                if (num8 != (byte) 0)
                {
                  buffer[count] = num8;
                  ++count;
                }
                if (flag1)
                {
                  buffer[count] = (byte) num15;
                  ++count;
                  if (num15 > (ushort) byte.MaxValue)
                  {
                    buffer[count] = (byte) ((uint) num15 >> 8);
                    ++count;
                  }
                }
                if (flag2)
                {
                  buffer[count] = mapTile.Light;
                  ++count;
                }
                if (num16 > 0)
                {
                  buffer[count] = (byte) num16;
                  ++count;
                  if (num16 > (int) byte.MaxValue)
                  {
                    buffer[count] = (byte) (num16 >> 8);
                    ++count;
                  }
                }
                for (int x5 = num11; x5 < num12; ++x5)
                {
                  buffer[count] = Main.Map[x5, y].Light;
                  ++count;
                }
                num6 = x1 + num16;
                if (count >= 4096)
                {
                  ((Stream) deflateStream).Write(buffer, 0, count);
                  count = 0;
                }
              }
            }
            if (count > 0)
              ((Stream) deflateStream).Write(buffer, 0, count);
            ((Stream) deflateStream).Dispose();
            FileUtilities.WriteAllBytes(path, output.ToArray(), isCloudSave);
          }
        }
      }
      MapHelper.noStatusText = false;
    }

    public static void LoadMapVersion1(BinaryReader fileIO, int release)
    {
      string str = fileIO.ReadString();
      int num1 = fileIO.ReadInt32();
      int num2 = fileIO.ReadInt32();
      int num3 = fileIO.ReadInt32();
      string worldName = Main.worldName;
      if (str != worldName || num1 != Main.worldID || num3 != Main.maxTilesX || num2 != Main.maxTilesY)
        throw new Exception("Map meta-data is invalid.");
      for (int x = 0; x < Main.maxTilesX; ++x)
      {
        float num4 = (float) x / (float) Main.maxTilesX;
        Main.statusText = Lang.gen[67].Value + " " + (object) (int) ((double) num4 * 100.0 + 1.0) + "%";
        for (int y = 0; y < Main.maxTilesY; ++y)
        {
          if (fileIO.ReadBoolean())
          {
            int index = release <= 77 ? (int) fileIO.ReadByte() : (int) fileIO.ReadUInt16();
            byte light = fileIO.ReadByte();
            MapHelper.OldMapHelper oldMapHelper;
            oldMapHelper.misc = fileIO.ReadByte();
            oldMapHelper.misc2 = release < 50 ? (byte) 0 : fileIO.ReadByte();
            bool flag = false;
            int num5 = (int) oldMapHelper.option();
            int type;
            if (oldMapHelper.active())
              type = num5 + (int) MapHelper.tileLookup[index];
            else if (oldMapHelper.water())
              type = (int) MapHelper.liquidPosition;
            else if (oldMapHelper.lava())
              type = (int) MapHelper.liquidPosition + 1;
            else if (oldMapHelper.honey())
              type = (int) MapHelper.liquidPosition + 2;
            else if (oldMapHelper.wall())
              type = num5 + (int) MapHelper.wallLookup[index];
            else if ((double) y < Main.worldSurface)
            {
              flag = true;
              int num6 = (int) (byte) (256.0 * ((double) y / Main.worldSurface));
              type = (int) MapHelper.skyPosition + num6;
            }
            else if ((double) y < Main.rockLayer)
            {
              flag = true;
              if (index > (int) byte.MaxValue)
                index = (int) byte.MaxValue;
              type = index + (int) MapHelper.dirtPosition;
            }
            else if (y < Main.UnderworldLayer)
            {
              flag = true;
              if (index > (int) byte.MaxValue)
                index = (int) byte.MaxValue;
              type = index + (int) MapHelper.rockPosition;
            }
            else
              type = (int) MapHelper.hellPosition;
            MapTile tile = MapTile.Create((ushort) type, light, (byte) 0);
            Main.Map.SetTile(x, y, ref tile);
            int num7 = (int) fileIO.ReadInt16();
            if (light == byte.MaxValue)
            {
              while (num7 > 0)
              {
                --num7;
                ++y;
                if (flag)
                {
                  int num8;
                  if ((double) y < Main.worldSurface)
                  {
                    flag = true;
                    int num9 = (int) (byte) (256.0 * ((double) y / Main.worldSurface));
                    num8 = (int) MapHelper.skyPosition + num9;
                  }
                  else if ((double) y < Main.rockLayer)
                  {
                    flag = true;
                    num8 = index + (int) MapHelper.dirtPosition;
                  }
                  else if (y < Main.UnderworldLayer)
                  {
                    flag = true;
                    num8 = index + (int) MapHelper.rockPosition;
                  }
                  else
                  {
                    flag = true;
                    num8 = (int) MapHelper.hellPosition;
                  }
                  tile.Type = (ushort) num8;
                }
                Main.Map.SetTile(x, y, ref tile);
              }
            }
            else
            {
              while (num7 > 0)
              {
                ++y;
                --num7;
                byte num10 = fileIO.ReadByte();
                if (num10 > (byte) 18)
                {
                  tile.Light = num10;
                  if (flag)
                  {
                    int num11;
                    if ((double) y < Main.worldSurface)
                    {
                      flag = true;
                      int num12 = (int) (byte) (256.0 * ((double) y / Main.worldSurface));
                      num11 = (int) MapHelper.skyPosition + num12;
                    }
                    else if ((double) y < Main.rockLayer)
                    {
                      flag = true;
                      num11 = index + (int) MapHelper.dirtPosition;
                    }
                    else if (y < Main.UnderworldLayer)
                    {
                      flag = true;
                      num11 = index + (int) MapHelper.rockPosition;
                    }
                    else
                    {
                      flag = true;
                      num11 = (int) MapHelper.hellPosition;
                    }
                    tile.Type = (ushort) num11;
                  }
                  Main.Map.SetTile(x, y, ref tile);
                }
              }
            }
          }
          else
          {
            int num13 = (int) fileIO.ReadInt16();
            y += num13;
          }
        }
      }
    }

    public static void LoadMapVersion2(BinaryReader fileIO, int release)
    {
      Main.MapFileMetadata = release < 135 ? FileMetadata.FromCurrentSettings(FileType.Map) : FileMetadata.Read(fileIO, FileType.Map);
      fileIO.ReadString();
      int num1 = fileIO.ReadInt32();
      int num2 = fileIO.ReadInt32();
      int num3 = fileIO.ReadInt32();
      int worldId = Main.worldID;
      if (num1 != worldId || num3 != Main.maxTilesX || num2 != Main.maxTilesY)
        throw new Exception("Map meta-data is invalid.");
      short length1 = fileIO.ReadInt16();
      short length2 = fileIO.ReadInt16();
      short num4 = fileIO.ReadInt16();
      short num5 = fileIO.ReadInt16();
      short num6 = fileIO.ReadInt16();
      short num7 = fileIO.ReadInt16();
      bool[] flagArray1 = new bool[(int) length1];
      byte num8 = 0;
      byte num9 = 128;
      for (int index = 0; index < (int) length1; ++index)
      {
        if (num9 == (byte) 128)
        {
          num8 = fileIO.ReadByte();
          num9 = (byte) 1;
        }
        else
          num9 <<= 1;
        if (((int) num8 & (int) num9) == (int) num9)
          flagArray1[index] = true;
      }
      bool[] flagArray2 = new bool[(int) length2];
      byte num10 = 0;
      byte num11 = 128;
      for (int index = 0; index < (int) length2; ++index)
      {
        if (num11 == (byte) 128)
        {
          num10 = fileIO.ReadByte();
          num11 = (byte) 1;
        }
        else
          num11 <<= 1;
        if (((int) num10 & (int) num11) == (int) num11)
          flagArray2[index] = true;
      }
      byte[] numArray1 = new byte[(int) length1];
      ushort num12 = 0;
      for (int index = 0; index < (int) length1; ++index)
      {
        numArray1[index] = !flagArray1[index] ? (byte) 1 : fileIO.ReadByte();
        num12 += (ushort) numArray1[index];
      }
      byte[] numArray2 = new byte[(int) length2];
      ushort num13 = 0;
      for (int index = 0; index < (int) length2; ++index)
      {
        numArray2[index] = !flagArray2[index] ? (byte) 1 : fileIO.ReadByte();
        num13 += (ushort) numArray2[index];
      }
      ushort[] numArray3 = new ushort[(int) num12 + (int) num13 + (int) num4 + (int) num5 + (int) num6 + (int) num7 + 2];
      numArray3[0] = (ushort) 0;
      ushort num14 = 1;
      ushort index1 = 1;
      ushort num15 = index1;
      for (int index2 = 0; index2 < (int) TileID.Count; ++index2)
      {
        if (index2 < (int) length1)
        {
          int num16 = (int) numArray1[index2];
          int tileOptionCount = MapHelper.tileOptionCounts[index2];
          for (int index3 = 0; index3 < tileOptionCount; ++index3)
          {
            if (index3 < num16)
            {
              numArray3[(int) index1] = num14;
              ++index1;
            }
            ++num14;
          }
        }
        else
          num14 += (ushort) MapHelper.tileOptionCounts[index2];
      }
      ushort num17 = index1;
      for (int index4 = 0; index4 < (int) WallID.Count; ++index4)
      {
        if (index4 < (int) length2)
        {
          int num18 = (int) numArray2[index4];
          int wallOptionCount = MapHelper.wallOptionCounts[index4];
          for (int index5 = 0; index5 < wallOptionCount; ++index5)
          {
            if (index5 < num18)
            {
              numArray3[(int) index1] = num14;
              ++index1;
            }
            ++num14;
          }
        }
        else
          num14 += (ushort) MapHelper.wallOptionCounts[index4];
      }
      ushort num19 = index1;
      for (int index6 = 0; index6 < 4; ++index6)
      {
        if (index6 < (int) num4)
        {
          numArray3[(int) index1] = num14;
          ++index1;
        }
        ++num14;
      }
      ushort num20 = index1;
      for (int index7 = 0; index7 < 256; ++index7)
      {
        if (index7 < (int) num5)
        {
          numArray3[(int) index1] = num14;
          ++index1;
        }
        ++num14;
      }
      ushort num21 = index1;
      for (int index8 = 0; index8 < 256; ++index8)
      {
        if (index8 < (int) num6)
        {
          numArray3[(int) index1] = num14;
          ++index1;
        }
        ++num14;
      }
      ushort num22 = index1;
      for (int index9 = 0; index9 < 256; ++index9)
      {
        if (index9 < (int) num7)
        {
          numArray3[(int) index1] = num14;
          ++index1;
        }
        ++num14;
      }
      ushort num23 = index1;
      numArray3[(int) index1] = num14;
      BinaryReader binaryReader = release < 93 ? new BinaryReader(fileIO.BaseStream) : new BinaryReader((Stream) new DeflateStream(fileIO.BaseStream, (CompressionMode) 1));
      for (int y = 0; y < Main.maxTilesY; ++y)
      {
        float num24 = (float) y / (float) Main.maxTilesY;
        Main.statusText = Lang.gen[67].Value + " " + (object) (int) ((double) num24 * 100.0 + 1.0) + "%";
        for (int x = 0; x < Main.maxTilesX; ++x)
        {
          byte num25 = binaryReader.ReadByte();
          byte num26 = ((int) num25 & 1) != 1 ? (byte) 0 : binaryReader.ReadByte();
          if (((int) num26 & 1) == 1)
          {
            int num27 = (int) binaryReader.ReadByte();
          }
          byte num28 = (byte) (((int) num25 & 14) >> 1);
          bool flag;
          switch (num28)
          {
            case 0:
              flag = false;
              break;
            case 1:
            case 2:
            case 7:
              flag = true;
              break;
            case 3:
            case 4:
            case 5:
              flag = false;
              break;
            case 6:
              flag = false;
              break;
            default:
              flag = false;
              break;
          }
          ushort index10 = !flag ? (ushort) 0 : (((int) num25 & 16) != 16 ? (ushort) binaryReader.ReadByte() : binaryReader.ReadUInt16());
          byte light = ((int) num25 & 32) != 32 ? byte.MaxValue : binaryReader.ReadByte();
          int num29;
          switch ((byte) (((int) num25 & 192) >> 6))
          {
            case 0:
              num29 = 0;
              break;
            case 1:
              num29 = (int) binaryReader.ReadByte();
              break;
            case 2:
              num29 = (int) binaryReader.ReadInt16();
              break;
            default:
              num29 = 0;
              break;
          }
          switch (num28)
          {
            case 0:
              x += num29;
              break;
            case 1:
              index10 += num15;
              goto default;
            case 2:
              index10 += num17;
              goto default;
            case 3:
            case 4:
            case 5:
              int num30 = (int) num28 - 3;
              if (((int) num26 & 64) == 64)
                num30 = 3;
              index10 += (ushort) ((uint) num19 + (uint) num30);
              goto default;
            case 6:
              if ((double) y < Main.worldSurface)
              {
                ushort num31 = (ushort) ((double) num5 * ((double) y / Main.worldSurface));
                index10 += (ushort) ((uint) num20 + (uint) num31);
                goto default;
              }
              else
              {
                index10 = num23;
                goto default;
              }
            case 7:
              if ((double) y < Main.rockLayer)
              {
                index10 += num21;
                goto default;
              }
              else
              {
                index10 += num22;
                goto default;
              }
            default:
              MapTile tile = MapTile.Create(numArray3[(int) index10], light, (byte) ((int) num26 >> 1 & 31));
              Main.Map.SetTile(x, y, ref tile);
              if (light == byte.MaxValue)
              {
                for (; num29 > 0; --num29)
                {
                  ++x;
                  Main.Map.SetTile(x, y, ref tile);
                }
                break;
              }
              for (; num29 > 0; --num29)
              {
                ++x;
                tile = tile.WithLight(binaryReader.ReadByte());
                Main.Map.SetTile(x, y, ref tile);
              }
              break;
          }
        }
      }
      binaryReader.Close();
    }

    private struct OldMapHelper
    {
      public byte misc;
      public byte misc2;

      public bool active() => ((int) this.misc & 1) == 1;

      public bool water() => ((int) this.misc & 2) == 2;

      public bool lava() => ((int) this.misc & 4) == 4;

      public bool honey() => ((int) this.misc2 & 64) == 64;

      public bool changed() => ((int) this.misc & 8) == 8;

      public bool wall() => ((int) this.misc & 16) == 16;

      public byte option()
      {
        byte num = 0;
        if (((int) this.misc & 32) == 32)
          ++num;
        if (((int) this.misc & 64) == 64)
          num += (byte) 2;
        if (((int) this.misc & 128) == 128)
          num += (byte) 4;
        if (((int) this.misc2 & 1) == 1)
          num += (byte) 8;
        return num;
      }

      public byte color() => (byte) (((int) this.misc2 & 30) >> 1);
    }
  }
}
