using SyntaxEditor.Models;

namespace SyntaxEditor {
    public interface IEditorCommandChannel {
        bool IsReady { get; set; }
        void Send(EditorCommandType type, object? payload = null);
    }
}
