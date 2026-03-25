namespace SyntaxEditorExampleWinForms {
    partial class Form1 {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing) {
                themeBehavior?.Dispose();
                syntaxEditor?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.skinBarSubItem = new DevExpress.XtraBars.BarSubItem();
            this.barApplyThemeColors = new DevExpress.XtraBars.BarCheckItem();
            this.barOpenFile = new DevExpress.XtraBars.BarButtonItem();
            this.barSaveFile = new DevExpress.XtraBars.BarButtonItem();
            this.barRegisterLanguage = new DevExpress.XtraBars.BarButtonItem();
            this.barChangeRules = new DevExpress.XtraBars.BarButtonItem();
            this.barStatusModified = new DevExpress.XtraBars.BarStaticItem();
            this.ribbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.grpTheme = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grpFile = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grpLanguage = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.splitContainer = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.skinBarSubItem,
            this.barApplyThemeColors,
            this.barOpenFile,
            this.barSaveFile,
            this.barRegisterLanguage,
            this.barChangeRules,
            this.barStatusModified});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage});
            this.ribbon.Size = new System.Drawing.Size(1500, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // skinBarSubItem
            // 
            this.skinBarSubItem.Caption = "Select Theme";
            this.skinBarSubItem.Id = 1;
            this.skinBarSubItem.Name = "skinBarSubItem";
            // 
            // barApplyThemeColors
            // 
            this.barApplyThemeColors.Caption = "Apply Theme Colors";
            this.barApplyThemeColors.Checked = true;
            this.barApplyThemeColors.Id = 2;
            this.barApplyThemeColors.Name = "barApplyThemeColors";
            this.barApplyThemeColors.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.BarApplyThemeColors_CheckedChanged);
            // 
            // barOpenFile
            // 
            this.barOpenFile.Caption = "Open File";
            this.barOpenFile.Id = 3;
            this.barOpenFile.Name = "barOpenFile";
            this.barOpenFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarOpenFile_ItemClick);
            // 
            // barSaveFile
            // 
            this.barSaveFile.Caption = "Save File";
            this.barSaveFile.Id = 4;
            this.barSaveFile.Name = "barSaveFile";
            this.barSaveFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarSaveFile_ItemClick);
            // 
            // barRegisterLanguage
            // 
            this.barRegisterLanguage.Caption = "Register Custom Language";
            this.barRegisterLanguage.Id = 5;
            this.barRegisterLanguage.Name = "barRegisterLanguage";
            this.barRegisterLanguage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarRegisterLanguage_ItemClick);
            // 
            // barChangeRules
            // 
            this.barChangeRules.Caption = "Change Rules";
            this.barChangeRules.Id = 6;
            this.barChangeRules.Name = "barChangeRules";
            this.barChangeRules.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarChangeRules_ItemClick);
            // 
            // barStatusModified
            // 
            this.barStatusModified.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barStatusModified.Caption = "Has Changes: False";
            this.barStatusModified.Id = 7;
            this.barStatusModified.Name = "barStatusModified";
            // 
            // ribbonPage
            // 
            this.ribbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.grpTheme,
            this.grpFile,
            this.grpLanguage});
            this.ribbonPage.Name = "ribbonPage";
            this.ribbonPage.Text = "Main";
            // 
            // grpTheme
            // 
            this.grpTheme.ItemLinks.Add(this.skinBarSubItem);
            this.grpTheme.ItemLinks.Add(this.barApplyThemeColors);
            this.grpTheme.Name = "grpTheme";
            this.grpTheme.Text = "Theme";
            // 
            // grpFile
            // 
            this.grpFile.ItemLinks.Add(this.barOpenFile);
            this.grpFile.ItemLinks.Add(this.barSaveFile);
            this.grpFile.Name = "grpFile";
            this.grpFile.Text = "File";
            // 
            // grpLanguage
            // 
            this.grpLanguage.ItemLinks.Add(this.barRegisterLanguage);
            this.grpLanguage.ItemLinks.Add(this.barChangeRules);
            this.grpLanguage.Name = "grpLanguage";
            this.grpLanguage.Text = "Language";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.barStatusModified);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 821);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1500, 29);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(0, 158);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Panel2.MinSize = 300;
            this.splitContainer.Size = new System.Drawing.Size(1500, 663);
            this.splitContainer.SplitterPosition = 1100;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1500, 850);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "Form1";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Syntax Editor Example";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.BarSubItem skinBarSubItem;
        private DevExpress.XtraBars.BarCheckItem barApplyThemeColors;
        private DevExpress.XtraBars.BarButtonItem barOpenFile;
        private DevExpress.XtraBars.BarButtonItem barSaveFile;
        private DevExpress.XtraBars.BarButtonItem barRegisterLanguage;
        private DevExpress.XtraBars.BarButtonItem barChangeRules;
        private DevExpress.XtraBars.BarStaticItem barStatusModified;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grpTheme;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grpFile;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grpLanguage;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraEditors.SplitContainerControl splitContainer;
    }
}

