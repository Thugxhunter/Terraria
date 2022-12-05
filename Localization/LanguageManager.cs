// Decompiled with JetBrains decompiler
// Type: Terraria.Localization.LanguageManager
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using CsvHelper;
using Newtonsoft.Json;
using ReLogic.Content;
using ReLogic.Content.Sources;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using Terraria.GameContent;
using Terraria.Initializers;
using Terraria.Utilities;

namespace Terraria.Localization
{
  public class LanguageManager
  {
    public static LanguageManager Instance = new LanguageManager();
    private readonly Dictionary<string, LocalizedText> _localizedTexts = new Dictionary<string, LocalizedText>();
    private readonly Dictionary<string, List<string>> _categoryGroupedKeys = new Dictionary<string, List<string>>();
    private GameCulture _fallbackCulture = GameCulture.DefaultCulture;

    public event LanguageChangeCallback OnLanguageChanging;

    public event LanguageChangeCallback OnLanguageChanged;

    public GameCulture ActiveCulture { get; private set; }

    private LanguageManager() => this._localizedTexts[""] = LocalizedText.Empty;

    public int GetCategorySize(string name) => this._categoryGroupedKeys.ContainsKey(name) ? this._categoryGroupedKeys[name].Count : 0;

    public void SetLanguage(int legacyId) => this.SetLanguage(GameCulture.FromLegacyId(legacyId));

    public void SetLanguage(string cultureName) => this.SetLanguage(GameCulture.FromName(cultureName));

    public int EstimateWordCount()
    {
      int num = 0;
      foreach (string key in this._localizedTexts.Keys)
      {
        string textValue = this.GetTextValue(key);
        textValue.Replace(",", "").Replace(".", "").Replace("\"", "").Trim();
        string[] strArray1 = textValue.Split(' ');
        string[] strArray2 = textValue.Split(' ');
        if (strArray1.Length == strArray2.Length)
        {
          foreach (string str in strArray1)
          {
            if (!string.IsNullOrWhiteSpace(str) && str.Length >= 1)
              ++num;
          }
        }
        else
          break;
      }
      return num;
    }

    private void SetAllTextValuesToKeys()
    {
      foreach (KeyValuePair<string, LocalizedText> localizedText in this._localizedTexts)
        localizedText.Value.SetValue(localizedText.Key);
    }

    private string[] GetLanguageFilesForCulture(GameCulture culture)
    {
      Assembly.GetExecutingAssembly();
      return Array.FindAll<string>(typeof (Program).Assembly.GetManifestResourceNames(), (Predicate<string>) (element => element.StartsWith("Terraria.Localization.Content." + culture.CultureInfo.Name) && element.EndsWith(".json")));
    }

    public void SetLanguage(GameCulture culture)
    {
      if (this.ActiveCulture == culture)
        return;
      if (culture != this._fallbackCulture && this.ActiveCulture != this._fallbackCulture)
      {
        this.SetAllTextValuesToKeys();
        this.LoadLanguage(this._fallbackCulture);
      }
      this.LoadLanguage(culture);
      this.ActiveCulture = culture;
      Thread.CurrentThread.CurrentCulture = culture.CultureInfo;
      Thread.CurrentThread.CurrentUICulture = culture.CultureInfo;
      if (this.OnLanguageChanged != null)
        this.OnLanguageChanged(this);
      Asset<DynamicSpriteFont> deathText = FontAssets.DeathText;
    }

    private void LoadLanguage(GameCulture culture, bool processCopyCommands = true)
    {
      this.LoadFilesForCulture(culture);
      if (this.OnLanguageChanging != null)
        this.OnLanguageChanging(this);
      if (processCopyCommands)
        this.ProcessCopyCommandsInTexts();
      ChatInitializer.PrepareAliases();
    }

    private void LoadFilesForCulture(GameCulture culture)
    {
      foreach (string path in this.GetLanguageFilesForCulture(culture))
      {
        try
        {
          string fileText = Utils.ReadEmbeddedResource(path);
          if (fileText == null || fileText.Length < 2)
            throw new FormatException();
          this.LoadLanguageFromFileTextJson(fileText, true);
        }
        catch (Exception ex)
        {
          if (Debugger.IsAttached)
            Debugger.Break();
          Console.WriteLine("Failed to load language file: " + path);
          break;
        }
      }
    }

    private void ProcessCopyCommandsInTexts()
    {
      Regex regex = new Regex("{\\$(\\w+\\.\\w+)}", RegexOptions.Compiled);
      foreach (KeyValuePair<string, LocalizedText> localizedText1 in this._localizedTexts)
      {
        LocalizedText localizedText2 = localizedText1.Value;
        for (int index = 0; index < 100; ++index)
        {
          string text = regex.Replace(localizedText2.Value, (MatchEvaluator) (match => this.GetTextValue(match.Groups[1].ToString())));
          if (!(text == localizedText2.Value))
            localizedText2.SetValue(text);
          else
            break;
        }
      }
    }

    public void UseSources(List<IContentSource> sourcesFromLowestToHighest)
    {
      string name = this.ActiveCulture.Name;
      string lower = ("Localization" + Path.DirectorySeparatorChar.ToString() + name).ToLower();
      this.LoadLanguage(this.ActiveCulture, false);
      foreach (IContentSource icontentSource in sourcesFromLowestToHighest)
      {
        foreach (string str in icontentSource.GetAllAssetsStartingWith(lower))
        {
          string extension = icontentSource.GetExtension(str);
          if ((extension == ".json" ? 1 : (extension == ".csv" ? 1 : 0)) != 0)
          {
            using (Stream stream = icontentSource.OpenStream(str))
            {
              using (StreamReader streamReader = new StreamReader(stream))
              {
                string end = streamReader.ReadToEnd();
                if (extension == ".json")
                  this.LoadLanguageFromFileTextJson(end, false);
                if (extension == ".csv")
                  this.LoadLanguageFromFileTextCsv(end);
              }
            }
          }
        }
      }
      this.ProcessCopyCommandsInTexts();
      ChatInitializer.PrepareAliases();
    }

    public void LoadLanguageFromFileTextCsv(string fileText)
    {
      using (TextReader textReader = (TextReader) new StringReader(fileText))
      {
        using (CsvReader csvReader = new CsvReader(textReader))
        {
          csvReader.Configuration.HasHeaderRecord = true;
          if (!csvReader.ReadHeader())
            return;
          string[] fieldHeaders = csvReader.FieldHeaders;
          int val1 = -1;
          int val2 = -1;
          for (int index = 0; index < fieldHeaders.Length; ++index)
          {
            string lower = fieldHeaders[index].ToLower();
            if (lower == "translation")
              val2 = index;
            if (lower == "key")
              val1 = index;
          }
          if (val1 == -1 || val2 == -1)
            return;
          int num = Math.Max(val1, val2) + 1;
          while (csvReader.Read())
          {
            string[] currentRecord = csvReader.CurrentRecord;
            if (currentRecord.Length >= num)
            {
              string key = currentRecord[val1];
              string text = currentRecord[val2];
              if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(text) && this._localizedTexts.ContainsKey(key))
                this._localizedTexts[key].SetValue(text);
            }
          }
        }
      }
    }

    public void LoadLanguageFromFileTextJson(string fileText, bool canCreateCategories)
    {
      foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair1 in JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(fileText))
      {
        string key1 = keyValuePair1.Key;
        foreach (KeyValuePair<string, string> keyValuePair2 in keyValuePair1.Value)
        {
          string key2 = keyValuePair1.Key + "." + keyValuePair2.Key;
          if (this._localizedTexts.ContainsKey(key2))
            this._localizedTexts[key2].SetValue(keyValuePair2.Value);
          else if (canCreateCategories)
          {
            this._localizedTexts.Add(key2, new LocalizedText(key2, keyValuePair2.Value));
            if (!this._categoryGroupedKeys.ContainsKey(keyValuePair1.Key))
              this._categoryGroupedKeys.Add(keyValuePair1.Key, new List<string>());
            this._categoryGroupedKeys[keyValuePair1.Key].Add(keyValuePair2.Key);
          }
        }
      }
    }

    [Conditional("DEBUG")]
    private void ValidateAllCharactersContainedInFont(DynamicSpriteFont font)
    {
      if (font == null)
        return;
      string str = "";
      foreach (LocalizedText localizedText in this._localizedTexts.Values)
      {
        foreach (char ch in localizedText.Value)
        {
          if (!font.IsCharacterSupported(ch))
            str = str + localizedText.Key + ", " + ch.ToString() + ", " + (object) (int) ch + "\n";
        }
      }
    }

    public LocalizedText[] FindAll(Regex regex)
    {
      int length = 0;
      foreach (KeyValuePair<string, LocalizedText> localizedText in this._localizedTexts)
      {
        if (regex.IsMatch(localizedText.Key))
          ++length;
      }
      LocalizedText[] all = new LocalizedText[length];
      int index = 0;
      foreach (KeyValuePair<string, LocalizedText> localizedText in this._localizedTexts)
      {
        if (regex.IsMatch(localizedText.Key))
        {
          all[index] = localizedText.Value;
          ++index;
        }
      }
      return all;
    }

    public LocalizedText[] FindAll(LanguageSearchFilter filter)
    {
      LinkedList<LocalizedText> source = new LinkedList<LocalizedText>();
      foreach (KeyValuePair<string, LocalizedText> localizedText in this._localizedTexts)
      {
        if (filter(localizedText.Key, localizedText.Value))
          source.AddLast(localizedText.Value);
      }
      return source.ToArray<LocalizedText>();
    }

    public LocalizedText SelectRandom(
      LanguageSearchFilter filter,
      UnifiedRandom random = null)
    {
      int maxValue = 0;
      foreach (KeyValuePair<string, LocalizedText> localizedText in this._localizedTexts)
      {
        if (filter(localizedText.Key, localizedText.Value))
          ++maxValue;
      }
      int num = (random ?? Main.rand).Next(maxValue);
      foreach (KeyValuePair<string, LocalizedText> localizedText in this._localizedTexts)
      {
        if (filter(localizedText.Key, localizedText.Value) && --maxValue == num)
          return localizedText.Value;
      }
      return LocalizedText.Empty;
    }

    public LocalizedText RandomFromCategory(string categoryName, UnifiedRandom random = null)
    {
      if (!this._categoryGroupedKeys.ContainsKey(categoryName))
        return new LocalizedText(categoryName + ".RANDOM", categoryName + ".RANDOM");
      List<string> categoryGroupedKey = this._categoryGroupedKeys[categoryName];
      return this.GetText(categoryName + "." + categoryGroupedKey[(random ?? Main.rand).Next(categoryGroupedKey.Count)]);
    }

    public LocalizedText IndexedFromCategory(string categoryName, int index)
    {
      if (!this._categoryGroupedKeys.ContainsKey(categoryName))
        return new LocalizedText(categoryName + ".INDEXED", categoryName + ".INDEXED");
      List<string> categoryGroupedKey = this._categoryGroupedKeys[categoryName];
      int index1 = index % categoryGroupedKey.Count;
      return this.GetText(categoryName + "." + categoryGroupedKey[index1]);
    }

    public bool Exists(string key) => this._localizedTexts.ContainsKey(key);

    public LocalizedText GetText(string key) => !this._localizedTexts.ContainsKey(key) ? new LocalizedText(key, key) : this._localizedTexts[key];

    public string GetTextValue(string key) => this._localizedTexts.ContainsKey(key) ? this._localizedTexts[key].Value : key;

    public string GetTextValue(string key, object arg0) => this._localizedTexts.ContainsKey(key) ? this._localizedTexts[key].Format(arg0) : key;

    public string GetTextValue(string key, object arg0, object arg1) => this._localizedTexts.ContainsKey(key) ? this._localizedTexts[key].Format(arg0, arg1) : key;

    public string GetTextValue(string key, object arg0, object arg1, object arg2) => this._localizedTexts.ContainsKey(key) ? this._localizedTexts[key].Format(arg0, arg1, arg2) : key;

    public string GetTextValue(string key, params object[] args) => this._localizedTexts.ContainsKey(key) ? this._localizedTexts[key].Format(args) : key;

    public void SetFallbackCulture(GameCulture culture) => this._fallbackCulture = culture;
  }
}
