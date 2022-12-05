// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIWorkshopImportWorldListItem
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.Audio;
using Terraria.GameContent.UI.States;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Social;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIWorkshopImportWorldListItem : AWorldListItem
  {
    private Asset<Texture2D> _dividerTexture;
    private Asset<Texture2D> _workshopIconTexture;
    private Asset<Texture2D> _innerPanelTexture;
    private UIElement _worldIcon;
    private UIText _buttonLabel;
    private Asset<Texture2D> _buttonImportTexture;
    private int _orderInList;
    public UIState _ownerState;

    public UIWorkshopImportWorldListItem(UIState ownerState, WorldFileData data, int orderInList)
    {
      this._ownerState = ownerState;
      this._orderInList = orderInList;
      this._data = data;
      this.LoadTextures();
      this.InitializeAppearance();
      this._worldIcon = this.GetIconElement();
      this._worldIcon.Left.Set(4f, 0.0f);
      this._worldIcon.OnLeftDoubleClick += new UIElement.MouseEvent(this.ImportButtonClick_ImportWorldToLocalFiles);
      this.Append(this._worldIcon);
      float pixels1 = 4f;
      UIImageButton element = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/ButtonPlay", (AssetRequestMode) 1));
      element.VAlign = 1f;
      element.Left.Set(pixels1, 0.0f);
      element.OnLeftClick += new UIElement.MouseEvent(this.ImportButtonClick_ImportWorldToLocalFiles);
      this.OnLeftDoubleClick += new UIElement.MouseEvent(this.ImportButtonClick_ImportWorldToLocalFiles);
      element.OnMouseOver += new UIElement.MouseEvent(this.PlayMouseOver);
      element.OnMouseOut += new UIElement.MouseEvent(this.ButtonMouseOut);
      this.Append((UIElement) element);
      float pixels2 = pixels1 + 24f;
      this._buttonLabel = new UIText("");
      this._buttonLabel.VAlign = 1f;
      this._buttonLabel.Left.Set(pixels2, 0.0f);
      this._buttonLabel.Top.Set(-3f, 0.0f);
      this.Append((UIElement) this._buttonLabel);
      element.SetSnapPoint("Import", orderInList);
    }

    private void LoadTextures()
    {
      this._dividerTexture = Main.Assets.Request<Texture2D>("Images/UI/Divider", (AssetRequestMode) 1);
      this._innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground", (AssetRequestMode) 1);
      this._workshopIconTexture = TextureAssets.Extra[243];
    }

    private void InitializeAppearance()
    {
      this.Height.Set(96f, 0.0f);
      this.Width.Set(0.0f, 1f);
      this.SetPadding(6f);
      this.SetColorsToNotHovered();
    }

    private void SetColorsToHovered()
    {
      this.BackgroundColor = new Color(73, 94, 171);
      this.BorderColor = new Color(89, 116, 213);
    }

    private void SetColorsToNotHovered()
    {
      this.BackgroundColor = new Color(63, 82, 151) * 0.7f;
      this.BorderColor = new Color(89, 116, 213) * 0.7f;
    }

    private void PlayMouseOver(UIMouseEvent evt, UIElement listeningElement) => this._buttonLabel.SetText(Language.GetTextValue("UI.Import"));

    private void ButtonMouseOut(UIMouseEvent evt, UIElement listeningElement) => this._buttonLabel.SetText("");

    private void ImportButtonClick_ImportWorldToLocalFiles(
      UIMouseEvent evt,
      UIElement listeningElement)
    {
      if (listeningElement != evt.Target)
        return;
      SoundEngine.PlaySound(10);
      Main.clrInput();
      UIVirtualKeyboard state = new UIVirtualKeyboard(Language.GetTextValue("Workshop.EnterNewNameForImportedWorld"), this._data.Name, new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingName), new Action(this.GoToMainMenu), allowEmpty: true);
      state.SetMaxInputLength(27);
      state.Text = this._data.Name;
      Main.MenuUI.SetState((UIState) state);
    }

    private void OnFinishedSettingName(string name)
    {
      string newDisplayName = name.Trim();
      if (SocialAPI.Workshop == null)
        return;
      SocialAPI.Workshop.ImportDownloadedWorldToLocalSaves(this._data, newDisplayName: newDisplayName);
    }

    private void GoToMainMenu()
    {
      SoundEngine.PlaySound(11);
      Main.menuMode = 0;
    }

    public override int CompareTo(object obj) => obj is UIWorkshopImportWorldListItem importWorldListItem ? this._orderInList.CompareTo(importWorldListItem._orderInList) : base.CompareTo(obj);

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

    private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width)
    {
      spriteBatch.Draw(this._innerPanelTexture.Value, position, new Rectangle?(new Rectangle(0, 0, 8, this._innerPanelTexture.Height())), Color.White);
      spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + 8f, position.Y), new Rectangle?(new Rectangle(8, 0, 8, this._innerPanelTexture.Height())), Color.White, 0.0f, Vector2.Zero, new Vector2((float) (((double) width - 16.0) / 8.0), 1f), SpriteEffects.None, 0.0f);
      spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2((float) ((double) position.X + (double) width - 8.0), position.Y), new Rectangle?(new Rectangle(16, 0, 8, this._innerPanelTexture.Height())), Color.White);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      base.DrawSelf(spriteBatch);
      CalculatedStyle innerDimensions = this.GetInnerDimensions();
      CalculatedStyle dimensions = this._worldIcon.GetDimensions();
      float x1 = dimensions.X + dimensions.Width;
      Color color = this._data.IsValid ? Color.White : Color.Gray;
      string worldName = this._data.GetWorldName(true);
      if (worldName != null)
        Utils.DrawBorderString(spriteBatch, worldName, new Vector2(x1 + 6f, dimensions.Y - 2f), color);
      spriteBatch.Draw(this._workshopIconTexture.Value, new Vector2((float) ((double) this.GetDimensions().X + (double) this.GetDimensions().Width - (double) this._workshopIconTexture.Width() - 3.0), this.GetDimensions().Y + 2f), new Rectangle?(this._workshopIconTexture.Frame()), Color.White);
      spriteBatch.Draw(this._dividerTexture.Value, new Vector2(x1, innerDimensions.Y + 21f), new Rectangle?(), Color.White, 0.0f, Vector2.Zero, new Vector2((float) (((double) this.GetDimensions().X + (double) this.GetDimensions().Width - (double) x1) / 8.0), 1f), SpriteEffects.None, 0.0f);
      Vector2 position = new Vector2(x1 + 6f, innerDimensions.Y + 29f);
      float width1 = 100f;
      this.DrawPanel(spriteBatch, position, width1);
      string expertText = "";
      Color gameModeColor = Color.White;
      this.GetDifficulty(out expertText, out gameModeColor);
      float x2 = FontAssets.MouseText.Value.MeasureString(expertText).X;
      float x3 = (float) ((double) width1 * 0.5 - (double) x2 * 0.5);
      Utils.DrawBorderString(spriteBatch, expertText, position + new Vector2(x3, 3f), gameModeColor);
      position.X += width1 + 5f;
      if (this._data._worldSizeName != null)
      {
        float width2 = 150f;
        if (!GameCulture.FromCultureName(GameCulture.CultureName.English).IsActive)
          width2 += 40f;
        this.DrawPanel(spriteBatch, position, width2);
        string textValue = Language.GetTextValue("UI.WorldSizeFormat", (object) this._data.WorldSizeName);
        float x4 = FontAssets.MouseText.Value.MeasureString(textValue).X;
        float x5 = (float) ((double) width2 * 0.5 - (double) x4 * 0.5);
        Utils.DrawBorderString(spriteBatch, textValue, position + new Vector2(x5, 3f), Color.White);
        position.X += width2 + 5f;
      }
      float width3 = innerDimensions.X + innerDimensions.Width - position.X;
      this.DrawPanel(spriteBatch, position, width3);
      string textValue1 = Language.GetTextValue("UI.WorldCreatedFormat", !GameCulture.FromCultureName(GameCulture.CultureName.English).IsActive ? (object) this._data.CreationTime.ToShortDateString() : (object) this._data.CreationTime.ToString("d MMMM yyyy"));
      float x6 = FontAssets.MouseText.Value.MeasureString(textValue1).X;
      float x7 = (float) ((double) width3 * 0.5 - (double) x6 * 0.5);
      Utils.DrawBorderString(spriteBatch, textValue1, position + new Vector2(x7, 3f), Color.White);
      position.X += width3 + 5f;
    }
  }
}
