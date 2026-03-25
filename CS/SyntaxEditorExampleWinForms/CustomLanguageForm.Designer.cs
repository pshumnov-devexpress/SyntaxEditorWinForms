namespace SyntaxEditorExampleWinForms {
    partial class CustomLanguageForm {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing) {
                monarchThemeBehavior?.Dispose();
                configThemeBehavior?.Dispose();
                monarchEditor?.Dispose();
                configEditor?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();
            // 
            // CustomLanguageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomLanguageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Register Custom Language";
            this.ResumeLayout(false);
        }

        #endregion
    }
}
