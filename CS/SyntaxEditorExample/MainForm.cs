using DevExpress.XtraBars;
using SyntaxEditorExample.Helpers;
using SyntaxEditor.Models;
using SyntaxEditor.Theming;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SyntaxEditorExample {
    public partial class MainForm : DevExpress.XtraEditors.XtraForm {
        List<MonacoThemeRule>? currentRules;

        public MainForm() {
            InitializeComponent();
            ConfigureUI();
            syntaxEditor.Text = Constants.defaultCSharpText.Replace("    ", "\t");
            currentRules = [.. syntaxEditor.Rules];
        }

        void ConfigureUI() {
            syntaxEditor.EditorInitialized += async (s, e) => {
                try {
                    await RefreshLanguages();
                    SetSelectedLanguage("csharp");
                }
                catch(Exception ex) {
                    MessageBox.Show($"An error occured: {ex.Message}");
                }
            };

            // Populate combo boxes

            cbeLanguage.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            cbeLanguage.Properties.AutoComplete = true;

            cbeWordWrap.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbeWordWrap.Properties.Items.AddRange(Enum.GetValues(typeof(EditorWordWrap)));
            cbeWordWrap.EditValue = syntaxEditor.WordWrap;

            cbeAutoIndent.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbeAutoIndent.Properties.Items.AddRange(Enum.GetValues(typeof(EditorAutoIndent)));
            cbeAutoIndent.EditValue = syntaxEditor.AutoIndent;

            // Configure SpinEdit 

            seLineNumbersMinChars.Properties.MinValue = 1;
            seLineNumbersMinChars.Properties.MaxValue = decimal.MaxValue;
            seLineNumbersMinChars.Properties.IsFloatValue = false;
            seLineNumbersMinChars.EditValue = syntaxEditor.LineNumbersMinChars;
            seLineNumbersMinChars.EditValueChanged += seLineNumbersMinChars_EditValueChanged;

            seScrollBeyondLastColumn.Properties.MinValue = 0;
            seScrollBeyondLastColumn.Properties.MaxValue = decimal.MaxValue;
            seScrollBeyondLastColumn.Properties.IsFloatValue = false;
            seScrollBeyondLastColumn.EditValue = syntaxEditor.ScrollBeyondLastColumn;
            seScrollBeyondLastColumn.EditValueChanged += seScrollBeyondLastColumn_EditValueChanged;

            seTabSize.Properties.MinValue = 1;
            seTabSize.Properties.MaxValue = 64;
            seTabSize.Properties.IsFloatValue = false;
            seTabSize.EditValue = syntaxEditor.TabSize;
            seTabSize.EditValueChanged += seTabSize_EditValueChanged;

            // Configure active skin combo
            cbeActiveSkin.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbeActiveSkin.Properties.Items.Add("Default");
            cbeActiveSkin.Properties.Items.AddRange(Enum.GetValues(typeof(MonacoThemeBase)));
            cbeActiveSkin.EditValue = "Default";
            cbeActiveSkin.Enabled = !syntaxEditor.ApplyDevExpressColors;
            cbeActiveSkin.EditValueChanged += cbeActiveSkin_EditValueChanged;

            // Sync check edits with initial editor values
            applySkinColorsCheckItem.Checked = syntaxEditor.ApplyDevExpressColors;
            ceReadOnly.Checked = syntaxEditor.ReadOnly;
            ceContextMenu.Checked = syntaxEditor.EnableContextMenu;
            ceDragAndDrop.Checked = syntaxEditor.EnableDragAndDrop;
            ceLineNumbers.Checked = syntaxEditor.ShowLineNumbers;
            ceMinimap.Checked = syntaxEditor.ShowMinimap;
            ceGlyphMargin.Checked = syntaxEditor.ShowGlyphMargin;
            ceFolding.Checked = syntaxEditor.EnableFolding;
            ceStickyScroll.Checked = syntaxEditor.EnableStickyScroll;
            ceSmoothScrolling.Checked = syntaxEditor.EnableSmoothScrolling;
            ceScrollBeyondLastLine.Checked = syntaxEditor.EnableScrollBeyondLastLine;
            ceMouseWheelZoom.Checked = syntaxEditor.EnableMouseWheelZoom;
            ceInsertSpaces.Checked = syntaxEditor.InsertSpaces;
            ceDetectIndentation.Checked = syntaxEditor.DetectIndentation;
            ceQuickSuggestions.Checked = syntaxEditor.EnableQuickSuggestions;
            ceWordBasedSuggestions.Checked = syntaxEditor.EnableWordBasedSuggestions;
            ceSuggestOnTriggerCharacters.Checked = syntaxEditor.EnableSuggestOnTriggerCharacters;
            ceEnableParameterHints.Checked = syntaxEditor.EnableParameterHints;
        }


        void SetSelectedLanguage(string language) {
            if(cbeLanguage.Properties.Items.Contains(language))
                cbeLanguage.EditValue = language;
        }

        async System.Threading.Tasks.Task RefreshLanguages() {
            try {
                IReadOnlyList<string> languages = await syntaxEditor.GetAvailableLanguagesAsync();
                cbeLanguage.Properties.Items.Clear();
                if(languages != null) {
                    foreach(string lang in languages)
                        cbeLanguage.Properties.Items.Add(lang);
                }
            }
            catch { }
        }



        void openItem_ItemClick(object sender, ItemClickEventArgs e) {
            using var dlg = new OpenFileDialog();
            if(dlg.ShowDialog(this) == DialogResult.OK) {
                syntaxEditor.Text = File.ReadAllText(dlg.FileName);
            }
        }

        void saveItem_ItemClick(object sender, ItemClickEventArgs e) {
            using var dlg = new SaveFileDialog();
            if(dlg.ShowDialog(this) == DialogResult.OK) {
                File.WriteAllText(dlg.FileName, syntaxEditor.Text);
                syntaxEditor.MarkAsSaved();
            }
        }

        async void customLanguageItem_ItemClick(object sender, ItemClickEventArgs e) {
            try {
                using var form = new CustomLanguageForm();
                form.LanguageId = "MyLang";
                form.Monarch = Constants.MyLangMonarch;
                form.Configuration = Constants.MyLangConfiguration;

                if(form.ShowDialog(this) == DialogResult.OK) {
                    var lang = new LanguageDescriptor {
                        Id = form.LanguageId,
                        Monarch = form.Monarch,
                        Configuration = form.Configuration
                    };

                    syntaxEditor.RegisterLanguage(lang);
                    await RefreshLanguages();
                    SetSelectedLanguage(form.LanguageId);
                    syntaxEditor.EditorLanguage = form.LanguageId;
                    syntaxEditor.Text = Constants.TestText;
                }
            }
            catch(Exception ex) {
                MessageBox.Show($"An error occured: {ex.Message}");
            }
        }

        void rulesItem_ItemClick(object sender, ItemClickEventArgs e) {
            using var form = new RulesForm();
            if(currentRules != null) {
                form.SetRules(currentRules);
            }

            if(form.ShowDialog(this) == DialogResult.OK) {
                currentRules = form.GetRules();
                syntaxEditor.Rules.Clear();
                syntaxEditor.Rules.AddRange(currentRules);
                syntaxEditor.ApplyCurrentTheme();
            }
        }

        void applySkinColorsCheckItem_CheckedChanged(object sender, ItemClickEventArgs e) {
            cbeActiveSkin.Enabled = !applySkinColorsCheckItem.Checked;
            object? theme = applySkinColorsCheckItem.Checked ? null : cbeActiveSkin.EditValue;
            SetSkinOverride(theme);
            syntaxEditor.ApplyDevExpressColors = applySkinColorsCheckItem.Checked;
        }

        void cbeActiveSkin_EditValueChanged(object? sender, EventArgs e) {
            SetSkinOverride(cbeActiveSkin.EditValue);
            syntaxEditor.ApplyCurrentTheme();
        }

        void SetSkinOverride(object? theme) {
            if(theme is MonacoThemeBase themeBase)
                LookAndFeelExtensions.SkinBaseOverride = themeBase;
            else
                LookAndFeelExtensions.SkinBaseOverride = null;
        }



        void ceReadOnly_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.ReadOnly = ceReadOnly.Checked;
        }

        void cbeLanguage_EditValueChanged(object sender, EventArgs e) {
            string? lang = cbeLanguage.EditValue?.ToString();
            if(!string.IsNullOrWhiteSpace(lang))
                syntaxEditor.EditorLanguage = lang;
        }



        void ceContextMenu_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableContextMenu = ceContextMenu.Checked;
        }

        void ceDragAndDrop_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableDragAndDrop = ceDragAndDrop.Checked;
        }



        void ceLineNumbers_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.ShowLineNumbers = ceLineNumbers.Checked;
        }

        void seLineNumbersMinChars_EditValueChanged(object? sender, EventArgs e) {
            syntaxEditor.LineNumbersMinChars = Convert.ToInt32(seLineNumbersMinChars.EditValue);
        }

        void ceMinimap_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.ShowMinimap = ceMinimap.Checked;
        }

        void ceGlyphMargin_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.ShowGlyphMargin = ceGlyphMargin.Checked;
        }

        void ceFolding_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableFolding = ceFolding.Checked;
        }

        void ceStickyScroll_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableStickyScroll = ceStickyScroll.Checked;
        }

        void cbeWordWrap_EditValueChanged(object? sender, EventArgs e) {
            if(cbeWordWrap.EditValue is EditorWordWrap ww)
                syntaxEditor.WordWrap = ww;
        }

        void ceSmoothScrolling_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableSmoothScrolling = ceSmoothScrolling.Checked;
        }

        void ceScrollBeyondLastLine_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableScrollBeyondLastLine = ceScrollBeyondLastLine.Checked;
        }

        void seScrollBeyondLastColumn_EditValueChanged(object? sender, EventArgs e) {
            syntaxEditor.ScrollBeyondLastColumn = Convert.ToInt32(seScrollBeyondLastColumn.EditValue);
        }

        void ceMouseWheelZoom_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableMouseWheelZoom = ceMouseWheelZoom.Checked;
        }



        void seTabSize_EditValueChanged(object sender, EventArgs e) {
            syntaxEditor.TabSize = Convert.ToInt32(seTabSize.EditValue);
        }

        void ceInsertSpaces_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.InsertSpaces = ceInsertSpaces.Checked;
        }

        void ceDetectIndentation_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.DetectIndentation = ceDetectIndentation.Checked;
        }

        void cbeAutoIndent_EditValueChanged(object sender, EventArgs e) {
            if(cbeAutoIndent.EditValue is EditorAutoIndent ai)
                syntaxEditor.AutoIndent = ai;
        }



        void ceQuickSuggestions_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableQuickSuggestions = ceQuickSuggestions.Checked;
        }

        void ceWordBasedSuggestions_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableWordBasedSuggestions = ceWordBasedSuggestions.Checked;
        }

        void ceSuggestOnTriggerCharacters_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableSuggestOnTriggerCharacters = ceSuggestOnTriggerCharacters.Checked;
        }

        void ceEnableParameterHints_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableParameterHints = ceEnableParameterHints.Checked;
        }

    }
}
