using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using SyntaxEditor.Models;
using SyntaxEditor.Theming;

namespace SyntaxEditor {
    public partial class SyntaxEditor : XtraUserControl, IEditorMessageHandler {
        bool applyDevExpressColors = true;
        EditorAutoIndent autoIndent = EditorAutoIndent.Full;
        IEditorCommandChannel commandChannel = null!;
        bool detectIndentation = true;
        string editorLanguage = "csharp";
        bool enableContextMenu = true;
        bool enableDragAndDrop = true;
        bool enableFolding = true;
        bool enableMouseWheelZoom = false;
        bool enableParameterHints = true;
        bool enableQuickSuggestions = true;
        bool enableScrollBeyondLastLine = true;
        bool enableSmoothScrolling;
        bool enableStickyScroll = true;
        bool enableSuggestOnTriggerCharacters = true;
        bool enableWordBasedSuggestions = true;
        bool insertSpaces = true;
        bool isModified = false;
        TaskCompletionSource<IReadOnlyList<string>>? languagesTcs;
        int lineNumbersMinChars = 5;
        EditorMessageDispatcher messageDispatcher = null!;
        bool readOnly = false;
        readonly Dictionary<string, LanguageDescriptor> registeredLanguages = new();
        int scrollBeyondLastColumn = 5;
        bool showGlyphMargin = false;
        bool showLineNumbers = true;
        bool showMinimap = false;
        int tabSize = 4;
        string text = string.Empty;
        string themeName = "vs";
        bool updatingFromEditor;
        WebView2? webView;
        EditorWordWrap wordWrap = EditorWordWrap.Off;

        public SyntaxEditor() {
            webView = new WebView2 {
                Dock = DockStyle.Fill
            };
            commandChannel = new WebView2CommandChannel(webView);
            messageDispatcher = new EditorMessageDispatcher(this);
            Controls.Add(webView);
            LookAndFeel.StyleChanged += LookAndFeel_StyleChanged;
            Rules = new List<MonacoThemeRule>();
        }

        static string? ConvertFontStyle(MonacoFontStyle style) {
            if(style == MonacoFontStyle.None)
                return null;

            StringBuilder sb = new StringBuilder(32);

            if((style & MonacoFontStyle.Bold) != 0)
                sb.Append("bold ");

            if((style & MonacoFontStyle.Italic) != 0)
                sb.Append("italic ");

            if((style & MonacoFontStyle.Underline) != 0)
                sb.Append("underline ");

            if(sb.Length == 0)
                return null;

            sb.Length--;
            return sb.ToString();
        }

        // Theming helpers
        static Dictionary<string, object>? ConvertRule(MonacoThemeRule r) {
            Dictionary<string, object> rule = new Dictionary<string, object> {
                ["token"] = r.Token
            };

            if(r.Foreground is Color fg)
                rule["foreground"] = ToHex(fg, false);

            if(r.Background is Color bg)
                rule["background"] = ToHex(bg, false);

            string? fontStyle = ConvertFontStyle(r.FontStyle ?? MonacoFontStyle.None);
            if(!string.IsNullOrEmpty(fontStyle))
                rule["fontStyle"] = fontStyle;

            return rule.Count > 1 ? rule : null;
        }

        void CoreWebView2_ContextMenuRequested(object? sender, CoreWebView2ContextMenuRequestedEventArgs e) {
            e.Handled = true;
        }

        void HandleEditorReady() {
            if(commandChannel.IsReady)
                return;

            commandChannel.IsReady = true;
            ApplyCurrentState();
            ApplyCurrentTheme();
            EventHandler? handler = Events[nameof(EditorInitialized)] as EventHandler;
            handler?.Invoke(this, EventArgs.Empty);
        }

        void HandleTextChanged(string newText) {
            updatingFromEditor = true;
            try {
                text = newText;
                EventHandler? handler = Events[nameof(TextChanged)] as EventHandler;
                handler?.Invoke(this, EventArgs.Empty);
            }
            finally {
                updatingFromEditor = false;
            }
        }

        async Task InitializeAsync() {
            WebView2? wv = webView;
            if(wv == null)
                return;

            await wv.EnsureCoreWebView2Async();

            if(webView != wv)
                return;

            // Unsubscribing followed by subscribing is needed to avoid registering multiple handlers in case SyntaxEditor is reinitialized.
            wv.CoreWebView2.WebMessageReceived -= messageDispatcher.HandleWebMessageReceived;
            wv.CoreWebView2.WebMessageReceived += messageDispatcher.HandleWebMessageReceived;
            wv.CoreWebView2.ContextMenuRequested -= CoreWebView2_ContextMenuRequested;
            wv.CoreWebView2.ContextMenuRequested += CoreWebView2_ContextMenuRequested;

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Monaco", "index.html");

            if(!File.Exists(path))
                throw new FileNotFoundException(path);

            wv.Source = new Uri(path);
        }

        void LookAndFeel_StyleChanged(object? sender, EventArgs e) {
            ApplyCurrentTheme();
        }

        static string MapBase(MonacoThemeBase value)
        => value switch
        {
            MonacoThemeBase.Light => "vs",
            MonacoThemeBase.Dark => "vs-dark",
            MonacoThemeBase.HighContrast => "hc-black",
            MonacoThemeBase.HighContrastLight => "hc-light",
            _ => throw new ArgumentOutOfRangeException()
        };

        static string ToHex(Color c, bool addHashTag = true)
        => $"{(addHashTag ? "#" : string.Empty)}{c.R:X2}{c.G:X2}{c.B:X2}{c.A:X2}".ToLower();

        public void ApplyCurrentTheme() {
            MonacoTheme monacoTheme = LookAndFeel.CreateMonacoTheme(Rules, ApplyDevExpressColors);
            RegisterTheme(monacoTheme);
            ThemeName = monacoTheme.Name;
        }

        public async Task<IReadOnlyList<string>> GetAvailableLanguagesAsync(CancellationToken cancellationToken = default) {
            // RunContinuationsAsynchronously prevents the completion callback from running
            // inline on the thread that calls TrySetResult, avoiding potential deadlocks.
            TaskCompletionSource<IReadOnlyList<string>> tcs = new TaskCompletionSource<IReadOnlyList<string>>(TaskCreationOptions.RunContinuationsAsynchronously);
            languagesTcs = tcs;

            // Auto-cancel if no response arrives within 5 seconds.
            using CancellationTokenSource timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);

            // When either token fires, attempt to cancel the pending TCS.
            using(linkedCts.Token.Register(() => tcs.TrySetCanceled(linkedCts.Token))) {
                try {
                    commandChannel.Send(EditorCommandType.GetLanguages);
                    return await tcs.Task.ConfigureAwait(false);
                }
                finally {
                    // Atomically clear languagesTcs only if it still points to our instance,
                    // so a concurrent call's TCS is not accidentally discarded.
                    Interlocked.CompareExchange(ref languagesTcs, null, tcs);
                }
            }
        }

        public void MarkAsSaved() {
            commandChannel.Send(EditorCommandType.MarkAsSaved);
        }

        // Public methods
        public void RegisterTheme(MonacoTheme theme) {
            if(theme == null)
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

            commandChannel.Send(EditorCommandType.RegisterTheme, payload);
        }

        void IEditorMessageHandler.OnEditorReady() {
            HandleEditorReady();
        }

        void IEditorMessageHandler.OnIsModifiedChanged(bool isModified) {
            IsModified = isModified;
        }

        void IEditorMessageHandler.OnLanguagesReceived(IReadOnlyList<string> languages) {
            languagesTcs?.TrySetResult(languages);
        }

        // IEditorMessageHandler implementation
        void IEditorMessageHandler.OnTextChanged(string text) {
            HandleTextChanged(text);
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            _ = InitializeAsync();
        }

        protected override void Dispose(bool disposing) {
            if(disposing) {
                WebView2? wv = webView;
                if(wv != null) {
                    if(wv.CoreWebView2 != null) {
                        wv.CoreWebView2.WebMessageReceived -= messageDispatcher.HandleWebMessageReceived;
                        wv.CoreWebView2.ContextMenuRequested -= CoreWebView2_ContextMenuRequested;
                    }
                    wv.Dispose();
                    webView = null;
                }
                commandChannel.IsReady = false;
                LookAndFeel.StyleChanged -= LookAndFeel_StyleChanged;
            }
            base.Dispose(disposing);
        }
    }
}
