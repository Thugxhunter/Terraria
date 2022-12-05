// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Golf.GolfHelper
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Physics;

namespace Terraria.GameContent.Golf
{
  public static class GolfHelper
  {
    public const int PointsNeededForLevel1 = 500;
    public const int PointsNeededForLevel2 = 1000;
    public const int PointsNeededForLevel3 = 2000;
    public static readonly PhysicsProperties PhysicsProperties = new PhysicsProperties(0.3f, 0.99f);
    public static readonly GolfHelper.ContactListener Listener = new GolfHelper.ContactListener();
    public static FancyGolfPredictionLine PredictionLine;

    public static BallStepResult StepGolfBall(
      Entity entity,
      ref float angularVelocity)
    {
      return BallCollision.Step(GolfHelper.PhysicsProperties, entity, ref angularVelocity, (IBallContactListener) GolfHelper.Listener);
    }

    public static Vector2 FindVectorOnOval(Vector2 vector, Vector2 radius) => (double) Math.Abs(radius.X) < 9.9999997473787516E-05 || (double) Math.Abs(radius.Y) < 9.9999997473787516E-05 ? Vector2.Zero : Vector2.Normalize(vector / radius) * radius;

    public static GolfHelper.ShotStrength CalculateShotStrength(
      Vector2 shotVector,
      GolfHelper.ClubProperties clubProperties)
    {
      Vector2.Normalize(shotVector);
      double num1 = (double) shotVector.Length();
      float num2 = GolfHelper.FindVectorOnOval(shotVector, clubProperties.MaximumStrength).Length();
      float num3 = GolfHelper.FindVectorOnOval(shotVector, clubProperties.MinimumStrength).Length();
      double min = (double) num3;
      double max = (double) num2;
      double num4 = (double) MathHelper.Clamp((float) num1, (float) min, (float) max);
      return new GolfHelper.ShotStrength((float) (num4 * 32.0), Math.Max((float) ((num4 - (double) num3) / ((double) num2 - (double) num3)), 1f / 1000f), clubProperties.RoughLandResistance);
    }

    public static bool IsPlayerHoldingClub(Player player)
    {
      if (player == null || player.HeldItem == null)
        return false;
      int type = player.HeldItem.type;
      switch (type)
      {
        case 4039:
        case 4092:
        case 4093:
        case 4094:
          return true;
        default:
          if ((uint) (type - 4587) > 11U)
            return false;
          goto case 4039;
      }
    }

    public static GolfHelper.ShotStrength CalculateShotStrength(
      Projectile golfHelper,
      Entity golfBall)
    {
      int num1 = Main.screenWidth;
      if (num1 > Main.screenHeight)
        num1 = Main.screenHeight;
      int num2 = 150;
      int num3 = (num1 - num2) / 2;
      if (num3 < 200)
        num3 = 200;
      float num4 = 300f;
      return (double) golfHelper.ai[0] != 0.0 ? new GolfHelper.ShotStrength() : GolfHelper.CalculateShotStrength((golfHelper.Center - golfBall.Center) / num4, GolfHelper.GetClubPropertiesFromGolfHelper(golfHelper));
    }

    public static GolfHelper.ClubProperties GetClubPropertiesFromGolfHelper(
      Projectile golfHelper)
    {
      return GolfHelper.GetClubProperties((short) Main.player[golfHelper.owner].HeldItem.type);
    }

    public static GolfHelper.ClubProperties GetClubProperties(short itemId)
    {
      Vector2 vector2 = new Vector2(0.25f, 0.25f);
      switch (itemId)
      {
        case 4039:
          return new GolfHelper.ClubProperties(vector2, Vector2.One, 0.0f);
        case 4092:
          return new GolfHelper.ClubProperties(Vector2.Zero, vector2, 0.0f);
        case 4093:
          return new GolfHelper.ClubProperties(vector2, new Vector2(0.65f, 1.5f), 1f);
        case 4094:
          return new GolfHelper.ClubProperties(vector2, new Vector2(1.5f, 0.65f), 0.0f);
        case 4587:
          return new GolfHelper.ClubProperties(vector2, Vector2.One, 0.0f);
        case 4588:
          return new GolfHelper.ClubProperties(Vector2.Zero, vector2, 0.0f);
        case 4589:
          return new GolfHelper.ClubProperties(vector2, new Vector2(0.65f, 1.5f), 1f);
        case 4590:
          return new GolfHelper.ClubProperties(vector2, new Vector2(1.5f, 0.65f), 0.0f);
        case 4591:
          return new GolfHelper.ClubProperties(vector2, Vector2.One, 0.0f);
        case 4592:
          return new GolfHelper.ClubProperties(Vector2.Zero, vector2, 0.0f);
        case 4593:
          return new GolfHelper.ClubProperties(vector2, new Vector2(0.65f, 1.5f), 1f);
        case 4594:
          return new GolfHelper.ClubProperties(vector2, new Vector2(1.5f, 0.65f), 0.0f);
        case 4595:
          return new GolfHelper.ClubProperties(vector2, Vector2.One, 0.0f);
        case 4596:
          return new GolfHelper.ClubProperties(Vector2.Zero, vector2, 0.0f);
        case 4597:
          return new GolfHelper.ClubProperties(vector2, new Vector2(0.65f, 1.5f), 1f);
        case 4598:
          return new GolfHelper.ClubProperties(vector2, new Vector2(1.5f, 0.65f), 0.0f);
        default:
          return new GolfHelper.ClubProperties();
      }
    }

    public static Projectile FindHelperFromGolfBall(Projectile golfBall)
    {
      for (int index = 0; index < 1000; ++index)
      {
        Projectile projectile = Main.projectile[index];
        if (projectile.active && projectile.type == 722 && projectile.owner == golfBall.owner)
          return Main.projectile[index];
      }
      return (Projectile) null;
    }

    public static Projectile FindGolfBallForHelper(Projectile golfHelper)
    {
      for (int index = 0; index < 1000; ++index)
      {
        Projectile golfBall = Main.projectile[index];
        Vector2 shotVector = golfHelper.Center - golfBall.Center;
        if (golfBall.active && ProjectileID.Sets.IsAGolfBall[golfBall.type] && golfBall.owner == golfHelper.owner && GolfHelper.ValidateShot((Entity) golfBall, Main.player[golfHelper.owner], ref shotVector))
          return Main.projectile[index];
      }
      return (Projectile) null;
    }

    public static bool IsGolfBallResting(Projectile golfBall) => (int) golfBall.localAI[1] == 0 || (double) Vector2.Distance(golfBall.position, golfBall.oldPos[golfBall.oldPos.Length - 1]) < 1.0;

    public static bool IsGolfShotValid(Entity golfBall, Player player)
    {
      Vector2 vector2 = golfBall.Center - player.Bottom;
      if (player.direction == -1)
        vector2.X *= -1f;
      return (double) vector2.X >= -16.0 && (double) vector2.X <= 32.0 && (double) vector2.Y <= 16.0 && (double) vector2.Y >= -16.0;
    }

    public static bool ValidateShot(Entity golfBall, Player player, ref Vector2 shotVector)
    {
      Vector2 vector2 = golfBall.Center - player.Bottom;
      if (player.direction == -1)
      {
        vector2.X *= -1f;
        shotVector.X *= -1f;
      }
      float rotation = shotVector.ToRotation();
      if ((double) rotation > 0.0)
        shotVector = shotVector.Length() * new Vector2((float) Math.Cos(0.0), (float) Math.Sin(0.0));
      else if ((double) rotation < -1.5207964181900024)
        shotVector = shotVector.Length() * new Vector2((float) Math.Cos(-1.5207964181900024), (float) Math.Sin(-1.5207964181900024));
      if (player.direction == -1)
        shotVector.X *= -1f;
      return (double) vector2.X >= -16.0 && (double) vector2.X <= 32.0 && (double) vector2.Y <= 16.0 && (double) vector2.Y >= -16.0;
    }

    public static void HitGolfBall(Entity entity, Vector2 velocity, float roughLandResistance)
    {
      Vector2 bottom = entity.Bottom;
      ++bottom.Y;
      Point tileCoordinates = bottom.ToTileCoordinates();
      Tile tile = Main.tile[tileCoordinates.X, tileCoordinates.Y];
      if (tile != null && tile.active())
      {
        TileMaterial byTileId = TileMaterials.GetByTileId(tile.type);
        velocity = Vector2.Lerp(velocity * byTileId.GolfPhysics.ClubImpactDampening, velocity, byTileId.GolfPhysics.ImpactDampeningResistanceEfficiency * roughLandResistance);
      }
      entity.velocity = velocity;
      if (!(entity is Projectile golfBall))
        return;
      golfBall.timeLeft = 18000;
      if ((double) golfBall.ai[1] < 0.0)
        golfBall.ai[1] = 0.0f;
      ++golfBall.ai[1];
      golfBall.localAI[1] = 1f;
      Main.LocalGolfState.RecordSwing(golfBall);
    }

    public static void DrawPredictionLine(
      Entity golfBall,
      Vector2 impactVelocity,
      float chargeProgress,
      float roughLandResistance)
    {
      if (GolfHelper.PredictionLine == null)
        GolfHelper.PredictionLine = new FancyGolfPredictionLine(20);
      GolfHelper.PredictionLine.Update(golfBall, impactVelocity, roughLandResistance);
      GolfHelper.PredictionLine.Draw(Main.Camera, Main.spriteBatch, chargeProgress);
    }

    public struct ClubProperties
    {
      public readonly Vector2 MinimumStrength;
      public readonly Vector2 MaximumStrength;
      public readonly float RoughLandResistance;

      public ClubProperties(
        Vector2 minimumStrength,
        Vector2 maximumStrength,
        float roughLandResistance)
      {
        this.MinimumStrength = minimumStrength;
        this.MaximumStrength = maximumStrength;
        this.RoughLandResistance = roughLandResistance;
      }
    }

    public struct ShotStrength
    {
      public readonly float AbsoluteStrength;
      public readonly float RelativeStrength;
      public readonly float RoughLandResistance;

      public ShotStrength(
        float absoluteStrength,
        float relativeStrength,
        float roughLandResistance)
      {
        this.AbsoluteStrength = absoluteStrength;
        this.RelativeStrength = relativeStrength;
        this.RoughLandResistance = roughLandResistance;
      }
    }

    public class ContactListener : IBallContactListener
    {
      public void OnCollision(
        PhysicsProperties properties,
        ref Vector2 position,
        ref Vector2 velocity,
        ref BallCollisionEvent collision)
      {
        TileMaterial byTileId = TileMaterials.GetByTileId(collision.Tile.type);
        Vector2 vector2_1 = velocity * byTileId.GolfPhysics.SideImpactDampening;
        Vector2 vector2_2 = collision.Normal * Vector2.Dot(velocity, collision.Normal) * (byTileId.GolfPhysics.DirectImpactDampening - byTileId.GolfPhysics.SideImpactDampening);
        velocity = vector2_1 + vector2_2;
        Projectile entity = collision.Entity as Projectile;
        switch (collision.Tile.type)
        {
          case 421:
          case 422:
            float num1 = 2.5f * collision.TimeScale;
            Vector2 vector2_3 = new Vector2(-collision.Normal.Y, collision.Normal.X);
            if (collision.Tile.type == (ushort) 422)
              vector2_3 = -vector2_3;
            float num2 = Vector2.Dot(velocity, vector2_3);
            if ((double) num2 < (double) num1)
            {
              velocity += vector2_3 * MathHelper.Clamp(num1 - num2, 0.0f, num1 * 0.5f);
              break;
            }
            break;
          case 476:
            float num3 = velocity.Length() / collision.TimeScale;
            if ((double) collision.Normal.Y <= -0.0099999997764825821 && (double) num3 <= 100.0)
            {
              velocity *= 0.0f;
              if (entity != null && entity.active)
              {
                this.PutBallInCup(entity, collision);
                break;
              }
              break;
            }
            break;
        }
        if (entity == null || (double) velocity.Y >= -0.30000001192092896 || (double) velocity.Y <= -2.0 || (double) velocity.Length() <= 1.0)
          return;
        Dust dust = Dust.NewDustPerfect(collision.Entity.Center, 31, new Vector2?(collision.Normal), (int) sbyte.MaxValue);
        dust.scale = 0.7f;
        dust.fadeIn = 1f;
        dust.velocity = dust.velocity * 0.5f + Main.rand.NextVector2CircularEdge(0.5f, 0.4f);
      }

      public void PutBallInCup(Projectile proj, BallCollisionEvent collision)
      {
        if (proj.owner == Main.myPlayer && Main.LocalGolfState.ShouldScoreHole)
        {
          Point tileCoordinates = (collision.ImpactPoint - collision.Normal * 0.5f).ToTileCoordinates();
          int owner = proj.owner;
          int num = (int) proj.ai[1];
          int type = proj.type;
          if (num > 1)
            Main.LocalGolfState.SetScoreTime();
          Main.LocalGolfState.RecordBallInfo(proj);
          Main.LocalGolfState.LandBall(proj);
          int golfBallScore = Main.LocalGolfState.GetGolfBallScore(proj);
          if (num > 0)
            Main.player[owner].AccumulateGolfingScore(golfBallScore);
          GolfHelper.ContactListener.PutBallInCup_TextAndEffects(tileCoordinates, owner, num, type);
          Main.LocalGolfState.ResetScoreTime();
          Wiring.HitSwitch(tileCoordinates.X, tileCoordinates.Y);
          NetMessage.SendData(59, number: tileCoordinates.X, number2: ((float) tileCoordinates.Y));
          if (Main.netMode == 1)
            NetMessage.SendData(128, number: owner, number2: ((float) num), number3: ((float) type), number5: tileCoordinates.X, number6: tileCoordinates.Y);
        }
        proj.Kill();
      }

      public static void PutBallInCup_TextAndEffects(
        Point hitLocation,
        int plr,
        int numberOfHits,
        int projid)
      {
        if (numberOfHits == 0)
          return;
        GolfHelper.ContactListener.EmitGolfballExplosion(hitLocation.ToWorldCoordinates(autoAddY: 0.0f));
        string key = "Game.BallBounceResultGolf_Single";
        NetworkText text;
        if (numberOfHits != 1)
          text = NetworkText.FromKey("Game.BallBounceResultGolf_Plural", (object) Main.player[plr].name, (object) NetworkText.FromKey(Lang.GetProjectileName(projid).Key), (object) numberOfHits);
        else
          text = NetworkText.FromKey(key, (object) Main.player[plr].name, (object) NetworkText.FromKey(Lang.GetProjectileName(projid).Key));
        switch (Main.netMode)
        {
          case 0:
          case 1:
            Main.NewText(text.ToString(), G: (byte) 240, B: (byte) 20);
            break;
          case 2:
            ChatHelper.BroadcastChatMessage(text, new Color((int) byte.MaxValue, 240, 20));
            break;
        }
      }

      public void OnPassThrough(
        PhysicsProperties properties,
        ref Vector2 position,
        ref Vector2 velocity,
        ref float angularVelocity,
        ref BallPassThroughEvent collision)
      {
        switch (collision.Type)
        {
          case BallPassThroughType.Water:
            velocity *= 0.91f;
            angularVelocity *= 0.91f;
            break;
          case BallPassThroughType.Honey:
            velocity *= 0.8f;
            angularVelocity *= 0.8f;
            break;
          case BallPassThroughType.Tile:
            TileMaterial byTileId = TileMaterials.GetByTileId(collision.Tile.type);
            velocity *= byTileId.GolfPhysics.PassThroughDampening;
            angularVelocity *= byTileId.GolfPhysics.PassThroughDampening;
            break;
        }
      }

      public static void EmitGolfballExplosion_Old(Vector2 Center) => GolfHelper.ContactListener.EmitGolfballExplosion(Center);

      public static void EmitGolfballExplosion(Vector2 Center)
      {
        SoundEngine.PlaySound(SoundID.Item129, Center);
        for (float num = 0.0f; (double) num < 1.0; num += 0.085f)
        {
          Dust dust = Dust.NewDustPerfect(Center, 278, new Vector2?((num * 6.28318548f).ToRotationVector2() * new Vector2(2f, 0.5f)));
          dust.fadeIn = 1.2f;
          dust.noGravity = true;
          dust.velocity.X *= 0.7f;
          dust.velocity.Y -= 1.5f;
          dust.position.Y += 8f;
          dust.velocity.X *= 2f;
          dust.color = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.5f);
        }
        float num1 = Main.rand.NextFloat();
        float num2 = (float) Main.rand.Next(5, 10);
        for (int index1 = 0; (double) index1 < (double) num2; ++index1)
        {
          int num3 = Main.rand.Next(5, 22);
          Vector2 vector2 = ((float) (((double) index1 - (double) num2 / 2.0) * 6.2831854820251465 / 256.0 - 1.5707963705062866)).ToRotationVector2() * new Vector2(5f, 1f) * (float) (0.25 + (double) Main.rand.NextFloat() * 0.05000000074505806);
          Color rgb = Main.hslToRgb((float) (((double) num1 + (double) index1 / (double) num2) % 1.0), 0.7f, 0.7f) with
          {
            A = 127
          };
          for (int index2 = 0; index2 < num3; ++index2)
          {
            Dust dust = Dust.NewDustPerfect(Center + new Vector2((float) index1 - num2 / 2f, 0.0f) * 2f, 278, new Vector2?(vector2));
            dust.fadeIn = 0.7f;
            dust.scale = 0.7f;
            dust.noGravity = true;
            dust.position.Y += -1f;
            dust.velocity *= (float) index2;
            dust.scale += (float) (0.20000000298023224 - (double) index2 * 0.029999999329447746);
            dust.velocity += Main.rand.NextVector2Circular(0.05f, 0.05f);
            dust.color = rgb;
          }
        }
        for (float num4 = 0.0f; (double) num4 < 1.0; num4 += 0.2f)
        {
          Dust dust = Dust.NewDustPerfect(Center, 278, new Vector2?((num4 * 6.28318548f).ToRotationVector2() * new Vector2(1f, 0.5f)));
          dust.fadeIn = 1.2f;
          dust.noGravity = true;
          dust.velocity.X *= 0.7f;
          dust.velocity.Y -= 0.5f;
          dust.position.Y += 8f;
          dust.velocity.X *= 2f;
          dust.color = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.3f);
        }
        float num5 = Main.rand.NextFloatDirection();
        for (float num6 = 0.0f; (double) num6 < 1.0; num6 += 0.15f)
        {
          Dust dust = Dust.NewDustPerfect(Center, 278, new Vector2?((num5 + num6 * 6.28318548f).ToRotationVector2() * 4f));
          dust.fadeIn = 1.5f;
          dust.velocity *= (float) (0.5 + (double) num6 * 0.800000011920929);
          dust.noGravity = true;
          dust.velocity.X *= 0.35f;
          dust.velocity.Y *= 2f;
          --dust.velocity.Y;
          dust.velocity.Y = -Math.Abs(dust.velocity.Y);
          dust.position += dust.velocity * 3f;
          dust.color = Main.hslToRgb(Main.rand.NextFloat(), 1f, (float) (0.60000002384185791 + (double) Main.rand.NextFloat() * 0.20000000298023224));
        }
      }

      public static void EmitGolfballExplosion_v1(Vector2 Center)
      {
        for (float num = 0.0f; (double) num < 1.0; num += 0.085f)
        {
          Dust dust = Dust.NewDustPerfect(Center, 278, new Vector2?((num * 6.28318548f).ToRotationVector2() * new Vector2(2f, 0.5f)));
          dust.fadeIn = 1.2f;
          dust.noGravity = true;
          dust.velocity.X *= 0.7f;
          dust.velocity.Y -= 1.5f;
          dust.position.Y += 8f;
          dust.color = Color.Lerp(Color.Silver, Color.White, 0.5f);
        }
        for (float num = 0.0f; (double) num < 1.0; num += 0.2f)
        {
          Dust dust = Dust.NewDustPerfect(Center, 278, new Vector2?((num * 6.28318548f).ToRotationVector2() * new Vector2(1f, 0.5f)));
          dust.fadeIn = 1.2f;
          dust.noGravity = true;
          dust.velocity.X *= 0.7f;
          dust.velocity.Y -= 0.5f;
          dust.position.Y += 8f;
          dust.color = Color.Lerp(Color.Silver, Color.White, 0.5f);
        }
        float num1 = Main.rand.NextFloatDirection();
        for (float num2 = 0.0f; (double) num2 < 1.0; num2 += 0.15f)
        {
          Dust dust = Dust.NewDustPerfect(Center, 278, new Vector2?((num1 + num2 * 6.28318548f).ToRotationVector2() * 4f));
          dust.fadeIn = 1.5f;
          dust.velocity *= (float) (0.5 + (double) num2 * 0.800000011920929);
          dust.noGravity = true;
          dust.velocity.X *= 0.35f;
          dust.velocity.Y *= 2f;
          --dust.velocity.Y;
          dust.velocity.Y = -Math.Abs(dust.velocity.Y);
          dust.position += dust.velocity * 3f;
          dust.color = Color.Lerp(Color.Silver, Color.White, 0.5f);
        }
      }
    }
  }
}
