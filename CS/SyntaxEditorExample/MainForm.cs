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
        private IReadOnlyList<MonacoThemeRule>? currentRules;

        public MainForm() {
            InitializeComponent();
            ConfigureUI();
            syntaxEditor.Text = Constants.defaultCSharpText;
        }

        private void ConfigureUI() {
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

        #region Language Helpers

        private void SetSelectedLanguage(string language) {
            if(cbeLanguage.Properties.Items.Contains(language))
                cbeLanguage.EditValue = language;
        }

        private async System.Threading.Tasks.Task RefreshLanguages() {
            try {
                var languages = await syntaxEditor.GetAvailableLanguagesAsync();
                cbeLanguage.Properties.Items.Clear();
                if(languages != null) {
                    foreach(var lang in languages)
                        cbeLanguage.Properties.Items.Add(lang);
                }
            }
            catch { }
        }

        #endregion Language Helpers

        #region Ribbon Button Handlers

        private void openItem_ItemClick(object sender, ItemClickEventArgs e) {
            using var dlg = new OpenFileDialog();
            if(dlg.ShowDialog(this) == DialogResult.OK) {
                syntaxEditor.Text = File.ReadAllText(dlg.FileName);
            }
        }

        private void saveItem_ItemClick(object sender, ItemClickEventArgs e) {
            using var dlg = new SaveFileDialog();
            if(dlg.ShowDialog(this) == DialogResult.OK) {
                File.WriteAllText(dlg.FileName, syntaxEditor.Text);
                syntaxEditor.MarkAsSaved();
            }
        }

        private async void customLanguageItem_ItemClick(object sender, ItemClickEventArgs e) {
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

        private void rulesItem_ItemClick(object sender, ItemClickEventArgs e) {
            using var form = new RulesForm();
            if(currentRules != null) {
                form.SetRules(currentRules);
            }

            if(form.ShowDialog(this) == DialogResult.OK) {
                currentRules = form.GetRules();
                syntaxEditor.Rules = currentRules;
                syntaxEditor.ApplyCurrentTheme();
            }
        }

        private void applySkinColorsCheckItem_CheckedChanged(object sender, ItemClickEventArgs e) {
            syntaxEditor.ApplyDevExpressColors = applySkinColorsCheckItem.Checked;
        }

        #endregion Ribbon Button Handlers

        #region General Options Handlers

        private void ceReadOnly_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.ReadOnly = ceReadOnly.Checked;
        }

        private void cbeLanguage_EditValueChanged(object sender, EventArgs e) {
            var lang = cbeLanguage.EditValue?.ToString();
            if(!string.IsNullOrWhiteSpace(lang))
                syntaxEditor.EditorLanguage = lang;
        }

        #endregion General Options Handlers

        #region Interaction Options Handlers

        private void ceContextMenu_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableContextMenu = ceContextMenu.Checked;
        }

        private void ceDragAndDrop_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableDragAndDrop = ceDragAndDrop.Checked;
        }

        #endregion Interaction Options Handlers

        #region Appearance Options Handlers

        private void ceLineNumbers_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.ShowLineNumbers = ceLineNumbers.Checked;
        }

        private void seLineNumbersMinChars_EditValueChanged(object? sender, EventArgs e) {
            syntaxEditor.LineNumbersMinChars = Convert.ToInt32(seLineNumbersMinChars.EditValue);
        }

        private void ceMinimap_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.ShowMinimap = ceMinimap.Checked;
        }

        private void ceGlyphMargin_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.ShowGlyphMargin = ceGlyphMargin.Checked;
        }

        private void ceFolding_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableFolding = ceFolding.Checked;
        }

        private void ceStickyScroll_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableStickyScroll = ceStickyScroll.Checked;
        }

        private void cbeWordWrap_EditValueChanged(object? sender, EventArgs e) {
            if(cbeWordWrap.EditValue is EditorWordWrap ww)
                syntaxEditor.WordWrap = ww;
        }

        private void ceSmoothScrolling_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableSmoothScrolling = ceSmoothScrolling.Checked;
        }

        private void ceScrollBeyondLastLine_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableScrollBeyondLastLine = ceScrollBeyondLastLine.Checked;
        }

        private void seScrollBeyondLastColumn_EditValueChanged(object? sender, EventArgs e) {
            syntaxEditor.ScrollBeyondLastColumn = Convert.ToInt32(seScrollBeyondLastColumn.EditValue);
        }

        private void ceMouseWheelZoom_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableMouseWheelZoom = ceMouseWheelZoom.Checked;
        }

        #endregion Appearance Options Handlers

        #region Editing Options Handlers

        private void seTabSize_EditValueChanged(object sender, EventArgs e) {
            syntaxEditor.TabSize = Convert.ToInt32(seTabSize.EditValue);
        }

        private void ceInsertSpaces_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.InsertSpaces = ceInsertSpaces.Checked;
        }

        private void ceDetectIndentation_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.DetectIndentation = ceDetectIndentation.Checked;
        }

        private void cbeAutoIndent_EditValueChanged(object sender, EventArgs e) {
            if(cbeAutoIndent.EditValue is EditorAutoIndent ai)
                syntaxEditor.AutoIndent = ai;
        }

        #endregion Editing Options Handlers

        #region IntelliSense Options Handlers

        private void ceQuickSuggestions_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableQuickSuggestions = ceQuickSuggestions.Checked;
        }

        private void ceWordBasedSuggestions_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableWordBasedSuggestions = ceWordBasedSuggestions.Checked;
        }

        private void ceSuggestOnTriggerCharacters_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableSuggestOnTriggerCharacters = ceSuggestOnTriggerCharacters.Checked;
        }

        private void ceEnableParameterHints_CheckedChanged(object sender, EventArgs e) {
            syntaxEditor.EnableParameterHints = ceEnableParameterHints.Checked;
        }

        #endregion IntelliSense Options Handlers
    }
}