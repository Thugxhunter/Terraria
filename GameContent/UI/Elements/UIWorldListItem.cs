// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIWorldListItem
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.OS;
using System;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI.States;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Social;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIWorldListItem : AWorldListItem
  {
    private Asset<Texture2D> _dividerTexture;
    private Asset<Texture2D> _innerPanelTexture;
    private UIElement _worldIcon;
    private UIText _buttonLabel;
    private UIText _deleteButtonLabel;
    private Asset<Texture2D> _buttonCloudActiveTexture;
    private Asset<Texture2D> _buttonCloudInactiveTexture;
    private Asset<Texture2D> _buttonFavoriteActiveTexture;
    private Asset<Texture2D> _buttonFavoriteInactiveTexture;
    private Asset<Texture2D> _buttonPlayTexture;
    private Asset<Texture2D> _buttonSeedTexture;
    private Asset<Texture2D> _buttonRenameTexture;
    private Asset<Texture2D> _buttonDeleteTexture;
    private UIImageButton _deleteButton;
    private int _orderInList;
    private bool _canBePlayed;

    public bool IsFavorite => this._data.IsFavorite;

    public UIWorldListItem(WorldFileData data, int orderInList, bool canBePlayed)
    {
      this._orderInList = orderInList;
      this._data = data;
      this._canBePlayed = canBePlayed;
      this.LoadTextures();
      this.InitializeAppearance();
      this._worldIcon = this.GetIconElement();
      this._worldIcon.OnLeftDoubleClick += new UIElement.MouseEvent(this.PlayGame);
      this.Append(this._worldIcon);
      if (this._data.DefeatedMoonlord)
      {
        UIImage element = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/IconCompletion", (AssetRequestMode) 1));
        element.HAlign = 0.5f;
        element.VAlign = 0.5f;
        element.Top = new StyleDimension(-10f, 0.0f);
        element.Left = new StyleDimension(-3f, 0.0f);
        element.IgnoresMouseInteraction = true;
        this._worldIcon.Append((UIElement) element);
      }
      float pixels1 = 4f;
      UIImageButton element1 = new UIImageButton(this._buttonPlayTexture);
      element1.VAlign = 1f;
      element1.Left.Set(pixels1, 0.0f);
      element1.OnLeftClick += new UIElement.MouseEvent(this.PlayGame);
      this.OnLeftDoubleClick += new UIElement.MouseEvent(this.PlayGame);
      element1.OnMouseOver += new UIElement.MouseEvent(this.PlayMouseOver);
      element1.OnMouseOut += new UIElement.MouseEvent(this.ButtonMouseOut);
      this.Append((UIElement) element1);
      float pixels2 = pixels1 + 24f;
      UIImageButton element2 = new UIImageButton(this._data.IsFavorite ? this._buttonFavoriteActiveTexture : this._buttonFavoriteInactiveTexture);
      element2.VAlign = 1f;
      element2.Left.Set(pixels2, 0.0f);
      element2.OnLeftClick += new UIElement.MouseEvent(this.FavoriteButtonClick);
      element2.OnMouseOver += new UIElement.MouseEvent(this.FavoriteMouseOver);
      element2.OnMouseOut += new UIElement.MouseEvent(this.ButtonMouseOut);
      element2.SetVisibility(1f, this._data.IsFavorite ? 0.8f : 0.4f);
      this.Append((UIElement) element2);
      float pixels3 = pixels2 + 24f;
      if (SocialAPI.Cloud != null)
      {
        UIImageButton element3 = new UIImageButton(this._data.IsCloudSave ? this._buttonCloudActiveTexture : this._buttonCloudInactiveTexture);
        element3.VAlign = 1f;
        element3.Left.Set(pixels3, 0.0f);
        element3.OnLeftClick += new UIElement.MouseEvent(this.CloudButtonClick);
        element3.OnMouseOver += new UIElement.MouseEvent(this.CloudMouseOver);
        element3.OnMouseOut += new UIElement.MouseEvent(this.ButtonMouseOut);
        element3.SetSnapPoint("Cloud", orderInList);
        this.Append((UIElement) element3);
        pixels3 += 24f;
      }
      if (this._data.WorldGeneratorVersion != 0UL)
      {
        UIImageButton element4 = new UIImageButton(this._buttonSeedTexture);
        element4.VAlign = 1f;
        element4.Left.Set(pixels3, 0.0f);
        element4.OnLeftClick += new UIElement.MouseEvent(this.SeedButtonClick);
        element4.OnMouseOver += new UIElement.MouseEvent(this.SeedMouseOver);
        element4.OnMouseOut += new UIElement.MouseEvent(this.ButtonMouseOut);
        element4.SetSnapPoint("Seed", orderInList);
        this.Append((UIElement) element4);
        pixels3 += 24f;
      }
      UIImageButton element5 = new UIImageButton(this._buttonRenameTexture);
      element5.VAlign = 1f;
      element5.Left.Set(pixels3, 0.0f);
      element5.OnLeftClick += new UIElement.MouseEvent(this.RenameButtonClick);
      element5.OnMouseOver += new UIElement.MouseEvent(this.RenameMouseOver);
      element5.OnMouseOut += new UIElement.MouseEvent(this.ButtonMouseOut);
      element5.SetSnapPoint("Rename", orderInList);
      this.Append((UIElement) element5);
      float num = pixels3 + 24f;
      UIImageButton element6 = new UIImageButton(this._buttonDeleteTexture);
      element6.VAlign = 1f;
      element6.HAlign = 1f;
      if (!this._data.IsFavorite)
        element6.OnLeftClick += new UIElement.MouseEvent(this.DeleteButtonClick);
      element6.OnMouseOver += new UIElement.MouseEvent(this.DeleteMouseOver);
      element6.OnMouseOut += new UIElement.MouseEvent(this.DeleteMouseOut);
      this._deleteButton = element6;
      this.Append((UIElement) element6);
      float pixels4 = num + 4f;
      this._buttonLabel = new UIText("");
      this._buttonLabel.VAlign = 1f;
      this._buttonLabel.Left.Set(pixels4, 0.0f);
      this._buttonLabel.Top.Set(-3f, 0.0f);
      this.Append((UIElement) this._buttonLabel);
      this._deleteButtonLabel = new UIText("");
      this._deleteButtonLabel.VAlign = 1f;
      this._deleteButtonLabel.HAlign = 1f;
      this._deleteButtonLabel.Left.Set(-30f, 0.0f);
      this._deleteButtonLabel.Top.Set(-3f, 0.0f);
      this.Append((UIElement) this._deleteButtonLabel);
      element1.SetSnapPoint("Play", orderInList);
      element2.SetSnapPoint("Favorite", orderInList);
      element5.SetSnapPoint("Rename", orderInList);
      element6.SetSnapPoint("Delete", orderInList);
    }

    private void LoadTextures()
    {
      this._dividerTexture = Main.Assets.Request<Texture2D>("Images/UI/Divider", (AssetRequestMode) 1);
      this._innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground", (AssetRequestMode) 1);
      this._buttonCloudActiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonCloudActive", (AssetRequestMode) 1);
      this._buttonCloudInactiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonCloudInactive", (AssetRequestMode) 1);
      this._buttonFavoriteActiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonFavoriteActive", (AssetRequestMode) 1);
      this._buttonFavoriteInactiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonFavoriteInactive", (AssetRequestMode) 1);
      this._buttonPlayTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonPlay", (AssetRequestMode) 1);
      this._buttonSeedTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonSeed", (AssetRequestMode) 1);
      this._buttonRenameTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonRename", (AssetRequestMode) 1);
      this._buttonDeleteTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonDelete", (AssetRequestMode) 1);
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
      if (this._canBePlayed)
        return;
      this.BorderColor = new Color(150, 150, 150) * 1f;
      this.BackgroundColor = Color.Lerp(this.BackgroundColor, new Color(120, 120, 120), 0.5f) * 1f;
    }

    private void SetColorsToNotHovered()
    {
      this.BackgroundColor = new Color(63, 82, 151) * 0.7f;
      this.BorderColor = new Color(89, 116, 213) * 0.7f;
      if (this._canBePlayed)
        return;
      this.BorderColor = new Color((int) sbyte.MaxValue, (int) sbyte.MaxValue, (int) sbyte.MaxValue) * 0.7f;
      this.BackgroundColor = Color.Lerp(new Color(63, 82, 151), new Color(80, 80, 80), 0.5f) * 0.7f;
    }

    private void RenameMouseOver(UIMouseEvent evt, UIElement listeningElement) => this._buttonLabel.SetText(Language.GetTextValue("UI.Rename"));

    private void FavoriteMouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
      if (this._data.IsFavorite)
        this._buttonLabel.SetText(Language.GetTextValue("UI.Unfavorite"));
      else
        this._buttonLabel.SetText(Language.GetTextValue("UI.Favorite"));
    }

    private void CloudMouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
      if (this._data.IsCloudSave)
        this._buttonLabel.SetText(Language.GetTextValue("UI.MoveOffCloud"));
      else
        this._buttonLabel.SetText(Language.GetTextValue("UI.MoveToCloud"));
    }

    private void PlayMouseOver(UIMouseEvent evt, UIElement listeningElement) => this._buttonLabel.SetText(Language.GetTextValue("UI.Play"));

    private void SeedMouseOver(UIMouseEvent evt, UIElement listeningElement) => this._buttonLabel.SetText(Language.GetTextValue("UI.CopySeed", (object) this._data.GetFullSeedText(true)));

    private void DeleteMouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
      if (this._data.IsFavorite)
        this._deleteButtonLabel.SetText(Language.GetTextValue("UI.CannotDeleteFavorited"));
      else
        this._deleteButtonLabel.SetText(Language.GetTextValue("UI.Delete"));
    }

    private void DeleteMouseOut(UIMouseEvent evt, UIElement listeningElement) => this._deleteButtonLabel.SetText("");

    private void ButtonMouseOut(UIMouseEvent evt, UIElement listeningElement) => this._buttonLabel.SetText("");

    private void CloudButtonClick(UIMouseEvent evt, UIElement listeningElement)
    {
      if (this._data.IsCloudSave)
        this._data.MoveToLocal();
      else
        this._data.MoveToCloud();
      ((UIImageButton) evt.Target).SetImage(this._data.IsCloudSave ? this._buttonCloudActiveTexture : this._buttonCloudInactiveTexture);
      if (this._data.IsCloudSave)
        this._buttonLabel.SetText(Language.GetTextValue("UI.MoveOffCloud"));
      else
        this._buttonLabel.SetText(Language.GetTextValue("UI.MoveToCloud"));
    }

    private void DeleteButtonClick(UIMouseEvent evt, UIElement listeningElement)
    {
      for (int index = 0; index < Main.WorldList.Count; ++index)
      {
        if (Main.WorldList[index] == this._data)
        {
          SoundEngine.PlaySound(10);
          Main.selectedWorld = index;
          Main.menuMode = 9;
          break;
        }
      }
    }

    private void PlayGame(UIMouseEvent evt, UIElement listeningElement)
    {
      if (listeningElement != evt.Target || this.TryMovingToRejectionMenuIfNeeded(this._data.GameMode))
        return;
      this._data.SetAsActive();
      SoundEngine.PlaySound(10);
      Main.clrInput();
      Main.GetInputText("");
      Main.menuMode = !Main.menuMultiplayer || SocialAPI.Network == null ? (!Main.menuMultiplayer ? 10 : 30) : 889;
      if (Main.menuMultiplayer)
        return;
      WorldGen.playWorld();
    }

    private bool TryMovingToRejectionMenuIfNeeded(int worldGameMode)
    {
      GameModeData gameModeData;
      if (!Main.RegisteredGameModes.TryGetValue(worldGameMode, out gameModeData))
      {
        SoundEngine.PlaySound(10);
        Main.statusText = Language.GetTextValue("UI.WorldCannotBeLoadedBecauseItHasAnInvalidGameMode");
        Main.menuMode = 1000000;
        return true;
      }
      bool flag = Main.ActivePlayerFileData.Player.difficulty == (byte) 3;
      bool isJourneyMode = gameModeData.IsJourneyMode;
      if (flag && !isJourneyMode)
      {
        SoundEngine.PlaySound(10);
        Main.statusText = Language.GetTextValue("UI.PlayerIsCreativeAndWorldIsNotCreative");
        Main.menuMode = 1000000;
        return true;
      }
      if (!(!flag & isJourneyMode))
        return false;
      SoundEngine.PlaySound(10);
      Main.statusText = Language.GetTextValue("UI.PlayerIsNotCreativeAndWorldIsCreative");
      Main.menuMode = 1000000;
      return true;
    }

    private void RenameButtonClick(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(10);
      Main.clrInput();
      UIVirtualKeyboard state = new UIVirtualKeyboard(Lang.menu[48].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingName), new Action(this.GoBackHere), allowEmpty: true);
      state.SetMaxInputLength(27);
      Main.MenuUI.SetState((UIState) state);
      if (!(this.Parent.Parent is UIList parent))
        return;
      parent.UpdateOrder();
    }

    private void OnFinishedSettingName(string name)
    {
      string newDisplayName = name.Trim();
      Main.menuMode = 10;
      this._data.Rename(newDisplayName);
    }

    private void GoBackHere() => Main.GoToWorldSelect();

    private void FavoriteButtonClick(UIMouseEvent evt, UIElement listeningElement)
    {
      this._data.ToggleFavorite();
      ((UIImageButton) evt.Target).SetImage(this._data.IsFavorite ? this._buttonFavoriteActiveTexture : this._buttonFavoriteInactiveTexture);
      ((UIImageButton) evt.Target).SetVisibility(1f, this._data.IsFavorite ? 0.8f : 0.4f);
      if (this._data.IsFavorite)
      {
        this._buttonLabel.SetText(Language.GetTextValue("UI.Unfavorite"));
        this._deleteButton.OnLeftClick -= new UIElement.MouseEvent(this.DeleteButtonClick);
      }
      else
      {
        this._buttonLabel.SetText(Language.GetTextValue("UI.Favorite"));
        this._deleteButton.OnLeftClick += new UIElement.MouseEvent(this.DeleteButtonClick);
      }
      if (!(this.Parent.Parent is UIList parent))
        return;
      parent.UpdateOrder();
    }

    private void SeedButtonClick(UIMouseEvent evt, UIElement listeningElement)
    {
      Platform.Get<IClipboard>().Value = this._data.GetFullSeedText();
      this._buttonLabel.SetText(Language.GetTextValue("UI.SeedCopied"));
    }

    public override int CompareTo(object obj) => obj is UIWorldListItem uiWorldListItem ? this._orderInList.CompareTo(uiWorldListItem._orderInList) : base.CompareTo(obj);

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
      Utils.DrawBorderString(spriteBatch, worldName, new Vector2(x1 + 6f, dimensions.Y - 2f), color);
      spriteBatch.Draw(this._dividerTexture.Value, new Vector2(x1, innerDimensions.Y + 21f), new Rectangle?(), Color.White, 0.0f, Vector2.Zero, new Vector2((float) (((double) this.GetDimensions().X + (double) this.GetDimensions().Width - (double) x1) / 8.0), 1f), SpriteEffects.None, 0.0f);
      Vector2 position = new Vector2(x1 + 6f, innerDimensions.Y + 29f);
      float width1 = 100f;
      this.DrawPanel(spriteBatch, position, width1);
      string expertText;
      Color gameModeColor;
      this.GetDifficulty(out expertText, out gameModeColor);
      float x2 = FontAssets.MouseText.Value.MeasureString(expertText).X;
      float x3 = (float) ((double) width1 * 0.5 - (double) x2 * 0.5);
      Utils.DrawBorderString(spriteBatch, expertText, position + new Vector2(x3, 3f), gameModeColor);
      position.X += width1 + 5f;
      float width2 = 150f;
      if (!GameCulture.FromCultureName(GameCulture.CultureName.English).IsActive)
        width2 += 40f;
      this.DrawPanel(spriteBatch, position, width2);
      string textValue1 = Language.GetTextValue("UI.WorldSizeFormat", (object) this._data.WorldSizeName);
      float x4 = FontAssets.MouseText.Value.MeasureString(textValue1).X;
      float x5 = (float) ((double) width2 * 0.5 - (double) x4 * 0.5);
      Utils.DrawBorderString(spriteBatch, textValue1, position + new Vector2(x5, 3f), Color.White);
      position.X += width2 + 5f;
      float width3 = innerDimensions.X + innerDimensions.Width - position.X;
      this.DrawPanel(spriteBatch, position, width3);
      string textValue2 = Language.GetTextValue("UI.WorldCreatedFormat", !GameCulture.FromCultureName(GameCulture.CultureName.English).IsActive ? (object) this._data.CreationTime.ToShortDateString() : (object) this._data.CreationTime.ToString("d MMMM yyyy"));
      float x6 = FontAssets.MouseText.Value.MeasureString(textValue2).X;
      float x7 = (float) ((double) width3 * 0.5 - (double) x6 * 0.5);
      Utils.DrawBorderString(spriteBatch, textValue2, position + new Vector2(x7, 3f), Color.White);
      position.X += width3 + 5f;
    }
  }
}
