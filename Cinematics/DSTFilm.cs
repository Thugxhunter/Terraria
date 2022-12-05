// Decompiled with JetBrains decompiler
// Type: Terraria.Cinematics.DSTFilm
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.Cinematics
{
  public class DSTFilm : Film
  {
    private NPC _deerclops;
    private Projectile _chester;
    private Vector2 _startPoint;
    private Item _oldItem;

    public DSTFilm() => this.BuildSequence();

    public override void OnBegin()
    {
      this.PrepareScene();
      Main.hideUI = true;
      base.OnBegin();
    }

    public override void OnEnd()
    {
      this.ClearScene();
      Main.hideUI = false;
      base.OnEnd();
    }

    private void BuildSequence()
    {
      this.AppendKeyFrames(new FrameEvent(this.EquipDSTShaderItem));
      this.AppendEmptySequence(60);
      this.AppendKeyFrames(new FrameEvent(this.CreateDeerclops), new FrameEvent(this.CreateChester), new FrameEvent(this.ControlPlayer));
      this.AppendEmptySequence(60);
      this.AppendEmptySequence(187);
      this.AppendKeyFrames(new FrameEvent(this.StopBeforeCliff));
      this.AppendEmptySequence(20);
      this.AppendKeyFrames(new FrameEvent(this.TurnPlayerToTheLeft));
      this.AppendEmptySequence(20);
      this.AppendKeyFrames(new FrameEvent(this.DeerclopsAttack));
      this.AppendEmptySequence(60);
      this.AppendKeyFrames(new FrameEvent(this.RemoveDSTShaderItem));
    }

    private void PrepareScene()
    {
      Main.dayTime = true;
      Main.time = 13500.0;
      Main.time = 43638.0;
      Main.windSpeedCurrent = Main.windSpeedTarget = 0.367999971f;
      Main.windCounter = 2011;
      Main.cloudAlpha = 0.0f;
      Main.raining = true;
      Main.rainTime = 3600;
      double num1;
      Main.cloudAlpha = (float) (num1 = 0.89999997615814209);
      Main.oldMaxRaining = (float) num1;
      Main.maxRaining = (float) num1;
      Main.raining = true;
      double num2;
      Main.cloudAlpha = (float) (num2 = 0.60000002384185791);
      Main.oldMaxRaining = (float) num2;
      Main.maxRaining = (float) num2;
      Main.raining = true;
      double num3;
      Main.cloudAlpha = (float) (num3 = 0.60000002384185791);
      Main.oldMaxRaining = (float) num3;
      Main.maxRaining = (float) num3;
      this._startPoint = new Point(4050, 488).ToWorldCoordinates();
      this._startPoint -= new Vector2(1280f, 0.0f);
    }

    private void ClearScene()
    {
      if (this._deerclops != null)
        this._deerclops.active = false;
      if (this._chester != null)
        this._chester.active = false;
      Main.LocalPlayer.isControlledByFilm = false;
    }

    private void EquipDSTShaderItem(FrameEventData evt)
    {
      this._oldItem = Main.LocalPlayer.armor[3];
      Item obj = new Item();
      obj.SetDefaults(5113);
      Main.LocalPlayer.armor[3] = obj;
    }

    private void RemoveDSTShaderItem(FrameEventData evt) => Main.LocalPlayer.armor[3] = this._oldItem;

    private void CreateDeerclops(FrameEventData evt)
    {
      this._deerclops = this.PlaceNPCOnGround(668, this._startPoint);
      this._deerclops.immortal = true;
      this._deerclops.dontTakeDamage = true;
      this._deerclops.takenDamageMultiplier = 0.0f;
      this._deerclops.immune[(int) byte.MaxValue] = 100000;
      this._deerclops.immune[Main.myPlayer] = 100000;
      this._deerclops.ai[0] = -1f;
      this._deerclops.velocity.Y = 4f;
      this._deerclops.velocity.X = 6f;
      this._deerclops.position.X -= 24f;
      this._deerclops.direction = this._deerclops.spriteDirection = 1;
    }

    private NPC PlaceNPCOnGround(int type, Vector2 position)
    {
      int x;
      int y;
      DSTFilm.FindFloorAt(position, out x, out y);
      if (type == 668)
        y -= 240;
      int Start = 100;
      int index = NPC.NewNPC((IEntitySource) new EntitySource_Film(), x, y, type, Start);
      return Main.npc[index];
    }

    private void CreateChester(FrameEventData evt)
    {
      int x;
      int y;
      DSTFilm.FindFloorAt(this._startPoint + new Vector2(110f, 0.0f), out x, out y);
      int Y = y - 240;
      int index = Projectile.NewProjectile((IEntitySource) null, (float) x, (float) Y, 0.0f, 0.0f, 960, 0, 0.0f, Main.myPlayer, -1f);
      this._chester = Main.projectile[index];
      this._chester.velocity.Y = 4f;
      this._chester.velocity.X = 6f;
    }

    private void ControlPlayer(FrameEventData evt)
    {
      Player localPlayer = Main.LocalPlayer;
      localPlayer.isControlledByFilm = true;
      localPlayer.controlRight = true;
      int x;
      int y;
      DSTFilm.FindFloorAt(this._startPoint + new Vector2(150f, 0.0f), out x, out y);
      localPlayer.BottomLeft = new Vector2((float) x, (float) y);
      localPlayer.velocity.X = 6f;
    }

    private void StopBeforeCliff(FrameEventData evt)
    {
      Main.LocalPlayer.controlRight = false;
      this._chester.ai[0] = -2f;
    }

    private void TurnPlayerToTheLeft(FrameEventData evt)
    {
      Main.LocalPlayer.ChangeDir(-1);
      this._chester.velocity = new Vector2(-0.1f, 0.0f);
      this._chester.spriteDirection = this._chester.direction = -1;
      this._deerclops.ai[0] = 1f;
      this._deerclops.ai[1] = 0.0f;
      this._deerclops.TargetClosest();
    }

    private void DeerclopsAttack(FrameEventData evt)
    {
      Main.LocalPlayer.controlJump = true;
      this._chester.velocity.Y = -11.4f;
      this._deerclops.ai[0] = 1f;
      this._deerclops.ai[1] = 0.0f;
      this._deerclops.TargetClosest();
    }

    private static void FindFloorAt(Vector2 position, out int x, out int y)
    {
      x = (int) position.X;
      y = (int) position.Y;
      int i = x / 16;
      int j = y / 16;
      while (!WorldGen.SolidTile(i, j))
        ++j;
      y = j * 16;
    }
  }
}
