// Decompiled with JetBrains decompiler
// Type: Terraria.WorldBuilding.GenerationProgress
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.WorldBuilding
{
  public class GenerationProgress
  {
    private string _message = "";
    private double _value;
    private double _totalProgress;
    public double TotalWeight;
    public double CurrentPassWeight = 1.0;

    public string Message
    {
      get => string.Format(this._message, (object) this.Value);
      set => this._message = value.Replace("%", "{0:0.0%}");
    }

    public double Value
    {
      set => this._value = Utils.Clamp<double>(value, 0.0, 1.0);
      get => this._value;
    }

    public double TotalProgress => this.TotalWeight == 0.0 ? 0.0 : (this.Value * this.CurrentPassWeight + this._totalProgress) / this.TotalWeight;

    public void Set(double value) => this.Value = value;

    public void Start(double weight)
    {
      this.CurrentPassWeight = weight;
      this._value = 0.0;
    }

    public void End()
    {
      this._totalProgress += this.CurrentPassWeight;
      this._value = 0.0;
    }
  }
}
