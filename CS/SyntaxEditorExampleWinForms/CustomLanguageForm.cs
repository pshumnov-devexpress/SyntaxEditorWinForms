using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using SyntaxEditorWinForms;
using SyntaxEditorWinForms.Theming;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SyntaxEditorExampleWinForms {
    public class CustomLanguageForm : XtraForm {

        private TextEdit txtLanguageId;
        private SyntaxEditor monarchEditor;
        private SyntaxEditor configEditor;
        private ThemeBehavior monarchThemeBehavior;
        private ThemeBehavior configThemeBehavior;
        private SimpleButton btnSave;
        private SimpleButton btnCancel;

        private static readonly Regex LanguageIdRegex = new("^[a-zA-Z0-9\\-_]+$", RegexOptions.Compiled);

        public string LanguageId {
            get => txtLanguageId?.Text ?? string.Empty;
            set { if (txtLanguageId != null) txtLanguageId.Text = value; }
        }

        public string? Monarch {
            get => monarchEditor?.Text;
            set { if (monarchEditor != null) monarchEditor.Text = value ?? string.Empty; }
        }

        public string? Configuration {
            get => configEditor?.Text;
            set { if (configEditor != null) configEditor.Text = value ?? string.Empty; }
        }

        public CustomLanguageForm() {
            BuildUI();
        }

        private void BuildUI() {
            this.Text = "Register Custom Language";
            this.Size = new System.Drawing.Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            var mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.RowCount = 3;
            mainLayout.ColumnCount = 2;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            mainLayout.Padding = new Padding(8);

            // Row 0: Language Name
            var nameGroup = new GroupControl { Text = "Language Name:", Dock = DockStyle.Fill };
            txtLanguageId = new TextEdit();
            txtLanguageId.Dock = DockStyle.Top;
            txtLanguageId.Properties.NullValuePrompt = "Enter a Language Name (letters, digits, '-' or '_'; case-sensitive).";
            txtLanguageId.EditValueChanged += (s, e) => ValidateLanguageId();
            nameGroup.Controls.Add(txtLanguageId);
            mainLayout.Controls.Add(nameGroup, 0, 0);
            mainLayout.SetColumnSpan(nameGroup, 2);

            // Row 1: Monarch editor
            var monarchGroup = new GroupControl { Text = "Monarch:", Dock = DockStyle.Fill };
            monarchEditor = new SyntaxEditor();
            monarchEditor.Dock = DockStyle.Fill;
            monarchEditor.EditorLanguage = "javascript";
            monarchEditor.ShowLineNumbers = false;
            monarchThemeBehavior = new ThemeBehavior();
            monarchThemeBehavior.Attach(monarchEditor);
            monarchGroup.Controls.Add(monarchEditor);
            mainLayout.Controls.Add(monarchGroup, 0, 1);

            // Row 1: Configuration editor
            var configGroup = new GroupControl { Text = "Configuration:", Dock = DockStyle.Fill };
            configEditor = new SyntaxEditor();
            configEditor.Dock = DockStyle.Fill;
            configEditor.EditorLanguage = "javascript";
            configEditor.ShowLineNumbers = false;
            configThemeBehavior = new ThemeBehavior();
            configThemeBehavior.Attach(configEditor);
            configGroup.Controls.Add(configEditor);
            mainLayout.Controls.Add(configGroup, 1, 1);

            // Row 2: Buttons
            var buttonPanel = new FlowLayoutPanel();
            buttonPanel.Dock = DockStyle.Right;
            buttonPanel.AutoSize = true;
            buttonPanel.FlowDirection = FlowDirection.RightToLeft;

            btnCancel = new SimpleButton { Text = "Cancel", DialogResult = DialogResult.Cancel, Width = 80 };
            btnSave = new SimpleButton { Text = "Save", DialogResult = DialogResult.OK, Width = 80, Enabled = false };

            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Controls.Add(btnSave);

            mainLayout.Controls.Add(buttonPanel, 0, 2);
            mainLayout.SetColumnSpan(buttonPanel, 2);

            this.Controls.Add(mainLayout);
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        private void ValidateLanguageId() {
            var id = txtLanguageId.Text;
            bool valid = !string.IsNullOrWhiteSpace(id)
                && LanguageIdRegex.IsMatch(id)
                && id.Length <= 50;
            btnSave.Enabled = valid;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                monarchThemeBehavior?.Dispose();
                configThemeBehavior?.Dispose();
                monarchEditor?.Dispose();
                configEditor?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
