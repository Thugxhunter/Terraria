// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Drawing.ParticleOrchestrator
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.GameContent.NetModules;
using Terraria.Graphics.Renderers;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Net;

namespace Terraria.GameContent.Drawing
{
  public class ParticleOrchestrator
  {
    private static ParticlePool<FadingParticle> _poolFading = new ParticlePool<FadingParticle>(200, new ParticlePool<FadingParticle>.ParticleInstantiator(ParticleOrchestrator.GetNewFadingParticle));
    private static ParticlePool<LittleFlyingCritterParticle> _poolFlies = new ParticlePool<LittleFlyingCritterParticle>(200, new ParticlePool<LittleFlyingCritterParticle>.ParticleInstantiator(ParticleOrchestrator.GetNewPooFlyParticle));
    private static ParticlePool<ItemTransferParticle> _poolItemTransfer = new ParticlePool<ItemTransferParticle>(100, new ParticlePool<ItemTransferParticle>.ParticleInstantiator(ParticleOrchestrator.GetNewItemTransferParticle));
    private static ParticlePool<FlameParticle> _poolFlame = new ParticlePool<FlameParticle>(200, new ParticlePool<FlameParticle>.ParticleInstantiator(ParticleOrchestrator.GetNewFlameParticle));
    private static ParticlePool<RandomizedFrameParticle> _poolRandomizedFrame = new ParticlePool<RandomizedFrameParticle>(200, new ParticlePool<RandomizedFrameParticle>.ParticleInstantiator(ParticleOrchestrator.GetNewRandomizedFrameParticle));
    private static ParticlePool<PrettySparkleParticle> _poolPrettySparkle = new ParticlePool<PrettySparkleParticle>(200, new ParticlePool<PrettySparkleParticle>.ParticleInstantiator(ParticleOrchestrator.GetNewPrettySparkleParticle));
    private static ParticlePool<GasParticle> _poolGas = new ParticlePool<GasParticle>(200, new ParticlePool<GasParticle>.ParticleInstantiator(ParticleOrchestrator.GetNewGasParticle));

    public static void RequestParticleSpawn(
      bool clientOnly,
      ParticleOrchestraType type,
      ParticleOrchestraSettings settings,
      int? overrideInvokingPlayerIndex = null)
    {
      settings.IndexOfPlayerWhoInvokedThis = (byte) Main.myPlayer;
      if (overrideInvokingPlayerIndex.HasValue)
        settings.IndexOfPlayerWhoInvokedThis = (byte) overrideInvokingPlayerIndex.Value;
      if (clientOnly)
        ParticleOrchestrator.SpawnParticlesDirect(type, settings);
      else
        NetManager.Instance.SendToServerAndSelf(NetParticlesModule.Serialize(type, settings));
    }

    public static void BroadcastParticleSpawn(
      ParticleOrchestraType type,
      ParticleOrchestraSettings settings)
    {
      settings.IndexOfPlayerWhoInvokedThis = (byte) Main.myPlayer;
      NetManager.Instance.BroadcastOrLoopback(NetParticlesModule.Serialize(type, settings));
    }

    public static void BroadcastOrRequestParticleSpawn(
      ParticleOrchestraType type,
      ParticleOrchestraSettings settings)
    {
      settings.IndexOfPlayerWhoInvokedThis = (byte) Main.myPlayer;
      if (Main.netMode == 1)
        NetManager.Instance.SendToServerAndSelf(NetParticlesModule.Serialize(type, settings));
      else
        NetManager.Instance.BroadcastOrLoopback(NetParticlesModule.Serialize(type, settings));
    }

    private static FadingParticle GetNewFadingParticle() => new FadingParticle();

    private static LittleFlyingCritterParticle GetNewPooFlyParticle() => new LittleFlyingCritterParticle();

    private static ItemTransferParticle GetNewItemTransferParticle() => new ItemTransferParticle();

    private static FlameParticle GetNewFlameParticle() => new FlameParticle();

    private static RandomizedFrameParticle GetNewRandomizedFrameParticle() => new RandomizedFrameParticle();

    private static PrettySparkleParticle GetNewPrettySparkleParticle() => new PrettySparkleParticle();

    private static GasParticle GetNewGasParticle() => new GasParticle();

    public static void SpawnParticlesDirect(
      ParticleOrchestraType type,
      ParticleOrchestraSettings settings)
    {
      if (Main.netMode == 2)
        return;
      switch (type)
      {
        case ParticleOrchestraType.Keybrand:
          ParticleOrchestrator.Spawn_Keybrand(settings);
          break;
        case ParticleOrchestraType.FlameWaders:
          ParticleOrchestrator.Spawn_FlameWaders(settings);
          break;
        case ParticleOrchestraType.StellarTune:
          ParticleOrchestrator.Spawn_StellarTune(settings);
          break;
        case ParticleOrchestraType.WallOfFleshGoatMountFlames:
          ParticleOrchestrator.Spawn_WallOfFleshGoatMountFlames(settings);
          break;
        case ParticleOrchestraType.BlackLightningHit:
          ParticleOrchestrator.Spawn_BlackLightningHit(settings);
          break;
        case ParticleOrchestraType.RainbowRodHit:
          ParticleOrchestrator.Spawn_RainbowRodHit(settings);
          break;
        case ParticleOrchestraType.BlackLightningSmall:
          ParticleOrchestrator.Spawn_BlackLightningSmall(settings);
          break;
        case ParticleOrchestraType.StardustPunch:
          ParticleOrchestrator.Spawn_StardustPunch(settings);
          break;
        case ParticleOrchestraType.PrincessWeapon:
          ParticleOrchestrator.Spawn_PrincessWeapon(settings);
          break;
        case ParticleOrchestraType.PaladinsHammer:
          ParticleOrchestrator.Spawn_PaladinsHammer(settings);
          break;
        case ParticleOrchestraType.NightsEdge:
          ParticleOrchestrator.Spawn_NightsEdge(settings);
          break;
        case ParticleOrchestraType.SilverBulletSparkle:
          ParticleOrchestrator.Spawn_SilverBulletSparkle(settings);
          break;
        case ParticleOrchestraType.TrueNightsEdge:
          ParticleOrchestrator.Spawn_TrueNightsEdge(settings);
          break;
        case ParticleOrchestraType.Excalibur:
          ParticleOrchestrator.Spawn_Excalibur(settings);
          break;
        case ParticleOrchestraType.TrueExcalibur:
          ParticleOrchestrator.Spawn_TrueExcalibur(settings);
          break;
        case ParticleOrchestraType.TerraBlade:
          ParticleOrchestrator.Spawn_TerraBlade(settings);
          break;
        case ParticleOrchestraType.ChlorophyteLeafCrystalPassive:
          ParticleOrchestrator.Spawn_LeafCrystalPassive(settings);
          break;
        case ParticleOrchestraType.ChlorophyteLeafCrystalShot:
          ParticleOrchestrator.Spawn_LeafCrystalShot(settings);
          break;
        case ParticleOrchestraType.AshTreeShake:
          ParticleOrchestrator.Spawn_AshTreeShake(settings);
          break;
        case ParticleOrchestraType.PetExchange:
          ParticleOrchestrator.Spawn_PetExchange(settings);
          break;
        case ParticleOrchestraType.SlapHand:
          ParticleOrchestrator.Spawn_SlapHand(settings);
          break;
        case ParticleOrchestraType.FlyMeal:
          ParticleOrchestrator.Spawn_FlyMeal(settings);
          break;
        case ParticleOrchestraType.GasTrap:
          ParticleOrchestrator.Spawn_GasTrap(settings);
          break;
        case ParticleOrchestraType.ItemTransfer:
          ParticleOrchestrator.Spawn_ItemTransfer(settings);
          break;
        case ParticleOrchestraType.ShimmerArrow:
          ParticleOrchestrator.Spawn_ShimmerArrow(settings);
          break;
        case ParticleOrchestraType.TownSlimeTransform:
          ParticleOrchestrator.Spawn_TownSlimeTransform(settings);
          break;
        case ParticleOrchestraType.LoadoutChange:
          ParticleOrchestrator.Spawn_LoadOutChange(settings);
          break;
        case ParticleOrchestraType.ShimmerBlock:
          ParticleOrchestrator.Spawn_ShimmerBlock(settings);
          break;
        case ParticleOrchestraType.Digestion:
          ParticleOrchestrator.Spawn_Digestion(settings);
          break;
        case ParticleOrchestraType.WaffleIron:
          ParticleOrchestrator.Spawn_WaffleIron(settings);
          break;
        case ParticleOrchestraType.PooFly:
          ParticleOrchestrator.Spawn_PooFly(settings);
          break;
        case ParticleOrchestraType.ShimmerTownNPC:
          ParticleOrchestrator.Spawn_ShimmerTownNPC(settings);
          break;
        case ParticleOrchestraType.ShimmerTownNPCSend:
          ParticleOrchestrator.Spawn_ShimmerTownNPCSend(settings);
          break;
      }
    }

    private static void Spawn_ShimmerTownNPCSend(ParticleOrchestraSettings settings)
    {
      Rectangle rect = Utils.CenteredRectangle(settings.PositionInWorld, new Vector2(30f, 60f));
      for (float num1 = 0.0f; (double) num1 < 20.0; ++num1)
      {
        PrettySparkleParticle prettySparkleParticle1 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        int num2 = Main.rand.Next(20, 40);
        prettySparkleParticle1.ColorTint = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.5f, (byte) 0);
        prettySparkleParticle1.LocalPosition = Main.rand.NextVector2FromRectangle(rect);
        prettySparkleParticle1.Rotation = 1.57079637f;
        prettySparkleParticle1.Scale = new Vector2((float) (1.0 + (double) Main.rand.NextFloat() * 2.0), (float) (0.699999988079071 + (double) Main.rand.NextFloat() * 0.699999988079071));
        prettySparkleParticle1.Velocity = new Vector2(0.0f, -1f);
        prettySparkleParticle1.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle1.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle1.TimeToLive = (float) num2;
        prettySparkleParticle1.FadeOutEnd = (float) num2;
        prettySparkleParticle1.FadeInEnd = (float) (num2 / 2);
        prettySparkleParticle1.FadeOutStart = (float) (num2 / 2);
        prettySparkleParticle1.AdditiveAmount = 0.35f;
        prettySparkleParticle1.DrawVerticalAxis = false;
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle1);
        PrettySparkleParticle prettySparkleParticle2 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        prettySparkleParticle2.ColorTint = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
        prettySparkleParticle2.LocalPosition = Main.rand.NextVector2FromRectangle(rect);
        prettySparkleParticle2.Rotation = 1.57079637f;
        prettySparkleParticle2.Scale = prettySparkleParticle1.Scale * 0.5f;
        prettySparkleParticle2.Velocity = new Vector2(0.0f, -1f);
        prettySparkleParticle2.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle2.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle2.TimeToLive = (float) num2;
        prettySparkleParticle2.FadeOutEnd = (float) num2;
        prettySparkleParticle2.FadeInEnd = (float) (num2 / 2);
        prettySparkleParticle2.FadeOutStart = (float) (num2 / 2);
        prettySparkleParticle2.AdditiveAmount = 1f;
        prettySparkleParticle2.DrawVerticalAxis = false;
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle2);
      }
    }

    private static void Spawn_ShimmerTownNPC(ParticleOrchestraSettings settings)
    {
      Rectangle rectangle = Utils.CenteredRectangle(settings.PositionInWorld, new Vector2(30f, 60f));
      for (float num1 = 0.0f; (double) num1 < 20.0; ++num1)
      {
        PrettySparkleParticle prettySparkleParticle1 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        int num2 = Main.rand.Next(20, 40);
        prettySparkleParticle1.ColorTint = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.5f, (byte) 0);
        prettySparkleParticle1.LocalPosition = Main.rand.NextVector2FromRectangle(rectangle);
        prettySparkleParticle1.Rotation = 1.57079637f;
        prettySparkleParticle1.Scale = new Vector2((float) (1.0 + (double) Main.rand.NextFloat() * 2.0), (float) (0.699999988079071 + (double) Main.rand.NextFloat() * 0.699999988079071));
        prettySparkleParticle1.Velocity = new Vector2(0.0f, -1f);
        prettySparkleParticle1.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle1.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle1.TimeToLive = (float) num2;
        prettySparkleParticle1.FadeOutEnd = (float) num2;
        prettySparkleParticle1.FadeInEnd = (float) (num2 / 2);
        prettySparkleParticle1.FadeOutStart = (float) (num2 / 2);
        prettySparkleParticle1.AdditiveAmount = 0.35f;
        prettySparkleParticle1.DrawVerticalAxis = false;
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle1);
        PrettySparkleParticle prettySparkleParticle2 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        prettySparkleParticle2.ColorTint = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
        prettySparkleParticle2.LocalPosition = Main.rand.NextVector2FromRectangle(rectangle);
        prettySparkleParticle2.Rotation = 1.57079637f;
        prettySparkleParticle2.Scale = prettySparkleParticle1.Scale * 0.5f;
        prettySparkleParticle2.Velocity = new Vector2(0.0f, -1f);
        prettySparkleParticle2.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle2.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle2.TimeToLive = (float) num2;
        prettySparkleParticle2.FadeOutEnd = (float) num2;
        prettySparkleParticle2.FadeInEnd = (float) (num2 / 2);
        prettySparkleParticle2.FadeOutStart = (float) (num2 / 2);
        prettySparkleParticle2.AdditiveAmount = 1f;
        prettySparkleParticle2.DrawVerticalAxis = false;
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle2);
      }
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(rectangle.TopLeft(), rectangle.Width, rectangle.Height, 308);
        Main.dust[index2].velocity.Y -= 8f;
        Main.dust[index2].velocity.X *= 0.5f;
        Main.dust[index2].scale = 0.8f;
        Main.dust[index2].noGravity = true;
        switch (Main.rand.Next(6))
        {
          case 0:
            Main.dust[index2].color = new Color((int) byte.MaxValue, (int) byte.MaxValue, 210);
            break;
          case 1:
            Main.dust[index2].color = new Color(190, 245, (int) byte.MaxValue);
            break;
          case 2:
            Main.dust[index2].color = new Color((int) byte.MaxValue, 150, (int) byte.MaxValue);
            break;
          default:
            Main.dust[index2].color = new Color(190, 175, (int) byte.MaxValue);
            break;
        }
      }
      SoundEngine.PlaySound(SoundID.Item29, settings.PositionInWorld);
    }

    private static void Spawn_PooFly(ParticleOrchestraSettings settings)
    {
      int fromValue = ParticleOrchestrator._poolFlies.CountParticlesInUse();
      if (fromValue > 50 && (double) Main.rand.NextFloat() >= (double) Utils.Remap((float) fromValue, 50f, 400f, 0.5f, 0.0f))
        return;
      LittleFlyingCritterParticle flyingCritterParticle = ParticleOrchestrator._poolFlies.RequestParticle();
      flyingCritterParticle.Prepare(settings.PositionInWorld, 300);
      Main.ParticleSystem_World_OverPlayers.Add((IParticle) flyingCritterParticle);
    }

    private static void Spawn_Digestion(ParticleOrchestraSettings settings)
    {
      Vector2 positionInWorld = settings.PositionInWorld;
      int num1 = (double) settings.MovementVector.X < 0.0 ? 1 : -1;
      int num2 = Main.rand.Next(4);
      for (int index1 = 0; index1 < 3 + num2; ++index1)
      {
        int index2 = Dust.NewDust(positionInWorld + Vector2.UnitX * (float) -num1 * 8f - Vector2.One * 5f + Vector2.UnitY * 8f, 3, 6, 216, (float) -num1, 1f);
        Main.dust[index2].velocity /= 2f;
        Main.dust[index2].scale = 0.8f;
      }
      if (Main.rand.Next(30) == 0)
      {
        int index = Gore.NewGore(positionInWorld + Vector2.UnitX * (float) -num1 * 8f, Vector2.Zero, Main.rand.Next(580, 583));
        Main.gore[index].velocity /= 2f;
        Main.gore[index].velocity.Y = Math.Abs(Main.gore[index].velocity.Y);
        Main.gore[index].velocity.X = -Math.Abs(Main.gore[index].velocity.X) * (float) num1;
      }
      SoundEngine.PlaySound(SoundID.Item16, settings.PositionInWorld);
    }

    private static void Spawn_ShimmerBlock(ParticleOrchestraSettings settings)
    {
      FadingParticle fadingParticle = ParticleOrchestrator._poolFading.RequestParticle();
      fadingParticle.SetBasicInfo(TextureAssets.Star[0], new Rectangle?(), settings.MovementVector, settings.PositionInWorld);
      float timeToLive = 45f;
      fadingParticle.SetTypeInfo(timeToLive);
      fadingParticle.AccelerationPerFrame = settings.MovementVector / timeToLive;
      fadingParticle.ColorTint = Main.hslToRgb(Main.rand.NextFloat(), 0.75f, 0.8f);
      fadingParticle.ColorTint.A = (byte) 30;
      fadingParticle.FadeInNormalizedTime = 0.5f;
      fadingParticle.FadeOutNormalizedTime = 0.5f;
      fadingParticle.Rotation = Main.rand.NextFloat() * 6.28318548f;
      fadingParticle.Scale = Vector2.One * (float) (0.5 + 0.5 * (double) Main.rand.NextFloat());
      Main.ParticleSystem_World_OverPlayers.Add((IParticle) fadingParticle);
    }

    private static void Spawn_LoadOutChange(ParticleOrchestraSettings settings)
    {
      Player player = Main.player[(int) settings.IndexOfPlayerWhoInvokedThis];
      if (!player.active)
        return;
      Rectangle hitbox = player.Hitbox;
      int num1 = 6;
      hitbox.Height -= num1;
      if ((double) player.gravDir == 1.0)
        hitbox.Y += num1;
      for (int index = 0; index < 40; ++index)
      {
        Dust dust = Dust.NewDustPerfect(Main.rand.NextVector2FromRectangle(hitbox), 16, Alpha: 120, Scale: ((float) ((double) Main.rand.NextFloat() * 0.800000011920929 + 0.800000011920929)));
        dust.velocity = new Vector2(0.0f, (float) ((double) -hitbox.Height * (double) Main.rand.NextFloat() * 0.039999999105930328)).RotatedBy((double) Main.rand.NextFloatDirection() * 6.2831854820251465 * 0.10000000149011612);
        dust.velocity += player.velocity * 2f * Main.rand.NextFloat();
        dust.noGravity = true;
        int num2;
        bool flag = (num2 = 1) != 0;
        dust.noLightEmittence = num2 != 0;
        dust.noLight = flag;
      }
      for (int index = 0; index < 5; ++index)
      {
        Dust dust = Dust.NewDustPerfect(Main.rand.NextVector2FromRectangle(hitbox), 43, Alpha: 254, newColor: Main.hslToRgb(Main.rand.NextFloat(), 0.3f, 0.8f), Scale: ((float) ((double) Main.rand.NextFloat() * 0.800000011920929 + 0.800000011920929)));
        dust.velocity = new Vector2(0.0f, (float) ((double) -hitbox.Height * (double) Main.rand.NextFloat() * 0.039999999105930328)).RotatedBy((double) Main.rand.NextFloatDirection() * 6.2831854820251465 * 0.10000000149011612);
        dust.velocity += player.velocity * 2f * Main.rand.NextFloat();
        dust.noGravity = true;
        int num3;
        bool flag = (num3 = 1) != 0;
        dust.noLightEmittence = num3 != 0;
        dust.noLight = flag;
      }
    }

    private static void Spawn_TownSlimeTransform(ParticleOrchestraSettings settings)
    {
      switch (settings.UniqueInfoPiece)
      {
        case 0:
          ParticleOrchestrator.NerdySlimeEffect(settings);
          break;
        case 1:
          ParticleOrchestrator.CopperSlimeEffect(settings);
          break;
        case 2:
          ParticleOrchestrator.ElderSlimeEffect(settings);
          break;
      }
    }

    private static void ElderSlimeEffect(ParticleOrchestraSettings settings)
    {
      for (int index = 0; index < 30; ++index)
      {
        Dust dust = Dust.NewDustPerfect(settings.PositionInWorld + Main.rand.NextVector2Circular(20f, 20f), 43, new Vector2?((settings.MovementVector * 0.75f + Main.rand.NextVector2Circular(6f, 6f)) * Main.rand.NextFloat()), 26, Color.Lerp(Main.OurFavoriteColor, Color.White, Main.rand.NextFloat()), (float) (1.0 + (double) Main.rand.NextFloat() * 1.3999999761581421));
        dust.fadeIn = 1.5f;
        if ((double) dust.velocity.Y > 0.0 && Main.rand.Next(2) == 0)
          dust.velocity.Y *= -1f;
        dust.noGravity = true;
      }
      for (int index = 0; index < 8; ++index)
        Gore.NewGoreDirect(settings.PositionInWorld + Utils.RandomVector2(Main.rand, -30f, 30f) * new Vector2(0.5f, 1f), Vector2.Zero, 61 + Main.rand.Next(3)).velocity *= 0.5f;
    }

    private static void NerdySlimeEffect(ParticleOrchestraSettings settings)
    {
      Color newColor = new Color(0, 80, (int) byte.MaxValue, 100);
      for (int index = 0; index < 60; ++index)
        Dust.NewDustPerfect(settings.PositionInWorld, 4, new Vector2?((settings.MovementVector * 0.75f + Main.rand.NextVector2Circular(6f, 6f)) * Main.rand.NextFloat()), 175, newColor, (float) (0.60000002384185791 + (double) Main.rand.NextFloat() * 1.3999999761581421));
    }

    private static void CopperSlimeEffect(ParticleOrchestraSettings settings)
    {
      for (int index = 0; index < 40; ++index)
      {
        Dust dust = Dust.NewDustPerfect(settings.PositionInWorld + Main.rand.NextVector2Circular(20f, 20f), 43, new Vector2?((settings.MovementVector * 0.75f + Main.rand.NextVector2Circular(6f, 6f)) * Main.rand.NextFloat()), 26, Color.Lerp(new Color(183, 88, 25), Color.White, Main.rand.NextFloat() * 0.5f), (float) (1.0 + (double) Main.rand.NextFloat() * 1.3999999761581421));
        dust.fadeIn = 1.5f;
        if ((double) dust.velocity.Y > 0.0 && Main.rand.Next(2) == 0)
          dust.velocity.Y *= -1f;
        dust.noGravity = true;
      }
    }

    private static void Spawn_ShimmerArrow(ParticleOrchestraSettings settings)
    {
      float num1 = 20f;
      for (int index1 = 0; index1 < 2; ++index1)
      {
        float num2 = (float) (6.2831854820251465 * (double) Main.rand.NextFloatDirection() * 0.05000000074505806);
        Color rgb = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.5f);
        rgb.A /= (byte) 2;
        Color color = Color.Lerp(rgb with
        {
          A = byte.MaxValue
        }, Color.White, 0.5f);
        for (float num3 = 0.0f; (double) num3 < 4.0; ++num3)
        {
          PrettySparkleParticle prettySparkleParticle1 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
          Vector2 v = (1.57079637f * num3 + num2).ToRotationVector2() * 4f;
          prettySparkleParticle1.ColorTint = rgb;
          prettySparkleParticle1.LocalPosition = settings.PositionInWorld;
          prettySparkleParticle1.Rotation = v.ToRotation();
          prettySparkleParticle1.Scale = new Vector2((double) num3 % 2.0 == 0.0 ? 2f : 4f, 0.5f) * 1.1f;
          prettySparkleParticle1.FadeInNormalizedTime = 5E-06f;
          prettySparkleParticle1.FadeOutNormalizedTime = 0.95f;
          prettySparkleParticle1.TimeToLive = num1;
          prettySparkleParticle1.FadeOutEnd = num1;
          prettySparkleParticle1.FadeInEnd = num1 / 2f;
          prettySparkleParticle1.FadeOutStart = num1 / 2f;
          prettySparkleParticle1.AdditiveAmount = 0.35f;
          prettySparkleParticle1.Velocity = -v * 0.2f;
          prettySparkleParticle1.DrawVerticalAxis = false;
          if ((double) num3 % 2.0 == 1.0)
          {
            PrettySparkleParticle prettySparkleParticle2 = prettySparkleParticle1;
            prettySparkleParticle2.Scale = prettySparkleParticle2.Scale * 0.9f;
            PrettySparkleParticle prettySparkleParticle3 = prettySparkleParticle1;
            prettySparkleParticle3.Velocity = prettySparkleParticle3.Velocity * 0.9f;
          }
          Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle1);
        }
        for (float num4 = 0.0f; (double) num4 < 4.0; ++num4)
        {
          PrettySparkleParticle prettySparkleParticle4 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
          Vector2 vector2 = (1.57079637f * num4 + num2).ToRotationVector2() * 4f;
          prettySparkleParticle4.ColorTint = color;
          prettySparkleParticle4.LocalPosition = settings.PositionInWorld;
          prettySparkleParticle4.Rotation = vector2.ToRotation();
          prettySparkleParticle4.Scale = new Vector2((double) num4 % 2.0 == 0.0 ? 2f : 4f, 0.5f) * 0.7f;
          prettySparkleParticle4.FadeInNormalizedTime = 5E-06f;
          prettySparkleParticle4.FadeOutNormalizedTime = 0.95f;
          prettySparkleParticle4.TimeToLive = num1;
          prettySparkleParticle4.FadeOutEnd = num1;
          prettySparkleParticle4.FadeInEnd = num1 / 2f;
          prettySparkleParticle4.FadeOutStart = num1 / 2f;
          prettySparkleParticle4.Velocity = vector2 * 0.2f;
          prettySparkleParticle4.DrawVerticalAxis = false;
          if ((double) num4 % 2.0 == 1.0)
          {
            PrettySparkleParticle prettySparkleParticle5 = prettySparkleParticle4;
            prettySparkleParticle5.Scale = prettySparkleParticle5.Scale * 1.2f;
            PrettySparkleParticle prettySparkleParticle6 = prettySparkleParticle4;
            prettySparkleParticle6.Velocity = prettySparkleParticle6.Velocity * 1.2f;
          }
          Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle4);
          if (index1 == 0)
          {
            for (int index2 = 0; index2 < 1; ++index2)
            {
              Dust dust1 = Dust.NewDustPerfect(settings.PositionInWorld, 306, new Vector2?(vector2.RotatedBy((double) Main.rand.NextFloatDirection() * 6.2831854820251465 * 0.02500000037252903) * Main.rand.NextFloat()));
              dust1.noGravity = true;
              dust1.scale = 1.4f;
              dust1.fadeIn = 1.2f;
              dust1.color = rgb;
              Dust dust2 = Dust.NewDustPerfect(settings.PositionInWorld, 306, new Vector2?(-vector2.RotatedBy((double) Main.rand.NextFloatDirection() * 6.2831854820251465 * 0.02500000037252903) * Main.rand.NextFloat()));
              dust2.noGravity = true;
              dust2.scale = 1.4f;
              dust2.fadeIn = 1.2f;
              dust2.color = rgb;
            }
          }
        }
      }
    }

    private static void Spawn_ItemTransfer(ParticleOrchestraSettings settings)
    {
      Vector2 vector2_1 = settings.PositionInWorld + settings.MovementVector;
      Vector2 vector2_2 = Main.rand.NextVector2Circular(32f, 32f);
      Vector2 playerPosition = settings.PositionInWorld + vector2_2;
      Vector2 vector2_3 = playerPosition;
      Vector2 vector2_4 = vector2_1 - vector2_3;
      int uniqueInfoPiece = settings.UniqueInfoPiece;
      Item obj;
      if (!ContentSamples.ItemsByType.TryGetValue(uniqueInfoPiece, out obj) || obj.IsAir)
        return;
      int type = obj.type;
      int lifeTimeTotal = Main.rand.Next(60, 80);
      Chest.AskForChestToEatItem(playerPosition + vector2_4 + new Vector2(-8f, -8f), lifeTimeTotal + 10);
      ItemTransferParticle transferParticle = ParticleOrchestrator._poolItemTransfer.RequestParticle();
      transferParticle.Prepare(type, lifeTimeTotal, playerPosition, playerPosition + vector2_4);
      Main.ParticleSystem_World_OverPlayers.Add((IParticle) transferParticle);
    }

    private static void Spawn_PetExchange(ParticleOrchestraSettings settings)
    {
      Vector2 positionInWorld = settings.PositionInWorld;
      for (int index = 0; index < 13; ++index)
      {
        Gore gore = Gore.NewGoreDirect(positionInWorld + new Vector2(-20f, -20f) + Main.rand.NextVector2Circular(20f, 20f), Vector2.Zero, Main.rand.Next(61, 64), (float) (1.0 + (double) Main.rand.NextFloat() * 0.30000001192092896));
        gore.alpha = 100;
        gore.velocity = (6.28318548f * (float) Main.rand.Next()).ToRotationVector2() * Main.rand.NextFloat() + settings.MovementVector * 0.5f;
      }
    }

    private static void Spawn_TerraBlade(ParticleOrchestraSettings settings)
    {
      float num1 = 30f;
      float f = settings.MovementVector.ToRotation() + 1.57079637f;
      float x = 3f;
      for (float num2 = 0.0f; (double) num2 < 4.0; ++num2)
      {
        PrettySparkleParticle prettySparkleParticle1 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        Vector2 v = (1.57079637f * num2 + f).ToRotationVector2() * 4f;
        prettySparkleParticle1.ColorTint = new Color(0.2f, 0.85f, 0.4f, 0.5f);
        prettySparkleParticle1.LocalPosition = settings.PositionInWorld;
        prettySparkleParticle1.Rotation = v.ToRotation();
        prettySparkleParticle1.Scale = new Vector2(x, 0.5f) * 1.1f;
        prettySparkleParticle1.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle1.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle1.TimeToLive = num1;
        prettySparkleParticle1.FadeOutEnd = num1;
        prettySparkleParticle1.FadeInEnd = num1 / 2f;
        prettySparkleParticle1.FadeOutStart = num1 / 2f;
        prettySparkleParticle1.AdditiveAmount = 0.35f;
        prettySparkleParticle1.Velocity = -v * 0.2f;
        prettySparkleParticle1.DrawVerticalAxis = false;
        if ((double) num2 % 2.0 == 1.0)
        {
          PrettySparkleParticle prettySparkleParticle2 = prettySparkleParticle1;
          prettySparkleParticle2.Scale = prettySparkleParticle2.Scale * 1.5f;
          PrettySparkleParticle prettySparkleParticle3 = prettySparkleParticle1;
          prettySparkleParticle3.Velocity = prettySparkleParticle3.Velocity * 2f;
        }
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle1);
      }
      for (float num3 = -1f; (double) num3 <= 1.0; num3 += 2f)
      {
        PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        Vector2 vector2_1 = f.ToRotationVector2() * 4f;
        Vector2 vector2_2 = (1.57079637f * num3 + f).ToRotationVector2() * 2f;
        prettySparkleParticle.ColorTint = new Color(0.4f, 1f, 0.4f, 0.5f);
        prettySparkleParticle.LocalPosition = settings.PositionInWorld;
        prettySparkleParticle.Rotation = vector2_2.ToRotation();
        prettySparkleParticle.Scale = new Vector2(x, 0.5f) * 1.1f;
        prettySparkleParticle.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle.TimeToLive = num1;
        prettySparkleParticle.FadeOutEnd = num1;
        prettySparkleParticle.FadeInEnd = num1 / 2f;
        prettySparkleParticle.FadeOutStart = num1 / 2f;
        prettySparkleParticle.AdditiveAmount = 0.35f;
        prettySparkleParticle.Velocity = vector2_2.RotatedBy(1.5707963705062866) * 0.5f;
        prettySparkleParticle.DrawVerticalAxis = false;
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle);
      }
      for (float num4 = 0.0f; (double) num4 < 4.0; ++num4)
      {
        PrettySparkleParticle prettySparkleParticle4 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        Vector2 vector2 = (1.57079637f * num4 + f).ToRotationVector2() * 4f;
        prettySparkleParticle4.ColorTint = new Color(0.2f, 1f, 0.2f, 1f);
        prettySparkleParticle4.LocalPosition = settings.PositionInWorld;
        prettySparkleParticle4.Rotation = vector2.ToRotation();
        prettySparkleParticle4.Scale = new Vector2(x, 0.5f) * 0.7f;
        prettySparkleParticle4.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle4.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle4.TimeToLive = num1;
        prettySparkleParticle4.FadeOutEnd = num1;
        prettySparkleParticle4.FadeInEnd = num1 / 2f;
        prettySparkleParticle4.FadeOutStart = num1 / 2f;
        prettySparkleParticle4.Velocity = vector2 * 0.2f;
        prettySparkleParticle4.DrawVerticalAxis = false;
        if ((double) num4 % 2.0 == 1.0)
        {
          PrettySparkleParticle prettySparkleParticle5 = prettySparkleParticle4;
          prettySparkleParticle5.Scale = prettySparkleParticle5.Scale * 1.5f;
          PrettySparkleParticle prettySparkleParticle6 = prettySparkleParticle4;
          prettySparkleParticle6.Velocity = prettySparkleParticle6.Velocity * 2f;
        }
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle4);
        for (int index = 0; index < 1; ++index)
        {
          Dust dust1 = Dust.NewDustPerfect(settings.PositionInWorld, 107, new Vector2?(vector2.RotatedBy((double) Main.rand.NextFloatDirection() * 6.2831854820251465 * 0.02500000037252903) * Main.rand.NextFloat()));
          dust1.noGravity = true;
          dust1.scale = 0.8f;
          Dust dust2 = Dust.NewDustPerfect(settings.PositionInWorld, 107, new Vector2?(-vector2.RotatedBy((double) Main.rand.NextFloatDirection() * 6.2831854820251465 * 0.02500000037252903) * Main.rand.NextFloat()));
          dust2.noGravity = true;
          dust2.scale = 1.4f;
        }
      }
    }

    private static void Spawn_Excalibur(ParticleOrchestraSettings settings)
    {
      float num1 = 30f;
      float num2 = 0.0f;
      for (float num3 = 0.0f; (double) num3 < 4.0; ++num3)
      {
        PrettySparkleParticle prettySparkleParticle1 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        Vector2 v = (1.57079637f * num3 + num2).ToRotationVector2() * 4f;
        prettySparkleParticle1.ColorTint = new Color(0.9f, 0.85f, 0.4f, 0.5f);
        prettySparkleParticle1.LocalPosition = settings.PositionInWorld;
        prettySparkleParticle1.Rotation = v.ToRotation();
        prettySparkleParticle1.Scale = new Vector2((double) num3 % 2.0 == 0.0 ? 2f : 4f, 0.5f) * 1.1f;
        prettySparkleParticle1.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle1.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle1.TimeToLive = num1;
        prettySparkleParticle1.FadeOutEnd = num1;
        prettySparkleParticle1.FadeInEnd = num1 / 2f;
        prettySparkleParticle1.FadeOutStart = num1 / 2f;
        prettySparkleParticle1.AdditiveAmount = 0.35f;
        prettySparkleParticle1.Velocity = -v * 0.2f;
        prettySparkleParticle1.DrawVerticalAxis = false;
        if ((double) num3 % 2.0 == 1.0)
        {
          PrettySparkleParticle prettySparkleParticle2 = prettySparkleParticle1;
          prettySparkleParticle2.Scale = prettySparkleParticle2.Scale * 1.5f;
          PrettySparkleParticle prettySparkleParticle3 = prettySparkleParticle1;
          prettySparkleParticle3.Velocity = prettySparkleParticle3.Velocity * 1.5f;
        }
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle1);
      }
      for (float num4 = 0.0f; (double) num4 < 4.0; ++num4)
      {
        PrettySparkleParticle prettySparkleParticle4 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        Vector2 vector2 = (1.57079637f * num4 + num2).ToRotationVector2() * 4f;
        prettySparkleParticle4.ColorTint = new Color(1f, 1f, 0.2f, 1f);
        prettySparkleParticle4.LocalPosition = settings.PositionInWorld;
        prettySparkleParticle4.Rotation = vector2.ToRotation();
        prettySparkleParticle4.Scale = new Vector2((double) num4 % 2.0 == 0.0 ? 2f : 4f, 0.5f) * 0.7f;
        prettySparkleParticle4.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle4.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle4.TimeToLive = num1;
        prettySparkleParticle4.FadeOutEnd = num1;
        prettySparkleParticle4.FadeInEnd = num1 / 2f;
        prettySparkleParticle4.FadeOutStart = num1 / 2f;
        prettySparkleParticle4.Velocity = vector2 * 0.2f;
        prettySparkleParticle4.DrawVerticalAxis = false;
        if ((double) num4 % 2.0 == 1.0)
        {
          PrettySparkleParticle prettySparkleParticle5 = prettySparkleParticle4;
          prettySparkleParticle5.Scale = prettySparkleParticle5.Scale * 1.5f;
          PrettySparkleParticle prettySparkleParticle6 = prettySparkleParticle4;
          prettySparkleParticle6.Velocity = prettySparkleParticle6.Velocity * 1.5f;
        }
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle4);
        for (int index = 0; index < 1; ++index)
        {
          Dust dust1 = Dust.NewDustPerfect(settings.PositionInWorld, 169, new Vector2?(vector2.RotatedBy((double) Main.rand.NextFloatDirection() * 6.2831854820251465 * 0.02500000037252903) * Main.rand.NextFloat()));
          dust1.noGravity = true;
          dust1.scale = 1.4f;
          Dust dust2 = Dust.NewDustPerfect(settings.PositionInWorld, 169, new Vector2?(-vector2.RotatedBy((double) Main.rand.NextFloatDirection() * 6.2831854820251465 * 0.02500000037252903) * Main.rand.NextFloat()));
          dust2.noGravity = true;
          dust2.scale = 1.4f;
        }
      }
    }

    private static void Spawn_SlapHand(ParticleOrchestraSettings settings) => SoundEngine.PlaySound(SoundID.Item175, settings.PositionInWorld);

    private static void Spawn_WaffleIron(ParticleOrchestraSettings settings) => SoundEngine.PlaySound(SoundID.Item178, settings.PositionInWorld);

    private static void Spawn_FlyMeal(ParticleOrchestraSettings settings) => SoundEngine.PlaySound(SoundID.Item16, settings.PositionInWorld);

    private static void Spawn_GasTrap(ParticleOrchestraSettings settings)
    {
      SoundEngine.PlaySound(SoundID.Item16, settings.PositionInWorld);
      Vector2 movementVector = settings.MovementVector;
      int num1 = 12;
      int num2 = 10;
      float num3 = 5f;
      float num4 = 2.5f;
      Color color = new Color(0.2f, 0.4f, 0.15f);
      Vector2 positionInWorld = settings.PositionInWorld;
      float maxRadians1 = 0.157079637f;
      float maxRadians2 = 0.209439516f;
      for (int index = 0; index < num1; ++index)
      {
        Vector2 vector2 = (movementVector + new Vector2(num3 + Main.rand.NextFloat() * 1f, 0.0f).RotatedBy((double) index / (double) num1 * 6.2831854820251465, Vector2.Zero)).RotatedByRandom((double) maxRadians1);
        GasParticle gasParticle = ParticleOrchestrator._poolGas.RequestParticle();
        gasParticle.AccelerationPerFrame = Vector2.Zero;
        gasParticle.Velocity = vector2;
        gasParticle.ColorTint = Color.White;
        gasParticle.LightColorTint = color;
        gasParticle.LocalPosition = positionInWorld + vector2;
        gasParticle.TimeToLive = (float) (50 + Main.rand.Next(20));
        gasParticle.InitialScale = (float) (1.0 + (double) Main.rand.NextFloat() * 0.34999999403953552);
        Main.ParticleSystem_World_BehindPlayers.Add((IParticle) gasParticle);
      }
      for (int index = 0; index < num2; ++index)
      {
        Vector2 vector2 = new Vector2(num4 + Main.rand.NextFloat() * 1.45f, 0.0f).RotatedBy((double) index / (double) num2 * 6.2831854820251465, Vector2.Zero).RotatedByRandom((double) maxRadians2);
        if (index % 2 == 0)
          vector2 *= 0.5f;
        GasParticle gasParticle = ParticleOrchestrator._poolGas.RequestParticle();
        gasParticle.AccelerationPerFrame = Vector2.Zero;
        gasParticle.Velocity = vector2;
        gasParticle.ColorTint = Color.White;
        gasParticle.LightColorTint = color;
        gasParticle.LocalPosition = positionInWorld;
        gasParticle.TimeToLive = (float) (80 + Main.rand.Next(30));
        gasParticle.InitialScale = (float) (1.0 + (double) Main.rand.NextFloat() * 0.5);
        Main.ParticleSystem_World_BehindPlayers.Add((IParticle) gasParticle);
      }
    }

    private static void Spawn_TrueExcalibur(ParticleOrchestraSettings settings)
    {
      float num1 = 36f;
      float num2 = 0.7853982f;
      for (float num3 = 0.0f; (double) num3 < 2.0; ++num3)
      {
        PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        Vector2 v = (1.57079637f * num3 + num2).ToRotationVector2() * 4f;
        prettySparkleParticle.ColorTint = new Color(1f, 0.0f, 0.3f, 1f);
        prettySparkleParticle.LocalPosition = settings.PositionInWorld;
        prettySparkleParticle.Rotation = v.ToRotation();
        prettySparkleParticle.Scale = new Vector2(5f, 0.5f) * 1.1f;
        prettySparkleParticle.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle.TimeToLive = num1;
        prettySparkleParticle.FadeOutEnd = num1;
        prettySparkleParticle.FadeInEnd = num1 / 2f;
        prettySparkleParticle.FadeOutStart = num1 / 2f;
        prettySparkleParticle.AdditiveAmount = 0.35f;
        prettySparkleParticle.DrawVerticalAxis = false;
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle);
      }
      for (float num4 = 0.0f; (double) num4 < 2.0; ++num4)
      {
        PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        Vector2 vector2 = (1.57079637f * num4 + num2).ToRotationVector2() * 4f;
        prettySparkleParticle.ColorTint = new Color(1f, 0.5f, 0.8f, 1f);
        prettySparkleParticle.LocalPosition = settings.PositionInWorld;
        prettySparkleParticle.Rotation = vector2.ToRotation();
        prettySparkleParticle.Scale = new Vector2(3f, 0.5f) * 0.7f;
        prettySparkleParticle.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle.TimeToLive = num1;
        prettySparkleParticle.FadeOutEnd = num1;
        prettySparkleParticle.FadeInEnd = num1 / 2f;
        prettySparkleParticle.FadeOutStart = num1 / 2f;
        prettySparkleParticle.DrawVerticalAxis = false;
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle);
        for (int index = 0; index < 1; ++index)
        {
          if (Main.rand.Next(2) != 0)
          {
            Dust dust1 = Dust.NewDustPerfect(settings.PositionInWorld, 242, new Vector2?(vector2.RotatedBy((double) Main.rand.NextFloatDirection() * 6.2831854820251465 * 0.02500000037252903) * Main.rand.NextFloat()));
            dust1.noGravity = true;
            dust1.scale = 1.4f;
            Dust dust2 = Dust.NewDustPerfect(settings.PositionInWorld, 242, new Vector2?(-vector2.RotatedBy((double) Main.rand.NextFloatDirection() * 6.2831854820251465 * 0.02500000037252903) * Main.rand.NextFloat()));
            dust2.noGravity = true;
            dust2.scale = 1.4f;
          }
        }
      }
      float num5 = 30f;
      float num6 = 0.0f;
      for (float num7 = 0.0f; (double) num7 < 4.0; ++num7)
      {
        PrettySparkleParticle prettySparkleParticle1 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        Vector2 v = (1.57079637f * num7 + num6).ToRotationVector2() * 4f;
        prettySparkleParticle1.ColorTint = new Color(0.9f, 0.85f, 0.4f, 0.5f);
        prettySparkleParticle1.LocalPosition = settings.PositionInWorld;
        prettySparkleParticle1.Rotation = v.ToRotation();
        prettySparkleParticle1.Scale = new Vector2((double) num7 % 2.0 == 0.0 ? 2f : 4f, 0.5f) * 1.1f;
        prettySparkleParticle1.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle1.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle1.TimeToLive = num5;
        prettySparkleParticle1.FadeOutEnd = num5;
        prettySparkleParticle1.FadeInEnd = num5 / 2f;
        prettySparkleParticle1.FadeOutStart = num5 / 2f;
        prettySparkleParticle1.AdditiveAmount = 0.35f;
        prettySparkleParticle1.Velocity = -v * 0.2f;
        prettySparkleParticle1.DrawVerticalAxis = false;
        if ((double) num7 % 2.0 == 1.0)
        {
          PrettySparkleParticle prettySparkleParticle2 = prettySparkleParticle1;
          prettySparkleParticle2.Scale = prettySparkleParticle2.Scale * 1.5f;
          PrettySparkleParticle prettySparkleParticle3 = prettySparkleParticle1;
          prettySparkleParticle3.Velocity = prettySparkleParticle3.Velocity * 1.5f;
        }
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle1);
      }
      for (float num8 = 0.0f; (double) num8 < 4.0; ++num8)
      {
        PrettySparkleParticle prettySparkleParticle4 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        Vector2 vector2 = (1.57079637f * num8 + num6).ToRotationVector2() * 4f;
        prettySparkleParticle4.ColorTint = new Color(1f, 1f, 0.2f, 1f);
        prettySparkleParticle4.LocalPosition = settings.PositionInWorld;
        prettySparkleParticle4.Rotation = vector2.ToRotation();
        prettySparkleParticle4.Scale = new Vector2((double) num8 % 2.0 == 0.0 ? 2f : 4f, 0.5f) * 0.7f;
        prettySparkleParticle4.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle4.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle4.TimeToLive = num5;
        prettySparkleParticle4.FadeOutEnd = num5;
        prettySparkleParticle4.FadeInEnd = num5 / 2f;
        prettySparkleParticle4.FadeOutStart = num5 / 2f;
        prettySparkleParticle4.Velocity = vector2 * 0.2f;
        prettySparkleParticle4.DrawVerticalAxis = false;
        if ((double) num8 % 2.0 == 1.0)
        {
          PrettySparkleParticle prettySparkleParticle5 = prettySparkleParticle4;
          prettySparkleParticle5.Scale = prettySparkleParticle5.Scale * 1.5f;
          PrettySparkleParticle prettySparkleParticle6 = prettySparkleParticle4;
          prettySparkleParticle6.Velocity = prettySparkleParticle6.Velocity * 1.5f;
        }
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle4);
        for (int index = 0; index < 1; ++index)
        {
          if (Main.rand.Next(2) != 0)
          {
            Dust dust3 = Dust.NewDustPerfect(settings.PositionInWorld, 169, new Vector2?(vector2.RotatedBy((double) Main.rand.NextFloatDirection() * 6.2831854820251465 * 0.02500000037252903) * Main.rand.NextFloat()));
            dust3.noGravity = true;
            dust3.scale = 1.4f;
            Dust dust4 = Dust.NewDustPerfect(settings.PositionInWorld, 169, new Vector2?(-vector2.RotatedBy((double) Main.rand.NextFloatDirection() * 6.2831854820251465 * 0.02500000037252903) * Main.rand.NextFloat()));
            dust4.noGravity = true;
            dust4.scale = 1.4f;
          }
        }
      }
    }

    private static void Spawn_AshTreeShake(ParticleOrchestraSettings settings)
    {
      float num1 = (float) (10.0 + 20.0 * (double) Main.rand.NextFloat());
      float num2 = -0.7853982f;
      float num3 = (float) (0.20000000298023224 + 0.40000000596046448 * (double) Main.rand.NextFloat());
      Color rgb = Main.hslToRgb((float) ((double) Main.rand.NextFloat() * 0.10000000149011612 + 0.059999998658895493), 1f, 0.5f);
      rgb.A /= (byte) 2;
      Color color = rgb * (float) ((double) Main.rand.NextFloat() * 0.30000001192092896 + 0.699999988079071);
      for (float num4 = 0.0f; (double) num4 < 2.0; ++num4)
      {
        PrettySparkleParticle prettySparkleParticle1 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        Vector2 v = ((float) (0.78539818525314331 + 3.1415927410125732 * (double) num4) + num2).ToRotationVector2() * 4f;
        prettySparkleParticle1.ColorTint = color;
        prettySparkleParticle1.LocalPosition = settings.PositionInWorld;
        prettySparkleParticle1.Rotation = v.ToRotation();
        prettySparkleParticle1.Scale = new Vector2(4f, 1f) * 1.1f * num3;
        prettySparkleParticle1.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle1.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle1.TimeToLive = num1;
        prettySparkleParticle1.FadeOutEnd = num1;
        prettySparkleParticle1.FadeInEnd = num1 / 2f;
        prettySparkleParticle1.FadeOutStart = num1 / 2f;
        prettySparkleParticle1.AdditiveAmount = 0.35f;
        PrettySparkleParticle prettySparkleParticle2 = prettySparkleParticle1;
        prettySparkleParticle2.LocalPosition = prettySparkleParticle2.LocalPosition - v * num1 * 0.25f;
        prettySparkleParticle1.Velocity = v * 0.05f;
        prettySparkleParticle1.DrawVerticalAxis = false;
        if ((double) num4 == 1.0)
        {
          PrettySparkleParticle prettySparkleParticle3 = prettySparkleParticle1;
          prettySparkleParticle3.Scale = prettySparkleParticle3.Scale * 1.5f;
          PrettySparkleParticle prettySparkleParticle4 = prettySparkleParticle1;
          prettySparkleParticle4.Velocity = prettySparkleParticle4.Velocity * 1.5f;
          PrettySparkleParticle prettySparkleParticle5 = prettySparkleParticle1;
          prettySparkleParticle5.LocalPosition = prettySparkleParticle5.LocalPosition - prettySparkleParticle1.Velocity * 4f;
        }
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle1);
      }
      for (float num5 = 0.0f; (double) num5 < 2.0; ++num5)
      {
        PrettySparkleParticle prettySparkleParticle6 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        Vector2 vector2 = ((float) (0.78539818525314331 + 3.1415927410125732 * (double) num5) + num2).ToRotationVector2() * 4f;
        prettySparkleParticle6.ColorTint = new Color(1f, 0.4f, 0.2f, 1f);
        prettySparkleParticle6.LocalPosition = settings.PositionInWorld;
        prettySparkleParticle6.Rotation = vector2.ToRotation();
        prettySparkleParticle6.Scale = new Vector2(4f, 1f) * 0.7f * num3;
        prettySparkleParticle6.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle6.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle6.TimeToLive = num1;
        prettySparkleParticle6.FadeOutEnd = num1;
        prettySparkleParticle6.FadeInEnd = num1 / 2f;
        prettySparkleParticle6.FadeOutStart = num1 / 2f;
        PrettySparkleParticle prettySparkleParticle7 = prettySparkleParticle6;
        prettySparkleParticle7.LocalPosition = prettySparkleParticle7.LocalPosition - vector2 * num1 * 0.25f;
        prettySparkleParticle6.Velocity = vector2 * 0.05f;
        prettySparkleParticle6.DrawVerticalAxis = false;
        if ((double) num5 == 1.0)
        {
          PrettySparkleParticle prettySparkleParticle8 = prettySparkleParticle6;
          prettySparkleParticle8.Scale = prettySparkleParticle8.Scale * 1.5f;
          PrettySparkleParticle prettySparkleParticle9 = prettySparkleParticle6;
          prettySparkleParticle9.Velocity = prettySparkleParticle9.Velocity * 1.5f;
          PrettySparkleParticle prettySparkleParticle10 = prettySparkleParticle6;
          prettySparkleParticle10.LocalPosition = prettySparkleParticle10.LocalPosition - prettySparkleParticle6.Velocity * 4f;
        }
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle6);
        for (int index = 0; index < 1; ++index)
        {
          Dust dust1 = Dust.NewDustPerfect(settings.PositionInWorld, 6, new Vector2?(vector2.RotatedBy((double) Main.rand.NextFloatDirection() * 6.2831854820251465 * 0.02500000037252903) * Main.rand.NextFloat()));
          dust1.noGravity = true;
          dust1.scale = 1.4f;
          Dust dust2 = Dust.NewDustPerfect(settings.PositionInWorld, 6, new Vector2?(-vector2.RotatedBy((double) Main.rand.NextFloatDirection() * 6.2831854820251465 * 0.02500000037252903) * Main.rand.NextFloat()));
          dust2.noGravity = true;
          dust2.scale = 1.4f;
        }
      }
    }

    private static void Spawn_LeafCrystalPassive(ParticleOrchestraSettings settings)
    {
      float num1 = 90f;
      float num2 = 6.28318548f * Main.rand.NextFloat();
      float num3 = 3f;
      for (float num4 = 0.0f; (double) num4 < (double) num3; ++num4)
      {
        PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        Vector2 v = (6.28318548f / num3 * num4 + num2).ToRotationVector2() * 4f;
        prettySparkleParticle.ColorTint = new Color(0.3f, 0.6f, 0.3f, 0.5f);
        prettySparkleParticle.LocalPosition = settings.PositionInWorld;
        prettySparkleParticle.Rotation = v.ToRotation();
        prettySparkleParticle.Scale = new Vector2(4f, 1f) * 0.4f;
        prettySparkleParticle.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle.TimeToLive = num1;
        prettySparkleParticle.FadeOutEnd = num1;
        prettySparkleParticle.FadeInEnd = 10f;
        prettySparkleParticle.FadeOutStart = 10f;
        prettySparkleParticle.AdditiveAmount = 0.5f;
        prettySparkleParticle.Velocity = Vector2.Zero;
        prettySparkleParticle.DrawVerticalAxis = false;
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle);
      }
    }

    private static void Spawn_LeafCrystalShot(ParticleOrchestraSettings settings)
    {
      int num = 30;
      PrettySparkleParticle prettySparkleParticle1 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
      Vector2 movementVector = settings.MovementVector;
      Color rgb = Main.hslToRgb((float) settings.UniqueInfoPiece / (float) byte.MaxValue, 1f, 0.5f);
      Color color = Color.Lerp(rgb, Color.Gold, (float) ((double) rgb.R / (double) byte.MaxValue * 0.5));
      prettySparkleParticle1.ColorTint = color;
      prettySparkleParticle1.LocalPosition = settings.PositionInWorld;
      prettySparkleParticle1.Rotation = movementVector.ToRotation();
      prettySparkleParticle1.Scale = new Vector2(4f, 1f) * 1f;
      prettySparkleParticle1.FadeInNormalizedTime = 5E-06f;
      prettySparkleParticle1.FadeOutNormalizedTime = 1f;
      prettySparkleParticle1.TimeToLive = (float) num;
      prettySparkleParticle1.FadeOutEnd = (float) num;
      prettySparkleParticle1.FadeInEnd = (float) (num / 2);
      prettySparkleParticle1.FadeOutStart = (float) (num / 2);
      prettySparkleParticle1.AdditiveAmount = 0.5f;
      prettySparkleParticle1.Velocity = settings.MovementVector;
      PrettySparkleParticle prettySparkleParticle2 = prettySparkleParticle1;
      prettySparkleParticle2.LocalPosition = prettySparkleParticle2.LocalPosition - prettySparkleParticle1.Velocity * 4f;
      prettySparkleParticle1.DrawVerticalAxis = false;
      Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle1);
    }

    private static void Spawn_TrueNightsEdge(ParticleOrchestraSettings settings)
    {
      float num1 = 30f;
      float num2 = 0.0f;
      for (float num3 = 0.0f; (double) num3 < 3.0; num3 += 2f)
      {
        PrettySparkleParticle prettySparkleParticle1 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        Vector2 v = ((float) (0.78539818525314331 + 0.78539818525314331 * (double) num3) + num2).ToRotationVector2() * 4f;
        prettySparkleParticle1.ColorTint = new Color(0.3f, 0.6f, 0.3f, 0.5f);
        prettySparkleParticle1.LocalPosition = settings.PositionInWorld;
        prettySparkleParticle1.Rotation = v.ToRotation();
        prettySparkleParticle1.Scale = new Vector2(4f, 1f) * 1.1f;
        prettySparkleParticle1.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle1.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle1.TimeToLive = num1;
        prettySparkleParticle1.FadeOutEnd = num1;
        prettySparkleParticle1.FadeInEnd = num1 / 2f;
        prettySparkleParticle1.FadeOutStart = num1 / 2f;
        prettySparkleParticle1.AdditiveAmount = 0.35f;
        PrettySparkleParticle prettySparkleParticle2 = prettySparkleParticle1;
        prettySparkleParticle2.LocalPosition = prettySparkleParticle2.LocalPosition - v * num1 * 0.25f;
        prettySparkleParticle1.Velocity = v;
        prettySparkleParticle1.DrawVerticalAxis = false;
        if ((double) num3 == 1.0)
        {
          PrettySparkleParticle prettySparkleParticle3 = prettySparkleParticle1;
          prettySparkleParticle3.Scale = prettySparkleParticle3.Scale * 1.5f;
          PrettySparkleParticle prettySparkleParticle4 = prettySparkleParticle1;
          prettySparkleParticle4.Velocity = prettySparkleParticle4.Velocity * 1.5f;
          PrettySparkleParticle prettySparkleParticle5 = prettySparkleParticle1;
          prettySparkleParticle5.LocalPosition = prettySparkleParticle5.LocalPosition - prettySparkleParticle1.Velocity * 4f;
        }
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle1);
      }
      for (float num4 = 0.0f; (double) num4 < 3.0; num4 += 2f)
      {
        PrettySparkleParticle prettySparkleParticle6 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        Vector2 vector2 = ((float) (0.78539818525314331 + 0.78539818525314331 * (double) num4) + num2).ToRotationVector2() * 4f;
        prettySparkleParticle6.ColorTint = new Color(0.6f, 1f, 0.2f, 1f);
        prettySparkleParticle6.LocalPosition = settings.PositionInWorld;
        prettySparkleParticle6.Rotation = vector2.ToRotation();
        prettySparkleParticle6.Scale = new Vector2(4f, 1f) * 0.7f;
        prettySparkleParticle6.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle6.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle6.TimeToLive = num1;
        prettySparkleParticle6.FadeOutEnd = num1;
        prettySparkleParticle6.FadeInEnd = num1 / 2f;
        prettySparkleParticle6.FadeOutStart = num1 / 2f;
        PrettySparkleParticle prettySparkleParticle7 = prettySparkleParticle6;
        prettySparkleParticle7.LocalPosition = prettySparkleParticle7.LocalPosition - vector2 * num1 * 0.25f;
        prettySparkleParticle6.Velocity = vector2;
        prettySparkleParticle6.DrawVerticalAxis = false;
        if ((double) num4 == 1.0)
        {
          PrettySparkleParticle prettySparkleParticle8 = prettySparkleParticle6;
          prettySparkleParticle8.Scale = prettySparkleParticle8.Scale * 1.5f;
          PrettySparkleParticle prettySparkleParticle9 = prettySparkleParticle6;
          prettySparkleParticle9.Velocity = prettySparkleParticle9.Velocity * 1.5f;
          PrettySparkleParticle prettySparkleParticle10 = prettySparkleParticle6;
          prettySparkleParticle10.LocalPosition = prettySparkleParticle10.LocalPosition - prettySparkleParticle6.Velocity * 4f;
        }
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle6);
        for (int index = 0; index < 2; ++index)
        {
          Dust dust1 = Dust.NewDustPerfect(settings.PositionInWorld, 75, new Vector2?(vector2.RotatedBy((double) Main.rand.NextFloatDirection() * 6.2831854820251465 * 0.02500000037252903) * Main.rand.NextFloat()));
          dust1.noGravity = true;
          dust1.scale = 1.4f;
          Dust dust2 = Dust.NewDustPerfect(settings.PositionInWorld, 75, new Vector2?(-vector2.RotatedBy((double) Main.rand.NextFloatDirection() * 6.2831854820251465 * 0.02500000037252903) * Main.rand.NextFloat()));
          dust2.noGravity = true;
          dust2.scale = 1.4f;
        }
      }
    }

    private static void Spawn_NightsEdge(ParticleOrchestraSettings settings)
    {
      float num1 = 30f;
      float num2 = 0.0f;
      for (float num3 = 0.0f; (double) num3 < 3.0; ++num3)
      {
        PrettySparkleParticle prettySparkleParticle1 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        Vector2 v = ((float) (0.78539818525314331 + 0.78539818525314331 * (double) num3) + num2).ToRotationVector2() * 4f;
        prettySparkleParticle1.ColorTint = new Color(0.25f, 0.1f, 0.5f, 0.5f);
        prettySparkleParticle1.LocalPosition = settings.PositionInWorld;
        prettySparkleParticle1.Rotation = v.ToRotation();
        prettySparkleParticle1.Scale = new Vector2(2f, 1f) * 1.1f;
        prettySparkleParticle1.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle1.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle1.TimeToLive = num1;
        prettySparkleParticle1.FadeOutEnd = num1;
        prettySparkleParticle1.FadeInEnd = num1 / 2f;
        prettySparkleParticle1.FadeOutStart = num1 / 2f;
        prettySparkleParticle1.AdditiveAmount = 0.35f;
        PrettySparkleParticle prettySparkleParticle2 = prettySparkleParticle1;
        prettySparkleParticle2.LocalPosition = prettySparkleParticle2.LocalPosition - v * num1 * 0.25f;
        prettySparkleParticle1.Velocity = v;
        prettySparkleParticle1.DrawVerticalAxis = false;
        if ((double) num3 == 1.0)
        {
          PrettySparkleParticle prettySparkleParticle3 = prettySparkleParticle1;
          prettySparkleParticle3.Scale = prettySparkleParticle3.Scale * 1.5f;
          PrettySparkleParticle prettySparkleParticle4 = prettySparkleParticle1;
          prettySparkleParticle4.Velocity = prettySparkleParticle4.Velocity * 1.5f;
          PrettySparkleParticle prettySparkleParticle5 = prettySparkleParticle1;
          prettySparkleParticle5.LocalPosition = prettySparkleParticle5.LocalPosition - prettySparkleParticle1.Velocity * 4f;
        }
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle1);
      }
      for (float num4 = 0.0f; (double) num4 < 3.0; ++num4)
      {
        PrettySparkleParticle prettySparkleParticle6 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        Vector2 v = ((float) (0.78539818525314331 + 0.78539818525314331 * (double) num4) + num2).ToRotationVector2() * 4f;
        prettySparkleParticle6.ColorTint = new Color(0.5f, 0.25f, 1f, 1f);
        prettySparkleParticle6.LocalPosition = settings.PositionInWorld;
        prettySparkleParticle6.Rotation = v.ToRotation();
        prettySparkleParticle6.Scale = new Vector2(2f, 1f) * 0.7f;
        prettySparkleParticle6.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle6.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle6.TimeToLive = num1;
        prettySparkleParticle6.FadeOutEnd = num1;
        prettySparkleParticle6.FadeInEnd = num1 / 2f;
        prettySparkleParticle6.FadeOutStart = num1 / 2f;
        PrettySparkleParticle prettySparkleParticle7 = prettySparkleParticle6;
        prettySparkleParticle7.LocalPosition = prettySparkleParticle7.LocalPosition - v * num1 * 0.25f;
        prettySparkleParticle6.Velocity = v;
        prettySparkleParticle6.DrawVerticalAxis = false;
        if ((double) num4 == 1.0)
        {
          PrettySparkleParticle prettySparkleParticle8 = prettySparkleParticle6;
          prettySparkleParticle8.Scale = prettySparkleParticle8.Scale * 1.5f;
          PrettySparkleParticle prettySparkleParticle9 = prettySparkleParticle6;
          prettySparkleParticle9.Velocity = prettySparkleParticle9.Velocity * 1.5f;
          PrettySparkleParticle prettySparkleParticle10 = prettySparkleParticle6;
          prettySparkleParticle10.LocalPosition = prettySparkleParticle10.LocalPosition - prettySparkleParticle6.Velocity * 4f;
        }
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle6);
      }
    }

    private static void Spawn_SilverBulletSparkle(ParticleOrchestraSettings settings)
    {
      double num1 = (double) Main.rand.NextFloat() * 6.2831854820251465;
      Vector2 movementVector = settings.MovementVector;
      Vector2 vector2_1 = new Vector2((float) ((double) Main.rand.NextFloat() * 0.20000000298023224 + 0.40000000596046448));
      double num2 = (double) Main.rand.NextFloat();
      float num3 = 1.57079637f;
      Vector2 vector2_2 = Main.rand.NextVector2Circular(4f, 4f) * vector2_1;
      PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
      prettySparkleParticle.AccelerationPerFrame = -movementVector * 1f / 30f;
      prettySparkleParticle.Velocity = movementVector;
      prettySparkleParticle.ColorTint = Color.White;
      prettySparkleParticle.LocalPosition = settings.PositionInWorld + vector2_2;
      prettySparkleParticle.Rotation = num3;
      prettySparkleParticle.Scale = vector2_1;
      prettySparkleParticle.FadeInNormalizedTime = 5E-06f;
      prettySparkleParticle.FadeOutNormalizedTime = 0.95f;
      prettySparkleParticle.FadeInEnd = 10f;
      prettySparkleParticle.FadeOutStart = 20f;
      prettySparkleParticle.FadeOutEnd = 30f;
      prettySparkleParticle.TimeToLive = 30f;
      Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle);
    }

    private static void Spawn_PaladinsHammer(ParticleOrchestraSettings settings)
    {
      float num1 = Main.rand.NextFloat() * 6.28318548f;
      float num2 = 1f;
      for (float num3 = 0.0f; (double) num3 < 1.0; num3 += 1f / num2)
      {
        float num4 = (float) (0.60000002384185791 + (double) Main.rand.NextFloat() * 0.34999999403953552);
        Vector2 vector2_1 = settings.MovementVector * num4;
        Vector2 vector2_2 = new Vector2((float) ((double) Main.rand.NextFloat() * 0.40000000596046448 + 0.20000000298023224));
        float f = num1 + Main.rand.NextFloat() * 6.28318548f;
        float num5 = 1.57079637f;
        Vector2 vector2_3 = 0.1f * vector2_2;
        Vector2 vector2_4 = Main.rand.NextVector2Circular(12f, 12f) * vector2_2;
        PrettySparkleParticle prettySparkleParticle1 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        prettySparkleParticle1.AccelerationPerFrame = -vector2_1 * 1f / 30f;
        prettySparkleParticle1.Velocity = vector2_1 + f.ToRotationVector2() * 2f * num4;
        prettySparkleParticle1.ColorTint = new Color(1f, 0.8f, 0.4f, 0.0f);
        prettySparkleParticle1.LocalPosition = settings.PositionInWorld + vector2_4;
        prettySparkleParticle1.Rotation = num5;
        prettySparkleParticle1.Scale = vector2_2;
        prettySparkleParticle1.FadeInNormalizedTime = 5E-06f;
        prettySparkleParticle1.FadeOutNormalizedTime = 0.95f;
        prettySparkleParticle1.TimeToLive = 40f;
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle1);
        PrettySparkleParticle prettySparkleParticle2 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        prettySparkleParticle2.AccelerationPerFrame = -vector2_1 * 1f / 30f;
        prettySparkleParticle2.Velocity = vector2_1 * 0.8f + f.ToRotationVector2() * 2f;
        prettySparkleParticle2.ColorTint = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
        prettySparkleParticle2.LocalPosition = settings.PositionInWorld + vector2_4;
        prettySparkleParticle2.Rotation = num5;
        prettySparkleParticle2.Scale = vector2_2 * 0.6f;
        prettySparkleParticle2.FadeInNormalizedTime = 0.1f;
        prettySparkleParticle2.FadeOutNormalizedTime = 0.9f;
        prettySparkleParticle2.TimeToLive = 60f;
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle2);
      }
      for (int index = 0; index < 2; ++index)
      {
        Color newColor = new Color(1f, 0.7f, 0.3f, 0.0f);
        int dustIndex = Dust.NewDust(settings.PositionInWorld, 0, 0, 267, newColor: newColor);
        Main.dust[dustIndex].velocity = Main.rand.NextVector2Circular(2f, 2f);
        Main.dust[dustIndex].velocity += settings.MovementVector * (float) (0.5 + 0.5 * (double) Main.rand.NextFloat()) * 1.4f;
        Main.dust[dustIndex].noGravity = true;
        Main.dust[dustIndex].scale = 0.1f;
        Main.dust[dustIndex].position += Main.rand.NextVector2Circular(16f, 16f);
        Main.dust[dustIndex].velocity = settings.MovementVector;
        if (dustIndex != 6000)
        {
          Dust dust = Dust.CloneDust(dustIndex);
          dust.scale /= 2f;
          dust.fadeIn *= 0.75f;
          dust.color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        }
      }
    }

    private static void Spawn_PrincessWeapon(ParticleOrchestraSettings settings)
    {
      float num1 = Main.rand.NextFloat() * 6.28318548f;
      float num2 = 1f;
      for (float num3 = 0.0f; (double) num3 < 1.0; num3 += 1f / num2)
      {
        Vector2 vector2_1 = settings.MovementVector * (float) (0.60000002384185791 + (double) Main.rand.NextFloat() * 0.34999999403953552);
        Vector2 vector2_2 = new Vector2((float) ((double) Main.rand.NextFloat() * 0.40000000596046448 + 0.20000000298023224));
        float f = num1 + Main.rand.NextFloat() * 6.28318548f;
        float num4 = 1.57079637f;
        Vector2 vector2_3 = 0.1f * vector2_2;
        float num5 = 60f;
        Vector2 vector2_4 = Main.rand.NextVector2Circular(8f, 8f) * vector2_2;
        PrettySparkleParticle prettySparkleParticle1 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        prettySparkleParticle1.Velocity = f.ToRotationVector2() * vector2_3 + vector2_1;
        prettySparkleParticle1.AccelerationPerFrame = f.ToRotationVector2() * -(vector2_3 / num5) - vector2_1 * 1f / 30f;
        prettySparkleParticle1.AccelerationPerFrame = -vector2_1 * 1f / 60f;
        prettySparkleParticle1.Velocity = vector2_1 * 0.66f;
        prettySparkleParticle1.ColorTint = Main.hslToRgb((float) ((0.92000001668930054 + (double) Main.rand.NextFloat() * 0.019999999552965164) % 1.0), 1f, (float) (0.40000000596046448 + (double) Main.rand.NextFloat() * 0.25));
        prettySparkleParticle1.ColorTint.A = (byte) 0;
        prettySparkleParticle1.LocalPosition = settings.PositionInWorld + vector2_4;
        prettySparkleParticle1.Rotation = num4;
        prettySparkleParticle1.Scale = vector2_2;
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle1);
        PrettySparkleParticle prettySparkleParticle2 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        prettySparkleParticle2.Velocity = f.ToRotationVector2() * vector2_3 + vector2_1;
        prettySparkleParticle2.AccelerationPerFrame = f.ToRotationVector2() * -(vector2_3 / num5) - vector2_1 * 1f / 15f;
        prettySparkleParticle2.AccelerationPerFrame = -vector2_1 * 1f / 60f;
        prettySparkleParticle2.Velocity = vector2_1 * 0.66f;
        prettySparkleParticle2.ColorTint = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
        prettySparkleParticle2.LocalPosition = settings.PositionInWorld + vector2_4;
        prettySparkleParticle2.Rotation = num4;
        prettySparkleParticle2.Scale = vector2_2 * 0.6f;
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle2);
      }
      for (int index = 0; index < 2; ++index)
      {
        Color rgb = Main.hslToRgb((float) ((0.92000001668930054 + (double) Main.rand.NextFloat() * 0.019999999552965164) % 1.0), 1f, (float) (0.40000000596046448 + (double) Main.rand.NextFloat() * 0.25));
        int dustIndex = Dust.NewDust(settings.PositionInWorld, 0, 0, 267, newColor: rgb);
        Main.dust[dustIndex].velocity = Main.rand.NextVector2Circular(2f, 2f);
        Main.dust[dustIndex].velocity += settings.MovementVector * (float) (0.5 + 0.5 * (double) Main.rand.NextFloat()) * 1.4f;
        Main.dust[dustIndex].noGravity = true;
        Main.dust[dustIndex].scale = 0.1f;
        Main.dust[dustIndex].position += Main.rand.NextVector2Circular(16f, 16f);
        Main.dust[dustIndex].velocity = settings.MovementVector;
        if (dustIndex != 6000)
        {
          Dust dust = Dust.CloneDust(dustIndex);
          dust.scale /= 2f;
          dust.fadeIn *= 0.75f;
          dust.color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        }
      }
    }

    private static void Spawn_StardustPunch(ParticleOrchestraSettings settings)
    {
      float num1 = Main.rand.NextFloat() * 6.28318548f;
      float num2 = 1f;
      for (float num3 = 0.0f; (double) num3 < 1.0; num3 += 1f / num2)
      {
        Vector2 vector2_1 = settings.MovementVector * (float) (0.30000001192092896 + (double) Main.rand.NextFloat() * 0.34999999403953552);
        Vector2 vector2_2 = new Vector2((float) ((double) Main.rand.NextFloat() * 0.40000000596046448 + 0.40000000596046448));
        float f = num1 + Main.rand.NextFloat() * 6.28318548f;
        float num4 = 1.57079637f;
        Vector2 vector2_3 = 0.1f * vector2_2;
        float num5 = 60f;
        Vector2 vector2_4 = Main.rand.NextVector2Circular(8f, 8f) * vector2_2;
        PrettySparkleParticle prettySparkleParticle1 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        prettySparkleParticle1.Velocity = f.ToRotationVector2() * vector2_3 + vector2_1;
        prettySparkleParticle1.AccelerationPerFrame = f.ToRotationVector2() * -(vector2_3 / num5) - vector2_1 * 1f / 60f;
        prettySparkleParticle1.ColorTint = Main.hslToRgb((float) ((0.60000002384185791 + (double) Main.rand.NextFloat() * 0.05000000074505806) % 1.0), 1f, (float) (0.40000000596046448 + (double) Main.rand.NextFloat() * 0.25));
        prettySparkleParticle1.ColorTint.A = (byte) 0;
        prettySparkleParticle1.LocalPosition = settings.PositionInWorld + vector2_4;
        prettySparkleParticle1.Rotation = num4;
        prettySparkleParticle1.Scale = vector2_2;
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle1);
        PrettySparkleParticle prettySparkleParticle2 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        prettySparkleParticle2.Velocity = f.ToRotationVector2() * vector2_3 + vector2_1;
        prettySparkleParticle2.AccelerationPerFrame = f.ToRotationVector2() * -(vector2_3 / num5) - vector2_1 * 1f / 30f;
        prettySparkleParticle2.ColorTint = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
        prettySparkleParticle2.LocalPosition = settings.PositionInWorld + vector2_4;
        prettySparkleParticle2.Rotation = num4;
        prettySparkleParticle2.Scale = vector2_2 * 0.6f;
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle2);
      }
      for (int index = 0; index < 2; ++index)
      {
        Color rgb = Main.hslToRgb((float) ((0.5899999737739563 + (double) Main.rand.NextFloat() * 0.05000000074505806) % 1.0), 1f, (float) (0.40000000596046448 + (double) Main.rand.NextFloat() * 0.25));
        int dustIndex = Dust.NewDust(settings.PositionInWorld, 0, 0, 267, newColor: rgb);
        Main.dust[dustIndex].velocity = Main.rand.NextVector2Circular(2f, 2f);
        Main.dust[dustIndex].velocity += settings.MovementVector * (float) (0.5 + 0.5 * (double) Main.rand.NextFloat()) * 1.4f;
        Main.dust[dustIndex].noGravity = true;
        Main.dust[dustIndex].scale = (float) (0.60000002384185791 + (double) Main.rand.NextFloat() * 2.0);
        Main.dust[dustIndex].position += Main.rand.NextVector2Circular(16f, 16f);
        if (dustIndex != 6000)
        {
          Dust dust = Dust.CloneDust(dustIndex);
          dust.scale /= 2f;
          dust.fadeIn *= 0.75f;
          dust.color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        }
      }
    }

    private static void Spawn_RainbowRodHit(ParticleOrchestraSettings settings)
    {
      float num1 = Main.rand.NextFloat() * 6.28318548f;
      float num2 = 6f;
      float num3 = Main.rand.NextFloat();
      for (float num4 = 0.0f; (double) num4 < 1.0; num4 += 1f / num2)
      {
        Vector2 vector2_1 = settings.MovementVector * Main.rand.NextFloatDirection() * 0.15f;
        Vector2 vector2_2 = new Vector2((float) ((double) Main.rand.NextFloat() * 0.40000000596046448 + 0.40000000596046448));
        float f = num1 + Main.rand.NextFloat() * 6.28318548f;
        float num5 = 1.57079637f;
        Vector2 vector2_3 = 1.5f * vector2_2;
        float num6 = 60f;
        Vector2 vector2_4 = Main.rand.NextVector2Circular(8f, 8f) * vector2_2;
        PrettySparkleParticle prettySparkleParticle1 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        prettySparkleParticle1.Velocity = f.ToRotationVector2() * vector2_3 + vector2_1;
        prettySparkleParticle1.AccelerationPerFrame = f.ToRotationVector2() * -(vector2_3 / num6) - vector2_1 * 1f / 60f;
        prettySparkleParticle1.ColorTint = Main.hslToRgb((float) (((double) num3 + (double) Main.rand.NextFloat() * 0.33000001311302185) % 1.0), 1f, (float) (0.40000000596046448 + (double) Main.rand.NextFloat() * 0.25));
        prettySparkleParticle1.ColorTint.A = (byte) 0;
        prettySparkleParticle1.LocalPosition = settings.PositionInWorld + vector2_4;
        prettySparkleParticle1.Rotation = num5;
        prettySparkleParticle1.Scale = vector2_2;
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle1);
        PrettySparkleParticle prettySparkleParticle2 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        prettySparkleParticle2.Velocity = f.ToRotationVector2() * vector2_3 + vector2_1;
        prettySparkleParticle2.AccelerationPerFrame = f.ToRotationVector2() * -(vector2_3 / num6) - vector2_1 * 1f / 60f;
        prettySparkleParticle2.ColorTint = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
        prettySparkleParticle2.LocalPosition = settings.PositionInWorld + vector2_4;
        prettySparkleParticle2.Rotation = num5;
        prettySparkleParticle2.Scale = vector2_2 * 0.6f;
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle2);
      }
      for (int index = 0; index < 12; ++index)
      {
        Color rgb = Main.hslToRgb((float) (((double) num3 + (double) Main.rand.NextFloat() * 0.11999999731779099) % 1.0), 1f, (float) (0.40000000596046448 + (double) Main.rand.NextFloat() * 0.25));
        int dustIndex = Dust.NewDust(settings.PositionInWorld, 0, 0, 267, newColor: rgb);
        Main.dust[dustIndex].velocity = Main.rand.NextVector2Circular(1f, 1f);
        Main.dust[dustIndex].velocity += settings.MovementVector * Main.rand.NextFloatDirection() * 0.5f;
        Main.dust[dustIndex].noGravity = true;
        Main.dust[dustIndex].scale = (float) (0.60000002384185791 + (double) Main.rand.NextFloat() * 0.89999997615814209);
        Main.dust[dustIndex].fadeIn = (float) (0.699999988079071 + (double) Main.rand.NextFloat() * 0.800000011920929);
        if (dustIndex != 6000)
        {
          Dust dust = Dust.CloneDust(dustIndex);
          dust.scale /= 2f;
          dust.fadeIn *= 0.75f;
          dust.color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        }
      }
    }

    private static void Spawn_BlackLightningSmall(ParticleOrchestraSettings settings)
    {
      float num1 = Main.rand.NextFloat() * 6.28318548f;
      float num2 = (float) Main.rand.Next(1, 3);
      float num3 = 0.7f;
      int i = 916;
      Main.instance.LoadProjectile(i);
      Color color1 = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      Color indigo = Color.Indigo with { A = 0 };
      for (float num4 = 0.0f; (double) num4 < 1.0; num4 += 1f / num2)
      {
        float f = (float) (6.2831854820251465 * (double) num4 + (double) num1 + (double) Main.rand.NextFloatDirection() * 0.25);
        float num5 = (float) ((double) Main.rand.NextFloat() * 4.0 + 0.10000000149011612);
        Vector2 initialLocalPosition = Main.rand.NextVector2Circular(12f, 12f) * num3;
        Color.Lerp(Color.Lerp(Color.Black, indigo, Main.rand.NextFloat() * 0.5f), color1, Main.rand.NextFloat() * 0.6f);
        Color color2 = new Color(0, 0, 0, (int) byte.MaxValue);
        int num6 = Main.rand.Next(4);
        if (num6 == 1)
          color2 = Color.Lerp(new Color(106, 90, 205, (int) sbyte.MaxValue), Color.Black, (float) (0.10000000149011612 + 0.699999988079071 * (double) Main.rand.NextFloat()));
        if (num6 == 2)
          color2 = Color.Lerp(new Color(106, 90, 205, 60), Color.Black, (float) (0.10000000149011612 + 0.800000011920929 * (double) Main.rand.NextFloat()));
        RandomizedFrameParticle randomizedFrameParticle = ParticleOrchestrator._poolRandomizedFrame.RequestParticle();
        randomizedFrameParticle.SetBasicInfo(TextureAssets.Projectile[i], new Rectangle?(), Vector2.Zero, initialLocalPosition);
        randomizedFrameParticle.SetTypeInfo(Main.projFrames[i], 2, 24f);
        randomizedFrameParticle.Velocity = f.ToRotationVector2() * num5 * new Vector2(1f, 0.5f) * 0.2f + settings.MovementVector;
        randomizedFrameParticle.ColorTint = color2;
        randomizedFrameParticle.LocalPosition = settings.PositionInWorld + initialLocalPosition;
        randomizedFrameParticle.Rotation = randomizedFrameParticle.Velocity.ToRotation();
        randomizedFrameParticle.Scale = Vector2.One * 0.5f;
        randomizedFrameParticle.FadeInNormalizedTime = 0.01f;
        randomizedFrameParticle.FadeOutNormalizedTime = 0.5f;
        randomizedFrameParticle.ScaleVelocity = new Vector2(0.025f);
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) randomizedFrameParticle);
      }
    }

    private static void Spawn_BlackLightningHit(ParticleOrchestraSettings settings)
    {
      float num1 = Main.rand.NextFloat() * 6.28318548f;
      float num2 = 7f;
      float num3 = 0.7f;
      int i = 916;
      Main.instance.LoadProjectile(i);
      Color color1 = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      Color indigo = Color.Indigo with { A = 0 };
      for (float num4 = 0.0f; (double) num4 < 1.0; num4 += 1f / num2)
      {
        float f = (float) (6.2831854820251465 * (double) num4 + (double) num1 + (double) Main.rand.NextFloatDirection() * 0.25);
        float num5 = (float) ((double) Main.rand.NextFloat() * 4.0 + 0.10000000149011612);
        Vector2 initialLocalPosition = Main.rand.NextVector2Circular(12f, 12f) * num3;
        Color.Lerp(Color.Lerp(Color.Black, indigo, Main.rand.NextFloat() * 0.5f), color1, Main.rand.NextFloat() * 0.6f);
        Color color2 = new Color(0, 0, 0, (int) byte.MaxValue);
        int num6 = Main.rand.Next(4);
        if (num6 == 1)
          color2 = Color.Lerp(new Color(106, 90, 205, (int) sbyte.MaxValue), Color.Black, (float) (0.10000000149011612 + 0.699999988079071 * (double) Main.rand.NextFloat()));
        if (num6 == 2)
          color2 = Color.Lerp(new Color(106, 90, 205, 60), Color.Black, (float) (0.10000000149011612 + 0.800000011920929 * (double) Main.rand.NextFloat()));
        RandomizedFrameParticle randomizedFrameParticle = ParticleOrchestrator._poolRandomizedFrame.RequestParticle();
        randomizedFrameParticle.SetBasicInfo(TextureAssets.Projectile[i], new Rectangle?(), Vector2.Zero, initialLocalPosition);
        randomizedFrameParticle.SetTypeInfo(Main.projFrames[i], 2, 24f);
        randomizedFrameParticle.Velocity = f.ToRotationVector2() * num5 * new Vector2(1f, 0.5f);
        randomizedFrameParticle.ColorTint = color2;
        randomizedFrameParticle.LocalPosition = settings.PositionInWorld + initialLocalPosition;
        randomizedFrameParticle.Rotation = f;
        randomizedFrameParticle.Scale = Vector2.One;
        randomizedFrameParticle.FadeInNormalizedTime = 0.01f;
        randomizedFrameParticle.FadeOutNormalizedTime = 0.5f;
        randomizedFrameParticle.ScaleVelocity = new Vector2(0.05f);
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) randomizedFrameParticle);
      }
    }

    private static void Spawn_StellarTune(ParticleOrchestraSettings settings)
    {
      float num1 = Main.rand.NextFloat() * 6.28318548f;
      float num2 = 5f;
      Vector2 vector2_1 = new Vector2(0.7f);
      for (float num3 = 0.0f; (double) num3 < 1.0; num3 += 1f / num2)
      {
        float f = (float) (6.2831854820251465 * (double) num3 + (double) num1 + (double) Main.rand.NextFloatDirection() * 0.25);
        Vector2 vector2_2 = 1.5f * vector2_1;
        float num4 = 60f;
        Vector2 vector2_3 = Main.rand.NextVector2Circular(12f, 12f) * vector2_1;
        Color color = Color.Lerp(Color.Gold, Color.HotPink, Main.rand.NextFloat());
        if (Main.rand.Next(2) == 0)
          color = Color.Lerp(Color.Violet, Color.HotPink, Main.rand.NextFloat());
        PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        prettySparkleParticle.Velocity = f.ToRotationVector2() * vector2_2;
        prettySparkleParticle.AccelerationPerFrame = f.ToRotationVector2() * -(vector2_2 / num4);
        prettySparkleParticle.ColorTint = color;
        prettySparkleParticle.LocalPosition = settings.PositionInWorld + vector2_3;
        prettySparkleParticle.Rotation = f;
        prettySparkleParticle.Scale = vector2_1 * (float) ((double) Main.rand.NextFloat() * 0.800000011920929 + 0.20000000298023224);
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle);
      }
    }

    private static void Spawn_Keybrand(ParticleOrchestraSettings settings)
    {
      float num1 = Main.rand.NextFloat() * 6.28318548f;
      float num2 = 3f;
      Vector2 vector2_1 = new Vector2(0.7f);
      for (float num3 = 0.0f; (double) num3 < 1.0; num3 += 1f / num2)
      {
        float f = (float) (6.2831854820251465 * (double) num3 + (double) num1 + (double) Main.rand.NextFloatDirection() * 0.10000000149011612);
        Vector2 vector2_2 = 1.5f * vector2_1;
        float num4 = 60f;
        Vector2 vector2_3 = Main.rand.NextVector2Circular(4f, 4f) * vector2_1;
        PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
        prettySparkleParticle.Velocity = f.ToRotationVector2() * vector2_2;
        prettySparkleParticle.AccelerationPerFrame = f.ToRotationVector2() * -(vector2_2 / num4);
        prettySparkleParticle.ColorTint = Color.Lerp(Color.Gold, Color.OrangeRed, Main.rand.NextFloat());
        prettySparkleParticle.LocalPosition = settings.PositionInWorld + vector2_3;
        prettySparkleParticle.Rotation = f;
        prettySparkleParticle.Scale = vector2_1 * 0.8f;
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) prettySparkleParticle);
      }
      float num5 = num1 + (float) (1.0 / (double) num2 / 2.0 * 6.2831854820251465);
      float num6 = Main.rand.NextFloat() * 6.28318548f;
      for (float num7 = 0.0f; (double) num7 < 1.0; num7 += 1f / num2)
      {
        float f = (float) (6.2831854820251465 * (double) num7 + (double) num6 + (double) Main.rand.NextFloatDirection() * 0.10000000149011612);
        Vector2 vector2_4 = 1f * vector2_1;
        float timeToLive = 30f;
        Color color = Color.Lerp(Color.White, Color.Lerp(Color.Gold, Color.OrangeRed, Main.rand.NextFloat()), 0.5f) with
        {
          A = 0
        };
        Vector2 vector2_5 = Main.rand.NextVector2Circular(4f, 4f) * vector2_1;
        FadingParticle fadingParticle = ParticleOrchestrator._poolFading.RequestParticle();
        fadingParticle.SetBasicInfo(TextureAssets.Extra[98], new Rectangle?(), Vector2.Zero, Vector2.Zero);
        fadingParticle.SetTypeInfo(timeToLive);
        fadingParticle.Velocity = f.ToRotationVector2() * vector2_4;
        fadingParticle.AccelerationPerFrame = f.ToRotationVector2() * -(vector2_4 / timeToLive);
        fadingParticle.ColorTint = color;
        fadingParticle.LocalPosition = settings.PositionInWorld + f.ToRotationVector2() * vector2_4 * vector2_1 * timeToLive * 0.2f + vector2_5;
        fadingParticle.Rotation = f + 1.57079637f;
        fadingParticle.FadeInNormalizedTime = 0.3f;
        fadingParticle.FadeOutNormalizedTime = 0.4f;
        fadingParticle.Scale = new Vector2(0.5f, 1.2f) * 0.8f * vector2_1;
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) fadingParticle);
      }
      float num8 = 1f;
      float num9 = Main.rand.NextFloat() * 6.28318548f;
      for (float num10 = 0.0f; (double) num10 < 1.0; num10 += 1f / num8)
      {
        float num11 = 6.28318548f * num10 + num9;
        float timeToLive = 30f;
        Color color = Color.Lerp(Color.CornflowerBlue, Color.White, Main.rand.NextFloat()) with
        {
          A = 127
        };
        Vector2 vector2_6 = Main.rand.NextVector2Circular(4f, 4f) * vector2_1;
        Vector2 vector2_7 = Main.rand.NextVector2Square(0.7f, 1.3f);
        FadingParticle fadingParticle = ParticleOrchestrator._poolFading.RequestParticle();
        fadingParticle.SetBasicInfo(TextureAssets.Extra[174], new Rectangle?(), Vector2.Zero, Vector2.Zero);
        fadingParticle.SetTypeInfo(timeToLive);
        fadingParticle.ColorTint = color;
        fadingParticle.LocalPosition = settings.PositionInWorld + vector2_6;
        fadingParticle.Rotation = num11 + 1.57079637f;
        fadingParticle.FadeInNormalizedTime = 0.1f;
        fadingParticle.FadeOutNormalizedTime = 0.4f;
        fadingParticle.Scale = new Vector2(0.1f, 0.1f) * vector2_1;
        fadingParticle.ScaleVelocity = vector2_7 * 1f / 60f;
        fadingParticle.ScaleAcceleration = vector2_7 * -0.0166666675f / 60f;
        Main.ParticleSystem_World_OverPlayers.Add((IParticle) fadingParticle);
      }
    }

    private static void Spawn_FlameWaders(ParticleOrchestraSettings settings)
    {
      float timeToLive = 60f;
      for (int index = -1; index <= 1; ++index)
      {
        int i = (int) Main.rand.NextFromList<short>((short) 326, (short) 327, (short) 328);
        Main.instance.LoadProjectile(i);
        Player player = Main.player[(int) settings.IndexOfPlayerWhoInvokedThis];
        float num = (float) ((double) Main.rand.NextFloat() * 0.89999997615814209 + 0.10000000149011612);
        Vector2 vector2 = settings.PositionInWorld + new Vector2((float) index * 5.33333349f, 0.0f);
        FlameParticle flameParticle = ParticleOrchestrator._poolFlame.RequestParticle();
        flameParticle.SetBasicInfo(TextureAssets.Projectile[i], new Rectangle?(), Vector2.Zero, vector2);
        flameParticle.SetTypeInfo(timeToLive, (int) settings.IndexOfPlayerWhoInvokedThis, player.cFlameWaker);
        flameParticle.FadeOutNormalizedTime = 0.4f;
        flameParticle.ScaleAcceleration = Vector2.One * num * -0.0166666675f / timeToLive;
        flameParticle.Scale = Vector2.One * num;
        Main.ParticleSystem_World_BehindPlayers.Add((IParticle) flameParticle);
        if (Main.rand.Next(16) == 0)
        {
          Dust dust = Dust.NewDustDirect(vector2, 4, 4, 6, Alpha: 100);
          if (Main.rand.Next(2) == 0)
          {
            dust.noGravity = true;
            dust.fadeIn = 1.15f;
          }
          else
            dust.scale = 0.6f;
          dust.velocity *= 0.6f;
          dust.velocity.Y -= 1.2f;
          dust.noLight = true;
          dust.position.Y -= 4f;
          dust.shader = GameShaders.Armor.GetSecondaryShader(player.cFlameWaker, player);
        }
      }
    }

    private static void Spawn_WallOfFleshGoatMountFlames(ParticleOrchestraSettings settings)
    {
      float timeToLive = 50f;
      for (int index = -1; index <= 1; ++index)
      {
        int i = (int) Main.rand.NextFromList<short>((short) 326, (short) 327, (short) 328);
        Main.instance.LoadProjectile(i);
        Player player = Main.player[(int) settings.IndexOfPlayerWhoInvokedThis];
        float num = (float) ((double) Main.rand.NextFloat() * 0.89999997615814209 + 0.10000000149011612);
        Vector2 vector2 = settings.PositionInWorld + new Vector2((float) index * 5.33333349f, 0.0f);
        FlameParticle flameParticle = ParticleOrchestrator._poolFlame.RequestParticle();
        flameParticle.SetBasicInfo(TextureAssets.Projectile[i], new Rectangle?(), Vector2.Zero, vector2);
        flameParticle.SetTypeInfo(timeToLive, (int) settings.IndexOfPlayerWhoInvokedThis, player.cMount);
        flameParticle.FadeOutNormalizedTime = 0.3f;
        flameParticle.ScaleAcceleration = Vector2.One * num * -0.0166666675f / timeToLive;
        flameParticle.Scale = Vector2.One * num;
        Main.ParticleSystem_World_BehindPlayers.Add((IParticle) flameParticle);
        if (Main.rand.Next(8) == 0)
        {
          Dust dust = Dust.NewDustDirect(vector2, 4, 4, 6, Alpha: 100);
          if (Main.rand.Next(2) == 0)
          {
            dust.noGravity = true;
            dust.fadeIn = 1.15f;
          }
          else
            dust.scale = 0.6f;
          dust.velocity *= 0.6f;
          dust.velocity.Y -= 1.2f;
          dust.noLight = true;
          dust.position.Y -= 4f;
          dust.shader = GameShaders.Armor.GetSecondaryShader(player.cMount, player);
        }
      }
    }
  }
}
