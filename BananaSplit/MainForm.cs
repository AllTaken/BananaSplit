using BananaSplit.Extensions;

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace BananaSplit
{
    public partial class MainForm : Form
    {
        private readonly SettingsForm SettingsForm;
        private readonly QueueManager queueManager;
        private readonly Processor processor;
        private readonly List<object> controlsToLock;

        public List<QueueItem> QueueItems { get; set; } = [];

        public MainForm(SettingsForm settingsForm, QueueManager queueManager, StatusBarManager statusBarManager, LogForm logForm, Processor processor)
        {
            InitializeComponent();            
            SettingsForm = settingsForm;
            queueManager.MainForm = this;
            statusBarManager.MainForm = this;
            this.queueManager = queueManager;
            logForm.MainForm = this;
            this.processor = processor;
            controlsToLock = [
                ProcessQueueButton,
                QueueListContextMenuProcess,
                QueueItemContextMenuProcess,
                QueueListContextMenuRemove,
                QueueItemContextMenuRemove,
                AddFilesToQueueMenuItem,
                AddFolderToQueueMenuItem
            ];
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Menu Items
            AddFilesToQueueMenuItem.Click += queueManager.AddFilesToQueueDialog;
            AddFolderToQueueMenuItem.Click += queueManager.AddFolderToQueueDialog;
            SettingsMenuItem.Click += OpenSettingsForm;

            // Queue List
            QueueList.SelectedIndexChanged += RenderReferenceImagesListView;
            QueueList.MouseUp += queueManager.OpenQueueItemContextMenu;
            QueueList.KeyDown += QueueListKeyDownHandler;
            QueueItemContextMenuProcess.Click += ProcessQueueItem;
            QueueItemContextMenuRemove.Click += queueManager.RemoveQueueItem;
            QueueListContextMenuProcess.Click += ProcessQueue;
            QueueListContextMenuRemove.Click += queueManager.RemoveQueueList;

            // Other
            ProcessQueueButton.Click += ProcessQueue;
            QueueList.Resize += queueManager.AutoSizeQueueList;

            // Drag and Drop
            this.AllowDrop = true;
            this.DragOver += QueueManager.SetDragOverEffect;
            this.DragDrop += queueManager.AddDragDropItemsToQueue;
        }

        private void LockControls(bool enable)
        {
            Invoke(
                new MethodInvoker(
                    delegate ()
                    {
                        ToggleControlsEnabled(enable);
                    }
                )
            );
        }

        private void ToggleControlsEnabled(bool enable)
        {
            foreach (var controlToLock in controlsToLock)
            {
                if (controlToLock is ToolStripMenuItem menuItem)
                {
                    menuItem.Enabled = enable;
                    continue;
                }
                if (controlToLock is Control control)
                {
                    control.Enabled = enable;
                }
                AllowDrop = enable;
            }
            Cursor.Current = enable ? Cursors.Default : Cursors.WaitCursor;
            Cursor = enable ? Cursors.Default : Cursors.WaitCursor;
        }

        private void RenderReferenceImagesListView(object sender, EventArgs e)
        {
            if (QueueList.SelectedItems.Count <= 0)
            {
                ReferenceImageListView.Clear();
                return;
            }

            var selectedItem = (QueueItem)QueueList.SelectedItems[0].Tag;

            foreach (var frame in selectedItem.BlackFrames)
            {
                if (frame.ReferenceFrame.Bitmap != null)
                {
                    var bmp = frame.ReferenceFrame.Bitmap;

                    ReferenceImageList.Add(bmp, frame.Id.ToString());

                    ReferenceImageListView.Items.Add(new ListViewItem()
                    {
                        ImageKey = frame.Id.ToString(),
                        Tag = frame,
                        Name = frame.Id.ToString(),
                        Text = frame.End.ToString(),
                        Checked = frame.Selected
                    });
                }
            }
        }

        private void OpenSettingsForm(object sender, EventArgs e)
        {
            SettingsForm.Show();
        }

        private void ReferenceImageListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in ReferenceImageListView.Items)
            {
                if (item.Selected)
                {
                    item.Selected = false;
                    item.Checked = !item.Checked;

                    ((BlackFrame)item.Tag).Selected = item.Checked;
                }
            }
        }

        private void ProcessQueue(object sender, EventArgs e)
        {
            processor.ProcessQueue(() => LockControls(false), () => LockControls(true), QueueItems);
        }

        private void ProcessQueueItem(object sender, EventArgs e)
        {
            processor.ProcessQueueItem(() => LockControls(false), () => LockControls(true), (QueueItem)QueueItemContextMenu.Tag);
        }

        private void QueueListKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (ListViewItem selectedItem in QueueList.SelectedItems)
                {
                    QueueItem queueItem = selectedItem.Tag as QueueItem;

                    QueueList.Items.Remove(selectedItem);
                    QueueItems.Remove(queueItem);
                }
            }
        }
    }
}
