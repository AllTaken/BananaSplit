using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BananaSplit;
public class QueueManager(Scanner scanner)
{

    public MainForm MainForm { get; set; }

    private readonly string[] supportedExtensions =
    [
        ".avi",
            ".flv",
            ".m4p",
            ".m4v",
            ".mkv",
            ".mov",
            ".mp2",
            ".mp4",
            ".mpe",
            ".mpeg",
            ".mpg",
            ".mpv",
            ".ogg",
            ".ts",
            ".webm",
            ".wmv"
    ];

    //  QueueItemContextMenuRemove
    public void RemoveQueueItem(object sender, EventArgs e)
    {
        QueueItem queueItem = (QueueItem)MainForm.QueueItemContextMenu.Tag;

        MainForm.QueueList.Items.RemoveByKey(queueItem.Id.ToString());
        MainForm.QueueItems.Remove(queueItem);
    }

    // QueueListContextMenuRemove
    public void RemoveQueueList(object sender, EventArgs e)
    {
        foreach (var queueItem in MainForm.QueueItems)
        {
            MainForm.QueueList.Items.RemoveByKey(queueItem.Id.ToString());
        }

        MainForm.QueueItems.Clear();
    }

    public void AutoSizeQueueList(object sender, EventArgs e)
    {
        MainForm.QueueList.Columns[0].Width = MainForm.QueueList.Width - 4;
    }

    public void OpenQueueItemContextMenu(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right)
            return;

        if (MainForm.QueueList.FocusedItem != null && MainForm.QueueList.FocusedItem.Bounds.Contains(e.Location))
        {
            MainForm.QueueItemContextMenu.Tag = MainForm.QueueList.FocusedItem.Tag;
        }

        MainForm.QueueListContextMenu.Show(Cursor.Position);
    }

    public void AddFilesToQueueDialog(object sender, EventArgs e)
    {
        string[] files;
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Filter = $"Video Files (*{string.Join(",*", supportedExtensions)})|*{string.Join(";*", supportedExtensions)}";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            files = openFileDialog.FileNames;
        }

        AddFilesToQueue(files);
    }

    public void AddFolderToQueueDialog(object sender, EventArgs e)
    {
        string[] files;

        using (FolderBrowserDialog openFolderDialog = new FolderBrowserDialog())
        {
            var result = openFolderDialog.ShowDialog();

            if (result != DialogResult.OK || string.IsNullOrWhiteSpace(openFolderDialog.SelectedPath))
                return;

            files = Directory.GetFiles(openFolderDialog.SelectedPath);
        }

        AddFilesToQueue(files);
    }

    public void AddFilesToQueue(string[] files)
    {
        if (files == null || files.Length == 0 || !files.Select(AddToQueue).ToList().Any(added => true))
            return;

        scanner.StartScanningThread(AddItemsToQueueList, MainForm.QueueItems);
    }

    public bool AddToQueue(string path)
    {
        if (File.Exists(path) && supportedExtensions.Contains(Path.GetExtension(path).ToLowerInvariant()))
        {
            MainForm.QueueItems.Add(new QueueItem(path));
            return true;
        }

        
        if (Directory.Exists(path))
        {
            bool addedAnything = false;
            foreach (var file in Directory.GetFiles(path))
                addedAnything |= AddToQueue(file);

            foreach (var folder in Directory.GetDirectories(path))
                addedAnything |= AddToQueue(folder);
            return addedAnything;
        }

        return false;
    }

    public static void SetDragOverEffect(object sender, DragEventArgs e)
    {
        e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Link : DragDropEffects.None;
    }

    public void AddDragDropItemsToQueue(object sender, DragEventArgs e)
    {
        var files = ((DataObject)e.Data).GetFileDropList().Cast<string>().ToArray();

        AddFilesToQueue(files);
    }

    public void AddItemsToQueueList(List<QueueItem> items)
    {
        MainForm.QueueList.Invoke(
            new MethodInvoker(
                delegate ()
                {
                    foreach (var item in items)
                    {
                        MainForm.QueueList.Items.Add(new ListViewItem()
                        {
                            Text = Path.GetFileName(item.FileName),
                            ToolTipText = item.FileName,
                            Name = item.Id.ToString(),
                            Tag = item
                        });
                    }
                }
            )
        );
    }
}
