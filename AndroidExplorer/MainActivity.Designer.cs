namespace AndroidExplorer {
    partial class MainActivity {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainActivity));
            this.listView = new System.Windows.Forms.ListView();
            this.fileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.user = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.group = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.link = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.iconList = new System.Windows.Forms.ImageList(this.components);
            this.directBox1 = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.directBox = new System.Windows.Forms.ToolStripTextBox();
            this.statusText = new System.Windows.Forms.ToolStripLabel();
            this.contextMenu.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.AllowDrop = true;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fileName,
            this.user,
            this.group,
            this.size,
            this.date,
            this.link});
            this.listView.ContextMenuStrip = this.contextMenu;
            resources.ApplyResources(this.listView, "listView");
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.LabelEdit = true;
            this.listView.Name = "listView";
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.ListView_AfterLabelEdit);
            this.listView.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.ListView_BeforeLabelEdit);
            this.listView.DragDrop += new System.Windows.Forms.DragEventHandler(this.onDrop);
            this.listView.DragEnter += new System.Windows.Forms.DragEventHandler(this.onDragEnter);
            this.listView.DoubleClick += new System.EventHandler(this.ListViewSelected);
            this.listView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DispachKeyEvent);
            // 
            // fileName
            // 
            resources.ApplyResources(this.fileName, "fileName");
            // 
            // user
            // 
            resources.ApplyResources(this.user, "user");
            // 
            // group
            // 
            resources.ApplyResources(this.group, "group");
            // 
            // size
            // 
            resources.ApplyResources(this.size, "size");
            // 
            // date
            // 
            resources.ApplyResources(this.date, "date");
            // 
            // link
            // 
            resources.ApplyResources(this.link, "link");
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRefresh,
            this.toolStripSeparator1,
            this.menuDownload,
            this.menuDelete,
            this.menuItem_Rename});
            this.contextMenu.Name = "contextMenu";
            resources.ApplyResources(this.contextMenu, "contextMenu");
            // 
            // menuRefresh
            // 
            this.menuRefresh.Name = "menuRefresh";
            resources.ApplyResources(this.menuRefresh, "menuRefresh");
            this.menuRefresh.Click += new System.EventHandler(this.onMenuSelected_MenuRefresh);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // menuDownload
            // 
            this.menuDownload.Name = "menuDownload";
            resources.ApplyResources(this.menuDownload, "menuDownload");
            this.menuDownload.Click += new System.EventHandler(this.onMenuSelected_SelectFileDownload);
            // 
            // menuDelete
            // 
            this.menuDelete.Name = "menuDelete";
            resources.ApplyResources(this.menuDelete, "menuDelete");
            this.menuDelete.Click += new System.EventHandler(this.onMenuSelected_Delete);
            // 
            // menuItem_Rename
            // 
            this.menuItem_Rename.Name = "menuItem_Rename";
            resources.ApplyResources(this.menuItem_Rename, "menuItem_Rename");
            this.menuItem_Rename.Click += new System.EventHandler(this.onMenuSelected_Rename);
            // 
            // iconList
            // 
            this.iconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.iconList, "iconList");
            this.iconList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // directBox1
            // 
            resources.ApplyResources(this.directBox1, "directBox1");
            this.directBox1.Name = "directBox1";
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.directBox,
            this.statusText});
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.TabStop = true;
            // 
            // directBox
            // 
            this.directBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.directBox.Name = "directBox";
            resources.ApplyResources(this.directBox, "directBox");
            this.directBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DirectBoxDispatchKeyEvent);
            // 
            // statusText
            // 
            this.statusText.Name = "statusText";
            resources.ApplyResources(this.statusText, "statusText");
            // 
            // MainActivity
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.listView);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.directBox1);
            this.Name = "MainActivity";
            this.Load += new System.EventHandler(this.onCreate);
            this.Shown += new System.EventHandler(this.onShown);
            this.contextMenu.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader user;
        private System.Windows.Forms.ColumnHeader group;
        private System.Windows.Forms.ColumnHeader size;
        private System.Windows.Forms.ColumnHeader date;
        private System.Windows.Forms.ColumnHeader fileName;
        private System.Windows.Forms.ColumnHeader link;
        private System.Windows.Forms.ImageList iconList;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem menuDownload;
        private System.Windows.Forms.TextBox directBox1;
        private System.Windows.Forms.ToolStripMenuItem menuRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuDelete;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox directBox;
        private System.Windows.Forms.ToolStripLabel statusText;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Rename;
    }
}

