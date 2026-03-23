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
            AdjustDXColors(monacoTheme, skinName);

            _editor.RegisterTheme(monacoTheme);
            _editor.ThemeName = monacoTheme.Name;
        }

        private void AdjustDXColors(MonacoTheme monacoTheme, string skinName) {
            if (monacoTheme.Colors == null)
                return;

            switch (skinName) {
                case "WXI":
                    if (monacoTheme.Colors.ContainsKey(MonacoColorKeys.SelectionBackground))
                        monacoTheme.Colors[MonacoColorKeys.SelectionBackground] = monacoTheme.Colors[MonacoColorKeys.SelectionBackground].Lighten(0.5);
                    if (monacoTheme.Colors.ContainsKey(MonacoColorKeys.InactiveSelectionBackground))
                        monacoTheme.Colors[MonacoColorKeys.InactiveSelectionBackground] = monacoTheme.Colors[MonacoColorKeys.InactiveSelectionBackground].Lighten(0.5);
                    break;
                case "VS2019Light":
                    if (monacoTheme.Colors.ContainsKey(MonacoColorKeys.SelectionBackground))
                        monacoTheme.Colors[MonacoColorKeys.SelectionBackground] = monacoTheme.Colors[MonacoColorKeys.SelectionBackground].Lighten(0.5);
                    if (monacoTheme.Colors.ContainsKey(MonacoColorKeys.InactiveSelectionBackground))
                        monacoTheme.Colors[MonacoColorKeys.InactiveSelectionBackground] = monacoTheme.Colors[MonacoColorKeys.InactiveSelectionBackground].Lighten(0.5);
                    break;
                case "Office 2019 Colorful":
                case "WXI Compact":
                case "VS2019Blue":
                    if (monacoTheme.Colors.ContainsKey(MonacoColorKeys.SuggestWidgetSelectedBackground))
                        monacoTheme.Colors[MonacoColorKeys.SuggestWidgetSelectedBackground] = monacoTheme.Colors[MonacoColorKeys.SuggestWidgetSelectedBackground].Darken(0.3);
                    break;
                case "WXI Dark":
                case "WXI Dark Compact":
                    if (monacoTheme.Colors.ContainsKey(MonacoColorKeys.SelectionBackground))
                        monacoTheme.Colors[MonacoColorKeys.SelectionBackground] = monacoTheme.Colors[MonacoColorKeys.SelectionBackground].Darken(0.4);
                    if (monacoTheme.Colors.ContainsKey(MonacoColorKeys.InactiveSelectionBackground))
                        monacoTheme.Colors[MonacoColorKeys.InactiveSelectionBackground] = monacoTheme.Colors[MonacoColorKeys.InactiveSelectionBackground].Darken(0.4);
                    if (monacoTheme.Colors.ContainsKey(MonacoColorKeys.SuggestWidgetSelectedBackground))
                        monacoTheme.Colors[MonacoColorKeys.SuggestWidgetSelectedBackground] = monacoTheme.Colors[MonacoColorKeys.SuggestWidgetSelectedBackground].Darken(0.4);
                    break;
                case "VS2019Dark":
                    if (monacoTheme.Colors.ContainsKey(MonacoColorKeys.SelectionBackground))
                        monacoTheme.Colors[MonacoColorKeys.SelectionBackground] = monacoTheme.Colors[MonacoColorKeys.SelectionBackground].Darken(0.1);
                    if (monacoTheme.Colors.ContainsKey(MonacoColorKeys.InactiveSelectionBackground))
                        monacoTheme.Colors[MonacoColorKeys.InactiveSelectionBackground] = monacoTheme.Colors[MonacoColorKeys.InactiveSelectionBackground].Darken(0.1);
                    break;
                default:
                    break;
            }
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
            var baseKind = ResolveBase(skinName);

            var result = new MonacoTheme {
                Name = $"{skinName.ToLowerInvariant().Replace(" ", "-")}",
                Base = baseKind,
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

            Color? TrySystem(params KnownColor[] knownColors) {
                foreach (var kc in knownColors) {
                    try {
                        var c = LookAndFeelHelper.GetSystemColor(lookAndFeel, Color.FromKnownColor(kc));
                        if (c != Color.Empty && c != Color.Transparent)
                            return c;
                    } catch { }
                }
                return null;
            }

            void Map(string monacoKey, Color? color) {
                if (color.HasValue)
                    result[monacoKey] = color.Value;
            }

            // === Base colors ===
            var windowBg = TrySystem(KnownColor.Window, KnownColor.Control);
            var windowFg = TrySystem(KnownColor.WindowText, KnownColor.ControlText);
            var highlight = TrySystem(KnownColor.Highlight);
            var highlightText = TrySystem(KnownColor.HighlightText);
            var grayText = TrySystem(KnownColor.GrayText);
            var controlBg = TrySystem(KnownColor.Control);
            var controlDark = TrySystem(KnownColor.ControlDark);
            var controlDarkDark = TrySystem(KnownColor.ControlDarkDark);

            Map(MonacoColorKeys.EditorBackground, windowBg);
            Map(MonacoColorKeys.EditorForeground, windowFg);

            // === Line numbers ===
            Map(MonacoColorKeys.LineNumberForeground, grayText);
            Map(MonacoColorKeys.LineNumberActiveForeground, windowFg);

            // === Cursor & selection ===
            Map(MonacoColorKeys.CursorForeground, highlight ?? windowFg);
            Map(MonacoColorKeys.SelectionBackground, highlight);
            Map(MonacoColorKeys.InactiveSelectionBackground, highlight ?? controlDark);

            // === Current line ===
            Map(MonacoColorKeys.LineHighlightBackground, controlBg);

            // === Gutter ===
            Map(MonacoColorKeys.GutterBackground, windowBg);

            // === Indent guides ===
            Map(MonacoColorKeys.IndentGuideBackground, controlDark);
            Map(MonacoColorKeys.IndentGuideActiveBackground, grayText);

            // === Scrollbar ===
            Map(MonacoColorKeys.ScrollbarShadow, controlDark);
            Map(MonacoColorKeys.ScrollbarSliderBackground, controlDark);
            Map(MonacoColorKeys.ScrollbarSliderHoverBackground, controlDarkDark);
            Map(MonacoColorKeys.ScrollbarSliderActiveBackground, controlDark);

            // === Brackets ===
            Map(MonacoColorKeys.BracketMatchBackground, controlBg);
            Map(MonacoColorKeys.BracketMatchBorder, highlight);

            // === Find ===
            Map(MonacoColorKeys.FindMatchBackground, highlight);
            Map(MonacoColorKeys.FindMatchHighlightBackground, highlight);

            // === Hover ===
            Map(MonacoColorKeys.HoverWidgetBackground, controlBg);
            Map(MonacoColorKeys.HoverWidgetBorder, controlDark);

            // === Suggest ===
            Map(MonacoColorKeys.SuggestWidgetBackground, controlBg ?? windowBg);
            Map(MonacoColorKeys.SuggestWidgetSelectedBackground, highlight);

            return result;
        }

        private static MonacoThemeBase ResolveBase(string skinName) {
            if (string.IsNullOrWhiteSpace(skinName))
                return MonacoThemeBase.Light;

            if (skinName.Contains("HighContrast", StringComparison.OrdinalIgnoreCase))
                return MonacoThemeBase.HighContrast;

            if (skinName.Contains("Dark", StringComparison.OrdinalIgnoreCase) ||
                skinName.Contains("Black", StringComparison.OrdinalIgnoreCase))
                return MonacoThemeBase.Dark;

            return MonacoThemeBase.Light;
        }

        public void Dispose() {
            if (!_disposed) {
                Detach();
                _disposed = true;
            }
        }
    }
}
