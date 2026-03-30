using DevExpress.Skins;
using DevExpress.XtraEditors;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using SyntaxEditorWinForms.Models;
using SyntaxEditorWinForms.Theming;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyntaxEditorWinForms {
    public class SyntaxEditor : XtraUserControl {

        private WebView2? _webView;
        private bool _editorReady;
        private bool _updatingFromEditor;

        public SyntaxEditor() {
            _webView = new WebView2();
            _webView.Dock = DockStyle.Fill;
            Controls.Add(_webView);
            LookAndFeel.StyleChanged += LookAndFeel_StyleChanged;
        }

        private void LookAndFeel_StyleChanged(object? sender, EventArgs e) {
            ApplyCurrentTheme();
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            _ = InitializeAsync();
        }

        #region Events

        public event EventHandler? EditorInitialized;
        public new event EventHandler? TextChanged;
        public event EventHandler? IsModifiedChanged;

        #endregion Events

        #region Basic Properties

        private string _text = string.Empty;
        public new string Text {
            get => _text;
            set {
                if (_text == value)
                    return;
                _text = value ?? string.Empty;
                if (!_updatingFromEditor)
                    SetEditorText(_text);
                TextChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void SetEditorText(string text) {
            SendCommand(EditorCommandType.SetText, text);
        }

        public void MarkAsSaved() {
            SendCommand(EditorCommandType.MarkAsSaved);
        }

        private bool _readOnly;
        public bool ReadOnly {
            get => _readOnly;
            set {
                if (_readOnly == value)
                    return;
                _readOnly = value;
                SetEditorReadOnly(value);
            }
        }

        private void SetEditorReadOnly(bool readOnly) {
            SendCommand(EditorCommandType.SetReadOnly, readOnly);
        }

        private bool _isModified;
        public bool IsModified {
            get => _isModified;
            private set {
                if (_isModified == value)
                    return;
                _isModified = value;
                IsModifiedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion Basic Properties

        #region Theme Properties
        public IReadOnlyList<MonacoThemeRule>? Rules { get; set; }

        bool _applyDevExpressColors = true;
        public bool ApplyDevExpressColors {
            get => _applyDevExpressColors;
            set {
                if(_applyDevExpressColors == value) return;
                _applyDevExpressColors = value;
                ApplyCurrentTheme();
            }
        }
        #endregion

        #region Options

        private static string ToMonacoOption(EditorOption option) => option switch {
            EditorOption.LineNumbers => "lineNumbers",
            EditorOption.Minimap => "minimap",
            EditorOption.GlyphMargin => "glyphMargin",
            EditorOption.Folding => "folding",
            EditorOption.ScrollBeyondLastLine => "scrollBeyondLastLine",
            EditorOption.ScrollBeyondLastColumn => "scrollBeyondLastColumn",
            EditorOption.ContextMenu => "contextmenu",
            EditorOption.SmoothScrolling => "smoothScrolling",
            EditorOption.DragAndDrop => "dragAndDrop",
            EditorOption.MouseWheelZoom => "mouseWheelZoom",
            EditorOption.LineNumbersMinChars => "lineNumbersMinChars",
            EditorOption.WordWrap => "wordWrap",
            EditorOption.StickyScroll => "stickyScroll",
            EditorOption.TabSize => "tabSize",
            EditorOption.InsertSpaces => "insertSpaces",
            EditorOption.DetectIndentation => "detectIndentation",
            EditorOption.AutoIndent => "autoIndent",
            EditorOption.EnableQuickSuggestions => "quickSuggestions",
            EditorOption.EnableWordBasedSuggestions => "wordBasedSuggestions",
            EditorOption.EnableSuggestOnTriggerCharacters => "suggestOnTriggerCharacters",
            EditorOption.EnableParameterHints => "parameterHints",
            _ => throw new ArgumentOutOfRangeException(nameof(option))
        };

        private void UpdateOption(EditorOption option, object? value) {
            var monacoOption = ToMonacoOption(option);

            object? monacoValue = null;
            switch (option) {
                case EditorOption.LineNumbers:
                    if (value is not bool show)
                        throw new ArgumentException("LineNumbers requires boolean value.", nameof(value));
                    monacoValue = show ? "on" : "off";
                    break;
                case EditorOption.Minimap:
                    if (value is not bool enabled)
                        throw new ArgumentException("Minimap requires boolean value.", nameof(value));
                    monacoValue = new { enabled };
                    break;
                default:
                    monacoValue = value;
                    break;
            }

            SendCommand(EditorCommandType.UpdateOption, new {
                option = monacoOption,
                value = monacoValue
            });
        }

        #region ShowLineNumbers

        private bool _showLineNumbers = true;
        public bool ShowLineNumbers {
            get => _showLineNumbers;
            set {
                if (_showLineNumbers == value) return;
                _showLineNumbers = value;
                SetShowLineNumbers(value);
            }
        }

        private void SetShowLineNumbers(bool show) {
            UpdateOption(EditorOption.LineNumbers, show);
        }

        #endregion ShowLineNumbers

        #region ShowMinimap

        private bool _showMinimap;
        public bool ShowMinimap {
            get => _showMinimap;
            set {
                if (_showMinimap == value) return;
                _showMinimap = value;
                SetShowMinimap(value);
            }
        }

        private void SetShowMinimap(bool show) {
            UpdateOption(EditorOption.Minimap, show);
        }

        #endregion ShowMinimap

        #region ShowGlyphMargin

        private bool _showGlyphMargin;
        public bool ShowGlyphMargin {
            get => _showGlyphMargin;
            set {
                if (_showGlyphMargin == value) return;
                _showGlyphMargin = value;
                SetShowGlyphMargin(value);
            }
        }

        private void SetShowGlyphMargin(bool show) {
            UpdateOption(EditorOption.GlyphMargin, show);
        }

        #endregion ShowGlyphMargin

        #region EnableFolding

        private bool _enableFolding = true;
        public bool EnableFolding {
            get => _enableFolding;
            set {
                if (_enableFolding == value) return;
                _enableFolding = value;
                SetEnableFolding(value);
            }
        }

        private void SetEnableFolding(bool enabled) {
            UpdateOption(EditorOption.Folding, enabled);
        }

        #endregion EnableFolding

        #region EnableContextMenu

        private bool _enableContextMenu = true;
        public bool EnableContextMenu {
            get => _enableContextMenu;
            set {
                if (_enableContextMenu == value) return;
                _enableContextMenu = value;
                SetEnableContextMenu(value);
            }
        }

        private void SetEnableContextMenu(bool enabled) {
            UpdateOption(EditorOption.ContextMenu, enabled);
        }

        #endregion EnableContextMenu

        #region EnableSmoothScrolling

        private bool _enableSmoothScrolling;
        public bool EnableSmoothScrolling {
            get => _enableSmoothScrolling;
            set {
                if (_enableSmoothScrolling == value) return;
                _enableSmoothScrolling = value;
                SetEnableSmoothScrolling(value);
            }
        }

        private void SetEnableSmoothScrolling(bool enabled) {
            UpdateOption(EditorOption.SmoothScrolling, enabled);
        }

        #endregion EnableSmoothScrolling

        #region EnableScrollBeyondLastLine

        private bool _enableScrollBeyondLastLine = true;
        public bool EnableScrollBeyondLastLine {
            get => _enableScrollBeyondLastLine;
            set {
                if (_enableScrollBeyondLastLine == value) return;
                _enableScrollBeyondLastLine = value;
                SetEnableScrollBeyondLastLine(value);
            }
        }

        private void SetEnableScrollBeyondLastLine(bool enabled) {
            UpdateOption(EditorOption.ScrollBeyondLastLine, enabled);
        }

        #endregion EnableScrollBeyondLastLine

        #region ScrollBeyondLastColumn

        private int _scrollBeyondLastColumn = 5;
        public int ScrollBeyondLastColumn {
            get => _scrollBeyondLastColumn;
            set {
                if (_scrollBeyondLastColumn == value) return;
                _scrollBeyondLastColumn = value;
                SetScrollBeyondLastColumn(value);
            }
        }

        private void SetScrollBeyondLastColumn(int columns) {
            UpdateOption(EditorOption.ScrollBeyondLastColumn, columns);
        }

        #endregion ScrollBeyondLastColumn

        #region LineNumbersMinChars

        private int _lineNumbersMinChars = 5;
        public int LineNumbersMinChars {
            get => _lineNumbersMinChars;
            set {
                if (_lineNumbersMinChars == value) return;
                _lineNumbersMinChars = value;
                SetLineNumbersMinChars(value);
            }
        }

        private void SetLineNumbersMinChars(int minChars) {
            UpdateOption(EditorOption.LineNumbersMinChars, minChars);
        }

        #endregion LineNumbersMinChars

        #region EnableDragAndDrop

        private bool _enableDragAndDrop = true;
        public bool EnableDragAndDrop {
            get => _enableDragAndDrop;
            set {
                if (_enableDragAndDrop == value) return;
                _enableDragAndDrop = value;
                SetEnableDragAndDrop(value);
            }
        }

        private void SetEnableDragAndDrop(bool enabled) {
            UpdateOption(EditorOption.DragAndDrop, enabled);
        }

        #endregion EnableDragAndDrop

        #region EnableMouseWheelZoom

        private bool _enableMouseWheelZoom;
        public bool EnableMouseWheelZoom {
            get => _enableMouseWheelZoom;
            set {
                if (_enableMouseWheelZoom == value) return;
                _enableMouseWheelZoom = value;
                SetEnableMouseWheelZoom(value);
            }
        }

        private void SetEnableMouseWheelZoom(bool enabled) {
            UpdateOption(EditorOption.MouseWheelZoom, enabled);
        }

        #endregion EnableMouseWheelZoom

        #region WordWrap

        private EditorWordWrap _wordWrap = EditorWordWrap.Off;
        public EditorWordWrap WordWrap {
            get => _wordWrap;
            set {
                if (_wordWrap == value) return;
                _wordWrap = value;
                SetWordWrap(value);
            }
        }

        private void SetWordWrap(EditorWordWrap wordWrap) {
            string monacoValue = wordWrap switch {
                EditorWordWrap.Off => "off",
                EditorWordWrap.On => "on",
                _ => throw new ArgumentOutOfRangeException(nameof(wordWrap))
            };
            UpdateOption(EditorOption.WordWrap, monacoValue);
        }

        #endregion WordWrap

        #region EnableStickyScroll

        private bool _enableStickyScroll = true;
        public bool EnableStickyScroll {
            get => _enableStickyScroll;
            set {
                if (_enableStickyScroll == value) return;
                _enableStickyScroll = value;
                SetEnableStickyScroll(value);
            }
        }

        private void SetEnableStickyScroll(bool enabled) {
            UpdateOption(EditorOption.StickyScroll, new { enabled });
        }

        #endregion EnableStickyScroll

        #region TabSize

        private int _tabSize = 4;
        public int TabSize {
            get => _tabSize;
            set {
                if (value < 1 || value > 64)
                    throw new ArgumentOutOfRangeException(nameof(value));
                if (_tabSize == value) return;
                _tabSize = value;
                SetTabSize(value);
            }
        }

        private void SetTabSize(int size) {
            UpdateOption(EditorOption.TabSize, size);
        }

        #endregion TabSize

        #region DetectIndentation

        private bool _detectIndentation = true;
        public bool DetectIndentation {
            get => _detectIndentation;
            set {
                if (_detectIndentation == value) return;
                _detectIndentation = value;
                SetDetectIndentation(value);
            }
        }

        private void SetDetectIndentation(bool detect) {
            UpdateOption(EditorOption.DetectIndentation, detect);
        }

        #endregion DetectIndentation

        #region InsertSpaces

        private bool _insertSpaces = true;
        public bool InsertSpaces {
            get => _insertSpaces;
            set {
                if (_insertSpaces == value) return;
                _insertSpaces = value;
                SetInsertSpaces(value);
            }
        }

        private void SetInsertSpaces(bool insertSpaces) {
            UpdateOption(EditorOption.InsertSpaces, insertSpaces);
        }

        #endregion InsertSpaces

        #region AutoIndent

        private EditorAutoIndent _autoIndent = EditorAutoIndent.Full;
        public EditorAutoIndent AutoIndent {
            get => _autoIndent;
            set {
                if (_autoIndent == value) return;
                _autoIndent = value;
                SetAutoIndent(value);
            }
        }

        private void SetAutoIndent(EditorAutoIndent autoIndent) {
            string monacoValue = autoIndent switch {
                EditorAutoIndent.None => "none",
                EditorAutoIndent.Keep => "keep",
                EditorAutoIndent.Brackets => "brackets",
                EditorAutoIndent.Advanced => "advanced",
                EditorAutoIndent.Full => "full",
                _ => throw new ArgumentOutOfRangeException(nameof(autoIndent))
            };

            UpdateOption(EditorOption.AutoIndent, monacoValue);
            SetTabSize(TabSize);
        }

        #endregion AutoIndent

        #region EnableQuickSuggestions

        private bool _enableQuickSuggestions = true;
        public bool EnableQuickSuggestions {
            get => _enableQuickSuggestions;
            set {
                if (_enableQuickSuggestions == value) return;
                _enableQuickSuggestions = value;
                SetEnableQuickSuggestions(value);
            }
        }

        private void SetEnableQuickSuggestions(bool enabled) {
            UpdateOption(EditorOption.EnableQuickSuggestions, enabled);
        }

        #endregion EnableQuickSuggestions

        #region EnableWordBasedSuggestions

        private bool _enableWordBasedSuggestions = true;
        public bool EnableWordBasedSuggestions {
            get => _enableWordBasedSuggestions;
            set {
                if (_enableWordBasedSuggestions == value) return;
                _enableWordBasedSuggestions = value;
                SetEnableWordBasedSuggestions(value);
            }
        }

        private void SetEnableWordBasedSuggestions(bool enabled) {
            var value = enabled ? "currentDocument" : "off";
            UpdateOption(EditorOption.EnableWordBasedSuggestions, value);
        }

        #endregion EnableWordBasedSuggestions

        #region EnableSuggestOnTriggerCharacters

        private bool _enableSuggestOnTriggerCharacters = true;
        public bool EnableSuggestOnTriggerCharacters {
            get => _enableSuggestOnTriggerCharacters;
            set {
                if (_enableSuggestOnTriggerCharacters == value) return;
                _enableSuggestOnTriggerCharacters = value;
                SetEnableSuggestOnTriggerCharacters(value);
            }
        }

        private void SetEnableSuggestOnTriggerCharacters(bool enabled) {
            UpdateOption(EditorOption.EnableSuggestOnTriggerCharacters, enabled);
        }

        #endregion EnableSuggestOnTriggerCharacters

        #region EnableParameterHints

        private bool _enableParameterHints = true;
        public bool EnableParameterHints {
            get => _enableParameterHints;
            set {
                if (_enableParameterHints == value) return;
                _enableParameterHints = value;
                SetEnableParameterHints(value);
            }
        }

        private void SetEnableParameterHints(bool enabled) {
            UpdateOption(EditorOption.EnableParameterHints, new { enabled });
        }

        #endregion EnableParameterHints

        #endregion Options

        #region Communication

        private void SendCommand(EditorCommandType type, object? payload = null) {
            if (!_editorReady)
                return;

            var cmd = new EditorCommand {
                Type = type,
                Payload = payload
            };

            var options = new JsonSerializerOptions(JsonSerializerOptions.Web);

            var json = JsonSerializer.Serialize(cmd, options);
            _webView?.CoreWebView2?.PostWebMessageAsJson(json);
        }

        #endregion Communication

        #region Theming

        public void RegisterTheme(MonacoTheme theme) {
            if (theme == null)
                throw new ArgumentNullException(nameof(theme));

            var payload = new {
                name = theme.Name,
                @base = MapBase(theme.Base),
                inherit = theme.Inherit,
                colors = theme.Colors?.ToDictionary(kvp => kvp.Key, kvp => ToHex(kvp.Value)) ?? new Dictionary<string, string>(),
                rules = (theme.Rules ?? Enumerable.Empty<MonacoThemeRule>())
                    .Select(ConvertRule)
                    .Where(r => r != null)
                    .ToList()
            };

            SendCommand(EditorCommandType.RegisterTheme, payload);
        }

        private static Dictionary<string, object>? ConvertRule(MonacoThemeRule r) {
            var rule = new Dictionary<string, object> {
                ["token"] = r.Token
            };

            if (r.Foreground is Color fg)
                rule["foreground"] = ToHex(fg, false);

            if (r.Background is Color bg)
                rule["background"] = ToHex(bg, false);

            var fontStyle = ConvertFontStyle(r.FontStyle ?? MonacoFontStyle.None);
            if (!string.IsNullOrEmpty(fontStyle))
                rule["fontStyle"] = fontStyle;

            return rule.Count > 1 ? rule : null;
        }

        private static string? ConvertFontStyle(MonacoFontStyle style) {
            if (style == MonacoFontStyle.None)
                return null;

            var sb = new StringBuilder(32);

            if ((style & MonacoFontStyle.Bold) != 0)
                sb.Append("bold ");

            if ((style & MonacoFontStyle.Italic) != 0)
                sb.Append("italic ");

            if ((style & MonacoFontStyle.Underline) != 0)
                sb.Append("underline ");

            if (sb.Length == 0)
                return null;

            sb.Length--;
            return sb.ToString();
        }

        private static string MapBase(MonacoThemeBase value) => value switch {
            MonacoThemeBase.Light => "vs",
            MonacoThemeBase.Dark => "vs-dark",
            MonacoThemeBase.HighContrast => "hc-black",
            _ => throw new ArgumentOutOfRangeException()
        };

        private static string ToHex(Color c, bool addHashTag = true)
            => $"{(addHashTag ? "#" : string.Empty)}{c.R:X2}{c.G:X2}{c.B:X2}{c.A:X2}".ToLower();

        private string _themeName = "vs";
        public string ThemeName {
            get => _themeName;
            set {
                if (_themeName == value) return;
                _themeName = value;
                SetTheme(value);
            }
        }

        private void SetTheme(string themeName) {
            if (string.IsNullOrWhiteSpace(themeName))
                return;
            SendCommand(EditorCommandType.SetTheme, themeName);
        }

        public void ApplyCurrentTheme() {
            var skinName = LookAndFeel.ActiveSkinName;
            var monacoTheme = CreateFromDXTheme(skinName, this.Rules, this.ApplyDevExpressColors);

            RegisterTheme(monacoTheme);
            ThemeName = monacoTheme.Name;
        }

        #region Build Theme
        private MonacoTheme CreateFromDXTheme(string skinName, IReadOnlyList<MonacoThemeRule>? rules = null, bool applyDevExpressColors = false) {
            var result = new MonacoTheme {
                Name = $"{skinName.ToLowerInvariant().Replace(" ", "-")}",
                Base = MonacoThemeBase.Light,
                Colors = applyDevExpressColors ? CreateMonacoColors() : null,
                Rules = applyDevExpressColors ? CreateDevExpressTokenRules(rules) : rules,
                Inherit = !applyDevExpressColors
            };
            return result;
        }

        private Dictionary<string, Color> CreateMonacoColors() {
            var result = new Dictionary<string, Color>();
            var skin = CommonSkins.GetSkin(LookAndFeel);
            if(skin == null)
                return result;

            var reportSkin = ReportsSkins.GetSkin(LookAndFeel);
            var reportElem = reportSkin[ReportsSkins.SkinScriptControl];

            // === Base colors ===
            result[MonacoColorKeys.EditorBackground] = skin.Colors.GetColor("Window");
            result[MonacoColorKeys.EditorForeground] = skin.Colors.GetColor("WindowText");

            // === Line numbers ===
            result[MonacoColorKeys.LineNumberForeground] = reportElem.Properties.GetColor("LineNumbersColor");
            result[MonacoColorKeys.LineNumberActiveForeground] = skin.Colors.GetColor("HighlightAlternate");

            // === Cursor ===
            result[MonacoColorKeys.CursorForeground] = skin.Colors.GetColor("WindowText");

            // === Selection ===
            var selectionElem = skin[CommonSkins.SkinSelection];
            int opacity = selectionElem.Properties.GetInteger(CommonSkins.SkinSelectionOpacity, 40);
            var color = Color.FromArgb(opacity, selectionElem.Color.GetBackColor());
            result[MonacoColorKeys.SelectionBackground] = color;
            result[MonacoColorKeys.InactiveSelectionBackground] = color;

            // === Current line ===
            result[MonacoColorKeys.LineHighlightBackground] = skin.Colors.GetColor("Control");

            // === Gutter ===
            result[MonacoColorKeys.GutterBackground] = skin.Colors.GetColor("Window");

            // === Indent guides ===
            result[MonacoColorKeys.IndentGuideBackground] = skin.Colors.GetColor("DisabledText");
            result[MonacoColorKeys.IndentGuideActiveBackground] = reportElem.Properties.GetColor("LineNumbersColor");

            // === Scrollbar ===
            result[MonacoColorKeys.ScrollbarShadow] = ColorTranslator.FromHtml("#a0a0a0");
            result[MonacoColorKeys.ScrollbarSliderBackground] = skin.Colors.GetColor("DisabledText");
            result[MonacoColorKeys.ScrollbarSliderHoverBackground] = skin.Colors.GetColor("WindowText");
            result[MonacoColorKeys.ScrollbarSliderActiveBackground] = skin.Colors.GetColor("WindowText");

            // === Brackets ===
            var bracketColor = reportElem.Properties.GetColor("BracketHighlightColor");
            var bracketOpacity = reportElem.Properties.GetInteger("BracketHighlightColorAlpha", 40);
            result[MonacoColorKeys.BracketMatchBackground] = Color.FromArgb(bracketOpacity, bracketColor);
            result[MonacoColorKeys.BracketMatchBorder] = Color.FromArgb(bracketOpacity, bracketColor);
            result[MonacoColorKeys.BracketMatchHighlightForeground1] = skin.Colors.GetColor("Question");
            result[MonacoColorKeys.BracketMatchHighlightForeground2] = skin.Colors.GetColor("Information");
            result[MonacoColorKeys.BracketMatchHighlightForeground3] = skin.Colors.GetColor("Critical");

            // === Find ===
            result[MonacoColorKeys.FindMatchBackground] = skin.Colors.GetColor("Warning");
            result[MonacoColorKeys.FindMatchHighlightBackground] = skin.Colors.GetColor("Warning");

            // === Hover ===
            result[MonacoColorKeys.HoverWidgetBackground] = skin.Colors.GetColor("Window");
            result[MonacoColorKeys.HoverWidgetBorder] = skin.Colors.GetColor("DisabledText");

            // === Suggest ===
            result[MonacoColorKeys.SuggestWidgetBackground] = skin.Colors.GetColor("Window");
            result[MonacoColorKeys.SuggestWidgetSelectedBackground] = skin.Colors.GetColor("Highlight");

            return result;
        }

        private IReadOnlyList<MonacoThemeRule> CreateDevExpressTokenRules(IReadOnlyList<MonacoThemeRule>? rules) {
            List<MonacoThemeRule> result = rules != null ? new List<MonacoThemeRule>(rules) : new List<MonacoThemeRule>();

            var skin = CommonSkins.GetSkin(LookAndFeel);
            if(skin == null)
                return result;

            Color blue = skin.Colors.GetColor("Question");
            Color green = skin.Colors.GetColor("Information");
            Color red = skin.Colors.GetColor("Critical");
            Color black = skin.Colors.GetColor("WindowText");
            Color white = skin.Colors.GetColor("Window");

            var existingTokens = new HashSet<string>(result.Select(r => r.Token));

            void AddRuleIfNotExists(string token, Color foreground) {
                if(!existingTokens.Contains(token)) {
                    result.Add(new MonacoThemeRule { Token = token, Foreground = foreground });
                }
            }

            // Comments
            AddRuleIfNotExists("comment", green);

            // Keywords
            AddRuleIfNotExists("keyword", blue);

            // Types and Classes
            AddRuleIfNotExists("type", blue);
            AddRuleIfNotExists("class", blue);
            AddRuleIfNotExists("interface", blue);
            AddRuleIfNotExists("enum", blue);
            AddRuleIfNotExists("struct", blue);

            // Functions and Methods
            AddRuleIfNotExists("function", black);
            AddRuleIfNotExists("method", black);

            // Strings
            AddRuleIfNotExists("string", red);
            AddRuleIfNotExists("regexp", red);
            AddRuleIfNotExists("regex", red);

            // Numbers and Constants
            AddRuleIfNotExists("number", green);
            AddRuleIfNotExists("constant", green);
            AddRuleIfNotExists("constant.language", blue);

            // Variables and Identifiers
            AddRuleIfNotExists("variable", black);
            AddRuleIfNotExists("identifier", black);
            AddRuleIfNotExists("parameter", black);
            AddRuleIfNotExists("property", black);
            AddRuleIfNotExists("member", black);

            // Operators and Delimiters
            AddRuleIfNotExists("operator", black);
            AddRuleIfNotExists("delimiter", black);
            AddRuleIfNotExists("punctuation", black);

            // Namespaces
            AddRuleIfNotExists("namespace", black);

            // Attributes and Annotations
            AddRuleIfNotExists("annotation", green);
            AddRuleIfNotExists("decorator", green);
            AddRuleIfNotExists("attribute", green);

            // Preprocessor
            AddRuleIfNotExists("preprocessor", blue);
            AddRuleIfNotExists("macro", blue);

            // Tags (for XML/HTML)
            AddRuleIfNotExists("tag", blue);

            // Invalid and Errors
            AddRuleIfNotExists("invalid", red);
            AddRuleIfNotExists("error", red);

            return result;
        }
        #endregion

        #endregion Theming

        #region Processing Monaco Messages

        private void CoreWebView2_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e) {
            if (sender is not CoreWebView2)
                return;

            EditorMessage? message;

            try {
                message = JsonSerializer.Deserialize<EditorMessage>(e.WebMessageAsJson, JsonSerializerOptions.Web);
            } catch {
                return;
            }

            if (message?.Type == null)
                return;

            switch (message.Type) {
                case EditorMessageType.TextChanged:
                    HandleTextChanged(message.Payload.GetString() ?? string.Empty);
                    break;
                case EditorMessageType.EditorReady:
                    HandleEditorReady();
                    break;
                case EditorMessageType.IsDirtyChanged:
                    IsModified = message.Payload.GetBoolean();
                    break;
                case EditorMessageType.Languages:
                    var langs = message.Payload
                        .EnumerateArray()
                        .Select(x => x.GetString()!)
                        .ToList();

                    LanguagesReceivedInternal?.Invoke(this, langs);
                    break;
                default:
                    break;
            }
        }

        private void HandleTextChanged(string text) {
            _updatingFromEditor = true;
            try {
                _text = text;
                TextChanged?.Invoke(this, EventArgs.Empty);
            } finally {
                _updatingFromEditor = false;
            }
        }

        private void HandleEditorReady() {
            if (_editorReady)
                return;

            _editorReady = true;
            ApplyCurrentState();
            ApplyCurrentTheme();
            EditorInitialized?.Invoke(this, EventArgs.Empty);
        }

        #endregion Processing Monaco Messages

        #region Language Support

        private string _editorLanguage = "csharp";
        public string EditorLanguage {
            get => _editorLanguage;
            set {
                if (_editorLanguage == value) return;
                _editorLanguage = value;
                SetEditorLanguage(value);
            }
        }

        private void SetEditorLanguage(string language) {
            if (string.IsNullOrWhiteSpace(language))
                return;

            SendCommand(EditorCommandType.SetLanguage, language);
        }

        private event EventHandler<IReadOnlyList<string>>? LanguagesReceivedInternal;

        public async Task<IReadOnlyList<string>> GetAvailableLanguagesAsync(CancellationToken cancellationToken = default) {

            var tcs = new TaskCompletionSource<IReadOnlyList<string>>(TaskCreationOptions.RunContinuationsAsynchronously);

            void Handler(object? s, IReadOnlyList<string> langs) {
                LanguagesReceivedInternal -= Handler;
                tcs.TrySetResult(langs);
            }

            LanguagesReceivedInternal += Handler;

            using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);

            using (linkedCts.Token.Register(() => {
                LanguagesReceivedInternal -= Handler;
                tcs.TrySetCanceled(linkedCts.Token);
            })) {
                try {
                    SendCommand(EditorCommandType.GetLanguages);
                    return await tcs.Task.ConfigureAwait(false);
                } finally {
                    LanguagesReceivedInternal -= Handler;
                }
            }
        }

        private readonly Dictionary<string, LanguageDescriptor> _registeredLanguages = new();

        public void RegisterLanguage(LanguageDescriptor language) {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            var payload = new {
                id = language.Id,
                monarch = language.Monarch,
                configuration = language.Configuration
            };
            SendCommand(EditorCommandType.RegisterLanguage, payload);
            _registeredLanguages[language.Id] = language;
        }

        private void RestoreRegisteredLanguages() {
            foreach (var language in _registeredLanguages.Values) {
                RegisterLanguage(language);
            }
        }

        #endregion Language Support

        #region Initialization and Cleanup

        private async Task InitializeAsync() {
            var webView = _webView;
            if (webView == null)
                return;

            await webView.EnsureCoreWebView2Async();

            if (_webView != webView)
                return;

            webView.CoreWebView2.WebMessageReceived -= CoreWebView2_WebMessageReceived;
            webView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
            webView.CoreWebView2.ContextMenuRequested -= CoreWebView2_ContextMenuRequested;
            webView.CoreWebView2.ContextMenuRequested += CoreWebView2_ContextMenuRequested;

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Monaco", "index.html");

            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            webView.Source = new Uri(path);
        }

        private void CoreWebView2_ContextMenuRequested(object? sender, CoreWebView2ContextMenuRequestedEventArgs e) {
            e.Handled = true;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                var webView = _webView;
                if (webView != null) {
                    if (webView.CoreWebView2 != null) {
                        webView.CoreWebView2.WebMessageReceived -= CoreWebView2_WebMessageReceived;
                        webView.CoreWebView2.ContextMenuRequested -= CoreWebView2_ContextMenuRequested;
                    }
                    webView.Dispose();
                    _webView = null;
                }
                _editorReady = false;
                LookAndFeel.StyleChanged -= LookAndFeel_StyleChanged;
            }
            base.Dispose(disposing);
        }

        #endregion Initialization and Cleanup

        private void ApplyCurrentState() {
            RestoreRegisteredLanguages();

            SetEditorLanguage(EditorLanguage);
            SetEditorReadOnly(ReadOnly);
            SetEditorText(Text);
            SetShowLineNumbers(ShowLineNumbers);
            SetShowMinimap(ShowMinimap);
            SetShowGlyphMargin(ShowGlyphMargin);
            SetEnableFolding(EnableFolding);
            SetEnableContextMenu(EnableContextMenu);
            SetEnableSmoothScrolling(EnableSmoothScrolling);
            SetEnableScrollBeyondLastLine(EnableScrollBeyondLastLine);
            SetScrollBeyondLastColumn(ScrollBeyondLastColumn);
            SetLineNumbersMinChars(LineNumbersMinChars);
            SetEnableDragAndDrop(EnableDragAndDrop);
            SetEnableMouseWheelZoom(EnableMouseWheelZoom);
            SetWordWrap(WordWrap);
            SetTheme(ThemeName);
            SetEnableStickyScroll(EnableStickyScroll);
            SetTabSize(TabSize);
            SetInsertSpaces(InsertSpaces);
            SetDetectIndentation(DetectIndentation);
            SetAutoIndent(AutoIndent);
            SetEnableQuickSuggestions(EnableQuickSuggestions);
            SetEnableWordBasedSuggestions(EnableWordBasedSuggestions);
            SetEnableSuggestOnTriggerCharacters(EnableSuggestOnTriggerCharacters);
            SetEnableParameterHints(EnableParameterHints);
        }
    }
}
