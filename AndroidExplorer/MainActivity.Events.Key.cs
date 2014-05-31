using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndroidExplorer {
    partial class MainActivity {

        private void DirectBoxDispatchKeyEvent(object sender, KeyEventArgs e) {
            e.SuppressKeyPress = true; // ignore sound
            switch (e.KeyData) {
                case Keys.Enter:
                    CurrentPath = this.directBox.Text.ToString();
                    if (!CurrentPath.StartsWith("/")) {
                        CurrentPath = "/" + CurrentPath;
                    }
                    RefreshFileList();
                    break;
                case Keys.F5:
                    DispachKeyEvent(sender, e);
                    break;
            }
        }

        private void DispachKeyEvent(object sender, KeyEventArgs e) {
            switch (e.KeyData) {
                case Keys.F5:
                    RefreshFileList();
                    break;
                case Keys.Delete:
                    DeleteSelectedItems();
                    break;
                case Keys.F2:
                    this.listView.SelectedItems[0].BeginEdit();
                    break;
            }
        }
    }
}
