// Decompiled with JetBrains decompiler
// Type: Terraria.UI.UIElement
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Terraria.GameContent.UI.Elements;

namespace Terraria.UI
{
  public class UIElement : IComparable
  {
    protected readonly List<UIElement> Elements = new List<UIElement>();
    public StyleDimension Top;
    public StyleDimension Left;
    public StyleDimension Width;
    public StyleDimension Height;
    public StyleDimension MaxWidth = StyleDimension.Fill;
    public StyleDimension MaxHeight = StyleDimension.Fill;
    public StyleDimension MinWidth = StyleDimension.Empty;
    public StyleDimension MinHeight = StyleDimension.Empty;
    private bool _isInitialized;
    public bool IgnoresMouseInteraction;
    public bool OverflowHidden;
    public SamplerState OverrideSamplerState;
    public float PaddingTop;
    public float PaddingLeft;
    public float PaddingRight;
    public float PaddingBottom;
    public float MarginTop;
    public float MarginLeft;
    public float MarginRight;
    public float MarginBottom;
    public float HAlign;
    public float VAlign;
    private CalculatedStyle _innerDimensions;
    private CalculatedStyle _dimensions;
    private CalculatedStyle _outerDimensions;
    private static readonly RasterizerState OverflowHiddenRasterizerState = new RasterizerState()
    {
      CullMode = CullMode.None,
      ScissorTestEnable = true
    };
    public bool UseImmediateMode;
    private SnapPoint _snapPoint;
    private static int _idCounter = 0;

    public UIElement Parent { get; private set; }

    public int UniqueId { get; private set; }

    public IEnumerable<UIElement> Children => (IEnumerable<UIElement>) this.Elements;

    public event UIElement.MouseEvent OnLeftMouseDown;

    public event UIElement.MouseEvent OnLeftMouseUp;

    public event UIElement.MouseEvent OnLeftClick;

    public event UIElement.MouseEvent OnLeftDoubleClick;

    public event UIElement.MouseEvent OnRightMouseDown;

    public event UIElement.MouseEvent OnRightMouseUp;

    public event UIElement.MouseEvent OnRightClick;

    public event UIElement.MouseEvent OnRightDoubleClick;

    public event UIElement.MouseEvent OnMouseOver;

    public event UIElement.MouseEvent OnMouseOut;

    public event UIElement.ScrollWheelEvent OnScrollWheel;

    public event UIElement.ElementEvent OnUpdate;

    public bool IsMouseHovering { get; private set; }

    public UIElement() => this.UniqueId = UIElement._idCounter++;

    public void SetSnapPoint(string name, int id, Vector2? anchor = null, Vector2? offset = null)
    {
      if (!anchor.HasValue)
        anchor = new Vector2?(new Vector2(0.5f));
      if (!offset.HasValue)
        offset = new Vector2?(Vector2.Zero);
      this._snapPoint = new SnapPoint(name, id, anchor.Value, offset.Value);
    }

    public bool GetSnapPoint(out SnapPoint point)
    {
      point = this._snapPoint;
      if (this._snapPoint != null)
        this._snapPoint.Calculate(this);
      return this._snapPoint != null;
    }

    public virtual void ExecuteRecursively(UIElement.UIElementAction action)
    {
      action(this);
      foreach (UIElement element in this.Elements)
        element.ExecuteRecursively(action);
    }

    protected virtual void DrawSelf(SpriteBatch spriteBatch)
    {
    }

    protected virtual void DrawChildren(SpriteBatch spriteBatch)
    {
      foreach (UIElement element in this.Elements)
        element.Draw(spriteBatch);
    }

    public void Append(UIElement element)
    {
      element.Remove();
      element.Parent = this;
      this.Elements.Add(element);
      element.Recalculate();
    }

    public void Remove()
    {
      if (this.Parent == null)
        return;
      this.Parent.RemoveChild(this);
    }

    public void RemoveChild(UIElement child)
    {
      this.Elements.Remove(child);
      child.Parent = (UIElement) null;
    }

    public void RemoveAllChildren()
    {
      foreach (UIElement element in this.Elements)
        element.Parent = (UIElement) null;
      this.Elements.Clear();
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
      int num = this.OverflowHidden ? 1 : 0;
      bool useImmediateMode = this.UseImmediateMode;
      RasterizerState rasterizerState = spriteBatch.GraphicsDevice.RasterizerState;
      Rectangle scissorRectangle = spriteBatch.GraphicsDevice.ScissorRectangle;
      SamplerState anisotropicClamp = SamplerState.AnisotropicClamp;
      if (useImmediateMode || this.OverrideSamplerState != null)
      {
        spriteBatch.End();
        spriteBatch.Begin(useImmediateMode ? SpriteSortMode.Immediate : SpriteSortMode.Deferred, BlendState.AlphaBlend, this.OverrideSamplerState != null ? this.OverrideSamplerState : anisotropicClamp, DepthStencilState.None, UIElement.OverflowHiddenRasterizerState, (Effect) null, Main.UIScaleMatrix);
        this.DrawSelf(spriteBatch);
        spriteBatch.End();
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, UIElement.OverflowHiddenRasterizerState, (Effect) null, Main.UIScaleMatrix);
      }
      else
        this.DrawSelf(spriteBatch);
      if (num != 0)
      {
        spriteBatch.End();
        Rectangle clippingRectangle = this.GetClippingRectangle(spriteBatch);
        spriteBatch.GraphicsDevice.ScissorRectangle = clippingRectangle;
        spriteBatch.GraphicsDevice.RasterizerState = UIElement.OverflowHiddenRasterizerState;
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, UIElement.OverflowHiddenRasterizerState, (Effect) null, Main.UIScaleMatrix);
      }
      this.DrawChildren(spriteBatch);
      if (num == 0)
        return;
      spriteBatch.End();
      spriteBatch.GraphicsDevice.ScissorRectangle = scissorRectangle;
      spriteBatch.GraphicsDevice.RasterizerState = rasterizerState;
      spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, rasterizerState, (Effect) null, Main.UIScaleMatrix);
    }

    public virtual void Update(GameTime gameTime)
    {
      if (this.OnUpdate != null)
        this.OnUpdate(this);
      foreach (UIElement element in this.Elements)
        element.Update(gameTime);
    }

    public Rectangle GetClippingRectangle(SpriteBatch spriteBatch)
    {
      Vector2 position1 = new Vector2(this._innerDimensions.X, this._innerDimensions.Y);
      Vector2 position2 = new Vector2(this._innerDimensions.Width, this._innerDimensions.Height) + position1;
      Vector2 vector2_1 = Vector2.Transform(position1, Main.UIScaleMatrix);
      Vector2 vector2_2 = Vector2.Transform(position2, Main.UIScaleMatrix);
      Rectangle rectangle = new Rectangle((int) vector2_1.X, (int) vector2_1.Y, (int) ((double) vector2_2.X - (double) vector2_1.X), (int) ((double) vector2_2.Y - (double) vector2_1.Y));
      int max1 = (int) ((double) Main.screenWidth * (double) Main.UIScale);
      int max2 = (int) ((double) Main.screenHeight * (double) Main.UIScale);
      rectangle.X = Utils.Clamp<int>(rectangle.X, 0, max1);
      rectangle.Y = Utils.Clamp<int>(rectangle.Y, 0, max2);
      rectangle.Width = Utils.Clamp<int>(rectangle.Width, 0, max1 - rectangle.X);
      rectangle.Height = Utils.Clamp<int>(rectangle.Height, 0, max2 - rectangle.Y);
      Rectangle scissorRectangle = spriteBatch.GraphicsDevice.ScissorRectangle;
      int x = Utils.Clamp<int>(rectangle.Left, scissorRectangle.Left, scissorRectangle.Right);
      int y = Utils.Clamp<int>(rectangle.Top, scissorRectangle.Top, scissorRectangle.Bottom);
      int num1 = Utils.Clamp<int>(rectangle.Right, scissorRectangle.Left, scissorRectangle.Right);
      int num2 = Utils.Clamp<int>(rectangle.Bottom, scissorRectangle.Top, scissorRectangle.Bottom);
      return new Rectangle(x, y, num1 - x, num2 - y);
    }

    public virtual List<SnapPoint> GetSnapPoints()
    {
      List<SnapPoint> snapPoints = new List<SnapPoint>();
      SnapPoint point;
      if (this.GetSnapPoint(out point))
        snapPoints.Add(point);
      foreach (UIElement element in this.Elements)
        snapPoints.AddRange((IEnumerable<SnapPoint>) element.GetSnapPoints());
      return snapPoints;
    }

    public virtual void Recalculate()
    {
      CalculatedStyle parentDimensions1 = this.Parent == null ? UserInterface.ActiveInstance.GetDimensions() : this.Parent.GetInnerDimensions();
      if (this.Parent != null && this.Parent is UIList)
        parentDimensions1.Height = float.MaxValue;
      CalculatedStyle parentDimensions2 = this.GetDimensionsBasedOnParentDimensions(parentDimensions1);
      this._outerDimensions = parentDimensions2;
      parentDimensions2.X += this.MarginLeft;
      parentDimensions2.Y += this.MarginTop;
      parentDimensions2.Width -= this.MarginLeft + this.MarginRight;
      parentDimensions2.Height -= this.MarginTop + this.MarginBottom;
      this._dimensions = parentDimensions2;
      parentDimensions2.X += this.PaddingLeft;
      parentDimensions2.Y += this.PaddingTop;
      parentDimensions2.Width -= this.PaddingLeft + this.PaddingRight;
      parentDimensions2.Height -= this.PaddingTop + this.PaddingBottom;
      this._innerDimensions = parentDimensions2;
      this.RecalculateChildren();
    }

    private CalculatedStyle GetDimensionsBasedOnParentDimensions(
      CalculatedStyle parentDimensions)
    {
      CalculatedStyle parentDimensions1;
      parentDimensions1.X = this.Left.GetValue(parentDimensions.Width) + parentDimensions.X;
      parentDimensions1.Y = this.Top.GetValue(parentDimensions.Height) + parentDimensions.Y;
      float min1 = this.MinWidth.GetValue(parentDimensions.Width);
      float max1 = this.MaxWidth.GetValue(parentDimensions.Width);
      float min2 = this.MinHeight.GetValue(parentDimensions.Height);
      float max2 = this.MaxHeight.GetValue(parentDimensions.Height);
      parentDimensions1.Width = MathHelper.Clamp(this.Width.GetValue(parentDimensions.Width), min1, max1);
      parentDimensions1.Height = MathHelper.Clamp(this.Height.GetValue(parentDimensions.Height), min2, max2);
      parentDimensions1.Width += this.MarginLeft + this.MarginRight;
      parentDimensions1.Height += this.MarginTop + this.MarginBottom;
      parentDimensions1.X += (float) ((double) parentDimensions.Width * (double) this.HAlign - (double) parentDimensions1.Width * (double) this.HAlign);
      parentDimensions1.Y += (float) ((double) parentDimensions.Height * (double) this.VAlign - (double) parentDimensions1.Height * (double) this.VAlign);
      return parentDimensions1;
    }

    public UIElement GetElementAt(Vector2 point)
    {
      UIElement uiElement = (UIElement) null;
      for (int index = this.Elements.Count - 1; index >= 0; --index)
      {
        UIElement element = this.Elements[index];
        if (!element.IgnoresMouseInteraction && element.ContainsPoint(point))
        {
          uiElement = element;
          break;
        }
      }
      if (uiElement != null)
        return uiElement.GetElementAt(point);
      if (this.IgnoresMouseInteraction)
        return (UIElement) null;
      return this.ContainsPoint(point) ? this : (UIElement) null;
    }

    public virtual bool ContainsPoint(Vector2 point) => (double) point.X > (double) this._dimensions.X && (double) point.Y > (double) this._dimensions.Y && (double) point.X < (double) this._dimensions.X + (double) this._dimensions.Width && (double) point.Y < (double) this._dimensions.Y + (double) this._dimensions.Height;

    public virtual Rectangle GetViewCullingArea() => this._dimensions.ToRectangle();

    public void SetPadding(float pixels)
    {
      this.PaddingBottom = pixels;
      this.PaddingLeft = pixels;
      this.PaddingRight = pixels;
      this.PaddingTop = pixels;
    }

    public virtual void RecalculateChildren()
    {
      foreach (UIElement element in this.Elements)
        element.Recalculate();
    }

    public CalculatedStyle GetInnerDimensions() => this._innerDimensions;

    public CalculatedStyle GetDimensions() => this._dimensions;

    public CalculatedStyle GetOuterDimensions() => this._outerDimensions;

    public void CopyStyle(UIElement element)
    {
      this.Top = element.Top;
      this.Left = element.Left;
      this.Width = element.Width;
      this.Height = element.Height;
      this.PaddingBottom = element.PaddingBottom;
      this.PaddingLeft = element.PaddingLeft;
      this.PaddingRight = element.PaddingRight;
      this.PaddingTop = element.PaddingTop;
      this.HAlign = element.HAlign;
      this.VAlign = element.VAlign;
      this.MinWidth = element.MinWidth;
      this.MaxWidth = element.MaxWidth;
      this.MinHeight = element.MinHeight;
      this.MaxHeight = element.MaxHeight;
      this.Recalculate();
    }

    public virtual void LeftMouseDown(UIMouseEvent evt)
    {
      if (this.OnLeftMouseDown != null)
        this.OnLeftMouseDown(evt, this);
      if (this.Parent == null)
        return;
      this.Parent.LeftMouseDown(evt);
    }

    public virtual void LeftMouseUp(UIMouseEvent evt)
    {
      if (this.OnLeftMouseUp != null)
        this.OnLeftMouseUp(evt, this);
      if (this.Parent == null)
        return;
      this.Parent.LeftMouseUp(evt);
    }

    public virtual void LeftClick(UIMouseEvent evt)
    {
      if (this.OnLeftClick != null)
        this.OnLeftClick(evt, this);
      if (this.Parent == null)
        return;
      this.Parent.LeftClick(evt);
    }

    public virtual void LeftDoubleClick(UIMouseEvent evt)
    {
      if (this.OnLeftDoubleClick != null)
        this.OnLeftDoubleClick(evt, this);
      if (this.Parent == null)
        return;
      this.Parent.LeftDoubleClick(evt);
    }

    public virtual void RightMouseDown(UIMouseEvent evt)
    {
      if (this.OnRightMouseDown != null)
        this.OnRightMouseDown(evt, this);
      if (this.Parent == null)
        return;
      this.Parent.RightMouseDown(evt);
    }

    public virtual void RightMouseUp(UIMouseEvent evt)
    {
      if (this.OnRightMouseUp != null)
        this.OnRightMouseUp(evt, this);
      if (this.Parent == null)
        return;
      this.Parent.RightMouseUp(evt);
    }

    public virtual void RightClick(UIMouseEvent evt)
    {
      if (this.OnRightClick != null)
        this.OnRightClick(evt, this);
      if (this.Parent == null)
        return;
      this.Parent.RightClick(evt);
    }

    public virtual void RightDoubleClick(UIMouseEvent evt)
    {
      if (this.OnRightDoubleClick != null)
        this.OnRightDoubleClick(evt, this);
      if (this.Parent == null)
        return;
      this.Parent.RightDoubleClick(evt);
    }

    public virtual void MouseOver(UIMouseEvent evt)
    {
      this.IsMouseHovering = true;
      if (this.OnMouseOver != null)
        this.OnMouseOver(evt, this);
      if (this.Parent == null)
        return;
      this.Parent.MouseOver(evt);
    }

    public virtual void MouseOut(UIMouseEvent evt)
    {
      this.IsMouseHovering = false;
      if (this.OnMouseOut != null)
        this.OnMouseOut(evt, this);
      if (this.Parent == null)
        return;
      this.Parent.MouseOut(evt);
    }

    public virtual void ScrollWheel(UIScrollWheelEvent evt)
    {
      if (this.OnScrollWheel != null)
        this.OnScrollWheel(evt, this);
      if (this.Parent == null)
        return;
      this.Parent.ScrollWheel(evt);
    }

    public void Activate()
    {
      if (!this._isInitialized)
        this.Initialize();
      this.OnActivate();
      foreach (UIElement element in this.Elements)
        element.Activate();
    }

    public virtual void OnActivate()
    {
    }

    [Conditional("DEBUG")]
    public void DrawDebugHitbox(BasicDebugDrawer drawer, float colorIntensity = 0.0f)
    {
      if (this.IsMouseHovering)
        colorIntensity += 0.1f;
      Color rgb = Main.hslToRgb(colorIntensity, colorIntensity, 0.5f);
      CalculatedStyle innerDimensions = this.GetInnerDimensions();
      drawer.DrawLine(innerDimensions.Position(), innerDimensions.Position() + new Vector2(innerDimensions.Width, 0.0f), 2f, rgb);
      drawer.DrawLine(innerDimensions.Position() + new Vector2(innerDimensions.Width, 0.0f), innerDimensions.Position() + new Vector2(innerDimensions.Width, innerDimensions.Height), 2f, rgb);
      drawer.DrawLine(innerDimensions.Position() + new Vector2(innerDimensions.Width, innerDimensions.Height), innerDimensions.Position() + new Vector2(0.0f, innerDimensions.Height), 2f, rgb);
      drawer.DrawLine(innerDimensions.Position() + new Vector2(0.0f, innerDimensions.Height), innerDimensions.Position(), 2f, rgb);
      foreach (UIElement element in this.Elements)
        ;
    }

    public void Deactivate()
    {
      this.OnDeactivate();
      foreach (UIElement element in this.Elements)
        element.Deactivate();
    }

    public virtual void OnDeactivate()
    {
    }

    public void Initialize()
    {
      this.OnInitialize();
      this._isInitialized = true;
    }

    public virtual void OnInitialize()
    {
    }

    public virtual int CompareTo(object obj) => 0;

    public delegate void MouseEvent(UIMouseEvent evt, UIElement listeningElement);

    public delegate void ScrollWheelEvent(UIScrollWheelEvent evt, UIElement listeningElement);

    public delegate void ElementEvent(UIElement affectedElement);

    public delegate void UIElementAction(UIElement element);
  }
}
