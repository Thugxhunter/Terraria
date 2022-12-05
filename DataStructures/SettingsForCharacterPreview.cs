// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.SettingsForCharacterPreview
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
  public class SettingsForCharacterPreview
  {
    public Vector2 Offset;
    public SettingsForCharacterPreview.SelectionBasedSettings Selected;
    public SettingsForCharacterPreview.SelectionBasedSettings NotSelected;
    public int SpriteDirection = 1;
    public SettingsForCharacterPreview.CustomAnimationCode CustomAnimation;

    public void ApplyTo(Projectile proj, bool walking)
    {
      Projectile projectile = proj;
      projectile.position = projectile.position + this.Offset;
      proj.spriteDirection = this.SpriteDirection;
      proj.direction = this.SpriteDirection;
      if (walking)
        this.Selected.ApplyTo(proj);
      else
        this.NotSelected.ApplyTo(proj);
      if (this.CustomAnimation == null)
        return;
      this.CustomAnimation(proj, walking);
    }

    public SettingsForCharacterPreview WhenSelected(
      int? startFrame = null,
      int? frameCount = null,
      int? delayPerFrame = null,
      bool? bounceLoop = null)
    {
      SettingsForCharacterPreview.Modify(ref this.Selected, startFrame, frameCount, delayPerFrame, bounceLoop);
      return this;
    }

    public SettingsForCharacterPreview WhenNotSelected(
      int? startFrame = null,
      int? frameCount = null,
      int? delayPerFrame = null,
      bool? bounceLoop = null)
    {
      SettingsForCharacterPreview.Modify(ref this.NotSelected, startFrame, frameCount, delayPerFrame, bounceLoop);
      return this;
    }

    private static void Modify(
      ref SettingsForCharacterPreview.SelectionBasedSettings target,
      int? startFrame,
      int? frameCount,
      int? delayPerFrame,
      bool? bounceLoop)
    {
      if (frameCount.HasValue && frameCount.Value < 1)
        frameCount = new int?(1);
      target.StartFrame = startFrame.HasValue ? startFrame.Value : target.StartFrame;
      target.FrameCount = frameCount.HasValue ? frameCount.Value : target.FrameCount;
      target.DelayPerFrame = delayPerFrame.HasValue ? delayPerFrame.Value : target.DelayPerFrame;
      target.BounceLoop = bounceLoop.HasValue ? bounceLoop.Value : target.BounceLoop;
    }

    public SettingsForCharacterPreview WithOffset(Vector2 offset)
    {
      this.Offset = offset;
      return this;
    }

    public SettingsForCharacterPreview WithOffset(float x, float y)
    {
      this.Offset = new Vector2(x, y);
      return this;
    }

    public SettingsForCharacterPreview WithSpriteDirection(
      int spriteDirection)
    {
      this.SpriteDirection = spriteDirection;
      return this;
    }

    public SettingsForCharacterPreview WithCode(
      SettingsForCharacterPreview.CustomAnimationCode customAnimation)
    {
      this.CustomAnimation = customAnimation;
      return this;
    }

    public delegate void CustomAnimationCode(Projectile proj, bool walking);

    public struct SelectionBasedSettings
    {
      public int StartFrame;
      public int FrameCount;
      public int DelayPerFrame;
      public bool BounceLoop;

      public void ApplyTo(Projectile proj)
      {
        if (this.FrameCount == 0)
          return;
        if (proj.frame < this.StartFrame)
          proj.frame = this.StartFrame;
        int num1 = proj.frame - this.StartFrame;
        int num2 = this.FrameCount * this.DelayPerFrame;
        int num3 = num2;
        if (this.BounceLoop)
          num3 = num2 * 2 - this.DelayPerFrame * 2;
        if (++proj.frameCounter >= num3)
          proj.frameCounter = 0;
        int num4 = proj.frameCounter / this.DelayPerFrame;
        if (this.BounceLoop && num4 >= this.FrameCount)
          num4 = this.FrameCount * 2 - num4 - 2;
        proj.frame = this.StartFrame + num4;
      }
    }
  }
}
