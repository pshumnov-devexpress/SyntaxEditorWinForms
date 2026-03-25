using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SyntaxEditorWinForms.Theming {
    public class ThemeBehavior : IDisposable {

        private SyntaxEditor? _editor;
        private bool _disposed;

        public ThemeBehavior() { }

        public IReadOnlyList<MonacoThemeRule>? Rules { get; set; }

        private bool _applyDevExpressColors = true;
        public bool ApplyDevExpressColors {
            get => _applyDevExpressColors;
            set {
                if (_applyDevExpressColors == value) return;
                _applyDevExpressColors = value;
                ApplyCurrentTheme();
            }
        }

        public void Attach(SyntaxEditor editor) {
            if (_editor != null)
                Detach();

            _editor = editor;
            UserLookAndFeel.Default.StyleChanged += OnStyleChanged;
            _editor.EditorInitialized += OnEditorInitialized;
        }

        public void Detach() {
            UserLookAndFeel.Default.StyleChanged -= OnStyleChanged;
            if (_editor != null) {
                _editor.EditorInitialized -= OnEditorInitialized;
                _editor = null;
            }
        }

        private void OnStyleChanged(object? sender, EventArgs e) {
            ApplyCurrentTheme();
        }

        private void OnEditorInitialized(object? sender, EventArgs e) {
            ApplyCurrentTheme();
        }

        public void ApplyCurrentTheme() {
            if (_editor == null)
                return;

            var skinName = UserLookAndFeel.Default.ActiveSkinName;
            var monacoTheme = CreateFromDXTheme(skinName, this.Rules, this.ApplyDevExpressColors);

            _editor.RegisterTheme(monacoTheme);
            _editor.ThemeName = monacoTheme.Name;
        }

        public static Color? GetColor(string skinElementName) {
            var skin = CommonSkins.GetSkin(UserLookAndFeel.Default);
            if (skin == null) return null;

            var element = skin[skinElementName];
            if (element != null && element.Color.BackColor != Color.Empty && element.Color.BackColor != Color.Transparent) {
                return element.Color.BackColor;
            }

            return null;
        }

        public static Color? GetSystemColor(KnownColor knownColor) {
            try {
                var lookAndFeel = UserLookAndFeel.Default;
                var skin = CommonSkins.GetSkin(lookAndFeel);
                if (skin == null) return null;

                var c = LookAndFeelHelper.GetSystemColor(lookAndFeel, Color.FromKnownColor(knownColor));
                if (c != Color.Empty && c != Color.Transparent)
                    return c;
                return null;
            } catch {
                return null;
            }
        }

        public static MonacoTheme CreateFromDXTheme(string skinName, IReadOnlyList<MonacoThemeRule>? rules = null, bool applyDevExpressColors = false) {
            var result = new MonacoTheme {
                Name = $"{skinName.ToLowerInvariant().Replace(" ", "-")}",
                Base = MonacoThemeBase.Light,
                Colors = applyDevExpressColors ? CreateMonacoColors() : null,
                Rules = rules
            };

            return result;
        }

        private static Dictionary<string, Color> CreateMonacoColors() {
            var result = new Dictionary<string, Color>();

            var lookAndFeel = UserLookAndFeel.Default;
            var skin = CommonSkins.GetSkin(lookAndFeel);
            if (skin == null)
                return result;

            var reportSkin = ReportsSkins.GetSkin(lookAndFeel);
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

        public void Dispose() {
            if (!_disposed) {
                Detach();
                _disposed = true;
            }
        }
    }
}
