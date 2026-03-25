namespace SyntaxEditorExampleWinForms {
    partial class RulesForm {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing) {
                rawEditorThemeBehavior?.Dispose();
                rawEditor?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();
            // 
            // RulesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RulesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Rules";
            this.ResumeLayout(false);
        }

        #endregion
    }
}
