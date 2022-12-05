// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Profiles
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria.Localization;

namespace Terraria.GameContent
{
  public class Profiles
  {
    public class StackedNPCProfile : ITownNPCProfile
    {
      internal ITownNPCProfile[] _profiles;

      public StackedNPCProfile(params ITownNPCProfile[] profilesInOrderOfVariants) => this._profiles = profilesInOrderOfVariants;

      public int RollVariation() => 0;

      public string GetNameForVariant(NPC npc)
      {
        int index = 0;
        if (this._profiles.IndexInRange<ITownNPCProfile>(npc.townNpcVariationIndex))
          index = npc.townNpcVariationIndex;
        return this._profiles[index].GetNameForVariant(npc);
      }

      public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
      {
        int index = 0;
        if (this._profiles.IndexInRange<ITownNPCProfile>(npc.townNpcVariationIndex))
          index = npc.townNpcVariationIndex;
        return this._profiles[index].GetTextureNPCShouldUse(npc);
      }

      public int GetHeadTextureIndex(NPC npc)
      {
        int index = 0;
        if (this._profiles.IndexInRange<ITownNPCProfile>(npc.townNpcVariationIndex))
          index = npc.townNpcVariationIndex;
        return this._profiles[index].GetHeadTextureIndex(npc);
      }
    }

    public class LegacyNPCProfile : ITownNPCProfile
    {
      private string _rootFilePath;
      private int _defaultVariationHeadIndex;
      internal Asset<Texture2D> _defaultNoAlt;
      internal Asset<Texture2D> _defaultParty;

      public LegacyNPCProfile(
        string npcFileTitleFilePath,
        int defaultHeadIndex,
        bool includeDefault = true,
        bool uniquePartyTexture = true)
      {
        this._rootFilePath = npcFileTitleFilePath;
        this._defaultVariationHeadIndex = defaultHeadIndex;
        if (Main.dedServ)
          return;
        this._defaultNoAlt = Main.Assets.Request<Texture2D>(npcFileTitleFilePath + (includeDefault ? "_Default" : ""), (AssetRequestMode) 0);
        if (uniquePartyTexture)
          this._defaultParty = Main.Assets.Request<Texture2D>(npcFileTitleFilePath + (includeDefault ? "_Default_Party" : "_Party"), (AssetRequestMode) 0);
        else
          this._defaultParty = this._defaultNoAlt;
      }

      public int RollVariation() => 0;

      public string GetNameForVariant(NPC npc) => NPC.getNewNPCName(npc.type);

      public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc) => npc.IsABestiaryIconDummy && !npc.ForcePartyHatOn || npc.altTexture != 1 ? this._defaultNoAlt : this._defaultParty;

      public int GetHeadTextureIndex(NPC npc) => this._defaultVariationHeadIndex;
    }

    public class TransformableNPCProfile : ITownNPCProfile
    {
      private string _rootFilePath;
      private int _defaultVariationHeadIndex;
      internal Asset<Texture2D> _defaultNoAlt;
      internal Asset<Texture2D> _defaultTransformed;
      internal Asset<Texture2D> _defaultCredits;

      public TransformableNPCProfile(
        string npcFileTitleFilePath,
        int defaultHeadIndex,
        bool includeCredits = true)
      {
        this._rootFilePath = npcFileTitleFilePath;
        this._defaultVariationHeadIndex = defaultHeadIndex;
        if (Main.dedServ)
          return;
        this._defaultNoAlt = Main.Assets.Request<Texture2D>(npcFileTitleFilePath + "_Default", (AssetRequestMode) 0);
        this._defaultTransformed = Main.Assets.Request<Texture2D>(npcFileTitleFilePath + "_Default_Transformed", (AssetRequestMode) 0);
        if (!includeCredits)
          return;
        this._defaultCredits = Main.Assets.Request<Texture2D>(npcFileTitleFilePath + "_Default_Credits", (AssetRequestMode) 0);
      }

      public int RollVariation() => 0;

      public string GetNameForVariant(NPC npc) => NPC.getNewNPCName(npc.type);

      public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
      {
        if (npc.altTexture == 3 && this._defaultCredits != null)
          return this._defaultCredits;
        return npc.IsABestiaryIconDummy || npc.altTexture != 2 ? this._defaultNoAlt : this._defaultTransformed;
      }

      public int GetHeadTextureIndex(NPC npc) => this._defaultVariationHeadIndex;
    }

    public class VariantNPCProfile : ITownNPCProfile
    {
      private string _rootFilePath;
      private string _npcBaseName;
      private int[] _variantHeadIDs;
      private string[] _variants;
      internal Dictionary<string, Asset<Texture2D>> _variantTextures = new Dictionary<string, Asset<Texture2D>>();

      public VariantNPCProfile(
        string npcFileTitleFilePath,
        string npcBaseName,
        int[] variantHeadIds,
        params string[] variantTextureNames)
      {
        this._rootFilePath = npcFileTitleFilePath;
        this._npcBaseName = npcBaseName;
        this._variantHeadIDs = variantHeadIds;
        this._variants = variantTextureNames;
        foreach (string variant in this._variants)
        {
          string key = this._rootFilePath + "_" + variant;
          if (!Main.dedServ)
            this._variantTextures[key] = Main.Assets.Request<Texture2D>(key, (AssetRequestMode) 0);
        }
      }

      public Profiles.VariantNPCProfile SetPartyTextures(params string[] variantTextureNames)
      {
        foreach (string variantTextureName in variantTextureNames)
        {
          string key = this._rootFilePath + "_" + variantTextureName + "_Party";
          if (!Main.dedServ)
            this._variantTextures[key] = Main.Assets.Request<Texture2D>(key, (AssetRequestMode) 0);
        }
        return this;
      }

      public int RollVariation() => Main.rand.Next(this._variants.Length);

      public string GetNameForVariant(NPC npc) => Language.RandomFromCategory(this._npcBaseName + "Names_" + this._variants[npc.townNpcVariationIndex], WorldGen.genRand).Value;

      public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
      {
        string key = this._rootFilePath + "_" + this._variants[npc.townNpcVariationIndex];
        return npc.IsABestiaryIconDummy || npc.altTexture != 1 || !this._variantTextures.ContainsKey(key + "_Party") ? this._variantTextures[key] : this._variantTextures[key + "_Party"];
      }

      public int GetHeadTextureIndex(NPC npc) => this._variantHeadIDs[npc.townNpcVariationIndex];
    }
  }
}
