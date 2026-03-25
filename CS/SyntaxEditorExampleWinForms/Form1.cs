using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using SyntaxEditorWinForms;
using SyntaxEditorWinForms.Models;
using SyntaxEditorWinForms.Theming;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SyntaxEditorExampleWinForms {
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm {

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

        private SyntaxEditor syntaxEditor;
        private ThemeBehavior themeBehavior;
        private IReadOnlyList<MonacoThemeRule>? currentRules;
        private CheckEdit chkReadOnly;
        private ComboBoxEdit cmbLanguage;
        private SimpleButton btnRefreshLanguages;
        private CheckEdit chkContextMenu;
        private CheckEdit chkDragAndDrop;
        private CheckEdit chkLineNumbers;
        private SpinEdit spnLineNumbersMinChars;
        private CheckEdit chkMinimap;
        private CheckEdit chkGlyphMargin;
        private CheckEdit chkFolding;
        private CheckEdit chkStickyScroll;
        private ComboBoxEdit cmbWordWrap;
        private CheckEdit chkSmoothScrolling;
        private CheckEdit chkScrollBeyondLastLine;
        private SpinEdit spnScrollBeyondLastColumn;
        private CheckEdit chkMouseWheelZoom;
        private SpinEdit spnTabSize;
        private CheckEdit chkInsertSpaces;
        private CheckEdit chkDetectIndentation;
        private ComboBoxEdit cmbAutoIndent;
        private CheckEdit chkQuickSuggestions;
        private CheckEdit chkWordBasedSuggestions;
        private CheckEdit chkSuggestOnTrigger;
        private CheckEdit chkParameterHints;

        public Form1() {
            InitializeComponent();
            ConfigureUI();
            syntaxEditor.Text = defaultCSharpText;
        }

        private void ConfigureUI() {
            // Skin dropdown
            SkinHelper.InitSkinPopupMenu(skinBarSubItem);

            // SyntaxEditor
            syntaxEditor = new SyntaxEditor();
            syntaxEditor.Dock = DockStyle.Fill;
            syntaxEditor.IsModifiedChanged += (s, e) => {
                barStatusModified.Caption = $"Has Changes: {syntaxEditor.IsModified}";
            };

            // Theme Behavior
            themeBehavior = new ThemeBehavior();
            themeBehavior.Attach(syntaxEditor);

            syntaxEditor.EditorInitialized += async (s, e) => {
                await RefreshLanguages();
                SetSelectedLanguage("csharp");
            };

            // Options Panel
            var optionsPanel = BuildOptionsPanel();

            // Add to split container
            splitContainer.Panel1.Controls.Add(syntaxEditor);
            splitContainer.Panel2.Controls.Add(optionsPanel);

            // Clear default ribbon toolbar items
            ribbon.Toolbar.ItemLinks.Clear();
        }

        private ScrollableControl BuildOptionsPanel() {
            var scroll = new XtraScrollableControl();
            scroll.Dock = DockStyle.Fill;

            var layout = new LayoutControl();
            layout.Dock = DockStyle.Top;
            layout.AutoSize = true;
            layout.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layout.Root.GroupBordersVisible = false;

            // General
            var grpGeneral = layout.Root.AddGroup("General");
            grpGeneral.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Regular;

            chkReadOnly = new CheckEdit();
            chkReadOnly.CheckedChanged += (s, e) => syntaxEditor.ReadOnly = chkReadOnly.Checked;
            grpGeneral.AddItem("Read Only", chkReadOnly);

            cmbLanguage = new ComboBoxEdit();
            cmbLanguage.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            cmbLanguage.Properties.AutoComplete = true;
            cmbLanguage.EditValueChanged += (s, e) => {
                var lang = cmbLanguage.EditValue?.ToString();
                if (!string.IsNullOrWhiteSpace(lang))
                    syntaxEditor.EditorLanguage = lang;
            };
            grpGeneral.AddItem("Language", cmbLanguage);

            btnRefreshLanguages = new SimpleButton { Text = "Refresh Languages" };
            btnRefreshLanguages.Click += async (s, e) => await RefreshLanguages();
            grpGeneral.AddItem(string.Empty, btnRefreshLanguages).TextVisible = false;

            // Interaction
            var grpInteraction = layout.Root.AddGroup("Interaction");

            chkContextMenu = new CheckEdit { Checked = true };
            chkContextMenu.CheckedChanged += (s, e) => syntaxEditor.EnableContextMenu = chkContextMenu.Checked;
            grpInteraction.AddItem("Context Menu", chkContextMenu);

            chkDragAndDrop = new CheckEdit { Checked = true };
            chkDragAndDrop.CheckedChanged += (s, e) => syntaxEditor.EnableDragAndDrop = chkDragAndDrop.Checked;
            grpInteraction.AddItem("Drag and Drop", chkDragAndDrop);

            // Appearance
            var grpAppearance = layout.Root.AddGroup("Appearance");

            chkLineNumbers = new CheckEdit { Checked = true };
            chkLineNumbers.CheckedChanged += (s, e) => syntaxEditor.ShowLineNumbers = chkLineNumbers.Checked;
            grpAppearance.AddItem("Line Numbers", chkLineNumbers);

            spnLineNumbersMinChars = new SpinEdit();
            spnLineNumbersMinChars.Properties.MinValue = 1;
            spnLineNumbersMinChars.Properties.IsFloatValue = false;
            spnLineNumbersMinChars.EditValue = 5;
            spnLineNumbersMinChars.EditValueChanged += (s, e) => syntaxEditor.LineNumbersMinChars = Convert.ToInt32(spnLineNumbersMinChars.EditValue);
            grpAppearance.AddItem("Line Numbers Min Chars", spnLineNumbersMinChars);

            chkMinimap = new CheckEdit();
            chkMinimap.CheckedChanged += (s, e) => syntaxEditor.ShowMinimap = chkMinimap.Checked;
            grpAppearance.AddItem("Minimap", chkMinimap);

            chkGlyphMargin = new CheckEdit();
            chkGlyphMargin.CheckedChanged += (s, e) => syntaxEditor.ShowGlyphMargin = chkGlyphMargin.Checked;
            grpAppearance.AddItem("Glyph Margin", chkGlyphMargin);

            chkFolding = new CheckEdit { Checked = true };
            chkFolding.CheckedChanged += (s, e) => syntaxEditor.EnableFolding = chkFolding.Checked;
            grpAppearance.AddItem("Folding", chkFolding);

            chkStickyScroll = new CheckEdit { Checked = true };
            chkStickyScroll.CheckedChanged += (s, e) => syntaxEditor.EnableStickyScroll = chkStickyScroll.Checked;
            grpAppearance.AddItem("Sticky Scroll", chkStickyScroll);

            cmbWordWrap = new ComboBoxEdit();
            cmbWordWrap.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cmbWordWrap.Properties.Items.AddRange(Enum.GetValues(typeof(EditorWordWrap)));
            cmbWordWrap.EditValue = EditorWordWrap.Off;
            cmbWordWrap.EditValueChanged += (s, e) => {
                if (cmbWordWrap.EditValue is EditorWordWrap ww)
                    syntaxEditor.WordWrap = ww;
            };
            grpAppearance.AddItem("Word Wrap", cmbWordWrap);

            chkSmoothScrolling = new CheckEdit();
            chkSmoothScrolling.CheckedChanged += (s, e) => syntaxEditor.EnableSmoothScrolling = chkSmoothScrolling.Checked;
            grpAppearance.AddItem("Smooth Scrolling", chkSmoothScrolling);

            chkScrollBeyondLastLine = new CheckEdit { Checked = true };
            chkScrollBeyondLastLine.CheckedChanged += (s, e) => syntaxEditor.EnableScrollBeyondLastLine = chkScrollBeyondLastLine.Checked;
            grpAppearance.AddItem("Scroll Beyond Last Line", chkScrollBeyondLastLine);

            spnScrollBeyondLastColumn = new SpinEdit();
            spnScrollBeyondLastColumn.Properties.MinValue = 0;
            spnScrollBeyondLastColumn.Properties.IsFloatValue = false;
            spnScrollBeyondLastColumn.EditValue = 5;
            spnScrollBeyondLastColumn.EditValueChanged += (s, e) => syntaxEditor.ScrollBeyondLastColumn = Convert.ToInt32(spnScrollBeyondLastColumn.EditValue);
            grpAppearance.AddItem("Scroll Beyond Last Column", spnScrollBeyondLastColumn);

            chkMouseWheelZoom = new CheckEdit();
            chkMouseWheelZoom.CheckedChanged += (s, e) => syntaxEditor.EnableMouseWheelZoom = chkMouseWheelZoom.Checked;
            grpAppearance.AddItem("Mouse Wheel Zoom", chkMouseWheelZoom);

            // Editing
            var grpEditing = layout.Root.AddGroup("Editing");

            spnTabSize = new SpinEdit();
            spnTabSize.Properties.MinValue = 1;
            spnTabSize.Properties.MaxValue = 64;
            spnTabSize.Properties.IsFloatValue = false;
            spnTabSize.EditValue = 4;
            spnTabSize.EditValueChanged += (s, e) => syntaxEditor.TabSize = Convert.ToInt32(spnTabSize.EditValue);
            grpEditing.AddItem("Tab Size", spnTabSize);

            chkInsertSpaces = new CheckEdit { Checked = true };
            chkInsertSpaces.CheckedChanged += (s, e) => syntaxEditor.InsertSpaces = chkInsertSpaces.Checked;
            grpEditing.AddItem("Insert Spaces", chkInsertSpaces);

            chkDetectIndentation = new CheckEdit { Checked = true };
            chkDetectIndentation.CheckedChanged += (s, e) => syntaxEditor.DetectIndentation = chkDetectIndentation.Checked;
            grpEditing.AddItem("Detect Indentation", chkDetectIndentation);

            cmbAutoIndent = new ComboBoxEdit();
            cmbAutoIndent.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cmbAutoIndent.Properties.Items.AddRange(Enum.GetValues(typeof(EditorAutoIndent)));
            cmbAutoIndent.EditValue = EditorAutoIndent.Full;
            cmbAutoIndent.EditValueChanged += (s, e) => {
                if (cmbAutoIndent.EditValue is EditorAutoIndent ai)
                    syntaxEditor.AutoIndent = ai;
            };
            grpEditing.AddItem("Auto Indent", cmbAutoIndent);

            // IntelliSense
            var grpIntelliSense = layout.Root.AddGroup("IntelliSense");

            chkQuickSuggestions = new CheckEdit { Checked = true };
            chkQuickSuggestions.CheckedChanged += (s, e) => syntaxEditor.EnableQuickSuggestions = chkQuickSuggestions.Checked;
            grpIntelliSense.AddItem("Quick Suggestions", chkQuickSuggestions);

            chkWordBasedSuggestions = new CheckEdit { Checked = true };
            chkWordBasedSuggestions.CheckedChanged += (s, e) => syntaxEditor.EnableWordBasedSuggestions = chkWordBasedSuggestions.Checked;
            grpIntelliSense.AddItem("Word Based Suggestions", chkWordBasedSuggestions);

            chkSuggestOnTrigger = new CheckEdit { Checked = true };
            chkSuggestOnTrigger.CheckedChanged += (s, e) => syntaxEditor.EnableSuggestOnTriggerCharacters = chkSuggestOnTrigger.Checked;
            grpIntelliSense.AddItem("Suggest On Trigger Characters", chkSuggestOnTrigger);

            chkParameterHints = new CheckEdit { Checked = true };
            chkParameterHints.CheckedChanged += (s, e) => syntaxEditor.EnableParameterHints = chkParameterHints.Checked;
            grpIntelliSense.AddItem("Enable Parameter Hints", chkParameterHints);

            scroll.Controls.Add(layout);
            return scroll;
        }

        private void BarApplyThemeColors_CheckedChanged(object sender, ItemClickEventArgs e) {
            if (themeBehavior != null)
                themeBehavior.ApplyDevExpressColors = barApplyThemeColors.Checked;
        }


        private void SetSelectedLanguage(string language) {
            if (cmbLanguage.Properties.Items.Contains(language))
                cmbLanguage.EditValue = language;
        }

        private async System.Threading.Tasks.Task RefreshLanguages() {
            try {
                var languages = await syntaxEditor.GetAvailableLanguagesAsync();
                cmbLanguage.Properties.Items.Clear();
                if (languages != null) {
                    foreach (var lang in languages)
                        cmbLanguage.Properties.Items.Add(lang);
                }
            } catch { }
        }

        private void BarOpenFile_ItemClick(object? sender, ItemClickEventArgs e) {
            using var dlg = new OpenFileDialog();
            if (dlg.ShowDialog(this) == DialogResult.OK) {
                syntaxEditor.Text = File.ReadAllText(dlg.FileName);
            }
        }

        private void BarSaveFile_ItemClick(object? sender, ItemClickEventArgs e) {
            using var dlg = new SaveFileDialog();
            if (dlg.ShowDialog(this) == DialogResult.OK) {
                File.WriteAllText(dlg.FileName, syntaxEditor.Text);
                syntaxEditor.MarkAsSaved();
            }
        }

        private async void BarRegisterLanguage_ItemClick(object? sender, ItemClickEventArgs e) {
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

                syntaxEditor.RegisterLanguage(lang);
                await RefreshLanguages();
                SetSelectedLanguage(form.LanguageId);
                syntaxEditor.EditorLanguage = form.LanguageId;
                syntaxEditor.Text = testText;
            }
        }

        private void BarChangeRules_ItemClick(object? sender, ItemClickEventArgs e) {
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
    }
}
