using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndroidExplorer {
    partial class MainActivity {

        private void ListViewSelected(object sender, EventArgs e) {
            FileInfo fileInfo = (FileInfo)this.listView.SelectedItems[0].Tag;
            switch (fileInfo.GetType()) {
                case FileInfo.TYPE_DIR:
                    CurrentPath = fileInfo.GetAbsoluteFilePath();
                    RefreshFileList();
                    break;
                case FileInfo.TYPE_FILE:
                    PullAndEditFile(TMP_PATH, fileInfo);
                    break;
                case FileInfo.TYPE_LINK:
                    SelectLink(fileInfo);
                    break;
            }
        }

        private void onDragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.All;
        }

        private void onDrop(object sender, DragEventArgs e) {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) {
                return;
            }
            ProgressDialog progress = new ProgressDialog();
            var context = TaskScheduler.FromCurrentSynchronizationContext();
            Task task = Task.Factory.StartNew(() => {
                string[] filePath = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (var path in filePath) {
                    if (Directory.Exists(path)) {
                        DirectoryInfo dirInfo = new DirectoryInfo(path);
                        PushFile(path, CurrentPath + "/" + dirInfo.Name);
                    } else if (File.Exists(path)) {
                        PushFile(path, CurrentPath);
                    }
                }
            });
            task.ContinueWith(x => {
                progress.Dispose();
                RefreshFileList();
            }, context);
            progress.ShowDialog(this);
        }

        private void onMenuSelected_SelectFileDownload(object sender, EventArgs e) {
            var items = this.listView.SelectedItems;
            IList<FileInfo> fileInfoList = new List<FileInfo>();
            foreach (ListViewItem item in items) {
                fileInfoList.Add((FileInfo)item.Tag);
            }
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.EnsureReadOnly = false;
            dialog.Multiselect = false;
            dialog.AllowNonFileSystemItems = false;
            var result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok) {
                ProgressDialog progress = new ProgressDialog();
                var context = TaskScheduler.FromCurrentSynchronizationContext();
                Task task = Task.Factory.StartNew(() => {
                    string dest = dialog.FileName;
                    foreach (var fileInfo in fileInfoList) {
                        PullFile(dest, fileInfo);
                    }
                });
                task.ContinueWith(x => {
                    progress.Dispose();
                    RefreshFileList();
                }, context);
                progress.ShowDialog(this);
            }
        }

        private void onMenuSelected_MenuRefresh(object sender, EventArgs e) {
            RefreshFileList();
        }

        private void onMenuSelected_Delete(object sender, EventArgs e) {
            DeleteSelectedItems();
        }

        private void onMenuSelected_Rename(object sender, EventArgs e) {
            this.listView.SelectedItems[0].BeginEdit();
        }
    }
}
