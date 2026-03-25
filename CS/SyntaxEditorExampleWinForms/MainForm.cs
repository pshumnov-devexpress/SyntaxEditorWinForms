using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using SyntaxEditorWinForms;
using SyntaxEditorWinForms.Models;
using SyntaxEditorWinForms.Theming;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SyntaxEditorExampleWinForms {
    public partial class MainForm : DevExpress.XtraEditors.XtraForm {

        #region Constants

        string MyLangMonarch = @"{
  defaultToken: ""invalid"",

  keywords: [
    ""function"", ""let"", ""const"", ""if"", ""else"",
    ""return"", ""while"", ""for"", ""true"", ""false"", ""null""
  ],

  typeKeywords: [""number"", ""string"", ""boolean""],

  operators: [
    ""="", "">"", ""<"", ""!"", ""~"", ""?"", "":"", ""=="", ""<="",
    "">="", ""!="", ""&&"", ""||"", ""+"", ""-"", ""*"", ""/"", ""%""
  ],

  symbols: /[=><!~?:&|+\-*\/%]+/,

  escapes: /\\(?:[abfnrtv\\""'`]|x[0-9A-Fa-f]{2}|u[0-9A-Fa-f]{4})/,

  tokenizer: {

    root: [

      // identifiers
      [/[a-zA-Z_$][\w$]*/, {
        cases: {
          ""@keywords"": ""keyword"",
          ""@typeKeywords"": ""type"",
          ""@default"": ""identifier""
        }
      }],

      // whitespace
      { include: ""@whitespace"" },

      // delimiters
      [/[{}()\[\]]/, ""@brackets""],

      // operators
      [/@symbols/, {
        cases: {
          ""@operators"": ""operator"",
          ""@default"": """"
        }
      }],

      // numbers
      [/\d*\.\d+([eE][\-+]?\d+)?/, ""number.float""],
      [/\d+/, ""number""],

      // regex literal (complex rule object)
      {
        regex: /\/(?!\*)(?:[^\\/]|\\.)+\/[gimsuy]*/,
        action: { token: ""regexp"" }
      },

      // strings
      [/""/, { token: ""string.quote"", bracket: ""@open"", next: ""@string"" }],

      // template string
      [/`/, { token: ""string.quote"", bracket: ""@open"", next: ""@template"" }]
    ],

    comment: [
      [/[^/*]+/, ""comment""],
      [/\/\*/, ""comment"", ""@push""],
      [/\*\//, ""comment"", ""@pop""],    
      [/./, ""comment""]
    ],

    string: [
      [/[^\\""]+/, ""string""],
      [/@escapes/, ""string.escape""],
      [/\\./, ""string.escape.invalid""],
      [/""/, { token: ""string.quote"", bracket: ""@close"", next: ""@pop"" }]
    ],

    template: [
      [/[^\\`$]+/, ""string""],
      [/\$\{/, { token: ""delimiter.bracket"", next: ""@braced"" }],
      [/@escapes/, ""string.escape""],
      [/`/, { token: ""string.quote"", bracket: ""@close"", next: ""@pop"" }]
    ],

    braced: [
        [/}/, { token: ""delimiter.bracket"", next: ""@pop"" }],
        { include: ""@root"" }
    ],

    whitespace: [
      [/[ \t\r\n]+/, ""white""],
      [/\/\*/, ""comment"", ""@comment""],
      [/\/\/.*$/, ""comment""]
    ]
  }
}";

        const string MyLangConfiguration = @"{
  comments: {
    lineComment: ""//"",
    blockComment: [""/*"", ""*/""]
  },

  brackets: [
    [""{"", ""}""],
    [""["", ""]""],
    [""("", "")""]
  ],

  autoClosingPairs: [
    { open: ""{"", close: ""}"" },
    { open: ""["", close: ""]"" },
    { open: ""("", close: "")"" },
    { open: ""\"""", close: ""\"""" },
    { open: ""`"", close: ""`"" }
  ],

  surroundingPairs: [
    { open: ""{"", close: ""}"" },
    { open: ""["", close: ""]"" },
    { open: ""("", close: "")"" },
    { open: ""\"""", close: ""\"""" },
    { open: ""`"", close: ""`"" }
  ]
}";

        private const string testText = @"function test(x: number) {

    /* outer comment
        /* nested comment */
    */

    let value = 10.5
    const flag = true

    let regex = /abc\d+/gi

    let str = ""hello world""

    let template = `value is ${value}`

    if (flag && value > 5) {
        return template
    }

    return null
}";

        private const string defaultCSharpText = @"/*
* C# Program to Display All the Prime Numbers Between 1 to 100
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VS
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isPrime = true;
            Console.WriteLine(""Prime Numbers : "");
            for (int i = 2; i <= 100; i++)
            {
                for (int j = 2; j <= 100; j++)
                {
                    if (i != j && i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    Console.Write(""\t"" +i);
                }
                isPrime = true;
            }
            Console.ReadKey();
        }
    }
}
";

        #endregion Constants

        private ThemeBehavior themeBehavior;
        private IReadOnlyList<MonacoThemeRule>? currentRules;

        public MainForm() {
            InitializeComponent();
            ConfigureUI();
            syntaxEditor1.Text = defaultCSharpText;
        }

        private void ConfigureUI() {
            themeBehavior = new ThemeBehavior();
            themeBehavior.Attach(syntaxEditor1);

            syntaxEditor1.EditorInitialized += async (s, e) => {
                await RefreshLanguages();
                SetSelectedLanguage("csharp");
            };

            // Populate combo boxes
            cbeLanguage.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            cbeLanguage.Properties.AutoComplete = true;

            cbeWordWrap.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbeWordWrap.Properties.Items.AddRange(Enum.GetValues(typeof(EditorWordWrap)));
            cbeWordWrap.EditValue = syntaxEditor1.WordWrap;

            cbeAutoIndent.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbeAutoIndent.Properties.Items.AddRange(Enum.GetValues(typeof(EditorAutoIndent)));
            cbeAutoIndent.EditValue = syntaxEditor1.AutoIndent;

            // Set SpinEdit constraints
            seLineNumbersMinChars.Properties.MinValue = 1;
            seLineNumbersMinChars.Properties.IsFloatValue = false;
            seLineNumbersMinChars.EditValue = syntaxEditor1.LineNumbersMinChars;

            seScrollBeyondLastColumn.Properties.MinValue = 0;
            seScrollBeyondLastColumn.Properties.IsFloatValue = false;
            seScrollBeyondLastColumn.EditValue = syntaxEditor1.ScrollBeyondLastColumn;

            seTabSize.Properties.MinValue = 1;
            seTabSize.Properties.MaxValue = 64;
            seTabSize.Properties.IsFloatValue = false;
            seTabSize.EditValue = syntaxEditor1.TabSize;

            // Sync check edits with initial editor values
            ceReadOnly.Checked = syntaxEditor1.ReadOnly;
            ceContextMenu.Checked = syntaxEditor1.EnableContextMenu;
            ceDragAndDrop.Checked = syntaxEditor1.EnableDragAndDrop;
            ceLineNumbers.Checked = syntaxEditor1.ShowLineNumbers;
            ceMinimap.Checked = syntaxEditor1.ShowMinimap;
            ceGlyphMargin.Checked = syntaxEditor1.ShowGlyphMargin;
            ceFolding.Checked = syntaxEditor1.EnableFolding;
            ceStickyScroll.Checked = syntaxEditor1.EnableStickyScroll;
            ceSmoothScrolling.Checked = syntaxEditor1.EnableSmoothScrolling;
            ceScrollBeyondLastLine.Checked = syntaxEditor1.EnableScrollBeyondLastLine;
            ceMouseWheelZoom.Checked = syntaxEditor1.EnableMouseWheelZoom;
            ceInsertSpaces.Checked = syntaxEditor1.InsertSpaces;
            ceDetectIndentation.Checked = syntaxEditor1.DetectIndentation;
            ceQuickSuggestions.Checked = syntaxEditor1.EnableQuickSuggestions;
            ceWordBasedSuggestions.Checked = syntaxEditor1.EnableWordBasedSuggestions;
            ceSuggestOnTriggerCharacters.Checked = syntaxEditor1.EnableSuggestOnTriggerCharacters;
            ceEnableParameterHints.Checked = syntaxEditor1.EnableParameterHints;
        }

        #region Language Helpers

        private void SetSelectedLanguage(string language) {
            if (cbeLanguage.Properties.Items.Contains(language))
                cbeLanguage.EditValue = language;
        }

        private async System.Threading.Tasks.Task RefreshLanguages() {
            try {
                var languages = await syntaxEditor1.GetAvailableLanguagesAsync();
                cbeLanguage.Properties.Items.Clear();
                if (languages != null) {
                    foreach (var lang in languages)
                        cbeLanguage.Properties.Items.Add(lang);
                }
            } catch { }
        }

        #endregion Language Helpers

        #region Ribbon Button Handlers

        private void openItem_ItemClick(object sender, ItemClickEventArgs e) {
            using var dlg = new OpenFileDialog();
            if (dlg.ShowDialog(this) == DialogResult.OK) {
                syntaxEditor1.Text = File.ReadAllText(dlg.FileName);
            }
        }

        private void saveItem_ItemClick(object sender, ItemClickEventArgs e) {
            using var dlg = new SaveFileDialog();
            if (dlg.ShowDialog(this) == DialogResult.OK) {
                File.WriteAllText(dlg.FileName, syntaxEditor1.Text);
                syntaxEditor1.MarkAsSaved();
            }
        }

        private async void customLanguageItem_ItemClick(object sender, ItemClickEventArgs e) {
            using var form = new CustomLanguageForm();
            form.LanguageId = "MyLang";
            form.Monarch = MyLangMonarch;
            form.Configuration = MyLangConfiguration;

            if (form.ShowDialog(this) == DialogResult.OK) {
                var lang = new LanguageDescriptor {
                    Id = form.LanguageId,
                    Monarch = form.Monarch,
                    Configuration = form.Configuration
                };

                syntaxEditor1.RegisterLanguage(lang);
                await RefreshLanguages();
                SetSelectedLanguage(form.LanguageId);
                syntaxEditor1.EditorLanguage = form.LanguageId;
                syntaxEditor1.Text = testText;
            }
        }

        private void rulesItem_ItemClick(object sender, ItemClickEventArgs e) {
            using var form = new RulesForm();
            if (currentRules != null) {
                form.SetRules(currentRules);
            }

            if (form.ShowDialog(this) == DialogResult.OK) {
                currentRules = form.GetRules();
                themeBehavior.Rules = currentRules;
                themeBehavior.ApplyCurrentTheme();
            }
        }

        #endregion Ribbon Button Handlers

        #region General Options Handlers

        private void ceReadOnly_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor1.ReadOnly = ceReadOnly.Checked;
        }

        private void cbeLanguage_EditValueChanged(object sender, EventArgs e) {
            var lang = cbeLanguage.EditValue?.ToString();
            if (!string.IsNullOrWhiteSpace(lang))
                syntaxEditor1.EditorLanguage = lang;
        }

        #endregion General Options Handlers

        #region Interaction Options Handlers

        private void ceContextMenu_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor1.EnableContextMenu = ceContextMenu.Checked;
        }

        private void ceDragAndDrop_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor1.EnableDragAndDrop = ceDragAndDrop.Checked;
        }

        #endregion Interaction Options Handlers

        #region Appearance Options Handlers

        private void ceLineNumbers_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor1.ShowLineNumbers = ceLineNumbers.Checked;
        }

        private void seLineNumbersMinChars_EditValueChanged(object sender, EventArgs e) {
            syntaxEditor1.LineNumbersMinChars = Convert.ToInt32(seLineNumbersMinChars.EditValue);
        }

        private void ceMinimap_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor1.ShowMinimap = ceMinimap.Checked;
        }

        private void ceGlyphMargin_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor1.ShowGlyphMargin = ceGlyphMargin.Checked;
        }

        private void ceFolding_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor1.EnableFolding = ceFolding.Checked;
        }

        private void ceStickyScroll_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor1.EnableStickyScroll = ceStickyScroll.Checked;
        }

        private void cbeWordWrap_EditValueChanged(object sender, EventArgs e) {
            if (cbeWordWrap.EditValue is EditorWordWrap ww)
                syntaxEditor1.WordWrap = ww;
        }

        private void ceSmoothScrolling_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor1.EnableSmoothScrolling = ceSmoothScrolling.Checked;
        }

        private void ceScrollBeyondLastLine_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor1.EnableScrollBeyondLastLine = ceScrollBeyondLastLine.Checked;
        }

        private void seScrollBeyondLastColumn_EditValueChanged(object sender, EventArgs e) {
            syntaxEditor1.ScrollBeyondLastColumn = Convert.ToInt32(seScrollBeyondLastColumn.EditValue);
        }

        private void ceMouseWheelZoom_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor1.EnableMouseWheelZoom = ceMouseWheelZoom.Checked;
        }

        #endregion Appearance Options Handlers

        #region Editing Options Handlers

        private void seTabSize_EditValueChanged(object sender, EventArgs e) {
            syntaxEditor1.TabSize = Convert.ToInt32(seTabSize.EditValue);
        }

        private void ceInsertSpaces_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor1.InsertSpaces = ceInsertSpaces.Checked;
        }

        private void ceDetectIndentation_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor1.DetectIndentation = ceDetectIndentation.Checked;
        }

        private void cbeAutoIndent_EditValueChanged(object sender, EventArgs e) {
            if (cbeAutoIndent.EditValue is EditorAutoIndent ai)
                syntaxEditor1.AutoIndent = ai;
        }

        #endregion Editing Options Handlers

        #region IntelliSense Options Handlers

        private void ceQuickSuggestions_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor1.EnableQuickSuggestions = ceQuickSuggestions.Checked;
        }

        private void ceWordBasedSuggestions_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor1.EnableWordBasedSuggestions = ceWordBasedSuggestions.Checked;
        }

        private void ceSuggestOnTriggerCharacters_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor1.EnableSuggestOnTriggerCharacters = ceSuggestOnTriggerCharacters.Checked;
        }

        private void ceEnableParameterHints_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor1.EnableParameterHints = ceEnableParameterHints.Checked;
        }

        #endregion IntelliSense Options Handlers
    }
}