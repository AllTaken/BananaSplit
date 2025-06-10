using BananaSplit.Extensions;
using Manina.Windows.Forms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace BananaSplit
{
    public partial class MainForm : Form, IMessageFilter
    {
        private readonly SettingsForm SettingsForm;
        private readonly QueueManager queueManager;
        private readonly Processor processor;
        private readonly Settings settings;
        private readonly List<object> controlsToLock;

        // P/Invoke declarations
        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point pt);
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        public List<QueueItem> QueueItems { get; set; } = [];

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x20a)
            {
                // WM_MOUSEWHEEL, find the control at screen position m.LParam
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                IntPtr hWnd = WindowFromPoint(pos);
                if (hWnd != IntPtr.Zero && hWnd != m.HWnd && Control.FromHandle(hWnd) != null)
                {
                    SendMessage(hWnd, m.Msg, m.WParam, m.LParam);
                    return true;
                }
            }
            return false;
        }

        public MainForm(SettingsForm settingsForm, QueueManager queueManager, StatusBarManager statusBarManager, LogForm logForm, Processor processor, Settings settings)
        {
            InitializeComponent();
            InitCacheDirectory();

            ReferenceImageListView.MouseWheel += ReferenceImageListViewMouseWheelHandler;
            Application.AddMessageFilter(this);
            SettingsForm = settingsForm;
            queueManager.MainForm = this;
            statusBarManager.MainForm = this;
            this.queueManager = queueManager;
            logForm.MainForm = this;
            this.processor = processor;
            this.settings = settings;
            ApplySettings();
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


        private static void InitCacheDirectory()
        {
            if (Directory.Exists("imagecache"))
                Directory.Delete("imagecache", true); // Clear cache on startup
            Directory.CreateDirectory("imagecache");
        }

        private void ReferenceImageListViewMouseWheelHandler(object sender, MouseEventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
                ReferenceImageListView.ThumbnailSize = new Size(ReferenceImageListView.ThumbnailSize.Width + e.Delta / 10, ReferenceImageListView.ThumbnailSize.Height + e.Delta / 10);
        }

        private void ApplySettings()
        {
            FileBrowserSplitContainer.SplitterDistance = settings.SplitterDistance;
            if (settings.Top.HasValue)
                StartPosition = FormStartPosition.Manual;
            Top = settings.Top ?? Top;
            Left = settings.Left ?? Left;
            Width = settings.Width ?? Width;
            Height = settings.Height ?? Height;
            WindowState = settings.WindowState ?? WindowState;
            ReferenceImageListView.ThumbnailSize = new(settings.ThumbnailSize, settings.ThumbnailSize);
            queueManager.AutoSizeQueueList(QueueList, new());
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
            if (QueueList.SelectedItems.Count != 1)
            {
                ReferenceImageListView.Items.Clear();
                return;
            }

            var selectedItem = (QueueItem)QueueList.SelectedItems[0].Tag;

            foreach (var frame in selectedItem.BlackFrames)
            {
                if (frame.ReferenceFrame.ImageFile != null)
                {
                    var item = new ImageListViewItem(frame.ReferenceFrame.ImageFile, frame.End.ToString("g"))
                    {
                        Tag = frame,
                        Checked = frame.Selected
                    };

                    ReferenceImageListView.Items.Add(item);
                }
            }
        }

        private void OpenSettingsForm(object sender, EventArgs e)
        {
            SettingsForm.Show();
        }

        private void ReferenceImageListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in ReferenceImageListView.Items)
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

        internal void SaveSettings()
        {
            settings.WindowState = WindowState;
            WindowState = FormWindowState.Normal; // Ensure the window is not minimized or maximized when saving position and size
            settings.Top = Top;
            settings.Left = Left;
            settings.Width = Width;
            settings.Height = Height;
            settings.SplitterDistance = FileBrowserSplitContainer.SplitterDistance;
            settings.ThumbnailSize = ReferenceImageListView.ThumbnailSize.Width;
            settings.Save();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
            if (Directory.Exists("imagecache"))
                Directory.Delete("imagecache", true); // Clear cache on shutdown
        }

        private void ReferenceImageListView_ItemCheckBoxClick(object sender, ItemEventArgs e)
        {
            ((BlackFrame)e.Item.Tag).Selected = e.Item.Checked;
        }

        private void ReferenceImageListView_MouseEnter(object sender, EventArgs e)
        {
            HintLabel.Text = "Hold Ctrl and scroll to change thumbnail size.";
            if (vlcPath != null)
            {
                HintLabel.Text += " Double-click to open in VLC.";
            }
            else
            {
                HintLabel.Text += " VLC not found. Double-click will not work.";
            }
        }

        private void ReferenceImageListView_MouseLeave(object sender, EventArgs e)
        {
            HintLabel.Text = "";
        }

        }
    }
}
