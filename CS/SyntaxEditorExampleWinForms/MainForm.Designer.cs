namespace SyntaxEditorExampleWinForms {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing) {
                components?.Dispose();
                themeBehavior?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            skinDropDownButtonItem1 = new DevExpress.XtraBars.SkinDropDownButtonItem();
            skinPaletteDropDownButtonItem1 = new DevExpress.XtraBars.SkinPaletteDropDownButtonItem();
            openItem = new DevExpress.XtraBars.BarButtonItem();
            saveItem = new DevExpress.XtraBars.BarButtonItem();
            customLanguageItem = new DevExpress.XtraBars.BarButtonItem();
            rulesItem = new DevExpress.XtraBars.BarButtonItem();
            applySkinColorsCheckItem = new DevExpress.XtraBars.BarCheckItem();
            ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            syntaxEditor = new SyntaxEditorWinForms.SyntaxEditor();
            sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
            optionsTabPage = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            layoutControl = new DevExpress.XtraLayout.LayoutControl();
            ceDragAndDrop = new DevExpress.XtraEditors.CheckEdit();
            ceContextMenu = new DevExpress.XtraEditors.CheckEdit();
            cbeLanguage = new DevExpress.XtraEditors.ComboBoxEdit();
            ceReadOnly = new DevExpress.XtraEditors.CheckEdit();
            ceLineNumbers = new DevExpress.XtraEditors.CheckEdit();
            seLineNumbersMinChars = new DevExpress.XtraEditors.SpinEdit();
            ceMinimap = new DevExpress.XtraEditors.CheckEdit();
            ceGlyphMargin = new DevExpress.XtraEditors.CheckEdit();
            ceFolding = new DevExpress.XtraEditors.CheckEdit();
            ceStickyScroll = new DevExpress.XtraEditors.CheckEdit();
            cbeWordWrap = new DevExpress.XtraEditors.ComboBoxEdit();
            ceSmoothScrolling = new DevExpress.XtraEditors.CheckEdit();
            ceScrollBeyondLastLine = new DevExpress.XtraEditors.CheckEdit();
            seScrollBeyondLastColumn = new DevExpress.XtraEditors.SpinEdit();
            ceMouseWheelZoom = new DevExpress.XtraEditors.CheckEdit();
            seTabSize = new DevExpress.XtraEditors.SpinEdit();
            ceInsertSpaces = new DevExpress.XtraEditors.CheckEdit();
            ceDetectIndentation = new DevExpress.XtraEditors.CheckEdit();
            cbeAutoIndent = new DevExpress.XtraEditors.ComboBoxEdit();
            ceQuickSuggestions = new DevExpress.XtraEditors.CheckEdit();
            ceWordBasedSuggestions = new DevExpress.XtraEditors.CheckEdit();
            ceSuggestOnTriggerCharacters = new DevExpress.XtraEditors.CheckEdit();
            ceEnableParameterHints = new DevExpress.XtraEditors.CheckEdit();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            lcgGeneral = new DevExpress.XtraLayout.LayoutControlGroup();
            lciLanguage = new DevExpress.XtraLayout.LayoutControlItem();
            lciReadOnly = new DevExpress.XtraLayout.LayoutControlItem();
            lcgInteraction = new DevExpress.XtraLayout.LayoutControlGroup();
            lciContextMenu = new DevExpress.XtraLayout.LayoutControlItem();
            lciDragAndDrop = new DevExpress.XtraLayout.LayoutControlItem();
            lcgAppearance = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            lciLineNumbersMinChars = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            lciWordWrap = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            lciScrollBeyondLastColumn = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            lcgEditing = new DevExpress.XtraLayout.LayoutControlGroup();
            lciTabSize = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            lciAutoIndent = new DevExpress.XtraLayout.LayoutControlItem();
            lcgIntelliSense = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).BeginInit();
            sidePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tabPane1).BeginInit();
            tabPane1.SuspendLayout();
            optionsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layoutControl).BeginInit();
            layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ceDragAndDrop.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ceContextMenu.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cbeLanguage.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ceReadOnly.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ceLineNumbers.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)seLineNumbersMinChars.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ceMinimap.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ceGlyphMargin.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ceFolding.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ceStickyScroll.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cbeWordWrap.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ceSmoothScrolling.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ceScrollBeyondLastLine.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)seScrollBeyondLastColumn.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ceMouseWheelZoom.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)seTabSize.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ceInsertSpaces.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ceDetectIndentation.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cbeAutoIndent.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ceQuickSuggestions.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ceWordBasedSuggestions.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ceSuggestOnTriggerCharacters.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ceEnableParameterHints.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lcgGeneral).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lciLanguage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lciReadOnly).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lcgInteraction).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lciContextMenu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lciDragAndDrop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lcgAppearance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lciLineNumbersMinChars).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lciWordWrap).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lciScrollBeyondLastColumn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lcgEditing).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lciTabSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lciAutoIndent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lcgIntelliSense).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem14).BeginInit();
            SuspendLayout();
            // 
            // ribbonControl1
            // 
            ribbonControl1.ExpandCollapseItem.Id = 0;
            ribbonControl1.ItemPanelStyle = DevExpress.XtraBars.Ribbon.RibbonItemPanelStyle.Classic;
            ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl1.ExpandCollapseItem, skinDropDownButtonItem1, skinPaletteDropDownButtonItem1, openItem, saveItem, customLanguageItem, rulesItem, applySkinColorsCheckItem });
            ribbonControl1.Location = new System.Drawing.Point(0, 0);
            ribbonControl1.MaxItemId = 9;
            ribbonControl1.Name = "ribbonControl1";
            ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1 });
            ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.OfficeUniversal;
            ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            ribbonControl1.Size = new System.Drawing.Size(2311, 96);
            // 
            // skinDropDownButtonItem1
            // 
            skinDropDownButtonItem1.Id = 1;
            skinDropDownButtonItem1.Name = "skinDropDownButtonItem1";
            // 
            // skinPaletteDropDownButtonItem1
            // 
            skinPaletteDropDownButtonItem1.ActAsDropDown = true;
            skinPaletteDropDownButtonItem1.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            skinPaletteDropDownButtonItem1.Id = 2;
            skinPaletteDropDownButtonItem1.Name = "skinPaletteDropDownButtonItem1";
            // 
            // openItem
            // 
            openItem.Caption = "Open File";
            openItem.Id = 3;
            openItem.ImageOptions.ImageUri.Uri = "Open";
            openItem.Name = "openItem";
            openItem.ItemClick += openItem_ItemClick;
            // 
            // saveItem
            // 
            saveItem.Caption = "Save File";
            saveItem.Id = 4;
            saveItem.ImageOptions.ImageUri.Uri = "Save";
            saveItem.Name = "saveItem";
            saveItem.ItemClick += saveItem_ItemClick;
            // 
            // customLanguageItem
            // 
            customLanguageItem.Caption = "Register Custom Language";
            customLanguageItem.Id = 5;
            customLanguageItem.ImageOptions.ImageUri.Uri = "richedit/showallfieldcodes";
            customLanguageItem.Name = "customLanguageItem";
            customLanguageItem.ItemClick += customLanguageItem_ItemClick;
            // 
            // rulesItem
            // 
            rulesItem.Caption = "Change Rules";
            rulesItem.Id = 6;
            rulesItem.ImageOptions.ImageUri.Uri = "dashboards/editquery";
            rulesItem.Name = "rulesItem";
            rulesItem.ItemClick += rulesItem_ItemClick;
            // 
            // applySkinColorsCheckItem
            // 
            applySkinColorsCheckItem.Caption = "Apply Skin Colors";
            applySkinColorsCheckItem.Id = 8;
            applySkinColorsCheckItem.ImageOptions.ImageUri.Uri = "dashboards/editcolors";
            applySkinColorsCheckItem.Name = "applySkinColorsCheckItem";
            applySkinColorsCheckItem.CheckedChanged += applySkinColorsCheckItem_CheckedChanged;
            // 
            // ribbonPage1
            // 
            ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup2, ribbonPageGroup3, ribbonPageGroup1 });
            ribbonPage1.Name = "ribbonPage1";
            ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup2
            // 
            ribbonPageGroup2.ItemLinks.Add(openItem);
            ribbonPageGroup2.ItemLinks.Add(saveItem);
            ribbonPageGroup2.Name = "ribbonPageGroup2";
            ribbonPageGroup2.Text = "ribbonPageGroup2";
            // 
            // ribbonPageGroup3
            // 
            ribbonPageGroup3.ItemLinks.Add(customLanguageItem);
            ribbonPageGroup3.ItemLinks.Add(rulesItem);
            ribbonPageGroup3.Name = "ribbonPageGroup3";
            ribbonPageGroup3.Text = "ribbonPageGroup3";
            // 
            // ribbonPageGroup1
            // 
            ribbonPageGroup1.ItemLinks.Add(skinDropDownButtonItem1);
            ribbonPageGroup1.ItemLinks.Add(skinPaletteDropDownButtonItem1);
            ribbonPageGroup1.ItemLinks.Add(applySkinColorsCheckItem);
            ribbonPageGroup1.Name = "ribbonPageGroup1";
            ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // syntaxEditor
            // 
            syntaxEditor.AutoIndent = SyntaxEditorWinForms.Models.EditorAutoIndent.Full;
            syntaxEditor.DetectIndentation = true;
            syntaxEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            syntaxEditor.EditorLanguage = "csharp";
            syntaxEditor.EnableContextMenu = true;
            syntaxEditor.EnableDragAndDrop = true;
            syntaxEditor.EnableFolding = true;
            syntaxEditor.EnableMouseWheelZoom = false;
            syntaxEditor.EnableParameterHints = true;
            syntaxEditor.EnableQuickSuggestions = true;
            syntaxEditor.EnableScrollBeyondLastLine = true;
            syntaxEditor.EnableSmoothScrolling = false;
            syntaxEditor.EnableStickyScroll = true;
            syntaxEditor.EnableSuggestOnTriggerCharacters = true;
            syntaxEditor.EnableWordBasedSuggestions = true;
            syntaxEditor.InsertSpaces = true;
            syntaxEditor.LineNumbersMinChars = 5;
            syntaxEditor.Location = new System.Drawing.Point(0, 96);
            syntaxEditor.Name = "syntaxEditor";
            syntaxEditor.ReadOnly = false;
            syntaxEditor.ScrollBeyondLastColumn = 5;
            syntaxEditor.ShowGlyphMargin = false;
            syntaxEditor.ShowLineNumbers = true;
            syntaxEditor.ShowMinimap = false;
            syntaxEditor.Size = new System.Drawing.Size(1797, 1382);
            syntaxEditor.TabIndex = 1;
            syntaxEditor.TabSize = 4;
            syntaxEditor.ThemeName = "vs";
            syntaxEditor.WordWrap = SyntaxEditorWinForms.Models.EditorWordWrap.Off;
            // 
            // sidePanel1
            // 
            sidePanel1.Controls.Add(tabPane1);
            sidePanel1.Dock = System.Windows.Forms.DockStyle.Right;
            sidePanel1.Location = new System.Drawing.Point(1797, 96);
            sidePanel1.Name = "sidePanel1";
            sidePanel1.Size = new System.Drawing.Size(514, 1382);
            sidePanel1.TabIndex = 2;
            sidePanel1.Text = "sidePanel1";
            // 
            // tabPane1
            // 
            tabPane1.Controls.Add(optionsTabPage);
            tabPane1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabPane1.Location = new System.Drawing.Point(2, 0);
            tabPane1.Name = "tabPane1";
            tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] { optionsTabPage });
            tabPane1.RegularSize = new System.Drawing.Size(512, 1382);
            tabPane1.SelectedPage = optionsTabPage;
            tabPane1.Size = new System.Drawing.Size(512, 1382);
            tabPane1.TabIndex = 0;
            tabPane1.Text = "tabPane1";
            // 
            // optionsTabPage
            // 
            optionsTabPage.Caption = "Options";
            optionsTabPage.Controls.Add(layoutControl);
            optionsTabPage.Name = "optionsTabPage";
            optionsTabPage.Size = new System.Drawing.Size(512, 1324);
            // 
            // layoutControl
            // 
            layoutControl.Controls.Add(ceDragAndDrop);
            layoutControl.Controls.Add(ceContextMenu);
            layoutControl.Controls.Add(cbeLanguage);
            layoutControl.Controls.Add(ceReadOnly);
            layoutControl.Controls.Add(ceLineNumbers);
            layoutControl.Controls.Add(seLineNumbersMinChars);
            layoutControl.Controls.Add(ceMinimap);
            layoutControl.Controls.Add(ceGlyphMargin);
            layoutControl.Controls.Add(ceFolding);
            layoutControl.Controls.Add(ceStickyScroll);
            layoutControl.Controls.Add(cbeWordWrap);
            layoutControl.Controls.Add(ceSmoothScrolling);
            layoutControl.Controls.Add(ceScrollBeyondLastLine);
            layoutControl.Controls.Add(seScrollBeyondLastColumn);
            layoutControl.Controls.Add(ceMouseWheelZoom);
            layoutControl.Controls.Add(seTabSize);
            layoutControl.Controls.Add(ceInsertSpaces);
            layoutControl.Controls.Add(ceDetectIndentation);
            layoutControl.Controls.Add(cbeAutoIndent);
            layoutControl.Controls.Add(ceQuickSuggestions);
            layoutControl.Controls.Add(ceWordBasedSuggestions);
            layoutControl.Controls.Add(ceSuggestOnTriggerCharacters);
            layoutControl.Controls.Add(ceEnableParameterHints);
            layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            layoutControl.Location = new System.Drawing.Point(0, 0);
            layoutControl.Name = "layoutControl";
            layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1544, 605, 1137, 700);
            layoutControl.OptionsView.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            layoutControl.Root = Root;
            layoutControl.Size = new System.Drawing.Size(512, 1324);
            layoutControl.TabIndex = 0;
            layoutControl.Text = "layoutControl1";
            // 
            // ceDragAndDrop
            // 
            ceDragAndDrop.Location = new System.Drawing.Point(23, 265);
            ceDragAndDrop.MenuManager = ribbonControl1;
            ceDragAndDrop.Name = "ceDragAndDrop";
            ceDragAndDrop.Properties.Caption = "Drag and Drop";
            ceDragAndDrop.Size = new System.Drawing.Size(436, 35);
            ceDragAndDrop.StyleController = layoutControl;
            ceDragAndDrop.TabIndex = 7;
            ceDragAndDrop.CheckedChanged += ceDragAndDrop_CheckedChanged;
            // 
            // ceContextMenu
            // 
            ceContextMenu.Location = new System.Drawing.Point(23, 224);
            ceContextMenu.MenuManager = ribbonControl1;
            ceContextMenu.Name = "ceContextMenu";
            ceContextMenu.Properties.Caption = "Context Menu";
            ceContextMenu.Size = new System.Drawing.Size(436, 35);
            ceContextMenu.StyleController = layoutControl;
            ceContextMenu.TabIndex = 6;
            ceContextMenu.CheckedChanged += ceContextMenu_CheckedChanged;
            // 
            // cbeLanguage
            // 
            cbeLanguage.Location = new System.Drawing.Point(265, 102);
            cbeLanguage.MenuManager = ribbonControl1;
            cbeLanguage.Name = "cbeLanguage";
            cbeLanguage.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cbeLanguage.Size = new System.Drawing.Size(194, 38);
            cbeLanguage.StyleController = layoutControl;
            cbeLanguage.TabIndex = 5;
            cbeLanguage.EditValueChanged += cbeLanguage_EditValueChanged;
            // 
            // ceReadOnly
            // 
            ceReadOnly.Location = new System.Drawing.Point(23, 61);
            ceReadOnly.MenuManager = ribbonControl1;
            ceReadOnly.Name = "ceReadOnly";
            ceReadOnly.Properties.Caption = "Read Only";
            ceReadOnly.Size = new System.Drawing.Size(436, 35);
            ceReadOnly.StyleController = layoutControl;
            ceReadOnly.TabIndex = 4;
            ceReadOnly.CheckedChanged += ceReadOnly_CheckedChanged;
            // 
            // ceLineNumbers
            // 
            ceLineNumbers.Location = new System.Drawing.Point(23, 384);
            ceLineNumbers.MenuManager = ribbonControl1;
            ceLineNumbers.Name = "ceLineNumbers";
            ceLineNumbers.Properties.Caption = "Line Numbers";
            ceLineNumbers.Size = new System.Drawing.Size(436, 35);
            ceLineNumbers.StyleController = layoutControl;
            ceLineNumbers.TabIndex = 8;
            ceLineNumbers.CheckedChanged += ceLineNumbers_CheckedChanged;
            // 
            // seLineNumbersMinChars
            // 
            seLineNumbersMinChars.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            seLineNumbersMinChars.Location = new System.Drawing.Point(265, 425);
            seLineNumbersMinChars.MenuManager = ribbonControl1;
            seLineNumbersMinChars.Name = "seLineNumbersMinChars";
            seLineNumbersMinChars.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            seLineNumbersMinChars.Size = new System.Drawing.Size(194, 38);
            seLineNumbersMinChars.StyleController = layoutControl;
            seLineNumbersMinChars.TabIndex = 9;
            seLineNumbersMinChars.EditValueChanged += seLineNumbersMinChars_EditValueChanged;
            // 
            // ceMinimap
            // 
            ceMinimap.Location = new System.Drawing.Point(23, 469);
            ceMinimap.MenuManager = ribbonControl1;
            ceMinimap.Name = "ceMinimap";
            ceMinimap.Properties.Caption = "Minimap";
            ceMinimap.Size = new System.Drawing.Size(436, 35);
            ceMinimap.StyleController = layoutControl;
            ceMinimap.TabIndex = 10;
            ceMinimap.CheckedChanged += ceMinimap_CheckedChanged;
            // 
            // ceGlyphMargin
            // 
            ceGlyphMargin.Location = new System.Drawing.Point(23, 510);
            ceGlyphMargin.MenuManager = ribbonControl1;
            ceGlyphMargin.Name = "ceGlyphMargin";
            ceGlyphMargin.Properties.Caption = "Glyph Margin";
            ceGlyphMargin.Size = new System.Drawing.Size(436, 35);
            ceGlyphMargin.StyleController = layoutControl;
            ceGlyphMargin.TabIndex = 11;
            ceGlyphMargin.CheckedChanged += ceGlyphMargin_CheckedChanged;
            // 
            // ceFolding
            // 
            ceFolding.Location = new System.Drawing.Point(23, 551);
            ceFolding.MenuManager = ribbonControl1;
            ceFolding.Name = "ceFolding";
            ceFolding.Properties.Caption = "Folding";
            ceFolding.Size = new System.Drawing.Size(436, 35);
            ceFolding.StyleController = layoutControl;
            ceFolding.TabIndex = 12;
            ceFolding.CheckedChanged += ceFolding_CheckedChanged;
            // 
            // ceStickyScroll
            // 
            ceStickyScroll.Location = new System.Drawing.Point(23, 592);
            ceStickyScroll.MenuManager = ribbonControl1;
            ceStickyScroll.Name = "ceStickyScroll";
            ceStickyScroll.Properties.Caption = "Sticky Scroll";
            ceStickyScroll.Size = new System.Drawing.Size(436, 35);
            ceStickyScroll.StyleController = layoutControl;
            ceStickyScroll.TabIndex = 13;
            ceStickyScroll.CheckedChanged += ceStickyScroll_CheckedChanged;
            // 
            // cbeWordWrap
            // 
            cbeWordWrap.Location = new System.Drawing.Point(265, 633);
            cbeWordWrap.MenuManager = ribbonControl1;
            cbeWordWrap.Name = "cbeWordWrap";
            cbeWordWrap.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cbeWordWrap.Size = new System.Drawing.Size(194, 38);
            cbeWordWrap.StyleController = layoutControl;
            cbeWordWrap.TabIndex = 14;
            cbeWordWrap.EditValueChanged += cbeWordWrap_EditValueChanged;
            // 
            // ceSmoothScrolling
            // 
            ceSmoothScrolling.Location = new System.Drawing.Point(23, 677);
            ceSmoothScrolling.MenuManager = ribbonControl1;
            ceSmoothScrolling.Name = "ceSmoothScrolling";
            ceSmoothScrolling.Properties.Caption = "Smooth Scrolling";
            ceSmoothScrolling.Size = new System.Drawing.Size(436, 35);
            ceSmoothScrolling.StyleController = layoutControl;
            ceSmoothScrolling.TabIndex = 15;
            ceSmoothScrolling.CheckedChanged += ceSmoothScrolling_CheckedChanged;
            // 
            // ceScrollBeyondLastLine
            // 
            ceScrollBeyondLastLine.Location = new System.Drawing.Point(23, 718);
            ceScrollBeyondLastLine.MenuManager = ribbonControl1;
            ceScrollBeyondLastLine.Name = "ceScrollBeyondLastLine";
            ceScrollBeyondLastLine.Properties.Caption = "Scroll Beyond Last Line";
            ceScrollBeyondLastLine.Size = new System.Drawing.Size(436, 35);
            ceScrollBeyondLastLine.StyleController = layoutControl;
            ceScrollBeyondLastLine.TabIndex = 16;
            ceScrollBeyondLastLine.CheckedChanged += ceScrollBeyondLastLine_CheckedChanged;
            // 
            // seScrollBeyondLastColumn
            // 
            seScrollBeyondLastColumn.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            seScrollBeyondLastColumn.Location = new System.Drawing.Point(265, 759);
            seScrollBeyondLastColumn.MenuManager = ribbonControl1;
            seScrollBeyondLastColumn.Name = "seScrollBeyondLastColumn";
            seScrollBeyondLastColumn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            seScrollBeyondLastColumn.Size = new System.Drawing.Size(194, 38);
            seScrollBeyondLastColumn.StyleController = layoutControl;
            seScrollBeyondLastColumn.TabIndex = 17;
            seScrollBeyondLastColumn.EditValueChanged += seScrollBeyondLastColumn_EditValueChanged;
            // 
            // ceMouseWheelZoom
            // 
            ceMouseWheelZoom.Location = new System.Drawing.Point(23, 803);
            ceMouseWheelZoom.MenuManager = ribbonControl1;
            ceMouseWheelZoom.Name = "ceMouseWheelZoom";
            ceMouseWheelZoom.Properties.Caption = "Mouse Wheel Zoom";
            ceMouseWheelZoom.Size = new System.Drawing.Size(436, 35);
            ceMouseWheelZoom.StyleController = layoutControl;
            ceMouseWheelZoom.TabIndex = 18;
            ceMouseWheelZoom.CheckedChanged += ceMouseWheelZoom_CheckedChanged;
            // 
            // seTabSize
            // 
            seTabSize.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            seTabSize.Location = new System.Drawing.Point(265, 922);
            seTabSize.MenuManager = ribbonControl1;
            seTabSize.Name = "seTabSize";
            seTabSize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            seTabSize.Size = new System.Drawing.Size(194, 38);
            seTabSize.StyleController = layoutControl;
            seTabSize.TabIndex = 19;
            seTabSize.EditValueChanged += seTabSize_EditValueChanged;
            // 
            // ceInsertSpaces
            // 
            ceInsertSpaces.Location = new System.Drawing.Point(23, 966);
            ceInsertSpaces.MenuManager = ribbonControl1;
            ceInsertSpaces.Name = "ceInsertSpaces";
            ceInsertSpaces.Properties.Caption = "Insert Spaces";
            ceInsertSpaces.Size = new System.Drawing.Size(436, 35);
            ceInsertSpaces.StyleController = layoutControl;
            ceInsertSpaces.TabIndex = 20;
            ceInsertSpaces.CheckedChanged += ceInsertSpaces_CheckedChanged;
            // 
            // ceDetectIndentation
            // 
            ceDetectIndentation.Location = new System.Drawing.Point(23, 1007);
            ceDetectIndentation.MenuManager = ribbonControl1;
            ceDetectIndentation.Name = "ceDetectIndentation";
            ceDetectIndentation.Properties.Caption = "Detect Indentation";
            ceDetectIndentation.Size = new System.Drawing.Size(436, 35);
            ceDetectIndentation.StyleController = layoutControl;
            ceDetectIndentation.TabIndex = 21;
            ceDetectIndentation.CheckedChanged += ceDetectIndentation_CheckedChanged;
            // 
            // cbeAutoIndent
            // 
            cbeAutoIndent.Location = new System.Drawing.Point(265, 1048);
            cbeAutoIndent.MenuManager = ribbonControl1;
            cbeAutoIndent.Name = "cbeAutoIndent";
            cbeAutoIndent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cbeAutoIndent.Size = new System.Drawing.Size(194, 38);
            cbeAutoIndent.StyleController = layoutControl;
            cbeAutoIndent.TabIndex = 22;
            cbeAutoIndent.EditValueChanged += cbeAutoIndent_EditValueChanged;
            // 
            // ceQuickSuggestions
            // 
            ceQuickSuggestions.Location = new System.Drawing.Point(23, 1170);
            ceQuickSuggestions.MenuManager = ribbonControl1;
            ceQuickSuggestions.Name = "ceQuickSuggestions";
            ceQuickSuggestions.Properties.Caption = "Quick Suggestions";
            ceQuickSuggestions.Size = new System.Drawing.Size(436, 35);
            ceQuickSuggestions.StyleController = layoutControl;
            ceQuickSuggestions.TabIndex = 23;
            ceQuickSuggestions.CheckedChanged += ceQuickSuggestions_CheckedChanged;
            // 
            // ceWordBasedSuggestions
            // 
            ceWordBasedSuggestions.Location = new System.Drawing.Point(23, 1211);
            ceWordBasedSuggestions.MenuManager = ribbonControl1;
            ceWordBasedSuggestions.Name = "ceWordBasedSuggestions";
            ceWordBasedSuggestions.Properties.Caption = "Word Based Suggestions";
            ceWordBasedSuggestions.Size = new System.Drawing.Size(436, 35);
            ceWordBasedSuggestions.StyleController = layoutControl;
            ceWordBasedSuggestions.TabIndex = 24;
            ceWordBasedSuggestions.CheckedChanged += ceWordBasedSuggestions_CheckedChanged;
            // 
            // ceSuggestOnTriggerCharacters
            // 
            ceSuggestOnTriggerCharacters.Location = new System.Drawing.Point(23, 1252);
            ceSuggestOnTriggerCharacters.MenuManager = ribbonControl1;
            ceSuggestOnTriggerCharacters.Name = "ceSuggestOnTriggerCharacters";
            ceSuggestOnTriggerCharacters.Properties.Caption = "Suggest On Trigger Characters";
            ceSuggestOnTriggerCharacters.Size = new System.Drawing.Size(436, 35);
            ceSuggestOnTriggerCharacters.StyleController = layoutControl;
            ceSuggestOnTriggerCharacters.TabIndex = 25;
            ceSuggestOnTriggerCharacters.CheckedChanged += ceSuggestOnTriggerCharacters_CheckedChanged;
            // 
            // ceEnableParameterHints
            // 
            ceEnableParameterHints.Location = new System.Drawing.Point(23, 1293);
            ceEnableParameterHints.MenuManager = ribbonControl1;
            ceEnableParameterHints.Name = "ceEnableParameterHints";
            ceEnableParameterHints.Properties.Caption = "Enable Parameter Hints";
            ceEnableParameterHints.Size = new System.Drawing.Size(436, 35);
            ceEnableParameterHints.StyleController = layoutControl;
            ceEnableParameterHints.TabIndex = 26;
            ceEnableParameterHints.CheckedChanged += ceEnableParameterHints_CheckedChanged;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { emptySpaceItem1, lcgGeneral, lcgInteraction, lcgAppearance, lcgEditing, lcgIntelliSense });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(482, 1361);
            Root.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.Location = new System.Drawing.Point(0, 1351);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(482, 10);
            // 
            // lcgGeneral
            // 
            lcgGeneral.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { lciLanguage, lciReadOnly });
            lcgGeneral.Location = new System.Drawing.Point(0, 0);
            lcgGeneral.Name = "lcgGeneral";
            lcgGeneral.Size = new System.Drawing.Size(482, 163);
            lcgGeneral.Text = "General";
            // 
            // lciLanguage
            // 
            lciLanguage.Control = cbeLanguage;
            lciLanguage.Location = new System.Drawing.Point(0, 41);
            lciLanguage.Name = "lciLanguage";
            lciLanguage.Size = new System.Drawing.Size(442, 44);
            lciLanguage.Text = "Language";
            lciLanguage.TextSize = new System.Drawing.Size(221, 23);
            // 
            // lciReadOnly
            // 
            lciReadOnly.Control = ceReadOnly;
            lciReadOnly.Location = new System.Drawing.Point(0, 0);
            lciReadOnly.Name = "lciReadOnly";
            lciReadOnly.Size = new System.Drawing.Size(442, 41);
            lciReadOnly.TextVisible = false;
            // 
            // lcgInteraction
            // 
            lcgInteraction.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { lciContextMenu, lciDragAndDrop });
            lcgInteraction.Location = new System.Drawing.Point(0, 163);
            lcgInteraction.Name = "lcgInteraction";
            lcgInteraction.Size = new System.Drawing.Size(482, 160);
            lcgInteraction.Text = "Interaction";
            // 
            // lciContextMenu
            // 
            lciContextMenu.Control = ceContextMenu;
            lciContextMenu.Location = new System.Drawing.Point(0, 0);
            lciContextMenu.Name = "lciContextMenu";
            lciContextMenu.Size = new System.Drawing.Size(442, 41);
            lciContextMenu.TextVisible = false;
            // 
            // lciDragAndDrop
            // 
            lciDragAndDrop.Control = ceDragAndDrop;
            lciDragAndDrop.Location = new System.Drawing.Point(0, 41);
            lciDragAndDrop.Name = "lciDragAndDrop";
            lciDragAndDrop.Size = new System.Drawing.Size(442, 41);
            lciDragAndDrop.TextVisible = false;
            // 
            // lcgAppearance
            // 
            lcgAppearance.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem3, layoutControlItem1, lciLineNumbersMinChars, layoutControlItem4, layoutControlItem5, layoutControlItem6, lciWordWrap, layoutControlItem8, layoutControlItem9, lciScrollBeyondLastColumn, layoutControlItem11 });
            lcgAppearance.Location = new System.Drawing.Point(0, 323);
            lcgAppearance.Name = "lcgAppearance";
            lcgAppearance.Size = new System.Drawing.Size(482, 538);
            lcgAppearance.Text = "Appearance";
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = ceMinimap;
            layoutControlItem3.Location = new System.Drawing.Point(0, 85);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(442, 41);
            layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = ceLineNumbers;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(442, 41);
            layoutControlItem1.TextVisible = false;
            // 
            // lciLineNumbersMinChars
            // 
            lciLineNumbersMinChars.Control = seLineNumbersMinChars;
            lciLineNumbersMinChars.Location = new System.Drawing.Point(0, 41);
            lciLineNumbersMinChars.Name = "lciLineNumbersMinChars";
            lciLineNumbersMinChars.Size = new System.Drawing.Size(442, 44);
            lciLineNumbersMinChars.Text = "Line Numbers Min Chars";
            lciLineNumbersMinChars.TextSize = new System.Drawing.Size(221, 23);
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = ceGlyphMargin;
            layoutControlItem4.Location = new System.Drawing.Point(0, 126);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new System.Drawing.Size(442, 41);
            layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = ceFolding;
            layoutControlItem5.Location = new System.Drawing.Point(0, 167);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new System.Drawing.Size(442, 41);
            layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            layoutControlItem6.Control = ceStickyScroll;
            layoutControlItem6.Location = new System.Drawing.Point(0, 208);
            layoutControlItem6.Name = "layoutControlItem6";
            layoutControlItem6.Size = new System.Drawing.Size(442, 41);
            layoutControlItem6.TextVisible = false;
            // 
            // lciWordWrap
            // 
            lciWordWrap.Control = cbeWordWrap;
            lciWordWrap.Location = new System.Drawing.Point(0, 249);
            lciWordWrap.Name = "lciWordWrap";
            lciWordWrap.Size = new System.Drawing.Size(442, 44);
            lciWordWrap.Text = "Word Wrap";
            lciWordWrap.TextSize = new System.Drawing.Size(221, 23);
            // 
            // layoutControlItem8
            // 
            layoutControlItem8.Control = ceSmoothScrolling;
            layoutControlItem8.Location = new System.Drawing.Point(0, 293);
            layoutControlItem8.Name = "layoutControlItem8";
            layoutControlItem8.Size = new System.Drawing.Size(442, 41);
            layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            layoutControlItem9.Control = ceScrollBeyondLastLine;
            layoutControlItem9.Location = new System.Drawing.Point(0, 334);
            layoutControlItem9.Name = "layoutControlItem9";
            layoutControlItem9.Size = new System.Drawing.Size(442, 41);
            layoutControlItem9.TextVisible = false;
            // 
            // lciScrollBeyondLastColumn
            // 
            lciScrollBeyondLastColumn.Control = seScrollBeyondLastColumn;
            lciScrollBeyondLastColumn.Location = new System.Drawing.Point(0, 375);
            lciScrollBeyondLastColumn.Name = "lciScrollBeyondLastColumn";
            lciScrollBeyondLastColumn.Size = new System.Drawing.Size(442, 44);
            lciScrollBeyondLastColumn.Text = "Scroll Beyond Last Column";
            lciScrollBeyondLastColumn.TextSize = new System.Drawing.Size(221, 23);
            // 
            // layoutControlItem11
            // 
            layoutControlItem11.Control = ceMouseWheelZoom;
            layoutControlItem11.Location = new System.Drawing.Point(0, 419);
            layoutControlItem11.Name = "layoutControlItem11";
            layoutControlItem11.Size = new System.Drawing.Size(442, 41);
            layoutControlItem11.TextVisible = false;
            // 
            // lcgEditing
            // 
            lcgEditing.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { lciTabSize, layoutControlItem2, layoutControlItem7, lciAutoIndent });
            lcgEditing.Location = new System.Drawing.Point(0, 861);
            lcgEditing.Name = "lcgEditing";
            lcgEditing.Size = new System.Drawing.Size(482, 248);
            lcgEditing.Text = "Editing";
            // 
            // lciTabSize
            // 
            lciTabSize.Control = seTabSize;
            lciTabSize.Location = new System.Drawing.Point(0, 0);
            lciTabSize.Name = "lciTabSize";
            lciTabSize.Size = new System.Drawing.Size(442, 44);
            lciTabSize.Text = "Tab Size";
            lciTabSize.TextSize = new System.Drawing.Size(221, 23);
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = ceInsertSpaces;
            layoutControlItem2.Location = new System.Drawing.Point(0, 44);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(442, 41);
            layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            layoutControlItem7.Control = ceDetectIndentation;
            layoutControlItem7.Location = new System.Drawing.Point(0, 85);
            layoutControlItem7.Name = "layoutControlItem7";
            layoutControlItem7.Size = new System.Drawing.Size(442, 41);
            layoutControlItem7.TextVisible = false;
            // 
            // lciAutoIndent
            // 
            lciAutoIndent.Control = cbeAutoIndent;
            lciAutoIndent.Location = new System.Drawing.Point(0, 126);
            lciAutoIndent.Name = "lciAutoIndent";
            lciAutoIndent.Size = new System.Drawing.Size(442, 44);
            lciAutoIndent.Text = "Auto Indent";
            lciAutoIndent.TextSize = new System.Drawing.Size(221, 23);
            // 
            // lcgIntelliSense
            // 
            lcgIntelliSense.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem10, layoutControlItem12, layoutControlItem13, layoutControlItem14 });
            lcgIntelliSense.Location = new System.Drawing.Point(0, 1109);
            lcgIntelliSense.Name = "lcgIntelliSense";
            lcgIntelliSense.Size = new System.Drawing.Size(482, 242);
            lcgIntelliSense.Text = "Intelli Sense";
            // 
            // layoutControlItem10
            // 
            layoutControlItem10.Control = ceQuickSuggestions;
            layoutControlItem10.Location = new System.Drawing.Point(0, 0);
            layoutControlItem10.Name = "layoutControlItem10";
            layoutControlItem10.Size = new System.Drawing.Size(442, 41);
            layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem12
            // 
            layoutControlItem12.Control = ceWordBasedSuggestions;
            layoutControlItem12.Location = new System.Drawing.Point(0, 41);
            layoutControlItem12.Name = "layoutControlItem12";
            layoutControlItem12.Size = new System.Drawing.Size(442, 41);
            layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            layoutControlItem13.Control = ceSuggestOnTriggerCharacters;
            layoutControlItem13.Location = new System.Drawing.Point(0, 82);
            layoutControlItem13.Name = "layoutControlItem13";
            layoutControlItem13.Size = new System.Drawing.Size(442, 41);
            layoutControlItem13.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            layoutControlItem14.Control = ceEnableParameterHints;
            layoutControlItem14.Location = new System.Drawing.Point(0, 123);
            layoutControlItem14.Name = "layoutControlItem14";
            layoutControlItem14.Size = new System.Drawing.Size(442, 41);
            layoutControlItem14.TextVisible = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(2311, 1478);
            Controls.Add(syntaxEditor);
            Controls.Add(sidePanel1);
            Controls.Add(ribbonControl1);
            Name = "MainForm";
            Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).EndInit();
            sidePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tabPane1).EndInit();
            tabPane1.ResumeLayout(false);
            optionsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)layoutControl).EndInit();
            layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ceDragAndDrop.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ceContextMenu.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)cbeLanguage.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ceReadOnly.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ceLineNumbers.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)seLineNumbersMinChars.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ceMinimap.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ceGlyphMargin.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ceFolding.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ceStickyScroll.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)cbeWordWrap.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ceSmoothScrolling.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ceScrollBeyondLastLine.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)seScrollBeyondLastColumn.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ceMouseWheelZoom.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)seTabSize.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ceInsertSpaces.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ceDetectIndentation.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)cbeAutoIndent.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ceQuickSuggestions.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ceWordBasedSuggestions.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ceSuggestOnTriggerCharacters.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ceEnableParameterHints.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)lcgGeneral).EndInit();
            ((System.ComponentModel.ISupportInitialize)lciLanguage).EndInit();
            ((System.ComponentModel.ISupportInitialize)lciReadOnly).EndInit();
            ((System.ComponentModel.ISupportInitialize)lcgInteraction).EndInit();
            ((System.ComponentModel.ISupportInitialize)lciContextMenu).EndInit();
            ((System.ComponentModel.ISupportInitialize)lciDragAndDrop).EndInit();
            ((System.ComponentModel.ISupportInitialize)lcgAppearance).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)lciLineNumbersMinChars).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).EndInit();
            ((System.ComponentModel.ISupportInitialize)lciWordWrap).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem8).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).EndInit();
            ((System.ComponentModel.ISupportInitialize)lciScrollBeyondLastColumn).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem11).EndInit();
            ((System.ComponentModel.ISupportInitialize)lcgEditing).EndInit();
            ((System.ComponentModel.ISupportInitialize)lciTabSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).EndInit();
            ((System.ComponentModel.ISupportInitialize)lciAutoIndent).EndInit();
            ((System.ComponentModel.ISupportInitialize)lcgIntelliSense).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem10).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem12).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem13).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem14).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.SkinDropDownButtonItem skinDropDownButtonItem1;
        private DevExpress.XtraBars.SkinPaletteDropDownButtonItem skinPaletteDropDownButtonItem1;
        private DevExpress.XtraBars.BarButtonItem openItem;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem saveItem;
        private DevExpress.XtraBars.BarButtonItem customLanguageItem;
        private DevExpress.XtraBars.BarButtonItem rulesItem;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private SyntaxEditorWinForms.SyntaxEditor syntaxEditor;
        private DevExpress.XtraEditors.SidePanel sidePanel1;
        private DevExpress.XtraBars.Navigation.TabPane tabPane1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage optionsTabPage;
        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.CheckEdit ceReadOnly;
        private DevExpress.XtraLayout.LayoutControlItem lciReadOnly;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.CheckEdit ceDragAndDrop;
        private DevExpress.XtraEditors.CheckEdit ceContextMenu;
        private DevExpress.XtraEditors.ComboBoxEdit cbeLanguage;
        private DevExpress.XtraLayout.LayoutControlItem lciContextMenu;
        private DevExpress.XtraLayout.LayoutControlItem lciDragAndDrop;
        private DevExpress.XtraLayout.LayoutControlItem lciLanguage;
        private DevExpress.XtraLayout.LayoutControlGroup lcgGeneral;
        private DevExpress.XtraLayout.LayoutControlGroup lcgInteraction;
        private DevExpress.XtraEditors.CheckEdit ceLineNumbers;
        private DevExpress.XtraEditors.SpinEdit seLineNumbersMinChars;
        private DevExpress.XtraEditors.CheckEdit ceMinimap;
        private DevExpress.XtraEditors.CheckEdit ceGlyphMargin;
        private DevExpress.XtraEditors.CheckEdit ceFolding;
        private DevExpress.XtraEditors.CheckEdit ceStickyScroll;
        private DevExpress.XtraEditors.ComboBoxEdit cbeWordWrap;
        private DevExpress.XtraLayout.LayoutControlGroup lcgAppearance;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem lciLineNumbersMinChars;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem lciWordWrap;
        private DevExpress.XtraEditors.CheckEdit ceSmoothScrolling;
        private DevExpress.XtraEditors.CheckEdit ceScrollBeyondLastLine;
        private DevExpress.XtraEditors.SpinEdit seScrollBeyondLastColumn;
        private DevExpress.XtraEditors.CheckEdit ceMouseWheelZoom;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem lciScrollBeyondLastColumn;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.SpinEdit seTabSize;
        private DevExpress.XtraEditors.CheckEdit ceInsertSpaces;
        private DevExpress.XtraEditors.CheckEdit ceDetectIndentation;
        private DevExpress.XtraLayout.LayoutControlGroup lcgEditing;
        private DevExpress.XtraLayout.LayoutControlItem lciTabSize;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.ComboBoxEdit cbeAutoIndent;
        private DevExpress.XtraLayout.LayoutControlItem lciAutoIndent;
        private DevExpress.XtraEditors.CheckEdit ceQuickSuggestions;
        private DevExpress.XtraEditors.CheckEdit ceWordBasedSuggestions;
        private DevExpress.XtraEditors.CheckEdit ceSuggestOnTriggerCharacters;
        private DevExpress.XtraEditors.CheckEdit ceEnableParameterHints;
        private DevExpress.XtraLayout.LayoutControlGroup lcgIntelliSense;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraBars.BarCheckItem applySkinColorsCheckItem;
    }
}