using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SyntaxEditorWinForms;
using SyntaxEditorWinForms.Theming;
using SyntaxEditorExampleWinForms.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SyntaxEditorExampleWinForms {
    public class RulesForm : XtraForm {

        private CheckEdit chkRawMode;
        private GridControl gridControl;
        private GridView gridView;
        private SyntaxEditor rawEditor;
        private ThemeBehavior rawEditorThemeBehavior;
        private SimpleButton btnSave;
        private SimpleButton btnCancel;
        private Panel gridPanel;
        private Panel rawPanel;

        private BindingList<RuleItem> ruleItems = new BindingList<RuleItem>();

        public RulesForm() {
            BuildUI();
        }

        public void SetRules(IReadOnlyList<MonacoThemeRule> rules) {
            ruleItems.Clear();
            foreach (var r in rules) {
                ruleItems.Add(new RuleItem {
                    Token = r.Token,
                    Foreground = r.Foreground,
                    Background = r.Background,
                    FontStyle = r.FontStyle
                });
            }
        }

        public List<MonacoThemeRule> GetRules() {
            return ruleItems.Select(r => new MonacoThemeRule {
                Token = r.Token,
                Foreground = r.Foreground,
                Background = r.Background,
                FontStyle = r.FontStyle
            }).ToList();
        }

        private void BuildUI() {
            this.Text = "Change Rules";
            this.Size = new System.Drawing.Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            var mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.RowCount = 3;
            mainLayout.ColumnCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainLayout.Padding = new Padding(8);

            // Row 0: Toggle
            chkRawMode = new CheckEdit { Text = "Edit Raw JS" };
            chkRawMode.CheckedChanged += ChkRawMode_CheckedChanged;
            mainLayout.Controls.Add(chkRawMode, 0, 0);

            // Row 1: Grid panel
            gridPanel = new Panel { Dock = DockStyle.Fill };
            BuildGrid();
            gridPanel.Controls.Add(gridControl);
            mainLayout.Controls.Add(gridPanel, 0, 1);

            // Row 1: Raw editor panel (hidden initially)
            rawPanel = new Panel { Dock = DockStyle.Fill, Visible = false };
            rawEditor = new SyntaxEditor();
            rawEditor.Dock = DockStyle.Fill;
            rawEditor.EditorLanguage = "javascript";
            rawEditor.ShowLineNumbers = false;
            rawEditor.ShowGlyphMargin = false;
            rawEditorThemeBehavior = new ThemeBehavior();
            rawEditorThemeBehavior.Attach(rawEditor);
            rawPanel.Controls.Add(rawEditor);
            mainLayout.Controls.Add(rawPanel, 0, 1);

            // Row 2: Buttons
            var buttonPanel = new FlowLayoutPanel();
            buttonPanel.Dock = DockStyle.Right;
            buttonPanel.AutoSize = true;
            buttonPanel.FlowDirection = FlowDirection.RightToLeft;

            btnCancel = new SimpleButton { Text = "Cancel", DialogResult = DialogResult.Cancel, Width = 80 };
            btnSave = new SimpleButton { Text = "Save", Width = 80 };
            btnSave.Click += BtnSave_Click;

            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Controls.Add(btnSave);

            mainLayout.Controls.Add(buttonPanel, 0, 2);

            this.Controls.Add(mainLayout);
            this.CancelButton = btnCancel;
        }

        private void BuildGrid() {
            gridControl = new GridControl();
            gridControl.Dock = DockStyle.Fill;
            gridView = new GridView(gridControl);
            gridControl.MainView = gridView;
            gridControl.DataSource = ruleItems;

            gridView.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
            gridView.OptionsView.ShowGroupPanel = false;

            // Delete button column
            var colDelete = new GridColumn { Caption = "", FieldName = "Delete", Width = 50, UnboundType = DevExpress.Data.UnboundColumnType.Object };
            colDelete.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            colDelete.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            colDelete.OptionsColumn.AllowMove = false;
            colDelete.OptionsColumn.AllowSize = false;
            colDelete.OptionsColumn.FixedWidth = true;
            var deleteButtonRepo = new RepositoryItemButtonEdit();
            deleteButtonRepo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            deleteButtonRepo.Buttons[0].Caption = "X";
            deleteButtonRepo.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete;
            deleteButtonRepo.ButtonClick += (s, e) => {
                if (gridView.FocusedRowHandle >= 0)
                    gridView.DeleteRow(gridView.FocusedRowHandle);
            };
            colDelete.ColumnEdit = deleteButtonRepo;
            gridControl.RepositoryItems.Add(deleteButtonRepo);
            colDelete.Visible = true;
            colDelete.VisibleIndex = 0;
            gridView.Columns.Add(colDelete);

            // Token column
            var colToken = new GridColumn { FieldName = "Token", Caption = "Token" };
            colToken.Visible = true;
            colToken.VisibleIndex = 1;
            gridView.Columns.Add(colToken);

            // Foreground (color picker)
            var colForeground = new GridColumn { FieldName = "Foreground", Caption = "Foreground" };
            var foregroundRepo = new RepositoryItemColorEdit();
            foregroundRepo.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            colForeground.ColumnEdit = foregroundRepo;
            gridControl.RepositoryItems.Add(foregroundRepo);
            colForeground.Visible = true;
            colForeground.VisibleIndex = 2;
            gridView.Columns.Add(colForeground);

            // Background (color picker, hidden by default)
            var colBackground = new GridColumn { FieldName = "Background", Caption = "Background" };
            var backgroundRepo = new RepositoryItemColorEdit();
            backgroundRepo.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            colBackground.ColumnEdit = backgroundRepo;
            gridControl.RepositoryItems.Add(backgroundRepo);
            colBackground.Visible = false;
            gridView.Columns.Add(colBackground);

            // FontStyle (checked combobox)
            var colFontStyle = new GridColumn { FieldName = "FontStyle", Caption = "Font Style" };
            var fontStyleRepo = new RepositoryItemCheckedComboBoxEdit();
            fontStyleRepo.Items.Add(MonacoFontStyle.Bold, "Bold");
            fontStyleRepo.Items.Add(MonacoFontStyle.Italic, "Italic");
            fontStyleRepo.Items.Add(MonacoFontStyle.Underline, "Underline");
            colFontStyle.ColumnEdit = fontStyleRepo;
            gridControl.RepositoryItems.Add(fontStyleRepo);
            colFontStyle.Visible = true;
            colFontStyle.VisibleIndex = 3;
            gridView.Columns.Add(colFontStyle);

            gridView.InitNewRow += (s, e) => {
                gridView.SetRowCellValue(e.RowHandle, "Token", "new-token");
            };
        }

        private void ChkRawMode_CheckedChanged(object? sender, EventArgs e) {
            bool isRaw = chkRawMode.Checked;
            if (isRaw) {
                // Switch to raw JS mode: serialize grid rules to text
                var rules = GetRules();
                rawEditor.Text = MonacoRulesParser.Serialize(rules);
                gridPanel.Visible = false;
                rawPanel.Visible = true;
            } else {
                // Switch to grid mode: parse raw JS back to rules
                if (MonacoRulesParser.TryParse(rawEditor.Text ?? string.Empty, out var parsed)) {
                    ruleItems.Clear();
                    foreach (var r in parsed) {
                        ruleItems.Add(new RuleItem {
                            Token = r.Token,
                            Foreground = r.Foreground,
                            Background = r.Background,
                            FontStyle = r.FontStyle
                        });
                    }
                } else {
                    XtraMessageBox.Show("Failed to parse rules. Please check the format.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    chkRawMode.Checked = true;
                    return;
                }
                rawPanel.Visible = false;
                gridPanel.Visible = true;
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e) {
            // If in raw mode, apply raw JS first
            if (chkRawMode.Checked) {
                if (!MonacoRulesParser.TryParse(rawEditor.Text ?? string.Empty, out var parsed)) {
                    XtraMessageBox.Show("Failed to parse rules. Please check the format.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ruleItems.Clear();
                foreach (var r in parsed) {
                    ruleItems.Add(new RuleItem {
                        Token = r.Token,
                        Foreground = r.Foreground,
                        Background = r.Background,
                        FontStyle = r.FontStyle
                    });
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                rawEditorThemeBehavior?.Dispose();
                rawEditor?.Dispose();
            }
            base.Dispose(disposing);
        }

        // Bindable data class for grid
        public class RuleItem {
            public string Token { get; set; } = string.Empty;
            public Color? Foreground { get; set; }
            public Color? Background { get; set; }
            public MonacoFontStyle? FontStyle { get; set; }
        }
    }
}
