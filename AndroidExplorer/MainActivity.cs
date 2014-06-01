using AndroidExplorer.Properties;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndroidExplorer {
    public partial class MainActivity : Form {

        private String TMP_PATH = Path.GetTempPath();
        private string CurrentPath = "/";
        private ImageList ImageList = new ImageList();

        public MainActivity() {
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            InitializeComponent();
        }

        private void onCreate(object sender, EventArgs e) {
            InitializeImageList();
            InitializeFileSystem();
        }

        private void InitializeImageList() {
            ImageList.Images.Add("/dir/", IconUtils.getFolderIcon(TMP_PATH));
            ImageList.Images.Add("/default/", IconUtils.FileAssociatedImage("default", false, false));

            Image image1 = ImageList.Images[1];
            Image image2 = new Bitmap(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("AndroidExplorer.shortcut_icon16.png"));
            Image icon = new Bitmap(image1);
            Graphics g = Graphics.FromImage(icon);
            g.DrawImage(image2, 0, image1.Height - image2.Height, image2.Width, image2.Height);
            g.Dispose();
            ImageList.Images.Add("/link/", icon);
            image1.Dispose();
            image2.Dispose();
        }

        private void InitializeFileSystem() {
            RefreshFileList();
        }

        private void RefreshFileList() {
            this.listView.Items.Clear();
            IList<FileInfo> fileInfoList = FileListFactory.createFileList(DoCommand.Exec("adb shell ls -l \"" + CurrentPath + "\""), CurrentPath);
            foreach (FileInfo fileInfo in fileInfoList) {
                string fileName = fileInfo.GetFileName();
                string ext = Path.GetExtension(fileName);
                if (!ImageList.Images.ContainsKey(ext)) {
                    ImageList.Images.Add(ext, IconUtils.FileAssociatedImage(fileName, false, false));
                }
                string[] row = {
                                   fileInfo.GetFileName(),
                                   fileInfo.GetUser(),
                                   fileInfo.GetGroup(),
                                   fileInfo.GetSize() == -1 ? "" : fileInfo.GetSize().ToString(),
                                   fileInfo.GetTime(),
                                   fileInfo.GetLink()
                               };
                ListViewItem item = new ListViewItem(row);
                switch (fileInfo.GetType()) {
                    case FileInfo.TYPE_DIR:
                        item.ImageIndex = 0;
                        break;
                    case FileInfo.TYPE_FILE:
                        if (ext == string.Empty) {
                            ext = "/default/";
                        }
                        item.ImageIndex = ImageList.Images.IndexOfKey(ext);
                        break;
                    case FileInfo.TYPE_LINK:
                        item.ImageIndex = ImageList.Images.IndexOfKey("/link/");
                        break;
                }
                item.Tag = fileInfo;
                this.listView.Items.Add(item);
            }
            this.listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.listView.SmallImageList = ImageList;
            this.listView.LargeImageList = ImageList;
            this.statusText.Text = "Location : " + CurrentPath;
            ForcusDirectBox();
        }

        private void SelectLink(FileInfo fileInfo) {
            // check File or Dir
            IList<FileInfo> fileInfoList = FileListFactory.createFileList(DoCommand.Exec("adb shell ls -ald \"" + fileInfo.GetLink() + "\""), CurrentPath);
            if (fileInfoList.Count != 2) {
                MessageBox.Show(Resources.UnknownError + " \nfilelist count : " + fileInfoList.Count, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FileInfo linkFileInfo = fileInfoList[1];
            switch (linkFileInfo.GetType()) {
                case FileInfo.TYPE_DIR:
                    CurrentPath = fileInfo.GetLink();
                    RefreshFileList();
                    break;
                case FileInfo.TYPE_FILE:
                    linkFileInfo.SetAbsoluteFilePath(fileInfo.GetLink());
                    PullAndEditFile(TMP_PATH, linkFileInfo);
                    break;
                case FileInfo.TYPE_LINK:
                    SelectLink(linkFileInfo);
                    break;
            }
        }

        private string PullFile(String destPath, FileInfo fileInfo) {
            string destFileAbsolutePath = destPath + "/" + fileInfo.GetFileName();
            string srcPath = fileInfo.GetType() != FileInfo.TYPE_LINK ? fileInfo.GetAbsoluteFilePath() : fileInfo.GetLink();
            return DoCommand.Exec("adb pull \"" + srcPath + "\" \"" + destFileAbsolutePath + "\"");
        }

        private void PullAndEditFile(string destPath, FileInfo fileInfo) {
            if (!Directory.Exists(destPath)) {
                Directory.CreateDirectory(destPath);
            }

            string destFileAbsolutePath = destPath + fileInfo.GetFileName();
            string result = PullFile(destPath, fileInfo);
            bool isModifiedFile = StartProcess(destFileAbsolutePath);
            if (isModifiedFile) {

                DialogResult dialogResult = MessageBox.Show(Resources.DetectedFileHasModified, Resources.Confirm, MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes) {
                    ProgressDialog progress = new ProgressDialog();
                    var context = TaskScheduler.FromCurrentSynchronizationContext();
                    Task task = Task.Factory.StartNew(() => {
                        PushFile(destFileAbsolutePath, fileInfo);
                    });
                    task.ContinueWith(x => {
                        progress.Dispose();
                        RefreshFileList();
                    }, context);
                    progress.ShowDialog(this);
                }
            }
        }

        private bool StartProcess(string path) {
            string beforeMd5 = FileUtils.md5(path);
            if (beforeMd5 == null || beforeMd5 == string.Empty) {
                MessageBox.Show(Resources.FilePullFailed, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            System.Diagnostics.Process p = System.Diagnostics.Process.Start(path);
            if (p != null) { 
                p.WaitForExit();
                return false;
            }  

            string afterMd5 = FileUtils.md5(path);
            if (beforeMd5 == null || afterMd5 == string.Empty) {
                MessageBox.Show(Resources.UnableToDetectThatTheFileHasChanged, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return !beforeMd5.Equals(afterMd5);
        }

        private void PushFile(string srcPath, FileInfo fileInfo) {
            PushFile(srcPath, fileInfo.GetAbsoluteFilePath());
        }

        private void PushFile(string srcPath, string destPath) {
            DoCommand.Exec("adb push \"" + srcPath + "\" \"" + destPath + "\"");
        }

        private void DeleteSelectedItems() {
            System.Windows.Forms.ListView.SelectedListViewItemCollection selectedItems = listView.SelectedItems;
            if (selectedItems.Count <= 0) {
                return;
            }
            DialogResult result = MessageBox.Show(Resources.DeleteConfirm, Resources.Confirm, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK) {
                foreach (ListViewItem item in selectedItems) {
                    FileInfo fileInfo = (FileInfo)item.Tag;
                    DoCommand.Exec("adb shell rm -r \"" + fileInfo.GetAbsoluteFilePath() + "\"");
                }
                RefreshFileList();
            }
        }

        private void ForcusDirectBox() {
            this.directBox.Focus();
            this.directBox.Select(directBox.Text.Length, 0);
        }

        private void onShown(object sender, EventArgs e) {
            ForcusDirectBox();
        }

        private void ListView_BeforeLabelEdit(object sender, LabelEditEventArgs e) {
            FileInfo item = (FileInfo)this.listView.Items[e.Item].Tag;
            if (item.GetFileName().Equals("/") || item.GetFileName().Equals("..")) {
                e.CancelEdit = true;
            }
        }

        private void ListView_AfterLabelEdit(object sender, LabelEditEventArgs e) {
            if (e.Label == null) {
                return;
            }
            foreach (ListViewItem item in this.listView.Items) {
                if (item.Index != e.Item && item.Text.Equals(e.Label)) {
                    MessageBox.Show(Resources.ItemAlreadyExist, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.CancelEdit = true;
                    return;
                }
            }
            FileInfo fileInfo = (FileInfo) this.listView.Items[e.Item].Tag;
            // Rename
            DoCommand.Exec("adb shell mv \"" + fileInfo.GetAbsoluteFilePath() + "\" " + "\"" + FileUtils.getParentDirPath(fileInfo.GetAbsoluteFilePath()) + "/" + e.Label + "\"");
            RefreshFileList();
        }
    }
}
