// Decompiled with JetBrains decompiler
// Type: Terraria.Audio.LegacySoundPlayer
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using ReLogic.Content;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria.ID;

namespace Terraria.Audio
{
  public class LegacySoundPlayer
  {
    public Asset<SoundEffect>[] SoundDrip = new Asset<SoundEffect>[3];
    public SoundEffectInstance[] SoundInstanceDrip = new SoundEffectInstance[3];
    public Asset<SoundEffect>[] SoundLiquid = new Asset<SoundEffect>[2];
    public SoundEffectInstance[] SoundInstanceLiquid = new SoundEffectInstance[2];
    public Asset<SoundEffect>[] SoundMech = new Asset<SoundEffect>[1];
    public SoundEffectInstance[] SoundInstanceMech = new SoundEffectInstance[1];
    public Asset<SoundEffect>[] SoundDig = new Asset<SoundEffect>[3];
    public SoundEffectInstance[] SoundInstanceDig = new SoundEffectInstance[3];
    public Asset<SoundEffect>[] SoundThunder = new Asset<SoundEffect>[7];
    public SoundEffectInstance[] SoundInstanceThunder = new SoundEffectInstance[7];
    public Asset<SoundEffect>[] SoundResearch = new Asset<SoundEffect>[4];
    public SoundEffectInstance[] SoundInstanceResearch = new SoundEffectInstance[4];
    public Asset<SoundEffect>[] SoundTink = new Asset<SoundEffect>[3];
    public SoundEffectInstance[] SoundInstanceTink = new SoundEffectInstance[3];
    public Asset<SoundEffect>[] SoundCoin = new Asset<SoundEffect>[5];
    public SoundEffectInstance[] SoundInstanceCoin = new SoundEffectInstance[5];
    public Asset<SoundEffect>[] SoundPlayerHit = new Asset<SoundEffect>[3];
    public SoundEffectInstance[] SoundInstancePlayerHit = new SoundEffectInstance[3];
    public Asset<SoundEffect>[] SoundFemaleHit = new Asset<SoundEffect>[3];
    public SoundEffectInstance[] SoundInstanceFemaleHit = new SoundEffectInstance[3];
    public Asset<SoundEffect> SoundPlayerKilled;
    public SoundEffectInstance SoundInstancePlayerKilled;
    public Asset<SoundEffect> SoundGrass;
    public SoundEffectInstance SoundInstanceGrass;
    public Asset<SoundEffect> SoundGrab;
    public SoundEffectInstance SoundInstanceGrab;
    public Asset<SoundEffect> SoundPixie;
    public SoundEffectInstance SoundInstancePixie;
    public Asset<SoundEffect>[] SoundItem = new Asset<SoundEffect>[(int) SoundID.ItemSoundCount];
    public SoundEffectInstance[] SoundInstanceItem = new SoundEffectInstance[(int) SoundID.ItemSoundCount];
    public Asset<SoundEffect>[] SoundNpcHit = new Asset<SoundEffect>[58];
    public SoundEffectInstance[] SoundInstanceNpcHit = new SoundEffectInstance[58];
    public Asset<SoundEffect>[] SoundNpcKilled = new Asset<SoundEffect>[(int) SoundID.NPCDeathCount];
    public SoundEffectInstance[] SoundInstanceNpcKilled = new SoundEffectInstance[(int) SoundID.NPCDeathCount];
    public SoundEffectInstance SoundInstanceMoonlordCry;
    public Asset<SoundEffect> SoundDoorOpen;
    public SoundEffectInstance SoundInstanceDoorOpen;
    public Asset<SoundEffect> SoundDoorClosed;
    public SoundEffectInstance SoundInstanceDoorClosed;
    public Asset<SoundEffect> SoundMenuOpen;
    public SoundEffectInstance SoundInstanceMenuOpen;
    public Asset<SoundEffect> SoundMenuClose;
    public SoundEffectInstance SoundInstanceMenuClose;
    public Asset<SoundEffect> SoundMenuTick;
    public SoundEffectInstance SoundInstanceMenuTick;
    public Asset<SoundEffect> SoundShatter;
    public SoundEffectInstance SoundInstanceShatter;
    public Asset<SoundEffect> SoundCamera;
    public SoundEffectInstance SoundInstanceCamera;
    public Asset<SoundEffect>[] SoundZombie = new Asset<SoundEffect>[131];
    public SoundEffectInstance[] SoundInstanceZombie = new SoundEffectInstance[131];
    public Asset<SoundEffect>[] SoundRoar = new Asset<SoundEffect>[3];
    public SoundEffectInstance[] SoundInstanceRoar = new SoundEffectInstance[3];
    public Asset<SoundEffect>[] SoundSplash = new Asset<SoundEffect>[6];
    public SoundEffectInstance[] SoundInstanceSplash = new SoundEffectInstance[6];
    public Asset<SoundEffect> SoundDoubleJump;
    public SoundEffectInstance SoundInstanceDoubleJump;
    public Asset<SoundEffect> SoundRun;
    public SoundEffectInstance SoundInstanceRun;
    public Asset<SoundEffect> SoundCoins;
    public SoundEffectInstance SoundInstanceCoins;
    public Asset<SoundEffect> SoundUnlock;
    public SoundEffectInstance SoundInstanceUnlock;
    public Asset<SoundEffect> SoundChat;
    public SoundEffectInstance SoundInstanceChat;
    public Asset<SoundEffect> SoundMaxMana;
    public SoundEffectInstance SoundInstanceMaxMana;
    public Asset<SoundEffect> SoundDrown;
    public SoundEffectInstance SoundInstanceDrown;
    public Asset<SoundEffect>[] TrackableSounds;
    public SoundEffectInstance[] TrackableSoundInstances;
    private readonly IServiceProvider _services;
    private List<SoundEffectInstance> _trackedInstances;

    public LegacySoundPlayer(IServiceProvider services)
    {
      this._services = services;
      this._trackedInstances = new List<SoundEffectInstance>();
      this.LoadAll();
    }

    public void Reload() => this.CreateAllSoundInstances();

    private void LoadAll()
    {
      this.SoundMech[0] = this.Load("Sounds/Mech_0");
      this.SoundGrab = this.Load("Sounds/Grab");
      this.SoundPixie = this.Load("Sounds/Pixie");
      this.SoundDig[0] = this.Load("Sounds/Dig_0");
      this.SoundDig[1] = this.Load("Sounds/Dig_1");
      this.SoundDig[2] = this.Load("Sounds/Dig_2");
      this.SoundThunder[0] = this.Load("Sounds/Thunder_0");
      this.SoundThunder[1] = this.Load("Sounds/Thunder_1");
      this.SoundThunder[2] = this.Load("Sounds/Thunder_2");
      this.SoundThunder[3] = this.Load("Sounds/Thunder_3");
      this.SoundThunder[4] = this.Load("Sounds/Thunder_4");
      this.SoundThunder[5] = this.Load("Sounds/Thunder_5");
      this.SoundThunder[6] = this.Load("Sounds/Thunder_6");
      this.SoundResearch[0] = this.Load("Sounds/Research_0");
      this.SoundResearch[1] = this.Load("Sounds/Research_1");
      this.SoundResearch[2] = this.Load("Sounds/Research_2");
      this.SoundResearch[3] = this.Load("Sounds/Research_3");
      this.SoundTink[0] = this.Load("Sounds/Tink_0");
      this.SoundTink[1] = this.Load("Sounds/Tink_1");
      this.SoundTink[2] = this.Load("Sounds/Tink_2");
      this.SoundPlayerHit[0] = this.Load("Sounds/Player_Hit_0");
      this.SoundPlayerHit[1] = this.Load("Sounds/Player_Hit_1");
      this.SoundPlayerHit[2] = this.Load("Sounds/Player_Hit_2");
      this.SoundFemaleHit[0] = this.Load("Sounds/Female_Hit_0");
      this.SoundFemaleHit[1] = this.Load("Sounds/Female_Hit_1");
      this.SoundFemaleHit[2] = this.Load("Sounds/Female_Hit_2");
      this.SoundPlayerKilled = this.Load("Sounds/Player_Killed");
      this.SoundChat = this.Load("Sounds/Chat");
      this.SoundGrass = this.Load("Sounds/Grass");
      this.SoundDoorOpen = this.Load("Sounds/Door_Opened");
      this.SoundDoorClosed = this.Load("Sounds/Door_Closed");
      this.SoundMenuTick = this.Load("Sounds/Menu_Tick");
      this.SoundMenuOpen = this.Load("Sounds/Menu_Open");
      this.SoundMenuClose = this.Load("Sounds/Menu_Close");
      this.SoundShatter = this.Load("Sounds/Shatter");
      this.SoundCamera = this.Load("Sounds/Camera");
      for (int index = 0; index < this.SoundCoin.Length; ++index)
        this.SoundCoin[index] = this.Load("Sounds/Coin_" + (object) index);
      for (int index = 0; index < this.SoundDrip.Length; ++index)
        this.SoundDrip[index] = this.Load("Sounds/Drip_" + (object) index);
      for (int index = 0; index < this.SoundZombie.Length; ++index)
        this.SoundZombie[index] = this.Load("Sounds/Zombie_" + (object) index);
      for (int index = 0; index < this.SoundLiquid.Length; ++index)
        this.SoundLiquid[index] = this.Load("Sounds/Liquid_" + (object) index);
      for (int index = 0; index < this.SoundRoar.Length; ++index)
        this.SoundRoar[index] = this.Load("Sounds/Roar_" + (object) index);
      for (int index = 0; index < this.SoundSplash.Length; ++index)
        this.SoundSplash[index] = this.Load("Sounds/Splash_" + (object) index);
      this.SoundDoubleJump = this.Load("Sounds/Double_Jump");
      this.SoundRun = this.Load("Sounds/Run");
      this.SoundCoins = this.Load("Sounds/Coins");
      this.SoundUnlock = this.Load("Sounds/Unlock");
      this.SoundMaxMana = this.Load("Sounds/MaxMana");
      this.SoundDrown = this.Load("Sounds/Drown");
      for (int index = 1; index < this.SoundItem.Length; ++index)
        this.SoundItem[index] = this.Load("Sounds/Item_" + (object) index);
      for (int index = 1; index < this.SoundNpcHit.Length; ++index)
        this.SoundNpcHit[index] = this.Load("Sounds/NPC_Hit_" + (object) index);
      for (int index = 1; index < this.SoundNpcKilled.Length; ++index)
        this.SoundNpcKilled[index] = this.Load("Sounds/NPC_Killed_" + (object) index);
      this.TrackableSounds = new Asset<SoundEffect>[SoundID.TrackableLegacySoundCount];
      this.TrackableSoundInstances = new SoundEffectInstance[this.TrackableSounds.Length];
      for (int id = 0; id < this.TrackableSounds.Length; ++id)
        this.TrackableSounds[id] = this.Load("Sounds/Custom" + Path.DirectorySeparatorChar.ToString() + SoundID.GetTrackableLegacySoundPath(id));
    }

    public void CreateAllSoundInstances()
    {
      foreach (SoundEffectInstance trackedInstance in this._trackedInstances)
        trackedInstance.Dispose();
      this._trackedInstances.Clear();
      this.SoundInstanceMech[0] = this.CreateInstance(this.SoundMech[0]);
      this.SoundInstanceGrab = this.CreateInstance(this.SoundGrab);
      this.SoundInstancePixie = this.CreateInstance(this.SoundGrab);
      this.SoundInstanceDig[0] = this.CreateInstance(this.SoundDig[0]);
      this.SoundInstanceDig[1] = this.CreateInstance(this.SoundDig[1]);
      this.SoundInstanceDig[2] = this.CreateInstance(this.SoundDig[2]);
      this.SoundInstanceTink[0] = this.CreateInstance(this.SoundTink[0]);
      this.SoundInstanceTink[1] = this.CreateInstance(this.SoundTink[1]);
      this.SoundInstanceTink[2] = this.CreateInstance(this.SoundTink[2]);
      this.SoundInstancePlayerHit[0] = this.CreateInstance(this.SoundPlayerHit[0]);
      this.SoundInstancePlayerHit[1] = this.CreateInstance(this.SoundPlayerHit[1]);
      this.SoundInstancePlayerHit[2] = this.CreateInstance(this.SoundPlayerHit[2]);
      this.SoundInstanceFemaleHit[0] = this.CreateInstance(this.SoundFemaleHit[0]);
      this.SoundInstanceFemaleHit[1] = this.CreateInstance(this.SoundFemaleHit[1]);
      this.SoundInstanceFemaleHit[2] = this.CreateInstance(this.SoundFemaleHit[2]);
      this.SoundInstancePlayerKilled = this.CreateInstance(this.SoundPlayerKilled);
      this.SoundInstanceChat = this.CreateInstance(this.SoundChat);
      this.SoundInstanceGrass = this.CreateInstance(this.SoundGrass);
      this.SoundInstanceDoorOpen = this.CreateInstance(this.SoundDoorOpen);
      this.SoundInstanceDoorClosed = this.CreateInstance(this.SoundDoorClosed);
      this.SoundInstanceMenuTick = this.CreateInstance(this.SoundMenuTick);
      this.SoundInstanceMenuOpen = this.CreateInstance(this.SoundMenuOpen);
      this.SoundInstanceMenuClose = this.CreateInstance(this.SoundMenuClose);
      this.SoundInstanceShatter = this.CreateInstance(this.SoundShatter);
      this.SoundInstanceCamera = this.CreateInstance(this.SoundCamera);
      this.SoundInstanceSplash[0] = this.CreateInstance(this.SoundRoar[0]);
      this.SoundInstanceSplash[1] = this.CreateInstance(this.SoundSplash[1]);
      this.SoundInstanceDoubleJump = this.CreateInstance(this.SoundRoar[0]);
      this.SoundInstanceRun = this.CreateInstance(this.SoundRun);
      this.SoundInstanceCoins = this.CreateInstance(this.SoundCoins);
      this.SoundInstanceUnlock = this.CreateInstance(this.SoundUnlock);
      this.SoundInstanceMaxMana = this.CreateInstance(this.SoundMaxMana);
      this.SoundInstanceDrown = this.CreateInstance(this.SoundDrown);
      this.SoundInstanceMoonlordCry = this.CreateInstance(this.SoundNpcKilled[10]);
      for (int index = 0; index < this.SoundThunder.Length; ++index)
        this.SoundInstanceThunder[index] = this.CreateInstance(this.SoundThunder[index]);
      for (int index = 0; index < this.SoundResearch.Length; ++index)
        this.SoundInstanceResearch[index] = this.CreateInstance(this.SoundResearch[index]);
      for (int index = 0; index < this.SoundCoin.Length; ++index)
        this.SoundInstanceCoin[index] = this.CreateInstance(this.SoundCoin[index]);
      for (int index = 0; index < this.SoundDrip.Length; ++index)
        this.SoundInstanceDrip[index] = this.CreateInstance(this.SoundDrip[index]);
      for (int index = 0; index < this.SoundZombie.Length; ++index)
        this.SoundInstanceZombie[index] = this.CreateInstance(this.SoundZombie[index]);
      for (int index = 0; index < this.SoundLiquid.Length; ++index)
        this.SoundInstanceLiquid[index] = this.CreateInstance(this.SoundLiquid[index]);
      for (int index = 0; index < this.SoundRoar.Length; ++index)
        this.SoundInstanceRoar[index] = this.CreateInstance(this.SoundRoar[index]);
      for (int index = 1; index < this.SoundItem.Length; ++index)
        this.SoundInstanceItem[index] = this.CreateInstance(this.SoundItem[index]);
      for (int index = 1; index < this.SoundNpcHit.Length; ++index)
        this.SoundInstanceNpcHit[index] = this.CreateInstance(this.SoundNpcHit[index]);
      for (int index = 1; index < this.SoundNpcKilled.Length; ++index)
        this.SoundInstanceNpcKilled[index] = this.CreateInstance(this.SoundNpcKilled[index]);
      for (int index = 0; index < this.TrackableSounds.Length; ++index)
        this.TrackableSoundInstances[index] = this.CreateInstance(this.TrackableSounds[index]);
    }

    private SoundEffectInstance CreateInstance(Asset<SoundEffect> asset)
    {
      SoundEffectInstance instance = asset.Value.CreateInstance();
      this._trackedInstances.Add(instance);
      return instance;
    }

    private Asset<SoundEffect> Load(string assetName) => XnaExtensions.Get<IAssetRepository>(this._services).Request<SoundEffect>(assetName, (AssetRequestMode) 2);

    public SoundEffectInstance PlaySound(
      int type,
      int x = -1,
      int y = -1,
      int Style = 1,
      float volumeScale = 1f,
      float pitchOffset = 0.0f)
    {
      int index1 = Style;
      try
      {
        if (Main.dedServ || (double) Main.soundVolume == 0.0 && (type < 30 || type > 35))
          return (SoundEffectInstance) null;
        bool flag = false;
        float num1 = 1f;
        float num2 = 0.0f;
        if (x == -1 || y == -1)
        {
          flag = true;
        }
        else
        {
          if (WorldGen.gen || Main.netMode == 2)
            return (SoundEffectInstance) null;
          Vector2 vector2 = new Vector2(Main.screenPosition.X + (float) Main.screenWidth * 0.5f, Main.screenPosition.Y + (float) Main.screenHeight * 0.5f);
          double num3 = (double) Math.Abs((float) x - vector2.X);
          float num4 = Math.Abs((float) y - vector2.Y);
          float num5 = (float) Math.Sqrt(num3 * num3 + (double) num4 * (double) num4);
          int num6 = 2500;
          if ((double) num5 < (double) num6)
          {
            flag = true;
            num2 = type != 43 ? (float) (((double) x - (double) vector2.X) / ((double) Main.screenWidth * 0.5)) : (float) (((double) x - (double) vector2.X) / 900.0);
            num1 = (float) (1.0 - (double) num5 / (double) num6);
          }
        }
        if ((double) num2 < -1.0)
          num2 = -1f;
        if ((double) num2 > 1.0)
          num2 = 1f;
        if ((double) num1 > 1.0)
          num1 = 1f;
        if ((double) num1 <= 0.0 && (type < 34 || type > 35 || type > 39))
          return (SoundEffectInstance) null;
        if (flag)
        {
          float num7;
          if (this.DoesSoundScaleWithAmbientVolume(type))
          {
            num7 = num1 * (Main.ambientVolume * (Main.gameInactive ? 0.0f : 1f));
            if (Main.gameMenu)
              num7 = 0.0f;
          }
          else
            num7 = num1 * Main.soundVolume;
          if ((double) num7 > 1.0)
            num7 = 1f;
          if ((double) num7 <= 0.0 && (type < 30 || type > 35) && type != 39)
            return (SoundEffectInstance) null;
          SoundEffectInstance sound = (SoundEffectInstance) null;
          if (type == 0)
          {
            int index2 = Main.rand.Next(3);
            if (this.SoundInstanceDig[index2] != null)
              this.SoundInstanceDig[index2].Stop();
            this.SoundInstanceDig[index2] = this.SoundDig[index2].Value.CreateInstance();
            this.SoundInstanceDig[index2].Volume = num7;
            this.SoundInstanceDig[index2].Pan = num2;
            this.SoundInstanceDig[index2].Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this.SoundInstanceDig[index2];
          }
          else if (type == 43)
          {
            int index3 = Main.rand.Next(this.SoundThunder.Length);
            for (int index4 = 0; index4 < this.SoundThunder.Length && this.SoundInstanceThunder[index3] != null && this.SoundInstanceThunder[index3].State == SoundState.Playing; ++index4)
              index3 = Main.rand.Next(this.SoundThunder.Length);
            if (this.SoundInstanceThunder[index3] != null)
              this.SoundInstanceThunder[index3].Stop();
            this.SoundInstanceThunder[index3] = this.SoundThunder[index3].Value.CreateInstance();
            this.SoundInstanceThunder[index3].Volume = num7;
            this.SoundInstanceThunder[index3].Pan = num2;
            this.SoundInstanceThunder[index3].Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this.SoundInstanceThunder[index3];
          }
          else if (type == 63)
          {
            int index5 = Main.rand.Next(1, 4);
            if (this.SoundInstanceResearch[index5] != null)
              this.SoundInstanceResearch[index5].Stop();
            this.SoundInstanceResearch[index5] = this.SoundResearch[index5].Value.CreateInstance();
            this.SoundInstanceResearch[index5].Volume = num7;
            this.SoundInstanceResearch[index5].Pan = num2;
            sound = this.SoundInstanceResearch[index5];
          }
          else if (type == 64)
          {
            if (this.SoundInstanceResearch[0] != null)
              this.SoundInstanceResearch[0].Stop();
            this.SoundInstanceResearch[0] = this.SoundResearch[0].Value.CreateInstance();
            this.SoundInstanceResearch[0].Volume = num7;
            this.SoundInstanceResearch[0].Pan = num2;
            sound = this.SoundInstanceResearch[0];
          }
          else if (type == 1)
          {
            int index6 = Main.rand.Next(3);
            if (this.SoundInstancePlayerHit[index6] != null)
              this.SoundInstancePlayerHit[index6].Stop();
            this.SoundInstancePlayerHit[index6] = this.SoundPlayerHit[index6].Value.CreateInstance();
            this.SoundInstancePlayerHit[index6].Volume = num7;
            this.SoundInstancePlayerHit[index6].Pan = num2;
            sound = this.SoundInstancePlayerHit[index6];
          }
          else if (type == 2)
          {
            if (index1 == 176)
              num7 *= 0.9f;
            if (index1 == 129)
              num7 *= 0.6f;
            if (index1 == 123)
              num7 *= 0.5f;
            if (index1 == 124 || index1 == 125)
              num7 *= 0.65f;
            if (index1 == 116)
              num7 *= 0.5f;
            if (index1 == 1)
            {
              int num8 = Main.rand.Next(3);
              if (num8 == 1)
                index1 = 18;
              if (num8 == 2)
                index1 = 19;
            }
            else if (index1 == 55 || index1 == 53)
            {
              num7 *= 0.75f;
              if (index1 == 55)
                num7 *= 0.75f;
              if (this.SoundInstanceItem[index1] != null && this.SoundInstanceItem[index1].State == SoundState.Playing)
                return (SoundEffectInstance) null;
            }
            else if (index1 == 37)
              num7 *= 0.5f;
            else if (index1 == 52)
              num7 *= 0.35f;
            else if (index1 == 157)
              num7 *= 0.7f;
            else if (index1 == 158)
              num7 *= 0.8f;
            if (index1 == 159)
            {
              if (this.SoundInstanceItem[index1] != null && this.SoundInstanceItem[index1].State == SoundState.Playing)
                return (SoundEffectInstance) null;
              num7 *= 0.75f;
            }
            else if (index1 != 9 && index1 != 10 && index1 != 24 && index1 != 26 && index1 != 34 && index1 != 43 && index1 != 103 && index1 != 156 && index1 != 162 && this.SoundInstanceItem[index1] != null)
              this.SoundInstanceItem[index1].Stop();
            this.SoundInstanceItem[index1] = this.SoundItem[index1].Value.CreateInstance();
            this.SoundInstanceItem[index1].Volume = num7;
            this.SoundInstanceItem[index1].Pan = num2;
            switch (index1)
            {
              case 53:
                this.SoundInstanceItem[index1].Pitch = (float) Main.rand.Next(-20, -11) * 0.02f;
                break;
              case 55:
                this.SoundInstanceItem[index1].Pitch = (float) -Main.rand.Next(-20, -11) * 0.02f;
                break;
              case 132:
                this.SoundInstanceItem[index1].Pitch = (float) Main.rand.Next(-20, 21) * (1f / 1000f);
                break;
              case 153:
                this.SoundInstanceItem[index1].Pitch = (float) Main.rand.Next(-50, 51) * (3f / 1000f);
                break;
              case 156:
                this.SoundInstanceItem[index1].Pitch = (float) Main.rand.Next(-50, 51) * (1f / 500f);
                this.SoundInstanceItem[index1].Volume *= 0.6f;
                break;
              default:
                this.SoundInstanceItem[index1].Pitch = (float) Main.rand.Next(-6, 7) * 0.01f;
                break;
            }
            if (index1 == 26 || index1 == 35 || index1 == 47)
            {
              this.SoundInstanceItem[index1].Volume = num7 * 0.75f;
              this.SoundInstanceItem[index1].Pitch = Main.musicPitch;
            }
            if (index1 == 169)
              this.SoundInstanceItem[index1].Pitch -= 0.8f;
            sound = this.SoundInstanceItem[index1];
          }
          else if (type == 3)
          {
            if (index1 >= 20 && index1 <= 54)
              num7 *= 0.5f;
            if (index1 == 57 && this.SoundInstanceNpcHit[index1] != null && this.SoundInstanceNpcHit[index1].State == SoundState.Playing)
              return (SoundEffectInstance) null;
            if (index1 == 57)
              num7 *= 0.6f;
            if (index1 == 55 || index1 == 56)
              num7 *= 0.5f;
            if (this.SoundInstanceNpcHit[index1] != null)
              this.SoundInstanceNpcHit[index1].Stop();
            this.SoundInstanceNpcHit[index1] = this.SoundNpcHit[index1].Value.CreateInstance();
            this.SoundInstanceNpcHit[index1].Volume = num7;
            this.SoundInstanceNpcHit[index1].Pan = num2;
            this.SoundInstanceNpcHit[index1].Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this.SoundInstanceNpcHit[index1];
          }
          else if (type == 4)
          {
            if (index1 >= 23 && index1 <= 57)
              num7 *= 0.5f;
            if (index1 == 61)
              num7 *= 0.6f;
            if (index1 == 62)
              num7 *= 0.6f;
            if (index1 == 10 && this.SoundInstanceNpcKilled[index1] != null && this.SoundInstanceNpcKilled[index1].State == SoundState.Playing)
              return (SoundEffectInstance) null;
            this.SoundInstanceNpcKilled[index1] = this.SoundNpcKilled[index1].Value.CreateInstance();
            this.SoundInstanceNpcKilled[index1].Volume = num7;
            this.SoundInstanceNpcKilled[index1].Pan = num2;
            this.SoundInstanceNpcKilled[index1].Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this.SoundInstanceNpcKilled[index1];
          }
          else if (type == 5)
          {
            if (this.SoundInstancePlayerKilled != null)
              this.SoundInstancePlayerKilled.Stop();
            this.SoundInstancePlayerKilled = this.SoundPlayerKilled.Value.CreateInstance();
            this.SoundInstancePlayerKilled.Volume = num7;
            this.SoundInstancePlayerKilled.Pan = num2;
            sound = this.SoundInstancePlayerKilled;
          }
          else if (type == 6)
          {
            if (this.SoundInstanceGrass != null)
              this.SoundInstanceGrass.Stop();
            this.SoundInstanceGrass = this.SoundGrass.Value.CreateInstance();
            this.SoundInstanceGrass.Volume = num7;
            this.SoundInstanceGrass.Pan = num2;
            this.SoundInstanceGrass.Pitch = (float) Main.rand.Next(-30, 31) * 0.01f;
            sound = this.SoundInstanceGrass;
          }
          else if (type == 7)
          {
            if (this.SoundInstanceGrab != null)
              this.SoundInstanceGrab.Stop();
            this.SoundInstanceGrab = this.SoundGrab.Value.CreateInstance();
            this.SoundInstanceGrab.Volume = num7;
            this.SoundInstanceGrab.Pan = num2;
            this.SoundInstanceGrab.Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this.SoundInstanceGrab;
          }
          else if (type == 8)
          {
            if (this.SoundInstanceDoorOpen != null)
              this.SoundInstanceDoorOpen.Stop();
            this.SoundInstanceDoorOpen = this.SoundDoorOpen.Value.CreateInstance();
            this.SoundInstanceDoorOpen.Volume = num7;
            this.SoundInstanceDoorOpen.Pan = num2;
            this.SoundInstanceDoorOpen.Pitch = (float) Main.rand.Next(-20, 21) * 0.01f;
            sound = this.SoundInstanceDoorOpen;
          }
          else if (type == 9)
          {
            if (this.SoundInstanceDoorClosed != null)
              this.SoundInstanceDoorClosed.Stop();
            this.SoundInstanceDoorClosed = this.SoundDoorClosed.Value.CreateInstance();
            this.SoundInstanceDoorClosed.Volume = num7;
            this.SoundInstanceDoorClosed.Pan = num2;
            this.SoundInstanceDoorClosed.Pitch = (float) Main.rand.Next(-20, 21) * 0.01f;
            sound = this.SoundInstanceDoorClosed;
          }
          else if (type == 10)
          {
            if (this.SoundInstanceMenuOpen != null)
              this.SoundInstanceMenuOpen.Stop();
            this.SoundInstanceMenuOpen = this.SoundMenuOpen.Value.CreateInstance();
            this.SoundInstanceMenuOpen.Volume = num7;
            this.SoundInstanceMenuOpen.Pan = num2;
            sound = this.SoundInstanceMenuOpen;
          }
          else if (type == 11)
          {
            if (this.SoundInstanceMenuClose != null)
              this.SoundInstanceMenuClose.Stop();
            this.SoundInstanceMenuClose = this.SoundMenuClose.Value.CreateInstance();
            this.SoundInstanceMenuClose.Volume = num7;
            this.SoundInstanceMenuClose.Pan = num2;
            sound = this.SoundInstanceMenuClose;
          }
          else if (type == 12)
          {
            if (Main.hasFocus)
            {
              if (this.SoundInstanceMenuTick != null)
                this.SoundInstanceMenuTick.Stop();
              this.SoundInstanceMenuTick = this.SoundMenuTick.Value.CreateInstance();
              this.SoundInstanceMenuTick.Volume = num7;
              this.SoundInstanceMenuTick.Pan = num2;
              sound = this.SoundInstanceMenuTick;
            }
          }
          else if (type == 13)
          {
            if (this.SoundInstanceShatter != null)
              this.SoundInstanceShatter.Stop();
            this.SoundInstanceShatter = this.SoundShatter.Value.CreateInstance();
            this.SoundInstanceShatter.Volume = num7;
            this.SoundInstanceShatter.Pan = num2;
            sound = this.SoundInstanceShatter;
          }
          else if (type == 14)
          {
            switch (Style)
            {
              case 489:
              case 586:
                int index7 = Main.rand.Next(21, 24);
                this.SoundInstanceZombie[index7] = this.SoundZombie[index7].Value.CreateInstance();
                this.SoundInstanceZombie[index7].Volume = num7 * 0.4f;
                this.SoundInstanceZombie[index7].Pan = num2;
                sound = this.SoundInstanceZombie[index7];
                break;
              case 542:
                int index8 = 7;
                this.SoundInstanceZombie[index8] = this.SoundZombie[index8].Value.CreateInstance();
                this.SoundInstanceZombie[index8].Volume = num7 * 0.4f;
                this.SoundInstanceZombie[index8].Pan = num2;
                sound = this.SoundInstanceZombie[index8];
                break;
              default:
                int index9 = Main.rand.Next(3);
                this.SoundInstanceZombie[index9] = this.SoundZombie[index9].Value.CreateInstance();
                this.SoundInstanceZombie[index9].Volume = num7 * 0.4f;
                this.SoundInstanceZombie[index9].Pan = num2;
                sound = this.SoundInstanceZombie[index9];
                break;
            }
          }
          else if (type == 15)
          {
            float num9 = 1f;
            if (index1 == 4)
            {
              index1 = 1;
              num9 = 0.25f;
            }
            if (this.SoundInstanceRoar[index1] == null || this.SoundInstanceRoar[index1].State == SoundState.Stopped)
            {
              this.SoundInstanceRoar[index1] = this.SoundRoar[index1].Value.CreateInstance();
              this.SoundInstanceRoar[index1].Volume = num7 * num9;
              this.SoundInstanceRoar[index1].Pan = num2;
              sound = this.SoundInstanceRoar[index1];
            }
          }
          else if (type == 16)
          {
            if (this.SoundInstanceDoubleJump != null)
              this.SoundInstanceDoubleJump.Stop();
            this.SoundInstanceDoubleJump = this.SoundDoubleJump.Value.CreateInstance();
            this.SoundInstanceDoubleJump.Volume = num7;
            this.SoundInstanceDoubleJump.Pan = num2;
            this.SoundInstanceDoubleJump.Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this.SoundInstanceDoubleJump;
          }
          else if (type == 17)
          {
            if (this.SoundInstanceRun != null)
              this.SoundInstanceRun.Stop();
            this.SoundInstanceRun = this.SoundRun.Value.CreateInstance();
            this.SoundInstanceRun.Volume = num7;
            this.SoundInstanceRun.Pan = num2;
            this.SoundInstanceRun.Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this.SoundInstanceRun;
          }
          else if (type == 18)
          {
            this.SoundInstanceCoins = this.SoundCoins.Value.CreateInstance();
            this.SoundInstanceCoins.Volume = num7;
            this.SoundInstanceCoins.Pan = num2;
            sound = this.SoundInstanceCoins;
          }
          else if (type == 19)
          {
            if (this.SoundInstanceSplash[index1] == null || this.SoundInstanceSplash[index1].State == SoundState.Stopped)
            {
              this.SoundInstanceSplash[index1] = this.SoundSplash[index1].Value.CreateInstance();
              if (index1 == 2 || index1 == 3)
                num7 *= 0.75f;
              if (index1 == 4 || index1 == 5)
              {
                num7 *= 0.75f;
                this.SoundInstanceSplash[index1].Pitch = (float) Main.rand.Next(-20, 1) * 0.01f;
              }
              else
                this.SoundInstanceSplash[index1].Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
              this.SoundInstanceSplash[index1].Volume = num7;
              this.SoundInstanceSplash[index1].Pan = num2;
              switch (index1)
              {
                case 4:
                  if (this.SoundInstanceSplash[5] == null || this.SoundInstanceSplash[5].State == SoundState.Stopped)
                  {
                    sound = this.SoundInstanceSplash[index1];
                    break;
                  }
                  break;
                case 5:
                  if (this.SoundInstanceSplash[4] == null || this.SoundInstanceSplash[4].State == SoundState.Stopped)
                  {
                    sound = this.SoundInstanceSplash[index1];
                    break;
                  }
                  break;
                default:
                  sound = this.SoundInstanceSplash[index1];
                  break;
              }
            }
          }
          else if (type == 20)
          {
            int index10 = Main.rand.Next(3);
            if (this.SoundInstanceFemaleHit[index10] != null)
              this.SoundInstanceFemaleHit[index10].Stop();
            this.SoundInstanceFemaleHit[index10] = this.SoundFemaleHit[index10].Value.CreateInstance();
            this.SoundInstanceFemaleHit[index10].Volume = num7;
            this.SoundInstanceFemaleHit[index10].Pan = num2;
            sound = this.SoundInstanceFemaleHit[index10];
          }
          else if (type == 21)
          {
            int index11 = Main.rand.Next(3);
            if (this.SoundInstanceTink[index11] != null)
              this.SoundInstanceTink[index11].Stop();
            this.SoundInstanceTink[index11] = this.SoundTink[index11].Value.CreateInstance();
            this.SoundInstanceTink[index11].Volume = num7;
            this.SoundInstanceTink[index11].Pan = num2;
            sound = this.SoundInstanceTink[index11];
          }
          else if (type == 22)
          {
            if (this.SoundInstanceUnlock != null)
              this.SoundInstanceUnlock.Stop();
            this.SoundInstanceUnlock = this.SoundUnlock.Value.CreateInstance();
            this.SoundInstanceUnlock.Volume = num7;
            this.SoundInstanceUnlock.Pan = num2;
            sound = this.SoundInstanceUnlock;
          }
          else if (type == 23)
          {
            if (this.SoundInstanceDrown != null)
              this.SoundInstanceDrown.Stop();
            this.SoundInstanceDrown = this.SoundDrown.Value.CreateInstance();
            this.SoundInstanceDrown.Volume = num7;
            this.SoundInstanceDrown.Pan = num2;
            sound = this.SoundInstanceDrown;
          }
          else if (type == 24)
          {
            this.SoundInstanceChat = this.SoundChat.Value.CreateInstance();
            this.SoundInstanceChat.Volume = num7;
            this.SoundInstanceChat.Pan = num2;
            sound = this.SoundInstanceChat;
          }
          else if (type == 25)
          {
            this.SoundInstanceMaxMana = this.SoundMaxMana.Value.CreateInstance();
            this.SoundInstanceMaxMana.Volume = num7;
            this.SoundInstanceMaxMana.Pan = num2;
            sound = this.SoundInstanceMaxMana;
          }
          else if (type == 26)
          {
            int index12 = Main.rand.Next(3, 5);
            this.SoundInstanceZombie[index12] = this.SoundZombie[index12].Value.CreateInstance();
            this.SoundInstanceZombie[index12].Volume = num7 * 0.9f;
            this.SoundInstanceZombie[index12].Pan = num2;
            this.SoundInstanceZombie[index12].Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this.SoundInstanceZombie[index12];
          }
          else if (type == 27)
          {
            if (this.SoundInstancePixie != null && this.SoundInstancePixie.State == SoundState.Playing)
            {
              this.SoundInstancePixie.Volume = num7;
              this.SoundInstancePixie.Pan = num2;
              this.SoundInstancePixie.Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
              return (SoundEffectInstance) null;
            }
            if (this.SoundInstancePixie != null)
              this.SoundInstancePixie.Stop();
            this.SoundInstancePixie = this.SoundPixie.Value.CreateInstance();
            this.SoundInstancePixie.Volume = num7;
            this.SoundInstancePixie.Pan = num2;
            this.SoundInstancePixie.Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this.SoundInstancePixie;
          }
          else if (type == 28)
          {
            if (this.SoundInstanceMech[index1] != null && this.SoundInstanceMech[index1].State == SoundState.Playing)
              return (SoundEffectInstance) null;
            this.SoundInstanceMech[index1] = this.SoundMech[index1].Value.CreateInstance();
            this.SoundInstanceMech[index1].Volume = num7;
            this.SoundInstanceMech[index1].Pan = num2;
            this.SoundInstanceMech[index1].Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this.SoundInstanceMech[index1];
          }
          else if (type == 29)
          {
            if (index1 >= 24 && index1 <= 87)
              num7 *= 0.5f;
            if (index1 >= 88 && index1 <= 91)
              num7 *= 0.7f;
            if (index1 >= 93 && index1 <= 99)
              num7 *= 0.4f;
            if (index1 == 92)
              num7 *= 0.5f;
            if (index1 == 103)
              num7 *= 0.4f;
            if (index1 == 104)
              num7 *= 0.55f;
            if (index1 == 100 || index1 == 101)
              num7 *= 0.25f;
            if (index1 == 102)
              num7 *= 0.4f;
            if (this.SoundInstanceZombie[index1] != null && this.SoundInstanceZombie[index1].State == SoundState.Playing)
              return (SoundEffectInstance) null;
            this.SoundInstanceZombie[index1] = this.SoundZombie[index1].Value.CreateInstance();
            this.SoundInstanceZombie[index1].Volume = num7;
            this.SoundInstanceZombie[index1].Pan = num2;
            this.SoundInstanceZombie[index1].Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this.SoundInstanceZombie[index1];
          }
          else if (type == 44)
          {
            int index13 = Main.rand.Next(106, 109);
            this.SoundInstanceZombie[index13] = this.SoundZombie[index13].Value.CreateInstance();
            this.SoundInstanceZombie[index13].Volume = num7 * 0.2f;
            this.SoundInstanceZombie[index13].Pan = num2;
            this.SoundInstanceZombie[index13].Pitch = (float) Main.rand.Next(-70, 1) * 0.01f;
            sound = this.SoundInstanceZombie[index13];
          }
          else if (type == 45)
          {
            int index14 = 109;
            if (this.SoundInstanceZombie[index14] != null && this.SoundInstanceZombie[index14].State == SoundState.Playing)
              return (SoundEffectInstance) null;
            this.SoundInstanceZombie[index14] = this.SoundZombie[index14].Value.CreateInstance();
            this.SoundInstanceZombie[index14].Volume = num7 * 0.3f;
            this.SoundInstanceZombie[index14].Pan = num2;
            this.SoundInstanceZombie[index14].Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this.SoundInstanceZombie[index14];
          }
          else if (type == 46)
          {
            if (this.SoundInstanceZombie[110] != null && this.SoundInstanceZombie[110].State == SoundState.Playing || this.SoundInstanceZombie[111] != null && this.SoundInstanceZombie[111].State == SoundState.Playing)
              return (SoundEffectInstance) null;
            int index15 = Main.rand.Next(110, 112);
            if (Main.rand.Next(300) == 0)
              index15 = Main.rand.Next(3) != 0 ? (Main.rand.Next(2) != 0 ? 112 : 113) : 114;
            this.SoundInstanceZombie[index15] = this.SoundZombie[index15].Value.CreateInstance();
            this.SoundInstanceZombie[index15].Volume = num7 * 0.9f;
            this.SoundInstanceZombie[index15].Pan = num2;
            this.SoundInstanceZombie[index15].Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this.SoundInstanceZombie[index15];
          }
          else if (type == 45)
          {
            int index16 = 109;
            this.SoundInstanceZombie[index16] = this.SoundZombie[index16].Value.CreateInstance();
            this.SoundInstanceZombie[index16].Volume = num7 * 0.2f;
            this.SoundInstanceZombie[index16].Pan = num2;
            this.SoundInstanceZombie[index16].Pitch = (float) Main.rand.Next(-70, 1) * 0.01f;
            sound = this.SoundInstanceZombie[index16];
          }
          else if (type == 30)
          {
            int index17 = Main.rand.Next(10, 12);
            if (Main.rand.Next(300) == 0)
            {
              index17 = 12;
              if (this.SoundInstanceZombie[index17] != null && this.SoundInstanceZombie[index17].State == SoundState.Playing)
                return (SoundEffectInstance) null;
            }
            this.SoundInstanceZombie[index17] = this.SoundZombie[index17].Value.CreateInstance();
            this.SoundInstanceZombie[index17].Volume = num7 * 0.75f;
            this.SoundInstanceZombie[index17].Pan = num2;
            this.SoundInstanceZombie[index17].Pitch = index17 == 12 ? (float) Main.rand.Next(-40, 21) * 0.01f : (float) Main.rand.Next(-70, 1) * 0.01f;
            sound = this.SoundInstanceZombie[index17];
          }
          else if (type == 31)
          {
            int index18 = 13;
            this.SoundInstanceZombie[index18] = this.SoundZombie[index18].Value.CreateInstance();
            this.SoundInstanceZombie[index18].Volume = num7 * 0.35f;
            this.SoundInstanceZombie[index18].Pan = num2;
            this.SoundInstanceZombie[index18].Pitch = (float) Main.rand.Next(-40, 21) * 0.01f;
            sound = this.SoundInstanceZombie[index18];
          }
          else if (type == 32)
          {
            if (this.SoundInstanceZombie[index1] != null && this.SoundInstanceZombie[index1].State == SoundState.Playing)
              return (SoundEffectInstance) null;
            this.SoundInstanceZombie[index1] = this.SoundZombie[index1].Value.CreateInstance();
            this.SoundInstanceZombie[index1].Volume = num7 * 0.15f;
            this.SoundInstanceZombie[index1].Pan = num2;
            this.SoundInstanceZombie[index1].Pitch = (float) Main.rand.Next(-70, 26) * 0.01f;
            sound = this.SoundInstanceZombie[index1];
          }
          else if (type == 67)
          {
            int index19 = Main.rand.Next(118, 121);
            if (this.SoundInstanceZombie[index19] != null && this.SoundInstanceZombie[index19].State == SoundState.Playing)
              return (SoundEffectInstance) null;
            this.SoundInstanceZombie[index19] = this.SoundZombie[index19].Value.CreateInstance();
            this.SoundInstanceZombie[index19].Volume = num7 * 0.3f;
            this.SoundInstanceZombie[index19].Pan = num2;
            this.SoundInstanceZombie[index19].Pitch = (float) Main.rand.Next(-5, 6) * 0.01f;
            sound = this.SoundInstanceZombie[index19];
          }
          else if (type == 68)
          {
            int index20 = Main.rand.Next(126, 129);
            if (this.SoundInstanceZombie[index20] != null && this.SoundInstanceZombie[index20].State == SoundState.Playing)
              return (SoundEffectInstance) null;
            this.SoundInstanceZombie[index20] = this.SoundZombie[index20].Value.CreateInstance();
            this.SoundInstanceZombie[index20].Volume = num7 * 0.22f;
            this.SoundInstanceZombie[index20].Pan = num2;
            this.SoundInstanceZombie[index20].Pitch = (float) Main.rand.Next(-5, 6) * 0.01f;
            sound = this.SoundInstanceZombie[index20];
          }
          else if (type == 69)
          {
            int index21 = Main.rand.Next(129, 131);
            if (this.SoundInstanceZombie[index21] != null && this.SoundInstanceZombie[index21].State == SoundState.Playing)
              return (SoundEffectInstance) null;
            this.SoundInstanceZombie[index21] = this.SoundZombie[index21].Value.CreateInstance();
            this.SoundInstanceZombie[index21].Volume = num7 * 0.2f;
            this.SoundInstanceZombie[index21].Pan = num2;
            this.SoundInstanceZombie[index21].Pitch = (float) Main.rand.Next(-5, 6) * 0.01f;
            sound = this.SoundInstanceZombie[index21];
          }
          else if (type == 66)
          {
            int index22 = Main.rand.Next(121, 124);
            if (this.SoundInstanceZombie[121] != null && this.SoundInstanceZombie[121].State == SoundState.Playing || this.SoundInstanceZombie[122] != null && this.SoundInstanceZombie[122].State == SoundState.Playing || this.SoundInstanceZombie[123] != null && this.SoundInstanceZombie[123].State == SoundState.Playing)
              return (SoundEffectInstance) null;
            this.SoundInstanceZombie[index22] = this.SoundZombie[index22].Value.CreateInstance();
            this.SoundInstanceZombie[index22].Volume = num7 * 0.45f;
            this.SoundInstanceZombie[index22].Pan = num2;
            this.SoundInstanceZombie[index22].Pitch = (float) Main.rand.Next(-15, 16) * 0.01f;
            sound = this.SoundInstanceZombie[index22];
          }
          else if (type == 33)
          {
            int index23 = 15;
            if (this.SoundInstanceZombie[index23] != null && this.SoundInstanceZombie[index23].State == SoundState.Playing)
              return (SoundEffectInstance) null;
            this.SoundInstanceZombie[index23] = this.SoundZombie[index23].Value.CreateInstance();
            this.SoundInstanceZombie[index23].Volume = num7 * 0.2f;
            this.SoundInstanceZombie[index23].Pan = num2;
            this.SoundInstanceZombie[index23].Pitch = (float) Main.rand.Next(-10, 31) * 0.01f;
            sound = this.SoundInstanceZombie[index23];
          }
          else if (type >= 47 && type <= 52)
          {
            int index24 = 133 + type - 47;
            for (int index25 = 133; index25 <= 138; ++index25)
            {
              if (this.SoundInstanceItem[index25] != null && this.SoundInstanceItem[index25].State == SoundState.Playing)
                this.SoundInstanceItem[index25].Stop();
            }
            this.SoundInstanceItem[index24] = this.SoundItem[index24].Value.CreateInstance();
            this.SoundInstanceItem[index24].Volume = num7 * 0.45f;
            this.SoundInstanceItem[index24].Pan = num2;
            sound = this.SoundInstanceItem[index24];
          }
          else if (type >= 53 && type <= 62)
          {
            int index26 = 139 + type - 53;
            if (this.SoundInstanceItem[index26] != null && this.SoundInstanceItem[index26].State == SoundState.Playing)
              this.SoundInstanceItem[index26].Stop();
            this.SoundInstanceItem[index26] = this.SoundItem[index26].Value.CreateInstance();
            this.SoundInstanceItem[index26].Volume = num7 * 0.7f;
            this.SoundInstanceItem[index26].Pan = num2;
            sound = this.SoundInstanceItem[index26];
          }
          else
          {
            switch (type)
            {
              case 34:
                float num10 = (float) index1 / 50f;
                if ((double) num10 > 1.0)
                  num10 = 1f;
                float num11 = num7 * num10 * 0.2f * (1f - Main.shimmerAlpha);
                if ((double) num11 <= 0.0 || x == -1 || y == -1)
                {
                  if (this.SoundInstanceLiquid[0] != null && this.SoundInstanceLiquid[0].State == SoundState.Playing)
                  {
                    this.SoundInstanceLiquid[0].Stop();
                    break;
                  }
                  break;
                }
                if (this.SoundInstanceLiquid[0] != null && this.SoundInstanceLiquid[0].State == SoundState.Playing)
                {
                  this.SoundInstanceLiquid[0].Volume = num11;
                  this.SoundInstanceLiquid[0].Pan = num2;
                  this.SoundInstanceLiquid[0].Pitch = -0.2f;
                  break;
                }
                this.SoundInstanceLiquid[0] = this.SoundLiquid[0].Value.CreateInstance();
                this.SoundInstanceLiquid[0].Volume = num11;
                this.SoundInstanceLiquid[0].Pan = num2;
                sound = this.SoundInstanceLiquid[0];
                break;
              case 35:
                float num12 = (float) index1 / 50f;
                if ((double) num12 > 1.0)
                  num12 = 1f;
                float num13 = num7 * num12 * 0.65f * (1f - Main.shimmerAlpha);
                if ((double) num13 <= 0.0 || x == -1 || y == -1)
                {
                  if (this.SoundInstanceLiquid[1] != null && this.SoundInstanceLiquid[1].State == SoundState.Playing)
                  {
                    this.SoundInstanceLiquid[1].Stop();
                    break;
                  }
                  break;
                }
                if (this.SoundInstanceLiquid[1] != null && this.SoundInstanceLiquid[1].State == SoundState.Playing)
                {
                  this.SoundInstanceLiquid[1].Volume = num13;
                  this.SoundInstanceLiquid[1].Pan = num2;
                  this.SoundInstanceLiquid[1].Pitch = -0.0f;
                  break;
                }
                this.SoundInstanceLiquid[1] = this.SoundLiquid[1].Value.CreateInstance();
                this.SoundInstanceLiquid[1].Volume = num13;
                this.SoundInstanceLiquid[1].Pan = num2;
                sound = this.SoundInstanceLiquid[1];
                break;
              case 36:
                int index27 = Style;
                if (Style == -1)
                  index27 = 0;
                this.SoundInstanceRoar[index27] = this.SoundRoar[index27].Value.CreateInstance();
                this.SoundInstanceRoar[index27].Volume = num7;
                this.SoundInstanceRoar[index27].Pan = num2;
                if (Style == -1)
                  this.SoundInstanceRoar[index27].Pitch += 0.6f;
                sound = this.SoundInstanceRoar[index27];
                break;
              case 37:
                int index28 = Main.rand.Next(57, 59);
                float num14 = !Main.starGame ? num7 * ((float) Style * 0.05f) : num7 * 0.15f;
                this.SoundInstanceItem[index28] = this.SoundItem[index28].Value.CreateInstance();
                this.SoundInstanceItem[index28].Volume = num14;
                this.SoundInstanceItem[index28].Pan = num2;
                this.SoundInstanceItem[index28].Pitch = (float) Main.rand.Next(-40, 41) * 0.01f;
                sound = this.SoundInstanceItem[index28];
                break;
              case 38:
                if (Main.starGame)
                  num7 *= 0.15f;
                int index29 = Main.rand.Next(5);
                this.SoundInstanceCoin[index29] = this.SoundCoin[index29].Value.CreateInstance();
                this.SoundInstanceCoin[index29].Volume = num7;
                this.SoundInstanceCoin[index29].Pan = num2;
                this.SoundInstanceCoin[index29].Pitch = (float) Main.rand.Next(-40, 41) * (1f / 500f);
                sound = this.SoundInstanceCoin[index29];
                break;
              case 39:
                int index30 = Style;
                this.SoundInstanceDrip[index30] = this.SoundDrip[index30].Value.CreateInstance();
                this.SoundInstanceDrip[index30].Volume = num7 * 0.5f;
                this.SoundInstanceDrip[index30].Pan = num2;
                this.SoundInstanceDrip[index30].Pitch = (float) Main.rand.Next(-30, 31) * 0.01f;
                sound = this.SoundInstanceDrip[index30];
                break;
              case 40:
                if (this.SoundInstanceCamera != null)
                  this.SoundInstanceCamera.Stop();
                this.SoundInstanceCamera = this.SoundCamera.Value.CreateInstance();
                this.SoundInstanceCamera.Volume = num7;
                this.SoundInstanceCamera.Pan = num2;
                sound = this.SoundInstanceCamera;
                break;
              case 41:
                this.SoundInstanceMoonlordCry = this.SoundNpcKilled[10].Value.CreateInstance();
                this.SoundInstanceMoonlordCry.Volume = (float) (1.0 / (1.0 + (double) (new Vector2((float) x, (float) y) - Main.player[Main.myPlayer].position).Length()));
                this.SoundInstanceMoonlordCry.Pan = num2;
                this.SoundInstanceMoonlordCry.Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
                sound = this.SoundInstanceMoonlordCry;
                break;
              case 42:
                sound = this.TrackableSounds[index1].Value.CreateInstance();
                sound.Volume = num7;
                sound.Pan = num2;
                this.TrackableSoundInstances[index1] = sound;
                break;
              case 65:
                if (this.SoundInstanceZombie[115] != null && this.SoundInstanceZombie[115].State == SoundState.Playing || this.SoundInstanceZombie[116] != null && this.SoundInstanceZombie[116].State == SoundState.Playing || this.SoundInstanceZombie[117] != null && this.SoundInstanceZombie[117].State == SoundState.Playing)
                  return (SoundEffectInstance) null;
                int index31 = Main.rand.Next(115, 118);
                this.SoundInstanceZombie[index31] = this.SoundZombie[index31].Value.CreateInstance();
                this.SoundInstanceZombie[index31].Volume = num7 * 0.5f;
                this.SoundInstanceZombie[index31].Pan = num2;
                sound = this.SoundInstanceZombie[index31];
                break;
            }
          }
          if (sound != null)
          {
            sound.Pitch += pitchOffset;
            sound.Volume *= volumeScale;
            sound.Play();
            SoundInstanceGarbageCollector.Track(sound);
          }
          return sound;
        }
      }
      catch
      {
      }
      return (SoundEffectInstance) null;
    }

    public SoundEffect GetTrackableSoundByStyleId(int id) => this.TrackableSounds[id].Value;

    public void StopAmbientSounds()
    {
      for (int index = 0; index < this.SoundInstanceLiquid.Length; ++index)
      {
        if (this.SoundInstanceLiquid[index] != null)
          this.SoundInstanceLiquid[index].Stop();
      }
    }

    public bool DoesSoundScaleWithAmbientVolume(int soundType)
    {
      switch (soundType)
      {
        case 30:
        case 31:
        case 32:
        case 33:
        case 34:
        case 35:
        case 39:
        case 43:
        case 44:
        case 45:
        case 46:
        case 67:
        case 68:
        case 69:
          return true;
        default:
          return false;
      }
    }
  }
}
