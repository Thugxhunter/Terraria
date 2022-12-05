// Decompiled with JetBrains decompiler
// Type: Terraria.Physics.BallStepResult
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.Physics
{
  public struct BallStepResult
  {
    public readonly BallState State;

    private BallStepResult(BallState state) => this.State = state;

    public static BallStepResult OutOfBounds() => new BallStepResult(BallState.OutOfBounds);

    public static BallStepResult Moving() => new BallStepResult(BallState.Moving);

    public static BallStepResult Resting() => new BallStepResult(BallState.Resting);
  }
}
