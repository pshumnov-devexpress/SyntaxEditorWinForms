using System;
using System.Windows.Forms;
using DevExpress.LookAndFeel;

namespace SyntaxEditorExampleWinForms {
    internal static class Program {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            UserLookAndFeel.Default.SetSkinStyle("WXI");
            Application.Run(new Form1());
        }
    }
}
