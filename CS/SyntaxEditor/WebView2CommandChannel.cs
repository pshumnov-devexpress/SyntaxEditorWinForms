using Microsoft.Web.WebView2.WinForms;
using SyntaxEditor.Models;
using System.Text.Json;

namespace SyntaxEditor {
    public sealed class WebView2CommandChannel : IEditorCommandChannel {
        private readonly WebView2? _webView;

        public WebView2CommandChannel(WebView2? webView) {
            _webView = webView;
        }

        public bool IsReady { get; set; }

        public void Send(EditorCommandType type, object? payload = null) {
            if (!IsReady)
                return;

            var cmd = new EditorCommand {
                Type = type,
                Payload = payload
            };

            var options = new JsonSerializerOptions(JsonSerializerOptions.Web);
            var json = JsonSerializer.Serialize(cmd, options);
            _webView?.CoreWebView2?.PostWebMessageAsJson(json);
        }
    }
}
