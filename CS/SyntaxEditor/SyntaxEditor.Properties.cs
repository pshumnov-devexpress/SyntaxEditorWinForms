using DevExpress.XtraEditors;
using SyntaxEditor.Models;
using SyntaxEditor.Theming;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SyntaxEditor {
    public partial class SyntaxEditor {
        // Events
        public event EventHandler EditorInitialized {
            add { Events.AddHandler(nameof(EditorInitialized), value); }
            remove { Events.RemoveHandler(nameof(EditorInitialized), value); }
        }
        public event EventHandler IsModifiedChanged {
            add { Events.AddHandler(nameof(IsModifiedChanged), value); }
            remove { Events.RemoveHandler(nameof(IsModifiedChanged), value); }
        }

        // Properties
        [DefaultValue("")]
        [DXCategory(CategoryName.Appearance)]
        public override string Text {
            get => text;
            set {
                if(text == value)
                    return;
                text = value ?? string.Empty;
                if(!updatingFromEditor)
                    commandChannel.Send(EditorCommandType.SetText, text);
                EventHandler? handler = Events[nameof(TextChanged)] as EventHandler;
                handler?.Invoke(this, EventArgs.Empty);
            }
        }

        [DefaultValue(false)]
        [DXCategory(CategoryName.Behavior)]
        public bool ReadOnly {
            get => readOnly;
            set {
                if(readOnly == value)
                    return;
                readOnly = value;
                commandChannel.Send(EditorCommandType.SetReadOnly, value);
            }
        }

        [DefaultValue(false)]
        [DXCategory(CategoryName.Data)]
        public bool IsModified {
            get => isModified;
            private set {
                if(isModified == value)
                    return;
                isModified = value;
                EventHandler? handler = Events[nameof(IsModifiedChanged)] as EventHandler;
                handler?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<MonacoThemeRule> Rules { get; }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Appearance)]
        public bool ApplyDevExpressColors {
            get => applyDevExpressColors;
            set {
                if(applyDevExpressColors == value) return;
                applyDevExpressColors = value;
                ApplyCurrentTheme();
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Appearance)]
        public bool ShowLineNumbers {
            get => showLineNumbers;
            set {
                if(showLineNumbers == value) return;
                showLineNumbers = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.LineNumbers, value);
            }
        }

        [DefaultValue(false)]
        [DXCategory(CategoryName.Appearance)]
        public bool ShowMinimap {
            get => showMinimap;
            set {
                if(showMinimap == value) return;
                showMinimap = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.Minimap, value);
            }
        }

        [DefaultValue(false)]
        [DXCategory(CategoryName.Appearance)]
        public bool ShowGlyphMargin {
            get => showGlyphMargin;
            set {
                if(showGlyphMargin == value) return;
                showGlyphMargin = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.GlyphMargin, value);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Appearance)]
        public bool EnableFolding {
            get => enableFolding;
            set {
                if(enableFolding == value) return;
                enableFolding = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.Folding, value);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Behavior)]
        public bool EnableContextMenu {
            get => enableContextMenu;
            set {
                if(enableContextMenu == value) return;
                enableContextMenu = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.ContextMenu, value);
            }
        }

        [DefaultValue(false)]
        [DXCategory(CategoryName.Behavior)]
        public bool EnableSmoothScrolling {
            get => enableSmoothScrolling;
            set {
                if(enableSmoothScrolling == value) return;
                enableSmoothScrolling = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.SmoothScrolling, value);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Behavior)]
        public bool EnableScrollBeyondLastLine {
            get => enableScrollBeyondLastLine;
            set {
                if(enableScrollBeyondLastLine == value) return;
                enableScrollBeyondLastLine = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.ScrollBeyondLastLine, value);
            }
        }

        [DefaultValue(5)]
        [DXCategory(CategoryName.Behavior)]
        public int ScrollBeyondLastColumn {
            get => scrollBeyondLastColumn;
            set {
                if(scrollBeyondLastColumn == value) return;
                scrollBeyondLastColumn = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.ScrollBeyondLastColumn, value);
            }
        }

        [DefaultValue(5)]
        [DXCategory(CategoryName.Appearance)]
        public int LineNumbersMinChars {
            get => lineNumbersMinChars;
            set {
                if(lineNumbersMinChars == value) return;
                lineNumbersMinChars = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.LineNumbersMinChars, value);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Behavior)]
        public bool EnableDragAndDrop {
            get => enableDragAndDrop;
            set {
                if(enableDragAndDrop == value) return;
                enableDragAndDrop = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.DragAndDrop, value);
            }
        }

        [DefaultValue(false)]
        [DXCategory(CategoryName.Behavior)]
        public bool EnableMouseWheelZoom {
            get => enableMouseWheelZoom;
            set {
                if(enableMouseWheelZoom == value) return;
                enableMouseWheelZoom = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.MouseWheelZoom, value);
            }
        }

        [DefaultValue(EditorWordWrap.Off)]
        [DXCategory(CategoryName.Appearance)]
        public EditorWordWrap WordWrap {
            get => wordWrap;
            set {
                if(wordWrap == value) return;
                this.wordWrap = value;
                SetWordWrap(value);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Appearance)]
        public bool EnableStickyScroll {
            get => enableStickyScroll;
            set {
                if(enableStickyScroll == value) return;
                enableStickyScroll = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.StickyScroll, new { enabled = value });
            }
        }

        [DefaultValue(4)]
        [DXCategory(CategoryName.Behavior)]
        public int TabSize {
            get => tabSize;
            set {
                if(value < 1 || value > 64)
                    throw new ArgumentOutOfRangeException(nameof(value));
                if(tabSize == value) return;
                tabSize = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.TabSize, value);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Behavior)]
        public bool DetectIndentation {
            get => detectIndentation;
            set {
                if(detectIndentation == value) return;
                detectIndentation = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.DetectIndentation, value);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Behavior)]
        public bool InsertSpaces {
            get => insertSpaces;
            set {
                if(insertSpaces == value) return;
                insertSpaces = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.InsertSpaces, value);
            }
        }

        [DefaultValue(EditorAutoIndent.Full)]
        [DXCategory(CategoryName.Behavior)]
        public EditorAutoIndent AutoIndent {
            get => autoIndent;
            set {
                if(autoIndent == value) return;
                autoIndent = value;
                SetAutoIndent(value);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Behavior)]
        public bool EnableQuickSuggestions {
            get => enableQuickSuggestions;
            set {
                if(enableQuickSuggestions == value) return;
                enableQuickSuggestions = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.EnableQuickSuggestions, value);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Behavior)]
        public bool EnableWordBasedSuggestions {
            get => enableWordBasedSuggestions;
            set {
                if(enableWordBasedSuggestions == value) return;
                enableWordBasedSuggestions = value;
                string suggestionMode = value ? "currentDocument" : "off";
                EditorOptionHelper.SendOption(commandChannel, EditorOption.EnableWordBasedSuggestions, suggestionMode);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Behavior)]
        public bool EnableSuggestOnTriggerCharacters {
            get => enableSuggestOnTriggerCharacters;
            set {
                if(enableSuggestOnTriggerCharacters == value) return;
                enableSuggestOnTriggerCharacters = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.EnableSuggestOnTriggerCharacters, value);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Behavior)]
        public bool EnableParameterHints {
            get => enableParameterHints;
            set {
                if(enableParameterHints == value) return;
                enableParameterHints = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.EnableParameterHints, new { enabled = value });
            }
        }

        [DefaultValue("vs")]
        [DXCategory(CategoryName.Appearance)]
        public string ThemeName {
            get => themeName;
            set {
                if(themeName == value) return;
                themeName = value;
                SetTheme(value);
            }
        }

        [DefaultValue("csharp")]
        [DXCategory(CategoryName.Behavior)]
        public string EditorLanguage {
            get => editorLanguage;
            set {
                if(editorLanguage == value) return;
                editorLanguage = value;
                SetEditorLanguage(value);
            }
        }

        // Property helpers
        void SetWordWrap(EditorWordWrap wrap) {
            string monacoValue = wrap switch {
                EditorWordWrap.Off => "off",
                EditorWordWrap.On => "on",
                _ => throw new ArgumentOutOfRangeException(nameof(wrap))
            };
            EditorOptionHelper.SendOption(commandChannel, EditorOption.WordWrap, monacoValue);
        }

        void SetAutoIndent(EditorAutoIndent indent) {
            string monacoValue = indent switch {
                EditorAutoIndent.None => "none",
                EditorAutoIndent.Keep => "keep",
                EditorAutoIndent.Brackets => "brackets",
                EditorAutoIndent.Advanced => "advanced",
                EditorAutoIndent.Full => "full",
                _ => throw new ArgumentOutOfRangeException(nameof(indent))
            };
            EditorOptionHelper.SendOption(commandChannel, EditorOption.AutoIndent, monacoValue);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.TabSize, TabSize);
        }

        void SetTheme(string name) {
            if(string.IsNullOrWhiteSpace(name))
                return;
            commandChannel.Send(EditorCommandType.SetTheme, name);
        }

        void SetEditorLanguage(string language) {
            if(string.IsNullOrWhiteSpace(language))
                return;
            commandChannel.Send(EditorCommandType.SetLanguage, language);
        }

        void RestoreRegisteredLanguages() {
            foreach(LanguageDescriptor language in registeredLanguages.Values) {
                RegisterLanguage(language);
            }
        }

        public void RegisterLanguage(LanguageDescriptor language) {
            if(language == null)
                throw new ArgumentNullException(nameof(language));

            var payload = new {
                id = language.Id,
                monarch = language.Monarch,
                configuration = language.Configuration
            };
            commandChannel.Send(EditorCommandType.RegisterLanguage, payload);
            registeredLanguages[language.Id] = language;
        }

        void ApplyCurrentState() {
            RestoreRegisteredLanguages();

            SetEditorLanguage(EditorLanguage);
            SetWordWrap(WordWrap);
            SetTheme(ThemeName);
            SetAutoIndent(AutoIndent);
            commandChannel.Send(EditorCommandType.SetReadOnly, ReadOnly);
            commandChannel.Send(EditorCommandType.SetText, Text);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.LineNumbers, ShowLineNumbers);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.Minimap, ShowMinimap);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.GlyphMargin, ShowGlyphMargin);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.Folding, EnableFolding);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.ContextMenu, EnableContextMenu);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.SmoothScrolling, EnableSmoothScrolling);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.ScrollBeyondLastLine, EnableScrollBeyondLastLine);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.ScrollBeyondLastColumn, ScrollBeyondLastColumn);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.LineNumbersMinChars, LineNumbersMinChars);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.DragAndDrop, EnableDragAndDrop);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.MouseWheelZoom, EnableMouseWheelZoom);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.StickyScroll, new { enabled = EnableStickyScroll });
            EditorOptionHelper.SendOption(commandChannel, EditorOption.TabSize, TabSize);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.InsertSpaces, InsertSpaces);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.DetectIndentation, DetectIndentation);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.EnableQuickSuggestions, EnableQuickSuggestions);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.EnableWordBasedSuggestions, EnableWordBasedSuggestions ? "currentDocument" : "off");
            EditorOptionHelper.SendOption(commandChannel, EditorOption.EnableSuggestOnTriggerCharacters, EnableSuggestOnTriggerCharacters);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.EnableParameterHints, new { enabled = EnableParameterHints });
        }
    }
}
