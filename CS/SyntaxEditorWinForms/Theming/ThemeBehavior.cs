using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
                Rules = applyDevExpressColors ? CreateDevExpressTokenRules(rules) : rules,
                Inherit = !applyDevExpressColors
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

        private static IReadOnlyList<MonacoThemeRule> CreateDevExpressTokenRules(IReadOnlyList<MonacoThemeRule>? rules) {
            List<MonacoThemeRule> result = rules != null ? new List<MonacoThemeRule>(rules) : new List<MonacoThemeRule>();

            var lookAndFeel = UserLookAndFeel.Default;
            var skin = CommonSkins.GetSkin(lookAndFeel);
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
