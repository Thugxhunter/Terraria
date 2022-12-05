// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIWorkshopPublishResourcePackListItem
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI.States;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIWorkshopPublishResourcePackListItem : UIPanel
  {
    private ResourcePack _data;
    private Asset<Texture2D> _dividerTexture;
    private Asset<Texture2D> _workshopIconTexture;
    private Asset<Texture2D> _iconBorderTexture;
    private Asset<Texture2D> _innerPanelTexture;
    private UIElement _iconArea;
    private UIElement _publishButton;
    private int _orderInList;
    private UIState _ownerState;
    private const int ICON_SIZE = 64;
    private const int ICON_BORDER_PADDING = 4;
    private const int HEIGHT_FLUFF = 10;
    private bool _canPublish;

    public UIWorkshopPublishResourcePackListItem(
      UIState ownerState,
      ResourcePack data,
      int orderInList,
      bool canBePublished)
    {
      this._ownerState = ownerState;
      this._orderInList = orderInList;
      this._data = data;
      this._canPublish = canBePublished;
      this.LoadTextures();
      this.InitializeAppearance();
      UIElement element1 = new UIElement()
      {
        Width = new StyleDimension(72f, 0.0f),
        Height = new StyleDimension(72f, 0.0f),
        Left = new StyleDimension(4f, 0.0f),
        VAlign = 0.5f
      };
      element1.OnLeftDoubleClick += new UIElement.MouseEvent(this.PublishButtonClick_ImportResourcePackToLocalFiles);
      UIImage uiImage1 = new UIImage(data.Icon);
      uiImage1.Width = new StyleDimension(-6f, 1f);
      uiImage1.Height = new StyleDimension(-6f, 1f);
      uiImage1.HAlign = 0.5f;
      uiImage1.VAlign = 0.5f;
      uiImage1.ScaleToFit = true;
      uiImage1.AllowResizingDimensions = false;
      uiImage1.IgnoresMouseInteraction = true;
      UIImage element2 = uiImage1;
      UIImage uiImage2 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders", (AssetRequestMode) 1));
      uiImage2.HAlign = 0.5f;
      uiImage2.VAlign = 0.5f;
      uiImage2.IgnoresMouseInteraction = true;
      UIImage element3 = uiImage2;
      element1.Append((UIElement) element2);
      element1.Append((UIElement) element3);
      this.Append(element1);
      this._iconArea = element1;
      this._publishButton = (UIElement) new UIIconTextButton(Language.GetText("Workshop.Publish"), Color.White, "Images/UI/Workshop/Publish");
      this._publishButton.HAlign = 1f;
      this._publishButton.VAlign = 1f;
      this._publishButton.OnLeftClick += new UIElement.MouseEvent(this.PublishButtonClick_ImportResourcePackToLocalFiles);
      this.OnLeftDoubleClick += new UIElement.MouseEvent(this.PublishButtonClick_ImportResourcePackToLocalFiles);
      this.Append(this._publishButton);
      this._publishButton.SetSnapPoint("Publish", orderInList);
    }

    private void LoadTextures()
    {
      this._dividerTexture = Main.Assets.Request<Texture2D>("Images/UI/Divider", (AssetRequestMode) 1);
      this._innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground", (AssetRequestMode) 1);
      this._iconBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders", (AssetRequestMode) 1);
      this._workshopIconTexture = TextureAssets.Extra[243];
    }

    private void InitializeAppearance()
    {
      this.Height.Set(82f, 0.0f);
      this.Width.Set(0.0f, 1f);
      this.SetPadding(6f);
      this.SetColorsToNotHovered();
    }

    private void SetColorsToHovered()
    {
      this.BackgroundColor = new Color(73, 94, 171);
      this.BorderColor = new Color(89, 116, 213);
      if (this._canPublish)
        return;
      this.BorderColor = new Color(150, 150, 150) * 1f;
      this.BackgroundColor = Color.Lerp(this.BackgroundColor, new Color(120, 120, 120), 0.5f) * 1f;
    }

    private void SetColorsToNotHovered()
    {
      this.BackgroundColor = new Color(63, 82, 151) * 0.7f;
      this.BorderColor = new Color(89, 116, 213) * 0.7f;
      if (this._canPublish)
        return;
      this.BorderColor = new Color((int) sbyte.MaxValue, (int) sbyte.MaxValue, (int) sbyte.MaxValue) * 0.7f;
      this.BackgroundColor = Color.Lerp(new Color(63, 82, 151), new Color(80, 80, 80), 0.5f) * 0.7f;
    }

    private void PublishButtonClick_ImportResourcePackToLocalFiles(
      UIMouseEvent evt,
      UIElement listeningElement)
    {
      if (listeningElement != evt.Target || this.TryMovingToRejectionMenuIfNeeded())
        return;
      Main.MenuUI.SetState((UIState) new WorkshopPublishInfoStateForResourcePack(this._ownerState, this._data));
    }

    private bool TryMovingToRejectionMenuIfNeeded()
    {
      if (this._canPublish)
        return false;
      SoundEngine.PlaySound(10);
      Main.instance.RejectionMenuInfo = new RejectionMenuInfo()
      {
        TextToShow = Language.GetTextValue("Workshop.ReportIssue_CannotPublishZips"),
        ExitAction = new ReturnFromRejectionMenuAction(this.RejectionMenuExitAction)
      };
      Main.menuMode = 1000001;
      return true;
    }

    private void RejectionMenuExitAction()
    {
      SoundEngine.PlaySound(11);
      if (this._ownerState == null)
      {
        Main.menuMode = 0;
      }
      else
      {
        Main.menuMode = 888;
        Main.MenuUI.SetState(this._ownerState);
      }
    }

    public override int CompareTo(object obj) => obj is UIWorkshopPublishResourcePackListItem resourcePackListItem ? this._orderInList.CompareTo(resourcePackListItem._orderInList) : base.CompareTo(obj);

    public override void MouseOver(UIMouseEvent evt)
    {
      base.MouseOver(evt);
      this.SetColorsToHovered();
    }

    public override void MouseOut(UIMouseEvent evt)
    {
      base.MouseOut(evt);
      this.SetColorsToNotHovered();
    }

    private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width, float height) => Utils.DrawSplicedPanel(spriteBatch, this._innerPanelTexture.Value, (int) position.X, (int) position.Y, (int) width, (int) height, 10, 10, 10, 10, Color.White);

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      base.DrawSelf(spriteBatch);
      CalculatedStyle innerDimensions = this.GetInnerDimensions();
      CalculatedStyle dimensions = this._iconArea.GetDimensions();
      float num1 = dimensions.X + dimensions.Width;
      Color white1 = Color.White;
      Utils.DrawBorderString(spriteBatch, this._data.Name, new Vector2(num1 + 8f, innerDimensions.Y + 3f), white1);
      double num2 = ((double) innerDimensions.Width - 22.0 - (double) dimensions.Width - (double) this._publishButton.GetDimensions().Width) / 2.0;
      float height = this._publishButton.GetDimensions().Height;
      Vector2 position = new Vector2(num1 + 8f, innerDimensions.Y + innerDimensions.Height - height);
      float width1 = (float) num2;
      this.DrawPanel(spriteBatch, position, width1, height);
      string textValue1 = Language.GetTextValue("UI.Author", (object) this._data.Author);
      Color white2 = Color.White;
      Vector2 vector2_1 = FontAssets.MouseText.Value.MeasureString(textValue1);
      float x1 = vector2_1.X;
      float y1 = vector2_1.Y;
      float x2 = (float) ((double) width1 * 0.5 - (double) x1 * 0.5);
      float num3 = (float) ((double) height * 0.5 - (double) y1 * 0.5);
      Utils.DrawBorderString(spriteBatch, textValue1, position + new Vector2(x2, num3 + 3f), white2);
      position.X += width1 + 5f;
      float width2 = (float) num2;
      this.DrawPanel(spriteBatch, position, width2, height);
      string textValue2 = Language.GetTextValue("UI.Version", (object) this._data.Version.GetFormattedVersion());
      Color white3 = Color.White;
      Vector2 vector2_2 = FontAssets.MouseText.Value.MeasureString(textValue2);
      float x3 = vector2_2.X;
      float y2 = vector2_2.Y;
      float x4 = (float) ((double) width2 * 0.5 - (double) x3 * 0.5);
      float num4 = (float) ((double) height * 0.5 - (double) y2 * 0.5);
      Utils.DrawBorderString(spriteBatch, textValue2, position + new Vector2(x4, num4 + 3f), white3);
      position.X += width2 + 5f;
    }
  }
}
